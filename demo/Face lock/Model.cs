using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System.IO;

namespace Face_lock
{
    class Model
    {
        Capture _capture;
        Mat _sourceMat;
        private readonly IFaceServiceClient _faceServiceClient = new FaceServiceClient("6f3d49e1c3114f3bafff32b821adf874");
        public static readonly string personGroupId = Guid.NewGuid().ToString();
        FaceAttributeType[] _requiedFaceAttributes = new FaceAttributeType[]
        {
            FaceAttributeType.Age,
            FaceAttributeType.Gender,
            FaceAttributeType.Smile,
        };

        public Model()
        {
            _capture = new Capture(0);
            _capture.FlipHorizontal = !_capture.FlipHorizontal;
            _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps, 30);
            _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth, 320);
            _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight, 240);
            _sourceMat = _capture.QueryFrame();
        }

        public Bitmap GetSourceBitmap()
        {
            if (_sourceMat != null)
            {
                _sourceMat.Dispose();
            }
            _sourceMat = _capture.QueryFrame();
            return _sourceMat.Bitmap;
        }

        public async Task ProcessTraining(string selectPath)
        {
            await DeleteExistPersonGroup();
            await CreatePersonGroup();
            await CreatePersons(selectPath);
            await TrainPersonGroup();
            await GetTrainStatus();
        }

        public async void ProcessIdentify(string catchPath)
        {
            Console.WriteLine(catchPath);
            using (Stream s = File.OpenRead(catchPath))
            {
                try
                {
                    var faces = await _faceServiceClient.DetectAsync(s,returnFaceLandmarks:true,returnFaceAttributes:_requiedFaceAttributes);
                    s.Close();
                    Dictionary<Guid, ExFace> queryFaceMap = new Dictionary<Guid, ExFace>();
                    faces.ToList().ForEach(face => queryFaceMap.Add(face.FaceId, new ExFace(face)));
                    IdentityResultForm resultForm = new IdentityResultForm();
                    resultForm.Show();
                    resultForm.ShowDetectFaces(catchPath, queryFaceMap.Values.ToList());
                    var faceIds = faces.Select(face => face.FaceId).ToArray();
                    var results = await _faceServiceClient.IdentifyAsync(personGroupId, faceIds);
                    bool isOpen = false;
                    foreach (var identifyResult in results)
                    {
                        Console.WriteLine("Result of face: {0}", identifyResult.FaceId);
                        if (identifyResult.Candidates.Length == 0)
                        {
                            Console.WriteLine("No one identified");
                            queryFaceMap[identifyResult.FaceId].PersonName = ExFace.UNKNOWN;
                        }
                        else
                        {
                            // Get top 1 among all candidates returned
                            var candidateId = identifyResult.Candidates[0].PersonId;
                            var person = await _faceServiceClient.GetPersonAsync(personGroupId, candidateId);
                            Console.WriteLine("Identified as {0}", person.Name);
                            queryFaceMap[identifyResult.FaceId].PersonName = person.Name;
                            isOpen = true;
                        }
                    }
                    resultForm.SetDoor(isOpen);
                    resultForm.ShowIdentityFaces(queryFaceMap.Values.ToList());
                }
                catch (FaceAPIException ex)
                {
                    Console.WriteLine("Response: {0}. {1}", ex.ErrorCode, ex.ErrorMessage);
                }
            }
        }

        private async Task GetTrainStatus()
        {
            try
            {
                TrainingStatus trainingStatus = null;
                while (true)
                {
                    trainingStatus = await _faceServiceClient.GetPersonGroupTrainingStatusAsync(personGroupId);
                    if (trainingStatus.Status != Status.Running)
                    {
                        break;
                    }
                    await Task.Delay(1000);
                    Console.WriteLine("GetPersonGroupTrainingStatusAsync");
                }
                //-----------------------------------------------------------------------------------------------------
                //_identityButton.Enabled = true;
                Console.WriteLine(trainingStatus.Status);
            }
            catch (FaceAPIException ex)
            {
                Console.WriteLine("Response: {0}. {1}", ex.ErrorCode, ex.ErrorMessage);
            }
        }

        private async Task TrainPersonGroup()
        {
            await _faceServiceClient.TrainPersonGroupAsync(personGroupId);
            Console.WriteLine("TrainPersonGroupAsync");
        }

        private async Task CreatePersons(string selectPath)
        {
            Console.WriteLine(selectPath);
            foreach (var dir in Directory.EnumerateDirectories(selectPath))
            {
                Console.WriteLine(dir);
                Console.WriteLine(Path.GetFileName(dir));
                // Define Anna
                CreatePersonResult friend = await _faceServiceClient.CreatePersonAsync(
                    // Id of the person group that the person belonged to
                    personGroupId,
                    // Name of the person
                    Path.GetFileName(dir)
                );
                Console.WriteLine("CreatePersonAsync--------------------------" + Path.GetFileName(dir));

                foreach (var file in Directory.EnumerateFiles(dir, "*.jpg", SearchOption.AllDirectories))
                {
                    Console.WriteLine(file);
                    Console.WriteLine(Path.GetFileName(file));
                    // Directory contains image files of folder
                    using (Stream s = File.OpenRead(file))
                    {
                        // Detect faces in the image and add to Anna
                        await _faceServiceClient.AddPersonFaceAsync(personGroupId, friend.PersonId, s);
                        Console.WriteLine("AddPersonFaceAsync------------------------------" + file);
                    }
                }
            }
        }

        private async Task CreatePersonGroup()
        {
            try
            {
                await _faceServiceClient.CreatePersonGroupAsync(personGroupId, "My Friends");
                Console.WriteLine("Response: Success. Group \"{0}\" created", "My Friends");
            }
            catch (FaceAPIException ex)
            {
                Console.WriteLine("Response: {0}. {1}", ex.ErrorCode, ex.ErrorMessage);
                return;
            }
            Console.WriteLine("CreatePersonGroupAsync");
        }

        private async Task DeleteExistPersonGroup()
        {
            bool groupExists = false;
            // Test whether the group already exists
            try
            {
                Console.WriteLine("Request: Group {0} will be used for build person database. Checking whether group exists.", personGroupId);
                await _faceServiceClient.GetPersonGroupAsync(personGroupId);
                groupExists = true;
                Console.WriteLine("Response: Group {0} exists.", personGroupId);
            }
            catch (FaceAPIException ex)
            {
                if (ex.ErrorCode != "PersonGroupNotFound")
                {
                    Console.WriteLine("Response: {0}. {1}", ex.ErrorCode, ex.ErrorMessage);
                    return;
                }
                else
                {
                    Console.WriteLine("Response: Group {0} does not exist before.", personGroupId);
                }
            }
            if (groupExists)
            {
                await _faceServiceClient.DeletePersonGroupAsync(personGroupId);
            }
        }
    }
}

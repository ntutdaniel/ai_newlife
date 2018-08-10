using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Face_lock
{
    public partial class IdentityResultForm : Form
    {
        Image<Bgr, byte> _image;

        public IdentityResultForm()
        {
            InitializeComponent();
        }

        public void ShowDetectFaces(string catchPath, List<ExFace> list)
        {
            _image = new Image<Bgr, byte>(catchPath);
            foreach (ExFace exFace in list)
            {
                _image.Draw(exFace.Rectangle, new Bgr(Color.Blue), 5);
            }
            _catchPictureBox.Image = _image.Bitmap;
        }

        public void ShowIdentityFaces(List<ExFace> list)
        {
            foreach (ExFace exFace in list)
            {
                Point point = new Point(exFace.Rectangle.X, exFace.Rectangle.Y);
                _image.Draw(exFace.PersonName, point, Emgu.CV.CvEnum.FontFace.HersheyTriplex, 1, new Bgr(Color.White));
                if (exFace.PersonName != ExFace.UNKNOWN)
                {                   
                    _image.Draw(exFace.Rectangle, new Bgr(Color.Green), 5);                    
                }
                else
                {
                    _image.Draw(exFace.Rectangle, new Bgr(Color.Red), 5);
                }
                Bgr dotColor = new Bgr(Color.White);                
                Image<Bgr, byte> image = _image.Clone();
                image.ROI = exFace.Rectangle;
                var attribute = exFace.Face.FaceAttributes;
                _dataGridView.Rows.Add(image.Bitmap,exFace.PersonName, attribute.Gender, attribute.Age, attribute.Smile);
                foreach (Point p in exFace.LandmarkPointList)
                {
                    //_image.Draw(new CircleF(p,2), dotColor);
                    _image.Draw(new CircleF(p, 2), dotColor, 1);
                }

                /*
                
                point.Y += 40;
                _image.Draw(exFace.Face.FaceAttributes.Age.ToString(), point, Emgu.CV.CvEnum.FontFace.HersheyTriplex, 1, new Bgr(Color.White));
                point.Y += 40;
                _image.Draw(exFace.Face.FaceAttributes.Gender.ToString(), point, Emgu.CV.CvEnum.FontFace.HersheyTriplex, 1, new Bgr(Color.White));
                point.Y += 40;
                _image.Draw(exFace.Face.FaceAttributes.Glasses.ToString(), point, Emgu.CV.CvEnum.FontFace.HersheyTriplex, 1, new Bgr(Color.White));
                */
            }
            _catchPictureBox.Image = _image.Bitmap;
        }

        public void SetDoor(bool isOpen)
        {
            if (isOpen)
            {
                _doorLabel.Text = "Welcome";
                _doorLabel.BackColor = Color.Green;
            }
            else
            {
                _doorLabel.Text = "Door Close";
                _doorLabel.BackColor = Color.Red;
            }
        }
    }
}

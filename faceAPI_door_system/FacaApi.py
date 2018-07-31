import requests
import cognitive_face as CF
from imgur import upload_photo
from files import *
import json


class FaceApi:
    uri_base = 'https://westus.api.cognitive.microsoft.com/face/v1.0/'
    subscription_key = '6f3d49e1c3114f3bafff32b821adf874'

    CF.Key.set(subscription_key)
    CF.BaseUrl.set(uri_base)

    dataset_detect = []

    def __init__(self):
        pass

    def detect(self, path):
        temp_link = upload_photo(path)
        faces = CF.face.detect(temp_link, attributes = 'age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise')

        print('Response:')

        if(len(faces) == 0):
            print('no face detected')
        
        return faces

    def train(self, path, faces):
        self.dataset_detect = []

        # 打開資料夾
        temp_dict = getFaceDict(path)
        temp_list = []

        for dic, imgs in temp_dict.items():
            temp_arrays = []
            for img in imgs:
                temp_link = upload_photo(img)
                # face detect 
                temp_id = (CF.face.detect(temp_link))
                if(len(temp_id)!=0):
                    self.dataset_detect.append({'faceId': temp_id[0]["faceId"], 'name': getDirName(dic)})
                    temp_arrays.append(temp_id[0]["faceId"])
            temp_list += temp_arrays
        
        # print(self.dataset_detect)

        # group
        face_ids = []
        for f in faces:
            face_ids.append(f["faceId"])
        temp_list += face_ids

        body = {"faceIds":temp_list}

        headers = {
            'Content-Type': 'application/json',
            'Ocp-Apim-Subscription-Key': self.subscription_key,
        }

        # Request parameters.
        params = {
            'returnFaceId': 'true',
            'returnFaceLandmarks': 'false',
            'returnFaceAttributes': 'age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise',
        }

        response = requests.request('POST', self.uri_base + 'group', json=body, data=None, headers=headers, params=params)

        parsed = response.json()
        # print(json.dumps(parsed, sort_keys=True, indent=2))

        # match
        match_list = list()
        for i in range(len(parsed['groups'])):
            match_list.append(parsed['groups'][i])

        return match_list, faces, self.dataset_detect



import requests

class FaceApi:
    uri_base = 'https://westus.api.cognitive.microsoft.com'
    subscription_key = '6f3d49e1c3114f3bafff32b821adf874'

    headers = {
        'Content-Type': 'application/octet-stream',
        'Ocp-Apim-Subscription-Key': subscription_key,
    }

    params = {
        'returnFaceId': 'true',
        'returnFaceLandmarks': 'false',
        'returnFaceAttributes': 'age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise',
    }

    path_to_face_api = '/face/v1.0/detect'

    def __init__(self, path):
        self.path = path

    def upload(self):
        with open(self.path, 'rb') as f:
            img_data = f.read()

        try:
            response = requests.post(self.uri_base + self.path_to_face_api,
                                     data=img_data,
                                     headers=self.headers,
                                     params=self.params)

            print('Response:')

            parsed = response.json()

            if(len(parsed) == 0):
                print('no face detected')

            return parsed


        except Exception as e:
            print('Error:')
            print(e)


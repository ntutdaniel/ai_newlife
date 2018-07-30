import requests


class FaceApi:
    uri_base = 'https://westus.api.cognitive.microsoft.com'
    subscription_key = '6f3d49e1c3114f3bafff32b821adf874'

    detect_face_api = '/face/v1.0/detect'
    verify_face_api = '/face/v1.0/verify'

    def __init__(self):
        pass

    def detect(self, path):
        headers = {
            'Content-Type': 'application/octet-stream',
            'Ocp-Apim-Subscription-Key': self.subscription_key,
        }

        params = {
            'returnFaceId': 'true',
            'returnFaceLandmarks': 'false',
            'returnFaceAttributes': 'age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise',
        }

        with open(path, 'rb') as f:
            img_data = f.read()

        try:
            response = requests.post(self.uri_base + self.detect_face_api,
                                     data=img_data,
                                     headers=headers,
                                     params=params)

            print('Response:')

            parsed = response.json()

            print(parsed)

            if (len(parsed) == 0):
                print('no face detected')

            return parsed


        except Exception as e:
            print('Error:')
            print(e)

    def verify(self, face1, face2):
        print(face1)
        print(face2)
        headers = {
            # Request headers
            'Content-Type': 'application/json',
            'Ocp-Apim-Subscription-Key': self.subscription_key,
        }

        json = {
            'faceId1': face1,
            'faceId2': face2
        }

        try:
            response = requests.post(self.uri_base + self.verify_face_api,
                                     headers=headers,
                                     json=json)

            print('Response:')

            parsed = response.json()

            print(parsed)

            if (len(parsed) == 0):
                print('no face detected')

            return parsed


        except Exception as e:
            print('Error:')
            print(e)

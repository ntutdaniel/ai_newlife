import tkinter
import tkinter.ttk as ttk
import cv2
import PIL.Image, PIL.ImageTk, PIL.ImageDraw, PIL.ImageFont
import time
from FacaApi import FaceApi
import threading
import json
from io import BytesIO


class identifyApp:
    def __init__(self, window, window_title, frame=None, canva_info=None):
        self.window = window
        self.window.title(window_title)
        self.frame = frame

        self.window.geometry("640x520")  # You want the size of the app to be 500x500
        self.window.resizable(1, 1)

        if (canva_info == None):
            self.width = 640
            self.height = 480
        else:
            self.width = canva_info["width"]
            self.height = canva_info["height"]

        # identity canva
        self.canvas = tkinter.Canvas(window, width=self.width, height=self.height)
        self.canvas.pack()

        self.info_labelText = tkinter.StringVar()
        self.info = tkinter.Label(window, textvariable=self.info_labelText)
        self.info.pack()

        self.updateLabelText(self.info_labelText, "processing...")
        #self.delay = 10
        #self.identify()
        self.thread_way()

        self.window.mainloop()

    def thread_way(self):
        th = threading.Thread(target=self.identify, args=())
        th.setDaemon(True)
        th.start()

    def identify(self):
        filename = "./photo/frame" + time.strftime("%d%m%Y%H%M%S") + ".jpg"
        cv2.imwrite(filename,
                    cv2.cvtColor(self.frame, cv2.COLOR_RGB2BGR))

        face = FaceApi()
        face_info = face.detect2(filename)
        print(json.dumps(face_info, sort_keys=True, indent=2))

        # train
        match_id, faceDictionary, dataset_detect = face.train('./train', face_info)

        if (len(face_info) == 0):
            self.updateLabelText(self.info_labelText, "no face detected")
        else:
            self.updateLabelText(self.info_labelText, "face detected")

        new_frame = self.drawRectangle(filename, match_id, faceDictionary, dataset_detect)

        self.photo = PIL.ImageTk.PhotoImage(image=new_frame)
        self.canvas.create_image(0, 0, image=self.photo, anchor=tkinter.NW)

    def updateLabelText(self, label_txt, txt):
        label_txt.set(txt)

    def drawRectangle(self, path, match_id, faceDictionary, dataset_detect):
        newimg = PIL.Image.open(path)
        draw = PIL.ImageDraw.Draw(newimg)
        # 判斷其大小
        size = len(str(newimg.size[0]))
        # 根據大小分配字體大小和字的位置
        if size >= 4:
            fs = 50
            ps = 130
        else:
            fs = 20
            ps = 98

        # 圖片的字體和顏色
        font = PIL.ImageFont.truetype("./font/FreeMono.ttf", fs)
        draw.ink = 255 + 0 * 256 + 0 * 256 * 256

        # 給每個識別出的人臉畫框、並標識年齡
        for a in faceDictionary:
            b1 = a[u'faceRectangle']
            c1 = self.getRectangle(b1)
            draw.rectangle(c1, outline='red')

        # test
        showname = ''

        for id in match_id:
            find_match_location = 0
            find_match_name = 0

            # find the name
            for data in dataset_detect:
                if data['faceId'] in set(id):
                    showname = data['name']
                    find_match_name = 1
            
            # find the location
            temp_face_info = {}
            for face in faceDictionary:
                if face['faceId'] in set(id):
                    temp_face_info = face
                    find_match_location = 1
            print(find_match_name,find_match_location)

            # if find the location and name
            if (find_match_location == 1) and (find_match_name == 1):
                print('match!!')
                # set font
                b2 = temp_face_info[u'faceRectangle']
                c2 = self.getRectangle(b2)
                draw.rectangle(c2, outline='green')
                temp_text = "Name=" + showname + "\n" + "Age=" + str(temp_face_info[u'faceAttributes'][u'age']) + "\n" + "Gender=" + temp_face_info[u'faceAttributes'][u'gender'] + "\n" + "Smile=" + str(temp_face_info[u'faceAttributes'][u'smile'])+ "\n" + "Glasses=" + temp_face_info[u'faceAttributes'][u'glasses']
                draw.text([c2[0][0], c2[0][1] - ps], temp_text, font = font, fill=(255,0,0,255))
        return newimg

    def getRectangle(self, mydata):
        left = mydata[u'left']
        top = mydata[u'top']
        bottom = left + mydata[u'height']
        right = top + mydata[u'width']
        return ((left, top), (bottom, right))

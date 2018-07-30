import tkinter
import tkinter.ttk as ttk
import cv2
import PIL.Image, PIL.ImageTk, PIL.ImageDraw, PIL.ImageFont
import time
from FacaApi import FaceApi
import threading
import tkinter.messagebox


class verifyApp:
    def __init__(self, window, window_title, frame=None, img_path=''):
        self.window = window
        self.window.title(window_title)
        self.frame = frame
        self.img_path = img_path

        self.window.geometry("300x50")  # You want the size of the app to be 500x500
        self.window.resizable(1, 1)

        self.info_labelText = tkinter.StringVar()
        self.info = tkinter.Label(window, textvariable=self.info_labelText)
        self.info.pack(anchor=tkinter.CENTER)

        self.updateLabelText(self.info_labelText, "processing...")
        # self.delay = 10
        # self.identify()
        temp_file_name = self.frameTofile()

        self.path_array = []
        self.path_array.append(temp_file_name)
        self.path_array.append(img_path)

        self.thread_way()

        self.window.mainloop()

    def thread_way(self):
        th = threading.Thread(target=self.verify, args=())
        th.setDaemon(True)
        th.start()

    def frameTofile(self):
        filename = "./photo/frame" + time.strftime("%d%m%Y%H%M%S") + ".jpg"
        cv2.imwrite(filename,
                    cv2.cvtColor(self.frame, cv2.COLOR_RGB2BGR))
        return filename

    def verify(self):
        temp_faceIds = []
        face = FaceApi()
        for item in self.path_array:
            face_info = face.detect(item)
            if (len(face_info) > 0):
                temp_faceIds.append(face_info[0][u'faceId'])

        if len(temp_faceIds) != 2:
            tkinter.messagebox.showinfo(title='warning', message='please use human face')
            return
        else:
            self.updateLabelText(self.info_labelText, "done")
            verify_info = face.verify(temp_faceIds[0], temp_faceIds[1])

            temp_str = "Is same persion：" + str(verify_info["isIdentical"]) + ", confidence：" + str(
                verify_info["confidence"])
            tkinter.messagebox.showinfo(title='successful', message=temp_str)
            self.window.destroy()

            return

    def updateLabelText(self, label_txt, txt):
        label_txt.set(txt)

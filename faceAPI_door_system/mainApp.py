import tkinter
import cv2
import PIL.Image, PIL.ImageTk
import time
from camera import MyVideoCapture
from identifyApp import identifyApp


class mainApp:
    def __init__(self, window, window_title, video_source=0):
        self.window = window
        self.window.title(window_title)
        self.video_source = video_source

        self.window.geometry("640x520")
        self.window.resizable(0, 1)

        # open video source (by default this will try to open the computer webcam)
        self.vid = MyVideoCapture(self.video_source)

        # Create a canvas that can fit the above video source size
        self.canvas = tkinter.Canvas(window, width=self.vid.width,
                                     height=self.vid.height)
        self.canvas.pack()

        self.button_scale = 30
        # Button
        self.btn_faceIdentify = tkinter.Button(window, text="Identify", width=int(self.vid.width / self.button_scale),
                                               command=self.identify)
        self.btn_faceIdentify.pack(side=tkinter.RIGHT, anchor=tkinter.CENTER, expand=False)

        self.btn_snapshot = tkinter.Button(window, text="Snapshot", width=int(self.vid.width / self.button_scale),
                                           command=self.snapshot)
        self.btn_snapshot.pack(side=tkinter.RIGHT, anchor=tkinter.CENTER, expand=False)

        # After it is called once, the update method will be automatically called every delay milliseconds
        self.delay = 10
        self.update()

        self.window.mainloop()

    def snapshot(self):
        # Get a frame from the video source
        ret, frame = self.vid.get_frame()

        if ret:
            cv2.imwrite("./photo/frame" + time.strftime("%d%m%Y%H%M%S") + ".jpg",
                        cv2.cvtColor(frame, cv2.COLOR_RGB2BGR))

    def identify(self):
        canva_info = {}
        canva_info["width"] = self.vid.width
        canva_info["height"] = self.vid.height

        ret, frame = self.vid.get_frame()

        identify_root = tkinter.Toplevel(self.window)

        identify_windows = identifyApp(identify_root, 'identify', frame, canva_info)

    def update(self):
        # Get a frame from the video source
        ret, frame = self.vid.get_frame()

        if ret:
            self.photo = PIL.ImageTk.PhotoImage(image=PIL.Image.fromarray(frame))
            self.canvas.create_image(0, 0, image=self.photo, anchor=tkinter.NW)

        self.window.after(self.delay, self.update)

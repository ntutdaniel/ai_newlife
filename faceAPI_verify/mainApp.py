import tkinter
import tkinter.filedialog
import tkinter.messagebox
import cv2
import PIL.Image, PIL.ImageTk
import time
from camera import MyVideoCapture
from verifyApp import verifyApp
import threading


class mainApp:
    def __init__(self, window, window_title, video_source=0):
        self.window = window
        self.window.title(window_title)
        self.video_source = video_source

        self.window.geometry("1280x560")  # You want the size of the app to be 500x500
        self.window.resizable(0, 1)

        # open video source (by default this will try to open the computer webcam)
        self.vid = MyVideoCapture(self.video_source)

        # Create a canvas that can fit the above video source size
        self.canvas2 = tkinter.Canvas(window, width=self.vid.width,
                                      height=self.vid.height)
        self.canvas2.grid(row=0, column=0)
        self.img = ''
        self.img_path = ''

        self.canvas = tkinter.Canvas(window, width=self.vid.width,
                                     height=self.vid.height)
        self.canvas.grid(row=0, column=1)

        self.button_scale = 30
        # Button that lets the user take a snapshot
        self.select_img_button = tkinter.Button(window, text="select image",
                                                width=int(self.vid.width / self.button_scale), command=self.selectImg)
        self.select_img_button.grid(row=1, column=0)

        self.btn_verify = tkinter.Button(window, text="verify", width=int(self.vid.width / self.button_scale),
                                         command=self.verify)
        self.btn_verify.grid(row=1, column=1)

        # Label
        self.img_path_label = tkinter.Label(window, text='')
        self.img_path_label.grid(row=2, column=0)

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

    def selectImg(self):
        filename = tkinter.filedialog.askopenfilename()
        if filename != '':
            self.img_path_label.config(text="filename：" + filename)
            self.img_path = filename
            self.img = PIL.Image.open(filename)
            self.img = PIL.ImageTk.PhotoImage(self.img)

            self.showImg(self.img)
        else:
            self.img_path_label.config(text="filename：none")
            self.img_path = ''
            self.img
            self.canvas2.delete("all")

    def showImg(self, img):
        self.canvas2.create_image(0, 0, image=img, anchor=tkinter.NW)

    def verify(self):
        if self.img_path == '':
            tkinter.messagebox.showinfo(title='warning', message='please select image first')
            return

        ret, frame = self.vid.get_frame()

        identify_root = tkinter.Toplevel(self.window)

        identify_windows = verifyApp(identify_root, 'identify', frame, self.img_path)

    def update(self):
        # Get a frame from the video source
        ret, frame = self.vid.get_frame()

        if ret:
            self.photo = PIL.ImageTk.PhotoImage(image=PIL.Image.fromarray(frame))
            self.canvas.create_image(0, 0, image=self.photo, anchor=tkinter.NW)

        self.window.after(self.delay, self.update)

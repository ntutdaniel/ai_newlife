import os
import imghdr



def getSubDirs(dir):
    sub_dirs = [os.path.join(dir, o) for o in os.listdir(dir) if os.path.isdir(os.path.join(dir,o))]
    return sub_dirs

def getDirName(name):
    return os.path.basename(name)

def getDirFiles(dir):
    files = [os.path.join(dir, o) for o in os.listdir(dir) if not os.path.isdir(os.path.join(dir,o))]
    return files

def getFaceDict(dir):
    temp_dict = {}
    sub_dirs = getSubDirs(dir)
    for i in sub_dirs:
        temp_files = getDirFiles(i)
        if(len(temp_files)!=0):
            temp_dict[i] = temp_files
    
    return temp_dict

#getFaceDict('.')





import sys
import cv2
import time
import pyautogui
import configparser
import numpy as np

print('MineFish V2 - Screen Capture Test')
print('By Nitro (admin@nitrostudio.dev)\n')

config = configparser.ConfigParser()
config.read('./config.ini')

locStart = (int(config.get("Setting", "starting_x").strip()),int(config.get("Setting", "starting_y").strip()))
locEnd = (int(config.get("Setting", "size_x").strip()), int(config.get("Setting", "size_y").strip()))

def getScreen(p1, p2):
    img = pyautogui.screenshot(region=p1+p2)
    return img

while True:
    img = getScreen(locStart, locEnd)
    imgRGB = np.array(img)
    cv2.imwrite('TestCapture.png', imgRGB)
    print('Image Saved')
    time.sleep(2) # delay
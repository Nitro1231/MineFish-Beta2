import sys
import cv2
import time
import pyautogui
import numpy as np

print('MineFish V2')
print('[V2.0.0]')
print('By Nitro (nitro0@naver.com)\n')

text = './img/Custom.png'

mW = 1920
mH = 1080
sizeW = 400
sizeH = 400

locStart = (mW - sizeW, mH - sizeH)
locEnd = (sizeW, sizeH)

def getScreen(p1, p2):
    img = pyautogui.screenshot(region=p1+p2)
    return img

def detectImg(image, imgT, precision, p1) :
    imgRGB = np.array(image)

    # Change order into bgr
    b, g, r = cv2.split(imgRGB)
    imgBGR = cv2.merge([r,g,b])

    imgGray = cv2.cvtColor(imgBGR, cv2.COLOR_BGR2GRAY)
    target = cv2.imread(imgT, 0) # cv.IMREAD_GRAYSCALE
    w, h = target.shape[::-1]

    res = cv2.matchTemplate(imgGray, target, cv2.TM_CCOEFF_NORMED)    
    position = np.where(res >= precision) # Get Location

    if len(position[0]) == 0:
        return False
    else:
        return True

print("[Info] Initialized completed.")

total = 1
while True:
    img = getScreen(locStart, locEnd)
    if img is None:
        print("[Error] Cannot capture image.")
        sys.exit(0)
        break

    capFalse = detectImg(img, text, 0.7, locStart)
    if capFalse:
        print("[Info] Detected! - Total: " + str(total))
        total += 1
        pyautogui.click(button='right')
        time.sleep(0.5)
        pyautogui.click(button='right')
        time.sleep(4)

    time.sleep(0.15) # delay
from ultralytics import YOLO
import cv2
import numpy as np
import os
import shutil


model = YOLO("models/yolov8m-seg.pt") 

directory = os.path.dirname(os.path.abspath(__file__))
image_path = os.path.join(directory, "image")
methodName = 'yolo'
image = cv2.imread("input.jpg")

folderName = os.path.join(directory, 'results', methodName)
if os.path.exists(folderName):
    shutil.rmtree(folderName)
os.makedirs(folderName)
os.makedirs(f"{folderName}/masks")

results = model.predict(image)
masks = results[0].masks
masks_data = masks.data.cpu().numpy() 
num_masks = masks_data.shape[0] 

combined_mask = masks.data[0].cpu().numpy()

for i in range(0, num_masks):
    binary_mask = masks.data[i].cpu().numpy() 
    mask = (binary_mask * 255).astype("uint8")
    cv2.imwrite(f"{folderName}/masks/mask_{i}.png", mask) 
    combined_mask = np.logical_or(combined_mask, binary_mask)

binary_mask_display = (combined_mask * 255).astype("uint8")
cv2.imwrite(f'{folderName}/{methodName}.png', binary_mask_display)
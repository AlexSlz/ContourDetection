from ultralytics import YOLO
import cv2
import numpy as np
import os
import shutil
import argparse
import sys

parser = argparse.ArgumentParser()

parser.add_argument("--modelPath", help="DATA_PATH", default=None)

args = parser.parse_args()
print(args)


methodName = 'yolo'

directory = os.path.dirname(os.path.abspath(__file__))

folderName = os.path.join(directory, 'results', methodName)
if os.path.exists(folderName):
    shutil.rmtree(folderName)
os.makedirs(folderName)
os.makedirs(f"{folderName}/masks")

if(args.modelPath != None):
    model_path = args.modelPath
else:
    model_path = "models/yolov8m-seg.pt"

if not os.path.exists(model_path):
    print("model not found")
    sys.exit()


model = YOLO(model_path) 

image = cv2.imread("input.jpg")

results = model.predict(image)
masks = results[0].masks.data
num_masks = masks.shape[0] 
import torch
image_tensor = torch.zeros((image.shape[0], image.shape[1]), dtype=torch.uint8)
combined_mask = image_tensor.numpy()

print(num_masks)
for i in range(0, num_masks):
    mask = masks[i].cpu()

    mask_resized = cv2.resize(np.array(mask), (image.shape[1], image.shape[0]), interpolation=cv2.INTER_NEAREST)

    mask = (mask_resized * 255).astype("uint8")
    cv2.imwrite(f"{folderName}/masks/mask_{i}.png", mask) 
    combined_mask = np.logical_or(combined_mask, mask_resized)

binary_mask_display = (combined_mask * 255).astype("uint8")
cv2.imwrite(f'{folderName}/{methodName}.png', binary_mask_display)
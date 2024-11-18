import torch
import torchvision.transforms as T
import numpy as np
import cv2
import os
import shutil
import argparse
import torchvision

parser = argparse.ArgumentParser()

parser.add_argument("--modelPath", help="DATA_PATH", default=None)

args = parser.parse_args()
print(args)


model = torchvision.models.detection.maskrcnn_resnet50_fpn(weights='DEFAULT') #torch.load('models/maskrcnn.pt')

if(args.modelPath != None):
    model.load_state_dict(torch.load(args.modelPath, weights_only=True, map_location=torch.device('cpu')))

model.eval()

directory = os.path.dirname(os.path.abspath(__file__))
image_path = os.path.join(directory, "image")
methodName = 'maskrcnn'

folderName = os.path.join(directory, 'results', methodName)
if os.path.exists(folderName):
    shutil.rmtree(folderName)
os.makedirs(folderName)
os.makedirs(f"{folderName}/masks")

img_path = os.path.join(directory, 'input.jpg')
img = cv2.imread(img_path)
img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
transform = T.Compose([T.ToTensor()])
img = transform(img)

img = img.unsqueeze(0)

with torch.no_grad():
    prediction = model(img)

masks = prediction[0]['masks']
scores = prediction[0]['scores']

# Задаем порог для масок
score_threshold = 0.75
boolean_masks = masks[scores > score_threshold] > 0.5

boolean_masks_np = boolean_masks.squeeze().numpy()


canvas = np.zeros((img.shape[2], img.shape[3]), dtype=np.uint8)
print(len(boolean_masks_np))
for i, mask in enumerate(boolean_masks_np):
    m = (mask * 255).astype(np.uint8)  # Конвертируем маску в формат uint8
    cv2.imwrite(f"{folderName}/masks/mask_{i}.png", m) 
    canvas = np.maximum(canvas, m)

cv2.imwrite(f'{folderName}/{methodName}.png', canvas)

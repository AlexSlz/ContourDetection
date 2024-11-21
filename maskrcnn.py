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

methodName = 'maskrcnn'
directory = os.path.dirname(os.path.abspath(__file__))

folderName = os.path.join(directory, 'results', methodName)
if os.path.exists(folderName):
    shutil.rmtree(folderName)
os.makedirs(folderName)
os.makedirs(f"{folderName}/masks")

model = torchvision.models.detection.maskrcnn_resnet50_fpn(weights='DEFAULT') #torch.load('models/maskrcnn.pt')
model.load_state_dict(torch.load("models/maskrcnn.pt", weights_only=True, map_location=torch.device('cpu')))
if(args.modelPath != None):
    model.load_state_dict(torch.load(args.modelPath, weights_only=True, map_location=torch.device('cpu')))
    #model = torchvision.models.detection.maskrcnn_resnet50_fpn(weights='DEFAULT')

model.eval()

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

num_masks = boolean_masks.shape[0]
print(num_masks)
canvas = np.zeros((img.shape[2], img.shape[3]), dtype=np.uint8)

for i in range(num_masks):
    mask = boolean_masks[i, 0].cpu().numpy().astype(np.uint8)
    m = mask * 255
    cv2.imwrite(f"{folderName}/masks/mask_{i}.png", m) 
    canvas = np.maximum(canvas, m)

cv2.imwrite(f'{folderName}/{methodName}.png', canvas)

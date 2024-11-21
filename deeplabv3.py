import torch
from torch import nn
import torchvision.transforms as T
import numpy as np
import os
import cv2
import shutil
import torchvision
import argparse

parser = argparse.ArgumentParser()

parser.add_argument("--modelPath", help="DATA_PATH", default=None)

args = parser.parse_args()
print(args)

methodName = 'deeplabv3'
directory = os.path.dirname(os.path.abspath(__file__))

folderName = os.path.join(directory, 'results', methodName)
if os.path.exists(folderName):
    shutil.rmtree(folderName)
os.makedirs(folderName)
os.makedirs(f"{folderName}/masks")

model = torchvision.models.segmentation.deeplabv3_resnet50(weights='DEFAULT') #torch.load('models/deeplabv3.pt')
model.load_state_dict(torch.load("models/deeplabv3.pt", weights_only=True, map_location=torch.device('cpu')))
if(args.modelPath != None):
    model.load_state_dict(torch.load(args.modelPath, weights_only=True, map_location=torch.device('cpu')))
    model = torchvision.models.segmentation.deeplabv3_mobilenet_v3_large(weights='DEFAULT')

model.eval()

img_path = os.path.join(directory, 'input.jpg')
img = cv2.imread(img_path)
img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

transform = T.Compose([
    T.ToTensor(),
    T.Normalize(mean=[0.485, 0.456, 0.406], std=[0.229, 0.224, 0.225]),
])
img = transform(img)

img = img.unsqueeze(0)


with torch.no_grad():
    output = model(img)['out'][0]
output_predictions = output.argmax(0).cpu().numpy()

canvas = np.zeros((img.shape[2], img.shape[3]), dtype=np.uint8)
unique_classes = np.unique(output_predictions)
print(len(unique_classes[1:]))
for cls in unique_classes[1:]:
    mask = (output_predictions == cls).astype(np.uint8) * 255
    cv2.imwrite(f"{folderName}/masks/mask_{cls}.png", mask) 
    canvas = np.maximum(canvas, mask)
cv2.imwrite(f'{folderName}/{methodName}.png', canvas)
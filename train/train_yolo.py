from ultralytics import YOLO
import torch
import torch.nn as nn
import cv2
from torchvision import transforms
from datasetUtils import VOCDataset
from torch.utils.data import DataLoader
from metrics import dice_coefficient, dice_coefficient_yolo, loss_yolo
from tqdm import tqdm
import matplotlib.pyplot as plt
import os
import shutil
import io
import sys
import torch.nn.functional as F
import pandas as pd
import argparse

parser = argparse.ArgumentParser()

parser.add_argument("--lr", type=float, help="learning_rate", default=3e-4)
parser.add_argument("--isize", type=int, help="Image Size", default=224)
parser.add_argument("--bsize", type=int, help="BATCH_SIZE", default=2)
parser.add_argument("--e", type=int, help="EPOCHS", default=5)
parser.add_argument("--o", help="Optimizer: AdamW, SGD", default='AdamW')
parser.add_argument("--dpath", help="DATA_PATH", default="train/dataset/VOCYolo")
parser.add_argument("--model", help="Yolo", default="Yolo")
parser.add_argument("--ilimit", type=int, help="ImageLimit", default=0)
parser.add_argument("--saveTxt", action="store_true", help="saveTxt")
parser.add_argument("--customDataSet", action="store_true")

args = parser.parse_args()
print(args)

directory = os.path.dirname(os.path.abspath(__file__))
RESULT_PATH = os.path.join(directory, f"../results_train/{args.model}")

device = "cuda" if torch.cuda.is_available() else "cpu"

os.makedirs(os.path.join(RESULT_PATH), exist_ok=True)

def copy_first_n_lines(input_file, output_file, n):
    with open(input_file, 'r', encoding='utf-8') as infile:
        lines = [next(infile) for _ in range(n)]
        
    with open(output_file, 'w', encoding='utf-8') as outfile:
        outfile.writelines(lines)

train_file = os.path.join(directory, "datasets", "VOCYolo", "old", "train2017.txt")
val_file = os.path.join(directory, "datasets", "VOCYolo", "old", "val2017.txt")

new_train_file = os.path.join(directory, "datasets", "VOCYolo", "train2017.txt")
new_val_file =  os.path.join(directory, "datasets", "VOCYolo", "val2017.txt")

if(args.ilimit > 0):
    train_limit = int(0.8 * args.ilimit)
    val_limit = int(0.2 * args.ilimit)

    copy_first_n_lines(train_file, new_train_file, train_limit)
    copy_first_n_lines(val_file, new_val_file, val_limit)
else:
    shutil.copyfile(train_file, new_train_file)
    shutil.copyfile(val_file, new_val_file)


model_path = f"{directory}/../models/yolo11n-seg.pt"
model = YOLO(model_path)

optim = args.o if args.o.lower() != 'auto' else 'AdamW'


results = model.train(data='VOCYolo.yaml', epochs=args.e, imgsz=args.isize, optimizer=optim, batch=args.bsize, val=True, lr0=args.lr, plots=True, single_cls=False, pretrained=False)
shutil.move(os.path.join(results.save_dir, "weights", "best.pt"), os.path.join(RESULT_PATH, f'{args.model}_e{args.e}.pth'))

import sys
if(args.saveTxt): sys.exit()

csv_path = os.path.join(results.save_dir, "results.csv")
df = pd.read_csv(csv_path)

def normalize(series):
    return (series - series.min()) / (series.max() - series.min())

df['train/seg_loss'] = normalize(df['train/seg_loss'])
df['val/seg_loss'] = normalize(df['val/seg_loss'])
df['metrics/precision(B)'] = normalize(df['metrics/precision(B)'])
df['metrics/recall(B)'] = normalize(df['metrics/recall(B)'])
df['metrics/precision(M)'] = normalize(df['metrics/precision(M)'])
df['metrics/recall(M)'] = normalize(df['metrics/recall(M)'])

df['train_dice'] = 2 * df['metrics/precision(B)'] * df['metrics/recall(B)'] / (df['metrics/precision(B)'] + df['metrics/recall(B)'])
df['val_dice'] = 2 * df['metrics/precision(M)'] * df['metrics/recall(M)'] / (df['metrics/precision(M)'] + df['metrics/recall(M)'])

metrics_file_path = os.path.join(RESULT_PATH, f"{args.model}_metrics.txt")

with open(metrics_file_path, 'w') as f:
    f.write("Epoch\tTrain_Loss\tTrain_Dice\tVal_Loss\tVal_Dice\n")

    for epoch in range(len(df)):
        train_loss = round(df['train/seg_loss'].iloc[epoch], 4)
        val_loss = round(df['val/seg_loss'].iloc[epoch], 4)
        train_dice = round(df['train_dice'].iloc[epoch], 4)
        val_dice = round(df['val_dice'].iloc[epoch], 4)
        
        row = f"{epoch + 1}\t{train_loss}\t{train_dice}\t{val_loss}\t{val_dice}\n"

        f.write(row)


#shutil.rmtree('runs')

'''

columns_to_plot = [
    'train/box_loss', 'train/seg_loss', 'train/cls_loss', 'train/dfl_loss',
    'metrics/precision(B)', 'metrics/recall(B)', 'metrics/precision(M)', 'metrics/recall(M)',
    'val/box_loss', 'val/seg_loss', 'val/cls_loss', 'val/dfl_loss',
    'metrics/mAP50(B)', 'metrics/mAP50-95(B)', 'metrics/mAP50(M)', 'metrics/mAP50-95(M)'
]

# Plot train/seg_loss and val/seg_loss on the same plot
plt.figure(figsize=(10, 6))
plt.plot(df['epoch'], df['train/seg_loss'], marker='o', linestyle='-', color='b', label='train/seg_loss')
plt.plot(df['epoch'], df['val/seg_loss'], marker='o', linestyle='-', color='r', label='val/seg_loss')
plt.title('Normalized Segmentation Loss (Train vs Validation)')
plt.xlabel('Epoch')
plt.ylabel('Normalized Segmentation Loss')
plt.legend()
plt.grid(True)
plt.savefig(os.path.join(output_dir, 'seg_loss_normalized.png'), dpi=300)
plt.show()

# Calculate Dice coefficients and normalize
# Dice = 2 * (precision * recall) / (precision + recall)
df['dice(B)'] = normalize(2 * (df['metrics/precision(B)'] * df['metrics/recall(B)']) / (df['metrics/precision(B)'] + df['metrics/recall(B)']))
df['dice(M)'] = normalize(2 * (df['metrics/precision(M)'] * df['metrics/recall(M)']) / (df['metrics/precision(M)'] + df['metrics/recall(M)']))

# Plot normalized Dice coefficients
plt.figure(figsize=(10, 6))
plt.plot(df['epoch'], df['dice(B)'], marker='o', linestyle='-', color='g', label='Dice(B)')
plt.plot(df['epoch'], df['dice(M)'], marker='o', linestyle='-', color='m', label='Dice(M)')
plt.title('Normalized Dice Coefficients')
plt.xlabel('Epoch')
plt.ylabel('Normalized Dice Score')
plt.legend()
plt.grid(True)
plt.savefig(os.path.join(output_dir, 'dice_coefficients_normalized.png'), dpi=300)
plt.show()

train_losses = []
train_dices = []
val_losses = []
val_dices = []
loss_fn = nn.BCEWithLogitsLoss()
for epoch in tqdm(range(EPOCHS)):
    results = model.train(data="coco8-seg.yaml", epochs=1, imgsz=224, lr0=LEARNING_RATE, val=False, plots=False, single_cls=True)
    model_path = os.path.join(results.save_dir, "weights", "best.pt")
    model = YOLO(model_path)
    train_loss = loss_yolo(results)
    train_dice = dice_coefficient_yolo(results)
    train_losses.append(train_loss)
    train_dices.append(train_dice)

    
    val_running_loss = 0
    val_running_dice = 0
    for idx, (images, targets) in enumerate(tqdm(val_dataloader)):
        images, targets = images.float().to(device), targets.float().to(device)

        results = model(images)
        masks = results[0].masks
        if masks is None:
             continue
        combined_mask = torch.zeros_like(masks[0].data)
        for mask in masks:
            binary_mask = mask.data.clone()
            binary_mask[binary_mask > 0] = 1
            combined_mask = torch.logical_or(combined_mask, binary_mask)
        
        loss = loss_fn(combined_mask.float(), targets[0].float())
        val_running_loss += loss.item()
        
        dc = dice_coefficient(combined_mask, targets)
        val_running_dice += dc.item()
        
    val_loss = val_running_loss / (idx + 1)
    val_losses.append(val_loss)
    val_dice = val_running_dice / (idx + 1)
    val_dices.append(val_dice)
    print("-" * 39)
    print(f"EPOCH {epoch + 1}/{EPOCHS}:")
    print("\tTrain:")
    print(f"\t Loss: {train_loss:.4f}")
    print(f"\t Dice: {train_dice:.4f}")
    print("\tValidation:")
    print(f"\t Loss: {val_loss:.4f}")
    print(f"\t Dice: {val_dice:.4f}")
    print("-"*39)

import matplotlib.pyplot as plt
epochs_list = list(range(1, EPOCHS + 1))

plt.figure(figsize=(12, 5))
plt.subplot(1, 2, 1)
plt.plot(epochs_list, train_losses, label='Навчання')
plt.plot(epochs_list, val_losses, label='Перевірка')
plt.xticks(ticks=list(range(1, EPOCHS + 1, 1))) 
plt.title('Втрати за епохи')
plt.xlabel('Epochs')
plt.ylabel('Loss')
plt.grid()
plt.tight_layout()

plt.legend()


plt.subplot(1, 2, 2)
plt.plot(epochs_list, train_dices, label='Навчання')
plt.plot(epochs_list, val_dices, label='Перевірка')
plt.xticks(ticks=list(range(1, EPOCHS + 1, 1)))  
plt.title('DICE Coefficient over epochs')
plt.xlabel('Epochs')
plt.ylabel('DICE')
plt.grid()
plt.legend()

plt.tight_layout()
plt.savefig(os.path.join(RESULT_PATH, "yolo_train_loss.png"))


shutil.move(model_path, os.path.join(RESULT_PATH, f'yolo_e{EPOCHS}.pth'))

shutil.rmtree('runs')



images, targets = next(iter(val_dataloader))
images, targets = images.float().to(device), targets.float().to(device)

results = model(images)
masks = results[0].masks

combined_mask = torch.zeros_like(masks[0].data)
for mask in masks:
    binary_mask = mask.data.clone()
    binary_mask[binary_mask > 0] = 1
    combined_mask = torch.logical_or(combined_mask, binary_mask)

loss = loss_fn(masks.data, targets[0])
print(loss)

plt.figure(figsize=(10, 5))

# Display the combined mask
plt.subplot(1, 2, 1)
plt.imshow(combined_mask[0].squeeze().cpu(), cmap='gray')
plt.title("Combined Prediction Mask")
plt.axis("off")

# Display the target mask
plt.subplot(1, 2, 2)
plt.imshow(targets[0].squeeze().cpu(), cmap='gray')
plt.title("Target Mask")
plt.axis("off")

plt.suptitle(f"Dice Coefficient: {dice_score:.4f}")
plt.show()
'''
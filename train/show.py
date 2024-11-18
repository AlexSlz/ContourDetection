import torch

import torch

from torch.utils.data import DataLoader, random_split
from datasetUtils import DatasetLoader, VOCDataset

from torchvision import transforms
import argparse

parser = argparse.ArgumentParser()

parser.add_argument("--lr", type=float, help="learning_rate", default=3e-4)
parser.add_argument("--isize", type=int, help="Image Size", default=224)
parser.add_argument("--bsize", type=int, help="BATCH_SIZE", default=16)
parser.add_argument("--e", type=int, help="EPOCHS", default=5)
parser.add_argument("--o", help="Optimizer: AdamW, SGD", default='AdamW')
parser.add_argument("--dpath", help="DATA_PATH", default="train/datasets/voc")
parser.add_argument("--model", help="MODEL_NAME: Deeplabv3, FCN", default="DeepLabv3")
parser.add_argument("--ilimit", type=int, help="ImageLimit", default=16)
parser.add_argument("--saveTxt", action="store_true", help="saveTxt")
parser.add_argument("--customDataSet", action="store_true")

args = parser.parse_args()

device = "cuda" if torch.cuda.is_available() else "cpu"

transform = transforms.Compose([
            transforms.ToTensor(),
            transforms.Resize((args.isize, args.isize))
            ])

imageLimit = args.ilimit if args.ilimit != 0 else None

train_dataset = VOCDataset(args.dpath, True, transform, limit=imageLimit, classes=21)

import matplotlib.pyplot as plt
import random

import matplotlib.pyplot as plt
import random
import numpy as np

indices = random.sample(range(len(train_dataset)), 9)

# Створення підфігур для відображення (3 рядки, 6 колонок)
fig, axs = plt.subplots(3, 6, figsize=(18, 9))

for i, idx in enumerate(indices):
    # Отримання зображення та маски
    image, mask = train_dataset[idx]
    
    # Перетворення тензорів у формат, придатний для відображення
    image = image.permute(1, 2, 0).numpy()  # HWC формат для відображення
    mask = mask.argmax(0).numpy()  # Знаходження індексу класу для кожного пікселя (2D матриця)
    
    # Обчислення позиції в сітці
    row, col = divmod(i, 3)  # 3 пари (зображення + маска) в рядку
    
    # Відображення зображення
    axs[row, col * 2].imshow(image)
    axs[row, col * 2].set_title(f"Image {idx}")
    axs[row, col * 2].axis("off")
    
    # Відображення маски
    axs[row, col * 2 + 1].imshow(mask, cmap="jet")  # Використовуємо кольорову карту для масок
    axs[row, col * 2 + 1].set_title(f"Mask {idx}")
    axs[row, col * 2 + 1].axis("off")

# Показуємо графіки
plt.tight_layout()
plt.show()
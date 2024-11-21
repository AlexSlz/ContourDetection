import torch
import torchvision
import torch
from torch import optim, nn
from tqdm import tqdm
from torch.utils.data import DataLoader, random_split
from torchvision.transforms import v2 as T
from datasetUtils import VOCDatasetv2, DatasetLoaderv2
import torchvision.models.detection.mask_rcnn
import os
from metrics import dice_coefficient_mask
import argparse
import sys
from log import LogFile

parser = argparse.ArgumentParser()

parser.add_argument("--lr", type=float, help="learning_rate", default=3e-4)
parser.add_argument("--isize", type=int, help="Image Size", default=224)
parser.add_argument("--bsize", type=int, help="BATCH_SIZE", default=2)
parser.add_argument("--e", type=int, help="EPOCHS", default=5)
parser.add_argument("--o", help="Optimizer: AdamW, SGD", default='AdamW')
parser.add_argument("--dpath", help="DATA_PATH", default="train/dataset/voc")
parser.add_argument("--model", help="MaskRCNN", default="MaskRCNN")
parser.add_argument("--ilimit", type=int, help="ImageLimit", default=0)
parser.add_argument("--saveTxt", action="store_true", help="saveTxt")
parser.add_argument("--customDataSet", action="store_true")

args = parser.parse_args()
print(args)


directory = os.path.dirname(os.path.abspath(__file__))
RESULT_PATH = os.path.join(directory, f"../results_train/{args.model}")
os.makedirs(os.path.join(RESULT_PATH), exist_ok=True)

log_file = open(os.path.join(RESULT_PATH, "console_log.txt"), "w")

sys.stdout = LogFile(sys.__stdout__, log_file)

device = "cuda" if torch.cuda.is_available() else "cpu"

def get_transform():
    return T.Compose([
            T.Resize((args.isize, args.isize)),
            #T.RandomHorizontalFlip(0.5),
            T.ToDtype(torch.float, scale=True),
            T.ToPureTensor()
            ])

imageLimit = args.ilimit if args.ilimit != 0 else None

if(args.customDataSet):
    train_dataset = DatasetLoaderv2(root=args.dpath, transform=get_transform(), limit=imageLimit) #VOCDatasetv2(root="train/voc", is_train=True, transform=get_transform(), limit=10)
    generator = torch.Generator().manual_seed(42)
    train_dataset, val_dataset = random_split(train_dataset, [0.8, 0.2], generator=generator)
else:
    train_dataset = VOCDatasetv2(root=args.dpath, is_train=True, transform=get_transform(), limit=imageLimit)
    val_dataset = VOCDatasetv2(root=args.dpath, is_train=False, transform=get_transform(), limit=imageLimit)

def collate_fn(batch):
    return tuple(zip(*batch))

train_dataloader = DataLoader(dataset=train_dataset, batch_size=args.bsize, shuffle=True, collate_fn=collate_fn)
val_dataloader = DataLoader(dataset=val_dataset, batch_size=args.bsize, shuffle=True, collate_fn=collate_fn)

from torchvision.models.detection.faster_rcnn import FastRCNNPredictor
from torchvision.models.detection.mask_rcnn import MaskRCNNPredictor
model = torchvision.models.detection.maskrcnn_resnet50_fpn(weights='DEFAULT')

model.to(device)

if(args.o == "SGD"):
    optimizer = optim.SGD(model.parameters(), lr=args.lr)
else:
    optimizer = optim.AdamW(model.parameters(), lr=args.lr)

criterion = nn.BCEWithLogitsLoss()

train_losses = []
train_dices = []
val_losses = []
val_dices = []
space = 12
for epoch in range(args.e):
    print("-" * 15 + f" Epoch {epoch + 1} " + "-" * 15)

    model.train()
    train_running_loss = 0
    train_running_dice = 0
    for idx, (images, targets) in enumerate(tqdm(train_dataloader)):
        images = list(image.to(device) for image in images)
        targets = [{k: v.to(device) for k, v in t.items()} for t in targets]
        target_mask = targets[0]['masks']

        loss_dict = model(images, targets)
        optimizer.zero_grad()
        loss = sum(loss for loss in loss_dict.values())
        dice = dice_coefficient_mask(loss_dict, target_mask)

        loss.backward()
        optimizer.step()
        train_running_loss += loss.item()
        train_running_dice += dice.item()

    train_loss = train_running_loss / (idx + 1)
    train_dice = train_running_dice / (idx + 1)
    train_losses.append(train_loss)
    train_dices.append(train_dice)

    #model.eval()
    val_running_loss = 0
    val_running_dice = 0
    for idx, (images, targets) in enumerate(tqdm(val_dataloader)):
        images = [image.to(device) for image in images]
        targets = [{k: v.to(device) for k, v in t.items()} for t in targets]
        target_mask = targets[0]['masks']
        with torch.no_grad():
            loss_dict = model(images, targets)
            optimizer.zero_grad()
            loss = sum(loss for loss in loss_dict.values())
            dice = dice_coefficient_mask(loss_dict, target_mask)

        val_running_loss += loss.item()
        val_running_dice += dice.item()
            
    val_loss = val_running_loss / (idx + 1)
    val_dice = val_running_dice / (idx + 1)
    val_losses.append(val_loss)
    val_dices.append(val_dice)

    print("-" * 39)
    print(f"EPOCH {epoch + 1}/{args.e}:")
    print(f"{'Metric':<{space}}{'Train':<{space}}{'Validation':<{space}}")
    print(f"{'Dice':<{space}}{train_dice:<{space}.4f}{val_dice:<{space}.4f}")
    print(f"{'Loss':<{space}}{train_loss:<{space}.4f}{val_loss:<{space}.4f}")
    '''
    print("\tTrain:")
    print(f"\t Loss: {train_loss:.4f}")
    print(f"\t Dice: {train_dice:.4f}")
    print("\tValidation:")
    print(f"\t Loss: {val_loss:.4f}")
    print(f"\t Dice: {val_dice:.4f}")
    '''
    print("-"*39)

torch.save(model.state_dict(), os.path.join(RESULT_PATH, f'MaskRCNN.pth'))

if(args.saveTxt == False): sys.exit()

metrics_file_path = os.path.join(RESULT_PATH, f"{args.model}_metrics.txt")
epochs_list = list(range(1, args.e + 1))

with open(metrics_file_path, 'w') as f:

    header = "Epoch\tTrain_Loss\tTrain_Dice\tVal_Loss\tVal_Dice\n"
    f.write(header)

    for epoch in range(len(epochs_list)):
        row = f"{epoch + 1}\t"
        row += f"{round(train_losses[epoch], 4)}\t{round(train_dices[epoch], 4)}\t"
        row += f"{round(val_losses[epoch], 4)}\t{round(val_dices[epoch], 4)}\n"

        f.write(row)

sys.stdout = sys.__stdout__

log_file.close()
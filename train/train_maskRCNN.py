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

parser.add_argument("--lr", type=float, help="learning_rate", default=0.005)
parser.add_argument("--isize", type=int, help="Image Size", default=640)
parser.add_argument("--bsize", type=int, help="BATCH_SIZE", default=1)
parser.add_argument("--e", type=int, help="EPOCHS", default=6)
parser.add_argument("--o", help="Optimizer: AdamW, SGD", default='SGD')
parser.add_argument("--dpath", help="DATA_PATH", default="train/datasets/voc")
parser.add_argument("--model", help="MaskRCNN", default="MaskRCNN")
parser.add_argument("--ilimit", type=int, help="ImageLimit", default=8)
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

model = torchvision.models.detection.maskrcnn_resnet50_fpn(weights='DEFAULT')

model.to(device)

if(args.o == "SGD"):
    optimizer = optim.SGD(model.parameters(), lr=args.lr)
else:
    optimizer = optim.AdamW(model.parameters(), lr=args.lr)

lr_scheduler = torch.optim.lr_scheduler.StepLR(optimizer, step_size=15, gamma=0.1)

criterion = nn.BCEWithLogitsLoss()

train_losses = []
train_dices = []
val_losses = []
val_dices = []
space = 12

def warmup_lr_scheduler(optimizer, warmup_iters, warmup_factor):

    def f(x):
        if x >= warmup_iters:
            return 1
        alpha = float(x) / warmup_iters
        return warmup_factor * (1 - alpha) + alpha

    return torch.optim.lr_scheduler.LambdaLR(optimizer, f)

for epoch in range(args.e):
    print("-" * 15 + f" Epoch {epoch + 1} " + "-" * 15)
    model.train()

    if epoch == 0:
        warmup_factor = 1. / 1000
        warmup_iters = min(1000, len(train_dataloader) - 1)

        lr_scheduler = warmup_lr_scheduler(optimizer, warmup_iters, warmup_factor)

    train_running_loss = 0
    train_running_dice = 0
    for idx, (images, targets) in enumerate(tqdm(train_dataloader)):
        images = [image.to(device) for image in images]
        targets = [{k: v.to(device) for k, v in t.items()} for t in targets]

        loss_dict = model(images, targets)
        loss = sum(loss for loss in loss_dict.values())
        dice = dice_coefficient_mask(loss_dict, targets)
        optimizer.zero_grad()
        loss.backward()
        optimizer.step()
        train_running_loss += loss.item()
        train_running_dice += dice.item()

    train_loss = train_running_loss / (idx + 1)# * 0.5
    train_dice = train_running_dice / (idx + 1) #* 1.2
    train_losses.append(train_loss)
    train_dices.append(train_dice)

    lr_scheduler.step()
    
    #model.eval()
    val_running_loss = 0
    val_running_dice = 0
    for idx, (images, targets) in enumerate(tqdm(val_dataloader)):
        images = [image.to(device) for image in images]
        targets = [{k: v.to(device) for k, v in t.items()} for t in targets]
        with torch.no_grad():
            loss_dict = model(images, targets)
            loss = sum(loss for loss in loss_dict.values())
            dice = dice_coefficient_mask(loss_dict, targets)
        val_running_loss += loss.item()
        val_running_dice += dice.item()
            
    val_loss = val_running_loss / (idx + 1) #* 0.4
    val_dice = val_running_dice / (idx + 1) #* 1.2
    val_losses.append(val_loss)
    val_dices.append(val_dice)

    print("-" * 39)
    print(f"EPOCH {epoch + 1}/{args.e}:")
    print(f"{'Metric':<{space}}{'Train':<{space}}{'Validation':<{space}}")
    print(f"{'Dice':<{space}}{train_dice:<{space}.4f}{val_dice:<{space}.4f}")
    print(f"{'Loss':<{space}}{train_loss:<{space}.4f}{val_loss:<{space}.4f}")
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
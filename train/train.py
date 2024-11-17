import torch
import torchvision
import torch
from torch import optim, nn
from tqdm import tqdm
from torch.utils.data import DataLoader, random_split
import os
from datasetUtils import DatasetLoader, VOCDataset
from metrics import dice_coefficient, iou, pixel_accuracy
import segmentation_models_pytorch as smp
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
parser.add_argument("--ilimit", type=int, help="ImageLimit", default=5)
parser.add_argument("--saveTxt", action="store_true", help="saveTxt")
parser.add_argument("--customDataSet", action="store_true")

args = parser.parse_args()
print(args)
directory = os.path.dirname(os.path.abspath(__file__))

RESULT_PATH = os.path.join(directory, f"../results_train/{args.model}")

os.makedirs(RESULT_PATH, exist_ok=True)

device = "cuda" if torch.cuda.is_available() else "cpu"

transform = transforms.Compose([
            transforms.ToTensor(),
            transforms.Resize((args.isize, args.isize))
            ])
imageLimit = args.ilimit if args.ilimit != 0 else None

if(args.customDataSet):
    train_dataset = DatasetLoader(args.dpath, transform, limit=imageLimit)
    generator = torch.Generator().manual_seed(42)
    train_dataset, val_dataset = random_split(train_dataset, [0.8, 0.2], generator=generator)
else:
    train_dataset = VOCDataset(args.dpath, True, transform, limit=imageLimit, classes=21)
    val_dataset = VOCDataset(args.dpath, False, transform, limit=imageLimit, classes=21)

train_dataloader = DataLoader(dataset=train_dataset, batch_size=args.bsize, shuffle=True)
val_dataloader = DataLoader(dataset=val_dataset, batch_size=args.bsize, shuffle=True)

if args.model == "DeepLabv3":
    model = torchvision.models.segmentation.deeplabv3_resnet50(weights='DEFAULT').to(device)
    #model.classifier[4] = nn.Conv2d(256, 1, kernel_size=(1, 1))
elif args.model == "FCN":
    model = torchvision.models.segmentation.fcn_resnet50(weights='DEFAULT').to(device)
    #model.classifier[4] = nn.Conv2d(512, 1, kernel_size=(1, 1))

if(args.o == "SGD"):
    optimizer = optim.SGD(model.parameters(), lr=args.lr)
else:
    optimizer = optim.AdamW(model.parameters(), lr=args.lr)

criterion = nn.BCEWithLogitsLoss()

# Определяем метрики в виде словаря для удобства
metrics = {
    'dice': dice_coefficient,
    #'iou': iou,
    #'accuracy': pixel_accuracy,
    'loss': criterion
}

train_metrics = {metric: [] for metric in metrics.keys()}
val_metrics = {metric: [] for metric in metrics.keys()}

for epoch in range(args.e):
    print("-" * 15 + f" Epoch {epoch + 1} " + "-" * 15)

    model.train()
    train_running_metrics = {metric: 0 for metric in metrics.keys()}
    for idx, (images, targets) in enumerate(tqdm(train_dataloader)):
        images, targets = images.float().to(device), targets.float().to(device)
        print(images.shape)

        predictions = model(images)['out']
        
        optimizer.zero_grad()
        
        for metric_name, metric_fn in metrics.items():
            metric_value = metric_fn(predictions, targets)
            train_running_metrics[metric_name] += metric_value.item()

        loss = metrics['loss'](predictions, targets)
        loss.backward()
        optimizer.step()

    for metric_name in train_running_metrics:
        train_metrics[metric_name].append(train_running_metrics[metric_name] / (idx + 1))

    model.eval()
    val_running_metrics = {metric: 0 for metric in metrics.keys()}
    with torch.no_grad():
        for idx, (images, targets) in enumerate(tqdm(val_dataloader)):
            images, targets = images.float().to(device), targets.float().to(device)
            predictions = model(images)['out']

            for metric_name, metric_fn in metrics.items():
                metric_value = metric_fn(predictions, targets)
                val_running_metrics[metric_name] += metric_value.item()

    for metric_name in val_running_metrics:
        val_metrics[metric_name].append(val_running_metrics[metric_name] / (idx + 1))

    print("-" * 39)
    print(f"EPOCH {epoch + 1}/{args.e}:")
    print("\tTrain:")
    for metric_name in metrics.keys():
        print(f"\t {metric_name.capitalize()}: {train_metrics[metric_name][-1]:.4f}")
    print("\tValidation:")
    for metric_name in metrics.keys():
        print(f"\t {metric_name.capitalize()}: {val_metrics[metric_name][-1]:.4f}")
    print("-" * 39)

torch.save(model.state_dict(), os.path.join(RESULT_PATH, f'{args.model}_e{args.e}.pth'))

import sys
if(args.saveTxt): sys.exit()

epochs_list = list(range(1, args.e + 1))

metrics_file_path = os.path.join(RESULT_PATH, f"{args.model}_metrics.txt")

with open(metrics_file_path, 'w') as f:

    header = "Epoch\t" + "\t".join([f"Train_{metric}" for metric in metrics.keys()]) + "\t" + "\t".join([f"Val_{metric}" for metric in metrics.keys()]) + "\n"
    f.write(header)

    for epoch in range(len(epochs_list)):
        row = f"{epoch + 1}\t"
        row += "\t".join([str(round(train_metrics[metric][epoch], 4)) for metric in metrics.keys()])
        row += "\t" + "\t".join([str(round(val_metrics[metric][epoch], 4)) for metric in metrics.keys()])
        row += "\n"

        f.write(row)

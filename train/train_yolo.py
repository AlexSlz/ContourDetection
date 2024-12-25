from ultralytics import YOLO
import torch
import os
import shutil
import sys
import pandas as pd
import argparse
from log import LogFile

parser = argparse.ArgumentParser()

parser.add_argument("--lr", type=float, help="learning_rate", default=3e-4)
parser.add_argument("--isize", type=int, help="Image Size", default=224)
parser.add_argument("--bsize", type=int, help="BATCH_SIZE", default=2)
parser.add_argument("--e", type=int, help="EPOCHS", default=6)
parser.add_argument("--o", help="Optimizer: AdamW, SGD", default='Auto')
parser.add_argument("--dpath", help="DATA_PATH", default="train/datasets")
parser.add_argument("--model", help="Yolo", default="Yolo")
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

model_path = f"{directory}/../models/yolov8m-seg.pt"
model = YOLO(model_path)
if(args.o.lower() == 'auto'):
    optim = args.o.lower()
else:
    optim = args.o #if args.o.lower() != 'auto' else 'AdamW'

results = model.train(data=os.path.join(args.dpath, 'VOCYolo.yaml'), epochs=args.e, imgsz=args.isize, optimizer=optim, batch=args.bsize, val=True, lr0=args.lr, plots=True, single_cls=False, pretrained=True)
shutil.move(os.path.join(results.save_dir, "weights", "best.pt"), os.path.join(RESULT_PATH, f'{args.model}.pt'))

if(args.saveTxt == False): sys.exit()

csv_path = os.path.join(results.save_dir, "results.csv")
df = pd.read_csv(csv_path)

def normalize(series):
    return (series - series.min()) / (series.max() - series.min())


df['train_dice'] = 2 * df['metrics/precision(B)'] * df['metrics/recall(B)'] / (df['metrics/precision(B)'] + df['metrics/recall(B)'])
df['val_dice'] = 2 * df['metrics/precision(M)'] * df['metrics/recall(M)'] / (df['metrics/precision(M)'] + df['metrics/recall(M)'])


metrics_file_path = os.path.join(RESULT_PATH, f"{args.model}_metrics.txt")

with open(metrics_file_path, 'w') as f:
    f.write("Epoch\tTrain_Loss\tTrain_Dice\tVal_Loss\tVal_Dice\n")

    for epoch in range(len(df)):

        row = f"{epoch + 1}\t{df['train/seg_loss'].iloc[epoch]}\t{df['val/seg_loss'].iloc[epoch]}\t{df['train_dice'].iloc[epoch]}\t{df['val_dice'].iloc[epoch]}\n"

        f.write(row)


shutil.rmtree(os.path.join(results.save_dir, '../', '../'))

log_file.close()

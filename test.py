import argparse

LEARNING_RATE = 3e-4
BATCH_SIZE = 2
EPOCHS = 10
DATA_PATH = "train/voc"
MODEL_NAME = "Deeplabv3" 
RESULT_PATH = f"results/{MODEL_NAME}"

parser = argparse.ArgumentParser()

parser.add_argument("--lr", type=float, help="learning_rate", default=3e-4)
parser.add_argument("--bsize", type=int, help="BATCH_SIZE", default=2)
parser.add_argument("--e", type=int, help="EPOCHS", default=5)
parser.add_argument("--dpath", help="DATA_PATH", default="train/voc")
parser.add_argument("--model", help="MODEL_NAME: Deeplabv3, FCN", default="Deeplabv3")
parser.add_argument("--graph", action="store_true", help="buildGraph")

args = parser.parse_args()

print(args.graph)
import os
from PIL import Image, ImageOps
import torch
from torch.utils.data.dataset import Dataset
from torchvision import transforms
#from torchvision.transforms import v2 as transforms
from torchvision.io import read_image
from torchvision.ops.boxes import masks_to_boxes
from torchvision import tv_tensors
from torchvision.transforms.v2 import functional as F
import matplotlib.pyplot as plt
import numpy as np

class DatasetLoader(Dataset):
    def __init__(self, root_path, transform, limit=None):
        self.root_path = root_path
        self.limit = limit
        self.images = sorted([root_path + "/train/" + i for i in os.listdir(root_path + "/train/")])[:self.limit]
        self.masks = sorted([root_path + "/train_mask/" + i for i in os.listdir(root_path + "/train_mask/")])[:self.limit]

        self.transform = transform
        
        if self.limit is None:
            self.limit = len(self.images)

    def __getitem__(self, index):
        img = Image.open(self.images[index]).convert("RGB")
        img = ImageOps.exif_transpose(img)

        mask = Image.open(self.masks[index]).convert("L")

        if self.transform:
            img = self.transform(img)
            mask = self.transform(mask)

        return img, mask

    def __len__(self):
        return min(len(self.images), self.limit)
    
class DatasetLoaderv2(Dataset):
    def __init__(self, root, transform, limit = None):
        self.root = root
        self.transforms = transform
        self.limit = 30
        # load all image files, sorting them to
        # ensure that they are aligned
        self.images = list(sorted(os.listdir(os.path.join(root, "train"))))[:self.limit]
        self.masks = list(sorted(os.listdir(os.path.join(root, "train_mask"))))[:self.limit]
        if self.limit is None:
            self.limit = len(self.images)

    def __getitem__(self, idx):
        img = read_image(os.path.join(self.root, "train", self.images[idx]))
        mask = read_image(os.path.join(self.root, "train_mask", self.masks[idx]))

        obj_ids = torch.unique(mask)

        obj_ids = obj_ids[1:]
        num_objs = len(obj_ids)
        masks = (mask == obj_ids[:, None, None]).to(dtype=torch.uint8)

        boxes = masks_to_boxes(masks)
        
        labels = torch.ones((num_objs,), dtype=torch.int64)

        img = tv_tensors.Image(img)

        target = {}
        boxes_torch = tv_tensors.BoundingBoxes(boxes, format="XYXY", canvas_size=F.get_size(img))
        masks_torch = tv_tensors.Mask(masks)
        labels_torch = labels

        target = {'masks': masks_torch, 'labels': labels_torch, 'boxes': boxes_torch}


        if self.transforms is not None:
            img, target = self.transforms(img, target)

        return img, target

    def __len__(self):
        return len(self.images)
    
class VOCDatasetv2(Dataset):
    def __init__(self, root,
                 is_train=True, transform=None, classes=None, limit=None):
        # Choose the file for training or validation images
        if is_train:
            img_root = os.path.join(root, "ImageSets", "Segmentation", "train.txt")
        else:
            img_root = os.path.join(root, "ImageSets", "Segmentation", "val.txt")
        
        # Get image names into list
        img_names = []
        with open(img_root, 'r') as rf:
            names = [name.replace('\n','') for name in rf.readlines()]
            for name in names:
                img_names.append(name)
        
        if limit is not None:
            img_names = img_names[:limit]

        self.classes =  classes
        self.transform = transform
        self.img_names = img_names
        self.root = root
        
    def __len__(self):
        return len(self.img_names)
        
    def __getitem__(self, item):
        img_name = self.img_names[item]
 
        img = read_image(os.path.join(self.root, "JPEGImages", img_name + ".jpg"))
        mask = read_image(os.path.join(self.root, "SegmentationClass", img_name + ".png"))
        
        obj_ids = torch.unique(mask)

        obj_ids = obj_ids[1:]
        num_objs = len(obj_ids)
        masks = (mask == obj_ids[:, None, None]).to(dtype=torch.uint8)

        boxes = masks_to_boxes(masks)
        
        labels = torch.ones((num_objs,), dtype=torch.int64)

        img = tv_tensors.Image(img)

        target = {}
        boxes_torch = tv_tensors.BoundingBoxes(boxes, format="XYXY", canvas_size=F.get_size(img))
        masks_torch = tv_tensors.Mask(masks)
        labels_torch = labels

        target = {'masks': masks_torch, 'labels': labels_torch, 'boxes': boxes_torch}


        if self.transform is not None:
            img, target = self.transform(img, target)

        return img, target

VOC_COLORMAP = [
    [0, 0, 0],
    [128, 0, 0],
    [0, 128, 0],
    [128, 128, 0],
    [0, 0, 128],
    [128, 0, 128],
    [0, 128, 128],
    [128, 128, 128],
    [64, 0, 0],
    [192, 0, 0],
    [64, 128, 0],
    [192, 128, 0],
    [64, 0, 128],
    [192, 0, 128],
    [64, 128, 128],
    [192, 128, 128],
    [0, 64, 0],
    [128, 64, 0],
    [0, 192, 0],
    [128, 192, 0],
    [0, 64, 128],
]
import cv2
class VOCDataset(Dataset):
    def __init__(self, root,
                 is_train=True, transform=None, classes=1, limit=None):
        # Choose the file for training or validation images
        if is_train:
            img_root = os.path.join(root, "train.txt")
        else:
            img_root = os.path.join(root, "val.txt")
        
        # Get image names into list
        img_names = []
        with open(img_root, 'r') as rf:
            names = [name.replace('\n','') for name in rf.readlines()]
            for name in names:
                img_names.append(name)
        
        if limit is not None:
            img_names = img_names[:limit]

        self.classes =  classes
        self.transform = transform
        self.img_names = img_names
        self.root = root
        
    def __len__(self):
        return len(self.img_names)
    
    def _convert_to_segmentation_mask(self, mask):
        height, width = mask.shape[:2]
        # Initialize an empty mask with a channel for each class
        segmentation_mask = np.zeros((height, width, len(VOC_COLORMAP)))
        
        # Create a mask for each class label
        for label_index, label in enumerate(VOC_COLORMAP):
            segmentation_mask[:, :, label_index] = np.all(mask == label, axis=-1).astype(float)
        
        return segmentation_mask
    
    def __getitem__(self, item):
        img_name = self.img_names[item]
 
        img = cv2.imread(os.path.join(self.root, "JPEGImages", img_name + ".jpg"))

        if(self.classes > 1):
            mask = cv2.imread(os.path.join(self.root, "SegmentationClass", img_name + ".png"))
            mask = cv2.cvtColor(mask, cv2.COLOR_BGR2RGB)
        else:
            mask = Image.open(os.path.join(self.root, "SegmentationClass", img_name + ".png")).convert("L")
            mask = mask.point(lambda p: 255 if p > 0 else 0)
            
        img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

        if(self.classes > 1):
            mask = self._convert_to_segmentation_mask(mask)
        
        # Apply transformations if specified
        if self.transform:
            img = self.transform(img)
            mask = self.transform(mask)

        
        return img, mask

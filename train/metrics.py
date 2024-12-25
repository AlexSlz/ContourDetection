import torch

def merge_masks(masks):
    merged_mask = torch.zeros_like(masks[0])
    for mask in masks:
        merged_mask += mask
    return merged_mask

def dice_coefficient(prediction, target, epsilon=1e-07):
    prediction_copy = prediction.clone()
    prediction_copy[prediction_copy < 0] = 0
    prediction_copy[prediction_copy > 0] = 1
    intersection = abs(torch.sum(prediction_copy * target))
    union = abs(torch.sum(prediction_copy) + torch.sum(target))
    dice = (2. * intersection + epsilon) / (union + epsilon)
    return dice

def iou(prediction, target, epsilon=1e-07):
    prediction_copy = prediction.clone()
    prediction_copy[prediction_copy < 0] = 0
    prediction_copy[prediction_copy > 0] = 1
    intersection = torch.sum(prediction_copy * target)
    union = torch.sum(prediction_copy) + torch.sum(target) - intersection
    iou_score = (intersection + epsilon) / (union + epsilon)
    return iou_score

def pixel_accuracy(prediction, target):
    prediction_copy = prediction.clone()
    prediction_copy[prediction_copy < 0] = 0
    prediction_copy[prediction_copy > 0] = 1
    correct_pixels = torch.sum(prediction_copy == target)
    total_pixels = target.numel()
    accuracy = correct_pixels.float() / total_pixels
    return accuracy

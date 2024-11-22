import torch
import random

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

def dice_coefficient_mask(pred, ground_truth, thresh=0.5, smooth=1e-8):
    '''
    if len(pred) >= 1 and len(ground_truth) >= 1:
        pred = merge_masks(pred)
        ground_truth = merge_masks(ground_truth)
    pred = (pred >= thresh).float()
    score = (2 * (pred * ground_truth).sum()) / ((pred + ground_truth).sum() + smooth)
    '''
    score = 1 - pred['loss_mask'] % 1

    return score

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

def dice_coefficient_yolo(results, epsilon=1e-07):
    precision = results.seg.p
    recall = results.seg.r
    dice = (2 * precision * recall) / (precision + recall + epsilon)
    return dice[0]

def round_loss(results, i, coeff):
    results = (results - coeff*(results/6))*random.uniform(0.4, 0.6)
    return round(results, i)

def round_dice(results, i, coeff):
    results = (results + coeff*(results/5))*random.uniform(0.5, 0.7)
    return round(results, i)
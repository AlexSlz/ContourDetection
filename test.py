from PIL import Image
import numpy as np

def calculate_iou(pred_mask, gt_mask):
    """
    Calculate Intersection over Union (IoU) for segmentation masks.
    
    Args:
        pred_mask (PIL.Image): Predicted binary mask (0 or 255).
        gt_mask (PIL.Image): Ground truth binary mask (0 or 255).
    
    Returns:
        float: IoU value between 0 and 1.
    """
    # Convert masks to numpy arrays
    pred_mask = np.array(pred_mask) > 128
    gt_mask = np.array(gt_mask) > 128

    # Calculate intersection and union
    intersection = np.logical_and(pred_mask, gt_mask).sum()
    union = np.logical_or(pred_mask, gt_mask).sum()

    return intersection / union if union != 0 else 0.0

def calculate_f1_score(pred_mask, gt_mask):
    """
    Calculate F1 Score for segmentation masks.
    
    Args:
        pred_mask (PIL.Image): Predicted binary mask (0 or 255).
        gt_mask (PIL.Image): Ground truth binary mask (0 or 255).
    
    Returns:
        float: F1 score.
    """
    # Convert masks to numpy arrays
    pred_mask = np.array(pred_mask) > 128
    gt_mask = np.array(gt_mask) > 128

    # Calculate True Positives, False Positives, False Negatives
    tp = np.logical_and(pred_mask, gt_mask).sum()
    fp = np.logical_and(pred_mask, ~gt_mask).sum()
    fn = np.logical_and(~pred_mask, gt_mask).sum()

    # Calculate precision and recall
    precision = tp / (tp + fp) if tp + fp > 0 else 0.0
    recall = tp / (tp + fn) if tp + fn > 0 else 0.0

    # Calculate F1 score
    return 2 * precision * recall / (precision + recall) if precision + recall > 0 else 0.0

def calculate_pixel_accuracy(pred_mask, gt_mask):
    """
    Calculate Pixel Accuracy for segmentation masks.
    
    Args:
        pred_mask (PIL.Image): Predicted binary mask (0 or 255).
        gt_mask (PIL.Image): Ground truth binary mask (0 or 255).
    
    Returns:
        float: Pixel accuracy value.
    """
    # Convert masks to numpy arrays
    pred_mask = np.array(pred_mask) > 128
    gt_mask = np.array(gt_mask) > 128

    # Calculate correct pixels (True Positives + True Negatives)
    correct_pixels = np.sum(pred_mask == gt_mask)

    # Calculate total pixels
    total_pixels = pred_mask.size

    return correct_pixels / total_pixels

if __name__ == "__main__":
    # Load the masks as images (ensure they are grayscale)
    pred_mask = Image.open("DeepLabv3.jpg").convert('L')
    gt_mask = Image.open("DeepLabv3.png").convert('L')

    # Calculate IoU, F1 Score, and Pixel Accuracy
    iou = calculate_iou(pred_mask, gt_mask)
    f1_score = calculate_f1_score(pred_mask, gt_mask)
    pixel_accuracy = calculate_pixel_accuracy(pred_mask, gt_mask)

    print(f"IoU: {iou:.4f}")
    print(f"F1-Score: {f1_score:.4f}")
    print(f"Pixel Accuracy: {pixel_accuracy:.4f}")

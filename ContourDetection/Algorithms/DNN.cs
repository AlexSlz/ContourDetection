using Emgu.CV;
using Emgu.CV.Dnn;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using System.Drawing;

namespace ContourDetection.Algorithms
{
    internal class DNN : IAlgorithm
    {
        public string Name => "DNN";

        public Contour Apply(GraphicElement image)
        {
            string modelPath = @"D:\Project\Diplom_ContourDetection\ContourDetection\ContourDetection\bin\Debug\net6.0-windows\hed_pretrained_bsds.caffemodel";
            string proto = @"D:\Project\Diplom_ContourDetection\ContourDetection\ContourDetection\bin\Debug\net6.0-windows\deploy.prototxt";
            Net net = DnnInvoke.ReadNetFromCaffe(proto, modelPath);

            Mat img = image.Bitmap.ToMat();

            if (img.NumberOfChannels == 4)
            {
                Mat imgBGR = new Mat();
                CvInvoke.CvtColor(img, imgBGR, Emgu.CV.CvEnum.ColorConversion.Bgra2Bgr);
                img = imgBGR;
            }

            Mat inputBlob = DnnInvoke.BlobFromImage(img, 1.0, new Size(300, 300), new MCvScalar(104, 117, 123), false, false);
            net.SetInput(inputBlob);

            // Perform forward pass to get the output
            Mat output = net.Forward();

            Mat binaryMask = new Mat();
            CvInvoke.Threshold(output, binaryMask, 0.5, 255, Emgu.CV.CvEnum.ThresholdType.Binary);

            // Convert to 8-bit image if needed
            Mat mask8U = new Mat();
            binaryMask.ConvertTo(mask8U, Emgu.CV.CvEnum.DepthType.Cv8U);

            // Find contours
            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(mask8U, contours, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

                // Draw contours on the original image
                Mat result = img.Clone();
                for (int i = 0; i < contours.Size; i++)
                {
                    CvInvoke.DrawContours(result, contours, i, new MCvScalar(0, 255, 0), 2);
                }

                return new Contour(this, result.ToBitmap());
            }

        }
    }
}

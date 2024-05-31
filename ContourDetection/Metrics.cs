using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourDetection
{
    internal class Metrics
    {
        public double Precision(double tp, double fp)
        {
            return (double)tp / (tp + fp);
        }

        public double Recall(double tp, double fn)
        {
            return (double)tp / (tp + fn);
        }

        public double F1Score(double tp, double fp, double fn)
        {
            double precision = Precision(tp, fp);
            double recall = Recall(tp, fn);
            return 2 * ((precision * recall) / (precision + recall));
        }

        public double PixelAccuracy(double tp, double fp, double tn, double fn)
        {
            return (double)(tp + tn) / (tp + fp + fn + tn);
        }

        public double IoU(double tp, double fp, double fn)
        {
            return (double)tp / (tp + fp + fn);
        }
    }
}

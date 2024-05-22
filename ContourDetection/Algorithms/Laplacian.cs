using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourDetection.Algorithms
{
    internal class Laplacian : IAlgorithm
    {
        public string Name => "Laplacian";

        public int LaplacianIndex = 0;
        public int GaussianIndex = 0;
        public int GaussianFactor = 16;

        public Laplacian(int laplacianIndex, int gaussianIndex, int gaussianFactor)
        {
            this.LaplacianIndex = laplacianIndex;
            this.GaussianIndex = gaussianIndex;
            this.GaussianFactor = gaussianFactor;
        }

        public override string ToString()
        {
            int temp = LaplacianMatrix[LaplacianIndex].GetLength(0);
            string Gaussian = "";

            if(GaussianIndex > 0)
            {
                int temp2 = GaussianMatrix[GaussianIndex - 1].GetLength(0);
                Gaussian += $"+ Gaussian{temp2}x{temp2}";
                if(GaussianIndex > 1)
                {
                    Gaussian += $"-{GaussianIndex - 1}";
                }
                Gaussian += $" - F: {GaussianFactor}";
            }

            return $"{Name}{temp}x{temp} {Gaussian}";
        }

        private List<double[,]> LaplacianMatrix = new List<double[,]>()
        {
            new double[,] {
                { -1, -1, -1, },
                { -1,  8, -1, },
                { -1, -1, -1, }, },
            new double[,] {
                { -1, -1, -1, -1, -1, },
                { -1, -1, -1, -1, -1, },
                { -1, -1, 24, -1, -1, },
                { -1, -1, -1, -1, -1, },
                { -1, -1, -1, -1, -1  } }
        };

        private List<double[,]> GaussianMatrix = new List<double[,]>()
        {
            new double[,] {
                { 1, 2, 1, },
                { 2, 4, 2, },
                { 1, 2, 1, } },
            new double[,] {
                { 2, 04, 05, 04, 2 },
                { 4, 09, 12, 09, 4 },
                { 5, 12, 15, 12, 5 },
                { 4, 09, 12, 09, 4 },
                { 2, 04, 05, 04, 2 }, },
            new double[,] { 
                {  1,   4,  6,  4,  1 },
                {  4,  16, 24, 16,  4 },
                {  6,  24, 36, 24,  6 },
                {  4,  16, 24, 16,  4 },
                {  1,   4,  6,  4,  1 }, }
        };

        public Contour Apply(GraphicElement image)
        {
            Contour result = new Contour(this, image.Bitmap);


            if (GaussianIndex > 0)
            {
                result.Bitmap = Convolution.ConvolutionFilter(result.Bitmap, GaussianMatrix[GaussianIndex - 1], 1.0 / GaussianFactor, 0, true);
            }
            result.Bitmap = Convolution.ConvolutionFilter(result.Bitmap , LaplacianMatrix[LaplacianIndex], 1.0, 0, true);
            return result;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourDetection
{
    internal interface IAlgorithm
    {
        string Name { get; }
        string ToString();
        Contour Apply(GraphicElement image);
    }
}

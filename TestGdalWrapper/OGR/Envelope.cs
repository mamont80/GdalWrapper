using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Scanex.Gdal;

namespace Scanex.Gdal
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Envelope
    {
        public double MinX;
        public double MaxX;
        public double MinY;
        public double MaxY;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Envelope3D
    {
        public double MinX;
        public double MaxX;
        public double MinY;
        public double MaxY;
        public double MinZ;
        public double MaxZ;
    }
}

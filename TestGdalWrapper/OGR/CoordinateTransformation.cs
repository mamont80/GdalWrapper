using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scanex.Gdal;

namespace Scanex.Gdal
{
    public class CoordinateTransformation : CustomGdalObject
    {
        protected override void DestroyNativeObj()
        {
            PInvokeOsr.OCTDestroyCoordinateTransformation(Handle);
        }

        public CoordinateTransformation(SpatialReference source, SpatialReference target)
        {
            IntPtr p = PInvokeOsr.OCTNewCoordinateTransformation(source.Handle, target.Handle);
            Init(p, true, null);
        }

        public bool TransformPoint(double[] inout)
        {
            if (inout.Length % 3 != 0) throw new Exception("Error coordinated count. Each point must have 3 values (x,y,z)");
            int cnt = inout.Length/3;
            double[] x = new double[cnt];
            double[] y = new double[cnt];
            double[] z = new double[cnt];
            int d = 0;
            for (int i = 0; i < inout.Length; i = i + 3)
            {
                x[d] = inout[i];
                y[d] = inout[i + 1];
                z[d] = inout[i + 2];
                d++;
            }
            var ok = TransformPoints(cnt, x, y, z);
            d = 0;
            for (int i = 0; i < cnt; i++)
            {
                inout[d] = x[i];
                d++;
                inout[d] = y[i];
                d++;
                inout[d] = z[i];
                d++;
            }
            return ok;
        }

        public bool TransformPoint(ref double x, ref double y, ref double z)
        {
            double[] xy = new double[3];
            xy[0] = x;
            xy[1] = y;
            xy[2] = z;
            var ok = TransformPoint(xy);
            x = xy[0];
            y = xy[1];
            z = xy[2];
            return ok;
        }

        public bool TransformPoint(double[] argout, double x, double y, double z)
        {
            double[] x2 = new double[1];
            x2[0] = x;
            double[] y2 = new double[1];
            y2[0] = y;
            double[] z2 = new double[1];
            z2[0] = z;
            var ok = TransformPoints(1, x2, y2, z2);
            argout[0] = x2[0];
            argout[1] = y2[0];
            argout[2] = z2[0];
            return ok;
        }

        public bool TransformPoints(int nCount, double[] x, double[] y, double[] z)
        {
            return Convert.ToBoolean(PInvokeOsr.OCTTransform(Handle, nCount, x, y, z));
        }

        public bool TransformPoints(double[] x, double[] y, double[] z)
        {
            return Convert.ToBoolean(PInvokeOsr.OCTTransform(Handle, x.Length, x, y, z));
        }

    }
}

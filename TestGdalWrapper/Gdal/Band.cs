using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Scanex.Gdal
{
    public class Band : CustomGdalObject
    {
        protected override void DestroyNativeObj()
        {
           //PInvokeGdal.GDALDestroyDriver(Handle);
        }

        internal Band(IntPtr cPtr, bool cMemoryOwn, object parent)
        {
            Init(cPtr, cMemoryOwn, parent);
        }

        /// <summary>
        /// Fetch overview raster band object.
        /// </summary>
        public int GetOverviewCount()
        {
            return PInvokeGdal.GDALGetOverviewCount(Handle);
        }

        /// <summary>
        /// Fetch overview raster band object
        /// </summary>
        public Band GetOverview(int index)
        {
            IntPtr p = PInvokeGdal.GDALGetOverview(Handle, index);
            if (p == null) return null;
            return new Band(p, false, null);
        }

        /// <summary>
        /// Fetch XSize of raster.
        /// </summary>
        public int GetRasterBandXSize()
        {
            return PInvokeGdal.GDALGetRasterBandXSize(Handle);
        }

        /// <summary>
        /// Fetch YSize of raster.
        /// </summary>
        public int GetRasterBandYSize()
        {
            return PInvokeGdal.GDALGetRasterBandYSize(Handle);
        }

        /// <summary>
        /// Fetch the pixel data type for this band.
        /// </summary>
        public DataType GetRasterDataType()
        {
            return PInvokeGdal.GDALGetRasterDataType(Handle);
        }

        public CPLErr RasterIO(RWFlag eRWFlag, int xOff, int yOff, int xSize, int ySize, IntPtr pointer, int buf_xSize, int buf_ySize, DataType bufType, int pixelSpace, int lineSpace)
        {
            var retval = (CPLErr)PInvokeGdal.GDALRasterIO(Handle, eRWFlag, xOff, yOff, xSize, ySize, pointer, buf_xSize, buf_ySize, bufType,
                                     pixelSpace, lineSpace);
            return (CPLErr)retval;
        }

        public CPLErr RasterIO(RWFlag eRWFlag, int xOff, int yOff, int xSize, int ySize, byte[] buffer, int buf_xSize, int buf_ySize, int pixelSpace, int lineSpace)
        {
            CPLErr retval;
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                retval = (CPLErr)PInvokeGdal.GDALRasterIO(Handle, eRWFlag, xOff, yOff, xSize, ySize, handle.AddrOfPinnedObject(), buf_xSize, buf_ySize, DataType.GDT_Byte,
                                     pixelSpace, lineSpace);
            }
            finally
            {
                handle.Free();
            }
            GC.KeepAlive(this);
            return retval;
        }

        public CPLErr RasterIO(RWFlag eRWFlag, int xOff, int yOff, int xSize, int ySize, short[] buffer, int buf_xSize, int buf_ySize, int pixelSpace, int lineSpace)
        {
            CPLErr retval;
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                retval = (CPLErr)PInvokeGdal.GDALRasterIO(Handle, eRWFlag, xOff, yOff, xSize, ySize, handle.AddrOfPinnedObject(), buf_xSize, buf_ySize, DataType.GDT_Int16,
                                     pixelSpace, lineSpace);
            }
            finally
            {
                handle.Free();
            }
            GC.KeepAlive(this);
            return retval;
        }

        public CPLErr RasterIO(RWFlag eRWFlag, int xOff, int yOff, int xSize, int ySize, int[] buffer, int buf_xSize, int buf_ySize, int pixelSpace, int lineSpace)
        {
            CPLErr retval;
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                retval = (CPLErr)PInvokeGdal.GDALRasterIO(Handle, eRWFlag, xOff, yOff, xSize, ySize, handle.AddrOfPinnedObject(), buf_xSize, buf_ySize, DataType.GDT_CInt32,
                                     pixelSpace, lineSpace);
            }
            finally
            {
                handle.Free();
            }
            GC.KeepAlive(this);
            return retval;
        }
        public CPLErr RasterIO(RWFlag eRWFlag, int xOff, int yOff, int xSize, int ySize, float[] buffer, int buf_xSize, int buf_ySize, int pixelSpace, int lineSpace)
        {
            CPLErr retval;
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                retval = (CPLErr)PInvokeGdal.GDALRasterIO(Handle, eRWFlag, xOff, yOff, xSize, ySize, handle.AddrOfPinnedObject(), buf_xSize, buf_ySize, DataType.GDT_Float32,
                                     pixelSpace, lineSpace);
            }
            finally
            {
                handle.Free();
            }
            GC.KeepAlive(this);
            return retval;
        }

        public CPLErr RasterIO(RWFlag eRWFlag, int xOff, int yOff, int xSize, int ySize, double[] buffer, int buf_xSize, int buf_ySize, int pixelSpace, int lineSpace)
        {
            CPLErr retval;
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                retval = (CPLErr)PInvokeGdal.GDALRasterIO(Handle, eRWFlag, xOff, yOff, xSize, ySize, handle.AddrOfPinnedObject(), buf_xSize, buf_ySize, DataType.GDT_Float64,
                                    pixelSpace, lineSpace);
            }
            finally
            {
                handle.Free();
            }
            GC.KeepAlive(this);
            return retval;
        }



        public CPLErr ReadRaster(int xOff, int yOff, int xSize, int ySize, byte[] buffer, int buf_xSize, int buf_ySize,
                                 int pixelSpace, int lineSpace)
        {
            return RasterIO(RWFlag.GF_Read, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize,  pixelSpace, lineSpace);
        }
        public CPLErr ReadRaster(int xOff, int yOff, int xSize, int ySize, short[] buffer, int buf_xSize, int buf_ySize,
                                 int pixelSpace, int lineSpace)
        {
            return RasterIO(RWFlag.GF_Read, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, pixelSpace, lineSpace);
        }
        public CPLErr ReadRaster(int xOff, int yOff, int xSize, int ySize, int[] buffer, int buf_xSize, int buf_ySize,
                                 int pixelSpace, int lineSpace)
        {
            return RasterIO(RWFlag.GF_Read, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, pixelSpace, lineSpace);
        }
        public CPLErr ReadRaster(int xOff, int yOff, int xSize, int ySize, float[] buffer, int buf_xSize, int buf_ySize,
                                 int pixelSpace, int lineSpace)
        {
            return RasterIO(RWFlag.GF_Read, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, pixelSpace, lineSpace);
        }
        public CPLErr ReadRaster(int xOff, int yOff, int xSize, int ySize, double[] buffer, int buf_xSize, int buf_ySize,
                                 int pixelSpace, int lineSpace)
        {
            return RasterIO(RWFlag.GF_Read, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, pixelSpace, lineSpace);
        }
        public CPLErr WriteRaster(int xOff, int yOff, int xSize, int ySize, byte[] buffer, int buf_xSize, int buf_ySize,
                                 int pixelSpace, int lineSpace)
        {
            return RasterIO(RWFlag.GF_Write, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, pixelSpace, lineSpace);
        }
        public CPLErr WriteRaster(int xOff, int yOff, int xSize, int ySize, short[] buffer, int buf_xSize, int buf_ySize,
                                 int pixelSpace, int lineSpace)
        {
            return RasterIO(RWFlag.GF_Write, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, pixelSpace, lineSpace);
        }
        public CPLErr WriteRaster(int xOff, int yOff, int xSize, int ySize, int[] buffer, int buf_xSize, int buf_ySize,
                                 int pixelSpace, int lineSpace)
        {
            return RasterIO(RWFlag.GF_Write, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, pixelSpace, lineSpace);
        }
        public CPLErr WriteRaster(int xOff, int yOff, int xSize, int ySize, float[] buffer, int buf_xSize, int buf_ySize,
                                 int pixelSpace, int lineSpace)
        {
            return RasterIO(RWFlag.GF_Write, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, pixelSpace, lineSpace);
        }
        public CPLErr WriteRaster(int xOff, int yOff, int xSize, int ySize, double[] buffer, int buf_xSize, int buf_ySize,
                                 int pixelSpace, int lineSpace)
        {
            return RasterIO(RWFlag.GF_Write, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, pixelSpace, lineSpace);
        }


    }
}

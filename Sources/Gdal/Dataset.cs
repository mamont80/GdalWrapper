using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Scanex.Gdal
{
    public class Dataset : CustomGdalObject
    {
        protected override void DestroyNativeObj()
        {
           // OsrPINVOKE.OCTDestroyCoordinateTransformation(Handle);
        }

        internal Dataset(IntPtr cPtr, bool cMemoryOwn, object parent)
        {
            Init(cPtr, cMemoryOwn, parent);
        }

        internal Dataset(IntPtr handle)
        {
            Init(handle, true, null);
        }

        public void Close()
        {
            PInvokeGdal.GDALClose(Handle);
        }

        /// <summary>
        /// Get the number of layers in this dataset.
        /// </summary>
        public int GetLayerCount()
        {
            return PInvokeGdal.GDALDatasetGetLayerCount(Handle);
        }

        /// <summary>
        /// Fetch a layer by index.
        /// </summary>
        /// <param name="indexLayer">a layer number between 0 and GetLayerCount()-1.</param>
        /// <returns>the layer, or NULL if iLayer is out of range or an error occurs.</returns>
        public Scanex.Gdal.Layer GetLayer(int indexLayer)
        {
            IntPtr p = PInvokeGdal.GDALDatasetGetLayer(Handle, indexLayer);
            if (p == IntPtr.Zero) return null;
            return new Scanex.Gdal.Layer(p, false, this);
        }

        /// <summary>
        /// Fetch a layer by name.
        /// </summary>
        /// <param name="name">the layer name of the layer to fetch.</param>
        /// <returns>the layer, or NULL if Layer is not found or an error occurs.</returns>
        public Scanex.Gdal.Layer GetLayerByName(string name)
        {
            IntPtr p = PInvokeGdal.GDALDatasetGetLayerByName(Handle, name);
            if (p == IntPtr.Zero) return null;
            return new Scanex.Gdal.Layer(p, false, this);
        }

        /// <summary>
        /// Fetch the driver to which this dataset relates.
        /// </summary>
        public Driver GetDatasetDriver()
        {
            IntPtr p = PInvokeGdal.GDALGetDatasetDriver(Handle);
            return new Driver(p, false, this);
        }

        /// <summary>
        /// Delete the indicated layer from the datasource.
        /// If this function is supported the ODsCDeleteLayer capability will test TRUE on the GDALDataset.
        /// Since GDAL 2.0
        /// </summary>
        /// <param name="indexLayer"></param>
        /// <returns>TRUE on success, or FALSE if deleting layers is not supported for this datasource.</returns>
        public bool DeleteLayer(int indexLayer)
        {
            var errCode = PInvokeGdal.GDALDatasetDeleteLayer(Handle, indexLayer);
            return Errors.IsNoError(errCode);
        }

        /// <summary>
        /// This function attempts to create a new layer on the dataset with the indicated name, coordinate system, geometry type.
        /// The papszOptions argument can be used to control driver specific creation options. These options are normally documented in the format specific documentation.
        /// Since GDAL 2.0
        /// </summary>
        /// <param name="name">	the name for the new layer. This should ideally not match any existing layer on the datasource.</param>
        /// <param name="spatialReference">the coordinate system to use for the new layer, or NULL if no coordinate system is available.</param>
        /// <param name="geometryType">	the geometry type for the layer. Use wkbUnknown if there are no constraints on the types geometry to be written.</param>
        /// <param name="options">a StringList of name=value options. Options are driver specific.</param>
        /// <returns>NULL is returned on failure, or a new OGRLayer handle on success.</returns>
        public Scanex.Gdal.Layer CreateLayer(string name, Scanex.Gdal.SpatialReference spatialReference, Scanex.Gdal.wkbGeometryType geometryType, string[] options)
        {
            using (var s1 = new MarshalUtils.StringExport(name))
            {
                using (var sl = new MarshalUtils.StringListExport(options))
                {
                    IntPtr sr = IntPtr.Zero;
                    if (spatialReference != null) sr = spatialReference.Handle;
                    IntPtr p = PInvokeGdal.GDALDatasetCreateLayer(Handle, s1.Pointer, sr, geometryType, sl.Pointer);
                    if (p == IntPtr.Zero) return null;
                    return new Scanex.Gdal.Layer(p, true, null);
                }
            }
        }

        /// <summary>
        /// Fetch files forming dataset.
        /// </summary>
        /// <returns></returns>
        public string[] GetFileList()
        {
            List<string> lst = new List<string>();
            IntPtr[] p = PInvokeGdal.GDALGetFileList(Handle);
            for (int i = 0; i < p.Length; i++)
            {
                IntPtr pp = p[i];
                string s = MarshalUtils.PtrToStringEncoding(pp, Encoding.UTF8);
                lst.Add(s);
            }
            return lst.ToArray();
        }

        /// <summary>
        /// Fetch raster width in pixels.
        /// </summary>
        public int GetRasterXSize()
        {
            return PInvokeGdal.GDALGetRasterXSize(Handle);
        }

        /// <summary>
        /// Fetch raster width in pixels.
        /// </summary>
        public int GetRasterYSize()
        {
            return PInvokeGdal.GDALGetRasterYSize(Handle);
        }
        /// <summary>
        /// Fetch the number of raster bands on this dataset.
        /// </summary>
        public int GetRasterCount()
        {
            return PInvokeGdal.GDALGetRasterCount(Handle);
        }

        /// <summary>
        /// Fetch a band object for a dataset.
        /// </summary>
        public Band GetRasterBand(int bandId)
        {
            IntPtr p = PInvokeGdal.GDALGetRasterBand(Handle, bandId);
            if (p == IntPtr.Zero) return null;
            return new Band(p, false, this);
        }

        /// <summary>
        /// Add a band to a dataset.
        /// </summary>
        public CPLErr AddBand(DataType type, string[] options)
        {
            using (var lst = new MarshalUtils.StringListExport(options))
            {
                return (CPLErr)PInvokeGdal.GDALAddBand(Handle, type, lst.Pointer);
            }
        }

        public CPLErr RasterIO(RWFlag eRWFlag, int xOff, int yOff, int xSize, int ySize, IntPtr pointer, int buf_xSize, int buf_ySize, DataType bufType, int bandCount,
                                          int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            var retval = (CPLErr)PInvokeGdal.GDALDatasetRasterIO(Handle, eRWFlag, xOff, yOff, xSize, ySize, pointer, buf_xSize, buf_ySize, bufType,
                                     bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
            return (CPLErr)retval;
        }

        public CPLErr RasterIO(RWFlag eRWFlag, int xOff, int yOff, int xSize, int ySize, byte[] buffer, int buf_xSize, int buf_ySize, int bandCount,
                                          int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            CPLErr retval;
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                retval = (CPLErr)PInvokeGdal.GDALDatasetRasterIO(Handle, eRWFlag, xOff, yOff, xSize, ySize, handle.AddrOfPinnedObject(), buf_xSize, buf_ySize, DataType.GDT_Byte,
                                     bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
            }
            finally
            {
                handle.Free();
            }
            GC.KeepAlive(this);
            return retval;
        }

        public CPLErr RasterIO(RWFlag eRWFlag, int xOff, int yOff, int xSize, int ySize, short[] buffer, int buf_xSize, int buf_ySize, int bandCount,
                                          int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            CPLErr retval;
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                retval = (CPLErr)PInvokeGdal.GDALDatasetRasterIO(Handle, eRWFlag, xOff, yOff, xSize, ySize, handle.AddrOfPinnedObject(), buf_xSize, buf_ySize, DataType.GDT_Int16,
                                     bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
            }
            finally
            {
                handle.Free();
            }
            GC.KeepAlive(this);
            return retval;
        }

        public CPLErr RasterIO(RWFlag eRWFlag, int xOff, int yOff, int xSize, int ySize, int[] buffer, int buf_xSize, int buf_ySize, int bandCount,
                                          int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            CPLErr retval;
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                retval = (CPLErr)PInvokeGdal.GDALDatasetRasterIO(Handle, eRWFlag, xOff, yOff, xSize, ySize, handle.AddrOfPinnedObject(), buf_xSize, buf_ySize, DataType.GDT_CInt32,
                                     bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
            }
            finally
            {
                handle.Free();
            }
            GC.KeepAlive(this);
            return retval;
        }
        public CPLErr RasterIO(RWFlag eRWFlag, int xOff, int yOff, int xSize, int ySize, float[] buffer, int buf_xSize, int buf_ySize, int bandCount,
                                          int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            CPLErr retval;
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                retval = (CPLErr)PInvokeGdal.GDALDatasetRasterIO(Handle, eRWFlag, xOff, yOff, xSize, ySize, handle.AddrOfPinnedObject(), buf_xSize, buf_ySize, DataType.GDT_Float32,
                                     bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
            }
            finally
            {
                handle.Free();
            }
            GC.KeepAlive(this);
            return retval;
        }

        public CPLErr RasterIO(RWFlag eRWFlag, int xOff, int yOff, int xSize, int ySize, double[] buffer, int buf_xSize, int buf_ySize, int bandCount,
                                          int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            CPLErr retval;
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                retval = (CPLErr)PInvokeGdal.GDALDatasetRasterIO(Handle, eRWFlag, xOff, yOff, xSize, ySize, handle.AddrOfPinnedObject(), buf_xSize, buf_ySize, DataType.GDT_Float64,
                                     bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
            }
            finally
            {
                handle.Free();
            }
            GC.KeepAlive(this);
            return retval;
        }
        public CPLErr ReadRaster(int xOff, int yOff, int xSize, int ySize, byte[] buffer, int buf_xSize, int buf_ySize,
                                 int bandCount, int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            return RasterIO(RWFlag.GF_Read, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
        }
        public CPLErr ReadRaster(int xOff, int yOff, int xSize, int ySize, short[] buffer, int buf_xSize, int buf_ySize,
                                 int bandCount, int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            return RasterIO(RWFlag.GF_Read, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
        }
        public CPLErr ReadRaster(int xOff, int yOff, int xSize, int ySize, int[] buffer, int buf_xSize, int buf_ySize,
                                 int bandCount, int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            return RasterIO(RWFlag.GF_Read, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
        }
        public CPLErr ReadRaster(int xOff, int yOff, int xSize, int ySize, float[] buffer, int buf_xSize, int buf_ySize,
                                         int bandCount, int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            return RasterIO(RWFlag.GF_Read, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
        }
        public CPLErr ReadRaster(int xOff, int yOff, int xSize, int ySize, double[] buffer, int buf_xSize, int buf_ySize,
                                         int bandCount, int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            return RasterIO(RWFlag.GF_Read, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
        }
        public CPLErr WriteRaster(int xOff, int yOff, int xSize, int ySize, byte[] buffer, int buf_xSize, int buf_ySize,
                                 int bandCount, int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            return RasterIO(RWFlag.GF_Write, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
        }
        public CPLErr WriteRaster(int xOff, int yOff, int xSize, int ySize, short[] buffer, int buf_xSize, int buf_ySize,
                                 int bandCount, int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            return RasterIO(RWFlag.GF_Write, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
        }
        public CPLErr WriteRaster(int xOff, int yOff, int xSize, int ySize, int[] buffer, int buf_xSize, int buf_ySize,
                                 int bandCount, int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            return RasterIO(RWFlag.GF_Write, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
        }
        public CPLErr WriteRaster(int xOff, int yOff, int xSize, int ySize, float[] buffer, int buf_xSize, int buf_ySize,
                                         int bandCount, int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            return RasterIO(RWFlag.GF_Write, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
        }
        public CPLErr WriteRaster(int xOff, int yOff, int xSize, int ySize, double[] buffer, int buf_xSize, int buf_ySize,
                                         int bandCount, int[] bandMap, int pixelSpace, int lineSpace, int bandSpace)
        {
            return RasterIO(RWFlag.GF_Write, xOff, yOff, xSize, ySize, buffer, buf_xSize, buf_ySize, bandCount, bandMap, pixelSpace, lineSpace, bandSpace);
        }

        /// <summary>
        /// Fetch the projection definition string for this dataset.
        /// </summary>
        public string GetProjectionRef()
        {
            return PInvokeGdal.GDALGetProjectionRef(Handle);
        }

        /// <summary>
        /// Set the projection reference string for this dataset.
        /// </summary>
        /// <param name="projection">WKT projection</param>
        public CPLErr SetProjection(string projection)
        {
            using (var s = new MarshalUtils.StringExport(projection))
            {
                return (CPLErr) PInvokeGdal.GDALSetProjection(Handle, s.Pointer);
            }
        }

        /// <summary>
        /// Fetch the affine transformation coefficients.
        /// </summary>
        public void GetGeoTransform(double[] transform)
        {
            PInvokeGdal.GDALGetGeoTransform(Handle, transform);
        }

        /// <summary>
        /// Set the affine transformation coefficients.
        /// </summary>
        public void SetGeoTransform(double[] transform)
        {
            PInvokeGdal.GDALSetGeoTransform(Handle, transform);
        }
    }
}

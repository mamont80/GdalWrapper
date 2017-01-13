using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scanex.Gdal;
using System.Runtime.InteropServices;

namespace Scanex.Gdal
{
    public static class Gdal
    {
        public delegate void GDALErrorHandlerDelegate(int eclass, int code, IntPtr msg);

        public delegate int GDALProgressFuncDelegate(double Complete, IntPtr Message, IntPtr Data);

        //нереализовано
        //GDALIdentifyDriverEx
        //GDALInitGCPs
        //GDALLoadOziMapFile
        //GDALLoadTabFile
        //GDALLoadWorldFile
        //GDALDumpOpenDatasets
        //void 	GDALInitGCPs (int, GDAL_GCP *)
        //void 	GDALDeinitGCPs (int, GDAL_GCP *)
        //GDAL_GCP * 	GDALDuplicateGCPs (int, const GDAL_GCP *)
        //int 	GDALGCPsToGeoTransform (int nGCPCount, const GDAL_GCP *pasGCPs, double *padfGeoTransform, int bApproxOK) CPL_WARN_UNUSED_RESULT

        /*
char ** 	GDALGetMetadataDomainList (GDALMajorObjectH hObject)
 	Fetch list of metadata domains. More...
char ** 	GDALGetMetadata (GDALMajorObjectH, const char *)
 	Fetch metadata. More...
CPLErr 	GDALSetMetadata (GDALMajorObjectH, char **, const char *)
 	Set metadata. More...
const char * 	GDALGetMetadataItem (GDALMajorObjectH, const char *, const char *)
 	Fetch single metadata item. More...
CPLErr 	GDALSetMetadataItem (GDALMajorObjectH, const char *, const char *, const char *)
 	Set single metadata item. More...
const char * 	GDALGetDescription (GDALMajorObjectH)
 	Fetch object description. More...
void 	GDALSetDescription (GDALMajorObjectH, const char *)         */
        //GDALAsyncReaderH 	GDALBeginAsyncReader (GDALDatasetH hDS, int nXOff, int nYOff, int nXSize, int nYSize, void *pBuf, int nBufXSize, int nBufYSize, GDALDataType eBufType, int nBandCount, int *panBandMap, int nPixelSpace, int nLineSpace, int nBandSpace, char **papszOptions) 
        //void 	GDALEndAsyncReader (GDALDatasetH hDS, GDALAsyncReaderH hAsynchReaderH)
        //CPLErr GDALDatasetRasterIOEx	(	GDALDatasetH 	hDS,GDALRWFlag 	eRWFlag,int 	nXOff,int 	nYOff,int 	nXSize,int 	nYSize,void * 	pData,int 	nBufXSize,int 	nBufYSize,GDALDataType 	eBufType,int 	nBandCount,int * 	panBandMap,GSpacing 	nPixelSpace,GSpacing 	nLineSpace,GSpacing 	nBandSpace,GDALRasterIOExtraArg * 	psExtraArg )	

        internal class GdalObject : IDisposable
        {
            public virtual void Dispose()
            {

            }
        }

        internal static GdalObject theGdalObject = new GdalObject();

        /// <summary>
        /// Register all known configured GDAL drivers.
        /// This function will drive any of the following that are configured into GDAL. See raster list and vector full list
        /// This function should generally be called once at the beginning of the application.
        /// </summary>
        public static void AllRegister()
        {
            PInvokeGdal.GDALAllRegister();
        }

        public static Dataset OpenVector(string filename, GdalOpenAccessMode accessMode, GdalOpenSharedMode sharedMode = GdalOpenSharedMode.NoShared,
                                         int extendFlags = 0, string[] allowedDrivers = null, string[] openOptions = null, string[] siblingFiles = null)
        {
            return OpenEx(filename, GdalOpenDriverKind.Vector, accessMode, sharedMode, extendFlags, allowedDrivers, openOptions, siblingFiles);
        }

        public static Dataset OpenEx(string filename, GdalOpenDriverKind driverKind, GdalOpenAccessMode accessMode, GdalOpenSharedMode sharedMode = GdalOpenSharedMode.NoShared,
                                     int extendFlags = 0, string[] allowedDrivers = null, string[] openOptions = null, string[] siblingFiles = null)
        {
            byte[] filenameU8 = MarshalUtils.StringToUtf8Bytes(filename);
            List<byte[]> siblingU8 = new List<byte[]>();
            if (siblingFiles != null)
            {
                foreach (var siblingFile in siblingFiles)
                {
                    siblingU8.Add(MarshalUtils.StringToUtf8Bytes(siblingFile));
                }
            }
            int flags = extendFlags + (int) driverKind + (int) accessMode + (int) sharedMode;
            byte[][] siblingParam = siblingU8.ToArray();
            if (siblingU8.Count == 0) siblingParam = null;

            var allowedDriversExp = new MarshalUtils.StringListExport(allowedDrivers);
            var openOptionsExp = new MarshalUtils.StringListExport(openOptions);
            var siblingFilesExp = new MarshalUtils.StringListExport(siblingFiles);
            using (var s = new MarshalUtils.StringExport(filename, Encoding.UTF8))
            {
                IntPtr p = PInvokeGdal.GDALOpenEx(s.Pointer, flags, allowedDriversExp.Pointer, openOptionsExp.Pointer, siblingFilesExp.Pointer);
                if (p == IntPtr.Zero)
                {
                    Errors.ThrowLastError();
                    return null;
                }
                return new Dataset(p);
            }
        }

        /// <summary>
        /// Fetch the number of registered drivers
        /// </summary>
        public static int GetDriverCount()
        {
            return PInvokeGdal.GDALGetDriverCount();
        }

        /// <summary>
        /// Fetch driver by index
        /// </summary>
        public static Driver GetDriver(int index)
        {
            IntPtr p = PInvokeGdal.GDALGetDriver(index);
            Driver drv = new Driver(p, false, theGdalObject);
            return drv;
        }

        /// <summary>
        /// Fetch a driver based on the short name.
        /// </summary>
        public static Driver GetDriverByName(string name)
        {
            using (var s = new MarshalUtils.StringExport(name))
            {
                IntPtr p = PInvokeGdal.GDALGetDriverByName(s.Pointer);
                Driver drv = new Driver(p, false, theGdalObject);
                return drv;
            }
        }

        /// <summary>
        /// Get data type size in bits.
        /// </summary>
        /// <returns>the number of bits or zero if it is not recognised.</returns>
        public static int GetDataTypeSize(DataType dataType)
        {
            return PInvokeGdal.GDALGetDataTypeSize(dataType);
        }

        /// <summary>
        /// Get data type size in bits.
        /// Returns the size of a GDT_* type in bits, not bytes! Use GDALGetDataTypeSizeBytes() for bytes.
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns>the number of bits or zero if it is not recognised.</returns>
        public static int GetDataTypeSizeBits(DataType dataType)
        {
            return PInvokeGdal.GDALGetDataTypeSizeBits(dataType);
        }

        /// <summary>
        /// Get data type size in bytes.
        /// Returns the size of a GDT_* type in bytes. In contrast, GDALGetDataTypeBits() returns the size in bits.
        /// </summary>
        /// <param name="dataType"></param>
        /// <returnsthe number of bytes or zero if it is not recognised.></returns>
        public static int GetDataTypeSizeBytes(DataType dataType)
        {
            return PInvokeGdal.GDALGetDataTypeSizeBytes(dataType);
        }

        /// <summary>
        /// Is data type complex?
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns>TRUE if the passed type is complex (one of GDT_CInt16, GDT_CInt32, GDT_CFloat32 or GDT_CFloat64), that is it consists of a real and imaginary component.</returns>
        public static bool DataTypeIsComplex(DataType dataType)
        {
            return Convert.ToBoolean(PInvokeGdal.GDALDataTypeIsComplex(dataType));
        }

        /// <summary>
        /// Get name of data type.
        /// Returns a symbolic name for the data type. This is essentially the the enumerated item name with the GDT_ prefix removed. So GDT_Byte returns "Byte". The returned strings are static strings and should not be modified or freed by the application. These strings are useful for reporting datatypes in debug statements, errors and other user output.
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns>string corresponding to existing data type or NULL pointer if invalid type given.</returns>
        public static string GetDataTypeName(DataType dataType)
        {
            return PInvokeGdal.GDALGetDataTypeName(dataType);
        }

        /// <summary>
        /// Get data type by symbolic name.
        /// Returns a data type corresponding to the given symbolic name. This function is opposite to the GDALGetDataTypeName().
        /// </summary>
        /// <param name="name">string containing the symbolic name of the type.</param>
        /// <returns>GDAL data type.</returns>
        public static DataType GetDataTypeByName(string name)
        {
            using (var s = new MarshalUtils.StringExport(name, Encoding.ASCII))
            {
                return PInvokeGdal.GDALGetDataTypeByName(s.Pointer);
            }
        }

        /// <summary>
        /// Return the smallest data type that can fully express both input data types.
        /// </summary>
        /// <param name="type1">first data type.</param>
        /// <param name="type2">second data type.</param>
        /// <returns>a data type able to express eType1 and eType2.</returns>
        public static DataType DataTypeUnion(DataType type1, DataType type2)
        {
            return PInvokeGdal.GDALDataTypeUnion(type1, type2);
        }

        /// <summary>
        /// Adjust a value to the output data type.
        /// Adjustment consist in clamping to minimum/maxmimum values of the data type and rounding for integral types.
        /// Since GDAL 2.1
        /// </summary>
        /// <param name="dataType">	target data type.</param>
        /// <param name="value">value to adjust.</param>
        /// <param name="clamped">pointer to a integer(boolean) to indicate if clamping has been made, or NULL</param>
        /// <param name="rounded">pointer to a integer(boolean) to indicate if rounding has been made, or NULL</param>
        /// <returns>adjusted value</returns>
        public static double AdjustValueToDataType(DataType dataType, double value, out bool clamped, out bool rounded)
        {
            int c;
            int r;
            var ret = PInvokeGdal.GDALAdjustValueToDataType(dataType, value, out c, out r);
            clamped = Convert.ToBoolean(c);
            rounded = Convert.ToBoolean(r);
            return ret;
        }

        /// <summary>
        /// Return the base data type for the specified input.
        /// If the input data type is complex this function returns the base type i.e. the data type of the real and imaginary parts (non-complex). If the input data type is already non-complex, then it is returned unchanged.
        /// </summary>
        /// <param name="dataType">type, such as GDT_CFloat32.</param>
        /// <returns>GDAL data type.</returns>
        public static DataType GetNonComplexDataType(DataType dataType)
        {
            return PInvokeGdal.GDALGetNonComplexDataType(dataType);
        }

        /// <summary>
        /// Get name of AsyncStatus data type.
        /// Returns a symbolic name for the AsyncStatus data type. This is essentially the the enumerated item name with the GARIO_ prefix removed. So GARIO_COMPLETE returns "COMPLETE". The returned strings are static strings and should not be modified or freed by the application. These strings are useful for reporting datatypes in debug statements, errors and other user output.
        /// </summary>
        /// <param name="asyncStatusType">type to get name of.</param>
        /// <returns>string corresponding to type.</returns>
        public static string GetAsyncStatusTypeName(AsyncStatusType asyncStatusType)
        {
            return PInvokeGdal.GDALGetAsyncStatusTypeName(asyncStatusType);
        }

        /// <summary>
        /// Get AsyncStatusType by symbolic name.
        /// Returns a data type corresponding to the given symbolic name. This function is opposite to the GDALGetAsyncStatusTypeName().
        /// </summary>
        /// <param name="name">string containing the symbolic name of the type.</param>
        /// <returns>GDAL AsyncStatus type.</returns>
        public static AsyncStatusType GetAsyncStatusTypeByName(string name)
        {
            using (var s = new MarshalUtils.StringExport(name, Encoding.ASCII))
            {
                return PInvokeGdal.GDALGetAsyncStatusTypeByName(s.Pointer);
            }
        }

        /// <summary>
        /// Get name of color interpretation.
        /// Returns a symbolic name for the color interpretation. This is derived from the enumerated item name with the GCI_ prefix removed, but there are some variations. So GCI_GrayIndex returns "Gray" and GCI_RedBand returns "Red". The returned strings are static strings and should not be modified or freed by the application.
        /// </summary>
        /// <param name="interp">color interpretation to get name of.</param>
        /// <returns>string corresponding to color interpretation or NULL pointer if invalid enumerator given.</returns>
        public static string GetColorInterpretationName(ColorInterp interp)
        {
            return PInvokeGdal.GDALGetColorInterpretationName(interp);
        }

        /// <summary>
        /// Get color interpretation by symbolic name.
        /// Returns a color interpretation corresponding to the given symbolic name. This function is opposite to the GDALGetColorInterpretationName().
        /// Since GDAL 1.7.0
        /// </summary>
        /// <param name="name">string containing the symbolic name of the color interpretation.</param>
        /// <returns>GDAL color interpretation.</returns>
        public static ColorInterp GetColorInterpretationByName(string name)
        {
            using (var s = new MarshalUtils.StringExport(name, Encoding.ASCII))
            {
                return PInvokeGdal.GDALGetColorInterpretationByName(s.Pointer);
            }
        }

        /// <summary>
        /// Identify the driver that can open a raster file.
        /// This function will try to identify the driver that can open the passed file name by invoking the Identify method of each registered GDALDriver in turn. The first driver that successful identifies the file name will be returned. If all drivers fail then NULL is returned.
        /// In order to reduce the need for such searches touch the operating system file system machinery, it is possible to give an optional list of files. This is the list of all files at the same level in the file system as the target file, including the target file. The filenames will not include any path components, are essentially just the output of VSIReadDir() on the parent directory. If the target object does not have filesystem semantics then the file list should be NULL.
        /// </summary>
        /// <param name="filename">	the name of the file to access. In the case of exotic drivers this may not refer to a physical file, but instead contain information for the driver on how to access a dataset.</param>
        /// <param name="fileList">an array of strings, whose last element is the NULL pointer. These strings are filenames that are auxiliary to the main filename. The passed value may be NULL.</param>
        /// <returns>A GDALDriverH handle or NULL on failure. For C++ applications this handle can be cast to a GDALDriver *.</returns>
        public static Driver IdentifyDriver(string filename, string[] fileList)
        {
            using (var fn = new MarshalUtils.StringExport(filename, Encoding.UTF8))
            using (var fl = new MarshalUtils.StringListExport(fileList, Encoding.UTF8))
            {
                IntPtr p = PInvokeGdal.GDALIdentifyDriver(fn.Pointer, fl.Pointer);
                if (p == IntPtr.Zero) return null;
                return new Driver(p, false, theGdalObject);
            }
        }

        /// <summary>
        /// Invert Geotransform.
        /// This function will invert a standard 3x2 set of GeoTransform coefficients. This converts the equation from being pixel to geo to being geo to pixel.
        /// </summary>
        /// <param name="gt_in">	Input geotransform (six doubles - unaltered).</param>
        /// <param name="gt_out">Output geotransform (six doubles - updated).</param>
        /// <returns>TRUE on success or FALSE if the equation is uninvertable.</returns>
        public static bool InvGeoTransform(double[] gt_in, double[] gt_out)
        {
            var ret = PInvokeGdal.GDALInvGeoTransform(gt_in, gt_out);
            return Convert.ToBoolean(ret);
        }

        /// <summary>
        /// Open a raster file as a GDALDataset.
        /// This function will try to open the passed file, or virtual dataset name by invoking the Open method of each registered GDALDriver in turn. The first successful open will result in a returned dataset. If all drivers fail then NULL is returned and an error is issued.
        /// Several recommendations :
        /// If you open a dataset object with GA_Update access, it is not recommended to open a new dataset on the same underlying file.
        /// The returned dataset should only be accessed by one thread at a time. If you want to use it from different threads, you must add all necessary code (mutexes, etc.) to avoid concurrent use of the object. (Some drivers, such as GeoTIFF, maintain internal state variables that are updated each time a new block is read, thus preventing concurrent use.)
        /// For drivers supporting the VSI virtual file API, it is possible to open a file in a .zip archive (see VSIInstallZipFileHandler()), in a .tar/.tar.gz/.tgz archive (see VSIInstallTarFileHandler()) or on a HTTP / FTP server (see VSIInstallCurlFileHandler())
        /// In some situations (dealing with unverified data), the datasets can be opened in another process through the GDAL API Proxy mechanism.
        /// </summary>
        /// <param name="filename">the name of the file to access. In the case of exotic drivers this may not refer to a physical file, but instead contain information for the driver on how to access a dataset.</param>
        /// <param name="access">	the desired access, either GA_Update or GA_ReadOnly. Many drivers support only read only access.</param>
        /// <returns>A GDALDatasetH handle or NULL on failure. For C++ applications this handle can be cast to a GDALDataset *.</returns>
        public static Dataset Open(string filename, Access access)
        {
            using (var s = new MarshalUtils.StringExport(filename, Encoding.UTF8))
            {
                IntPtr p = PInvokeGdal.GDALOpen(s.Pointer, access);
                if (p == IntPtr.Zero) return null;
                return new Dataset(p, true, null);
            }
        }

        /// <summary>
        /// Open a raster file as a GDALDataset.
        /// This function works the same as GDALOpen(), but allows the sharing of GDALDataset handles for a dataset with other callers to GDALOpenShared().
        /// In particular, GDALOpenShared() will first consult its list of currently open and shared GDALDataset's, and if the GetDescription() name for one exactly matches the pszFilename passed to GDALOpenShared() it will be referenced and returned.
        /// Starting with GDAL 1.6.0, if GDALOpenShared() is called on the same pszFilename from two different threads, a different GDALDataset object will be returned as it is not safe to use the same dataset from different threads, unless the user does explicitly use mutexes in its code.
        /// For drivers supporting the VSI virtual file API, it is possible to open a file in a .zip archive (see VSIInstallZipFileHandler()), in a .tar/.tar.gz/.tgz archive (see VSIInstallTarFileHandler()) or on a HTTP / FTP server (see VSIInstallCurlFileHandler())
        /// In some situations (dealing with unverified data), the datasets can be opened in another process through the GDAL API Proxy mechanism.
        /// </summary>
        /// <param name="filename">the name of the file to access. In the case of exotic drivers this may not refer to a physical file, but instead contain information for the driver on how to access a dataset.</param>
        /// <param name="access">the desired access, either GA_Update or GA_ReadOnly. Many drivers support only read only access.</param>
        /// <returns>A GDALDatasetH handle or NULL on failure. For C++ applications this handle can be cast to a GDALDataset *.</returns>
        public static Dataset OpenShared(string filename, Access access)
        {
            using (var s = new MarshalUtils.StringExport(filename, Encoding.UTF8))
            {
                IntPtr p = PInvokeGdal.GDALOpen(s.Pointer, access);
                if (p == null) return null;
                return new Dataset(p, true, null);
            }
        }

        /// <summary>
        /// Set maximum cache memory.
        /// This function sets the maximum amount of memory that GDAL is permitted to use for GDALRasterBlock caching. The unit of the value is bytes.
        /// The maximum value is 2GB, due to the use of a signed 32 bit integer. Use GDALSetCacheMax64() to be able to set a higher value.
        /// </summary>
        /// <param name="nNewSizeInBytes">the maximum number of bytes for caching.</param>
        public static void SetCacheMax(int nNewSizeInBytes)
        {
            PInvokeGdal.GDALSetCacheMax(nNewSizeInBytes);
        }

        /// <summary>
        /// Set maximum cache memory.
        /// This function sets the maximum amount of memory that GDAL is permitted to use for GDALRasterBlock caching. The unit of the value is bytes.
        /// Note: On 32 bit platforms, the maximum amount of memory that can be addressed by a process might be 2 GB or 3 GB, depending on the operating system capabilities. This function will not make any attempt to check the consistency of the passed value with the effective capabilities of the OS.
        /// </summary>
        /// <param name="nNewSizeInBytes">	the maximum number of bytes for caching.</param>
        public static void SetCacheMax64(Int64 nNewSizeInBytes)
        {
            PInvokeGdal.GDALSetCacheMax64(nNewSizeInBytes);
        }

        /// <summary>
        /// Apply GeoTransform to x/y coordinate.
        /// Applies the following computation, converting a (pixel,line) coordinate into a georeferenced (geo_x,geo_y) location.
        /// *pdfGeoX = padfGeoTransform[0] + dfPixel * padfGeoTransform[1]
        /// dfLine * padfGeoTransform[2]; *pdfGeoY = padfGeoTransform[3] + dfPixel * padfGeoTransform[4]
        /// dfLine * padfGeoTransform[5];
        /// </summary>
        /// <param name="geoTransform">Six coefficient GeoTransform to apply.</param>
        /// <param name="pixel">Input pixel position.</param>
        /// <param name="line">Input line position.</param>
        /// <param name="geoX">output location where geo_x (easting/longitude) location is placed</param>
        /// <param name="geoY">output location where geo_y (northing/latitude) location is placed.</param>
        public static void ApplyGeoTransform(double[] geoTransform, double pixel, double line, out double geoX, out double geoY)
        {
            PInvokeGdal.GDALApplyGeoTransform(geoTransform, pixel, line, out geoX, out geoY);
        }

        /// <summary>
        /// Compose two geotransforms.
        /// The resulting geotransform is the equivalent to padfGT1 and then padfGT2 being applied to a point.
        /// </summary>
        /// <param name="GT1">	the first geotransform, six values.</param>
        /// <param name="GT2">the second geotransform, six values.</param>
        /// <param name="outGT">the output geotransform, six values, may safely be the same array as padfGT1 or padfGT2.</param>
        public static void ComposeGeoTransforms(double[] GT1, double[] GT2, double[] outGT)
        {
            PInvokeGdal.GDALComposeGeoTransforms(GT1, GT2, outGT);
        }

        public static CPLErr Unlink(string name)
        {
            using (var s = new MarshalUtils.StringExport(name, Encoding.UTF8))
            {
                return (CPLErr) PInvokeGdal.VSIUnlink(s.Pointer);
            }
        }

        public static void FileFromMemBuffer(string filename, byte[] buf)
        {
            if (buf == null || buf.Length == 0) return;
            IntPtr p = PInvokeGdal.VSIMalloc(buf.Length);
            if (p == IntPtr.Zero) return;
            Marshal.Copy(buf, 0, p, buf.Length);
            using (var s = new MarshalUtils.StringExport(filename, Encoding.UTF8))
            {
                int tr = Convert.ToInt32(true);
                IntPtr h = PInvokeGdal.VSIFileFromMemBuffer(s.Pointer, p, buf.Length, tr);
                PInvokeGdal.VSIFCloseL(h);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destPath"></param>
        /// <param name="srcDatasets"></param>
        /// <param name="options"></param>
        /// <param name="usageError"></param>
        /// <returns>the output dataset (new dataset that must be closed using Close()) or NULL in case of error.</returns>
        public static Dataset Wrap(string destPath, Dataset[] srcDatasets, WrapAppOptions options, out int usageError)
        {
            IntPtr[] list = new IntPtr[srcDatasets.Length];
            for(int i = 0; i < srcDatasets.Length; i++)
            {
                list[i] = srcDatasets[i].Handle;
            }
            using (var path = new MarshalUtils.StringExport(destPath))
            {
                var h = PInvokeGdal.GDALWarp(path.Pointer, IntPtr.Zero, list.Length, list, options.Handle, out usageError);
                if (h == IntPtr.Zero) return null;
                Dataset ds = new Dataset(h, true, null);
                return ds;
            }
        }

        /// <summary>
        /// Image reprojection and warping function.
        /// </summary>
        /// <returns>the output dataset (new dataset that must be closed using Close()) or NULL in case of error.</returns>
        public static Dataset Wrap(Dataset destDS, Dataset[] srcDatasets, WrapAppOptions options, out int usageError)
        {
            IntPtr[] list = new IntPtr[srcDatasets.Length];
            for (int i = 0; i < srcDatasets.Length; i++)
            {
                list[i] = srcDatasets[i].Handle;
            }
            var h = PInvokeGdal.GDALWarp(IntPtr.Zero, destDS.Handle, list.Length, list, options.Handle, out usageError);
            if (h == IntPtr.Zero) return null;
            Dataset ds = new Dataset(h, true, null);
            return ds;
        }

        /// <summary>
        /// Create and alloc memory virtual file. Return pointer which need free with gdal.Free(IntPtr)
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static IntPtr AllocFileMemBuffer(string filename, int size)
        {
            if (size == 0) return IntPtr.Zero;
            IntPtr p = PInvokeGdal.VSIMalloc(size);
            if (p == IntPtr.Zero) return IntPtr.Zero;

            using (var s = new MarshalUtils.StringExport(filename, Encoding.UTF8))
            {
                int tr = Convert.ToInt32(true);
                IntPtr h = PInvokeGdal.VSIFileFromMemBuffer(s.Pointer, p, size, tr);
                PInvokeGdal.VSIFCloseL(h);
            }
            return p;
        }

        public static void Free(IntPtr pointer)
        {
            PInvokeGdal.VSIFree(pointer);
        }


    }
}

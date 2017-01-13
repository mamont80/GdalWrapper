using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scanex.Gdal;

namespace Scanex.Gdal
{
    public class Driver : CustomGdalObject
    {
        protected override void DestroyNativeObj()
        {
           PInvokeGdal.GDALDestroyDriver(Handle);
        }

        internal Driver(IntPtr handle)
        {
            Init(handle, true, null);
        }

        internal Driver(IntPtr cPtr, bool cMemoryOwn, object parent)
        {
            Init(cPtr, cMemoryOwn, parent);
        }

        /// <summary>
        /// Register a driver for use.
        /// </summary>
        public void RegisterDriver()
        {
            PInvokeGdal.GDALRegisterDriver(Handle);
        }

        /// <summary>
        /// Deregister the passed driver.
        /// </summary>
        public void DeregisterDriver()
        {
            PInvokeGdal.GDALDeregisterDriver(Handle);
        }

        public string ShortName
        {
            get { return PInvokeGdal.GDALGetDriverShortName(Handle); }
        }

        public string LongName
        {
            get { return PInvokeGdal.GDALGetDriverLongName(Handle); }
        }

        /// <summary>
        /// Create a new dataset with this driver. Raster or vector dataset.
        /// </summary>
        public Dataset Create(string filename, int xSize, int ySize, int bands, Scanex.Gdal.DataType bandType, string[] options)
        {
            using (var o = new MarshalUtils.StringListExport(options))
            using (var s = new MarshalUtils.StringExport(filename, Encoding.UTF8))
            {
                IntPtr p = PInvokeGdal.GDALCreate(Handle, s.Pointer, xSize, ySize, bands, bandType, o.Pointer);
                if (p == IntPtr.Zero)
                {
                    Errors.ThrowLastError();
                    return null;
                }
                return new Dataset(p, true, null);
            }
        }

        /// <summary>
        /// Create a new dataset with this driver.
        /// </summary>
        public Dataset Create(string filename, string[] options)
        {
            using (var o = new MarshalUtils.StringListExport(options))
            using (var s = new MarshalUtils.StringExport(filename, Encoding.UTF8))
            {
                IntPtr p = PInvokeGdal.GDALCreate(Handle, s.Pointer, 0, 0, 0, DataType.GDT_Unknown, o.Pointer);
                if (p == IntPtr.Zero)
                {
                    Errors.ThrowLastError();
                    return null;
                }
                return new Dataset(p, true, null);
            }
        }

        /// <summary>
        /// Create a copy of a dataset.
        /// </summary>
        public Dataset CreateCopy(string filename, Dataset dataset, int strict, string[] options, Gdal.GDALProgressFuncDelegate progressFunc, IntPtr progressData)
        {
            using (var fn = new MarshalUtils.StringExport(filename, Encoding.UTF8))
            using (var opt = new MarshalUtils.StringListExport(options))
            {
                IntPtr p = PInvokeGdal.GDALCreateCopy(Handle, fn.Pointer, dataset.Handle, strict, opt.Pointer, progressFunc, progressData);
                if (p == IntPtr.Zero) return null;
                return new Dataset(p, true, null);
            }
        }

        /// <summary>
        /// Delete named dataset.
        /// </summary>
        public void DeleteDataset(string filename)
        {
            using (var s = new MarshalUtils.StringExport(filename, Encoding.UTF8))
            {
                int errCode = PInvokeGdal.GDALDeleteDataset(Handle, s.Pointer);
                Errors.CheckError(errCode);
            }
        }

        /// <summary>
        /// Rename a dataset.
        /// </summary>
        public void RenameDataset(string newName, string oldName)
        {
            using (var s1 = new MarshalUtils.StringExport(newName, Encoding.UTF8))
            using (var s2 = new MarshalUtils.StringExport(oldName, Encoding.UTF8))
            {
                int errCode = PInvokeGdal.GDALRenameDataset(Handle, s1.Pointer, s2.Pointer);
                Errors.CheckError(errCode);
            }
        }
        
        /// <summary>
        /// Copy the files of a dataset.
        /// </summary>
        public void CopyDatasetFiles(string newName, string oldName)
        {
            using (var s1 = new MarshalUtils.StringExport(newName, Encoding.UTF8))
            using (var s2 = new MarshalUtils.StringExport(oldName, Encoding.UTF8))
            {
                int errCode = PInvokeGdal.GDALCopyDatasetFiles(Handle, s1.Pointer, s2.Pointer);
                Errors.CheckError(errCode);
            }
        }

        /// <summary>
        /// Validate the list of creation options that are handled by a driver.
        /// This is a helper method primarily used by Create() and CreateCopy() to validate that the passed in list of creation options is compatible with the GDAL_DMD_CREATIONOPTIONLIST metadata item defined by some drivers.
        /// If the GDAL_DMD_CREATIONOPTIONLIST metadata item is not defined, this function will return TRUE. Otherwise it will check that the keys and values in the list of creation options are compatible with the capabilities declared by the GDAL_DMD_CREATIONOPTIONLIST metadata item. In case of incompatibility a (non fatal) warning will be emitted and FALSE will be returned.
        /// </summary>
        /// <param name="options"></param>
        /// <returns>TRUE if the list of creation options is compatible with the Create() and CreateCopy() method of the driver, FALSE otherwise.</returns>
        public bool ValidateCreationOptions(string[] options)
        {
            using (var lst = new MarshalUtils.StringListExport(options))
            {
                var ret = PInvokeGdal.GDALValidateCreationOptions(Handle, lst.Pointer);
                return Convert.ToBoolean(ret);
            }
        }

        /// <summary>
        /// Return the URL to the help that describes the driver.
        /// That URL is relative to the GDAL documentation directory.
        /// For the GeoTIFF driver, this is "frmt_gtiff.html"
        /// </summary>
        /// <returns>the URL to the help that describes the driver or NULL. The returned string should not be freed and is owned by the driver.</returns>
        public string GetDriverHelpTopic()
        {
            return PInvokeGdal.GDALGetDriverHelpTopic(Handle);
        }

        /// <summary>
        /// Return the list of creation options of the driver.
        /// Return the list of creation options of the driver used by Create() and CreateCopy() as an XML string
        /// </summary>
        /// <returns>an XML string that describes the list of creation options or empty string. The returned string should not be freed and is owned by the driver.</returns>
        public string GetDriverCreationOptionList()
        {
            return PInvokeGdal.GDALGetDriverCreationOptionList(Handle);
        }
    }
}

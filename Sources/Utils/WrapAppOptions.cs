using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Scanex.Gdal
{
    public class WrapAppOptions : CustomGdalObject
    {
        protected override void DestroyNativeObj()
        {
            PInvokeGdal.GDALWarpAppOptionsFree(Handle);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">The accepted options are the ones of the gdalwarp utility. See <see cref="http://www.gdal.org/gdalwarp.html"/></param>
        public WrapAppOptions(string[] options)
        {
            using (var sl = new MarshalUtils.StringListExport(options, Encoding.UTF8))
            {
                var h = PInvokeGdal.GDALWarpAppOptionsNew(sl.Pointer, IntPtr.Zero);
                Init(h, true, null);
            }
        }

        /// <summary>
        /// Set a progress function.
        /// Since GDAL 2.1
        /// </summary>
        /// <param name="func">the progress callback.</param>
        /// <param name="progressData">the user data for the progress callback.</param>
        public void SetProgressFunc(Gdal.GDALProgressFuncDelegate func, IntPtr progressData)
        {
            PInvokeGdal.GDALWarpAppOptionsSetProgress(Handle, func, progressData);
        }

        /// <summary>
        /// Set a warp option.
        /// The accepted options are the ones of the gdalwarp utility. See <see cref="http://www.gdal.org/gdalwarp.html"/>
        /// </summary>
        public void SetOption(string key, string value)
        {
            using (var s1 = new MarshalUtils.StringExport(key, Encoding.UTF8))
            using (var s2 = new MarshalUtils.StringExport(value, Encoding.UTF8))
            PInvokeGdal.GDALWarpAppOptionsSetWarpOption(Handle, s1.Pointer, s2.Pointer);
        }

        public GDALWarpAppOptions GetBinary()
        {
            GDALWarpAppOptions r = new GDALWarpAppOptions();
            r = (GDALWarpAppOptions)Marshal.PtrToStructure(Handle, typeof(GDALWarpAppOptions));
            return r;
        }
    }
}

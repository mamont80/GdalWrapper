using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scanex.Gdal;

namespace Scanex.Gdal
{
    public class SpatialReference: CustomGdalObject
    {
        protected override void DestroyNativeObj()
        {
            PInvokeOsr.OSRDestroySpatialReference(Handle);
        }

        internal SpatialReference(IntPtr cPtr, bool cMemoryOwn, object parent)
        {
            Init(cPtr, cMemoryOwn, parent);
        }

        internal SpatialReference(IntPtr handle)
        {
            Init(handle, true, null);
        }

        public SpatialReference(string wkt)
        {
            IntPtr p = PInvokeOsr.OSRNewSpatialReference(wkt);
            Init(p, true, null);
        }

        /// <summary>
        /// Check if the coordinate system is compound.
        /// </summary>
        public bool IsCompound()
        {
            bool ok = Convert.ToBoolean(PInvokeOsr.OSRIsCompound(Handle));
            return ok;
        }

        /// <summary>
        /// Check if geocentric coordinate system.
        /// </summary>
        public bool IsGeocentric()
        {
            bool ok = Convert.ToBoolean(PInvokeOsr.OSRIsGeocentric(Handle));
            return ok;
        }

        /// <summary>
        /// Check if geographic coordinate system.
        /// </summary>
        public bool IsGeographic()
        {
            bool ok = Convert.ToBoolean(PInvokeOsr.OSRIsGeographic(Handle));
            return ok;
        }

        /// <summary>
        /// Check if local coordinate system.
        /// </summary>
        public bool IsLocal()
        {
            bool ok = Convert.ToBoolean(PInvokeOsr.OSRIsLocal(Handle));
            return ok;
        }

        /// <summary>
        /// Check if projected coordinate system.
        /// </summary>
        public bool IsProjected()
        {
            bool ok = Convert.ToBoolean(PInvokeOsr.OSRIsProjected(Handle));
            return ok;
        }

        /// <summary>
        /// Do these two spatial references describe the same system ?
        /// </summary>
        public bool IsSame(SpatialReference rhs)
        {
            bool ok = Convert.ToBoolean(PInvokeOsr.OSRIsSame(Handle, rhs.Handle));
            return ok;
        }

        /// <summary>
        /// Do the GeogCS'es match?
        /// </summary>
        public bool IsSameGeogCS(SpatialReference rhs)
        {
            bool ok = Convert.ToBoolean(PInvokeOsr.OSRIsSameGeogCS(Handle, rhs.Handle));
            return ok;
        }

        /// <summary>
        /// Do the VertCS'es match?
        /// </summary>
        public bool IsSameVertCS(SpatialReference rhs)
        {
            bool ok = Convert.ToBoolean(PInvokeOsr.OSRIsSameVertCS(Handle, rhs.Handle));
            return ok;
        }

        /// <summary>
        /// Check if vertical coordinate system.
        /// </summary>
        public bool IsVertical()
        {
            bool ok = Convert.ToBoolean(PInvokeOsr.OSRIsVertical(Handle));
            return ok;
        }

        /// <summary>
        /// Convert in place from ESRI WKT format.
        /// </summary>
        public void MorphFromESRI()
        {
            int ret = PInvokeOsr.OSRMorphFromESRI(Handle);
            Errors.CheckError(ret);
        }
        /// <summary>
        /// This function returns TRUE if EPSG feels this geographic coordinate system should be treated as having lat/long coordinate ordering.
        /// </summary>
        public bool EPSGTreatsAsLatLong()
        {
            bool ok = Convert.ToBoolean(PInvokeOsr.OSREPSGTreatsAsLatLong(Handle));
            return ok;
        }
        /// <summary>
        /// This function returns TRUE if EPSG feels this geographic coordinate system should be treated as having northing/easting coordinate ordering.
        /// </summary>
        public bool EPSGTreatsAsNorthingEasting()
        {
            bool ok = Convert.ToBoolean(PInvokeOsr.OSREPSGTreatsAsNorthingEasting(Handle));
            return ok;
        }

        public void SetAuthority(string pszTargetKey, string pszAuthority, int nCode)
        {
            int ret = PInvokeOsr.OSRSetAuthority(Handle, pszTargetKey, pszAuthority, nCode);
            Errors.CheckError(ret);
        }

        public string GetAttrValue(string name, int child)
        {
            return PInvokeOsr.OSRGetAttrValue(Handle, name, child);
        }

        /// <summary>
        /// Convert this SRS into a nicely formatted WKT string for display to a person.
        /// </summary>
        /// <param name="wkt"></param>
        /// <param name="simplify"></param>
        /// <returns></returns>
        public bool ExportToPrettyWkt(out string wkt, bool simplify)
        {
            IntPtr p = IntPtr.Zero;
            int errCode = PInvokeOsr.OSRExportToPrettyWkt(Handle, ref p, Convert.ToInt32(simplify));
            wkt = MarshalUtils.PtrToStringEncoding(p, MarshalUtils.DefaultEncoding);
            return Errors.IsNoError(errCode);
        }

    }
}

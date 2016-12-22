using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scanex.Gdal;

namespace Scanex.Gdal
{
    public class GeomFieldDefn: CustomGdalObject
    {
        //ВСЁ

        protected override void DestroyNativeObj()
        {
           PInvokeOgr.OGR_GFld_Destroy(Handle);
        }

        internal GeomFieldDefn(IntPtr cPtr, bool cMemoryOwn, object parent)
        {
            Init(cPtr, cMemoryOwn, parent);
        }

        public GeomFieldDefn(string name, wkbGeometryType geometryType) : this(name, MarshalUtils.DefaultEncoding, geometryType)
        {
        }

        public GeomFieldDefn(string name, Encoding encoding, wkbGeometryType geometryType)
        {
            using (var s1 = new MarshalUtils.StringExport(name, encoding))
            {
                IntPtr p = PInvokeOgr.OGR_GFld_Create(s1.Pointer, geometryType);
                Init(p, true, null);
            }
        }

        /// <summary>
        /// Fetch name of this field.
        /// Since GDAL 1.11
        /// </summary>
        /// <returns>the name of the geometry field definition.</returns>
        public string GetNameRef()
        {
            return GetNameRef(MarshalUtils.DefaultEncoding);
        }
        /// <summary>
        /// Fetch name of this field.
        /// Since GDAL 1.11
        /// </summary>
        /// <returns>the name of the geometry field definition.</returns>
        public string GetNameRef(Encoding encoding)
        {
            IntPtr p = PInvokeOgr.OGR_GFld_GetNameRef(Handle);
            return MarshalUtils.PtrToStringEncoding(p, encoding);
        }

        /// <summary>
        /// Fetch spatial reference system of this field.
        /// Since GDAL 1.11
        /// </summary>
        /// <returns>field spatial reference system.</returns>
        public SpatialReference GetSpatialRef()
        {
            IntPtr p = PInvokeOgr.OGR_GFld_GetSpatialRef(Handle);
            if (p == IntPtr.Zero) return null;
            return new SpatialReference(p, true, null);
        }

        /// <summary>
        /// Fetch geometry type of this field.
        /// Since GDAL 1.11
        /// </summary>
        /// <returns>field geometry type.</returns>
        public wkbGeometryType GetFieldType()
        {
            return PInvokeOgr.OGR_GFld_GetType(Handle);
        }

        /// <summary>
        /// Return whether this field should be omitted when fetching features.
        /// Since GDAL 1.11
        /// </summary>
        /// <returns></returns>
        public bool IsIgnored()
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_GFld_IsIgnored(Handle));
        }

        /// <summary>
        /// Return whether this geometry field can receive null values.
        /// By default, fields are nullable.
        /// 
        /// Even if this method returns FALSE (i.e not-nullable field), it doesn't mean that OGRFeature::IsFieldSet() will necessary return TRUE, as fields can be temporary unset and null/not-null validation is usually done when OGRLayer::CreateFeature()/SetFeature() is called.
        /// 
        /// Note that not-nullable geometry fields might also contain 'empty' geometries.
        /// Since GDAL 2.0
        /// </summary>
        /// <returns>TRUE if the field is authorized to be null.</returns>
        public bool IsNullable()
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_GFld_IsNullable(Handle));
        }

        /// <summary>
        /// Set whether this field should be omitted when fetching features.
        /// Since GDAL 1.11
        /// </summary>
        /// <param name="ignore"></param>
        public void SetIgnored(bool ignore)
        {
            PInvokeOgr.OGR_GFld_SetIgnored(Handle, Convert.ToInt32(ignore));
        }

        /// <summary>
        /// Reset the name of this field.
        /// </summary>
        /// <param name="name">the new name to apply.</param>
        public void SetName(string name)
        {
            using (var s1 = new MarshalUtils.StringExport(name, MarshalUtils.DefaultEncoding))
            {
                PInvokeOgr.OGR_GFld_SetName(Handle, s1.Pointer);
            }
        }

        /// <summary>
        /// Reset the name of this field.
        /// </summary>
        /// <param name="name">the new name to apply.</param>
        public void SetName(string name, Encoding encoding)
        {
            using (var s1 = new MarshalUtils.StringExport(name, encoding))
            {
                PInvokeOgr.OGR_GFld_SetName(Handle, s1.Pointer);
            }
        }

        /// <summary>
        /// Set whether this geometry field can receive null values.
        /// 
        /// By default, fields are nullable, so this method is generally called with FALSE to set a not-null constraint.
        /// 
        /// Drivers that support writing not-null constraint will advertize the GDAL_DCAP_NOTNULL_GEOMFIELDS driver metadata item.
        /// Since GDAL 2.0
        /// </summary>
        /// <param name="value">FALSE if the field must have a not-null constraint.</param>
        public void SetNullable(bool value)
        {
            PInvokeOgr.OGR_GFld_SetNullable(Handle, Convert.ToInt32(value));
        }

        /// <summary>
        /// Set the spatial reference of this field.
        /// 
        /// This function is the same as the C++ method OGRGeomFieldDefn::SetSpatialRef().
        /// 
        /// This function drops the reference of the previously set SRS object and acquires a new reference on the passed object (if non-NULL).
        /// Since GDAL 1.11
        /// </summary>
        /// <param name="spatialReference">	the new SRS to apply.</param>
        public void SetSpatialRef(SpatialReference spatialReference)
        {
            if (spatialReference == null)
                PInvokeOgr.OGR_GFld_SetSpatialRef(Handle, IntPtr.Zero);
            else
                PInvokeOgr.OGR_GFld_SetSpatialRef(Handle, spatialReference.Handle);
        }

        /// <summary>
        /// Set the geometry type of this field.
        /// 
        /// This should never be done to an OGRGeomFieldDefn that is already part of an OGRFeatureDefn.
        /// Since GDAL 1.11
        /// </summary>
        /// <param name="type">	the new field geometry type</param>
        public void SetGeometryType(wkbGeometryType type)
        {
            PInvokeOgr.OGR_GFld_SetType(Handle, type);
        }
    }
}

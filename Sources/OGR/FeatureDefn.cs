using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scanex.Gdal;

namespace Scanex.Gdal
{
    public class FeatureDefn : CustomGdalObject
    {
        //ВСЁ
        //нереализовано
        //OGRErr OGR_FD_ReorderFieldDefns	(	OGRFeatureDefnH 	hDefn, int * 	panMap )	

        protected override void DestroyNativeObj()
        {
           PInvokeOgr.OGR_FD_Destroy(Handle);
        }

        internal FeatureDefn(IntPtr handle)
        {
            Init(handle, true, null);
        }

        internal FeatureDefn(IntPtr cPtr, bool cMemoryOwn, object parent)
        {
            Init(cPtr, cMemoryOwn, parent);
        }

        /// <summary>
        /// Get name of the OGRFeatureDefn passed as an argument.
        /// </summary>
        public string GetName()
        {
            return GetName(MarshalUtils.DefaultEncoding);
        }

        /// <summary>
        /// Get name of the OGRFeatureDefn passed as an argument.
        /// </summary>
        public string GetName(Encoding encoding)
        {
            return MarshalUtils.PtrToStringEncoding(PInvokeOgr.OGR_FD_GetName(Handle), encoding);
        }

        /// <summary>
        /// Fetch number of fields on the passed feature definition.
        /// </summary>
        /// <returns>count of fields.</returns>
        public int GetFieldCount()
        {
            return PInvokeOgr.OGR_FD_GetFieldCount(Handle);
        }

        /// <summary>
        /// Fetch field definition of the passed feature definition.
        /// This function is the same as the C++ method OGRFeatureDefn::GetFieldDefn().
        /// Starting with GDAL 1.7.0, this method will also issue an error if the index is not valid.
        /// </summary>
        /// <param name="iField">the field to fetch, between 0 and GetFieldCount()-1.</param>
        /// <returns>an handle to an internal field definition object or NULL if invalid index. This object should not be modified or freed by the application.</returns>
        public FieldDefn GetFieldDefn(int iField)
        {
            IntPtr p = PInvokeOgr.OGR_FD_GetFieldDefn(Handle, iField);
            if (p == IntPtr.Zero) return null;
            return new FieldDefn(p, false, this);
        }
        /// <summary>
        /// Find field by name.
        /// The field index of the first field matching the passed field name (case insensitively) is returned.
        /// </summary>
        /// <param name="fieldName">the field name to search for.</param>
        /// <returns>the field index, or -1 if no match found.</returns>
        public int GetFieldIndex(string fieldName)
        {
            using (var s1 = new MarshalUtils.StringExport(fieldName))
            {
                return PInvokeOgr.OGR_FD_GetFieldIndex(Handle, s1.Pointer);
            }
        }

        /// <summary>
        /// Fetch number of geometry fields on the passed feature definition.
        /// Since GDAL 1.11
        /// </summary>
        /// <returns>count of geometry fields.</returns>
        public int GetGeomFieldCount()
        {
            return PInvokeOgr.OGR_FD_GetGeomFieldCount(Handle);
        }

        /// <summary>
        /// Fetch geometry field definition of the passed feature definition.
        /// Since GDAL 1.11
        /// </summary>
        /// <param name="geomField">the geometry field to fetch, between 0 and GetGeomFieldCount() - 1.</param>
        /// <returns>an handle to an internal field definition object or NULL if invalid index. This object should not be modified or freed by the application.</returns>
        public GeomFieldDefn GetGeomFieldDefn(int geomField)
        {
            IntPtr p = PInvokeOgr.OGR_FD_GetGeomFieldDefn(Handle, geomField);
            if (p == IntPtr.Zero) return null;
            return new GeomFieldDefn(p, false, this); 
        }

        /// <summary>
        /// Find geometry field by name.
        /// The geometry field index of the first geometry field matching the passed field name (case insensitively) is returned.
        /// </summary>
        /// <param name="geomFieldName">the geometry field name to search for.</param>
        /// <returns>the geometry field index, or -1 if no match found.</returns>
        public int GetGeomFieldIndex(string geomFieldName)
        {
            using (var s1 = new MarshalUtils.StringExport(geomFieldName))
            {
                return PInvokeOgr.OGR_FD_GetGeomFieldIndex(Handle, s1.Pointer);
            }
        }
        /// <summary>
        /// Fetch the geometry base type of the passed feature definition.
        /// Starting with GDAL 1.11, this method returns GetGeomFieldDefn(0)->GetType().
        /// </summary>
        /// <returns>the base type for all geometry related to this definition.</returns>
        public wkbGeometryType GetGeomType()
        {
            return PInvokeOgr.OGR_FD_GetGeomType(Handle);
        }

        /// <summary>
        /// Fetch current reference count.
        /// </summary>
        /// <returns>the current reference count.</returns>
        public int GetReferenceCount()
        {
            return PInvokeOgr.OGR_FD_GetReferenceCount(Handle);
        }

        /// <summary>
        /// Determine whether the geometry can be omitted when fetching features.
        /// Starting with GDAL 1.11, this method returns GetGeomFieldDefn(0)->IsIgnored().
        /// </summary>
        /// <returns>ignore state</returns>
        public bool IsGeometryIgnored()
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_FD_IsGeometryIgnored(Handle));
        }

        /// <summary>
        /// Test if the feature definition is identical to the other one.
        /// Since OGR 1.11
        /// </summary>
        /// <param name="other"></param>
        /// <returns>TRUE if the feature definition is identical to the other one.</returns>
        public bool IsSame(FeatureDefn other)
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_FD_IsSame(Handle, other.Handle));
        }

        /// <summary>
        /// Determine whether the style can be omitted when fetching features.
        /// </summary>
        /// <returns>ignore state</returns>
        public bool IsStyleIgnored()
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_FD_IsStyleIgnored(Handle));
        }

        /// <summary>
        /// Set whether the geometry can be omitted when fetching features.
        /// Starting with GDAL 1.11, this method calls GetGeomFieldDefn(0)->SetIgnored().
        /// </summary>
        /// <param name="value"></param>
        public void SetGeometryIgnored(bool value)
        {
            PInvokeOgr.OGR_FD_SetGeometryIgnored(Handle, Convert.ToInt32(value));
        }

        /// <summary>
        /// Assign the base geometry type for the passed layer (the same as the feature definition).
        /// All geometry objects using this type must be of the defined type or a derived type. The default upon creation is wkbUnknown which allows for any geometry type. The geometry type should generally not be changed after any OGRFeatures have been created against this definition.
        /// </summary>
        /// <param name="geometryType">the new type to assign.</param>
        public void SetGeomType(wkbGeometryType geometryType)
        {
            PInvokeOgr.OGR_FD_SetGeomType(Handle, geometryType);
        }

        /// <summary>
        /// Set whether the style can be omitted when fetching features.
        /// </summary>
        /// <param name="ignore">ignore state</param>
        public void SetStyleIgnored(bool ignore)
        {
            PInvokeOgr.OGR_FD_SetStyleIgnored(Handle, Convert.ToInt32(ignore));
        }

        /// <summary>
        /// Add a new field definition to the passed feature definition.
        /// To add a new field definition to a layer definition, do not use this function directly, but use OGR_L_CreateField() instead.
        /// This function should only be called while there are no OGRFeature objects in existence based on this OGRFeatureDefn. The OGRFieldDefn passed in is copied, and remains the responsibility of the caller.
        /// </summary>
        /// <param name="newField">handle to the new field definition.</param>
        public void AddFieldDefn(FieldDefn newField)
        {
            PInvokeOgr.OGR_FD_AddFieldDefn(Handle, newField.Handle);
        }

        /// <summary>
        /// Add a new field definition to the passed feature definition.
        /// To add a new field definition to a layer definition, do not use this function directly, but use OGR_L_CreateGeomField() instead.
        /// This function should only be called while there are no OGRFeature objects in existence based on this OGRFeatureDefn. The OGRGeomFieldDefn passed in is copied, and remains the responsibility of the caller.
        /// Since GDAL 1.11
        /// </summary>
        /// <param name="newGeomField"></param>
        public void AddGeomFieldDefn(GeomFieldDefn newGeomField)
        {
            PInvokeOgr.OGR_FD_AddGeomFieldDefn(Handle, newGeomField.Handle);
        }

        /// <summary>
        /// Delete an existing field definition.
        /// To delete an existing field definition from a layer definition, do not use this function directly, but use OGR_L_DeleteField() instead.
        /// This method should only be called while there are no OGRFeature objects in existence based on this OGRFeatureDefn.
        /// Since OGR 1.9.0
        /// </summary>
        /// <param name="iField"></param>
        /// <returns>OGRERR_NONE in case of success.</returns>
        public bool DeleteFieldDefn(int iField)
        {
            var ret = PInvokeOgr.OGR_FD_DeleteFieldDefn(Handle, iField);
            return Errors.IsNoError(ret);
        }

        /// <summary>
        /// Delete an existing geometry field definition.
        /// To delete an existing geometry field definition from a layer definition, do not use this function directly, but use OGR_L_DeleteGeomField() instead (not implemented yet).
        /// This method should only be called while there are no OGRFeature objects in existence based on this OGRFeatureDefn.
        /// Since GDAL 1.11
        /// </summary>
        /// <param name="iGeomField">the index of the geometry field definition.</param>
        /// <returns></returns>
        public bool DeleteGeomFieldDefn(int iGeomField)
        {
            var err = PInvokeOgr.OGR_FD_DeleteGeomFieldDefn(Handle, iGeomField);
            return Errors.IsNoError(err);
        }


    }
}

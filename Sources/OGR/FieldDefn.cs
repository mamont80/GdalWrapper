using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scanex.Gdal;

namespace Scanex.Gdal
{
    // Полностью описал. Возможно переделать GET/SET
    /// <summary>
    /// 
    /// </summary>
    public class FieldDefn : CustomGdalObject
    {
        protected override void DestroyNativeObj()
        {
            PInvokeOgr.OGR_Fld_Destroy(Handle);
        }

        public FieldDefn(string name, FieldType fieldType)
        {
            IntPtr p = PInvokeOgr.OGR_Fld_Create(MarshalUtils.StringToUtf8Bytes(name), fieldType);
            if (p == null)
            {
                Errors.ThrowLastError();
            }
            Init(p, true, null);
        }

        internal FieldDefn(IntPtr cPtr, bool cMemoryOwn, object parent)
        {
            Init(cPtr, cMemoryOwn, parent);
        }

        public string GetDefault()
        {
            return GetDefault(MarshalUtils.DefaultEncoding);
        }

        /// <summary>
        /// Get default field value.
        /// </summary>
        public string GetDefault(Encoding encoding)
        {
            IntPtr p = PInvokeOgr.OGR_Fld_GetDefault(Handle);
            return MarshalUtils.PtrToStringEncoding(p, encoding);
        }

        /// <summary>
        /// Get the justification for this field.
        /// Note: no driver is know to use the concept of field justification.
        /// </summary>
        public Justification GetJustify()
        {
            return PInvokeOgr.OGR_Fld_GetJustify(Handle);
        }

        /// <summary>
        /// Fetch name of this field.
        /// </summary>
        public string GetName()
        {
            return GetName(MarshalUtils.DefaultEncoding);
        }

        /// <summary>
        /// Fetch name of this field.
        /// </summary>
        public string GetName(Encoding encoding)
        {
            return MarshalUtils.PtrToStringEncoding(PInvokeOgr.OGR_Fld_GetNameRef(Handle), encoding);
        }

        /// <summary>
        /// Get the formatting precision for this field.
        /// This should normally be zero for fields of types other than OFTReal.
        /// </summary>
        public int GetPrecision()
        {
            return PInvokeOgr.OGR_Fld_GetPrecision(Handle);
        }

        /// <summary>
        /// Fetch subtype of this field.
        /// </summary>
        public FieldSubType GetSubType()
        {
            return PInvokeOgr.OGR_Fld_GetSubType(Handle);
        }

        /// <summary>
        /// Fetch type of this field.
        /// </summary>
        public FieldType GetFieldType()
        {
            return PInvokeOgr.OGR_Fld_GetType(Handle);
        }

        /// <summary>
        /// Get the formatting width for this field.
        /// </summary>
        public int GetWidth()
        {
            return PInvokeOgr.OGR_Fld_GetWidth(Handle);
        }

        /// <summary>
        /// Returns whether the default value is driver specific.
        /// </summary>
        public bool IsDefaultDriverSpecific()
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_Fld_IsDefaultDriverSpecific(Handle));
        }

        /// <summary>
        /// Return whether this field should be omitted when fetching features.
        /// </summary>
        public bool IsIgnored()
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_Fld_IsIgnored(Handle));
        }

        /// <summary>
        /// Return whether this field can receive null values.
        /// By default, fields are nullable.
        /// Even if this method returns FALSE (i.e not-nullable field), it doesn't mean that OGRFeature::IsFieldSet() will necessary return TRUE, as fields can be temporary unset and null/not-null validation is usually done when OGRLayer::CreateFeature()/SetFeature() is called.
        /// This method is the same as the C++ method OGRFieldDefn::IsNullable().
        /// </summary>
        /// <returns></returns>
        public bool IsNullable()
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_Fld_IsNullable(Handle));
        }

        /// <summary>
        /// Set defining parameters for a field in one call.
        /// </summary>
        /// <param name="name">the new name to assign</param>
        /// <param name="fieldType">the new type (one of the OFT values like OFTInteger).</param>
        /// <param name="widht">the preferred formatting width. Defaults to zero indicating undefined.</param>
        /// <param name="precision">number of decimals places for formatting, defaults to zero indicating undefined.</param>
        /// <param name="justify">the formatting justification (OJLeft or OJRight), defaults to OJUndefined.</param>
        public void Set(string name, FieldType fieldType, int widht, int precision, Justification justify)
        {
            PInvokeOgr.OGR_Fld_Set(Handle, MarshalUtils.StringToUtf8Bytes(name), fieldType, widht, precision, justify);
        }

        /// <summary>
        /// Set default field value.
        /// The default field value is taken into account by drivers (generally those with a SQL interface) that support it at field creation time. OGR will generally not automatically set the default field value to null fields by itself when calling OGRFeature::CreateFeature() / OGRFeature::SetFeature(), but will let the low-level layers to do the job. So retrieving the feature from the layer is recommended.
        /// The accepted values are NULL, a numeric value, a literal value enclosed between single quote characters (and inner single quote characters escaped by repetition of the single quote character), CURRENT_TIMESTAMP, CURRENT_TIME, CURRENT_DATE or a driver specific expression (that might be ignored by other drivers). For a datetime literal value, format should be 'YYYY/MM/DD HH:MM:SS[.sss]' (considered as UTC time).
        /// Drivers that support writing DEFAULT clauses will advertize the GDAL_DCAP_DEFAULT_FIELDS driver metadata item.
        /// </summary>
        /// <param name="name"></param>
        public void SetDefault(string name)
        {
            PInvokeOgr.OGR_Fld_SetDefault(Handle, MarshalUtils.StringToUtf8Bytes(name));
        }

        /// <summary>
        /// Set whether this field should be omitted when fetching features.
        /// </summary>
        /// <param name="ignore">ignore state</param>
        public void SetIgnored(int ignore)
        {
            PInvokeOgr.OGR_Fld_SetIgnored(Handle, ignore);
        }

        /// <summary>
        /// Set the justification for this field.
        /// Note: no driver is know to use the concept of field justification.
        /// </summary>
        public void SetJustify(Justification justify)
        {
            PInvokeOgr.OGR_Fld_SetJustify(Handle, justify);
        }

        /// <summary>
        /// Reset the name of this field.
        /// </summary>
        public void SetName(string name)
        {
            PInvokeOgr.OGR_Fld_SetName(Handle, MarshalUtils.StringToUtf8Bytes(name));
        }

        /// <summary>
        /// Set whether this field can receive null values.
        /// By default, fields are nullable, so this method is generally called with FALSE to set a not-null constraint.
        /// Drivers that support writing not-null constraint will advertize the GDAL_DCAP_NOTNULL_FIELDS driver metadata item.
        /// </summary>
        public void SetNullable(bool value)
        {
            PInvokeOgr.OGR_Fld_SetNullable(Handle, Convert.ToInt32(value));
        }

        /// <summary>
        /// Set the formatting precision for this field in characters.
        /// This should normally be zero for fields of types other than OFTReal.
        /// </summary>
        /// <param name="precision">the new precision.</param>
        public void SetPrecision(int precision)
        {
            PInvokeOgr.OGR_Fld_SetPrecision(Handle, precision);
        }

        /// <summary>
        /// Set the subtype of this field.
        /// </summary>
        /// <param name="fieldSubType"></param>
        public void SetSubType(FieldSubType fieldSubType)
        {
            PInvokeOgr.OGR_Fld_SetSubType(Handle, fieldSubType);
        }

        /// <summary>
        /// Set the type of this field.
        /// This should never be done to an OGRFieldDefn that is already part of an OGRFeatureDefn.
        /// </summary>
        /// <param name="type">the new field type.</param>
        public void SetType(FieldType type)
        {
            PInvokeOgr.OGR_Fld_SetType(Handle, type);
        }

        /// <summary>
        /// Set the formatting width for this field in characters.
        /// </summary>
        public void SetWidth(int newWidth)
        {
            PInvokeOgr.OGR_Fld_SetWidth(Handle, newWidth);
        }

        /// <summary>
        /// Fetch human readable name for a field type.
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public string GetFieldTypeName(FieldType fieldType)
        {
            IntPtr p = PInvokeOgr.OGR_GetFieldTypeName(fieldType);
            return MarshalUtils.PtrToStringEncoding(p, Encoding.ASCII);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Scanex.Gdal;

namespace Scanex.Gdal
{
    public class Feature : CustomGdalObject
    {
        //типа всё подготовил, кроме указанных
        //не реализовано
        //void OGR_F_DumpReadable	(	OGRFeatureH 	hFeat,FILE * 	fpOut )	
        //int OGR_F_Validate	(	OGRFeatureH 	hFeat,int 	nValidateFlags,int 	bEmitError )	
        //OGRGeometryH OGR_F_StealGeometry	(	OGRFeatureH 	hFeat	)	
        //void OGR_F_SetStyleStringDirectly	(	OGRFeatureH 	hFeat,char * 	pszStyle )	
        //char** OGR_F_GetFieldAsStringList	(	OGRFeatureH 	hFeat,int 	iField )	
        //OGRErr OGR_F_SetFromWithMap	(	OGRFeatureH 	hFeat,OGRFeatureH 	hOtherFeat,int 	bForgiving,int * 	panMap )	

        protected override void DestroyNativeObj()
        {
           PInvokeOgr.OGR_F_Destroy(Handle);
        }

        internal Feature(IntPtr cPtr, bool cMemoryOwn, object parent)
        {
            Init(cPtr, cMemoryOwn, parent);
        }

        public Feature(FeatureDefn featureDefn)
        {
            IntPtr p = PInvokeOgr.OGR_F_Create(featureDefn.Handle);
            Init(p, true, null);
        }

        public bool Equal(Feature otherFeature)
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_F_Equal(Handle, otherFeature.Handle));
        }

        /// <summary>
        /// Fill unset fields with default values that might be defined.
        /// </summary>
        /// <param name="notNullableOnly">if we should fill only unset fields with a not-null constraint.</param>
        /// <param name="options">unused currently. Must be set to NULL.</param>
        public void FillUnsetWithDefault(int notNullableOnly, string[] options)
        {
            using (var l = new MarshalUtils.StringListExport(options))
            {
                PInvokeOgr.OGR_F_FillUnsetWithDefault(Handle, notNullableOnly, l.Pointer);
            }
        }

        public FeatureDefn GetDefnRef()
        {
            IntPtr p = PInvokeOgr.OGR_F_GetDefnRef(Handle);
            if (p == IntPtr.Zero) return null;
            return new FeatureDefn(p, false, this);
        }

        public Int64 FID
        {
            get { return GetFID(); }
            set
            {
                SetFID(value);
            }
        }

        public Int64 GetFID()
        {
            return PInvokeOgr.OGR_F_GetFID(Handle); 
        }

        public void SetFID(Int64 value)
        {
            var ret = PInvokeOgr.OGR_F_SetFID(Handle, value);
            Errors.CheckError(ret);
        }

        public byte[] GetFieldAsBinary(int index)
        {
            int bytes;
            IntPtr p = PInvokeOgr.OGR_F_GetFieldAsBinary(Handle, index, out bytes);
            byte[] ret = new byte[bytes];
            Marshal.Copy(p, ret, 0, bytes);
            return ret;
        }

        
        /// <summary>
        /// Set feature geometry of a specified geometry field.
        /// This function updates the features geometry, and operate exactly as SetGeometryDirectly(), except that this function does not assume ownership of the passed geometry, but instead makes a copy of it.
        /// </summary>
        /// <param name="iField">geometry field to set.</param>
        /// <param name="geometry">the new geometry to apply to feature.</param>
        public void SetGeomField(int iField, Geometry geometry)
        {
            var ret = PInvokeOgr.OGR_F_SetGeomField(Handle, iField, geometry.Handle);
            Errors.CheckError(ret);
        }

        /// <summary>
        /// Set feature geometry of a specified geometry field.
        /// This function updates the features geometry, and operate exactly as SetGeomField(), except that this function assumes ownership of the passed geometry (even in case of failure of that function).
        /// </summary>
        /// <param name="iField">geometry field to set.</param>
        /// <param name="geometry">the new geometry to apply to feature.</param>
        public void SetGeomFieldDirectly(int iField, Geometry geometry)
        {
            var ret = PInvokeOgr.OGR_F_SetGeomFieldDirectly(Handle, iField, geometry.Handle);
            Errors.CheckError(ret);
            geometry.NewOwner(this);
        }

        /// <summary>
        /// Sets the native data for the feature.
        /// The native data is the representation in a "natural" form that comes from the driver that created this feature, or that is aimed at an output driver. The native data may be in different format, which is indicated by OGR_F_GetNativeMediaType().
        /// Since GDAL 2.1
        /// </summary>
        /// <param name="nativeData">a string with the native data, or NULL if there is none.</param>
        public void SetNativeData(string nativeData)
        {
            PInvokeOgr.OGR_F_SetNativeData(Handle, MarshalUtils.StringToUtf8Bytes(nativeData));
        }

        /// <summary>
        /// Sets the native media type for the feature.
        /// The native media type is the identifier for the format of the native data. It follows the IANA RFC 2045 (see https://en.wikipedia.org/wiki/Media_type), e.g. "application/vnd.geo+json" for JSon.
        /// Since GDAL 2.1
        /// </summary>
        /// <param name="nativeMediaType">a string with the native media type, or NULL if there is none.</param>
        public void SetNativeMediaType(string nativeMediaType)
        {
            PInvokeOgr.OGR_F_SetNativeMediaType(Handle, MarshalUtils.StringToUtf8Bytes(nativeMediaType));
        }

        /// <summary>
        /// Set feature style string.
        /// This method operate exactly as OGR_F_SetStyleStringDirectly() except that it does not assume ownership of the passed string, but instead makes a copy of it.
        /// </summary>
        /// <param name="style">the style string to apply to this feature, cannot be NULL.</param>
        public void SetStyleString(string style)
        {
            PInvokeOgr.OGR_F_SetStyleString(Handle, MarshalUtils.StringToUtf8Bytes(style));
        }

        /// <summary>
        /// Clear a field, marking it as unset.
        /// </summary>
        /// <param name="iField">the field to unset.</param>
        public void UnsetField(int iField)
        {
            PInvokeOgr.OGR_F_UnsetField(Handle, iField);
        }

        /// <summary>
        /// Duplicate feature.
        /// The newly created feature is owned by the caller, and will have it's own reference to the OGRFeatureDefn.
        /// </summary>
        /// <returns></returns>
        public Feature Clone()
        {
            IntPtr p = PInvokeOgr.OGR_F_Clone(Handle);
            if (p == IntPtr.Zero) return null;
            return new Feature(p, true, null);
        }

        /// <summary>
        /// Fetch field value as date and time.
        /// </summary>
        /// <param name="iField"></param>
        /// <returns></returns>
        public DateTime? GetFieldAsDateTime(int iField)
        {
            int year;
            int month;
            int day;
            int hour;
            int minute;
            int second;
            int flag;
            int ret = PInvokeOgr.OGR_F_GetFieldAsDateTime(Handle, iField, out year, out month, out day, out hour, out minute, out second, out flag);
            if (!Convert.ToBoolean(ret)) return null;
            DateTimeKind dk = DateTimeKind.Unspecified;
            if (flag == 1) dk = DateTimeKind.Local;
            if (flag == 100) dk = DateTimeKind.Utc;
            DateTime dt = new DateTime(year, month, day, hour, minute, second, dk);
            return dt;
        }
        /// <summary>
        /// Fetch field value as date and time with millisecond accuracy. Since GDAL 2.0
        /// </summary>
        /// <param name="iField"></param>
        /// <returns></returns>
        public DateTime? GetFieldAsDateTimeEx(int iField)
        {
            int year;
            int month;
            int day;
            int hour;
            int minute;
            float second;
            int flag;
            int ret = PInvokeOgr.OGR_F_GetFieldAsDateTimeEx(Handle, iField, out year, out month, out day, out hour, out minute, out second, out flag);
            if (!Convert.ToBoolean(ret)) return null;
            DateTimeKind dk = DateTimeKind.Unspecified;
            if (flag == 1) dk = DateTimeKind.Local;
            if (flag == 100) dk = DateTimeKind.Utc;

            int sec = (int)Math.Floor(second);
            int msec = (int) Math.Floor((second - sec)*1000.0);
            DateTime dt = new DateTime(year, month, day, hour, minute, sec, msec, dk);
            return dt;
        }

        /// <summary>
        /// Fetch field value as a double.
        /// </summary>
        /// <param name="iField">the field to fetch, from 0 to GetFieldCount()-1.</param>
        /// <returns>the field value.</returns>
        public double GetFieldAsDouble(int iField)
        {
            return PInvokeOgr.OGR_F_GetFieldAsDouble(Handle, iField);
        }

        public double[] GetFieldAsDoubleList(int iField)
        {
            int count;
            IntPtr p = PInvokeOgr.OGR_F_GetFieldAsDoubleList(Handle, iField, out count);
            double[] buf = new double[count];
            Marshal.Copy(p, buf, 0, count);
            return buf;
        }

        public int GetFieldAsInteger(int iField)
        {
            return PInvokeOgr.OGR_F_GetFieldAsInteger(Handle, iField);
        }

        public Int64 GetFieldAsInteger64(int iField)
        {
            return PInvokeOgr.OGR_F_GetFieldAsInteger64(Handle, iField);
        }

        public Int64[] GetFieldAsInteger64List(int iField)
        {
            int count;
            IntPtr p = PInvokeOgr.OGR_F_GetFieldAsInteger64List(Handle, iField, out count);
            Int64[] buf = new Int64[count];
            Marshal.Copy(p, buf, 0, count);
            return buf;
        }

        public int[] GetFieldAsIntegerList(int iField)
        {
            int count;
            IntPtr p = PInvokeOgr.OGR_F_GetFieldAsIntegerList(Handle, iField, out count);
            int[] buf = new int[count];
            Marshal.Copy(p, buf, 0, count);
            return buf;
        }

        public string GetFieldAsString(int iField)
        {
            return GetFieldAsString(iField, MarshalUtils.DefaultEncoding);
        }

        public string GetFieldAsString(int iField, Encoding encoding)
        {
            IntPtr p = PInvokeOgr.OGR_F_GetFieldAsString(Handle, iField);
            return MarshalUtils.PtrToStringEncoding(p, encoding);
        }

        /// <summary>
        /// Fetch number of fields on this feature This will always be the same as the field count for the OGRFeatureDefn.
        /// </summary>
        public int GetFieldCount()
        {
            return PInvokeOgr.OGR_F_GetFieldCount(Handle);
        }

        /// <summary>
        /// Fetch definition for this field.
        /// </summary>
        /// <param name="i">he field to fetch, from 0 to GetFieldCount()-1.</param>
        /// <returns>an handle to the field definition (from the OGRFeatureDefn). This is an internal reference, and should not be deleted or modified.</returns>
        public FieldDefn GetFieldDefnRef(int i)
        {
            IntPtr p = PInvokeOgr.OGR_F_GetFieldDefnRef(Handle, i);
            if (p == IntPtr.Zero) return null;
            return new FieldDefn(p, false, this);
        }

        /// <summary>
        /// Fetch the field index given field name.
        /// This is a cover for the OGRFeatureDefn::GetFieldIndex() method.
        /// </summary>
        /// <param name="name">the name of the field to search for.</param>
        /// <returns>the field index, or -1 if no matching field is found.</returns>
        public int GetFieldIndex(string name)
        {
            return GetFieldIndex(name, MarshalUtils.DefaultEncoding);
        }

        /// <summary>
        /// Fetch the field index given field name.
        /// This is a cover for the OGRFeatureDefn::GetFieldIndex() method.
        /// </summary>
        /// <param name="name">the name of the field to search for.</param>
        /// <returns>the field index, or -1 if no matching field is found.</returns>
        public int GetFieldIndex(string name, Encoding encoding)
        {
            using (var s1 = new MarshalUtils.StringExport(name, encoding))
            {
                int ret = PInvokeOgr.OGR_F_GetFieldIndex(Handle, s1.Pointer);
                return ret;
            }
        }

        /// <summary>
        /// Fetch an handle to feature geometry.
        /// </summary>
        /// <returns>an handle to internal feature geometry. This object should not be modified.</returns>
        public Geometry GetGeometryRef()
        {
            IntPtr p = PInvokeOgr.OGR_F_GetGeometryRef(Handle);
            if (p == IntPtr.Zero) return null;
            return new Geometry(p, false, this);
        }

        /// <summary>
        /// Fetch number of geometry fields on this feature This will always be the same as the geometry field count for the OGRFeatureDefn.
        /// Since GDAL 1.11
        /// </summary>
        /// <returns>count of geometry fields.</returns>
        public int GetGeomFieldCount()
        {
            int ret = PInvokeOgr.OGR_F_GetGeomFieldCount(Handle);
            return ret;
        }

        /// <summary>
        /// Fetch definition for this geometry field.
        /// Since GDAL 1.11
        /// </summary>
        /// <param name="index">the field to fetch, from 0 to GetGeomFieldCount()-1.</param>
        /// <returns>an handle to the field definition (from the OGRFeatureDefn). This is an internal reference, and should not be deleted or modified.</returns>
        public GeomFieldDefn GetGeomFieldDefnRef(int index)
        {
            IntPtr p = PInvokeOgr.OGR_F_GetGeomFieldDefnRef(Handle, index);
            if (p == IntPtr.Zero) return null;
            return new GeomFieldDefn(p, false, this);
        }

        /// <summary>
        /// Fetch the geometry field index given geometry field name.
        /// This is a cover for the OGRFeatureDefn::GetGeomFieldIndex() method.
        /// Since GDAL 1.11
        /// </summary>
        /// <param name="name">the name of the geometry field to search for.</param>
        /// <returns>the geometry field index, or -1 if no matching geometry field is found.</returns>
        public int GetGeomFieldIndex(string name)
        {
            return PInvokeOgr.OGR_F_GetGeomFieldIndex(Handle, MarshalUtils.StringToUtf8Bytes(name));
        }

        /// <summary>
        /// Fetch an handle to feature geometry.
        /// Since GDAL 1.11
        /// </summary>
        /// <param name="iField">geometry field to get.</param>
        /// <returns>an handle to internal feature geometry. This object should not be modified.</returns>
        public Geometry GetGeomFieldRef(int iField)
        {
            IntPtr p = PInvokeOgr.OGR_F_GetGeomFieldRef(Handle, iField);
            if (p == IntPtr.Zero) return null;
            return new Geometry(p, false, this);
        }

        /// <summary>
        /// Returns the native data for the feature.
        /// The native data is the representation in a "natural" form that comes from the driver that created this feature, or that is aimed at an output driver. The native data may be in different format, which is indicated by OGR_F_GetNativeMediaType().
        /// Note that most drivers do not support storing the native data in the feature object, and if they do, generally the NATIVE_DATA open option must be passed at dataset opening.
        /// The "native data" does not imply it is something more performant or powerful than what can be obtained with the rest of the API, but it may be useful in round-tripping scenarios where some characteristics of the underlying format are not captured otherwise by the OGR abstraction.
        /// Since GDAL 2.1
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns>a string with the native data, or NULL if there is none</returns>
        public string GetNativeData()
        {
            return GetNativeData(MarshalUtils.DefaultEncoding);
        }
        /// <summary>
        /// Returns the native data for the feature.
        /// The native data is the representation in a "natural" form that comes from the driver that created this feature, or that is aimed at an output driver. The native data may be in different format, which is indicated by OGR_F_GetNativeMediaType().
        /// Note that most drivers do not support storing the native data in the feature object, and if they do, generally the NATIVE_DATA open option must be passed at dataset opening.
        /// The "native data" does not imply it is something more performant or powerful than what can be obtained with the rest of the API, but it may be useful in round-tripping scenarios where some characteristics of the underlying format are not captured otherwise by the OGR abstraction.
        /// Since GDAL 2.1
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns>a string with the native data, or NULL if there is none</returns>
        public string GetNativeData(Encoding encoding)
        {
            return MarshalUtils.PtrToStringEncoding(PInvokeOgr.OGR_F_GetNativeData(Handle), encoding);
        }

        public string GetNativeMediaType()
        {
            return GetNativeMediaType(MarshalUtils.DefaultEncoding);
        }
        /// <summary>
        /// Returns the native media type for the feature.
        /// The native media type is the identifier for the format of the native data. It follows the IANA RFC 2045 (see https://en.wikipedia.org/wiki/Media_type), e.g. "application/vnd.geo+json" for JSon.
        /// Since GDAL 2.1
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns>a string with the native media type, or NULL if there is none.</returns>
        public string GetNativeMediaType(Encoding encoding)
        {
            return MarshalUtils.PtrToStringEncoding(PInvokeOgr.OGR_F_GetNativeData(Handle), encoding);
        }
        /// <summary>
        /// Fetch an handle to the internal field value given the index.
        /// </summary>
        /// <param name="iField"></param>
        /// <returns>the returned handle is to an internal data structure, and should not be freed, or modified.</returns>
        public Field GetRawFieldRef(int iField)
        {
            IntPtr p = PInvokeOgr.OGR_F_GetRawFieldRef(Handle, iField);
            if (p == IntPtr.Zero) return null;
            return new Field(p, false, this);
        }

        /// <summary>
        /// Fetch style string for this feature.
        /// Set the OGR Feature Style Specification for details on the format of this string, and ogr_featurestyle.h for services available to parse it.
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns>a reference to a representation in string format, or NULL if there isn't one.</returns>
        public string GetStyleString()
        {
            return GetStyleString(MarshalUtils.DefaultEncoding);
        }
        /// <summary>
        /// Fetch style string for this feature.
        /// Set the OGR Feature Style Specification for details on the format of this string, and ogr_featurestyle.h for services available to parse it.
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns>a reference to a representation in string format, or NULL if there isn't one.</returns>
        public string GetStyleString(Encoding encoding)
        {
            return MarshalUtils.PtrToStringEncoding(PInvokeOgr.OGR_F_GetStyleString(Handle), encoding);
        }

        /// <summary>
        /// Test if a field has ever been assigned a value or not.
        /// </summary>
        /// <param name="iField">the field to test.</param>
        /// <returns>TRUE if the field has been set, otherwise false.</returns>
        public bool IsFieldSet(int iField)
        {
            int ret = PInvokeOgr.OGR_F_IsFieldSet(Handle, iField);
            return Convert.ToBoolean(ret);
        }
        /// <summary>
        /// Set field to binary data.
        /// This function currently on has an effect of OFTBinary fields.
        /// </summary>
        /// <param name="iField">the field to set, from 0 to GetFieldCount()-1</param>
        /// <param name="data"></param>
        public void SetFieldBinary(int iField, byte[] data)
        {
            using (var exp = new MarshalUtils.BytesExport(data))
            {
                PInvokeOgr.OGR_F_SetFieldBinary(Handle, iField, exp.Length, exp.Pointer);
            }
        }

        /// <summary>
        /// Set field to datetime with millisecond accuracy. 
        /// Since GDAL 2.0
        /// </summary>
        /// <param name="iField"></param>
        /// <param name="value"></param>
        public void SetFieldDateTimeEx(int iField, DateTime value)
        {
            float sec = value.Second + (float)(value.Millisecond / 1000.0);
            int flag = 0;
            if (value.Kind == DateTimeKind.Local) flag = 1;
            if (value.Kind == DateTimeKind.Utc) flag = 100;
            PInvokeOgr.OGR_F_SetFieldDateTimeEx(Handle, iField, value.Year, value.Month, value.Day, value.Hour, value.Minute, sec, flag);
        }

        /// <summary>
        /// Set field to datetime. 
        /// </summary>
        public void SetFieldDateTime(int iField, DateTime value)
        {
            int flag = 0;
            if (value.Kind == DateTimeKind.Local) flag = 1;
            if (value.Kind == DateTimeKind.Utc) flag = 100;
            PInvokeOgr.OGR_F_SetFieldDateTime(Handle, iField, value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, flag);
        }
        /// <summary>
        /// Set field to datetime. 
        /// </summary>
        public void SetFieldTime(int iField, TimeSpan value)
        {
            int flag;
            flag = 1;
            PInvokeOgr.OGR_F_SetFieldDateTime(Handle, iField, 0, 0, 0, (int)value.TotalHours, (int)value.TotalMinutes, (int)value.TotalSeconds, flag);
        }
        /// <summary>
        /// Set field to double value.
        /// OFTInteger, OFTInteger64 and OFTReal fields will be set directly. OFTString fields will be assigned a string representation of the value, but not necessarily taking into account formatting constraints on this field. Other field types may be unaffected.
        /// </summary>
        /// <param name="iField"></param>
        /// <param name="value"></param>
        public void SetFieldDouble(int iField, double value)
        {
            PInvokeOgr.OGR_F_SetFieldDouble(Handle, iField, value);
        }
        /// <summary>
        /// Set field to list of doubles value.
        /// This function currently on has an effect of OFTIntegerList, OFTInteger64List, OFTRealList fields.
        /// </summary>
        public void SetFieldDoubleList(int iField, double[] values)
        {
            using (var exp = new MarshalUtils.DoublesExport(values))
            {
                PInvokeOgr.OGR_F_SetFieldDoubleList(Handle, iField, exp.Length, exp.Pointer);
            }
        }

        /// <summary>
        /// Set field to integer value.
        /// OFTInteger, OFTInteger64 and OFTReal fields will be set directly. OFTString fields will be assigned a string representation of the value, but not necessarily taking into account formatting constraints on this field. Other field types may be unaffected.
        /// </summary>
        public void SetFieldInteger(int iField, int value)
        {
            PInvokeOgr.OGR_F_SetFieldInteger(Handle, iField, value);
        }

        /// <summary>
        /// Set field to 64 bit integer value.
        /// OFTInteger, OFTInteger64 and OFTReal fields will be set directly. OFTString fields will be assigned a string representation of the value, but not necessarily taking into account formatting constraints on this field. Other field types may be unaffected.
        /// Since GDAL 2.0
        /// </summary>
        /// <param name="iField">the field to fetch, from 0 to GetFieldCount()-1.</param>
        /// <param name="value"></param>
        public void SetFieldInteger64(int iField, Int64 value)
        {
            PInvokeOgr.OGR_F_SetFieldInteger64(Handle, iField, value);
        }
        /// <summary>
        /// Set field to list of 64 bit integers value.
        /// This function currently on has an effect of OFTIntegerList, OFTInteger64List and OFTRealList fields.
        /// </summary>
        public void SetFieldInteger64List(int iField, Int64[] values)
        {
            using (var exp = new MarshalUtils.Int64Export(values))
            {
                PInvokeOgr.OGR_F_SetFieldInteger64List(Handle, iField, exp.Length, exp.Pointer);
            }
        }
        /// <summary>
        /// Set field to list of integers value.
        /// This function currently on has an effect of OFTIntegerList, OFTInteger64List and OFTRealList fields.
        /// </summary>
        public void SetFieldIntegerList(int iField, int[] values)
        {
            using (var exp = new MarshalUtils.IntExport(values))
            {
                PInvokeOgr.OGR_F_SetFieldIntegerList(Handle, iField, exp.Length, exp.Pointer);
            }
        }

        /// <summary>
        /// Set field.
        /// The passed value OGRField must be of exactly the same type as the target field, or an application crash may occur. The passed value is copied, and will not be affected. It remains the responsibility of the caller.
        /// </summary>
        /// <param name="iField"></param>
        /// <param name="field"></param>
        public void SetFieldRaw(int iField, Field field)
        {
            if (field == null)
            {
                PInvokeOgr.OGR_F_SetFieldRaw(Handle, iField, IntPtr.Zero);
            }
            else
                PInvokeOgr.OGR_F_SetFieldRaw(Handle, iField, field.Handle);
        }

        public void SetFieldString(int iField, string value, Encoding encoding)
        {
            using (var s = new MarshalUtils.StringExport(value, encoding))
            {
                PInvokeOgr.OGR_F_SetFieldString(Handle, iField, s.Pointer);
            }
        }

        public void SetFieldString(int iField, string value)
        {
            using (var s = new MarshalUtils.StringExport(value))
            {
                PInvokeOgr.OGR_F_SetFieldString(Handle, iField, s.Pointer);
            }
        }

        public void SetFieldStringList(int iField, string[] values)
        {
            using (var s = new MarshalUtils.StringListExport(values))
            {
                PInvokeOgr.OGR_F_SetFieldStringList(Handle, iField, s.Pointer);
            }
        }
        /// <summary>
        /// Set one feature from another.
        /// Overwrite the contents of this feature from the geometry and attributes of another. The hOtherFeature does not need to have the same OGRFeatureDefn. Field values are copied by corresponding field names. Field types do not have to exactly match. OGR_F_SetField*() function conversion rules will be applied as needed.
        /// </summary>
        /// <param name="otherFature">handle to the feature from which geometry, and field values will be copied.</param>
        /// <param name="forgiving">TRUE if the operation should continue despite lacking output fields matching some of the source fields.</param>
        public void SetFrom(Feature otherFature, bool forgiving)
        {
            var ret = PInvokeOgr.OGR_F_SetFrom(Handle, otherFature.Handle, Convert.ToInt32(forgiving));
            Errors.CheckError(ret);
        }

        /// <summary>
        /// Set feature geometry.
        /// This function updates the features geometry, and operate exactly as SetGeometryDirectly(), except that this function does not assume ownership of the passed geometry, but instead makes a copy of it.
        /// </summary>
        /// <param name="geometry">handle to the new geometry to apply to feature.</param>
        /// <returns>OGRERR_NONE if successful, or OGR_UNSUPPORTED_GEOMETRY_TYPE if the geometry type is illegal for the OGRFeatureDefn (checking not yet implemented).</returns>
        public bool SetGeometry(Geometry geometry)
        {
            if (geometry == null) PInvokeOgr.OGR_F_SetGeometry(Handle, IntPtr.Zero);
            var ret = PInvokeOgr.OGR_F_SetGeometry(Handle, geometry.Handle);
            return Errors.IsNoError(ret);
        }

        public bool SetGeometryDirectly(Geometry geometry)
        {
            var ret = PInvokeOgr.OGR_F_SetGeometryDirectly(Handle, geometry.Handle);
            geometry.NewOwner(this);
            return Errors.IsNoError(ret);
        }

        public void SetField(int indexField, string value)
        {
            SetFieldString(indexField, value);
        }

        public void SetField(int indexField, TimeSpan value)
        {
            SetFieldTime(indexField, value);
        }

        public void SetField(int indexField, string value, Encoding encoding)
        {
            SetFieldString(indexField, value, encoding);
        }

        public void SetField(int indexField, int value)
        {
            SetFieldInteger(indexField, value);
        }

        public void SetField(int indexField, Int64 value)
        {
            SetFieldInteger64(indexField, value);
        }

        public void SetField(int indexField, double value)
        {
            SetFieldDouble(indexField, value);
        }

        public void SetField(int indexField, DateTime value)
        {
            SetFieldDateTime(indexField, value);
        }

        public void SetField(int indexField, byte[] value)
        {
            SetFieldBinary(indexField, value);
        }




        public bool SetField(string fieldName, string value)
        {
            var idx = GetFieldIndex(fieldName);
            if (idx < 0) return false;
            SetFieldString(idx, value);
            return true;
        }

        public bool SetField(string fieldName, string value, Encoding encoding)
        {
            var idx = GetFieldIndex(fieldName, encoding);
            if (idx < 0) return false;
            SetFieldString(idx, value, encoding);
            return true;
        }

        public bool SetField(string fieldName, int value)
        {
            return SetField(fieldName, value, MarshalUtils.DefaultEncoding);
        }

        public bool SetField(string fieldName, int value, Encoding encoding)
        {
            var idx = GetFieldIndex(fieldName, encoding);
            if (idx < 0) return false;
            SetFieldInteger(idx, value);
            return true;
        }

        public bool SetField(string fieldName, Int64 value)
        {
            return SetField(fieldName, value, MarshalUtils.DefaultEncoding);
        }

        public bool SetField(string fieldName, Int64 value, Encoding encoding)
        {
            var idx = GetFieldIndex(fieldName, encoding);
            if (idx < 0) return false;
            SetFieldInteger64(idx, value);
            return true;
        }

        public bool SetField(string fieldName, double value)
        {
            return SetField(fieldName, value, MarshalUtils.DefaultEncoding);
        }

        public bool SetField(string fieldName, double value, Encoding encoding)
        {
            var idx = GetFieldIndex(fieldName, encoding);
            if (idx < 0) return false;
            SetFieldDouble(idx, value);
            return true;
        }

        public bool SetField(string fieldName, DateTime value)
        {
            return SetField(fieldName, value, MarshalUtils.DefaultEncoding);
        }

        public bool SetField(string fieldName, DateTime value, Encoding encoding)
        {
            var idx = GetFieldIndex(fieldName);
            if (idx < 0) return false;
            SetFieldDateTime(idx, value);
            return true;
        }

        public bool SetField(string fieldName, byte[] value)
        {
            return SetField(fieldName, value, MarshalUtils.DefaultEncoding);
        }

        public bool SetField(string fieldName, byte[] value, Encoding encoding)
        {
            var idx = GetFieldIndex(fieldName);
            if (idx < 0) return false;
            SetFieldBinary(idx, value);
            return true;
        }

    }
}

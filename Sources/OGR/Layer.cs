using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Scanex.Gdal;

namespace Scanex.Gdal
{
    public class Layer : CustomGdalObject
    {
        //НЕ ВСЁ. ПЕРЕПРОВЕРИТЬ.

        //не реализовано
        //OGRErr OGR_L_Identity	(	OGRLayerH 	pLayerInput,OGRLayerH 	pLayerMethod,OGRLayerH 	pLayerResult,char ** 	papszOptions,GDALProgressFunc 	pfnProgress,void * 	pProgressArg )	
        //OGRErr OGR_L_Intersection	(	OGRLayerH 	pLayerInput,OGRLayerH 	pLayerMethod,OGRLayerH 	pLayerResult,char ** 	papszOptions,GDALProgressFunc 	pfnProgress,void * 	pProgressArg )	
        //OGRErr OGR_L_ReorderFields	(	OGRLayerH 	hLayer,int * 	panMap )	
        //OGRErr OGR_L_SetIgnoredFields	(	OGRLayerH 	,const char ** 	papszFields )	
        //OGRErr OGR_L_SymDifference	(	OGRLayerH 	pLayerInput,OGRLayerH 	pLayerMethod,OGRLayerH 	pLayerResult,char ** 	papszOptions,GDALProgressFunc 	pfnProgress,void * 	pProgressArg )	
        //int OGR_L_TestCapability	(	OGRLayerH 	hLayer,const char * 	pszCap )	
        //OGRErr OGR_L_Union	(	OGRLayerH 	pLayerInput,OGRLayerH 	pLayerMethod,OGRLayerH 	pLayerResult,char ** 	papszOptions,GDALProgressFunc 	pfnProgress,void * 	pProgressArg )		
        //OGRErr OGR_L_Update	(	OGRLayerH 	pLayerInput,OGRLayerH 	pLayerMethod,OGRLayerH 	pLayerResult,char ** 	papszOptions,GDALProgressFunc 	pfnProgress,void * 	pProgressArg )	

        protected override void DestroyNativeObj()
        {
           // OsrPINVOKE.OCTDestroyCoordinateTransformation(Handle);
        }

        internal Layer(IntPtr handle)
        {
            Init(handle, true, null);
        }

        internal Layer(IntPtr cPtr, bool cMemoryOwn, object parent)
        {
            Init(cPtr, cMemoryOwn, parent);
        }

        /// <summary>
        /// Return the layer name. This returns the same content as GetLayerDefn().GetName()
        /// </summary>
        public string GetName()
        {
            return GetName(Encoding.UTF8);
        }

        /// <summary>
        /// Return the layer name. This returns the same content as GetLayerDefn().GetName()
        /// </summary>
        public string GetName(Encoding encoding)
        {
            IntPtr p = PInvokeOgr.OGR_L_GetName(Handle);
            return MarshalUtils.PtrToStringEncoding(p, encoding);
        }

        public FeatureDefn GetLayerDefn()
        {
            IntPtr p = PInvokeOgr.OGR_L_GetLayerDefn(Handle);
            if (p == IntPtr.Zero)
            {
                Errors.ThrowLastError();
                return null;
            }
            return new FeatureDefn(p, false, this);
        }
        /// <summary>
        /// Fetch the next available feature from this layer.
        /// The returned feature becomes the responsibility of the caller to delete with OGR_F_Destroy(). It is critical that all features associated with an OGRLayer (more specifically an OGRFeatureDefn) be deleted before that layer/datasource is deleted.
        /// Only features matching the current spatial filter (set with SetSpatialFilter()) will be returned.
        /// This function implements sequential access to the features of a layer. The OGR_L_ResetReading() function can be used to start at the beginning again.
        /// Features returned by OGR_GetNextFeature() may or may not be affected by concurrent modifications depending on drivers. A guaranteed way of seeing modifications in effect is to call OGR_L_ResetReading() on layers where OGR_GetNextFeature() has been called, before reading again. Structural changes in layers (field addition, deletion, ...) when a read is in progress may or may not be possible depending on drivers. If a transaction is committed/aborted, the current sequential reading may or may not be valid after that operation and a call to OGR_L_ResetReading() might be needed.
        /// </summary>
        /// <returns>an handle to a feature, or NULL if no more features are available.</returns>
        public Feature GetNextFeature()
        {
            IntPtr p = PInvokeOgr.OGR_L_GetNextFeature(Handle);
            if (p == IntPtr.Zero) return null;
            return new Feature(p, true, null);
        }

        /// <summary>
        /// This function returns the current spatial filter for this layer.
        /// The returned pointer is to an internally owned object, and should not be altered or deleted by the caller.
        /// </summary>
        /// <returns>spatial reference, or NULL if there isn't one.</returns>
        public Geometry GetSpatialFilter()
        {
            IntPtr p = PInvokeOgr.OGR_L_GetSpatialFilter(Handle);
            if (p == IntPtr.Zero) return null;
            return new Geometry(p, false, this);
        }

        /// <summary>
        /// Fetch the spatial reference system for this layer.
        /// The returned object is owned by the OGRLayer and should not be modified or freed by the application.
        /// </summary>
        /// <returns>spatial reference, or NULL if there isn't one.</returns>
        public SpatialReference GetSpatialRef()
        {
            IntPtr p = PInvokeOgr.OGR_L_GetSpatialRef(Handle);
            if (p == IntPtr.Zero) return null;
            return new SpatialReference(p, false, null);
        }

        /// <summary>
        /// Reorder an existing field on a layer.
        /// This function is a convenience wrapper of OGR_L_ReorderFields() dedicated to move a single field.
        /// You must use this to reorder existing fields on a real layer. Internally the OGRFeatureDefn for the layer will be updated to reflect the reordering of the fields. Applications should never modify the OGRFeatureDefn used by a layer directly.
        /// This function should not be called while there are feature objects in existence that were obtained or created with the previous layer definition.
        /// The field definition that was at initial position iOldFieldPos will be moved at position iNewFieldPos, and elements between will be shuffled accordingly.
        /// For example, let suppose the fields were "0","1","2","3","4" initially. ReorderField(1, 3) will reorder them as "0","2","3","1","4".
        /// Not all drivers support this function. You can query a layer to check if it supports it with the OLCReorderFields capability. Some drivers may only support this method while there are still no features in the layer. When it is supported, the existing features of the backing file/database should be updated accordingly.
        /// Since OGR 1.9.0
        /// </summary>
        /// <param name="oldFieldPos">previous position of the field to move. Must be in the range [0,GetFieldCount()-1].</param>
        /// <param name="newFieldPos">new position of the field to move. Must be in the range [0,GetFieldCount()-1].</param>
        /// <returns>True on success.</returns>
        public bool ReorderField(int oldFieldPos, int newFieldPos)
        {
            var errCode = PInvokeOgr.OGR_L_ReorderField(Handle, oldFieldPos, newFieldPos);
            return Errors.IsNoError(errCode);
        }

        /// <summary>
        /// Reset feature reading to start on the first feature.
        /// This affects GetNextFeature().
        /// </summary>
        public void ResetReading()
        {
            PInvokeOgr.OGR_L_ResetReading(Handle);
        }

        /// <summary>
        /// For datasources which support transactions, RollbackTransaction will roll back a datasource to its state before the start of the current transaction.
        /// If no transaction is active, or the rollback fails, will return OGRERR_FAILURE. Datasources which do not support transactions will always return OGRERR_NONE.
        /// </summary>
        /// <returns>True on success.</returns>
        public bool RollbackTransaction()
        {
            var errcode = PInvokeOgr.OGR_L_RollbackTransaction(Handle);
            return Errors.IsNoError(errcode);
        }

        /// <summary>
        /// Set a new attribute query.
        /// This function sets the attribute query string to be used when fetching features via the OGR_L_GetNextFeature() function. Only features for which the query evaluates as true will be returned.
        /// The query string should be in the format of an SQL WHERE clause. For instance "population > 1000000 and population < 5000000" where population is an attribute in the layer. The query format is a restricted form of SQL WHERE clause as defined "eq_format=restricted_where" about half way through this document:
        /// http://ogdi.sourceforge.net/prop/6.2.CapabilitiesMetadata.html
        /// Note that installing a query string will generally result in resetting the current reading position (ala OGR_L_ResetReading()).
        /// </summary>
        /// <param name="query">query in restricted SQL WHERE format, or NULL to clear the current query.</param>
        /// <returns>True if successfully installed, or an error code if the query expression is in error, or some other failure occurs.</returns>
        public bool SetAttributeFilter(string query)
        {
            using (var s1 = new MarshalUtils.StringExport(query))
            {
                int errCode = PInvokeOgr.OGR_L_SetAttributeFilter(Handle, s1.Pointer);
                return Errors.IsNoError(errCode);
            }
        }
        /// <summary>
        /// Rewrite an existing feature.
        /// This function will write a feature to the layer, based on the feature id within the OGRFeature.
        /// Use OGR_L_TestCapability(OLCRandomWrite) to establish if this layer supports random access writing via OGR_L_SetFeature().
        /// </summary>
        /// <param name="feature">the feature to write.</param>
        /// <returns>True if the operation works, otherwise an appropriate error code (e.g OGRERR_NON_EXISTING_FEATURE if the feature does not exist).</returns>
        public bool SetFeature(Feature feature)
        {
            int errCode = PInvokeOgr.OGR_L_SetFeature(Handle, feature.Handle);
            return Errors.IsNoError(errCode);
        }

        /// <summary>
        /// Move read cursor to the nIndex'th feature in the current resultset.
        /// This method allows positioning of a layer such that the GetNextFeature() call will read the requested feature, where nIndex is an absolute index into the current result set. So, setting it to 3 would mean the next feature read with GetNextFeature() would have been the 4th feature to have been read if sequential reading took place from the beginning of the layer, including accounting for spatial and attribute filters.
        /// Only in rare circumstances is SetNextByIndex() efficiently implemented. In all other cases the default implementation which calls ResetReading() and then calls GetNextFeature() nIndex times is used. To determine if fast seeking is available on the current layer use the TestCapability() method with a value of OLCFastSetNextByIndex.
        /// </summary>
        /// <param name="index">	the index indicating how many steps into the result set to seek.</param>
        /// <returns>True on success or an error code.</returns>
        public bool SetNextByIndex(Int64 index)
        {
            int errCode = PInvokeOgr.OGR_L_SetNextByIndex(Handle, index);
            return Errors.IsNoError(errCode);
        }

        /// <summary>
        /// Set a new spatial filter.
        /// This function set the geometry to be used as a spatial filter when fetching features via the OGR_L_GetNextFeature() function. Only features that geometrically intersect the filter geometry will be returned.
        /// Currently this test is may be inaccurately implemented, but it is guaranteed that all features who's envelope (as returned by OGR_G_GetEnvelope()) overlaps the envelope of the spatial filter will be returned. This can result in more shapes being returned that should strictly be the case.
        /// This function makes an internal copy of the passed geometry. The passed geometry remains the responsibility of the caller, and may be safely destroyed.
        /// For the time being the passed filter geometry should be in the same SRS as the layer (as returned by OGR_L_GetSpatialRef()). In the future this may be generalized.
        /// </summary>
        /// <param name="geometry"></param>
        public void SetSpatialFilter(Geometry geometry)
        {
            PInvokeOgr.OGR_L_SetSpatialFilter(Handle, (geometry == null)?IntPtr.Zero:geometry.Handle);
        }

        /// <summary>
        /// Set a new spatial filter.
        /// This function set the geometry to be used as a spatial filter when fetching features via the OGR_L_GetNextFeature() function. Only features that geometrically intersect the filter geometry will be returned.
        /// Currently this test is may be inaccurately implemented, but it is guaranteed that all features who's envelope (as returned by OGR_G_GetEnvelope()) overlaps the envelope of the spatial filter will be returned. This can result in more shapes being returned that should strictly be the case.
        /// This function makes an internal copy of the passed geometry. The passed geometry remains the responsibility of the caller, and may be safely destroyed.
        /// For the time being the passed filter geometry should be in the same SRS as the geometry field definition it corresponds to (as returned by GetLayerDefn()->OGRFeatureDefn::GetGeomFieldDefn(iGeomField)->GetSpatialRef()). In the future this may be generalized.
        /// Note that only the last spatial filter set is applied, even if several successive calls are done with different iGeomField values.
        /// Since GDAL 1.11
        /// </summary>
        /// <param name="iGeomField">index of the geometry field on which the spatial filter operates.</param>
        /// <param name="geometry">handle to the geometry to use as a filtering region. NULL may be passed indicating that the current spatial filter should be cleared, but no new one instituted.</param>
        public void SetSpatialFilterEx(int iGeomField, Geometry geometry)
        {
            PInvokeOgr.OGR_L_SetSpatialFilterEx(Handle, iGeomField, geometry.Handle);
        }

        /// <summary>
        /// Set a new rectangular spatial filter.
        /// This method set rectangle to be used as a spatial filter when fetching features via the OGR_L_GetNextFeature() method. Only features that geometrically intersect the given rectangle will be returned.
        /// The x/y values should be in the same coordinate system as the layer as a whole (as returned by OGRLayer::GetSpatialRef()). Internally this method is normally implemented as creating a 5 vertex closed rectangular polygon and passing it to OGRLayer::SetSpatialFilter(). It exists as a convenience.
        /// The only way to clear a spatial filter set with this method is to call OGRLayer::SetSpatialFilter(NULL).
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        public void SetSpatialFilterRect(double minX, double minY, double maxX, double maxY)
        {
            PInvokeOgr.OGR_L_SetSpatialFilterRect(Handle, minX, minY, maxX, maxY);
        }

        /// <summary>
        /// Set a new rectangular spatial filter.
        /// This method set rectangle to be used as a spatial filter when fetching features via the OGR_L_GetNextFeature() method. Only features that geometrically intersect the given rectangle will be returned.
        /// The x/y values should be in the same coordinate system as as the geometry field definition it corresponds to (as returned by GetLayerDefn()->OGRFeatureDefn::GetGeomFieldDefn(iGeomField)->GetSpatialRef()). Internally this method is normally implemented as creating a 5 vertex closed rectangular polygon and passing it to OGRLayer::SetSpatialFilter(). It exists as a convenience.
        /// The only way to clear a spatial filter set with this method is to call OGRLayer::SetSpatialFilter(NULL).
        /// Since GDAL 1.11
        /// </summary>
        /// <param name="iGeomField"></param>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        public void SetSpatialFilterRectEx(int iGeomField, double minX, double minY, double maxX, double maxY)
        {
            PInvokeOgr.OGR_L_SetSpatialFilterRectEx(Handle, iGeomField, minX, minY, maxX, maxY);
        }

        /// <summary>
        /// For datasources which support transactions, StartTransaction creates a transaction.
        /// If starting the transaction fails, will return OGRERR_FAILURE. Datasources which do not support transactions will always return OGRERR_NONE.
        /// Note: as of GDAL 2.0, use of this API is discouraged when the dataset offers dataset level transaction with GDALDataset::StartTransaction(). The reason is that most drivers can only offer transactions at dataset level, and not layer level. Very few drivers really support transactions at layer scope.
        /// </summary>
        /// <returns>True on success.</returns>
        public bool StartTransaction()
        {
            int errCode = PInvokeOgr.OGR_L_StartTransaction(Handle);
            return Errors.IsNoError(errCode);
        }

        /// <summary>
        /// Flush pending changes to disk.
        /// This call is intended to force the layer to flush any pending writes to disk, and leave the disk file in a consistent state. It would not normally have any effect on read-only datasources.
        /// Some layers do not implement this method, and will still return OGRERR_NONE. The default implementation just returns OGRERR_NONE. An error is only returned if an error occurs while attempting to flush to disk.
        /// In any event, you should always close any opened datasource with OGR_DS_Destroy() that will ensure all data is correctly flushed.
        /// </summary>
        /// <returns>True if no error occurs (even if nothing is done) or an error code.</returns>
        public bool SyncToDisk()
        {
            int errCode = PInvokeOgr.OGR_L_SyncToDisk(Handle);
            return Errors.IsNoError(errCode);
        }
        /// <summary>
        /// Create a new field on a layer.
        /// You must use this to create new fields on a real layer. Internally the OGRFeatureDefn for the layer will be updated to reflect the new field. Applications should never modify the OGRFeatureDefn used by a layer directly.
        /// This function should not be called while there are feature objects in existence that were obtained or created with the previous layer definition.
        /// Not all drivers support this function. You can query a layer to check if it supports it with the OLCCreateField capability. Some drivers may only support this method while there are still no features in the layer. When it is supported, the existing features of the backing file/database should be updated accordingly.
        /// Drivers may or may not support not-null constraints. If they support creating fields with not-null constraints, this is generally before creating any feature to the layer.
        /// </summary>
        /// <param name="field">handle of the field definition to write to disk.</param>
        /// <param name="approxOK">If TRUE, the field may be created in a slightly different form depending on the limitations of the format driver.</param>
        /// <returns>TRUE on success.</returns>
        public bool CreateField(FieldDefn field, bool approxOK = true)
        {
            var errCode =PInvokeOgr.OGR_L_CreateField(Handle, field.Handle, Convert.ToInt32(approxOK));
            return Errors.IsNoError(errCode);
        }

        /// <summary>
        /// Create and write a new feature within a layer.
        /// The passed feature is written to the layer as a new feature, rather than overwriting an existing one. If the feature has a feature id other than OGRNullFID, then the native implementation may use that as the feature id of the new feature, but not necessarily. Upon successful return the passed feature will have been updated with the new feature id.
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public bool CreateFeature(Feature feature)
        {
            var errCode = PInvokeOgr.OGR_L_CreateFeature(Handle, feature.Handle);
            return Errors.IsNoError(errCode);
        }

        /// <summary>
        /// Fetch the feature count in this layer.
        /// Returns the number of features in the layer. For dynamic databases the count may not be exact. If bForce is FALSE, and it would be expensive to establish the feature count a value of -1 may be returned indicating that the count isn't know. If bForce is TRUE some implementations will actually scan the entire layer once to count objects.
        /// The returned count takes the spatial filter into account.
        /// Note that some implementations of this method may alter the read cursor of the layer.
        /// Note: since GDAL 2.0, this method returns a GIntBig (previously a int)
        /// </summary>
        /// <param name="force">Flag indicating whether the count should be computed even if it is expensive.</param>
        /// <returns>feature count, -1 if count not known.</returns>
        public Int64 GetFeatureCount(bool force)
        {
            return PInvokeOgr.OGR_L_GetFeatureCount(Handle, Convert.ToInt32(force));
        }

        /// <summary>
        /// Fetch the extent of this layer.
        /// Returns the extent (MBR) of the data in the layer. If bForce is FALSE, and it would be expensive to establish the extent then OGRERR_FAILURE will be returned indicating that the extent isn't know. If bForce is TRUE then some implementations will actually scan the entire layer once to compute the MBR of all the features in the layer.
        /// Depending on the drivers, the returned extent may or may not take the spatial filter into account. So it is safer to call OGR_L_GetExtent() without setting a spatial filter.
        /// Layers without any geometry may return OGRERR_FAILURE just indicating that no meaningful extents could be collected.
        /// Note that some implementations of this method may alter the read cursor of the layer.
        /// </summary>
        /// <param name="force">Flag indicating whether the extent should be computed even if it is expensive.</param>
        /// <returns>TRUE on success, FALSE if extent not known.</returns>
        public bool GetExtent(out Envelope envelope, bool force)
        {
            envelope = new Envelope();
            int errCode = PInvokeOgr.OGR_L_GetExtent(Handle, out envelope, Convert.ToInt32(force));
            return Errors.IsNoError(errCode);
        }
        /// <summary>
        /// Fetch the extent of this layer, on the specified geometry field.
        /// Returns the extent (MBR) of the data in the layer. If bForce is FALSE, and it would be expensive to establish the extent then OGRERR_FAILURE will be returned indicating that the extent isn't know. If bForce is TRUE then some implementations will actually scan the entire layer once to compute the MBR of all the features in the layer.
        /// Depending on the drivers, the returned extent may or may not take the spatial filter into account. So it is safer to call OGR_L_GetExtent() without setting a spatial filter.
        /// Layers without any geometry may return OGRERR_FAILURE just indicating that no meaningful extents could be collected.
        /// Note that some implementations of this method may alter the read cursor of the layer.
        /// </summary>
        /// <param name="geomFieldIndex">the index of the geometry field on which to compute the extent.</param>
        /// <param name="envelope">	the structure in which the extent value will be returned.</param>
        /// <param name="force">	Flag indicating whether the extent should be computed even if it is expensive.</param>
        /// <returns>TRUE on success, FALSE if extent not known.</returns>
        public bool GetExtentEx(int geomFieldIndex, out Envelope envelope, bool force)
        {
            envelope = new Envelope();
            int errCode = PInvokeOgr.OGR_L_GetExtentEx(Handle, geomFieldIndex, out envelope, Convert.ToInt32(force));
            return Errors.IsNoError(errCode);
        }

        /// <summary>
        /// Return the layer geometry type.
        /// This returns the same result as OGR_FD_GetGeomType(OGR_L_GetLayerDefn(hLayer)), but for a few drivers, calling OGR_L_GetGeomType() directly can avoid lengthy layer definition initialization.For layers with multiple geometry fields, this method only returns the geometry type of the first geometry column. For other columns, use OGR_GFld_GetType(OGR_FD_GetGeomFieldDefn(OGR_L_GetLayerDefn(hLayer), i)). For layers without any geometry field, this method returns wkbNone.
        /// </summary>
        /// <returns></returns>
        public wkbGeometryType GetGeomType()
        {
            return PInvokeOgr.OGR_L_GetGeomType(Handle);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Scanex.Gdal
{
    public static class PInvokeGdal
    {
        #region **** Common

        //int GDALGetDataTypeSize	(	GDALDataType 	eDataType	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALGetDataTypeSize(DataType dataType);

        //int GDALGetDataTypeSizeBits	(	GDALDataType 	eDataType	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALGetDataTypeSizeBits(DataType dataType);

        //int GDALGetDataTypeSizeBytes	(	GDALDataType 	eDataType	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALGetDataTypeSizeBytes(DataType dataType);

        //int GDALDataTypeIsComplex	(	GDALDataType 	eDataType	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALDataTypeIsComplex(DataType dataType);

        //const char* GDALGetDataTypeName	(	GDALDataType 	eDataType	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string GDALGetDataTypeName(DataType dataType);

        //GDALDataType GDALGetDataTypeByName	(	const char * 	pszName	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern DataType GDALGetDataTypeByName(IntPtr name);

        //GDALDataType GDALDataTypeUnion	(	GDALDataType 	eType1,GDALDataType 	eType2 )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern DataType GDALDataTypeUnion(DataType type1, DataType type2);

        //double GDALAdjustValueToDataType	(	GDALDataType 	eDT,double 	dfValue,int * 	pbClamped,int * 	pbRounded )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern double GDALAdjustValueToDataType(DataType dataType, double value, out int clamped, out int rounded);

        //GDALDataType GDALGetNonComplexDataType	(	GDALDataType 	eDataType	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern DataType GDALGetNonComplexDataType(DataType dataType);
        
        //const char* GDALGetAsyncStatusTypeName	(	GDALAsyncStatusType 	eAsyncStatusType	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string GDALGetAsyncStatusTypeName(AsyncStatusType eAsyncStatusType);

        //GDALAsyncStatusType GDALGetAsyncStatusTypeByName	(	const char * 	pszName	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern AsyncStatusType GDALGetAsyncStatusTypeByName(IntPtr name);

        //const char* GDALGetColorInterpretationName	(	GDALColorInterp 	eInterp	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string GDALGetColorInterpretationName(ColorInterp interp);

        //GDALColorInterp GDALGetColorInterpretationByName	(	const char * 	pszName	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern ColorInterp GDALGetColorInterpretationByName(IntPtr name);

        //GDALDriverH GDALIdentifyDriver	(	const char * 	pszFilename,char ** 	papszFileList )		
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GDALIdentifyDriver(IntPtr filename, [In, Out]IntPtr[] fileList);

        //int GDALInvGeoTransform	(	double * 	gt_in,double * 	gt_out )
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALInvGeoTransform(double[] gt_in, [In, Out]double[] gt_out);

        //void GDALApplyGeoTransform	(	double * 	padfGeoTransform,double 	dfPixel,double 	dfLine,double * 	pdfGeoX,double * 	pdfGeoY )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void GDALApplyGeoTransform(double[] geoTransform, double pixel, double line, out double geoX, out double geoY);

        //void GDALComposeGeoTransforms	(	const double * 	padfGT1,const double * 	padfGT2,double * 	padfGTOut )		
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GDALComposeGeoTransforms([In, Out]double[] GT1, [In, Out]double[] GT2, [In, Out]double[] GTOut);











        //void GDALAllRegister	(	void 		)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void GDALAllRegister();

        //int 	GDALRegisterDriver (GDALDriverH)
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void GDALRegisterDriver(IntPtr hGDALDriver);

        //void 	GDALDeregisterDriver (GDALDriverH)
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void GDALDeregisterDriver(IntPtr hGDALDriver);

        //void GDALSetCacheMax	(	int 	nNewSizeInBytes	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void GDALSetCacheMax(int nNewSizeInBytes);

        //void GDALSetCacheMax64	(	GIntBig 	nNewSizeInBytes	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void GDALSetCacheMax64(Int64 nNewSizeInBytes);


        #endregion

        #region VSI
        //VSIUnlink
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VSIUnlink(IntPtr name);
        #endregion


        #region  Driver
        //GDALDriverH GDALGetDriverByName	(	const char * 	pszName	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GDALGetDriverByName(IntPtr name);

        //int GDALGetDriverCount(void)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALGetDriverCount();

        //void GDALDestroyDriver	(	GDALDriverH 	hDriver	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void GDALDestroyDriver(IntPtr hGDALDriver);

        //GDALDriverH GDALGetDriver	(	int 	iDriver	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr GDALGetDriver(int iDriver);

        //const char* GDALGetDriverShortName	(	GDALDriverH 	hDriver	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string GDALGetDriverShortName(IntPtr hDriver);

        //const char* GDALGetDriverLongName	(	GDALDriverH 	hDriver	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string GDALGetDriverLongName(IntPtr hDriver);

        //GDALDatasetH GDALCreate	(	GDALDriverH 	hDriver,const char * 	pszFilename,int 	nXSize,int 	nYSize,int 	nBands,GDALDataType 	eBandType,char ** 	papszOptions )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr GDALCreate(IntPtr hDriver, IntPtr filename, int xSize, int ySize, int bands, Scanex.Gdal.DataType bandType, [In, Out]IntPtr[] options);

        //GDALDatasetH GDALCreateCopy	(	GDALDriverH 	hDriver,const char * 	pszFilename,GDALDatasetH 	hSrcDS,int 	bStrict,char ** 	papszOptions,GDALProgressFunc 	pfnProgress,void * 	pProgressData )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GDALCreateCopy(IntPtr hDriver, IntPtr filename, IntPtr dataset, int strict, [In, Out]IntPtr[] options, Gdal.GDALProgressFuncDelegate progress, IntPtr progressData);

        //CPLErr GDALDeleteDataset	(	GDALDriverH 	hDriver,const char * 	pszFilename )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALDeleteDataset(IntPtr hDriver, IntPtr filename);

        //CPLErr GDALRenameDataset	(	GDALDriverH 	hDriver,const char * 	pszNewName,const char * 	pszOldName )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALRenameDataset(IntPtr hDriver, IntPtr newName, IntPtr oldName);


        
        //CPLErr GDALCopyDatasetFiles	(	GDALDriverH 	hDriver,const char * 	pszNewName,const char * 	pszOldName )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALCopyDatasetFiles(IntPtr hDriver, IntPtr newName, IntPtr oldName);

        //int GDALValidateCreationOptions	(	GDALDriverH 	hDriver,char ** 	papszCreationOptions )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALValidateCreationOptions(IntPtr hDriver, [In, Out]IntPtr[] papszCreationOptions);

        //const char* GDALGetDriverHelpTopic	(	GDALDriverH 	hDriver	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string GDALGetDriverHelpTopic(IntPtr hDriver);

        //const char* GDALGetDriverCreationOptionList	(	GDALDriverH 	hDriver	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string GDALGetDriverCreationOptionList(IntPtr hDriver);

        #endregion


        #region   Dataset
        //GDALDatasetH GDALOpen	(	const char * 	pszFilename,GDALAccess 	eAccess )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GDALOpen(IntPtr filename, Access access);

        //GDALDatasetH GDALOpenShared	(	const char * 	pszFilename,GDALAccess 	eAccess )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GDALOpenShared(IntPtr filename, Access access);

        //GDALDatasetH GDALOpenEx	(	const char * 	pszFilename,unsigned int 	nOpenFlags,const char *const * 	papszAllowedDrivers,const char *const * 	papszOpenOptions,const char *const * 	papszSiblingFiles )
        //pszFilename - должно быть в UTF-8
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GDALOpenEx(IntPtr filename, int OpenFlags, [In, Out]IntPtr[] papszAllowedDrivers, [In, Out]IntPtr[] papszOpenOptions, [In, Out]IntPtr[] papszSiblingFiles);

        //void GDALClose	(	GDALDatasetH 	hDS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void GDALClose(IntPtr hDS);

        //int GDALDatasetGetLayerCount	(	GDALDatasetH 	hDS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GDALDatasetGetLayerCount(IntPtr hDS);

        //OGRLayerH 	GDALDatasetGetLayer (GDALDatasetH, int)
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr GDALDatasetGetLayer(IntPtr hGDALDataset, int iLayer);

        //OGRLayerH GDALDatasetGetLayerByName	(	GDALDatasetH 	hDS,const char * 	pszName )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr GDALDatasetGetLayerByName(IntPtr hGDALDataset, string name);

        //GDALDriverH 	GDALGetDatasetDriver (GDALDatasetH)
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GDALGetDatasetDriver(IntPtr hGDALDataset);

        //OGRErr GDALDatasetDeleteLayer	(	GDALDatasetH 	hDS,int 	iLayer )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GDALDatasetDeleteLayer(IntPtr hGDALDataset, int iLayer);

        //OGRLayerH GDALDatasetCreateLayer	(	GDALDatasetH 	hDS,const char * 	pszName,OGRSpatialReferenceH 	hSpatialRef,OGRwkbGeometryType 	eGType,char ** 	papszOptions )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GDALDatasetCreateLayer(IntPtr hGDALDataset, IntPtr name, IntPtr spatialRef, Scanex.Gdal.wkbGeometryType gType, [In, Out]IntPtr[] options);

        //char** GDALGetFileList	(	GDALDatasetH 	hDS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr[] GDALGetFileList(IntPtr hDS);

        //int GDALGetRasterXSize	(	GDALDatasetH 	hDataset	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALGetRasterXSize(IntPtr hDS);

        //int GDALGetRasterYSize	(	GDALDatasetH 	hDataset	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALGetRasterYSize(IntPtr hDS);

        //int GDALGetRasterCount	(	GDALDatasetH 	hDS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALGetRasterCount(IntPtr hDS);

        //GDALRasterBandH GDALGetRasterBand	(	GDALDatasetH 	hDS,int 	nBandId )
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GDALGetRasterBand(IntPtr hDS, int bandId);

        //CPLErr GDALAddBand	(	GDALDatasetH 	hDataset,GDALDataType 	eType,char ** 	papszOptions )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALAddBand(IntPtr hDS, DataType type, IntPtr[] options);

        //CPLErr GDALDatasetRasterIO	(	GDALDatasetH 	hDS,GDALRWFlag 	eRWFlag,int 	nXOff,int 	nYOff,int 	nXSize,int 	nYSize,void * 	pData,int 	nBufXSize,int 	nBufYSize,GDALDataType 	eBufType,int 	nBandCount,int * 	panBandMap,int 	nPixelSpace,int 	nLineSpace,int 	nBandSpace )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALDatasetRasterIO(IntPtr hDS, RWFlag RWFlag, int nXOff, int nYOff, int nXSize, int nYSize, IntPtr pData, int nBufXSize, int nBufYSize, DataType eBufType,
                                                     int nBandCount, int[] panBandMap, int nPixelSpace, int nLineSpace, int nBandSpace);

        //const char* GDALGetProjectionRef	(	GDALDatasetH 	hDS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern string GDALGetProjectionRef(IntPtr hDS);

        //CPLErr GDALSetProjection	(	GDALDatasetH 	hDS,const char * 	pszProjection )		
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALSetProjection(IntPtr hDS, IntPtr projection);

        //CPLErr GDALGetGeoTransform	(	GDALDatasetH 	hDS,double * 	padfTransform )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALGetGeoTransform(IntPtr hDS, [In, Out]double[] padfTransform);

        //CPLErr GDALSetGeoTransform	(	GDALDatasetH 	hDS,double * 	padfTransform )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALSetGeoTransform(IntPtr hDS, [In, Out]double[] padfTransform);

        #endregion

        #region Band
        
        //int GDALGetOverviewCount	(	GDALRasterBandH 	hBand	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALGetOverviewCount(IntPtr hBand);

        //GDALRasterBandH GDALGetOverview	(	GDALRasterBandH 	hBand,int 	i )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GDALGetOverview(IntPtr hBand, int i);

        //int GDALGetRasterBandXSize	(	GDALRasterBandH 	hBand	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALGetRasterBandXSize(IntPtr hBand);

        //int GDALGetRasterBandYSize	(	GDALRasterBandH 	hBand	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALGetRasterBandYSize(IntPtr hBand);

        //GDALDataType GDALGetRasterDataType	(	GDALRasterBandH 	hBand	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern DataType GDALGetRasterDataType(IntPtr hBand);

        //CPLErr GDALRasterIO	(	GDALRasterBandH 	hBand,GDALRWFlag 	eRWFlag,int 	nXOff,int 	nYOff,int 	nXSize,int 	nYSize,void * 	pData,int 	nBufXSize,int 	nBufYSize,GDALDataType 	eBufType,int 	nPixelSpace,int 	nLineSpace )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALRasterIO(IntPtr hDS, RWFlag RWFlag, int nXOff, int nYOff, int nXSize, int nYSize, IntPtr pData, int nBufXSize, int nBufYSize, DataType eBufType,
                                                     int nPixelSpace, int nLineSpace);

        #endregion
    }
}

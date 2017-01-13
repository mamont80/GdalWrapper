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
        //int CPL_DLL VSIUnlink( const char * pathname );
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VSIUnlink(IntPtr name);

        //VSILFILE CPL_DLL *VSIFileFromMemBuffer( const char *pszFilename,GByte *pabyData,vsi_l_offset nDataLength,int bTakeOwnership ) CPL_WARN_UNUSED_RESULT;
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr VSIFileFromMemBuffer(IntPtr filenameUtf8, IntPtr data, int dataLength, int takeOwnership);

        //int CPL_DLL     VSIFCloseL( VSILFILE * ) EXPERIMENTAL_CPL_WARN_UNUSED_RESULT;
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VSIFCloseL(IntPtr fileHandle);
        
        //void CPL_DLL   *VSIMalloc( size_t ) CPL_WARN_UNUSED_RESULT;
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr VSIMalloc(int size);

        //void CPL_DLL    VSIFree( void * );
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void VSIFree(IntPtr mem);

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
        public static extern IntPtr GDALGetProjectionRef(IntPtr hDS);

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

        //CPLErr GDALGetRasterStatistics	(	GDALRasterBandH 	hBand,int 	bApproxOK,int 	bForce,double * 	pdfMin,double * 	pdfMax,double * 	pdfMean,double * 	pdfStdDev )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GDALGetRasterStatistics(IntPtr hBand, int approxOK, int force, out double min, out double max, out double mean, out double stdDev);
        #endregion

        #region Utils
        //GDALWarpAppOptions* GDALWarpAppOptionsNew	(	char ** 	papszArgv,GDALWarpAppOptionsForBinary * 	psOptionsForBinary )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GDALWarpAppOptionsNew([In, Out] IntPtr[] args, IntPtr optionsForBinary);

        //GDALDatasetH GDALWarp	(	const char * 	pszDest,GDALDatasetH 	hDstDS,int 	nSrcCount,GDALDatasetH * 	pahSrcDS,const GDALWarpAppOptions * 	psOptionsIn,int * 	pbUsageError )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GDALWarp(IntPtr dest, IntPtr dstDS, int srcCount, [In, Out] IntPtr[] srcDS, IntPtr options, out int usageError);

        //void GDALWarpAppOptionsFree	(	GDALWarpAppOptions * 	psOptions	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GDALWarpAppOptionsFree(IntPtr options);

        //void GDALWarpAppOptionsSetProgress	(	GDALWarpAppOptions * 	psOptions,GDALProgressFunc 	pfnProgress,void * 	pProgressData )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GDALWarpAppOptionsSetProgress(IntPtr options, Gdal.GDALProgressFuncDelegate progress, IntPtr progressData);

        //void GDALWarpAppOptionsSetWarpOption	(	GDALWarpAppOptions * 	psOptions,const char * 	pszKey,const char * 	pszValue )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GDALWarpAppOptionsSetWarpOption(IntPtr options, IntPtr key, IntPtr value);
        #endregion
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GDALWarpAppOptions
    {
        /*! set georeferenced extents of output file to be created (in target SRS by default,
            or in the SRS specified with pszTE_SRS) */
        double dfMinX;
        double dfMinY;
        double dfMaxX;
        double dfMaxY;

        /*! the SRS in which to interpret the coordinates given in GDALWarpAppOptions::dfMinX,
            GDALWarpAppOptions::dfMinY, GDALWarpAppOptions::dfMaxX and GDALWarpAppOptions::dfMaxY.
            The SRS may be any of the usual GDAL/OGR forms,
            complete WKT, PROJ.4, EPSG:n or a file containing the WKT. It is a
            convenience e.g. when knowing the output coordinates in a
            geodetic long/lat SRS, but still wanting a result in a projected
            coordinate system. */
        IntPtr pszTE_SRS;

        /*! set output file resolution (in target georeferenced units) */
        double dfXRes;
        double dfYRes;

        /*! align the coordinates of the extent of the output file to the values of the
            GDALWarpAppOptions::dfXRes and GDALWarpAppOptions::dfYRes, such that the
            aligned extent includes the minimum extent. */
        int bTargetAlignedPixels;

        /*! set output file size in pixels and lines. If GDALWarpAppOptions::nForcePixels
            or GDALWarpAppOptions::nForceLines is set to 0, the other dimension will be
            guessed from the computed resolution. Note that GDALWarpAppOptions::nForcePixels and
            GDALWarpAppOptions::nForceLines cannot be used with GDALWarpAppOptions::dfXRes and
            GDALWarpAppOptions::dfYRes. */
        int nForcePixels;
        int nForceLines;

        /*! allow or suppress progress monitor and other non-error output */
        int bQuiet;

        /*! the progress function to use */
        IntPtr pfnProgress;

        /*! pointer to the progress data variable */
        IntPtr pProgressData;

        /*! creates an output alpha band to identify nodata (unset/transparent) pixels
            when set to TRUE */
        int bEnableDstAlpha;

        int bEnableSrcAlpha;

        /*! output format. The default is GeoTIFF (GTiff). Use the short format name. */
        IntPtr pszFormat;

        int bCreateOutput;

        /*! list of warp options. ("NAME1=VALUE1","NAME2=VALUE2",...). The
            GDALWarpOptions::papszWarpOptions docs show all options. */
        IntPtr papszWarpOptions;

        double dfErrorThreshold;

        /*! the amount of memory (in megabytes) that the warp API is allowed
            to use for caching. */
        double dfWarpMemoryLimit;

        /*! list of create options for the output format driver. See format
            specific documentation for legal creation options for each format. */
        IntPtr papszCreateOptions;

        /*! the data type of the output bands */
        int eOutputType;

        /*! working pixel data type. The data type of pixels in the source
            image and destination image buffers. */
        int eWorkingType;

        /*! the resampling method. Available methods are: near, bilinear,
            cubic, cubicspline, lanczos, average, mode, max, min, med,
            q1, q3 */
        int eResampleAlg;

        /*! nodata masking values for input bands (different values can be supplied
            for each band). ("value1 value2 ..."). Masked values will not be used
            in interpolation. Use a value of "None" to ignore intrinsic nodata
            settings on the source dataset. */
        IntPtr pszSrcNodata;

        /*! nodata values for output bands (different values can be supplied for
            each band). ("value1 value2 ..."). New files will be initialized to
            this value and if possible the nodata value will be recorded in the
            output file. Use a value of "None" to ensure that nodata is not defined.
            If this argument is not used then nodata values will be copied from
            the source dataset. */
        IntPtr pszDstNodata;

        /*! use multithreaded warping implementation. Multiple threads will be used
            to process chunks of image and perform input/output operation simultaneously. */
        int bMulti;

        /*! list of transformer options suitable to pass to GDALCreateGenImgProjTransformer2().
            ("NAME1=VALUE1","NAME2=VALUE2",...) */
        IntPtr papszTO;

        /*! enable use of a blend cutline from the name OGR support pszCutlineDSName */
        IntPtr pszCutlineDSName;

        /*! the named layer to be selected from the cutline datasource */
        IntPtr pszCLayer;

        /*! restrict desired cutline features based on attribute query */
        IntPtr pszCWHERE;

        /*! SQL query to select the cutline features instead of from a layer
            with pszCLayer */
        IntPtr pszCSQL;

        /*! crop the extent of the target dataset to the extent of the cutline */
        int bCropToCutline;

        /*! copy dataset and band metadata will be copied from the first source dataset. Items that differ between
            source datasets will be set "*" (see GDALWarpAppOptions::pszMDConflictValue) */
        int bCopyMetadata;

        /*! copy band information from the first source dataset */
        int bCopyBandInfo;

        /*! value to set metadata items that conflict between source datasets (default is "*").
            Use "" to remove conflicting items. */
        IntPtr pszMDConflictValue;

        /*! set the color interpretation of the bands of the target dataset from the source dataset */
        int bSetColorInterpretation;

        /*! overview level of source files to be used */
        int nOvLevel;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Scanex.Gdal
{
    public static class PInvokeOsr
    {
        //OGRSpatialReferenceH OSRNewSpatialReference	(	const char * 	pszWKT	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr OSRNewSpatialReference(string pszWKT);

        //void OSRDestroySpatialReference	(	OGRSpatialReferenceH 	hSRS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void OSRDestroySpatialReference(IntPtr hSRS);

        //int OSRIsCompound	(	OGRSpatialReferenceH 	hSRS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OSRIsCompound(IntPtr hSRS);

        //int OSRIsGeocentric	(	OGRSpatialReferenceH 	hSRS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OSRIsGeocentric(IntPtr hSRS);

        //int OSRIsGeographic	(	OGRSpatialReferenceH 	hSRS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]//
        public static extern int OSRIsGeographic(IntPtr hSRS);

        //int OSRIsLocal	(	OGRSpatialReferenceH 	hSRS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OSRIsLocal(IntPtr hSRS);

        //int OSRIsProjected	(	OGRSpatialReferenceH 	hSRS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OSRIsProjected(IntPtr hSRS);

        //int 	OSRIsSame (OGRSpatialReferenceH, OGRSpatialReferenceH)
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OSRIsSame(IntPtr hSRS, IntPtr hSRS2);

        //int OSRIsSameGeogCS	(	OGRSpatialReferenceH 	hSRS1,OGRSpatialReferenceH 	hSRS2 )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OSRIsSameGeogCS(IntPtr hSRS, IntPtr hSRS2);

        //int OSRIsSameVertCS	(	OGRSpatialReferenceH 	hSRS1,OGRSpatialReferenceH 	hSRS2 )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRIsSameVertCS(IntPtr hSRS, IntPtr hSRS2);

        //int OSRIsVertical	(	OGRSpatialReferenceH 	hSRS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OSRIsVertical(IntPtr hSRS);

        //OGRErr OSRMorphFromESRI	(	OGRSpatialReferenceH 	hSRS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OSRMorphFromESRI(IntPtr hSRS);

        //OGRErr OSRMorphToESRI	(	OGRSpatialReferenceH 	hSRS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OSRMorphToESRI(IntPtr hSRS);

        //OGRErr OSRSetAngularUnits	(	OGRSpatialReferenceH 	hSRS,const char * 	pszUnits,double 	dfInRadians )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetAngularUnits(IntPtr hSRS, string units, double radians);

        //OGRErr OSRSetAttrValue	(	OGRSpatialReferenceH 	hSRS,const char * 	pszPath,const char * 	pszValue )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int OSRSetAttrValue(IntPtr hSRS, string path, string value);

        //OGRErr OSRSetAuthority	(	OGRSpatialReferenceH 	hSRS,const char * 	pszTargetKey,const char * 	pszAuthority,int 	nCode )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int OSRSetAttrValue(IntPtr hSRS, string targetKey, string authority, int code);

        //OGRErr OSRSetAxes	(	OGRSpatialReferenceH 	hSRS,const char * 	pszTargetKey,const char * 	pszXAxisName,OGRAxisOrientation 	eXAxisOrientation,const char * 	pszYAxisName,OGRAxisOrientation 	eYAxisOrientation )	
        //[DllImport(OgrPINVOKE.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        //public static extern int OSRSetAxes(IntPtr hSRS, string targetKey, string xAxisName, int code);

        //OGRErr OSRSetCompoundCS	(	OGRSpatialReferenceH 	hSRS,const char * 	pszName,OGRSpatialReferenceH 	hHorizSRS,OGRSpatialReferenceH 	hVertSRS )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetCompoundCS(IntPtr hSRS, string name, IntPtr horizSRS, IntPtr vertSRS);

        //OGRErr OSRSetFromUserInput	(	OGRSpatialReferenceH 	hSRS,const char * 	pszDef )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int OSRSetFromUserInput(IntPtr hSRS, string def);

        //OGRErr OSRSetGeocCS	(	OGRSpatialReferenceH 	hSRS,const char * 	pszName )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetGeocCS(IntPtr hSRS, string name);

        //OGRErr OSRSetGeogCS	(	OGRSpatialReferenceH 	hSRS,const char * 	pszGeogName,const char * 	pszDatumName,const char * 	pszSpheroidName,double 	dfSemiMajor,double 	dfInvFlattening,const char * 	pszPMName,double 	dfPMOffset,const char * 	pszAngularUnits,double 	dfConvertToRadians )
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetGeogCS(IntPtr hSRS, string pszGeogName, string pszDatumName, string pszSpheroidName, double dfSemiMajor, double dfInvFlattening, string pszPMName, double dfPMOffset, string pszAngularUnits, double dfConvertToRadians);

        //OGRErr OSRSetHOM	(	OGRSpatialReferenceH 	hSRS,double 	dfCenterLat,double 	dfCenterLong,double 	dfAzimuth,double 	dfRectToSkew,double 	dfScale,double 	dfFalseEasting,double 	dfFalseNorthing )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetHOM(IntPtr hSRS, double dfCenterLat, double dfCenterLong, double dfAzimuth, double dfRectToSkew, double dfScale, double dfFalseEasting, double dfFalseNorthing);

        //OGRErr OSRSetHOM2PNO	(	OGRSpatialReferenceH 	hSRS,double 	dfCenterLat,double 	dfLat1,double 	dfLong1,double 	dfLat2,double 	dfLong2,double 	dfScale,double 	dfFalseEasting,double 	dfFalseNorthing )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetHOM2PNO(IntPtr hSRS, double dfCenterLat, double dfLat1, double dfLong1, double dfLat2, double dfLong2, double dfScale, double dfFalseEasting,
                                               double dfFalseNorthing);

        //OGRErr OSRSetHOMAC	(	OGRSpatialReferenceH 	hSRS,double 	dfCenterLat,double 	dfCenterLong,double 	dfAzimuth,double 	dfRectToSkew,double 	dfScale,double 	dfFalseEasting,double 	dfFalseNorthing )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetHOMAC(IntPtr hSRS, double dfCenterLat, double dfCenterLong, double dfAzimuth, double dfRectToSkew, double dfScale, double dfFalseEasting, double dfFalseNorthing);

        //OGRErr OSRSetLinearUnits	(	OGRSpatialReferenceH 	hSRS,const char * 	pszUnits,double 	dfInMeters )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetLinearUnits(IntPtr hSRS, string pszUnits, double dfInMeters);

        //OGRErr OSRSetLinearUnitsAndUpdateParameters	(	OGRSpatialReferenceH 	hSRS,const char * 	pszUnits,double 	dfInMeters )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetLinearUnitsAndUpdateParameters(IntPtr hSRS, string pszUnits, double dfInMeters);

        //OGRErr OSRSetLocalCS	(	OGRSpatialReferenceH 	hSRS,const char * 	pszName )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetLocalCS(IntPtr hSRS, string name);

        //OGRErr OSRSetNormProjParm	(	OGRSpatialReferenceH 	hSRS,const char * 	pszParmName,double 	dfValue )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetNormProjParm(IntPtr hSRS, string parmName, double value);

        //OGRErr OSRSetProjCS	(	OGRSpatialReferenceH 	hSRS,const char * 	pszName )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetProjCS(IntPtr hSRS, string name);

        //OGRErr OSRSetProjection	(	OGRSpatialReferenceH 	hSRS,const char * 	pszProjection )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetProjection(IntPtr hSRS, string pszProjection);

        //OGRErr OSRSetProjParm	(	OGRSpatialReferenceH 	hSRS,const char * 	pszParmName,double 	dfValue )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetProjParm(IntPtr hSRS, string pszParmName, double dfValue);

        //OGRErr OSRSetStatePlane	(	OGRSpatialReferenceH 	hSRS,int 	nZone,int 	bNAD83 )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetStatePlane(IntPtr hSRS, int nZone, int bNAD83);

        //OGRErr OSRSetStatePlaneWithUnits	(	OGRSpatialReferenceH 	hSRS,int 	nZone,int 	bNAD83,const char * 	pszOverrideUnitName,double 	dfOverrideUnit )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetStatePlaneWithUnits(IntPtr hSRS, int nZone, int bNAD83, string pszOverrideUnitName, double dfOverrideUnit);

        //OGRErr OSRSetTargetLinearUnits	(	OGRSpatialReferenceH 	hSRS,const char * 	pszTargetKey,const char * 	pszUnits,double 	dfInMeters )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetTargetLinearUnits(IntPtr hSRS, string pszTargetKey, string pszUnits, double dfInMeters);

        //OGRErr OSRSetTM	(	OGRSpatialReferenceH 	hSRS,double 	dfCenterLat,double 	dfCenterLong,double 	dfScale,double 	dfFalseEasting,double 	dfFalseNorthing )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetTM(IntPtr hSRS, double dfCenterLat, double dfCenterLong, double dfScale, double dfFalseEasting, double dfFalseNorthing);

        //OGRErr OSRSetTOWGS84	(	OGRSpatialReferenceH 	hSRS,double 	dfDX,double 	dfDY,double 	dfDZ,double 	dfEX,double 	dfEY,double 	dfEZ,double 	dfPPM )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetTOWGS84(IntPtr hSRS, double dfDX, double dfDY, double dfDZ, double dfEX, double dfEY, double dfEZ, double dfPPM);

        //OGRErr OSRSetUTM	(	OGRSpatialReferenceH 	hSRS,int 	nZone,int 	bNorth )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetUTM(IntPtr hSRS, int nZone, int bNorth);

        //OGRErr OSRSetVertCS	(	OGRSpatialReferenceH 	hSRS,const char * 	pszVertCSName,const char * 	pszVertDatumName,int 	nVertDatumType )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetVertCS(IntPtr hSRS, string pszVertCSName, string pszVertDatumName, int nVertDatumType);

        //OGRErr OSRSetWellKnownGeogCS	(	OGRSpatialReferenceH 	hSRS,const char * 	pszName )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetWellKnownGeogCS(IntPtr hSRS, string pszName);

        //OGRErr OSRStripCTParms	(	OGRSpatialReferenceH 	hSRS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRStripCTParms(IntPtr hSRS);

        //OGRErr OSRValidate	(	OGRSpatialReferenceH 	hSRS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRValidate(IntPtr hSRS);

        //OGRErr OSRImportFromXML	(	OGRSpatialReferenceH 	hSRS,const char * 	pszXML )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRImportFromXML(IntPtr hSRS, string pszXML);

        //OGRErr OSRImportFromWkt	(	OGRSpatialReferenceH 	hSRS,char ** 	ppszInput )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRImportFromWkt(IntPtr hSRS, string ppszInput);

        //OGRErr OSRImportFromUSGS	(	OGRSpatialReferenceH 	hSRS,long 	iProjsys,long 	iZone,double * 	padfPrjParams,long 	iDatum )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRImportFromUSGS(IntPtr hSRS, long iProjsys, long iZone, [In, Out]double[] padfPrjParams, long iDatum);

        //OGRErr OSRImportFromUrl	(	OGRSpatialReferenceH 	hSRS,const char * 	pszUrl )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRImportFromUrl(IntPtr hSRS, string pszUrl);

        //OGRErr OSRImportFromProj4	(	OGRSpatialReferenceH 	hSRS,const char * 	pszProj4 )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRImportFromProj4(IntPtr hSRS, string pszProj4);

        //OGRErr OSRImportFromPCI	(	OGRSpatialReferenceH 	hSRS,const char * 	pszProj,const char * 	pszUnits,double * 	padfPrjParams )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRImportFromPCI(IntPtr hSRS, string pszProj, string pszUnits, [In, Out]double[] padfPrjParams);

        //OGRErr OSRImportFromPanorama	(	OGRSpatialReferenceH 	hSRS,long 	iProjSys,long 	iDatum,long 	iEllips,double * 	padfPrjParams )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRImportFromPanorama(IntPtr hSRS, long iProjSys, long iDatum, long iEllips, [In, Out] double[] padfPrjParams);

        //OGRErr OSRImportFromOzi	(	OGRSpatialReferenceH 	hSRS,const char * papszLines )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRImportFromOzi(IntPtr hSRS, string[] papszLines);

        //OGRErr OSRImportFromMICoordSys	(	OGRSpatialReferenceH 	hSRS,const char * 	pszCoordSys )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRImportFromMICoordSys(IntPtr hSRS, string pszCoordSys);

        //OGRErr OSRImportFromESRI	(	OGRSpatialReferenceH 	hSRS, char ** 	papszPrj )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRImportFromESRI(IntPtr hSRS, string[] papszPrj);

        //OGRErr OSRImportFromERM	(	OGRSpatialReferenceH 	hSRS,const char * 	pszProj,const char * 	pszDatum,const char * 	pszUnits )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRImportFromERM(IntPtr hSRS, string pszProj, string pszDatum, string pszUnits);

        //OGRErr OSRImportFromEPSGA	(	OGRSpatialReferenceH 	hSRS,int 	nCode )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int OSRImportFromEPSGA(IntPtr hSRS, int nCode);

        //OGRErr OSRImportFromEPSG	(	OGRSpatialReferenceH 	hSRS,int 	nCode )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int OSRImportFromEPSG(IntPtr hSRS, int nCode);

        //OGRErr OSRImportFromDict	(	OGRSpatialReferenceH 	hSRS,const char * 	pszDictFile,const char * 	pszCode )		
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRImportFromDict(IntPtr hSRS, string pszDictFile, string pszCode);

        //int OSRGetUTMZone	(	OGRSpatialReferenceH 	hSRS,int * 	pbNorth )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRGetUTMZone(IntPtr hSRS, out int north);

        //int OSREPSGTreatsAsLatLong	(	OGRSpatialReferenceH 	hSRS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSREPSGTreatsAsLatLong(IntPtr hSRS);

        //int OSREPSGTreatsAsNorthingEasting	(	OGRSpatialReferenceH 	hSRS	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSREPSGTreatsAsNorthingEasting(IntPtr hSRS);

        //OGRErr OSRSetAuthority	(	OGRSpatialReferenceH 	hSRS,const char * 	pszTargetKey,const char * 	pszAuthority,int 	nCode )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OSRSetAuthority(IntPtr hSRS, string targetKey, string authority, int nCode);

        //const char* OSRGetAttrValue	(	OGRSpatialReferenceH 	hSRS,const char * 	pszKey,int 	iChild )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string OSRGetAttrValue(IntPtr hSRS, string key, int iChild);

        //OGRErr OSRExportToPrettyWkt	(	OGRSpatialReferenceH 	hSRS,char ** 	ppszReturn,int 	bSimplify )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int OSRExportToPrettyWkt(IntPtr hSRS, ref IntPtr ppszReturn, int simplify);


        //******* Coordinate transformation class
        
        //OGRCoordinateTransformationH OCTNewCoordinateTransformation	(	OGRSpatialReferenceH 	hSourceSRS,OGRSpatialReferenceH 	hTargetSRS )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr OCTNewCoordinateTransformation(IntPtr hSourceSRS, IntPtr hTargetSRS);

        //void OCTDestroyCoordinateTransformation	(	OGRCoordinateTransformationH 	hCT	)	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void OCTDestroyCoordinateTransformation(IntPtr hCT);

        //int OCTTransform	(	OGRCoordinateTransformationH 	hTransform, int 	nCount,double * 	x,double * 	y,double * 	z )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int OCTTransform(IntPtr hTransform, int nCount, double[] x, double[] y, double[] z);

        //int OCTTransformEx	(	OGRCoordinateTransformationH 	hTransform,int 	nCount,double * 	x,double * 	y,double * 	z,int * 	pabSuccess )	
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int OCTTransformEx(IntPtr hTransform, int nCount, double[] x, double[] y, double[] z, int[] pabSuccess);
    }
}

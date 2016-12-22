using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Scanex.Gdal
{
    public static class PInvokeOgr
    {
        //public const string GdalDllName = "gdal";
        public const string GdalDllName = "gdal201.dll";

        #region Geometry
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_G_DestroyGeometry(IntPtr hGeom);

        //OGRErr OGR_G_CreateFromWkt(char ** ppszData, OGRSpatialReferenceH hSRS, OGRGeometryH * phGeometry)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OGR_G_CreateFromWkt(ref IntPtr s, IntPtr hSRS, out IntPtr hGeom);

        //OGRGeometryH OGR_G_CreateGeometry(OGRwkbGeometryType eGeometryType)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_G_CreateGeometry(Scanex.Gdal.wkbGeometryType geomType);

        //OGRErr OGR_G_CreateFromWkb(unsigned char * pabyData, OGRSpatialReferenceH hSRS, OGRGeometryH * phGeometry, int nBytes)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_CreateFromWkb(IntPtr buffer, IntPtr hSRS, out IntPtr hGeom, int nBytes);

        //OGRGeometryH OGR_G_CreateFromGML(const char * pszGML)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr OGR_G_CreateFromGML(string gml);

        //OGRErr OGR_G_ImportFromWkt(OGRGeometryH hGeom, char ** ppszSrcText)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_ImportFromWkt(IntPtr hGeom, ref IntPtr text);

        //OGRErr OGR_G_ExportToWkt	(	OGRGeometryH 	hGeom,char ** 	ppszSrcText )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OGR_G_ExportToWkt(IntPtr hGeom, ref IntPtr text);
        
        //int OGR_G_WkbSize	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_WkbSize(IntPtr hGeom);

        //OGRErr OGR_G_ExportToWkb(OGRGeometryH hGeom, OGRwkbByteOrder 	eOrder, unsigned char * pabyDstBuffer)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_ExportToWkb(IntPtr hGeom, Scanex.Gdal.wkbByteOrder eOrder, IntPtr buffer);

        //char* OGR_G_ExportToGML(OGRGeometryH hGeometry)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern string OGR_G_ExportToGML(IntPtr hGeom);

        //char* OGR_G_ExportToGMLEx	(OGRGeometryH hGeometry, char ** papszOptions )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern string OGR_G_ExportToGMLEx(IntPtr hGeom, [In, Out]IntPtr[] options);
        
        //char* OGR_G_ExportToKML(OGRGeometryH 	hGeometry, const char * pszAltitudeMode )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern string OGR_G_ExportToKML(IntPtr hGeom, string pszAltitudeMode);
        
        //char* OGR_G_ExportToJson(OGRGeometryH 	hGeometry)
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern string OGR_G_ExportToJson(IntPtr hGeom);

        //char* OGR_G_ExportToJsonEx(OGRGeometryH hGeometry,char ** papszOptions)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern string OGR_G_ExportToJsonEx(IntPtr hGeom, [In, Out]IntPtr[] options);

        //void OGR_G_AddPoint(OGRGeometryH hGeom, double dfX,double dfY, double dfZ)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_G_AddPoint(IntPtr hGeom, double dfX, double dfY, double dfZ);

        //void OGR_G_AddPoint_2D(OGRGeometryH hGeom, double dfX, double dfY)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_G_AddPoint_2D(IntPtr hGeom, double dfX, double dfY);

        //OGRErr OGR_G_AddGeometry(OGRGeometryH hGeom, OGRGeometryH hNewSubGeom)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_AddGeometry(IntPtr hGeom, IntPtr hNewSubGeom);

        //OGRErr OGR_G_AddGeometryDirectly(OGRGeometryH hGeom, OGRGeometryH hNewSubGeom)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_AddGeometryDirectly(IntPtr hGeom, IntPtr hNewSubGeom);

        //OGRGeometryH OGR_G_Clone	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_G_Clone(IntPtr hGeom);

        //OGRwkbGeometryType OGR_G_GetGeometryType	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Scanex.Gdal.wkbGeometryType OGR_G_GetGeometryType(IntPtr hGeom);

        //const char* OGR_G_GetGeometryName	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern string OGR_G_GetGeometryName(IntPtr hGeom);

        //double OGR_G_Length	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern double OGR_G_Length(IntPtr hGeom);

        //double OGR_G_Area	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern double OGR_G_Area(IntPtr hGeom);

        //int OGR_G_GetPointCount	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_GetPointCount(IntPtr hGeom);

        //double OGR_G_GetX	(	OGRGeometryH 	hGeom,int 	i )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern double OGR_G_GetX(IntPtr hGeom, int i);

        //double OGR_G_GetY	(	OGRGeometryH 	hGeom,int 	i )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern double OGR_G_GetY(IntPtr hGeom, int i);

        //double OGR_G_GetZ	(	OGRGeometryH 	hGeom,int 	i )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern double OGR_G_GetZ(IntPtr hGeom, int i);

        //void OGR_G_GetPoint	(	OGRGeometryH 	hGeom,int 	i,double * 	pdfX,double * 	pdfY,double * 	pdfZ )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_G_GetPoint(IntPtr hGeom, int i, out double x, out double y, out double z);

        //int OGR_G_GetGeometryCount	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_GetGeometryCount(IntPtr hGeom);

        //void OGR_G_SetPoint	(	OGRGeometryH 	hGeom,int 	i,double 	dfX,double 	dfY,double 	dfZ )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_G_SetPoint(IntPtr hGeom, int i, double x, double y, double z);

        //void OGR_G_SetPoint_2D	(	OGRGeometryH 	hGeom,int 	i,double 	dfX,double 	dfY )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_G_SetPoint_2D(IntPtr hGeom, int i, double x, double y);

        //OGRGeometryH OGR_G_GetGeometryRef	(	OGRGeometryH 	hGeom,int 	iSubGeom )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_G_GetGeometryRef(IntPtr hGeom, int iSubGeom);

        //OGRGeometryH OGR_G_Simplify	(	OGRGeometryH 	hThis,double 	dTolerance )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_G_Simplify(IntPtr hGeom, double dTolerance);

        //OGRGeometryH OGR_G_SimplifyPreserveTopology	(	OGRGeometryH 	hThis,double 	dTolerance )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_G_SimplifyPreserveTopology(IntPtr hGeom, double dTolerance);

        //OGRGeometryH OGR_G_Boundary	(	OGRGeometryH 	hTarget	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_G_Boundary(IntPtr hGeom);

        //OGRGeometryH OGR_G_ConvexHull	(	OGRGeometryH 	hTarget	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_G_ConvexHull(IntPtr hGeom);

        //OGRGeometryH OGR_G_Buffer	(	OGRGeometryH 	hTarget, double 	dfDist, int 	nQuadSegs )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_G_Buffer(IntPtr hGeom, double dist, int nQuadSegs);

        //OGRGeometryH OGR_G_Intersection	(	OGRGeometryH 	hThis,OGRGeometryH 	hOther )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_G_Intersection(IntPtr hGeom, IntPtr other);

        //OGRGeometryH OGR_G_Union	(	OGRGeometryH 	hThis,OGRGeometryH 	hOther )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_G_Union(IntPtr hGeom, IntPtr other);

        //OGRGeometryH OGR_G_UnionCascaded	(	OGRGeometryH 	hThis	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_G_UnionCascaded(IntPtr hGeom);

        //OGRGeometryH OGR_G_Difference	(	OGRGeometryH 	hThis,OGRGeometryH 	hOther )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_G_Difference(IntPtr hGeom, IntPtr other);

        //OGRGeometryH OGR_G_SymDifference	(	OGRGeometryH 	hThis,OGRGeometryH 	hOther )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_G_SymDifference(IntPtr hGeom, IntPtr other);

        //double OGR_G_Distance	(	OGRGeometryH 	hFirst,OGRGeometryH 	hOther )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern double OGR_G_Distance(IntPtr hGeom, IntPtr other);

        //void OGR_G_Empty	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_G_Empty(IntPtr hGeom);

        //int OGR_G_IsEmpty	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_IsEmpty(IntPtr hGeom);

        //int OGR_G_IsValid	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_IsValid(IntPtr hGeom);

        //int OGR_G_IsSimple	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_IsSimple(IntPtr hGeom);

        //int OGR_G_IsRing	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_IsRing(IntPtr hGeom);

        //int OGR_G_Intersects	(	OGRGeometryH 	hGeom,OGRGeometryH 	hOtherGeom )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_Intersects(IntPtr hGeom, IntPtr other);

        //int OGR_G_Equals	(	OGRGeometryH 	hGeom,OGRGeometryH 	hOther )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_Equals(IntPtr hGeom, IntPtr other);

        //int OGR_G_Disjoint	(	OGRGeometryH 	hThis,OGRGeometryH 	hOther )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_Disjoint(IntPtr hGeom, IntPtr other);

        //int OGR_G_Touches	(	OGRGeometryH 	hThis,OGRGeometryH 	hOther )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_Touches(IntPtr hGeom, IntPtr other);

        //int OGR_G_Crosses	(	OGRGeometryH 	hThis,OGRGeometryH 	hOther )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_Crosses(IntPtr hGeom, IntPtr other);

        //int OGR_G_Within	(	OGRGeometryH 	hThis,OGRGeometryH 	hOther )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_Within(IntPtr hGeom, IntPtr other);

        //int OGR_G_Contains	(	OGRGeometryH 	hThis,OGRGeometryH 	hOther )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_Contains(IntPtr hGeom, IntPtr other);
        
        //int OGR_G_Overlaps	(	OGRGeometryH 	hThis,OGRGeometryH 	hOther )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_Overlaps(IntPtr hGeom, IntPtr other);

        //void OGR_G_CloseRings	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_G_CloseRings(IntPtr hGeom);

        //void OGR_G_FlattenTo2D	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_G_FlattenTo2D(IntPtr hGeom);

        //void OGR_G_Segmentize	(	OGRGeometryH 	hGeom,double 	dfMaxLength )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_G_Segmentize(IntPtr hGeom, double dfMaxLength);

        //void OGR_G_GetEnvelope	(	OGRGeometryH 	hGeom,OGREnvelope * 	psEnvelope )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_G_GetEnvelope(IntPtr hGeom, out Scanex.Gdal.Envelope psEnvelope);

        //void OGR_G_GetEnvelope3D	(	OGRGeometryH 	hGeom,OGREnvelope * 	psEnvelope )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_G_GetEnvelope3D(IntPtr hGeom, out Scanex.Gdal.Envelope3D psEnvelope);

        //int OGR_G_Centroid	(	OGRGeometryH 	hGeom, OGRGeometryH 	hCentroidPoint )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_Centroid(IntPtr hGeom, IntPtr hCentroidPoint);

        //OGRGeometryH OGR_G_PointOnSurface	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_G_PointOnSurface(IntPtr hGeom);

        //int OGR_G_GetCoordinateDimension	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_GetCoordinateDimension(IntPtr hGeom);

        //void OGR_G_SetCoordinateDimension	(	OGRGeometryH 	hGeom,int 	nNewDimension )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_G_SetCoordinateDimension(IntPtr hGeom, int nNewDimension);

        //int OGR_G_GetDimension	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_GetDimension(IntPtr hGeom);

        //OGRErr OGR_G_Transform	(	OGRGeometryH 	hGeom,OGRCoordinateTransformationH 	hTransform )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_Transform(IntPtr hGeom, IntPtr hTransform);

        //OGRErr OGR_G_TransformTo	(	OGRGeometryH 	hGeom, OGRSpatialReferenceH 	hSRS )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_G_TransformTo(IntPtr hGeom, IntPtr hSRS);

        //OGRSpatialReferenceH OGR_G_GetSpatialReference	(	OGRGeometryH 	hGeom	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_G_GetSpatialReference(IntPtr hGeom);

        //void OGR_G_AssignSpatialReference	(	OGRGeometryH 	hGeom,OGRSpatialReferenceH 	hSRS )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_G_AssignSpatialReference(IntPtr hGeom, IntPtr hSRS);

        #endregion

        #region FeatureDefn
        //OGRFeatureDefnH OGR_FD_Create	(	const char * 	pszName	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr OGR_FD_Create(string name);

        //OGRErr OGR_FD_DeleteFieldDefn	(	OGRFeatureDefnH 	hDefn,int 	iField )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OGR_FD_DeleteFieldDefn(IntPtr hOGRFeatureDefn, int iField);
 	
        //OGRErr OGR_FD_DeleteGeomFieldDefn	(	OGRFeatureDefnH 	hDefn,int 	iGeomField )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OGR_FD_DeleteGeomFieldDefn(IntPtr hOGRFeatureDefn, int iGeomField);

        //void OGR_FD_Destroy	(	OGRFeatureDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void OGR_FD_Destroy(IntPtr hOGRFeatureDefn);

        //int OGR_FD_GetFieldCount	(	OGRFeatureDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OGR_FD_GetFieldCount(IntPtr hOGRFeatureDefn);

        //OGRFieldDefnH OGR_FD_GetFieldDefn	(	OGRFeatureDefnH 	hDefn,int 	iField )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr OGR_FD_GetFieldDefn(IntPtr hOGRFeatureDefn, int iField);

        //int OGR_FD_GetFieldIndex	(	OGRFeatureDefnH 	hDefn,const char * 	pszFieldName )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OGR_FD_GetFieldIndex(IntPtr hOGRFeatureDefn, IntPtr fieldName);

        //int OGR_FD_GetGeomFieldCount	(	OGRFeatureDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OGR_FD_GetGeomFieldCount(IntPtr hOGRFeatureDefn);

        //OGRGeomFieldDefnH OGR_FD_GetGeomFieldDefn	(	OGRFeatureDefnH 	hDefn,int 	iGeomField )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr OGR_FD_GetGeomFieldDefn(IntPtr hOGRFeatureDefn, int iGeomField);

        //int OGR_FD_GetGeomFieldIndex	(	OGRFeatureDefnH 	hDefn,const char * 	pszGeomFieldName )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OGR_FD_GetGeomFieldIndex(IntPtr hOGRFeatureDefn, IntPtr geomFieldName);

        //OGRwkbGeometryType OGR_FD_GetGeomType	(	OGRFeatureDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern Scanex.Gdal.wkbGeometryType OGR_FD_GetGeomType(IntPtr hOGRFeatureDefn);

        //const char* OGR_FD_GetName	(	OGRFeatureDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr OGR_FD_GetName(IntPtr hOGRFeatureDefn);

        //int OGR_FD_IsGeometryIgnored	(	OGRFeatureDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OGR_FD_IsGeometryIgnored(IntPtr hOGRFeatureDefn);

        //int OGR_FD_IsSame	(	OGRFeatureDefnH 	hFDefn,OGRFeatureDefnH 	hOtherFDefn )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int OGR_FD_IsSame(IntPtr hOGRFeatureDefn, IntPtr hOtherFDefn);

        //void OGR_FD_Release	(	OGRFeatureDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void OGR_FD_Release(IntPtr hOGRFeatureDefn);

        //int OGR_FD_GetReferenceCount	(	OGRFeatureDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_FD_GetReferenceCount(IntPtr hOGRFeatureDefn);

        //int OGR_FD_IsStyleIgnored	(	OGRFeatureDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_FD_IsStyleIgnored(IntPtr hOGRFeatureDefn);

        //void OGR_FD_SetGeometryIgnored	(	OGRFeatureDefnH 	hDefn,int 	bIgnore )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_FD_SetGeometryIgnored(IntPtr hOGRFeatureDefn, int ignore);

        //void OGR_FD_SetGeomType	(	OGRFeatureDefnH 	hDefn,OGRwkbGeometryType 	eType )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_FD_SetGeomType(IntPtr hOGRFeatureDefn, Scanex.Gdal.wkbGeometryType eType);

        //void OGR_FD_SetStyleIgnored	(	OGRFeatureDefnH 	hDefn,int 	bIgnore )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_FD_SetStyleIgnored(IntPtr hOGRFeatureDefn, int ignore);

        //void OGR_FD_AddFieldDefn	(	OGRFeatureDefnH 	hDefn,OGRFieldDefnH 	hNewField )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_FD_AddFieldDefn(IntPtr hOGRFeatureDefn, IntPtr newField);

        //void OGR_FD_AddGeomFieldDefn	(	OGRFeatureDefnH 	hDefn,OGRGeomFieldDefnH 	hNewGeomField )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_FD_AddGeomFieldDefn(IntPtr hOGRFeatureDefn, IntPtr hNewGeomField);


        #endregion

        #region Layer

        //const char * OGR_L_GetName	(	OGRLayerH 	hLayer	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_L_GetName(IntPtr hLayer);

        //OGRFeatureDefnH OGR_L_GetLayerDefn	(	OGRLayerH 	hLayer	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_L_GetLayerDefn(IntPtr hLayer);

        //OGRFeatureH OGR_L_GetNextFeature	(	OGRLayerH 	hLayer	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_L_GetNextFeature(IntPtr hLayer);

        //OGRGeometryH OGR_L_GetSpatialFilter	(	OGRLayerH 	hLayer	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_L_GetSpatialFilter(IntPtr hLayer);

        //OGRSpatialReferenceH OGR_L_GetSpatialRef	(	OGRLayerH 	hLayer	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_L_GetSpatialRef(IntPtr hLayer);

        //OGRErr OGR_L_ReorderField	(	OGRLayerH 	hLayer,int 	iOldFieldPos,int 	iNewFieldPos )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_L_ReorderField(IntPtr hLayer, int oldPosition, int newDieldPos);

        //void OGR_L_ResetReading	(	OGRLayerH 	hLayer	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_L_ResetReading(IntPtr hLayer);

        //OGRErr OGR_L_RollbackTransaction	(	OGRLayerH 	hLayer	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_L_RollbackTransaction(IntPtr hLayer);

        //OGRErr OGR_L_SetAttributeFilter	(	OGRLayerH 	hLayer,const char * 	pszQuery )		
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_L_SetAttributeFilter(IntPtr hLayer, IntPtr query);

        //OGRErr OGR_L_SetFeature	(	OGRLayerH 	hLayer,OGRFeatureH 	hFeat )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_L_SetFeature(IntPtr hLayer, IntPtr hFeat);

        //OGRErr OGR_L_SetNextByIndex	(	OGRLayerH 	hLayer,GIntBig 	nIndex )		
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_L_SetNextByIndex(IntPtr hLayer, Int64 index);

        //void OGR_L_SetSpatialFilter	(	OGRLayerH 	hLayer,OGRGeometryH 	hGeom )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_L_SetSpatialFilter(IntPtr hLayer, IntPtr hGeom);

        //void OGR_L_SetSpatialFilterEx	(	OGRLayerH 	hLayer,int 	iGeomField,OGRGeometryH 	hGeom )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_L_SetSpatialFilterEx(IntPtr hLayer, int geomField, IntPtr hGeom);

        //void OGR_L_SetSpatialFilterRect	(	OGRLayerH 	hLayer,double 	dfMinX,double 	dfMinY,double 	dfMaxX,double 	dfMaxY )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_L_SetSpatialFilterRect(IntPtr hLayer, double dfMinX, double dfMinY, double dfMaxX, double dfMaxY);

        //void OGR_L_SetSpatialFilterRectEx	(	OGRLayerH 	hLayer,int 	iGeomField,double 	dfMinX,double 	dfMinY,double 	dfMaxX,double 	dfMaxY )		
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_L_SetSpatialFilterRectEx(IntPtr hLayer, int iGeomField, double dfMinX, double dfMinY, double dfMaxX, double dfMaxY);

        //OGRErr OGR_L_StartTransaction	(	OGRLayerH 	hLayer	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_L_StartTransaction(IntPtr hLayer);

        //OGRErr OGR_L_SyncToDisk	(	OGRLayerH 	hLayer	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_L_SyncToDisk(IntPtr hLayer);

        //OGRErr OGR_L_CreateField	(	OGRLayerH 	hLayer,OGRFieldDefnH 	hField,int 	bApproxOK )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_L_CreateField(IntPtr hLayer, IntPtr hField, int approxOK);

        //OGRErr OGR_L_CreateFeature	(	OGRLayerH 	hLayer,OGRFeatureH 	hFeat )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_L_CreateFeature(IntPtr hLayer, IntPtr hFeat);

        //GIntBig OGR_L_GetFeatureCount	(	OGRLayerH 	hLayer,int 	bForce )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 OGR_L_GetFeatureCount(IntPtr hLayer, int force);

        //OGRErr OGR_L_GetExtent	(	OGRLayerH 	hLayer, OGREnvelope * 	psExtent,int 	bForce )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_L_GetExtent(IntPtr hLayer, out Scanex.Gdal.Envelope psExtent, int force);

        //OGRErr OGR_L_GetExtentEx	(	OGRLayerH 	hLayer,int 	iGeomField, OGREnvelope * 	psExtent,int 	bForce )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_L_GetExtentEx(IntPtr hLayer, int iGeomField, out Scanex.Gdal.Envelope psExtent, int force);

        #endregion

        #region GeomFieldDefn

        //OGRGeomFieldDefnH OGR_GFld_Create	(	const char * 	pszName,OGRwkbGeometryType 	eType )		
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_GFld_Create(IntPtr name, Scanex.Gdal.wkbGeometryType eType);

        //void OGR_GFld_Destroy	(	OGRGeomFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_GFld_Destroy(IntPtr hDefn);

        //const char* OGR_GFld_GetNameRef	(	OGRGeomFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_GFld_GetNameRef(IntPtr hDefn);

        //OGRSpatialReferenceH OGR_GFld_GetSpatialRef	(	OGRGeomFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_GFld_GetSpatialRef(IntPtr hDefn);

        //OGRwkbGeometryType OGR_GFld_GetType	(	OGRGeomFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Scanex.Gdal.wkbGeometryType OGR_GFld_GetType(IntPtr hDefn);

        //int OGR_GFld_IsIgnored	(	OGRGeomFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_GFld_IsIgnored(IntPtr hDefn);

        //int OGR_GFld_IsNullable	(	OGRGeomFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_GFld_IsNullable(IntPtr hDefn);

        //void OGR_GFld_SetIgnored	(	OGRGeomFieldDefnH 	hDefn,int 	ignore )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_GFld_SetIgnored(IntPtr hDefn, int ignore);

        //void OGR_GFld_SetName	(	OGRGeomFieldDefnH 	hDefn, const char * 	pszName )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_GFld_SetName(IntPtr hDefn, IntPtr name);

        //void OGR_GFld_SetNullable	(	OGRGeomFieldDefnH 	hDefn,int 	bNullableIn )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_GFld_SetNullable(IntPtr hDefn, int nullable);

        //void OGR_GFld_SetSpatialRef	(	OGRGeomFieldDefnH 	hDefn,OGRSpatialReferenceH 	hSRS )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_GFld_SetSpatialRef(IntPtr hDefn, IntPtr hSRS);

        //void OGR_GFld_SetType	(	OGRGeomFieldDefnH 	hDefn,OGRwkbGeometryType 	eType )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_GFld_SetType(IntPtr hDefn, Scanex.Gdal.wkbGeometryType eType);




        #endregion

        #region FieldDefn

        //OGRFieldDefnH OGR_Fld_Create	(	const char * 	pszName,OGRFieldType 	eType )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_Fld_Create([In, Out]byte[] name, Scanex.Gdal.FieldType type);

        //void OGR_Fld_Destroy	(	OGRFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_Fld_Destroy(IntPtr handle);

        //const char* OGR_Fld_GetDefault	(	OGRFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_Fld_GetDefault(IntPtr handle);

        //OGRJustification OGR_Fld_GetJustify	(	OGRFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Scanex.Gdal.Justification OGR_Fld_GetJustify(IntPtr handle);

        //const char* OGR_Fld_GetNameRef	(	OGRFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_Fld_GetNameRef(IntPtr handle);

        //int OGR_Fld_GetPrecision	(	OGRFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_Fld_GetPrecision(IntPtr handle);

        //OGRFieldSubType OGR_Fld_GetSubType	(	OGRFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Scanex.Gdal.FieldSubType OGR_Fld_GetSubType(IntPtr handle);

        //OGRFieldType OGR_Fld_GetType	(	OGRFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Scanex.Gdal.FieldType OGR_Fld_GetType(IntPtr handle);

        //int OGR_Fld_GetWidth	(	OGRFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_Fld_GetWidth(IntPtr handle);

        //int OGR_Fld_IsDefaultDriverSpecific	(	OGRFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_Fld_IsDefaultDriverSpecific(IntPtr handle);

        //int OGR_Fld_IsIgnored	(	OGRFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_Fld_IsIgnored(IntPtr handle);

        //int OGR_Fld_IsNullable	(	OGRFieldDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_Fld_IsNullable(IntPtr handle);

        //void OGR_Fld_Set	(	OGRFieldDefnH 	hDefn,const char * 	pszNameIn,OGRFieldType 	eTypeIn,int 	nWidthIn,int 	nPrecisionIn,OGRJustification 	eJustifyIn )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_Fld_Set(IntPtr handle, [In, Out] byte[] name, Scanex.Gdal.FieldType typeIn, int widthIn, int precisionIn, Scanex.Gdal.Justification justify);

        //void OGR_Fld_SetDefault	(	OGRFieldDefnH 	hDefn,const char * 	pszDefault )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_Fld_SetDefault(IntPtr handle, [In, Out] byte[] name);

        //void OGR_Fld_SetIgnored	(	OGRFieldDefnH 	hDefn,int 	ignore )
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_Fld_SetIgnored(IntPtr handle, int ignore);

        //void OGR_Fld_SetJustify	(	OGRFieldDefnH 	hDefn,OGRJustification 	eJustify )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_Fld_SetJustify(IntPtr handle, Scanex.Gdal.Justification justify);

        //void OGR_Fld_SetName	(	OGRFieldDefnH 	hDefn,const char * 	pszName )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_Fld_SetName(IntPtr handle, [In, Out] byte[] name);

        //void OGR_Fld_SetNullable	(	OGRFieldDefnH 	hDefn,int 	bNullableIn )		
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_Fld_SetNullable(IntPtr handle, int nullableIn);

        //void OGR_Fld_SetPrecision	(	OGRFieldDefnH 	hDefn,int 	nPrecision )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_Fld_SetPrecision(IntPtr handle, int nPrecision);

        //void OGR_Fld_SetSubType	(	OGRFieldDefnH 	hDefn,OGRFieldSubType 	eSubType )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_Fld_SetSubType(IntPtr handle, Scanex.Gdal.FieldSubType eSubType);

        //void OGR_Fld_SetType	(	OGRFieldDefnH 	hDefn,OGRFieldType 	eType )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_Fld_SetType(IntPtr handle, Scanex.Gdal.FieldType eType);

        //void OGR_Fld_SetWidth	(	OGRFieldDefnH 	hDefn,int 	nNewWidth )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_Fld_SetWidth(IntPtr handle, int newWidth);

        //const char* OGR_GetFieldTypeName	(	OGRFieldType 	eType	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_GetFieldTypeName(Scanex.Gdal.FieldType type);


        #endregion

        #region Feature

        //OGRFeatureH OGR_F_Create	(	OGRFeatureDefnH 	hDefn	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_Create(IntPtr hDefn);

        //void OGR_F_Destroy	(	OGRFeatureH 	hFeat	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_Destroy(IntPtr hFeat);

        //int OGR_F_Equal	(	OGRFeatureH 	hFeat,OGRFeatureH 	hOtherFeat )		
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_F_Equal(IntPtr hFeat, IntPtr otherFeat);

        //void OGR_F_FillUnsetWithDefault	(	OGRFeatureH 	hFeat,int 	bNotNullableOnly,char ** 	papszOptions )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_FillUnsetWithDefault(IntPtr hFeat, int notNullableOnly, [In, Out] IntPtr[] options);

        //OGRFeatureDefnH OGR_F_GetDefnRef	(	OGRFeatureH 	hFeat	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_GetDefnRef(IntPtr hFeat);

        //GIntBig OGR_F_GetFID	(	OGRFeatureH 	hFeat	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 OGR_F_GetFID(IntPtr hFeat);

        //OGRErr OGR_F_SetFID	(	OGRFeatureH 	hFeat,GIntBig 	nFID )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_F_SetFID(IntPtr hFeat, Int64 nFID);

        //GByte* OGR_F_GetFieldAsBinary	(	OGRFeatureH 	hFeat,int 	iField,int * 	pnBytes )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_GetFieldAsBinary(IntPtr hFeat, int iField, out int bytes);

        //OGRErr OGR_F_SetGeometryDirectly	(	OGRFeatureH 	hFeat,OGRGeometryH 	hGeom )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_F_SetGeometryDirectly(IntPtr hFeat, IntPtr hGeom);

        //OGRErr OGR_F_SetGeomField	(	OGRFeatureH 	hFeat,int 	iField,OGRGeometryH 	hGeom )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_F_SetGeomField(IntPtr hFeat, int iField, IntPtr hGeom);

        //OGRErr OGR_F_SetGeomFieldDirectly	(	OGRFeatureH 	hFeat,int 	iField,OGRGeometryH 	hGeom )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_F_SetGeomFieldDirectly(IntPtr hFeat, int iField, IntPtr hGeom);

        //void OGR_F_SetNativeData	(	OGRFeatureH 	hFeat,const char * 	pszNativeData )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetNativeData(IntPtr hFeat, [In, Out]byte[] nativeData);

        //void OGR_F_SetNativeMediaType	(	OGRFeatureH 	hFeat,const char * 	pszNativeMediaType )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetNativeMediaType(IntPtr hFeat, [In, Out] byte[] nativeData);

        //void OGR_F_SetStyleString	(	OGRFeatureH 	hFeat,const char * 	pszStyle )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetStyleString(IntPtr hFeat, [In, Out]byte[] style);

        //void OGR_F_SetStyleStringDirectly	(	OGRFeatureH 	hFeat,char * 	pszStyle )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetStyleStringDirectly(IntPtr hFeat, [In, Out]byte[] style);

        //OGRGeometryH OGR_F_StealGeometry	(	OGRFeatureH 	hFeat	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_StealGeometry(IntPtr hFeat);

        //void OGR_F_UnsetField	(	OGRFeatureH 	hFeat,int 	iField )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_UnsetField(IntPtr hFeat, int iField);

        //OGRFeatureH OGR_F_Clone	(	OGRFeatureH 	hFeat	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_Clone(IntPtr hFeat);

        //int OGR_F_GetFieldAsDateTime	(	OGRFeatureH 	hFeat,int 	iField,int * 	pnYear,int * 	pnMonth,int * 	pnDay,int * 	pnHour,int * 	pnMinute,int * 	pnSecond,int * 	pnTZFlag )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_F_GetFieldAsDateTime(IntPtr hFeat, int iField, out int year, out int month, out int day, out int hour, out int minute, out int second, out int tzFlag);

        //int OGR_F_GetFieldAsDateTime	(	OGRFeatureH 	hFeat,int 	iField,int * 	pnYear,int * 	pnMonth,int * 	pnDay,int * 	pnHour,int * 	pnMinute,int * 	pnSecond,int * 	pnTZFlag )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_F_GetFieldAsDateTimeEx(IntPtr hFeat, int iField, out int year, out int month, out int day, out int hour, out int minute, out float second, out int tzFlag);

        //double OGR_F_GetFieldAsDouble	(	OGRFeatureH 	hFeat,int 	iField )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern double OGR_F_GetFieldAsDouble(IntPtr hFeat, int iField);

        //const double* OGR_F_GetFieldAsDoubleList	(	OGRFeatureH 	hFeat,int 	iField,int * 	pnCount )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_GetFieldAsDoubleList(IntPtr hFeat, int iField, out int count);

        //int OGR_F_GetFieldAsInteger	(	OGRFeatureH 	hFeat,int 	iField )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_F_GetFieldAsInteger(IntPtr hFeat, int iField);

        //GIntBig OGR_F_GetFieldAsInteger64	(	OGRFeatureH 	hFeat,int 	iField )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 OGR_F_GetFieldAsInteger64(IntPtr hFeat, int iField);

        //const GIntBig* OGR_F_GetFieldAsInteger64List	(	OGRFeatureH 	hFeat,int 	iField,int * 	pnCount )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_GetFieldAsInteger64List(IntPtr hFeat, int iField, out int count);

        //const int* OGR_F_GetFieldAsIntegerList	(	OGRFeatureH 	hFeat,int 	iField,int * 	pnCount )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_GetFieldAsIntegerList(IntPtr hFeat, int iField, out int count);

        //const char* OGR_F_GetFieldAsString	(	OGRFeatureH 	hFeat,int 	iField )
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_GetFieldAsString(IntPtr hFeat, int iField);

        //int OGR_F_GetFieldCount	(	OGRFeatureH 	hFeat	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_F_GetFieldCount(IntPtr hFeat);

        //OGRFieldDefnH OGR_F_GetFieldDefnRef	(	OGRFeatureH 	hFeat,int 	i )		
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_GetFieldDefnRef(IntPtr hFeat, int i);

        //int OGR_F_GetFieldIndex	(	OGRFeatureH 	hFeat,const char * 	pszName )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_F_GetFieldIndex(IntPtr hFeat, IntPtr name);

        //OGRGeometryH OGR_F_GetGeometryRef	(	OGRFeatureH 	hFeat	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_GetGeometryRef(IntPtr hFeat);

        //int OGR_F_GetGeomFieldCount	(	OGRFeatureH 	hFeat	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_F_GetGeomFieldCount(IntPtr hFeat);

        //OGRGeomFieldDefnH OGR_F_GetGeomFieldDefnRef	(	OGRFeatureH 	hFeat,int 	i )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_GetGeomFieldDefnRef(IntPtr hFeat, int i);

        //int OGR_F_GetGeomFieldIndex	(	OGRFeatureH 	hFeat,const char * 	pszName )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_F_GetGeomFieldIndex(IntPtr hFeat, [In, Out] byte[] name);

        //OGRGeometryH OGR_F_GetGeomFieldRef	(	OGRFeatureH 	hFeat,int 	iField )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_GetGeomFieldRef(IntPtr hFeat, int iField);

        //const char* OGR_F_GetNativeData	(	OGRFeatureH 	hFeat	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_GetNativeData(IntPtr hFeat);

        //const char* OGR_F_GetNativeMediaType	(	OGRFeatureH 	hFeat	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_GetNativeMediaType(IntPtr hFeat);

        //OGRField* OGR_F_GetRawFieldRef	(	OGRFeatureH 	hFeat,int 	iField )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_GetRawFieldRef(IntPtr hFeat, int iField);

        //const char* OGR_F_GetStyleString	(	OGRFeatureH 	hFeat	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_F_GetStyleString(IntPtr hFeat);

        //int OGR_F_IsFieldSet	(	OGRFeatureH 	hFeat,int 	iField )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_F_IsFieldSet(IntPtr hFeat, int iField);

        //void OGR_F_SetFieldBinary	(	OGRFeatureH 	hFeat,int 	iField,int 	nBytes,GByte * 	pabyData )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetFieldBinary(IntPtr hFeat, int iField, int nBytes, IntPtr data);

        //void OGR_F_SetFieldDateTimeEx	(	OGRFeatureH 	hFeat,int 	iField,int 	nYear,int 	nMonth,int 	nDay,int 	nHour,int 	nMinute,float 	fSecond,int 	nTZFlag )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetFieldDateTimeEx(IntPtr hFeat, int iField, int nYear, int nMonth, int nDay, int nHour, int nMinute, float fSecond, int nTZFlag);

        //void OGR_F_SetFieldDateTime	(	OGRFeatureH 	hFeat,int 	iField,int 	nYear,int 	nMonth,int 	nDay,int 	nHour,int 	nMinute,int 	nSecond,int 	nTZFlag )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetFieldDateTime(IntPtr hFeat, int iField, int nYear, int nMonth, int nDay, int nHour, int nMinute, int nSecond, int nTZFlag);

        //void OGR_F_SetFieldDouble	(	OGRFeatureH 	hFeat,int 	iField,double 	dfValue )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetFieldDouble(IntPtr hFeat, int iField, double value);

        //void OGR_F_SetFieldDoubleList	(	OGRFeatureH 	hFeat,int 	iField,int 	nCount,double * 	padfValues )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetFieldDoubleList(IntPtr hFeat, int iField, int count, IntPtr values);

        //void OGR_F_SetFieldInteger	(	OGRFeatureH 	hFeat,int 	iField,int 	nValue )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetFieldInteger(IntPtr hFeat, int iField, int value);

        //void OGR_F_SetFieldInteger64	(	OGRFeatureH 	hFeat,int 	iField,GIntBig 	nValue )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetFieldInteger64(IntPtr hFeat, int iField, Int64 value);

        //void OGR_F_SetFieldInteger64List	(	OGRFeatureH 	hFeat,int 	iField,int 	nCount,const GIntBig * 	panValues )
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetFieldInteger64List(IntPtr hFeat, int iField, int count, IntPtr values);

        //void OGR_F_SetFieldIntegerList	(	OGRFeatureH 	hFeat,int 	iField,int 	nCount,int * 	panValues )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetFieldIntegerList(IntPtr hFeat, int iField, int count, IntPtr values);

        //void OGR_F_SetFieldRaw	(	OGRFeatureH 	hFeat,int 	iField,OGRField * 	psValue )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetFieldRaw(IntPtr hFeat, int iField, IntPtr value);

        //void OGR_F_SetFieldString	(	OGRFeatureH 	hFeat,int 	iField,const char * 	pszValue )		
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetFieldString(IntPtr hFeat, int iField, IntPtr value);

        //void OGR_F_SetFieldStringList	(	OGRFeatureH 	hFeat,int 	iField,char ** 	papszValues )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_F_SetFieldStringList(IntPtr hFeat, int iField, [In, Out]IntPtr[] values);

        //OGRErr OGR_F_SetFrom	(	OGRFeatureH 	hFeat ,OGRFeatureH 	hOtherFeat,int 	bForgiving )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_F_SetFrom(IntPtr hFeat, IntPtr otherFeat, int forgiving);

        //OGRErr OGR_F_SetGeometry	(	OGRFeatureH 	hFeat,OGRGeometryH 	hGeom )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OGR_F_SetGeometry(IntPtr hFeat, IntPtr hGeom);






        #endregion

        #region DataSource

        //void OGR_DS_Destroy	(	OGRDataSourceH 	hDataSource	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void OGR_DS_Destroy(IntPtr hDataSource);

        //OGRLayerH OGR_DS_ExecuteSQL	(	OGRDataSourceH 	hDS,const char * 	pszSQLCommand,OGRGeometryH 	hSpatialFilter,const char * 	pszDialect )	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_DS_ExecuteSQL(IntPtr hDataSource, IntPtr sqlCommand, IntPtr spatialFilter, IntPtr dialect);

        //OGRSFDriverH OGR_DS_GetDriver	(	OGRDataSourceH 	hDS	)	
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OGR_DS_GetDriver(IntPtr hDataSource);


        #endregion

        //нет объявления. Синтаксис по догадке
        [DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void OGRFree(IntPtr handle);

        //Скорее всего выглядит так:
        //VSIFileFromMemBuffer(utf8_path, (GByte*) pabyDataDup, nBytes, TRUE)

        //void 	VSIFree (void *)
        //[DllImport(GdalDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        //public static extern void VSIFree(IntPtr handle);
        //public static extern int OGR_G_ExportToWkt(IntPtr hGeom, [MarshalAs(UnmanagedType.LPStr)] out string text);

        //[DllImport(GdalDllName, CallingConvention = CallingConvention.StdCall)]
        //public static extern int OGR_G_ExportToWkt(IntPtr hGeom, ref IntPtr p); 

        /*
        internal unsafe static List<string> StringListParse(IntPtr strPtr)
        {
            List<string> res = new List<string>();
            void** stringList = (void**)strPtr;
            if (new IntPtr(stringList) != IntPtr.Zero)
            {
                while (new IntPtr(*stringList) != IntPtr.Zero)
                {
                    string keyValue = Marshal.PtrToStringAnsi(new IntPtr(*stringList));
                    res.Add(keyValue);
                    ++stringList;
                }
            }
            return res;
        }*/
    }
}

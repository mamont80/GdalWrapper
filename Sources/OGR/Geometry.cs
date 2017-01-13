using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Scanex.Gdal;

namespace Scanex.Gdal
{
    public class Geometry : CustomGdalObject
    {
        protected override void DestroyNativeObj()
        {
            PInvokeOgr.OGR_G_DestroyGeometry(Handle);
        }

        public Geometry(IntPtr cPtr, bool cMemoryOwn, object parent)
        {
            Init(cPtr, cMemoryOwn, parent);
        }

        public Geometry(wkbGeometryType wkbType):this(PInvokeOgr.OGR_G_CreateGeometry(wkbType), true, null)
        {
        }

        public Geometry(string wkt)
        {
            IntPtr p;
            using (var lst = new MarshalUtils.StringExport(wkt))
            {
                IntPtr sp = lst.Pointer;
                PInvokeOgr.OGR_G_CreateFromWkt(ref sp, IntPtr.Zero, out p);
            }
            Init(p, true, null);
        }

        public static Geometry CreateFromWkt(string wkt)
        {
            return new Geometry(wkt);
        }

        public static Geometry CreateFromGML(string gml)
        {
            IntPtr p;
            p = PInvokeOgr.OGR_G_CreateFromGML(gml);
            if (p == null) Error();
            return new Geometry(p, true, null);
        }

        private static void Error()
        {
            throw new Exception("OGR error");
        }

        public static Geometry CreateFromWkb(byte[] wkb)
        {
            if (wkb.Length == 0)
                throw new ArgumentException("Buffer size is small (CreateFromWkb)");
            IntPtr ptr = Marshal.AllocHGlobal(wkb.Length * Marshal.SizeOf(wkb[0]));
            try
            {
                Marshal.Copy(wkb, 0, ptr, wkb.Length);
                IntPtr hGeom;
                PInvokeOgr.OGR_G_CreateFromWkb(ptr, IntPtr.Zero, out hGeom, wkb.Length);
                if (hGeom == null) throw new Exception("Can not create geometry by WKB");
                return new Geometry(hGeom, true, null);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        //не работает
        public int ExportToWkt(out string argout)
        {
            IntPtr p = IntPtr.Zero;
            int code = PInvokeOgr.OGR_G_ExportToWkt(Handle, ref p);
            argout = Marshal.PtrToStringAnsi(p);
            PInvokeOgr.OGRFree(p);
            return code;
        }

        public byte[] ExportToWkb(wkbByteOrder byte_order)
        {
            int size = WkbSize();
            byte[] buffer = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size * Marshal.SizeOf(buffer[0]));
            try
            {
                PInvokeOgr.OGR_G_ExportToWkb(Handle, byte_order, ptr);
                Marshal.Copy(ptr, buffer, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
            GC.KeepAlive(this);
            return buffer;
        }

        public string ExportToGML()
        {
            string s = PInvokeOgr.OGR_G_ExportToGML(Handle);
            if (string.IsNullOrEmpty(s)) Error();
            return s;
        }

        public string ExportToGML(string[] options) 
        {
            using (var lst = new MarshalUtils.StringListExport(options))
            {
                string s = PInvokeOgr.OGR_G_ExportToGMLEx(Handle, lst.Pointer);
                if (string.IsNullOrEmpty(s)) Error();
                return s;
            }
        }

        public string ExportToKML(string altitude_mode)
        {
            string s = PInvokeOgr.OGR_G_ExportToKML(Handle, altitude_mode);
            if (string.IsNullOrEmpty(s)) Error();
            return s;
        }

        public string ExportToJson(string[] options)
        {
            using (var lst = new MarshalUtils.StringListExport(options))
            {
                string s = PInvokeOgr.OGR_G_ExportToJsonEx(Handle, lst.Pointer);
                if (string.IsNullOrEmpty(s)) Error();
                return s;
            }
        }
        public string ExportToJson()
        {
            string s = PInvokeOgr.OGR_G_ExportToJson(Handle);
            if (string.IsNullOrEmpty(s)) Error();
            return s;
        }

        public void AddPoint(double x, double y, double z)
        {
            PInvokeOgr.OGR_G_AddPoint(Handle, x, y, z);
        }

        public void AddPoint_2D(double x, double y)
        {
            PInvokeOgr.OGR_G_AddPoint_2D(Handle, x, y);
        }

        public void AddGeometryDirectly(Geometry other_disown)
        {
            int ret = PInvokeOgr.OGR_G_AddGeometryDirectly(Handle, Geometry.getCPtrAndDisown(other_disown, ThisOwn_false()));
            Errors.CheckError(ret);
        }

        public void AddGeometry(Geometry other)
        {
            int ret = PInvokeOgr.OGR_G_AddGeometry(Handle, other.Handle);
            Errors.CheckError(ret);
        }

        public Geometry Clone()
        {
            IntPtr cPtr = PInvokeOgr.OGR_G_Clone(Handle);
            Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, null);
            return ret;
        }

        public wkbGeometryType GetGeometryType()
        {
            return PInvokeOgr.OGR_G_GetGeometryType(Handle);
        }

        public string GetGeometryName()
        {
            return PInvokeOgr.OGR_G_GetGeometryName(Handle);
        }

        public double Length()
        {
            return PInvokeOgr.OGR_G_Length(Handle);
        }

        public double Area()
        {
            return PInvokeOgr.OGR_G_Area(Handle);
        }
        /// <summary>
        /// Area of geometry. Alias of Area()
        /// </summary>
        public double GetArea()
        {
            return Area();
        }

        public int GetPointCount()
        {
            return PInvokeOgr.OGR_G_GetPointCount(Handle);
        }

        public double GetX(int point)
        {
            return PInvokeOgr.OGR_G_GetX(Handle, point);
        }

        public double GetY(int point)
        {
            return PInvokeOgr.OGR_G_GetY(Handle, point);
        }

        public double GetZ(int point)
        {
            return PInvokeOgr.OGR_G_GetZ(Handle, point);
        }

        public void GetPoint(int iPoint, double[] argout)
        {
            double x;
            double y;
            double z;
            PInvokeOgr.OGR_G_GetPoint(Handle, iPoint, out x, out y, out z);
            argout[0] = x;
            argout[1] = y;
            argout[2] = z;
        }

        public void GetPoint(int iPoint, out double x, out double y, out double z)
        {
            PInvokeOgr.OGR_G_GetPoint(Handle, iPoint, out x, out y, out z);
        }

        public void GetPoint_2D(int iPoint, double[] argout)
        {
            double x;
            double y;
            double z;
            PInvokeOgr.OGR_G_GetPoint(Handle, iPoint, out x, out y, out z);
            argout[0] = x;
            argout[1] = y;
        }

        public void GetPoint(int iPoint, out double x, out double y)
        {
            double z;
            PInvokeOgr.OGR_G_GetPoint(Handle, iPoint, out x, out y, out z);
        }

        public int GetGeometryCount()
        {
            return PInvokeOgr.OGR_G_GetGeometryCount(Handle);
        }

        public void SetPoint(int point, double x, double y, double z)
        {
            PInvokeOgr.OGR_G_SetPoint(Handle, point, x, y, z);
        }

        public void SetPoint_2D(int point, double x, double y)
        {
            PInvokeOgr.OGR_G_SetPoint_2D(Handle, point, x, y);
        }

        public Geometry GetGeometryRef(int geom)
        {
            IntPtr p = PInvokeOgr.OGR_G_GetGeometryRef(Handle, geom);
            Geometry ret = (p == IntPtr.Zero) ? null : new Geometry(p, false, ThisOwn_false());
            return ret;
        }

        public Geometry Simplify(double tolerance)
        {
            IntPtr p = PInvokeOgr.OGR_G_Simplify(Handle, tolerance);
            Geometry ret = (p == IntPtr.Zero) ? null : new Geometry(p, true, null);
            return ret;
        }

        public Geometry SimplifyPreserveTopology(double tolerance)
        {
            IntPtr p = PInvokeOgr.OGR_G_SimplifyPreserveTopology(Handle, tolerance);
            Geometry ret = (p == IntPtr.Zero) ? null : new Geometry(p, true, null);
            return ret;
        }

        public Geometry Boundary()
        {
            IntPtr p = PInvokeOgr.OGR_G_Boundary(Handle);
            Geometry ret = (p == IntPtr.Zero) ? null : new Geometry(p, true, null);
            return ret;
        }

        public Geometry ConvexHull()
        {
            IntPtr p = PInvokeOgr.OGR_G_ConvexHull(Handle);
            Geometry ret = (p == IntPtr.Zero) ? null : new Geometry(p, true, null);
            return ret;
        }

        public Geometry Buffer(double distance, int quadsecs)
        {
            IntPtr p = PInvokeOgr.OGR_G_Buffer(Handle, distance, quadsecs);
            Geometry ret = (p == IntPtr.Zero) ? null : new Geometry(p, true, null);
            return ret;
        }
        
        public Geometry Intersection(Geometry other)
        {
            IntPtr p = PInvokeOgr.OGR_G_Intersection(Handle, other.Handle);
            Geometry ret = (p == IntPtr.Zero) ? null : new Geometry(p, true, null);
            return ret;
        }

        public Geometry Union(Geometry other)
        {
            IntPtr p = PInvokeOgr.OGR_G_Union(Handle, other.Handle);
            Geometry ret = (p == IntPtr.Zero) ? null : new Geometry(p, true, null);
            return ret;
        }

        public Geometry UnionCascaded()
        {
            IntPtr p = PInvokeOgr.OGR_G_UnionCascaded(Handle);
            Geometry ret = (p == IntPtr.Zero) ? null : new Geometry(p, true, null);
            return ret;
        }

        public Geometry Difference(Geometry other)
        {
            IntPtr p = PInvokeOgr.OGR_G_Difference(Handle, other.Handle);
            Geometry ret = (p == IntPtr.Zero) ? null : new Geometry(p, true, null);
            return ret;
        }

        public Geometry SymDifference(Geometry other)
        {
            IntPtr p = PInvokeOgr.OGR_G_SymDifference(Handle, other.Handle);
            Geometry ret = (p == IntPtr.Zero) ? null : new Geometry(p, true, null);
            return ret;
        }

        public double Distance(Geometry other)
        {
            double d = PInvokeOgr.OGR_G_Distance(Handle, other.Handle);
            return d;
        }

        public void Empty()
        {
            PInvokeOgr.OGR_G_Empty(Handle);
        }

        public bool IsEmpty()
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_G_IsEmpty(Handle));
        }

        public bool IsValid()
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_G_IsValid(Handle));
        }

        public bool IsSimple()
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_G_IsSimple(Handle));
        }

        public bool IsRing()
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_G_IsRing(Handle));
        }

        public bool Intersects(Geometry other)
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_G_Intersects(Handle, other.Handle));
        }

        public bool Equals(Geometry other)
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_G_Equals(Handle, other.Handle));
        }

        public bool Disjoint(Geometry other)
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_G_Disjoint(Handle, other.Handle));
        }

        public bool Touches(Geometry other)
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_G_Touches(Handle, other.Handle));
        }

        public bool Crosses(Geometry other)
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_G_Crosses(Handle, other.Handle));
        }

        public bool Within(Geometry other)
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_G_Within(Handle, other.Handle));
        }

        public bool Contains(Geometry other)
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_G_Contains(Handle, other.Handle));
        }

        public bool Overlaps(Geometry other)
        {
            return Convert.ToBoolean(PInvokeOgr.OGR_G_Overlaps(Handle, other.Handle));
        }
        
        
        public bool TransformTo(SpatialReference reference)
        {
            var ret = PInvokeOgr.OGR_G_TransformTo(Handle, reference.Handle);
            return Errors.IsNoError(ret);
        }
        /// <summary>
        /// Apply arbitrary coordinate transformation to geometry.
        /// </summary>
        public bool Transform(CoordinateTransformation transform)
        {
            var ret = PInvokeOgr.OGR_G_Transform(Handle, transform.Handle);
            return Errors.IsNoError(ret);
        }

        /// <summary>
        /// Returns spatial reference system for geometry.
        /// </summary>
        public SpatialReference GetSpatialReference()
        {
            IntPtr p = PInvokeOgr.OGR_G_GetSpatialReference(Handle);
            return new SpatialReference(p);
        }

        /// <summary>
        /// Assign spatial reference to this object.
        /// </summary>
        public void AssignSpatialReference(SpatialReference reference)
        {
            PInvokeOgr.OGR_G_AssignSpatialReference(Handle, reference.Handle);
        }

        public void CloseRings()
        {
            PInvokeOgr.OGR_G_CloseRings(Handle);
        }

        public void FlattenTo2D()
        {
            PInvokeOgr.OGR_G_FlattenTo2D(Handle);
        }

        public void Segmentize(double dfMaxLength)
        {
            PInvokeOgr.OGR_G_Segmentize(Handle, dfMaxLength);
        }

        public Envelope GetEnvelope()
        {
            Envelope e = new Envelope();
            PInvokeOgr.OGR_G_GetEnvelope(Handle, out e);
            return e;
        }

        public Envelope3D GetEnvelope3D()
        {
            Envelope3D e = new Envelope3D();
            PInvokeOgr.OGR_G_GetEnvelope3D(Handle, out e);
            return e;
        }

        public Geometry Centroid()
        {
            Geometry g = new Geometry(wkbGeometryType.wkbPoint);
            PInvokeOgr.OGR_G_Centroid(Handle, g.Handle);
            return g;
        }

        public Geometry PointOnSurface()
        {
            IntPtr p = PInvokeOgr.OGR_G_PointOnSurface(Handle);
            Geometry ret = (p == IntPtr.Zero) ? null : new Geometry(p, true, null);
            return ret;
        }

        public int WkbSize()
        {
            return PInvokeOgr.OGR_G_WkbSize(Handle);
        }

        public int GetCoordinateDimension()
        {
            int r = PInvokeOgr.OGR_G_GetCoordinateDimension(Handle);
            return r;
        }

        public void SetCoordinateDimension(int dimension)
        {
            PInvokeOgr.OGR_G_SetCoordinateDimension(Handle, dimension);
        }

        public int GetDimension()
        {
            int ret = PInvokeOgr.OGR_G_GetDimension(Handle);
            return ret;
        }

        public byte[] ExportToWkb()
        {
            return ExportToWkb(wkbByteOrder.wkbXDR);
        }

    }
}

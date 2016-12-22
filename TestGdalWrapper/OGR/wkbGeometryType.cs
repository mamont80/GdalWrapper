using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scanex.Gdal
{
    public enum wkbGeometryType
    {
        /// <summary>
        /// unknown type, non-standard
        /// </summary>
        wkbUnknown = 0,
        /// <summary>
        /// 0-dimensional geometric object, standard WKB
        /// </summary>
        wkbPoint = 1,
        /// <summary>
        /// 1-dimensional geometric object with linear interpolation between Points, standard WKB
        /// </summary>
        wkbLineString = 2,
        /// <summary>
        /// planar 2-dimensional geometric object defined by 1 exterior boundary and 0 or more interior boundaries, standard WKB
        /// </summary>
        wkbPolygon = 3,
        /// <summary>
        /// GeometryCollection of Points, standard WKB.
        /// </summary>
        wkbMultiPoint = 4,
        /// <summary>
        /// GeometryCollection of LineStrings, standard WKB.
        /// </summary>
        wkbMultiLineString = 5,
        /// <summary>
        /// GeometryCollection of Polygons, standard WKB.
        /// </summary>
        wkbMultiPolygon = 6,
        /// <summary>
        /// geometric object that is a collection of 1 or more geometric objects, standard WKB
        /// </summary>
        wkbGeometryCollection = 7,
        /// <summary>
        /// non-standard, for pure attribute records
        /// </summary>
        wkbNone = 100,
        /// <summary>
        /// non-standard, just for createGeometry()
        /// </summary>
        wkbLinearRing = 101,
        wkbPoint25D = -2147483647,
        wkbLineString25D = -2147483646,
        wkbPolygon25D = -2147483645,
        wkbMultiPoint25D = -2147483644,
        wkbMultiLineString25D = -2147483643,
        wkbMultiPolygon25D = -2147483642,
        wkbGeometryCollection25D = -2147483641
    }
}

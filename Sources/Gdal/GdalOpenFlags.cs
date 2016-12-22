using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scanex.Gdal
{
    public enum GdalOpenDriverKind
    {
        All = 0, 
        Raster = 2, 
        Vector = 4
    }

    public enum GdalOpenAccessMode
    {
        ReadOnly = 0,
        Update = 1
    }

    public enum GdalOpenSharedMode
    {
        Shared = 0x20,
        NoShared = 0
    }

    public enum GdalOpenFlags
    {
        GDAL_OF_ALL = 0,
        GDAL_OF_RASTER = 2,
        GDAL_OF_VECTOR = 4,
        GDAL_OF_READONLY = 0,
        GDAL_OF_UPDATE = 1
    }
}

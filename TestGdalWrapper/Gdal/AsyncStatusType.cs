using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scanex.Gdal
{
    public enum AsyncStatusType
    {
        GARIO_PENDING = 0,
        GARIO_UPDATE = 1,
        GARIO_ERROR = 2,
        GARIO_COMPLETE = 3,
        GARIO_TypeCount = 4
    }
}

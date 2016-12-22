using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scanex.Gdal
{
    public enum FieldType
    {
        OFTInteger = 0,
        OFTIntegerList = 1,
        OFTReal = 2,
        OFTRealList = 3,
        OFTString = 4,
        OFTStringList = 5,
        OFTWideString = 6,
        OFTWideStringList = 7,
        OFTBinary = 8,
        OFTDate = 9,
        OFTTime = 10,
        OFTDateTime = 11,
        OFTInteger64 = 12,
        OFTInteger64List = 13
    }
}

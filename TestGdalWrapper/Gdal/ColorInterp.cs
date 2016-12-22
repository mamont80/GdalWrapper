﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scanex.Gdal
{
    public enum ColorInterp
    {
        GCI_Undefined = 0,
        GCI_GrayIndex = 1,
        GCI_PaletteIndex = 2,
        GCI_RedBand = 3,
        GCI_GreenBand = 4,
        GCI_BlueBand = 5,
        GCI_AlphaBand = 6,
        GCI_HueBand = 7,
        GCI_SaturationBand = 8,
        GCI_LightnessBand = 9,
        GCI_CyanBand = 10,
        GCI_MagentaBand = 11,
        GCI_YellowBand = 12,
        GCI_BlackBand = 13,
        GCI_YCbCr_YBand = 14,
        GCI_YCbCr_CbBand = 15,
        GCI_YCbCr_CrBand = 16,
        GCI_Max = 16
    }
}

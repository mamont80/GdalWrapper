using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Scanex.Gdal
{
    public static class PInvokeError
    {
        //const char * 	CPLGetLastErrorMsg (void)
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string CPLGetLastErrorMsg();

        //Fetch the last error number. 
        //int 	CPLGetLastErrorNo (void)
        [DllImport(PInvokeOgr.GdalDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int CPLGetLastErrorNo();

    }
}

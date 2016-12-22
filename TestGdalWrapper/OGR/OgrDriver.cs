using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scanex.Gdal;

namespace Scanex.Gdal
{
    public class OgrDriver: CustomGdalObject
    {
        protected override void DestroyNativeObj()
        {
            //PInvokeOgr.OGR_DS_Destroy(Handle);
        }

        internal OgrDriver(IntPtr cPtr, bool cMemoryOwn, object parent)
        {
            Init(cPtr, cMemoryOwn, parent);
        }
    }
}

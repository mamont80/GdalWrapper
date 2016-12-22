using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scanex.Gdal;

namespace Scanex.Gdal
{
    /// <summary>
    /// Какая-то хрень. Реализовывать думаю не надо. Есть 2 функции которые это возвращают.
    /// </summary>
    public class Field: CustomGdalObject
    {
        protected override void DestroyNativeObj()
        {
           //PInvokeOgr.OGR_F_Destroy(Handle);
        }

        internal Field(IntPtr cPtr, bool cMemoryOwn, object parent)
        {
            Init(cPtr, cMemoryOwn, parent);
        }
    }
}

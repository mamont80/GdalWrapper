using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scanex.Gdal
{
    internal static class Errors
    {
        public static void ThrowLastError()
        {
            string msg = PInvokeError.CPLGetLastErrorMsg();
            throw new Exception(msg);
        }

        public static bool IsNoError(int errCode)
        {
            return (errCode == 0);
        }

        public static void CheckError(int errCode)
        {
            if (errCode== 0) return;
            string msg = "Internal unknow error";
            switch (errCode)
            {
                case 1:
                    msg = "NOT ENOUGH DATA";
                    break;
                case 2:
                    msg = "NOT ENOUGH MEMORY";
                    break;
                case 3:
                    msg = "UNSUPPORTED GEOMETRY TYPE";
                    break;
                case 4:
                    msg = "UNSUPPORTED OPERATION";
                    break;
                case 5:
                    msg = "CORRUPT DATA";
                    break;
                case 6:
                    msg = "FAILURE";
                    break;
                case 7:
                    msg = "UNSUPPORTED SRS";
                    break;
            }
            throw new Exception(msg);
        }
    }
}

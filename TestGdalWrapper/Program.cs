using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scanex.Gdal;
using System.Runtime.InteropServices;

namespace TestGdalWrapper
{
    class Program
    {

        //проверить открытие Dataset с ошибкой и без
        //копирование фалов драйвера в UTF8
        static void Main(string[] args)
        {
            string path = @";C:\Mamont\GeoMixer\GeoMixer\Utils\UtilsExe\gdal\x86\bins";
            string oldPath = Environment.GetEnvironmentVariable("PATH");
            Environment.SetEnvironmentVariable("PATH", oldPath+path);

            string s = "POINT (30 10)";
            Geometry g = new Geometry(s);
            int code = g.ExportToWkt(out s);
            Console.WriteLine("Code: " + code.ToString() + " Result: " + s);
            
            g = new Geometry(wkbGeometryType.wkbPoint);
            g.AddPoint_2D(1, 2);
            for(int i = 0; i< 10000000;i++)
              code = g.ExportToWkt(out s);
            Console.WriteLine("Code: "+code.ToString()+" Result: "+s);
            GC.Collect();
            GC.Collect();
            GC.Collect();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
            //Marshal.PtrToStringAnsi
            
        }
    }
}

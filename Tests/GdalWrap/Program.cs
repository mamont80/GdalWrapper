using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scanex.Gdal;
using System.Diagnostics;

namespace GdalWrap
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Environment.GetEnvironmentVariable("Path");
            //path += @";D:\Gdal\release-1500-gdal-2-1-0-mapserver-7-0-1\bin";
            path += @";D:\GeoMixer_Work\Utils\gdal212\x64\bins";

            Environment.SetEnvironmentVariable("Path", path);
            Gdal.AllRegister();

            string s1 = @"D:\Слои\rasters\1020010048E0C200.jpg";
            Dataset src = Gdal.OpenEx(s1, GdalOpenDriverKind.Raster, GdalOpenAccessMode.ReadOnly);

            var driver = Gdal.GetDriverByName("GTiff");
            string OutFilename = @"c:\123456.tif";
            var outDS = driver.Create(OutFilename, 5000, 5000, 3, DataType.GDT_Byte, null);
            var wkt = src.GetProjectionRef();
            double[] transf = new double[6];
            src.GetGeoTransform(transf);
            outDS.SetProjection("GEOGCS[\"WGS 84\",DATUM[\"WGS_1984\",SPHEROID[\"WGS 84\",6378137,298.257223563,AUTHORITY[\"EPSG\",\"7030\"]],TOWGS84[0,0,0,0,0,0,0],AUTHORITY[\"EPSG\",\"6326\"]],PRIMEM[\"Greenwich\",0,AUTHORITY[\"EPSG\",\"8901\"]],UNIT[\"degree\",0.0174532925199433,AUTHORITY[\"EPSG\",\"9108\"]],AUTHORITY[\"EPSG\",\"4326\"]]");
            outDS.SetGeoTransform(transf);

            List<string> optStr = new List<string>();
            optStr.Add("-et");
            optStr.Add("100");
            //optStr.Add("-t_srs");
            //optStr.Add("+proj=utm +zone=11 +datum=WGS84");
            //optStr.Add("-to");
            //optStr.Add("DST_SRS=");
            //optStr.Add("-ts");
            //optStr.Add("500");
            //optStr.Add("500");
            //optStr.Add("-overwrite");
            WrapAppOptions opt = new WrapAppOptions(optStr.ToArray());
            //opt.SetOption("ts","500 500");
            int errCode;
            //Dataset dst2 = Gdal.Wrap(@"c:\12345.tif", new Dataset[1] { src }, opt, out errCode);
            var timer = Stopwatch.StartNew();
            Dataset dst2 = Gdal.Wrap(outDS, new Dataset[1] { src }, opt, out errCode);
            Console.WriteLine(timer.ElapsedMilliseconds);
            //dst2.Close();
            outDS.Close();
            Console.ReadKey();
        }
    }
}

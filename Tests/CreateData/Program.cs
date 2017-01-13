using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scanex.Gdal;


namespace CreateData
{
    /******************************************************************************
     * $Id: createdata.cs 27044 2014-03-16 23:41:27Z rouault $
     *
     * Name:     createdata.cs
     * Project:  GDAL CSharp Interface
     * Purpose:  A sample app to create a spatial data source and a layer.
     * Author:   Tamas Szekeres, szekerest@gmail.com
     *
     ******************************************************************************
     * Copyright (c) 2007, Tamas Szekeres
     * Copyright (c) 2009-2010, Even Rouault <even dot rouault at mines-paris dot org>
     *
     * Permission is hereby granted, free of charge, to any person obtaining a
     * copy of this software and associated documentation files (the "Software"),
     * to deal in the Software without restriction, including without limitation
     * the rights to use, copy, modify, merge, publish, distribute, sublicense,
     * and/or sell copies of the Software, and to permit persons to whom the
     * Software is furnished to do so, subject to the following conditions:
     *
     * The above copyright notice and this permission notice shall be included
     * in all copies or substantial portions of the Software.
     *
     * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
     * OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
     * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
     * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
     * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
     * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
     * DEALINGS IN THE SOFTWARE.
     *****************************************************************************/



    /**

     * <p>Title: GDAL C# createdata example.</p>
     * <p>Description: A sample app to create a spatial data source and a layer.</p>
     * @author Tamas Szekeres (szekerest@gmail.com)
     * @version 1.0
     */



    /// <summary>
    /// A C# based sample to create a layer.
    /// </summary> 

    class CreateData
    {

        public static void usage()
        {
            Console.WriteLine("usage: createdata {data source name} {layername}");
            System.Environment.Exit(-1);
        }

        public static void Main(string[] args)
        {
            string path = Environment.GetEnvironmentVariable("Path");
            //path += @";D:\Gdal\release-1500-gdal-2-1-0-mapserver-7-0-1\bin";
            path += @";D:\GeoMixer_Work\Utils\gdal212\x86\bins";
            
            Environment.SetEnvironmentVariable("Path", path);
            Gdal.AllRegister();

            string fn = @"d:\geomixer_work\UserFolder\LayerManager 85ce4ae9\world_simple\TM_WORLD_BORDERS_SIMPL-0.3.shp";
            var re = Gdal.OpenEx(fn, GdalOpenDriverKind.Vector, GdalOpenAccessMode.ReadOnly, GdalOpenSharedMode.Shared);

            args = new string[2]{ @"123.shp", "123"};
            if (args.Length != 2) usage();

            // Using early initialization of System.Console
            Console.WriteLine("");

            /* -------------------------------------------------------------------- */
            /*      Register format(s).                                             */
            /* -------------------------------------------------------------------- */

            /* -------------------------------------------------------------------- */
            /*      Get driver                                                      */
            /* -------------------------------------------------------------------- */

            Scanex.Gdal.Driver drv = Gdal.GetDriverByName("ESRI Shapefile");
            Dataset ds = drv.Create(args[0], 0, 0, 0, DataType.GDT_Unknown, null);
            //Dataset ds = Gdal.OpenEx(args[0], GdalOpenDriverKind.All, GdalOpenAccessMode.Update, GdalOpenSharedMode.NoShared);

            // TODO: drv.name is still unsafe with lazy initialization (Bug 1339)
            string DriverName = drv.LongName;
            Console.WriteLine("Using driver " + DriverName);

            /* -------------------------------------------------------------------- */
            /*      Creating the datasource                                         */
            /* -------------------------------------------------------------------- */
            
            if (ds == null)
            {
                Console.WriteLine("Can't create the datasource.");
                System.Environment.Exit(-1);
            }

            /* -------------------------------------------------------------------- */
            /*      Creating the layer                                              */
            /* -------------------------------------------------------------------- */

            Layer layer;
            
            int i;
            for (i = 0; i < ds.GetLayerCount(); i++)
            {
                layer = ds.GetLayer(i);
                if (layer != null && layer.GetLayerDefn().GetName() == args[1])
                {
                    Console.WriteLine("Layer already existed. Recreating it.\n");
                    ds.DeleteLayer(i);
                    break;
                }
            }

            layer = ds.CreateLayer(args[1], null, wkbGeometryType.wkbPoint, new string[] { });
            if (layer == null)
            {
                Console.WriteLine("Layer creation failed.");
                System.Environment.Exit(-1);
            }

            /* -------------------------------------------------------------------- */
            /*      Adding attribute fields                                         */
            /* -------------------------------------------------------------------- */

            FieldDefn fdefn = new FieldDefn("Name", FieldType.OFTString);

            fdefn.SetWidth(32);
            
            if (!layer.CreateField(fdefn))
            {
                Console.WriteLine("Creating Name field failed.");
                System.Environment.Exit(-1);
            }

            fdefn = new FieldDefn("IntField", FieldType.OFTInteger);
            if (!layer.CreateField(fdefn))
            {
                Console.WriteLine("Creating IntField field failed.");
                System.Environment.Exit(-1);
            }

            fdefn = new FieldDefn("DbleField", FieldType.OFTReal);
            if (!layer.CreateField(fdefn))
            {
                Console.WriteLine("Creating DbleField field failed.");
                System.Environment.Exit(-1);
            }

            fdefn = new FieldDefn("DateField", FieldType.OFTDate);
            if (!layer.CreateField(fdefn))
            {
                Console.WriteLine("Creating DateField field failed.");
                System.Environment.Exit(-1);
            }

            /* -------------------------------------------------------------------- */
            /*      Adding features                                                 */
            /* -------------------------------------------------------------------- */

            Feature feature = new Feature(layer.GetLayerDefn());
            feature.SetField("Name", "value");
            feature.SetField("IntField", (int)123);
            feature.SetField("DbleField", (double)12.345);
            feature.SetField("DateField", DateTime.Now);

            Geometry geom = Geometry.CreateFromWkt("POINT(47.0 19.2)");

            if (!feature.SetGeometry(geom))
            {
                Console.WriteLine("Failed add geometry to the feature");
                System.Environment.Exit(-1);
            }

            if (!layer.CreateFeature(feature))
            {
                Console.WriteLine("Failed to create feature in shapefile");
                System.Environment.Exit(-1);
            }

            ReportLayer(layer);
        }

        public static void ReportLayer(Layer layer)
        {
            FeatureDefn def = layer.GetLayerDefn();
            Console.WriteLine("Layer name: " + def.GetName());
            Console.WriteLine("Feature Count: " + layer.GetFeatureCount(true));
            Envelope ext = new Envelope();
            layer.GetExtent(out ext, true);
            Console.WriteLine("Extent: " + ext.MinX + "," + ext.MaxX + "," +
                ext.MinY + "," + ext.MaxY);

            /* -------------------------------------------------------------------- */
            /*      Reading the spatial reference                                   */
            /* -------------------------------------------------------------------- */
            SpatialReference sr = layer.GetSpatialRef();
            string srs_wkt;
            if (sr != null)
            {
                sr.ExportToPrettyWkt(out srs_wkt, true);
            }
            else
                srs_wkt = "(unknown)";


            Console.WriteLine("Layer SRS WKT: " + srs_wkt);

            /* -------------------------------------------------------------------- */
            /*      Reading the fields                                              */
            /* -------------------------------------------------------------------- */
            Console.WriteLine("Field definition:");
            for (int iAttr = 0; iAttr < def.GetFieldCount(); iAttr++)
            {
                FieldDefn fdef = def.GetFieldDefn(iAttr);

                Console.WriteLine(fdef.GetName() + ": " +
                    fdef.GetFieldTypeName(fdef.GetFieldType()) + " (" +
                    fdef.GetWidth() + "." +
                    fdef.GetPrecision() + ")");
            }

            /* -------------------------------------------------------------------- */
            /*      Reading the shapes                                              */
            /* -------------------------------------------------------------------- */
            Console.WriteLine("");
            Feature feat;
            while ((feat = layer.GetNextFeature()) != null)
            {
                ReportFeature(feat, def);
                feat.Dispose();
            }
        }

        public static void ReportFeature(Feature feat, FeatureDefn def)
        {
            Console.WriteLine("Feature(" + def.GetName() + "): " + feat.GetFID());
            for (int iField = 0; iField < feat.GetFieldCount(); iField++)
            {
                FieldDefn fdef = def.GetFieldDefn(iField);

                Console.Write(fdef.GetName() + " (" +
                    fdef.GetFieldTypeName(fdef.GetFieldType()) + ") = ");

                if (feat.IsFieldSet(iField))
                    Console.WriteLine(feat.GetFieldAsString(iField));
                else
                    Console.WriteLine("(null)");

            }

            if (feat.GetStyleString() != null)
                Console.WriteLine("  Style = " + feat.GetStyleString());

            Geometry geom = feat.GetGeometryRef();
            if (geom != null)
                Console.WriteLine("  " + geom.GetGeometryName() +
                    "(" + geom.GetGeometryType() + ")");

            Envelope env = geom.GetEnvelope();
            Console.WriteLine("   ENVELOPE: " + env.MinX + "," + env.MaxX + "," +
                env.MinY + "," + env.MaxY);

            string geom_wkt;
            geom.ExportToWkt(out geom_wkt);
            Console.WriteLine("  " + geom_wkt);

            Console.WriteLine("");
        }
    }
}

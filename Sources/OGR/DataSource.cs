using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scanex.Gdal;

namespace Scanex.Gdal
{
    public class DataSource : CustomGdalObject
    {
        protected override void DestroyNativeObj()
        {
            PInvokeOgr.OGR_DS_Destroy(Handle);
        }

        internal DataSource(IntPtr cPtr, bool cMemoryOwn, object parent)
        {
            Init(cPtr, cMemoryOwn, parent);
        }

        /// <summary>
        /// Execute an SQL statement against the data store.
        /// The result of an SQL query is either NULL for statements that are in error, or that have no results set, or an OGRLayer handle representing a results set from the query. Note that this OGRLayer is in addition to the layers in the data store and must be destroyed with OGR_DS_ReleaseResultSet() before the data source is closed (destroyed).
        /// For more information on the SQL dialect supported internally by OGR review the OGR SQL document. Some drivers (i.e. Oracle and PostGIS) pass the SQL directly through to the underlying RDBMS.
        /// Starting with OGR 1.10, the SQLITE dialect can also be used.
        /// </summary>
        /// <param name="sqlcommand">the SQL statement to execute.</param>
        /// <param name="spatialFilter">handle to a geometry which represents a spatial filter. Can be NULL.</param>
        /// <param name="dialect">allows control of the statement dialect. If set to NULL, the OGR SQL engine will be used, except for RDBMS drivers that will use their dedicated SQL engine, unless OGRSQL is explicitly passed as the dialect. Starting with OGR 1.10, the SQLITE dialect can also be used.</param>
        /// <returns>an handle to a OGRLayer containing the results of the query. Deallocate with OGR_DS_ReleaseResultSet().</returns>
        public Layer ExecuteSQL(string sqlcommand, Geometry spatialFilter, string dialect)
        {
            using (var sql = new MarshalUtils.StringExport(sqlcommand))
            using (var d = new MarshalUtils.StringExport(dialect))
            {
                IntPtr g = IntPtr.Zero;
                if (spatialFilter != null) g = spatialFilter.Handle;
                IntPtr p = PInvokeOgr.OGR_DS_ExecuteSQL(Handle, sql.Pointer, g, d.Pointer);
                if (p == null) return null;
                return new Layer(p, false, this);
            }
        }

        /// <summary>
        /// Returns the driver that the dataset was opened with.
        /// </summary>
        /// <returns>NULL if driver info is not available, or pointer to a driver owned by the OGRSFDriverManager.</returns>
        public OgrDriver GetDriver()
        {
            IntPtr p = PInvokeOgr.OGR_DS_GetDriver(Handle);
            if (p == IntPtr.Zero) return null;
            return new OgrDriver(p, false, this);
        }

    }
}

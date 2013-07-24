using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SI2_TP.Models.Transform;

namespace SI2_TP.Models
{
    public class EmpresaDao
    {
        public string GetHtmlTablesFromEmpresas()
        {
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("GetAllEmpresasInstadSp", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var da = new SqlDataAdapter { SelectCommand = cmd };
                var ds = new DataSet();
                da.Fill(ds);
                var s = ds.Tables[0].Rows[0][0].ToString();

                return TransformXsl.ToHtml(s, "toTransform.xsl");
            }
        }
    }
}
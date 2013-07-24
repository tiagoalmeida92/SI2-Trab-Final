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
        public string GetHtmlTablesFromEmpresas(string path)
        {
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("GetAllEmpresasInsta", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                var xmlreader = cmd.ExecuteXmlReader();

                //var da = new SqlDataAdapter { SelectCommand = cmd };
                //var ds = new DataSet();
                //da.Fill(ds);
                //var header = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
                //var s = ds.Tables[0].Rows[0][0].ToString();
                
                return TransformXsl.ToHtml(xmlreader, path);
            }
        }
    }
}
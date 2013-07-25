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
                return TransformXsl.ToHtml(xmlreader, path);
            }
        }
    }
}
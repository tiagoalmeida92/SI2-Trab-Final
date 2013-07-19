using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SI2_TP.Models
{
    public class OcorrenciasDao
    {
        public IEnumerable<Ocorrencias> GetAll(int idFunc)
        {
            var list = new LinkedList<Ocorrencias>();
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using(var con = new SqlConnection(conString))
            using(var cmd = new SqlCommand("GetOcurrenciasFromFunc",con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idFunc", idFunc);
                SqlDataAdapter adapter = new SqlDataAdapter {SelectCommand = cmd};
                var ds = new DataSet();
                adapter.Fill(ds);
                DataRowCollection drc = ds.Tables[0].Rows;
                foreach (DataRow row in drc)
                {
                    list.AddLast(new Ocorrencias(Convert.ToInt32(row["id"]), Convert.ToDateTime(row["dataHoraAct"]),
                                                 Convert.ToDateTime(row["dataHoraEnt"]),
                                                 (Estado) Convert.ToInt32(row["estado"]),
                                                 (Tipo) Convert.ToInt32(row["tipo"]), Convert.ToInt32(row["secInst"]),
                                                 Convert.ToInt32(row["secPiso"])
                                                 , Convert.ToString(row["secZona"])));
                }
            }
            return list;
        }

        public void updateResolucaoOcorrencia(int id)
        {
            var list = new LinkedList<Ocorrencias>();
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("GetOcurrenciasFromFunc", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
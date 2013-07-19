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
                    DateTime t1 = Convert.ToDateTime(row["dataHoraEnt"].ToString());
                    DateTime t2 = Convert.ToDateTime(row["dataHoraAct"].ToString());
                    list.AddLast(new Ocorrencias
                            {
                                id = Convert.ToInt32(row["id"]),
                                dataHoraEnt = t1,
                                dataHoraAct =t2 ,
                                tipo = (Tipo)Convert.ToInt32(row["tipo"]),
                                estado = (Estado)Convert.ToInt32(row["estado"]),
                                secInst = Convert.ToInt32(row["secInst"]),
                                secPiso = Convert.ToInt32(row["secPiso"]),
                                secZona = Convert.ToInt32(row["secZona"])
                            });
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
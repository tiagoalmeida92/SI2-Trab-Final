using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SI2_TP.Models
{
    public class OcorrenciaDao
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
                var adapter = new SqlDataAdapter {SelectCommand = cmd};
                var ds = new DataSet();
                adapter.Fill(ds);
                con.Close();
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

        //b. Permitir registar e modificar toda a informação relacionada com uma ocorrência, incluindo mudanças de estado e 
        //respectiva gestão dos trabalhos nas áreas de intervenção afectas.

        public void Insert(Ocorrencias ocorr)
        {
            var list = new LinkedList<Ocorrencias>();
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("reportaOcorrencia", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", ocorr.id);
                cmd.Parameters.AddWithValue("@tipo", ocorr.tipo);
                cmd.Parameters.AddWithValue("@estado", ocorr.estado);
                cmd.Parameters.AddWithValue("@secInst", ocorr.secInst);
                cmd.Parameters.AddWithValue("@secPiso", ocorr.secPiso);
                cmd.Parameters.AddWithValue("@secZona", ocorr.secZona);
                cmd.ExecuteNonQuery();
            }
        }

        public void Cancelar(int idOcorrencia)
        {
            var list = new LinkedList<Ocorrencias>();
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("CancelarOcurrencia", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idOcurr", idOcorrencia);

                cmd.ExecuteNonQuery();
            }
        }

        public void SetEmResolucao(int idOcorr)
        {
            var list = new LinkedList<Ocorrencias>();
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("iniciaResolucaoOcorrencia", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", idOcorr);

                cmd.ExecuteNonQuery();
            }
        }
        
        //d.  Permitir listar todas as ocorrências ainda não concluídas (que estejam em estado inicial ou em resolução) que 
        //tenham dado entrada depois de uma determinada data. 

        public IEnumerable<Ocorrencias> GetAllNaoConcluidas(DateTime data)
        {
            var list = new LinkedList<Ocorrencias>();
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("GetOcurrenciasNaoConcluidas", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@data", data);
                var adapter = new SqlDataAdapter { SelectCommand = cmd };
                var ds = new DataSet();
                adapter.Fill(ds);
                con.Close();
                DataRowCollection drc = ds.Tables[0].Rows;
                foreach (DataRow row in drc)
                {
                    list.AddLast(new Ocorrencias(Convert.ToInt32(row["id"]), Convert.ToDateTime(row["dataHoraAct"]),
                                                 Convert.ToDateTime(row["dataHoraEnt"]),
                                                 (Estado)Convert.ToInt32(row["estado"]),
                                                 (Tipo)Convert.ToInt32(row["tipo"]), Convert.ToInt32(row["secInst"]),
                                                 Convert.ToInt32(row["secPiso"])
                                                 , Convert.ToString(row["secZona"])));
                }
            }
            return list;
        } 



    }
}
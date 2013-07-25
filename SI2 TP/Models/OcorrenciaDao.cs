using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SI2_TP.Models.OcorrenciaDataSetTableAdapters;

namespace SI2_TP.Models
{
    public class OcorrenciaDao
    {
        public IEnumerable<Ocorrencia> GetAll(int idFunc)
        {
            var list = new LinkedList<Ocorrencia>();
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("GetOcurrenciasFromFunc", con))
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
                    list.AddLast(ConvertOcorrencia(row));
                }
            }
            return list;
        }

        public IEnumerable<Ocorrencia> GetAll()
        {
            var list = new LinkedList<Ocorrencia>();
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("SELECT * FROM Ocorrencia", con))
            {
                var adapter = new SqlDataAdapter {SelectCommand = cmd};
                var ds = new DataSet();
                adapter.Fill(ds);
                DataRowCollection drc = ds.Tables[0].Rows;
                foreach (DataRow row in drc)
                {
                    list.AddLast(ConvertOcorrencia(row));
                }
            }
            return list;
        }


        public Ocorrencia GetById(int idOcorrencia)
        {
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("GetOcorrenciaById", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idOcorrencia", idOcorrencia);
                var adapter = new SqlDataAdapter {SelectCommand = cmd};
                var ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0].Rows.Count == 1 ? ConvertOcorrencia(ds.Tables[0].Rows[0]) : null;
            }
        }

        private static Ocorrencia ConvertOcorrencia(DataRow row)
        {
            return new Ocorrencia
                       {
                           id = Convert.ToInt32(row["id"]),
                           dataHoraEnt = Convert.ToDateTime(row["dataHoraEnt"]),
                           dataHoraAct = Convert.ToDateTime(row["dataHoraAct"]),
                           tipo = (Tipo) Convert.ToInt32(row["tipo"]),
                           estado = (Estado) Convert.ToInt32(row["estado"]),
                           secInst = Convert.ToInt32(row["secInst"]),
                           secPiso = Convert.ToInt32(row["secPiso"]),
                           secZona = Convert.ToString(row["secZona"])
                       };
        }

        //b. Permitir registar e modificar toda a informação relacionada com uma ocorrência, incluindo mudanças de estado e 
        //respectiva gestão dos trabalhos nas áreas de intervenção afectas.

        public void Insert(Ocorrencia ocorr)
        {
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("reportaOcorrencia", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", ocorr.id);
                cmd.Parameters.AddWithValue("@tipo", ocorr.tipo);
                cmd.Parameters.AddWithValue("@secInst", ocorr.secInst);
                cmd.Parameters.AddWithValue("@secPiso", ocorr.secPiso);
                cmd.Parameters.AddWithValue("@secZona", ocorr.secZona);
                cmd.ExecuteNonQuery();
            }
        }

        public void SetCancelar(int idOcorrencia)
        {
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
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("iniciaResolucaoOcorrencia", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", idOcorr);

                cmd.ExecuteNonQuery();
            }
        }

        public void SetConcluido(int idOcorr)
        {
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("UpdateEstadoOcorrenciaConcluido", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idOcorr", idOcorr);

                cmd.ExecuteNonQuery();
            }
        }

        public void SetRecusado(int idOcorr)
        {
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("UpdateEstadoOcorrenciaRecusado", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idOcorr", idOcorr);

                cmd.ExecuteNonQuery();
            }
        }

        public void SetEmProcessamento(int idOcorr)
        {
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("UpdateEstadoOcorrenciaEmProcessamento", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idOcorr", idOcorr);

                cmd.ExecuteNonQuery();
            }
        }

        //d.  Permitir listar todas as ocorrências ainda não concluídas (que estejam em estado inicial ou em resolução) que 
        //tenham dado entrada depois de uma determinada data. 

        public IEnumerable<Ocorrencia> GetAllNaoConcluidas(DateTime data)
        {
            var list = new LinkedList<Ocorrencia>();
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("GetOcurrenciasNaoConcluidas", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@data", data);
                var adapter = new SqlDataAdapter {SelectCommand = cmd};
                var ds = new DataSet();
                adapter.Fill(ds);
                con.Close();
                DataRowCollection drc = ds.Tables[0].Rows;
                foreach (DataRow row in drc)
                {
                    list.AddLast(ConvertOcorrencia(row));
                }
            }
            return list;
        }

        // só o admin -> adicionar trabalho numa dada area de intervencao e ocorrencia

        public void InsertTrabalhoOnAreaInterv(int idAInterv, int idOcorr, string desc)
        {
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("AdminInsertTrabalho", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idAInter", idAInterv);
                cmd.Parameters.AddWithValue("@idOcorr", idOcorr);
                cmd.Parameters.AddWithValue("@descTrabalho", desc);
                cmd.ExecuteNonQuery();
            }
        }


        public IEnumerable<Trabalho> GetTrabalhos(int idOcorr)
        {
            var list = new List<Trabalho>();
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("getTrabalhosDaOcorrencia", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idOcorr", idOcorr);
                var adapter = new SqlDataAdapter {SelectCommand = cmd};
                var ds = new DataSet();
                adapter.Fill(ds);
                DataRowCollection drc = ds.Tables[0].Rows;
                foreach (DataRow row in drc)
                {
                    list.Add(ConvertTrabalho(row));
                }
            }
            return list;
        }

        private Trabalho ConvertTrabalho(DataRow row)
        {
            return new Trabalho
                       {
                           AreaIntervencao = Convert.ToInt32(row[1]),
                           Desc = Convert.ToString(row[2]),
                           Concluido = Convert.ToBoolean(row[3]),
                           Coordenador = Convert.ToInt32(row[4])
                       };
        }


        public void InsertAfecto(int idOcorr, int areaInterv, int idFunc)
        {
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("AdminInsertTrabalho", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@areainterv", areaInterv);
                cmd.Parameters.AddWithValue("@idOcorr", idOcorr);
                cmd.Parameters.AddWithValue("@idFunc", idFunc);
                cmd.ExecuteNonQuery();
            }
        }

        public string GetByIdXml(int idOcorr)
        {
            var ds = new OcorrenciaDataSet();
            var da = new OcorrenciaTableAdapter();
            da.Fill(ds.Ocorrencia);
            return ds.GetXml();
        }
    }
}
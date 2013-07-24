using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SI2_TP.Models
{
    public class FuncionarioDao
    {
        private const string GetByIdSp = "FuncionarioGetById";

        public Funcionario GetById(int cod)
        {
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand(GetByIdSp, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@num", cod);
                var da = new SqlDataAdapter {SelectCommand = cmd};
                var ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0].Rows.Count > 0 ? ConvertFuncionario(ds.Tables[0].Rows[0]) : null;
            }
        }


        //só coordenador -> insere em afecto um funcionario (na view há uma lista de funcionario habilitados)
        public void InsertFuncAfecto(int idOcorr,int idAInterv, int idFunc)
        {
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand("adicionaFuncOcor", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ocorrencia", idOcorr);
                cmd.Parameters.AddWithValue("@areainterv", idAInterv);
                cmd.Parameters.AddWithValue("@func", idFunc);
                cmd.ExecuteNonQuery();
            }
        }

        //mostra lista de todos os funcionarios habilitados a essa area de intervencao
        //public IEnumerable<Funcionario> GetAllFuncDisponiveis(int idAInterv)
        //{
        //    var list = new LinkedList<Funcionario>();
        //    var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
        //    using (var con = new SqlConnection(conString))
        //    using (var cmd = new SqlCommand("GetOcurrenciasFromFunc", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@areainterv", idAInterv);
        //        var adapter = new SqlDataAdapter { SelectCommand = cmd };
        //        var ds = new DataSet();
        //        adapter.Fill(ds);
        //        con.Close();
        //        DataRowCollection drc = ds.Tables[0].Rows;
        //        foreach (DataRow row in drc)
        //        {
        //            list.AddLast(ConvertFuncionario(row));
        //        }
        //    }
        //    return list;
        //}

        public IEnumerable<Funcionario> GetAvailableWorkers(int areaInterv)
        {
            var list = new List<Funcionario>();
            var conString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            using(var con = new SqlConnection(conString))
            using(var cmd = new SqlCommand("getAllFuncDisponiveis",con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@areainterv", areaInterv);
                var adapter = new SqlDataAdapter {SelectCommand = cmd};
                var ds = new DataSet();
                adapter.Fill(ds);
                DataRowCollection drc = ds.Tables[0].Rows;
                foreach (DataRow row in drc)
                {
                    list.Add(ConvertFuncionario2(row));
                }
            }
            return list;
        }

        private static Funcionario ConvertFuncionario(DataRow row)
        {
            return new Funcionario
            {
                Num = Convert.ToInt32(row[0]),
                Name = Convert.ToString(row[1]),
                BornDate = Convert.ToDateTime(row[2]),
                Estado = Convert.ToString(row[3]),
                Admin = Convert.ToBoolean(row[4]),
                Coordenador = Convert.ToBoolean(row[5])
            };
        }

        private static Funcionario ConvertFuncionario2(DataRow row)
        {
            return new Funcionario
            {
                Num = Convert.ToInt32(row[0]),
                Name = Convert.ToString(row[1]),
                Estado = Convert.ToString(row[2]),
                BornDate = Convert.ToDateTime(row[3]),
            };
        }
    }
}
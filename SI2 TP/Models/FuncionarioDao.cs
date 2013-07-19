using System;
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
                return ds.Tables[0].Rows.Count > 0 ? CriarFuncionario(ds.Tables[0].Rows[0]) : null;
            }
        }

        private static Funcionario CriarFuncionario(DataRow row)
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
    }
}
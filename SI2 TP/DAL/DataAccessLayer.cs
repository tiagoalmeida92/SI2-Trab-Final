using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SI2_TP.DAL
{
    public class DataAccessLayer
    {
        private SqlConnection _con;
        private SqlCommand _com;
        private SqlDataAdapter _adpter;
        public DataAccessLayer()
        {
            _con = new SqlConnection();
            _con.Open();
        }


    }
}
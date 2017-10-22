using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace QuincaillerieApp
{
   public class dataBase
    {
        //connection a la base
        public SqlConnection connectiondata()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=COMPAQ-PC\SQLEXPRESS;Initial Catalog=application;Integrated Security=True");
            return connection;
        }

        //connecion pour la page User d admin et password
        public SqlDataAdapter select(SqlConnection connection, string text1, string text2)
        {
            dataBase data = new dataBase();
            connection = data.connectiondata();
            connection.Open();
            SqlDataAdapter query = new SqlDataAdapter("Select * from utilisateur where login='" + text1 + "' and password='" + text2 + "'", connection);
            return query;
        }


        

    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LinqTransportes.DB
{
    class Database
    {
        private static string strCnn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Statics.Conexion.Ubicacion + ";Integrated Security=True";
        private static string usuario = "";
        private static string clave = "";

        private Database() { }

        public static SqlConnection GetConnection()
        {
            SqlConnection cnn = new SqlConnection(strCnn);

            try
            {
                System.Diagnostics.Debug.WriteLine(Statics.Texto.StrConexionAbrir);
                cnn.Open();
                return cnn;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(Statics.Texto.StrConexionError + ": " + ex.ToString());
                return null;
            }
        }
    }
}
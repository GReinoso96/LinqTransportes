using LinqTransportes.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LinqTransportes.DAO
{
    public static class TrabajadorDAO
    {
        private static SqlConnection con;

        private static bool Conexion()
        {
            Console.WriteLine(Statics.Texto.StrConexionAbrir);
            con = DB.Database.GetConnection();

            if (con != null)
                Console.WriteLine(Statics.Texto.StrConexionEstablecida);
            else
                throw new Exception(Statics.Texto.StrConexionError);
            return true;
        }

        private static void Dispose()
        {
            con.Dispose();
            con.Close();
            Console.WriteLine(Statics.Texto.StrConexionCerrar);
        }

        public static bool Insert(Trabajador item)
        {
            bool succ = false;
            Conexion();
            using(SqlCommand cmd = new SqlCommand("INSERT INTO Trabajadores (Nombres, APaterno, AMaterno, Rut, Telefono, Email, Direccion, FechaCreacion, Puesto, Contrasena) VALUES (@Nombres, @APaterno, @AMaterno, @Rut, @Telefono, @Email, @Direccion, @Fecha, @Puesto, @Contrasena)", con))
            {
                System.Diagnostics.Debug.WriteLine("Entro?");
                cmd.Parameters.AddWithValue("@Nombres", item.Nombres);
                cmd.Parameters.AddWithValue("@APaterno", item.APaterno);
                cmd.Parameters.AddWithValue("@AMaterno", item.AMaterno);
                cmd.Parameters.AddWithValue("@Rut", item.Rut);
                cmd.Parameters.AddWithValue("@Telefono", item.Telefono);
                cmd.Parameters.AddWithValue("@Email", item.Email);
                cmd.Parameters.AddWithValue("@Direccion", item.Direccion);
                cmd.Parameters.AddWithValue("@Fecha", item.Fecha);
                cmd.Parameters.AddWithValue("@Puesto", item.Puesto.Id);
                cmd.Parameters.AddWithValue("@Contrasena", "-");
                try
                {
                    System.Diagnostics.Debug.WriteLine("Aqui");
                    cmd.ExecuteNonQuery();
                    succ = true;
                    System.Diagnostics.Debug.WriteLine(Statics.Texto.StrSQLInsertar);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Aca");
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
            Dispose();
            return succ;
        }

        public static bool Update(Trabajador item)
        {
            bool succ = false;
            Conexion();
            using (SqlCommand cmd = new SqlCommand("UPDATE Trabajadores SET Nombres = @Nombres, APaterno = @APaterno, AMaterno = @AMaterno, Rut = @Rut, Telefono = @Telefono, Email = @Email, Direccion = @Direccion, Puesto = @Puesto WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", item.ID);
                cmd.Parameters.AddWithValue("@Nombres", item.Nombres);
                cmd.Parameters.AddWithValue("@APaterno", item.APaterno);
                cmd.Parameters.AddWithValue("@AMaterno", item.AMaterno);
                cmd.Parameters.AddWithValue("@Rut", item.Rut);
                cmd.Parameters.AddWithValue("@Telefono", item.Telefono);
                cmd.Parameters.AddWithValue("@Email", item.Email);
                cmd.Parameters.AddWithValue("@Direccion", item.Direccion);
                cmd.Parameters.AddWithValue("@Fecha", item.Fecha);
                cmd.Parameters.AddWithValue("@Puesto", item.Puesto.Id);
                try
                {
                    cmd.ExecuteNonQuery();
                    succ = true;
                    System.Diagnostics.Debug.WriteLine(Statics.Texto.StrSQLActualizar);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            Dispose();
            return succ;
        }

        public static bool Delete(int id)
        {
            bool succ = false;
            Conexion();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Trabajadores WHERE Id = @id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                try
                {
                    cmd.ExecuteNonQuery();
                    succ = true;
                    System.Diagnostics.Debug.WriteLine(Statics.Texto.StrSQLActualizar);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            Dispose();
            return succ;
        }

        public static int GetNextIdentity()
        {
            int val = 0;
            Conexion();
            using (SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('Trabajadores')+1", con))
            {
                val = Convert.ToInt32(cmd.ExecuteScalar());
            }
            Dispose();
            return val;
        }

        public static void GetData()
        {
            Conexion();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Puestos", con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Statics.Datos.Puestos.Clear();
                    while (reader.Read())
                    {
                        Statics.Datos.Puestos.Add(reader.GetInt32(0), new Puesto { Id = reader.GetInt32(0), Nombre = reader.GetString(1) });
                    }
                }
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Trabajadores", con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Statics.Datos.Trabajadores.Clear();
                    while (reader.Read())
                    {
                        Statics.Datos.Trabajadores.Add(reader.GetInt32(0), new Trabajador
                        {
                            ID = reader.GetInt32(0),
                            Nombres = reader.GetString(1),
                            APaterno = reader.GetString(2),
                            AMaterno = reader.GetString(3),
                            Rut = reader.GetString(4),
                            Telefono = reader.GetString(5),
                            Email = reader.GetString(6),
                            Direccion = reader.GetString(7),
                            Fecha = reader.GetDateTime(9),
                            Puesto = Statics.Datos.Puestos[reader.GetInt32(10)]
                        });
                    }
                }
            }

            Dispose();
        }
    }
}
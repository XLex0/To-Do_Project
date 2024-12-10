using Npgsql;
using System;

namespace Singleton_BaseDatos
{
    public class Conexion
    {
        private readonly NpgsqlConnection _conn = new NpgsqlConnection();
        private readonly string servidor;
        private readonly string bd;
        private readonly string user;
        private readonly string password;
        private readonly string puerto;
        private readonly string linea;

        private static readonly Conexion instanciaUnica = new Conexion();

        private Conexion()
        {
            servidor = Settings1.Default.servidor;
            bd = Settings1.Default.baseDatos;
            user = Settings1.Default.username;
            password = Settings1.Default.password;
            puerto = Settings1.Default.puerto;

            linea = "server=" + servidor + "; port=" + puerto +
                    "; user id=" + user + "; password=" + password +
                    "; database=" + bd + ";";
        }

        public static Conexion ObtenerConexion()
        {
            return instanciaUnica;
        }

        public NpgsqlConnection EstablecerConexion()
        {
            try
            {
                if (_conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn.ConnectionString = linea;
                    _conn.Open();
                    Console.WriteLine("Se conectó correctamente a la base de datos.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No se pudo conectar a la base de datos. " + e.Message);
            }
            return _conn;
        }

        public void CerrarConexion()
        {
            try
            {
                if (_conn.State == System.Data.ConnectionState.Open)
                {
                    _conn.Close();
                    Console.WriteLine("La conexión a la base de datos se cerró correctamente.");
                }
                else
                {
                    Console.WriteLine("Ya se cerró la conexión antes");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No se pudo cerrar la conexión a la base de datos. " + e.Message);
            }
        }
    }
}

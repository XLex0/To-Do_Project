using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Singleton_BaseDatos;

namespace ConsoleApp1
{
    internal class Gerente
    {

        public static void AbrirConexionGerente()
        {

            Conexion conexionGerente = Conexion.ObtenerConexion();
            Console.WriteLine("Conexion desde gerente abierta");

            conexionGerente.EstablecerConexion();

        }

        public static void CerrarConexionGerente()
        {

            Conexion conexionGerente = Conexion.ObtenerConexion();
            Console.WriteLine("Conexion desde gerente cerrada");

            conexionGerente.CerrarConexion();

        }

    }
}

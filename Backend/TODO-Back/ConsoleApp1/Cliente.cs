using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Singleton_BaseDatos;

namespace ConsoleApp1
{
    internal class Cliente
    {

        public static void AbrirConexionCliente()
        {

            Conexion conexionCliente = Conexion.ObtenerConexion();
            Console.WriteLine("Conexion desde cliente abierta");

            conexionCliente.EstablecerConexion();

        }

        public static void CerrarConexionCliente()
        {

            Conexion conexionCliente = Conexion.ObtenerConexion();
            Console.WriteLine("Conexion desde cliente cerrada");

            conexionCliente.CerrarConexion();

        }

    }
}

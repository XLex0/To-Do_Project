using System;
using Singleton_BaseDatos;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Cliente.AbrirConexionCliente();
            Cliente.CerrarConexionCliente();


            Gerente.AbrirConexionGerente();
            Gerente.CerrarConexionGerente();



            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}

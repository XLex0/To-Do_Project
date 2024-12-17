using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocioPro
{
    public class Usuario
    {
        private Inventario inventario;
        private bool log;

        public Usuario()
        {
            this.log = false;
            this.inventario = null;
        }

        public bool login (string user, string password)
        {
            try
            {
                this.inventario = new Inventario(user, password);
                this.log = true;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al hacer login {ex}");
            }
            return false;
        }


        public void logout() {
            if (log)
            {
                this.log = false;
                this.inventario = null;
            }
        }

        public Inventario getInventario() { return this.inventario; }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
        

        private Usuario(CapaAccesoBD.Models.Usuario user)
        { 
            this.inventario = new Inventario(user);
        }

        public static object register(string username, string password, string email)
        {
            try
            {
                var db = Context.GetInstance().GetDbContext();

                var user = new CapaAccesoBD.Models.Usuario
                {
                    Username = username,
                    Password = password,
                    Email = email
                };


                db.Usuarios.Add(user);
                db.SaveChanges();

                return new { sucess=true, message = $"User: {user.Username} Creado correctamente" };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = $"Error creando user: {ex}",

                };
            }
        }
        public static int login(string username, string pass)
        {
            try
            {
                var db = Context.GetInstance().GetDbContext();

                var user = db.Usuarios.FirstOrDefault(t => t.Username == username);

                if (user != null)
                {
                    if (user.Password == pass)
                    {
                        return user.Iduser;
                      
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error de login {e}");
            }


            return -1;
        }
            public static Usuario cargar (int id)
        {
            try
            {
                var db = Context.GetInstance().GetDbContext();

                var x = db.Usuarios.FirstOrDefault(t => t.Iduser == id);

             
                var y = new Usuario(x);
                return y;
                    
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al recuperar User {ex}");
            }
            return null;
        }




        public Inventario getInventario() { return this.inventario; }
        
    }
}

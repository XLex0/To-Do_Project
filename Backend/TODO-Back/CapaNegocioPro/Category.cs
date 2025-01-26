using CapaAccesoBD.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocioPro
{
    public class Category:IProducto
    {
        private int idUser {  get; set; }
        private int idLabel { get; set; }
        private string name { get; set; }
        private string description { get; set; }

        public Category(int idLabel, string name, int idUser, string? description)
        {
            this.idUser = idUser;
            this.idLabel = idLabel;
            this.name = name;
            this.description = description ?? ""; 
        }



        public Category(string name, string? description = null)
        {
            this.name = name;
            this.description = description ?? "";
        }



        public bool CargarProducto( int idUser)
        {
            try
            {
                var dbContext = Context.GetInstance().GetDbContext();

                // Comprobar si ya existe una categoría con el mismo nombre para el mismo usuario
                var existingCategory = dbContext.Categorylabels
               .FirstOrDefault(c => c.Name == this.name && c.Iduser == idUser);

                if (existingCategory != null)
                {
                    Console.WriteLine("La categoría ya existe.");
                    return false ;
                }

                // Crear la nueva categoría
                var nuevaCategory = new CapaAccesoBD.Models.Categorylabel
                {
                    Name = this.name,
                    Description = this.description,
                    Iduser = idUser,
                };

                // Guardar la nueva categoría en la base de datos
                dbContext.Categorylabels.Add(nuevaCategory);
                dbContext.SaveChanges();
                 
                return true ;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al crear la categoría: {e.Message}");
            }
            return false ;
        }



        public bool EliminarProducto()
        {
            try
            {

                var dbContext = Context.GetInstance().GetDbContext();


                var category = dbContext.Categorylabels.FirstOrDefault(t => t.Idlabel == this.idLabel);



                var asignaciones = dbContext.Asignations.Where(a => a.Idlabel == this.idLabel).ToList();

                if (asignaciones.Any())
                {
                    dbContext.Asignations.RemoveRange(asignaciones);
                }
                if (category != null)
                {
                    dbContext.Categorylabels.Remove(category);
                }

                dbContext.SaveChanges();

                Console.WriteLine($"Categoria con ID {this.idLabel} eliminada exitosamente, junto con sus asignaciones.");

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al eliminar la Categoria: {e}");
                return false;
            }
        }

        // se encarga de cargar la lista de las categorias del usuario
     

        public object RetornarJson()
        {
            var categoryData = new
            {
                idlabel = this.idLabel,
                name = this .name,
                description = this.description,

            };
            return categoryData;
        }
        public void setDescription(string description) {this.description = description;}
        public string getName() {return this.name;}
        public int getIdLabel() { return this.idLabel; }
    }

}

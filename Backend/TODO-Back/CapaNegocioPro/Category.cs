using CapaAccesoBD.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocioPro
{
    public class Category
    {
        private int idLabel { get; set; }
        private string name { get; set; }
        private string description { get; set; }

        public Category(int idLabel, string name)
        {
            this.idLabel = idLabel;
            this.name = name;
            this.description = "";
        }

        /**
        * Input(descripcionTarea, prioridadTarea, idUser, finTarea?)
        * -- Se crea Tarea con los datos 
        * Output: boolean Exito
        */
        public static bool CreateCategory(string name, int idUser,  string description)
        {
            try

            {
                var dbContext = Context.GetInstance().GetDbContext();

                var existingCategory = dbContext.Categorylabels
                                .FirstOrDefault(c => c.Name == name && c.Iduser == idUser);

                if (existingCategory != null)
                {
                    Console.WriteLine("La categoría ya existe.");
                    return false;
                }


                var nuevaCategory = new CapaAccesoBD.Models.Categorylabel
                {
                    Name = name,
                    Description = string.IsNullOrEmpty(description) ? "" : description,
                    Iduser = idUser,
                };

                

                dbContext.Categorylabels.Add(nuevaCategory);
                dbContext.SaveChanges();
                Console.WriteLine("Añadida Categoria");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al crear la categoria: {e}");
                return false;
            }
        }

        public static bool RemoveCategory(int id)
        {
            try
            {

                var dbContext = Context.GetInstance().GetDbContext();


                var category = dbContext.Categorylabels.FirstOrDefault(t => t.Idlabel == id);



                var asignaciones = dbContext.Asignations.Where(a => a.Idlabel == id).ToList();

                if (asignaciones.Any())
                {
                    dbContext.Asignations.RemoveRange(asignaciones);
                }
                if (category != null)
                {
                    dbContext.Categorylabels.Remove(category);
                }

                dbContext.SaveChanges();

                Console.WriteLine($"Categoria con ID {id} eliminada exitosamente, junto con sus asignaciones.");

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al eliminar la Categoria: {e}");
                return false;
            }
        }



        public object getCategoryJson()
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
        public int getIdLabel() {return this.idLabel;}
    }

}

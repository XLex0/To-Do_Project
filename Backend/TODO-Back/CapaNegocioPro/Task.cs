using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Task = System.Threading.Tasks.Task;
using CapaAccesoBD.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;


namespace CapaNegocioPro
{
    public class Task
    {
        private int idtask {  get; set; }
        private string description { get; set; }
        private string priority { get; set; }
        private string startDate { get; set; }
        private string endDate { get; set; }
        private List <string> category { get; set; }


        /**
         * Input(idTarea)
         * -- Se borra las asignaciones de Tarea y esta misma
         * Output: boolean Exito
         */ 
        public Task(int id, string des, string star)
        {
            this.idtask = id;
            this.description = des;
            this.priority = "Medium";
            this.startDate = star;
            this.category = new List<string> {"main"};
        }


        /**
         * Input(idTarea)
         * -- Se borra las asignaciones de Tarea y esta misma
         * Output: boolean Exito
         */
        public bool RemoveTask(int id)
        {
            try
            {

                var dbContext = Context.GetInstance().GetDbContext();


                var tarea = dbContext.Tasks.FirstOrDefault(t => t.Idtask == id);

               

                var asignaciones = dbContext.Asignations.Where(a => a.Idtask == id).ToList();

                if (asignaciones.Any())
                {
                    dbContext.Asignations.RemoveRange(asignaciones);
                }
                if (tarea != null)
                {
                    dbContext.Tasks.Remove(tarea);
                }

                dbContext.SaveChanges();

                Console.WriteLine($"Tarea con ID {id} eliminada exitosamente, junto con sus asignaciones.");

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al eliminar la tarea: {e.Message}");
                return false;
            }
        }

        /**
         * Input(descripcionTarea, prioridadTarea, idUser, finTarea?)
         * -- Se crea Tarea con los datos 
         * Output: boolean Exito
         */
        public static bool CreateTask(string description, string priority,int idUser, string? endDate=null)
        {
            try
            {
                var dbContext = Context.GetInstance().GetDbContext();

                var nuevaTarea = new CapaAccesoBD.Models.Task
                {
                    Description = description,
                    Priority = priority,
                    Creationdate = DateOnly.FromDateTime(DateTime.Now), 
                    Iduser = idUser,
                    Enddate = string.IsNullOrEmpty(endDate) ? null : DateOnly.Parse(endDate)
                };


                dbContext.Tasks.Add(nuevaTarea);
                dbContext.SaveChanges();
                Console.WriteLine("Añadida Tarea");
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error al crear la tarea: {e.Message}");
                return false;
            }
        }


  

        public void Imprimir()
        {
            Console.WriteLine($"{idtask}, {description}");
        }

        /**
         * Setters de varios campos
         */
        public void setEndDate(string endDate) { this.endDate = endDate; }
        public void setPriority(string priority) { this.priority = priority; }
        public void setCategory(string category) { this.category.Add(category); }
        public int getIdTask() { return this.idtask; }
    }
}

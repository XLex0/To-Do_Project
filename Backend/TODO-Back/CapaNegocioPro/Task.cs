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
using System.Text.Json;


namespace CapaNegocioPro
{
    public class Task
    {
        private int idtask { get; set; }
        private string description { get; set; }
        private string priority { get; set; }
        private string startDate { get; set; }
        private string endDate { get; set; }
        private bool state { get; set; }
        private List<string> category { get; set; }

       


        /**
         * Input(idTarea)
         * -- Se borra las asignaciones de Tarea y esta misma
         * Output: boolean Exito
         */
        public Task(int id, string des, string star, bool estado)
        {
            this.idtask = id;
            this.description = des;
            this.priority = "Medium";
            this.startDate = star;
            this.category = new List<string> { "main" };
            this.state = estado;

        }


        /**
         * Input(idTarea)
         * -- Se borra las asignaciones de Tarea y esta misma
         * Output: boolean Exito
         */
        public bool RemoveTask()
        {
            try
            {

                var dbContext = Context.GetInstance().GetDbContext();


                var tarea = dbContext.Tasks.FirstOrDefault(t => t.Idtask == this.idtask);


                var asignaciones = dbContext.Asignations.Where(a => a.Idtask == this.idtask).ToList();

                if (asignaciones.Any())
                {
                    dbContext.Asignations.RemoveRange(asignaciones);
                }
                if (tarea != null)
                {
                    dbContext.Tasks.Remove(tarea);
                }

                dbContext.SaveChanges();

                Console.WriteLine($"Tarea con ID {this.idtask} eliminada exitosamente, junto con sus asignaciones.");

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
        public static bool CreateTask(string description, string priority, int idUser, string endDate)
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
                    Enddate = string.IsNullOrEmpty(endDate) ? null : DateOnly.Parse(endDate),
                    Estado = true
                };


                dbContext.Tasks.Add(nuevaTarea);
                dbContext.SaveChanges();
                Console.WriteLine("Añadida Tarea");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al crear la tarea: {e}");
                return false;
            }
        }




        /**
         * Setters de varios campos
         */
        public void setEndDate(string endDate)
        {
            try
            {
     
                var dbContext = Context.GetInstance().GetDbContext();

                // Buscar la tarea en la base de datos usando su ID
                var tarea = dbContext.Tasks.FirstOrDefault(t => t.Idtask == this.idtask);
                if (tarea == null)
                {
                    return;
                }

                tarea.Enddate = string.IsNullOrEmpty(endDate) ? null : DateOnly.Parse(endDate);

                dbContext.SaveChanges();

                this.endDate = endDate;

                Console.WriteLine("Fecha de finalización actualizada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la fecha de finalización: {ex.Message}");
            }
        }

        public void setPriority(string priority) { this.priority = priority; }
        public void setCategory(string category) { this.category.Add(category); }


        public int getIdTask() { return this.idtask; }
        public string getPriority() { return this.priority; }
        public List<string> getCategory() { return this.category; }
        public bool getState(){return this.state;}


        public object getTaskJson()
        {
            var taskData = new
            {
                idtask = this.idtask,
                description = this.description,
                priority = this.priority,
                startDate = this.startDate,
                endDate = string.IsNullOrEmpty(this.endDate) ? null : this.endDate,
                category = this.category,
                estado = this.state
            };

            return taskData;
        }
    }
}


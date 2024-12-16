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


        public Task(int id, string des, string star)
        {
            this.idtask = id;
            this.description = des;
            this.priority = "Medium";
            this.startDate = star;
            this.category = new List<string> {"main"};
        }

      
        
        public bool eliminarTarea(int id)
        {
            var tarea = Context.GetInstance().GetDbContext().Tasks.Find(id);

            if (tarea == null)
            {
                return false;
            }
         
                Context.GetInstance().GetDbContext().Tasks.Remove(tarea);
                Context.GetInstance().GetDbContext().SaveChanges();
                return true;
        }


        public void setEndDate(string endDate) { this.endDate = endDate; }
        public void setPriority(string priority) { this.priority = priority; }
        public void setCategory(string category) { this.category = category; }
    }
}

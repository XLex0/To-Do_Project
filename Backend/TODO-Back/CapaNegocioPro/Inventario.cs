using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Task = System.Threading.Tasks.Task;
using CapaAccesoBD.Models;
using Microsoft.EntityFrameworkCore;

namespace CapaNegocioPro
{
    public class Inventario
    {
        List<Task> inventarioTareas;
        List<Category> inventarioCategorias;



        public Inventario(string username, string password)
        {
           var user = Context.GetInstance().GetDbContext().Usuarios.FirstOrDefault(x => x.Username == "username");
 
            if ( user!=null && password == user.Password)
            {

                inventarioTareas = new List<Task>();

                var tasks = Context.GetInstance().GetDbContext().Tasks.Where(x => x.Iduser == user.Iduser).ToList();

                foreach (var task in tasks) {

                    var i = new Task(task.Idtask, task.Description, task.Creationdate.ToString());

                    if (task.Enddate != null)
                    {
                        i.setEndDate(task.Enddate.ToString());
                    }

                    if (task.Priority != null)
                    {
                        i.setPriority(task.Priority);
                    }
                    if (task.Asignations != null && task.Asignations.Count() > 0)
                    {
                        foreach(var asign in task.Asignations)
                        {
                            var category= Context.GetInstance().GetDbContext().Categorylabels.Where(x => x.Idlabel == asign.Idlabel).ToList();
                            

                        }
                    }

                inventarioTareas.Append(i);
                }

            }
        }

    }
}

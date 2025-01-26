using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CapaNegocioPro
{

    public class TaskCreador : ProductoCreador
    {
        public TaskCreador() : base() { }

        // Sobrescribe el método de la clase base
        public override List<IProducto> ObtenerProductos(CapaAccesoBD.Models.Usuario user)
        {
            var productosToReturn = new List<IProducto>();
            var dbacces = Context.GetInstance().GetDbContext();

            try
            {
                var tasks = dbacces.Tasks
                    .Where(x => x.Iduser == user.Iduser)
                    .ToList();

                productosToReturn = tasks.Select(task =>
                {
                    var taskObj = new Task(task.Idtask, task.Description, task.Creationdate.ToString(), task.Estado, task.Priority);

                    // Si existe una fecha de finalización, la asignamos
                    if (task.Enddate != null)
                    {
                        taskObj.setEndDate(task.Enddate.ToString());
                    }

                    // Obtener categorías asociadas a la tarea
                    var categories = dbacces.Asignations
                                            .Where(asig => asig.Idtask == task.Idtask)
                                            .SelectMany(asig => dbacces.Categorylabels.Where(c => c.Idlabel == asig.Idlabel))
                                            .Select(c => c.Name)
                                            .ToList();

                    // Asignamos todas las categorías a la tarea
                    categories.ForEach(categoryName => taskObj.setCategory(categoryName));

                    return taskObj as IProducto; // Retornamos como Producto
                }).ToList();

                return productosToReturn;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Cargando Tareas {e}");
            }

            return new List<IProducto>();
        }

        public override IProducto crearProducto(string description, string? priority=null, string? endDate = null)
        {

            if (priority == null)
            {
                priority = "Low";
            }
            IProducto producto = new Task(description, priority, endDate);
            return producto;
        }
    }

    public class CategoryCreador : ProductoCreador
    {
        public CategoryCreador() : base() { }

        // Sobrescribe el método de la clase base
        public override List<IProducto> ObtenerProductos(CapaAccesoBD.Models.Usuario user)
        {
            var productosToReturn = new List<IProducto>();

            try
            {
                var categories = Context.GetInstance().GetDbContext()
                                        .Categorylabels
                                        .Where(x => x.Iduser == user.Iduser)
                                        .ToList();

                foreach (var cat in categories)
                {
                    var category = new Category(cat.Idlabel, cat.Name, cat.Iduser, cat.Description);
                    productosToReturn.Add(category as IProducto);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Cargando Categorías : {e}");
            }

            return productosToReturn;
        }

        public override IProducto crearProducto(string name, string? description, string? a)
        {

            if (description == null)
            {
                description = "";
            }
            IProducto producto = new Category(name, description);
            return producto;
        }

    }
}

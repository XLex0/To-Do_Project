using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocioPro
{
  

        public interface IProducto
        {

            bool CargarProducto(int id);


            bool EliminarProducto();


            object RetornarJson();
        }

    
}

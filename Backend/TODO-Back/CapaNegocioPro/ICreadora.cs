namespace CapaNegocioPro
{
    public abstract class ProductoCreador
    {
        protected List<IProducto> productos;

        protected ProductoCreador()
        {
            productos = new List<IProducto>();
        }

        // Marca este método como virtual para permitir que las clases hijas lo sobrescriban
        public virtual List<IProducto> ObtenerProductos(CapaAccesoBD.Models.Usuario user)
        {
            return productos;
        }

        public virtual IProducto crearProducto(string par1, string? par2, string? par3=null)
        {
            return null;
        }
    }

}

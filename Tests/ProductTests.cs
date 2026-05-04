using Microsoft.VisualStudio.TestTools.UnitTesting;
using Negocio;

namespace Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void ListarProductos_DebeContenerProductoExistente()
        {
            var productos = new NProd().Listar();

            Assert.IsNotNull(productos);
            Assert.IsTrue(productos.Exists(p => p.IdProd == 5 && p.Nombre == "Flexible Mesa"));
        }
    }
}

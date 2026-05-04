using Entidad;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Negocio;

namespace Tests
{
    [TestClass]
    public class UserLoginTests
    {
        [TestMethod]
        public void VerificarUsuario_ConCredencialesValidas_RetornaTrue()
        {
            var respuesta = NUsua.Verificar(new EUsua
            {
                Nombre = "AlexisH",
                Pass = "master001"
            });

            Assert.IsTrue(respuesta.estado, respuesta.valor);
        }

        [TestMethod]
        public void VerificarUsuario_ConCredencialesInvalidas_RetornaFalse()
        {
            var respuesta = NUsua.Verificar(new EUsua
            {
                Nombre = "AlexisH",
                Pass = "incorrecto"
            });

            Assert.IsFalse(respuesta.estado);
        }
    }
}

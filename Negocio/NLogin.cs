using Datos;

// Espacio de nombres establecido para regular el comportamiento transaccional del sistema a nivel Negocio.
namespace Negocio
{
    // Clase manejadora para la delegación de autenticaciones o Inicios de Sesión (Login).
    public class NLogin
    {
        // Instancia un componente directo hacia la base de datos (DLogin), aunque se nota el uso Singleton posterior.
        private DLogin Datos = new DLogin();

        // Método lógico responsable de tomar Nombre de cuenta y Contraseña de la capa de interfaz y pasarlo a verificación.
        public int IngSig(string Nombre, string Pass)
        {
            // Aquí llamas al método correspondiente en la capa de Datos (DLogin)
            // Llama y retorna consecuentemente un entero como llave (IdUsu) que representa el resultado exitoso del inicio de sesión (0 = Fallo).
            return DLogin.Instancia.IngSig(Nombre, Pass);
        }
    }
}

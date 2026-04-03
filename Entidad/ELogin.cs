using System;

// Espacio de nombres para las entidades del sistema que transportan los datos de las capas.
namespace Entidad
{
    // Clase pública ELogin que maneja los datos de autenticación del usuario.
    public class ELogin
    {
        // Variable interna que almacena el número de identificador del usuario.
        public int ValIdUsu;

        // Variable interna que guarda el nombre de cuenta del usuario.
        public String ValNombre;

        // Variable interna que provee almacenamiento para la contraseña del usuario.
        public String ValPass;

        // Propiedad que permite obtener o reasignar el identificador del usuario autenticado.
        public int IdUsu
        {
            get { return ValIdUsu; }
            set { ValIdUsu = value; }
        }

        // Propiedad que da acceso o altera el nombre utilizado para el inicio de sesión.
        public String Nombre
        {
            get { return ValNombre; }
            set { ValNombre = value; }
        }

        // Propiedad designada para transferir o leer la contraseña asociada a la cuenta temporalmente en memoria.
        public String Pass
        {
            get { return ValPass; }
            set { ValPass = value; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Espacio de nombres para las entidades del sistema que transportan los datos de las capas.
namespace Entidad
{
    // Clase pública EUsua que estandariza las propiedades persistentes manejadas de un Usuario.
    public class EUsua
    {
        // Variable interna para almacenar el número de identificador único del usuario.
        public int ValIdUsu;

        // Variable interna que guarda en texto el nombre del usuario o su apodo (login handle).
        public String ValNombre;

        // Variable para mantener resguardada la contraseña del usuario en memoria antes del tránsito.
        public String ValPass;

        // Propiedad que permite obtener o reemplazar de forma controlada el Id del usuario asigando.
        public int IdUsu
        {
            get { return ValIdUsu; }
            set { ValIdUsu = value; }
        }

        // Propiedad que sirve para suministrar u obtener el nombre de este usuario.
        public String Nombre
        {
            get { return ValNombre; }
            set { ValNombre = value; }
        }

        // Propiedad destinada a la lectura o modificación abstracta de la clave de ingreso de un usuario específico.
        public String Pass
        {
            get { return ValPass; }
            set { ValPass = value; }
        }
    }
}
using System;

// Espacio de nombres que agrupa las clases que representan entidades del sistema.
namespace Entidad
{
    // Clase que representa la entidad Comuna (Localidad Comuna).
    public class ELocCom
    {
        // Variable que almacena el identificador único de la comuna.
        public int ValIdCom;

        // Variable que almacena el nombre de la comuna.
        public String ValNombre;

        // Variable que almacena el identificador de la provincia a la que pertenece.
        public int ValIdPro;

        // Objeto que representa la provincia a la que pertenece la comuna.
        public ELocPro ValPro;

        // Variable que almacena el identificador de la región a la que pertenece la comuna.
        public int ValIdReg;

        // Objeto que representa la región a la que pertenece la comuna.
        public ELocReg ValReg;

        // Propiedad para obtener o establecer el identificador de la comuna.
        public int IdCom
        {
            get { return ValIdCom; }
            set { ValIdCom = value; }
        }

        // Propiedad para obtener o establecer el nombre de la comuna.
        public String Nombre
        {
            get { return ValNombre; }
            set { ValNombre = value; }
        }

        // Propiedad para obtener o establecer el identificador de la provincia.
        public int IdPro
        {
            get { return ValIdPro; }
            set { ValIdPro = value; }
        }

        // Propiedad para obtener o establecer el objeto provincia relacionado.
        public ELocPro Pro
        {
            get { return ValPro; }
            set { ValPro = value; }
        }

        // Propiedad para obtener o establecer el identificador de la región.
        public int IdReg
        {
            get { return ValIdReg; }
            set { ValIdReg = value; }
        }

        // Propiedad para obtener o establecer el objeto región relacionado.
        public ELocReg Reg
        {
            get { return ValReg; }
            set { ValReg = value; }
        }
    }
}
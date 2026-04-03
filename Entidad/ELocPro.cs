using System;

// Espacio de nombres que agrupa las clases de entidad del dominio.
namespace Entidad
{
    // Clase pública ELocPro que representa la entidad Provincia.
    public class ELocPro
    {
        // Variable para almacenar el ID interno de la provincia.
        public int ValIdPro;

        // Variable para guardar el nombre de la provincia.
        public String ValNombre;

        // Variable para registrar el ID de la región administrativa a la que pertenece dictada por la llave foránea.
        public int ValIdReg;

        // Propiedad de tipo objeto para encapsular la información completa de la región aliada.
        public ELocReg ValReg;

        // Propiedad que permite acceder y modificar el identificador de la provincia.
        public int IdPro
        {
            get { return ValIdPro; }
            set { ValIdPro = value; }
        }

        // Propiedad que faculta leer o escribir el nombre designado a la provincia.
        public String Nombre
        {
            get { return ValNombre; }
            set { ValNombre = value; }
        }

        // Propiedad mediadora para establecer y solicitar el ID de región enlazado.
        public int IdReg
        {
            get { return ValIdReg; }
            set { ValIdReg = value; }
        }

        // Propiedad de tipo ELocReg empleada para la carga del nivel relacional superior (Región).
        public ELocReg Reg
        {
            get { return ValReg; }
            set { ValReg = value; }
        }
    }
}
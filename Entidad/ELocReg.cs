using System;

// Espacio de nombres que agrupa las clases correspondientes a las entidades de negocio.
namespace Entidad
{
    // Clase pública ELocReg que representa la entidad Región dentro del sistema.
    public class ELocReg
    {
        // Variable interna para almacenar el identificador único de la región.
        public int ValIdReg;

        // Variable interna para almacenar el nombre descriptivo de la región.
        public String ValNombre;

        // Propiedad pública que expone y permite modificar el identificador de la región.
        public int IdReg
        {
            get { return ValIdReg; }
            set { ValIdReg = value; }
        }

        // Propiedad pública que expone y permite modificar el nombre de la región.
        public String Nombre
        {
            get { return ValNombre; }
            set { ValNombre = value; }
        }

    }
}

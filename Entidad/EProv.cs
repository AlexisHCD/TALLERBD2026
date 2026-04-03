using System;

// Espacio de nombres que agrupa las clases correspondientes a las entidades de negocio.
namespace Entidad
{
    // Clase pública EProv que representa la entidad Proveedor en el sistema.
    public class EProv
    {
        // Variable interna que almacena el identificador único del proveedor.
        public int ValIdProv;

        // Variable interna para guardar el nombre o razón social del proveedor.
        public String ValNombre;

        // Variable que guarda el rol único tributario (RUT) asociado al proveedor.
        public String ValRut;

        // Variable que almacena el ID foráneo correspondiente a la Región.
        public int ValIdReg;

        // Objeto que encapsula los datos completos de la Región asociada al proveedor.
        public ELocReg ValReg;

        // Variable que almacena el ID foráneo de la Provincia.
        public int ValIdPro;

        // Objeto que encapsula de forma anidada la Provincia a la que corresponde.
        public ELocPro ValPro;

        // Variable que almacena el ID foráneo ligado a la Comuna.
        public int ValIdCom;

        // Objeto para manejar y transportar todos los detalles de la Comuna referenciada.
        public ELocCom ValCom;

        // Almacena la dirección física descriptiva (calle, número, etc.) del proveedor.
        public String ValDireccion;

        // Almacena el número telefónico de contacto del proveedor.
        public String ValTel;

        // Almacena el correo electrónico operativo del proveedor.
        public String ValEmail;

        // Almacena el rubro principal o giro comercial del proveedor.
        public String ValGiro;

        // Variable para almacenar cualquier descripción o dato adicional referente al proveedor.
        public String ValDescr;

        // Propiedad encargada de leer o modificar el ID primario del proveedor.
        public int IdProv
        {
            get { return ValIdProv; }
            set { ValIdProv = value; }
        }

        // Propiedad que permite acceder u otorgar el nombre del proveedor.
        public String Nombre
        {
            get { return ValNombre; }
            set { ValNombre = value; }
        }

        // Propiedad que define o recupera el RUT procesado en este objeto.
        public String Rut
        {
            get { return ValRut; }
            set { ValRut = value; }
        }

        // Propiedad que manipula el identificador (Id) de la región configurada para el proveedor.
        public int IdReg
        {
            get { return ValIdReg; }
            set { ValIdReg = value; }
        }

        // Propiedad que expone la instancia completa representativa de la Región (ELocReg).
        public ELocReg Reg
        {
            get { return ValReg; }
            set { ValReg = value; }
        }

        // Propiedad para extraer o actualizar el identificador interno de la provincia.
        public int IdPro
        {
            get { return ValIdPro; }
            set { ValIdPro = value; }
        }

        // Encapsula el acceso y sustitución de un objeto modelado de Provincia (ELocPro).
        public ELocPro Pro
        {
            get { return ValPro; }
            set { ValPro = value; }
        }

        // Propiedad que lee o establece directamente en memoria el número ID de la comuna asignada.
        public int IdCom
        {
            get { return ValIdCom; }
            set { ValIdCom = value; }
        }

        // Da acceso a un contexto más rico encapsulado a través de la entidad Comuna (ELocCom).
        public ELocCom Com
        {
            get { return ValCom; }
            set { ValCom = value; }
        }

        // Provee un medio de obtención o fijado de la cadena de texto con la dirección física.
        public String Direccion
        {
            get { return ValDireccion; }
            set { ValDireccion = value; }
        }

        // Control de acceso para el teléfono fijo/móvil asignado al proveedor.
        public String Tel
        {
            get { return ValTel; }
            set { ValTel = value; }
        }

        // Control de acceso al correo electrónico, permite asignar el string de contacto.
        public String Email
        {
            get { return ValEmail; }
            set { ValEmail = value; }
        }

        // Tránsito directo de Get y Set referente al contexto corporativo o Giro de sus operaciones.
        public String Giro
        {
            get { return ValGiro; }
            set { ValGiro = value; }
        }

        // Permite el manejo de un texto opcional extra para describir mayores peculiaridades de este proveedor.
        public String Descr
        {
            get { return ValDescr; }
            set { ValDescr = value; }
        }
    }
}
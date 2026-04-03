using System;

// Espacio de nombres que encapsula a todos los objetos del dominio o entidades del sistema.
namespace Entidad
{
    // Clase pública ECliente que representa a la tabla o entidad abstracta de Cliente con todas sus propiedades relacionales.
    public class ECliente
    {
        // Almacena el identificador único numérico del cliente en la BD.
        public int ValIdP_Cli;

        // Almacena el nombre descriptivo del cliente.
        public String ValNombre;

        // Almacena el Registro Único Tributario (RUT) que identifica al cliente legalmente.
        public String ValRut;

        // Llave foránea del componente de Región territorial.
        public int ValIdReg;

        // Contenedor objeto completo que permite extender o consultar datos adjuntos de la Región a la que pertenece.
        public ELocReg ValReg;

        // Llave foránea que señala el componente de Provincia dentro de Región.
        public int ValIdPro;

        // Entidad secundaria anidada para almacenar los datos detallados de dicha Provincia (composicion/relación).
        public ELocPro ValPro;

        // Llave foránea para denotar la Comuna en la escala territorial donde reside.
        public int ValIdCom;

        // Entidad secundaria anidada detallando los datos de la comuna del cliente.
        public ELocCom ValCom;

        // Dirección literal a texto describiendo ubicación física de residencia/oficina.
        public String ValDireccion;

        // Teléfono o número de contacto personal/corporativo en modo de texto.
        public String ValTel;

        // Casilla de correo electrónico en formato String convencional.
        public String ValEmail;

        // Descripción de su giro comercial a nivel texto.
        public String ValGiro;

        // Propiedad encapsulada para el campo 'ValIdP_Cli' con funciones de lectura (get) y escritura (set).
        public int IdP_Cli
        {
            get { return ValIdP_Cli; }
            set { ValIdP_Cli = value; }
        }

        // Propiedad vinculante para obtener o imponer el nombre sobre 'ValNombre'.
        public String Nombre
        {
            get { return ValNombre; }
            set { ValNombre = value; }
        }

        // Manipula 'ValRut' y facilita su acceso usando convención de nombramiento en Alta con 'Rut'.
        public String Rut
        {
            get { return ValRut; }
            set { ValRut = value; }
        }

        // Recupera o fija de memoria temporal el Id foráneo para su región en la memoria local ('ValIdReg').
        public int IdReg
        {
            get { return ValIdReg; }
            set { ValIdReg = value; }
        }

        // Lectura y escritura sobre el objeto anidado Región ('ValReg') entero perteneciente al cliente.
        public ELocReg Reg
        {
            get { return ValReg; }
            set { ValReg = value; }
        }

        // Propiedad que permite transferir o consultar la clave 'ValIdPro' de Provincia.
        public int IdPro
        {
            get { return ValIdPro; }
            set { ValIdPro = value; }
        }

        // Permite encapsular el objeto detallado de la provincia asigando al Cliente ('ValPro').
        public ELocPro Pro
        {
            get { return ValPro; }
            set { ValPro = value; }
        }

        // Obtiene o define la id de la comuna foránea para el cliente ('ValIdCom').
        public int IdCom
        {
            get { return ValIdCom; }
            set { ValIdCom = value; }
        }

        // Tránsito directo de Get y Set al objeto en código sobre Comuna ('ValCom') del individuo.
        public ELocCom Com
        {
            get { return ValCom; }
            set { ValCom = value; }
        }

        // Configura el nombre base textual de la ubicación vía propiedad direccion ('ValDireccion').
        public String Direccion
        {
            get { return ValDireccion; }
            set { ValDireccion = value; }
        }

        // Método de acceso simplificado y modificación sobre el teléfono asignado al Cliente ('ValTel').
        public String Tel
        {
            get { return ValTel; }
            set { ValTel = value; }
        }

        // Correo personal resguardado mediante esta propiedad ('ValEmail').
        public String Email
        {
            get { return ValEmail; }
            set { ValEmail = value; }
        }

        // Lectura y escritura sobre el nivel de orientación corporativa (Giro) de un cliente ('ValGiro').
        public String Giro
        {
            get { return ValGiro; }
            set { ValGiro = value; }
        }
    }
}
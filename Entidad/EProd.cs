using System;

// Espacio de nombres que agrupa las clases correspondientes a las entidades de negocio.
namespace Entidad
{
    // Clase pública EProd que representa la entidad Producto.
    public class EProd
    {
        public int ValIdProd;
        public String ValNombre;
        public String ValFInc;
        public String ValCInc;
        public String ValCAct;
        public String ValCArr;
        public String ValTAct;
        public String ValVArr;

        public int IdProd
        {
            get { return ValIdProd; }
            set { ValIdProd = value; }
        }

        public String Nombre
        {
            get { return ValNombre; }
            set { ValNombre = value; }
        }

        public String FInc
        {
            get { return ValFInc; }
            set { ValFInc = value; }
        }

        public String CInc
        {
            get { return ValCInc; }
            set { ValCInc = value; }
        }

        public String CAct
        {
            get { return ValCAct; }
            set { ValCAct = value; }
        }

        public String CArr
        {
            get { return ValCArr; }
            set { ValCArr = value; }
        }

        public String TAct
        {
            get { return ValTAct; }
            set { ValTAct = value; }
        }

        public String VArr
        {
            get { return ValVArr; }
            set { ValVArr = value; }
        }
    }
}

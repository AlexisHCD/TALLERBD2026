using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

// Espacio de nombres principal para la capa de interface gráfica (Windows Forms).
namespace Presentacion
{
    // Clase estática interna principal (Program) definida automáticamente por .NET.
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread] // Indica que el modelo de enhebrado COM para la aplicación es Single-Threaded Apartment (reducido a un solo hilo).
        static void Main()
        {
            // Habilita los estilos visuales del sistema operativo actual a los controles.
            Application.EnableVisualStyles();
            // Establece compatibilidad básica usando formato gráfico GDI predeterminado.
            Application.SetCompatibleTextRenderingDefault(false);
            // Punto de arranque para la aplicación con su formulario base principal (Menu).
            Application.Run(new Menu());
        }
    }
}

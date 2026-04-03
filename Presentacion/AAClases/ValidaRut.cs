using System;

// Espacio de nombres para las clases de utilidad y ayudantes (AAClases) en la capa de Presentación.
namespace Presentacion.AAClases
{
    // Clase pública ValidaRut encargada de dar formato y validar la correctitud del RUT chileno.
    public class ValidaRut
    {
        // Método que recibe un RUT en formato libre y le aplica el formato estándar con puntos y guion.
        public string formatoRut(string rut) // Formato del rut
        {
            // Contador para saber cuándo insertar un punto (cada 3 caracteres).
            int cont = 0;
            // Variable que acumulará el RUT formateado de derecha a izquierda.
            string format;
            // Elimina puntos preexistentes para limpiar la cadena.
            rut = rut.Replace(".", "");
            // Elimina guiones preexistentes.
            rut = rut.Replace("-", "");
            // Inicia el formato con el guion adherido al último caracter (dígito verificador).
            format = "-" + rut.Substring(rut.Length - 1);
            // Recorre el resto de los caracteres (la parte numérica del RUT) desde el final hacia el principio.
            for (int i = rut.Length - 2; i >= 0; i--)
            {
                // Concatena el dígito actual por delante de la cadena ya formateada.
                format = rut.Substring(i, 1) + format;
                cont++; // Acumula cuántos dígitos se han leído.
                // Si ya van 3 dígitos y aún no llegamos al principio, se inserta un punto separador de miles.
                if (cont == 3 && i != 0)
                {
                    format = "." + format;
                    cont = 0; // Reinicia el contador de bloques de 3.
                }
            }
            // Devuelve el RUT ya formateado (ej. "12.345.678-9").
            return format;
        }

        // Método booleano que verifica matemáticamente si un RUT dado es válido de acuerdo a su dígito verificador.
        public bool validarRut(string rut) // Validar Rut
        {
            // Inicializa la variable de validación en falso asumiendo que el RUT es inválido por defecto.
            bool validacion = false;
            try
            {
                // Transforma cualquier letra a mayúscula (como la 'K').
                rut = rut.ToUpper();
                // Limpia el RUT quitándole posibles puntos.
                rut = rut.Replace(".", "");
                // Limpia el RUT quitándole posibles guiones.
                rut = rut.Replace("-", "");
                // Extrae y convierte la sección numérica bruta del RUT a entero (todo menos el último caracter).
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));
                // Extrae el dígito verificador provisto como un char (último caracter).
                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));
                // Algoritmo mod 11 para calcular el dígito verificador esperado.
                int m = 0, s = 1;
                // Bucle donde se multiplica cada dígito por la serie de factores 2,3,4,5,6,7 repetitivamente.
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                // Si el cálculo algorítmico resulta en un carácter idéntico al DV provisto.
                if (dv == (char)(s != 0 ? s + 47 : 75)) // (char)75 es 'K' en ASCII
                {
                    // La validación fue exitosa, el rut es real/válido de formato.
                    validacion = true;
                }
            }
            catch (Exception)
            {
                // En caso de que se intente parsear algo inválido (ej: letras donde van números), se atrapa silenciosamente y retorna false.
            }
            // Retorna validación final (true si pasa algoritmo, false si falla cualquier control o es incorrecto).
            return validacion;
        }
    }
}
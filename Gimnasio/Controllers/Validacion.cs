using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gimnasio.Controllers
{
    class Validacion
    {
        public bool validarNombre(string nombre)
        {
            //debe comenzar con mayuscula y seguir con minimo dos caracteres mas en minusculas
            Regex pattern = new Regex(@"^[A-Z]{1}[a-z]{2,12}$");

            if (pattern.IsMatch(nombre)){

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validarDNI(long dni)
        {
            //verificamos que el numero tenga entre 7 y 8 caracteres
            if (dni >= 999999 && dni < 100000000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validarGenero(string genero)
        {
            Regex pattern = new Regex("^(masculino|femenino)$");

            if (pattern.IsMatch(genero))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validarTipoActividad(string tipo)
        {
            Regex pattern = new Regex(@"^[A-Za-z\s]+$");

            if (pattern.IsMatch(tipo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validarTitulo(string titulo)
        {
            Regex pattern = new Regex(@"^([A-Z]{1}[a-z]{8,40})|([a-z]{8,40})$");

            if (pattern.IsMatch(titulo))
            {
                return true;
            }else
            {
                return false;
            }

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Controllers
{
    class Entre_Act_Controller
    {

        //metodo para retornar los datos del entrenador segun la activada que dicta siempre y cuando la activdad este activa
        public object ObtenerEntrenadores(int id_actividad)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                return db.Entrenador_Actividad.Where(x => x.actividad_id == id_actividad).Select(x => new
                {
                    nombre_entrenador = x.Entrenador.nombre,
                    apellido_entrenador = x.Entrenador.apellido,
                    dni_entrenador = x.Entrenador.dni,
                    nombre_actividad = x.Actividad.nombre        

                }).ToList();
            }
        }

    }
}

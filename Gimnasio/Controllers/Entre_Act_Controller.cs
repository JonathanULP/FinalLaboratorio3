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
                return db.Entrenador_Actividad.Where(x => x.actividad_id == id_actividad && x.Entrenador.borrado_logico == false).Select(x => new
                {
                    entrenador_id = x.entrenador_id,
                    nombre_entrenador = x.Entrenador.nombre,
                    apellido_entrenador = x.Entrenador.apellido,
                    dni_entrenador = x.Entrenador.dni,
                    nombre_actividad = x.Actividad.nombre        

                }).ToList();
            }
        }

        public void crearObjeto(int id_actividad,int id_entrenador)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Entrenador_Actividad ea = new Entrenador_Actividad();

                ea.actividad_id = id_actividad;
                ea.entrenador_id = id_entrenador;

                db.Entrenador_Actividad.Add(ea);
                db.SaveChanges();
            }
        }

    }
}

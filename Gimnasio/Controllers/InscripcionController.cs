using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Controllers
{
    class InscripcionController
    {
        public void crearInscripcion(int cliente_id, int actividad_id, int plan_id, DateTime fecha_inicio, int cant_dias)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Inscripcion ins = new Inscripcion();

                ins.cliente_id = cliente_id;
                ins.actividad_id = actividad_id;
                ins.plan_id = plan_id;
                ins.fecha_inicio = fecha_inicio;
                ins.cant_dias = cant_dias;
                ins.activo = true;
            }
        }

        public void bajaLogica(int id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Inscripcion inscripcion = new Inscripcion();
                inscripcion = db.Inscripcion.Find(id);

                inscripcion.activo = false;

                db.Entry(inscripcion).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges(); 
            }
        }

        public void bajaFisica(int id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Inscripcion inscripcion = new Inscripcion();
                inscripcion = db.Inscripcion.Find(id);

                db.Inscripcion.Remove(inscripcion);
                db.SaveChanges();
            }
        }
    }
}

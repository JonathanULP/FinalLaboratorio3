using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Controllers
{
    class ActividadController
    {
        public void crearActividad(string nombre, string tipo)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Actividad act = new Actividad();

                act.nombre = nombre;
                act.tipo = tipo;

                db.Actividad.Add(act);
                db.SaveChanges();
            }
        }

        public void modificarActividad(int id, string nombre, string tipo)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Actividad act = new Actividad();
                act = db.Actividad.Find(id);

                act.nombre = nombre;
                act.tipo = tipo;

                db.Entry(act).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void bajaFisica(int id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Actividad act = new Actividad();
                act = db.Actividad.Find(id);

                db.Actividad.Remove(act);
                db.SaveChanges();

            }
        }

        public DbSet<Actividad> obtenerActividad()
        {
            GimnasioEntities db = new GimnasioEntities();
            return db.Actividad;
        }
    }
}

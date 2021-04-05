using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Controllers
{
    class PlanController
    {
        public void crearPlan(int cant_dias,DateTime fecha_limite)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Plaan plan = new Plaan();

                plan.cant_dias = cant_dias;
                plan.fecha_limite = fecha_limite;
                plan.borrado_logico = false;

                db.Plaan.Add(plan);
                db.SaveChanges();
            }
        }

        //metodo para modificiar cantidad de dias// no se si se utilizara
        public void modificarPlan(int id,int cant_dias)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Plaan plan = new Plaan();
                plan = db.Plaan.Find(id);

                plan.cant_dias = cant_dias;

                db.Entry(plan).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void bajaLogica(int id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Plaan plan = new Plaan();
                plan = db.Plaan.Find(id);

                plan.borrado_logico = true;

                db.Entry(plan).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void bajaFisica(int id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Plaan plan = new Plaan();
                plan = db.Plaan.Find(id);

                db.Plaan.Remove(plan);
                db.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Controllers
{
    class RegistroController
    {
        public void crearRegistro(DateTime dia_ingreso,DateTime hora_ingreso,int cliente_id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Registro register = new Registro();

                register.dia_ingreso = dia_ingreso;
                register.hora_ingreso = hora_ingreso;
                register.cliente_id = cliente_id;
                register.borrado_logico = false;

                db.Registro.Add(register);
                db.SaveChanges();
            }
        }

        public void bajaLogica(int id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Registro register = new Registro();
                register = db.Registro.Find(id);

                register.borrado_logico = true;

                db.Entry(register).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges(); 
            }
        }

        public void bajaFisica(int id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Registro register = new Registro();
                register = db.Registro.Find(id);

                db.Registro.Remove(register);
                db.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Controllers
{
    class EntrenadorController
    {
        public void insertarEntrenador(string nombre, string apellido, int dni, DateTime fecha, string sexo, string titulo)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Entrenador trainer = new Entrenador();

                trainer.nombre = nombre;
                trainer.apellido = apellido;
                trainer.dni = dni;
                trainer.fecha = fecha;
                trainer.sexo = sexo;
                trainer.titulo = titulo;
                trainer.borrado_logico = false;

                db.Entrenador.Add(trainer);
                db.SaveChanges();


            }
        }

        public void modificarEntrenador(int id, string nombre, string apellido, int dni, DateTime fecha, string sexo, string titulo)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Entrenador trainer = new Entrenador();
                trainer = db.Entrenador.Find(id);

                trainer.nombre = nombre;
                trainer.apellido = apellido;
                trainer.dni = dni;
                trainer.fecha = fecha;
                trainer.sexo = sexo;
                trainer.titulo = titulo;
                trainer.borrado_logico = false;

                db.Entry(trainer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void bajaLogica(int id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Entrenador trainer = new Entrenador();
                trainer = db.Entrenador.Find(id);

                trainer.borrado_logico = true;

                db.Entry(trainer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void bajaFisica(int id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Entrenador trainer = new Entrenador();
                trainer = db.Entrenador.Find(id);

                db.Entrenador.Remove(trainer);
                db.SaveChanges();
            }
        }

        public DbSet<Entrenador> obtenerEntrenador()
        {
            GimnasioEntities db = new GimnasioEntities();
            return db.Entrenador;
        }

        public Entrenador obtenerEntrenadorID(int id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {    
                return db.Entrenador.Find(id);    
            }
        }
    }
}

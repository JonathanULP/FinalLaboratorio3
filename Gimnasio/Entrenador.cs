//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gimnasio
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public partial class Entrenador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Entrenador()
        {
            this.Entrenador_Actividad = new HashSet<Entrenador_Actividad>();
        }
    
        public int entrenador_id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int dni { get; set; }
        public System.DateTime fecha { get; set; }
        public string sexo { get; set; }
        public string titulo { get; set; }
        public Nullable<bool> borrado_logico { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entrenador_Actividad> Entrenador_Actividad { get; set; }

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
    }
}

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

                db.Inscripcion.Add(ins);
                db.SaveChanges();
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

        public List<Inscripcion> obtenerInscripciones()
        {
            List<Inscripcion> lista = new List<Inscripcion>();
            using (GimnasioEntities db = new GimnasioEntities())
            {
                lista = db.Inscripcion.ToList();
            }

            return lista;
        }

        public object obtenerInscripcionesActivas()
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                var resultado = db.Inscripcion.Where(x => x.activo == true).Select(x => new
                {
                    inscripcion_id = x.inscripcion_id,
                    nombre_cli = x.Cliente.nombre,
                    apellido_cli = x.Cliente.apellido,
                    nombre_act = x.Actividad.nombre,
                    fecha_inic = x.fecha_inicio,
                    fecha_limite = x.Plaan.fecha_limite,
                    cant_dias = x.Plaan.cant_dias,

                }).ToList();

                return resultado;
            }
        }

        public Inscripcion obtenerInscripcionxDNI(int dni)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Inscripcion ins = new Inscripcion();

                ins = (from i in db.Inscripcion
                       where i.Cliente.dni == dni && i.activo == true && i.Plaan.borrado_logico == false
                       select i).FirstOrDefault();

                return ins;
            }
        }

        //Controlamos si el cliente tiene una cuenta activa
        public bool existe(long dni)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                var resultado = (from i in db.Inscripcion
                                 where i.Cliente.dni == dni && i.activo == true
                                 select i).FirstOrDefault();

                if (resultado == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public object obtenerRegistrosActivos(long cliente_dni)
        {
            GimnasioEntities db = new GimnasioEntities();

            Inscripcion ins = db.Inscripcion.Where(x => x.activo == true && x.Cliente.dni == cliente_dni).First();

            return db.Registro.Where(x => x.cliente_id == ins.cliente_id)
                             .Select(x => new
                             {
                                 nombre_cliente = x.Cliente.nombre,
                                 apellido_cliente = x.Cliente.apellido,
                                 fecha_ingreso = x.dia_ingreso,
                                 hora_ingreso = x.hora_ingreso
                             });

        }

        public object bajaFechasExpiradas(ref int cantidad)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                List<Inscripcion> ins = new List<Inscripcion>();
                object result = db.Inscripcion.Where(x => DateTime.Now > x.Plaan.fecha_limite && x.activo == true)
                                              .Select(x => new
                                              {
                                                  nombre = x.Cliente.nombre,
                                                  apellido = x.Cliente.apellido,
                                                  dni = x.Cliente.dni

                                              }).ToList();

                ins = db.Inscripcion.Where(x => DateTime.Now > x.Plaan.fecha_limite && x.activo == true).ToList();
                cantidad = ins.Count();

                foreach(var item in ins)
                {
                    item.activo = false;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return result;

                
            }

        }

        public void modificarInscripcion(int id, int cliente_id, int actividad_id, int plan_id, DateTime fecha_inicio, int cant_dias)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Inscripcion inscripcion = new Inscripcion();
                inscripcion = db.Inscripcion.Find(id);

                inscripcion.cliente_id = cliente_id;
                inscripcion.actividad_id = actividad_id;
                inscripcion.plan_id = plan_id;
                inscripcion.fecha_inicio = fecha_inicio;
                inscripcion.cant_dias = cant_dias;
                inscripcion.activo = true;

                db.Entry(inscripcion).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public Inscripcion obtenerInscripcionID(int id)
        {
                GimnasioEntities db = new GimnasioEntities();
            
                Inscripcion ins = new Inscripcion();
                ins = db.Inscripcion.Find(id);
                return ins;
        }

    }
}

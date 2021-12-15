using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Controllers
{
    class RegistroController
    {
        public void crearRegistro(DateTime dia_ingreso, string hora_ingreso, int cliente_id)
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

        public void insertarRegistro(DateTime dia_ingreso, string hora_ingreso, int cliente_id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Registro register = new Registro();

                register.dia_ingreso = dia_ingreso;
                register.cliente_id = cliente_id;
                register.borrado_logico = false;
                register.hora_ingreso = hora_ingreso;


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

        public DbSet<Registro> obtenerRegistros()
        {
            GimnasioEntities db = new GimnasioEntities();
            return db.Registro;
        }

        //hay que probarlo, probablemente hay que cambiar el tipo de retorno
        public object obtenerRegistroDeClienteXDNI(long cliente_dni)
        {

            GimnasioEntities db = new GimnasioEntities();

            return db.Registro.Where(x => x.Cliente.dni == cliente_dni)
                        .Select(x => new
                        {
                            nombre_cliente = x.Cliente.nombre,
                            apellido_cliente = x.Cliente.apellido,
                            dni_cliente = x.Cliente.dni,
                            hora_ingreso = x.hora_ingreso,
                            dia_ingreso = x.dia_ingreso

                        });

        }

        public object obtenerTodosRegistros()
        {

            GimnasioEntities db = new GimnasioEntities();

            return db.Registro
                        .Select(x => new
                        {
                            nombre_cliente = x.Cliente.nombre,
                            apellido_cliente = x.Cliente.apellido,
                            dni_cliente = x.Cliente.dni,
                            hora_ingreso = x.hora_ingreso,
                            dia_ingreso = x.dia_ingreso

                        });

        }

        public object obtenerRegistroDeClienteXNombreOrApellido(string nombre)
        {
            GimnasioEntities db = new GimnasioEntities();
            string nombreEnMinusculas = nombre.ToLower();

            return db.Registro.Where(x => DbFunctions.Like(x.Cliente.nombre.ToLower(), "%" + nombreEnMinusculas + "%") || DbFunctions.Like(x.Cliente.apellido.ToLower(), "%" + nombreEnMinusculas + "%"))
                              .Select(x => new
                              {
                                  nombre_cliente = x.Cliente.nombre,
                                  apellido_cliente = x.Cliente.apellido,
                                  dni_cliente = x.Cliente.dni,
                                  hora_ingreso = x.hora_ingreso,
                                  dia_ingreso = x.dia_ingreso
                              });

        }

        public object obtenerRegistroDeClientesXFecha(DateTime dia_ingreso)
        {
            GimnasioEntities db = new GimnasioEntities();

            return db.Registro.Where(x => DbFunctions.TruncateTime(x.dia_ingreso) >= DbFunctions.TruncateTime(dia_ingreso))
                              .Select(x => new
                              {

                                  nombre_cliente = x.Cliente.nombre,
                                  apellido_cliente = x.Cliente.apellido,
                                  dni_cliente = x.Cliente.dni,
                                  hora_ingreso = x.hora_ingreso,
                                  dia_ingreso = x.dia_ingreso
                              }).OrderByDescending(x => x.dia_ingreso);
        }
    }
}
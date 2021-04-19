using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Controllers
{
    class ClienteController
    {
        public void insertarCliente(string nombre, string apellido, long dni, DateTime fecha_nac, string sexo)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Cliente cli = new Cliente();

                cli.nombre = nombre;
                cli.apellido = apellido;
                cli.dni = dni;
                cli.fecha_nac = fecha_nac;
                cli.sexo = sexo;
                cli.borrado_logico = false;

                db.Cliente.Add(cli);
                db.SaveChanges();
            }
        }

        public void modificarCliente(int id, string nombre, string apellido, long dni, DateTime fecha_nac, string sexo)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Cliente cli = new Cliente();
                cli = db.Cliente.Find(id);

                cli.nombre = nombre;
                cli.apellido = apellido;
                cli.dni = dni;
                cli.fecha_nac = fecha_nac;
                cli.sexo = sexo;
                cli.borrado_logico = false;

                db.Entry(cli).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void bajaLogica(int id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Cliente cli = new Cliente();
                cli = db.Cliente.Find(id);

                cli.borrado_logico = true;

                db.Entry(cli).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void bajaFisica(int id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Cliente cli = new Cliente();
                cli = db.Cliente.Find(id);

                db.Cliente.Remove(cli);
                db.SaveChanges();
            }
        }

        public List<Cliente> obtenerClientes()
        {
            GimnasioEntities db = new GimnasioEntities();
            return db.Cliente.ToList();
        }

        public Cliente obtenerClienteID(int id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Cliente cli = new Cliente();
                cli = db.Cliente.Find(id);

                return cli;
            }
        }

    }
}

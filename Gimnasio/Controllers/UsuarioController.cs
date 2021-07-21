using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Controllers
{
    class UsuarioController
    {
        //metodo para encriptar contraseña --ni idea como funciona--
        private string GetSHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        //metodo para registrarse
        public void crearUsuario(string nombre, string apellido, string contraseña, long dni, int rol_id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Usuario user = new Usuario();

                user.nombre = nombre;
                user.apellido = apellido;
                user.contraseña = GetSHA1(contraseña);
                user.dni = dni;
                user.rol_id = rol_id;
                user.borrado_logico = false;

                db.Usuario.Add(user);
                db.SaveChanges();
            }
        }

        //metodo para login
        public int ingresoUser(string nombre, string contraseña)
        {
            int respuesta;
            string contraseñaE = GetSHA1(contraseña);
            GimnasioEntities db = new GimnasioEntities();

            //seleccionamos el usuario el cual coincida el nombre y la contraseña
            /*var user = (from u in db.Usuario
                        where u.nombre == nombre && u.contraseña == contraseñaE
                        select u).FirstOrDefault();*/

                  Usuario user = new Usuario();

                  user = db.Usuario.Where(x => x.nombre == nombre && x.contraseña == contraseñaE).FirstOrDefault();

                if (user != null)
                {
                    respuesta = user.usuario_id;
                }
                else
                {
                    respuesta = -1;
                }
            

            return respuesta;
        }

        public void bajaLogica(int id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Usuario user = new Usuario();
                user = db.Usuario.Find(id);

                user.borrado_logico = true;

                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void bajaFisica(int id)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                Usuario user = new Usuario();
                user = db.Usuario.Find(id);

                db.Usuario.Remove(user);
                db.SaveChanges();
            }
        }

        public Usuario obtenerUsuario(int id_usuario)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                return db.Usuario.Find(id_usuario);
            }
        }
    }
}

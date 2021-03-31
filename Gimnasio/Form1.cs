using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gimnasio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            /* try
             {
                 Usuario user = new Usuario();

                 user.crearUsuario("Jonathan", "Montiel", "123456", 39137505, 1);
                 MessageBox.Show("Usuario creado con exito");
             }
             catch(Exception ex)
             {
                 MessageBox.Show(ex.Message,"Error");
             }*/

            try
            {
                Usuario user = new Usuario();

                if (user.ingresoUser("Jonathan", "12346"))
                {
                    MessageBox.Show("Existe usuario");
                }else
                {
                    MessageBox.Show("datos invalidos");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error",ex.Message);
            }


        }
    }
}

using Gimnasio.Controllers;
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
        frmHome1 home;
        frmRegistro register;
        public Form1()
        {
            InitializeComponent();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioController user = new UsuarioController();
                int result = user.ingresoUser(tbnombreuser.Text, tbcontraseñauser.Text);

                if(((home == null) || (home.IsDisposed)) && (result != -1))
                {
                    home = new frmHome1(result);
                    home.Show();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario incorrecto","Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if((register == null) || (register.IsDisposed))
            {
                register = new frmRegistro();
                register.ShowDialog();

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea salir?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if(result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void tbcontraseñauser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((int)e.KeyChar == (int)Keys.Enter)
            {
                try
                {
                    UsuarioController user = new UsuarioController();
                    int result = user.ingresoUser(tbnombreuser.Text, tbcontraseñauser.Text);

                    if (((home == null) || (home.IsDisposed)) && (result != -1))
                    {
                        home = new frmHome1(result);
                        home.Show();

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Usuario incorrecto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                catch
                {

                }
            }
        }
    }
}

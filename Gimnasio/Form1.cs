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
                bool result = user.ingresoUser(tbnombreuser.Text, tbcontraseñauser.Text);

                if(((home == null) || (home.IsDisposed)) && (result))
                {
                    home = new frmHome1();
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
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
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
                    bool result = user.ingresoUser(tbnombreuser.Text, tbcontraseñauser.Text);

                    if (((home == null) || (home.IsDisposed)) && (result))
                    {
                        home = new frmHome1();
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

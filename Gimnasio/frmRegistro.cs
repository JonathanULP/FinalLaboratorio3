using Gimnasio.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gimnasio
{
    public partial class frmRegistro : Form
    {
        public frmRegistro()
        {
            InitializeComponent();
            
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();


        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnmax_Click(object sender, EventArgs e)
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

        private void btnmin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void frmRegistro_Load(object sender, EventArgs e)
        {
            using (GimnasioEntities db = new GimnasioEntities())
            {
                cboRol.DataSource = db.Rol.ToList();

                cboRol.DisplayMember = "nombre";
                cboRol.ValueMember = "rol_id";
            }
        }

        private void btnregistrar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioController user = new UsuarioController();
                user.crearUsuario(tbnombre.Text,tbapellido.Text,tbcontraseña.Text, Convert.ToInt64(tbdni.Text),Convert.ToInt32(cboRol.SelectedValue));
                MessageBox.Show("Usuario creado correctamente","Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

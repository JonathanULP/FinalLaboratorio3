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

        private void limpiarCampos()
        {
            tbapellido.Clear();
            tbnombre.Clear();
            tbdni.Clear();
            tbcontraseña.Clear();
          
        }

        private void btnregistrar_Click(object sender, EventArgs e)
        {
            try
            {
                Validacion val = new Validacion();
                UsuarioController user = new UsuarioController();

                if (val.validarNombre(tbnombre.Text))
                {
                    if (val.validarNombre(tbapellido.Text))
                    {
                        if (val.validarDNI(Convert.ToInt64(tbdni.Text)))
                        {
                            if(tbcontraseña.TextLength >= 6)
                            {
                                user.crearUsuario(tbnombre.Text, tbapellido.Text, tbcontraseña.Text, Convert.ToInt64(tbdni.Text), Convert.ToInt32(cboRol.SelectedValue));
                                MessageBox.Show("Usuario creado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                limpiarCampos();
                            }
                            else
                            {
                                MessageBox.Show("Por favor ingrese una contraseña con 6 caracteres minimo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tbcontraseña.Clear();
                                tbcontraseña.Focus();

                            }
                        }
                        else
                        {
                            MessageBox.Show("Por favor ingrese un DNI valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tbdni.Clear();
                            tbdni.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor ingrese un apellido valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbapellido.Clear();
                        tbapellido.Focus();

                    }
                }
                else
                {
                    MessageBox.Show("Por favor ingrese un nombre valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbnombre.Clear();
                    tbnombre.Focus();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("¿Seguro desea salir?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(res == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}

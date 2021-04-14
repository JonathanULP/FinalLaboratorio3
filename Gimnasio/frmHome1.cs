using Gimnasio.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gimnasio
{
    public partial class frmHome1 : Form
    {

        //las siguientes lineas ayudan a mover el formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();


        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public frmHome1()
        {
            InitializeComponent();
            asignarActividades();
            rellenarLabels();
            rbdeshabilitar.Select();
        }

        //esta funcion permite llenar el combobox con datos traidos desde la base de datos
        public void asignarActividades()
        {
            try
            {
                ActividadController act = new ActividadController();
                cboactividades.DataSource = act.obtenerActividad();
                cboactividades.DisplayMember = "nombre";
                cboactividades.ValueMember = "actividad_id";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Sin actividades");
            }
        }

        //funcion que permite rellenar textbox con los datos de la activadad seleccionada a traves del combobox
        public void rellenarLabels()
        {
            try
            {
                Actividad ac = new Actividad();
                ActividadController act = new ActividadController();

                ac = act.buscarId(Convert.ToInt32(cboactividades.SelectedValue));
                tbnombreactividad.Text = ac.nombre;
                tbtipoactividad.Text = ac.tipo;
                if (ac.borrado_logico == true)
                {
                    tbactivo.Text = "NO";
                }
                else
                {
                    tbactivo.Text = "SI";
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
            
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
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


        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            asignarActividades();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 6;
        }


       
        
        private void cboactividades_SelectedIndexChanged(object sender, EventArgs e)
        {
            rellenarLabels();
        }

        //funcion que permite editar una actividad
        private void btneditar_Click(object sender, EventArgs e)
        {
            try
            {
                ActividadController act = new ActividadController();
                Validacion val = new Validacion();

                DialogResult res = MessageBox.Show("¿Seguro quiere editar esta actividad?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                {
                    if((val.validarNombre(tbnombreactividad.Text)) && (val.validarNombre(tbtipoactividad.Text)))
                    {
                        act.modificarActividad(Convert.ToInt32(cboactividades.SelectedValue), tbnombreactividad.Text, tbtipoactividad.Text);
                        MessageBox.Show("Actividad modificada", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        asignarActividades();
                    }
                    else
                    {
                        MessageBox.Show("Ingrese valores validos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error al modificar actividad"+ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
           
        }

        //funcion para eliminar actividad
        private void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = MessageBox.Show("¿Seguro quiere eliminar esta actividad?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if(res == DialogResult.Yes)
                {
                    ActividadController act = new ActividadController();
                    act.bajaLogica(Convert.ToInt32(cboactividades.SelectedValue));
                    MessageBox.Show("Actividad eliminada", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    asignarActividades();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar actividad" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //boton para agregar actividad
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                ActividadController act = new ActividadController();
                Validacion val = new Validacion();
                
                    if((val.validarNombre(tbnombreactividad.Text)) && (val.validarNombre(tbtipoactividad.Text)))
                    {
                        act.crearActividad(tbnombreactividad.Text,tbtipoactividad.Text);
                        MessageBox.Show("Actividad agregada", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        asignarActividades();
    
                    }
                else
                {
                    MessageBox.Show("Ingrese valores validos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }catch(Exception ex)
            {
                MessageBox.Show("Error al agregar actividad" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //radio button para deshabilitar la funcion de agregar 
        private void rbdeshabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                btnagregar.Enabled = false;
                btnagregar.ForeColor = Color.White;
                rellenarLabels();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Error");
            }
           
        }

        //funcion para habilitar campo de agregar actividad
        private void rbhabilitar_CheckedChanged(object sender, EventArgs e)
        {
            btnagregar.Enabled = true;

            tbactivo.Clear();
            tbnombreactividad.Clear();
            tbtipoactividad.Clear();
            tbnombreactividad.Focus();
            tbactivo.Enabled = false;
        }


        //--------------------------------------hasta aca llega la parte de actividad-------------------------
        //-----------------------------------------------------------------------------------------------------

       
    }
}

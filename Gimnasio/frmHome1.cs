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
            llenarGrillaCliente();
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

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 5;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            llenarGrillaCliente();
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


        //------------------aca empieza la parte de cliente--------------------------------
        //---------------------------------------------------------------


              
       

        //-----------------metodo para llenar grilla con clientes obtenidos desde la base de datos-------------------------------
        public void llenarGrillaCliente()
        {
            try
            {
                ClienteController cli = new ClienteController();
                int x = 0;
                foreach (var item in cli.obtenerClientes())
                {
                    dgvClientes.Rows.Add();
                    dgvClientes.Rows[x].Cells["id"].Value = item.cliente_id;
                    dgvClientes.Rows[x].Cells["nombre"].Value = item.nombre;
                    dgvClientes.Rows[x].Cells["apellido"].Value = item.apellido;
                    dgvClientes.Rows[x].Cells["dni"].Value = item.dni;
                    dgvClientes.Rows[x].Cells["fecha_nac"].Value = item.fecha_nac;
                    dgvClientes.Rows[x].Cells["sexo"].Value = item.sexo;
                    x++;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener clientes"+ex.Message, "Error");
            }
        }

        //metodo para obtener id desde la grilla
        private int? getID()
        {
            try
            {
                return Convert.ToInt32(dgvClientes.Rows[dgvClientes.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }
        }


        //-------------------------metodo para agregar un cliente nuevo--------------------------
        private void btnagregarcliente_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteController cli = new ClienteController();
                Validacion val = new Validacion();
                if (val.validarNombre((tbnombrecliente.Text)) && (val.validarNombre(tbapellidocliente.Text)) && (val.validarDNI(Convert.ToInt32(tbdnicliente.Text))) && (val.validarGenero(tbsexocliente.Text)))
                {
                    cli.insertarCliente(tbnombrecliente.Text, tbapellidocliente.Text, Convert.ToInt64(tbdnicliente.Text), dtpcliente.Value.Date, tbsexocliente.Text);
                    MessageBox.Show("Cliente agregado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    llenarGrillaCliente();
                    limpiarLabels();
                }
                else
                {
                    MessageBox.Show("Ingrese valores correctos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar cliente" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //metodo para seleccionar una celda y poder editarla
        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClienteController cli = new ClienteController();
                Cliente c = new Cliente();

                int id = Convert.ToInt32(getID());

                c = cli.obtenerClienteID(id);
                tbnombrecliente.Text = c.nombre;
                tbapellidocliente.Text = c.apellido;
                tbdnicliente.Text = c.dni.ToString();
                dtpcliente.Text = c.fecha_nac.ToString();
                tbsexocliente.Text = c.sexo;
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al mostrar cliente" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        //---------------------metodo para modificar cliente-------------------------------------
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteController cli = new ClienteController();
                Validacion val = new Validacion();
                int id = Convert.ToInt32(getID());

                if(val.validarNombre((tbnombrecliente.Text)) && (val.validarNombre(tbapellidocliente.Text)) && (val.validarDNI(Convert.ToInt32(tbdnicliente.Text))) && (val.validarGenero(tbsexocliente.Text)))
                {
                    cli.modificarCliente(id, tbnombrecliente.Text, tbapellidocliente.Text, Convert.ToInt64(tbdnicliente.Text), dtpcliente.Value.Date, tbsexocliente.Text);
                    MessageBox.Show("Cliente modificado con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    llenarGrillaCliente();
                    limpiarLabels();
                }
                else
                {
                    MessageBox.Show("Ingrese valores correctos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }



            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al modificar cliente" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //----------------metodo para eliminar clientes------------------------------------
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(getID());
                ClienteController cli = new ClienteController();
                DialogResult res = MessageBox.Show("Seguro quiere eliminar este cliente", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(res == DialogResult.Yes)
                {
                    cli.bajaFisica(id);
                    MessageBox.Show("Cliente eliminado con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    llenarGrillaCliente();
                }
       
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar cliente" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                llenarGrillaCliente();
            }
        }

        //-------------------------metodo para limpiar labels------------------------
        public void limpiarLabels()
        {
            tbnombrecliente.Clear();
            tbapellidocliente.Clear();
            tbdnicliente.Clear();
            tbsexocliente.Clear();

            tbnombrecliente.Focus();
        }
    }
}

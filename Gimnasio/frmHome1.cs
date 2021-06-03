using Gimnasio.Controllers;
using System;
using System.Collections;
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
            asignarActividades(cboactividades);
            rellenarLabels();
            rbdeshabilitar.Select();
            llenarGrillaCliente();
            llenarGrillaInscripcion();
            llenarComboTrainer();


        }

        //esta funcion permite llenar el combobox con datos traidos desde la base de datos
        public void asignarActividades(ComboBox box)
        {
            try
            {
                ActividadController act = new ActividadController();
                box.DataSource = act.obtenerActividad();
                box.DisplayMember = "nombre";
                box.ValueMember = "actividad_id";
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


        //abre tabpage de actividades
        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            asignarActividades(cboactividades);
        }

        //abre tabpage de ingresos
        private void button7_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        //abre tabpage de inscripciones
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            asignarActividades(cbactividadinscripcion);
            llenarGrillaInscripcion();
            llenarComboDias();

        }

        //abre tabpage de personal
        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            llenarGrillaTrainer(-1);
            llenarComboSexo(cbosexotrainer);
            limpiarCampossTrainer();
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
        }

        //abre tabpage de clientes
        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 5;
            llenarComboSexo(cbosexocliente);
            limpiarLabelsCliente();
        }


        //abre tabpage de registros
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                llenarComboRegistro();
                tabControl1.SelectedIndex = 6;

                RegistroController register = new RegistroController();
                dynamic objeto = register.obtenerRegistroDeClientesXFecha(DateTime.Today.AddYears(-100));
                llenarGrillaRegistros(objeto);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al cargar esta vista " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

            
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
                        asignarActividades(cboactividades);
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
                    asignarActividades(cboactividades);
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
                        asignarActividades(cboactividades);
    
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
                dgvClientes.Rows.Clear();
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
        private int? getID(DataGridView dgv)
        {
            try
            {
                return Convert.ToInt32(dgv.Rows[dgv.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }
        }

        private void llenarComboSexo(ComboBox cbo)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("masculino", "Masculino");
                dic.Add("femenino", "Femenino");

                cbo.DataSource = dic.ToList();
                cbo.DisplayMember = "Value";
                cbo.ValueMember = "Key";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al cargar combobox cliente "+ex.Message, "Error",MessageBoxButtons.OK);
            }
        }


        //-------------------------metodo para agregar un cliente nuevo--------------------------
        private void btnagregarcliente_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteController cli = new ClienteController();
                Validacion val = new Validacion();
                if (val.validarNombre((tbnombrecliente.Text)) && (val.validarNombre(tbapellidocliente.Text)) && (val.validarDNI(Convert.ToInt32(tbdnicliente.Text))))
                {
                    cli.insertarCliente(tbnombrecliente.Text, tbapellidocliente.Text, Convert.ToInt64(tbdnicliente.Text), dtpcliente.Value.Date, cbosexocliente.SelectedValue.ToString());
                    MessageBox.Show("Cliente agregado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    llenarGrillaCliente();
                    limpiarLabelsCliente();
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

                int id = Convert.ToInt32(getID(dgvClientes));

                c = cli.obtenerClienteID(id);
                tbnombrecliente.Text = c.nombre;
                tbapellidocliente.Text = c.apellido;
                tbdnicliente.Text = c.dni.ToString();
                dtpcliente.Text = c.fecha_nac.ToString();
                
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
                int id = Convert.ToInt32(getID(dgvClientes));

                if(val.validarNombre((tbnombrecliente.Text)) && (val.validarNombre(tbapellidocliente.Text)) && (val.validarDNI(Convert.ToInt32(tbdnicliente.Text))))
                {
                    cli.modificarCliente(id, tbnombrecliente.Text, tbapellidocliente.Text, Convert.ToInt64(tbdnicliente.Text), dtpcliente.Value.Date, cbosexocliente.SelectedValue.ToString());
                    MessageBox.Show("Cliente modificado con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    llenarGrillaCliente();
                    limpiarLabelsCliente();
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
                int id = Convert.ToInt32(getID(dgvClientes));
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
                MessageBox.Show("Este error puede deberse a que este cliente aun tenga inscripciones activas", "Error al eliminar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);

            }
            finally
            {
                llenarGrillaCliente();
            }
        }

        //-------------------------metodo para limpiar labels------------------------
        public void limpiarLabelsCliente()
        {
            tbnombrecliente.Clear();
            tbapellidocliente.Clear();
            tbdnicliente.Clear();
           

            tbnombrecliente.Focus();
        }



        //-----------------------------------ACA EMPIEZA LA PARTE DE INSCRIPCION---------------------------
        //-------------------------------------------------------------------------------------------------


        private void btncrearinscripcion_Click(object sender, EventArgs e)
        {
            try
            {
                InscripcionController ins = new InscripcionController();
                PlanController plan = new PlanController();
                ClienteController cli = new ClienteController();
                int cant_dias = Convert.ToInt32(cboCantDia.SelectedValue);
                int actividad_id = Convert.ToInt32(cbactividadinscripcion.SelectedValue);


                if (ins.existe(Convert.ToInt64(tbdninscripcion.Text)))
                {
                    //insertar el plan
                    plan.crearPlan(cant_dias, dtpfechalimiteinscripcion.Value.Date);
                    //obtener el id del ultimo plan obtenido
                    Plaan p = plan.obtenerUltimo();
                    Cliente c = cli.obtenerClienteDNI(Convert.ToInt64(tbdninscripcion.Text));

                    //creacion de inscripcion

                    ins.crearInscripcion(c.cliente_id, actividad_id, p.plan_id, dtpfechainicioinscripcion.Value.Date, cant_dias);
                    MessageBox.Show("Inscripcion creada", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    llenarGrillaInscripcion();
                    tbdninscripcion.Clear();
                }
                else
                {
                    MessageBox.Show("Este cliente ya tiene una inscripcion activa ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }               
  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear inscripcion. Verifique el DNI del cliente ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
            }
        }

        private void llenarGrillaInscripcion()
        {
          try
            {
                InscripcionController ins = new InscripcionController();
                dynamic lista = ins.obtenerInscripcionesActivas();
                int x = 0;
                dgvInscripciones.Rows.Clear();
                foreach(var item in lista)
                {
                    dgvInscripciones.Rows.Add();
                    dgvInscripciones.Rows[x].Cells["nombre_cli"].Value = item.nombre_cli+" "+item.apellido_cli;
                    dgvInscripciones.Rows[x].Cells["nombre_act"].Value = item.nombre_act;
                    dgvInscripciones.Rows[x].Cells["fecha_inic"].Value = item.fecha_inic.ToShortDateString();
                    dgvInscripciones.Rows[x].Cells["fecha_limite"].Value = item.fecha_limite.ToShortDateString();
                    dgvInscripciones.Rows[x].Cells["cant_dias"].Value = item.cant_dias;
                    x++;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }
        //-----------EVENTO QUE GENERA UN NUEVO INGRESO DE UN CLIENTE--------------------------
        //----------------------------------------------------------------------------------
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                InscripcionController ins = new InscripcionController();
                PlanController plan = new PlanController();
                RegistroController register = new RegistroController();
                ClienteController cli = new ClienteController();
                Cliente c = new Cliente();


                Inscripcion i = new Inscripcion();
                i = ins.obtenerInscripcionxDNI(Convert.ToInt32(tbingreso.Text)); // busca el ingreso de un cliente a traves del dni del cliente
                                                                                 //

                c = cli.obtenerClienteDNI(Convert.ToInt32(tbingreso.Text));
                int id_cliente = Convert.ToInt32(c.cliente_id);
                int id_plan = i.plan_id;

                Plaan p = new Plaan();
                p = plan.obtenerPlanId(id_plan);
                int fecha = DateTime.Compare(Convert.ToDateTime(p.fecha_limite), DateTime.Now); // verificar que no haya pasado la fecha

                if (fecha > 0) 
                {

                    if (p.cant_dias > 0)//resta una clase en el if
                    {
                        plan.restarClase(id_plan);
                        MessageBox.Show("Ya puede ingresar al gimnasio ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tbdiasrestantes.Text = Convert.ToString(p.cant_dias - 1);
                        tbfecha_limite.Text = p.fecha_limite.ToString();
                        //generar un registro
                        register.insertarRegistro(DateTime.Now.Date, DateTime.Now.ToShortTimeString(), id_cliente);
                    }
                    else
                    {
                        MessageBox.Show("No quedan mas clases disponibles", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        plan.bajaLogica(id_plan);
                        ins.bajaLogica(i.inscripcion_id);
                    }  
                   
                }
                else
                {
                    MessageBox.Show("La fecha limite ya caduco", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    plan.bajaLogica(id_plan);
                    ins.bajaLogica(i.inscripcion_id);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Es probable que el DNI no sea correcto o este cliente ya no tenga inscripciones activas"+ex.InnerException.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
            }
        }

        private void llenarComboDias()
        {
            try
            {
                Dictionary<int, string> dic = new Dictionary<int, string>();
                dic.Add(8, "2(Dos) veces por semana");
                dic.Add(12, "3(Tres) veces por semana");
                dic.Add(20, "5(Cinco) veces por semana");
                dic.Add(30, "Todos los dias");

                cboCantDia.DataSource = dic.ToList();
                cboCantDia.DisplayMember = "Value";
                cboCantDia.ValueMember = "Key";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error a llenar combobox", "Error", MessageBoxButtons.OK);
                Console.WriteLine(ex.Message);
            }
           
        }

        //------------------------ACA EMPIEZA LA VISTA DE PERSONAL-------------------------------------
        //---------------------------------------------------------------------------------------------

        private void llenarComboTrainer()
        {
            try
            {
                ActividadController act = new ActividadController();
                List<Actividad> a = new List<Actividad>();

                a = act.obtenerActividadActiva();
                Dictionary<int, string> dic = new Dictionary<int, string>();

                dic.Add(-1, "Todos");
                foreach (var item in a)
                {
                    dic.Add(item.actividad_id, item.nombre);
                }

                cbotipoactividad.DataSource = dic.ToList();
                cbotipoactividad.DisplayMember = "Value";
                cbotipoactividad.ValueMember = "Key";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al llenar combobox trainer", "Error", MessageBoxButtons.OK);
                Console.Write(ex.Message);
            }
        }

        private void llenarGrillaTrainer(int id)
        {
            try
            {
                Entre_Act_Controller ea = new Entre_Act_Controller();
                EntrenadorController e = new EntrenadorController();

                //limpiar dgv antes de volver a llenar
                dgvtrainers.Rows.Clear();

                int x = 0;
                dynamic lista = ea.obtenerEntrenadores(id);
                

                if (id == -1)
                {

                    foreach (var item in e.obtenerEntrenador())
                    {
                        dgvtrainers.Rows.Add();
                        dgvtrainers.Rows[x].Cells["entrenador_id"].Value = item.entrenador_id;
                        dgvtrainers.Rows[x].Cells["nombre_entrenador"].Value = item.nombre + " " + item.apellido;
                        dgvtrainers.Rows[x].Cells["dni_entrenador"].Value = item.dni;
                        dgvtrainers.Rows[x].Cells["nombre_actividad"].Value = "-";
                        x++;
                    }

                }
                else
                {
                    

                    foreach (var item in lista)
                    {
                        dgvtrainers.Rows.Add();
                        dgvtrainers.Rows[x].Cells["entrenador_id"].Value = item.entrenador_id;
                        dgvtrainers.Rows[x].Cells["nombre_entrenador"].Value = item.nombre_entrenador + " " + item.apellido_entrenador;
                        dgvtrainers.Rows[x].Cells["dni_entrenador"].Value = item.dni_entrenador;
                        dgvtrainers.Rows[x].Cells["nombre_actividad"].Value = item.nombre_actividad;
                        x++;
                    }

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al cargar grilla de entrenadores " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnasignaractividad_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(getID(dgvtrainers));
                Entrenador ee = new Entrenador();
                EntrenadorController ec = new EntrenadorController();

                ee = ec.obtenerEntrenadorID(id);

                lblnombretrainer.Text = ee.nombre + " " + ee.apellido;
                lbldnitrainer.Text = ee.dni.ToString();
                lblgenero.Text = ee.sexo;
                asignarActividades(cboactividadtrainer);
                pnlasignaractividad.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al elegir entrenador "+ex.Message,"Error",MessageBoxButtons.OK);
            }
            
        }

       

       

        private void cbotipoactividad_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                llenarGrillaTrainer(Convert.ToInt32(cbotipoactividad.SelectedValue));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener entrenadores " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = MessageBox.Show("¿Seguro quiere cancelar la operación?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(res == DialogResult.Yes)
                {
                    pnlasignaractividad.Visible =false;
                    lblnombretrainer.Text = "";
                    lbldnitrainer.Text = "";
                    lblgenero.Text = "";

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

      

        private void btnasignar_Click(object sender, EventArgs e)
        {
            try
            {
                Entre_Act_Controller ea = new Entre_Act_Controller();
                int id_actividad = Convert.ToInt32(cboactividadtrainer.SelectedValue);
                int id_entrenador = Convert.ToInt32(getID(dgvtrainers));
                ea.crearObjeto(id_actividad, id_entrenador);

                DialogResult res = MessageBox.Show("Tarea asignada con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(res == DialogResult.OK)
                {
                    pnlformulariotrainer.Visible = true;
                    pnlasignaractividad.Visible = false;
                    llenarGrillaTrainer(-1);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al asignar tarea "+ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnlformulariotrainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void limpiarCampossTrainer()
        {
            tbnombretrainer.Clear();
            tbapellidotrainer.Clear();
            tbdnitrainer.Clear();
            tbtitulotrainer.Clear();

            tbnombretrainer.Focus();
        }


        //-----------------------------Evento utilizado para crear un nuevo entrenador-----------------------------
        //----------------------------------------------------------------------------------------------
        private void btncreartrainer_Click(object sender, EventArgs e)
        {
            try
            {
                EntrenadorController trainer = new EntrenadorController();
                Validacion val = new Validacion();
                int dni_entrenador = Convert.ToInt32(tbdnitrainer.Text);
                if (val.validarNombre(tbnombretrainer.Text))
                {
                    if (val.validarNombre(tbapellidotrainer.Text))
                    {
                        if (val.validarDNI(dni_entrenador))
                        {
                            if (val.validarTitulo(tbtitulotrainer.Text))
                            {
                                trainer.insertarEntrenador(tbnombretrainer.Text, tbapellidotrainer.Text, dni_entrenador, dtpfechanactrainer.Value, cbosexotrainer.SelectedValue.ToString(), tbtitulotrainer.Text);
                                MessageBox.Show("Personal agregado con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                limpiarCampossTrainer();
                                llenarGrillaTrainer(-1);
                            }
                            else
                            {
                                MessageBox.Show("Por favor ingrese un titulo valido. El apellido puede comenzar con mayusculas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                tbtitulotrainer.Clear();
                                tbtitulotrainer.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Por favor ingrese un DNI valido. El DNI solo debe contener numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tbdnitrainer.Clear();
                            tbdnitrainer.Focus();

                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor ingrese un apellido valido. El apellido debe comenzar con mayusculas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbapellidotrainer.Clear();
                        tbapellidotrainer.Focus();
                    }

                }
                else
                {
                    MessageBox.Show("Por favor ingrese un nombre valido. El nombre debe comenzar con mayusculas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbnombretrainer.Clear();
                    tbnombretrainer.Focus();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("No es posible agregar personal nuevo "+ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //---------------------------------Evento para editar un entrenador--------------------------------
        //-------------------------------------------------------------------------------------------------
        private void btneditartrainer_Click(object sender, EventArgs e)
        {
           try
            {
                EntrenadorController trainer = new EntrenadorController();
                Entrenador entrenador = new Entrenador();
                Validacion val = new Validacion();
                int id_entrenador = Convert.ToInt32(getID(dgvtrainers));
                int dni_entrenador = Convert.ToInt32(tbdnitrainer.Text);


                if (val.validarNombre(tbnombretrainer.Text))
                {
                    if (val.validarNombre(tbapellidotrainer.Text))
                    {
                        if (val.validarDNI(dni_entrenador))
                        {
                            if (val.validarTitulo(tbtitulotrainer.Text))
                            {
                                trainer.modificarEntrenador(id_entrenador,tbnombretrainer.Text, tbapellidotrainer.Text, dni_entrenador, dtpfechanactrainer.Value, cbosexotrainer.SelectedValue.ToString(), tbtitulotrainer.Text);
                                MessageBox.Show("Personal editado con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                limpiarCampossTrainer();
                                llenarGrillaTrainer(-1);
                            }
                            else
                            {
                                MessageBox.Show("Por favor ingrese un titulo valido. El apellido puede comenzar con mayusculas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                tbtitulotrainer.Clear();
                                tbtitulotrainer.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Por favor ingrese un DNI valido. El DNI solo debe contener numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tbdnitrainer.Clear();
                            tbdnitrainer.Focus();

                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor ingrese un apellido valido. El apellido debe comenzar con mayusculas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbapellidotrainer.Clear();
                        tbapellidotrainer.Focus();
                    }

                }
                else
                {
                    MessageBox.Show("Por favor ingrese un nombre valido. El nombre debe comenzar con mayusculas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbnombretrainer.Clear();
                    tbnombretrainer.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No es posible editar personal " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dgvtrainers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EntrenadorController trainer = new EntrenadorController();
                Entrenador entrenador = new Entrenador();
              

                int id_entrenador = Convert.ToInt32(getID(dgvtrainers));

                entrenador = trainer.obtenerEntrenadorID(id_entrenador);

                tbnombretrainer.Text = entrenador.nombre;
                tbapellidotrainer.Text = entrenador.apellido;
                tbdnitrainer.Text = entrenador.dni.ToString();
                dtpfechanactrainer.Value = entrenador.fecha;
                tbtitulotrainer.Text = entrenador.titulo;

            }
            catch (Exception ex)
            {
                MessageBox.Show("No es posible obtener los datos de este entrenador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnborrartrainer_Click(object sender, EventArgs e)
        {
            try
            {
                EntrenadorController trainer = new EntrenadorController();
                int id_entrenador = Convert.ToInt32(getID(dgvtrainers));

                DialogResult res = MessageBox.Show("¿Esta seguro que desea eliminar este entrenador?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if(res == DialogResult.Yes)
                {
                    trainer.bajaLogica(id_entrenador);
                    MessageBox.Show("Entrenador eliminado con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    llenarGrillaTrainer(-1);
                    limpiarCampossTrainer();

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("No es posible eliminar este entrenador " + ex.Message,"ERROR", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }



        //-----------------------------------------ACA EMPIEZA LA PARTE DE REGISTRO--------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------


        private void llenarComboRegistro()
        {
            try
            {
                Dictionary<string, DateTime> dic = new Dictionary<string, DateTime>();

                dic.Add("Todos", DateTime.Today.AddYears(-100));
                dic.Add("Hoy",DateTime.Now.Date);
                dic.Add("Ultimos 3 dias", DateTime.Today.AddDays(-3));
                dic.Add("Ultima semana", DateTime.Today.AddDays(-7));
                dic.Add("Ultimo mes", DateTime.Today.AddMonths(-1));
                dic.Add("Ultimos tres meses", DateTime.Today.AddMonths(-3));
                dic.Add("Ultimos seis meses", DateTime.Today.AddMonths(-6));
                dic.Add("Ultimo año", DateTime.Today.AddYears(-1));
               

                cbofecharegistro.DataSource = dic.ToList();
                cbofecharegistro.DisplayMember = "Key";
                cbofecharegistro.ValueMember = "Value";
            }
            catch(Exception ex)
            {
                MessageBox.Show("No se puede llenar el combobox de registro "+ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void llenarGrillaRegistros(dynamic objeto)
        {
            dgvregistros.Rows.Clear();//limpiamos la grilla

            //llenamos la grilla con datos obtenidos desde la base de datos
            int x = 0;
            foreach (var item in objeto)
            {
                dgvregistros.Rows.Add();//genera la fila donde se guardaran los datos
                dgvregistros.Rows[x].Cells["nombre_cliente"].Value = item.nombre_cliente;
                dgvregistros.Rows[x].Cells["apellido_cliente"].Value = item.apellido_cliente;
                dgvregistros.Rows[x].Cells["dni_cliente"].Value = item.dni_cliente;
                dgvregistros.Rows[x].Cells["fecha_ingreso"].Value = item.dia_ingreso.ToShortDateString();
                dgvregistros.Rows[x].Cells["hora_ingreso"].Value = item.hora_ingreso;
                x++;
            }
        }

        private void cbofecharegistro_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                RegistroController register = new RegistroController();

                //obtenemos un object de la busqueda en la base de datos y ese objeto lo usamos para llenar la grilla
                dynamic objeto = register.obtenerRegistroDeClientesXFecha(Convert.ToDateTime(cbofecharegistro.SelectedValue));

                llenarGrillaRegistros(objeto);

               
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al cargar fecha. "+ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbfiltrarregistro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((int)e.KeyChar == (int)Keys.Enter)
            {
                Validacion val = new Validacion();
                RegistroController register = new RegistroController();
                dynamic objeto;
                int n;
                bool isNumero = int.TryParse(tbfiltrarregistro.Text, out n);
                try
                {
                    if (!isNumero && val.validarNombreMinusculas(tbfiltrarregistro.Text))
                    {
                        objeto = register.obtenerRegistroDeClienteXNombreOrApellido(tbfiltrarregistro.Text);
                        llenarGrillaRegistros(objeto);
                    }
                    else if (isNumero && val.validarDNI(Convert.ToInt64(tbfiltrarregistro.Text)))
                    {
                        objeto = register.obtenerRegistroDeClienteXDNI(Convert.ToInt64(tbfiltrarregistro.Text));
                        llenarGrillaRegistros(objeto);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron resultados para: " + tbfiltrarregistro.Text, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error al cargar la grilla de registros "+ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

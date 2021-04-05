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
        public Form1()
        {
            InitializeComponent();
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                EntrenadorController user = new EntrenadorController();
                user.insertarEntrenador("Diego","Montiel",39990100,DateTime.Now,"masculino","PF");
                MessageBox.Show("Entrenador agregado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"ERROR");

            }


        }
    }
}

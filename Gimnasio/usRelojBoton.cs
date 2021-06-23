using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gimnasio
{
    public partial class usRelojBoton : UserControl
    {
        public usRelojBoton()
        {
            InitializeComponent();
        }

        private void usRelojBoton_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblhora.Text = DateTime.Now.ToLongTimeString();
        }

        public event EventHandler evCerrar;

        private void pbcerrar_Click(object sender, EventArgs e)
        {
           if(evCerrar != null)
            {
                evCerrar(sender, e);
            }
        }
    }
}

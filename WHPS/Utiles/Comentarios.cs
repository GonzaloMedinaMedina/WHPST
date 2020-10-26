using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WHPS.Utiles
{
    public partial class Comentarios : Form
    {
        private WHPS.ProgramMenus.WHPST_Cambio_Turno parent;
        private string aux;
        public Comentarios(WHPS.ProgramMenus.WHPST_Cambio_Turno p, string s)
        {
            this.Text = s;
            aux = s;
            parent = p;
            parent.Enabled = false;
            InitializeComponent();
            this.TopMost = true;
            this.Focus();
            this.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            parent.Show();
            parent.Enabled = true;
            this.Close();
            //parent.NotificarCambio(aux,richTextBox1.Text);
            parent.Visible = true;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;

namespace WHPS.ProgramMenus
{
    public partial class WHPST_CALCULADORA : Form
    {


        public string TextoTeclado;
        public WHPST_CALCULADORA()
        {
            InitializeComponent();
        }

        private void buttonENTER_Click(object sender, EventArgs e)
        {

        }

        private void buttonBORRAR_Click(object sender, EventArgs e)
        {
            if (TecladoTB.Text != "") TecladoTB.Text = TecladoTB.Text.Remove(TecladoTB.Text.Length - 1, 1);
        }

        private void buttonpoint_Click(object sender, EventArgs e)
        {
            TecladoTB.Text += ".";
        }

        private void button0_Click(object sender, EventArgs e)
        {
            TecladoTB.Text += "0";
        }
        private void button1_Click(object sender, EventArgs e)
        {

            TecladoTB.Text += "1";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            TecladoTB.Text += "2";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            TecladoTB.Text += "3";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            TecladoTB.Text += "4";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            TecladoTB.Text += "5";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            TecladoTB.Text += "6";
        }
        private void button7_Click(object sender, EventArgs e)
        {
            TecladoTB.Text += "7";
        }
        private void button8_Click(object sender, EventArgs e)
        {
            TecladoTB.Text += "8";
        }
        private void button9_Click(object sender, EventArgs e)
        {
            TecladoTB.Text += "9";
        }
    }
}

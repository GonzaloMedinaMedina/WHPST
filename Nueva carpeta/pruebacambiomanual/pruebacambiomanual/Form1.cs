using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pruebacambiomanual
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int h = 00, m=00, s=00;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            s++;
            if (s > 60) { m++; s = 00; }
            else if (m > 60) { h++; m = 00; }
            else if (h > 12) { h = 00; }
            label1.Text=(h.ToString() + ":" + m.ToString() + ":" + s.ToString());
        }
    }
}
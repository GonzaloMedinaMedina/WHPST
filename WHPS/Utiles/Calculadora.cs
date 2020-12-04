using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;

namespace WHPS.Utiles
{
    public partial class Calculadora : UserControl
    {
        public decimal Numero1 = 0;
        public int Numero2 = 0;
        public bool BOOL = false;
        public bool clicked = false;
        public string TextoTeclado;
        public string Seleccion = "";
        decimal parte_dcha = 0;

        public Calculadora()
        {
            InitializeComponent();
        }


        private void buttonBORRAR_Click(object sender, EventArgs e)
        {
            BOOL = false;
            TecladoTB.Text = "";
            Numero1 = 0;
            parte_dcha = 0;
            Seleccion = "";
            ColorBoton(Seleccion);
        }
        private void buttonpoint_Click(object sender, EventArgs e)
        {
            if (Seleccion != "" && BOOL == false)
            {
                TecladoTB.Text = "";
                BOOL = true;
            }
            if (Seleccion == "" && Numero1 != 0)
            {
                TecladoTB.Text = "";
            }
                TecladoTB.Text += ",";
        }
        private void button0_Click(object sender, EventArgs e)
        {
            if (Seleccion != "" && BOOL == false)
            {
                TecladoTB.Text = "";
                BOOL = true;
            }
            if (Seleccion == "" && Numero1 != 0)
            {
                TecladoTB.Text = "";
                Numero1 = 0;
            }
            TecladoTB.Text += "0";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Seleccion != "" && BOOL == false)
            {
                TecladoTB.Text = "";
                BOOL = true;
            }
            if (Seleccion == "" && Numero1 != 0)
            {
                TecladoTB.Text = "";
                Numero1= 0;
            }
            TecladoTB.Text += "1";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Seleccion != "" && BOOL == false)
            {
                TecladoTB.Text = "";
                BOOL = true;
            }
            if (Seleccion == "" && Numero1 != 0)
            {
                TecladoTB.Text = "";
                Numero1 = 0;
            }
            TecladoTB.Text += "2";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (Seleccion != "" && BOOL == false)
            {
                TecladoTB.Text = "";
                BOOL = true;
            }
            if (Seleccion == "" && Numero1 != 0)
            {
                TecladoTB.Text = "";
                Numero1 = 0;
            }
            TecladoTB.Text += "3";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (Seleccion != "" && BOOL == false)
            {
                TecladoTB.Text = "";
                BOOL = true;
            }
            if (Seleccion == "" && Numero1 != 0)
            {
                TecladoTB.Text = "";
                Numero1 = 0;
            }
            TecladoTB.Text += "4";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (Seleccion != "" && BOOL == false)
            {
                TecladoTB.Text = "";
                BOOL = true;
            }
            if (Seleccion == "" && Numero1 != 0)
            {
                TecladoTB.Text = "";
                Numero1 = 0;
            }
            TecladoTB.Text += "5";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (Seleccion != "" && BOOL == false)
            {
                TecladoTB.Text = "";
                BOOL = true;
            }
            if (Seleccion == "" && Numero1 != 0)
            {
                TecladoTB.Text = "";
                Numero1 = 0;
            }
            TecladoTB.Text += "6";
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (Seleccion != "" && BOOL == false)
            {
                TecladoTB.Text = "";
                BOOL = true;
            }
            if (Seleccion == "" && Numero1 != 0)
            {
                TecladoTB.Text = "";
                Numero1 = 0;
            }
            TecladoTB.Text += "7";
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (Seleccion != "" && BOOL == false)
            {
                TecladoTB.Text = "";
                BOOL = true;
            }
            if (Seleccion == "" && Numero1 != 0)
            {
                TecladoTB.Text = "";
                Numero1 = 0;
            }
            TecladoTB.Text += "8";
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (Seleccion != "" && BOOL == false)
            {
                TecladoTB.Text = "";
                BOOL = true;
            }
            if (Seleccion == "" && Numero1 != 0)
            {
                TecladoTB.Text = "";
                Numero1 = 0;
            }
            TecladoTB.Text += "9";
        }



        //OPERACIONES
        private void buttonSUMAR_Click(object sender, EventArgs e)
        {
            if (TecladoTB.Text != "" && TecladoTB.Text != ",") if (Numero1 == 0) Numero1 = Convert.ToDecimal(TecladoTB.Text);
            Seleccion = "+";
            ColorBoton(Seleccion);

        }


        private void buttonRESTAR_Click(object sender, EventArgs e)
        {
            if(TecladoTB.Text!="" && TecladoTB.Text !=",") if (Numero1 == 0) Numero1 = Convert.ToDecimal(TecladoTB.Text);
            Seleccion = "-";
            ColorBoton(Seleccion);

        }

        private void buttonMULTIPLICAR_Click(object sender, EventArgs e)
        {
            if (TecladoTB.Text != "" && TecladoTB.Text != ",") if (Numero1 == 0) Numero1 = Convert.ToDecimal(TecladoTB.Text);
            Seleccion = "x";
            ColorBoton(Seleccion);
        }

        private void buttonDIVIDIR_Click(object sender, EventArgs e)
        {
            if (TecladoTB.Text != "" && TecladoTB.Text != ",") if (Numero1 == 0) Numero1 = Convert.ToDecimal(TecladoTB.Text);
            Seleccion = "/";
            ColorBoton(Seleccion);

        }


        private void buttonENTER_Click(object sender, EventArgs e)
        {

            if (TecladoTB.Text != "")
            {
                if (Seleccion != "" && parte_dcha == 0) parte_dcha = Convert.ToDecimal(TecladoTB.Text);
                switch (Seleccion)
                {
                    case "+":

                        TecladoTB.Text = (parte_dcha == 0) ? Convert.ToString(Numero1 + Convert.ToDecimal(TecladoTB.Text)) : Convert.ToString(Numero1 + parte_dcha);
                        Numero1 = Convert.ToDecimal(TecladoTB.Text);
                        break;
                    case "-":
                        TecladoTB.Text = (parte_dcha == 0) ? Convert.ToString(Numero1 - Convert.ToDecimal(TecladoTB.Text)) : Convert.ToString(Numero1 - parte_dcha);
                        Numero1 = Convert.ToDecimal(TecladoTB.Text);
                        break;
                    case "x":
                        TecladoTB.Text = (parte_dcha == 0) ? Convert.ToString(Numero1 * Convert.ToDecimal(TecladoTB.Text)) : Convert.ToString(Numero1 * parte_dcha);
                        Numero1 = Convert.ToDecimal(TecladoTB.Text);
                        break;
                    case "/":
                        TecladoTB.Text = (parte_dcha == 0) ? Convert.ToString(Numero1 / Convert.ToDecimal(TecladoTB.Text)) : Convert.ToString(Numero1 / parte_dcha);
                        Numero1 = Convert.ToDecimal(TecladoTB.Text);
                        break;
                }
                BOOL = false;
                buttonSUMAR.BackColor = Color.White;
                buttonRESTAR.BackColor = Color.White;
                buttonMULTIPLICAR.BackColor = Color.White;
                buttonDIVIDIR.BackColor = Color.White;
            }
        }
        /// <summary>
        /// Funcion embellezedora que cambia el color de un boton, si este abre un form hijo.
        /// </summary>
        /// <param name="Boton">Variable que indica que boton tiene que cambiar su color.</param>
        private void ColorBoton(string Seleccion)
        {
            switch (Seleccion)
            {
                case "":
                    buttonSUMAR.BackColor = Color.White;
                    buttonRESTAR.BackColor = Color.White;
                    buttonMULTIPLICAR.BackColor = Color.White;
                    buttonDIVIDIR.BackColor = Color.White;
                    break;
                case "+":
                    buttonSUMAR.BackColor = Color.Maroon;
                    buttonRESTAR.BackColor = Color.White;
                    buttonMULTIPLICAR.BackColor = Color.White;
                    buttonDIVIDIR.BackColor = Color.White;
                    break;
                case "-":
                    buttonSUMAR.BackColor = Color.White;
                    buttonRESTAR.BackColor = Color.Maroon;
                    buttonMULTIPLICAR.BackColor = Color.White;
                    buttonDIVIDIR.BackColor = Color.White;
                    break;
                case "x":
                    buttonSUMAR.BackColor = Color.White;
                    buttonRESTAR.BackColor = Color.White;
                    buttonMULTIPLICAR.BackColor = Color.Maroon;
                    buttonDIVIDIR.BackColor = Color.White;
                    break;
                case "/":
                    buttonSUMAR.BackColor = Color.White;
                    buttonRESTAR.BackColor = Color.White;
                    buttonMULTIPLICAR.BackColor = Color.White;
                    buttonDIVIDIR.BackColor = Color.Maroon;
                    break;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            clicked = true;
        }
        int posX = 0;
        int posY = 0;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button != MouseButtons.Left)
            {
                posX = e.X;
                posY = e.Y;
            }
            else
            {
                Left = Left + (e.X - posX);
                Top = Top + (e.Y + posY);
            }
        }
    }
}


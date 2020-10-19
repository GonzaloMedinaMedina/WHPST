using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using WHPS.Model;

namespace WHPS.Parte
{
    public partial class Parte_CargaExcel : Form
    {
        public Parte_CargaExcel()
        {
            InitializeComponent();
        }

        private void Parte_CargaExcel_Load(object sender, EventArgs e)
        {
            Datos_Parte.Despaletizador_est[0] = false;
            Datos_Parte.Despaletizador_est[1] = false;
            Datos_Parte.Despaletizador_est[2] = false;
            Datos_Parte.Despaletizador_est[3] = false;
            Datos_Parte.Llenadora_est[0] = false;
            Datos_Parte.Llenadora_est[1] = false;
            Datos_Parte.Llenadora_est[2] = false;
            Datos_Parte.Llenadora_est[3] = false;
            Datos_Parte.Etiquetadora_est[0] = false;
            Datos_Parte.Etiquetadora_est[1] = false;
            Datos_Parte.Etiquetadora_est[2] = false;
            Datos_Parte.Etiquetadora_est[3] = false;
            Datos_Parte.Encajonadora_est[0] = false;
            Datos_Parte.Encajonadora_est[1] = false;
            Datos_Parte.Encajonadora_est[2] = false;
            Datos_Parte.Encajonadora_est[3] = false;
            timer1.Enabled = true;
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;
            progressBar4.Value = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Visualización línea de carga
            //ProgressBar1
            if (Datos_Parte.Despaletizador_est[3])
            {
                progressBar1.Value = 100;
            }
            else
            {
                if (Datos_Parte.Despaletizador_est[2])
                {
                    progressBar1.Value = 75;
                }
                else
                {
                    if (Datos_Parte.Despaletizador_est[1])
                    {
                        progressBar1.Value = 50;
                    }
                    else
                    {
                        if (Datos_Parte.Despaletizador_est[0]) { progressBar1.Value = 25; }
                    }
                }
            }//ProgressBar1 FIN
            if ((progressBar1.Value == 100))
            {

                timer1.Enabled = false;
                timer2.Enabled = true;
            }




        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            //ProgressBar2
            if (Datos_Parte.Llenadora_est[3])
            {
                progressBar2.Value = 100;
            }
            else
            {
                if (Datos_Parte.Llenadora_est[2])
                {
                    progressBar2.Value = 75;
                }
                else
                {
                    if (Datos_Parte.Llenadora_est[1])
                    {
                        progressBar2.Value = 50;
                    }
                    else
                    {
                        if (Datos_Parte.Llenadora_est[0]) { progressBar2.Value = 25; }
                    }
                }
            }//ProgressBar2 FIN
            if (progressBar2.Value == 100)
            {
                timer2.Enabled = false;
                timer3.Enabled = true;
            }
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            //ProgressBar3
            if (Datos_Parte.Etiquetadora_est[3])
            {
                progressBar3.Value = 100;
            }
            else
            {
                if (Datos_Parte.Etiquetadora_est[2])
                {
                    progressBar3.Value = 75;
                }
                else
                {
                    if (Datos_Parte.Etiquetadora_est[1])
                    {
                        progressBar3.Value = 50;
                    }
                    else
                    {
                        if (Datos_Parte.Etiquetadora_est[0]) { progressBar3.Value = 25; }
                    }
                }
            }//ProgressBar 3 FIN
            if (progressBar3.Value == 100)
            {
                timer3.Enabled = false;
                timer4.Enabled = true;
            }
        }
        private void timer4_Tick(object sender, EventArgs e)
        {
            //ProgressBar4
            if (Datos_Parte.Encajonadora_est[3])
            {
                progressBar4.Value = 100;
            }
            else
            {
                if (Datos_Parte.Encajonadora_est[2])
                {
                    progressBar4.Value = 75;
                }
                else
                {
                    if (Datos_Parte.Encajonadora_est[1])
                    {
                        progressBar4.Value = 50;
                    }
                    else
                    {
                        if (Datos_Parte.Encajonadora_est[0]) { progressBar4.Value = 25; }
                    }
                }
            }//ProgressBar 4 FIN
            if (progressBar4.Value == 100)
            {
                timer4.Enabled = false;
                MaquinaLinea.CARGANDO = false;
                Close();
            }
        }

    }//Public Class
} //Namespace FIN


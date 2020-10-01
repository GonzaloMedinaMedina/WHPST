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

namespace WHPS.Rotura
{
    public partial class Rotura_Carga : Form
    {
        public Rotura_Carga()
        {
            InitializeComponent();
        }

        private void Rotura_Carga_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;
            progressBar4.Value = 0;
            MainRotura.estadobusqueda = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Visualización línea de carga
            //ProgressBar1
            if (Datos_BusAdmin.Despaletizador_est[3])
            {
                progressBar1.Value = 100;
            }
            else
            {
                if (Datos_BusAdmin.Despaletizador_est[2])
                {
                    progressBar1.Value = 75;
                }
                else
                {
                    if (Datos_BusAdmin.Despaletizador_est[1])
                    {
                        progressBar1.Value = 50;
                    }
                    else
                    {
                        if (Datos_BusAdmin.Despaletizador_est[0]) { progressBar1.Value = 25; }
                    }
                }
            }//ProgressBar1 FIN

            //ProgressBar2
            if (Datos_BusAdmin.Llenadora_est[3])
            {
                progressBar2.Value = 100;
            }
            else
            {
                if (Datos_BusAdmin.Llenadora_est[2])
                {
                    progressBar2.Value = 75;
                }
                else
                {
                    if (Datos_BusAdmin.Llenadora_est[1])
                    {
                        progressBar2.Value = 50;
                    }
                    else
                    {
                        if (Datos_BusAdmin.Llenadora_est[0]) { progressBar2.Value = 25; }
                    }
                }
            }//ProgressBar2 FIN

            //ProgressBar3
            if (Datos_BusAdmin.Etiquetadora_est[3])
            {
                progressBar3.Value = 100;
            }
            else
            {
                if (Datos_BusAdmin.Etiquetadora_est[2])
                {
                    progressBar3.Value = 75;
                }
                else
                {
                    if (Datos_BusAdmin.Etiquetadora_est[1])
                    {
                        progressBar3.Value = 50;
                    }
                    else
                    {
                        if (Datos_BusAdmin.Etiquetadora_est[0]) { progressBar3.Value = 25; }
                    }
                }
            }//ProgressBar 3 FIN

            //ProgressBar4
            if (Datos_BusAdmin.Encajonadora_est[3])
            {
                progressBar4.Value = 100;
            }
            else
            {
                if (Datos_BusAdmin.Encajonadora_est[2])
                {
                    progressBar4.Value = 75;
                }
                else
                {
                    if (Datos_BusAdmin.Encajonadora_est[1])
                    {
                        progressBar4.Value = 50;
                    }
                    else
                    {
                        if (Datos_BusAdmin.Encajonadora_est[0]) { progressBar4.Value = 25; }
                    }
                }
            }//ProgressBar 4 FIN




            if ((progressBar1.Value == 100) || (progressBar2.Value == 100) || (progressBar3.Value == 100) || (progressBar4.Value == 100))
            {
                timer1.Enabled = false;
                Close();
                MainRotura.estadobusqueda = true;
                MaquinaLinea.CARGANDO = false;
            }

        } //Function Timer_Click

    }//Public Class
} //Namespace FIN


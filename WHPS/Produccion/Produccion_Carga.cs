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

namespace WHPS.Produccion
{
    public partial class Produccion_Carga : Form
    {
        public Produccion_Carga()
        {
            InitializeComponent();
        }

        private void Produccion_Carga_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            progressBar4.Value = 0;
            MainProduccion.estadobusqueda = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            //ProgressBar4
            if (Datos_Produccion.EncajonadoraL2_est[3])
            {
                progressBar4.Value = 100;
            }
            else
            {
                if (Datos_Produccion.EncajonadoraL2_est[2])
                {
                    progressBar4.Value = 75;
                }
                else
                {
                    if (Datos_Produccion.EncajonadoraL2_est[1])
                    {
                        progressBar4.Value = 50;
                    }
                    else
                    {
                        if (Datos_Produccion.EncajonadoraL2_est[0]) { progressBar4.Value = 25; }
                    }
                }
            }//ProgressBar 4 FIN




            if ((progressBar4.Value == 100))
            {
                timer1.Enabled = false;
                Close();
                MainProduccion.estadobusqueda = true;

                MaquinaLinea.CARGANDO = false;   
            }

        } //Function Timer_Click

    }//Public Class
} //Namespace FIN


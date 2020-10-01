﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;

namespace WHPS.Etiquetadora
{
    public partial class Etiquetadora_Carga : Form
    {
        public Etiquetadora_Carga()
        {
            InitializeComponent();
        }

        private void Etiquetadora_Carga_Load(object sender, EventArgs e)
        {

        
        timer1.Enabled = true;
            progressBar2.Value = 0;
            Etiquetadora_Parte.estadobusqueda = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            //ProgressBar4
            if (Datos_BusAdmin.Etiquetadora_est[3])
            {
                progressBar2.Value = 100;
            }
            else
            {
                if (Datos_BusAdmin.Etiquetadora_est[2])
                {
                    progressBar2.Value = 75;
                }
                else
                {
                    if (Datos_BusAdmin.Etiquetadora_est[1])
                    {
                        progressBar2.Value = 50;
                    }
                    else
                    {
                        if (Datos_BusAdmin.Etiquetadora_est[0]) { progressBar2.Value = 25; }
                    }
                }
            }//ProgressBar 4 FIN




            if ((progressBar2.Value == 100))
            {
                timer1.Enabled = false;
                Close();
                Etiquetadora_Parte.estadobusqueda = true;

                MaquinaLinea.CARGANDO = false;
            }

        } //Function Timer_Click


    }
}

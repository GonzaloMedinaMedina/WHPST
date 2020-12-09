using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Despaletizador;
using WHPS.Model;

namespace WHPS.Despaletizador { 

    public partial class Despaletizador_Carga : Form
    {
        public Despaletizador_Carga()
        {
            InitializeComponent();
        }

        private void Despaletizador_Carga_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            progressBar2.Value = 0;
            Despaletizador_Parte.estadobusqueda = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            //ProgressBar4
            if (Datos_BusAdmin.Despaletizador_est[3])
            {
                progressBar2.Value = 100;
            }
            else
            {
                if (Datos_BusAdmin.Despaletizador_est[2])
                {
                    progressBar2.Value = 75;
                }
                else
                {
                    if (Datos_BusAdmin.Despaletizador_est[1])
                    {
                        progressBar2.Value = 50;
                    }
                    else
                    {
                        if (Datos_BusAdmin.Despaletizador_est[0]) { progressBar2.Value = 25; }
                    }
                }
            }//ProgressBar 4 FIN




            if ((progressBar2.Value == 100))
            {
                timer1.Enabled = false;
                Close();
                Despaletizador_Parte.estadobusqueda = true;

                MaquinaLinea.CARGANDO = false;
            }

        } //Function Timer_Click


    }
}

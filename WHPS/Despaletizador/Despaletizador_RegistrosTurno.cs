using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;

namespace WHPS.Despaletizador
{
    public partial class Despaletizador_RegistrosTurno : Form
    {
        public Despaletizador_RegistrosTurno()
        {
            InitializeComponent();
        }

        private void Llenadora_RegistrosTurno_Load(object sender, EventArgs e)
        {
            try
            {
                //dataGridViewInicio.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[0, 0]];
                dataGridViewParo.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[3, 0]];
                dataGridVieBotella.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[1, 0]];
                dataGridViewCierre.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[2, 0]];
                dataGridViewRoturas.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[4, 0]];
                dataGridViewComentarios.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[5, 0]];

            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }/*
            dataGridViewParo.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewComentarios.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCierre.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;*/
        }
    }
}

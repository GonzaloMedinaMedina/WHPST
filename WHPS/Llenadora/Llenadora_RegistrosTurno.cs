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

namespace WHPS.Llenadora
{
    public partial class Llenadora_RegistrosTurno : Form
    {
        public Llenadora_RegistrosTurno()
        {
            InitializeComponent();
        }

        private void Llenadora_RegistrosTurno_Load(object sender, EventArgs e)
        {
            try
            {
                //dataGridViewInicio.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[0, 0]];
                dataGridViewPresion.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[1, 0]];
                dataGridViewTempLlenadora.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[2, 0]];
                dataGridViewTempCaldera.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[3, 0]];
                dataGridViewRegistro.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[4, 0]];
                dataGridViewParo.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[5, 0]];
                dataGridViewControl30min.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[6, 0]];
                dataGridViewVerificacion.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[7, 0]];
                dataGridViewVolumen.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[8, 0]];
                dataGridViewTorquimetro.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[9, 0]];
                dataGridViewRoturas.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[10, 0]];
                dataGridViewComentarios.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[11, 0]];

            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }
            dataGridViewParo.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewComentarios.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTempLlenadora.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTempCaldera.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewVerificacion.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}

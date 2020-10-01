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

namespace WHPS.Encajonadora
{
    public partial class Encajonadora_RegistrosTurno : Form
    {
        public Encajonadora_RegistrosTurno()
        {
            InitializeComponent();
        }

        private void Encajonadora_RegistrosTurno_Load(object sender, EventArgs e)
        {
            try
            {
                //dataGridViewInicio.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FLlen[0, 0]];
                dataGridViewRegistro.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[1, 0]];
                dataGridViewParo.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[2, 0]];
                dataGridViewRoturas.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[3, 0]];
                dataGridViewComentarios.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[4, 0]];
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }
            //dataGridViewParo.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridViewComentarios.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridViewTempEncajonadora.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridViewTempCaldera.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridViewVerificacion.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}

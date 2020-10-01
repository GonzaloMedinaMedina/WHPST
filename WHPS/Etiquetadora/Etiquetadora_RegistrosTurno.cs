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

namespace WHPS.Etiquetadora
{
    public partial class Etiquetadora_RegistrosTurno : Form
    {
        public Etiquetadora_RegistrosTurno()
        {
            InitializeComponent();
        }

        private void Etiquetadora_RegistrosTurno_Load(object sender, EventArgs e)
        {
            try
            {
                //dataGridViewInicio.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[0, 0]];
                dataGridViewRegistro.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[1, 0]];
                dataGridViewParo.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[2, 0]];
                dataGridViewControl30min.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[3, 0]];
                dataGridViewVisionArtificial.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[4, 0]];
                dataGridViewRoturas.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[5, 0]];
                dataGridViewComentarios.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[6, 0]];
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }
            //dataGridViewParo.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridViewComentarios.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridViewTempEtiquetadora.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridViewTempCaldera.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridViewVerificacion.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}

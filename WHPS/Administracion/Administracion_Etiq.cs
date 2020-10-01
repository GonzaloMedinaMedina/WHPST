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
using WHPS.Utiles;

namespace WHPS.Administracion
{
    public partial class Administracion_Etiq : Form
    {
        public string Linea;
        public string Dia;
        public string Lote;
        public string Turno;
        public string ColumnasFiltro;
        public int Puntero;

        public Administracion_Etiq()
        {
            InitializeComponent();
        }

        private void Administracion_Etiq_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewInicio.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[0, 0]];
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
            if (MaquinaLinea.TipoBus == false)
            {
                dataGridViewInicio.Columns["Maquinista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewRegistro.Columns["Fecha"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                dataGridViewRegistro.Columns["Turno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                dataGridViewRegistro.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewParo.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewControl30min.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //dataGridViewRoturas.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewComentarios.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (MaquinaLinea.TipoBus == true)
            {
                //dataGridViewParo.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                dataGridViewComentarios.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}

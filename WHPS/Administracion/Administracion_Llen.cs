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
    public partial class Administracion_Llen : Form
    {
        public string Linea;
        public string Dia;
        public string Lote;
        public string Turno;
        public string ColumnasFiltro;
        public int Puntero;

        public Administracion_Llen()
        {
            InitializeComponent();
        }

        private void Administracion_Llen_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewInicio.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[0, 0]];
                dataGridViewPresion.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[1, 0]];
                dataGridViewTempLLenadora.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[2, 0]];
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
            if (MaquinaLinea.TipoBus == false)
            {
                dataGridViewRegistro.Columns["Turno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                dataGridViewRegistro.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewPresion.Columns["Maquinista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewTempLLenadora.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewTempCaldera.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewParo.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewControl30min.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewVerificacion.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewVolumen.Columns["Maquinista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewRoturas.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewComentarios.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (MaquinaLinea.TipoBus == true)
            {
                //dataGridViewParo.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                dataGridViewComentarios.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewPresion.Columns["Maquinista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewTempCaldera.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewVerificacion.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}

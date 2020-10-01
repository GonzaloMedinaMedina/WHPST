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
    public partial class Administracion_Enc : Form
    {
        public string Linea;
        public string Dia;
        public string Lote;
        public string Turno;
        public string ColumnasFiltro;
        public int Puntero;

        public Administracion_Enc()
        {
            InitializeComponent();
        }

        private void Administracion_Enc_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewInicio.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[0, 0]];
                dataGridViewRegistro.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[1, 0]];
                dataGridViewParo.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[2, 0]];
                dataGridViewRoturas.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[3, 0]];
                dataGridViewComentarios.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[4, 0]];
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }
            if (MaquinaLinea.TipoBus == false)
            {
                //dataGridViewInicio.Columns["Fecha"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                dataGridViewInicio.Columns["Maquinista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewRegistro.Columns["Fecha"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                dataGridViewRegistro.Columns["Turno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                dataGridViewRegistro.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewParo.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewRoturas.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

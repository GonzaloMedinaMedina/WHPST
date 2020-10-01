using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using WHPS.Utiles;
using WHPS.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace WHPS.Administracion
{
    public partial class Administracion_Desp : Form
    {
        public string Linea;
        public string Dia;
        public string Lote;
        public string Turno;
        public string ColumnasFiltro;
        public int Puntero;
        public int Filtro;

        public Administracion_Desp()
        {
            InitializeComponent();
        }


        private void Administracion_Desp_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewInicio.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[0, 0]];
                dataGridViewBotellas.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[1, 0]];
                dataGridViewCierres.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[2, 0]];
                dataGridViewParo.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[3, 0]];
                dataGridViewRoturas.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[4, 0]];
                dataGridViewComentarios.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[5, 0]];
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }
            if (MaquinaLinea.TipoBus == false)
            {
                dataGridViewInicio.Columns["Maquinista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewBotellas.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewCierres.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewParo.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewRoturas.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewComentarios.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (MaquinaLinea.TipoBus == true)
            {
                dataGridViewInicio.Columns["Maquinista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewBotellas.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                dataGridViewCierres.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                dataGridViewParo.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewRoturas.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                dataGridViewComentarios.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}

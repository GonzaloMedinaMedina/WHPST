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
using WHPS.Utiles;

namespace WHPS
{
    public partial class TestExcel : Form
    {
        public TestExcel()
        {
            InitializeComponent();
        }

        private void cbEjecutarSelect_Click(object sender, EventArgs e)
        {
            List<string[]> valoresAFiltrar = generarLista(dgvSelectFiltro, 4);

            DataSet excelDataSet = new DataSet();
            string result;
           // List<string[]> valoresAFiltrar = dgvSelectFiltro.DataSource;
            excelDataSet = ExcelUtiles.LeerFicheroExcel(tbSelectClaveMaquina.Text, tbSelectNombreHoja.Text, tbSelectNombreColumnas.Text.Split(';'), valoresAFiltrar, out result);
            tbSelectSalidaError.Text = result;
            try
            {
                dgvSalidaConsulta.DataSource = excelDataSet.Tables[0];
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }
        }

        private void cbUpdateEjecutar_Click(object sender, EventArgs e)
        {
            List<string[]> valoresAFiltrar = generarLista(dgvUpdateFiltro, 4);
            List<string[]> valoresAActualizar = generarLista(dgvUpdateValores, 2);
            tbUpdateSalida.Text = ExcelUtiles.ActualizarFicheroExcel(tbUpdateClaveMaquina.Text, tbUpdateNombreHoja.Text, valoresAActualizar, valoresAFiltrar);
        }

        private void cbInsertEjecutar_Click(object sender, EventArgs e)
        {
            List<string[]> valoresAInsertar = generarLista(dgvInsertValores, 2);
            tbInsertSalida.Text = ExcelUtiles.EscribirFicheroExcel(tbInsertClaveMaquina.Text, tbInsertNombreHoja.Text, valoresAInsertar, tbInsertIdentificador.Text);
        }

        protected List<string[]> generarLista(DataGridView dgvFiltro, int size)
        {
            List<string[]> listaValores = new List<string[]>();
            foreach (DataGridViewRow dr in dgvFiltro.Rows)
            {
                int i = 0;
                string[] valores = new string[size];
                foreach (DataGridViewCell dc in dr.Cells)
                {
                    if (dc.Value != null)
                    {
                        valores[i] = (string)dc.Value;
                    }
                    else
                    {
                        valores[i] = "";
                    }
                    i++;
                }
                listaValores.Add(valores);
            }
            return listaValores;
        }
    }
}

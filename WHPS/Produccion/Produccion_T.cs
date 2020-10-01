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

namespace WHPS.Produccion
{
    public partial class Produccion_T : Form
    {
        public string DIA1;
        public string DIA2;
        public string DIA3;
        public string DIA4;
        public string DIA5;
        public int suma = 0;
        public string formato;
        public int cajas = 0;
        public DateTime DiaInicio;
        public Produccion_T()
        {
            InitializeComponent();
        }


        private void Produccion_T_Load(object sender, EventArgs e)
        {
            //Se añaden 2 filas al dgv donde se van a mostrar los datos de los ultimos 5 días
            dgvIndicadores.Rows.Add(2);
            dgvIndicadores.Rows[0].Cells["LINEA1"].Value = "LÍNEA 2";
            dgvIndicadores.Rows[1].Cells["LINEA1"].Value = "LÍNEA 3";
            dgvIndicadores.Rows[2].Cells["LINEA1"].Value = "LÍNEA 5";

            //Obtenemos los datos de la busqueda que se ha realizado
            DiaInicio = DateTime.Parse(Properties.Settings.Default.BusDia);
            DIA1 = DiaInicio.ToString().Substring(0, 10);
            DIA2 = Convert.ToString(DiaInicio.AddDays(+1)).Substring(0, 10);
            DIA3 = Convert.ToString(DiaInicio.AddDays(+2)).Substring(0, 10);
            DIA4 = Convert.ToString(DiaInicio.AddDays(+3)).Substring(0, 10);
            DIA5 = Convert.ToString(DiaInicio.AddDays(+4)).Substring(0, 10);

            //Se cambia el nombre y el texto de las colmnas
            dgvIndicadores.Columns["Column1"].HeaderText = DIA1;
            dgvIndicadores.Columns["Column2"].HeaderText = DIA2;
            dgvIndicadores.Columns["Column3"].HeaderText = DIA3;
            dgvIndicadores.Columns["Column4"].HeaderText = DIA4;
            dgvIndicadores.Columns["Column5"].HeaderText = DIA5;


            dgvIndicadores.Columns["Column1"].Name = DIA1;
            dgvIndicadores.Columns["Column2"].Name = DIA2;
            dgvIndicadores.Columns["Column3"].Name = DIA3;
            dgvIndicadores.Columns["Column4"].Name = DIA4;
            dgvIndicadores.Columns["Column5"].Name = DIA5;

            CompletarRegistros();

            //Se añaden los datos al dgv
            CompletarGrafica();
        }
        //Funcion que procesa el valor obtenido de cada fila y lo incrementa
        private void CompletarRegistros()
        {
            //int acumulado = 0;
            for (int i = 0; i < 5; i++)
            {
                dgvIndicadores.Rows[0].Cells[i + 1].Value = Convert.ToString(MaquinaLinea.DatosTotalesL2[i]);
                dgvIndicadores.Rows[1].Cells[i + 1].Value = Convert.ToString(MaquinaLinea.DatosTotalesL3[i]);
                dgvIndicadores.Rows[2].Cells[i + 1].Value = Convert.ToString(MaquinaLinea.DatosTotalesL5[i]);
            }
        }
        //Funcion que incrementa todos los datos de un día
        private void CompletarGrafica()
        {
            for (int j = 0; j < 5; j++)
            {
                //MessageBox.Show(dgvIndicadores.Rows[0].Cells[j].Value.ToString());
               if (dgvIndicadores.Rows[0].Cells[j+1].Value != null) chart1.Series["LINEA 2"].Points.AddXY(j, int.Parse(dgvIndicadores.Rows[0].Cells[j+1].Value.ToString()));
               if (dgvIndicadores.Rows[1].Cells[j+1].Value != null) chart1.Series["LINEA 3"].Points.AddXY(j, int.Parse(dgvIndicadores.Rows[1].Cells[j+1].Value.ToString()));
               if (dgvIndicadores.Rows[2].Cells[j+1].Value != null) chart1.Series["LINEA 5"].Points.AddXY(j, int.Parse(dgvIndicadores.Rows[2].Cells[j+1].Value.ToString()));
            }
        }
    }
}

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
    public partial class Produccion_L3 : Form
    {
        public string DIA1;
        public string DIA2;
        public string DIA3;
        public string DIA4;
        public string DIA5;
        public int suma = 0;
        public string formato;
        public int cajas = 0;
        public int[] DatosFinales = new int[5];
        public DateTime DiaInicio;
        public Produccion_L3()
        {
            InitializeComponent();
        }


        private void Produccion_L3_Load(object sender, EventArgs e)
        {
            AbrirGraficaB.BackColor = Color.Maroon;

            //Se añaden 2 filas al dgv donde se van a mostrar los datos de los ultimos 5 días
            dgvIndicadores.Rows.Add(2);
            dgvIndicadores.Rows[0].Cells["TURNO1"].Value = "Mañana";
            dgvIndicadores.Rows[1].Cells["TURNO1"].Value = "Tarde";
            dgvIndicadores.Rows[2].Cells["TURNO1"].Value = "Noche";

            //Obtenemos los datos de la busqueda que se ha realizado
            DiaInicio = DateTime.Parse(Properties.Settings.Default.BusDia);
            DIA1 = DiaInicio.ToString().Substring(0, 10);
            DIA2 = Convert.ToString(DiaInicio.AddDays(+1)).Substring(0, 10);
            DIA3 = Convert.ToString(DiaInicio.AddDays(+2)).Substring(0, 10);
            DIA4 = Convert.ToString(DiaInicio.AddDays(+3)).Substring(0, 10);
            DIA5 = Convert.ToString(DiaInicio.AddDays(+4)).Substring(0, 10);

            //Se cambia el nombre y el texto de las colmnas
            dgvIndicadores.Columns["Dia1"].HeaderText = DIA1;
            dgvIndicadores.Columns["Dia2"].HeaderText = DIA2;
            dgvIndicadores.Columns["Dia3"].HeaderText = DIA3;
            dgvIndicadores.Columns["Dia4"].HeaderText = DIA4;
            dgvIndicadores.Columns["Dia5"].HeaderText = DIA5;


            dgvIndicadores.Columns["Dia1"].Name = DIA1;
            dgvIndicadores.Columns["Dia2"].Name = DIA2;
            dgvIndicadores.Columns["Dia3"].Name = DIA3;
            dgvIndicadores.Columns["Dia4"].Name = DIA4;
            dgvIndicadores.Columns["Dia5"].Name = DIA5;
            //Se cargan los datos del registro
            try
            {
                dataGridViewRegistroEnc.DataSource = Datos_Produccion.EncajonadoraL3.Tables[Datos_Produccion.FEncL3[1, 0]];

            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }

            //Se extraen los datos del registro 1 a 1 operando el formato con las cajas para obtener las botellas
            for (int i = 0; i < dataGridViewRegistroEnc.RowCount - 1; i++)
            {

                //MessageBox.Show(dataGridViewRegistroEnc.Rows[i].Cells[4].Value.ToString());
                formato = dataGridViewRegistroEnc.Rows[i].Cells[5].Value.ToString();
                cajas = int.Parse(dataGridViewRegistroEnc.Rows[i].Cells[7].Value.ToString());
                if (formato.Substring(1, 1) == "X")
                {
                    suma += Convert.ToInt16(formato.Substring(0, 1)) * cajas;
                    CompletarRegistros(dataGridViewRegistroEnc.Rows[i].Cells[3].Value.ToString(), dataGridViewRegistroEnc.Rows[i].Cells[0].Value.ToString(), Convert.ToInt16(formato.Substring(0, 1)) * cajas);
                }
                if (formato.Substring(2, 1) == "X")
                {
                    suma += Convert.ToInt16(formato.Substring(0, 2)) * cajas;
                    CompletarRegistros(dataGridViewRegistroEnc.Rows[i].Cells[3].Value.ToString(), dataGridViewRegistroEnc.Rows[i].Cells[0].Value.ToString(), Convert.ToInt16(formato.Substring(0, 2)) * cajas);
                }
            }
            suma = 0;

            //Se añaden los datos al dgv
            int k = 1;
            for (int j = 0; j < 5; j++)
            {
                int Botellas = TotalBotellas(k);
                chart1.Series["LINEA 3"].Points.AddXY(j, Botellas);
                chart1.Series["OBJETIVO"].Points.AddXY(j, MaquinaLinea.OBJETIVOL3);
                DatosFinales[j] = Botellas;
                k++;
            }
            MaquinaLinea.DatosTotalesL3 = DatosFinales;
        }
        //Funcion que procesa el valor obtenido de cada fila y lo incrementa
        private void CompletarRegistros(string truno, string dia, int valor)
        {
            int acumulado = 0;
            switch (truno)
            {
                case "Mañana":

                    if (dgvIndicadores.Rows[0].Cells[dia].Value != null) acumulado = int.Parse(dgvIndicadores.Rows[0].Cells[dia].Value.ToString());   
                    dgvIndicadores.Rows[0].Cells[dia].Value = Convert.ToString(acumulado + valor);
                    break;
                case "Tarde":

                    if (dgvIndicadores.Rows[1].Cells[dia].Value != null) acumulado = int.Parse(dgvIndicadores.Rows[1].Cells[dia].Value.ToString());
                    dgvIndicadores.Rows[1].Cells[dia].Value = Convert.ToString(acumulado + valor);
                    break;
                case "Noche":

                    if (dgvIndicadores.Rows[2].Cells[dia].Value != null) acumulado = int.Parse(dgvIndicadores.Rows[2].Cells[dia].Value.ToString());
                    dgvIndicadores.Rows[2].Cells[dia].Value = Convert.ToString(acumulado + valor);
                    break;
            }
            acumulado = 0;
        }
        //Funcion que incrementa todos los datos de un día
        private int TotalBotellas(int dia)
        {
            int acumulado = 0;
            for (int i = 0; i<3;i++)
            {
                if (dgvIndicadores.Rows[i].Cells[dia].Value != null) acumulado += int.Parse(dgvIndicadores.Rows[i].Cells[dia].Value.ToString());

            }
            return acumulado;
        }
        //Botones
        private void AbrirGraficaB_Click(object sender, EventArgs e)
        {
            AbrirGraficaB.BackColor = Color.Maroon;
            AbriDatosB.BackColor = Color.DimGray;
            panel2.Visible=false;
            panel1.Visible = true;
        }

        private void AbriDatosB_Click(object sender, EventArgs e)
        {
            AbriDatosB.BackColor = Color.FromArgb(27, 33,41);
            AbrirGraficaB.BackColor = Color.DimGray;
            
            panel1.Visible = false;
            panel2.Visible=true;
        }
    }
}

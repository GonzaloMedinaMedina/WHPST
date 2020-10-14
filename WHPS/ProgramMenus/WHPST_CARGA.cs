using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Utiles;
using WHPS.Model;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace WHPS.ProgramMenus
{
    public partial class WHPST_CARGA : Form
    {
        bool[] status = new bool[4];

        public WHPST_CARGA()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 1;
            if ((status[2]) && (progressBar1.Value > 95))
            {
                ListaCarga();
                estadotxt.Text = "Actualización completada...";
            }
            else
            {
                if (progressBar1.Value > 65 && status[1] && !status[2])
                {
                    ListaCarga();
                    estadotxt.Text = "Actualizando WHPS: Materiales/TablasDatos/ ...";
                }
                else
                {
                    if (progressBar1.Value > 45 && status[0] && !status[1])
                    {
                        ListaCarga();
                        estadotxt.Text = "Actualizando Lanzamiento...";
                    }
                    else
                    {
                        if (progressBar1.Value > 25 && !status[0] && !status[3])
                        {
                            ListaCarga();
                            estadotxt.Text = "Actualizando Lanzamiento...";
                        }
                        else
                        {
                            if ((progressBar1.Value < 2) && !status[0] && !status[3])
                            {
                                estadotxt.Text = "Comenzando Actualización...";
                            }
                        }
                    }
                }
            }

            if (progressBar1.Value ==100)
            {
                timer1.Enabled = false;
                WHPST_INICIO Form = new WHPST_INICIO();
                Hide();
                Form.Show();
            }


        }



        private void ListaCarga()
        {
            string result;
            if (status[2] && !status[3])
            {
                //Copia de los ficheros Actualizados del sistema
                ExcelUtiles.CopiaFile("Materiales");
                status[3] = true;
            }
            if (status[1] && !status[2])
            {
                //Copia el fichero, Interpreta y carga el DataSet
                LanzamientoLinea.DBL5 = ExcelUtiles.ObtenerUltimosMovimientosLanzador("DB_L5", "Linea 5", out result);
                LanzamientoLinea.DBL5_Precinta = ExcelUtiles.ObtenerUltimosMovimientosPrecinta("DB_L5", "Linea 5", out result);
                //Pasa a true el estado de las variables
                Model.LanzamientoLinea.DBL5_bool = true;
                status[2] = true;
            }
            if (status[0] && !status[1])
            {
                //Copia el fichero, Interpreta y carga el DataSet
                LanzamientoLinea.DBL3 = ExcelUtiles.ObtenerUltimosMovimientosLanzador("DB_L3", "Linea 3", out result);
                LanzamientoLinea.DBL3_Precinta = ExcelUtiles.ObtenerUltimosMovimientosPrecinta("DB_L3", "Linea 3", out result);

                //Pasa a true el estado de las variables
                Model.LanzamientoLinea.DBL3_bool = true;
                status[1] = true;
            }
            if (!status[0] && !status[3])
            {
                //Copia el fichero, Interpreta y carga el DataSet
                LanzamientoLinea.DBL2 = ExcelUtiles.ObtenerUltimosMovimientosLanzador("DB_L2", "Linea 2", out result);
                LanzamientoLinea.DBL2_Precinta = ExcelUtiles.ObtenerUltimosMovimientosPrecinta("DB_L2", "Linea 2", out result);

                //Pasa a true el estado de las variables
                Model.LanzamientoLinea.DBL2_bool = true;
                status[0] = true;
            }
        }

        private void WHPST_CARGA_Load(object sender, EventArgs e)
        {
            //COMPRUEBA SI REQUIERE ACTUALIZACIÓN PARA ELLO LEE EN EL EXCEL.
            //Realiza la busqueda
            List<string[]> valoresAFiltrar = new List<string[]>();
            string[] filterval = new string[4];
            filterval[0] = "AND";
            filterval[1] = "IDPC";
            filterval[2] = "LIKE";
            filterval[3] = " \"" + Properties.Settings.Default.IDPC + "\"";
            valoresAFiltrar.Add(filterval);

            DataSet excelDataSet = new DataSet();
            string result;

            excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileComprobarActualizacion, "ACTUALIZACION", "ACTUALIZACION".Split(';'), valoresAFiltrar, out result);
            //MessageBox.Show(result);
            //Una vez realizada la busqueda si esta es correcta se modifican los parámetros de la tabla para se adecuen a las necesidades del usuario

            if (excelDataSet.Tables[0].Rows.Count > 0)
            {
                MaquinaLinea.ACTUALIZARPC = Convert.ToString(excelDataSet.Tables[0].Rows[0]["ACTUALIZACION"]);
            }

            panel1.BackColor = Properties.Settings.Default.COLOR1;
            panel2.BackColor = Properties.Settings.Default.COLOR1;
            panel3.BackColor = Properties.Settings.Default.COLOR1;
            panel4.BackColor = Properties.Settings.Default.COLOR1;
            pictureBox2.BackColor = Properties.Settings.Default.COLOR1;
            timer1.Enabled = true;
        }

        private void LeerFicheroExcel(string v1, object claveMaquina, string v2, object hoja, char v3, object campos, bool v4, out string v5, object result)
        {
            throw new NotImplementedException();
        }
    }
}

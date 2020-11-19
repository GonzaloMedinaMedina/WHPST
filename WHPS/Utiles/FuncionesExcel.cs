using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;

namespace WHPS.Utiles
{
    class FuncionesExcel
    {
        private static string result = "";

        /// <summary>
        /// Función que lee el excel datos línea y guada los nombres.
        /// </summary>
        public static void LeerExcelDatos_Lineas(string Excel, string Hoja, string Columnas)
        {
            //La lista de valores a filtrar.
            List<string[]> listavalores = new List<string[]>();

            //DataSet donde se registra los datos
            DataSet excelDataSet = new DataSet();

            //Registramos los datos con los parámetros indicados
            excelDataSet = ExcelUtiles.LeerFicheroExcel(Excel, Hoja, Columnas.Split(';'), listavalores, out result);
            //MessageBox.Show(result);

            //Si encuentra la tabla, los valores se han registrado correctamente y los guardamos.
            if (excelDataSet.Tables[0].Rows.Count > 0)
            {
                MaquinaLinea.Responsable = Convert.ToString(excelDataSet.Tables[0].Rows[0][MaquinaLinea.turno]);
                MaquinaLinea.MDespaletizador = Convert.ToString(excelDataSet.Tables[0].Rows[1][MaquinaLinea.turno]);
                MaquinaLinea.MLlenadora = Convert.ToString(excelDataSet.Tables[0].Rows[2][MaquinaLinea.turno]);
                MaquinaLinea.MEtiquetadora = Convert.ToString(excelDataSet.Tables[0].Rows[3][MaquinaLinea.turno]);
                MaquinaLinea.MEncajonadora = Convert.ToString(excelDataSet.Tables[0].Rows[4][MaquinaLinea.turno]);
                MaquinaLinea.ControlCal = Convert.ToString(excelDataSet.Tables[0].Rows[5][MaquinaLinea.turno]);
            }
            else
            {
                MessageBox.Show(Properties.Settings.Default.AvisoProblemaFichero);
            }
        }



        /// <summary>
        /// Función que lee el excel DB y devuelve el DataSet encontrado.
        /// </summary>
        public static DataSet LeerExcelDB(string Excel, string Hoja, string Columnas, string ColumnaBusqueda, string Busqueda)
        {
            //Realiza la busqueda para detectar si hay algun producto iniciado
            List<string[]> valoresAFiltrar = new List<string[]>();
            string[] filterval = new string[4];
            filterval[0] = "AND";
            filterval[1] = ColumnaBusqueda;
            filterval[2] = "LIKE";
            filterval[3] = " \"" + Busqueda + "\"";
            valoresAFiltrar.Add(filterval);

            DataSet excelDataSet = new DataSet();
            string result;
            //List<string[]> valoresAFiltrar = dgvSelectFiltro.DataSource;
            excelDataSet = ExcelUtiles.LeerFicheroExcel(Excel, Hoja, Columnas.Split(';'), valoresAFiltrar, out result);
            //MessageBox.Show(result);
                return excelDataSet;
        }
    }
}

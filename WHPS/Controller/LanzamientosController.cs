using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.Utiles;

namespace WHPS.Controller
{
    class LanzamientosController
    {
        internal static DataSet ObtenerUltimosMovimientosLanzador(string claveMaquina, string nombreHoja, out string result)
        {
            result = "";
            DataSet resultDataSet = new DataSet();
            DataSet excelDataSet = new DataSet();
            List<LanzamientoLinea> listaLineasLanzamiento = new List<LanzamientoLinea>();
            List<LanzamientoLinea> listaLineasLanzamientoTemp = new List<LanzamientoLinea>();
            // Comprobación del valor de la claveMaquina
            if (claveMaquina != null && claveMaquina != "")
            {
                //string connStr = iniciaDatosConn(claveMaquina);
                string connStr = Properties.Settings.Default.TipoConexion == "SQL" ? ConexionDatos.iniciaDatosConnSQL() : ConexionDatos.iniciaDatosConn(claveMaquina);
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connStr))
                    {
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.Connection = conn;

                        // Obtenemos todos los registros
                        DateTime fechaAnterior = DateTime.Now;
                        if (!TurnosController.GetTurnoAnterior().Equals("Noche"))
                        {
                            fechaAnterior = DateTime.Now.AddDays(-1);
                        }

                        string cmdText = "SELECT * FROM [" + nombreHoja + "$]";
                        cmdText = Properties.Settings.Default.TipoConexion == "SQL" ? cmdText.Replace("[", "").Replace("$]", "") : cmdText;
                        cmd.CommandText = cmdText;
                        Debug.Print(cmdText);
                        DataTable dt = new DataTable();
                        dt.TableName = nombreHoja;

                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(dt);

                        cmd = null;
                        conn.Close();
                        excelDataSet.Tables.Add(dt);

                    }
                    if (excelDataSet != null && excelDataSet.Tables != null && excelDataSet.Tables.Count > 0)
                    {
                        listaLineasLanzamientoTemp = ObtenerListaLanzanmientos(excelDataSet);
                    }

                    try
                    {
                        int contCompletados = 1;
                        listaLineasLanzamientoTemp.Reverse();
                        foreach (LanzamientoLinea linea in listaLineasLanzamientoTemp)
                        {
                            if (!linea.iDOrd.Equals("ID_Ord") && !linea.iDOrd.Equals("DATOS ORDEN - PLANIFICACIÓN"))
                            {
                                if (!linea.iDOrd.Equals(""))
                                {
                                    if (linea.estado.ToUpper().Equals("COMPLETADO") && contCompletados < MaquinaLinea.MovimientosLanzamientoAnterior)
                                    {
                                        listaLineasLanzamiento.Add(linea);
                                        contCompletados++;
                                    }
                                }
                            }
                        }
                        listaLineasLanzamiento.Reverse();
                        listaLineasLanzamientoTemp.Reverse();
                        foreach (LanzamientoLinea linea in listaLineasLanzamientoTemp)
                        {
                            if (!linea.iDOrd.Equals("ID_Ord") && !linea.iDOrd.Equals("DATOS ORDEN - PLANIFICACIÓN"))
                            {
                                if (!linea.iDOrd.Equals(""))
                                {
                                    if (!linea.estado.ToUpper().Equals("COMPLETADO"))
                                    {
                                        listaLineasLanzamiento.Add(linea);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                        Debug.Print(ex.StackTrace);
                        if (!ErrorLog.ErrorRoutine(false, ex))
                        {
                            MessageBox.Show("Unable to write a log");
                        }
                        return null;
                    }
                    //Debug.Print(result);
                    resultDataSet = GenerarHojaLanzamientoNueva(listaLineasLanzamiento);
                    return resultDataSet;
                }
                catch (Exception ex)
                {
                    Debug.Print("MESSAGE:" + ex.Message);
                    Debug.Print("STACKTRACE:" + ex.StackTrace);
                    result = Environment.NewLine + "ERROR:" + Environment.NewLine + ex.Message + Environment.NewLine;
                    if (!ErrorLog.ErrorRoutine(false, ex))
                    {
                        MessageBox.Show("Unable to write a log");
                    }
                    return resultDataSet;
                }
            }
            result += Environment.NewLine + "OK";
            return resultDataSet;
        }

        private static List<LanzamientoLinea> ObtenerListaLanzanmientos(DataSet excelDataSet)
        {
            List<LanzamientoLinea> listaLanzamiento = new List<LanzamientoLinea>();
            foreach (DataRow dr in excelDataSet.Tables[0].Rows)
            {

                foreach (var colValue in dr.ItemArray)
                {
                    if (!string.IsNullOrEmpty(colValue.ToString()))
                    {
                        LanzamientoLinea lanzl = new LanzamientoLinea();
                        lanzl.GuardarInfoDeDataRow(dr);
                        listaLanzamiento.Add(lanzl);
                        break;
                    }
                }
            }
            return listaLanzamiento;
        }

        private static DataSet GenerarHojaLanzamientoNueva(List<LanzamientoLinea> listaLanzamiento)
        {
            DataSet resultDataSet = new DataSet();
            resultDataSet.Tables.Add(new DataTable());
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "ID_Ord");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "ID_Lanz");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "REFERENCIA");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "ORDEN");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "CLIENTE");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "PRODUCTO");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "CAJAS");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "FORMATO");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "PA");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "REF.");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "GDO.");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "TIPO");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "COMENTARIOS");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "LÍQUIDOS");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "OBSERVACIONES");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "MATERIALES");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "ESTADO");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "FECHA INICIO (doble click)");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "OBSERVACIONES PRODUCCIÓN");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "ESTADO2");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "Fecha");

            foreach (LanzamientoLinea linea in listaLanzamiento)
            {
                resultDataSet.Tables[0].Rows.Add(linea.ToDataRow().ItemArray);
            }
            return resultDataSet;
        }

    }
}

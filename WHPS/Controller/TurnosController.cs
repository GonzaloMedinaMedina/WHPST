using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.Utiles;

namespace WHPS.Controller
{
    class TurnosController
    {
        public static string GetTurnoActual()
        {
            string result = "";
            //Rellenamos el turno - Identificando el turno
            int hora = Convert.ToInt16(DateTime.Now.ToString("HH"));
            if (hora >= 7 && hora < 15)
            {
                result = "Mañana";
            }
            else
            {
                if (hora >= 15 && hora < 23)
                {
                    result = "Tarde";
                }
                else { result = "Noche"; }
            }
            return result;
        }

        public static string GetTurnoAnterior()
        {
            string turnoAnterior = "Mañana";
            switch (MaquinaLinea.turno)
            {
                case "Mañana":
                    turnoAnterior = "Noche";
                    break;
                case "Tarde":
                    turnoAnterior = "Mañana";
                    break;
                case "Noche":
                    turnoAnterior = "Tarde";
                    break;
                default:
                    turnoAnterior = "Mañana";
                    break;
            }
            return turnoAnterior;
        }

        //Rellenamos el turno - Identificando el turno
        public static bool ComprobarTurno(string numeroLinea)
        {
            string turnoActual = "";
            int diaC = Convert.ToInt16(DateTime.Now.ToString("dd"));
            int hora = Convert.ToInt16(DateTime.Now.ToString("HH"));
            if (hora >= 7 && hora < 15)
            {
                turnoActual = "Mañana";
            }
            else if (hora >= 15 && hora < 23)
            {
                turnoActual = "Tarde";
            }
            else
            {
                turnoActual = "Noche";
            }

            if ((turnoActual != MaquinaLinea.turno) || ((numeroLinea == "2") && (!MaquinaLinea.checkL2)) || ((numeroLinea == "3") && (!MaquinaLinea.checkL3)) || ((numeroLinea == "5") && (!MaquinaLinea.checkL5)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Leer el fichero excel.
        /// Ejemplo "SELECT tipo_botella FROM [hoja3$] WHERE linea = 3;";
        /// </summary>
        /// <param name="claveMaquina">Clave con la que se identifica el fichero en el que escribir las nuevas líneas</param>
        /// <param name="hoja">Nombre de la hoja que se quiere leer</param>
        /// <param name="campos">Lista con los nombres de columnas que se quieren obtener en la lectura. En caso de no especificar ninguno este se traerá todos</param>
        /// <param name="valoresAFiltrar">Lista de valores a filtrar: Condicion (AND, OR), nombre de la columna, valor a filtrar. Ejemplo: ['AND', 'Tipo Botella','=', '1,75'] </param>
        public static DataSet ObtenerUltimosMovimientosTurnos(string claveMaquina, string nombreHoja, out string result)
        {
            result = "";
            DataSet excelDataSet = new DataSet();

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
                        if (!GetTurnoAnterior().Equals("Noche"))
                        {
                            fechaAnterior = DateTime.Now.AddDays(-1);
                        }
                        if (nombreHoja == null)
                        {
                            nombreHoja = claveMaquina;
                        }

                        string cmdText = "SELECT TOP " + MaquinaLinea.MovimientosTurnoAnterior + " * FROM [" + nombreHoja + "$] Where Turno LIKE '%" + GetTurnoAnterior() + "%' AND FECHA LIKE '" + fechaAnterior.ToShortDateString() + "' ORDER BY HORA DESC";
                        cmdText += " UNION ALL SELECT * FROM [" + nombreHoja + "$] Where Turno LIKE '%" + MaquinaLinea.turno + "%' AND FECHA LIKE '" + DateTime.Now.ToShortDateString() + "' ORDER BY HORA DESC";
                        cmdText = Properties.Settings.Default.TipoConexion == "SQL" ? cmdText.Replace("[", "").Replace("$]", "") : cmdText;
                        cmd.CommandText = cmdText;

                        DataTable dt = new DataTable();
                        dt.TableName = nombreHoja;

                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(dt);

                        excelDataSet.Tables.Add(dt);

                        cmd = null;
                        conn.Close();
                    }

                    // Cadena devuelta para mostrar datos del resultado
                    result += Environment.NewLine + excelDataSet.Tables[0].Rows.Count + " filas encontradas";
                    //Debug.Print(result);
                    return excelDataSet;
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.StackTrace);
                    result = Environment.NewLine + "ERROR:" + Environment.NewLine + ex.Message + Environment.NewLine;
                    if (!ErrorLog.ErrorRoutine(false, ex))
                    {
                        MessageBox.Show("Unable to write a log");
                    }
                    return excelDataSet;
                }
            }
            result += Environment.NewLine + "OK";
            return excelDataSet;
        }

    }
}
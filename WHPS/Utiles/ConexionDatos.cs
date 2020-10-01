using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Controller;
using WHPS.Model;

namespace WHPS.Utiles
{
    public class ConexionDatos
    {
        //static string filename = Properties.Settings.Default.RutaExcel + Properties.Settings.Default.NombreExcel;
        //string connStr = string.Format(@"Provider = Microsoft.ACE.OLEDB.12.0;Data Source = { 0 }; Extended Properties = ""Text;HDR=YES;IMEX=2;FMT=Delimited""", Path.GetDirectoryName(filename));
        //string extensionFichero = "xls";

        /// <summary>
        /// Obtenemos el fichero donde se guardan las claves y buscamos la clave para obtener la ruta del fichero que necesitamos.
        /// </summary>
        /// <param name="clave">clave que identifica una ruta de fichero.</param>
        /// <returns>Devuelve la ruta del fichero al que pertenece la clave insertada por parámetro</returns>
        public static string ObtenerFicheroClave(string clave)
        {
            string filename = Properties.Settings.Default.RutaFichero + Properties.Settings.Default.NombreFichero;
            string rutaClave = "";
            if (clave != null && clave.Length > 0)
            {
                string[] lines = System.IO.File.ReadAllLines(filename);
                if (lines.Length > 0)
                {
                    // Línea 0 para la cabecera. Empezamos en la línea 11
                    Dictionary<string, string> ficheroClaves = new Dictionary<string, string>();
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] clavesValores = lines[i].Split(',');
                        if (clavesValores[0] != null)
                        {
                            ficheroClaves.Add(clavesValores[0], clavesValores[1]);
                        }
                    }
                    ficheroClaves.TryGetValue(clave, out rutaClave);
                }
            }
            return rutaClave;
        }

        /// <summary>
        /// Obtener la extensión del fichero
        /// </summary>
        /// <param name="rutaFichero">Ruta en la que se encuentra el fichero del que se va a obtener su extensión</param>
        /// <returns>Devuelve la extension del fichero leido</returns>
        public static string ComprobarExtension(string rutaFichero)
        {
            string extensionFichero = "xls";
            try
            {
                FileInfo fi = new FileInfo(rutaFichero);
                Debug.Print("ComprobarExtension");
                extensionFichero = fi.Extension;
                return extensionFichero;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);

                if (!ErrorLog.ErrorRoutine(false, ex))
                {
                    MessageBox.Show("Unable to write a log");
                }
                return ".xlsx";
                // exception here
            }
        }

        /// <summary>
        /// Construir cadena de conexión según la extensión del fichero
        /// </summary>
        /// <param name="rutaFichero">Ruta en la que se encuentra el fichero del que se va a obtener la cadena de conexión</param>
        /// <param name="extensionFichero">Extensión del fichero empleada para la cadena de conexión</param>
        /// <returns>Devuelve la cadena de conexion para el fichero de la ruta asignada por parámetro</returns>
        public static string CrearStrConexion(string rutaFichero, string extensionFichero)
        {
            string connStr = "";
            //string pathFichero = Path.GetFullPath(rutaFichero);
            switch (extensionFichero)
            {
                case ".xls":
                    // XLS
                    connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaFichero + ";Extended Properties=\"Excel 8.0;\"";
                    break;
                case ".xlsx":
                    // XLSX
                    //connStr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Text;HDR=YES;IMEX=2;FMT=Delimited""", Path.GetDirectoryName(filename));
                    connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaFichero + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1;\"";
                    //connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaFichero + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"";
                    break;
                default:
                    // CSV
                    connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaFichero + ";Extended Properties =\"Text;HDR=YES;IMEX=2;FMT=Delimited\"";
                    break;
            }
            connStr = connStr.Replace(@"\\", @"\");
            return @connStr;
        }

        /// <summary>
        /// Inicia los datos de conexión a partir de una clave.
        /// </summary>
        /// <param name="claveMaquina">Clave unica con la que se puede localizar el fichero empleado como Base de Datos</param>
        /// <returns>Devuelve la cadena de conexión generada para dicha clave</returns>
        public static string iniciaDatosConn(string claveMaquina)
        {
            string rutaFichero = ObtenerFicheroClave(claveMaquina);
            //MessageBox.Show(rutaFichero);
            string extensionFichero = ComprobarExtension(rutaFichero);
            string connStr = CrearStrConexion(rutaFichero, extensionFichero);
            return connStr;
        }

        /// <summary>
        /// Inicia los datos de conexión a partir de una clave.
        /// </summary>
        /// <param name="claveMaquina">Clave unica con la que se puede localizar el fichero empleado como Base de Datos</param>
        /// <returns>Devuelve la cadena de conexión generada para dicha clave</returns>
        public static string iniciaDatosConnSQL()
        {
            string connStr = "Provider=SQLNCLI11;Server=PC-ITC3\\SQLEXPRESS;Database=WHS;Trusted_Connection=yes;";
            return connStr;
        }

        /// <summary>
        /// Leer el fichero excel.
        /// Ejemplo "SELECT tipo_botella FROM [hoja3$] WHERE linea = 3;";
        /// </summary>
        /// <param name="claveMaquina">Clave con la que se identifica el fichero en el que escribir las nuevas líneas</param>
        /// <param name="nombreTabla">Nombre de la hoja que se quiere leer</param>
        /// <param name="campos">Lista con los nombres de columnas que se quieren obtener en la lectura. En caso de no especificar ninguno este se traerá todos</param>
        /// <param name="valoresAFiltrar">Lista de valores a filtrar: Condicion (AND, OR), nombre de la columna, valor a filtrar. Ejemplo: ['AND', 'Tipo Botella','=', '1,75'] </param>
        public static DataSet LeerDatos(string claveMaquina, string nombreTabla, string[] campos, List<string[]> valoresAFiltrar, out string result)
        {
            string strSelect = "SELECT ";
            result = "";
            DataSet excelDataSet = new DataSet();

            // Comprobación del valor de la hoja
            if (nombreTabla != null && nombreTabla != "")
            {
                // Comprobación de campos
                if (campos != null && campos.Length > 0)
                {
                    // Concatenamos los campos a seleccionar
                    foreach (string campo in campos)
                    {
                        strSelect += campo + ",";
                    }

                    // Comprobamos que el campo tiene valor. Si esta vacío nos traemos todos '*'
                    if (campos[0] == "")
                    {
                        strSelect += "*";
                    }
                }
                else
                {
                    strSelect += "*";
                }

                strSelect += Properties.Settings.Default.TipoConexion == "SQL" ? " FROM " + nombreTabla + "" : " FROM [" + nombreTabla + "$]";
                strSelect = strSelect.Replace(", FROM", " FROM");
                strSelect = strSelect.Replace(",*", "*");

                // Llamamos a la funcion para generar el Where
                strSelect += generarWhere(valoresAFiltrar);
                strSelect = strSelect.Replace(", WHERE", " WHERE");
                Debug.Print("########### LeerDatos: " + strSelect);
                // Obtenemos la cadena de conexión según la clave de la máquina
                string connStr = Properties.Settings.Default.TipoConexion == "SQL" ? iniciaDatosConnSQL() : iniciaDatosConn(claveMaquina);
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connStr))
                    {
                        conn.Open();
                        // Obtenemos el resultado
                        OleDbDataAdapter objDA = new OleDbDataAdapter(strSelect, conn);
                        result += Environment.NewLine + "--SELECT:" + Environment.NewLine + strSelect + Environment.NewLine;
                        // Pasamos el resultado a un DataSet para su uso posterior por el DataGridView del form
                        objDA.Fill(excelDataSet);
                    }
                    // Cadena devuelta para mostrar datos del resultado
                    result += Environment.NewLine + excelDataSet.Tables[0].Rows.Count + " filas encontradas";
                    return excelDataSet;
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
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

        /// <summary>
        /// Función para realizar la inserción de lineas en la hoja excel.
        /// Ejemplo "UPDATE [hoja3$] SET pedido = 'PE12345' WHERE linea = 3;";
        /// </summary>
        /// <param name="claveMaquina">Clave con la que se identifica el fichero en el que escribir las nuevas líneas</param>
        /// <param name="nombreTabla">Nombre de la hoja en la que insertar las lineas nuevas</param>
        /// <param name="valoresAIntroducir">Lista de pareja de valores a introducir: nombre de la columna, valor de la columna</param>
        /// <param name="nombreIdentificador">Nombre de la columna con la que se identifica la tabla. Tiene que ser un entero</param>
        public static string EscribirDatos(string claveMaquina, string nombreTabla, List<string[]> valoresAIntroducir, string nombreIdentificador)
        {
            string result = "";
            int numInserted = 0;
            if (nombreTabla != null && nombreTabla != "")
            {
                string commToInsert = "";
                string colNames = "";
                string cellValue = "";
                int identificador = -1;
                // Obtenemos la cadena de conexión según la clave de la máquina
                string connStr = Properties.Settings.Default.TipoConexion == "SQL" ? iniciaDatosConnSQL() : iniciaDatosConn(claveMaquina);
                //MessageBox.Show(connStr);

                // Comprobamos que existe identificador
                if (nombreIdentificador != null && nombreIdentificador.Length > 0)
                {
                    try
                    {
                        using (OleDbConnection conn = new OleDbConnection(connStr))
                        {
                            conn.Open();
                            // Consulta que obtiene una fila con el mayor número de identificador
                            string strSelect = "SELECT TOP 1 MAX(" + nombreIdentificador + ") FROM ";
                            strSelect += Properties.Settings.Default.TipoConexion == "SQL" ? nombreTabla : "[" + nombreTabla + "$]";

                            OleDbDataAdapter objDA = new OleDbDataAdapter(strSelect, conn);
                            DataSet excelDataSet = new DataSet();
                            objDA.Fill(excelDataSet);
                            // Obtenemos el valor del último identificador
                            DataRow ultimaFila = (DataRow)excelDataSet.Tables[0].Rows[0];
                            // Obtenemos el siguiente valor de identificador
                            identificador = Convert.ToInt32(ultimaFila.ItemArray[0]) + 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        result += Environment.NewLine + "ERROR DE ID:" + Environment.NewLine + ex.Message;
                        if (!ErrorLog.ErrorRoutine(false, ex))
                        {
                            MessageBox.Show("Unable to write a log");
                        }
                        return null;
                    }
                }
                // Generamos la consulta para realizar el insert
                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.Connection = conn;
                        commToInsert += "Insert into [" + nombreTabla + "$] (";
                        colNames = "";
                        cellValue = "";
                        // obtenemos los datos en variables separadas de nombre y valor
                        foreach (string[] strList in valoresAIntroducir)
                        {
                            // Comprobamos que el campo a introducir no está vacio
                            if (strList[0] != "")
                            {
                                colNames += "[" + strList[0] + "],";
                                cellValue += "'" + strList[1] + "',";
                            }
                        }
                        // Concatenamos los valores anteriores
                        commToInsert += colNames + ") VALUES (" + cellValue + ");";

                        // Si existe identificador lo concatenamos a la consulta en último lugar
                        if (identificador != -1)
                        {
                            commToInsert = commToInsert.Replace(") VALUES (", "[" + nombreIdentificador + "]) VALUES (");
                            commToInsert = commToInsert.Replace(");", identificador.ToString() + ");");
                        }

                        // Ejecutamos la consulta y mostramos los valores en el cuadro de salida
                        cmd.CommandText = commToInsert;
                        //Debug.Print("QUERY:" + commToInsert);
                        result += Environment.NewLine + "--INSERT:" + Environment.NewLine + commToInsert + Environment.NewLine;
                        numInserted = cmd.ExecuteNonQuery();
                        result += Environment.NewLine + "Insertadas " + numInserted + " filas";
                    }
                    catch (Exception ex)
                    {
                        //exception here
                        //Debug.Print(ex.Message + Environment.NewLine + ex.StackTrace);
                        result += Environment.NewLine + "ERROR EN INSERT:" + Environment.NewLine + ex.Message;
                        if (!ErrorLog.ErrorRoutine(false, ex))
                        {
                            MessageBox.Show("Unable to write a log");
                        }
                        return null;
                    }
                    finally
                    {
                        // Cerramos conexión y liberamos recursos
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Función que actualiza los valores del excel
        /// Ejemplo "UPDATE [hoja3$] SET pedido = 'PE12345' WHERE linea = 3;";
        /// </summary>
        /// <param name="claveMaquina">Clave con la que se identifica el fichero en el que escribir las nuevas líneas</param>
        /// <param name="nombreTabla">Nombre de la hoja en la que realizar los cambios</param>
        /// <param name="valoresAActualizar">Lista de pareja de valores a actualizar: nombre de la columna, valor de la columna</param>
        /// <param name="valoresAFiltrar">Lista de valores a filtrar: Condicion (AND, OR), nombre de la columna, valor a filtrar. Ejemplo: ['AND', 'Tipo Botella'. '1,75'] </param>
        public static string ActualizarDatos(string claveMaquina, string nombreTabla, List<string[]> valoresAActualizar, List<string[]> valoresAFiltrar)
        {
            string result = "";
            int numUpdated = 0;
            if (nombreTabla != null && nombreTabla != "")
            {
                // Obtenemos la cadena de conexión según la clave de la máquina
                //string connStr = iniciaDatosConn(claveMaquina);
                string connStr = Properties.Settings.Default.TipoConexion == "SQL" ? iniciaDatosConnSQL() : iniciaDatosConn(claveMaquina);
                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.Connection = conn;

                        string commToUpdate = "";
                        string filtroWhere = "";
                        string updateValue = "";
                        filtroWhere = generarWhere(valoresAFiltrar);

                        if (valoresAActualizar != null && valoresAActualizar.Count > 0)
                        {
                            commToUpdate += Properties.Settings.Default.TipoConexion == "SQL" ? "UPDATE " + nombreTabla + " SET " : "UPDATE [" + nombreTabla + "$] SET ";
                            foreach (string[] strList in valoresAActualizar)
                            {
                                if (strList[0] != null && strList[0] != "")
                                {
                                    updateValue += "" + strList[0] + "= '" + strList[1] + "',";
                                }
                            }
                            commToUpdate += updateValue + filtroWhere + ";";
                            commToUpdate = commToUpdate.Replace(", WHERE", " WHERE");
                            cmd.CommandText = commToUpdate;
                            result += Environment.NewLine + "--UPDATE:" + Environment.NewLine + commToUpdate + Environment.NewLine;
                            numUpdated = cmd.ExecuteNonQuery();
                        }
                        result += Environment.NewLine + "Actualizadas " + numUpdated + " filas";
                    }
                    catch (Exception ex)
                    {
                        //exception here
                        result += Environment.NewLine + "ERROR:" + Environment.NewLine + ex.StackTrace;
                        if (!ErrorLog.ErrorRoutine(false, ex))
                        {
                            MessageBox.Show("Unable to write a log");
                        }
                        return null;
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            return result;
        }

        public static string generarWhere(List<string[]> valoresAFiltrar)
        {
            string filtroWhere = "";
            int contWhere = 0;
            if (valoresAFiltrar != null && valoresAFiltrar.Count > 0)
            {
                filtroWhere = " WHERE ";
                foreach (string[] strList in valoresAFiltrar)
                {
                    if (strList[1] != null && strList[1] != "")
                    {
                        if (contWhere != 0)
                        {
                            filtroWhere += " " + strList[0] + " " + strList[1] + " " + strList[2] + " " + strList[3];
                        }
                        else
                        {
                            filtroWhere += " " + strList[1] + " " + strList[2] + " " + strList[3];
                        }
                        contWhere += 1;
                    }
                }
            }
            if (contWhere == 0)
            {
                return "";
            }
            else
            {
                return filtroWhere;
            }
        }

        private static void PrintTableOrView(DataTable table, string label)
        {
            System.IO.StringWriter sw;
            string output;

            Debug.WriteLine(label);

            // Loop through each row in the table.
            foreach (DataRow row in table.Rows)
            {
                sw = new StringWriter();

                // Loop through each column.
                foreach (DataColumn col in table.Columns)
                {
                    // Output the value of each column's data.
                    sw.Write(row[col].ToString() + ", ");
                }
                output = sw.ToString();

                // Trim off the trailing ", ", so the output looks correct.
                if (output.Length > 2)
                {
                    output = output.Substring(0, output.Length - 2);
                }
                // Display the row in the console window.
                Debug.WriteLine(output);
            }
            Debug.WriteLine("----");
        }


    }
}

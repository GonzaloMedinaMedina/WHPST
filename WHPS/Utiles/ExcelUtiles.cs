using ClosedXML.Excel;
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
using WHPS.Model;

namespace WHPS.Utiles
{
    public class ExcelUtiles
    {
        //static string filename = Properties.Settings.Default.RutaExcel + Properties.Settings.Default.NombreExcel;
        //string connStr = string.Format(@"Provider = Microsoft.ACE.OLEDB.12.0;Data Source = { 0 }; Extended Properties = ""Text;HDR=YES;IMEX=2;FMT=Delimited""", Path.GetDirectoryName(filename));
        //string extensionFichero = "xls";

        //######################### FUNCIONES INTERNAS #########################
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
                        if(clavesValores[0] != null) {
                            ficheroClaves.Add(clavesValores[0], clavesValores[1]);
                        }
                    }
                    ficheroClaves.TryGetValue(clave, out rutaClave);
                }
            }
            return rutaClave;
        }

        /// <summary>
        /// Obtener la extensión del fichero.
        /// </summary>
        /// <param name="rutaFichero">Ruta en la que se encuentra el fichero del que se va a obtener su extensión</param>
        /// <returns>Devuelve la extension del fichero leido</returns>
        public static string ComprobarExtension(string rutaFichero)
        {
            string extensionFichero = "xls";
            try
            {
                FileInfo fi = new FileInfo(rutaFichero);
                extensionFichero = fi.Extension;
                return extensionFichero;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return "xls";
                // exception here
            }
        }

        /// <summary>
        /// Construir cadena de conexión según la extensión del fichero.
        /// </summary>
        /// <param name="rutaFichero">Ruta en la que se encuentra el fichero del que se va a obtener la cadena de conexión</param>
        /// <param name="extensionFichero">Extensión del fichero empleada para la cadena de conexión</param>
        /// <returns>Devuelve la cadena de conexion para el fichero de la ruta asignada por parámetro</returns>
        public static string CrearStrConexion(string rutaFichero, string extensionFichero)
        {
            string connStr = "";
            switch (extensionFichero)
            {
                case ".xls":
                    // XLS
                    connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaFichero + ";Extended Properties=\"Excel 8.0;\"";
                    break;
                case ".xlsx":
                    // XLSX
                    //connStr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Text;HDR=YES;IMEX=2;FMT=Delimited""", Path.GetDirectoryName(filename));
                    connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaFichero + ";Extended Properties=\"Excel 12.0;\"";
                    break;
                default:
                    // CSV
                    connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaFichero + ";Extended Properties =\"Text;HDR=YES;IMEX=2;FMT=Delimited\"";
                    break;
            }
            return connStr;
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
        public static string iniciaDatosConnLocal(string claveMaquina)
        {
            string rutaFichero = ExcelUtiles.ObtenerFicheroClave(claveMaquina);
            string nombreFichero = Path.GetFileName(rutaFichero);
            string rutaLocal = ExcelUtiles.ObtenerFicheroClave("Ruta_Local");
            

            //MessageBox.Show(rutaFichero);
            string extensionFichero = ExcelUtiles.ComprobarExtension(rutaFichero);
            string rutaFicheroLocal = rutaLocal + nombreFichero;
            FileInfo fiOrigen = new FileInfo(rutaFichero);
            try
            {
                if (!Directory.Exists(rutaLocal))
                {
                    Directory.CreateDirectory(rutaLocal);
                }

                if (File.Exists(rutaFicheroLocal))
                {
                    FileInfo fiCopia = new FileInfo(rutaFicheroLocal);
                    if (fiCopia.LastWriteTime != fiOrigen.LastWriteTime)
                    {
                        File.Copy(rutaFichero, rutaFicheroLocal,true);
                    }
                }
                else
                {
                    File.Copy(rutaFichero, rutaFicheroLocal,true);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                Debug.Print(ex.StackTrace);
                //throw;
            }
            string connStr = CrearStrConexion(rutaFicheroLocal, extensionFichero);
            return connStr;
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



        //######################### FUNCIONES EXPORTADAS #########################
        /// <summary>
        /// Leer el fichero excel.
        /// Ejemplo "SELECT tipo_botella FROM [hoja3$] WHERE linea = 3;";
        /// </summary>
        /// <param name="claveMaquina">Clave con la que se identifica el fichero en el que escribir las nuevas líneas</param>
        /// <param name="hoja">Nombre de la hoja que se quiere leer</param>
        /// <param name="campos">Lista con los nombres de columnas que se quieren obtener en la lectura. En caso de no especificar ninguno este se traerá todos</param>
        /// <param name="valoresAFiltrar">Lista de valores a filtrar: Condicion (AND, OR), nombre de la columna, valor a filtrar. Ejemplo: ['AND', 'Tipo Botella','=', '1,75'] </param>
        public static DataSet LeerFicheroExcel(string claveMaquina, string hoja, string[] campos, List<string[]> valoresAFiltrar, out string result)
        {
            string strSelect = "SELECT ";
            result = "";
            DataSet excelDataSet = new DataSet();

            // Comprobación del valor de la hoja
            if (hoja != null && hoja != "")
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

                strSelect += " FROM [" + hoja + "$]";
                strSelect = strSelect.Replace(", FROM", " FROM");
                strSelect = strSelect.Replace(",*", "*");
                
                // Llamamos a la funcion para generar el Where
                strSelect += generarWhere(valoresAFiltrar);
                strSelect = strSelect.Replace(", WHERE", " WHERE");
                Debug.Print("########### " + strSelect);
                // Obtenemos la cadena de conexión según la clave de la máquina
                string connStr = iniciaDatosConnLocal(claveMaquina);
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connStr))
                    {
                        conn.Open();
                        // Obtenemos el resultado
                        OleDbDataAdapter objDA = new OleDbDataAdapter(strSelect, conn);
                        result += Environment.NewLine +"--SELECT:" + Environment.NewLine + strSelect + Environment.NewLine;
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
                    return excelDataSet;
                }
            }
            result += Environment.NewLine + "OK";
            return excelDataSet;
        }

        internal static void CrearTablaLanzamientosPrecinta(DataGridView gdv)
        {
            string nombreUltimaPeticion = string.Empty;
            DateTime fechaPeticion = DateTime.Now.AddMinutes(-1);
            string nombreHoja = "Lanzador";
            if ((fechaUltimaPeticion == null || nombreUltimaPeticion == string.Empty) || (fechaUltimaPeticion <= fechaPeticion || nombreUltimaPeticion != nombreHoja))
            {
                string result = "";
                MaquinaLinea.FileLanzador = "DB_L" + MaquinaLinea.numlin.ToString();

                bool estadofile = ExcelUtiles.CopiaFile(MaquinaLinea.FileLanzador);
                //LINEA 2
                if ((MaquinaLinea.numlin == 2))
                {
                    if (estadofile)
                    {
                        LanzamientoLinea.DBL2_Precinta = ExcelUtiles.ObtenerUltimosMovimientosPrecinta(MaquinaLinea.FileLanzador, "Linea " + MaquinaLinea.numlin, out result);
                    }
                    
                        if (LanzamientoLinea.DBL2_Precinta != null && LanzamientoLinea.DBL2_Precinta.Tables != null && LanzamientoLinea.DBL2.Tables.Count > 0)
                        {
                            gdv.DataSource = LanzamientoLinea.DBL2_Precinta.Tables[0];
                        }
                    
                }
                //LINEA 3
                if ((MaquinaLinea.numlin == 3))
                {
                    if (estadofile)
                    {
                        LanzamientoLinea.DBL3_Precinta = ExcelUtiles.ObtenerUltimosMovimientosPrecinta(MaquinaLinea.FileLanzador, "Linea " + MaquinaLinea.numlin, out result);
                    }
                    if (LanzamientoLinea.DBL3_Precinta != null && LanzamientoLinea.DBL3_Precinta.Tables != null && LanzamientoLinea.DBL3_Precinta.Tables.Count > 0)
                    {
                      gdv.DataSource = LanzamientoLinea.DBL3_Precinta.Tables[0];
                    }
                    
                    
                }
                //LINEA 5
                if ((MaquinaLinea.numlin == 5))
                {
                    if (estadofile && (MaquinaLinea.numlin == 5))
                    {
                        LanzamientoLinea.DBL5_Precinta = ExcelUtiles.ObtenerUltimosMovimientosPrecinta(MaquinaLinea.FileLanzador, "Linea " + MaquinaLinea.numlin, out result);
                    }
                    if (LanzamientoLinea.DBL5_Precinta != null && LanzamientoLinea.DBL5_Precinta.Tables != null && LanzamientoLinea.DBL5_Precinta.Tables.Count > 0)
                    {
                        gdv.DataSource = LanzamientoLinea.DBL5_Precinta.Tables[0];
                    }
                    
                    
                }        
                      
                //Parametrización de las tablas
                //gdv.ReadOnly = true;
                //gdv.Dock = DockStyle.Fill;
                //gdv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //gdv.MultiSelect = false;
                //dgvInfoMovimientos.AutoResizeColumns();
                //gdv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

                //Se ocultan las columnas no necesarias
                gdv.Columns["ID"].Visible = false;
                gdv.Columns["ID Lanz"].Visible = false;
                gdv.Columns["PA"].Visible = false;
                gdv.Columns["REF."].Visible = false;
                gdv.Columns["GDO."].Visible = false;
                gdv.Columns["TIPO"].Visible = false;
                gdv.Columns["OBSERVACIONES LAB"].Visible = false;
                gdv.Columns["OBSERVACIONES PROD."].Visible = false;
                gdv.Columns["ESTADO EXP"].Visible = false;
                gdv.Columns["FECHA EXP"].Visible = false;
                gdv.Columns["Comentarios"].Visible = false;
                if (gdv.Name == "dgvEncajonadora") gdv.Columns["PA"].Visible = true;



                //gdv.Columns["Estado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                //gdv.Columns["Orden"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                //gdv.Columns["Formato"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //gdv.Columns["Cajas"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                gdv.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                gdv.Columns["Cliente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //gdv.Columns["Estado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                //gdv.Columns["Fecha Inicio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //gdv.RowHeadersVisible = false;
                fechaUltimaPeticion = DateTime.Now;
                nombreUltimaPeticion = nombreHoja;
            }
        }

        internal static DataSet ObtenerUltimosMovimientosPrecinta(string claveMaquina, string nombreHoja, out string result)
        {
            result = "";
            DataSet resultDataSet = new DataSet();
            DataSet excelDataSet = new DataSet();
            List<LanzamientoLinea> listaLineasLanzamiento = new List<LanzamientoLinea>();
            List<LanzamientoLinea> listaLineasLanzamientoTemp = new List<LanzamientoLinea>();
            // Comprobación del valor de la claveMaquina
            if (claveMaquina != null && claveMaquina != "")
            {
                string connStr = iniciaDatosConnLocal(claveMaquina);
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

                        string cmdText = "SELECT * FROM [" + nombreHoja + "$]";
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
                        listaLineasLanzamientoTemp.Reverse();
                        foreach (LanzamientoLinea linea in listaLineasLanzamientoTemp)
                        {
                            if (!linea.iDOrd.Equals(""))
                            {
                                if ((linea.estado.ToUpper().Equals("INICIADO")))
                                {
                                    listaLineasLanzamiento.Add(linea);
                                }
                                
                            }
                        }



                        listaLineasLanzamiento.Reverse();
                        listaLineasLanzamientoTemp.Reverse();
                        foreach (LanzamientoLinea linea in listaLineasLanzamientoTemp)
                        {
                            if (!linea.iDOrd.Equals(""))
                            {
                                if (!linea.estado.ToUpper().Equals("INICIADO"))
                                {
                                    //listaLineasLanzamiento.Add(linea);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Debug.Print(ex.Message);
                        Debug.Print(ex.StackTrace);
                    }
                    //Debug.Print(result);
                    resultDataSet = GenerarHojaLanzamientoNueva(listaLineasLanzamiento);
                    return resultDataSet;
                }
                catch (Exception ex)
                {
                    //Debug.Print(ex.Message);
                    Debug.Print(ex.StackTrace);
                    result = Environment.NewLine + "ERROR:" + Environment.NewLine + ex.Message + Environment.NewLine;
                    return resultDataSet;
                }
            }
            result += Environment.NewLine + "OK";
            return resultDataSet;
        }


        /// <summary>
        /// Función para realizar la inserción de lineas en la hoja excel.
        /// Ejemplo "UPDATE [hoja3$] SET pedido = 'PE12345' WHERE linea = 3;";
        /// </summary>
        /// <param name="claveMaquina">Clave con la que se identifica el fichero en el que escribir las nuevas líneas</param>
        /// <param name="hoja">Nombre de la hoja en la que insertar las lineas nuevas</param>
        /// <param name="valoresAIntroducir">Lista de pareja de valores a introducir: nombre de la columna, valor de la columna</param>
        /// <param name="nombreIdentificador">Nombre de la columna con la que se identifica la tabla. Tiene que ser un entero</param>
        public static string EscribirFicheroExcel(string claveMaquina, string hoja, List<string[]> valoresAIntroducir, string nombreIdentificador)
        {
            string result = "";
            int numInserted = 0;
            if (hoja != null && hoja != "")
            {
                string commToInsert = "";
                string colNames = "";
                string cellValue = "";
                int identificador = -1;
                // Obtenemos la cadena de conexión según la clave de la máquina
                string connStr = iniciaDatosConn(claveMaquina);
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
                            string strSelect = "SELECT TOP 1 MAX(" + nombreIdentificador + ") FROM [" + hoja + "$]";
                            
                            OleDbDataAdapter objDA = new OleDbDataAdapter(strSelect, conn);
                            DataSet excelDataSet = new DataSet();
                            objDA.Fill(excelDataSet);
                            // Obtenemos el valor del último identificador
                            DataRow ultimaFila = (DataRow)excelDataSet.Tables[0].Rows[0];
                            // Obtenemos el siguiente valor de identificador
                            //identificador = Convert.ToInt32(ultimaFila[nombreIdentificador].ToString()) + 1;
                            identificador = Convert.ToInt32(ultimaFila.ItemArray[0]) + 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        result += Environment.NewLine + "ERROR DE ID:" + Environment.NewLine + ex.Message;
                        throw;
                        
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

                        commToInsert += "Insert into [" + hoja + "$] (";
                        colNames = "";
                        cellValue = "";
                        // obtenemos los datos en variables separadas de nombre y valor
                        foreach (string[] strList in valoresAIntroducir)
                        {
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
        /// <param name="hoja">Nombre de la hoja en la que realizar los cambios</param>
        /// <param name="valoresAActualizar">Lista de pareja de valores a actualizar: nombre de la columna, valor de la columna</param>
        /// <param name="valoresAFiltrar">Lista de valores a filtrar: Condicion (AND, OR), nombre de la columna, valor a filtrar. Ejemplo: ['AND', 'Tipo Botella'. '1,75'] </param>
        public static string ActualizarFicheroExcel(string claveMaquina, string hoja, List<string[]> valoresAActualizar, List<string[]> valoresAFiltrar)
        {
            string result = "";
            int numUpdated = 0;
            if (hoja != null && hoja != "")
            {
                // Obtenemos la cadena de conexión según la clave de la máquina
                string connStr = iniciaDatosConn(claveMaquina);
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
                            commToUpdate += "UPDATE [" + hoja + "$] SET ";
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
                        result += Environment.NewLine + "ERROR:" + Environment.NewLine +  ex.StackTrace;
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

        public static bool ActualizarCeldaExcel(string claveMaquina, string hoja, string[] valoresAActualizar, string[] valoresAFiltrar)
        {
            string connStr = iniciaDatosConn(claveMaquina);
            //Comprobamos que el archivo esta abierto
            if (ErrorArchivoAbierto(connStr))
            {
                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand();
                        cmd = new OleDbCommand("UPDATE [" + hoja + "$A4:V104] SET " + valoresAActualizar[0] + "= " + "'" + valoresAActualizar[1] + "'" + " where " + valoresAFiltrar[1] + " " + valoresAFiltrar[2] + " " + valoresAFiltrar[3], conn);                      
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("El archivo sobre el que se quiere escribir esta abierto, avisa al responsable de línea que debe cambiar el ESTADO del producto.");
                return false;
            }
        }
        public static string InsertarLineaExcel(string claveMaquina, string hoja, string[] valoresAActualizar, string[] nombrevalores)
        {
            string result = "";
            string connStr = iniciaDatosConn(claveMaquina);
            string commandText="";
            int ncambios = 0;
            //Comprobamos que el archivo esta abierto
            if (ErrorArchivoAbierto(connStr))
            {
                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    try
                    {

                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.Connection = conn;


                        commandText += "INSERT INTO [" + hoja + "$] (";
                        for (int i = 0; i < nombrevalores.Length; i++)
                        {
                            if(i== nombrevalores.Length-1) commandText += "["+nombrevalores[i] + "]) ";
                            else commandText += "["+nombrevalores[i] + "],";
                        }

                        commandText += "VALUES (";

                        for (int i = 0; i < valoresAActualizar.Length; i++)
                        {
                            if (i == valoresAActualizar.Length - 1) commandText += "'"+valoresAActualizar[i]+"'" + ") where " + nombrevalores[0] + " = " + valoresAActualizar[0];
                            else commandText += "'"+ valoresAActualizar[i]+ "'"+ ",";
                        }
                        Console.WriteLine(commandText);
                        cmd.CommandText = commandText; 
                        ncambios=cmd.ExecuteNonQuery();
                        result += ncambios;

                    }
                    catch (Exception ex)
                    {
                        //exception here
                        result += Environment.NewLine + "ERROR:" + Environment.NewLine + ex.ToString();
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }

            }

            else
            {
                return "FALLO EN LA LECTURA DEL FICHERO";

            }
            return result;
        }


        public static int ExportDtToExcel(DataTable dt, string ficherodestino, string hoja)
        {
            // Verificamos el valor de los parámetros pasados.
            //
            if (dt == null) { return 0; }

            try {
                if (File.Exists(@ficherodestino)) File.Delete(ficherodestino);
                 
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //CREA UNA PLANTILLA LANZAMIENTO Y CAMBIA ESTA RUTA YISUS
                File.Copy("C:/Users/Gonzalo/source/repos/BD_excel/10.10.10.11/COMPARTIDAS/PRODUCCION/LANZAMIENTO/DB_LANZAMIENTO/Plantilla_Lanzamiento" + ".xlsx", ficherodestino + ".xlsx");
     

                XLWorkbook wb = new XLWorkbook(ficherodestino + ".xlsx");
                IXLWorksheet newsh = wb.Worksheets.First();

                int fila = 5;
                int columna = 1;

                for (int df = 0; df < dt.Rows.Count; df++)
                {
                    for (int dc = 0; dc < dt.Columns.Count; dc++)
                    {
                        newsh.Cell(fila, columna).Value = dt.Rows[df][dc];
                        columna++;
                    }
                    columna = 1;
                    fila++;
                }
                newsh.Name = hoja;
                wb.Save();

                return 1;
            }
            catch (Exception)
            {
                // Devolvemos la excepción al procedimiento llamador
                 throw;
            }
        }








        static public bool ErrorArchivoAbierto (string path)
        {
            try
            {
                Stream s = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None);
                s.Close();
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }
        //######################### FUNCIONES INCOMPLETAS #########################
        //public static void CrearFicheroExcel(DataGridView dgv, string claveMaquina)
        //{
        //    string connStr = iniciaDatosConn(claveMaquina);
        //    string nombreHoja = "Hoja 1";
        //    // Connect EXCEL sheet with OLEDB using connection string
        //    using (OleDbConnection conn = new OleDbConnection(connStr))
        //    {
        //        conn.Open();
        //        OleDbDataAdapter objDA = new System.Data.OleDb.OleDbDataAdapter ("select * from [Hoja1$]", conn);
        //        DataSet excelDataSet = new DataSet();
        //        objDA.Fill(excelDataSet);
        //        dgv.DataSource = excelDataSet.Tables[0];
        //    }

        //    //In above code '[Sheet1$]' is the first sheet name with '$' as default selector,
        //    // with the help of data adaptor we can load records in dataset		

        //    //write data in EXCEL sheet (Insert data)
        //    using (OleDbConnection conn = new OleDbConnection(connStr))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            OleDbCommand cmd = new OleDbCommand();
        //            cmd.Connection = conn;
        //            cmd.CommandText = @"Insert into [Sheet1$] (month,mango,apple,orange) VALUES ('DEC','40','60','80');";
        //            cmd.ExecuteNonQuery();
        //        }
        //        catch (Exception)
        //        {
        //            //exception here
        //        }
        //        finally
        //        {
        //            conn.Close();
        //            conn.Dispose();
        //        }
        //    }

        //    //update data in EXCEL sheet (update data)
        //    using (OleDbConnection conn = new OleDbConnection(connStr))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            OleDbCommand cmd = new OleDbCommand();
        //            cmd.Connection = conn;
        //            cmd.CommandText = "UPDATE [Sheet1$] SET month = 'DEC' WHERE apple = 74;";
        //            cmd.ExecuteNonQuery();
        //        }
        //        catch (Exception)
        //        {
        //            //exception here
        //        }
        //        finally
        //        {
        //            conn.Close();
        //            conn.Dispose();
        //        }
        //    }
        //}
        //public static void CrearFicheroExcel(DataGridView dgv, string claveMaquina)
        //{
        //    string connStr = iniciaDatosConn(claveMaquina);
        //    OleDbConnection conn = new OleDbConnection(connStr);
        //    DataSet DtSet = new DataSet();
        //    conn.Open();
        //    // Obtenemos el resultado
        //    OleDbDataAdapter objDA = new OleDbDataAdapter("select * from [Hoja1$]", conn);

        //    //objDA.TableMappings.Add("Table", "Net-informations.com");

        //    objDA.Fill(DtSet);
        //    dgv.DataSource = DtSet.Tables[0];
        //    conn.Close();
        //}


       






        //######################### FUNCIONES SIN REFERENCIAS #########################
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
                string connStr = iniciaDatosConn(claveMaquina);
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

                        string cmdText = "SELECT TOP " + MaquinaLinea.MovimientosTurnoAnterior + " * FROM [" + nombreHoja + "$] Where Turno LIKE '%" + GetTurnoAnterior() + "%' AND FECHA LIKE '" + fechaAnterior.ToShortDateString() + "' ORDER BY HORA DESC";
                        cmdText += " UNION ALL SELECT * FROM [" + nombreHoja + "$] Where Turno LIKE '%" + MaquinaLinea.turno + "%' AND FECHA LIKE '" + DateTime.Now.ToShortDateString() + "' ORDER BY HORA DESC";
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
                    return excelDataSet;
                }
            }
            result += Environment.NewLine + "OK";
            return excelDataSet;
        }
        internal static DataSet LeerFicheroExcel(object fileTablas, string graduacion, string[] v, List<string[]> listavalores, out string result)
        {
            throw new NotImplementedException();
        }
        public static void CargardatosExcel(string claveMaquina, string nombreHoja, DataGridView Data)
        {
            OleDbConnection conn;
            OleDbDataAdapter MyDataAdapter;
            DataTable dt;
            // Comprobación del valor de la claveMaquina

            string connStr = iniciaDatosConn(claveMaquina);
            try
            {
                conn = new OleDbConnection(connStr);
                MyDataAdapter = new OleDbDataAdapter("Select * from [" + nombreHoja + "$]", conn);
                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                Data.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

        //######################### FUNCIONES LANZAMIENTO #########################
        /// <summary>
        /// Función que obtiene el turno anterior al que esta registrado
        /// </summary>
        /// <returns>Devuelve el turno anterior al regitrado en maquinalinea.turno</returns>
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

        /// <summary>
        /// Leer el fichero excel.
        /// Ejemplo "SELECT tipo_botella FROM [hoja3$] WHERE linea = 3;";
        /// </summary>
        /// <param name="claveMaquina">Clave con la que se identifica el fichero en el que escribir las nuevas líneas</param>
        /// <param name="hoja">Nombre de la hoja que se quiere leer</param>
        /// <param name="campos">Lista con los nombres de columnas que se quieren obtener en la lectura. En caso de no especificar ninguno este se traerá todos</param>
        /// <param name="valoresAFiltrar">Lista de valores a filtrar: Condicion (AND, OR), nombre de la columna, valor a filtrar. Ejemplo: ['AND', 'Tipo Botella','=', '1,75'] </param>
        public static DataSet ObtenerUltimosMovimientosTurnos(string claveMaquina, out string result)
        {
            result = "";
            DataSet excelDataSet = new DataSet();

            // Comprobación del valor de la claveMaquina
            if (claveMaquina != null && claveMaquina != "")
            {
                string connStr = iniciaDatosConn(claveMaquina);
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connStr))
                    {
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.Connection = conn;

                        // Obtenemos todas las hojas en el excel
                        DataTable dtHoja = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                        // Iteramos en todas las hojas
                        foreach (DataRow dr in dtHoja.Rows)
                        {
                            string nombreHoja = dr["TABLE_NAME"].ToString();
                            //Debug.Print("HOJA:" + nombreHoja);
                            if (!nombreHoja.Contains("Inicio"))
                            {

                                if (!nombreHoja.EndsWith("$"))
                                    continue;

                                // Obtenemos todos los registros
                                DateTime fechaAnterior = DateTime.Now;
                                if (!GetTurnoAnterior().Equals("Noche"))
                                {
                                    fechaAnterior = DateTime.Now.AddDays(-1);
                                }
                                string cmdText = "SELECT TOP " + MaquinaLinea.MovimientosTurnoAnterior + " * FROM [" + nombreHoja + "] Where Turno LIKE '%" + GetTurnoAnterior() + "%' AND FECHA LIKE '" + fechaAnterior.ToShortDateString() + "' ORDER BY HORA DESC";
                                cmdText += " UNION ALL SELECT * FROM [" + nombreHoja + "] Where Turno LIKE '%" + MaquinaLinea.turno + "%' AND FECHA LIKE '" + DateTime.Now.ToShortDateString() + "' ORDER BY HORA DESC";
                                cmd.CommandText = cmdText;

                                DataTable dt = new DataTable();
                                dt.TableName = nombreHoja;

                                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                                da.Fill(dt);

                                excelDataSet.Tables.Add(dt);
                            }
                        }

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
                    return excelDataSet;
                }
            }
            result += Environment.NewLine + "OK";
            return excelDataSet;
        }


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
                string connStr = iniciaDatosConnLocal(claveMaquina);
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
                        
                        string cmdText = "SELECT * FROM [" + nombreHoja + "$]";
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
                            if (!linea.iDOrd.Equals(""))
                            {
                                if ((linea.estado.ToUpper().Equals("COMPLETADO") && contCompletados < MaquinaLinea.MovimientosLanzamientoAnterior))
                                {
                                    listaLineasLanzamiento.Add(linea);
                                    contCompletados++;
                                }
                                if (!linea.estado.ToUpper().Equals("COMPLETADO"))
                                {
                                    listaLineasLanzamiento.Add(linea);
                                    contCompletados = 1;
                                }
                            }
                        }



                        listaLineasLanzamiento.Reverse();
                        listaLineasLanzamientoTemp.Reverse();
                        foreach (LanzamientoLinea linea in listaLineasLanzamientoTemp)
                        {
                            if (!linea.iDOrd.Equals(""))
                            {
                                if (!linea.estado.ToUpper().Equals("COMPLETADO"))
                                {
                                    //listaLineasLanzamiento.Add(linea);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Debug.Print(ex.Message);
                        Debug.Print(ex.StackTrace);
                    }
                    //Debug.Print(result);
                    resultDataSet = GenerarHojaLanzamientoNueva(listaLineasLanzamiento);
                    return resultDataSet;
                }
                catch (Exception ex)
                {
                    //Debug.Print(ex.Message);
                    Debug.Print(ex.StackTrace);
                    result = Environment.NewLine + "ERROR:" + Environment.NewLine + ex.Message + Environment.NewLine;
                    return resultDataSet;
                }
            }
            result += Environment.NewLine + "OK";
            return resultDataSet;
        }
        internal static DataSet ObtenerUltimosMovimientosLanzadorAmd(string claveMaquina, string nombreHoja, out string result)
        {
            result = "";
            DataSet resultDataSet = new DataSet();
            DataSet excelDataSet = new DataSet();
            List<LanzamientoLinea> listaLineasLanzamiento = new List<LanzamientoLinea>();
            List<LanzamientoLinea> listaLineasLanzamientoTemp = new List<LanzamientoLinea>();
            // Comprobación del valor de la claveMaquina
            if (claveMaquina != null && claveMaquina != "")
            {
                string connStr = iniciaDatosConnLocal(claveMaquina);
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

                        string cmdText = "SELECT * FROM [" + nombreHoja + "$]";
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
                        //Buscamos todas las filas que tengan un id orden y las añadimos a la lista
                        foreach (LanzamientoLinea linea in listaLineasLanzamientoTemp)
                        {
                            if (!linea.iDOrd.Equals(""))
                            {
                                listaLineasLanzamiento.Add(linea);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Debug.Print(ex.Message);
                        Debug.Print(ex.StackTrace);
                    }
                    Debug.Print(result);
                    resultDataSet = GenerarHojaLanzamientoNueva(listaLineasLanzamiento);
                    return resultDataSet;
                }
                catch (Exception ex)
                {
                    //Debug.Print(ex.Message);
                    Debug.Print(ex.StackTrace);
                    result = Environment.NewLine + "ERROR:" + Environment.NewLine + ex.Message + Environment.NewLine;
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
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "ID");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "ID LANZ");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "CÓDIGO");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "ORDEN");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "CLIENTE");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "PRODUCTO");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "CAJAS");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "FORM.");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "PA");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "REF.");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "GDO.");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "TIPO");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "COMENTARIOS");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "LÍQUIDOS");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "OBSERVACIONES LAB");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "MATERIALES");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "ESTADO");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "FECHA INICIO");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "OBSERVACIONES PROD.");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "ESTADO EXP");
            resultDataSet.Tables[0].Columns.Add(new DataColumn().ColumnName = "FECHA EXP");
            foreach (LanzamientoLinea linea in listaLanzamiento)
            {
                resultDataSet.Tables[0].Rows.Add(linea.ToDataRow().ItemArray);
            }
            return resultDataSet;
        }
        
        /// <summary>
        /// Verificar si esta el fichero y proceder a su copia.
        /// </summary>
        /// <param name="clavefile"></param>
        /// <returns></returns>
        public static bool CopiaFile(string clavefile)
        {
            string rutaFichero = ObtenerFicheroClave(clavefile);
            string nombreFichero = Path.GetFileName(rutaFichero);
            string rutaLocal = ObtenerFicheroClave("Ruta_Local");
            bool resultado = false;
            //MessageBox.Show(rutaFichero);
            string extensionFichero = ComprobarExtension(rutaFichero);
            string rutaFicheroLocal = rutaLocal + nombreFichero;
            FileInfo fiOrigen = new FileInfo(rutaFichero);
            try
            {
                if (!Directory.Exists(rutaLocal))
                {
                    Directory.CreateDirectory(rutaLocal);
                }

                if (File.Exists(rutaFicheroLocal))
                {
                    FileInfo fiCopia = new FileInfo(rutaFicheroLocal);
                    if (fiCopia.LastWriteTime != fiOrigen.LastWriteTime)
                    {
                        File.Copy(rutaFichero, rutaFicheroLocal, true);
                        resultado = true;
                    }
                }
                else
                {
                    File.Copy(rutaFichero, rutaFicheroLocal, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                Debug.Print(ex.StackTrace);
            }
            return resultado;
        }



        static DateTime fechaUltimaPeticion;
        /// <summary>
        /// Función que muestra la tabla de lanzamiento en el DataGridView
        /// </summary>
        public static void CrearTablaLanzamientos(DataGridView gdv)
        {
            string nombreUltimaPeticion = string.Empty;
            DateTime fechaPeticion = DateTime.Now.AddMinutes(-1);
            string nombreHoja = "Lanzador";
            if ((fechaUltimaPeticion == null || nombreUltimaPeticion == string.Empty) || (fechaUltimaPeticion <= fechaPeticion || nombreUltimaPeticion != nombreHoja))
            {
                string result = "";
                MaquinaLinea.FileLanzador = "DB_L" + MaquinaLinea.numlin.ToString();

                bool estadofile = ExcelUtiles.CopiaFile(MaquinaLinea.FileLanzador);

                //LINEA 2
                if ((MaquinaLinea.numlin == 2))
                {
                    if (estadofile)
                    {
                        LanzamientoLinea.DBL2 = ExcelUtiles.ObtenerUltimosMovimientosLanzador(MaquinaLinea.FileLanzador, "Linea " + MaquinaLinea.numlin, out result);
                        if (LanzamientoLinea.DBL2 != null && LanzamientoLinea.DBL2.Tables != null && LanzamientoLinea.DBL2.Tables.Count > 0)
                        {
                            gdv.DataSource = LanzamientoLinea.DBL2.Tables[0];
                        }
                    }
                    else
                    {
                        if (LanzamientoLinea.DBL2 != null && LanzamientoLinea.DBL2.Tables != null && LanzamientoLinea.DBL2.Tables.Count > 0)
                        {
                            gdv.DataSource = LanzamientoLinea.DBL2.Tables[0];
                        }
                    }
                }
                //LINEA 3
                if ((MaquinaLinea.numlin == 3))
                {
                    if (estadofile)
                    {
                        LanzamientoLinea.DBL3 = ExcelUtiles.ObtenerUltimosMovimientosLanzador(MaquinaLinea.FileLanzador, "Linea " + MaquinaLinea.numlin, out result);
                        if (LanzamientoLinea.DBL3 != null && LanzamientoLinea.DBL3.Tables != null && LanzamientoLinea.DBL3.Tables.Count > 0)
                        {
                            gdv.DataSource = LanzamientoLinea.DBL3.Tables[0];
                        }
                    }
                    else
                    {
                        if (LanzamientoLinea.DBL3 != null && LanzamientoLinea.DBL3.Tables != null && LanzamientoLinea.DBL3.Tables.Count > 0)
                        {
                            gdv.DataSource = LanzamientoLinea.DBL3.Tables[0];
                        }
                    }
                }
                //LINEA 5
                if ((MaquinaLinea.numlin == 5))
                {
                    if (estadofile && (MaquinaLinea.numlin == 5))
                    {
                        LanzamientoLinea.DBL5 = ExcelUtiles.ObtenerUltimosMovimientosLanzador(MaquinaLinea.FileLanzador, "Linea " + MaquinaLinea.numlin, out result);
                        if (LanzamientoLinea.DBL5 != null && LanzamientoLinea.DBL5.Tables != null && LanzamientoLinea.DBL5.Tables.Count > 0)
                        {
                            gdv.DataSource = LanzamientoLinea.DBL5.Tables[0];
                        }
                    }
                    else
                    {
                        if (LanzamientoLinea.DBL5 != null && LanzamientoLinea.DBL5.Tables != null && LanzamientoLinea.DBL5.Tables.Count > 0)
                        {
                            gdv.DataSource = LanzamientoLinea.DBL5.Tables[0];
                        }
                    }
                }

                //Parametrización de las tablas
                //gdv.ReadOnly = true;
                //gdv.Dock = DockStyle.Fill;
                //gdv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //gdv.MultiSelect = false;
                //dgvInfoMovimientos.AutoResizeColumns();
                //gdv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

                //Se ocultan las columnas no necesarias
                gdv.Columns["ID"].Visible = false;
                gdv.Columns["ID LANZ"].Visible = false;
                gdv.Columns["PA"].Visible = false;
                gdv.Columns["REF."].Visible = false;
                gdv.Columns["GDO."].Visible = false;
                gdv.Columns["TIPO"].Visible = false;
                gdv.Columns["OBSERVACIONES LAB"].Visible = false;
                gdv.Columns["OBSERVACIONES PROD."].Visible = false;
                gdv.Columns["ESTADO EXP"].Visible = false;
                gdv.Columns["FECHA EXP"].Visible = false;
                gdv.Columns["Comentarios"].Visible = false;
                if(gdv.Name== "dgvEncajonadora") gdv.Columns["PA"].Visible = true;



                //gdv.Columns["Estado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                //gdv.Columns["Orden"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                //gdv.Columns["Formato"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //gdv.Columns["Cajas"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                gdv.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                gdv.Columns["Cliente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //gdv.Columns["Estado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                //gdv.Columns["Fecha Inicio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //gdv.RowHeadersVisible = false;
                fechaUltimaPeticion = DateTime.Now;
                nombreUltimaPeticion = nombreHoja;
            }
        }
        static DateTime fechaUltimaPeticionAdm;
        /// <summary>
        /// Función que muestra la tabla de lanzamiento en el DataGridView
        /// </summary>
        public static void CrearTablaLanzamientosAmd(DataGridView gdv)
        {
            string nombreUltimaPeticion = string.Empty;
            DateTime fechaUltimaPeticionAdm = DateTime.Now.AddMinutes(-1);
            string nombreHoja = "Lanzador";
            if ((fechaUltimaPeticion == null || nombreUltimaPeticion == string.Empty) || (fechaUltimaPeticion <= fechaUltimaPeticionAdm || nombreUltimaPeticion != nombreHoja))
            {
                string result = "";
                MaquinaLinea.FileLanzador = "DB_L" + MaquinaLinea.numlin.ToString();

                bool estadofile = ExcelUtiles.CopiaFile(MaquinaLinea.FileLanzador);

                //LINEA 2
                if ((MaquinaLinea.numlin == 2))
                {
                    if (estadofile)
                    {
                        LanzamientoLinea.DBL2 = ExcelUtiles.ObtenerUltimosMovimientosLanzadorAmd(MaquinaLinea.FileLanzador, "Linea " + MaquinaLinea.numlin, out result);
                        if (LanzamientoLinea.DBL2 != null && LanzamientoLinea.DBL2.Tables != null && LanzamientoLinea.DBL2.Tables.Count > 0)
                        {
                            gdv.DataSource = LanzamientoLinea.DBL2.Tables[0];
                        }
                    }
                    else
                    {
                        if (LanzamientoLinea.DBL2 != null && LanzamientoLinea.DBL2.Tables != null && LanzamientoLinea.DBL2.Tables.Count > 0)
                        {
                            gdv.DataSource = LanzamientoLinea.DBL2.Tables[0];
                        }
                    }
                }
                //LINEA 3
                if ((MaquinaLinea.numlin == 3))
                {
                    if (estadofile)
                    {
                        LanzamientoLinea.DBL3 = ExcelUtiles.ObtenerUltimosMovimientosLanzadorAmd(MaquinaLinea.FileLanzador, "Linea " + MaquinaLinea.numlin, out result);
                        if (LanzamientoLinea.DBL3 != null && LanzamientoLinea.DBL3.Tables != null && LanzamientoLinea.DBL3.Tables.Count > 0)
                        {
                            gdv.DataSource = LanzamientoLinea.DBL3.Tables[0];
                        }
                    }
                    else
                    {
                        if (LanzamientoLinea.DBL3 != null && LanzamientoLinea.DBL3.Tables != null && LanzamientoLinea.DBL3.Tables.Count > 0)
                        {
                            gdv.DataSource = LanzamientoLinea.DBL3.Tables[0];
                        }
                    }
                }
                //LINEA 5
                if ((MaquinaLinea.numlin == 5))
                {
                    if (estadofile && (MaquinaLinea.numlin == 5))
                    {
                        LanzamientoLinea.DBL5 = ExcelUtiles.ObtenerUltimosMovimientosLanzadorAmd(MaquinaLinea.FileLanzador, "Linea " + MaquinaLinea.numlin, out result);
                        if (LanzamientoLinea.DBL5 != null && LanzamientoLinea.DBL5.Tables != null && LanzamientoLinea.DBL5.Tables.Count > 0)
                        {
                            gdv.DataSource = LanzamientoLinea.DBL5.Tables[0];
                        }
                    }
                    else
                    {
                        if (LanzamientoLinea.DBL5 != null && LanzamientoLinea.DBL5.Tables != null && LanzamientoLinea.DBL5.Tables.Count > 0)
                        {
                            gdv.DataSource = LanzamientoLinea.DBL5.Tables[0];
                        }
                    }
                }

                //Parametrización de las tablas
                //gdv.ReadOnly = true;
                //gdv.Dock = DockStyle.Fill;
                //gdv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //gdv.MultiSelect = false;
                //dgvInfoMovimientos.AutoResizeColumns();
                gdv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                gdv.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                //gdv.Columns["Estado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                //gdv.Columns["Orden"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                //gdv.Columns["Formato"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //gdv.Columns["Cajas"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                gdv.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                gdv.Columns["CÓDIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                gdv.Columns["Cliente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                gdv.Columns["FORM."].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                //gdv.Columns["Estado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                gdv.Columns["PA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                gdv.Columns["REF."].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                gdv.Columns["GDO."].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                gdv.Columns["TIPO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                gdv.Columns["FECHA INICIO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                gdv.Columns["Estado EXP"].Visible = false;
                gdv.Columns["FECHA EXP"].Visible = false;
                //gdv.RowHeadersVisible = false;
                fechaUltimaPeticion = DateTime.Now;
                nombreUltimaPeticion = nombreHoja;
            }
        }
    }
}


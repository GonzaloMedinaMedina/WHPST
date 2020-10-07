using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.Utiles;
using System.IO;
using WHPS.Despaletizador;
using WHPS.Llenadora;
using WHPS.Etiquetadora;
using WHPS.Encajonadora;

namespace WHPS.ProgramMenus
{
    public partial class WHPST_BOM : Form
    {
        //puntero indica que tipo de busqueda se va a realizar (Referencia o Descripcion)
        public string puntero;
        //Parametros que guardan la ultima busqueda de Referencia o Descripcion por si es necesario volver a utilizar estos datos
        public string UltimaDescripcion;
        public string UltimaReferencia;


        TextBox TextBox;
        //Variable que indica en que ListBox se van a mostrar los archivos (Producto o Materiales)
        //public string ListBox;

        //Código de referencia de un producto
        public string CodMaterial;
        public string DescMaterial;
        //Variables que se emplean para la busqueda
        public string hoja;
        public string parametros;
        public string columnabusqueda;
        
        //Variable que determina en que modo se encuentra el boton EdicionB
        public bool EdicionBoton = true;

        //Variable que marca el número de fila que se ha seleccionado para extraer de ahi la información necesaria
        public int fila;

        public WHPST_BOM()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>
        /// <param name="BackL+numlin">Parámetro que identifica a cual form hijo de WHPST_INICIO debe volver en función del número de línea.</param>
        private void ExitB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.RetornoInicio == "BOM")
            {
                WHPST_INICIO Form = new WHPST_INICIO();
                Hide();
                Form.Show();
            }
            if (MaquinaLinea.RetornoBOM == "Despaletizador")
            {
                MaquinaLinea.RetornoBOM = "";
                MainDespaletizador Form = new MainDespaletizador();
                Hide();
                Form.Show();
            }
            if (MaquinaLinea.RetornoBOM == "Llenadora")
            {
                MaquinaLinea.RetornoBOM = "";
                MainLlenadora Form = new MainLlenadora();
                Hide();
                Form.Show();
            }
            if (MaquinaLinea.RetornoBOM == "Etiquetadora")
            {
                MaquinaLinea.RetornoBOM = "";
                MainEtiquetadora Form = new MainEtiquetadora();
                Hide();
                Form.Show();
            }
            if (MaquinaLinea.RetornoBOM == "Encajonadora")
            {
                MaquinaLinea.RetornoBOM = "";
                MainEncajonadora Form = new MainEncajonadora();
                Hide();
                Form.Show();
            }
        }
        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }


        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        private void WHPST_BOM_Load(object sender, EventArgs e)
        {
            //Muestra la hora ya que el timer tarda 1s en tomar el control del Label lbReloj.
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

 
            EdicionB.Hide();
            //Si se ingresa en el form con una referencia predefinida esta se mostrará directamente en pantalla preparada para la busqueda
            if (MaquinaLinea.ReferenciaBOM != "")
            {
                puntero = "Referencia";
                RefTB.Text = MaquinaLinea.ReferenciaBOM;
                CodMaterial = MaquinaLinea.ReferenciaBOM;

                //Se muestra los resultados de la busqueda
                CompletarTablaExcel(RefTB.Text, puntero);
                //Se expone los archivos del producto buscado si se dispone de ellos
                Utilidades.MostrarImagen(RefTB.Text,Imagen);
                MostrarListaArchivos(RefTB.Text, "Producto");
            }
            //Una vez utilizada la variable la anulamos
            MaquinaLinea.ReferenciaBOM = "";
        }

        /// <summary>
        /// Función del temporizador que se ejecuta cada segundo, indíca la hora y si se activa la alarma. 
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            //Para activar la alarma debe, estar desactivada, haberse chequeado el inicio de turno y que la hora cuadre con la alarma 
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }

        }


        //#########################      BOTONES      #########################
        /// <summary>
        /// Boton que comienza la busqueda por la referencia o la descripción en función de la variable puntero y que textbox haya sido completado
        /// </summary>
        private void BuscarB_Click(object sender, EventArgs e)
        {
            //Caso donde se utiliza para la busqueda la referencia del producto
            if (RefTB.Text != "" && puntero == "Referencia")
            {
                //Se muestra los resultados de la busqueda
                CompletarTablaExcel(RefTB.Text, puntero);
                //Se expone los archivos del producto buscado si se dispone de ellos
                Utilidades.MostrarImagen(RefTB.Text, Imagen);
                MostrarListaArchivos(RefTB.Text, "Producto");
            }
            //Caso donde se utiliza para la busqueda la descripción del producto
            if (DescripcionTB.Text != "" && puntero == "Descripcion")
            {
                //La busqueda por descripción no es exacta, por ello se muestra el boton EdicionB que permite volver a la seleccion del producto
                EdicionB.Show();
                EdicionBoton = true;
                EdicionB.Text = "MOSTRAR LA COMPOSICIÓN DEL PRODUCTO";
                //Para volver a la selección del producto empleamos la variable UltimaDescripcion
                UltimaDescripcion = DescripcionTB.Text;
                //Se restaura los archivos buscados
                DocumentacionMaterialListBox.Items.Clear();
                DocumentacionProductoListBox.Items.Clear();
                Imagen.Image = null;
                //Se muestran los resultados de la busqueda por Descripción
                CompletarTablaExcel(DescripcionTB.Text, puntero);
            }
        }
        /// <summary>
        /// Boton que solo se muestra si se realiza un busqueda por descripción y tiene 2 modos.
        /// EdicionBoton = true -> Permite realizar una busqueda por Referencia de un producto que se ha seleccionado.
        /// EdicionBoton = false -> Permite volver a la seleccin del producto final con la busqueda que se realizó anteriormente.
        /// </summary>
        /// <param name="EdicionBoton"> Variable que indica en que modo se encuentra el boton.</param>
        private void EdicionB_Click(object sender, EventArgs e)
        {
            switch (EdicionBoton)
            {
                case true:
                    //Se marca una fila y extrae el código de referencia del producto selecionado
                    SelecciónFila();
                    //Se indica que se va a realizar una busqueda por referencia y se registra que esta es la ultima busqueda
                    puntero = "Referencia";
                    UltimaReferencia = CodMaterial;
                    //Se muestra los resultados de la busqueda
                    CompletarTablaExcel(CodMaterial, puntero);
                    //Se expone los archivos del producto buscado si se dispone de ellos
                    Utilidades.MostrarImagen(CodMaterial, Imagen);
                    MostrarListaArchivos(CodMaterial, "Producto");

                    //Se entra en el modo opuesto del boton por si se necesita volver a la selección anterior
                    EdicionBoton = false;
                    EdicionB.Text = "VOLVER A LA SELECCIÓN DEL PRODUCTO";
                    break;
                case false:
                    //Se indica que se va a realizar una busqueda por descripcion y se empleará la ultima busqueda realizada
                    puntero = "Descripcion";
                    //Se restaura los archivos buscados
                    DocumentacionMaterialListBox.Items.Clear();
                    DocumentacionProductoListBox.Items.Clear();
                    Imagen.Image = null;
                    //Se muestra los resultados de la busqueda
                    CompletarTablaExcel(UltimaDescripcion, puntero);
                    //Se entra en el modo opuesto del boton por si se necesita visualizar las caracteristicas y los materiales de un producto mostrado
                    EdicionBoton = true;
                    EdicionB.Text = "MOSTRAR LA COMPOSICIÓN DEL PRODUCTO";
                    break;
            }
        }
        //#####################################################################

        //#########################      TEXTBOX      #########################
        /// <summary>
        /// Para indicar que busqueda se va arealizar necesitamos el tipo y el dato para ello se emplean los textbox donde se indica el dato de busqueda.
        /// </summary>
        private void RefTB_MouseClick(object sender, MouseEventArgs e)
        {
            DescripcionTB.Text = "";
            puntero = "Referencia";
            if (MaquinaLinea.usuario == "" || MaquinaLinea.usuario == "encargado")
            {
                WHPS.Utiles.VentanaTeclados.AbrirCalculadora(DescripcionTB);

            }
        }
        private void DescripcionTB_MouseClick(object sender, MouseEventArgs e)
        {
            RefTB.Text = "";
            puntero = "Descripcion";
            if (MaquinaLinea.usuario == "" || MaquinaLinea.usuario == "encargado")
            {
                  WHPS.Utiles.VentanaTeclados.AbrirCalculadora(RefTB);
            }
        }

        /// <summary>
        /// Se detecta cuando se ha hecho click en el DATAGRIDVIEW.
        /// </summary>
        private void dataGridViewBOM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Cuando esto sucede, se marca la fila y extrae el código de referencia del producto de la fila marcada.
            SelecciónFila();

            if (CodMaterial != "")
            {
                //Una vez obtenido el código del producto se muestran todos los archivos del mismo.
                MostrarListaArchivos(CodMaterial, "Material");
                if ((CodMaterial.Substring(0, 3) == "625" || CodMaterial.Substring(0, 2) == "68") && DescMaterial.Substring(0, 3) == "REF")
                {
                    Utilidades.MostrarImagen("liquido", Imagen);
                }
                else Utilidades.MostrarImagen(CodMaterial, Imagen);
            }
        }
        private void dataGridViewBOM_SelectionChanged(object sender, EventArgs e)
        {
            //Cuando esto sucede, se marca la fila y extrae el código de referencia del producto de la fila marcada.
            SelecciónFila();
            if (CodMaterial != "")
            {
                //Una vez obtenido el código del producto se muestran todos los archivos del mismo.
                MostrarListaArchivos(CodMaterial, "Material");
                if ((CodMaterial.Substring(0, 3) == "625" || CodMaterial.Substring(0, 2) == "68") && DescMaterial.Substring(0, 3) == "REF")
                {
                    Utilidades.MostrarImagen("liquido", Imagen);
                }
                else Utilidades.MostrarImagen(CodMaterial, Imagen);
            }
        }
        /// <summary>
        /// Los archivos mostrados se pueden abrir haciendo click encima del nombre de los mismo
        /// </summary>
        private void DocumentacionProductoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DocumentacionProductoListBox.SelectedItem.ToString() != "")
            {
                string NombrePDF = DocumentacionProductoListBox.SelectedItem.ToString();
                Process.Start(MaquinaLinea.RutaFolderBOM + NombrePDF);
            }
        }
        private void DocumentacionMaterialListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DocumentacionMaterialListBox.SelectedItem.ToString() != "")
            {
                string NombrePDF = DocumentacionMaterialListBox.SelectedItem.ToString();
                Process.Start(MaquinaLinea.RutaFolderBOM + NombrePDF);
            }
        }
        /// <summary>
        /// La imagen mostrada se puede abrir haciendo doble click encima de la misma
        /// </summary>
        private void Imagen_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaPicutreBOM + CodMaterial + ".jpg");
        }

        //#########################     FUNCIONES     #########################
        /// <summary>
        /// Función que muestra los resultados de la busqueda en el DATAGRIDVIEW.
        /// </summary>
        /// <param name="Busqueda">Variable se emplea para detectar resultados en la base de datos.</param>
        /// <param name="puntero">Variable que indica que tipo de busqueda se va a realizar en base a la Referencia o Descripción.</param>
        private void CompletarTablaExcel(string Busqueda, string puntero)
        {
            //Para realizar la busqueda se requiere el dato de busqueda dado en la función y donde se encuentra el dato que lo ofrece el parametro puntero
            if (puntero == "Referencia")
            {
                columnabusqueda = "CodProd";
                hoja = "FICHA";
                parametros = "CodMaterial;DescMaterial;Matundventa";
            }
            if (puntero == "Descripcion")
            {
                Busqueda = "'%" + Busqueda + "%'";
                columnabusqueda = "DescProd";
                hoja = "ProdFinal";
                parametros = "CodProd;DescProd";
            }
             
            //Realiza la busqueda
            List<string[]> valoresAFiltrar = new List<string[]>();
            string[] filterval = new string[4];
            filterval[0] = "AND";
            filterval[1] = columnabusqueda;
            filterval[2] = "LIKE";
            filterval[3] = Busqueda;
            valoresAFiltrar.Add(filterval);

            DataSet excelDataSet = new DataSet();
            string result;
            //List<string[]> valoresAFiltrar = dgvSelectFiltro.DataSource;
            excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileBOM, hoja, parametros.Split(';'), valoresAFiltrar, out result);
            //tbSelectSalidaError.Text = result;
            //MessageBox.Show(result);
            //Una vez realizada la busqueda si esta es correcta se modifican los parámetros de la tabla para se adecuen a las necesidades del usuario
            try
            {
                if(puntero == "Referencia")
                {
                    //Se sobreescribe el encabezado de las columnas
                    excelDataSet.Tables[0].Columns["Matundventa"].ColumnName = "QTY";
                    excelDataSet.Tables[0].Columns["CodMaterial"].ColumnName = "Código";
                    excelDataSet.Tables[0].Columns["DescMaterial"].ColumnName = "Descripción";
                    dataGridViewBOM.DataSource = excelDataSet.Tables[0];
                    //Se redondea a dos decimales la columna QTY (QUANTITY)
                    dataGridViewBOM.Columns["QTY"].DefaultCellStyle.Format = "N2";
                    //Se ajustan las columnas
                    dataGridViewBOM.Columns["QTY"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewBOM.Columns["Código"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                if (puntero == "Descripcion")
                {
                    //Se sobreescribe el encabezado de las columnas
                    excelDataSet.Tables[0].Columns["CodProd"].ColumnName = "Código";
                    excelDataSet.Tables[0].Columns["DescProd"].ColumnName = "Descripción";
                    dataGridViewBOM.DataSource = excelDataSet.Tables[0];
                    //Se ajustan las columnas
                    dataGridViewBOM.Columns["Código"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                //Ocultamos la columna (-1) ya que no proporciona información
                dataGridViewBOM.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }
        }

        /// <summary>
        /// Cuando se busca o solecciona una fila se ha de mostrar los archivos que el producto disponga.
        /// </summary>
        /// <param name="Codigo">Referencia del producto con el que se buscar el o los archivos.</param>
        /// <param name="ListBox">Parámetro que indica si los archivos se deben mostrar en un ListBox u otro.</param>
        private void MostrarListaArchivos(string Codigo, string ListBox)
        {
            //Se indica que si la busqueda es por Descripción todos los archivos se tiene que mostrar en el ListBox del producto
            //Si la busqueda es por referencia se ha de comprobar si el código es el con el que se ha realizado la busqueda dado que en ese caso tambien se debe motrar en el ListBox del producto
            if ((puntero == "Referencia" && (Codigo == RefTB.Text || Codigo == UltimaReferencia))|| puntero == "Descripcion") ListBox = "Producto";
            
            //Dado que el nombre del archivo no solo contiene en cogido del producto realizamos una busqueda parcial, importando todos los archivos que lleven el código en su nombre
            string partialName = Codigo;
            DirectoryInfo carpeta = new DirectoryInfo(MaquinaLinea.RutaFolderBOM);
            FileInfo[] filesInDir = carpeta.GetFiles("*" + partialName + "*.*");
            if (ListBox == "Producto")
            {
                DocumentacionProductoListBox.Items.Clear();
                foreach (FileInfo archivos in filesInDir)
                {
                    DocumentacionProductoListBox.Items.Add(archivos.Name);
                }
            }

            if (ListBox == "Material")
            {
                DocumentacionMaterialListBox.Items.Clear();
                foreach (FileInfo archivos in filesInDir)
                {
                    DocumentacionMaterialListBox.Items.Add(archivos.Name);
                }
            }
        }
        /// <summary>
        /// Función que marca toda la fila de la celda seleccionada
        /// </summary>
        /// <returns>CodMaterial -> Devuelve el codigo del material del producto que esta en la fila seleccionada</returns>
        private string SelecciónFila()
        {
            //DataGridView permite seleccionar una fila, una celda o todas las celdas
            Int32 selectedCellCount = dataGridViewBOM.GetCellCount(DataGridViewElementStates.Selected);
            Int32 selectedRowCount = dataGridViewBOM.Rows.GetRowCount(DataGridViewElementStates.Selected);

            //Nos aseguramos que se haya seleccionado una celda
            if (selectedCellCount > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //Se calcula que fila y que columna se ha seleccionado
                for (int i = 0; i < selectedCellCount; i++)
                {
                    fila = dataGridViewBOM.SelectedCells[i].RowIndex;
                }

                //Se marca toda la fila
                dataGridViewBOM.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                //Si se ha seleccionado una fila entera mostramos el dato que haya en la columna CodMaterial que siempre será la primera, es decir, la número 0
                if (selectedRowCount > 0)
                {
                    CodMaterial = dataGridViewBOM.Rows[fila].Cells[0].Value.ToString();
                    DescMaterial = dataGridViewBOM.Rows[fila].Cells[1].Value.ToString();
                }

                //Si no se ha seleccionado una fila se ha seleccionado una celda
                else
                {
                    CodMaterial = dataGridViewBOM.Rows[fila].Cells[0].Value.ToString();
                    DescMaterial = dataGridViewBOM.Rows[fila].Cells[1].Value.ToString();
                }
            }
            return CodMaterial;
        }

        private void DescripcionTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Caso donde se utiliza para la busqueda la referencia del producto
                if (RefTB.Text != "" && puntero == "Referencia")
                {
                    //Se muestra los resultados de la busqueda
                    CompletarTablaExcel(RefTB.Text, puntero);
                    //Se expone los archivos del producto buscado si se dispone de ellos
                    Utilidades.MostrarImagen(RefTB.Text, Imagen);
                    MostrarListaArchivos(RefTB.Text, "Producto");
                }
                //Caso donde se utiliza para la busqueda la descripción del producto
                if (DescripcionTB.Text != "" && puntero == "Descripcion")
                {
                    //La busqueda por descripción no es exacta, por ello se muestra el boton EdicionB que permite volver a la seleccion del producto
                    EdicionB.Show();
                    EdicionBoton = true;
                    EdicionB.Text = "MOSTRAR LA COMPOSICIÓN DEL PRODUCTO";
                    //Para volver a la selección del producto empleamos la variable UltimaDescripcion
                    UltimaDescripcion = DescripcionTB.Text;
                    //Se restaura los archivos buscados
                    DocumentacionMaterialListBox.Items.Clear();
                    DocumentacionProductoListBox.Items.Clear();
                    Imagen.Image = null;
                    //Se muestran los resultados de la busqueda por Descripción
                    CompletarTablaExcel(DescripcionTB.Text, puntero);
                }
            }
        }

        private void RefTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Caso donde se utiliza para la busqueda la referencia del producto
                if (RefTB.Text != "" && puntero == "Referencia")
                {
                    //Se muestra los resultados de la busqueda
                    CompletarTablaExcel(RefTB.Text, puntero);
                    //Se expone los archivos del producto buscado si se dispone de ellos
                    Utilidades.MostrarImagen(RefTB.Text, Imagen);
                    MostrarListaArchivos(RefTB.Text, "Producto");
                }
                //Caso donde se utiliza para la busqueda la descripción del producto
                if (DescripcionTB.Text != "" && puntero == "Descripcion")
                {
                    //La busqueda por descripción no es exacta, por ello se muestra el boton EdicionB que permite volver a la seleccion del producto
                    EdicionB.Show();
                    EdicionBoton = true;
                    EdicionB.Text = "MOSTRAR LA COMPOSICIÓN DEL PRODUCTO";
                    //Para volver a la selección del producto empleamos la variable UltimaDescripcion
                    UltimaDescripcion = DescripcionTB.Text;
                    //Se restaura los archivos buscados
                    DocumentacionMaterialListBox.Items.Clear();
                    DocumentacionProductoListBox.Items.Clear();
                    Imagen.Image = null;
                    //Se muestran los resultados de la busqueda por Descripción
                    CompletarTablaExcel(DescripcionTB.Text, puntero);
                }
            }
        }

        private void BajarB_Click(object sender, EventArgs e)
        {
            ////DataGridView permite seleccionar una fila, una celda o todas las celdas
            //Int32 selectedCellCount = dataGridViewBOM.GetCellCount(DataGridViewElementStates.Selected);
            //Int32 selectedRowCount = dataGridViewBOM.Rows.GetRowCount(DataGridViewElementStates.Selected);

        }
    }
}        

 
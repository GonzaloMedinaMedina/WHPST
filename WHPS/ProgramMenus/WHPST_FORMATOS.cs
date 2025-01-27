﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Llenadora;
using WHPS.Model;
using WHPS.Utiles;

namespace WHPS.ProgramMenus
{
    public partial class WHPST_FORMATOS : Form
    {
        //VARIABLES PARA FILTRAR
        public string Maquina, Linea, FormatoActual, FormatoCambio;
        //VARIABLES PARA BUSCAR
        public string Columnabusqueda, Hoja, Parametros;
        //VARIABLES DE OPERACION
        public string SeleccionFila, DescMaterial, CodMaterial;
        public int Fila = 0;
        //VARIABLES PARA INDICAR COLOR Y OBSERVACIONES
        public string Color1,Color2;
        Llenadora_Documentacion parent;
        public WHPST_FORMATOS(Llenadora_Documentacion p)
        {
            InitializeComponent();
            parent = p;
        }

        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>
        /// <param name="BackL+numlin">Parámetro que identifica a cual form hijo de WHPST_INICIO debe volver en función del número de línea.</param>
        private void ExitB_Click(object sender, EventArgs e)
        {
            Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(Llenadora_Documentacion));
            this.Hide();
            this.Dispose();

        }
        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

        private void WHPST_FORMATOS_Load(object sender, EventArgs e)
        {
            //Muestra la hora ya que el timer tarda 1s en tomar el control del Label lbReloj.
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            LineaCB.Text = MaquinaLinea.DatosBusquedaFormato[0];
            MaquinaCB.Text = MaquinaLinea.DatosBusquedaFormato[1];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            //Para activar la alarma debe, estar desactivada, haberse chequeado el inicio de turno y que la hora cuadre con la alarma 
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }
        }

        private void BuscarB_Click(object sender, EventArgs e)
        {
            Maquina = MaquinaCB.Text;
            Linea = LineaCB.Text;
            FormatoActual = FormatoActualCB.Text;
            FormatoCambio = FormatoCambioCB.Text;
            
            //Si todos los campos han sido completados, realizamos la busqueda
            if (Maquina != "" && Linea != "" && (FormatoActual != "" && FormatoCambio != ""))
            {
                MaquinaLinea.FileBOF = "BOF" + Linea + Maquina.Substring(0, 4);
                //NECESITAMOS LOS CÓDIGOS DE LAS BOTELLAS PARA REALIZAR LA BUSQUEDA
                ObtenerCodigo(FormatoCambio, "DescProd", "BOTELLA", "CodProd");
                ObtenerCodigo(FormatoActual, "DescProd", "BOTELLA", "CodProd");
                //MOSTRAMOS EN PANTALLA LAS TABLAS
                CompletarTablaExcel(Maquina + Linea, "MAQLIN", "POSICIONESMAQ", "COMPOSICION", DataGridViewPosicionPieza);
                CompletarTablaExcel(FormatoActual, "CodProd", "FICHA", "CodMaterial;DescMaterial;COLOR1;COLOR2;OBSERVACIONES", DataGridViewFormatoA);
                CompletarTablaExcel(FormatoCambio, "CodProd", "FICHA", "CodMaterial;DescMaterial;COLOR1;COLOR2;OBSERVACIONES", DataGridViewFormatoC);
            }
        }






        //####################### FUNCIONES DE BUSQUEDA ######################
        /// <summary>
        /// Función que muestra los resultados de la busqueda en el DATAGRIDVIEW.
        /// </summary>
        private void CompletarTablaExcel(string Busqueda, string Columnabusqueda, string Hoja, string Parametro, DataGridView gdv)
        {
            //Realiza la busqueda
            List<string[]> valoresAFiltrar = new List<string[]>();
            string[] filterval = new string[4];
            filterval[0] = "AND";
            filterval[1] = Columnabusqueda;
            filterval[2] = "LIKE";
            filterval[3] = "'%" + Busqueda + "%'";
            valoresAFiltrar.Add(filterval);

            DataSet excelDataSet = new DataSet();
            string result;
            //List<string[]> valoresAFiltrar = dgvSelectFiltro.DataSource;
            excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileBOF, Hoja, Parametro.Split(';'), valoresAFiltrar, out result);
            ////tbSelectSalidaError.Text = result;
            //MessageBox.Show(result);
            //Una vez realizada la busqueda si esta es correcta se modifican los parámetros de la tabla para se adecuen a las necesidades del usuario
            try
            {
                //Se sobreescribe el encabezado de las columnas
                if (gdv == DataGridViewPosicionPieza)
                {
                    excelDataSet.Tables[0].Columns[Parametro].ColumnName = "POSICIÓN";
                    gdv.DataSource = excelDataSet.Tables[0];
                }
                if (gdv == DataGridViewFormatoA)
                {
                    excelDataSet.Tables[0].Columns["DescMaterial"].ColumnName = "FORMATO ACTUAL";
                    gdv.DataSource = excelDataSet.Tables[0];
                    gdv.Columns["CodMaterial"].Visible = false;
                    gdv.Columns["COLOR1"].Visible = false;
                    gdv.Columns["COLOR2"].Visible = false;
                    gdv.Columns["OBSERVACIONES"].Visible = false;
                }
                if (gdv == DataGridViewFormatoC)
                {
                    excelDataSet.Tables[0].Columns["DescMaterial"].ColumnName = "FORMATO DE CAMBIO";
                    gdv.DataSource = excelDataSet.Tables[0];
                    gdv.Columns["CodMaterial"].Visible = false;
                    gdv.Columns["COLOR1"].Visible = false;
                    gdv.Columns["COLOR2"].Visible = false;
                    gdv.Columns["OBSERVACIONES"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }
        }
        /// <summary>
        /// Función que OBTIENE los códigos de las referencias
        /// </summary>
        private void ObtenerCodigo(string Busqueda, string Columnabusqueda, string Hoja, string Parametro)
        {
            //Realiza la busqueda
            List<string[]> valoresAFiltrar = new List<string[]>();
            string[] filterval = new string[4];
            filterval[0] = "AND";
            filterval[1] = Columnabusqueda;
            filterval[2] = "LIKE";
            filterval[3] = "'%" + Busqueda + "%'";
            valoresAFiltrar.Add(filterval);

            DataSet excelDataSet = new DataSet();
            string result;
            //List<string[]> valoresAFiltrar = dgvSelectFiltro.DataSource;
            excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileBOF, Hoja, Parametro.Split(';'), valoresAFiltrar, out result);
            ////tbSelectSalidaError.Text = result;
            //MessageBox.Show(result);
            //Una vez realizada la busqueda si esta es correcta se modifican los parámetros de la tabla para se adecuen a las necesidades del usuario
            try
            {
                if (excelDataSet.Tables[0].Rows.Count > 0)
                {
                    if (Busqueda == FormatoActual) FormatoActual = Convert.ToString(excelDataSet.Tables[0].Rows[0][Parametro]);
                    if (Busqueda == FormatoCambio) FormatoCambio = Convert.ToString(excelDataSet.Tables[0].Rows[0][Parametro]);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }
        }



        //####################### FUNCIONES VISUALIZACIÓN DE PÁGINA ######################
        //MOVEMOS ARRIBA Y ABAJO LA TABLA CON EL SCROLL DE DGVFORMATOC
        private void DataGridViewFormatoC_Scroll(object sender, ScrollEventArgs e)
        {
            if ((e.Type == ScrollEventType.SmallDecrement || e.Type == ScrollEventType.LargeDecrement) && (DataGridViewPosicionPieza.FirstDisplayedScrollingRowIndex > 0 || DataGridViewFormatoA.FirstDisplayedScrollingRowIndex > 0))
            {
                DataGridViewFormatoA.FirstDisplayedScrollingRowIndex -= Convert.ToInt16(e.OldValue - e.NewValue);
                DataGridViewPosicionPieza.FirstDisplayedScrollingRowIndex -= Convert.ToInt16(e.OldValue - e.NewValue);
            }
            if ((e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement) && (DataGridViewPosicionPieza.FirstDisplayedScrollingRowIndex < DataGridViewPosicionPieza.RowCount || DataGridViewFormatoA.RowCount < DataGridViewFormatoA.RowCount))
            {
                DataGridViewFormatoA.FirstDisplayedScrollingRowIndex += Convert.ToInt16(e.NewValue - e.OldValue);
                DataGridViewPosicionPieza.FirstDisplayedScrollingRowIndex += Convert.ToInt16(e.NewValue - e.OldValue);
            }
        }
        //MOSTRAMOS EN VERDE LAS PIEZAS QUE NO ES NECESARIO CAMBIAR
        private void DataGridViewFormatoA_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DataGridViewFormatoA.Columns[e.ColumnIndex].Name == "FORMATO ACTUAL")
            {
                try
                {
                    if (Convert.ToString(e.Value) == DataGridViewFormatoC.Rows[e.RowIndex].Cells[1].Value.ToString())
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.DarkSeaGreen;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }
        }


        private void LineaCB_TextChanged(object sender, EventArgs e)
        {
            FormatoActualCB.Text = "";
            FormatoCambioCB.Text = "";
            MostrarBotellas(LineaCB.Text, FormatoActualCB);
            MostrarBotellas(LineaCB.Text, FormatoCambioCB);
        }

        private void DataGridViewFormatoC_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DataGridViewFormatoC.Columns[e.ColumnIndex].Name == "FORMATO DE CAMBIO")
            {
                try
                {
                    if (Convert.ToString(e.Value) == DataGridViewFormatoA.Rows[e.RowIndex].Cells[1].Value.ToString())
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.DarkSeaGreen;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }
        }

        //MOSTRAMOS LA IMAGEN DE LAS PIEZAS SELECCIONADA
        private void DataGridViewPosicionPieza_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SeleccionCelda(DataGridViewPosicionPieza);
            CodMaterial = DataGridViewPosicionPieza.Rows[Fila].Cells[0].Value.ToString();
            //DescMaterial = DataGridViewPosicionPieza.Rows[Fila].Cells[1].Value.ToString();
            if (CodMaterial != "")
            {
                if (DescMaterial.Length > 3 && DescMaterial.Substring(0, 3) == "BOT") Utilidades.MostrarImagen(CodMaterial, Imagen);
                else MostrarImagen(CodMaterial, Imagen, Linea);
            }
        }
        private void DataGridViewFormatoA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Cuando esto sucede, se marca la fila y extrae el código de referencia del producto de la fila marcada.
            SeleccionCelda(DataGridViewFormatoA);
            CodMaterial = DataGridViewFormatoA.Rows[Fila].Cells[0].Value.ToString();
            DescMaterial = DataGridViewFormatoA.Rows[Fila].Cells[1].Value.ToString();
            if (CodMaterial != "")
            {
                if (DescMaterial.Length > 3 && DescMaterial.Contains("BOT")) Utilidades.MostrarImagen(CodMaterial, Imagen);
                else MostrarImagen(CodMaterial, Imagen, Linea);
            }
        }
        private void DataGridViewFormatoC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Cuando esto sucede, se marca la fila y extrae el código de referencia del producto de la fila marcada.
            SeleccionCelda(DataGridViewFormatoC);
            CodMaterial = DataGridViewFormatoC.Rows[Fila].Cells[0].Value.ToString();
            DescMaterial = DataGridViewFormatoC.Rows[Fila].Cells[1].Value.ToString();
            if (CodMaterial != "")
            {
                if (DescMaterial.Length > 3 && DescMaterial.Contains("BOT")) Utilidades.MostrarImagen(CodMaterial, Imagen);
                else MostrarImagen(CodMaterial, Imagen, Linea);
            }
        }

        /// <summary>
        /// Función masca la celda seleccionada
        /// </summary>
        private void SeleccionCelda(DataGridView dgv)
        {
            //DataGridViewFormatoA.Rows[Fila].Selected = false;
            //DataGridViewFormatoC.Rows[Fila].Selected = false;
            //DataGridViewPosicionPieza.Rows[Fila].Selected = false;
            //DataGridView permite seleccionar una fila, una celda o todas las celdas
            Int32 selectedCellCount = dgv.GetCellCount(DataGridViewElementStates.Selected);
            //Se calcula que fila y que columna se ha seleccionado
            for (int i = 0; i < selectedCellCount; i++)
            {
                Fila = dgv.SelectedCells[i].RowIndex;
            }
            DescMaterial = dgv.Rows[Fila].Cells[0].Value.ToString();
            DataGridViewFormatoA.Rows[Fila].Selected = true;
            DataGridViewFormatoC.Rows[Fila].Selected = true;
            DataGridViewPosicionPieza.Rows[Fila].Selected = true;
            if (dgv != DataGridViewPosicionPieza)
            {
                if (dgv.Rows[Fila].Cells[4].Value.ToString() != "0") InstruccionTB.Text = dgv.Rows[Fila].Cells[4].Value.ToString();
                Color1 = dgv.Rows[Fila].Cells[2].Value.ToString();
                Color2 = dgv.Rows[Fila].Cells[3].Value.ToString();
                MostrarColores(Color1, Color1TB);
                MostrarColores(Color2, Color2TB);
            }
        }

        public void MostrarColores(string Color1, TextBox TextBox)
        {
           
            switch (Color1)
            {
                case "AMARILLO":
                    TextBox.Visible = true;
                    TextBox.BackColor = Color.Yellow;
                    break;
                case "ROJO":
                    TextBox.Visible = true;
                    TextBox.BackColor = Color.Maroon;
                    break;
                case "AZUL":
                    TextBox.Visible = true;
                    TextBox.BackColor = Color.Blue;
                    break;
                case "CELESTE":
                    TextBox.Visible = true;
                    TextBox.BackColor = Color.LightSkyBlue;
                    break;
                case "VERDE":
                    TextBox.Visible = true;
                    TextBox.BackColor = Color.Green;
                    break;
                case "GRIS":
                    TextBox.Visible = true;
                    TextBox.BackColor = Color.Gray;
                    break;
                case "BLANCO":
                    TextBox.Visible = true;
                    TextBox.BackColor = Color.White;
                    break;
                default:
                    TextBox.Visible = false;
                    break;
            }
        }
        public void MostrarBotellas (string Linea, ComboBox FormatoCB)
        {
            FormatoCB.Items.Clear();
            switch (Linea)
            {
                case "L2":
                    FormatoCB.Items.Add("");
                    break;
                case "L3":
                    FormatoCB.Items.Add("");
                    FormatoCB.Items.Add("01.01.01. BOT. ACACIA AVANT 70cL B/P");
                    FormatoCB.Items.Add("02.01.01. BOT. ACACIA AVANT 100cL B/P");
                    FormatoCB.Items.Add("03.01.01. BOT. JEREZANA NEGRA 75cL B/R");
                    FormatoCB.Items.Add("03.02.01. BOT. JEREZANA NEGRA 75cL B/C");
                    FormatoCB.Items.Add("04.01.01. BOT. BORDELESA SEDUCCION 50cL B/C");
                    FormatoCB.Items.Add("05.01.01. BOT. BORDELESA VERDE 75cL B/R");
                    FormatoCB.Items.Add("06.01.01. BOT. CANASTA 37,5cL B/C");
                    FormatoCB.Items.Add("07.01.01. BOT. CANASTA NEGRA 75cL B/C");
                    FormatoCB.Items.Add("08.01.01. BOT. CANASTA 100cL B/C");
                    FormatoCB.Items.Add("09.01.01. BOT. MANILA 70cL B/R");
                    FormatoCB.Items.Add("10.01.01. BOT. TIO NICO 75cL B/R");
                    break;

                case "L5":
                    FormatoCB.Items.Add("");
                    FormatoCB.Items.Add("01.01.01. BOT. LICOR 100 100cL B/P");
                    FormatoCB.Items.Add("02.01.01. BOT. ACACIA 100cL B/P");
                    FormatoCB.Items.Add("03.01.01. BOT. JEREZANA NEGRA 75cL B/R");
                    FormatoCB.Items.Add("03.02.01. BOT. JEREZANA NEGRA 75cL B/C");
                    FormatoCB.Items.Add("04.01.01. BOT. JEREZANA NEGRA 100cL B/R");
                    FormatoCB.Items.Add("04.02.01. BOT. JEREZANA NEGRA 100cL B/C");
                    FormatoCB.Items.Add("05.01.01. BOT. BDY.WH FILIPINAS 70cL B/R");
                    FormatoCB.Items.Add("06.01.01. BOT. BDY.WH FILIPINAS 100cL B/R");
                    FormatoCB.Items.Add("07.01.01. BOT. PCHE.WILLIAMS PLATA 100cL B/P");
                    FormatoCB.Items.Add("08.01.01. BOT. BDY.WH FILIPINAS S/G 100cL B/R");
                    FormatoCB.Items.Add("09.01.01. BOT. ALHAMBRA 70cL B/P");
                    FormatoCB.Items.Add("10.01.01. BOT. ALHAMBRA 100cL B/P");
                    FormatoCB.Items.Add("11.01.01. BOT. GIN ZAFIRO 70cL B/R");
                    FormatoCB.Items.Add("12.01.01. BOT. GIN ZAFIRO 100cL B/R");
                    break;
                default:
                    FormatoCB.Items.Add("");
                    break;
            }
        }
        /// <summary>
        /// Función que muesta la imagen de la busqueda o de la fila selecionada en pantalla.
        /// </summary>
        /// <param name="NombreImagen">El nombre de la imagen se guarda con el códgo de referencia de aquel producto que se este exhibiendo.</param>
        /// <param name="Imagen">Se indica en nombre del PictureBox donde se va a mostrar la imagen.</param>
        public static void MostrarImagen(string NombreImagen, PictureBox Imagen, string Linea)
        {
            string RutaImagen = "";
            if (Linea == "L2") RutaImagen = MaquinaLinea.RutaImagen_BOFL2;
            if (Linea == "L3") RutaImagen = MaquinaLinea.RutaImagen_BOFL3;
            if (Linea == "L5") RutaImagen = MaquinaLinea.RutaImagen_BOFL5;

            //Las imagenes se guardan en formato .jpg por ello se le añade la terminación
            NombreImagen = NombreImagen + ".jpg";
            if (NombreImagen != "")
            {
                try
                {
                    Imagen.Image = Image.FromFile(RutaImagen + NombreImagen);
                }
                //Si la referencia no tiene imagen se muestra una imagen  establezida que indicará que no tiene foto el producto
                catch (System.IO.FileNotFoundException)
                {
                    Imagen.Image = Properties.Resources.GenSinImagen;
                }
            }
        }
    }
}


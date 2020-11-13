using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WHPS.Utiles;
using System.Diagnostics;
using System.IO;
using WHPS.ProgramMenus;
using WHPS.Model;


namespace WHPS.Despaletizador
{
    public partial class Despaletizador_Cierres : Form
    {
        //Variable que indica que el modo manual (TRUE) o no (FALSE)
        public bool ReferenciaWH = false;
        public bool modo_manual = false;
        string CodigoMaterial = "";
        Datos_Cierres datos_cierres = new Datos_Cierres();
        MainDespaletizador parent;
        public Despaletizador_Cierres(MainDespaletizador p)
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
            Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainDespaletizador));
            this.Hide();
            this.Dispose();
        }


        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }


        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        private void Despaletizador_Cierres_Load(object sender, EventArgs e)
        {
            //Muestra la hora ya que el timer tarda 1s en tomar el control del Label lbReloj.
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //Rellenamos los parámetros iniciales
            dateTB.Text = DateTime.Now.ToString("dd/MM/yyyy");
            turnoTB.Text = Utilidades.ObtenerTurnoActual();
            respTB.Text = MaquinaLinea.Responsable;
            maqTB.Text = MaquinaLinea.MDespaletizador;

            //Seleccionamos directamente el campo de texto y etablecemos el modo no manual
            InputTB.Select();
            modo_manual = false;
            ReferenciaWH = true;
        }

        /// <summary>
        /// Función del temporizador que se ejecuta cada segundo, indíca la hora y si se activa la alarma. 
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Activa la alarma cuando la hora marcada es la misma que la que se muestra en pantalla.
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }
        }

        //########################   RELLENAR EL CÓDIGO   ########################
        /// <summary>
        /// El lector introduce el código y aplica un enter, cuando este de detecte ejecuta la acción. 
        /// </summary>
        private void InputTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (ReferenciaWH == false)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //Generamos la lista de valores a filtrar y traer desde la lista de materiales AND, 'Columna', LIKE, 'Codigo Material'
                    List<string[]> listavalores = new List<string[]>();
                    string[] valores = new string[4];
                    valores[0] = "AND";
                    valores[1] = "Codigo";
                    valores[2] = "LIKE";
                    valores[3] = " \"" + InputTB.Text + "\"";
                    listavalores.Add(valores);

                    //Llamamos la busqueda del fichero excel
                    string result;
                    DataSet excelDataSet = new DataSet();
                    excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileMateriales, "Cierres", "EAN;Proveedor;Descripcion;Cantidad".Split(';'), listavalores, out result);
                    //MessageBox.Show(result);

                    if (excelDataSet.Tables[0].Rows.Count > 0)
                    {
                        eanTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["EAN"]);
                        DescTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Descripcion"]);
                        provTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Proveedor"]);
                        cantidadTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Cantidad"]);
                        if (cantidadTB.Text == "") cantidadTB.Text = " -";
                        refwhTB.Text = InputTB.Text;
                        InputTB.Text = "";
                        loteTB.Text = " -";
                        fabdateTB.Text = " -";
                        ssccTB.Text = " -";
                    }
                    else
                    {
                        MessageBox.Show("CODIGO NO ENCONTRADO: Informar al responsable");
                    }
                }
            }
            else
            if (e.KeyCode == Keys.Enter)
            {

                Datos_Cierres prasing = new Datos_Cierres();
                prasing = Apps_Despaletizador.ParsingCod_Cierres(InputTB.Text);

                //Rellenamos todos los datos que han sido identificados
                if (prasing.whDescrip != "")
                {
                    DescTB.Text = prasing.whDescrip;
                }
                if (prasing.ean != "")
                {
                    eanTB.Text = prasing.ean;
                }
                if (prasing.refInt != "")
                {
                    refwhTB.Text = prasing.refInt;
                }
                if (prasing.Proveedor != "")
                {
                    provTB.Text = prasing.Proveedor;
                }
                if (prasing.FechaFab != "")
                {
                    fabdateTB.Text = prasing.FechaFab;
                }
                if (prasing.Cantidad != "")
                {
                    cantidadTB.Text = prasing.Cantidad;
                }
                if (prasing.LoteFab != "")
                {
                    loteTB.Text = prasing.LoteFab;
                }
                if (prasing.SSCC != "")
                {
                    ssccTB.Text = prasing.SSCC;
                }
                InputTB.Text = "";
            }
        }

        /// <summary>
        /// Si el lector no funciona, se posibilita abrir el teclado  haciendo click en el TB cuando el modo manual esta activado.
        /// </summary>
        private void InputTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (modo_manual == true)
            {
                WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,InputTB);
            }
        }

        /// <summary>
        /// Boton que activa o desactiva el modomaual.
        /// </summary>
        private void ModoManualBot_Click(object sender, EventArgs e)
        {
            if (modo_manual == true)
            {
                ModoManualBot.BackColor = Color.FromArgb(27, 33, 41);
                modo_manual = false;
            }
            else
            {
                ModoManualBot.BackColor = Color.DarkSeaGreen;
                modo_manual = true;
            }
        }
        /// <summary>
        /// Boton que envia el códgo para hacer el parsing.
        /// </summary>
        private void EnviarB_Click(object sender, EventArgs e)
        {
            InputTB.Select();
            SendKeys.Send("{ENTER}");
        }
        //########################################################################

        //########################   GUARDAR LOS DATOS   #########################
        private void saveBot_Click(object sender, EventArgs e)
        {
            if (eanTB.Text != "" && refwhTB.Text != "" && provTB.Text != "" && loteTB.Text!="" && fabdateTB.Text!="")
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "Fecha", DateTime.Now.ToString("dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Hora", DateTime.Now.ToString("HH:mm:ss") });
                listavalores.Add(new string[2] { "FechaDB", DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Responsable", MaquinaLinea.Responsable });
                listavalores.Add(new string[2] { "Maquinista", MaquinaLinea.MDespaletizador });
                listavalores.Add(new string[2] { "Turno", turnoTB.Text });
                listavalores.Add(new string[2] { "Descripcion", DescTB.Text });
                listavalores.Add(new string[2] { "LoteFab", loteTB.Text });
                listavalores.Add(new string[2] { "RefInterna", refwhTB.Text });
                listavalores.Add(new string[2] { "Proveedor", provTB.Text });
                listavalores.Add(new string[2] { "EAN", eanTB.Text });
                listavalores.Add(new string[2] { "Cantidad", cantidadTB.Text });
                listavalores.Add(new string[2] { "SSCC", ssccTB.Text });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileDespaletizador, "Cierres", listavalores, "Id");
                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                    //MessageBox.Show(salida.ToString());
                }
                else
                {
                    DescTB.Text = "";
                    eanTB.Text = "";
                    refwhTB.Text = "";
                    provTB.Text = "";
                    fabdateTB.Text = "";
                    cantidadTB.Text = "";
                    loteTB.Text = "";
                    ssccTB.Text = "";
                    if (MaquinaLinea.numlin == 2) MaquinaLinea.ProductoSeleccionadoDespL2 = "";
                    if (MaquinaLinea.numlin == 3) MaquinaLinea.ProductoSeleccionadoDespL3 = "";
                    if (MaquinaLinea.numlin == 5) MaquinaLinea.ProductoSeleccionadoDespL5 = "";
                    InputTB.Select();
                }
            }
            else
            {
                MessageBox.Show("FALTAN CAMPOS POR RELLENAR! -> NO SE HA GUARDADO LA INFORMACIÓN");
                InputTB.Select();
            }
        }

      

        private void RefWB_Click(object sender, EventArgs e)
        {
            if (ReferenciaWH)
            {
                RefWB.BackColor = Color.DarkSeaGreen;
                InputLB.Text = "Referencia WH:";
                ReferenciaWH = false;
                ExtraerDatosBOM(MaquinaLinea.CodigoProd);
                ExtraerDatosMateriales(CodigoMaterial);
            }
            else
            {
                RefWB.BackColor = Color.FromArgb(27, 33, 41);
                InputLB.Text = "Código del producto:";
                ReferenciaWH = true;
            }
        }

        /// <summary>
        /// Función que busca el código del cierre.
        /// </summary>
        /// <param name="Referencia">Parámetro que inidica el código del producto que se esta procesadon y se extrae del main.</param>
        /// <returns>Devuelve un código del cierre</returns>
        public void ExtraerDatosBOM(string Referencia)
        {
            int i = 0;
            while (i < 3)
            {
                List<string[]> valoresAFiltrar = new List<string[]>();
                string[] filterval = new string[8];
                filterval[0] = "AND";
                filterval[1] = "CodProd";
                filterval[2] = "LIKE";
                filterval[3] = Referencia;
                valoresAFiltrar.Add(filterval);

                if (i == 0)
                {
                    string[] filterval1 = new string[4];
                    filterval1[0] = "AND";
                    filterval1[1] = "DescMaterial";
                    filterval1[2] = "LIKE";
                    filterval1[3] = "'%" + "TAP." + "%'";
                    valoresAFiltrar.Add(filterval1);
                }
                if (i == 1)
                {
                    string[] filterval2 = new string[4];
                    filterval2[0] = "AND";
                    filterval2[1] = "DescMaterial";
                    filterval2[2] = "LIKE";
                    filterval2[3] = "'%" + "CAPS." + "%'";
                    valoresAFiltrar.Add(filterval2);
                }
                if (i == 2)
                {
                    string[] filterval3 = new string[4];
                    filterval3[0] = "AND";
                    filterval3[1] = "DescMaterial";
                    filterval3[2] = "LIKE";
                    filterval3[3] = "'%" + "C.MET" + "%'";
                    valoresAFiltrar.Add(filterval3);
                }
                DataSet excelDataSet = new DataSet();
                string result;
                //List<string[]> valoresAFiltrar = dgvSelectFiltro.DataSource;
                excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileBOM, "FICHA", "CodMaterial ".Split(';'), valoresAFiltrar, out result);
                //tbSelectSalidaError.Text = result;
                //MessageBox.Show(result);
                if (excelDataSet.Tables[0].Rows.Count > 0)
                {
                    CodigoMaterial = Convert.ToString(excelDataSet.Tables[0].Rows[0]["CodMaterial"]);
                    i = 3;
                }
                valoresAFiltrar.Clear();
                i++;
            }
        }
        /// <summary>
        /// Función que completa los textbox de la descripcion del cierre y el proveedor del mismo.
        /// </summary>
        /// <param name="CodigoMaterial">Código extraido de la funcion EstraerDatosBOM.</param>
        public void ExtraerDatosMateriales(string CodigoMaterial)
        {
            if (CodigoMaterial != "")
            {
                List<string[]> valoresAFiltrar = new List<string[]>();
                string[] filterval = new string[8];
                filterval[0] = "AND";
                filterval[1] = "Codigo";
                filterval[2] = "LIKE";
                filterval[3] = CodigoMaterial;
                valoresAFiltrar.Add(filterval);


                DataSet excelDataSet = new DataSet();
                string result;
                //List<string[]> valoresAFiltrar = dgvSelectFiltro.DataSource;
                excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileMateriales, "Cierres", "Codigo".Split(';'), valoresAFiltrar, out result);
                //tbSelectSalidaError.Text = result;
                //MessageBox.Show(result);
                if (excelDataSet.Tables[0].Rows.Count > 0)
                {
                    InputTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Codigo"]);
                }
                else MessageBox.Show("No se ha entontrado la descripción del cierre, comunicalo al responsable");
            }
        }
        //########################################################################
    }//## END CLASS
} //## END NAMESPACE

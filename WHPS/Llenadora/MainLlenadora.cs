using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.ProgramMenus;
using WHPS.Utiles;

namespace WHPS.Llenadora
{
    public partial class MainLlenadora : Form
    {
        //Varibles de parpadeo parada.
        public bool inicio_paro = false;
        public string hora_ini_paro = "";

        //Variable que cambia el la actualizacion del lanzamiento
        public bool LanzActualizado = false;
        public int contador = 10;
        //Variable dgv, indica cuando se ha seleccionado una fila.
        private bool ClickEvent = false;
        //Variables que determinan que fila y columna se ha clickeado.
        int fila = 0, columna = 0;
        string IDLanzamiento = "";

        //Array para la transferencia de datos de produccion
        public static string[] DatosProduccion = new string[8];

        public int[] temp = new int[3];

        public static WHPST_INICIO parentInicio;
        public static Llenadora_CambioTurno FormCambioTurno;
        public static Llenadora_Comentarios FormComentarios;
        public static Llenadora_Control_Presion FormControlPresion;
        public static Llenadora_Control_Temperatura FormControlTemperatura;
        public static Llenadora_Control_Volumen FormControlVolumen;
        public static Llenadora_Control30m FormControl30min;
        public static Llenadora_Documentacion FormDocumentacion;
        public static Llenadora_Parte FormParte;
        public static Llenadora_Registro_Paro FormParo;
        public static Llenadora_Registro_Produccion FormProduccion;
        public static Llenadora_RotBotellas FormRotura;
        public static Llenadora_Torquimetro FormTorquimetro;
        public static Llenadora_Verificacion_Cierre_Volumen FormVerificacionCierre;

        public MainLlenadora(WHPST_INICIO p)
        {
            InitializeComponent();
            ActivarParadaGuardada();
            parentInicio = p;
        }


        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>
        /// <param name="BackL+numlin">Parámetro que identifica a cual form hijo de WHPST_INICIO debe volver en función del número de línea.</param>
        private void ExitB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.VolverInicioA = (MaquinaLinea.numlin == 2) ? RetornoInicio.L2 : RetornoInicio.L5;
            MaquinaLinea.VolverInicioA = (MaquinaLinea.numlin == 3) ? RetornoInicio.L3 : MaquinaLinea.VolverInicioA;
            Utilidades.AbrirForm(parentInicio,this, typeof(WHPST_INICIO));
        }
        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        public void MainLlenadora_Load(object sender, EventArgs e)
        {
            //Muestra la hora ya que el timer tarda 1s en tomar el control del Label lbReloj.
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //Muestra la tabla de lanzaminento.
            ExcelUtiles.CrearTablaLanzamientos(dgv);
            LanzamientoActualizado();

            //Función que determina el cambio de turno.
            Utilidades.FuncionLoad(MaquinistaTB, MaquinaLinea.MLlenadora, MaquinaLinea.chLlenL2, MaquinaLinea.chLlenL3, MaquinaLinea.chLlenL5, CambioTurnoB);

            //Definimos nuestro ID DE LANZAMIENTO
            if (MaquinaLinea.numlin == 2) IDLanzamiento = Properties.Settings.Default.DPiDLanzLlenL2;
            if (MaquinaLinea.numlin == 3) IDLanzamiento = Properties.Settings.Default.DPiDLanzLlenL3;
            if (MaquinaLinea.numlin == 5) IDLanzamiento = Properties.Settings.Default.DPiDLanzLlenL5;

            //Completamos los datos registrados
            if (MaquinaLinea.numlin == 2)
            {
                DepositoTB.Text = Properties.Settings.Default.DPDepositoLlenL2;
                if (Properties.Settings.Default.DPDeposito2LlenL2 != "") DepositoTB.Text = Properties.Settings.Default.DPDeposito2LlenL2;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL2;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioLlenL2;
            }
            if (MaquinaLinea.numlin == 3)
            {
                DepositoTB.Text = Properties.Settings.Default.DPDepositoLlenL3;
                if (Properties.Settings.Default.DPDeposito2LlenL3 != "") DepositoTB.Text = Properties.Settings.Default.DPDeposito2LlenL3;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL3;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioLlenL3;
            }
            if (MaquinaLinea.numlin == 5)
            {
                DepositoTB.Text = Properties.Settings.Default.DPDepositoLlenL5;
                if (Properties.Settings.Default.DPDeposito2LlenL5 != "") DepositoTB.Text = Properties.Settings.Default.DPDeposito2LlenL5;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL5;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioLlenL5;
            }

            //Extraemos los datos de producción
            if (IDLanzamiento != "") ExtraerDatosProduccion(Utilidades.BuscarFila(IDLanzamiento, dgv));
        }
  
        /// <summary>
        /// Función del temporizador que se ejecuta cada segundo, indíca la hora y si se activa la alarma. 
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Cada segundo carga la hora en pantalla
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Si una prada se ha iniciado, el boton quedará parpadeando.
            if (inicio_paro) ParoB.BackColor = Utilidades.AvisoBoton(ParoB.BackColor);


            //Si se ha actualizado el lanzamiento activamos una cuenta atras y lo apagamos.
            if (LanzActualizado == true)
            {
                TimerLB.Visible = true;

                if (contador != 0)
                {
                    contador--;
                    TimerLB.Text = Convert.ToString(contador) + "s";
                }

                else
                {
                    TimerLB.Visible = false;
                    ImagenCarga.BackgroundImage = Properties.Resources._Refresco;
                    LanzActualizado = false;
                }
            }
        }

        //#############################   BOTONES   ############################
        /// <summary>
        /// Función que recarga el lanzamiento.
        /// </summary>
        private void MaquinistaTB_Click(object sender, EventArgs e)
        {
            MaquinistaTB.SelectionLength = 0;
            if (!LanzActualizado)
            {
                ExcelUtiles.CrearTablaLanzamientos(dgv);
                LanzamientoActualizado();
                if (Properties.Settings.Default.DPiDLanzLlenL2 != "" && MaquinaLinea.numlin == 2) ExtraerDatosProduccion(Utilidades.BuscarFila(Properties.Settings.Default.DPiDLanzLlenL2, dgv));
                if (Properties.Settings.Default.DPiDLanzLlenL3 != "" && MaquinaLinea.numlin == 3) ExtraerDatosProduccion(Utilidades.BuscarFila(Properties.Settings.Default.DPiDLanzLlenL3, dgv));
                if (Properties.Settings.Default.DPiDLanzLlenL5 != "" && MaquinaLinea.numlin == 5) ExtraerDatosProduccion(Utilidades.BuscarFila(Properties.Settings.Default.DPiDLanzLlenL5, dgv));
            }
        }
        private void MaquinistaTB_MouseEnter(object sender, EventArgs e)
        {
            MaquinistaTB.SelectionLength = 0;
        }
        private void MaquinistaTB_MouseUp(object sender, MouseEventArgs e)
        {
            MaquinistaTB.SelectionLength = 0;
        }

        //Cuando pulsa el boton, registra el tiempo en la variable correspondiente
        private void ParoB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chLlenL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                        FormParo = new Llenadora_Registro_Paro(this, inicio_paro, hora_ini_paro, temp);
                        Hide();
                        FormParo.Show();
                       // Form.PLlendeTB.Text = (inicio_paro == true) ? hora_ini_paro : DateTime.Now.ToString("HH:mm:ss");
                        GC.Collect();
                    
                }
                else
                {
                    this.Hide();
                    FormCambioTurno.Show();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chLlenL3 == true || MaquinaLinea.usuario == "Administracion")
                {

                        FormParo = new Llenadora_Registro_Paro(this, inicio_paro, hora_ini_paro, temp);
                        Hide();
                        FormParo.Show();
                        //Form.PLlendeTB.Text = (inicio_paro == true) ? hora_ini_paro : DateTime.Now.ToString("HH:mm:ss");
                        GC.Collect();
                    
                }
                else
                {
                    this.Hide();
                    FormCambioTurno.Show();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chLlenL5 == true || MaquinaLinea.usuario == "Administracion")
                {

                        FormParo = new Llenadora_Registro_Paro(this, inicio_paro, hora_ini_paro, temp);
                        Hide();
                        FormParo.Show();
                       // Form.PLlendeTB.Text = (inicio_paro == true) ? hora_ini_paro : DateTime.Now.ToString("HH:mm:ss");
                        GC.Collect();
                    
                }
                else
                {
                    this.Hide();
                    FormCambioTurno.Show();
                }
            }
        }
        private void RegistroB_Click(object sender, EventArgs e)
        {
            if (IDLanzamiento == "") MessageBox.Show("Debes primero realizar la selección del producto. Para ello, pulsa el boton que aparece haciendo doble click en el mismo.");
            else AbrirFormLlenadora(FormProduccion, typeof(Llenadora_Registro_Produccion));

            /*Properties.Settings.Default.DPCodigoProdLlenL2 = CodProductoTB.Text;
            Properties.Settings.Default.DPCapacidadLlenL2 = CapacidadTB.Text;
            Properties.Settings.Default.DPProductoLlenL2 = ProductoTB.Text;
            Properties.Settings.Default.DPGraduacionLlenL2 = GraduacionTB.Text;
            Properties.Settings.Default.Save();*/

        }
        private void SiguienteB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.DatosBusquedaFormato = new string[2] {"L"+MaquinaLinea.numlin,"Llenadora"};
            AbrirFormLlenadora(FormParte, typeof(Llenadora_Parte));
        }
        private void DocumentacionB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.DatosBusquedaFormato = new string[2] { "L" + MaquinaLinea.numlin, "Llenadora" };
            AbrirFormLlenadora(FormDocumentacion, typeof(Llenadora_Documentacion));
        }
        //Abre el form del cambio de turno
        private void CambioTurnoB_Click(object sender, EventArgs e)
        {
            Utilidades.AbrirForm(FormCambioTurno, this, typeof(Llenadora_CambioTurno));
        }
        //Abre el form de control de medida de presión
        private void ControlPresionB_Click(object sender, EventArgs e)
        {
            AbrirFormLlenadora(FormControlPresion, typeof(Llenadora_Control_Presion));
        }
        //Abre el form de control de temperatura
        private void TemperaturaB_Click(object sender, EventArgs e)
        {
            AbrirFormLlenadora(FormControlTemperatura, typeof(Llenadora_Control_Temperatura));
        }
        private void Control30mB_Click(object sender, EventArgs e)
        {
            AbrirFormLlenadora(FormControl30min, typeof(Llenadora_Control30m));
        }
        private void Verificación_Cierre_VolumenB_Click(object sender, EventArgs e)
        {
            AbrirFormLlenadora(FormVerificacionCierre, typeof(Llenadora_Verificacion_Cierre_Volumen));
        }
        private void Control_VolumenB_Click(object sender, EventArgs e)
        {
            AbrirFormLlenadora(FormControlVolumen, typeof(Llenadora_Control_Volumen));
        }
        private void TorquimetroB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.CodigoProd = CodProductoTB.Text;
            AbrirFormLlenadora(FormTorquimetro, typeof(Llenadora_Torquimetro));



        }
        /// <summary>
        /// Boton que muestra el form del registro de botellas rotas.
        /// </summary>
        /// <param name="chDesL">Variable que indíca si se ha checkeado el estado del puesto de trabajo, no dejará abrir este form si no se ha checkeado.</param>
        private void RotasB_Click(object sender, EventArgs e)
        {
            AbrirFormLlenadora(FormRotura, typeof(Llenadora_RotBotellas));
        }
        /// <summary>
        /// Boton que muestra el form del registro de comtentarios.
        /// </summary>
        /// <param name="chDesL">Variable que indíca si se ha checkeado el estado del puesto de trabajo, no dejará abrir este form si no se ha checkeado.</param>
        private void ComentB_Click(object sender, EventArgs e)
        {
            AbrirFormLlenadora(FormComentarios, typeof(Llenadora_Comentarios));
        }
        /// <summary>
        /// Boton que muestra la calculadora en pantalla.
        /// </summary>
        private void CalculadoraB_Click(object sender, EventArgs e)
        {
            if (calculadora1.Visible == true) { calculadora1.Visible = false; CalculadoraB.BackColor = Color.FromArgb(27, 33, 41); }
            else { calculadora1.Visible = true; CalculadoraB.BackColor = Color.DarkSeaGreen; }
        }

        //######################   APARTADO DE LANZAMIENTO   #####################
        /// <summary>
        /// Acción que muestra las especificaciones del producto y los datos de su producción .
        /// </summary>
        private void dgvLlenadora_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ClickEvent == false) ClickEvent = true;
            else
            {
                fila = e.RowIndex;
                columna = e.ColumnIndex;

                //Comprobamos que la fila seleccionada está dentro de los parámetros del dgv con datos.
                if (fila >= 0 && fila < dgv.Rows.Count - 1)
                {
                    if (columna == 3 && dgv.Rows[fila].Cells[3].Value.ToString() != "")
                    {
                        try
                        {
                            //Se muestra la orden
                            string NombrePDF = dgv.Rows[fila].Cells[3].Value.ToString();
                            Process.Start(MaquinaLinea.RutaFolderOrden + NombrePDF + ".PDF");
                        }

                        catch (Exception ex)
                        {
                            Debug.Print(ex.Message);
                            MessageBox.Show("No se ha encontrado el fichero");
                        }
                    }
                    else
                    {
                        //Se completan los datos del BOX - DatosSeleccionadoBOX.
                        ExtraerDatosLanz();
                       
                        //Cambiamos los colores de los text BOX.
                        Utilidades.ColorTextBox(EstadoSelecTB, LiquidoSelecTB, MaterialesSelecTB);

                        //Mostramos el BOX u ocultamos el dgv
                        DatosSeleccionadoBOX.Visible = true;
                        dgv.Visible = false;

                        //Ponemos a verde el boton de seleccion si ya esta seleccionado.
                        SeleccionarProductoB.BackColor = (IDLanzamiento == dgv.Rows[fila].Cells[1].Value.ToString()) ? Color.DarkSeaGreen : Color.FromArgb(27, 33, 41);
                    }
                }
            }
        }
        private void dgvLlenadora_SelectionChanged(object sender, EventArgs e)
        {
            //Si cambia la fila seleccionada volvemos false para simular siempre un doble click
            ClickEvent = false;
        }
        /// <summary>
        /// Función que extrae unos datos impuestos de una fila determinda y escribe en su correspondiente TextBox.
        /// </summary>
        /// <param name="fila">Número entero que indica a la función de que fila tiene que extraer los datos</param>
        public void ExtraerDatosLanz()
        {
            //Estraigo los datos de lanzamiento
            CodProductoSelecTB.Text = dgv.Rows[fila].Cells[2].Value.ToString();
            OrdenSelecTB.Text = dgv.Rows[fila].Cells[3].Value.ToString();
            ClienteSelecTB.Text = dgv.Rows[fila].Cells[4].Value.ToString();
            ProductoSelecTB.Text = dgv.Rows[fila].Cells[5].Value.ToString();
            CajasSelecTB.Text = dgv.Rows[fila].Cells[6].Value.ToString();
            FormatoSelecTB.Text = dgv.Rows[fila].Cells[7].Value.ToString();
            PASelecTB.Text = dgv.Rows[fila].Cells[8].Value.ToString();
            ReferenciaSelecTB.Text = dgv.Rows[fila].Cells[9].Value.ToString();
            GradSelecTB.Text = dgv.Rows[fila].Cells[10].Value.ToString();
            TipoSelecTB.Text = dgv.Rows[fila].Cells[11].Value.ToString();
            ComentariosSelecTB.Text = dgv.Rows[fila].Cells[12].Value.ToString();
            LiquidoSelecTB.Text = dgv.Rows[fila].Cells[13].Value.ToString();
            ObservLabSelecTB.Text = dgv.Rows[fila].Cells[14].Value.ToString();
            MaterialesSelecTB.Text = dgv.Rows[fila].Cells[15].Value.ToString();
            EstadoSelecTB.Text = dgv.Rows[fila].Cells[16].Value.ToString();
            ObservProdSelecTB.Text = dgv.Rows[fila].Cells[18].Value.ToString();
            FiltracionSelecTB.Text =  ExtraerTipoFiltracion(ReferenciaSelecTB.Text);
            Utilidades.MostrarImagen(CodProductoSelecTB.Text, Imagen);
        }
        /// <summary>
        /// Boton que seleciona el producto con el que se va a trabajar.
        /// </summary>
        private void SeleccionarProductoB_Click(object sender, EventArgs e)
        {
            bool cambiofila = false;
            //Obtenemos el ID de la fila seleccionada
            if (IDLanzamiento != dgv.Rows[fila].Cells[1].Value.ToString()) IDLanzamiento = dgv.Rows[fila].Cells[1].Value.ToString(); cambiofila = true;
            //Si alguna cambia a true la seleccion volvemos a extaer los datos.
            if (cambiofila)
            {
                ExtraerDatosProduccion(Utilidades.BuscarFila(IDLanzamiento, dgv));
                //Motramos en verde el boton para indicar que se ha completado el proceso.
                SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
            }
        }
        /// <summary>
        /// Función que extrae los datos del producto que se esta procesando y los escribe en su correspondiente TextBox.
        /// </summary>
        /// <param name="fila">Número entero que indica a la función de que fila tiene que extraer los datos</param>
        /// <param name="dgv">DataGridView donde se opera</param>
        public void ExtraerDatosProduccion(int fila)
        {
            LanzamientoLinea datos_lanzamiento = new LanzamientoLinea();
            datos_lanzamiento.iDLanz = dgv.Rows[fila].Cells[1].Value.ToString();
            datos_lanzamiento.codProducto = dgv.Rows[fila].Cells[2].Value.ToString();
            datos_lanzamiento.orden = dgv.Rows[fila].Cells[3].Value.ToString();
            datos_lanzamiento.cliente = dgv.Rows[fila].Cells[4].Value.ToString();
            datos_lanzamiento.producto = dgv.Rows[fila].Cells[5].Value.ToString();
            datos_lanzamiento.caja = dgv.Rows[fila].Cells[6].Value.ToString();
            datos_lanzamiento.formato = dgv.Rows[fila].Cells[7].Value.ToString();
            datos_lanzamiento.pa = dgv.Rows[fila].Cells[8].Value.ToString();
            datos_lanzamiento.referencia = dgv.Rows[fila].Cells[9].Value.ToString();
            datos_lanzamiento.gdo = dgv.Rows[fila].Cells[10].Value.ToString();
            datos_lanzamiento.tipo = dgv.Rows[fila].Cells[11].Value.ToString();
            datos_lanzamiento.comentarios = dgv.Rows[fila].Cells[12].Value.ToString();
            datos_lanzamiento.liquido = dgv.Rows[fila].Cells[13].Value.ToString();
            datos_lanzamiento.observacionesLaboratorio = dgv.Rows[fila].Cells[14].Value.ToString();
            datos_lanzamiento.materiales = dgv.Rows[fila].Cells[15].Value.ToString();
            datos_lanzamiento.estado = dgv.Rows[fila].Cells[16].Value.ToString();
            datos_lanzamiento.observacionesProduccion = dgv.Rows[fila].Cells[18].Value.ToString();

            //Completamos los datos de producción
            CodProductoTB.Text = datos_lanzamiento.codProducto;
            OrdenTB.Text = datos_lanzamiento.orden;
            ClienteTB.Text = datos_lanzamiento.cliente;
            ProductoTB.Text = datos_lanzamiento.producto;
            GraduacionTB.Text = datos_lanzamiento.gdo;
            NBotTB.Text = Utilidades.ObtenerBotellas(datos_lanzamiento.formato, datos_lanzamiento.caja);
            CapacidadTB.Text = Utilidades.ObtenerCapacidad(datos_lanzamiento.formato);

            if (MaquinaLinea.numlin == 2) Properties.Settings.Default.DPiDLanzLlenL2 = IDLanzamiento;
            if (MaquinaLinea.numlin == 3) Properties.Settings.Default.DPiDLanzLlenL3 = IDLanzamiento;
            if (MaquinaLinea.numlin == 5) Properties.Settings.Default.DPiDLanzLlenL5 = IDLanzamiento;
            Properties.Settings.Default.Save();

            DatosProduccion = new string[8] { OrdenTB.Text, CodProductoTB.Text, datos_lanzamiento.referencia,  CapacidadTB.Text, ProductoTB.Text , ClienteTB.Text, GraduacionTB.Text,NBotTB.Text}; 

            //Se marca de color la fila que se ha seleccionado
            Utilidades.SeleccionFila(dgv, Color.LightBlue, fila);
            //Hacemos un Scroll para que se muestre la fila seleccionada en el centro.
            if (fila >= 12) dgv.FirstDisplayedScrollingRowIndex = fila - 6;
        }
        /// <summary>
        /// Función que extrae el tipo de filtración necesaria para la referencia del liquido en cuestion
        /// </summary>
        public string ExtraerTipoFiltracion(string Referencia)
        {
            string Filtro = "ERROR";
            if (Referencia != "")
            {
                List<string[]> valoresAFiltrar = new List<string[]>();
                string[] filterval = new string[4];
                filterval[0] = "AND";
                filterval[1] = "Ref";
                filterval[2] = "LIKE";
                filterval[3] = " \"" + Referencia + "\"";
                valoresAFiltrar.Add(filterval);


                DataSet excelDataSet = new DataSet();
                string result;
                excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileFiltracion, "Filtros", "MicrasFiltro".Split(';'), valoresAFiltrar, out result);
                //MessageBox.Show(result);

                if (excelDataSet.Tables[0].Rows.Count > 0)
                    {
                    return Filtro = Convert.ToString(excelDataSet.Tables[0].Rows[0]["MicrasFiltro"]);
                    }
                    else return Filtro = " ---";
            }
            else return Filtro;
        }
        /// <summary>
        /// Boton que oculta el BOX datosselecionados y muestra de nuevo el lanzamiento
        /// </summary>
        private void VolverB_Click(object sender, EventArgs e)
        {
            dgv.Visible = true;
            DatosSeleccionadoBOX.Visible = false;

            //Registramos la fila que esta guardada
            int Seleccion = Utilidades.BuscarFila(IDLanzamiento, dgv);
            for (int i = 0; (i < dgv.RowCount - 1); i++)
            {
                if (dgv.Rows[i].Cells["CÓDIGO"].Style.BackColor == System.Drawing.Color.LightBlue && i != Seleccion) Utilidades.SeleccionFila(dgv, Color.White, i);
            }
        }
        /// <summary>
        /// Boton que muestra el form del BOM donde se puede consultar las especificaciones de algún producto.
        /// </summary>
        private void BOMB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.ReferenciaBOM = CodProductoSelecTB.Text;
            MaquinaLinea.VolverA = RetornoBOM.Llen;
            Utilidades.AbrirForm(parentInicio.GetBOM(), parentInicio, typeof(WHPST_BOM));
            Hide();
            Dispose();
        }
        /// <summary>
        /// Función que detecta las celdas, marca las celdas que tienen que sobresalir y detecta que producto esta iniciado para rellenar los datos de producción.
        /// </summary>
        private void dgvLlenadora_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv.Columns[e.ColumnIndex].Name == "LÍQUIDOS")
            {
                switch (Convert.ToString(e.Value))
                {
                    case "OK":
                        e.CellStyle.BackColor = System.Drawing.Color.Green;
                        e.CellStyle.ForeColor = System.Drawing.Color.White;
                        break;
                    case "ELABORACIÓN":
                        e.CellStyle.BackColor = System.Drawing.Color.Yellow;
                        break;
                    case "NOK":
                        e.CellStyle.BackColor = System.Drawing.Color.Red;
                        break;
                }
            }

            if (dgv.Columns[e.ColumnIndex].Name == "ESTADO")
            {
                {
                    switch (Convert.ToString(e.Value))
                    {
                        case "Completado":
                            e.CellStyle.BackColor = System.Drawing.Color.Green;
                            e.CellStyle.ForeColor = System.Drawing.Color.White;
                            break;
                        case "Saltado":
                            e.CellStyle.BackColor = System.Drawing.Color.Yellow;
                            break;
                        case "Iniciado":
                            e.CellStyle.BackColor = System.Drawing.Color.Orange;
                            break;
                        case "Sin Terminar":
                            e.CellStyle.BackColor = System.Drawing.Color.Red;
                            break;
                    }
                }
            }
            if (dgv.Columns[e.ColumnIndex].Name == "MATERIALES")
            {
                {
                    switch (Convert.ToString(e.Value))
                    {
                        case "OK":
                            e.CellStyle.BackColor = System.Drawing.Color.Green;
                            e.CellStyle.ForeColor = System.Drawing.Color.White;
                            break;
                        case "PENDIENTE":
                            e.CellStyle.BackColor = System.Drawing.Color.Yellow;
                            break;
                        case "NOK":
                            e.CellStyle.BackColor = System.Drawing.Color.Red;
                            break;
                    }
                }
            }
        }

        //#############################   OTRAS FUNCIONES   ############################
        private void ActivarParadaGuardada()
        {
            char[] t1 = new char[6];
            for (int i = 0; i < 6; i++) { t1[i] = '1'; }
            if (MaquinaLinea.numlin == 2 && Properties.Settings.Default.Paro_Llen_L2)
            {
                inicio_paro = true;
                hora_ini_paro = Properties.Settings.Default.Hora_Paro_Llen_L2;
                t1[0] = Properties.Settings.Default.Hora_Paro_Llen_L2[0];
                t1[1] = Properties.Settings.Default.Hora_Paro_Llen_L2[1];
                t1[2] = Properties.Settings.Default.Hora_Paro_Llen_L2[3];
                t1[3] = Properties.Settings.Default.Hora_Paro_Llen_L2[4];
                t1[4] = Properties.Settings.Default.Hora_Paro_Llen_L2[6];
                t1[5] = Properties.Settings.Default.Hora_Paro_Llen_L2[7];

            }
            else if (MaquinaLinea.numlin == 3 && Properties.Settings.Default.Paro_Llen_L3)
            {
                inicio_paro = true;
                hora_ini_paro = Properties.Settings.Default.Hora_Paro_Llen_L3;
                t1[0] = Properties.Settings.Default.Hora_Paro_Llen_L3[0];
                t1[1] = Properties.Settings.Default.Hora_Paro_Llen_L3[1];
                t1[2] = Properties.Settings.Default.Hora_Paro_Llen_L3[3];
                t1[3] = Properties.Settings.Default.Hora_Paro_Llen_L3[4];
                t1[4] = Properties.Settings.Default.Hora_Paro_Llen_L3[6];
                t1[5] = Properties.Settings.Default.Hora_Paro_Llen_L3[7];

            }
            else if (MaquinaLinea.numlin == 5 && Properties.Settings.Default.Paro_Llen_L5)
            {
                inicio_paro = true;
                hora_ini_paro = Properties.Settings.Default.Hora_Paro_Llen_L5;
                t1[0] = Properties.Settings.Default.Hora_Paro_Llen_L5[0];
                t1[1] = Properties.Settings.Default.Hora_Paro_Llen_L5[1];
                t1[2] = Properties.Settings.Default.Hora_Paro_Llen_L5[3];
                t1[3] = Properties.Settings.Default.Hora_Paro_Llen_L5[4];
                t1[4] = Properties.Settings.Default.Hora_Paro_Llen_L5[6];
                t1[5] = Properties.Settings.Default.Hora_Paro_Llen_L5[7];
            }

            temp[0] = Int32.Parse(t1[0].ToString()) * 10 + Int32.Parse(t1[1].ToString());
            temp[1] = Int32.Parse(t1[2].ToString()) * 10 + Int32.Parse(t1[3].ToString());
            temp[2] = Int32.Parse(t1[4].ToString()) * 10 + Int32.Parse(t1[5].ToString());
        }
        internal void ActivarTimer()
        {
            timer_cambio_turno.Enabled = true;
        }
        internal void AdvertenciaParo(bool paro)
        {
            inicio_paro = paro;
            FormParo = null;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (!MaquinaLinea.controlsavedLlenL2)
                {
                    Control30mB.BackColor = Utilidades.AvisoBoton(Control30mB.BackColor);
                }

                if ((DateTime.Now.Minute != 30 && DateTime.Now.Minute != 00) && MaquinaLinea.controlsavedLlenL2)
                {
                    MaquinaLinea.controlsavedLlenL2 = false;
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (!MaquinaLinea.controlsavedLlenL3)
                {
                    Control30mB.BackColor = Utilidades.AvisoBoton(Control30mB.BackColor);
                }


                if ((DateTime.Now.Minute != 30 && DateTime.Now.Minute != 00) && MaquinaLinea.controlsavedLlenL3)
                {
                    MaquinaLinea.controlsavedLlenL3 = false;
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (!MaquinaLinea.controlsavedLlenL5)
                {
                    Control30mB.BackColor = Utilidades.AvisoBoton(Control30mB.BackColor);
                }

                if ((DateTime.Now.Minute != 30 && DateTime.Now.Minute != 00) && MaquinaLinea.controlsavedLlenL5)
                {
                    MaquinaLinea.controlsavedLlenL5 = false;
                }
            }


        }
        private void AbrirFormLlenadora(Form FormAbierto, Type t)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chLlenL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormAbierto, this, t);
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Llenadora_CambioTurno));
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chLlenL3 == true || MaquinaLinea.usuario == "Administracion")
                {

                    Utilidades.AbrirForm(FormAbierto, this, t);
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Llenadora_CambioTurno));
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chLlenL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormAbierto, this, t);
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Llenadora_CambioTurno));
                }
            }
        }
        private void timer_cambio_turno_Tick(object sender, EventArgs e)
        {
            CambioTurnoB.BackColor = Utilidades.AvisoBoton(CambioTurnoB.BackColor);

            if (MaquinaLinea.numlin == 2 && Properties.Settings.Default.chLlenL2) { timer_cambio_turno.Enabled = false; CambioTurnoB.BackColor = Color.White; }
            if (MaquinaLinea.numlin == 3 && Properties.Settings.Default.chLlenL3) { timer_cambio_turno.Enabled = false; CambioTurnoB.BackColor = Color.White; }
            if (MaquinaLinea.numlin == 5 && Properties.Settings.Default.chLlenL5) { timer_cambio_turno.Enabled = false; CambioTurnoB.BackColor = Color.White; }
        }
        internal void ActivarTimerControl30Min()
        {
            timer2.Enabled = true;
        }
        private void LanzamientoActualizado()
        {
            contador = 10;
            LanzActualizado = true;
            ImagenCarga.BackgroundImage = Properties.Resources.OKRefresco;
        }
        //Incrementa o decrementan el contador
        private void SumaBotellasB_Click(object sender, EventArgs e)
        {
            ConteoBotellasTB.Text = Convert.ToString(Convert.ToDouble(ConteoBotellasTB.Text) + 1);
            OK_ConteoB.BackColor = Color.Maroon;
        }
        private void RestaBotellasB_Click(object sender, EventArgs e)
        {
            ConteoBotellasTB.Text = Convert.ToString(Convert.ToDouble(ConteoBotellasTB.Text) - 1);
            OK_ConteoB.BackColor = Color.Maroon;
        }
        private void OK_ConteoB_Click(object sender, EventArgs e)
        {
            if (NBotTB.Text != "")
            {
                DatosProduccion[7] = Convert.ToString(Convert.ToDouble(DatosProduccion[7]) + Convert.ToDouble(ConteoBotellasTB.Text));
                NBotTB.Text = DatosProduccion[7];
                ConteoBotellasTB.Text = "0";
                OK_ConteoB.BackColor = Color.FromArgb(27, 33, 41);
            }
        }

        //######################   FUNCIONES DE SET AND GET FORMS   #####################
        public WHPST_INICIO GetParentInicio()
        {
            return parentInicio;
        }
        public void SetComentarios(Llenadora_Comentarios c)
        {
            FormComentarios = c;
        }
        public Llenadora_Comentarios GetComentarios()
        {
            return FormComentarios;
        }

    }
}

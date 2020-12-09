using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.ProgramMenus;
using WHPS.Utiles;

namespace WHPS.Encajonadora
{
    public partial class MainEncajonadora : Form
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
        public static string[] DatosProduccion = new string[10];

        public int[] temp = new int[3];

        public static WHPST_INICIO parentInicio;
        public static Encajonadora_CambioTurno FormCambioTurno;
        public static Encajonadora_Comentarios FormComentarios;
        public static Encajonadora_Parte FormParte;
        public static Encajonadora_Registro_Paro FormParo;
        public static Encajonadora_Registro_Produccion FormProduccion;
        public static Encajonadora_RotBotellas FormRotura;

        public MainEncajonadora(WHPST_INICIO p)
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
            Utilidades.AbrirForm(parentInicio, this, typeof(WHPST_INICIO));
        }
        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }
        /*Carga la directamente el responsable de la instalación y reconoce 
          si se ha comprobado el estado la instalación en el cambio de turno.*/
        public void MainEncajonadora_Load(object sender, EventArgs e)
        {
            //Muestra la hora ya que el timer tarda 1s en tomar el control del Label lbReloj.
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //Muestra la tabla de lanzaminento.
            ExcelUtiles.CrearTablaLanzamientos(dgvEncajonadora);
            LanzamientoActualizado();
            //Función que determina el cambio de turno.
            Utilidades.FuncionLoad(MaquinistaTB, MaquinaLinea.MEncajonadora, MaquinaLinea.chEncL2, MaquinaLinea.chEncL3, MaquinaLinea.chEncL5, CambioTurnoB);

            //Definimos nuestro ID DE LANZAMIENTO
            if (MaquinaLinea.numlin == 2) IDLanzamiento = Properties.Settings.Default.DPiDLanzEncL2;
            if (MaquinaLinea.numlin == 3) IDLanzamiento = Properties.Settings.Default.DPiDLanzEncL3;
            if (MaquinaLinea.numlin == 5) IDLanzamiento = Properties.Settings.Default.DPiDLanzEncL5;

            //Estado del boton de paro y de producción
            if (MaquinaLinea.numlin == 2)
            {
                LoteTB.Text = Properties.Settings.Default.DPLoteEncL2;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL2;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEncL2;
            }
            if (MaquinaLinea.numlin == 3)
            {
                LoteTB.Text = Properties.Settings.Default.DPLoteEncL3;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL3;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEncL3;

            }
            if (MaquinaLinea.numlin == 5)
            {
                LoteTB.Text = Properties.Settings.Default.DPLoteEncL5;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL5;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEncL5;

            }
            //Extraemos los datos de producción
            if (IDLanzamiento != "") ExtraerDatosProduccion(Utilidades.BuscarFila(IDLanzamiento, dgvEncajonadora));
        }

        //Temporizador que controla la hora y y el parpade de aviso de finalización de turno
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
                ExcelUtiles.CrearTablaLanzamientos(dgvEncajonadora);
                LanzamientoActualizado();
                if (Properties.Settings.Default.DPiDLanzEncL2 != "" && MaquinaLinea.numlin == 2) ExtraerDatosProduccion(Utilidades.BuscarFila(Properties.Settings.Default.DPiDLanzEncL2, dgvEncajonadora));
                if (Properties.Settings.Default.DPiDLanzEncL3 != "" && MaquinaLinea.numlin == 3) ExtraerDatosProduccion(Utilidades.BuscarFila(Properties.Settings.Default.DPiDLanzEncL3, dgvEncajonadora));
                if (Properties.Settings.Default.DPiDLanzEncL5 != "" && MaquinaLinea.numlin == 5) ExtraerDatosProduccion(Utilidades.BuscarFila(Properties.Settings.Default.DPiDLanzEncL5, dgvEncajonadora));
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
                if (MaquinaLinea.chEncL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                        Properties.Settings.Default.ParoDesdeEncL2 = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
                        FormParo = new Encajonadora_Registro_Paro(this,inicio_paro, hora_ini_paro, temp);
                        Hide();
                        FormParo.Show();

                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Encajonadora_CambioTurno));

                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chEncL3 == true || MaquinaLinea.usuario == "Administracion")
                {              
                        Properties.Settings.Default.ParoDesdeEncL3 = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
                        FormParo = new Encajonadora_Registro_Paro(this,inicio_paro, hora_ini_paro, temp);
                        Hide();
                        FormParo.Show();
                    // Form.PDesdeTB.Text = (inicio_paro == true) ? hora_ini_paro : DateTime.Now.ToString("HH:mm:ss");

                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Encajonadora_CambioTurno));

                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chEncL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                  
                        Properties.Settings.Default.ParoDesdeEncL5 = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
                        FormParo = new Encajonadora_Registro_Paro(this,inicio_paro, hora_ini_paro, temp);
                        Hide();
                        FormParo.Show();
                    //Form.PDesdeTB.Text = (inicio_paro == true) ? hora_ini_paro : DateTime.Now.ToString("HH:mm:ss");


                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Encajonadora_CambioTurno));

                }
            }
        }
        private void RegistroB_Click(object sender, EventArgs e)
        {
            if (IDLanzamiento == "") MessageBox.Show("Debes primero realizar la selección del producto. Para ello, pulsa el boton que aparece haciendo doble click en el mismo.");
            else AbrirFormEncajonadora(FormProduccion, typeof(Encajonadora_Registro_Produccion));
        }
        private void SiguienteB_Click(object sender, EventArgs e)
        {
            // MaquinaLinea.CapacidadLlen = CapacidadTB.Text;
            //MaquinaLinea.GraduacionLLen = GraduacionTB.Text;
            AbrirFormEncajonadora(FormParte, typeof(Encajonadora_Parte));   
        }
        //Abre el form del cambio de turno
        private void CambioTurnoB_Click(object sender, EventArgs e)
        {
            Utilidades.AbrirForm(FormCambioTurno, this, typeof(Encajonadora_CambioTurno));
        }
        //Abre el form de rotura de botellas
        private void RotasB_Click(object sender, EventArgs e)
        {
            AbrirFormEncajonadora(FormRotura, typeof(Encajonadora_RotBotellas));
        }
        //Abre el form de comentarios
        private void ComentB_Click(object sender, EventArgs e)
        {
            AbrirFormEncajonadora(FormComentarios, typeof(Encajonadora_Comentarios));
        }
        private void CalculadoraB_Click(object sender, EventArgs e)
        {
            if (calculadora1.Visible == true) { calculadora1.Visible = false; CalculadoraB.BackColor = Color.FromArgb(27, 33, 41); }
            else { calculadora1.Visible = true; CalculadoraB.BackColor = Color.DarkSeaGreen; }
        }

        //######################   APARTADO DE LANZAMIENTO   #####################
        /// <summary>
        /// Acción que muestra las especificaciones del producto y los datos de su producción .
        /// </summary>
        private void dgvEncajonadora_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ClickEvent == false) ClickEvent = true;
            else
            {
                fila = e.RowIndex;
                columna = e.ColumnIndex;
                if (fila >= 0 && fila < dgvEncajonadora.Rows.Count - 1)
                {
                    if (columna == 3 && dgvEncajonadora.Rows[fila].Cells[3].Value.ToString() != "")
                    {
                        try
                        {
                            //Se muestra la orden
                            string NombrePDF = dgvEncajonadora.Rows[fila].Cells[3].Value.ToString();
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
                        dgvEncajonadora.Visible = false;

                        //Ponemos a verde el boton de seleccion si ya esta seleccionado.
                        SeleccionarProductoB.BackColor = (IDLanzamiento == dgvEncajonadora.Rows[fila].Cells[1].Value.ToString()) ? Color.DarkSeaGreen : Color.FromArgb(27, 33, 41);
                    }
                }
            }
        }
        private void dgvEncajonadora_SelectionChanged(object sender, EventArgs e)
        {
            ClickEvent = false;
        }
        /// <summary>
        /// Función que extrae unos datos impuestos de una fila determinda y escribe en su correspondiente TextBox.
        /// </summary>
        /// <param name="fila">Número entero que indica a la función de que fila tiene que extraer los datos</param>
        public void ExtraerDatosLanz()
        {
            //Estraigo los datos de lanzamiento
            CodProductoSelecTB.Text = dgvEncajonadora.Rows[fila].Cells[2].Value.ToString();
            OrdenSelecTB.Text = dgvEncajonadora.Rows[fila].Cells[3].Value.ToString();
            ClienteSelecTB.Text = dgvEncajonadora.Rows[fila].Cells[4].Value.ToString();
            ProductoSelecTB.Text = dgvEncajonadora.Rows[fila].Cells[5].Value.ToString();
            CajasSelecTB.Text = dgvEncajonadora.Rows[fila].Cells[6].Value.ToString();
            FormatoSelecTB.Text = dgvEncajonadora.Rows[fila].Cells[7].Value.ToString();
            PASelecTB.Text = dgvEncajonadora.Rows[fila].Cells[8].Value.ToString();
            ReferenciaSelecTB.Text = dgvEncajonadora.Rows[fila].Cells[9].Value.ToString();
            GradSelecTB.Text = dgvEncajonadora.Rows[fila].Cells[10].Value.ToString();
            TipoSelecTB.Text = dgvEncajonadora.Rows[fila].Cells[11].Value.ToString();
            ComentariosSelecTB.Text = dgvEncajonadora.Rows[fila].Cells[12].Value.ToString();
            LiquidoSelecTB.Text = dgvEncajonadora.Rows[fila].Cells[13].Value.ToString();
            ObservLabSelecTB.Text = dgvEncajonadora.Rows[fila].Cells[14].Value.ToString();
            MaterialesSelecTB.Text = dgvEncajonadora.Rows[fila].Cells[15].Value.ToString();
            EstadoSelecTB.Text = dgvEncajonadora.Rows[fila].Cells[16].Value.ToString();
            ObservProdSelecTB.Text = dgvEncajonadora.Rows[fila].Cells[18].Value.ToString();
            Utilidades.MostrarImagen(CodProductoSelecTB.Text, Imagen);
        }
        private void SeleccionarProductoB_Click(object sender, EventArgs e)
        {
            bool cambiofila = false;
            //Obtenemos el ID de la fila seleccionada
            if (IDLanzamiento != dgvEncajonadora.Rows[fila].Cells[1].Value.ToString()) IDLanzamiento = dgvEncajonadora.Rows[fila].Cells[1].Value.ToString(); cambiofila = true;
            //Si alguna cambia a true la seleccion volvemos a extaer los datos.
            if (cambiofila)
            {
                ExtraerDatosProduccion(Utilidades.BuscarFila(IDLanzamiento, dgvEncajonadora));
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
            datos_lanzamiento.iDLanz = dgvEncajonadora.Rows[fila].Cells[1].Value.ToString();
            datos_lanzamiento.codProducto = dgvEncajonadora.Rows[fila].Cells[2].Value.ToString();
            datos_lanzamiento.orden = dgvEncajonadora.Rows[fila].Cells[3].Value.ToString();
            datos_lanzamiento.cliente = dgvEncajonadora.Rows[fila].Cells[4].Value.ToString();
            datos_lanzamiento.producto = dgvEncajonadora.Rows[fila].Cells[5].Value.ToString();
            datos_lanzamiento.caja = dgvEncajonadora.Rows[fila].Cells[6].Value.ToString();
            datos_lanzamiento.formato = dgvEncajonadora.Rows[fila].Cells[7].Value.ToString();
            datos_lanzamiento.pa = dgvEncajonadora.Rows[fila].Cells[8].Value.ToString();
            datos_lanzamiento.referencia = dgvEncajonadora.Rows[fila].Cells[9].Value.ToString();
            datos_lanzamiento.gdo = dgvEncajonadora.Rows[fila].Cells[10].Value.ToString();
            datos_lanzamiento.tipo = dgvEncajonadora.Rows[fila].Cells[11].Value.ToString();
            datos_lanzamiento.comentarios = dgvEncajonadora.Rows[fila].Cells[12].Value.ToString();
            datos_lanzamiento.liquido = dgvEncajonadora.Rows[fila].Cells[13].Value.ToString();
            datos_lanzamiento.observacionesLaboratorio = dgvEncajonadora.Rows[fila].Cells[14].Value.ToString();
            datos_lanzamiento.materiales = dgvEncajonadora.Rows[fila].Cells[15].Value.ToString();
            datos_lanzamiento.estado = dgvEncajonadora.Rows[fila].Cells[16].Value.ToString();
            datos_lanzamiento.observacionesProduccion = dgvEncajonadora.Rows[fila].Cells[18].Value.ToString();

            CodProductoTB.Text = datos_lanzamiento.codProducto;
            OrdenTB.Text = datos_lanzamiento.orden;
            ClienteTB.Text = datos_lanzamiento.cliente;
            ProductoTB.Text = datos_lanzamiento.producto;
            string Graduacion = datos_lanzamiento.gdo;
            FormatoTB.Text = datos_lanzamiento.formato;
            string Botellas = Utilidades.ObtenerBotellas(datos_lanzamiento.formato, datos_lanzamiento.caja);
            string Capacidad = Utilidades.ObtenerCapacidad(datos_lanzamiento.formato);
            NCajasTB.Text = datos_lanzamiento.caja;

            if (MaquinaLinea.numlin == 2) Properties.Settings.Default.DPiDLanzEncL2 = IDLanzamiento;
            if (MaquinaLinea.numlin == 3) Properties.Settings.Default.DPiDLanzEncL3 = IDLanzamiento;
            if (MaquinaLinea.numlin == 5) Properties.Settings.Default.DPiDLanzEncL5 = IDLanzamiento;
            Properties.Settings.Default.Save();

            DatosProduccion = new string[10] { OrdenTB.Text, CodProductoTB.Text, datos_lanzamiento.referencia, Capacidad, ProductoTB.Text, ClienteTB.Text, Graduacion, Botellas, FormatoTB.Text, NCajasTB.Text };

            //Se marca de color la fila que se ha seleccionado
            Utilidades.SeleccionFila(dgvEncajonadora, Color.LightBlue, fila);
            //Hacemos un Scroll para que se muestre la fila seleccionada en el centro.
            if (fila >= 12) dgvEncajonadora.FirstDisplayedScrollingRowIndex = fila - 6;
        }
        /// <summary>
        /// Boton que oculta el BOX datosselecionados y muestra de nuevo el lanzamiento
        /// </summary>
        private void VolverB_Click(object sender, EventArgs e)
        {
            dgvEncajonadora.Visible = true;
            DatosSeleccionadoBOX.Visible = false;

            //Registramos la fila que esta guardada
            int Seleccion = Utilidades.BuscarFila(IDLanzamiento, dgvEncajonadora);
            for (int i = 0; (i < dgvEncajonadora.RowCount - 1); i++)
            {
                if (dgvEncajonadora.Rows[i].Cells["CÓDIGO"].Style.BackColor == System.Drawing.Color.LightBlue && i != Seleccion) Utilidades.SeleccionFila(dgvEncajonadora, Color.White, i);
            }
        }
        /// <summary>
        /// Boton que muestra el form del BOM donde se puede consultar las especificaciones de algún producto.
        /// </summary>
        private void BOMB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.ReferenciaBOM = CodProductoSelecTB.Text;
            MaquinaLinea.VolverA = RetornoBOM.Enc;
            Utilidades.AbrirForm(parentInicio.GetBOM(), parentInicio, typeof(WHPST_BOM));
            Hide();
            Dispose();

        }
        /// <summary>
        /// Función que detecta las celdas, marca las celdas que tienen que sobresalir y detecta que producto esta iniciado para rellenar los datos de producción.
        /// </summary>
        private void dgvEncajonadora_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvEncajonadora.Columns[e.ColumnIndex].Name == "LÍQUIDOS")
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
            if (dgvEncajonadora.Columns[e.ColumnIndex].Name == "MATERIALES")
            {
                switch (Convert.ToString(e.Value))
                {
                    case "OK":
                        e.CellStyle.BackColor = System.Drawing.Color.Green;
                        e.CellStyle.ForeColor = System.Drawing.Color.White;
                        break;
                    case "PENDIENTE":
                        e.CellStyle.BackColor = System.Drawing.Color.Orange;
                        break;
                    case "NOK":
                        e.CellStyle.BackColor = System.Drawing.Color.Red;
                        break;
                }
            }
            if (dgvEncajonadora.Columns[e.ColumnIndex].Name == "ESTADO")
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
        }




        //#############################   OTRAS FUNCIONES   ############################
        private void ActivarParadaGuardada()
        {
            char[] t1 = new char[6];
            for (int i = 0; i < 6; i++) { t1[i] = '1'; }

            if (MaquinaLinea.numlin == 2 && Properties.Settings.Default.Paro_Enc_L2)
            {
                inicio_paro = true;
                hora_ini_paro = Properties.Settings.Default.Hora_Paro_Enc_L2;
                t1[0] = Properties.Settings.Default.Hora_Paro_Enc_L2[0];
                t1[1] = Properties.Settings.Default.Hora_Paro_Enc_L2[1];
                t1[2] = Properties.Settings.Default.Hora_Paro_Enc_L2[3];
                t1[3] = Properties.Settings.Default.Hora_Paro_Enc_L2[4];
                t1[4] = Properties.Settings.Default.Hora_Paro_Enc_L2[6];
                t1[5] = Properties.Settings.Default.Hora_Paro_Enc_L2[7];

            }
            else if (MaquinaLinea.numlin == 3 && Properties.Settings.Default.Paro_Enc_L3)
            {
                inicio_paro = true;
                hora_ini_paro = Properties.Settings.Default.Hora_Paro_Enc_L3;
                t1[0] = Properties.Settings.Default.Hora_Paro_Enc_L3[0];
                t1[1] = Properties.Settings.Default.Hora_Paro_Enc_L3[1];
                t1[2] = Properties.Settings.Default.Hora_Paro_Enc_L3[3];
                t1[3] = Properties.Settings.Default.Hora_Paro_Enc_L3[4];
                t1[4] = Properties.Settings.Default.Hora_Paro_Enc_L3[6];
                t1[5] = Properties.Settings.Default.Hora_Paro_Enc_L3[7];

            }
            else if (MaquinaLinea.numlin == 5 && Properties.Settings.Default.Paro_Enc_L5)
            {
                inicio_paro = true;
                hora_ini_paro = Properties.Settings.Default.Hora_Paro_Enc_L5;
                t1[0] = Properties.Settings.Default.Hora_Paro_Enc_L5[0];
                t1[1] = Properties.Settings.Default.Hora_Paro_Enc_L5[1];
                t1[2] = Properties.Settings.Default.Hora_Paro_Enc_L5[3];
                t1[3] = Properties.Settings.Default.Hora_Paro_Enc_L5[4];
                t1[4] = Properties.Settings.Default.Hora_Paro_Enc_L5[6];
                t1[5] = Properties.Settings.Default.Hora_Paro_Enc_L5[7];
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
        private void AbrirFormEncajonadora(Form FormAbierto, Type t)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chEncL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormAbierto, this, t);
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Encajonadora_CambioTurno));
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chEncL3 == true || MaquinaLinea.usuario == "Administracion")
                {

                    Utilidades.AbrirForm(FormAbierto, this, t);
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Encajonadora_CambioTurno));
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chEncL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormAbierto, this, t);
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Encajonadora_CambioTurno));
                }
            }
        }
        private void timer_cambio_turno_Tick(object sender, EventArgs e)
        {
            CambioTurnoB.BackColor = Utilidades.AvisoBoton(CambioTurnoB.BackColor);
            if (MaquinaLinea.numlin == 2 && Properties.Settings.Default.chEncL2) { timer_cambio_turno.Enabled = false; CambioTurnoB.BackColor = Color.White; }
            if (MaquinaLinea.numlin == 3 && Properties.Settings.Default.chEncL3) { timer_cambio_turno.Enabled = false; CambioTurnoB.BackColor = Color.White; }
            if (MaquinaLinea.numlin == 5 && Properties.Settings.Default.chEncL5) { timer_cambio_turno.Enabled = false; CambioTurnoB.BackColor = Color.White; }


        }
        private void LanzamientoActualizado()
        {
            contador = 10;
            LanzActualizado = true;
            ImagenCarga.BackgroundImage = Properties.Resources.OKRefresco;
        }
        //Incrementa o decrementan el contador
        private void OK_ConteoB_Click(object sender, EventArgs e)
        {
            if (NCajasTB.Text != "")
            {


                DatosProduccion[7] = Convert.ToString(Convert.ToDouble(DatosProduccion[9]) + Convert.ToDouble(ConteoBotellasTB.Text));
                NCajasTB.Text = DatosProduccion[9];
                ConteoBotellasTB.Text = "0";
                OK_ConteoB.BackColor = Color.FromArgb(27, 33, 41);
            }
        }
        private void SumaCajasB_Click(object sender, EventArgs e)
        {
            ConteoBotellasTB.Text = Convert.ToString(Convert.ToDouble(ConteoBotellasTB.Text) + 1);
            OK_ConteoB.BackColor = Color.Maroon;
        }
        private void RestaCajasB_Click(object sender, EventArgs e)
        {
            ConteoBotellasTB.Text = Convert.ToString(Convert.ToDouble(ConteoBotellasTB.Text) - 1);
            OK_ConteoB.BackColor = Color.Maroon;
        }

        //######################   FUNCIONES DE SET AND GET FORMS   #####################
        public WHPST_INICIO GetParentInicio()
        {
            return parentInicio;
        }
        public void SetComentarios(Encajonadora_Comentarios c)
        {
            FormComentarios = c;
        }
        public Encajonadora_Comentarios GetComentarios()
        {
            return FormComentarios;
        }
    }
}
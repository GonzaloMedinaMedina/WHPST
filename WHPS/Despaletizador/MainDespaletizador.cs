using System;
using System.Data;
using System.Windows.Forms;
using WHPS.Controller;
using WHPS.Model;
using WHPS.Utiles;
using WHPS.ProgramMenus;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace WHPS.Despaletizador
{
    public partial class MainDespaletizador : Form
    {
        //Varibles de parpadeo parada
        public bool statusboton = false, inicio_paro = false, statusboton_paro = false;
        public string hora_ini_paro = "";


        public int[] temp = new int[3];
        int fila, columna;
        bool OK_Fila = false;
        double botellascaja;
        bool ClickEvent = false;

        public static WHPST_INICIO parentInicio;
        public static Despaletizador_Botellas FormBotellas;
        public static Despaletizador_Cierres FormCierres;
        public static Despaletizador_CambioTurno FormCambioTurno;
        public static Despaletizador_Comentarios FormComentarios;
        public static Despaletizador_Registro_Paro FormParo;
        public static Despaletizador_RotBotellas FormRotura;
   
        public MainDespaletizador(WHPST_INICIO p)
        {
            InitializeComponent();
            parentInicio = p;
            ActivarParadaGuardada();
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

        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        public void MainDespaletizador_Load(object sender, EventArgs e)
        {
            //Muestra la hora ya que el timer tarda 1s en tomar el control del Label lbReloj.
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //Muestra la tabla de lanzaminento.
            ExcelUtiles.CrearTablaLanzamientos(dgvDespaletizador);
            
            //Función que determina el cambio de turno.
            Utilidades.FuncionLoad(MaquinistaTB, MaquinaLinea.MDespaletizador, MaquinaLinea.chDesL2, MaquinaLinea.chDesL3, MaquinaLinea.chDesL5, CambioTurnoB);

            //Extraemos los datos de producción
            if (MaquinaLinea.numlin == 2)
            {
                if (Properties.Settings.Default.DPiDLanzDespL2 != "") ExtraerDatosProduccion(BuscarFila(Properties.Settings.Default.DPiDLanzDespL2));
                NumBotTB.Text = Properties.Settings.Default.DPNumBotDespL2.ToString();
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (Properties.Settings.Default.DPiDLanzDespL3 != "") ExtraerDatosProduccion(BuscarFila(Properties.Settings.Default.DPiDLanzDespL3));
                NumBotTB.Text = Properties.Settings.Default.DPNumBotDespL3.ToString();
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (Properties.Settings.Default.DPiDLanzDespL5 != "") ExtraerDatosProduccion(BuscarFila(Properties.Settings.Default.DPiDLanzDespL5));
                NumBotTB.Text = Properties.Settings.Default.DPNumBotDespL5.ToString();
            }
        }

        private void CalculadoraB_Click(object sender, EventArgs e)
        {
            if (calculadora1.Visible == true) { calculadora1.Visible = false; CalculadoraB.BackColor = Color.FromArgb(27, 33, 41); }
            else { calculadora1.Visible = true; CalculadoraB.BackColor = Color.DarkSeaGreen; }
        }

        /// <summary>
        /// Función del temporizador que se ejecuta cada segundo, indíca la hora y si se activa la alarma. 
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");


            if (inicio_paro)
            {             
                if (statusboton_paro)
                {
                    ParoB.BackColor = Color.Red;
                    ParoB.Update();
                    statusboton_paro = false;
                }
                else
                {
                    statusboton_paro = true;
                    ParoB.BackColor = Color.Yellow;
                    ParoB.Update();
                }
            }

        }



        //#############################   BOTONES   ############################
        /// <summary>
        /// Boton que borra los datos del número de botellas que han entrado en el despaletizador.
        /// </summary>
        private void BorrarB_Click(object sender, EventArgs e)
        {
            NumBotTB.Text = "0";

            if (MaquinaLinea.numlin == 2) Properties.Settings.Default.DPNumBotDespL2 = 0;
            if (MaquinaLinea.numlin == 3) Properties.Settings.Default.DPNumBotDespL3 = 0;
            if (MaquinaLinea.numlin == 5) Properties.Settings.Default.DPNumBotDespL5 = 0;

            Properties.Settings.Default.Save();
        }
        /// <summary>
        /// Boton que muestra el form del cambio de turno.
        /// </summary>
        private void CambioTurnoB_Click(object sender, EventArgs e)
        {
            Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));

        }
        /// <summary>
        /// Boton que muestra el form del registro de botellas.
        /// </summary>
        /// <param name="chDesL">Variable que indíca si se ha checkeado el estado del puesto de trabajo, no dejará abrir este form si no se ha checkeado.</param>
        private void BottB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chDesL2 == true  || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormBotellas, this, typeof(Despaletizador_Botellas));
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chDesL3 == true || MaquinaLinea.usuario == "Administracion")
                {

                    Utilidades.AbrirForm(FormBotellas, this, typeof(Despaletizador_Botellas));
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chDesL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormBotellas, this, typeof(Despaletizador_Botellas));
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));
                }
            }
        }
        /// <summary>
        /// Boton que muestra el form del registro de cierres.
        /// </summary>
        /// <param name="chDesL">Variable que indíca si se ha checkeado el estado del puesto de trabajo, no dejará abrir este form si no se ha checkeado.</param>      
        private void CierreB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.CodigoProd = CodProductoTB.Text;
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chDesL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormCierres, this, typeof(Despaletizador_Cierres));
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));

                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chDesL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormCierres, this, typeof(Despaletizador_Cierres));
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));

                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chDesL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormCierres, this, typeof(Despaletizador_Cierres));
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));

                }
            }
        }
        /// <summary>
        /// Boton que muestra el form del registro de botellas rotas.
        /// </summary>
        /// <param name="chDesL">Variable que indíca si se ha checkeado el estado del puesto de trabajo, no dejará abrir este form si no se ha checkeado.</param>
        private void RotasB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.RotCodProd = CodProductoTB.Text;

            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chDesL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormRotura, this, typeof(Despaletizador_RotBotellas));
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));

                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chDesL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormRotura, this, typeof(Despaletizador_RotBotellas));
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));

                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chDesL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormRotura, this, typeof(Despaletizador_RotBotellas));
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));

                }
            }
        }
        /// <summary>
        /// Boton que muestra el form del registro de comtentarios.
        /// </summary>
        /// <param name="chDesL">Variable que indíca si se ha checkeado el estado del puesto de trabajo, no dejará abrir este form si no se ha checkeado.</param>
        private void ComentB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chDesL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormComentarios, this, typeof(Despaletizador_Comentarios));

                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));

                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chDesL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormComentarios, this, typeof(Despaletizador_Comentarios));

                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));

                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chDesL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormComentarios, this, typeof(Despaletizador_Comentarios));
                }
                else
                {
         
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));

                }
            }

        }
        /// <summary>
        /// Boton que muestra el form de notificación de parada.
        /// </summary>
        /// <param name="chDesL">Variable que indíca si se ha checkeado el estado del puesto de trabajo, no dejará abrir este form si no se ha checkeado.</param>
        private void ParoB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chDesL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    FormParo = new Despaletizador_Registro_Paro(this,inicio_paro, hora_ini_paro, temp);
                    Hide();
                    FormParo.Show();

                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));

                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chDesL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    FormParo = new Despaletizador_Registro_Paro(this,inicio_paro, hora_ini_paro, temp);
                    Hide();
                    FormParo.Show();

                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));

                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chDesL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    FormParo = new Despaletizador_Registro_Paro(this,inicio_paro, hora_ini_paro, temp);
                    Hide();
                    FormParo.Show();

                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Despaletizador_CambioTurno));

                }
            }
        }
        //######################   APARTADO DE LANZAMIENTO   #####################
        /// <summary>
        /// Acción que muestra las especificaciones del producto y los datos de su producción .
        /// </summary>
        private void dgvDespaletizador_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            fila = e.RowIndex;
            columna = e.ColumnIndex;
            if (ClickEvent == false)
            {
                ClickEvent = true;
            }
            else
            {
                if (columna == 3 && dgvDespaletizador.Rows[fila].Cells[3].Value.ToString() != "")
                {
                    try
                    {
                        //Se muestra la orden
                        string NombrePDF = dgvDespaletizador.Rows[fila].Cells[3].Value.ToString();
                        Process.Start(MaquinaLinea.RutaFolderOrden + NombrePDF + ".PDF");
                    }

                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                        MessageBox.Show("No se ha encontrado el fichero");
                    }
                }
                if (fila >= 0 && columna != 3)
                {
                    //Extraer los datos de esa filia y los coloca en el BOX datosseleccionado
                    ExtraerDatosLanz(fila);
                    
                    ColorTextBox();
                    DataGridViewPANEL.Visible = false;
                    DatosSeleccionadoBOX.Visible = true;
                }
            }
        }
        private void dgvDespaletizador_SelectionChanged(object sender, EventArgs e)
        {
            ClickEvent = false;
        }


        /// <summary>
        /// Función que extrae unos datos impuestos de una fila determinda y escribe en su correspondiente TextBox.
        /// </summary>
        /// <param name="fila">Número entero que indica a la función de que fila tiene que extraer los datos</param>
        public void ExtraerDatosLanz(int fila)
        {
            LanzamientoLinea datos_lanzamiento = new LanzamientoLinea();
            //DataGridView permite seleccionar una fila, una celda o todas las celdas
            Int32 selectedRowCount = dgvDespaletizador.Rows.GetRowCount(DataGridViewElementStates.Selected);


            //Si se ha seleccionado una fila entera mostramos el dato que haya en la columna CodMaterial que siempre será la primera, es decir, la número 0
            if (selectedRowCount > 0 && fila < dgvDespaletizador.RowCount-1)
            {
                datos_lanzamiento.codProducto = dgvDespaletizador.Rows[fila].Cells[2].Value.ToString();
                datos_lanzamiento.orden = dgvDespaletizador.Rows[fila].Cells[3].Value.ToString();
                datos_lanzamiento.cliente = dgvDespaletizador.Rows[fila].Cells[4].Value.ToString();
                datos_lanzamiento.producto = dgvDespaletizador.Rows[fila].Cells[5].Value.ToString();
                datos_lanzamiento.caja = dgvDespaletizador.Rows[fila].Cells[6].Value.ToString();
                datos_lanzamiento.formato = dgvDespaletizador.Rows[fila].Cells[7].Value.ToString();
                datos_lanzamiento.pa = dgvDespaletizador.Rows[fila].Cells[8].Value.ToString();
                datos_lanzamiento.referencia = dgvDespaletizador.Rows[fila].Cells[9].Value.ToString();
                datos_lanzamiento.gdo = dgvDespaletizador.Rows[fila].Cells[10].Value.ToString();
                datos_lanzamiento.tipo = dgvDespaletizador.Rows[fila].Cells[11].Value.ToString();
                datos_lanzamiento.comentarios = dgvDespaletizador.Rows[fila].Cells[12].Value.ToString();
                datos_lanzamiento.liquido = dgvDespaletizador.Rows[fila].Cells[13].Value.ToString();
                datos_lanzamiento.observacionesLaboratorio = dgvDespaletizador.Rows[fila].Cells[14].Value.ToString();
                datos_lanzamiento.materiales = dgvDespaletizador.Rows[fila].Cells[15].Value.ToString();
                datos_lanzamiento.estado = dgvDespaletizador.Rows[fila].Cells[16].Value.ToString();
                datos_lanzamiento.observacionesProduccion = dgvDespaletizador.Rows[fila].Cells[18].Value.ToString();
            }
            CodProductoSelecTB.Text = datos_lanzamiento.codProducto;
            OrdenSelecTB.Text = datos_lanzamiento.orden;
            ClienteSelecTB.Text = datos_lanzamiento.cliente;
            ProductoSelecTB.Text = datos_lanzamiento.producto;
            CajasSelecTB.Text = datos_lanzamiento.caja;
            FormatoSelecTB.Text = datos_lanzamiento.formato;
            PASelecTB.Text = datos_lanzamiento.pa;
            ReferenciaSelecTB.Text = datos_lanzamiento.referencia;
            GradSelecTB.Text = datos_lanzamiento.gdo;
            TipoSelecTB.Text = datos_lanzamiento.tipo;
            ComentariosSelecTB.Text = datos_lanzamiento.comentarios;
            LiquidoSelecTB.Text = datos_lanzamiento.liquido;
            ObservLabSelecTB.Text = datos_lanzamiento.observacionesLaboratorio;
            MaterialesSelecTB.Text = datos_lanzamiento.materiales;
            EstadoSelecTB.Text = datos_lanzamiento.estado;
            ObservProdSelecTB.Text = datos_lanzamiento.observacionesProduccion;
            Utilidades.MostrarImagen(datos_lanzamiento.codProducto, Imagen);
        }

        /// <summary>
        /// Función que extrae los datos del producto que se esta procesando y los escribe en su correspondiente TextBox.
        /// </summary>
        /// <param name="fila">Número entero que indica a la función de que fila tiene que extraer los datos</param>
        public void ExtraerDatosProduccion(int fila)
        {
            LanzamientoLinea datos_lanzamiento = new LanzamientoLinea();
            //DataGridView permite seleccionar una fila, una celda o todas las celdas
            Int32 selectedRowCount = fila;


            //Si se ha seleccionado una fila entera mostramos el dato que haya en la columna CodMaterial que siempre será la primera, es decir, la número 0
            if (selectedRowCount >= 0)
            {
                datos_lanzamiento.iDLanz = dgvDespaletizador.Rows[fila].Cells[1].Value.ToString();
                datos_lanzamiento.codProducto = dgvDespaletizador.Rows[fila].Cells[2].Value.ToString();
                datos_lanzamiento.orden = dgvDespaletizador.Rows[fila].Cells[3].Value.ToString();
                datos_lanzamiento.cliente = dgvDespaletizador.Rows[fila].Cells[4].Value.ToString();
                datos_lanzamiento.producto = dgvDespaletizador.Rows[fila].Cells[5].Value.ToString();
                datos_lanzamiento.caja = dgvDespaletizador.Rows[fila].Cells[6].Value.ToString();
                datos_lanzamiento.formato = dgvDespaletizador.Rows[fila].Cells[7].Value.ToString();
                datos_lanzamiento.pa = dgvDespaletizador.Rows[fila].Cells[8].Value.ToString();
                datos_lanzamiento.referencia = dgvDespaletizador.Rows[fila].Cells[9].Value.ToString();
                datos_lanzamiento.gdo = dgvDespaletizador.Rows[fila].Cells[10].Value.ToString();
                datos_lanzamiento.tipo = dgvDespaletizador.Rows[fila].Cells[11].Value.ToString();
                datos_lanzamiento.comentarios = dgvDespaletizador.Rows[fila].Cells[12].Value.ToString();
                datos_lanzamiento.liquido = dgvDespaletizador.Rows[fila].Cells[13].Value.ToString();
                datos_lanzamiento.observacionesLaboratorio = dgvDespaletizador.Rows[fila].Cells[14].Value.ToString();
                datos_lanzamiento.materiales = dgvDespaletizador.Rows[fila].Cells[15].Value.ToString();
                datos_lanzamiento.estado = dgvDespaletizador.Rows[fila].Cells[16].Value.ToString();
                datos_lanzamiento.observacionesProduccion = dgvDespaletizador.Rows[fila].Cells[18].Value.ToString();
            }
            CodProductoTB.Text = datos_lanzamiento.codProducto;
            OrdenTB.Text = datos_lanzamiento.orden;
            ClienteTB.Text = datos_lanzamiento.cliente;
            ProductoTB.Text = datos_lanzamiento.producto;
            //ExtraerDatosBOM(ReferenciaTB.Text, "BOT.");

            //Se calcula cuantas botellas es necesario producir
            double caja = caja = Convert.ToDouble(datos_lanzamiento.caja);
            string formato = datos_lanzamiento.formato;

            if (formato.Substring(2, 1) == "X")
            {
                botellascaja = Convert.ToDouble(formato.Substring(0, 2));
                formato = formato.Substring(3, 4);

            }
            if (formato.Substring(1, 1) == "X")
            {
                botellascaja = Convert.ToDouble(formato.Substring(0, 1));
                formato = formato.Substring(2, 4);
            }

            NumBotTotalTB.Text = Convert.ToString(caja * botellascaja);

            if (MaquinaLinea.numlin == 2)
            {
                Properties.Settings.Default.DPiDLanzDespL2 = datos_lanzamiento.iDLanz;
                Properties.Settings.Default.DPCodigoProdDespL2 = CodProductoTB.Text;
                Properties.Settings.Default.DPOrdenDespL2 = OrdenTB.Text;
                Properties.Settings.Default.DPProductoDespL2 = ProductoTB.Text;
                Properties.Settings.Default.DPClienteDespL2 = ClienteTB.Text;
                Properties.Settings.Default.DPNumBotTotalDespL2 = NumBotTotalTB.Text;
            }
            if (MaquinaLinea.numlin == 3)
            {
                Properties.Settings.Default.DPiDLanzDespL3 = datos_lanzamiento.iDLanz;

                Properties.Settings.Default.DPCodigoProdDespL3 = CodProductoTB.Text;
                Properties.Settings.Default.DPOrdenDespL3 = OrdenTB.Text;
                Properties.Settings.Default.DPProductoDespL3 = ProductoTB.Text;
                Properties.Settings.Default.DPClienteDespL3 = ClienteTB.Text;
                Properties.Settings.Default.DPNumBotTotalDespL3 = NumBotTotalTB.Text;

            }
            if (MaquinaLinea.numlin == 5)
            {
                Properties.Settings.Default.DPiDLanzDespL5 = datos_lanzamiento.iDLanz;

                Properties.Settings.Default.DPCodigoProdDespL5 = CodProductoTB.Text;
                Properties.Settings.Default.DPOrdenDespL5 = OrdenTB.Text;
                Properties.Settings.Default.DPProductoDespL5 = ProductoTB.Text;
                Properties.Settings.Default.DPClienteDespL5 = ClienteTB.Text;
                Properties.Settings.Default.DPNumBotTotalDespL5 = NumBotTotalTB.Text;
            }
            Properties.Settings.Default.Save();

            //Se marca de color la fila que se ha seleccionado
            if (OK_Fila)
            {
                dgvDespaletizador.Rows[fila].Cells["ORDEN"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgvDespaletizador.Rows[fila].Cells["FORM."].Style.BackColor = System.Drawing.Color.LightBlue;
                dgvDespaletizador.Rows[fila].Cells["CAJAS"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgvDespaletizador.Rows[fila].Cells["PRODUCTO"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgvDespaletizador.Rows[fila].Cells["CLIENTE"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgvDespaletizador.Rows[fila].Cells["CÓDIGO"].Style.BackColor = System.Drawing.Color.LightBlue;
            }
            if (fila >= 12)
            {
                dgvDespaletizador.FirstDisplayedScrollingRowIndex = fila - 6;
            }
        }

        /// <summary>
        /// Función que da color a unos text box determinados para indicar el estado del producto
        /// </summary>
        public void ColorTextBox()
        {
            switch (EstadoSelecTB.Text)
            {
                case "Completado":
                    EstadoSelecTB.BackColor = System.Drawing.Color.Green;
                    EstadoSelecTB.ForeColor = System.Drawing.Color.White;
                    break;
                case "Saltado":
                    EstadoSelecTB.BackColor = System.Drawing.Color.Yellow;
                    break;
                case "Iniciado":
                    EstadoSelecTB.BackColor = System.Drawing.Color.Orange;
                    break;
                case "Sin Terminar":
                    EstadoSelecTB.BackColor = System.Drawing.Color.Red;
                    break;
                case "":
                    EstadoSelecTB.BackColor = System.Drawing.Color.Gainsboro;
                    break;
            }
            switch (LiquidoSelecTB.Text)
            {
                case "OK":
                    LiquidoSelecTB.BackColor = System.Drawing.Color.Green;
                    LiquidoSelecTB.ForeColor = System.Drawing.Color.White;
                    break;
                case "ELABORACIÓN":
                    LiquidoSelecTB.BackColor = System.Drawing.Color.Yellow;
                    break;
                case "NOK":
                    LiquidoSelecTB.BackColor = System.Drawing.Color.Red;
                    break;
                case "":
                    LiquidoSelecTB.BackColor = System.Drawing.Color.Gainsboro;
                    break;
            }
            switch (MaterialesSelecTB.Text)
            {
                case "OK":
                    MaterialesSelecTB.BackColor = System.Drawing.Color.Green;
                    MaterialesSelecTB.ForeColor = System.Drawing.Color.White;
                    break;
                case "PENDIENTE":
                    MaterialesSelecTB.BackColor = System.Drawing.Color.Orange;
                    break;
                case "NOK":
                    MaterialesSelecTB.BackColor = System.Drawing.Color.Red;
                    break;
                case "":
                    MaterialesSelecTB.BackColor = System.Drawing.Color.Gainsboro;
                    break;
            }
        }

        /// <summary>
        /// Boton que muestra el form del BOM donde se puede consultar las especificaciones de algún producto.
        /// </summary>
        private void BoomB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.ReferenciaBOM = CodProductoSelecTB.Text;
            MaquinaLinea.VolverA = RetornoBOM.Desp;
            Utilidades.AbrirForm(parentInicio.GetBOM(), parentInicio, typeof(WHPST_BOM));
            Hide();
            Dispose();
        }

        /// <summary>
        /// Boton que oculta el BOX datosselecionados y muestra de nuevo el lanzamiento
        /// </summary>
        private void VolverB_Click(object sender, EventArgs e)
        {
            DatosSeleccionadoBOX.Hide();
            DataGridViewPANEL.Show();
        }

        private void SeleccionarProductoB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.ProductoSeleccionadoDespL2 != Properties.Settings.Default.DPiDLanzDespL2 && MaquinaLinea.numlin == 2)
            {
                MaquinaLinea.ProductoSeleccionadoDespL2 = Properties.Settings.Default.DPiDLanzDespL2;
                Properties.Settings.Default.BotellasAProducirDespL2 = "";
            }
            if (MaquinaLinea.ProductoSeleccionadoDespL3 != Properties.Settings.Default.DPiDLanzDespL3 && MaquinaLinea.numlin == 3)
            {
                MaquinaLinea.ProductoSeleccionadoDespL3 = Properties.Settings.Default.DPiDLanzDespL3;
                Properties.Settings.Default.BotellasAProducirDespL3 = "";

            }
            if (MaquinaLinea.ProductoSeleccionadoDespL5 != Properties.Settings.Default.DPiDLanzDespL5 && MaquinaLinea.numlin == 5)
            {
                MaquinaLinea.ProductoSeleccionadoDespL5 = Properties.Settings.Default.DPiDLanzDespL5;
                Properties.Settings.Default.BotellasAProducirDespL5 = "";

            }
            SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
            ExtraerDatosProduccion(fila);
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Función busca que fila estamos segun el idorden se haya indicado.
        /// </summary>
        public int BuscarFila(string idorden)
        {
            OK_Fila = false;
            for (int i = 0; (i < (dgvDespaletizador.RowCount - 1)) && OK_Fila == false; i++)
            {
                if (MaquinaLinea.numlin == 2) { if (dgvDespaletizador.Rows[i].Cells[1].Value.ToString() == Properties.Settings.Default.DPiDLanzDespL2) { fila = i; OK_Fila = true; } }
                if (MaquinaLinea.numlin == 3) { if (dgvDespaletizador.Rows[i].Cells[1].Value.ToString() == Properties.Settings.Default.DPiDLanzDespL3) { fila = i; OK_Fila = true; } }
                if (MaquinaLinea.numlin == 5) { if (dgvDespaletizador.Rows[i].Cells[1].Value.ToString() == Properties.Settings.Default.DPiDLanzDespL5) { fila = i; OK_Fila = true; } }
            }
            return fila;
        }
        private void MaquinistaTB_Click(object sender, EventArgs e)
        {
            ExcelUtiles.CrearTablaLanzamientos(dgvDespaletizador);
            if (Properties.Settings.Default.DPiDLanzDespL2 != "" && MaquinaLinea.numlin == 2) ExtraerDatosProduccion(BuscarFila(Properties.Settings.Default.DPiDLanzDespL2));
            if (Properties.Settings.Default.DPiDLanzDespL3 != "" && MaquinaLinea.numlin == 3) ExtraerDatosProduccion(BuscarFila(Properties.Settings.Default.DPiDLanzDespL3));
            if (Properties.Settings.Default.DPiDLanzDespL5 != "" && MaquinaLinea.numlin == 5) ExtraerDatosProduccion(BuscarFila(Properties.Settings.Default.DPiDLanzDespL5));

        }

        

        /// <summary>
        /// Función que detecta las celdas, marca las celdas que tienen que sobresalir y detecta que producto esta iniciado para rellenar los datos de producción.
        /// </summary>
        private void dgvInfoMovimientos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDespaletizador.Columns[e.ColumnIndex].Name == "LÍQUIDOS")
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
            if (dgvDespaletizador.Columns[e.ColumnIndex].Name == "MATERIALES")
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
            if (dgvDespaletizador.Columns[e.ColumnIndex].Name == "ESTADO")
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
                /*if (dgvDespaletizador.Columns[e.ColumnIndex].Name == "ESTADO")
                {
                    if (Convert.ToString(e.Value) == "Iniciado")
                    {
                        //Se muestra los datos del productos que se está tratando
                        if (MaquinaLinea.numlin == 2 && MaquinaLinea.ProductoSeleccionadoDespL2 == "") ExtraerDatosProduccion(e.RowIndex);
                        if (MaquinaLinea.numlin == 3 && MaquinaLinea.ProductoSeleccionadoDespL3 == "") ExtraerDatosProduccion(e.RowIndex);
                        if (MaquinaLinea.numlin == 5 && MaquinaLinea.ProductoSeleccionadoDespL5 == "") ExtraerDatosProduccion(e.RowIndex);
                    }
                }*/
            }
        }
        //#############################   FUNCIONES   ############################
        //public void ExtraerDatosBOM(string Referencia,string Busqueda)
        //{

        //    List<string[]> valoresAFiltrar = new List<string[]>();
        //    string[] filterval = new string[8];
        //    filterval[0] = "AND";
        //    filterval[1] = "CodProd";
        //    filterval[2] = "LIKE";
        //    filterval[3] = Referencia;
        //    valoresAFiltrar.Add(filterval);

        //    string[] filterval1= new string[4];
        //    filterval1[0] = "AND";
        //    filterval1[1] = "DescMaterial";
        //    filterval1[2] = "LIKE";
        //    filterval1[3] = "'%" + Busqueda + "%'";
        //    valoresAFiltrar.Add(filterval1);
        //    DataSet excelDataSet = new DataSet();
        //    string result;
        //    //List<string[]> valoresAFiltrar = dgvSelectFiltro.DataSource;
        //    excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileBOM, "FICHA", "CodMaterial ".Split(';'), valoresAFiltrar, out result);
        //    //tbSelectSalidaError.Text = result;
        //    //MessageBox.Show(result);
        //    if (excelDataSet.Tables[0].Rows.Count > 0)
        //    {
        //        RefBotellaTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["CodMaterial"]);
        //    }
        //}
        private void ActivarParadaGuardada()
        {
            char[] t1 = new char[6];
            for (int i = 0; i < 6; i++) { t1[i] = '1'; }
            if (MaquinaLinea.numlin == 2 && Properties.Settings.Default.Paro_Desp_L2)
            {
                inicio_paro = true;
                hora_ini_paro = Properties.Settings.Default.Hora_Paro_Desp_L2;
                t1[0] = Properties.Settings.Default.Hora_Paro_Desp_L2[0];
                t1[1] = Properties.Settings.Default.Hora_Paro_Desp_L2[1];
                t1[2] = Properties.Settings.Default.Hora_Paro_Desp_L2[3];
                t1[3] = Properties.Settings.Default.Hora_Paro_Desp_L2[4];
                t1[4] = Properties.Settings.Default.Hora_Paro_Desp_L2[6];
                t1[5] = Properties.Settings.Default.Hora_Paro_Desp_L2[7];

            }
            else if (MaquinaLinea.numlin == 3 && Properties.Settings.Default.Paro_Desp_L3)
            {
                inicio_paro = true;
                hora_ini_paro = Properties.Settings.Default.Hora_Paro_Desp_L3;
                t1[0] = Properties.Settings.Default.Hora_Paro_Desp_L3[0];
                t1[1] = Properties.Settings.Default.Hora_Paro_Desp_L3[1];
                t1[2] = Properties.Settings.Default.Hora_Paro_Desp_L3[3];
                t1[3] = Properties.Settings.Default.Hora_Paro_Desp_L3[4];
                t1[4] = Properties.Settings.Default.Hora_Paro_Desp_L3[6];
                t1[5] = Properties.Settings.Default.Hora_Paro_Desp_L3[7];

            }
            else if (MaquinaLinea.numlin == 5 && Properties.Settings.Default.Paro_Desp_L5)
            {
                inicio_paro = true;
                hora_ini_paro = Properties.Settings.Default.Hora_Paro_Desp_L5;
                t1[0] = Properties.Settings.Default.Hora_Paro_Desp_L5[0];
                t1[1] = Properties.Settings.Default.Hora_Paro_Desp_L5[1];
                t1[2] = Properties.Settings.Default.Hora_Paro_Desp_L5[3];
                t1[3] = Properties.Settings.Default.Hora_Paro_Desp_L5[4];
                t1[4] = Properties.Settings.Default.Hora_Paro_Desp_L5[6];
                t1[5] = Properties.Settings.Default.Hora_Paro_Desp_L5[7];
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
            statusboton_paro = inicio_paro ? true : false;
            FormParo = null;
        }

        public void SetComentarios(Despaletizador_Comentarios c)
        {
            FormComentarios = c;  
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            CambioTurnoB.BackColor = Utilidades.AvisoBoton(CambioTurnoB.BackColor);
            if (CambioTurnoB.BackColor != Color.White) CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoSalirAlarma;
            if (MaquinaLinea.numlin == 2 && !Properties.Settings.Default.chDesL2) { timer_cambio_turno.Enabled = false; CambioTurnoB.BackColor = Color.White; }
            if (MaquinaLinea.numlin == 3 && !Properties.Settings.Default.chDesL3) { timer_cambio_turno.Enabled = false; CambioTurnoB.BackColor = Color.White; }
            if (MaquinaLinea.numlin == 5 && !Properties.Settings.Default.chDesL5) { timer_cambio_turno.Enabled = false; CambioTurnoB.BackColor = Color.White; }
        }

        public Despaletizador_Comentarios GetComentarios()
        {
            return FormComentarios;
        }

        public WHPST_INICIO GetParentInicio()
        {
            return parentInicio;
        }

    }
}
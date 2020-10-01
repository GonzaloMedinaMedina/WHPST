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

namespace WHPS.Despaletizador
{
    public partial class MainDespaletizador : Form
    {
        //Variable que intercala la imagen del cambio de turno cuando la alarma se activa
        public bool statusboton = false;
        int fila, columna;
        bool ClickEvent = false;
        public MainDespaletizador()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        private void MainDespaletizador_Load(object sender, EventArgs e)
        {
            //Al venir de un form hijo, se quedará abierta la ventana anterior (form padre), para solucionar esto cerramos la ventana anterior si venimos del form SELECTMAQ (true).
            if (MaquinaLinea.SELECTMAQ == true)
            {
                Owner.Hide();
                MaquinaLinea.SELECTMAQ = false;
            }
            
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            MaquinistaTB.Text = MaquinaLinea.MDespaletizador;
            //Muestra la hora ya que el timer tarda 1s en tomar el control del Label lbReloj.
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Inicialmente se muestra el lanzamiento y se oculta el BOX del los datos del producto
            DataGridViewPANEL.Show();
            dgvDespaletizador.Show();
            DatosSeleccionadoBOX.Hide();

            //Recargamos los parámetro que vamos a utilizar
            MaquinaLinea.chalarma = Properties.Settings.Default.chalarma;
            MaquinaLinea.chalarmaDesL2 = Properties.Settings.Default.chalarmaDesL2;
            MaquinaLinea.chalarmaDesL3 = Properties.Settings.Default.chalarmaDesL3;
            MaquinaLinea.chalarmaDesL5 = Properties.Settings.Default.chalarmaDesL5;
            MaquinaLinea.alarmah1 = Properties.Settings.Default.alarmah1;
            MaquinaLinea.alarmam1 = Properties.Settings.Default.alarmam1;
            MaquinaLinea.alarmah2 = Properties.Settings.Default.alarmah2;
            MaquinaLinea.alarmam2 = Properties.Settings.Default.alarmam2;
            MaquinaLinea.alarmah3 = Properties.Settings.Default.alarmah3;
            MaquinaLinea.alarmam3 = Properties.Settings.Default.alarmam3;



            //Muestra la tabla de lanzaminento
            ExcelUtiles.CrearTablaLanzamientos(dgvDespaletizador);

            //Carga los parámetros que se han guardado en función de la línea e indica el estado del turno
            // chDesL (TRUE) y chalarmaDesL (FALSE) ---> Salimos del turno
            // chDesL (FALSE) y chalarmaDesL (FALSE) ---> Entramos en el turno
            // chalarmaDesL (TRUE) ---> El turno esta apunto de finalizar y deber checkear el cambio de turno
            /// <param name="NumBotL">Variable que indíca el número de botellas que han entrado en el despaletizador.</param>
            /// <param name="chDesL">Variable que indíca si se ha checkeado el estado del puesto de trabajo.</param>
            /// <param name="chalarmaDesL">Variable que indíca si queda poco tiempo para finalizar el turno.</param>
            if (MaquinaLinea.numlin == 2)
            {
                MaquinistaTB.BackColor = Color.IndianRed;
                if (Properties.Settings.Default.FilaSeleccionadaDespL2 != "") ExtraerDatosProduccion(Convert.ToInt32(Properties.Settings.Default.FilaSeleccionadaDespL2));
                NumBotTB.Text = Properties.Settings.Default.DPNumBotDespL2.ToString();

                if (MaquinaLinea.chDesL2 == false && MaquinaLinea.chalarmaDesL2 == false) CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoEntrar;
                if (MaquinaLinea.chDesL2 == true && MaquinaLinea.chalarmaDesL2 == false) CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoSalir;
                if (MaquinaLinea.chalarmaDesL2 == true)
                {
                    CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoSalirAlarmaRojo;
                    if (CambioTurnoB.BackgroundImage == Properties.Resources.CambioTurnoSalir) CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoSalirAlarmaRojo;
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                MaquinistaTB.BackColor = Color.Green;
                if (Properties.Settings.Default.FilaSeleccionadaDespL3 != "") ExtraerDatosProduccion(Convert.ToInt32(Properties.Settings.Default.FilaSeleccionadaDespL3));
                NumBotTB.Text = Properties.Settings.Default.DPNumBotDespL3.ToString();

                if (MaquinaLinea.chDesL3 == false && MaquinaLinea.chalarmaDesL3 == false) CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoEntrar;
                if (MaquinaLinea.chDesL3 == true && MaquinaLinea.chalarmaDesL3 == false) CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoSalir;
                if (MaquinaLinea.chalarmaDesL3 == true)
                {
                    CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoSalirAlarmaRojo;
                    if (CambioTurnoB.BackgroundImage == Properties.Resources.CambioTurnoSalir)CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoSalirAlarmaRojo;
                }

            }
            if (MaquinaLinea.numlin == 5)
            {
                MaquinistaTB.BackColor = Color.LightSkyBlue;
                if (Properties.Settings.Default.FilaSeleccionadaDespL5 != "") ExtraerDatosProduccion(Convert.ToInt32(Properties.Settings.Default.FilaSeleccionadaDespL5));
                NumBotTB.Text = Properties.Settings.Default.DPNumBotDespL5.ToString();

                if (MaquinaLinea.chDesL5 == false && MaquinaLinea.chalarmaDesL5 == false) CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoEntrar;
                if (MaquinaLinea.chDesL5 == true && MaquinaLinea.chalarmaDesL5 == false) CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalir;
                if (MaquinaLinea.chalarmaDesL5 == true)
                {
                    CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoSalirAlarmaRojo;
                    if (CambioTurnoB.BackgroundImage == Properties.Resources.CambioTurnoSalir)CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoSalirAlarmaRojo;
                }

            }

            //Puesto que la tabla tarda en cargar se han ocultado previamente algunos campos que se muestran a continuación
            DatosProduccionBOX.Visible = true;
        }

        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>
        /// <param name="BackL+numlin">Parámetro que identifica a cual form hijo de WHPST_INICIO debe volver en función del número de línea.</param>
        private void ExitB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin==2)
            {
                MaquinaLinea.BackL2 = true;
                WHPST_INICIO Form = new WHPST_INICIO();
                Hide();
                Form.Show();
                GC.Collect();
            }
            if (MaquinaLinea.numlin == 3)
            {
                MaquinaLinea.BackL3 = true;
                WHPST_INICIO Form = new WHPST_INICIO();
                Hide();
                Form.Show();
                GC.Collect();
            }
            if (MaquinaLinea.numlin == 5)
            {
                MaquinaLinea.BackL5 = true;
                WHPST_INICIO Form = new WHPST_INICIO();
                Hide();
                Form.Show();
                GC.Collect();
            }
        }
        
        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized;}

        private void CalculadoraB_Click(object sender, EventArgs e)
        {
            //calculadora1.Location = new Point(450, 250);
            if (calculadora1.Visible == true) { calculadora1.Visible = false; CalculadoraB.BackColor = Color.FromArgb(27, 33, 41); }
            else { calculadora1.Visible = true; CalculadoraB.BackColor = Color.DarkSeaGreen; }
        }

        /// <summary>
        /// Función del temporizador que se ejecuta cada segundo, indíca la hora y si se activa la alarma. 
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Activa la alarma cuando la hora marcada es la misma que ya que se muestra en pantalla e inicia la variable statusboton
            if ((lbReloj.Text == (MaquinaLinea.alarmah1 + ":" + MaquinaLinea.alarmam1 + ":" + "00") || lbReloj.Text == (MaquinaLinea.alarmah2 + ":" + MaquinaLinea.alarmam2 + ":" + "00") || lbReloj.Text == (MaquinaLinea.alarmah3 + ":" + MaquinaLinea.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
                CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalirAlarmaRojo;
                statusboton = true;
            }

            //Cuando la alarma esta activada, el cambio de turno parpadeará cada segundo en la línea correspondiente
            if (MaquinaLinea.numlin == 2 && MaquinaLinea.chalarmaDesL2 == true)
            {
                if (statusboton)
                {
                    statusboton = false;
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalirAlarmaRojo;
                }
                else
                {
                    statusboton = true;
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalirAlarmaAmarillo;
                }
            }
            if (MaquinaLinea.numlin == 3 && MaquinaLinea.chalarmaDesL3 == true)
            {
                if (statusboton)
                {
                    statusboton = false;
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalirAlarmaRojo;
                }
                else
                {
                    statusboton = true;
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalirAlarmaAmarillo;
                }
            }
            if (MaquinaLinea.numlin == 5 && MaquinaLinea.chalarmaDesL5 == true)
            {
                if (statusboton)
                {
                    statusboton = false;
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalirAlarmaRojo;
                }
                else
                {
                    statusboton = true;
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalirAlarmaAmarillo;
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
            Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
            Hide();
            Form.Show();            GC.Collect();
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
                    Despaletizador_Botellas Form1 = new Despaletizador_Botellas();
                    Hide();
                    Form1.Show();
                }
                else
                {
                    Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
                    Hide();
                    Form.Show();            GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chDesL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Despaletizador_Botellas Form = new Despaletizador_Botellas();
                    Hide();
                    Form.Show();            GC.Collect();
                }
                else
                {
                    Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
                    Hide();
                    Form.Show();            GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chDesL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Despaletizador_Botellas Form = new Despaletizador_Botellas();
                    Hide();
                    Form.Show();            GC.Collect();
                }
                else
                {
                    Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
                    Hide();
                    Form.Show();            GC.Collect();
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
                    Despaletizador_Cierres Form = new Despaletizador_Cierres();
                    Hide();
                    Form.Show();            GC.Collect();
                }
                else
                {
                    Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
                    Hide();
                    Form.Show();            GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chDesL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Despaletizador_Cierres Form = new Despaletizador_Cierres();
                    Hide();
                    Form.Show();            GC.Collect();
                }
                else
                {
                    Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
                    Hide();
                    Form.Show();            GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chDesL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Despaletizador_Cierres Form = new Despaletizador_Cierres();
                    Hide();
                    Form.Show();            GC.Collect();
                }
                else
                {
                    Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
                    Hide();
                    Form.Show();            GC.Collect();
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
                    Despaletizador_RotBotellas Form = new Despaletizador_RotBotellas();
                    Hide();
                    Form.Show();            GC.Collect();
                }
                else
                {
                    Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
                    Hide();
                    Form.Show();            GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chDesL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Despaletizador_RotBotellas Form = new Despaletizador_RotBotellas();
                    Hide();
                    Form.Show();            GC.Collect();
                }
                else
                {
                    Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
                    Hide();
                    Form.Show();            GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chDesL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Despaletizador_RotBotellas Form = new Despaletizador_RotBotellas();
                    Hide();
                    Form.Show();            GC.Collect();
                }
                else
                {
                    Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
                    Hide();
                    Form.Show();            GC.Collect();
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
                    Despaletizador_Comentarios Form = new Despaletizador_Comentarios();
                    Hide();
                    Form.Show();            GC.Collect();

                }
                else
                {
                    Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
                    Hide();
                    Form.Show();            GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chDesL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Despaletizador_Comentarios Form = new Despaletizador_Comentarios();
                    Hide();
                    Form.Show();            GC.Collect();
                }
                else
                {
                    Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
                    Hide();
                    Form.Show();            GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chDesL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Despaletizador_Comentarios Form = new Despaletizador_Comentarios();
                    Hide();
                    Form.Show();            GC.Collect();
                }
                else
                {
                    Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
                    Hide();
                    Form.Show();            GC.Collect();
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
                    Despaletizador_Registro_Paro Form1 = new Despaletizador_Registro_Paro();
                    Hide();
                    Form1.Show();
                }
                else
                {
                    Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
                    Hide();
                    Form.Show();            GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chDesL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Despaletizador_Registro_Paro Form = new Despaletizador_Registro_Paro();
                    Hide();
                    Form.Show();            GC.Collect();
                }
                else
                {
                    Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
                    Hide();
                    Form.Show();            GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chDesL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Despaletizador_Registro_Paro Form = new Despaletizador_Registro_Paro();
                    Hide();
                    Form.Show();            GC.Collect();
                }
                else
                {
                    Despaletizador_CambioTurno Form = new Despaletizador_CambioTurno();
                    Hide();
                    Form.Show();            GC.Collect();
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
            int caja = Convert.ToInt16(datos_lanzamiento.caja);
            string formato = datos_lanzamiento.formato;
            if (formato.Substring(2, 1) == "x") { formato = formato.Substring(0, 1); }
            else { formato = formato.Substring(0, 2); }

            NumBotTotalTB.Text = Convert.ToString(caja* Convert.ToInt32(formato));

            if (MaquinaLinea.numlin == 2)
            {
                Properties.Settings.Default.DPCodigoProdDespL2 = CodProductoTB.Text;
                Properties.Settings.Default.DPOrdenDespL2 = OrdenTB.Text;
                Properties.Settings.Default.DPProductoDespL2 = ProductoTB.Text;
                Properties.Settings.Default.DPClienteDespL2 = ClienteTB.Text;
                Properties.Settings.Default.DPNumBotTotalDespL2 = NumBotTotalTB.Text;
            }
            if (MaquinaLinea.numlin == 3)
            {
                Properties.Settings.Default.DPCodigoProdDespL3 = CodProductoTB.Text;
                Properties.Settings.Default.DPOrdenDespL3 = OrdenTB.Text;
                Properties.Settings.Default.DPProductoDespL3 = ProductoTB.Text;
                Properties.Settings.Default.DPClienteDespL3 = ClienteTB.Text;
                Properties.Settings.Default.DPNumBotTotalDespL3 = NumBotTotalTB.Text;
            }
            if (MaquinaLinea.numlin == 5)
            {
                Properties.Settings.Default.DPCodigoProdDespL5 = CodProductoTB.Text;
                Properties.Settings.Default.DPOrdenDespL5 = OrdenTB.Text;
                Properties.Settings.Default.DPProductoDespL5 = ProductoTB.Text;
                Properties.Settings.Default.DPClienteDespL5 = ClienteTB.Text;
                Properties.Settings.Default.DPNumBotTotalDespL5 = NumBotTotalTB.Text;
            }
            Properties.Settings.Default.Save();

            //Se marca de color la fila que se ha seleccionado
            if (OrdenTB.Text != "")
            {
                dgvDespaletizador.Rows[fila].Cells["ORDEN"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgvDespaletizador.Rows[fila].Cells["FORMATO"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgvDespaletizador.Rows[fila].Cells["CAJAS"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgvDespaletizador.Rows[fila].Cells["PRODUCTO"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgvDespaletizador.Rows[fila].Cells["CLIENTE"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgvDespaletizador.Rows[fila].Cells["REFERENCIA"].Style.BackColor = System.Drawing.Color.LightBlue;
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
            MaquinaLinea.RetornoBOM = "Despaletizador";
            WHPST_BOM Form = new WHPST_BOM();
            Hide();
            Form.Show();            GC.Collect();
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
            if (MaquinaLinea.numlin == 2)
                if (MaquinaLinea.ProductoSeleccionadoDespL2 == "" || MaquinaLinea.ProductoSeleccionadoDespL2 != OrdenSelecTB.Text || CodProductoSelecTB.Text != CodProductoTB.Text || ProductoSelecTB.Text != ProductoTB.Text)
                {
                    Properties.Settings.Default.BotellasAProducirDespL2 = "";
                    MaquinaLinea.ProductoSeleccionadoDespL2 = OrdenSelecTB.Text;
                    SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
                    Properties.Settings.Default.FilaSeleccionadaDespL2 = Convert.ToString(fila);
                    ExtraerDatosProduccion(fila);
                }
                else
                {
                    CodProductoTB.Text = "";
                    OrdenTB.Text = "";
                    ClienteTB.Text = "";
                    ProductoTB.Text = "";
                    NumBotTotalTB.Text = "0";
                    SeleccionarProductoB.BackColor = Color.FromArgb(27, 33, 41);
                    MaquinaLinea.ProductoSeleccionadoDespL2 = "";
                }
            if (MaquinaLinea.numlin == 3)
                if (MaquinaLinea.ProductoSeleccionadoDespL3 == "" || MaquinaLinea.ProductoSeleccionadoDespL3 != OrdenSelecTB.Text || CodProductoSelecTB.Text != CodProductoTB.Text || ProductoSelecTB.Text != ProductoTB.Text)
                {
                    Properties.Settings.Default.BotellasAProducirDespL3 = "";
                    MaquinaLinea.ProductoSeleccionadoDespL3 = OrdenSelecTB.Text;
                    SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
                    Properties.Settings.Default.FilaSeleccionadaDespL3 = Convert.ToString(fila);
                    ExtraerDatosProduccion(fila);
                }
                else
                {
                    CodProductoTB.Text = "";
                    OrdenTB.Text = "";
                    ClienteTB.Text = "";
                    ProductoTB.Text = "";
                    NumBotTotalTB.Text = "0";
                    SeleccionarProductoB.BackColor = Color.FromArgb(27, 33, 41);
                    MaquinaLinea.ProductoSeleccionadoDespL3 = "";
                }
            if (MaquinaLinea.numlin == 5)
                if (MaquinaLinea.ProductoSeleccionadoDespL5 == "" || MaquinaLinea.ProductoSeleccionadoDespL5 != OrdenSelecTB.Text || CodProductoSelecTB.Text != CodProductoTB.Text || ProductoSelecTB.Text != ProductoTB.Text)
                {
                    Properties.Settings.Default.BotellasAProducirDespL5 = "";
                    MaquinaLinea.ProductoSeleccionadoDespL5 = OrdenSelecTB.Text;
                    SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
                    Properties.Settings.Default.FilaSeleccionadaDespL5 = Convert.ToString(fila);
                    ExtraerDatosProduccion(fila);
                }
                else
                {
                    CodProductoTB.Text = "";
                    OrdenTB.Text = "";
                    ClienteTB.Text = "";
                    ProductoTB.Text = "";
                    NumBotTotalTB.Text = "0";
                    SeleccionarProductoB.BackColor = Color.FromArgb(27, 33, 41);
                    MaquinaLinea.ProductoSeleccionadoDespL5 = "";
                }
            Properties.Settings.Default.Save();
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
                if (dgvDespaletizador.Columns[e.ColumnIndex].Name == "ESTADO")
                {
                    if (Convert.ToString(e.Value) == "Iniciado")
                    {
                        //Se muestra los datos del productos que se está tratando
                        if (MaquinaLinea.numlin == 2 && MaquinaLinea.ProductoSeleccionadoDespL2 == "") ExtraerDatosProduccion(e.RowIndex);
                        if (MaquinaLinea.numlin == 3 && MaquinaLinea.ProductoSeleccionadoDespL3 == "") ExtraerDatosProduccion(e.RowIndex);
                        if (MaquinaLinea.numlin == 5 && MaquinaLinea.ProductoSeleccionadoDespL5 == "") ExtraerDatosProduccion(e.RowIndex);
                    }
                }
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
    }
}
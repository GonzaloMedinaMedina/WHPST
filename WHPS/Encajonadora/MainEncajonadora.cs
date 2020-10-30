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
        public bool statusboton = false;
        public bool statusboton_paro = false;
        public bool inicio_paro = false;
        public int[] temp = new int[3];
        public string hora_ini_paro = "";
 
        int columna, fila;
        bool ClickEvent = false;
        public MainEncajonadora()
        {
            InitializeComponent();
            ActivarParadaGuardada();

        }
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


        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>
        /// <param name="BackL+numlin">Parámetro que identifica a cual form hijo de WHPST_INICIO debe volver en función del número de línea.</param>
        private void ExitB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                MaquinaLinea.BackL2 = true;
                WHPST_INICIO Form = new WHPST_INICIO();
                Hide();
                Form.Show();
            }
            if (MaquinaLinea.numlin == 3)
            {
                MaquinaLinea.BackL3 = true;
                WHPST_INICIO Form = new WHPST_INICIO();
                Hide();
                Form.Show();
            }
            if (MaquinaLinea.numlin == 5)
            {
                MaquinaLinea.BackL5 = true;
                WHPST_INICIO Form = new WHPST_INICIO();
                Hide();
                Form.Show();
            }
        }

        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }



        //Abre el form de ajustes
        private void AjustesB_Click(object sender, EventArgs e)
        {
            Encajonadora_Ajustes Form = new Encajonadora_Ajustes();
            Hide();
            Form.Show();
        }

        /*Carga la directamente el responsable de la instalación y reconoce 
          si se ha comprobado el estado la instalación en el cambio de turno.*/
        private void MainEncajonadora_Load(object sender, EventArgs e)
        {

            //Para que el form selecmaq no se quede abierto, lo cerramos si venimos de el, es decir, si la variable es true.
            if (MaquinaLinea.SELECTMAQ == true)
            {
                Owner.Hide();
                MaquinaLinea.SELECTMAQ = false;
            }


            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;
            MaquinistaTB.Text = MaquinaLinea.MEncajonadora;
            //Puesto que el timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            MaquinaLinea.chalarma = Properties.Settings.Default.chalarma;
            MaquinaLinea.chalarmaEncL2 = Properties.Settings.Default.chalarmaEncL2;
            MaquinaLinea.chalarmaEncL3 = Properties.Settings.Default.chalarmaEncL3;
            MaquinaLinea.chalarmaEncL5 = Properties.Settings.Default.chalarmaEncL5;
            MaquinaLinea.alarmah1 = Properties.Settings.Default.alarmah1;
            MaquinaLinea.alarmam1 = Properties.Settings.Default.alarmam1;
            MaquinaLinea.alarmah2 = Properties.Settings.Default.alarmah2;
            MaquinaLinea.alarmam2 = Properties.Settings.Default.alarmam2;
            MaquinaLinea.alarmah3 = Properties.Settings.Default.alarmah3;
            MaquinaLinea.alarmam3 = Properties.Settings.Default.alarmam3;

            if (MaquinaLinea.numlin == 2)
            {
                MaquinistaTB.BackColor = Color.IndianRed;
                //SI se ha chequeado el despaletizador y la alarma no esta activada.
                if (MaquinaLinea.chEncL2 == true && MaquinaLinea.chalarmaEncL2 == false)
                {
                    CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoSalir;
                }
                //NO se ha chequeado el despaletizador y la alarma no esta activada.
                if (MaquinaLinea.chEncL2 == false && MaquinaLinea.chalarmaEncL2 == false)
                {
                    CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoEntrar;
                }

                //Cuando la alarma se activa, aparace un mensaje de alarma. Para que solo aparezaca una vez lo mostramos cuando 
                if (MaquinaLinea.chalarmaEncL2 == true)
                {
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalirAlarmaRojo;
                    if (CambioTurnoB.BackgroundImage == WHPS.Properties.Resources.CambioTurnoSalir)
                    {
                        CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalirAlarmaRojo;
                        MessageBox.Show("El turno esta a punto de finalizar, realice y registre la inspección de la máquina.");
                    }
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                MaquinistaTB.BackColor = Color.Green;
                if (MaquinaLinea.chEncL3 == true && MaquinaLinea.chalarmaEncL3 == false)
                {
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalir;
                }
                if (MaquinaLinea.chEncL3 == false && MaquinaLinea.chalarmaEncL3 == false)
                {
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoEntrar;
                }
                if (MaquinaLinea.chalarmaEncL3 == true)
                {
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalirAlarmaRojo;
                    if (CambioTurnoB.BackgroundImage == WHPS.Properties.Resources.CambioTurnoSalir)
                    {
                        CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalirAlarmaRojo;
                        MessageBox.Show("El turno esta a punto de finalizar, realice y registre la inspección de la máquina.");
                    }
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                MaquinistaTB.BackColor = Color.LightSkyBlue;
                if (MaquinaLinea.chEncL5 == true && MaquinaLinea.chalarmaEncL5 == false)
                {
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalir;
                }
                if (MaquinaLinea.chEncL5 == false && MaquinaLinea.chalarmaEncL5 == false)
                {
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoEntrar;
                }
                if (MaquinaLinea.chalarmaEncL5 == true)
                {
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalirAlarmaRojo;
                    if (CambioTurnoB.BackgroundImage == WHPS.Properties.Resources.CambioTurnoSalir)
                    {
                        CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalirAlarmaRojo;
                        MessageBox.Show("El turno esta a punto de finalizar, realice y registre la inspección de la máquina.");
                    }
                }
            }
            //Muestra la tabla de lanzaminento
            ExcelUtiles.CrearTablaLanzamientos(dgvEncajonadora);

            //Estado del boton de paro y de producción
            if (MaquinaLinea.numlin == 2)
            {
                if (Properties.Settings.Default.DPiDLanzEncL2 != "") ExtraerDatosProduccion(BuscarFila(Properties.Settings.Default.DPiDLanzEncL2), dgvEncajonadora);
                //Producción
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL2;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEncL2;
                LoteTB.Text = Properties.Settings.Default.DPLoteEncL2;
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (Properties.Settings.Default.DPiDLanzEncL3 != "") ExtraerDatosProduccion(BuscarFila(Properties.Settings.Default.DPiDLanzEncL3), dgvEncajonadora);
                //Producción
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL3;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEncL3;
                LoteTB.Text = Properties.Settings.Default.DPLoteEncL3;
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (Properties.Settings.Default.DPiDLanzEncL5 != "") ExtraerDatosProduccion(BuscarFila(Properties.Settings.Default.DPiDLanzEncL5), dgvEncajonadora);
                //Producción
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL5;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEncL5;
                LoteTB.Text = Properties.Settings.Default.DPLoteEncL5;
            }
            //Puesto que la tabla tarda en cargar se han ocultado previamente algunos campos que se muestran a continuación
            DatosProduccionBOX.Visible = true;
        }


        internal void AdvertenciaParo(bool paro)
        {
            inicio_paro = paro;
            statusboton_paro = inicio_paro ? true : false;

        }


        //Temporizador que controla la hora y y el parpade de aviso de finalización de turno
        private void timer1_Tick(object sender, EventArgs e)
        {
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
            //Cada segundo carga la hora en pantalla
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Para activar la alarma debe, estar desactivada, haberse chequeado el inicio de turno y que la hora cuadre con la alarma 
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }
            //Cuando la alarma esta activada, el cambio de turno parpadeará cada segundo
            if (MaquinaLinea.numlin == 2 && MaquinaLinea.chalarmaEncL2 == true)
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
            if (MaquinaLinea.numlin == 3 && MaquinaLinea.chalarmaEncL3 == true)
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
            if (MaquinaLinea.numlin == 5 && MaquinaLinea.chalarmaEncL5 == true)
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

        //Cuando pulsa el boton, registra el tiempo en la variable correspondiente
        private void ParoB_Click(object sender, EventArgs e)
        {

            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chEncL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                        Properties.Settings.Default.ParoDesdeEncL2 = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
                        Encajonadora_Registro_Paro Form = new Encajonadora_Registro_Paro(inicio_paro, hora_ini_paro, temp);
                        Hide();
                        Form.Show();

                }
                else
                {
                    Encajonadora_CambioTurno Form = new Encajonadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chEncL3 == true || MaquinaLinea.usuario == "Administracion")
                {              
                        Properties.Settings.Default.ParoDesdeEncL3 = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
                        Encajonadora_Registro_Paro Form = new Encajonadora_Registro_Paro(inicio_paro, hora_ini_paro, temp);
                        Hide();
                        Form.Show();
                       // Form.PDesdeTB.Text = (inicio_paro == true) ? hora_ini_paro : DateTime.Now.ToString("HH:mm:ss");
                   
                }
                else
                {
                    Encajonadora_CambioTurno Form = new Encajonadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chEncL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                  
                        Properties.Settings.Default.ParoDesdeEncL5 = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
                        Encajonadora_Registro_Paro Form = new Encajonadora_Registro_Paro(inicio_paro, hora_ini_paro, temp);
                        Hide();
                        Form.Show();
                        //Form.PDesdeTB.Text = (inicio_paro == true) ? hora_ini_paro : DateTime.Now.ToString("HH:mm:ss");

                   
                }
                else
                {
                    Encajonadora_CambioTurno Form = new Encajonadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
        }

        //Abre el form del cambio de turno
        private void CambioTurnoB_Click(object sender, EventArgs e)
        {
            Encajonadora_CambioTurno Form = new Encajonadora_CambioTurno();
            Hide();
            Form.Show();
        }

        //Abre el form de rotura de botellas
        private void RotasB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.RotCodProd = CodProductoTB.Text;
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chEncL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Encajonadora_RotBotellas Form = new Encajonadora_RotBotellas();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Encajonadora_CambioTurno Form = new Encajonadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chEncL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Encajonadora_RotBotellas Form = new Encajonadora_RotBotellas();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Encajonadora_CambioTurno Form = new Encajonadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chEncL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Encajonadora_RotBotellas Form = new Encajonadora_RotBotellas();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Encajonadora_CambioTurno Form = new Encajonadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }

        }

        //Abre el form de comentarios
        private void ComentB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chEncL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Encajonadora_Comentarios Form = new Encajonadora_Comentarios();
                    Hide();
                    Form.Show();

                }
                else
                {
                    Encajonadora_CambioTurno Form = new Encajonadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chEncL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Encajonadora_Comentarios Form = new Encajonadora_Comentarios();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Encajonadora_CambioTurno Form = new Encajonadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chEncL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Encajonadora_Comentarios Form = new Encajonadora_Comentarios();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Encajonadora_CambioTurno Form = new Encajonadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
        }

        private void RegistroB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chEncL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    if (OrdenTB.Text == "") MessageBox.Show("Debes primero realizar la selección del producto. Para ello, pulsa el boton que aparece haciendo doble click en el mismo.");
                    else
                    {
                        Properties.Settings.Default.DPOrdenEncL2 = OrdenTB.Text;
                        Properties.Settings.Default.DPFormatoEncL2 = FormatoTB.Text;
                        Properties.Settings.Default.DPProductoEncL2 = ProductoTB.Text;
                        Properties.Settings.Default.DPClienteEncL2 = ClienteTB.Text;
                        Properties.Settings.Default.Save();
                        Encajonadora_Registro_Produccion Form = new Encajonadora_Registro_Produccion();
                        Hide();
                        Form.Show();
                        GC.Collect();
                    }
                }
                else
                {
                    Encajonadora_CambioTurno Form = new Encajonadora_CambioTurno();
                    Hide();
                    Form.Show();
                    GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chEncL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    if (OrdenTB.Text == "") MessageBox.Show("Debes primero realizar la selección del producto. Para ello, pulsa el boton que aparece haciendo doble click en el mismo.");
                    else
                    {
                        Properties.Settings.Default.DPOrdenEncL3 = OrdenTB.Text;
                        Properties.Settings.Default.DPFormatoEncL3 = FormatoTB.Text;
                        Properties.Settings.Default.DPProductoEncL3 = ProductoTB.Text;
                        Properties.Settings.Default.DPClienteEncL3 = ClienteTB.Text;
                        Properties.Settings.Default.Save();
                        Encajonadora_Registro_Produccion Form = new Encajonadora_Registro_Produccion();
                        Hide();
                        Form.Show();
                        GC.Collect();
                    }
                }
                else
                {
                    Encajonadora_CambioTurno Form = new Encajonadora_CambioTurno();
                    Hide();
                    Form.Show();
                    GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chEncL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    if (OrdenTB.Text == "") MessageBox.Show("Debes primero realizar la selección del producto. Para ello, pulsa el boton que aparece haciendo doble click en el mismo.");
                    else
                    {
                        Properties.Settings.Default.DPOrdenEncL5 = OrdenTB.Text;
                        Properties.Settings.Default.DPFormatoEncL5 = FormatoTB.Text;
                        Properties.Settings.Default.DPProductoEncL5 = ProductoTB.Text;
                        Properties.Settings.Default.DPClienteEncL5 = ClienteTB.Text;
                        Properties.Settings.Default.Save();
                        Encajonadora_Registro_Produccion Form = new Encajonadora_Registro_Produccion();
                        Hide();
                        Form.Show();
                        GC.Collect();
                    }
                }
                else
                {
                    Encajonadora_CambioTurno Form = new Encajonadora_CambioTurno();
                    Hide();
                    Form.Show();
                    GC.Collect();
                }
            }

        }

        //######################   APARTADO DE LANZAMIENTO   #####################
        /// <summary>
        /// Acción que muestra las especificaciones del producto y los datos de su producción .
        /// </summary>
        private void dgvEncajonadora_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            fila = e.RowIndex;
            columna = e.ColumnIndex;
            if (ClickEvent == false)
            {
                ClickEvent = true;
            }
            else
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
                if (fila >= 0 && columna != 3)
                {
                    //Extraer los datos de esa filia y los coloca en el BOX datosseleccionado
                    ExtraerDatosLanz(fila, dgvEncajonadora);
                    ColorTextBox();
                    DataGridViewPANEL.Visible = false;
                    DatosSeleccionadoBOX.Visible = true;

                    if (MaquinaLinea.numlin == 2)
                        if (Convert.ToString(fila) != Properties.Settings.Default.FilaSeleccionadaEncL2) SeleccionarProductoB.BackColor = Color.FromArgb(27, 33, 41);
                        else SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
                    if (MaquinaLinea.numlin == 3)
                        if (Convert.ToString(fila) != Properties.Settings.Default.FilaSeleccionadaEncL3) SeleccionarProductoB.BackColor = Color.FromArgb(27, 33, 41);
                        else SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
                    if (MaquinaLinea.numlin == 5)
                        if (Convert.ToString(fila) != Properties.Settings.Default.FilaSeleccionadaEncL5) SeleccionarProductoB.BackColor = Color.FromArgb(27, 33, 41);
                        else SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
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
        public void ExtraerDatosLanz(int fila, DataGridView gdv)
        {
            LanzamientoLinea datos_lanzamiento = new LanzamientoLinea();
            //DataGridView permite seleccionar una fila, una celda o todas las celdas
            Int32 selectedRowCount = gdv.Rows.GetRowCount(DataGridViewElementStates.Selected);


            //Si se ha seleccionado una fila entera mostramos el dato que haya en la columna CodMaterial que siempre será la primera, es decir, la número 0
            if (selectedRowCount > 0)
            {
                datos_lanzamiento.codProducto = gdv.Rows[fila].Cells[2].Value.ToString();
                datos_lanzamiento.orden = gdv.Rows[fila].Cells[3].Value.ToString();
                datos_lanzamiento.cliente = gdv.Rows[fila].Cells[4].Value.ToString();
                datos_lanzamiento.producto = gdv.Rows[fila].Cells[5].Value.ToString();
                datos_lanzamiento.caja = gdv.Rows[fila].Cells[6].Value.ToString();
                datos_lanzamiento.formato = gdv.Rows[fila].Cells[7].Value.ToString();
                datos_lanzamiento.pa = gdv.Rows[fila].Cells[8].Value.ToString();
                datos_lanzamiento.referencia = gdv.Rows[fila].Cells[9].Value.ToString();
                datos_lanzamiento.gdo = gdv.Rows[fila].Cells[10].Value.ToString();
                datos_lanzamiento.tipo = gdv.Rows[fila].Cells[11].Value.ToString();
                datos_lanzamiento.comentarios = gdv.Rows[fila].Cells[12].Value.ToString();
                datos_lanzamiento.liquido = gdv.Rows[fila].Cells[13].Value.ToString();
                datos_lanzamiento.observacionesLaboratorio = gdv.Rows[fila].Cells[14].Value.ToString();
                datos_lanzamiento.materiales = gdv.Rows[fila].Cells[15].Value.ToString();
                datos_lanzamiento.estado = gdv.Rows[fila].Cells[16].Value.ToString();
                datos_lanzamiento.observacionesProduccion = gdv.Rows[fila].Cells[18].Value.ToString();
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
        /// <param name="dgv">DataGridView donde se opera</param>
        public void ExtraerDatosProduccion(int fila, DataGridView dgv)
        {
            LanzamientoLinea datos_lanzamiento = new LanzamientoLinea();
            //DataGridView permite seleccionar una fila, una celda o todas las celdas
            Int32 selectedRowCount = fila;

            //Si se ha seleccionado una fila entera mostramos el dato que haya en la columna CodMaterial que siempre será la primera, es decir, la número 0
            if (selectedRowCount >= 0 && fila < dgv.RowCount - 1)
            {
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
            }
            CodProductoTB.Text = datos_lanzamiento.codProducto;
            OrdenTB.Text = datos_lanzamiento.orden;
            ClienteTB.Text = datos_lanzamiento.cliente;
            ProductoTB.Text = datos_lanzamiento.producto;
            FormatoTB.Text = datos_lanzamiento.formato;


            if (MaquinaLinea.numlin == 2)
            {
                if (Properties.Settings.Default.CajasAProducirEncL2 == "") Properties.Settings.Default.CajasAProducirEncL2 = datos_lanzamiento.caja;
                NCajasTB.Text = Properties.Settings.Default.CajasAProducirEncL2;
                Properties.Settings.Default.DPiDLanzEncL2 = datos_lanzamiento.iDLanz;
                Properties.Settings.Default.FilaSeleccionadaEncL2 = Convert.ToString(BuscarFila(datos_lanzamiento.iDOrd));
                Properties.Settings.Default.DPCodigoProdEncL2 = CodProductoTB.Text;
                Properties.Settings.Default.DPOrdenEncL2 = OrdenTB.Text;
                Properties.Settings.Default.DPProductoEncL2 = ProductoTB.Text;
                Properties.Settings.Default.DPClienteEncL2 = ClienteTB.Text;
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (Properties.Settings.Default.CajasAProducirEncL3 == "") Properties.Settings.Default.CajasAProducirEncL3 = datos_lanzamiento.caja;
                NCajasTB.Text = Properties.Settings.Default.CajasAProducirEncL3;
                Properties.Settings.Default.DPiDLanzEncL3 = datos_lanzamiento.iDLanz;
                Properties.Settings.Default.DPCodigoProdEncL3 = CodProductoTB.Text;
                Properties.Settings.Default.DPOrdenEncL3 = OrdenTB.Text;
                Properties.Settings.Default.DPProductoEncL3 = ProductoTB.Text;
                Properties.Settings.Default.DPClienteEncL3 = ClienteTB.Text;
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (Properties.Settings.Default.CajasAProducirEncL5 == "") Properties.Settings.Default.CajasAProducirEncL5 = datos_lanzamiento.caja;
                NCajasTB.Text = Properties.Settings.Default.CajasAProducirEncL5;
                Properties.Settings.Default.DPiDLanzEncL5 = datos_lanzamiento.iDLanz;
                Properties.Settings.Default.DPCodigoProdEncL5 = CodProductoTB.Text;
                Properties.Settings.Default.DPOrdenEncL5 = OrdenTB.Text;
                Properties.Settings.Default.DPProductoEncL5 = ProductoTB.Text;
                Properties.Settings.Default.DPClienteEncL5 = ClienteTB.Text;
            }
            Properties.Settings.Default.Save();
            //Se marca de color la fila que se ha seleccionado
            if (OrdenTB.Text != "")
            {
                dgv.Rows[fila].Cells["ORDEN"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgv.Rows[fila].Cells["FORMATO"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgv.Rows[fila].Cells["CAJAS"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgv.Rows[fila].Cells["PRODUCTO"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgv.Rows[fila].Cells["CLIENTE"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgv.Rows[fila].Cells["REFERENCIA"].Style.BackColor = System.Drawing.Color.LightBlue;
            }

            if (fila >= 12)
            {
                dgv.FirstDisplayedScrollingRowIndex = fila - 6;
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
                case "Sin terminar":
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
        /// Función busca que fila estamos segun el idorden se haya indicado.
        /// </summary>
        public int BuscarFila(string idorden)
        {
            bool OK = false;
            for (int i = 0; (i < (dgvEncajonadora.RowCount - 1)) && OK == false; i++)
            {
                if (MaquinaLinea.numlin == 2) { if (dgvEncajonadora.Rows[i].Cells[1].Value.ToString() == Properties.Settings.Default.DPiDLanzEncL2){fila = i; OK = true; }}
                if (MaquinaLinea.numlin == 3) { if (dgvEncajonadora.Rows[i].Cells[1].Value.ToString() == Properties.Settings.Default.DPiDLanzEncL3) { fila = i; OK = true; }}
                if (MaquinaLinea.numlin == 5) { if (dgvEncajonadora.Rows[i].Cells[1].Value.ToString() == Properties.Settings.Default.DPiDLanzEncL5) { fila = i; OK = true; }}
            }
            return fila;
        }
        /// <summary>
        /// Boton que muestra el form del BOM donde se puede consultar las especificaciones de algún producto.
        /// </summary>
        private void BOMB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.ReferenciaBOM = CodProductoSelecTB.Text;
            MaquinaLinea.RetornoBOM = "Encajonadora";
            WHPST_BOM Form = new WHPST_BOM();
            Hide();
            Form.Show();
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
            if (MaquinaLinea.ProductoSeleccionadoEncL2 != Properties.Settings.Default.DPiDLanzEncL2 && MaquinaLinea.numlin == 2)
            {
                MaquinaLinea.ProductoSeleccionadoEncL2 = Properties.Settings.Default.DPiDLanzEncL2;
                Properties.Settings.Default.CajasAProducirEncL2 = "";
            }
            if (MaquinaLinea.ProductoSeleccionadoEncL3 != Properties.Settings.Default.DPiDLanzEncL3 && MaquinaLinea.numlin == 3)
            {
                MaquinaLinea.ProductoSeleccionadoEncL3 = Properties.Settings.Default.DPiDLanzEncL3;
                Properties.Settings.Default.CajasAProducirEncL3 = "";

            }
            if (MaquinaLinea.ProductoSeleccionadoEncL5 != Properties.Settings.Default.DPiDLanzEncL5 && MaquinaLinea.numlin == 5)
            {
                MaquinaLinea.ProductoSeleccionadoEncL5 = Properties.Settings.Default.DPiDLanzEncL5;
                Properties.Settings.Default.CajasAProducirEncL5 = "";

            }
            SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
            ExtraerDatosProduccion(fila, dgvEncajonadora);
            Properties.Settings.Default.Save();
        }
        //Incrementa o decrementan el contador
        private void OK_ConteoB_Click(object sender, EventArgs e)
        {
            if (NCajasTB.Text != "")
            {
                if (MaquinaLinea.numlin == 2)
                {

                    Properties.Settings.Default.CajasAProducirEncL2 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.CajasAProducirEncL2) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NCajasTB.Text = Properties.Settings.Default.CajasAProducirEncL2;
                }
                if (MaquinaLinea.numlin == 3)
                {

                    Properties.Settings.Default.CajasAProducirEncL3 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.CajasAProducirEncL3) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NCajasTB.Text = Properties.Settings.Default.CajasAProducirEncL3;
                }
                if (MaquinaLinea.numlin == 5)
                {

                    Properties.Settings.Default.CajasAProducirEncL5 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.CajasAProducirEncL5) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NCajasTB.Text = Properties.Settings.Default.CajasAProducirEncL5;
                }
                Properties.Settings.Default.Save();
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

        private void CalculadoraB_Click(object sender, EventArgs e)
        {
            if (calculadora1.Visible == true) { calculadora1.Visible = false; CalculadoraB.BackColor = Color.FromArgb(27, 33, 41); }
            else { calculadora1.Visible = true; CalculadoraB.BackColor = Color.DarkSeaGreen; }
        }

        
        private void SiguienteB_Click(object sender, EventArgs e)
            {
                 // MaquinaLinea.CapacidadLlen = CapacidadTB.Text;
                  //MaquinaLinea.GraduacionLLen = GraduacionTB.Text;
                if (MaquinaLinea.numlin == 2)
                {
                    if (MaquinaLinea.chEncL2 == true || MaquinaLinea.usuario == "Administracion")
                    {
                        Encajonadora_Parte Form = new Encajonadora_Parte();
                        Hide();
                        Form.Show(); GC.Collect();
                    }
                    else
                    {
                        Encajonadora_Parte Form = new Encajonadora_Parte();
                        Hide();
                        Form.Show(); GC.Collect();
                    }
                }
                if (MaquinaLinea.numlin == 3)
                {
                    if (MaquinaLinea.chEncL2 == true || MaquinaLinea.usuario == "Administracion")
                    {
                        Encajonadora_Parte Form = new Encajonadora_Parte();
                        Hide();
                        Form.Show(); GC.Collect();
                    }
                    else
                    {
                        Encajonadora_Parte Form = new Encajonadora_Parte();
                        Hide();
                        Form.Show(); GC.Collect();
                    }
                }
                if (MaquinaLinea.numlin == 5)
                {
                    if (MaquinaLinea.chEncL2 == true || MaquinaLinea.usuario == "Administracion")
                    {
                        Encajonadora_Parte Form = new Encajonadora_Parte();
                        Hide();
                        Form.Show();
                        GC.Collect();
                    }
                    else
                    {
                        Encajonadora_Parte Form = new Encajonadora_Parte();
                        Hide();
                        Form.Show();
                        GC.Collect();
                    }
                }
            }

        private void MaquinistaTB_Click(object sender, EventArgs e)
        {
            ExcelUtiles.CrearTablaLanzamientos(dgvEncajonadora);
            if (Properties.Settings.Default.DPiDLanzEncL2 != "" && MaquinaLinea.numlin == 2) ExtraerDatosProduccion(BuscarFila(Properties.Settings.Default.DPiDLanzEncL2), dgvEncajonadora);
            if (Properties.Settings.Default.DPiDLanzEncL3 != "" && MaquinaLinea.numlin == 3) ExtraerDatosProduccion(BuscarFila(Properties.Settings.Default.DPiDLanzEncL3), dgvEncajonadora);
            if (Properties.Settings.Default.DPiDLanzEncL5 != "" && MaquinaLinea.numlin == 5) ExtraerDatosProduccion(BuscarFila(Properties.Settings.Default.DPiDLanzEncL5), dgvEncajonadora);
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


        //########################################################################
    }
}
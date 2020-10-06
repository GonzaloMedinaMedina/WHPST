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
//PROBANDO GIT
namespace WHPS.Etiquetadora
{
    public partial class MainEtiquetadora : Form
    {//
        //Variable que realiza el cambio de imagen cuando el boton de cambio de turno esta parpadeando y como consecuencia la alarma esta activada
        public bool statusboton = false;
        public string hora_ini_paro = "";
        public bool inicio_paro = false;
        public bool statusboton_paro = false;
        public int[] temporizador = new int[6];

        int columna, fila;
        double caja, botellascaja;
        bool ClickEvent = false;
        public MainEtiquetadora()
        {
            InitializeComponent();
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
            if (calculadora1.Visible == true) { calculadora1.Visible = false; CalculadoraB.BackColor = Color.FromArgb(27, 33, 41); }
            else { calculadora1.Visible = true; CalculadoraB.BackColor = Color.DarkSeaGreen; }
        }


        /*Carga la directamente el responsable de la instalación y reconoce 
          si se ha comprobado el estado la instalación en el cambio de turno.*/
        private void MainEtiquetadora_Load(object sender, EventArgs e)
        {
            //Para que el form selecmaq no se quede abierto, lo cerramos si venimos de el, es decir, si la variable es true.
            if (MaquinaLinea.SELECTMAQ == true)
            {
                Owner.Hide();
                MaquinaLinea.SELECTMAQ = false;
            }

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;
            MaquinistaTB.Text = MaquinaLinea.MEtiquetadora;
            //Puesto que el timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            MaquinaLinea.chalarma = Properties.Settings.Default.chalarma;
            MaquinaLinea.chalarmaEtiqL2 = Properties.Settings.Default.chalarmaEtiqL2;
            MaquinaLinea.chalarmaEtiqL3 = Properties.Settings.Default.chalarmaEtiqL3;
            MaquinaLinea.chalarmaEtiqL5 = Properties.Settings.Default.chalarmaEtiqL5;
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
                if (MaquinaLinea.chEtiqL2 == true && MaquinaLinea.chalarmaEtiqL2 == false)
                {
                    CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoSalir;
                }
                //NO se ha chequeado el despaletizador y la alarma no esta activada.
                if (MaquinaLinea.chEtiqL2 == false && MaquinaLinea.chalarmaEtiqL2 == false)
                {
                    CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoEntrar;
                }

                //Cuando la alarma se activa, aparace un mensaje de alarma. Para que solo aparezaca una vez lo mostramos cuando 
                if (MaquinaLinea.chalarmaEtiqL2 == true)
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
                if (MaquinaLinea.chEtiqL3 == true && MaquinaLinea.chalarmaEtiqL3 == false)
                {
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalir;
                }
                if (MaquinaLinea.chEtiqL3 == false && MaquinaLinea.chalarmaEtiqL3 == false)
                {
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoEntrar;
                }
                if (MaquinaLinea.chalarmaEtiqL3 == true)
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
                if (MaquinaLinea.chEtiqL5 == true && MaquinaLinea.chalarmaEtiqL5 == false)
                {
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalir;
                }
                if (MaquinaLinea.chEtiqL5 == false && MaquinaLinea.chalarmaEtiqL5 == false)
                {
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoEntrar;
                }
                if (MaquinaLinea.chalarmaEtiqL5 == true)
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
            ExcelUtiles.CrearTablaLanzamientos(dgvEtiquetadora);

            //Estado del boton de paro y de producción
            if (MaquinaLinea.numlin == 2)
            {
                if (Properties.Settings.Default.DPiDLanzEtiqL2 != "") ExtraerDatosProduccion(BuscarFila(Properties.Settings.Default.DPiDLanzEtiqL2), dgvEtiquetadora);
                //Producción
                LoteTB.Text = Properties.Settings.Default.DPLoteEtiqL2;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEtiqL2;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEtiqL2;
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (Properties.Settings.Default.DPiDLanzEtiqL3 != "") ExtraerDatosProduccion(BuscarFila(Properties.Settings.Default.DPiDLanzEtiqL3), dgvEtiquetadora);
                //Producción
                LoteTB.Text = Properties.Settings.Default.DPLoteEtiqL3;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEtiqL3;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEtiqL3;


            }
            if (MaquinaLinea.numlin == 5)
            {
                if (Properties.Settings.Default.DPiDLanzEtiqL5 != "")ExtraerDatosProduccion(BuscarFila(Properties.Settings.Default.DPiDLanzEtiqL5), dgvEtiquetadora);
                //Producción
                LoteTB.Text = Properties.Settings.Default.DPLoteEtiqL5;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEtiqL5;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEtiqL5;
            }


            //Puesto que la tabla tarda en cargar se han ocultado previamente algunos campos que se muestran a continuación
            DatosProduccionBOX.Visible = true;
        }

        internal void AdvertenciaParo(bool paro, string horaparo, int[] temp)
        {
            inicio_paro = paro;

            if (inicio_paro)
            {
                hora_ini_paro = (hora_ini_paro == "") ? horaparo : "";
                statusboton_paro = paro;
                for(int i = 0; i<6; i++)
                {
                    this.temporizador[i] = temp[i];
                }
            }
        }

        //Temporizador que controla la hora y y el parpade de aviso de finalización de turno
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (inicio_paro)
            {
                temporizador[4] += 1;
                if (temporizador[4] > 9)
                {
                    temporizador[4] = 0;
                    temporizador[5] += 1;
                }
                if (temporizador[4] == 0 && temporizador[5] > 5)
                {
                    temporizador[5] = 0;
                    temporizador[2] += 1;
                }
                if (temporizador[2] > 9)
                {
                    temporizador[2] = 0;
                    temporizador[3] += 1;
                }
                if (temporizador[2] == 0 && temporizador[3] > 5)
                {
                    temporizador[3] = 0;
                    temporizador[0] += 1;
                }
                if (temporizador[0] > 4)
                {
                    temporizador[0] = 0;
                    temporizador[1] += 1;
                }
                if (temporizador[0] == 0 && temporizador[1] > 2)
                {
                    temporizador[1] = 0;
                }
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
            lbReloj.Text = DateTime.Now.ToString("HH:30:ss");
            //Alarma para el control cada 30min
            if (lbReloj.Text[4].ToString() == "0" && (lbReloj.Text[3].ToString() == "3" || lbReloj.Text[3].ToString() == "0")) { Apps_Etiquetadora.alarma30min = true;}
            else { Apps_Etiquetadora.alarma30min = false; }

            if(Apps_Etiquetadora.alarma30min && !Apps_Etiquetadora.controlsaved) { 
               if (Control30mB.BackColor != Color.Red)
                {
                    Control30mB.BackColor = Color.Red;
                    Control30mB.Update();
                }
                else
                {
                    Control30mB.BackColor = Color.Yellow;
                    Control30mB.Update();
                }

            }



            //Para activar la alarma debe, estar desactivada, haberse chequeado el inicio de turno y que la hora cuadre con la alarma 
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }
            //Cuando la alarma esta activada, el cambio de turno parpadeará cada segundo
            if (MaquinaLinea.numlin == 2 && MaquinaLinea.chalarmaEtiqL2 == true)
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
            if (MaquinaLinea.numlin == 3 && MaquinaLinea.chalarmaEtiqL3 == true)
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
            if (MaquinaLinea.numlin == 5 && MaquinaLinea.chalarmaEtiqL5 == true)
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
                if (MaquinaLinea.chEtiqL2 == true || MaquinaLinea.usuario == "Administracion")
                {

                        Etiquetadora_Registro_Paro Form = new Etiquetadora_Registro_Paro(inicio_paro, hora_ini_paro, temporizador);
                        Hide();
                        Form.Show();

                    
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chEtiqL3 == true || MaquinaLinea.usuario == "Administracion")
                {
 
                    {
                        Etiquetadora_Registro_Paro Form = new Etiquetadora_Registro_Paro(inicio_paro, hora_ini_paro, temporizador);
                        Hide();
                        Form.Show();

                    }
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chEtiqL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    
                        Etiquetadora_Registro_Paro Form = new Etiquetadora_Registro_Paro(inicio_paro, hora_ini_paro, temporizador);
                        Hide();
                        Form.Show();

                    
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
        }

        //Abre el form del cambio de turno
        private void CambioTurnoB_Click(object sender, EventArgs e)
        {
            Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
            Hide();
            Form.Show();
        }

        //Abre el form de rotura de botellas
        private void RotasB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.RotCodProd = CodProductoTB.Text;
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chEtiqL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Etiquetadora_RotBotellas Form = new Etiquetadora_RotBotellas();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chEtiqL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Etiquetadora_RotBotellas Form = new Etiquetadora_RotBotellas();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chEtiqL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Etiquetadora_RotBotellas Form = new Etiquetadora_RotBotellas();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
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
                if (MaquinaLinea.chEtiqL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Etiquetadora_Comentarios Form = new Etiquetadora_Comentarios();
                    Hide();
                    Form.Show();

                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chEtiqL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Etiquetadora_Comentarios Form = new Etiquetadora_Comentarios();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chEtiqL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Etiquetadora_Comentarios Form = new Etiquetadora_Comentarios();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
        }

        private void RegistroB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chEtiqL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    if (OrdenTB.Text == "") MessageBox.Show("Debes primero realizar la selección del producto. Para ello, pulsa el boton que aparece haciendo doble click en el mismo.");
                    else
                    {
                        Properties.Settings.Default.DPLoteEtiqL2 = LoteTB.Text;
                        Properties.Settings.Default.DPOrdenEtiqL2 = OrdenTB.Text;
                        Properties.Settings.Default.DPProductoDespL2 = ProductoTB.Text;
                        Properties.Settings.Default.DPClienteEtiqL2 = ClienteTB.Text;
                        Properties.Settings.Default.DPFormatoEtiqL2 = FormatoTB.Text;
                        Properties.Settings.Default.DPGraduacionEtiqL2 = GraduacionTB.Text;
                        Properties.Settings.Default.Save();
                        Etiquetadora_Registro_Produccion Form = new Etiquetadora_Registro_Produccion();
                        Hide();
                        Form.Show(); GC.Collect();
                    }
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show(); GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {

                if (MaquinaLinea.chEtiqL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    if (OrdenTB.Text == "") MessageBox.Show("Debes primero realizar la selección del producto. Para ello, pulsa el boton que aparece haciendo doble click en el mismo.");
                    else
                    {
                        Properties.Settings.Default.DPLoteEtiqL3 = LoteTB.Text;
                        Properties.Settings.Default.DPOrdenEtiqL3 = OrdenTB.Text;
                        Properties.Settings.Default.DPProductoDespL3 = ProductoTB.Text;
                        Properties.Settings.Default.DPClienteEtiqL3 = ClienteTB.Text;
                        Properties.Settings.Default.DPFormatoEtiqL3 = FormatoTB.Text;
                        Properties.Settings.Default.DPGraduacionEtiqL3 = GraduacionTB.Text;
                        Properties.Settings.Default.Save();
                        Etiquetadora_Registro_Produccion Form = new Etiquetadora_Registro_Produccion();
                        Hide();
                        Form.Show(); GC.Collect();
                    }
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show(); GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chEtiqL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    if (OrdenTB.Text == "") MessageBox.Show("Debes primero realizar la selección del producto. Para ello, pulsa el boton que aparece haciendo doble click en el mismo.");
                    else
                    {
                        Properties.Settings.Default.DPLoteEtiqL5 = LoteTB.Text;
                        Properties.Settings.Default.DPOrdenEtiqL5 = OrdenTB.Text;
                        Properties.Settings.Default.DPProductoDespL5 = ProductoTB.Text;
                        Properties.Settings.Default.DPClienteEtiqL5 = ClienteTB.Text;
                        Properties.Settings.Default.DPFormatoEtiqL5 = FormatoTB.Text;
                        Properties.Settings.Default.DPGraduacionEtiqL5 = GraduacionTB.Text;
                        Properties.Settings.Default.Save();
                        Etiquetadora_Registro_Produccion Form = new Etiquetadora_Registro_Produccion();
                        Hide();
                        Form.Show(); GC.Collect();
                    }
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show(); GC.Collect();
                }
            }
        }

        private void Control30mB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chEtiqL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Etiquetadora_Control30m Form = new Etiquetadora_Control30m();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chEtiqL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Etiquetadora_Control30m Form = new Etiquetadora_Control30m();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chEtiqL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Etiquetadora_Control30m Form = new Etiquetadora_Control30m();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
        }

        private void VisionArtificialB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chEtiqL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Etiquetadora_VisionArtificial Form = new Etiquetadora_VisionArtificial();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chEtiqL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Etiquetadora_VisionArtificial Form = new Etiquetadora_VisionArtificial();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chEtiqL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Etiquetadora_VisionArtificial Form = new Etiquetadora_VisionArtificial();
                    Hide();
                    Form.Show();
                }
                else
                {
                    Etiquetadora_CambioTurno Form = new Etiquetadora_CambioTurno();
                    Hide();
                    Form.Show();
                }
            }
        }

        //######################   APARTADO DE LANZAMIENTO   #####################
        /// <summary>
        /// Acción que muestra las especificaciones del producto y los datos de su producción .
        /// </summary>
        private void dgvEtiquetadora_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            fila = e.RowIndex;
            columna = e.ColumnIndex;
            if (ClickEvent == false)
            {
                ClickEvent = true;
            }
            else
            {
                if (columna == 3 && dgvEtiquetadora.Rows[fila].Cells[3].Value.ToString() != "")
                {
                    try
                    {
                        //Se muestra la orden
                        string NombrePDF = dgvEtiquetadora.Rows[fila].Cells[3].Value.ToString();
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
                    ExtraerDatosLanz(fila, dgvEtiquetadora);
                    ColorTextBox();
                    DataGridViewPANEL.Visible = false;
                    DatosSeleccionadoBOX.Visible = true;

                    if (MaquinaLinea.numlin == 2)
                        if (Convert.ToString(fila) != Properties.Settings.Default.FilaSeleccionadaEtiqL2) SeleccionarProductoB.BackColor = Color.FromArgb(27, 33, 41);
                        else SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
                    if (MaquinaLinea.numlin == 3)
                        if (Convert.ToString(fila) != Properties.Settings.Default.FilaSeleccionadaEtiqL3) SeleccionarProductoB.BackColor = Color.FromArgb(27, 33, 41);
                        else SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
                    if (MaquinaLinea.numlin == 5)
                        if (Convert.ToString(fila) != Properties.Settings.Default.FilaSeleccionadaEtiqL5) SeleccionarProductoB.BackColor = Color.FromArgb(27, 33, 41);
                        else SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
                }
            }
        }
        private void dgvEtiquetadora_SelectionChanged(object sender, EventArgs e)
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
            GraduacionTB.Text = datos_lanzamiento.gdo;
            FormatoTB.Text = datos_lanzamiento.formato;

            //Se calcula la cantidad de botellas que requiere el producto para ser completado

            string formato = datos_lanzamiento.formato;

            if (datos_lanzamiento.caja != "") { caja = Convert.ToDouble(datos_lanzamiento.caja); }
            if (formato.Substring(2, 1) == "X") botellascaja = Convert.ToDouble(formato.Substring(0, 2));
            if (formato.Substring(1, 1) == "X") botellascaja = Convert.ToDouble(formato.Substring(0, 1));

            if (MaquinaLinea.numlin == 2)
            {
                if (Properties.Settings.Default.BotellasAProducirEtiqL2 == "") Properties.Settings.Default.BotellasAProducirEtiqL2 = Convert.ToString(caja * botellascaja);
                NBotTB.Text = Properties.Settings.Default.BotellasAProducirEtiqL2;
                Properties.Settings.Default.DPiDLanzEtiqL2 = datos_lanzamiento.iDLanz;
                Properties.Settings.Default.DPCodigoProdEtiqL2 = CodProductoTB.Text;
                Properties.Settings.Default.DPOrdenEtiqL2 = OrdenTB.Text;
                Properties.Settings.Default.DPProductoEtiqL2 = ProductoTB.Text;
                Properties.Settings.Default.DPClienteEtiqL2 = ClienteTB.Text;
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (Properties.Settings.Default.BotellasAProducirEtiqL3 == "") Properties.Settings.Default.BotellasAProducirEtiqL3 = Convert.ToString(caja * botellascaja);
                NBotTB.Text = Properties.Settings.Default.BotellasAProducirEtiqL3;
                Properties.Settings.Default.DPiDLanzEtiqL3 = datos_lanzamiento.iDLanz;
                Properties.Settings.Default.DPCodigoProdEtiqL3 = CodProductoTB.Text;
                Properties.Settings.Default.DPOrdenEtiqL3 = OrdenTB.Text;
                Properties.Settings.Default.DPProductoEtiqL3 = ProductoTB.Text;
                Properties.Settings.Default.DPClienteEtiqL3 = ClienteTB.Text;
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (Properties.Settings.Default.BotellasAProducirEtiqL5 == "") Properties.Settings.Default.BotellasAProducirEtiqL5 = Convert.ToString(caja * botellascaja);
                NBotTB.Text = Properties.Settings.Default.BotellasAProducirEtiqL5;
                Properties.Settings.Default.DPiDLanzEtiqL5 = datos_lanzamiento.iDLanz;
                Properties.Settings.Default.DPCodigoProdEtiqL5 = CodProductoTB.Text;
                Properties.Settings.Default.DPOrdenEtiqL5 = OrdenTB.Text;
                Properties.Settings.Default.DPProductoEtiqL5 = ProductoTB.Text;
                Properties.Settings.Default.DPClienteEtiqL5 = ClienteTB.Text;
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
        /// Boton que muestra el form del BOM donde se puede consultar las especificaciones de algún producto.
        /// </summary>
        private void BOMB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.ReferenciaBOM = CodProductoSelecTB.Text;
            MaquinaLinea.RetornoBOM = "Etiquetadora";
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
            if (MaquinaLinea.numlin == 2)
                if (MaquinaLinea.ProductoSeleccionadoEtiqL2 == "" || MaquinaLinea.ProductoSeleccionadoEtiqL2 != OrdenSelecTB.Text || CodProductoSelecTB.Text != CodProductoTB.Text || ProductoSelecTB.Text != ProductoTB.Text)
                {
                    Properties.Settings.Default.BotellasAProducirEtiqL2 = "";
                    MaquinaLinea.ProductoSeleccionadoEtiqL2 = OrdenSelecTB.Text;
                    SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
                    Properties.Settings.Default.FilaSeleccionadaEtiqL2 = Convert.ToString(fila);
                    ExtraerDatosProduccion(fila, dgvEtiquetadora);
                }
            if (MaquinaLinea.numlin == 3)
                if (MaquinaLinea.ProductoSeleccionadoEtiqL3 == "" || MaquinaLinea.ProductoSeleccionadoEtiqL3 != OrdenSelecTB.Text || CodProductoSelecTB.Text != CodProductoTB.Text || ProductoSelecTB.Text != ProductoTB.Text)
                {
                    Properties.Settings.Default.BotellasAProducirEtiqL3 = "";
                    MaquinaLinea.ProductoSeleccionadoEtiqL3 = OrdenSelecTB.Text;
                    SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
                    Properties.Settings.Default.FilaSeleccionadaEtiqL3 = Convert.ToString(fila);
                    ExtraerDatosProduccion(fila, dgvEtiquetadora);
                }
            if (MaquinaLinea.numlin == 5)
                if (MaquinaLinea.ProductoSeleccionadoEtiqL5 == "" || MaquinaLinea.ProductoSeleccionadoEtiqL5 != OrdenSelecTB.Text || CodProductoSelecTB.Text != CodProductoTB.Text || ProductoSelecTB.Text != ProductoTB.Text)
                {
                    Properties.Settings.Default.BotellasAProducirEtiqL5 = "";
                    MaquinaLinea.ProductoSeleccionadoEtiqL5 = OrdenSelecTB.Text;
                    SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
                    Properties.Settings.Default.FilaSeleccionadaEtiqL5 = Convert.ToString(fila);
                    ExtraerDatosProduccion(fila, dgvEtiquetadora);
                }
            Properties.Settings.Default.Save();
        }


        /// <summary>
        /// Función busca que fila estamos segun el idorden se haya indicado.
        /// </summary>
        /// <summary>
        /// Función busca que fila estamos segun el idorden se haya indicado.
        /// </summary>
        public int BuscarFila(string idorden)
        {
            bool OK = false;
            for (int i = 0; (i < (dgvEtiquetadora.RowCount - 1)) && OK == false; i++)
            {
                if (MaquinaLinea.numlin == 2) { if (dgvEtiquetadora.Rows[i].Cells[1].Value.ToString() == Properties.Settings.Default.DPiDLanzEtiqL2) { fila = i; OK = true; } }
                if (MaquinaLinea.numlin == 3) { if (dgvEtiquetadora.Rows[i].Cells[1].Value.ToString() == Properties.Settings.Default.DPiDLanzEtiqL3) { fila = i; OK = true; } }
                if (MaquinaLinea.numlin == 5)
                {
                    if (dgvEtiquetadora.Rows[i].Cells[1].Value.ToString() == Properties.Settings.Default.DPiDLanzEtiqL5) { fila = i; OK = true; }
                }
            }
            return fila;
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
       
        private void CalculdoraB_Click(object sender, EventArgs e)
        {
            if (calculadora1.Visible == true) { calculadora1.Visible = false; CalculadoraB.BackColor = Color.FromArgb(27, 33, 41); }
            else { calculadora1.Visible = true; CalculadoraB.BackColor = Color.DarkSeaGreen; }
        }

        private void SiguienteB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.FormatoEtiq = FormatoTB.Text;
            //    MaquinaLinea.GraduacionLLen = GraduacionTB.Text;
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chEtiqL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Etiquetadora_Parte Form = new Etiquetadora_Parte();
                    Hide();
                    Form.Show(); GC.Collect();
                }
                else
                {
                    Etiquetadora_Parte Form = new Etiquetadora_Parte();
                    Hide();
                    Form.Show(); GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chEtiqL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Etiquetadora_Parte Form = new Etiquetadora_Parte();
                    Hide();
                    Form.Show(); GC.Collect();
                }
                else
                {
                    Etiquetadora_Parte Form = new Etiquetadora_Parte();
                    Hide();
                    Form.Show(); GC.Collect();
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chEtiqL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Etiquetadora_Parte Form = new Etiquetadora_Parte();
                    Hide();
                    Form.Show();
                    GC.Collect();
                }
                else
                {
                    Etiquetadora_Parte Form = new Etiquetadora_Parte();
                    Hide();
                    Form.Show();
                    GC.Collect();
                }
            }

        }

        private void OK_ConteoB_Click(object sender, EventArgs e)
        {
            if (NBotTB.Text != "")
            {
                if (MaquinaLinea.numlin == 2)
                {

                    Properties.Settings.Default.BotellasAProducirEtiqL2 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.BotellasAProducirEtiqL2) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NBotTB.Text = Properties.Settings.Default.BotellasAProducirEtiqL2;
                }
                if (MaquinaLinea.numlin == 3)
                {

                    Properties.Settings.Default.BotellasAProducirEtiqL3 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.BotellasAProducirEtiqL3) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NBotTB.Text = Properties.Settings.Default.BotellasAProducirEtiqL3;
                }
                if (MaquinaLinea.numlin == 5)
                {

                    Properties.Settings.Default.BotellasAProducirEtiqL5 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.BotellasAProducirEtiqL5) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NBotTB.Text = Properties.Settings.Default.BotellasAProducirEtiqL5;
                }
                Properties.Settings.Default.Save();
                ConteoBotellasTB.Text = "0";
                OK_ConteoB.BackColor = Color.FromArgb(27, 33, 41);
            }
        }


        /// <summary>
        /// Función que detecta las celdas, marca las celdas que tienen que sobresalir y detecta que producto esta iniciado para reEtiqar los datos de producción.
        /// </summary>
        private void dgvEtiquetadora_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvEtiquetadora.Columns[e.ColumnIndex].Name == "LÍQUIDOS")
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
            if (dgvEtiquetadora.Columns[e.ColumnIndex].Name == "MATERIALES")
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
            if (dgvEtiquetadora.Columns[e.ColumnIndex].Name == "ESTADO")
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

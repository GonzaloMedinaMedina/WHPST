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
        //Variable que realiza el cambio de imagen cuando el boton de cambio de turno esta parpadeando y como consecuencia la alarma esta activada
        public bool statusboton = false;
        public bool statusboton_paro = false;
        public bool inicio_paro = false;
        public string hora_ini_paro = "";
        bool OK_Fila = false;
        public int[] temp = new int[3];
        int columna, fila;
        double caja, botellascaja;
        bool ClickEvent = false;

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

        private void ActivarParadaGuardada()
        {
            char[] t1 = new char[6];
            for(int i = 0; i<6; i++) { t1[i] = '1'; }
            if (MaquinaLinea.numlin == 2 && Properties.Settings.Default.Paro_Llen_L2) {
                inicio_paro = true;
                hora_ini_paro = Properties.Settings.Default.Hora_Paro_Llen_L2;
                t1[0] = Properties.Settings.Default.Hora_Paro_Llen_L2[0];
                t1[1] = Properties.Settings.Default.Hora_Paro_Llen_L2[1];
                t1[2] = Properties.Settings.Default.Hora_Paro_Llen_L2[3];
                t1[3] = Properties.Settings.Default.Hora_Paro_Llen_L2[4];
                t1[4] = Properties.Settings.Default.Hora_Paro_Llen_L2[6];
                t1[5] = Properties.Settings.Default.Hora_Paro_Llen_L2[7];

            }
            else if (MaquinaLinea.numlin == 3 && Properties.Settings.Default.Paro_Llen_L3) {
                inicio_paro = true;
                hora_ini_paro = Properties.Settings.Default.Hora_Paro_Llen_L3;
                t1[0] = Properties.Settings.Default.Hora_Paro_Llen_L3[0];
                t1[1] = Properties.Settings.Default.Hora_Paro_Llen_L3[1];
                t1[2] = Properties.Settings.Default.Hora_Paro_Llen_L3[3];
                t1[3] = Properties.Settings.Default.Hora_Paro_Llen_L3[4];
                t1[4] = Properties.Settings.Default.Hora_Paro_Llen_L3[6];
                t1[5] = Properties.Settings.Default.Hora_Paro_Llen_L3[7];

            }
            else if (MaquinaLinea.numlin == 5 && Properties.Settings.Default.Paro_Llen_L5) {
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
            //Si se está registrado con un usuario mostras un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;
            
            //Puesto que el timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));


            Utilidades.FuncionLoad(MaquinistaTB, MaquinaLinea.MLlenadora, MaquinaLinea.chLlenL2, MaquinaLinea.chLlenL3, MaquinaLinea.chLlenL5, CambioTurnoB);

            //Muestra la tabla de lanzaminento
            ExcelUtiles.CrearTablaLanzamientos(dgvLlenadora);

            //Estado del boton de paro y de producción
            if (MaquinaLinea.numlin == 2)
            {
                if (Properties.Settings.Default.DPiDLanzLlenL2 != "") ExtraerDatosProduccion(BuscarFila(), dgvLlenadora);
                //Producción
                HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL2;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioLlenL2;
                if (Properties.Settings.Default.DPDeposito2LlenL2 == "") DepositoTB.Text = Properties.Settings.Default.DPDepositoLlenL2;
                else DepositoTB.Text = Properties.Settings.Default.DPDeposito2LlenL2;
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (Properties.Settings.Default.DPiDLanzLlenL3 != "") ExtraerDatosProduccion(BuscarFila(), dgvLlenadora);
                //Producción
                HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL3;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioLlenL3;
                if (Properties.Settings.Default.DPDeposito2LlenL3 == "") DepositoTB.Text = Properties.Settings.Default.DPDepositoLlenL3;
                else DepositoTB.Text = Properties.Settings.Default.DPDeposito2LlenL3;
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (Properties.Settings.Default.DPiDLanzLlenL5 != "") ExtraerDatosProduccion(BuscarFila(), dgvLlenadora);
                //Producción
                HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL5;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioLlenL5;
                if (Properties.Settings.Default.DPDeposito2LlenL5 == "") DepositoTB.Text = Properties.Settings.Default.DPDepositoLlenL5;
                else DepositoTB.Text = Properties.Settings.Default.DPDeposito2LlenL5;
            }
            //Puesto que la tabla tarda en cargar se han ocultado previamente algunos campos que se muestran a continuación
            DatosProduccionBOX.Visible = true;

        }
        //
        internal void AdvertenciaParo(bool paro)
        {
            inicio_paro = paro;
            statusboton_paro = inicio_paro ? true : false;
            
        }

        /// <summary>
        /// Función del temporizador que se ejecuta cada segundo, indíca la hora y si se activa la alarma. 
        /// </summary>
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

            //Alarma para el control cada 30min
            if (DateTime.Now.Minute == 30 || DateTime.Now.Minute == 00)
            {
                if (!Apps_Llenadora.controlsaved)
                {
                    this.Control_30_min();
                }
            }
            if ((DateTime.Now.Minute != 30 && DateTime.Now.Minute != 00) && Apps_Llenadora.controlsaved)
            {
                Apps_Llenadora.controlsaved = false;
            }



            //Para activar la alarma debe, estar desactivada, haberse chequeado el inicio de turno y que la hora cuadre con la alarma 
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }

            //Cuando la alarma esta activada, el cambio de turno parpadeará cada segundo
            if (MaquinaLinea.numlin == 2 )
            {
                if (MaquinaLinea.chalarmaLlenL2 == true)
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
            if (MaquinaLinea.numlin == 3 && MaquinaLinea.chalarmaLlenL3 == true)
            {
                if (statusboton)
                {
                    statusboton = false;
                    CambioTurnoB.BackgroundImage = WHPS.Properties.Resources.CambioTurnoSalirAlarmaRojo;
                }
                else
                {
                    statusboton = true;
                    CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoSalirAlarmaAmarillo;
                }
            }
            if (MaquinaLinea.numlin == 5 && MaquinaLinea.chalarmaLlenL5 == true)
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
            string Hora = lbReloj.Text;
            if (Hora.Substring(3, 2) != "00" && Hora.Substring(3, 2) != "30"){ MaquinaLinea.AnuladorAlarma = true;}
            if (Hora.Substring(3, 2) == "00" || Hora.Substring(3, 2) == "30")
            {
                if (MaquinaLinea.AnuladorAlarma == true)  {Apps_Llenadora.AlarmaControl30min();}
            }
            if (MaquinaLinea.numlin == 2 && Properties.Settings.Default.AlarmaC30LlenL2 == true) { Control30mB.BackColor = Color.Red; }
            if (MaquinaLinea.numlin == 3 && Properties.Settings.Default.AlarmaC30LlenL3 == true) { Control30mB.BackColor = Color.Red; }
            if (MaquinaLinea.numlin == 5 && Properties.Settings.Default.AlarmaC30LlenL5 == true) { Control30mB.BackColor = Color.Red; }

        }

        private void Control_30_min()
        {
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
        }
        //Cuando pulsa el boton, registra el tiempo en la variable correspondiente
        private void ParoB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chLlenL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                        Llenadora_Registro_Paro Form = new Llenadora_Registro_Paro(this, inicio_paro, hora_ini_paro, temp);
                        Hide();
                        Form.Show();
                       // Form.PDesdeTB.Text = (inicio_paro == true) ? hora_ini_paro : DateTime.Now.ToString("HH:mm:ss");
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
                    
                        Llenadora_Registro_Paro Form = new Llenadora_Registro_Paro(this, inicio_paro, hora_ini_paro, temp);
                        Hide();
                        Form.Show();
                        //Form.PDesdeTB.Text = (inicio_paro == true) ? hora_ini_paro : DateTime.Now.ToString("HH:mm:ss");
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
                    
                        Llenadora_Registro_Paro Form = new Llenadora_Registro_Paro(this, inicio_paro, hora_ini_paro, temp);
                        Hide();
                        Form.Show();
                       // Form.PDesdeTB.Text = (inicio_paro == true) ? hora_ini_paro : DateTime.Now.ToString("HH:mm:ss");
                        GC.Collect();
                    
                }
                else
                {
                    this.Hide();
                    FormCambioTurno.Show();
                }
            }
        }

        //Abre el form del cambio de turno
        private void CambioTurnoB_Click(object sender, EventArgs e)
        {
            Utilidades.AbrirForm(FormCambioTurno, this, typeof(Llenadora_CambioTurno));

        }

        //Abre el form de control de medida de presión
        private void ControlPresionB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chLlenL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormControlPresion, this, typeof(Llenadora_Control_Presion));

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
                    Utilidades.AbrirForm(FormControlPresion, this, typeof(Llenadora_Control_Presion));

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
                    Utilidades.AbrirForm(FormControlPresion, this, typeof(Llenadora_Control_Presion));

                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Llenadora_CambioTurno));


                }
            }
        }

        //Abre el form de comentarios
        private void ComentB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chLlenL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormComentarios, this, typeof(Llenadora_Comentarios));

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
                    Utilidades.AbrirForm(FormComentarios, this, typeof(Llenadora_Comentarios));

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
                    Utilidades.AbrirForm(FormComentarios, this, typeof(Llenadora_Comentarios));

                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Llenadora_CambioTurno));

                }
            }
        }

        //Abre el form de control de temperatura
        private void TemperaturaB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chLlenL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormControlTemperatura, this, typeof(Llenadora_Control_Temperatura));

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
                    Utilidades.AbrirForm(FormControlTemperatura, this, typeof(Llenadora_Control_Temperatura));

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
                    Utilidades.AbrirForm(FormControlTemperatura, this, typeof(Llenadora_Control_Temperatura));

                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Llenadora_CambioTurno));

                }
            }
        }

        private void RegistroB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chLlenL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    if (OrdenTB.Text == "") MessageBox.Show("Debes primero realizar la selección del producto. Para ello, pulsa el boton que aparece haciendo doble click en el mismo.");
                    else
                    {
                        Properties.Settings.Default.DPCodigoProdLlenL2 = CodProductoTB.Text;
                        Properties.Settings.Default.DPCapacidadLlenL2 = CapacidadTB.Text;
                        Properties.Settings.Default.DPProductoDespL2 = ProductoTB.Text;
                        Properties.Settings.Default.DPGraduacionLlenL2 = GraduacionTB.Text;
                        Properties.Settings.Default.Save();
                        Utilidades.AbrirForm(FormProduccion, this, typeof(Llenadora_Registro_Produccion));

                    }
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
                    if (OrdenTB.Text == "") MessageBox.Show("Debes primero realizar la selección del producto. Para ello, pulsa el boton que aparece haciendo doble click en el mismo.");
                    else
                    {
                        Properties.Settings.Default.DPCodigoProdLlenL3 = CodProductoTB.Text;
                        Properties.Settings.Default.DPCapacidadLlenL3 = CapacidadTB.Text;
                        Properties.Settings.Default.DPProductoDespL3 = ProductoTB.Text;
                        Properties.Settings.Default.DPGraduacionLlenL3 = GraduacionTB.Text;
                        Properties.Settings.Default.Save();
                        Utilidades.AbrirForm(FormProduccion, this, typeof(Llenadora_Registro_Produccion));

                    }
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
                    if (OrdenTB.Text == "") MessageBox.Show("Debes primero realizar la selección del producto. Para ello, pulsa el boton que aparece haciendo doble click en el mismo.");
                    else
                    {
                        Properties.Settings.Default.DPCodigoProdLlenL5 = CodProductoTB.Text;
                        Properties.Settings.Default.DPCapacidadLlenL5 = CapacidadTB.Text;
                        Properties.Settings.Default.DPProductoDespL5 = ProductoTB.Text;
                        Properties.Settings.Default.DPGraduacionLlenL5 = GraduacionTB.Text;
                        Properties.Settings.Default.Save();
                        Utilidades.AbrirForm(FormProduccion, this, typeof(Llenadora_Registro_Produccion));

                    }
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Llenadora_CambioTurno));

                }
            }
        }

        private void RotasB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.RotCodProd = CodProductoTB.Text;
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chLlenL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormRotura, this, typeof(Llenadora_RotBotellas));

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
                    Utilidades.AbrirForm(FormRotura, this, typeof(Llenadora_RotBotellas));

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
                    Utilidades.AbrirForm(FormRotura, this, typeof(Llenadora_RotBotellas));

                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Llenadora_CambioTurno));

                }
            }

        }

        private void Control30mB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chLlenL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Properties.Settings.Default.DPCapacidadLlenL2 = CapacidadTB.Text;
                    Properties.Settings.Default.DPGraduacionLlenL2 = GraduacionTB.Text;
                    Properties.Settings.Default.Save();
                    Utilidades.AbrirForm(FormControl30min, this, typeof(Llenadora_Control30m));

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
                    Properties.Settings.Default.DPCapacidadLlenL3 = CapacidadTB.Text;
                    Properties.Settings.Default.DPGraduacionLlenL3 = GraduacionTB.Text;
                    Properties.Settings.Default.Save();
                    Utilidades.AbrirForm(FormControl30min, this, typeof(Llenadora_Control30m));

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
                    Properties.Settings.Default.DPCapacidadLlenL5 = CapacidadTB.Text;
                    Properties.Settings.Default.DPGraduacionLlenL5 = GraduacionTB.Text;
                    Properties.Settings.Default.Save();
                    Utilidades.AbrirForm(FormControl30min, this, typeof(Llenadora_Control30m));

                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Llenadora_CambioTurno));

                }
            }
        }

        private void Verificación_Cierre_VolumenB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.CodigoProd = CodProductoTB.Text;
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chLlenL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormVerificacionCierre, this, typeof(Llenadora_Verificacion_Cierre_Volumen));

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
                    Utilidades.AbrirForm(FormVerificacionCierre, this, typeof(Llenadora_Verificacion_Cierre_Volumen));

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
                    Utilidades.AbrirForm(FormVerificacionCierre, this, typeof(Llenadora_Verificacion_Cierre_Volumen));

                }
                else
                { 
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Llenadora_CambioTurno));

            }
        }
        }

        private void Control_VolumenB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chLlenL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Properties.Settings.Default.DPCapacidadLlenL2 = CapacidadTB.Text;
                    Properties.Settings.Default.DPGraduacionLlenL2 = GraduacionTB.Text;
                    Properties.Settings.Default.Save();
                    this.Hide();
                    FormControlVolumen.Show();
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
                    Properties.Settings.Default.DPCapacidadLlenL3 = CapacidadTB.Text;
                    Properties.Settings.Default.DPGraduacionLlenL3 = GraduacionTB.Text;
                    Properties.Settings.Default.Save();
                    this.Hide();
                    FormControlVolumen.Show();
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
                    Properties.Settings.Default.DPCapacidadLlenL5 = CapacidadTB.Text;
                    Properties.Settings.Default.DPGraduacionLlenL5 = GraduacionTB.Text;
                    Properties.Settings.Default.Save();
                    this.Hide();
                    FormControlVolumen.Show();
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Llenadora_CambioTurno));

                }
            }
        }

        private void TorquimetroB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.CodigoProd = CodProductoTB.Text;
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chLlenL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    this.Hide();
                    FormTorquimetro.Show();
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
                    this.Hide();
                    FormTorquimetro.Show();
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
                    this.Hide();
                    FormTorquimetro.Show();
                }
                else
                {
                    Utilidades.AbrirForm(FormCambioTurno, this, typeof(Llenadora_CambioTurno));

                }
            }
        }
        private void DocumentacionB_Click(object sender, EventArgs e)
        {
            Utilidades.AbrirForm(FormDocumentacion, this, typeof(Llenadora_Documentacion));

        }

        private void SiguienteB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.CapacidadLlen = CapacidadTB.Text;
            MaquinaLinea.GraduacionLLen = GraduacionTB.Text;
            if (MaquinaLinea.numlin == 2)
            {
                if (MaquinaLinea.chLlenL2 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormParte, this, typeof(Llenadora_Parte));

                }
                else
                {
                    Utilidades.AbrirForm(FormParte, this, typeof(Llenadora_Parte));

                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (MaquinaLinea.chLlenL3 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormParte, this, typeof(Llenadora_Parte));

                }
                else
                {
                    Utilidades.AbrirForm(FormParte, this, typeof(Llenadora_Parte));

                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (MaquinaLinea.chLlenL5 == true || MaquinaLinea.usuario == "Administracion")
                {
                    Utilidades.AbrirForm(FormParte, this, typeof(Llenadora_Parte));

                }
                else
                {
                    Utilidades.AbrirForm(FormParte, this, typeof(Llenadora_Parte));

                }
            }
        }
        //######################   APARTADO DE LANZAMIENTO   #####################
        /// <summary>
        /// Acción que muestra las especificaciones del producto y los datos de su producción .
        /// </summary>
        private void dgvLlenadora_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            fila = e.RowIndex;
            columna = e.ColumnIndex;
            if (ClickEvent == false)
            {
                ClickEvent = true;
            }
            else
            {
                if (columna == 3 && dgvLlenadora.Rows[fila].Cells[3].Value.ToString() != "")
                {
                    try
                    {
                        //Se muestra la orden
                        string NombrePDF = dgvLlenadora.Rows[fila].Cells[3].Value.ToString();
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
                    ExtraerDatosLanz(fila, dgvLlenadora);
                    ColorTextBox();
                    DataGridViewPANEL.Visible = false;
                    DatosSeleccionadoBOX.Visible = true;

                    if (MaquinaLinea.numlin == 2)
                        if(Convert.ToString(fila) != Properties.Settings.Default.FilaSeleccionadaLlenL2)SeleccionarProductoB.BackColor=Color.FromArgb(27, 33, 41);
                        else SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
                    if (MaquinaLinea.numlin == 3)
                        if (Convert.ToString(fila) != Properties.Settings.Default.FilaSeleccionadaLlenL3) SeleccionarProductoB.BackColor = Color.FromArgb(27, 33, 41);
                        else SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
                    if (MaquinaLinea.numlin == 5)
                        if (Convert.ToString(fila) != Properties.Settings.Default.FilaSeleccionadaLlenL5) SeleccionarProductoB.BackColor = Color.FromArgb(27, 33, 41);
                        else SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
             
                }
            }
        }
        private void dgvLlenadora_SelectionChanged(object sender, EventArgs e)
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
             FiltracionSelecTB.Text = ExtraerTipoFiltracion(datos_lanzamiento.referencia);
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

            //Se calcula la capacidad y la cantidad de botellas que requiere el producto para ser completado
            double Capacidad;
            string formato = datos_lanzamiento.formato;
            caja = Convert.ToDouble(datos_lanzamiento.caja);
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
            Capacidad = Convert.ToDouble(formato) * 1000;
            CapacidadTB.Text = Convert.ToString(Capacidad);

            if (MaquinaLinea.numlin == 2)
            {
                if (Properties.Settings.Default.BotellasAProducirLlenL2 == "") Properties.Settings.Default.BotellasAProducirLlenL2 = Convert.ToString(caja * botellascaja);
                NBotTB.Text = Properties.Settings.Default.BotellasAProducirLlenL2;
                Properties.Settings.Default.DPiDLanzLlenL2 = datos_lanzamiento.iDLanz;
                Properties.Settings.Default.DPEstadoLlenL2 = datos_lanzamiento.estado;
                Properties.Settings.Default.DPReferenciaLlenL2 = datos_lanzamiento.referencia;
                Properties.Settings.Default.DPCodigoProdLlenL2 = CodProductoTB.Text;
                Properties.Settings.Default.DPOrdenLlenL2 = OrdenTB.Text;
                Properties.Settings.Default.DPProductoLlenL2 = ProductoTB.Text;
                Properties.Settings.Default.DPClienteLlenL2 = ClienteTB.Text;
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (Properties.Settings.Default.BotellasAProducirLlenL3 == "") Properties.Settings.Default.BotellasAProducirLlenL3 = Convert.ToString(caja * botellascaja);
                NBotTB.Text = Properties.Settings.Default.BotellasAProducirLlenL3;
                Properties.Settings.Default.DPiDLanzLlenL3 = datos_lanzamiento.iDLanz;
                Properties.Settings.Default.DPEstadoLlenL3 = datos_lanzamiento.estado;
                Properties.Settings.Default.DPReferenciaLlenL3 = datos_lanzamiento.referencia;
                Properties.Settings.Default.DPCodigoProdLlenL3 = CodProductoTB.Text;
                Properties.Settings.Default.DPOrdenLlenL3 = OrdenTB.Text;
                Properties.Settings.Default.DPProductoLlenL3 = ProductoTB.Text;
                Properties.Settings.Default.DPClienteLlenL3 = ClienteTB.Text;
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (Properties.Settings.Default.BotellasAProducirLlenL5 == "") Properties.Settings.Default.BotellasAProducirLlenL5 = Convert.ToString(caja * botellascaja);
                NBotTB.Text = Properties.Settings.Default.BotellasAProducirLlenL5;
                Properties.Settings.Default.DPiDLanzLlenL5 = datos_lanzamiento.iDLanz;
                Properties.Settings.Default.DPEstadoLlenL5 = datos_lanzamiento.estado;
                Properties.Settings.Default.DPReferenciaLlenL5 = datos_lanzamiento.referencia;
                Properties.Settings.Default.DPCodigoProdLlenL5 = CodProductoTB.Text;
                Properties.Settings.Default.DPOrdenLlenL5 = OrdenTB.Text;
                Properties.Settings.Default.DPProductoLlenL5 = ProductoTB.Text;
                Properties.Settings.Default.DPClienteLlenL5 = ClienteTB.Text;
            }
            Properties.Settings.Default.Save();

            //Se marca de color la fila que se ha seleccionado
            if (OK_Fila)
            {
                dgvLlenadora.Rows[fila].Cells["ORDEN"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgvLlenadora.Rows[fila].Cells["FORM."].Style.BackColor = System.Drawing.Color.LightBlue;
                dgvLlenadora.Rows[fila].Cells["CAJAS"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgvLlenadora.Rows[fila].Cells["PRODUCTO"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgvLlenadora.Rows[fila].Cells["CLIENTE"].Style.BackColor = System.Drawing.Color.LightBlue;
                dgvLlenadora.Rows[fila].Cells["CÓDIGO"].Style.BackColor = System.Drawing.Color.LightBlue;
            }
            if (fila>=12)
            {
                dgvLlenadora.FirstDisplayedScrollingRowIndex = fila-6;
            }
        }

        /// <summary>
        /// Función busca que fila estamos segun el idorden se haya indicado.
        /// </summary>
        public int BuscarFila()
        {
            OK_Fila = false;
            for (int i = 0; (i < (dgvLlenadora.RowCount - 1)) && OK_Fila == false; i++)
            {
                if (MaquinaLinea.numlin == 2) { if (dgvLlenadora.Rows[i].Cells[1].Value.ToString() == Properties.Settings.Default.DPiDLanzLlenL2) { fila = i; OK_Fila = true; } }
                if (MaquinaLinea.numlin == 3) { if (dgvLlenadora.Rows[i].Cells[1].Value.ToString() == Properties.Settings.Default.DPiDLanzLlenL3) { fila = i; OK_Fila = true; } }
                if (MaquinaLinea.numlin == 5) { if (dgvLlenadora.Rows[i].Cells[1].Value.ToString() == Properties.Settings.Default.DPiDLanzLlenL5) { fila = i; OK_Fila = true; } }
            }
            return fila;
        }
        /// <summary>
        /// Función que extrae el tipo de filtración necesaria para la referencia del liquido en cuestion
        /// </summary>
        public string ExtraerTipoFiltracion(string Referencia)
        {
            string Filtro = "";
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
                    else return Filtro;
            }
            else return Filtro;
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
            MaquinaLinea.VolverA = RetornoBOM.Llen;
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
        //Seleciona el producto que se muestra
        private void SeleccionarProductoB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.ProductoSeleccionadoLlenL2 != Properties.Settings.Default.DPiDLanzLlenL2 && MaquinaLinea.numlin == 2)
            {
                MaquinaLinea.ProductoSeleccionadoLlenL2 = Properties.Settings.Default.DPiDLanzLlenL2;
                Properties.Settings.Default.BotellasAProducirLlenL2 = "";
            }
            if (MaquinaLinea.ProductoSeleccionadoLlenL3 != Properties.Settings.Default.DPiDLanzLlenL3 && MaquinaLinea.numlin == 3)
            {
                MaquinaLinea.ProductoSeleccionadoLlenL3 = Properties.Settings.Default.DPiDLanzLlenL3;
                Properties.Settings.Default.BotellasAProducirLlenL3 = "";

            }
            if (MaquinaLinea.ProductoSeleccionadoLlenL5 != Properties.Settings.Default.DPiDLanzLlenL5 && MaquinaLinea.numlin == 5)
            {
                MaquinaLinea.ProductoSeleccionadoLlenL5 = Properties.Settings.Default.DPiDLanzLlenL5;
                Properties.Settings.Default.BotellasAProducirLlenL5 = "";
            }
            SeleccionarProductoB.BackColor = Color.DarkSeaGreen;
            ExtraerDatosProduccion(fila, dgvLlenadora);
            Properties.Settings.Default.Save();
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
                if (MaquinaLinea.numlin == 2)
                {

                    Properties.Settings.Default.BotellasAProducirLlenL2 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.BotellasAProducirLlenL2) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NBotTB.Text = Properties.Settings.Default.BotellasAProducirLlenL2;
                }
                if (MaquinaLinea.numlin == 3)
                {

                    Properties.Settings.Default.BotellasAProducirLlenL3 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.BotellasAProducirLlenL3) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NBotTB.Text = Properties.Settings.Default.BotellasAProducirLlenL3;
                }
                if (MaquinaLinea.numlin == 5)
                {

                    Properties.Settings.Default.BotellasAProducirLlenL5 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.BotellasAProducirLlenL5) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NBotTB.Text = Properties.Settings.Default.BotellasAProducirLlenL5;
                }
                Properties.Settings.Default.Save();
                ConteoBotellasTB.Text = "0";
                OK_ConteoB.BackColor = Color.FromArgb(27, 33, 41);
            }
        }

        private void MaquinistaTB_Click(object sender, EventArgs e)
        {
            ExcelUtiles.CrearTablaLanzamientos(dgvLlenadora);
            if (Properties.Settings.Default.DPiDLanzLlenL2 != "" && MaquinaLinea.numlin == 2) ExtraerDatosProduccion(BuscarFila(), dgvLlenadora);
            if (Properties.Settings.Default.DPiDLanzLlenL3 != "" && MaquinaLinea.numlin == 3) ExtraerDatosProduccion(BuscarFila(), dgvLlenadora);
            if (Properties.Settings.Default.DPiDLanzLlenL5 != "" && MaquinaLinea.numlin == 5) ExtraerDatosProduccion(BuscarFila(), dgvLlenadora);
        }

        private void CalculadoraB_Click(object sender, EventArgs e)
        {
            if (calculadora1.Visible == true) { calculadora1.Visible = false; CalculadoraB.BackColor = Color.FromArgb(27, 33, 41); }
            else { calculadora1.Visible = true; CalculadoraB.BackColor = Color.DarkSeaGreen; }
        }




        /// <summary>
        /// Función que detecta las celdas, marca las celdas que tienen que sobresalir y detecta que producto esta iniciado para rellenar los datos de producción.
        /// </summary>
        private void dgvLlenadora_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLlenadora.Columns[e.ColumnIndex].Name == "LÍQUIDOS")
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

            if (dgvLlenadora.Columns[e.ColumnIndex].Name == "ESTADO")
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
            if (dgvLlenadora.Columns[e.ColumnIndex].Name == "MATERIALES")
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
        public void SetComentarios(Llenadora_Comentarios c)
        {
            FormComentarios = c;
        }
        public Llenadora_Comentarios GetComentarios()
        {
            return FormComentarios;
        }
        public WHPST_INICIO GetParentInicio()
        {
            return parentInicio;
        }
        //########################################################################

    }
}

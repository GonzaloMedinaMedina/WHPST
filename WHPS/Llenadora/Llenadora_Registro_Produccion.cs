using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.ProgramMenus;
using WHPS.Utiles;

namespace WHPS.Llenadora
{
    public partial class Llenadora_Registro_Produccion : Form
    {
        TextBox TextBox;
        private List<TextBox> mis_tb = new List<TextBox>();
        public Llenadora_Registro_Produccion()
        {
            InitializeComponent();
            mis_tb.Add(BarraCopiadaTB);
            mis_tb.Add(DepositoCopiadoTB);
            mis_tb.Add(FrioCopiadoTB);
            //mis_tb.Add();
            

        }

        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>
        private void ExitB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                Properties.Settings.Default.DPBarraLlenL2 = BarraTB.Text;
                Properties.Settings.Default.DPDepositoLlenL2 =  DepositoTB.Text;
                Properties.Settings.Default.DPFrioLlenL2 = FrioTB.Text;
                Properties.Settings.Default.DPNBotellasLlenL2 = NBotTB.Text;
                Properties.Settings.Default.DPBarra2LlenL2 = Barra2TB.Text;
                Properties.Settings.Default.DPDeposito2LlenL2 =  Deposito2TB.Text;
                Properties.Settings.Default.DPFrio2LlenL2 = Frio2TB.Text;
                Properties.Settings.Default.DPNBotellas2LlenL2 = NBot2TB.Text;
                Properties.Settings.Default.DPHInicioLlenL2 = HInicioTB.Text;
                Properties.Settings.Default.DPHFinLlenL2 = HFinTB.Text;
                MaquinaLinea.BarraCopiadoLlenL2 = BarraCopiadaTB.Text;
                MaquinaLinea.DepCopiadoLlenL2 = DepositoCopiadoTB.Text;
                MaquinaLinea.FrioCopiadoLlenL2 = FrioCopiadoTB.Text;

            }
            if (MaquinaLinea.numlin == 3)
            {
                Properties.Settings.Default.DPBarraLlenL3 = BarraTB.Text;
                Properties.Settings.Default.DPDepositoLlenL3 = DepositoTB.Text;
                Properties.Settings.Default.DPFrioLlenL3 = FrioTB.Text;
                Properties.Settings.Default.DPNBotellasLlenL3 =  NBotTB.Text;
                Properties.Settings.Default.DPBarra2LlenL3 = Barra2TB.Text;
                Properties.Settings.Default.DPDeposito2LlenL3 = Deposito2TB.Text;
                Properties.Settings.Default.DPFrio2LlenL3 = Frio2TB.Text;
                Properties.Settings.Default.DPNBotellas2LlenL3 = NBot2TB.Text;
                Properties.Settings.Default.DPHInicioLlenL3 = HInicioTB.Text;
                Properties.Settings.Default.DPHFinLlenL3 = HFinTB.Text;
                MaquinaLinea.BarraCopiadoLlenL3 = BarraCopiadaTB.Text;
                MaquinaLinea.DepCopiadoLlenL3 = DepositoCopiadoTB.Text;
                MaquinaLinea.FrioCopiadoLlenL3 = FrioCopiadoTB.Text;
            }
            if (MaquinaLinea.numlin == 5)
            {
                Properties.Settings.Default.DPBarraLlenL5 = BarraTB.Text;
                Properties.Settings.Default.DPDepositoLlenL5 = DepositoTB.Text;
                Properties.Settings.Default.DPFrioLlenL5 = FrioTB.Text;
                Properties.Settings.Default.DPNBotellasLlenL5 = NBotTB.Text;
                Properties.Settings.Default.DPBarra2LlenL5 = Barra2TB.Text;
                Properties.Settings.Default.DPDeposito2LlenL5 = Deposito2TB.Text;
                Properties.Settings.Default.DPFrio2LlenL5 = Frio2TB.Text;
                Properties.Settings.Default.DPNBotellas2LlenL5 = NBot2TB.Text;
                Properties.Settings.Default.DPHInicioLlenL5 = HInicioTB.Text;
                Properties.Settings.Default.DPHFinLlenL5 = HFinTB.Text;
                MaquinaLinea.BarraCopiadoLlenL5 = BarraCopiadaTB.Text;
                MaquinaLinea.DepCopiadoLlenL5 = DepositoCopiadoTB.Text;
                MaquinaLinea.FrioCopiadoLlenL5 = FrioCopiadoTB.Text;
            }
            Properties.Settings.Default.Save();
            MainLlenadora Form = new MainLlenadora();
            Hide();
            Form.Show();
            GC.Collect();
        }

        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        private void Llenadora_Registro_Produccion_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;



            //Rellenamos los datos del equipo
            dateTB.Text = DateTime.Now.ToString("dd/MM/yyyy");
            respTB.Text = MaquinaLinea.Responsable;
            maqTB.Text = MaquinaLinea.MLlenadora;
            turnoTB.Text = Utilidades.ObtenerTurnoActual();


            if (MaquinaLinea.numlin == 2)
            {
                //Rellenamos los datos obtenidos del lanzamiento
                ReferenciaTB.Text = Properties.Settings.Default.DPReferenciaLlenL2;
                OrdenTB.Text = Properties.Settings.Default.DPOrdenLlenL2;
                CodigoProdTB.Text = Properties.Settings.Default.DPCodigoProdLlenL2;
                CapacidadTB.Text = Properties.Settings.Default.DPCapacidadLlenL2;
                ProductoTB.Text = Properties.Settings.Default.DPProductoDespL2;
                GraduacionTB.Text = Properties.Settings.Default.DPGraduacionLlenL2;
                ClienteTB.Text = Properties.Settings.Default.DPClienteLlenL2;
                BotellasAProducirLB.Text = "(Botellas a producir: " + Properties.Settings.Default.BotellasAProducirLlenL2+ ")";

                //Rellenamos los registros que ya han sido guardados
                DepositoTB.Text = Properties.Settings.Default.DPDepositoLlenL2;
                FrioTB.Text = Properties.Settings.Default.DPFrioLlenL2;
                BarraTB.Text = Properties.Settings.Default.DPBarraLlenL2;
                NBotTB.Text = Properties.Settings.Default.DPNBotellasLlenL2;
                Deposito2TB.Text = Properties.Settings.Default.DPDeposito2LlenL2;
                Frio2TB.Text = Properties.Settings.Default.DPFrio2LlenL2;
                Barra2TB.Text = Properties.Settings.Default.DPBarra2LlenL2;
                NBot2TB.Text = Properties.Settings.Default.DPNBotellas2LlenL2;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL2;
                HFinTB.Text = Properties.Settings.Default.DPHFinLlenL2;
                BarraCopiadaTB.Text = MaquinaLinea.BarraCopiadoLlenL2;
                DepositoCopiadoTB.Text = MaquinaLinea.DepCopiadoLlenL2;
                FrioCopiadoTB.Text = MaquinaLinea.FrioCopiadoLlenL2;
                //Indicamos que muestre el boton verde cuando el tiempo de preparacion ha comenzado
                if (Properties.Settings.Default.DPHInicioCambioLlenL2 != "" && HInicioTB.Text == "") LanzamientocargadoB.BackColor = Color.DarkSeaGreen;
            }
            if (MaquinaLinea.numlin == 3)
            {
                //Rellenamos los datos obtenidos del lanzamiento
                ReferenciaTB.Text = Properties.Settings.Default.DPReferenciaLlenL3;
                OrdenTB.Text = Properties.Settings.Default.DPOrdenLlenL3;
                CodigoProdTB.Text = Properties.Settings.Default.DPCodigoProdLlenL3;
                CapacidadTB.Text = Properties.Settings.Default.DPCapacidadLlenL3;
                ProductoTB.Text = Properties.Settings.Default.DPProductoDespL3;
                GraduacionTB.Text = Properties.Settings.Default.DPGraduacionLlenL3;
                ClienteTB.Text = Properties.Settings.Default.DPClienteLlenL3;
                BotellasAProducirLB.Text = "(Botellas a producir: " + Properties.Settings.Default.BotellasAProducirLlenL3+ ")";

                //Rellenamos los registros que ya han sido guardados
                DepositoTB.Text = Properties.Settings.Default.DPDepositoLlenL3;
                FrioTB.Text = Properties.Settings.Default.DPFrioLlenL3;
                BarraTB.Text = Properties.Settings.Default.DPBarraLlenL3;
                NBotTB.Text = Properties.Settings.Default.DPNBotellasLlenL3;
                Deposito2TB.Text = Properties.Settings.Default.DPDeposito2LlenL3;
                Frio2TB.Text = Properties.Settings.Default.DPFrio2LlenL3;
                Barra2TB.Text = Properties.Settings.Default.DPBarra2LlenL3;
                NBot2TB.Text = Properties.Settings.Default.DPNBotellas2LlenL3;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL3;
                HFinTB.Text = Properties.Settings.Default.DPHFinLlenL3;
                BarraCopiadaTB.Text = MaquinaLinea.BarraCopiadoLlenL3;
                DepositoCopiadoTB.Text = MaquinaLinea.DepCopiadoLlenL3;
                FrioCopiadoTB.Text = MaquinaLinea.FrioCopiadoLlenL3;
                //Indicamos que muestre el boton verde cuando el tiempo de preparacion ha comenzado
                if (Properties.Settings.Default.DPHInicioCambioLlenL3 != "" && HInicioTB.Text == "") LanzamientocargadoB.BackColor = Color.DarkSeaGreen;
            }
            if (MaquinaLinea.numlin == 5)
            {
                //Rellenamos los datos obtenidos del lanzamiento
                ReferenciaTB.Text = Properties.Settings.Default.DPReferenciaLlenL5;
                OrdenTB.Text = Properties.Settings.Default.DPOrdenLlenL5;
                CodigoProdTB.Text = Properties.Settings.Default.DPCodigoProdLlenL5;
                CapacidadTB.Text = Properties.Settings.Default.DPCapacidadLlenL5;
                ProductoTB.Text = Properties.Settings.Default.DPProductoDespL5;
                GraduacionTB.Text = Properties.Settings.Default.DPGraduacionLlenL5;
                ClienteTB.Text = Properties.Settings.Default.DPClienteLlenL5;
                BotellasAProducirLB.Text = "(Botellas a producir: " + Properties.Settings.Default.BotellasAProducirLlenL5+ ")";

                //Rellenamos los registros que ya han sido guardados
                DepositoTB.Text = Properties.Settings.Default.DPDepositoLlenL5;
                FrioTB.Text = Properties.Settings.Default.DPFrioLlenL5;
                BarraTB.Text = Properties.Settings.Default.DPBarraLlenL5;
                NBotTB.Text = Properties.Settings.Default.DPNBotellasLlenL5;
                Deposito2TB.Text = Properties.Settings.Default.DPDeposito2LlenL5;
                Frio2TB.Text = Properties.Settings.Default.DPFrio2LlenL5;
                Barra2TB.Text = Properties.Settings.Default.DPBarra2LlenL5;
                NBot2TB.Text = Properties.Settings.Default.DPNBotellas2LlenL5;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL5;
                HFinTB.Text = Properties.Settings.Default.DPHFinLlenL5;
                BarraCopiadaTB.Text = MaquinaLinea.BarraCopiadoLlenL5;
                DepositoCopiadoTB.Text = MaquinaLinea.DepCopiadoLlenL5;
                FrioCopiadoTB.Text = MaquinaLinea.FrioCopiadoLlenL5;
                //Indicamos que muestre el boton verde cuando el tiempo de preparacion ha comenzado
                if (Properties.Settings.Default.DPHInicioCambioLlenL5 != "" && HInicioTB.Text == "") LanzamientocargadoB.BackColor = Color.DarkSeaGreen;
            }
            //Indicamos si estamos en tiempo de preparación o no cambiando el color del boton
            if (HInicioTB.Text != "") { LanzamientocargadoB.BackColor = Color.FromArgb(27, 33, 41); ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar; }
            else ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionInicio;

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
            string Hora = lbReloj.Text;
            if (Hora.Substring(3, 2) != "00" && Hora.Substring(3, 2) != "30")
            {
                MaquinaLinea.AnuladorAlarma = true;
            }
            if (Hora.Substring(3, 2) == "00" || Hora.Substring(3, 2) == "30")
            {
                if (MaquinaLinea.AnuladorAlarma == true)
                {
                    Apps_Llenadora.AlarmaControl30min();
                }
            }
        }

        //Al hacer click en los textbox se mostrará un taclado para completar el formulario
        private void BarraTB_MouseClick(object sender, MouseEventArgs e)
        {
                TextBox = BarraTB;
                WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,BarraTB);
        }
        private void DepositoTB_MouseClick(object sender, MouseEventArgs e)
        {         
                
                TextBox = DepositoTB;
                WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,DepositoTB);
            
        }
        private void NBotTB_MouseClick(object sender, MouseEventArgs e)
        {
                
                TextBox = NBotTB;
                WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,NBotTB);
  
        }
        private void Barra2TB_MouseClick(object sender, MouseEventArgs e)
        {
            
                
                TextBox = Barra2TB;
                WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,Barra2TB);                
            
        }
        private void Deposito2TB_MouseClick(object sender, MouseEventArgs e)
        {
           
                
                TextBox = Deposito2TB;
                WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,Deposito2TB);
 
        }
        private void NBot2TB_MouseClick(object sender, MouseEventArgs e)
        {
                
                TextBox = NBot2TB;
                WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,NBot2TB);
            

        }
        private void FrioTB_MouseClick(object sender, MouseEventArgs e)
        {
                
                TextBox = FrioTB;
                WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,FrioTB);
 
        }

        private void Frio2TB_MouseClick(object sender, MouseEventArgs e)
        {
                
                TextBox = Frio2TB;
                WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,Frio2TB);

      
        }

        //Registrará el tiempo en el TB de cuya variable sea true
        private void LanzamientoCargadoB_Click(object sender, EventArgs e)
        {
            if (HInicioTB.Text == "")
            {
                if (MaquinaLinea.numlin == 2 && Properties.Settings.Default.DPHInicioCambioLlenL2 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioLlenL2 = DateTime.Now.ToString("HH:mm:ss");
                    LanzamientocargadoB.BackColor = Color.DarkSeaGreen;
                }
                if (MaquinaLinea.numlin == 3 && Properties.Settings.Default.DPHInicioCambioLlenL3 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioLlenL3 = DateTime.Now.ToString("HH:mm:ss");
                    LanzamientocargadoB.BackColor = Color.DarkSeaGreen;
                }
                if (MaquinaLinea.numlin == 5 && Properties.Settings.Default.DPHInicioCambioLlenL5 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioLlenL5 = DateTime.Now.ToString("HH:mm:ss");
                    LanzamientocargadoB.BackColor = Color.DarkSeaGreen;
                }
                Properties.Settings.Default.Save();
            }
        }
        private void ComienzoProdB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                //Puede ser que se haya iniciado la producción.
                if (Properties.Settings.Default.DPHInicioCambioLlenL2 != "" && Properties.Settings.Default.DPHInicioLlenL2 != "" && Properties.Settings.Default.DPHFinLlenL2 == "")
                {
                    Properties.Settings.Default.DPHFinLlenL2 = DateTime.Now.ToString("HH:mm:ss");
                    HFinTB.Text = Properties.Settings.Default.DPHFinLlenL2;
                }
                //Puede ser que SI se haya iniciado la preparacion por que no sea necesario.
                //MessageBox.Show("prerp: " + Properties.Settings.Default.DPHInicioCambioLlenL2 + "inicio: " + Properties.Settings.Default.DPHInicioLlenL2 + "fin: " + Properties.Settings.Default.DPHFinLlenL2);
                if (Properties.Settings.Default.DPHInicioCambioLlenL2 != "" && Properties.Settings.Default.DPHInicioLlenL2 == "" && Properties.Settings.Default.DPHFinLlenL2 == "")
                {
                    Properties.Settings.Default.DPHInicioLlenL2 = DateTime.Now.ToString("HH:mm:ss");
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL2;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinLlenL2 = "";

                    //Filtro ID ORDEN
                    string[] valoresAFiltrar = new string[4];
                    valoresAFiltrar[0] = "AND";
                    valoresAFiltrar[1] = "ID_Lanz";
                    valoresAFiltrar[2] = "LIKE";
                    valoresAFiltrar[3] = "\"" + Properties.Settings.Default.DPiDLanzLlenL2 + "\"";
                    string[] valoresAActualizar = new string[2];
                    valoresAActualizar[0] = "ESTADO";
                    valoresAActualizar[1] = "Iniciado";

                    bool salida;
                    salida = ExcelUtiles.ActualizarCeldaExcel("DB_L2", "Linea 2", valoresAActualizar, valoresAFiltrar);
                    //MessageBox.Show(salida.ToString());

                    valoresAActualizar[0] = "FECHAINICIO";
                    valoresAActualizar[1] = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

                    salida = ExcelUtiles.ActualizarCeldaExcel("DB_L2", "Linea 2", valoresAActualizar, valoresAFiltrar);
                    //MessageBox.Show(salida.ToString());

                }
                //Puede ser que NO se haya iniciado la preparacion por que no sea necesario.
                if (Properties.Settings.Default.DPHInicioCambioLlenL2 == "" && Properties.Settings.Default.DPHInicioLlenL2 == "" && Properties.Settings.Default.DPHFinLlenL2 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioLlenL2 = DateTime.Now.ToString("HH:mm:ss");
                    Properties.Settings.Default.DPHInicioLlenL2 = Properties.Settings.Default.DPHInicioCambioLlenL2;
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL2;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinLlenL2 = "";
                    if (Properties.Settings.Default.DPEstadoLlenL2 != "Iniciado")
                    {
                        //Filtro ID ORDEN
                        string[] valoresAFiltrar = new string[4];
                        valoresAFiltrar[0] = "AND";
                        valoresAFiltrar[1] = "ID_Lanz";
                        valoresAFiltrar[2] = "LIKE";
                        valoresAFiltrar[3] = "\"" + Properties.Settings.Default.DPiDLanzLlenL2 + "\"";
                        string[] valoresAActualizar = new string[2];
                        valoresAActualizar[0] = "ESTADO";
                        valoresAActualizar[1] = "Iniciado";

                        bool salida;
                        salida = ExcelUtiles.ActualizarCeldaExcel("DB_L2", "Linea 2", valoresAActualizar, valoresAFiltrar);
                        //MessageBox.Show(salida.ToString());

                        valoresAActualizar[0] = "FECHAINICIO";
                        valoresAActualizar[1] = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

                        salida = ExcelUtiles.ActualizarCeldaExcel("DB_L2", "Linea 2", valoresAActualizar, valoresAFiltrar);
                        //MessageBox.Show(salida.ToString());
                    }
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                //Puede ser que se haya iniciado la producción.
                if (Properties.Settings.Default.DPHInicioCambioLlenL3 != "" && Properties.Settings.Default.DPHInicioLlenL3 != "" && Properties.Settings.Default.DPHFinLlenL3 == "")
                {
                    Properties.Settings.Default.DPHFinLlenL3 = DateTime.Now.ToString("HH:mm:ss");
                    HFinTB.Text = Properties.Settings.Default.DPHFinLlenL3;
                }
                //Puede ser que SI se haya iniciado la preparacion por que no sea necesario.
                //MessageBox.Show("prerp: " + Properties.Settings.Default.DPHInicioCambioLlenL3 + "inicio: " + Properties.Settings.Default.DPHInicioLlenL3 + "fin: " + Properties.Settings.Default.DPHFinLlenL3);
                if (Properties.Settings.Default.DPHInicioCambioLlenL3 != "" && Properties.Settings.Default.DPHInicioLlenL3 == "" && Properties.Settings.Default.DPHFinLlenL3 == "")
                {
                    Properties.Settings.Default.DPHInicioLlenL3 = DateTime.Now.ToString("HH:mm:ss");
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL3;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinLlenL3 = "";

                    //Filtro ID ORDEN
                    string[] valoresAFiltrar = new string[4];
                    valoresAFiltrar[0] = "AND";
                    valoresAFiltrar[1] = "ID_Lanz";
                    valoresAFiltrar[2] = "LIKE";
                    valoresAFiltrar[3] = "\"" + Properties.Settings.Default.DPiDLanzLlenL3 + "\"";
                    string[] valoresAActualizar = new string[2];
                    valoresAActualizar[0] = "ESTADO";
                    valoresAActualizar[1] = "Iniciado";

                    bool salida;
                    salida = ExcelUtiles.ActualizarCeldaExcel("DB_L3", "Linea 3", valoresAActualizar, valoresAFiltrar);
                    //MessageBox.Show(salida.ToString());

                    valoresAActualizar[0] = "FECHAINICIO";
                    valoresAActualizar[1] = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

                    salida = ExcelUtiles.ActualizarCeldaExcel("DB_L3", "Linea 3", valoresAActualizar, valoresAFiltrar);
                    //MessageBox.Show(salida.ToString());


                }
                //Puede ser que NO se haya iniciado la preparacion por que no sea necesario.
                if (Properties.Settings.Default.DPHInicioCambioLlenL3 == "" && Properties.Settings.Default.DPHInicioLlenL3 == "" && Properties.Settings.Default.DPHFinLlenL3 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioLlenL3 = DateTime.Now.ToString("HH:mm:ss");
                    Properties.Settings.Default.DPHInicioLlenL3 = Properties.Settings.Default.DPHInicioCambioLlenL3;
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL3;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinLlenL3 = "";
                    if (Properties.Settings.Default.DPEstadoLlenL3 != "Iniciado")
                    {
                        //Filtro ID ORDEN
                        string[] valoresAFiltrar = new string[4];
                        valoresAFiltrar[0] = "AND";
                        valoresAFiltrar[1] = "ID_Lanz";
                        valoresAFiltrar[2] = "LIKE";
                        valoresAFiltrar[3] = "\"" + Properties.Settings.Default.DPiDLanzLlenL3 + "\"";
                        string[] valoresAActualizar = new string[2];
                        valoresAActualizar[0] = "ESTADO";
                        valoresAActualizar[1] = "Iniciado";

                        bool salida;
                        salida = ExcelUtiles.ActualizarCeldaExcel("DB_L3", "Linea 3", valoresAActualizar, valoresAFiltrar);
                        //MessageBox.Show(salida.ToString());

                        valoresAActualizar[0] = "FECHAINICIO";
                        valoresAActualizar[1] = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

                        salida = ExcelUtiles.ActualizarCeldaExcel("DB_L3", "Linea 3", valoresAActualizar, valoresAFiltrar);
                        //MessageBox.Show(salida.ToString());
                    }
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                //Puede ser que se haya iniciado la producción.
                if (Properties.Settings.Default.DPHInicioCambioLlenL5 != "" && Properties.Settings.Default.DPHInicioLlenL5 != "" && Properties.Settings.Default.DPHFinLlenL5 == "")
                {
                    Properties.Settings.Default.DPHFinLlenL5 = DateTime.Now.ToString("HH:mm:ss");
                    HFinTB.Text = Properties.Settings.Default.DPHFinLlenL5;
                }
                //Puede ser que SI se haya iniciado la preparacion por que no sea necesario.
                //MessageBox.Show("prerp: " + Properties.Settings.Default.DPHInicioCambioLlenL5 + "inicio: " + Properties.Settings.Default.DPHInicioLlenL5 + "fin: " + Properties.Settings.Default.DPHFinLlenL5);
                if (Properties.Settings.Default.DPHInicioCambioLlenL5 != "" && Properties.Settings.Default.DPHInicioLlenL5 == "" && Properties.Settings.Default.DPHFinLlenL5 == "")
                {
                    Properties.Settings.Default.DPHInicioLlenL5 = DateTime.Now.ToString("HH:mm:ss");
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL5;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinLlenL5 = "";


                    //Filtro ID ORDEN
                    string[] valoresAFiltrar = new string[4];
                    valoresAFiltrar[0] = "AND";
                    valoresAFiltrar[1] = "ID_Lanz";
                    valoresAFiltrar[2] = "LIKE";
                    valoresAFiltrar[3] = "\"" + Properties.Settings.Default.DPiDLanzLlenL5 + "\"";
                    string[] valoresAActualizar = new string[2];
                    valoresAActualizar[0] = "ESTADO";
                    valoresAActualizar[1] = "Iniciado";

                    bool salida;
                    salida = ExcelUtiles.ActualizarCeldaExcel("DB_L5", "Linea 5", valoresAActualizar, valoresAFiltrar);
                    //MessageBox.Show(salida.ToString());

                    valoresAActualizar[0] = "FECHAINICIO";
                    valoresAActualizar[1] = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

                    salida = ExcelUtiles.ActualizarCeldaExcel("DB_L5", "Linea 5", valoresAActualizar, valoresAFiltrar);
                    //MessageBox.Show(salida.ToString());


                }
                //Puede ser que NO se haya iniciado la preparacion por que no sea necesario.
                if (Properties.Settings.Default.DPHInicioCambioLlenL5 == "" && Properties.Settings.Default.DPHInicioLlenL5 == "" && Properties.Settings.Default.DPHFinLlenL5 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioLlenL5 = DateTime.Now.ToString("HH:mm:ss");
                    Properties.Settings.Default.DPHInicioLlenL5 = Properties.Settings.Default.DPHInicioCambioLlenL5;
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL5;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinLlenL5 = "";
                    if (Properties.Settings.Default.DPEstadoLlenL5 != "Iniciado")
                    {
                        //Filtro ID ORDEN
                        string[] valoresAFiltrar = new string[4];
                        valoresAFiltrar[0] = "AND";
                        valoresAFiltrar[1] = "ID_Lanz";
                        valoresAFiltrar[2] = "LIKE";
                        valoresAFiltrar[3] = "\"" + Properties.Settings.Default.DPiDLanzLlenL5 + "\"";
                        string[] valoresAActualizar = new string[2];
                        valoresAActualizar[0] = "ESTADO";
                        valoresAActualizar[1] = "Iniciado";

                        bool salida;
                        salida = ExcelUtiles.ActualizarCeldaExcel("DB_L5", "Linea 5", valoresAActualizar, valoresAFiltrar);
                        //MessageBox.Show(salida.ToString());

                        valoresAActualizar[0] = "FECHAINICIO";
                        valoresAActualizar[1] = DateTime.Now.ToString("dd/MM/yyyy hh:mm");

                        salida = ExcelUtiles.ActualizarCeldaExcel("DB_L5", "Linea 5", valoresAActualizar, valoresAFiltrar);
                        //MessageBox.Show(salida.ToString());
                    }
                }
            }
            Properties.Settings.Default.Save();
        }
        //Botón que elimina toda la información del registro
        private void BorrarB_Click(object sender, EventArgs e)
        {
            DepositoTB.Text = "";
            FrioTB.Text = "";
            BarraTB.Text = "";
            NBotTB.Text = "";
            Deposito2TB.Text = "";
            Frio2TB.Text = "";
            Barra2TB.Text = "";
            NBot2TB.Text = "";
            HInicioTB.Text = "";
            HFinTB.Text = "";
            LanzamientocargadoB.BackColor = Color.FromArgb(27, 33, 41);
            ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionInicio;
            if (MaquinaLinea.numlin == 2) Properties.Settings.Default.DPHInicioCambioLlenL2 = ""; Properties.Settings.Default.DPHInicioLlenL2 = ""; Properties.Settings.Default.DPHFinLlenL2 = "";
            if (MaquinaLinea.numlin == 3) Properties.Settings.Default.DPHInicioCambioLlenL3 = ""; Properties.Settings.Default.DPHInicioLlenL3 = ""; Properties.Settings.Default.DPHFinLlenL3 = "";
            if (MaquinaLinea.numlin == 5) Properties.Settings.Default.DPHInicioCambioLlenL5 = ""; Properties.Settings.Default.DPHInicioLlenL5 = ""; Properties.Settings.Default.DPHFinLlenL5 = "";
            Properties.Settings.Default.Save();
        }

        //Guardamos que la producción se ha terminado
        private void saveBot_Click(object sender, EventArgs e)
        {
            //Para poder guardar todos los campos deben estar cumplimentados
            if (DepositoTB.Text != "" && CodigoProdTB.Text != "" && ProductoTB.Text != "" && NBotTB.Text != "" && HInicioTB.Text != "" && HFinTB.Text != "" && BarraTB.Text != "")
            {
                if (MaquinaLinea.numlin == 2) MaquinaLinea.HInicioCambioLlen = Properties.Settings.Default.DPHInicioCambioLlenL2;
                if (MaquinaLinea.numlin == 3) MaquinaLinea.HInicioCambioLlen = Properties.Settings.Default.DPHInicioCambioLlenL3;
                if (MaquinaLinea.numlin == 5) MaquinaLinea.HInicioCambioLlen = Properties.Settings.Default.DPHInicioCambioLlenL5;
                string NbotTotal = NBotTB.Text;
                if (NBot2TB.Text != "") NbotTotal = Convert.ToString(Convert.ToInt64(NBotTB.Text) + Convert.ToInt64(NBot2TB.Text));


                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "Fecha", DateTime.Now.ToString("dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Hora", DateTime.Now.ToString("HH:mm:ss") });
                listavalores.Add(new string[2] { "FechaDB", DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Responsable", MaquinaLinea.Responsable });
                listavalores.Add(new string[2] { "Maquinista", MaquinaLinea.MLlenadora });
                listavalores.Add(new string[2] { "Turno", turnoTB.Text });
                listavalores.Add(new string[2] { "Orden", OrdenTB.Text });
                listavalores.Add(new string[2] { "Deposito1", DepositoTB.Text });
                listavalores.Add(new string[2] { "Frio1", FrioTB.Text });
                listavalores.Add(new string[2] { "Barra1", BarraTB.Text });
                listavalores.Add(new string[2] { "NBotellas1", NBotTB.Text });
                listavalores.Add(new string[2] { "Deposito2", Deposito2TB.Text });
                listavalores.Add(new string[2] { "Frio2", Frio2TB.Text });
                listavalores.Add(new string[2] { "Barra2", Barra2TB.Text });
                listavalores.Add(new string[2] { "NBotellas2", NBot2TB.Text });
                listavalores.Add(new string[2] { "NBotellasTotal", NbotTotal });
                listavalores.Add(new string[2] { "CodigoProducto", CodigoProdTB.Text });
                listavalores.Add(new string[2] { "Referencia", ReferenciaTB.Text });
                listavalores.Add(new string[2] { "Capacidad", CapacidadTB.Text });
                listavalores.Add(new string[2] { "Producto", ProductoTB.Text });
                listavalores.Add(new string[2] { "Graduacion", GraduacionTB.Text });
                listavalores.Add(new string[2] { "Inicio", HInicioTB.Text });
                listavalores.Add(new string[2] { "Fin", HFinTB.Text });
                listavalores.Add(new string[2] { "InicioCambio", MaquinaLinea.HInicioCambioLlen });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileLlenadora, "Registro", listavalores, "Id");
                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show(Properties.Settings.Default.AvisoProblemaFichero);
                }
                else
                {
                    //INSERTARFILA();

                    //Restablecemos los valores correspondientes
                    DepositoTB.Text = "";
                    BarraTB.Text = "";
                    Deposito2TB.Text = "";
                    Barra2TB.Text = "";
                    CodigoProdTB.Text = "";
                    CapacidadTB.Text = "";
                    ProductoTB.Text = "";
                    NBotTB.Text = "";
                    NBot2TB.Text = "";
                    GraduacionTB.Text = "";
                    HInicioTB.Text = "";
                    HFinTB.Text = "";

                    if (MaquinaLinea.numlin == 2)
                    {
                        Properties.Settings.Default.FilaSeleccionadaLlenL2 = "";
                        Properties.Settings.Default.DPiDLanzLlenL2 = "";
                        Properties.Settings.Default.DPHInicioLlenL2 = "";
                        Properties.Settings.Default.DPHFinLlenL2 = "";
                        Properties.Settings.Default.DPHInicioCambioLlenL2 = "";
                        Properties.Settings.Default.DPNBotellasLlenL2 = "";
                        Properties.Settings.Default.DPDepositoLlenL2 = "";
                        Properties.Settings.Default.DPBarraLlenL2 = "";
                        Properties.Settings.Default.DPFrioLlenL2 = "";
                        Properties.Settings.Default.DPNBotellas2LlenL2 = "";
                        Properties.Settings.Default.DPDeposito2LlenL2 = "";
                        Properties.Settings.Default.DPBarra2LlenL2 = "";
                        Properties.Settings.Default.DPFrio2LlenL2 = "";

                    }
                    if (MaquinaLinea.numlin == 3)
                    {
                        Properties.Settings.Default.FilaSeleccionadaLlenL3 = "";
                        Properties.Settings.Default.DPiDLanzLlenL3 = "";
                        Properties.Settings.Default.DPHInicioLlenL3 = "";
                        Properties.Settings.Default.DPHFinLlenL3 = "";
                        Properties.Settings.Default.DPHInicioCambioLlenL3 = "";
                        Properties.Settings.Default.DPNBotellasLlenL3 = "";
                        Properties.Settings.Default.DPDepositoLlenL3 = "";
                        Properties.Settings.Default.DPBarraLlenL3 = "";
                        Properties.Settings.Default.DPFrioLlenL3 = "";
                        Properties.Settings.Default.DPNBotellas2LlenL3 = "";
                        Properties.Settings.Default.DPDeposito2LlenL3 = "";
                        Properties.Settings.Default.DPBarra2LlenL3 = "";
                        Properties.Settings.Default.DPFrio2LlenL3 = "";
                    }
                    if (MaquinaLinea.numlin == 5)
                    {
                        Properties.Settings.Default.FilaSeleccionadaLlenL5 = "";
                        Properties.Settings.Default.DPiDLanzLlenL5 = "";
                        Properties.Settings.Default.DPHInicioLlenL5 = "";
                        Properties.Settings.Default.DPHFinLlenL5 = "";
                        Properties.Settings.Default.DPHInicioCambioLlenL5 = "";
                        Properties.Settings.Default.DPNBotellasLlenL5 = "";
                        Properties.Settings.Default.DPDepositoLlenL5 = "";
                        Properties.Settings.Default.DPBarraLlenL5 = "";
                        Properties.Settings.Default.DPFrioLlenL5 = "";
                        Properties.Settings.Default.DPNBotellas2LlenL5 = "";
                        Properties.Settings.Default.DPDeposito2LlenL5 = "";
                        Properties.Settings.Default.DPBarra2LlenL5 = "";
                        Properties.Settings.Default.DPFrio2LlenL5 = "";
                    }
                    LanzamientocargadoB.BackColor = Color.FromArgb(27, 33, 41);
                    Properties.Settings.Default.Save();
                    //MessageBox.Show(salida);
                    MainLlenadora Form = new MainLlenadora();
                    Hide();
                    Form.Show();
                    GC.Collect();
                }
            }
            else
            {
                MessageBox.Show(Properties.Settings.Default.AvisoCampos);
            }
        }

     
        private void CopiarAB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                MaquinaLinea.BarraCopiadoLlenL2 = BarraCopiadaTB.Text;
                MaquinaLinea.DepCopiadoLlenL2 = DepositoCopiadoTB.Text;
                MaquinaLinea.FrioCopiadoLlenL2 = FrioCopiadoTB.Text;
            }
            if (MaquinaLinea.numlin == 3)
            {
                MaquinaLinea.BarraCopiadoLlenL3 = BarraCopiadaTB.Text;
                MaquinaLinea.DepCopiadoLlenL3 = DepositoCopiadoTB.Text;
                MaquinaLinea.FrioCopiadoLlenL3 = FrioCopiadoTB.Text;
            }
            if (MaquinaLinea.numlin == 5)
            {
                MaquinaLinea.BarraCopiadoLlenL5 = BarraCopiadaTB.Text;
                MaquinaLinea.DepCopiadoLlenL5 = DepositoCopiadoTB.Text;
                MaquinaLinea.FrioCopiadoLlenL5 = FrioCopiadoTB.Text;
            }
            BarraTB.Text = BarraCopiadaTB.Text;
            DepositoTB.Text = DepositoCopiadoTB.Text;
            FrioTB.Text = FrioCopiadoTB.Text;
        }

        private void CopiarBB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                MaquinaLinea.BarraCopiadoLlenL2 = BarraCopiadaTB.Text;
                MaquinaLinea.DepCopiadoLlenL2 = DepositoCopiadoTB.Text;
                MaquinaLinea.FrioCopiadoLlenL2 = FrioCopiadoTB.Text;
            }
            if (MaquinaLinea.numlin == 3)
            {
                MaquinaLinea.BarraCopiadoLlenL3 = BarraCopiadaTB.Text;
                MaquinaLinea.DepCopiadoLlenL3 = DepositoCopiadoTB.Text;
                MaquinaLinea.FrioCopiadoLlenL3 = FrioCopiadoTB.Text;
            }
            if (MaquinaLinea.numlin == 5)
            {
                MaquinaLinea.BarraCopiadoLlenL5 = BarraCopiadaTB.Text;
                MaquinaLinea.DepCopiadoLlenL5 = DepositoCopiadoTB.Text;
                MaquinaLinea.FrioCopiadoLlenL5 = FrioCopiadoTB.Text;
            }
            Barra2TB.Text = BarraCopiadaTB.Text;
            Deposito2TB.Text = DepositoCopiadoTB.Text;
            Frio2TB.Text = FrioCopiadoTB.Text;
        }

        private void BarraCopiadaTB_MouseClick(object sender, MouseEventArgs e)
        {

                Console.WriteLine("Index Barra: "+BarraCopiadaTB.TabIndex);         
                TextBox = BarraCopiadaTB;
                WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,BarraCopiadaTB);
      
        }

        private void DepositoCopiadoTB_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Index Dep: " + DepositoCopiadoTB.TabIndex);

                TextBox = DepositoCopiadoTB;
                WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,DepositoCopiadoTB);

        }

        private void FrioCopiadoTB_MouseClick(object sender, MouseEventArgs e)
        {
                
                TextBox = FrioCopiadoTB;
                WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,FrioCopiadoTB);
    
        }

        private void CopiaBotellasB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2) { NBotTB.Text = Properties.Settings.Default.BotellasAProducirLlenL2;}
            if (MaquinaLinea.numlin == 3) { NBotTB.Text = Properties.Settings.Default.BotellasAProducirLlenL3;}
            if (MaquinaLinea.numlin == 5) { NBotTB.Text = Properties.Settings.Default.BotellasAProducirLlenL5;}
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void NUEVO_Click(object sender, EventArgs e)
        {
            //NUEVO.ReadOnly = false;
            //puntero = "Turno";
            PanelRespuesta.Show();
           // BusTurnoTB.Text = "";
            Opcion1B.Text = "Lavado";
            Opcion2B.Text = "Lavado Especial";
            Opcion3B.Text = "Cambio formato";
        }

        private void Opcion1B_Click(object sender, EventArgs e)
        {
            nuevoTB.Text = Opcion1B.Text;
            PanelRespuesta.Hide();
        }

        private void Opcion2B_Click(object sender, EventArgs e)
        {
            nuevoTB.Text = Opcion2B.Text;
            PanelRespuesta.Hide();
        }

        private void Opcion3B_Click(object sender, EventArgs e)
        {
            nuevoTB.Text = Opcion3B.Text;
            PanelRespuesta.Hide();
        }
    }
}


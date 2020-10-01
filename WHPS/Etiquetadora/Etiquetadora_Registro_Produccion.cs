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

namespace WHPS.Etiquetadora
{
    public partial class Etiquetadora_Registro_Produccion : Form
    {
        TextBox TextBox;
        public Etiquetadora_Registro_Produccion()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void ExitB_Click(object sender, EventArgs e)
        {

            //Realizamos una "foto" de los datos que han sido introducidos
            if (MaquinaLinea.numlin == 2)
            {
                Properties.Settings.Default.DPLoteEtiqL2 = LoteTB.Text;
                Properties.Settings.Default.DPNBotellasEtiqL2 = NBotTB.Text;
                Properties.Settings.Default.DPHInicioEtiqL2 = HInicioTB.Text;
                Properties.Settings.Default.DPHFinEtiqL2 = HFinTB.Text;


                Properties.Settings.Default.Save();
            }
            if (MaquinaLinea.numlin == 3)
            {
                Properties.Settings.Default.DPLoteEtiqL3 = LoteTB.Text;
                Properties.Settings.Default.DPNBotellasEtiqL3 = NBotTB.Text;
                Properties.Settings.Default.DPHInicioEtiqL3 = HInicioTB.Text;
                Properties.Settings.Default.DPHFinEtiqL3 = HFinTB.Text;

                Properties.Settings.Default.Save();
            }
            if (MaquinaLinea.numlin == 5)
            {
                Properties.Settings.Default.DPLoteEtiqL5 = LoteTB.Text;
                Properties.Settings.Default.DPNBotellasEtiqL5 = NBotTB.Text;
                Properties.Settings.Default.DPHInicioEtiqL5 = HInicioTB.Text;
                Properties.Settings.Default.DPHFinEtiqL5 = HFinTB.Text;

                Properties.Settings.Default.Save();
            }

            MainEtiquetadora Form = new MainEtiquetadora();
            Hide();
            Form.Show();
        }
        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        private void Etiquetadora_Registro_Produccion_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:s");

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //Se oculta el teclado
            numberpad1.Visible = false;

            //Se rellenan los datos del equipo
            dateTB.Text = DateTime.Now.ToString("dd/MM/yyyy");
            respTB.Text = MaquinaLinea.Responsable;
            maqTB.Text = MaquinaLinea.MEtiquetadora;
            turnoTB.Text = Utilidades.ObtenerTurnoActual();

            //Se muestra los datos del LOTE
            DiaJulianoLB.Text = "Día Juliano: " + DateTime.Now.DayOfYear.ToString();
            AñoLB.Text = "Año: " + DateTime.Now.Year.ToString();
            LineaLB.Text = "Línea: L" + MaquinaLinea.numlin;

            //Indicamos si estamos en tiempo de preparación o no cambiando el color del boton
            if (HInicioTB.Text != "") LanzamientocargadoB.BackColor = Color.FromArgb(27, 33, 41);

            if (MaquinaLinea.numlin == 2)
            {
                //Rellenamos los datos obtenidos del lanzamiento
                OrdenTB.Text = Properties.Settings.Default.DPOrdenEtiqL2;
                ClienteTB.Text = Properties.Settings.Default.DPClienteEtiqL2;
                ProductoTB.Text = Properties.Settings.Default.DPProductoEtiqL2;
                GraduacionTB.Text = Properties.Settings.Default.DPGraduacionEtiqL2;
                BotellasAProducirLB.Text = "(Botellas a producir: " + Properties.Settings.Default.BotellasAProducirEtiqL2 + ")";

                //Rellenamos los registros que ya han sido guardados
                LoteTB.Text = Properties.Settings.Default.DPLoteEtiqL2;
                FormatoTB.Text = Properties.Settings.Default.DPFormatoEtiqL2;
                NBotTB.Text = Properties.Settings.Default.DPNBotellasEtiqL2;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEtiqL2;
                HFinTB.Text = Properties.Settings.Default.DPHFinEtiqL2;
                LoteCopiadoTB.Text = MaquinaLinea.LoteCopiadoEtiqL2;
                //Indicamos que muestre el boton verde cuando el tiempo de preparacion ha comenzado
                if (Properties.Settings.Default.DPHInicioCambioEtiqL2 != "" && HInicioTB.Text == "") LanzamientocargadoB.BackColor = Color.DarkSeaGreen;

            }
            if (MaquinaLinea.numlin == 3)
            {
                //Rellenamos los datos obtenidos del lanzamiento
                OrdenTB.Text = Properties.Settings.Default.DPOrdenEtiqL3;
                ClienteTB.Text = Properties.Settings.Default.DPClienteEtiqL3;
                ProductoTB.Text = Properties.Settings.Default.DPProductoEtiqL3;
                GraduacionTB.Text = Properties.Settings.Default.DPGraduacionEtiqL3;
                BotellasAProducirLB.Text = "(Botellas a producir: " + Properties.Settings.Default.BotellasAProducirEtiqL3 + ")";

                //Rellenamos los registros que ya han sido guardados
                LoteTB.Text = Properties.Settings.Default.DPLoteEtiqL3;
                FormatoTB.Text = Properties.Settings.Default.DPFormatoEtiqL3;
                NBotTB.Text = Properties.Settings.Default.DPNBotellasEtiqL3;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEtiqL3;
                HFinTB.Text = Properties.Settings.Default.DPHFinEtiqL3;
                LoteCopiadoTB.Text = MaquinaLinea.LoteCopiadoEtiqL3;
                //Indicamos que muestre el boton verde cuando el tiempo de preparacion ha comenzado
                if (Properties.Settings.Default.DPHInicioCambioEtiqL3 != "" && HInicioTB.Text == "") LanzamientocargadoB.BackColor = Color.DarkSeaGreen;
            }
            if (MaquinaLinea.numlin == 5)
            {
                //Rellenamos los datos obtenidos del lanzamiento
                OrdenTB.Text = Properties.Settings.Default.DPOrdenEtiqL5;
                ClienteTB.Text = Properties.Settings.Default.DPClienteEtiqL5;
                ProductoTB.Text = Properties.Settings.Default.DPProductoEtiqL5;
                GraduacionTB.Text = Properties.Settings.Default.DPGraduacionEtiqL5;
                BotellasAProducirLB.Text = "(Botellas a producir: " + Properties.Settings.Default.BotellasAProducirEtiqL5 + ")";

                //Rellenamos los registros que ya han sido guardados
                LoteTB.Text = Properties.Settings.Default.DPLoteEtiqL5;
                FormatoTB.Text = Properties.Settings.Default.DPFormatoEtiqL5;
                NBotTB.Text = Properties.Settings.Default.DPNBotellasEtiqL5;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEtiqL5;
                HFinTB.Text = Properties.Settings.Default.DPHFinEtiqL5;
                LoteCopiadoTB.Text = MaquinaLinea.LoteCopiadoEtiqL5;
                //Indicamos si estamos en tiempo de preparación o no cambiando el color del boton
                if (HInicioTB.Text != "") { LanzamientocargadoB.BackColor = Color.FromArgb(27, 33, 41); ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar; }
                else ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionInicio;
            }
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
            if (MaquinaLinea.StatusTeclado == true) Utilidades.EscribirTeclado(numberpad1, TextBox, null);
        }


        //Muestra el PDF donde viene como de ser el lote
        private void PDFLoteB_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaPDFLOTE);
        }

        //Copia el lote que este en el portafolio
        private void InsertarLoteB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2) MaquinaLinea.LoteCopiadoEtiqL2 = LoteCopiadoTB.Text;
            if (MaquinaLinea.numlin == 3) MaquinaLinea.LoteCopiadoEtiqL3 = LoteCopiadoTB.Text;
            if (MaquinaLinea.numlin == 5) MaquinaLinea.LoteCopiadoEtiqL5 = LoteCopiadoTB.Text;
            LoteTB.Text = LoteCopiadoTB.Text;
        }


        //Al hacer click en los textbox se mostrará un taclado para completar el formulario
        private void NBotTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(NBotTB);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = NBotTB;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }
        private void LoteTB_MouseClick(object sender, MouseEventArgs e)
        {
            LoteTB.Text = "";
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(LoteTB);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = LoteTB;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }
        private void LoteCopiadoTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(LoteCopiadoTB);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = LoteCopiadoTB;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }





        //El teclado manual realiza la accion al ser ocultado
        private void numberpad1_VisibleChanged(object sender, EventArgs e)
        {
            if (MaquinaLinea.StatusTeclado == true) Utilidades.EscribirTeclado(numberpad1, TextBox, null);
        }



        //Registrará el tiempo en el TB de cuya variable sea true
        private void LanzamientocargadoB_Click(object sender, EventArgs e)
        {
            if (HInicioTB.Text == "")
            {
                if (MaquinaLinea.numlin == 2 && Properties.Settings.Default.DPHInicioCambioEtiqL2 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioEtiqL2 = DateTime.Now.ToString("HH:mm:ss");
                    LanzamientocargadoB.BackColor = Color.DarkSeaGreen;
                }
                if (MaquinaLinea.numlin == 3 && Properties.Settings.Default.DPHInicioCambioEtiqL3 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioEtiqL3 = DateTime.Now.ToString("HH:mm:ss");
                    LanzamientocargadoB.BackColor = Color.DarkSeaGreen;
                }
                if (MaquinaLinea.numlin == 5 && Properties.Settings.Default.DPHInicioCambioEtiqL5 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioEtiqL5 = DateTime.Now.ToString("HH:mm:ss");
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
                if (Properties.Settings.Default.DPHInicioCambioEtiqL2 != "" && Properties.Settings.Default.DPHInicioEtiqL2 != "" && Properties.Settings.Default.DPHFinEtiqL2 == "")
                {
                    Properties.Settings.Default.DPHFinEtiqL2 = DateTime.Now.ToString("HH:mm:ss");
                    HFinTB.Text = Properties.Settings.Default.DPHFinEtiqL2;
                }
                //Puede ser que SI se haya iniciado la preparacion por que no sea necesario.
                //MessageBox.Show("prerp: " + Properties.Settings.Default.DPHInicioCambioEtiqL2 + "inicio: " + Properties.Settings.Default.DPHInicioEtiqL2 + "fin: " + Properties.Settings.Default.DPHFinEtiqL2);
                if (Properties.Settings.Default.DPHInicioCambioEtiqL2 != "" && Properties.Settings.Default.DPHInicioEtiqL2 == "" && Properties.Settings.Default.DPHFinEtiqL2 == "")
                {
                    Properties.Settings.Default.DPHInicioEtiqL2 = DateTime.Now.ToString("HH:mm:ss");
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioEtiqL2;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinEtiqL2 = "";
                }
                //Puede ser que NO se haya iniciado la preparacion por que no sea necesario.
                if (Properties.Settings.Default.DPHInicioCambioEtiqL2 == "" && Properties.Settings.Default.DPHInicioEtiqL2 == "" && Properties.Settings.Default.DPHFinEtiqL2 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioEtiqL2 = DateTime.Now.ToString("HH:mm:ss");
                    Properties.Settings.Default.DPHInicioEtiqL2 = Properties.Settings.Default.DPHInicioCambioEtiqL2;
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioEtiqL2;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinEtiqL2 = "";
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                //Puede ser que se haya iniciado la producción.
                if (Properties.Settings.Default.DPHInicioCambioEtiqL3 != "" && Properties.Settings.Default.DPHInicioEtiqL3 != "" && Properties.Settings.Default.DPHFinEtiqL3 == "")
                {
                    Properties.Settings.Default.DPHFinEtiqL3 = DateTime.Now.ToString("HH:mm:ss");
                    HFinTB.Text = Properties.Settings.Default.DPHFinEtiqL3;
                }
                //Puede ser que SI se haya iniciado la preparacion por que no sea necesario.
                //MessageBox.Show("prerp: " + Properties.Settings.Default.DPHInicioCambioEtiqL3 + "inicio: " + Properties.Settings.Default.DPHInicioEtiqL3 + "fin: " + Properties.Settings.Default.DPHFinEtiqL3);
                if (Properties.Settings.Default.DPHInicioCambioEtiqL3 != "" && Properties.Settings.Default.DPHInicioEtiqL3 == "" && Properties.Settings.Default.DPHFinEtiqL3 == "")
                {
                    Properties.Settings.Default.DPHInicioEtiqL3 = DateTime.Now.ToString("HH:mm:ss");
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioEtiqL3;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinEtiqL3 = "";
                }
                //Puede ser que NO se haya iniciado la preparacion por que no sea necesario.
                if (Properties.Settings.Default.DPHInicioCambioEtiqL3 == "" && Properties.Settings.Default.DPHInicioEtiqL3 == "" && Properties.Settings.Default.DPHFinEtiqL3 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioEtiqL3 = DateTime.Now.ToString("HH:mm:ss");
                    Properties.Settings.Default.DPHInicioEtiqL3 = Properties.Settings.Default.DPHInicioCambioEtiqL3;
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioEtiqL3;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinEtiqL3 = "";
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                //Puede ser que se haya iniciado la producción.
                if (Properties.Settings.Default.DPHInicioCambioEtiqL5 != "" && Properties.Settings.Default.DPHInicioEtiqL5 != "" && Properties.Settings.Default.DPHFinEtiqL5 == "")
                {
                    Properties.Settings.Default.DPHFinEtiqL5 = DateTime.Now.ToString("HH:mm:ss");
                    HFinTB.Text = Properties.Settings.Default.DPHFinEtiqL5;
                }
                //Puede ser que SI se haya iniciado la preparacion por que no sea necesario.
                //MessageBox.Show("prerp: " + Properties.Settings.Default.DPHInicioCambioEtiqL5 + "inicio: " + Properties.Settings.Default.DPHInicioEtiqL5 + "fin: " + Properties.Settings.Default.DPHFinEtiqL5);
                if (Properties.Settings.Default.DPHInicioCambioEtiqL5 != "" && Properties.Settings.Default.DPHInicioEtiqL5 == "" && Properties.Settings.Default.DPHFinEtiqL5 == "")
                {
                    Properties.Settings.Default.DPHInicioEtiqL5 = DateTime.Now.ToString("HH:mm:ss");
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioEtiqL5;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinEtiqL5 = "";
                }
                //Puede ser que NO se haya iniciado la preparacion por que no sea necesario.
                if (Properties.Settings.Default.DPHInicioCambioEtiqL5 == "" && Properties.Settings.Default.DPHInicioEtiqL5 == "" && Properties.Settings.Default.DPHFinEtiqL5 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioEtiqL5 = DateTime.Now.ToString("HH:mm:ss");
                    Properties.Settings.Default.DPHInicioEtiqL5 = Properties.Settings.Default.DPHInicioCambioEtiqL5;
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioEtiqL5;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinEtiqL5 = "";
                }
            }
            Properties.Settings.Default.Save();
        }
        //Botón que elimina toda la información del registro
        private void BorrarB_Click(object sender, EventArgs e)
        {
            LoteTB.Text = "";
            NBotTB.Text = "";
            HInicioTB.Text = "";
            HFinTB.Text = "";
            LanzamientocargadoB.BackColor = Color.FromArgb(27, 33, 41);
            if (MaquinaLinea.numlin == 2) Properties.Settings.Default.DPHInicioCambioEtiqL2 = "";
            if (MaquinaLinea.numlin == 3) Properties.Settings.Default.DPHInicioCambioEtiqL3 = "";
            if (MaquinaLinea.numlin == 5) Properties.Settings.Default.DPHInicioCambioEtiqL5 = "";
            Properties.Settings.Default.Save();
        }


        //Guardamos que la producción se ha terminado
        private void saveBot_Click(object sender, EventArgs e)
        {



            //Para poder guardar todos los campos deben estar cumplimentados
            if (OrdenTB.Text != "" && ClienteTB.Text != "" && ProductoTB.Text != "" && NBotTB.Text != "" && HInicioTB.Text != "" && HFinTB.Text != "")
            {
                //Cargamos las variables del cambio en variables globales
                if (MaquinaLinea.numlin == 2) MaquinaLinea.HInicioCambioEtiq = Properties.Settings.Default.DPHInicioCambioEtiqL2;
                if (MaquinaLinea.numlin == 3) MaquinaLinea.HInicioCambioEtiq = Properties.Settings.Default.DPHInicioCambioEtiqL3;
                if (MaquinaLinea.numlin == 5) MaquinaLinea.HInicioCambioEtiq = Properties.Settings.Default.DPHInicioCambioEtiqL5;

                List<string[]> listavalores = new List<string[]>();

                listavalores.Add(new string[2] { "Fecha", DateTime.Now.ToString("dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Hora", DateTime.Now.ToString("HH:mm:ss") });
                listavalores.Add(new string[2] { "FechaDB", DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Responsable", MaquinaLinea.Responsable });
                listavalores.Add(new string[2] { "Maquinista", MaquinaLinea.MDespaletizador });
                listavalores.Add(new string[2] { "Turno", turnoTB.Text });
                listavalores.Add(new string[2] { "Lote", LoteTB.Text });
                listavalores.Add(new string[2] { "Orden", OrdenTB.Text });
                listavalores.Add(new string[2] { "Producto", ProductoTB.Text });
                listavalores.Add(new string[2] { "Cliente", ClienteTB.Text });
                listavalores.Add(new string[2] { "Formato", FormatoTB.Text });
                listavalores.Add(new string[2] { "Graduacion", GraduacionTB.Text });
                listavalores.Add(new string[2] { "NBotellas", NBotTB.Text });
                listavalores.Add(new string[2] { "Inicio", HInicioTB.Text });
                listavalores.Add(new string[2] { "Fin", HFinTB.Text });
                listavalores.Add(new string[2] { "InicioCambio", MaquinaLinea.HInicioCambioEtiq });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileEtiquetadora, "Registro", listavalores, "Id");
                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show(Properties.Settings.Default.AvisoProblemaFichero);
                }
                else
                {
                    //Restablecemos los valores correspondientes
                    OrdenTB.Text = "";
                    ProductoTB.Text = "";
                    ClienteTB.Text = "";
                    FormatoTB.Text = "";
                    GraduacionTB.Text = "";

                    if (MaquinaLinea.numlin == 2)
                    {
                        Properties.Settings.Default.FilaSeleccionadaEtiqL2 = "";
                        Properties.Settings.Default.DPiDLanzEtiqL2 = "";
                        Properties.Settings.Default.DPLoteEtiqL2 = "";
                        Properties.Settings.Default.DPHInicioEtiqL2 = "";
                        Properties.Settings.Default.DPHFinEtiqL2 = "";
                        Properties.Settings.Default.DPHInicioCambioEtiqL2 = "";
                        Properties.Settings.Default.DPNBotellasEtiqL2 = "";

                    }
                    if (MaquinaLinea.numlin == 3)
                    {
                        Properties.Settings.Default.FilaSeleccionadaEtiqL3 = "";
                        Properties.Settings.Default.DPiDLanzEtiqL3 = "";
                        Properties.Settings.Default.DPLoteEtiqL3 = "";
                        Properties.Settings.Default.DPHInicioEtiqL3 = "";
                        Properties.Settings.Default.DPHFinEtiqL3 = "";
                        Properties.Settings.Default.DPHInicioCambioEtiqL3 = "";
                        Properties.Settings.Default.DPNBotellasEtiqL3 = "";

                    }
                    if (MaquinaLinea.numlin == 5)
                    {
                        Properties.Settings.Default.FilaSeleccionadaEtiqL5 = "";
                        Properties.Settings.Default.DPiDLanzEtiqL5 = "";
                        Properties.Settings.Default.DPLoteEtiqL5 = "";
                        Properties.Settings.Default.DPHInicioEtiqL5 = "";
                        Properties.Settings.Default.DPHFinEtiqL5 = "";
                        Properties.Settings.Default.DPHInicioCambioEtiqL5 = "";
                        Properties.Settings.Default.DPNBotellasEtiqL5 = "";
                    }
                    LanzamientocargadoB.BackColor = Color.FromArgb(27, 33, 41);
                    Properties.Settings.Default.Save();
                    //MessageBox.Show(salida);
                    MainEtiquetadora Form = new MainEtiquetadora();
                    Hide();
                    Form.Show();
                }
            }
            else
            {
                MessageBox.Show(Properties.Settings.Default.AvisoCampos);
            }
        }

        private void CopiaBotellasB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2) { NBotTB.Text = Properties.Settings.Default.BotellasAProducirEtiqL2; }
            if (MaquinaLinea.numlin == 3) { NBotTB.Text = Properties.Settings.Default.BotellasAProducirEtiqL3; }
            if (MaquinaLinea.numlin == 5) { NBotTB.Text = Properties.Settings.Default.BotellasAProducirEtiqL5; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.ProgramMenus;
using WHPS.Utiles;

namespace WHPS.Llenadora
{
    public partial class Llenadora_Control_Volumen : Form
    {
        //Declaramos las variable que iran registrando los valores en los diferentes textbox
        public string VolumenNominal = "";
        public string Graduacion = "";
        public decimal Temperatura;
        public decimal VolumenTeorico;
        public string VolumenMedido = "";
        public decimal CoefCorreccion;
        public decimal CapacidadReal;
        public decimal Error;
        public string Estado = "";
        public double N;
        TextBox TextBox;
        public Image EstadoVerde = Properties.Resources.LlenEstadoVerde;
        public Image EstadoRojo = Properties.Resources.LlenEstadoRojo;
        public Image EstadoNaranja = Properties.Resources.LlenEstadoNaranja;

        public Llenadora_Control_Volumen()
        {
            InitializeComponent();
        }


        //Volvemos a la pantalla anterior pulsando el boton back
        private void ExitB_Click(object sender, EventArgs e)
        {
            MainLlenadora Form = new MainLlenadora();
            Hide();
            Form.Show();
            GC.Collect();
        }

        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }


        private void Llenadora_Control_Volumen_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //Rellenamos fecha
            dateTB.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //Rellenamos responsable y maquinista
            maqTB.Text = MaquinaLinea.MLlenadora;

            //Rellenamos el turno - Identificando el turno
            turnoTB.Text = Utilidades.ObtenerTurnoActual();

            //Ocultamos el mensaje de aviso, aparecerá posteriormente si es necesario
            AvisoLB.Hide();

            if (MaquinaLinea.numlin == 2)
            {
                //Rellenamos los datos obtenidos del lanzamiento
                CapacidadTB.Text = Properties.Settings.Default.DPCapacidadLlenL2;
                GraduacionTB.Text = Properties.Settings.Default.DPGraduacionLlenL2;
                ProductoTB.Text = Properties.Settings.Default.DPProductoLlenL2;
                //Datos añadidos
                TemperaturaTB.Text = Properties.Settings.Default.TemperaturaContVolLlenL2;
                VolumenMedidoTB.Text = Properties.Settings.Default.VolumenContVolLlenL2;

                //Rellenamos los resultados obtenidos
                CompletarRegistros(1, Properties.Settings.Default.VolMedido1LlenL2, Properties.Settings.Default.Vol1LlenL2, Properties.Settings.Default.Error1LlenL2, Properties.Settings.Default.CapReal1LlenL2);
                CompletarRegistros(2, Properties.Settings.Default.VolMedido2LlenL2, Properties.Settings.Default.Vol2LlenL2, Properties.Settings.Default.Error2LlenL2, Properties.Settings.Default.CapReal2LlenL2);
                CompletarRegistros(3, Properties.Settings.Default.VolMedido3LlenL2, Properties.Settings.Default.Vol3LlenL2, Properties.Settings.Default.Error3LlenL2, Properties.Settings.Default.CapReal3LlenL2);
                CompletarRegistros(4, Properties.Settings.Default.VolMedido4LlenL2, Properties.Settings.Default.Vol4LlenL2, Properties.Settings.Default.Error4LlenL2, Properties.Settings.Default.CapReal4LlenL2);
                CompletarRegistros(5, Properties.Settings.Default.VolMedido5LlenL2, Properties.Settings.Default.Vol5LlenL2, Properties.Settings.Default.Error5LlenL2, Properties.Settings.Default.CapReal5LlenL2);
                CompletarRegistros(6, Properties.Settings.Default.VolMedido6LlenL2, Properties.Settings.Default.Vol6LlenL2, Properties.Settings.Default.Error6LlenL2, Properties.Settings.Default.CapReal6LlenL2);
                CompletarRegistros(7, Properties.Settings.Default.VolMedido7LlenL2, Properties.Settings.Default.Vol7LlenL2, Properties.Settings.Default.Error7LlenL2, Properties.Settings.Default.CapReal7LlenL2);
                CompletarRegistros(8, Properties.Settings.Default.VolMedido8LlenL2, Properties.Settings.Default.Vol8LlenL2, Properties.Settings.Default.Error8LlenL2, Properties.Settings.Default.CapReal8LlenL2);
                CompletarRegistros(9, Properties.Settings.Default.VolMedido9LlenL2, Properties.Settings.Default.Vol9LlenL2, Properties.Settings.Default.Error9LlenL2, Properties.Settings.Default.CapReal9LlenL2);
                CompletarRegistros(10, Properties.Settings.Default.VolMedido10LlenL2, Properties.Settings.Default.Vol10LlenL2, Properties.Settings.Default.Error10LlenL2, Properties.Settings.Default.CapReal10LlenL2);
                CompletarRegistros(11, Properties.Settings.Default.VolMedido11LlenL2, Properties.Settings.Default.Vol11LlenL2, Properties.Settings.Default.Error11LlenL2, Properties.Settings.Default.CapReal11LlenL2);
                CompletarRegistros(12, Properties.Settings.Default.VolMedido12LlenL2, Properties.Settings.Default.Vol12LlenL2, Properties.Settings.Default.Error12LlenL2, Properties.Settings.Default.CapReal12LlenL2);
                CompletarRegistros(13, Properties.Settings.Default.VolMedido13LlenL2, Properties.Settings.Default.Vol13LlenL2, Properties.Settings.Default.Error13LlenL2, Properties.Settings.Default.CapReal13LlenL2);
                CompletarRegistros(14, Properties.Settings.Default.VolMedido14LlenL2, Properties.Settings.Default.Vol14LlenL2, Properties.Settings.Default.Error14LlenL2, Properties.Settings.Default.CapReal14LlenL2);
                CompletarRegistros(15, Properties.Settings.Default.VolMedido15LlenL2, Properties.Settings.Default.Vol15LlenL2, Properties.Settings.Default.Error15LlenL2, Properties.Settings.Default.CapReal15LlenL2);
                CompletarRegistros(16, Properties.Settings.Default.VolMedido16LlenL2, Properties.Settings.Default.Vol16LlenL2, Properties.Settings.Default.Error16LlenL2, Properties.Settings.Default.CapReal16LlenL2);
                CompletarRegistros(17, Properties.Settings.Default.VolMedido17LlenL2, Properties.Settings.Default.Vol17LlenL2, Properties.Settings.Default.Error17LlenL2, Properties.Settings.Default.CapReal17LlenL2);
                CompletarRegistros(18, Properties.Settings.Default.VolMedido18LlenL2, Properties.Settings.Default.Vol18LlenL2, Properties.Settings.Default.Error18LlenL2, Properties.Settings.Default.CapReal18LlenL2);
                CompletarRegistros(19, Properties.Settings.Default.VolMedido19LlenL2, Properties.Settings.Default.Vol19LlenL2, Properties.Settings.Default.Error19LlenL2, Properties.Settings.Default.CapReal19LlenL2);
                CompletarRegistros(20, Properties.Settings.Default.VolMedido20LlenL2, Properties.Settings.Default.Vol20LlenL2, Properties.Settings.Default.Error20LlenL2, Properties.Settings.Default.CapReal20LlenL2);

            }
            if (MaquinaLinea.numlin == 3)
            {
                //Rellenamos los datos obtenidos del lanzamiento
                CapacidadTB.Text = Properties.Settings.Default.DPCapacidadLlenL3;
                GraduacionTB.Text = Properties.Settings.Default.DPGraduacionLlenL3;
                ProductoTB.Text = Properties.Settings.Default.DPProductoLlenL3;
                //Datos añadidos
                TemperaturaTB.Text = Properties.Settings.Default.TemperaturaContVolLlenL3;
                VolumenMedidoTB.Text = Properties.Settings.Default.VolumenContVolLlenL3;
                //Rellenamos los resultados obtenidos
                CompletarRegistros(1, Properties.Settings.Default.VolMedido1LlenL3, Properties.Settings.Default.Vol1LlenL3, Properties.Settings.Default.Error1LlenL3, Properties.Settings.Default.CapReal1LlenL3);
                CompletarRegistros(2, Properties.Settings.Default.VolMedido2LlenL3, Properties.Settings.Default.Vol2LlenL3, Properties.Settings.Default.Error2LlenL3, Properties.Settings.Default.CapReal2LlenL3);
                CompletarRegistros(3, Properties.Settings.Default.VolMedido3LlenL3, Properties.Settings.Default.Vol3LlenL3, Properties.Settings.Default.Error3LlenL3, Properties.Settings.Default.CapReal3LlenL3);
                CompletarRegistros(4, Properties.Settings.Default.VolMedido4LlenL3, Properties.Settings.Default.Vol4LlenL3, Properties.Settings.Default.Error4LlenL3, Properties.Settings.Default.CapReal4LlenL3);
                CompletarRegistros(5, Properties.Settings.Default.VolMedido5LlenL3, Properties.Settings.Default.Vol5LlenL3, Properties.Settings.Default.Error5LlenL3, Properties.Settings.Default.CapReal5LlenL3);
                CompletarRegistros(6, Properties.Settings.Default.VolMedido6LlenL3, Properties.Settings.Default.Vol6LlenL3, Properties.Settings.Default.Error6LlenL3, Properties.Settings.Default.CapReal6LlenL3);
                CompletarRegistros(7, Properties.Settings.Default.VolMedido7LlenL3, Properties.Settings.Default.Vol7LlenL3, Properties.Settings.Default.Error7LlenL3, Properties.Settings.Default.CapReal7LlenL3);
                CompletarRegistros(8, Properties.Settings.Default.VolMedido8LlenL3, Properties.Settings.Default.Vol8LlenL3, Properties.Settings.Default.Error8LlenL3, Properties.Settings.Default.CapReal8LlenL3);
                CompletarRegistros(9, Properties.Settings.Default.VolMedido9LlenL3, Properties.Settings.Default.Vol9LlenL3, Properties.Settings.Default.Error9LlenL3, Properties.Settings.Default.CapReal9LlenL3);
                CompletarRegistros(10, Properties.Settings.Default.VolMedido10LlenL3, Properties.Settings.Default.Vol10LlenL3, Properties.Settings.Default.Error10LlenL3, Properties.Settings.Default.CapReal10LlenL3);
                CompletarRegistros(11, Properties.Settings.Default.VolMedido11LlenL3, Properties.Settings.Default.Vol11LlenL3, Properties.Settings.Default.Error11LlenL3, Properties.Settings.Default.CapReal11LlenL3);
                CompletarRegistros(12, Properties.Settings.Default.VolMedido12LlenL3, Properties.Settings.Default.Vol12LlenL3, Properties.Settings.Default.Error12LlenL3, Properties.Settings.Default.CapReal12LlenL3);
                CompletarRegistros(13, Properties.Settings.Default.VolMedido13LlenL3, Properties.Settings.Default.Vol13LlenL3, Properties.Settings.Default.Error13LlenL3, Properties.Settings.Default.CapReal13LlenL3);
                CompletarRegistros(14, Properties.Settings.Default.VolMedido14LlenL3, Properties.Settings.Default.Vol14LlenL3, Properties.Settings.Default.Error14LlenL3, Properties.Settings.Default.CapReal14LlenL3);
                CompletarRegistros(15, Properties.Settings.Default.VolMedido15LlenL3, Properties.Settings.Default.Vol15LlenL3, Properties.Settings.Default.Error15LlenL3, Properties.Settings.Default.CapReal15LlenL3);
                CompletarRegistros(16, Properties.Settings.Default.VolMedido16LlenL3, Properties.Settings.Default.Vol16LlenL3, Properties.Settings.Default.Error16LlenL3, Properties.Settings.Default.CapReal16LlenL3);
                CompletarRegistros(17, Properties.Settings.Default.VolMedido17LlenL3, Properties.Settings.Default.Vol17LlenL3, Properties.Settings.Default.Error17LlenL3, Properties.Settings.Default.CapReal17LlenL3);
                CompletarRegistros(18, Properties.Settings.Default.VolMedido18LlenL3, Properties.Settings.Default.Vol18LlenL3, Properties.Settings.Default.Error18LlenL3, Properties.Settings.Default.CapReal18LlenL3);
                CompletarRegistros(19, Properties.Settings.Default.VolMedido19LlenL3, Properties.Settings.Default.Vol19LlenL3, Properties.Settings.Default.Error19LlenL3, Properties.Settings.Default.CapReal19LlenL3);
                CompletarRegistros(20, Properties.Settings.Default.VolMedido20LlenL3, Properties.Settings.Default.Vol20LlenL3, Properties.Settings.Default.Error20LlenL3, Properties.Settings.Default.CapReal20LlenL3);

            }

            if (MaquinaLinea.numlin == 5)
            {
                //Rellenamos los datos obtenidos del lanzamiento
                CapacidadTB.Text = Properties.Settings.Default.DPCapacidadLlenL5;
                GraduacionTB.Text = Properties.Settings.Default.DPGraduacionLlenL5;
                ProductoTB.Text = Properties.Settings.Default.DPProductoLlenL5;
                //Datos añadidos
                TemperaturaTB.Text = Properties.Settings.Default.TemperaturaContVolLlenL5;
                VolumenMedidoTB.Text = Properties.Settings.Default.VolumenContVolLlenL5;
                //Rellenamos los resultados obtenidos
                CompletarRegistros(1, Properties.Settings.Default.VolMedido1LlenL5, Properties.Settings.Default.Vol1LlenL5, Properties.Settings.Default.Error1LlenL5, Properties.Settings.Default.CapReal1LlenL5);
                CompletarRegistros(2, Properties.Settings.Default.VolMedido2LlenL5, Properties.Settings.Default.Vol2LlenL5, Properties.Settings.Default.Error2LlenL5, Properties.Settings.Default.CapReal2LlenL5);
                CompletarRegistros(3, Properties.Settings.Default.VolMedido3LlenL5, Properties.Settings.Default.Vol3LlenL5, Properties.Settings.Default.Error3LlenL5, Properties.Settings.Default.CapReal3LlenL5);
                CompletarRegistros(4, Properties.Settings.Default.VolMedido4LlenL5, Properties.Settings.Default.Vol4LlenL5, Properties.Settings.Default.Error4LlenL5, Properties.Settings.Default.CapReal4LlenL5);
                CompletarRegistros(5, Properties.Settings.Default.VolMedido5LlenL5, Properties.Settings.Default.Vol5LlenL5, Properties.Settings.Default.Error5LlenL5, Properties.Settings.Default.CapReal5LlenL5);
                CompletarRegistros(6, Properties.Settings.Default.VolMedido6LlenL5, Properties.Settings.Default.Vol6LlenL5, Properties.Settings.Default.Error6LlenL5, Properties.Settings.Default.CapReal6LlenL5);
                CompletarRegistros(7, Properties.Settings.Default.VolMedido7LlenL5, Properties.Settings.Default.Vol7LlenL5, Properties.Settings.Default.Error7LlenL5, Properties.Settings.Default.CapReal7LlenL5);
                CompletarRegistros(8, Properties.Settings.Default.VolMedido8LlenL5, Properties.Settings.Default.Vol8LlenL5, Properties.Settings.Default.Error8LlenL5, Properties.Settings.Default.CapReal8LlenL5);
                CompletarRegistros(9, Properties.Settings.Default.VolMedido9LlenL5, Properties.Settings.Default.Vol9LlenL5, Properties.Settings.Default.Error9LlenL5, Properties.Settings.Default.CapReal9LlenL5);
                CompletarRegistros(10, Properties.Settings.Default.VolMedido10LlenL5, Properties.Settings.Default.Vol10LlenL5, Properties.Settings.Default.Error10LlenL5, Properties.Settings.Default.CapReal10LlenL5);
                CompletarRegistros(11, Properties.Settings.Default.VolMedido11LlenL5, Properties.Settings.Default.Vol11LlenL5, Properties.Settings.Default.Error11LlenL5, Properties.Settings.Default.CapReal11LlenL5);
                CompletarRegistros(12, Properties.Settings.Default.VolMedido12LlenL5, Properties.Settings.Default.Vol12LlenL5, Properties.Settings.Default.Error12LlenL5, Properties.Settings.Default.CapReal12LlenL5);
                CompletarRegistros(13, Properties.Settings.Default.VolMedido13LlenL5, Properties.Settings.Default.Vol13LlenL5, Properties.Settings.Default.Error13LlenL5, Properties.Settings.Default.CapReal13LlenL5);
                CompletarRegistros(14, Properties.Settings.Default.VolMedido14LlenL5, Properties.Settings.Default.Vol14LlenL5, Properties.Settings.Default.Error14LlenL5, Properties.Settings.Default.CapReal14LlenL5);
                CompletarRegistros(15, Properties.Settings.Default.VolMedido15LlenL5, Properties.Settings.Default.Vol15LlenL5, Properties.Settings.Default.Error15LlenL5, Properties.Settings.Default.CapReal15LlenL5);
                CompletarRegistros(16, Properties.Settings.Default.VolMedido16LlenL5, Properties.Settings.Default.Vol16LlenL5, Properties.Settings.Default.Error16LlenL5, Properties.Settings.Default.CapReal16LlenL5);
                CompletarRegistros(17, Properties.Settings.Default.VolMedido17LlenL5, Properties.Settings.Default.Vol17LlenL5, Properties.Settings.Default.Error17LlenL5, Properties.Settings.Default.CapReal17LlenL5);
                CompletarRegistros(18, Properties.Settings.Default.VolMedido18LlenL5, Properties.Settings.Default.Vol18LlenL5, Properties.Settings.Default.Error18LlenL5, Properties.Settings.Default.CapReal18LlenL5);
                CompletarRegistros(19, Properties.Settings.Default.VolMedido19LlenL5, Properties.Settings.Default.Vol19LlenL5, Properties.Settings.Default.Error19LlenL5, Properties.Settings.Default.CapReal19LlenL5);
                CompletarRegistros(20, Properties.Settings.Default.VolMedido20LlenL5, Properties.Settings.Default.Vol20LlenL5, Properties.Settings.Default.Error20LlenL5, Properties.Settings.Default.CapReal20LlenL5);

            }
            if (VolTeorico20TB.Text != "")
            {
                //En el momento que todos los campos esten completados realizamos los cálculos
                MediaTB.Text = Convert.ToString((Convert.ToDecimal(CapReal1TB.Text) + Convert.ToDecimal(CapReal2TB.Text) + Convert.ToDecimal(CapReal3TB.Text) + Convert.ToDecimal(CapReal4TB.Text) + Convert.ToDecimal(CapReal5TB.Text) + Convert.ToDecimal(CapReal6TB.Text) + Convert.ToDecimal(CapReal7TB.Text) + Convert.ToDecimal(CapReal8TB.Text) + Convert.ToDecimal(CapReal9TB.Text) + Convert.ToDecimal(CapReal10TB.Text) + Convert.ToDecimal(CapReal11TB.Text) + Convert.ToDecimal(CapReal12TB.Text) + Convert.ToDecimal(CapReal13TB.Text) + Convert.ToDecimal(CapReal14TB.Text) + Convert.ToDecimal(CapReal15TB.Text) + Convert.ToDecimal(CapReal16TB.Text) + Convert.ToDecimal(CapReal17TB.Text) + Convert.ToDecimal(CapReal18TB.Text) + Convert.ToDecimal(CapReal19TB.Text) + Convert.ToDecimal(CapReal20TB.Text)) / 20);
                VarianzaTB.Text = Convert.ToString((((Convert.ToDecimal(CapReal1TB.Text) * Convert.ToDecimal(CapReal1TB.Text)) + (Convert.ToDecimal(CapReal2TB.Text) * Convert.ToDecimal(CapReal2TB.Text)) + (Convert.ToDecimal(CapReal3TB.Text) * Convert.ToDecimal(CapReal3TB.Text)) + (Convert.ToDecimal(CapReal4TB.Text) * Convert.ToDecimal(CapReal4TB.Text)) + (Convert.ToDecimal(CapReal5TB.Text) * Convert.ToDecimal(CapReal5TB.Text)) + (Convert.ToDecimal(CapReal6TB.Text) * Convert.ToDecimal(CapReal6TB.Text)) + (Convert.ToDecimal(CapReal7TB.Text) * Convert.ToDecimal(CapReal7TB.Text)) + (Convert.ToDecimal(CapReal8TB.Text) * Convert.ToDecimal(CapReal8TB.Text)) + (Convert.ToDecimal(CapReal9TB.Text) * Convert.ToDecimal(CapReal9TB.Text)) + (Convert.ToDecimal(CapReal10TB.Text) * Convert.ToDecimal(CapReal10TB.Text)) + (Convert.ToDecimal(CapReal11TB.Text) * Convert.ToDecimal(CapReal11TB.Text)) + (Convert.ToDecimal(CapReal12TB.Text) * Convert.ToDecimal(CapReal12TB.Text)) + (Convert.ToDecimal(CapReal13TB.Text) * Convert.ToDecimal(CapReal13TB.Text)) + (Convert.ToDecimal(CapReal14TB.Text) * Convert.ToDecimal(CapReal14TB.Text)) + (Convert.ToDecimal(CapReal15TB.Text) * Convert.ToDecimal(CapReal15TB.Text)) + (Convert.ToDecimal(CapReal16TB.Text) * Convert.ToDecimal(CapReal16TB.Text)) + (Convert.ToDecimal(CapReal17TB.Text) * Convert.ToDecimal(CapReal17TB.Text)) + (Convert.ToDecimal(CapReal18TB.Text) * Convert.ToDecimal(CapReal18TB.Text)) + (Convert.ToDecimal(CapReal19TB.Text) * Convert.ToDecimal(CapReal19TB.Text)) + (Convert.ToDecimal(CapReal20TB.Text) * Convert.ToDecimal(CapReal20TB.Text))) / 20) - (Convert.ToDecimal(MediaTB.Text) * Convert.ToDecimal(MediaTB.Text)));
                DesviacionTipicaTB.Text = Convert.ToString(Math.Sqrt(Convert.ToDouble(VarianzaTB.Text)));
                RealDecretoTB.Text = Convert.ToString((Convert.ToDecimal(CapacidadTB.Text) - ((Convert.ToDecimal(DesviacionTipicaTB.Text) * 64) / 100)));
                if (Convert.ToDecimal(MediaTB.Text) >= Convert.ToDecimal(RealDecretoTB.Text))
                {
                    BOEn266PB.BackgroundImage = EstadoVerde;
                }
                if (Convert.ToDecimal(MediaTB.Text) < Convert.ToDecimal(RealDecretoTB.Text))
                {
                    BOEn266PB.BackgroundImage = EstadoRojo;
                }

                //Mostramos el mensaje de aviso para que no olviden guardar
                AvisoLB.Show();
            }
        }

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


        private void TemperaturaTB_MouseClick(object sender, MouseEventArgs e)
        {
            WHPS.Utiles.Numberpad2.AbrirCalculadora(TemperaturaTB);
        }

        private void VolumenTB_MouseClick(object sender, MouseEventArgs e)
        {
            WHPS.Utiles.Numberpad2.AbrirCalculadora(VolumenMedidoTB);

        }

        private void SumaB_Click(object sender, EventArgs e)
        {
            if (TemperaturaTB.Text != "" && VolumenMedidoTB.Text != "" && GraduacionTB.Text != "" && ProductoTB.Text != "")
            {
                Temperatura = Math.Round(Convert.ToDecimal(TemperaturaTB.Text), 0);
                if (Temperatura <= 30)
                {
                    try
                    {
                        VolumenNominal = CapacidadTB.Text;
                        Graduacion = GraduacionTB.Text;

                        //Dada la temperatura, la graduacon y la capacidad o volumen nominal obtenemos el volumen teorico del liquido en una serie de tablas
                        Datos_Volumen prasing = new Datos_Volumen();
                        prasing = Apps_Llenadora.ParsingTablasVolumen(VolumenNominal, Graduacion, Convert.ToString(Temperatura));

                        //Rellenamos todos los datos que han sido identificados
                        if (prasing.Volumen != "")
                        {
                            VolumenTeorico = Math.Round(Convert.ToDecimal(prasing.Volumen), 2);
                            VolumenTeoricoTB.Text = Convert.ToString(VolumenTeorico);


                            //CoefCorreccion = VolumenTeorico/VolumenNominal
                            CoefCorreccion = Convert.ToDecimal(VolumenNominal) / Convert.ToDecimal(VolumenTeorico);

                            //Valor introducido por los operarios
                            VolumenMedido = VolumenMedidoTB.Text;

                            //CapacidadReal = VolumenMedido * CoefCorreccion
                            CapacidadReal = Math.Round(Convert.ToDecimal(VolumenMedido) * CoefCorreccion, 2);
                            CapacidadRealTB.Text = Convert.ToString(CapacidadReal);


                            Error = Math.Round(Convert.ToDecimal(VolumenTeorico) - Convert.ToDecimal(VolumenMedido), 2);
                            ErrorTB.Text = Convert.ToString(Error);
                        }
                        Datos_Volumen Color = new Datos_Volumen();
                        Color = Apps_Llenadora.ParsingEstadoVolumen(Error, VolumenNominal);

                        if (Color.estado == "Verde")
                        {
                            EstadoPB.BackgroundImage = EstadoVerde;
                        }
                        if (Color.estado == "Naranja")
                        {
                            EstadoPB.BackgroundImage = EstadoNaranja;
                        }
                        if (Color.estado == "Rojo")
                        {
                            EstadoPB.BackgroundImage = EstadoRojo;
                        }



                        if (VolTeorico20TB.Text == "" && VolTeorico19TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 20, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(20, Properties.Settings.Default.VolMedido20LlenL2, Properties.Settings.Default.Vol20LlenL2, Properties.Settings.Default.Error20LlenL2, Properties.Settings.Default.CapReal20LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(20, Properties.Settings.Default.VolMedido20LlenL3, Properties.Settings.Default.Vol20LlenL3, Properties.Settings.Default.Error20LlenL3, Properties.Settings.Default.CapReal20LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(20, Properties.Settings.Default.VolMedido20LlenL5, Properties.Settings.Default.Vol20LlenL5, Properties.Settings.Default.Error20LlenL5, Properties.Settings.Default.CapReal20LlenL5);
                        }
                        if (VolTeorico19TB.Text == "" && VolTeorico18TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 19, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(19, Properties.Settings.Default.VolMedido19LlenL2, Properties.Settings.Default.Vol19LlenL2, Properties.Settings.Default.Error19LlenL2, Properties.Settings.Default.CapReal19LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(19, Properties.Settings.Default.VolMedido19LlenL3, Properties.Settings.Default.Vol19LlenL3, Properties.Settings.Default.Error19LlenL3, Properties.Settings.Default.CapReal19LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(19, Properties.Settings.Default.VolMedido19LlenL5, Properties.Settings.Default.Vol19LlenL5, Properties.Settings.Default.Error19LlenL5, Properties.Settings.Default.CapReal19LlenL5);
                        }


                        if (VolTeorico18TB.Text == "" && VolTeorico17TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 18, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(18, Properties.Settings.Default.VolMedido18LlenL2, Properties.Settings.Default.Vol18LlenL2, Properties.Settings.Default.Error18LlenL2, Properties.Settings.Default.CapReal18LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(18, Properties.Settings.Default.VolMedido18LlenL3, Properties.Settings.Default.Vol18LlenL3, Properties.Settings.Default.Error18LlenL3, Properties.Settings.Default.CapReal18LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(18, Properties.Settings.Default.VolMedido18LlenL5, Properties.Settings.Default.Vol18LlenL5, Properties.Settings.Default.Error18LlenL5, Properties.Settings.Default.CapReal18LlenL5);
                        }
                        if (VolTeorico17TB.Text == "" && VolTeorico16TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 17, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(17, Properties.Settings.Default.VolMedido17LlenL2, Properties.Settings.Default.Vol17LlenL2, Properties.Settings.Default.Error17LlenL2, Properties.Settings.Default.CapReal17LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(17, Properties.Settings.Default.VolMedido17LlenL3, Properties.Settings.Default.Vol17LlenL3, Properties.Settings.Default.Error17LlenL3, Properties.Settings.Default.CapReal17LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(17, Properties.Settings.Default.VolMedido17LlenL5, Properties.Settings.Default.Vol17LlenL5, Properties.Settings.Default.Error17LlenL5, Properties.Settings.Default.CapReal17LlenL5);
                        }
                        if (VolTeorico16TB.Text == "" && VolTeorico15TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 16, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(16, Properties.Settings.Default.VolMedido16LlenL2, Properties.Settings.Default.Vol16LlenL2, Properties.Settings.Default.Error16LlenL2, Properties.Settings.Default.CapReal16LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(16, Properties.Settings.Default.VolMedido16LlenL3, Properties.Settings.Default.Vol16LlenL3, Properties.Settings.Default.Error16LlenL3, Properties.Settings.Default.CapReal16LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(16, Properties.Settings.Default.VolMedido16LlenL5, Properties.Settings.Default.Vol16LlenL5, Properties.Settings.Default.Error16LlenL5, Properties.Settings.Default.CapReal16LlenL5);
                        }

                        if (VolTeorico15TB.Text == "" && VolTeorico14TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 15, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(15, Properties.Settings.Default.VolMedido15LlenL2, Properties.Settings.Default.Vol15LlenL2, Properties.Settings.Default.Error15LlenL2, Properties.Settings.Default.CapReal15LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(15, Properties.Settings.Default.VolMedido15LlenL3, Properties.Settings.Default.Vol15LlenL3, Properties.Settings.Default.Error15LlenL3, Properties.Settings.Default.CapReal15LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(15, Properties.Settings.Default.VolMedido15LlenL5, Properties.Settings.Default.Vol15LlenL5, Properties.Settings.Default.Error15LlenL5, Properties.Settings.Default.CapReal15LlenL5);
                        }
                        if (VolTeorico14TB.Text == "" && VolTeorico13TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 14, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(14, Properties.Settings.Default.VolMedido14LlenL2, Properties.Settings.Default.Vol14LlenL2, Properties.Settings.Default.Error14LlenL2, Properties.Settings.Default.CapReal14LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(14, Properties.Settings.Default.VolMedido14LlenL3, Properties.Settings.Default.Vol14LlenL3, Properties.Settings.Default.Error14LlenL3, Properties.Settings.Default.CapReal14LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(14, Properties.Settings.Default.VolMedido14LlenL5, Properties.Settings.Default.Vol14LlenL5, Properties.Settings.Default.Error14LlenL5, Properties.Settings.Default.CapReal14LlenL5);
                        }
                        if (VolTeorico13TB.Text == "" && VolTeorico12TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 13, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(13, Properties.Settings.Default.VolMedido13LlenL2, Properties.Settings.Default.Vol13LlenL2, Properties.Settings.Default.Error13LlenL2, Properties.Settings.Default.CapReal13LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(13, Properties.Settings.Default.VolMedido13LlenL3, Properties.Settings.Default.Vol13LlenL3, Properties.Settings.Default.Error13LlenL3, Properties.Settings.Default.CapReal13LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(13, Properties.Settings.Default.VolMedido13LlenL5, Properties.Settings.Default.Vol13LlenL5, Properties.Settings.Default.Error13LlenL5, Properties.Settings.Default.CapReal13LlenL5);
                        }
                        if (VolTeorico12TB.Text == "" && VolTeorico11TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 12, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(12, Properties.Settings.Default.VolMedido12LlenL2, Properties.Settings.Default.Vol12LlenL2, Properties.Settings.Default.Error12LlenL2, Properties.Settings.Default.CapReal12LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(12, Properties.Settings.Default.VolMedido12LlenL3, Properties.Settings.Default.Vol12LlenL3, Properties.Settings.Default.Error12LlenL3, Properties.Settings.Default.CapReal12LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(12, Properties.Settings.Default.VolMedido12LlenL5, Properties.Settings.Default.Vol12LlenL5, Properties.Settings.Default.Error12LlenL5, Properties.Settings.Default.CapReal12LlenL5);
                        }
                        if (VolTeorico11TB.Text == "" && VolTeorico10TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 11, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(11, Properties.Settings.Default.VolMedido11LlenL2, Properties.Settings.Default.Vol11LlenL2, Properties.Settings.Default.Error11LlenL2, Properties.Settings.Default.CapReal11LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(11, Properties.Settings.Default.VolMedido11LlenL3, Properties.Settings.Default.Vol11LlenL3, Properties.Settings.Default.Error11LlenL3, Properties.Settings.Default.CapReal11LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(11, Properties.Settings.Default.VolMedido11LlenL5, Properties.Settings.Default.Vol11LlenL5, Properties.Settings.Default.Error11LlenL5, Properties.Settings.Default.CapReal11LlenL5);
                        }
                        if (VolTeorico10TB.Text == "" && VolTeorico9TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 10, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(10, Properties.Settings.Default.VolMedido10LlenL2, Properties.Settings.Default.Vol10LlenL2, Properties.Settings.Default.Error10LlenL2, Properties.Settings.Default.CapReal10LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(10, Properties.Settings.Default.VolMedido10LlenL3, Properties.Settings.Default.Vol10LlenL3, Properties.Settings.Default.Error10LlenL3, Properties.Settings.Default.CapReal10LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(10, Properties.Settings.Default.VolMedido10LlenL5, Properties.Settings.Default.Vol10LlenL5, Properties.Settings.Default.Error10LlenL5, Properties.Settings.Default.CapReal10LlenL5);
                        }
                        if (VolTeorico9TB.Text == "" && VolTeorico8TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 9, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(9, Properties.Settings.Default.VolMedido9LlenL2, Properties.Settings.Default.Vol9LlenL2, Properties.Settings.Default.Error9LlenL2, Properties.Settings.Default.CapReal9LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(9, Properties.Settings.Default.VolMedido9LlenL3, Properties.Settings.Default.Vol9LlenL3, Properties.Settings.Default.Error9LlenL3, Properties.Settings.Default.CapReal9LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(9, Properties.Settings.Default.VolMedido9LlenL5, Properties.Settings.Default.Vol9LlenL5, Properties.Settings.Default.Error9LlenL5, Properties.Settings.Default.CapReal9LlenL5);
                        }
                        if (VolTeorico8TB.Text == "" && VolTeorico7TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 8, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(8, Properties.Settings.Default.VolMedido8LlenL2, Properties.Settings.Default.Vol8LlenL2, Properties.Settings.Default.Error8LlenL2, Properties.Settings.Default.CapReal8LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(8, Properties.Settings.Default.VolMedido8LlenL3, Properties.Settings.Default.Vol8LlenL3, Properties.Settings.Default.Error8LlenL3, Properties.Settings.Default.CapReal8LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(8, Properties.Settings.Default.VolMedido8LlenL5, Properties.Settings.Default.Vol8LlenL5, Properties.Settings.Default.Error8LlenL5, Properties.Settings.Default.CapReal8LlenL5);
                        }
                        if (VolTeorico7TB.Text == "" && VolTeorico6TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 7, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(7, Properties.Settings.Default.VolMedido7LlenL2, Properties.Settings.Default.Vol7LlenL2, Properties.Settings.Default.Error7LlenL2, Properties.Settings.Default.CapReal7LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(7, Properties.Settings.Default.VolMedido7LlenL3, Properties.Settings.Default.Vol7LlenL3, Properties.Settings.Default.Error7LlenL3, Properties.Settings.Default.CapReal7LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(7, Properties.Settings.Default.VolMedido7LlenL5, Properties.Settings.Default.Vol7LlenL5, Properties.Settings.Default.Error7LlenL5, Properties.Settings.Default.CapReal7LlenL5);
                        }
                        if (VolTeorico6TB.Text == "" && VolTeorico5TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 6, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(6, Properties.Settings.Default.VolMedido6LlenL2, Properties.Settings.Default.Vol6LlenL2, Properties.Settings.Default.Error6LlenL2, Properties.Settings.Default.CapReal6LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(6, Properties.Settings.Default.VolMedido6LlenL3, Properties.Settings.Default.Vol6LlenL3, Properties.Settings.Default.Error6LlenL3, Properties.Settings.Default.CapReal6LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(6, Properties.Settings.Default.VolMedido6LlenL5, Properties.Settings.Default.Vol6LlenL5, Properties.Settings.Default.Error6LlenL5, Properties.Settings.Default.CapReal6LlenL5);
                        }
                        if (VolTeorico5TB.Text == "" && VolTeorico4TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 5, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(5, Properties.Settings.Default.VolMedido5LlenL2, Properties.Settings.Default.Vol5LlenL2, Properties.Settings.Default.Error5LlenL2, Properties.Settings.Default.CapReal5LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(5, Properties.Settings.Default.VolMedido5LlenL3, Properties.Settings.Default.Vol5LlenL3, Properties.Settings.Default.Error5LlenL3, Properties.Settings.Default.CapReal5LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(5, Properties.Settings.Default.VolMedido5LlenL5, Properties.Settings.Default.Vol5LlenL5, Properties.Settings.Default.Error5LlenL5, Properties.Settings.Default.CapReal5LlenL5);
                        }
                        if (VolTeorico4TB.Text == "" && VolTeorico3TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 4, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(4, Properties.Settings.Default.VolMedido4LlenL2, Properties.Settings.Default.Vol4LlenL2, Properties.Settings.Default.Error4LlenL2, Properties.Settings.Default.CapReal4LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(4, Properties.Settings.Default.VolMedido4LlenL3, Properties.Settings.Default.Vol4LlenL3, Properties.Settings.Default.Error4LlenL3, Properties.Settings.Default.CapReal4LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(4, Properties.Settings.Default.VolMedido4LlenL5, Properties.Settings.Default.Vol4LlenL5, Properties.Settings.Default.Error4LlenL5, Properties.Settings.Default.CapReal4LlenL5);
                        }
                        if (VolTeorico3TB.Text == "" && VolTeorico2TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 3, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(3, Properties.Settings.Default.VolMedido3LlenL2, Properties.Settings.Default.Vol3LlenL2, Properties.Settings.Default.Error3LlenL2, Properties.Settings.Default.CapReal3LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(3, Properties.Settings.Default.VolMedido3LlenL3, Properties.Settings.Default.Vol3LlenL3, Properties.Settings.Default.Error3LlenL3, Properties.Settings.Default.CapReal3LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(3, Properties.Settings.Default.VolMedido3LlenL5, Properties.Settings.Default.Vol3LlenL5, Properties.Settings.Default.Error3LlenL5, Properties.Settings.Default.CapReal3LlenL5);
                        }
                        if (VolTeorico2TB.Text == "" && VolTeorico1TB.Text != "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 2, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(2, Properties.Settings.Default.VolMedido2LlenL2, Properties.Settings.Default.Vol2LlenL2, Properties.Settings.Default.Error2LlenL2, Properties.Settings.Default.CapReal2LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(2, Properties.Settings.Default.VolMedido2LlenL3, Properties.Settings.Default.Vol2LlenL3, Properties.Settings.Default.Error2LlenL3, Properties.Settings.Default.CapReal2LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(2, Properties.Settings.Default.VolMedido2LlenL5, Properties.Settings.Default.Vol2LlenL5, Properties.Settings.Default.Error2LlenL5, Properties.Settings.Default.CapReal2LlenL5);
                        }
                        if (VolTeorico1TB.Text == "")
                        {
                            EstablecerVariables(MaquinaLinea.numlin, 1, VolumenTeoricoTB.Text, VolumenMedidoTB.Text, ErrorTB.Text, CapacidadRealTB.Text);
                            if (MaquinaLinea.numlin == 2) CompletarRegistros(1, Properties.Settings.Default.VolMedido1LlenL2, Properties.Settings.Default.Vol1LlenL2, Properties.Settings.Default.Error1LlenL2, Properties.Settings.Default.CapReal1LlenL2);
                            if (MaquinaLinea.numlin == 3) CompletarRegistros(1, Properties.Settings.Default.VolMedido1LlenL3, Properties.Settings.Default.Vol1LlenL3, Properties.Settings.Default.Error1LlenL3, Properties.Settings.Default.CapReal1LlenL3);
                            if (MaquinaLinea.numlin == 5) CompletarRegistros(1, Properties.Settings.Default.VolMedido1LlenL5, Properties.Settings.Default.Vol1LlenL5, Properties.Settings.Default.Error1LlenL5, Properties.Settings.Default.CapReal1LlenL5);
                        }
                        if (VolTeorico20TB.Text != "")
                        {
                            //En el momento que todos los campos esten completados realizamos los cálculos
                            MediaTB.Text = Convert.ToString(Math.Round(((Convert.ToDecimal(CapReal1TB.Text) + Convert.ToDecimal(CapReal2TB.Text) + Convert.ToDecimal(CapReal3TB.Text) + Convert.ToDecimal(CapReal4TB.Text) + Convert.ToDecimal(CapReal5TB.Text) + Convert.ToDecimal(CapReal6TB.Text) + Convert.ToDecimal(CapReal7TB.Text) + Convert.ToDecimal(CapReal8TB.Text) + Convert.ToDecimal(CapReal9TB.Text) + Convert.ToDecimal(CapReal10TB.Text) + Convert.ToDecimal(CapReal11TB.Text) + Convert.ToDecimal(CapReal12TB.Text) + Convert.ToDecimal(CapReal13TB.Text) + Convert.ToDecimal(CapReal14TB.Text) + Convert.ToDecimal(CapReal15TB.Text) + Convert.ToDecimal(CapReal16TB.Text) + Convert.ToDecimal(CapReal17TB.Text) + Convert.ToDecimal(CapReal18TB.Text) + Convert.ToDecimal(CapReal19TB.Text) + Convert.ToDecimal(CapReal20TB.Text)) / 20), 2));
                            VarianzaTB.Text = Convert.ToString(Math.Round((((Convert.ToDecimal(CapReal1TB.Text) * Convert.ToDecimal(CapReal1TB.Text)) + (Convert.ToDecimal(CapReal2TB.Text) * Convert.ToDecimal(CapReal2TB.Text)) + (Convert.ToDecimal(CapReal3TB.Text) * Convert.ToDecimal(CapReal3TB.Text)) + (Convert.ToDecimal(CapReal4TB.Text) * Convert.ToDecimal(CapReal4TB.Text)) + (Convert.ToDecimal(CapReal5TB.Text) * Convert.ToDecimal(CapReal5TB.Text)) + (Convert.ToDecimal(CapReal6TB.Text) * Convert.ToDecimal(CapReal6TB.Text)) + (Convert.ToDecimal(CapReal7TB.Text) * Convert.ToDecimal(CapReal7TB.Text)) + (Convert.ToDecimal(CapReal8TB.Text) * Convert.ToDecimal(CapReal8TB.Text)) + (Convert.ToDecimal(CapReal9TB.Text) * Convert.ToDecimal(CapReal9TB.Text)) + (Convert.ToDecimal(CapReal10TB.Text) * Convert.ToDecimal(CapReal10TB.Text)) + (Convert.ToDecimal(CapReal11TB.Text) * Convert.ToDecimal(CapReal11TB.Text)) + (Convert.ToDecimal(CapReal12TB.Text) * Convert.ToDecimal(CapReal12TB.Text)) + (Convert.ToDecimal(CapReal13TB.Text) * Convert.ToDecimal(CapReal13TB.Text)) + (Convert.ToDecimal(CapReal14TB.Text) * Convert.ToDecimal(CapReal14TB.Text)) + (Convert.ToDecimal(CapReal15TB.Text) * Convert.ToDecimal(CapReal15TB.Text)) + (Convert.ToDecimal(CapReal16TB.Text) * Convert.ToDecimal(CapReal16TB.Text)) + (Convert.ToDecimal(CapReal17TB.Text) * Convert.ToDecimal(CapReal17TB.Text)) + (Convert.ToDecimal(CapReal18TB.Text) * Convert.ToDecimal(CapReal18TB.Text)) + (Convert.ToDecimal(CapReal19TB.Text) * Convert.ToDecimal(CapReal19TB.Text)) + (Convert.ToDecimal(CapReal20TB.Text) * Convert.ToDecimal(CapReal20TB.Text))) / 20) - (Convert.ToDecimal(MediaTB.Text) * Convert.ToDecimal(MediaTB.Text)), 2));
                            DesviacionTipicaTB.Text = Convert.ToString(Math.Round(Math.Sqrt(Convert.ToDouble(VarianzaTB.Text)), 2));
                            RealDecretoTB.Text = Convert.ToString(Math.Round((Convert.ToDecimal(CapacidadTB.Text) - ((Convert.ToDecimal(DesviacionTipicaTB.Text) * 64) / 100)), 2));
                            if (Convert.ToDecimal(MediaTB.Text) >= Convert.ToDecimal(RealDecretoTB.Text))
                            {
                                BOEn266PB.BackgroundImage = EstadoVerde;
                            }
                            if (Convert.ToDecimal(MediaTB.Text) < Convert.ToDecimal(RealDecretoTB.Text))
                            {
                                BOEn266PB.BackgroundImage = EstadoRojo;
                            }
                            //Mostramos el mensaje de aviso para que no olviden guardar
                            AvisoLB.Show();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Hay algun tipo de fallo con la entrada de Temperatura, asegurese de que el valor introduccido es correcto y ha utilizado coma en caso de decimales.");
                    }
                }
                else MessageBox.Show(Properties.Settings.Default.AvisoVolumenLlen);
            }
            else MessageBox.Show(Properties.Settings.Default.AvisoCampos);
        }


        private void RestaB_Click(object sender, EventArgs e)
        {
            //Vamos elimniando datos 1 a uno por si hay algun fallo
            if (VolTeorico1TB.Text != "" && VolTeorico2TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 1, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(1, Properties.Settings.Default.VolMedido1LlenL2, Properties.Settings.Default.Vol1LlenL2, Properties.Settings.Default.Error1LlenL2, Properties.Settings.Default.CapReal1LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(1, Properties.Settings.Default.VolMedido1LlenL3, Properties.Settings.Default.Vol1LlenL3, Properties.Settings.Default.Error1LlenL3, Properties.Settings.Default.CapReal1LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(1, Properties.Settings.Default.VolMedido1LlenL5, Properties.Settings.Default.Vol1LlenL5, Properties.Settings.Default.Error1LlenL5, Properties.Settings.Default.CapReal1LlenL5);
            }
            if (VolTeorico2TB.Text != "" && VolTeorico3TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 2, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(2, Properties.Settings.Default.VolMedido2LlenL2, Properties.Settings.Default.Vol2LlenL2, Properties.Settings.Default.Error2LlenL2, Properties.Settings.Default.CapReal2LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(2, Properties.Settings.Default.VolMedido2LlenL3, Properties.Settings.Default.Vol2LlenL3, Properties.Settings.Default.Error2LlenL3, Properties.Settings.Default.CapReal2LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(2, Properties.Settings.Default.VolMedido2LlenL5, Properties.Settings.Default.Vol2LlenL5, Properties.Settings.Default.Error2LlenL5, Properties.Settings.Default.CapReal2LlenL5);
            }
            if (VolTeorico3TB.Text != "" && VolTeorico4TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 3, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(3, Properties.Settings.Default.VolMedido3LlenL2, Properties.Settings.Default.Vol3LlenL2, Properties.Settings.Default.Error3LlenL2, Properties.Settings.Default.CapReal3LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(3, Properties.Settings.Default.VolMedido3LlenL3, Properties.Settings.Default.Vol3LlenL3, Properties.Settings.Default.Error3LlenL3, Properties.Settings.Default.CapReal3LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(3, Properties.Settings.Default.VolMedido3LlenL5, Properties.Settings.Default.Vol3LlenL5, Properties.Settings.Default.Error3LlenL5, Properties.Settings.Default.CapReal3LlenL5);
            }
            if (VolTeorico4TB.Text != "" && VolTeorico5TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 4, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(4, Properties.Settings.Default.VolMedido4LlenL2, Properties.Settings.Default.Vol4LlenL2, Properties.Settings.Default.Error4LlenL2, Properties.Settings.Default.CapReal4LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(4, Properties.Settings.Default.VolMedido4LlenL3, Properties.Settings.Default.Vol4LlenL3, Properties.Settings.Default.Error4LlenL3, Properties.Settings.Default.CapReal4LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(4, Properties.Settings.Default.VolMedido4LlenL5, Properties.Settings.Default.Vol4LlenL5, Properties.Settings.Default.Error4LlenL5, Properties.Settings.Default.CapReal4LlenL5);
            }
            if (VolTeorico5TB.Text != "" && VolTeorico6TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 5, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(5, Properties.Settings.Default.VolMedido5LlenL2, Properties.Settings.Default.Vol5LlenL2, Properties.Settings.Default.Error5LlenL2, Properties.Settings.Default.CapReal5LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(5, Properties.Settings.Default.VolMedido5LlenL3, Properties.Settings.Default.Vol5LlenL3, Properties.Settings.Default.Error5LlenL3, Properties.Settings.Default.CapReal5LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(5, Properties.Settings.Default.VolMedido5LlenL5, Properties.Settings.Default.Vol5LlenL5, Properties.Settings.Default.Error5LlenL5, Properties.Settings.Default.CapReal5LlenL5);
            }
            if (VolTeorico6TB.Text != "" && VolTeorico7TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 6, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(6, Properties.Settings.Default.VolMedido6LlenL2, Properties.Settings.Default.Vol6LlenL2, Properties.Settings.Default.Error6LlenL2, Properties.Settings.Default.CapReal6LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(6, Properties.Settings.Default.VolMedido6LlenL3, Properties.Settings.Default.Vol6LlenL3, Properties.Settings.Default.Error6LlenL3, Properties.Settings.Default.CapReal6LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(6, Properties.Settings.Default.VolMedido6LlenL5, Properties.Settings.Default.Vol6LlenL5, Properties.Settings.Default.Error6LlenL5, Properties.Settings.Default.CapReal6LlenL5);
            }
            if (VolTeorico7TB.Text != "" && VolTeorico8TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 7, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(7, Properties.Settings.Default.VolMedido7LlenL2, Properties.Settings.Default.Vol7LlenL2, Properties.Settings.Default.Error7LlenL2, Properties.Settings.Default.CapReal7LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(7, Properties.Settings.Default.VolMedido7LlenL3, Properties.Settings.Default.Vol7LlenL3, Properties.Settings.Default.Error7LlenL3, Properties.Settings.Default.CapReal7LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(7, Properties.Settings.Default.VolMedido7LlenL5, Properties.Settings.Default.Vol7LlenL5, Properties.Settings.Default.Error7LlenL5, Properties.Settings.Default.CapReal7LlenL5);
            }
            if (VolTeorico8TB.Text != "" && VolTeorico9TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 8, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(8, Properties.Settings.Default.VolMedido8LlenL2, Properties.Settings.Default.Vol8LlenL2, Properties.Settings.Default.Error8LlenL2, Properties.Settings.Default.CapReal8LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(8, Properties.Settings.Default.VolMedido8LlenL3, Properties.Settings.Default.Vol8LlenL3, Properties.Settings.Default.Error8LlenL3, Properties.Settings.Default.CapReal8LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(8, Properties.Settings.Default.VolMedido8LlenL5, Properties.Settings.Default.Vol8LlenL5, Properties.Settings.Default.Error8LlenL5, Properties.Settings.Default.CapReal8LlenL5);
            }
            if (VolTeorico9TB.Text != "" && VolTeorico10TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 9, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(9, Properties.Settings.Default.VolMedido9LlenL2, Properties.Settings.Default.Vol9LlenL2, Properties.Settings.Default.Error9LlenL2, Properties.Settings.Default.CapReal9LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(9, Properties.Settings.Default.VolMedido9LlenL3, Properties.Settings.Default.Vol9LlenL3, Properties.Settings.Default.Error9LlenL3, Properties.Settings.Default.CapReal9LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(9, Properties.Settings.Default.VolMedido9LlenL5, Properties.Settings.Default.Vol9LlenL5, Properties.Settings.Default.Error9LlenL5, Properties.Settings.Default.CapReal9LlenL5);
            }
            if (VolTeorico10TB.Text != "" && VolTeorico11TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 10, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(10, Properties.Settings.Default.VolMedido10LlenL2, Properties.Settings.Default.Vol10LlenL2, Properties.Settings.Default.Error10LlenL2, Properties.Settings.Default.CapReal10LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(10, Properties.Settings.Default.VolMedido10LlenL3, Properties.Settings.Default.Vol10LlenL3, Properties.Settings.Default.Error10LlenL3, Properties.Settings.Default.CapReal10LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(10, Properties.Settings.Default.VolMedido10LlenL5, Properties.Settings.Default.Vol10LlenL5, Properties.Settings.Default.Error10LlenL5, Properties.Settings.Default.CapReal10LlenL5);
            }
            if (VolTeorico11TB.Text != "" && VolTeorico12TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 11, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(11, Properties.Settings.Default.VolMedido11LlenL2, Properties.Settings.Default.Vol11LlenL2, Properties.Settings.Default.Error11LlenL2, Properties.Settings.Default.CapReal11LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(11, Properties.Settings.Default.VolMedido11LlenL3, Properties.Settings.Default.Vol11LlenL3, Properties.Settings.Default.Error11LlenL3, Properties.Settings.Default.CapReal11LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(11, Properties.Settings.Default.VolMedido11LlenL5, Properties.Settings.Default.Vol11LlenL5, Properties.Settings.Default.Error11LlenL5, Properties.Settings.Default.CapReal11LlenL5);
            }
            if (VolTeorico12TB.Text != "" && VolTeorico13TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 12, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(12, Properties.Settings.Default.VolMedido12LlenL2, Properties.Settings.Default.Vol12LlenL2, Properties.Settings.Default.Error12LlenL2, Properties.Settings.Default.CapReal12LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(12, Properties.Settings.Default.VolMedido12LlenL3, Properties.Settings.Default.Vol12LlenL3, Properties.Settings.Default.Error12LlenL3, Properties.Settings.Default.CapReal12LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(12, Properties.Settings.Default.VolMedido12LlenL5, Properties.Settings.Default.Vol12LlenL5, Properties.Settings.Default.Error12LlenL5, Properties.Settings.Default.CapReal12LlenL5);
            }

            if (VolTeorico13TB.Text != "" && VolTeorico14TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 13, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(13, Properties.Settings.Default.VolMedido13LlenL2, Properties.Settings.Default.Vol13LlenL2, Properties.Settings.Default.Error13LlenL2, Properties.Settings.Default.CapReal13LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(13, Properties.Settings.Default.VolMedido13LlenL3, Properties.Settings.Default.Vol13LlenL3, Properties.Settings.Default.Error13LlenL3, Properties.Settings.Default.CapReal13LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(13, Properties.Settings.Default.VolMedido13LlenL5, Properties.Settings.Default.Vol13LlenL5, Properties.Settings.Default.Error13LlenL5, Properties.Settings.Default.CapReal13LlenL5);
            }
            if (VolTeorico14TB.Text != "" && VolTeorico15TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 14, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(14, Properties.Settings.Default.VolMedido14LlenL2, Properties.Settings.Default.Vol14LlenL2, Properties.Settings.Default.Error14LlenL2, Properties.Settings.Default.CapReal14LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(14, Properties.Settings.Default.VolMedido14LlenL3, Properties.Settings.Default.Vol14LlenL3, Properties.Settings.Default.Error14LlenL3, Properties.Settings.Default.CapReal14LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(14, Properties.Settings.Default.VolMedido14LlenL5, Properties.Settings.Default.Vol14LlenL5, Properties.Settings.Default.Error14LlenL5, Properties.Settings.Default.CapReal14LlenL5);
            }
            if (VolTeorico15TB.Text != "" && VolTeorico16TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 15, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(15, Properties.Settings.Default.VolMedido15LlenL2, Properties.Settings.Default.Vol15LlenL2, Properties.Settings.Default.Error15LlenL2, Properties.Settings.Default.CapReal15LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(15, Properties.Settings.Default.VolMedido15LlenL3, Properties.Settings.Default.Vol15LlenL3, Properties.Settings.Default.Error15LlenL3, Properties.Settings.Default.CapReal15LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(15, Properties.Settings.Default.VolMedido15LlenL5, Properties.Settings.Default.Vol15LlenL5, Properties.Settings.Default.Error15LlenL5, Properties.Settings.Default.CapReal15LlenL5);
            }
            if (VolTeorico16TB.Text != "" && VolTeorico17TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 16, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(16, Properties.Settings.Default.VolMedido16LlenL2, Properties.Settings.Default.Vol16LlenL2, Properties.Settings.Default.Error16LlenL2, Properties.Settings.Default.CapReal16LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(16, Properties.Settings.Default.VolMedido16LlenL3, Properties.Settings.Default.Vol16LlenL3, Properties.Settings.Default.Error16LlenL3, Properties.Settings.Default.CapReal16LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(16, Properties.Settings.Default.VolMedido16LlenL5, Properties.Settings.Default.Vol16LlenL5, Properties.Settings.Default.Error16LlenL5, Properties.Settings.Default.CapReal16LlenL5);
            }
            if (VolTeorico17TB.Text != "" && VolTeorico18TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 17, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(17, Properties.Settings.Default.VolMedido17LlenL2, Properties.Settings.Default.Vol17LlenL2, Properties.Settings.Default.Error17LlenL2, Properties.Settings.Default.CapReal17LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(17, Properties.Settings.Default.VolMedido17LlenL3, Properties.Settings.Default.Vol17LlenL3, Properties.Settings.Default.Error17LlenL3, Properties.Settings.Default.CapReal17LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(17, Properties.Settings.Default.VolMedido17LlenL5, Properties.Settings.Default.Vol17LlenL5, Properties.Settings.Default.Error17LlenL5, Properties.Settings.Default.CapReal17LlenL5);
            }
            if (VolTeorico18TB.Text != "" && VolTeorico19TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 18, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(18, Properties.Settings.Default.VolMedido18LlenL2, Properties.Settings.Default.Vol18LlenL2, Properties.Settings.Default.Error18LlenL2, Properties.Settings.Default.CapReal18LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(18, Properties.Settings.Default.VolMedido18LlenL3, Properties.Settings.Default.Vol18LlenL3, Properties.Settings.Default.Error18LlenL3, Properties.Settings.Default.CapReal18LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(18, Properties.Settings.Default.VolMedido18LlenL5, Properties.Settings.Default.Vol18LlenL5, Properties.Settings.Default.Error18LlenL5, Properties.Settings.Default.CapReal18LlenL5);
            }

            if (VolTeorico19TB.Text != "" && VolTeorico20TB.Text == "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 19, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(19, Properties.Settings.Default.VolMedido19LlenL2, Properties.Settings.Default.Vol19LlenL2, Properties.Settings.Default.Error19LlenL2, Properties.Settings.Default.CapReal19LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(19, Properties.Settings.Default.VolMedido19LlenL3, Properties.Settings.Default.Vol19LlenL3, Properties.Settings.Default.Error19LlenL3, Properties.Settings.Default.CapReal19LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(19, Properties.Settings.Default.VolMedido19LlenL5, Properties.Settings.Default.Vol19LlenL5, Properties.Settings.Default.Error19LlenL5, Properties.Settings.Default.CapReal19LlenL5);
            }
            if (VolTeorico20TB.Text != "")
            {
                EstablecerVariables(MaquinaLinea.numlin, 20, "", "", "", "");
                if (MaquinaLinea.numlin == 2) CompletarRegistros(20, Properties.Settings.Default.VolMedido20LlenL2, Properties.Settings.Default.Vol20LlenL2, Properties.Settings.Default.Error20LlenL2, Properties.Settings.Default.CapReal20LlenL2);
                if (MaquinaLinea.numlin == 3) CompletarRegistros(20, Properties.Settings.Default.VolMedido20LlenL3, Properties.Settings.Default.Vol20LlenL3, Properties.Settings.Default.Error20LlenL3, Properties.Settings.Default.CapReal20LlenL3);
                if (MaquinaLinea.numlin == 5) CompletarRegistros(20, Properties.Settings.Default.VolMedido20LlenL5, Properties.Settings.Default.Vol20LlenL5, Properties.Settings.Default.Error20LlenL5, Properties.Settings.Default.CapReal20LlenL5);
            }
            if (VolTeorico20TB.Text == "")
            {
                //En el momento que todos los campos esten completados realizamos los cálculos
                MediaTB.Text = "";
                VarianzaTB.Text = "";
                DesviacionTipicaTB.Text = "";
                RealDecretoTB.Text = "";
                BOEn266PB.BackgroundImage = null;
                //Ocultamos el mensaje de aviso
                AvisoLB.Hide();
            }
        }

        //Función que establece la variable
        public void EstablecerVariables(int numlin, int Registro, string VolMedido, string Volumen, string Error, string CapReal)
        {
            if (MaquinaLinea.numlin == 2)
            {
                switch (Registro)
                {
                    case 1:
                        Properties.Settings.Default.VolMedido1LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol1LlenL2 = Volumen;
                        Properties.Settings.Default.Error1LlenL2 = Error;
                        Properties.Settings.Default.CapReal1LlenL2 = CapReal;                        
                        break;
                    case 2:
                        Properties.Settings.Default.VolMedido2LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol2LlenL2 = Volumen;
                        Properties.Settings.Default.Error2LlenL2 = Error;
                        Properties.Settings.Default.CapReal2LlenL2 = CapReal;                        
                        break;
                    case 3:
                        Properties.Settings.Default.VolMedido3LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol3LlenL2 = Volumen;
                        Properties.Settings.Default.Error3LlenL2 = Error;
                        Properties.Settings.Default.CapReal3LlenL2 = CapReal;                        
                        break;
                    case 4:
                        Properties.Settings.Default.VolMedido4LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol4LlenL2 = Volumen;
                        Properties.Settings.Default.Error4LlenL2 = Error;
                        Properties.Settings.Default.CapReal4LlenL2 = CapReal;                        
                        break;
                    case 5:
                        Properties.Settings.Default.VolMedido5LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol5LlenL2 = Volumen;
                        Properties.Settings.Default.Error5LlenL2 = Error;
                        Properties.Settings.Default.CapReal5LlenL2 = CapReal;                        
                        break;
                    case 6:
                        Properties.Settings.Default.VolMedido6LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol6LlenL2 = Volumen;
                        Properties.Settings.Default.Error6LlenL2 = Error;
                        Properties.Settings.Default.CapReal6LlenL2 = CapReal;                        
                        break;
                    case 7:
                        Properties.Settings.Default.VolMedido7LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol7LlenL2 = Volumen;
                        Properties.Settings.Default.Error7LlenL2 = Error;
                        Properties.Settings.Default.CapReal7LlenL2 = CapReal;                        
                        break;
                    case 8:
                        Properties.Settings.Default.VolMedido8LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol8LlenL2 = Volumen;
                        Properties.Settings.Default.Error8LlenL2 = Error;
                        Properties.Settings.Default.CapReal8LlenL2 = CapReal;                        
                        break;
                    case 9:
                        Properties.Settings.Default.VolMedido9LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol9LlenL2 = Volumen;
                        Properties.Settings.Default.Error9LlenL2 = Error;
                        Properties.Settings.Default.CapReal9LlenL2 = CapReal;                        
                        break;
                    case 10:
                        Properties.Settings.Default.VolMedido10LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol10LlenL2 = Volumen;
                        Properties.Settings.Default.Error10LlenL2 = Error;
                        Properties.Settings.Default.CapReal10LlenL2 = CapReal;                        
                        break;
                    case 11:
                        Properties.Settings.Default.VolMedido11LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol11LlenL2 = Volumen;
                        Properties.Settings.Default.Error11LlenL2 = Error;
                        Properties.Settings.Default.CapReal11LlenL2 = CapReal;                        
                        break;
                    case 12:
                        Properties.Settings.Default.VolMedido12LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol12LlenL2 = Volumen;
                        Properties.Settings.Default.Error12LlenL2 = Error;
                        Properties.Settings.Default.CapReal12LlenL2 = CapReal;                        
                        break;
                    case 13:
                        Properties.Settings.Default.VolMedido13LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol13LlenL2 = Volumen;
                        Properties.Settings.Default.Error13LlenL2 = Error;
                        Properties.Settings.Default.CapReal13LlenL2 = CapReal;                        
                        break;
                    case 14:
                        Properties.Settings.Default.VolMedido14LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol14LlenL2 = Volumen;
                        Properties.Settings.Default.Error14LlenL2 = Error;
                        Properties.Settings.Default.CapReal14LlenL2 = CapReal;                        
                        break;
                    case 15:
                        Properties.Settings.Default.VolMedido15LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol15LlenL2 = Volumen;
                        Properties.Settings.Default.Error15LlenL2 = Error;
                        Properties.Settings.Default.CapReal15LlenL2 = CapReal;                        
                        break;
                    case 16:
                        Properties.Settings.Default.VolMedido16LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol16LlenL2 = Volumen;
                        Properties.Settings.Default.Error16LlenL2 = Error;
                        Properties.Settings.Default.CapReal16LlenL2 = CapReal;
                        break;
                    case 17:
                        Properties.Settings.Default.VolMedido17LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol17LlenL2 = Volumen;
                        Properties.Settings.Default.Error17LlenL2 = Error;
                        Properties.Settings.Default.CapReal17LlenL2 = CapReal;                        
                        break;
                    case 18:
                        Properties.Settings.Default.VolMedido18LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol18LlenL2 = Volumen;
                        Properties.Settings.Default.Error18LlenL2 = Error;
                        Properties.Settings.Default.CapReal18LlenL2 = CapReal;                        
                        break;
                    case 19:
                        Properties.Settings.Default.VolMedido19LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol19LlenL2 = Volumen;
                        Properties.Settings.Default.Error19LlenL2 = Error;
                        Properties.Settings.Default.CapReal19LlenL2 = CapReal;                        
                        break;
                    case 20:
                        Properties.Settings.Default.VolMedido20LlenL2 = VolMedido;
                        Properties.Settings.Default.Vol20LlenL2 = Volumen;
                        Properties.Settings.Default.Error20LlenL2 = Error;
                        Properties.Settings.Default.CapReal20LlenL2 = CapReal;                        
                        break;
                }
            }

            if (MaquinaLinea.numlin == 3)
            {
                switch (Registro)
                {
                    case 1:
                        Properties.Settings.Default.VolMedido1LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol1LlenL3 = Volumen;
                        Properties.Settings.Default.Error1LlenL3 = Error;
                        Properties.Settings.Default.CapReal1LlenL3 = CapReal;                        
                        break;
                    case 2:
                        Properties.Settings.Default.VolMedido2LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol2LlenL3 = Volumen;
                        Properties.Settings.Default.Error2LlenL3 = Error;
                        Properties.Settings.Default.CapReal2LlenL3 = CapReal;                        
                        break;
                    case 3:
                        Properties.Settings.Default.VolMedido3LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol3LlenL3 = Volumen;
                        Properties.Settings.Default.Error3LlenL3 = Error;
                        Properties.Settings.Default.CapReal3LlenL3 = CapReal;                        
                        break;
                    case 4:
                        Properties.Settings.Default.VolMedido4LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol4LlenL3 = Volumen;
                        Properties.Settings.Default.Error4LlenL3 = Error;
                        Properties.Settings.Default.CapReal4LlenL3 = CapReal;                        
                        break;
                    case 5:
                        Properties.Settings.Default.VolMedido5LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol5LlenL3 = Volumen;
                        Properties.Settings.Default.Error5LlenL3 = Error;
                        Properties.Settings.Default.CapReal5LlenL3 = CapReal;                        
                        break;
                    case 6:
                        Properties.Settings.Default.VolMedido6LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol6LlenL3 = Volumen;
                        Properties.Settings.Default.Error6LlenL3 = Error;
                        Properties.Settings.Default.CapReal6LlenL3 = CapReal;                        
                        break;
                    case 7:
                        Properties.Settings.Default.VolMedido7LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol7LlenL3 = Volumen;
                        Properties.Settings.Default.Error7LlenL3 = Error;
                        Properties.Settings.Default.CapReal7LlenL3 = CapReal;                        
                        break;
                    case 8:
                        Properties.Settings.Default.VolMedido8LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol8LlenL3 = Volumen;
                        Properties.Settings.Default.Error8LlenL3 = Error;
                        Properties.Settings.Default.CapReal8LlenL3 = CapReal;                        
                        break;
                    case 9:
                        Properties.Settings.Default.VolMedido9LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol9LlenL3 = Volumen;
                        Properties.Settings.Default.Error9LlenL3 = Error;
                        Properties.Settings.Default.CapReal9LlenL3 = CapReal;                        
                        break;
                    case 10:
                        Properties.Settings.Default.VolMedido10LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol10LlenL3 = Volumen;
                        Properties.Settings.Default.Error10LlenL3 = Error;
                        Properties.Settings.Default.CapReal10LlenL3 = CapReal;                        
                        break;
                    case 11:
                        Properties.Settings.Default.VolMedido11LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol11LlenL3 = Volumen;
                        Properties.Settings.Default.Error11LlenL3 = Error;
                        Properties.Settings.Default.CapReal11LlenL3 = CapReal;                        
                        break;
                    case 12:
                        Properties.Settings.Default.VolMedido12LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol12LlenL3 = Volumen;
                        Properties.Settings.Default.Error12LlenL3 = Error;
                        Properties.Settings.Default.CapReal12LlenL3 = CapReal;                        
                        break;
                    case 13:
                        Properties.Settings.Default.VolMedido13LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol13LlenL3 = Volumen;
                        Properties.Settings.Default.Error13LlenL3 = Error;
                        Properties.Settings.Default.CapReal13LlenL3 = CapReal;                        
                        break;
                    case 14:
                        Properties.Settings.Default.VolMedido14LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol14LlenL3 = Volumen;
                        Properties.Settings.Default.Error14LlenL3 = Error;
                        Properties.Settings.Default.CapReal14LlenL3 = CapReal;                        
                        break;
                    case 15:
                        Properties.Settings.Default.VolMedido15LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol15LlenL3 = Volumen;
                        Properties.Settings.Default.Error15LlenL3 = Error;
                        Properties.Settings.Default.CapReal15LlenL3 = CapReal;                        
                        break;
                    case 16:
                        Properties.Settings.Default.VolMedido16LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol16LlenL3 = Volumen;
                        Properties.Settings.Default.Error16LlenL3 = Error;
                        Properties.Settings.Default.CapReal16LlenL3 = CapReal;                        
                        break;
                    case 17:
                        Properties.Settings.Default.VolMedido17LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol17LlenL3 = Volumen;
                        Properties.Settings.Default.Error17LlenL3 = Error;
                        Properties.Settings.Default.CapReal17LlenL3 = CapReal;                        
                        break;
                    case 18:
                        Properties.Settings.Default.VolMedido18LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol18LlenL3 = Volumen;
                        Properties.Settings.Default.Error18LlenL3 = Error;
                        Properties.Settings.Default.CapReal18LlenL3 = CapReal;                        
                        break;
                    case 19:
                        Properties.Settings.Default.VolMedido19LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol19LlenL3 = Volumen;
                        Properties.Settings.Default.Error19LlenL3 = Error;
                        Properties.Settings.Default.CapReal19LlenL3 = CapReal;                        
                        break;
                    case 20:
                        Properties.Settings.Default.VolMedido20LlenL3 = VolMedido;
                        Properties.Settings.Default.Vol20LlenL3 = Volumen;
                        Properties.Settings.Default.Error20LlenL3 = Error;
                        Properties.Settings.Default.CapReal20LlenL3 = CapReal;                        
                        break;
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                switch (Registro)
                {
                    case 1:
                        Properties.Settings.Default.VolMedido1LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol1LlenL5 = Volumen;
                        Properties.Settings.Default.Error1LlenL5 = Error;
                        Properties.Settings.Default.CapReal1LlenL5 = CapReal;                        
                        break;
                    case 2:
                        Properties.Settings.Default.VolMedido2LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol2LlenL5 = Volumen;
                        Properties.Settings.Default.Error2LlenL5 = Error;
                        Properties.Settings.Default.CapReal2LlenL5 = CapReal;                        
                        break;
                    case 3:
                        Properties.Settings.Default.VolMedido3LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol3LlenL5 = Volumen;
                        Properties.Settings.Default.Error3LlenL5 = Error;
                        Properties.Settings.Default.CapReal3LlenL5 = CapReal;                        
                        break;
                    case 4:
                        Properties.Settings.Default.VolMedido4LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol4LlenL5 = Volumen;
                        Properties.Settings.Default.Error4LlenL5 = Error;
                        Properties.Settings.Default.CapReal4LlenL5 = CapReal;                        
                        break;
                    case 5:
                        Properties.Settings.Default.VolMedido5LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol5LlenL5 = Volumen;
                        Properties.Settings.Default.Error5LlenL5 = Error;
                        Properties.Settings.Default.CapReal5LlenL5 = CapReal;                        
                        break;
                    case 6:
                        Properties.Settings.Default.VolMedido6LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol6LlenL5 = Volumen;
                        Properties.Settings.Default.Error6LlenL5 = Error;
                        Properties.Settings.Default.CapReal6LlenL5 = CapReal;                        
                        break;
                    case 7:
                        Properties.Settings.Default.VolMedido7LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol7LlenL5 = Volumen;
                        Properties.Settings.Default.Error7LlenL5 = Error;
                        Properties.Settings.Default.CapReal7LlenL5 = CapReal;                        
                        break;
                    case 8:
                        Properties.Settings.Default.VolMedido8LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol8LlenL5 = Volumen;
                        Properties.Settings.Default.Error8LlenL5 = Error;
                        Properties.Settings.Default.CapReal8LlenL5 = CapReal;                        
                        break;
                    case 9:
                        Properties.Settings.Default.VolMedido9LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol9LlenL5 = Volumen;
                        Properties.Settings.Default.Error9LlenL5 = Error;
                        Properties.Settings.Default.CapReal9LlenL5 = CapReal;                        
                        break;
                    case 10:
                        Properties.Settings.Default.VolMedido10LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol10LlenL5 = Volumen;
                        Properties.Settings.Default.Error10LlenL5 = Error;
                        Properties.Settings.Default.CapReal10LlenL5 = CapReal;                        
                        break;
                    case 11:
                        Properties.Settings.Default.VolMedido11LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol11LlenL5 = Volumen;
                        Properties.Settings.Default.Error11LlenL5 = Error;
                        Properties.Settings.Default.CapReal11LlenL5 = CapReal;                        
                        break;
                    case 12:
                        Properties.Settings.Default.VolMedido12LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol12LlenL5 = Volumen;
                        Properties.Settings.Default.Error12LlenL5 = Error;
                        Properties.Settings.Default.CapReal12LlenL5 = CapReal;                        
                        break;
                    case 13:
                        Properties.Settings.Default.VolMedido13LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol13LlenL5 = Volumen;
                        Properties.Settings.Default.Error13LlenL5 = Error;
                        Properties.Settings.Default.CapReal13LlenL5 = CapReal;                        
                        break;
                    case 14:
                        Properties.Settings.Default.VolMedido14LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol14LlenL5 = Volumen;
                        Properties.Settings.Default.Error14LlenL5 = Error;
                        Properties.Settings.Default.CapReal14LlenL5 = CapReal;                        
                        break;
                    case 15:
                        Properties.Settings.Default.VolMedido15LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol15LlenL5 = Volumen;
                        Properties.Settings.Default.Error15LlenL5 = Error;
                        Properties.Settings.Default.CapReal15LlenL5 = CapReal;                        
                        break;
                    case 16:
                        Properties.Settings.Default.VolMedido16LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol16LlenL5 = Volumen;
                        Properties.Settings.Default.Error16LlenL5 = Error;
                        Properties.Settings.Default.CapReal16LlenL5 = CapReal;                        
                        break;
                    case 17:
                        Properties.Settings.Default.VolMedido17LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol17LlenL5 = Volumen;
                        Properties.Settings.Default.Error17LlenL5 = Error;
                        Properties.Settings.Default.CapReal17LlenL5 = CapReal;                        
                        break;
                    case 18:
                        Properties.Settings.Default.VolMedido18LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol18LlenL5 = Volumen;
                        Properties.Settings.Default.Error18LlenL5 = Error;
                        Properties.Settings.Default.CapReal18LlenL5 = CapReal;                        
                        break;
                    case 19:
                        Properties.Settings.Default.VolMedido19LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol19LlenL5 = Volumen;
                        Properties.Settings.Default.Error19LlenL5 = Error;
                        Properties.Settings.Default.CapReal19LlenL5 = CapReal;                        
                        break;
                    case 20:
                        Properties.Settings.Default.VolMedido20LlenL5 = VolMedido;
                        Properties.Settings.Default.Vol20LlenL5 = Volumen;
                        Properties.Settings.Default.Error20LlenL5 = Error;
                        Properties.Settings.Default.CapReal20LlenL5 = CapReal;                        
                        break;
                }
            }
            Properties.Settings.Default.Save();
        }
        //Función que completa los registros
        public void CompletarRegistros(int Registro, string VolMedido, string Volumen, string Error, string CapReal)
        {
            Image Estado = null;
            if (Error != "" && CapacidadTB.Text != "")
            {
                Datos_Volumen Color = new Datos_Volumen();
                Color = Apps_Llenadora.ParsingEstadoVolumen(Convert.ToDecimal(Error), CapacidadTB.Text);
                if (Color.estado == "Verde")
                {
                    Estado = EstadoVerde;
                }
                if (Color.estado == "Naranja")
                {
                    Estado = EstadoNaranja;
                }
                if (Color.estado == "Rojo")
                {
                    Estado = EstadoRojo;
                }
            }
            switch (Registro)
            {
                case 1:
                    VolTeorico1TB.Text = VolMedido;
                    Vol1TB.Text = Volumen;
                    Error1TB.Text = Error;
                    CapReal1TB.Text = CapReal;
                    Estado1PB.BackgroundImage = Estado;
                    break;
                case 2:
                    VolTeorico2TB.Text = VolMedido;
                    Vol2TB.Text = Volumen;
                    Error2TB.Text = Error;
                    CapReal2TB.Text = CapReal;
                    Estado2PB.BackgroundImage = Estado;
                    break;
                case 3:
                    VolTeorico3TB.Text = VolMedido;
                    Vol3TB.Text = Volumen;
                    Error3TB.Text = Error;
                    CapReal3TB.Text = CapReal;
                    Estado3PB.BackgroundImage = Estado;
                    break;
                case 4:
                    VolTeorico4TB.Text = VolMedido;
                    Vol4TB.Text = Volumen;
                    Error4TB.Text = Error;
                    CapReal4TB.Text = CapReal;
                    Estado4PB.BackgroundImage = Estado;
                    break;
                case 5:
                    VolTeorico5TB.Text = VolMedido;
                    Vol5TB.Text = Volumen;
                    Error5TB.Text = Error;
                    CapReal5TB.Text = CapReal;
                    Estado5PB.BackgroundImage = Estado;
                    break;
                case 6:
                    VolTeorico6TB.Text = VolMedido;
                    Vol6TB.Text = Volumen;
                    Error6TB.Text = Error;
                    CapReal6TB.Text = CapReal;
                    Estado6PB.BackgroundImage = Estado;
                    break;
                case 7:
                    VolTeorico7TB.Text = VolMedido;
                    Vol7TB.Text = Volumen;
                    Error7TB.Text = Error;
                    CapReal7TB.Text = CapReal;
                    Estado7PB.BackgroundImage = Estado;
                    break;
                case 8:
                    VolTeorico8TB.Text = VolMedido;
                    Vol8TB.Text = Volumen;
                    Error8TB.Text = Error;
                    CapReal8TB.Text = CapReal;
                    Estado8PB.BackgroundImage = Estado;
                    break;
                case 9:
                    VolTeorico9TB.Text = VolMedido;
                    Vol9TB.Text = Volumen;
                    Error9TB.Text = Error;
                    CapReal9TB.Text = CapReal;
                    Estado9PB.BackgroundImage = Estado;
                    break;

                case 10:
                    VolTeorico10TB.Text = VolMedido;
                    Vol10TB.Text = Volumen;
                    Error10TB.Text = Error;
                    CapReal10TB.Text = CapReal;
                    Estado10PB.BackgroundImage = Estado;
                    break;
                case 11:
                    VolTeorico11TB.Text = VolMedido;
                    Vol11TB.Text = Volumen;
                    Error11TB.Text = Error;
                    CapReal11TB.Text = CapReal;
                    Estado11PB.BackgroundImage = Estado;
                    break;
                case 12:
                    VolTeorico12TB.Text = VolMedido;
                    Vol12TB.Text = Volumen;
                    Error12TB.Text = Error;
                    CapReal12TB.Text = CapReal;
                    Estado12PB.BackgroundImage = Estado;
                    break;
                case 13:
                    VolTeorico13TB.Text = VolMedido;
                    Vol13TB.Text = Volumen;
                    Error13TB.Text = Error;
                    CapReal13TB.Text = CapReal;
                    Estado13PB.BackgroundImage = Estado;
                    break;
                case 14:
                    VolTeorico14TB.Text = VolMedido;
                    Vol14TB.Text = Volumen;
                    Error14TB.Text = Error;
                    CapReal14TB.Text = CapReal;
                    Estado14PB.BackgroundImage = Estado;
                    break;
                case 15:
                    VolTeorico15TB.Text = VolMedido;
                    Vol15TB.Text = Volumen;
                    Error15TB.Text = Error;
                    CapReal15TB.Text = CapReal;
                    Estado15PB.BackgroundImage = Estado;
                    break;
                case 16:
                    VolTeorico16TB.Text = VolMedido;
                    Vol16TB.Text = Volumen;
                    Error16TB.Text = Error;
                    CapReal16TB.Text = CapReal;
                    Estado16PB.BackgroundImage = Estado;
                    break;
                case 17:
                    VolTeorico17TB.Text = VolMedido;
                    Vol17TB.Text = Volumen;
                    Error17TB.Text = Error;
                    CapReal17TB.Text = CapReal;
                    Estado17PB.BackgroundImage = Estado;
                    break;
                case 18:
                    VolTeorico18TB.Text = VolMedido;
                    Vol18TB.Text = Volumen;
                    Error18TB.Text = Error;
                    CapReal18TB.Text = CapReal;
                    Estado18PB.BackgroundImage = Estado;
                    break;
                case 19:
                    VolTeorico19TB.Text = VolMedido;
                    Vol19TB.Text = Volumen;
                    Error19TB.Text = Error;
                    CapReal19TB.Text = CapReal;
                    Estado19PB.BackgroundImage = Estado;
                    break;
                case 20:
                    VolTeorico20TB.Text = VolMedido;
                    Vol20TB.Text = Volumen;
                    Error20TB.Text = Error;
                    CapReal20TB.Text = CapReal;
                    Estado20PB.BackgroundImage = Estado;
                    break;
            }

        }
        private void saveBot_Click(object sender, EventArgs e)
        {
            //Cuando el textbox  del real decreto este relleno, significará que todos los campos han sido registrado y por ello guardamos todos los valores
            if (RealDecretoTB.Text != "")
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "Fecha", DateTime.Now.ToString("dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Hora", DateTime.Now.ToString("HH:mm:ss") });
                listavalores.Add(new string[2] { "FechaDB", DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Responsable", MaquinaLinea.Responsable });
                listavalores.Add(new string[2] { "Maquinista", MaquinaLinea.MLlenadora });
                listavalores.Add(new string[2] { "Turno", turnoTB.Text });
                listavalores.Add(new string[2] { "Producto", ProductoTB.Text });
                listavalores.Add(new string[2] { "M1CapReal", CapReal1TB.Text });
                listavalores.Add(new string[2] { "M1VolMed", Vol1TB.Text });
                listavalores.Add(new string[2] { "M2CapReal", CapReal2TB.Text });
                listavalores.Add(new string[2] { "M2VolMed", Vol2TB.Text });
                listavalores.Add(new string[2] { "M3CapReal", CapReal3TB.Text });
                listavalores.Add(new string[2] { "M3VolMed", Vol3TB.Text });
                listavalores.Add(new string[2] { "M4CapReal", CapReal4TB.Text });
                listavalores.Add(new string[2] { "M4VolMed", Vol4TB.Text });
                listavalores.Add(new string[2] { "M5CapReal", CapReal5TB.Text });
                listavalores.Add(new string[2] { "M5VolMed", Vol5TB.Text });
                listavalores.Add(new string[2] { "M6CapReal", CapReal6TB.Text });
                listavalores.Add(new string[2] { "M6VolMed", Vol6TB.Text });
                listavalores.Add(new string[2] { "M7CapReal", CapReal7TB.Text });
                listavalores.Add(new string[2] { "M7VolMed", Vol7TB.Text });
                listavalores.Add(new string[2] { "M8CapReal", CapReal8TB.Text });
                listavalores.Add(new string[2] { "M8VolMed", Vol8TB.Text });
                listavalores.Add(new string[2] { "M9CapReal", CapReal9TB.Text });
                listavalores.Add(new string[2] { "M9VolMed", Vol9TB.Text });
                listavalores.Add(new string[2] { "M10CapReal", CapReal10TB.Text });
                listavalores.Add(new string[2] { "M10VolMed", Vol10TB.Text });
                listavalores.Add(new string[2] { "M11CapReal", CapReal11TB.Text });
                listavalores.Add(new string[2] { "M11VolMed", Vol11TB.Text });
                listavalores.Add(new string[2] { "M12CapReal", CapReal12TB.Text });
                listavalores.Add(new string[2] { "M12VolMed", Vol12TB.Text });
                listavalores.Add(new string[2] { "M13CapReal", CapReal13TB.Text });
                listavalores.Add(new string[2] { "M13VolMed", Vol13TB.Text });
                listavalores.Add(new string[2] { "M14CapReal", CapReal14TB.Text });
                listavalores.Add(new string[2] { "M14VolMed", Vol14TB.Text });
                listavalores.Add(new string[2] { "M15CapReal", CapReal15TB.Text });
                listavalores.Add(new string[2] { "M15VolMed", Vol15TB.Text });
                listavalores.Add(new string[2] { "M16CapReal", CapReal16TB.Text });
                listavalores.Add(new string[2] { "M16VolMed", Vol16TB.Text });
                listavalores.Add(new string[2] { "M17CapReal", CapReal17TB.Text });
                listavalores.Add(new string[2] { "M17VolMed", Vol17TB.Text });
                listavalores.Add(new string[2] { "M18CapReal", CapReal18TB.Text });
                listavalores.Add(new string[2] { "M18VolMed", Vol18TB.Text });
                listavalores.Add(new string[2] { "M19CapReal", CapReal19TB.Text });
                listavalores.Add(new string[2] { "M19VolMed", Vol19TB.Text });
                listavalores.Add(new string[2] { "M20CapReal", CapReal20TB.Text });
                listavalores.Add(new string[2] { "M20VolMed", Vol20TB.Text });
                listavalores.Add(new string[2] { "Media", MediaTB.Text });
                listavalores.Add(new string[2] { "DesviacionTipica", DesviacionTipicaTB.Text });
                listavalores.Add(new string[2] { "Varianza", VarianzaTB.Text });
                listavalores.Add(new string[2] { "RealDecreto", RealDecretoTB.Text });
                if (Convert.ToDecimal(MediaTB.Text) >= Convert.ToDecimal(RealDecretoTB.Text))
                {
                    listavalores.Add(new string[2] { "BOE", "VALIDO" });
                }
                if (Convert.ToDecimal(MediaTB.Text) < Convert.ToDecimal(RealDecretoTB.Text))
                {
                    listavalores.Add(new string[2] { "BOE", "NO VALIDO" });
                }

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileLlenadora, "ControlVolumen", listavalores, "Id");
                if (salida.Contains("Error"))
                {
                    MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                    //MessageBox.Show(salida);
                }
                else
                {

                    if (MaquinaLinea.numlin == 2)
                    {
                        Properties.Settings.Default.TemperaturaContVolLlenL2 = "";
                        Properties.Settings.Default.VolumenContVolLlenL2 = "";
                        Properties.Settings.Default.VolMedido1LlenL2 = "";
                        Properties.Settings.Default.VolMedido2LlenL2 = "";
                        Properties.Settings.Default.VolMedido3LlenL2 = "";
                        Properties.Settings.Default.VolMedido4LlenL2 = "";
                        Properties.Settings.Default.VolMedido5LlenL2 = "";
                        Properties.Settings.Default.VolMedido6LlenL2 = "";
                        Properties.Settings.Default.VolMedido7LlenL2 = "";
                        Properties.Settings.Default.VolMedido8LlenL2 = "";
                        Properties.Settings.Default.VolMedido9LlenL2 = "";
                        Properties.Settings.Default.VolMedido10LlenL2 = "";
                        Properties.Settings.Default.VolMedido11LlenL2 = "";
                        Properties.Settings.Default.VolMedido12LlenL2 = "";
                        Properties.Settings.Default.VolMedido13LlenL2 = "";
                        Properties.Settings.Default.VolMedido14LlenL2 = "";
                        Properties.Settings.Default.VolMedido15LlenL2 = "";
                        Properties.Settings.Default.VolMedido16LlenL2 = "";
                        Properties.Settings.Default.VolMedido17LlenL2 = "";
                        Properties.Settings.Default.VolMedido18LlenL2 = "";
                        Properties.Settings.Default.VolMedido19LlenL2 = "";
                        Properties.Settings.Default.VolMedido20LlenL2 = "";
                        Properties.Settings.Default.Vol1LlenL2 = "";
                        Properties.Settings.Default.Vol2LlenL2 = "";
                        Properties.Settings.Default.Vol3LlenL2 = "";
                        Properties.Settings.Default.Vol4LlenL2 = "";
                        Properties.Settings.Default.Vol5LlenL2 = "";
                        Properties.Settings.Default.Vol6LlenL2 = "";
                        Properties.Settings.Default.Vol7LlenL2 = "";
                        Properties.Settings.Default.Vol8LlenL2 = "";
                        Properties.Settings.Default.Vol9LlenL2 = "";
                        Properties.Settings.Default.Vol10LlenL2 = "";
                        Properties.Settings.Default.Vol11LlenL2 = "";
                        Properties.Settings.Default.Vol12LlenL2 = "";
                        Properties.Settings.Default.Vol13LlenL2 = "";
                        Properties.Settings.Default.Vol14LlenL2 = "";
                        Properties.Settings.Default.Vol15LlenL2 = "";
                        Properties.Settings.Default.Vol16LlenL2 = "";
                        Properties.Settings.Default.Vol17LlenL2 = "";
                        Properties.Settings.Default.Vol18LlenL2 = "";
                        Properties.Settings.Default.Vol19LlenL2 = "";
                        Properties.Settings.Default.Vol20LlenL2 = "";
                        Properties.Settings.Default.Error1LlenL2 = "";
                        Properties.Settings.Default.Error2LlenL2 = "";
                        Properties.Settings.Default.Error3LlenL2 = "";
                        Properties.Settings.Default.Error4LlenL2 = "";
                        Properties.Settings.Default.Error5LlenL2 = "";
                        Properties.Settings.Default.Error6LlenL2 = "";
                        Properties.Settings.Default.Error7LlenL2 = "";
                        Properties.Settings.Default.Error8LlenL2 = "";
                        Properties.Settings.Default.Error9LlenL2 = "";
                        Properties.Settings.Default.Error10LlenL2 = "";
                        Properties.Settings.Default.Error11LlenL2 = "";
                        Properties.Settings.Default.Error12LlenL2 = "";
                        Properties.Settings.Default.Error13LlenL2 = "";
                        Properties.Settings.Default.Error14LlenL2 = "";
                        Properties.Settings.Default.Error15LlenL2 = "";
                        Properties.Settings.Default.Error16LlenL2 = "";
                        Properties.Settings.Default.Error17LlenL2 = "";
                        Properties.Settings.Default.Error18LlenL2 = "";
                        Properties.Settings.Default.Error19LlenL2 = "";
                        Properties.Settings.Default.Error20LlenL2 = "";
                        Properties.Settings.Default.CapReal1LlenL2 = "";
                        Properties.Settings.Default.CapReal2LlenL2 = "";
                        Properties.Settings.Default.CapReal3LlenL2 = "";
                        Properties.Settings.Default.CapReal4LlenL2 = "";
                        Properties.Settings.Default.CapReal5LlenL2 = "";
                        Properties.Settings.Default.CapReal6LlenL2 = "";
                        Properties.Settings.Default.CapReal7LlenL2 = "";
                        Properties.Settings.Default.CapReal8LlenL2 = "";
                        Properties.Settings.Default.CapReal9LlenL2 = "";
                        Properties.Settings.Default.CapReal10LlenL2 = "";
                        Properties.Settings.Default.CapReal11LlenL2 = "";
                        Properties.Settings.Default.CapReal12LlenL2 = "";
                        Properties.Settings.Default.CapReal13LlenL2 = "";
                        Properties.Settings.Default.CapReal14LlenL2 = "";
                        Properties.Settings.Default.CapReal15LlenL2 = "";
                        Properties.Settings.Default.CapReal16LlenL2 = "";
                        Properties.Settings.Default.CapReal17LlenL2 = "";
                        Properties.Settings.Default.CapReal18LlenL2 = "";
                        Properties.Settings.Default.CapReal19LlenL2 = "";
                        Properties.Settings.Default.CapReal20LlenL2 = "";
                        Properties.Settings.Default.Save();
                        CompletarRegistros(1, Properties.Settings.Default.VolMedido1LlenL2, Properties.Settings.Default.Vol1LlenL2, Properties.Settings.Default.Error1LlenL2, Properties.Settings.Default.CapReal1LlenL2);
                        CompletarRegistros(2, Properties.Settings.Default.VolMedido2LlenL2, Properties.Settings.Default.Vol2LlenL2, Properties.Settings.Default.Error2LlenL2, Properties.Settings.Default.CapReal2LlenL2);
                        CompletarRegistros(3, Properties.Settings.Default.VolMedido3LlenL2, Properties.Settings.Default.Vol3LlenL2, Properties.Settings.Default.Error3LlenL2, Properties.Settings.Default.CapReal3LlenL2);
                        CompletarRegistros(4, Properties.Settings.Default.VolMedido4LlenL2, Properties.Settings.Default.Vol4LlenL2, Properties.Settings.Default.Error4LlenL2, Properties.Settings.Default.CapReal4LlenL2);
                        CompletarRegistros(5, Properties.Settings.Default.VolMedido5LlenL2, Properties.Settings.Default.Vol5LlenL2, Properties.Settings.Default.Error5LlenL2, Properties.Settings.Default.CapReal5LlenL2);
                        CompletarRegistros(6, Properties.Settings.Default.VolMedido6LlenL2, Properties.Settings.Default.Vol6LlenL2, Properties.Settings.Default.Error6LlenL2, Properties.Settings.Default.CapReal6LlenL2);
                        CompletarRegistros(7, Properties.Settings.Default.VolMedido7LlenL2, Properties.Settings.Default.Vol7LlenL2, Properties.Settings.Default.Error7LlenL2, Properties.Settings.Default.CapReal7LlenL2);
                        CompletarRegistros(8, Properties.Settings.Default.VolMedido8LlenL2, Properties.Settings.Default.Vol8LlenL2, Properties.Settings.Default.Error8LlenL2, Properties.Settings.Default.CapReal8LlenL2);
                        CompletarRegistros(9, Properties.Settings.Default.VolMedido9LlenL2, Properties.Settings.Default.Vol9LlenL2, Properties.Settings.Default.Error9LlenL2, Properties.Settings.Default.CapReal9LlenL2);
                        CompletarRegistros(10, Properties.Settings.Default.VolMedido10LlenL2, Properties.Settings.Default.Vol10LlenL2, Properties.Settings.Default.Error10LlenL2, Properties.Settings.Default.CapReal10LlenL2);
                        CompletarRegistros(11, Properties.Settings.Default.VolMedido11LlenL2, Properties.Settings.Default.Vol11LlenL2, Properties.Settings.Default.Error11LlenL2, Properties.Settings.Default.CapReal11LlenL2);
                        CompletarRegistros(12, Properties.Settings.Default.VolMedido12LlenL2, Properties.Settings.Default.Vol12LlenL2, Properties.Settings.Default.Error12LlenL2, Properties.Settings.Default.CapReal12LlenL2);
                        CompletarRegistros(13, Properties.Settings.Default.VolMedido13LlenL2, Properties.Settings.Default.Vol13LlenL2, Properties.Settings.Default.Error13LlenL2, Properties.Settings.Default.CapReal13LlenL2);
                        CompletarRegistros(14, Properties.Settings.Default.VolMedido14LlenL2, Properties.Settings.Default.Vol14LlenL2, Properties.Settings.Default.Error14LlenL2, Properties.Settings.Default.CapReal14LlenL2);
                        CompletarRegistros(15, Properties.Settings.Default.VolMedido15LlenL2, Properties.Settings.Default.Vol15LlenL2, Properties.Settings.Default.Error15LlenL2, Properties.Settings.Default.CapReal15LlenL2);
                        CompletarRegistros(16, Properties.Settings.Default.VolMedido16LlenL2, Properties.Settings.Default.Vol16LlenL2, Properties.Settings.Default.Error16LlenL2, Properties.Settings.Default.CapReal16LlenL2);
                        CompletarRegistros(17, Properties.Settings.Default.VolMedido17LlenL2, Properties.Settings.Default.Vol17LlenL2, Properties.Settings.Default.Error17LlenL2, Properties.Settings.Default.CapReal17LlenL2);
                        CompletarRegistros(18, Properties.Settings.Default.VolMedido18LlenL2, Properties.Settings.Default.Vol18LlenL2, Properties.Settings.Default.Error18LlenL2, Properties.Settings.Default.CapReal18LlenL2);
                        CompletarRegistros(19, Properties.Settings.Default.VolMedido19LlenL2, Properties.Settings.Default.Vol19LlenL2, Properties.Settings.Default.Error19LlenL2, Properties.Settings.Default.CapReal19LlenL2);
                        CompletarRegistros(20, Properties.Settings.Default.VolMedido20LlenL2, Properties.Settings.Default.Vol20LlenL2, Properties.Settings.Default.Error20LlenL2, Properties.Settings.Default.CapReal20LlenL2);
                    }
                    if (MaquinaLinea.numlin == 3)
                    {
                        Properties.Settings.Default.TemperaturaContVolLlenL3 = "";
                        Properties.Settings.Default.VolumenContVolLlenL3 = "";
                        Properties.Settings.Default.VolMedido1LlenL3 = "";
                        Properties.Settings.Default.VolMedido2LlenL3 = "";
                        Properties.Settings.Default.VolMedido3LlenL3 = "";
                        Properties.Settings.Default.VolMedido4LlenL3 = "";
                        Properties.Settings.Default.VolMedido5LlenL3 = "";
                        Properties.Settings.Default.VolMedido6LlenL3 = "";
                        Properties.Settings.Default.VolMedido7LlenL3 = "";
                        Properties.Settings.Default.VolMedido8LlenL3 = "";
                        Properties.Settings.Default.VolMedido9LlenL3 = "";
                        Properties.Settings.Default.VolMedido10LlenL3 = "";
                        Properties.Settings.Default.VolMedido11LlenL3 = "";
                        Properties.Settings.Default.VolMedido12LlenL3 = "";
                        Properties.Settings.Default.VolMedido13LlenL3 = "";
                        Properties.Settings.Default.VolMedido14LlenL3 = "";
                        Properties.Settings.Default.VolMedido15LlenL3 = "";
                        Properties.Settings.Default.VolMedido16LlenL3 = "";
                        Properties.Settings.Default.VolMedido17LlenL3 = "";
                        Properties.Settings.Default.VolMedido18LlenL3 = "";
                        Properties.Settings.Default.VolMedido19LlenL3 = "";
                        Properties.Settings.Default.VolMedido20LlenL3 = "";
                        Properties.Settings.Default.Vol1LlenL3 = "";
                        Properties.Settings.Default.Vol2LlenL3 = "";
                        Properties.Settings.Default.Vol3LlenL3 = "";
                        Properties.Settings.Default.Vol4LlenL3 = "";
                        Properties.Settings.Default.Vol5LlenL3 = "";
                        Properties.Settings.Default.Vol6LlenL3 = "";
                        Properties.Settings.Default.Vol7LlenL3 = "";
                        Properties.Settings.Default.Vol8LlenL3 = "";
                        Properties.Settings.Default.Vol9LlenL3 = "";
                        Properties.Settings.Default.Vol10LlenL3 = "";
                        Properties.Settings.Default.Vol11LlenL3 = "";
                        Properties.Settings.Default.Vol12LlenL3 = "";
                        Properties.Settings.Default.Vol13LlenL3 = "";
                        Properties.Settings.Default.Vol14LlenL3 = "";
                        Properties.Settings.Default.Vol15LlenL3 = "";
                        Properties.Settings.Default.Vol16LlenL3 = "";
                        Properties.Settings.Default.Vol17LlenL3 = "";
                        Properties.Settings.Default.Vol18LlenL3 = "";
                        Properties.Settings.Default.Vol19LlenL3 = "";
                        Properties.Settings.Default.Vol20LlenL3 = "";
                        Properties.Settings.Default.Error1LlenL3 = "";
                        Properties.Settings.Default.Error2LlenL3 = "";
                        Properties.Settings.Default.Error3LlenL3 = "";
                        Properties.Settings.Default.Error4LlenL3 = "";
                        Properties.Settings.Default.Error5LlenL3 = "";
                        Properties.Settings.Default.Error6LlenL3 = "";
                        Properties.Settings.Default.Error7LlenL3 = "";
                        Properties.Settings.Default.Error8LlenL3 = "";
                        Properties.Settings.Default.Error9LlenL3 = "";
                        Properties.Settings.Default.Error10LlenL3 = "";
                        Properties.Settings.Default.Error11LlenL3 = "";
                        Properties.Settings.Default.Error12LlenL3 = "";
                        Properties.Settings.Default.Error13LlenL3 = "";
                        Properties.Settings.Default.Error14LlenL3 = "";
                        Properties.Settings.Default.Error15LlenL3 = "";
                        Properties.Settings.Default.Error16LlenL3 = "";
                        Properties.Settings.Default.Error17LlenL3 = "";
                        Properties.Settings.Default.Error18LlenL3 = "";
                        Properties.Settings.Default.Error19LlenL3 = "";
                        Properties.Settings.Default.Error20LlenL3 = "";
                        Properties.Settings.Default.CapReal1LlenL3 = "";
                        Properties.Settings.Default.CapReal2LlenL3 = "";
                        Properties.Settings.Default.CapReal3LlenL3 = "";
                        Properties.Settings.Default.CapReal4LlenL3 = "";
                        Properties.Settings.Default.CapReal5LlenL3 = "";
                        Properties.Settings.Default.CapReal6LlenL3 = "";
                        Properties.Settings.Default.CapReal7LlenL3 = "";
                        Properties.Settings.Default.CapReal8LlenL3 = "";
                        Properties.Settings.Default.CapReal9LlenL3 = "";
                        Properties.Settings.Default.CapReal10LlenL3 = "";
                        Properties.Settings.Default.CapReal11LlenL3 = "";
                        Properties.Settings.Default.CapReal12LlenL3 = "";
                        Properties.Settings.Default.CapReal13LlenL3 = "";
                        Properties.Settings.Default.CapReal14LlenL3 = "";
                        Properties.Settings.Default.CapReal15LlenL3 = "";
                        Properties.Settings.Default.CapReal16LlenL3 = "";
                        Properties.Settings.Default.CapReal17LlenL3 = "";
                        Properties.Settings.Default.CapReal18LlenL3 = "";
                        Properties.Settings.Default.CapReal19LlenL3 = "";
                        Properties.Settings.Default.CapReal20LlenL3 = "";
                        Properties.Settings.Default.Save();
                        CompletarRegistros(1, Properties.Settings.Default.VolMedido1LlenL3, Properties.Settings.Default.Vol1LlenL3, Properties.Settings.Default.Error1LlenL3, Properties.Settings.Default.CapReal1LlenL3);
                        CompletarRegistros(2, Properties.Settings.Default.VolMedido2LlenL3, Properties.Settings.Default.Vol2LlenL3, Properties.Settings.Default.Error2LlenL3, Properties.Settings.Default.CapReal2LlenL3);
                        CompletarRegistros(3, Properties.Settings.Default.VolMedido3LlenL3, Properties.Settings.Default.Vol3LlenL3, Properties.Settings.Default.Error3LlenL3, Properties.Settings.Default.CapReal3LlenL3);
                        CompletarRegistros(4, Properties.Settings.Default.VolMedido4LlenL3, Properties.Settings.Default.Vol4LlenL3, Properties.Settings.Default.Error4LlenL3, Properties.Settings.Default.CapReal4LlenL3);
                        CompletarRegistros(5, Properties.Settings.Default.VolMedido5LlenL3, Properties.Settings.Default.Vol5LlenL3, Properties.Settings.Default.Error5LlenL3, Properties.Settings.Default.CapReal5LlenL3);
                        CompletarRegistros(6, Properties.Settings.Default.VolMedido6LlenL3, Properties.Settings.Default.Vol6LlenL3, Properties.Settings.Default.Error6LlenL3, Properties.Settings.Default.CapReal6LlenL3);
                        CompletarRegistros(7, Properties.Settings.Default.VolMedido7LlenL3, Properties.Settings.Default.Vol7LlenL3, Properties.Settings.Default.Error7LlenL3, Properties.Settings.Default.CapReal7LlenL3);
                        CompletarRegistros(8, Properties.Settings.Default.VolMedido8LlenL3, Properties.Settings.Default.Vol8LlenL3, Properties.Settings.Default.Error8LlenL3, Properties.Settings.Default.CapReal8LlenL3);
                        CompletarRegistros(9, Properties.Settings.Default.VolMedido9LlenL3, Properties.Settings.Default.Vol9LlenL3, Properties.Settings.Default.Error9LlenL3, Properties.Settings.Default.CapReal9LlenL3);
                        CompletarRegistros(10, Properties.Settings.Default.VolMedido10LlenL3, Properties.Settings.Default.Vol10LlenL3, Properties.Settings.Default.Error10LlenL3, Properties.Settings.Default.CapReal10LlenL3);
                        CompletarRegistros(11, Properties.Settings.Default.VolMedido11LlenL3, Properties.Settings.Default.Vol11LlenL3, Properties.Settings.Default.Error11LlenL3, Properties.Settings.Default.CapReal11LlenL3);
                        CompletarRegistros(12, Properties.Settings.Default.VolMedido12LlenL3, Properties.Settings.Default.Vol12LlenL3, Properties.Settings.Default.Error12LlenL3, Properties.Settings.Default.CapReal12LlenL3);
                        CompletarRegistros(13, Properties.Settings.Default.VolMedido13LlenL3, Properties.Settings.Default.Vol13LlenL3, Properties.Settings.Default.Error13LlenL3, Properties.Settings.Default.CapReal13LlenL3);
                        CompletarRegistros(14, Properties.Settings.Default.VolMedido14LlenL3, Properties.Settings.Default.Vol14LlenL3, Properties.Settings.Default.Error14LlenL3, Properties.Settings.Default.CapReal14LlenL3);
                        CompletarRegistros(15, Properties.Settings.Default.VolMedido15LlenL3, Properties.Settings.Default.Vol15LlenL3, Properties.Settings.Default.Error15LlenL3, Properties.Settings.Default.CapReal15LlenL3);
                        CompletarRegistros(16, Properties.Settings.Default.VolMedido16LlenL3, Properties.Settings.Default.Vol16LlenL3, Properties.Settings.Default.Error16LlenL3, Properties.Settings.Default.CapReal16LlenL3);
                        CompletarRegistros(17, Properties.Settings.Default.VolMedido17LlenL3, Properties.Settings.Default.Vol17LlenL3, Properties.Settings.Default.Error17LlenL3, Properties.Settings.Default.CapReal17LlenL3);
                        CompletarRegistros(18, Properties.Settings.Default.VolMedido18LlenL3, Properties.Settings.Default.Vol18LlenL3, Properties.Settings.Default.Error18LlenL3, Properties.Settings.Default.CapReal18LlenL3);
                        CompletarRegistros(19, Properties.Settings.Default.VolMedido19LlenL3, Properties.Settings.Default.Vol19LlenL3, Properties.Settings.Default.Error19LlenL3, Properties.Settings.Default.CapReal19LlenL3);
                        CompletarRegistros(20, Properties.Settings.Default.VolMedido20LlenL3, Properties.Settings.Default.Vol20LlenL3, Properties.Settings.Default.Error20LlenL3, Properties.Settings.Default.CapReal20LlenL3);
                    }
                    if (MaquinaLinea.numlin == 5)
                    {
                        Properties.Settings.Default.TemperaturaContVolLlenL5 = "";
                        Properties.Settings.Default.VolumenContVolLlenL5 = "";
                        Properties.Settings.Default.VolMedido1LlenL5 = "";
                        Properties.Settings.Default.VolMedido2LlenL5 = "";
                        Properties.Settings.Default.VolMedido3LlenL5 = "";
                        Properties.Settings.Default.VolMedido4LlenL5 = "";
                        Properties.Settings.Default.VolMedido5LlenL5 = "";
                        Properties.Settings.Default.VolMedido6LlenL5 = "";
                        Properties.Settings.Default.VolMedido7LlenL5 = "";
                        Properties.Settings.Default.VolMedido8LlenL5 = "";
                        Properties.Settings.Default.VolMedido9LlenL5 = "";
                        Properties.Settings.Default.VolMedido10LlenL5 = "";
                        Properties.Settings.Default.VolMedido11LlenL5 = "";
                        Properties.Settings.Default.VolMedido12LlenL5 = "";
                        Properties.Settings.Default.VolMedido13LlenL5 = "";
                        Properties.Settings.Default.VolMedido14LlenL5 = "";
                        Properties.Settings.Default.VolMedido15LlenL5 = "";
                        Properties.Settings.Default.VolMedido16LlenL5 = "";
                        Properties.Settings.Default.VolMedido17LlenL5 = "";
                        Properties.Settings.Default.VolMedido18LlenL5 = "";
                        Properties.Settings.Default.VolMedido19LlenL5 = "";
                        Properties.Settings.Default.VolMedido20LlenL5 = "";
                        Properties.Settings.Default.Vol1LlenL5 = "";
                        Properties.Settings.Default.Vol2LlenL5 = "";
                        Properties.Settings.Default.Vol3LlenL5 = "";
                        Properties.Settings.Default.Vol4LlenL5 = "";
                        Properties.Settings.Default.Vol5LlenL5 = "";
                        Properties.Settings.Default.Vol6LlenL5 = "";
                        Properties.Settings.Default.Vol7LlenL5 = "";
                        Properties.Settings.Default.Vol8LlenL5 = "";
                        Properties.Settings.Default.Vol9LlenL5 = "";
                        Properties.Settings.Default.Vol10LlenL5 = "";
                        Properties.Settings.Default.Vol11LlenL5 = "";
                        Properties.Settings.Default.Vol12LlenL5 = "";
                        Properties.Settings.Default.Vol13LlenL5 = "";
                        Properties.Settings.Default.Vol14LlenL5 = "";
                        Properties.Settings.Default.Vol15LlenL5 = "";
                        Properties.Settings.Default.Vol16LlenL5 = "";
                        Properties.Settings.Default.Vol17LlenL5 = "";
                        Properties.Settings.Default.Vol18LlenL5 = "";
                        Properties.Settings.Default.Vol19LlenL5 = "";
                        Properties.Settings.Default.Vol20LlenL5 = "";
                        Properties.Settings.Default.Error1LlenL5 = "";
                        Properties.Settings.Default.Error2LlenL5 = "";
                        Properties.Settings.Default.Error3LlenL5 = "";
                        Properties.Settings.Default.Error4LlenL5 = "";
                        Properties.Settings.Default.Error5LlenL5 = "";
                        Properties.Settings.Default.Error6LlenL5 = "";
                        Properties.Settings.Default.Error7LlenL5 = "";
                        Properties.Settings.Default.Error8LlenL5 = "";
                        Properties.Settings.Default.Error9LlenL5 = "";
                        Properties.Settings.Default.Error10LlenL5 = "";
                        Properties.Settings.Default.Error11LlenL5 = "";
                        Properties.Settings.Default.Error12LlenL5 = "";
                        Properties.Settings.Default.Error13LlenL5 = "";
                        Properties.Settings.Default.Error14LlenL5 = "";
                        Properties.Settings.Default.Error15LlenL5 = "";
                        Properties.Settings.Default.Error16LlenL5 = "";
                        Properties.Settings.Default.Error17LlenL5 = "";
                        Properties.Settings.Default.Error18LlenL5 = "";
                        Properties.Settings.Default.Error19LlenL5 = "";
                        Properties.Settings.Default.Error20LlenL5 = "";
                        Properties.Settings.Default.CapReal1LlenL5 = "";
                        Properties.Settings.Default.CapReal2LlenL5 = "";
                        Properties.Settings.Default.CapReal3LlenL5 = "";
                        Properties.Settings.Default.CapReal4LlenL5 = "";
                        Properties.Settings.Default.CapReal5LlenL5 = "";
                        Properties.Settings.Default.CapReal6LlenL5 = "";
                        Properties.Settings.Default.CapReal7LlenL5 = "";
                        Properties.Settings.Default.CapReal8LlenL5 = "";
                        Properties.Settings.Default.CapReal9LlenL5 = "";
                        Properties.Settings.Default.CapReal10LlenL5 = "";
                        Properties.Settings.Default.CapReal11LlenL5 = "";
                        Properties.Settings.Default.CapReal12LlenL5 = "";
                        Properties.Settings.Default.CapReal13LlenL5 = "";
                        Properties.Settings.Default.CapReal14LlenL5 = "";
                        Properties.Settings.Default.CapReal15LlenL5 = "";
                        Properties.Settings.Default.CapReal16LlenL5 = "";
                        Properties.Settings.Default.CapReal17LlenL5 = "";
                        Properties.Settings.Default.CapReal18LlenL5 = "";
                        Properties.Settings.Default.CapReal19LlenL5 = "";
                        Properties.Settings.Default.CapReal20LlenL5 = "";
                        Properties.Settings.Default.Save();
                        CompletarRegistros(1, Properties.Settings.Default.VolMedido1LlenL5, Properties.Settings.Default.Vol1LlenL5, Properties.Settings.Default.Error1LlenL5, Properties.Settings.Default.CapReal1LlenL5);
                        CompletarRegistros(2, Properties.Settings.Default.VolMedido2LlenL5, Properties.Settings.Default.Vol2LlenL5, Properties.Settings.Default.Error2LlenL5, Properties.Settings.Default.CapReal2LlenL5);
                        CompletarRegistros(3, Properties.Settings.Default.VolMedido3LlenL5, Properties.Settings.Default.Vol3LlenL5, Properties.Settings.Default.Error3LlenL5, Properties.Settings.Default.CapReal3LlenL5);
                        CompletarRegistros(4, Properties.Settings.Default.VolMedido4LlenL5, Properties.Settings.Default.Vol4LlenL5, Properties.Settings.Default.Error4LlenL5, Properties.Settings.Default.CapReal4LlenL5);
                        CompletarRegistros(5, Properties.Settings.Default.VolMedido5LlenL5, Properties.Settings.Default.Vol5LlenL5, Properties.Settings.Default.Error5LlenL5, Properties.Settings.Default.CapReal5LlenL5);
                        CompletarRegistros(6, Properties.Settings.Default.VolMedido6LlenL5, Properties.Settings.Default.Vol6LlenL5, Properties.Settings.Default.Error6LlenL5, Properties.Settings.Default.CapReal6LlenL5);
                        CompletarRegistros(7, Properties.Settings.Default.VolMedido7LlenL5, Properties.Settings.Default.Vol7LlenL5, Properties.Settings.Default.Error7LlenL5, Properties.Settings.Default.CapReal7LlenL5);
                        CompletarRegistros(8, Properties.Settings.Default.VolMedido8LlenL5, Properties.Settings.Default.Vol8LlenL5, Properties.Settings.Default.Error8LlenL5, Properties.Settings.Default.CapReal8LlenL5);
                        CompletarRegistros(9, Properties.Settings.Default.VolMedido9LlenL5, Properties.Settings.Default.Vol9LlenL5, Properties.Settings.Default.Error9LlenL5, Properties.Settings.Default.CapReal9LlenL5);
                        CompletarRegistros(10, Properties.Settings.Default.VolMedido10LlenL5, Properties.Settings.Default.Vol10LlenL5, Properties.Settings.Default.Error10LlenL5, Properties.Settings.Default.CapReal10LlenL5);
                        CompletarRegistros(11, Properties.Settings.Default.VolMedido11LlenL5, Properties.Settings.Default.Vol11LlenL5, Properties.Settings.Default.Error11LlenL5, Properties.Settings.Default.CapReal11LlenL5);
                        CompletarRegistros(12, Properties.Settings.Default.VolMedido12LlenL5, Properties.Settings.Default.Vol12LlenL5, Properties.Settings.Default.Error12LlenL5, Properties.Settings.Default.CapReal12LlenL5);
                        CompletarRegistros(13, Properties.Settings.Default.VolMedido13LlenL5, Properties.Settings.Default.Vol13LlenL5, Properties.Settings.Default.Error13LlenL5, Properties.Settings.Default.CapReal13LlenL5);
                        CompletarRegistros(14, Properties.Settings.Default.VolMedido14LlenL5, Properties.Settings.Default.Vol14LlenL5, Properties.Settings.Default.Error14LlenL5, Properties.Settings.Default.CapReal14LlenL5);
                        CompletarRegistros(15, Properties.Settings.Default.VolMedido15LlenL5, Properties.Settings.Default.Vol15LlenL5, Properties.Settings.Default.Error15LlenL5, Properties.Settings.Default.CapReal15LlenL5);
                        CompletarRegistros(16, Properties.Settings.Default.VolMedido16LlenL5, Properties.Settings.Default.Vol16LlenL5, Properties.Settings.Default.Error16LlenL5, Properties.Settings.Default.CapReal16LlenL5);
                        CompletarRegistros(17, Properties.Settings.Default.VolMedido17LlenL5, Properties.Settings.Default.Vol17LlenL5, Properties.Settings.Default.Error17LlenL5, Properties.Settings.Default.CapReal17LlenL5);
                        CompletarRegistros(18, Properties.Settings.Default.VolMedido18LlenL5, Properties.Settings.Default.Vol18LlenL5, Properties.Settings.Default.Error18LlenL5, Properties.Settings.Default.CapReal18LlenL5);
                        CompletarRegistros(19, Properties.Settings.Default.VolMedido19LlenL5, Properties.Settings.Default.Vol19LlenL5, Properties.Settings.Default.Error19LlenL5, Properties.Settings.Default.CapReal19LlenL5);
                        CompletarRegistros(20, Properties.Settings.Default.VolMedido20LlenL5, Properties.Settings.Default.Vol20LlenL5, Properties.Settings.Default.Error20LlenL5, Properties.Settings.Default.CapReal20LlenL5);
                    }
                    if (VolTeorico20TB.Text == "")
                    {
                        //En el momento que todos los campos esten completados realizamos los cálculos
                        MediaTB.Text = "";
                        VarianzaTB.Text = "";
                        DesviacionTipicaTB.Text = "";
                        RealDecretoTB.Text = "";
                        BOEn266PB.BackgroundImage = null;
                        //Ocultamos el mensaje de aviso
                        AvisoLB.Hide();
                    }
                }
            }
            else
            {
                MessageBox.Show("FALTAN CAMPOS POR RELLENAR! -> NO SE HA GUARDADO LA INFORMACIÓN");
            }
        }

    }
}

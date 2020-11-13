using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using WHPS.Model;
using WHPS.ProgramMenus;
using WHPS.Utiles;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace WHPS.Despaletizador
{
    public partial class Despaletizador_Botellas : Form
    {
        //Variable que indica que el modo manual (TRUE) o no (FALSE)
        public bool modo_manual = false;
        MainDespaletizador parent;
        public Despaletizador_Botellas(MainDespaletizador p)
        {
            InitializeComponent();
            parent = p;
        }

        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>
        /// <param name="BackL+numlin">Parámetro que identifica a cual form hijo de WHPST_INICIO debe volver en función del número de línea.</param>
        private void ExitB_Click(object sender, EventArgs e)
        {
            Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainDespaletizador));
            this.Hide();
            this.Dispose();
        }

        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }



        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        private void Despaletizador_Botellas_Load(object sender, EventArgs e)
        {
            //Muestra la hora ya que el timer tarda 1s en tomar el control del Label lbReloj.
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");


            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //Rellenamos los parámetros iniciales
            dateTB.Text = DateTime.Now.ToString("dd/MM/yyyy");
            turnoTB.Text = Utilidades.ObtenerTurnoActual();
            respTB.Text = MaquinaLinea.Responsable;
            maqTB.Text = MaquinaLinea.MDespaletizador;

            //Seleccionamos directamente el campo de texto y etablecemos el modo no manual
            InputTB.Select();
            modo_manual = false;
        }

        /// <summary>
        /// Función del temporizador que se ejecuta cada segundo, indíca la hora y si se activa la alarma. 
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Activa la alarma cuando la hora marcada es la misma que la que se muestra en pantalla.
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }
            //if (MaquinaLinea.StatusTeclado == true) Utilidades.EscribirTeclado(numberpad1, InputTB, null);
        }

        //########################   RELLENAR EL CÓDIGO   ########################
        /// <summary>
        /// El lector introduce el código y aplica un enter, cuando este de detecte ejecuta la acción. 
        /// </summary>
        private void InputTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Datos_Botellas datos_bot = new Datos_Botellas();
                datos_bot = Apps_Despaletizador.ParsingCod_Botellas(InputTB.Text);

                //Rellenamos todos los datos que han sido identificados
                if (datos_bot.ean != "" )
                {
                    if (eanTB.Text != "")
                    {
                        eanTB.Text = "";
                        refwhTB.Text = "";
                        provTB.Text = "";
                        fabdateTB.Text = "";
                        loteTB.Text = "";
                        DescTB.Text = "";
                        cantidadTB.Text = "";
                    }
                    eanTB.Text = datos_bot.ean;
                    DescTB.Text = datos_bot.whDescrip;
                    refwhTB.Text = datos_bot.refInt;
                    provTB.Text = datos_bot.Proveedor;
                    fabdateTB.Text = datos_bot.FechaFab;
                    if (loteTB.Text == "") loteTB.Text = datos_bot.LoteFab;
                    cantidadTB.Text = datos_bot.Cantidad;
                    MaquinaLinea.NumBot = datos_bot.Cantidad;
                }

                if (datos_bot.SSCC != "")
                {
                    if (ssccTB.Text != "")
                    {
                        loteTB.Text = "";
                        ssccTB.Text = "";
                    }
                    ssccTB.Text = datos_bot.SSCC;
                    if (loteTB.Text == "") loteTB.Text = datos_bot.LoteFab;
                }
                if (datos_bot.eanTM != "")
                {
                    DescTMTB.Text = datos_bot.whDescrip;
                    eanTMTB.Text = datos_bot.eanTM;
                    refwhTMTB.Text = datos_bot.refInt;
                    provTMTB.Text = datos_bot.Proveedor;
                    fabdateTMTB.Text = datos_bot.FechaFabTM;
                    loteTMTB.Text = datos_bot.LoteFabTM;
                    TMLB.Visible = true;
                    DescTMTB.Visible = true;
                    eanTMTB.Visible = true;
                    refwhTMTB.Visible = true;
                    provTMTB.Visible = true;
                    fabdateTMTB.Visible = true;
                    loteTMTB.Visible = true;
                    //turnoTB.Size = new Size(246, 22);
                    //DescTB.Size = new Size(246, 22);
                    //eanTB.Size = new Size(246, 22);
                    //provTB.Size = new Size(246, 22);
                    //refwhTB.Size = new Size(246, 22);
                    //fabdateTB.Size = new Size(246, 22);
                    //loteTB.Size = new Size(246, 22);
                }
                InputTB.Text = "";
            }
        }

        /// <summary>
        /// Si el lector no funciona, se posibilita abrir el teclado  haciendo click en el TB cuando el modo manual esta activado.
        /// </summary>
        private void InputTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (modo_manual == true)
            {
                WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,InputTB);

            }
        }

        /// <summary>
        /// Boton que activa o desactiva el modomaual.
        /// </summary>
        private void ModoManualBot_Click(object sender, EventArgs e)
        {
            if (modo_manual == true)
            {
                ModoManualBot.BackColor = Color.FromArgb(27, 33, 41);
                modo_manual = false;
            }
            else
            {
                ModoManualBot.BackColor = Color.DarkSeaGreen;
                modo_manual = true;
            }
        }
        /// <summary>
        /// Boton que envia el códgo para hacer el parsing.
        /// </summary>
        private void EnviarB_Click(object sender, EventArgs e)
        {
            InputTB.Select();
            SendKeys.Send("{ENTER}");
        }
        //########################################################################

        //########################   GUARDAR LOS DATOS   #########################
        private void saveBot_Click(object sender, EventArgs e)
        {
            if ((eanTB.Text != "") && (refwhTB.Text != "") && (provTB.Text != "") && (fabdateTB.Text != "") && (loteTB.Text != ""))
            {
                List<string[]> listavalores = new List<string[]>();
                string[] valores0 = new string[2];
                string[] valores1 = new string[2];
                string[] valores01 = new string[2];
                string[] valores2 = new string[2];
                string[] valores3 = new string[2];
                string[] valores4 = new string[2];
                string[] valores5 = new string[2];
                string[] valores6 = new string[2];
                string[] valores7 = new string[2];
                string[] valores8 = new string[2];
                string[] valores9 = new string[2];
                string[] valores10 = new string[2];
                string[] valores11 = new string[2];
                string[] valores12 = new string[2];

                valores0[0] = "Fecha";
                valores0[1] = DateTime.Now.ToString("dd/MM/yyyy");
                listavalores.Add(valores0);
                valores1[0] = "Hora";
                valores1[1] = DateTime.Now.ToString("HH:mm:ss");
                listavalores.Add(valores1);
                valores01[0] = "FechaDB";
                valores01[1] = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
                listavalores.Add(valores01);
                valores2[0] = "Responsable";
                valores2[1] = MaquinaLinea.Responsable;
                listavalores.Add(valores2);
                valores3[0] = "Maquinista";
                valores3[1] = MaquinaLinea.MDespaletizador;
                listavalores.Add(valores3);
                valores4[0] = "Turno";
                valores4[1] = turnoTB.Text;
                listavalores.Add(valores4);
                valores5[0] = "EAN";
                valores5[1] = eanTB.Text;
                listavalores.Add(valores5);
                valores6[0] = "RefInterna";
                valores6[1] = refwhTB.Text;
                listavalores.Add(valores6);
                valores7[0] = "Proveedor";
                valores7[1] = provTB.Text;
                listavalores.Add(valores7);
                valores8[0] = "FechaFab";
                valores8[1] = fabdateTB.Text;
                listavalores.Add(valores8);
                valores9[0] = "LoteFab";
                valores9[1] = loteTB.Text;
                listavalores.Add(valores9);
                valores10[0] = "Descripcion";
                valores10[1] = DescTB.Text;
                listavalores.Add(valores10);
                valores11[0] = "SSCC";
                valores11[1] = ssccTB.Text;
                listavalores.Add(valores11);
                valores12[0] = "Cantidad";
                valores12[1] = cantidadTB.Text;
                listavalores.Add(valores12);
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileDespaletizador, "Botellas", listavalores, "Id");

                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show(Properties.Settings.Default.AvisoProblemaFichero);
                }
                //Si los datos se han guardado correctamente incrementamos el contador de botellas y salimos del form
                else
                {
                    if (MaquinaLinea.numlin == 2)
                    {
                        Properties.Settings.Default.DPNumBotDespL2 = Properties.Settings.Default.DPNumBotDespL2 + Convert.ToInt16(MaquinaLinea.NumBot);
                        Properties.Settings.Default.Save();
                    }
                    if (MaquinaLinea.numlin == 3)
                    {
                        Properties.Settings.Default.DPNumBotDespL3 = Properties.Settings.Default.DPNumBotDespL3 + Convert.ToInt16(MaquinaLinea.NumBot);
                        Properties.Settings.Default.Save();
                    }
                    if (MaquinaLinea.numlin == 5)
                    {
                        Properties.Settings.Default.DPNumBotDespL5 = Properties.Settings.Default.DPNumBotDespL5 + Convert.ToInt16(MaquinaLinea.NumBot);
                        Properties.Settings.Default.Save();
                    }

                    //MessageBox.Show(salida);
                    eanTB.Text = "";
                    refwhTB.Text = "";
                    provTB.Text = "";
                    fabdateTB.Text = "";
                    loteTB.Text = "";
                    DescTB.Text = "";
                    cantidadTB.Text = "";
                    ssccTB.Text = "";
                    InputTB.Select();
                }
            }
            else
            {
                MessageBox.Show(Properties.Settings.Default.AvisoCampos);
                InputTB.Select();
            }
        }

    /*    private void numberpad1_VisibleChanged(object sender, EventArgs e)
        {
            if (MaquinaLinea.StatusTeclado == true) InputTB.Text = Utilidades.EscribirTeclado(numberpad1, InputTB, null);
        }*/
        //########################################################################
    }
}

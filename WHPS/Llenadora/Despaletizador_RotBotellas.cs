﻿using System;
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

namespace WHPS.Despaletizador
{
    public partial class Despaletizador_RotBotellas : Form
    {
        //Variables del form
        string botrotas;
        string limpieza_area = "";
        string limpieza_trab = "";
        string Turno = "";

        public Despaletizador_RotBotellas()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>
        private void ExitB_Click(object sender, EventArgs e)
        {
            MainDespaletizador Form = new MainDespaletizador();
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
        private void Despaletizador_RotBotellas_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            //numberpad1.Visible = false;
            Turno = Utilidades.ObtenerTurnoActual();

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;


            //Ocultamos los cuadros que no son necesarios
            DatosRoturaBOX.Hide();
            ReferenciaBotellasBOX.Hide();
        }

        //Un temporizador, nos sincroniza con la pantalla del main para que si al volver ha se ha activado la alarma nos avise
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Para activar la alarma debe, estar desactivada, haberse chequeado el inicio de turno y que la hora cuadre con la alarma 
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }

            //if (MaquinaLinea.StatusTeclado == true) Utilidades.EscribirTeclado(numberpad1, numrotasTB, ConfrRespB);

        }

        //Botones con los que rellenamos los diferentes campos
        private void BotRotas_SI_B_Click(object sender, EventArgs e)
        {
            botrotas = "SI";
            ReferenciaBotellasBOX.Hide();
            DatosRoturaBOX.Show();
            BotRotas_NO_B.BackColor = Color.LightGray;
            BotRotas_SI_B.BackColor = Color.DarkSeaGreen;
        }
        private void BotRotas_NO_B_Click(object sender, EventArgs e)
        {

            BotRotas_NO_B.BackColor = Color.IndianRed;
            BotRotas_SI_B.BackColor = Color.LightGray;
            botrotas = "NO";
            limpieza_area = "NO";
            limpieza_trab = "NO";
            numrotasTB.Text = "0";

            DatosRoturaBOX.Hide();
            RegistarReferenciaBotellas();
        }
        private void InspArea_SI_B_Click(object sender, EventArgs e)
        {
            limpieza_area = "SI";
            InspArea_SI_B.BackColor = Color.DarkSeaGreen;
            InspArea_NO_B.BackColor = Color.LightGray;

        }
        private void InspArea_NO_B_Click(object sender, EventArgs e)
        {
            limpieza_area = "NO";
            InspArea_NO_B.BackColor = Color.IndianRed;
            InspArea_SI_B.BackColor = Color.LightGray;

        }
        private void InspTrab_SI_B_Click(object sender, EventArgs e)
        {
            limpieza_trab = "SI";
            InspTrab_SI_B.BackColor = Color.DarkSeaGreen;
            InspTrab_NO_B.BackColor = Color.LightGray;
        }
        private void InspTrab_NO_B_Click(object sender, EventArgs e)
        {
            limpieza_trab = "NO";
            InspTrab_NO_B.BackColor = Color.IndianRed;
            InspTrab_SI_B.BackColor = Color.LightGray;
        }
        private void ConfrRespB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == false && ContraseñaTB.Text == "")
            {
                Utilidades.MostrarTecladoPredeterminado(null);
                ContraseñaTB.Visible = true;
                ContraseñaTB.UseSystemPasswordChar = true;
                ContraseñaTB.Select();
            }
            if (MaquinaLinea.TecladoWindows == true)
            {
                Utilidades.ParametrosTeclado(true, 0);
                //numberpad1.Location = new Point(450, 250);
                //numberpad1.Visible = true;
            }
        }

        private void ContraseñaTB_KeyDown(object sender, KeyEventArgs e)
        {
            string Contraseña = MaquinaLinea.ContraseñaEncargado;
            if (e.KeyCode == Keys.Enter)
            {
                ContraseñaTB.Visible = false;
                ContraseñaTB.UseSystemPasswordChar = false;

                switch (ContraseñaTB.Text)
                {
                    case "1877":
                        ConfrRespB.BackColor = Color.DarkSeaGreen;
                        MaquinaLinea.Password = true;
                        ContraseñaTB.Text = "";
                        break;
                    default:
                        ConfrRespB.BackColor = Color.IndianRed;
                        MaquinaLinea.Password = false;
                        ContraseñaTB.Text = "";
                        break;
                }
            }
        }
        //Cuando clickeamos en el textbox aparecerá un teclado manual
        private void numrotasTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == false) Utilidades.MostrarTecladoPredeterminado(numrotasTB);
            else
            {
                Utilidades.ParametrosTeclado(false, 0);
                //numberpad1.Location = new Point(450, 250);
                //numberpad1.Visible = true;
            }
        }
        //private void numberpad1_VisibleChanged(object sender, EventArgs e)
        //{
        //    if (MaquinaLinea.StatusTeclado == true) Utilidades.EscribirTeclado(numberpad1, numrotasTB, ConfrRespB);
        //}

        private void RegistarReferenciaBotellas()
        {
            ReferenciaBotellasBOX.Show();
            if (MaquinaLinea.UltBotella_Descp != "" && MaquinaLinea.UltBotella_Ref != "")
            {
                DescripcionTB.Text = MaquinaLinea.UltBotella_Descp;
                ReferenciaTB.Text = MaquinaLinea.UltBotella_Ref;
            }
            else
            {
                DescripcionTB.Text ="";
                ReferenciaTB.Text = "";
            }
        }

        //Si la contraseña es correcta y han sido rellenados todos los campos se guardará la información
        private void saveBot_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.Password == true && RoturaBotellasTB.Text !="")
            {

                string salida = MaquinaLinea.GuardarFormRoturaBotellas("Despaletizador", MaquinaLinea.MDespaletizador, botrotas, RoturaBotellasTB.Text, limpieza_area, limpieza_trab);
                //Si existe algun error de salvado de datos se expondrá en un MESSAGE 
                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                    //MessageBox.Show(salida); 
                }
                //Cuando los datos han sido salvados correctamente reestablecemos los parámetros
                else
                {
                    MaquinaLinea.Password = false;

                    MainDespaletizador Form = new MainDespaletizador();
                    Hide();
                    Form.Show();
                    GC.Collect();
                }
            }
        }
        //########## EN CASO DE QUE NO TENGAMOS REFERENCIA DE BOTELLAS ###########
        private void groupBox4_Enter(object sender, EventArgs e)
        {
            InputTB.Select();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (InputTB.Text != "")
                {
                    ReferenciaTB.Text = MaquinaLinea.UltBotella_Ref;
                    DescripcionTB.Text = MaquinaLinea.UltBotella_Descp;
                    ReferenciaBotellasBOX.Hide();
                }
                else
                {
                    InputTB.Text = "";
                }
            }
        }
    }
}

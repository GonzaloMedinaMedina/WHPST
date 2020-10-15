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
using System.Diagnostics;
using System.IO;


namespace WHPS.Llenadora
{
    public partial class Llenadora_Control30m : Form
    {
        //Declaramos las variable que iran registrando los valores en los diferentes textbox
        public string ControlCierre = "";
        public string Volumen = "";
        public string CuelloBoca = "";
        public string SensorSuperior = "";
        public string NivelVolumen = "";
        public string Registro = "Check";

        public Llenadora_Control30m()
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


        private void Llenadora_Control30m_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //Rellenamos fecha
            dateTB.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //Rellenamos responsable y maquinista
            respTB.Text = MaquinaLinea.Responsable;
            maqTB.Text = MaquinaLinea.MLlenadora;

            //Rellenamos el turno - Identificando el turno
            turnoTB.Text = Utilidades.ObtenerTurnoActual();

            //Cargamos los controles realizados en la tabla.
            if (MaquinaLinea.numlin == 2)
            {
                //Introducimos la hora
                HoraControl1TB.Text = Properties.Settings.Default.HoraControlLlen1L2;
                HoraControl2TB.Text = Properties.Settings.Default.HoraControlLlen2L2;
                HoraControl3TB.Text = Properties.Settings.Default.HoraControlLlen3L2;
                HoraControl4TB.Text = Properties.Settings.Default.HoraControlLlen4L2;
                HoraControl5TB.Text = Properties.Settings.Default.HoraControlLlen5L2;
                HoraControl6TB.Text = Properties.Settings.Default.HoraControlLlen6L2;
                HoraControl7TB.Text = Properties.Settings.Default.HoraControlLlen7L2;
                HoraControl8TB.Text = Properties.Settings.Default.HoraControlLlen8L2;
                HoraControl9TB.Text = Properties.Settings.Default.HoraControlLlen9L2;
                HoraControl10TB.Text = Properties.Settings.Default.HoraControlLlen10L2;
                HoraControl11TB.Text = Properties.Settings.Default.HoraControlLlen11L2;
                HoraControl12TB.Text = Properties.Settings.Default.HoraControlLlen12L2;
                HoraControl13TB.Text = Properties.Settings.Default.HoraControlLlen13L2;
                HoraControl14TB.Text = Properties.Settings.Default.HoraControlLlen14L2;
                HoraControl15TB.Text = Properties.Settings.Default.HoraControlLlen15L2;
                HoraControl16TB.Text = Properties.Settings.Default.HoraControlLlen16L2;
            }

            if (MaquinaLinea.numlin == 3)
            {
                //Introducimos la hora
                HoraControl1TB.Text = Properties.Settings.Default.HoraControlLlen1L3;
                HoraControl2TB.Text = Properties.Settings.Default.HoraControlLlen2L3;
                HoraControl3TB.Text = Properties.Settings.Default.HoraControlLlen3L3;
                HoraControl4TB.Text = Properties.Settings.Default.HoraControlLlen4L3;
                HoraControl5TB.Text = Properties.Settings.Default.HoraControlLlen5L3;
                HoraControl6TB.Text = Properties.Settings.Default.HoraControlLlen6L3;
                HoraControl7TB.Text = Properties.Settings.Default.HoraControlLlen7L3;
                HoraControl8TB.Text = Properties.Settings.Default.HoraControlLlen8L3;
                HoraControl9TB.Text = Properties.Settings.Default.HoraControlLlen9L3;
                HoraControl10TB.Text = Properties.Settings.Default.HoraControlLlen10L3;
                HoraControl11TB.Text = Properties.Settings.Default.HoraControlLlen11L3;
                HoraControl12TB.Text = Properties.Settings.Default.HoraControlLlen12L3;
                HoraControl13TB.Text = Properties.Settings.Default.HoraControlLlen13L3;
                HoraControl14TB.Text = Properties.Settings.Default.HoraControlLlen14L3;
                HoraControl15TB.Text = Properties.Settings.Default.HoraControlLlen15L3;
                HoraControl16TB.Text = Properties.Settings.Default.HoraControlLlen16L3;
            }
            if (MaquinaLinea.numlin == 5)
            {
                //Introducimos la hora
                HoraControl1TB.Text = Properties.Settings.Default.HoraControlLlen1L5;
                HoraControl2TB.Text = Properties.Settings.Default.HoraControlLlen2L5;
                HoraControl3TB.Text = Properties.Settings.Default.HoraControlLlen3L5;
                HoraControl4TB.Text = Properties.Settings.Default.HoraControlLlen4L5;
                HoraControl5TB.Text = Properties.Settings.Default.HoraControlLlen5L5;
                HoraControl6TB.Text = Properties.Settings.Default.HoraControlLlen6L5;
                HoraControl7TB.Text = Properties.Settings.Default.HoraControlLlen7L5;
                HoraControl8TB.Text = Properties.Settings.Default.HoraControlLlen8L5;
                HoraControl9TB.Text = Properties.Settings.Default.HoraControlLlen9L5;
                HoraControl10TB.Text = Properties.Settings.Default.HoraControlLlen10L5;
                HoraControl11TB.Text = Properties.Settings.Default.HoraControlLlen11L5;
                HoraControl12TB.Text = Properties.Settings.Default.HoraControlLlen12L5;
                HoraControl13TB.Text = Properties.Settings.Default.HoraControlLlen13L5;
                HoraControl14TB.Text = Properties.Settings.Default.HoraControlLlen14L5;
                HoraControl15TB.Text = Properties.Settings.Default.HoraControlLlen15L5;
                HoraControl16TB.Text = Properties.Settings.Default.HoraControlLlen16L5;
            }

            //Introducimos la el registro
            if (HoraControl1TB.Text != "") { RegControl1TB.Text = Registro; }
            if (HoraControl2TB.Text != "") { RegControl2TB.Text = Registro; }
            if (HoraControl3TB.Text != "") { RegControl3TB.Text = Registro; }
            if (HoraControl4TB.Text != "") { RegControl4TB.Text = Registro; }
            if (HoraControl5TB.Text != "") { RegControl5TB.Text = Registro; }
            if (HoraControl6TB.Text != "") { RegControl6TB.Text = Registro; }
            if (HoraControl7TB.Text != "") { RegControl7TB.Text = Registro; }
            if (HoraControl8TB.Text != "") { RegControl8TB.Text = Registro; }
            if (HoraControl9TB.Text != "") { RegControl9TB.Text = Registro; }
            if (HoraControl10TB.Text != "") { RegControl10TB.Text = Registro; }
            if (HoraControl11TB.Text != "") { RegControl11TB.Text = Registro; }
            if (HoraControl12TB.Text != "") { RegControl12TB.Text = Registro; }
            if (HoraControl13TB.Text != "") { RegControl13TB.Text = Registro; }
            if (HoraControl14TB.Text != "") { RegControl14TB.Text = Registro; }
            if (HoraControl15TB.Text != "") { RegControl15TB.Text = Registro; }
            if (HoraControl16TB.Text != "") { RegControl16TB.Text = Registro; }
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

        //Para cargar la hora, el operario tiene que hacer click en el textbox correspondiente
        private void HoraControlLlenTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (HoraControlTB.Text != "")
            {
                DialogResult opcion;
                opcion = MessageBox.Show("¿Estas seguro de sobrescribir los datos? Perderá la informacion", "", MessageBoxButtons.OKCancel);
                if (opcion == DialogResult.OK)
                {
                    if (MaquinaLinea.numlin == 2)
                    {
                        HoraControlTB.Text = DateTime.Now.ToString("HH:mm:ss");
                    }
                    if (MaquinaLinea.numlin == 3)
                    {
                        HoraControlTB.Text = DateTime.Now.ToString("HH:mm:ss");
                    }
                    if (MaquinaLinea.numlin == 5)
                    {
                        HoraControlTB.Text = DateTime.Now.ToString("HH:mm:ss");
                    }
                }
            }
            if (HoraControlTB.Text == "")
            {
                HoraControlTB.Text = DateTime.Now.ToString("HH:mm:ss");
            }
        }

        //Chequeamos el estado pulasando los botones correspondientes
        private void ControlCierre_OK_B_Click(object sender, EventArgs e)
        {
            ControlCierre = "OK";
            ControlCierre_OK_B.BackColor = Color.DarkSeaGreen;
            ControlCierre_NOOK_B.BackColor = Color.LightGray;
        }
        private void ControlCierre_NOOK_B_Click(object sender, EventArgs e)
        {
            ControlCierre = "NO OK";
            ControlCierre_NOOK_B.BackColor = Color.IndianRed;
            ControlCierre_OK_B.BackColor = Color.LightGray;
        }
        private void Volumen_OK_B_Click(object sender, EventArgs e)
        {
            Volumen = "OK";
            Volumen_OK_B.BackColor = Color.DarkSeaGreen;
            Volumen_NOOK_B.BackColor = Color.LightGray;
        }
        private void Volumen_NOOK_B_Click(object sender, EventArgs e)
        {
            Volumen = "NO OK";
            Volumen_NOOK_B.BackColor = Color.IndianRed;
            Volumen_OK_B.BackColor = Color.LightGray;
        }
        private void CuelloBoca_OK_B_Click(object sender, EventArgs e)
        {
            CuelloBoca = "OK";
            CuelloBoca_OK_B.BackColor = Color.DarkSeaGreen;
            CuelloBoca_NOOK_B.BackColor = Color.LightGray;
        }
        private void CuelloBoca_NOOK_B_Click(object sender, EventArgs e)
        {
            CuelloBoca = "NO OK";
            CuelloBoca_NOOK_B.BackColor = Color.IndianRed;
            CuelloBoca_OK_B.BackColor = Color.LightGray;
        }
        private void ComentariosTB_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //Process.Start(Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "Sysnative", "cmd.exe"), "/c osk.exe" + " & exit");
                Process p1 = new Process();
                p1.StartInfo.FileName = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "Sysnative", "cmd.exe");
                p1.StartInfo.Arguments = "/c osk.exe";
                p1.StartInfo.UseShellExecute = false;
                p1.StartInfo.CreateNoWindow = true;

                p1.Start();

                p1.WaitForExit(100);
                p1.Close();
            }
            catch { }
            //Hacemos activo el campo de texto y marcamos el cursor para la selección
            Activate();
            ComentariosTB.Select();
        }

        //Una vez esté completado el control, guardamos y en caso de que algun elemento no este correcto
        //se abrirá el form de comentarios para que se regitre la incidencia.

        private void saveBot_Click(object sender, EventArgs e)
        {

        
            if (HoraControlTB.Text != "" && ControlCierre != "" && Volumen != "" && CuelloBoca != "")
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
                valores3[1] = MaquinaLinea.MLlenadora;
                listavalores.Add(valores3);
                valores4[0] = "Turno";
                valores4[1] = turnoTB.Text;
                listavalores.Add(valores4);
                valores5[0] = "HoraControl";
                valores5[1] = HoraControlTB.Text;
                listavalores.Add(valores5);
                valores6[0] = "ControlCierre";
                valores6[1] = ControlCierre;
                listavalores.Add(valores6);
                valores7[0] = "Volumen";
                valores7[1] = Volumen;
                listavalores.Add(valores7);
                valores8[0] = "Cuelloboca";
                valores8[1] = CuelloBoca;
                listavalores.Add(valores8);
                valores9[0] = "Comentarios";
                valores9[1] = ComentariosTB.Text;
                listavalores.Add(valores9);

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileLlenadora, "Control30min", listavalores, "Id");
                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                }

                //Registramos que ha sido guardado correctamente
                if (MaquinaLinea.numlin == 2)
                {
                    if (HoraControl15TB.Text != "" && HoraControl16TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen16L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl16TB.Text = Properties.Settings.Default.HoraControlLlen16L2;
                        RegControl16TB.Text = Registro;

                    }
                    if (HoraControl14TB.Text != "" && HoraControl15TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen15L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl15TB.Text = Properties.Settings.Default.HoraControlLlen15L2;
                        RegControl15TB.Text = Registro;

                    }
                    if (HoraControl13TB.Text != "" && HoraControl14TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen14L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl14TB.Text = Properties.Settings.Default.HoraControlLlen14L2;
                        RegControl14TB.Text = Registro;

                    }
                    if (HoraControl12TB.Text != "" && HoraControl13TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen13L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl13TB.Text = Properties.Settings.Default.HoraControlLlen13L2;
                        RegControl13TB.Text = Registro;

                    }
                    if (HoraControl11TB.Text != "" && HoraControl12TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen12L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl12TB.Text = Properties.Settings.Default.HoraControlLlen12L2;
                        RegControl12TB.Text = Registro;

                    }
                    if (HoraControl10TB.Text != "" && HoraControl11TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen11L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl11TB.Text = Properties.Settings.Default.HoraControlLlen11L2;
                        RegControl11TB.Text = Registro;

                    }
                    if (HoraControl9TB.Text != "" && HoraControl10TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen10L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl10TB.Text = Properties.Settings.Default.HoraControlLlen10L2;
                        RegControl10TB.Text = Registro;

                    }
                    if (HoraControl8TB.Text != "" && HoraControl9TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen9L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl9TB.Text = Properties.Settings.Default.HoraControlLlen9L2;
                        RegControl9TB.Text = Registro;

                    }
                    if (HoraControl7TB.Text != "" && HoraControl8TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen8L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl8TB.Text = Properties.Settings.Default.HoraControlLlen8L2;
                        RegControl8TB.Text = Registro;

                    }
                    if (HoraControl6TB.Text != "" && HoraControl7TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen7L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl7TB.Text = Properties.Settings.Default.HoraControlLlen7L2;
                        RegControl7TB.Text = Registro;

                    }
                    if (HoraControl5TB.Text != "" && HoraControl6TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen6L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl6TB.Text = Properties.Settings.Default.HoraControlLlen6L2;
                        RegControl6TB.Text = Registro;

                    }
                    if (HoraControl4TB.Text != "" && HoraControl5TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen5L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl5TB.Text = Properties.Settings.Default.HoraControlLlen5L2;
                        RegControl5TB.Text = Registro;

                    }
                    if (HoraControl3TB.Text != "" && HoraControl4TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen4L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl4TB.Text = Properties.Settings.Default.HoraControlLlen4L2;
                        RegControl4TB.Text = Registro;

                    }
                    if (HoraControl2TB.Text != "" && HoraControl3TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen3L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl3TB.Text = Properties.Settings.Default.HoraControlLlen3L2;
                        RegControl3TB.Text = Registro;

                    }
                    if (HoraControl1TB.Text != "" && HoraControl2TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen2L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl2TB.Text = Properties.Settings.Default.HoraControlLlen2L2;
                        RegControl2TB.Text = Registro;
                    }
                    if (HoraControl1TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen1L2 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl1TB.Text = Properties.Settings.Default.HoraControlLlen1L2;
                        RegControl1TB.Text = Registro;
                    }
                }

                if (MaquinaLinea.numlin == 3)
                {
                    if (HoraControl15TB.Text != "" && HoraControl16TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen16L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl16TB.Text = Properties.Settings.Default.HoraControlLlen16L3;
                        RegControl16TB.Text = Registro;

                    }
                    if (HoraControl14TB.Text != "" && HoraControl15TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen15L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl15TB.Text = Properties.Settings.Default.HoraControlLlen15L3;
                        RegControl15TB.Text = Registro;

                    }
                    if (HoraControl13TB.Text != "" && HoraControl14TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen14L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl14TB.Text = Properties.Settings.Default.HoraControlLlen14L3;
                        RegControl14TB.Text = Registro;

                    }
                    if (HoraControl12TB.Text != "" && HoraControl13TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen13L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl13TB.Text = Properties.Settings.Default.HoraControlLlen13L3;
                        RegControl13TB.Text = Registro;

                    }
                    if (HoraControl11TB.Text != "" && HoraControl12TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen12L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl12TB.Text = Properties.Settings.Default.HoraControlLlen12L3;
                        RegControl12TB.Text = Registro;

                    }
                    if (HoraControl10TB.Text != "" && HoraControl11TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen11L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl11TB.Text = Properties.Settings.Default.HoraControlLlen11L3;
                        RegControl11TB.Text = Registro;

                    }
                    if (HoraControl9TB.Text != "" && HoraControl10TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen10L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl10TB.Text = Properties.Settings.Default.HoraControlLlen10L3;
                        RegControl10TB.Text = Registro;

                    }
                    if (HoraControl8TB.Text != "" && HoraControl9TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen9L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl9TB.Text = Properties.Settings.Default.HoraControlLlen9L3;
                        RegControl9TB.Text = Registro;

                    }
                    if (HoraControl7TB.Text != "" && HoraControl8TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen8L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl8TB.Text = Properties.Settings.Default.HoraControlLlen8L3;
                        RegControl8TB.Text = Registro;

                    }
                    if (HoraControl6TB.Text != "" && HoraControl7TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen7L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl7TB.Text = Properties.Settings.Default.HoraControlLlen7L3;
                        RegControl7TB.Text = Registro;

                    }
                    if (HoraControl5TB.Text != "" && HoraControl6TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen6L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl6TB.Text = Properties.Settings.Default.HoraControlLlen6L3;
                        RegControl6TB.Text = Registro;

                    }
                    if (HoraControl4TB.Text != "" && HoraControl5TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen5L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl5TB.Text = Properties.Settings.Default.HoraControlLlen5L3;
                        RegControl5TB.Text = Registro;

                    }
                    if (HoraControl3TB.Text != "" && HoraControl4TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen4L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl4TB.Text = Properties.Settings.Default.HoraControlLlen4L3;
                        RegControl4TB.Text = Registro;

                    }
                    if (HoraControl2TB.Text != "" && HoraControl3TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen3L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl3TB.Text = Properties.Settings.Default.HoraControlLlen3L3;
                        RegControl3TB.Text = Registro;

                    }
                    if (HoraControl1TB.Text != "" && HoraControl2TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen2L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl2TB.Text = Properties.Settings.Default.HoraControlLlen2L3;
                        RegControl2TB.Text = Registro;
                    }
                    if (HoraControl1TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen1L3 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl1TB.Text = Properties.Settings.Default.HoraControlLlen1L3;
                        RegControl1TB.Text = Registro;
                    }
                }
                if (MaquinaLinea.numlin == 5)
                {
                    if (HoraControl15TB.Text != "" && HoraControl16TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen16L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl16TB.Text = Properties.Settings.Default.HoraControlLlen16L5;
                        RegControl16TB.Text = Registro;

                    }
                    if (HoraControl14TB.Text != "" && HoraControl15TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen15L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl15TB.Text = Properties.Settings.Default.HoraControlLlen15L5;
                        RegControl15TB.Text = Registro;

                    }
                    if (HoraControl13TB.Text != "" && HoraControl14TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen14L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl14TB.Text = Properties.Settings.Default.HoraControlLlen14L5;
                        RegControl14TB.Text = Registro;

                    }
                    if (HoraControl12TB.Text != "" && HoraControl13TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen13L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl13TB.Text = Properties.Settings.Default.HoraControlLlen13L5;
                        RegControl13TB.Text = Registro;

                    }
                    if (HoraControl11TB.Text != "" && HoraControl12TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen12L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl12TB.Text = Properties.Settings.Default.HoraControlLlen12L5;
                        RegControl12TB.Text = Registro;

                    }
                    if (HoraControl10TB.Text != "" && HoraControl11TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen11L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl11TB.Text = Properties.Settings.Default.HoraControlLlen11L5;
                        RegControl11TB.Text = Registro;

                    }
                    if (HoraControl9TB.Text != "" && HoraControl10TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen10L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl10TB.Text = Properties.Settings.Default.HoraControlLlen10L5;
                        RegControl10TB.Text = Registro;

                    }
                    if (HoraControl8TB.Text != "" && HoraControl9TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen9L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl9TB.Text = Properties.Settings.Default.HoraControlLlen9L5;
                        RegControl9TB.Text = Registro;

                    }
                    if (HoraControl7TB.Text != "" && HoraControl8TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen8L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl8TB.Text = Properties.Settings.Default.HoraControlLlen8L5;
                        RegControl8TB.Text = Registro;

                    }
                    if (HoraControl6TB.Text != "" && HoraControl7TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen7L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl7TB.Text = Properties.Settings.Default.HoraControlLlen7L5;
                        RegControl7TB.Text = Registro;

                    }
                    if (HoraControl5TB.Text != "" && HoraControl6TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen6L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl6TB.Text = Properties.Settings.Default.HoraControlLlen6L5;
                        RegControl6TB.Text = Registro;

                    }
                    if (HoraControl4TB.Text != "" && HoraControl5TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen5L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl5TB.Text = Properties.Settings.Default.HoraControlLlen5L5;
                        RegControl5TB.Text = Registro;

                    }
                    if (HoraControl3TB.Text != "" && HoraControl4TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen4L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl4TB.Text = Properties.Settings.Default.HoraControlLlen4L5;
                        RegControl4TB.Text = Registro;

                    }
                    if (HoraControl2TB.Text != "" && HoraControl3TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen3L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl3TB.Text = Properties.Settings.Default.HoraControlLlen3L5;
                        RegControl3TB.Text = Registro;

                    }
                    if (HoraControl1TB.Text != "" && HoraControl2TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen2L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl2TB.Text = Properties.Settings.Default.HoraControlLlen2L5;
                        RegControl2TB.Text = Registro;
                    }
                    if (HoraControl1TB.Text == "")
                    {
                        Properties.Settings.Default.HoraControlLlen1L5 = HoraControlTB.Text;
                        Properties.Settings.Default.Save();
                        HoraControl1TB.Text = Properties.Settings.Default.HoraControlLlen1L5;
                        RegControl1TB.Text = Registro;
                    }
                }

                //Restablecemos los valores correspondientes
                HoraControlTB.Text = "";
                ControlCierre = "";
                Volumen = "";
                CuelloBoca = "";
                ComentariosTB.Text = "";
                ControlCierre_OK_B.BackColor = Color.FromArgb(27, 33, 41);
                ControlCierre_NOOK_B.BackColor = Color.FromArgb(27, 33, 41);
                Volumen_OK_B.BackColor = Color.FromArgb(27, 33, 41);
                Volumen_NOOK_B.BackColor = Color.FromArgb(27, 33, 41);
                CuelloBoca_OK_B.BackColor = Color.FromArgb(27, 33, 41);
                CuelloBoca_NOOK_B.BackColor = Color.FromArgb(27, 33, 41);
                Apps_Llenadora.controlsaved = true;

                //Desactivammos la alarma y decrementamos el contrador
                if (MaquinaLinea.numlin == 2 && Properties.Settings.Default.AlarmaC30LlenL2 == true) Properties.Settings.Default.AlarmaC30LlenL2 = false; Properties.Settings.Default.ContadorC30LlenL2 -= 1;
                if (MaquinaLinea.numlin == 3 && Properties.Settings.Default.AlarmaC30LlenL3 == true) Properties.Settings.Default.AlarmaC30LlenL3 = false; Properties.Settings.Default.ContadorC30LlenL3 -= 1;
                if (MaquinaLinea.numlin == 5 && Properties.Settings.Default.AlarmaC30LlenL5 == true) Properties.Settings.Default.AlarmaC30LlenL5 = false; Properties.Settings.Default.ContadorC30LlenL5 -= 1;
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("FALTAN CAMPOS POR RELLENAR! -> NO SE HA GUARDADO LA INFORMACIÓN");
            }
        }

        private void BorrarB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                Properties.Settings.Default.HoraControlLlen1L2 = "";
                Properties.Settings.Default.HoraControlLlen2L2 = "";
                Properties.Settings.Default.HoraControlLlen3L2 = "";
                Properties.Settings.Default.HoraControlLlen4L2 = "";
                Properties.Settings.Default.HoraControlLlen5L2 = "";
                Properties.Settings.Default.HoraControlLlen6L2 = "";
                Properties.Settings.Default.HoraControlLlen7L2 = "";
                Properties.Settings.Default.HoraControlLlen8L2 = "";
                Properties.Settings.Default.HoraControlLlen9L2 = "";
                Properties.Settings.Default.HoraControlLlen10L2 = "";
                Properties.Settings.Default.HoraControlLlen11L2 = "";
                Properties.Settings.Default.HoraControlLlen12L2 = "";
                Properties.Settings.Default.HoraControlLlen13L2 = "";
                Properties.Settings.Default.HoraControlLlen14L2 = "";
                Properties.Settings.Default.HoraControlLlen15L2 = "";
                Properties.Settings.Default.HoraControlLlen16L2 = "";
                Properties.Settings.Default.Save();

                HoraControl1TB.Text = Properties.Settings.Default.HoraControlLlen1L2;
                HoraControl2TB.Text = Properties.Settings.Default.HoraControlLlen2L2;
                HoraControl3TB.Text = Properties.Settings.Default.HoraControlLlen3L2;
                HoraControl4TB.Text = Properties.Settings.Default.HoraControlLlen4L2;
                HoraControl5TB.Text = Properties.Settings.Default.HoraControlLlen5L2;
                HoraControl6TB.Text = Properties.Settings.Default.HoraControlLlen6L2;
                HoraControl7TB.Text = Properties.Settings.Default.HoraControlLlen7L2;
                HoraControl8TB.Text = Properties.Settings.Default.HoraControlLlen8L2;
                HoraControl9TB.Text = Properties.Settings.Default.HoraControlLlen9L2;
                HoraControl10TB.Text = Properties.Settings.Default.HoraControlLlen10L2;
                HoraControl11TB.Text = Properties.Settings.Default.HoraControlLlen11L2;
                HoraControl12TB.Text = Properties.Settings.Default.HoraControlLlen12L2;
                HoraControl13TB.Text = Properties.Settings.Default.HoraControlLlen13L2;
                HoraControl14TB.Text = Properties.Settings.Default.HoraControlLlen14L2;
                HoraControl15TB.Text = Properties.Settings.Default.HoraControlLlen15L2;
                HoraControl16TB.Text = Properties.Settings.Default.HoraControlLlen16L2;


                RegControl1TB.Text = "";
                RegControl2TB.Text = "";
                RegControl3TB.Text = "";
                RegControl4TB.Text = "";
                RegControl5TB.Text = "";
                RegControl6TB.Text = "";
                RegControl7TB.Text = "";
                RegControl8TB.Text = "";
                RegControl9TB.Text = "";
                RegControl10TB.Text = "";
                RegControl11TB.Text = "";
                RegControl12TB.Text = "";
                RegControl13TB.Text = "";
                RegControl14TB.Text = "";
                RegControl15TB.Text = "";
                RegControl16TB.Text = "";
            }
            if (MaquinaLinea.numlin == 3)
            {
                Properties.Settings.Default.HoraControlLlen1L3 = "";
                Properties.Settings.Default.HoraControlLlen2L3 = "";
                Properties.Settings.Default.HoraControlLlen3L3 = "";
                Properties.Settings.Default.HoraControlLlen4L3 = "";
                Properties.Settings.Default.HoraControlLlen5L3 = "";
                Properties.Settings.Default.HoraControlLlen6L3 = "";
                Properties.Settings.Default.HoraControlLlen7L3 = "";
                Properties.Settings.Default.HoraControlLlen8L3 = "";
                Properties.Settings.Default.HoraControlLlen9L3 = "";
                Properties.Settings.Default.HoraControlLlen10L3 = "";
                Properties.Settings.Default.HoraControlLlen11L3 = "";
                Properties.Settings.Default.HoraControlLlen12L3 = "";
                Properties.Settings.Default.HoraControlLlen13L3 = "";
                Properties.Settings.Default.HoraControlLlen14L3 = "";
                Properties.Settings.Default.HoraControlLlen15L3 = "";
                Properties.Settings.Default.HoraControlLlen16L3 = "";
                Properties.Settings.Default.Save();

                HoraControl1TB.Text = Properties.Settings.Default.HoraControlLlen1L3;
                HoraControl2TB.Text = Properties.Settings.Default.HoraControlLlen2L3;
                HoraControl3TB.Text = Properties.Settings.Default.HoraControlLlen3L3;
                HoraControl4TB.Text = Properties.Settings.Default.HoraControlLlen4L3;
                HoraControl5TB.Text = Properties.Settings.Default.HoraControlLlen5L3;
                HoraControl6TB.Text = Properties.Settings.Default.HoraControlLlen6L3;
                HoraControl7TB.Text = Properties.Settings.Default.HoraControlLlen7L3;
                HoraControl8TB.Text = Properties.Settings.Default.HoraControlLlen8L3;
                HoraControl9TB.Text = Properties.Settings.Default.HoraControlLlen9L3;
                HoraControl10TB.Text = Properties.Settings.Default.HoraControlLlen10L3;
                HoraControl11TB.Text = Properties.Settings.Default.HoraControlLlen11L3;
                HoraControl12TB.Text = Properties.Settings.Default.HoraControlLlen12L3;
                HoraControl13TB.Text = Properties.Settings.Default.HoraControlLlen13L3;
                HoraControl14TB.Text = Properties.Settings.Default.HoraControlLlen14L3;
                HoraControl15TB.Text = Properties.Settings.Default.HoraControlLlen15L3;
                HoraControl16TB.Text = Properties.Settings.Default.HoraControlLlen16L3;


                RegControl1TB.Text = "";
                RegControl2TB.Text = "";
                RegControl3TB.Text = "";
                RegControl4TB.Text = "";
                RegControl5TB.Text = "";
                RegControl6TB.Text = "";
                RegControl7TB.Text = "";
                RegControl8TB.Text = "";
                RegControl9TB.Text = "";
                RegControl10TB.Text = "";
                RegControl11TB.Text = "";
                RegControl12TB.Text = "";
                RegControl13TB.Text = "";
                RegControl14TB.Text = "";
                RegControl15TB.Text = "";
                RegControl16TB.Text = "";
            }
            if (MaquinaLinea.numlin == 5)
            {
                Properties.Settings.Default.HoraControlLlen1L5 = "";
                Properties.Settings.Default.HoraControlLlen2L5 = "";
                Properties.Settings.Default.HoraControlLlen3L5 = "";
                Properties.Settings.Default.HoraControlLlen4L5 = "";
                Properties.Settings.Default.HoraControlLlen5L5 = "";
                Properties.Settings.Default.HoraControlLlen6L5 = "";
                Properties.Settings.Default.HoraControlLlen7L5 = "";
                Properties.Settings.Default.HoraControlLlen8L5 = "";
                Properties.Settings.Default.HoraControlLlen9L5 = "";
                Properties.Settings.Default.HoraControlLlen10L5 = "";
                Properties.Settings.Default.HoraControlLlen11L5 = "";
                Properties.Settings.Default.HoraControlLlen12L5 = "";
                Properties.Settings.Default.HoraControlLlen13L5 = "";
                Properties.Settings.Default.HoraControlLlen14L5 = "";
                Properties.Settings.Default.HoraControlLlen15L5 = "";
                Properties.Settings.Default.HoraControlLlen16L5 = "";
                Properties.Settings.Default.Save();

                HoraControl1TB.Text = Properties.Settings.Default.HoraControlLlen1L5;
                HoraControl2TB.Text = Properties.Settings.Default.HoraControlLlen2L5;
                HoraControl3TB.Text = Properties.Settings.Default.HoraControlLlen3L5;
                HoraControl4TB.Text = Properties.Settings.Default.HoraControlLlen4L5;
                HoraControl5TB.Text = Properties.Settings.Default.HoraControlLlen5L5;
                HoraControl6TB.Text = Properties.Settings.Default.HoraControlLlen6L5;
                HoraControl7TB.Text = Properties.Settings.Default.HoraControlLlen7L5;
                HoraControl8TB.Text = Properties.Settings.Default.HoraControlLlen8L5;
                HoraControl9TB.Text = Properties.Settings.Default.HoraControlLlen9L5;
                HoraControl10TB.Text = Properties.Settings.Default.HoraControlLlen10L5;
                HoraControl11TB.Text = Properties.Settings.Default.HoraControlLlen11L5;
                HoraControl12TB.Text = Properties.Settings.Default.HoraControlLlen12L5;
                HoraControl13TB.Text = Properties.Settings.Default.HoraControlLlen13L5;
                HoraControl14TB.Text = Properties.Settings.Default.HoraControlLlen14L5;
                HoraControl15TB.Text = Properties.Settings.Default.HoraControlLlen15L5;
                HoraControl16TB.Text = Properties.Settings.Default.HoraControlLlen16L5;


                RegControl1TB.Text = "";
                RegControl2TB.Text = "";
                RegControl3TB.Text = "";
                RegControl4TB.Text = "";
                RegControl5TB.Text = "";
                RegControl6TB.Text = "";
                RegControl7TB.Text = "";
                RegControl8TB.Text = "";
                RegControl9TB.Text = "";
                RegControl10TB.Text = "";
                RegControl11TB.Text = "";
                RegControl12TB.Text = "";
                RegControl13TB.Text = "";
                RegControl14TB.Text = "";
                RegControl15TB.Text = "";
                RegControl16TB.Text = "";
            }
        }
    }
}

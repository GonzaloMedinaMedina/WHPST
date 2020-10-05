using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WHPS.Utiles;
using System.Diagnostics;
using System.IO;
using WHPS.Model;
using WHPS.ProgramMenus;

namespace WHPS.Llenadora
{
    public partial class Llenadora_Control_Temperatura : Form
    {
        public string Modo = "Llenadora";
        TextBox TextBox;
        public Llenadora_Control_Temperatura()
        {
            InitializeComponent();
        }

        //Volvemos al menu de la llenadora cerrando la ventana


        //Volvemos al menu de la llenadora pulsando el boton de volver
        private void ExitB_Click(object sender, EventArgs e)
        {
            EstablecerVariables(MaquinaLinea.numlin, HoraInicioTB.Text, TemperaturaInicioTB.Text, HoraFinTB.Text, TemperaturaFinTB.Text);

            MainLlenadora Form = new MainLlenadora();
            Hide();
            Form.Show();
            GC.Collect();
        }

        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

        //Cargamos incialmente los datos ya obtenidos
        private void Llenadora_Control_Temperatura_Load(object sender, EventArgs e)
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

            //Indicamos que incialmente registra la temperatura de la llenadora
            ModoLlenB.BackColor = Color.Goldenrod;


            if (MaquinaLinea.numlin == 2) CompletarRegistros(Properties.Settings.Default.HoraInicioL2, Properties.Settings.Default.TemperaturaInicioL2, Properties.Settings.Default.HoraFinL2, Properties.Settings.Default.TemperaturaFinL2);
            if (MaquinaLinea.numlin == 3) CompletarRegistros(Properties.Settings.Default.HoraInicioL3, Properties.Settings.Default.TemperaturaInicioL3, Properties.Settings.Default.HoraFinL3, Properties.Settings.Default.TemperaturaFinL3);
            if (MaquinaLinea.numlin == 5) CompletarRegistros(Properties.Settings.Default.HoraInicioL5, Properties.Settings.Default.TemperaturaInicioL5, Properties.Settings.Default.HoraFinL5, Properties.Settings.Default.TemperaturaFinL5);
        }

        //Temporizador, nos sincroniza con la pantalla del main para que si al volver se ha activado la alarma nos avise
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            //Para activar la alarma debe, estar desactivada, haberse chequeado el inicio de turno y que la hora cuadre con la alarma 
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }
            string Hora = lbReloj.Text;
            if (Hora.Substring(3, 2) != "00" && Hora.Substring(3, 2) != "30"){ MaquinaLinea.AnuladorAlarma = true;}
            if (Hora.Substring(3, 2) == "00" || Hora.Substring(3, 2) == "30"){ if (MaquinaLinea.AnuladorAlarma == true) {Apps_Llenadora.AlarmaControl30min();}}
        }

        //Establece donde se registra el formulario
        private void ModoLlenB_Click(object sender, EventArgs e)
        {
            Modo = "Llenadora";
            ModoLlenB.BackColor = Color.Goldenrod;
            ModoCalderaB.BackColor = Color.FromArgb(27, 33, 41);
            RegistroFinalBOX.Show();
        }
        private void ModoCalderaB_Click(object sender, EventArgs e)
        {
            Modo = "Caldera";
            ModoLlenB.BackColor = Color.FromArgb(27, 33, 41);
            ModoCalderaB.BackColor = Color.Goldenrod;
            RegistroFinalBOX.Hide();
        }



        //Carga la hora en pantalla, si esta ya esta cargada y se pretende sobrescribirlos saltará un mensaje de seguridad. 
        private void CargaInicialB_Click(object sender, EventArgs e)
        {
            if (HoraInicioTB.Text != "")
            {
                DialogResult opcion;
                opcion = MessageBox.Show("¿Estas seguro de sobrescribir los datos? Perderá la informacion", "", MessageBoxButtons.OKCancel);
                if (opcion == DialogResult.OK)
                {
                    HoraInicioTB.Text = DateTime.Now.ToString("HH:mm:ss");
                    EstablecerVariables(MaquinaLinea.numlin, HoraInicioTB.Text, TemperaturaInicioTB.Text, HoraFinTB.Text, TemperaturaFinTB.Text);
                }
            }
           else
           {
                HoraInicioTB.Text = DateTime.Now.ToString("HH:mm:ss");
                EstablecerVariables(MaquinaLinea.numlin, HoraInicioTB.Text, TemperaturaInicioTB.Text, HoraFinTB.Text, TemperaturaFinTB.Text);
            }
        }
        private void CargaFinalB_Click(object sender, EventArgs e)
        {
            if (HoraFinTB.Text != "")
            {
                DialogResult opcion;
                opcion = MessageBox.Show("¿Estas seguro de sobrescribir los datos? Perderá la informacion", "", MessageBoxButtons.OKCancel);
                if (opcion == DialogResult.OK)
                {
                    HoraFinTB.Text = DateTime.Now.ToString("HH:mm:ss");
                    EstablecerVariables(MaquinaLinea.numlin, HoraInicioTB.Text, TemperaturaInicioTB.Text, HoraFinTB.Text, TemperaturaFinTB.Text);
                }
            }
            else
            {
                HoraFinTB.Text = DateTime.Now.ToString("HH:mm:ss");
                EstablecerVariables(MaquinaLinea.numlin, HoraInicioTB.Text, TemperaturaInicioTB.Text, HoraFinTB.Text, TemperaturaFinTB.Text);
            }
        }

        private void TemperaturaInicioTB_MouseClick(object sender, MouseEventArgs e)
        {
            WHPS.Utiles.Numberpad2.AbrirCalculadora(TemperaturaInicioTB);
        }
        private void TemperaturaFinTB_MouseClick(object sender, MouseEventArgs e)
        {
            WHPS.Utiles.Numberpad2.AbrirCalculadora(TemperaturaFinTB);
        }

        //Limpiamos las variables de temperatura y tiempo
        private void BorrarInicialB_Click(object sender, EventArgs e)
        {
            EstablecerVariables(MaquinaLinea.numlin, "", "", HoraFinTB.Text, TemperaturaFinTB.Text);
            if (MaquinaLinea.numlin == 2) CompletarRegistros(Properties.Settings.Default.HoraInicioL2, Properties.Settings.Default.TemperaturaInicioL2, Properties.Settings.Default.HoraFinL2, Properties.Settings.Default.TemperaturaFinL2);
            if (MaquinaLinea.numlin == 3) CompletarRegistros(Properties.Settings.Default.HoraInicioL3, Properties.Settings.Default.TemperaturaInicioL3, Properties.Settings.Default.HoraFinL3, Properties.Settings.Default.TemperaturaFinL3);
            if (MaquinaLinea.numlin == 5) CompletarRegistros(Properties.Settings.Default.HoraInicioL5, Properties.Settings.Default.TemperaturaInicioL5, Properties.Settings.Default.HoraFinL5, Properties.Settings.Default.TemperaturaFinL5);
        }
        private void BorraFinalB_Click(object sender, EventArgs e)
        {
            EstablecerVariables(MaquinaLinea.numlin, HoraInicioTB.Text, TemperaturaInicioTB.Text, "", "");
            if (MaquinaLinea.numlin == 2) CompletarRegistros(Properties.Settings.Default.HoraInicioL2, Properties.Settings.Default.TemperaturaInicioL2, Properties.Settings.Default.HoraFinL2, Properties.Settings.Default.TemperaturaFinL2);
            if (MaquinaLinea.numlin == 3) CompletarRegistros(Properties.Settings.Default.HoraInicioL3, Properties.Settings.Default.TemperaturaInicioL3, Properties.Settings.Default.HoraFinL3, Properties.Settings.Default.TemperaturaFinL3);
            if (MaquinaLinea.numlin == 5) CompletarRegistros(Properties.Settings.Default.HoraInicioL5, Properties.Settings.Default.TemperaturaInicioL5, Properties.Settings.Default.HoraFinL5, Properties.Settings.Default.TemperaturaFinL5);
        }

        //Al pulsar sobre la parte de comentarios se nos despliega un teclado con el que poder escribir
        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
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
        }

        //Función que establece la variable
        public void EstablecerVariables(int numlin, string HoraI, string TempI, string HoraF, string TempF)
        {
            if (numlin == 2)
            {
                Properties.Settings.Default.HoraInicioL2 = HoraI;
                Properties.Settings.Default.TemperaturaInicioL2 = TempI;
                Properties.Settings.Default.HoraFinL2 = HoraF;
                Properties.Settings.Default.TemperaturaFinL2 = TempF;
            }
            if (numlin == 3)
            {
                Properties.Settings.Default.HoraInicioL3 = HoraI;
                Properties.Settings.Default.TemperaturaInicioL3 = TempI;
                Properties.Settings.Default.HoraFinL3 = HoraF;
                Properties.Settings.Default.TemperaturaFinL3 = TempF;
            }
            if (numlin == 5)
            {
                Properties.Settings.Default.HoraInicioL5 = HoraI;
                Properties.Settings.Default.TemperaturaInicioL5 = TempI;
                Properties.Settings.Default.HoraFinL5 = HoraF;
                Properties.Settings.Default.TemperaturaFinL5 = TempF;
            }
            Properties.Settings.Default.Save();
        }
        //Función que completa los registros
        public void CompletarRegistros(string HoraI, string TempI, string HoraF, string TempF)
        {
            HoraInicioTB.Text = HoraI;
            TemperaturaInicioTB.Text = TempI;
            HoraFinTB.Text = HoraF;
            TemperaturaFinTB.Text = TempF;
        }
        //Para guardar los datos deben estar completos los registros
        private void saveBot_Click(object sender, EventArgs e)
        {
            if ((Modo == "Llenadora" && HoraInicioTB.Text != "" && TemperaturaInicioTB.Text != "" && HoraFinTB.Text != "" && TemperaturaFinTB.Text != "") || (Modo == "Caldera" && HoraInicioTB.Text != "" && TemperaturaInicioTB.Text != ""))
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
                valores5[0] = "HoraInicial";
                valores5[1] = HoraInicioTB.Text;
                listavalores.Add(valores5);
                valores6[0] = "TemperaturaInicial";
                valores6[1] = TemperaturaInicioTB.Text;
                listavalores.Add(valores6);
                if (Modo =="Llenadora")
                {
                valores7[0] = "HoraFinal";
                valores7[1] = HoraFinTB.Text;
                listavalores.Add(valores7);
                valores8[0] = "TemperaturaFinal";
                valores8[1] = TemperaturaFinTB.Text;
                listavalores.Add(valores8);
                }
                valores9[0] = "Comentarios";
                valores9[1] = richTextBox1.Text;
                listavalores.Add(valores9);
                if (Modo == "Llenadora")
                {
                    string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileLlenadora, "TemperaturaLlenadora", listavalores, "Id");
                    if (salida.Contains("ERROR"))
                    {
                        MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                    }
                    else
                    {
                        EstablecerVariables(MaquinaLinea.numlin, "", "", "", "");
                        if (MaquinaLinea.numlin == 2) CompletarRegistros(Properties.Settings.Default.HoraInicioL2, Properties.Settings.Default.TemperaturaInicioL2, Properties.Settings.Default.HoraFinL2, Properties.Settings.Default.TemperaturaFinL2);
                        if (MaquinaLinea.numlin == 3) CompletarRegistros(Properties.Settings.Default.HoraInicioL3, Properties.Settings.Default.TemperaturaInicioL3, Properties.Settings.Default.HoraFinL3, Properties.Settings.Default.TemperaturaFinL3);
                        if (MaquinaLinea.numlin == 5) CompletarRegistros(Properties.Settings.Default.HoraInicioL5, Properties.Settings.Default.TemperaturaInicioL5, Properties.Settings.Default.HoraFinL5, Properties.Settings.Default.TemperaturaFinL5);
                        //MessageBox.Show(salida);
                        MainLlenadora Form = new MainLlenadora();
                        Hide();
                        Form.Show();
                    }
                }
                if (Modo == "Caldera")
                {
                    string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileLlenadora, "TemperaturaCaldera", listavalores, "Id");
                    if (salida.Contains("ERROR"))
                    {
                        MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                    }
                    else
                    {
                        EstablecerVariables(MaquinaLinea.numlin, "", "", "", "");
                        if (MaquinaLinea.numlin == 2) CompletarRegistros(Properties.Settings.Default.HoraInicioL2, Properties.Settings.Default.TemperaturaInicioL2, Properties.Settings.Default.HoraFinL2, Properties.Settings.Default.TemperaturaFinL2);
                        if (MaquinaLinea.numlin == 3) CompletarRegistros(Properties.Settings.Default.HoraInicioL3, Properties.Settings.Default.TemperaturaInicioL3, Properties.Settings.Default.HoraFinL3, Properties.Settings.Default.TemperaturaFinL3);
                        if (MaquinaLinea.numlin == 5) CompletarRegistros(Properties.Settings.Default.HoraInicioL5, Properties.Settings.Default.TemperaturaInicioL5, Properties.Settings.Default.HoraFinL5, Properties.Settings.Default.TemperaturaFinL5);
                        //MessageBox.Show(salida);
                        MainLlenadora Form = new MainLlenadora();
                        Hide();
                        Form.Show();
                        GC.Collect();
                    }
                }
            }
            else
            {
                MessageBox.Show("FALTAN CAMPOS POR RELLENAR! -> NO SE HA GUARDADO LA INFORMACIÓN");
            }

        }


        private void HoraInicioTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (HoraInicioTB.Text != "")
            {
                DialogResult opcion;
                opcion = MessageBox.Show("¿Estas seguro de sobrescribir los datos? Perderá la informacion", "", MessageBoxButtons.OKCancel);
                if (opcion == DialogResult.OK)
                {
                    HoraInicioTB.Text = DateTime.Now.ToString("HH:mm:ss");
                    EstablecerVariables(MaquinaLinea.numlin, HoraInicioTB.Text, TemperaturaInicioTB.Text, HoraFinTB.Text, TemperaturaFinTB.Text);
                }
            }
            else
            {
                HoraInicioTB.Text = DateTime.Now.ToString("HH:mm:ss");
                EstablecerVariables(MaquinaLinea.numlin, HoraInicioTB.Text, TemperaturaInicioTB.Text, HoraFinTB.Text, TemperaturaFinTB.Text);
            }
        }

        private void HoraFinTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (HoraFinTB.Text != "")
            {
                DialogResult opcion;
                opcion = MessageBox.Show("¿Estas seguro de sobrescribir los datos? Perderá la informacion", "", MessageBoxButtons.OKCancel);
                if (opcion == DialogResult.OK)
                {
                    HoraFinTB.Text = DateTime.Now.ToString("HH:mm:ss");
                    EstablecerVariables(MaquinaLinea.numlin, HoraInicioTB.Text, TemperaturaInicioTB.Text, HoraFinTB.Text, TemperaturaFinTB.Text);
                }
            }
            else
            {
                HoraFinTB.Text = DateTime.Now.ToString("HH:mm:ss");
                EstablecerVariables(MaquinaLinea.numlin, HoraInicioTB.Text, TemperaturaInicioTB.Text, HoraFinTB.Text, TemperaturaFinTB.Text);
            }
        }
    }
}

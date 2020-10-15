using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WHPS.Utiles;
using System.Diagnostics;
using System.IO;
using WHPS.Model;
using WHPS.ProgramMenus;
using WHPS.Parte;

namespace WHPS.Despaletizador
{

    public partial class Despaletizador_Comentarios : Form
    {
        string Turno;
        Process p1 = new Process();
        public Despaletizador_Comentarios()
        {
            InitializeComponent();
        }

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


        private void Despaletizador_Comentarios_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            Turno = Utilidades.ObtenerTurnoActual();

            //############## ABRIR ON SCREEN KEYBOARD  ###############
            try
            {
                //Process.Start(Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "Sysnative", "cmd.exe"), "/c osk.exe" + " & exit");
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
            Despaletizador_Comentarios.ActiveForm.Activate();
            ComentariosTB.Select();
        }

        //Un temporizador, nos sincroniza con la pantalla del main para que si al volver ha se ha activado la alarma nos avise
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            //Para activar la alarma debe, estar desactivada, haberse chequeado el inicio de turno y que la hora cuadre con la alarma 
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }
        }

        private void saveBot_Click(object sender, EventArgs e)
        {
            List<string[]> listavalores = new List<string[]>();
            listavalores.Add(new string[2] { "Fecha", DateTime.Now.ToString("dd/MM/yyyy") });
            listavalores.Add(new string[2] { "Hora", DateTime.Now.ToString("HH:mm:ss") });
            listavalores.Add(new string[2] { "FechaDB", DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") });
            listavalores.Add(new string[2] { "Responsable", MaquinaLinea.Responsable });
            listavalores.Add(new string[2] { "Maquinista", MaquinaLinea.MDespaletizador });
            listavalores.Add(new string[2] { "Turno", Turno });
            listavalores.Add(new string[2] { "Comentarios", ComentariosTB.Text });

            if (ComentariosTB.Text != "")
            {
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileDespaletizador, "Comentarios", listavalores, "Id");

                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                }
                //MessageBox.Show(salida);

                else
                {
                    MainDespaletizador Form = new MainDespaletizador();
                    Hide();
                    Form.Show();
                    GC.Collect();
                }
            }
        }

       
    }
}


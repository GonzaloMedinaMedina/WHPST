using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.ProgramMenus;
using WHPS.Utiles;

namespace WHPS.Llenadora
{
    public partial class Llenadora_Registro_Paro : Form
    {
        public int h1 = 0;
        public int m1 = 0;
        public int s1 = 0;
        public int h2 = 0;
        public int m2 = 0;
        public int s2 = 0;

        bool Comentrarios = false;
        string Motivo;
        private bool inicio_paro;
        private string hora_ini_paro;
        MainLlenadora parent;
        public Llenadora_Registro_Paro()
        {
            InitializeComponent();
         
        }

        public Llenadora_Registro_Paro(MainLlenadora p, bool inicio, string hora_i, int[] t)
        {
            InitializeComponent();
            parent = p;
            GuardarVariable(true);

            if (inicio)
            {
                this.inicio_paro = inicio;
                this.hora_ini_paro = hora_i;
                
                int[] now = new int[4];
                now[0] = (DateTime.Now.Hour/10)*10+ DateTime.Now.Hour % 10;
                now[1] = (DateTime.Now.Minute/10)*10 + DateTime.Now.Minute % 10;
                now[2] = (DateTime.Now.Second / 10)*10 + DateTime.Now.Second % 10;
                now[3] = (DateTime.Now.Day);

                int[] result = Utiles.AlertaTurno.DiferenciaEntreHoras(t, now);

                h2 =result[0]/10;
                h1 =result[0]%10;
                m2 =result[1]/10;
                m1 =result[1]%10;
                s2 =result[2]/10;
                s1 =result[2]%10;
                //Console.WriteLine("DIFERENCIA: "+h1+h2+m1+m2+s1+s2);
            }
        }

        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        private void Llenadora_Registro_Paro_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //Se inicia el temporizador
            TemporizadorTB.Text = Convert.ToString(h2) + Convert.ToString(h1) + ":" + Convert.ToString(m2) + Convert.ToString(m1) + ":" + Convert.ToString(s2) + Convert.ToString(s1);

            //Se rellena los datos del equipo
            dateTB.Text = DateTime.Now.ToString("dd/MM/yyyy");
            respTB.Text = MaquinaLinea.Responsable;
            maqTB.Text = MaquinaLinea.MLlenadora;
            turnoTB.Text = MaquinaLinea.turno;

            //Se rellena los datos del registro de parada
            PDesdeTB.Text = (inicio_paro)? hora_ini_paro : DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// Función del temporizador que se ejecuta cada segundo, indíca la hora y si se activa la alarma. 
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Para activar la alarma debe, estar desactivada, haberse chequeado el inicio de turno y que la hora cuadre con la alarma 
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }
            s1 += 1;
            if (s1 > 9)
            {
                s1 = 0;
                s2 += 1;
            }
            if (s1 == 0 && s2 > 5)
            {
                s2 = 0;
                m1 += 1;
            }
            if (m1 > 9)
            {
                m1 = 0;
                m2 += 1;
            }
            if (m1 == 0 && m2 > 5)
            {
                m2 = 0;
                h1 += 1;
            }
            if (h1 > 4)
            {
                h1 = 0;
                h2 += 1;
            }
            if (h1 == 0 && h2 > 2)
            {
                h2 = 0;
            }
            
            TemporizadorTB.Text = Convert.ToString(h2) + Convert.ToString(h1) + ":" + Convert.ToString(m2) + Convert.ToString(m1) + ":" + Convert.ToString(s2) + Convert.ToString(s1);
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

        /// <summary>
        /// Boton que vuelve al form anterior con la confirmación del usuario.
        /// </summary>
        private void CancelarB_Click(object sender, EventArgs e)
        {
            DialogResult opcion;
            opcion = MessageBox.Show("¿Estas seguro que quieres cancelar la parada? Se perderá toda la informacion.", "", MessageBoxButtons.YesNo);
            if (opcion == DialogResult.Yes)
            {
                GuardarVariable(false);
                parent.AdvertenciaParo(false);
                Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainLlenadora));
                this.Hide();
                this.Dispose();
            }
            else if(opcion == DialogResult.No)
            {
                parent.AdvertenciaParo(true);
                Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainLlenadora));
                this.Hide();
                this.Dispose();
            }
        }

        private void saveBot_Click(object sender, EventArgs e)
        {
            //Para poder guardar todos los campos deben estar cumplimentados
            if (PDesdeTB.Text != "" && MotivoCB.Text != "")
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
                valores5[0] = "ParoDesde";
                valores5[1] = PDesdeTB.Text;
                listavalores.Add(valores5);
                valores6[0] = "ParoHasta";
                valores6[1] = DateTime.Now.ToString("HH:mm:ss");
                listavalores.Add(valores6);
                if (Comentrarios == true && ComentariosTB.Text == "" && Motivo2CB.Text!="") Motivo = Motivo + ": " + Motivo2CB.Text;
                if (Comentrarios == true && ComentariosTB.Text != "" && Motivo2CB.Text == "") Motivo = Motivo + ": " + ComentariosTB.Text;
                if (Comentrarios == false && Motivo2CB.Text == "") Motivo = MotivoCB.Text;
                valores7[0] = "Motivo";
                valores7[1] = Motivo;
                listavalores.Add(valores7);
                valores8[0] = "TiempoParada";
                valores8[1] = TemporizadorTB.Text;
                listavalores.Add(valores8);

                string salida = Utiles.ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileLlenadora, "RegistroParada", listavalores, "Id");
                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                }
                else
                {
                    PDesdeTB.Text = "";
                    GuardarVariable(false);
                    //MessageBox.Show(salida);
                    Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainLlenadora));
                    this.Hide();
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show("FALTAN CAMPOS POR RELLENAR! -> NO SE HA GUARDADO LA INFORMACIÓN");
            }
        }

        private void ComentariosTB_MouseClick(object sender, MouseEventArgs e)
        {
            Motivo2CB.Text = "";
            try
            {
                Process p1 = new Process();
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
            ComentariosTB.Select();
        }

        private void MotivoCB_TextChanged(object sender, EventArgs e)
        {
            Motivo = MotivoCB.Text;
            if (Motivo.Substring(0, 6) == "PARADA")
            {
                Motivo2CB.Visible = true;
                ComentariosTB.Visible = true;
                Comentrarios = true;
            }
            else
            {
                Motivo2CB.Visible = false;
                ComentariosTB.Visible = false;
                Comentrarios = false;
            }
        }

        private void Motivo2CB_MouseClick(object sender, MouseEventArgs e)
        {
            ComentariosTB.Text = "";
        }


        private void GuardarVariable(bool valor)
        {
            if (MaquinaLinea.numlin == 2)
            {
                Properties.Settings.Default.Paro_Llen_L2 = valor;
                Properties.Settings.Default.Hora_Paro_Llen_L2 = (Properties.Settings.Default.Hora_Paro_Llen_L2 =="")? DateTime.Now.ToString("HH:mm:ss") : Properties.Settings.Default.Hora_Paro_Llen_L2;
                Properties.Settings.Default.Hora_Paro_Llen_L2 = (valor)? Properties.Settings.Default.Hora_Paro_Llen_L2 : "";
            }
            else if (MaquinaLinea.numlin == 3)
            {
                Properties.Settings.Default.Paro_Llen_L3 = valor;
                Properties.Settings.Default.Hora_Paro_Llen_L3 = (Properties.Settings.Default.Hora_Paro_Llen_L3 == "") ? DateTime.Now.ToString("HH:mm:ss") : Properties.Settings.Default.Hora_Paro_Llen_L3;
                Properties.Settings.Default.Hora_Paro_Llen_L3 = (valor) ? Properties.Settings.Default.Hora_Paro_Llen_L3 : "";

            }
            else if(MaquinaLinea.numlin==5)
            {
                Properties.Settings.Default.Paro_Llen_L5 = valor;
                Properties.Settings.Default.Hora_Paro_Llen_L5 = (Properties.Settings.Default.Hora_Paro_Llen_L5 == "") ? DateTime.Now.ToString("HH:mm:ss") : Properties.Settings.Default.Hora_Paro_Llen_L5;
                Properties.Settings.Default.Hora_Paro_Llen_L5 = (valor) ? Properties.Settings.Default.Hora_Paro_Llen_L5 : "";

            }
        }
    }
}

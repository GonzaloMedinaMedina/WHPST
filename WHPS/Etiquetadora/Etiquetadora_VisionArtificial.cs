using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.ProgramMenus;
using WHPS.Utiles;

namespace WHPS.Etiquetadora
{
    public partial class Etiquetadora_VisionArtificial : Form
    {
        public string EstadoRev1 = "-";
        public string EstadoRev2 = "-";
        public string EstadoRev3 = "-";
        public string EstadoRev4 = "-";
        MainEtiquetadora parent;
        public Etiquetadora_VisionArtificial(MainEtiquetadora p)
        {
            InitializeComponent();
            parent = p;
        }

        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>
        private void BackB_Click(object sender, EventArgs e)
        { 
                  Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainEtiquetadora));
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
        private void Etiquetadora_VisionArtificial_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;
            
            //Rellenamos los datos del producto
            dateTB.Text = DateTime.Now.ToString("dd/MM/yyyy");
            respTB.Text = MaquinaLinea.Responsable;
            maqTB.Text = MaquinaLinea.MEtiquetadora;
            turnoTB.Text = Utilidades.ObtenerTurnoActual();

            //Cerramos los avisos iniciales
            Aviso2LB.Hide();

            //Guadamos los valores que ya han sido rellenados
            if (MaquinaLinea.numlin == 2)
            {
                Hora1TB.Text = Properties.Settings.Default.HoraRev1EtiqL2;
                Hora2TB.Text = Properties.Settings.Default.HoraRev2EtiqL2;
                Hora3TB.Text = Properties.Settings.Default.HoraRev3EtiqL2;
                Hora4TB.Text = Properties.Settings.Default.HoraRev4EtiqL2;

                Estado11TB.Text = Properties.Settings.Default.EstadoRev11EtiqL2;
                Estado12TB.Text = Properties.Settings.Default.EstadoRev12EtiqL2;
                Estado13TB.Text = Properties.Settings.Default.EstadoRev13EtiqL2;
                Estado14TB.Text = Properties.Settings.Default.EstadoRev14EtiqL2;
                Estado21TB.Text = Properties.Settings.Default.EstadoRev21EtiqL2;
                Estado22TB.Text = Properties.Settings.Default.EstadoRev22EtiqL2;
                Estado23TB.Text = Properties.Settings.Default.EstadoRev23EtiqL2;
                Estado24TB.Text = Properties.Settings.Default.EstadoRev24EtiqL2;
                Estado31TB.Text = Properties.Settings.Default.EstadoRev31EtiqL2;
                Estado32TB.Text = Properties.Settings.Default.EstadoRev32EtiqL2;
                Estado33TB.Text = Properties.Settings.Default.EstadoRev33EtiqL2;
                Estado34TB.Text = Properties.Settings.Default.EstadoRev34EtiqL2;
                Estado41TB.Text = Properties.Settings.Default.EstadoRev41EtiqL2;
                Estado42TB.Text = Properties.Settings.Default.EstadoRev42EtiqL2;
                Estado43TB.Text = Properties.Settings.Default.EstadoRev43EtiqL2;
                Estado44TB.Text = Properties.Settings.Default.EstadoRev44EtiqL2;
            }
            if (MaquinaLinea.numlin == 3)
            {
                Hora1TB.Text = Properties.Settings.Default.HoraRev1EtiqL3;
                Hora2TB.Text = Properties.Settings.Default.HoraRev2EtiqL3;
                Hora3TB.Text = Properties.Settings.Default.HoraRev3EtiqL3;
                Hora4TB.Text = Properties.Settings.Default.HoraRev4EtiqL3;

                Estado11TB.Text = Properties.Settings.Default.EstadoRev11EtiqL3;
                Estado12TB.Text = Properties.Settings.Default.EstadoRev12EtiqL3;
                Estado13TB.Text = Properties.Settings.Default.EstadoRev13EtiqL3;
                Estado14TB.Text = Properties.Settings.Default.EstadoRev14EtiqL3;
                Estado21TB.Text = Properties.Settings.Default.EstadoRev21EtiqL3;
                Estado22TB.Text = Properties.Settings.Default.EstadoRev22EtiqL3;
                Estado23TB.Text = Properties.Settings.Default.EstadoRev23EtiqL3;
                Estado24TB.Text = Properties.Settings.Default.EstadoRev24EtiqL3;
                Estado31TB.Text = Properties.Settings.Default.EstadoRev31EtiqL3;
                Estado32TB.Text = Properties.Settings.Default.EstadoRev32EtiqL3;
                Estado33TB.Text = Properties.Settings.Default.EstadoRev33EtiqL3;
                Estado34TB.Text = Properties.Settings.Default.EstadoRev34EtiqL3;
                Estado41TB.Text = Properties.Settings.Default.EstadoRev41EtiqL3;
                Estado42TB.Text = Properties.Settings.Default.EstadoRev42EtiqL3;
                Estado43TB.Text = Properties.Settings.Default.EstadoRev43EtiqL3;
                Estado44TB.Text = Properties.Settings.Default.EstadoRev44EtiqL3;
            }
            if (MaquinaLinea.numlin == 5)
            {
                Hora1TB.Text = Properties.Settings.Default.HoraRev1EtiqL5;
                Hora2TB.Text = Properties.Settings.Default.HoraRev2EtiqL5;
                Hora3TB.Text = Properties.Settings.Default.HoraRev3EtiqL5;
                Hora4TB.Text = Properties.Settings.Default.HoraRev4EtiqL5;

                Estado11TB.Text = Properties.Settings.Default.EstadoRev11EtiqL5;
                Estado12TB.Text = Properties.Settings.Default.EstadoRev12EtiqL5;
                Estado13TB.Text = Properties.Settings.Default.EstadoRev13EtiqL5;
                Estado14TB.Text = Properties.Settings.Default.EstadoRev14EtiqL5;
                Estado21TB.Text = Properties.Settings.Default.EstadoRev21EtiqL5;
                Estado22TB.Text = Properties.Settings.Default.EstadoRev22EtiqL5;
                Estado23TB.Text = Properties.Settings.Default.EstadoRev23EtiqL5;
                Estado24TB.Text = Properties.Settings.Default.EstadoRev24EtiqL5;
                Estado31TB.Text = Properties.Settings.Default.EstadoRev31EtiqL5;
                Estado32TB.Text = Properties.Settings.Default.EstadoRev32EtiqL5;
                Estado33TB.Text = Properties.Settings.Default.EstadoRev33EtiqL5;
                Estado34TB.Text = Properties.Settings.Default.EstadoRev34EtiqL5;
                Estado41TB.Text = Properties.Settings.Default.EstadoRev41EtiqL5;
                Estado42TB.Text = Properties.Settings.Default.EstadoRev42EtiqL5;
                Estado43TB.Text = Properties.Settings.Default.EstadoRev43EtiqL5;
                Estado44TB.Text = Properties.Settings.Default.EstadoRev44EtiqL5;
            }
        }

        /// <summary>
        /// Función del temporizador que se ejecuta cada segundo, indíca la hora y si se activa la alarma. 
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Cada segundo carga la hora en pantalla
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Para activar la alarma debe, estar desactivada, haberse chequeado el inicio de turno y que la hora cuadre con la alarma 
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }
        }

        //Haciendo click en el text box de la hora, se introducirá la misma
        private void HoraTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (HoraTB.Text != "")
            {
                DialogResult opcion;
                opcion = MessageBox.Show("¿Quieres sobrescribir la hora? Se perderá el dato anterior.", "", MessageBoxButtons.OKCancel);
                if (opcion == DialogResult.OK) HoraTB.Text = DateTime.Now.ToString("HH:mm:ss");
            }
            else HoraTB.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        //Caja de comentarios al clickear mortrará el teclado
        private void ComentarioBOX_MouseClick(object sender, MouseEventArgs e)
        {
            if (Environment.Is64BitOperatingSystem)
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
            else
            {
                try
                {
                    Process.Start(@"osk.exe");

                }
                catch { }
            }
            Activate();
        }


        //Función que indica cual es el color del boton una vez se ha registrado
        private void ColorBoton(string Estado, int Linea)
        {
            switch (Estado)
            {
                case "OK":
                    switch(Linea)
                    {
                        case 1:
                            Rev_OK_01B.BackColor = Color.DarkSeaGreen;
                            Rev_NOOK_01B.BackColor = Color.LightGray;
                            break;
                        case 2:
                             Rev_OK_02B.BackColor = Color.DarkSeaGreen;
                            Rev_NOOK_02B.BackColor = Color.LightGray;
                            break;
                        case 3:
                            Rev_OK_03B.BackColor = Color.DarkSeaGreen;
                            Rev_NOOK_03B.BackColor = Color.LightGray;
                            break;

                        case 4:
                            Rev_OK_04B.BackColor = Color.DarkSeaGreen;
                            Rev_NOOK_04B.BackColor = Color.LightGray;
                            break;
                    }
                    break;
                case "NO OK":
                    switch (Linea)
                    {
                        case 1:
                            Rev_OK_01B.BackColor = Color.LightGray;
                            Rev_NOOK_01B.BackColor = Color.IndianRed;
                            Aviso2LB.Show();
                            break;
                        case 2:
                            Rev_OK_02B.BackColor = Color.LightGray;
                            Rev_NOOK_02B.BackColor = Color.IndianRed;
                            Aviso2LB.Show();
                            break;
                        case 3:
                            Rev_OK_03B.BackColor = Color.LightGray;
                            Rev_NOOK_03B.BackColor = Color.IndianRed;
                            break;
                        case 4:
                            Rev_OK_04B.BackColor = Color.LightGray;
                            Rev_NOOK_04B.BackColor = Color.IndianRed;
                            Aviso2LB.Show();
                            break;
                    }
                    break;
                case "-":
                    switch (Linea)
                    {
                        case 1:
                            Rev_OK_01B.BackColor = Color.FromArgb(27, 33, 41);
                            Rev_NOOK_01B.BackColor = Color.FromArgb(27, 33, 41);
                            break;
                        case 2:
                            Rev_OK_02B.BackColor = Color.FromArgb(27, 33, 41);
                            Rev_NOOK_02B.BackColor = Color.FromArgb(27, 33, 41);
                            break;
                        case 3:
                            Rev_OK_03B.BackColor = Color.FromArgb(27, 33, 41);
                            Rev_NOOK_03B.BackColor = Color.FromArgb(27, 33, 41);
                            break;
                        case 4:
                            Rev_OK_04B.BackColor = Color.FromArgb(27, 33, 41);
                            Rev_NOOK_04B.BackColor = Color.FromArgb(27, 33, 41);
                            break;
                    }
                    break;
            }
        }

        //Marcamos los botones de ok/ no ok
        private void Rev_OK_1B_Click(object sender, EventArgs e)
        {
            if (EstadoRev1 == "OK") { EstadoRev1 = "-"; ColorBoton(EstadoRev1, 1); }
            else EstadoRev1 = "OK"; ColorBoton(EstadoRev1, 1);

        }            
        private void Rev_NOOK_1B_Click(object sender, EventArgs e)
        {
            if (EstadoRev1 == "NO OK") { EstadoRev1 = "-"; ColorBoton(EstadoRev1, 1); }
            else EstadoRev1 = "NO OK"; ColorBoton(EstadoRev1, 1);
        }
        private void Rev_OK_2B_Click(object sender, EventArgs e)
        {
            if (EstadoRev2 == "OK") { EstadoRev2 = "-"; ColorBoton(EstadoRev2, 2); }
            else EstadoRev2 = "OK"; ColorBoton(EstadoRev2, 2);
        }
        private void Rev_NOOK_2B_Click(object sender, EventArgs e)
        {
            if (EstadoRev2 == "NO OK") { EstadoRev2 = "-"; ColorBoton(EstadoRev2, 2); }
            else EstadoRev2 = "NO OK"; ColorBoton(EstadoRev2, 2);
        }
        private void Rev_OK_3B_Click(object sender, EventArgs e)
        {
            if (EstadoRev3 == "OK") { EstadoRev3 = "-"; ColorBoton(EstadoRev3, 3); }
            else EstadoRev3 = "OK"; ColorBoton(EstadoRev3, 3);
        }
        private void Rev_NOOK_3B_Click(object sender, EventArgs e)
        {
            if (EstadoRev3 == "NO OK") { EstadoRev3 = "-"; ColorBoton(EstadoRev3, 3); }
            else EstadoRev3 = "NO OK"; ColorBoton(EstadoRev3, 3);
        }
        private void Rev_OK_4B_Click(object sender, EventArgs e)
        {
            if (EstadoRev4 == "OK") { EstadoRev4 = "-"; ColorBoton(EstadoRev4, 4); }

            else EstadoRev4 = "OK"; ColorBoton(EstadoRev4, 4);

        }
        private void Rev_NOOK_4B_Click(object sender, EventArgs e)
        {
            if (EstadoRev4 == "NO OK") { EstadoRev4 = "-"; ColorBoton(EstadoRev4, 4); }

            else EstadoRev4 = "NO OK"; ColorBoton(EstadoRev4, 4);
        }


        private void BorrarB_Click(object sender, EventArgs e)
        {
            Hora1TB.Text = "";
            Hora2TB.Text = "";
            Hora3TB.Text = "";
            Hora4TB.Text = "";
            Estado11TB.Text = "";
            Estado12TB.Text = "";
            Estado13TB.Text = "";
            Estado14TB.Text = "";
            Estado21TB.Text = "";
            Estado22TB.Text = "";
            Estado23TB.Text = "";
            Estado24TB.Text = "";
            Estado31TB.Text = "";
            Estado32TB.Text = "";
            Estado33TB.Text = "";
            Estado34TB.Text = "";
            Estado41TB.Text = "";
            Estado42TB.Text = "";
            Estado43TB.Text = "";
            Estado44TB.Text = "";

            if (MaquinaLinea.numlin == 2)
            {
                Properties.Settings.Default.HoraRev1EtiqL2 = "";
                Properties.Settings.Default.EstadoRev11EtiqL2 = Estado11TB.Text;
                Properties.Settings.Default.EstadoRev12EtiqL2 = Estado12TB.Text;
                Properties.Settings.Default.EstadoRev13EtiqL2 = Estado13TB.Text;
                Properties.Settings.Default.EstadoRev14EtiqL2 = Estado14TB.Text;

                Properties.Settings.Default.HoraRev2EtiqL2 = "";
                Properties.Settings.Default.EstadoRev21EtiqL2 = Estado21TB.Text;
                Properties.Settings.Default.EstadoRev22EtiqL2 = Estado22TB.Text;
                Properties.Settings.Default.EstadoRev23EtiqL2 = Estado23TB.Text;
                Properties.Settings.Default.EstadoRev24EtiqL2 = Estado24TB.Text;

                Properties.Settings.Default.HoraRev3EtiqL2 = "";
                Properties.Settings.Default.EstadoRev31EtiqL2 = Estado31TB.Text;
                Properties.Settings.Default.EstadoRev32EtiqL2 = Estado32TB.Text;
                Properties.Settings.Default.EstadoRev33EtiqL2 = Estado33TB.Text;
                Properties.Settings.Default.EstadoRev34EtiqL2 = Estado34TB.Text;

                Properties.Settings.Default.HoraRev4EtiqL2 = "";
                Properties.Settings.Default.EstadoRev41EtiqL2 = Estado41TB.Text;
                Properties.Settings.Default.EstadoRev42EtiqL2 = Estado42TB.Text;
                Properties.Settings.Default.EstadoRev43EtiqL2 = Estado43TB.Text;
                Properties.Settings.Default.EstadoRev44EtiqL2 = Estado44TB.Text;
            }
            if (MaquinaLinea.numlin == 3)
            {

                Properties.Settings.Default.HoraRev1EtiqL3 = "";
                Properties.Settings.Default.EstadoRev11EtiqL3 = Estado11TB.Text;
                Properties.Settings.Default.EstadoRev12EtiqL3 = Estado12TB.Text;
                Properties.Settings.Default.EstadoRev13EtiqL3 = Estado13TB.Text;
                Properties.Settings.Default.EstadoRev14EtiqL3 = Estado14TB.Text;

                Properties.Settings.Default.HoraRev2EtiqL3 = "";
                Properties.Settings.Default.EstadoRev21EtiqL3 = Estado21TB.Text;
                Properties.Settings.Default.EstadoRev22EtiqL3 = Estado22TB.Text;
                Properties.Settings.Default.EstadoRev23EtiqL3 = Estado23TB.Text;
                Properties.Settings.Default.EstadoRev24EtiqL3 = Estado24TB.Text;

                Properties.Settings.Default.HoraRev3EtiqL3 = "";
                Properties.Settings.Default.EstadoRev31EtiqL3 = Estado31TB.Text;
                Properties.Settings.Default.EstadoRev32EtiqL3 = Estado32TB.Text;
                Properties.Settings.Default.EstadoRev33EtiqL3 = Estado33TB.Text;
                Properties.Settings.Default.EstadoRev34EtiqL3 = Estado34TB.Text;

                Properties.Settings.Default.HoraRev4EtiqL3 = "";
                Properties.Settings.Default.EstadoRev41EtiqL3 = Estado41TB.Text;
                Properties.Settings.Default.EstadoRev42EtiqL3 = Estado42TB.Text;
                Properties.Settings.Default.EstadoRev43EtiqL3 = Estado43TB.Text;
                Properties.Settings.Default.EstadoRev44EtiqL3 = Estado44TB.Text;

            }
            if (MaquinaLinea.numlin == 5)
            {
                Properties.Settings.Default.HoraRev1EtiqL5 = "";
                Properties.Settings.Default.EstadoRev11EtiqL5 = Estado11TB.Text;
                Properties.Settings.Default.EstadoRev12EtiqL5 = Estado12TB.Text;
                Properties.Settings.Default.EstadoRev13EtiqL5 = Estado13TB.Text;
                Properties.Settings.Default.EstadoRev14EtiqL5 = Estado14TB.Text;

                Properties.Settings.Default.HoraRev2EtiqL5 = "";
                Properties.Settings.Default.EstadoRev21EtiqL5 = Estado21TB.Text;
                Properties.Settings.Default.EstadoRev22EtiqL5 = Estado22TB.Text;
                Properties.Settings.Default.EstadoRev23EtiqL5 = Estado23TB.Text;
                Properties.Settings.Default.EstadoRev24EtiqL5 = Estado24TB.Text;

                Properties.Settings.Default.HoraRev3EtiqL5 = "";
                Properties.Settings.Default.EstadoRev31EtiqL5 = Estado31TB.Text;
                Properties.Settings.Default.EstadoRev32EtiqL5 = Estado32TB.Text;
                Properties.Settings.Default.EstadoRev33EtiqL5 = Estado33TB.Text;
                Properties.Settings.Default.EstadoRev34EtiqL5 = Estado34TB.Text;

                Properties.Settings.Default.HoraRev4EtiqL5 = "";
                Properties.Settings.Default.EstadoRev41EtiqL5 = Estado41TB.Text;
                Properties.Settings.Default.EstadoRev42EtiqL5 = Estado42TB.Text;
                Properties.Settings.Default.EstadoRev43EtiqL5 = Estado43TB.Text;
                Properties.Settings.Default.EstadoRev44EtiqL5 = Estado44TB.Text;
            }
            Properties.Settings.Default.Save();
        }


        private void saveBot_Click(object sender, EventArgs e)
        {
            if (HoraTB.Text != "")
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
                string[] valores13 = new string[2];
                string[] valores14 = new string[2];


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
                valores3[1] = MaquinaLinea.MEtiquetadora;
                listavalores.Add(valores3);
                valores4[0] = "Turno";
                valores4[1] = turnoTB.Text;
                listavalores.Add(valores4);
                valores5[0] = "Hora1";
                valores5[1] = HoraTB.Text;
                listavalores.Add(valores5);
                valores6[0] = "Revision1";
                valores6[1] = Revision01TB.Text;
                listavalores.Add(valores6);
                valores7[0] = "Estado1";
                valores7[1] = EstadoRev1;
                listavalores.Add(valores7);
                valores8[0] = "Revision2";
                valores8[1] = Revision02TB.Text;
                listavalores.Add(valores8);
                valores9[0] = "Estado2";
                valores9[1] = EstadoRev2;
                listavalores.Add(valores9);
                valores10[0] = "Revision3";
                valores10[1] = Revision03TB.Text;
                listavalores.Add(valores10);
                valores11[0] = "Estado3";
                valores11[1] = EstadoRev3;
                listavalores.Add(valores11);
                valores12[0] = "Revision4";
                valores12[1] = Revision04TB.Text;
                listavalores.Add(valores12);
                valores13[0] = "Estado4";
                valores13[1] = EstadoRev4;
                listavalores.Add(valores13);
                valores14[0] = "Comentarios";
                valores14[1] = ComentarioBOX.Text;
                listavalores.Add(valores14);

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileEtiquetadora, "VisionArtificial", listavalores, "Id");
                //MessageBox.Show(salida);
                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                }
                else
                {
                    if (MaquinaLinea.numlin == 2)
                    {
                        if (Hora4TB.Text == "" && Hora3TB.Text != "")
                        {
                            Hora4TB.Text = HoraTB.Text;
                            Estado41TB.Text = EstadoRev1;
                            Estado42TB.Text = EstadoRev2;
                            Estado43TB.Text = EstadoRev3;
                            Estado44TB.Text = EstadoRev4;
                            Properties.Settings.Default.HoraRev4EtiqL2 = HoraTB.Text;
                            Properties.Settings.Default.EstadoRev41EtiqL2 = Estado41TB.Text;
                            Properties.Settings.Default.EstadoRev42EtiqL2 = Estado42TB.Text;
                            Properties.Settings.Default.EstadoRev43EtiqL2 = Estado43TB.Text;
                            Properties.Settings.Default.EstadoRev44EtiqL2 = Estado44TB.Text;
                        }
                        if (Hora3TB.Text == "" && Hora2TB.Text != "")
                        {
                            Hora3TB.Text = HoraTB.Text;
                            Estado31TB.Text = EstadoRev1;
                            Estado32TB.Text = EstadoRev2;
                            Estado33TB.Text = EstadoRev3;
                            Estado34TB.Text = EstadoRev4;
                            Properties.Settings.Default.HoraRev3EtiqL2 = HoraTB.Text;
                            Properties.Settings.Default.EstadoRev31EtiqL2 = Estado31TB.Text;
                            Properties.Settings.Default.EstadoRev32EtiqL2 = Estado32TB.Text;
                            Properties.Settings.Default.EstadoRev33EtiqL2 = Estado33TB.Text;
                            Properties.Settings.Default.EstadoRev34EtiqL2 = Estado34TB.Text;
                        }
                        if (Hora2TB.Text == "" && Hora1TB.Text != "")
                        {
                            Hora2TB.Text = HoraTB.Text;
                            Estado21TB.Text = EstadoRev1;
                            Estado22TB.Text =EstadoRev2;
                            Estado23TB.Text =EstadoRev3;
                            Estado24TB.Text =EstadoRev4;
                            Properties.Settings.Default.HoraRev2EtiqL2 = HoraTB.Text;
                            Properties.Settings.Default.EstadoRev21EtiqL2 = Estado21TB.Text;
                            Properties.Settings.Default.EstadoRev22EtiqL2 = Estado22TB.Text;
                            Properties.Settings.Default.EstadoRev23EtiqL2 = Estado23TB.Text;
                            Properties.Settings.Default.EstadoRev24EtiqL2 = Estado24TB.Text;
                        }
                        if (Hora1TB.Text == "")
                        {
                            Hora1TB.Text = HoraTB.Text;
                            Estado11TB.Text = EstadoRev1;
                            Estado12TB.Text =EstadoRev2;
                            Estado13TB.Text =EstadoRev3;
                            Estado14TB.Text =EstadoRev4;
                            Properties.Settings.Default.HoraRev1EtiqL2 = HoraTB.Text;
                            Properties.Settings.Default.EstadoRev11EtiqL2 = Estado11TB.Text;
                            Properties.Settings.Default.EstadoRev12EtiqL2 = Estado12TB.Text;
                            Properties.Settings.Default.EstadoRev13EtiqL2 = Estado13TB.Text;
                            Properties.Settings.Default.EstadoRev14EtiqL2 = Estado14TB.Text;
                        }
                    }
                    if (MaquinaLinea.numlin == 3)
                    {
                        if (Hora4TB.Text == "" && Hora3TB.Text != "")
                        {
                            Hora4TB.Text = HoraTB.Text;
                            Estado41TB.Text = EstadoRev1;
                            Estado42TB.Text = EstadoRev2;
                            Estado43TB.Text = EstadoRev3;
                            Estado44TB.Text = EstadoRev4;
                            Properties.Settings.Default.HoraRev4EtiqL3 = HoraTB.Text;
                            Properties.Settings.Default.EstadoRev41EtiqL3 = Estado41TB.Text;
                            Properties.Settings.Default.EstadoRev42EtiqL3 = Estado42TB.Text;
                            Properties.Settings.Default.EstadoRev43EtiqL3 = Estado43TB.Text;
                            Properties.Settings.Default.EstadoRev44EtiqL3 = Estado44TB.Text;
                        }
                        if (Hora3TB.Text == "" && Hora2TB.Text != "")
                        {
                            Hora3TB.Text = HoraTB.Text;
                            Estado31TB.Text = EstadoRev1;
                            Estado32TB.Text = EstadoRev2;
                            Estado33TB.Text = EstadoRev3;
                            Estado34TB.Text = EstadoRev4;
                            Properties.Settings.Default.HoraRev3EtiqL3 = HoraTB.Text;
                            Properties.Settings.Default.EstadoRev31EtiqL3 = Estado31TB.Text;
                            Properties.Settings.Default.EstadoRev32EtiqL3 = Estado32TB.Text;
                            Properties.Settings.Default.EstadoRev33EtiqL3 = Estado33TB.Text;
                            Properties.Settings.Default.EstadoRev34EtiqL3 = Estado34TB.Text;
                        }
                        if (Hora2TB.Text == "" && Hora1TB.Text != "")
                        {
                            Hora2TB.Text = HoraTB.Text;
                            Estado21TB.Text = EstadoRev1;
                            Estado22TB.Text = EstadoRev2;
                            Estado23TB.Text = EstadoRev3;
                            Estado24TB.Text = EstadoRev4;
                            Properties.Settings.Default.HoraRev2EtiqL3 = HoraTB.Text;
                            Properties.Settings.Default.EstadoRev21EtiqL3 = Estado21TB.Text;
                            Properties.Settings.Default.EstadoRev22EtiqL3 = Estado22TB.Text;
                            Properties.Settings.Default.EstadoRev23EtiqL3 = Estado23TB.Text;
                            Properties.Settings.Default.EstadoRev24EtiqL3 = Estado24TB.Text;
                        }
                        if (Hora1TB.Text == "")
                        {
                            Hora1TB.Text = HoraTB.Text;
                            Estado11TB.Text = EstadoRev1;
                            Estado12TB.Text = EstadoRev2;
                            Estado13TB.Text = EstadoRev3;
                            Estado14TB.Text = EstadoRev4;
                            Properties.Settings.Default.HoraRev1EtiqL3 = HoraTB.Text;
                            Properties.Settings.Default.EstadoRev11EtiqL3 = Estado11TB.Text;
                            Properties.Settings.Default.EstadoRev12EtiqL3 = Estado12TB.Text;
                            Properties.Settings.Default.EstadoRev13EtiqL3 = Estado13TB.Text;
                            Properties.Settings.Default.EstadoRev14EtiqL3 = Estado14TB.Text;
                        }
                    }
                    if (MaquinaLinea.numlin == 5)
                    {
                        if (Hora4TB.Text == "" && Hora3TB.Text != "")
                        {
                            Hora4TB.Text = HoraTB.Text;
                            Estado41TB.Text = EstadoRev1;
                            Estado42TB.Text = EstadoRev2;
                            Estado43TB.Text = EstadoRev3;
                            Estado44TB.Text = EstadoRev4;
                            Properties.Settings.Default.HoraRev4EtiqL5 = HoraTB.Text;
                            Properties.Settings.Default.EstadoRev41EtiqL5 = Estado41TB.Text;
                            Properties.Settings.Default.EstadoRev42EtiqL5 = Estado42TB.Text;
                            Properties.Settings.Default.EstadoRev43EtiqL5 = Estado43TB.Text;
                            Properties.Settings.Default.EstadoRev44EtiqL5 = Estado44TB.Text;
                        }
                        if (Hora3TB.Text == "" && Hora2TB.Text != "")
                        {
                            Hora3TB.Text = HoraTB.Text;
                            Estado31TB.Text = EstadoRev1;
                            Estado32TB.Text = EstadoRev2;
                            Estado33TB.Text = EstadoRev3;
                            Estado34TB.Text = EstadoRev4;
                            Properties.Settings.Default.HoraRev3EtiqL5 = HoraTB.Text;
                            Properties.Settings.Default.EstadoRev31EtiqL5 = Estado31TB.Text;
                            Properties.Settings.Default.EstadoRev32EtiqL5 = Estado32TB.Text;
                            Properties.Settings.Default.EstadoRev33EtiqL5 = Estado33TB.Text;
                            Properties.Settings.Default.EstadoRev34EtiqL5 = Estado34TB.Text;
                        }
                        if (Hora2TB.Text == "" && Hora1TB.Text != "")
                        {
                            Hora2TB.Text = HoraTB.Text;
                            Estado21TB.Text = EstadoRev1;
                            Estado22TB.Text = EstadoRev2;
                            Estado23TB.Text = EstadoRev3;
                            Estado24TB.Text = EstadoRev4;
                            Properties.Settings.Default.HoraRev2EtiqL5 = HoraTB.Text;
                            Properties.Settings.Default.EstadoRev21EtiqL5 = Estado21TB.Text;
                            Properties.Settings.Default.EstadoRev22EtiqL5 = Estado22TB.Text;
                            Properties.Settings.Default.EstadoRev23EtiqL5 = Estado23TB.Text;
                            Properties.Settings.Default.EstadoRev24EtiqL5 = Estado24TB.Text;
                        }
                        if (Hora1TB.Text == "")
                        {
                            Hora1TB.Text = HoraTB.Text;
                            Estado11TB.Text = EstadoRev1;
                            Estado12TB.Text = EstadoRev2;
                            Estado13TB.Text = EstadoRev3;
                            Estado14TB.Text = EstadoRev4;
                            Properties.Settings.Default.HoraRev1EtiqL5 = HoraTB.Text;
                            Properties.Settings.Default.EstadoRev11EtiqL5 = Estado11TB.Text;
                            Properties.Settings.Default.EstadoRev12EtiqL5 = Estado12TB.Text;
                            Properties.Settings.Default.EstadoRev13EtiqL5 = Estado13TB.Text;
                            Properties.Settings.Default.EstadoRev14EtiqL5 = Estado14TB.Text;
                        }
                    }
                    Properties.Settings.Default.Save();
                    //Restablecemos los valores correspondientes
                    HoraTB.Text = "";
                    EstadoRev1 = "-";
                    EstadoRev2 = "-";
                    EstadoRev3 = "-";
                    EstadoRev4 = "-";
                    ComentarioBOX.Text = "";
                    Rev_OK_01B.BackColor = Color.FromArgb(27, 33, 41);
                    Rev_OK_02B.BackColor = Color.FromArgb(27, 33, 41);
                    Rev_OK_03B.BackColor = Color.FromArgb(27, 33, 41);
                    Rev_OK_04B.BackColor = Color.FromArgb(27, 33, 41);

                    Rev_NOOK_01B.BackColor = Color.FromArgb(27, 33, 41);
                    Rev_NOOK_02B.BackColor = Color.FromArgb(27, 33, 41);
                    Rev_NOOK_03B.BackColor = Color.FromArgb(27, 33, 41);
                    Rev_NOOK_04B.BackColor = Color.FromArgb(27, 33, 41);
                }
            }

            else
            {
                MessageBox.Show("FALTAN CAMPOS POR RELLENAR! -> NO SE HA GUARDADO LA INFORMACIÓN");
            }
        }

    }
}
    


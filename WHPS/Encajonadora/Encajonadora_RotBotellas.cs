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

namespace WHPS.Encajonadora
{
    public partial class Encajonadora_RotBotellas : Form
    {
        //Variables del form
        string botrotas;
        string limpieza_area = "";
        string limpieza_trab = "";
        string Turno = "";
        public static MainEncajonadora parent;
        public Encajonadora_RotBotellas(MainEncajonadora p)
        {
            InitializeComponent();
            parent = p;
        }
        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>
        private void ExitB_Click(object sender, EventArgs e)
        {
            Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainEncajonadora));
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
        private void Encajonadora_RotBotellas_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            //numberpad1.Visible = false;
            Turno = MaquinaLinea.turno;

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            if (MainEncajonadora.DatosProduccion[1]!="") MaquinaLinea.ExtraerDatosMateriales(MaquinaLinea.ExtraerCodigoBotellaRota(MainEncajonadora.DatosProduccion[1]));
            //Ocultamos los cuadros que no son necesarios
            DatosRoturaBOX.Hide();
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
            //if (MaquinaLinea.TecladoWindows == 1 && ContraseñaTB.Text == "")
            //{
            //    Utilidades.MostrarTecladoPredeterminado(null);
            //    ContraseñaTB.Visible = true;
            //    ContraseñaTB.UseSystemPasswordChar = true;
            //    ContraseñaTB.Select();
            //}
            //if (MaquinaLinea.TecladoWindows == 2)
            //{
            //    Utilidades.ParametrosTeclado(true, 0);
            //    //numberpad1.Location = new Point(450, 250);
            //    //numberpad1.Visible = true;
            //}
            if (ContraseñaTB.Text == "1111")
            {
                ConfrRespB.BackColor = Color.DarkSeaGreen;
                MaquinaLinea.Password = true;
                ContraseñaTB.Text = "";
            }
            ContraseñaTB.Visible = true;
            ContraseñaTB.UseSystemPasswordChar = true;
            WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this, ContraseñaTB);


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
                    case "1111":
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
            //    if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(numrotasTB);
            //    if (MaquinaLinea.TecladoWindows == 2)
            //    {
            //        Utilidades.ParametrosTeclado(false, 0);
            //        //numberpad1.Location = new Point(450, 250);
            //        //numberpad1.Visible = true;
            //    }
            WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this, numrotasTB);

        }



        //Si la contraseña es correcta y han sido rellenados todos los campos se guardará la información
        private void saveBot_Click(object sender, EventArgs e)
        {
            if ((MaquinaLinea.Password == true && numrotasTB.Text != "") || botrotas == "NO")
            {
                   
                string salida = MaquinaLinea.GuardarFormRoturaBotellas("Encajonadora", MaquinaLinea.MEncajonadora, botrotas, numrotasTB.Text, limpieza_area, limpieza_trab, "Encajonadora");
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
                    Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainEncajonadora));
                    this.Hide();
                    this.Dispose();
                }
            }
        }
    }
}

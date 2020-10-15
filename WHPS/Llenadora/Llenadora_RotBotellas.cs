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
    public partial class Llenadora_RotBotellas : Form
    {
        //Variables del form
        string botrotas;
        string maquina = "";
        string limpieza_area = "";
        string limpieza_trab = "";
        string Turno = "";

        public Llenadora_RotBotellas()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>
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

        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        private void Llenadora_RotBotellas_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            //numberpad1.Visible = false;
            Turno = Utilidades.ObtenerTurnoActual();

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            if (MaquinaLinea.RotCodProd != "") MaquinaLinea.ExtraerDatosMateriales(MaquinaLinea.ExtraerCodigoBotellaRota(MaquinaLinea.RotCodProd));
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
            if(ContraseñaTB.Text == "1111")
            {
                ConfrRespB.BackColor = Color.DarkSeaGreen;
                MaquinaLinea.Password = true;
                ContraseñaTB.Text = "";
            }            
                ContraseñaTB.Visible = true;
                ContraseñaTB.UseSystemPasswordChar = true;
                WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this, ContraseñaTB);

            
        }
        private void CapsuladoraB_Click(object sender, EventArgs e)
        {
            maquina = "Capsuladora";

            CapsuladoraB.BackColor = Color.DarkSeaGreen;
            TapRoscB.BackColor = Color.LightGray;
            LlenadoraB.BackColor = Color.LightGray;
            EnjuagadoraB.BackColor = Color.LightGray;
        }

        private void TapRoscB_Click(object sender, EventArgs e)
        {
            maquina = "Taponadora/Roscadora";

            TapRoscB.BackColor = Color.DarkSeaGreen;
            CapsuladoraB.BackColor = Color.LightGray;
            LlenadoraB.BackColor = Color.LightGray;
            EnjuagadoraB.BackColor = Color.LightGray;
        }

        private void LlenadoraB_Click(object sender, EventArgs e)
        {
            maquina = "Llenadora";

            LlenadoraB.BackColor = Color.DarkSeaGreen;
            CapsuladoraB.BackColor = Color.LightGray;
            TapRoscB.BackColor = Color.LightGray;
            EnjuagadoraB.BackColor = Color.LightGray;
        }

        private void EnjuagadoraB_Click(object sender, EventArgs e)
        {
            maquina = "Enjuagadora";

            EnjuagadoraB.BackColor = Color.DarkSeaGreen;
            CapsuladoraB.BackColor = Color.LightGray;
            TapRoscB.BackColor = Color.LightGray;
            LlenadoraB.BackColor = Color.LightGray;
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
            WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this, numrotasTB);

            //numberpad1.Location = new Point(450, 250);
            //numberpad1.Visible = true;
        }
    
        //private void numberpad1_VisibleChanged(object sender, EventArgs e)
        //{
        //    if (MaquinaLinea.StatusTeclado == true) Utilidades.EscribirTeclado(numberpad1, numrotasTB, ConfrRespB);
        //}


        //Si la contraseña es correcta y han sido rellenados todos los campos se guardará la información
        private void saveBot_Click(object sender, EventArgs e)
        {
            if ((MaquinaLinea.Password == true && numrotasTB.Text != "") || botrotas == "NO")
            {

                string salida = MaquinaLinea.GuardarFormRoturaBotellas("Llenadora", MaquinaLinea.MLlenadora, botrotas, numrotasTB.Text, limpieza_area, limpieza_trab, maquina);
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

                    MainLlenadora Form = new MainLlenadora();
                    Hide();
                    Form.Show();
                    GC.Collect();
                }
            }
        }
    }
}

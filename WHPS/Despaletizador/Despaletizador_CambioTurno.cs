using System;
using System.Drawing;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.Parte;
using WHPS.ProgramMenus;

namespace WHPS.Despaletizador
{
    public partial class Despaletizador_CambioTurno : Form
    {
        public string limpio = "";
        public string protecciones = "";
        public string cuter = "";
        public string herramientas = "";
        public string EstadoTurno = "";
        public static MainDespaletizador parent;

        public Despaletizador_CambioTurno(MainDespaletizador p)
        {
            InitializeComponent();
            parent = p;
        }

        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>       
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
        private void Despaletizador_CambioTurno_Load(object sender, EventArgs e)
        {
            //Muestra la hora ya que el timer tarda 1s en tomar el control del Label lbReloj.
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;
        }

        /// <summary>
        /// Función del temporizador que se ejecuta cada segundo, indíca la hora y activa la alarma. 
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        //#######################  BOTONES DECORACIÓN  ##############################
        private void btnSiPuestoLimpio_Click(object sender, EventArgs e)
        {
            limpio = "SI";
            btnSiPuestoLimpio.BackColor = Color.DarkSeaGreen;
            btnNoPuestoLimpio.BackColor = Color.Gray;
        }
        private void btnNoPuestoLimpio_Click(object sender, EventArgs e)
        {
            limpio = "NO";

            btnSiPuestoLimpio.BackColor = Color.Gray;
            btnNoPuestoLimpio.BackColor = Color.IndianRed;
        }
        private void btnSiProtecciones_Click(object sender, EventArgs e)
        {
            protecciones = "SI";
            btnSiProtecciones.BackColor = Color.DarkSeaGreen;
            btnNoProtecciones.BackColor = Color.Gray;
        }
        private void btnNoProtecciones_Click(object sender, EventArgs e)
        {
            protecciones = "NO";
            btnSiProtecciones.BackColor = Color.Gray;
            btnNoProtecciones.BackColor = Color.IndianRed;
        }
        private void btnSiCuterUbicado_Click(object sender, EventArgs e)
        {
            cuter = "SI";
            btnSiCuterUbicado.BackColor = Color.DarkSeaGreen;
            btnNoCuterUbicado.BackColor = Color.Gray;
        }
        private void btnNoCuterUbicado_Click(object sender, EventArgs e)
        {
            cuter = "NO";
            btnSiCuterUbicado.BackColor = Color.Gray;
            btnNoCuterUbicado.BackColor = Color.IndianRed;
        }
        private void btnSiHerramientas_Click(object sender, EventArgs e)
        {
            herramientas = "SI";
            btnSiHerramientas.BackColor = Color.DarkSeaGreen;
            btnNoHerramientas.BackColor = Color.Gray;
        }
        private void btnNoHerramientas_Click(object sender, EventArgs e)
        {
            herramientas = "NO";
            btnSiHerramientas.BackColor = Color.Gray;
            btnNoHerramientas.BackColor = Color.IndianRed;
        }
        //###########################################################################

        //###############  GUARDADO DE LOS DATOS DE INICIO DEL TURNO  ###############
        private void saveBot_Click(object sender, EventArgs e)
        {
            //Guardamos si todos los campos han sido rellenados
            if (limpio != "" && protecciones != "" && cuter != "" && herramientas != "")
            {
                string salida = MaquinaLinea.GuardarFormCambioTurno("Despaletizador", MaquinaLinea.MDespaletizador, limpio, cuter, herramientas, protecciones);
                //Si existe algun error de salvado de datos se expondrá en un MESSAGE 
                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show(Properties.Settings.Default.AvisoProblemaFichero);
                    //MessageBox.Show(salida);
                }
                //Cuando los datos han sido salvados correctamente reestablecemos los parámetros
                else
                {
                    //En función que que estado estemos, cambiaremos a otro.
                    if (MaquinaLinea.numlin == 2) Properties.Settings.Default.chDesL2 = (Properties.Settings.Default.chDesL2 == true) ? false : true;
                    if (MaquinaLinea.numlin == 3) Properties.Settings.Default.chDesL2 = (Properties.Settings.Default.chDesL3 == true) ? false : true;
                    if (MaquinaLinea.numlin == 5) Properties.Settings.Default.chDesL2 = (Properties.Settings.Default.chDesL5 == true) ? false : true;

                    //Cargamos las variables ya que han sido modificadas
                    MaquinaLinea.chDesL2 = Properties.Settings.Default.chDesL2;
                    MaquinaLinea.chDesL3 = Properties.Settings.Default.chDesL3;
                    MaquinaLinea.chDesL5 = Properties.Settings.Default.chDesL5;
                    Properties.Settings.Default.Save();

                    //Si algún elemento no está en el estado que debe, se mostrará el form de comentarios si el operio lo puede justificar
                    if (limpio == "NO" || protecciones == "NO" || cuter == "NO" || herramientas == "NO")
                    {
                        DialogResult opcion;
                        opcion = MessageBox.Show("Algunos de los campos selecciondos no se encuentra en el estado que debería. ¿Puede indicar a que se debe?", "", MessageBoxButtons.YesNo);
                        if (opcion == DialogResult.Yes)
                        {
                            Utilidades.AbrirForm(parent.GetComentarios(), parent, typeof(Despaletizador_Comentarios));
                            this.Hide();
                            this.Dispose();
                        }
                        else
                        {

                            Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainDespaletizador));
                            this.Hide();
                            this.Dispose();
                        }
                    }
                    else
                    {
                        Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainDespaletizador));
                        this.Hide();
                        this.Dispose();
                    }
                }
            }
            //Si los campos no han sido rellenados un MESSAGE avisará al operario
            else
            {
                MessageBox.Show(Properties.Settings.Default.AvisoCampos);
            }
        }
        //###########################################################################
    }
}
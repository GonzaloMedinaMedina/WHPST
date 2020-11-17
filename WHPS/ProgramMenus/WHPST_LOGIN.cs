using System;
using System.Drawing;
using System.Windows.Forms;
using WHPS.Model;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace WHPS.ProgramMenus
{
    public partial class WHPST_LOGIN : Form
    {
        public bool usuariocorrecto = false;
        WHPST_INICIO parent;
        public WHPST_LOGIN(WHPST_INICIO p)
        {
            InitializeComponent();
            parent = p;
        }

        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>
        private void CerrarB_Click(object sender, EventArgs e)
        {
            parent.AbrirFormHijo(parent.GetMenu(), "Menu");
            Utilidades.AbrirForm(parent, this,typeof(WHPST_INICIO));
        }

        /// <summary>
        /// Boton muestra el form de ajustes.
        /// </summary>
        private void WHPST_LOGIN_Load(object sender, EventArgs e)
        {
            usuariocorrecto = false;
            ContraseñaTB.UseSystemPasswordChar = false;
        }


        /// <summary>
        /// Funciones que te permiten mover el form haciendo click en la barra superior
        /// </summary>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void BarraPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }



        /// <summary>
        /// Aspectos visuales que facilitan la compresion del campo a rellenar
        /// </summary>
        private void UsuarioTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (UsuarioTB.Text == "USUARIO")
            {
                UsuarioTB.Text = "";
                UsuarioTB.ForeColor = Color.Gray;
            }
            if (ContraseñaTB.Text == "")
            {
                ContraseñaTB.UseSystemPasswordChar = false;
                ContraseñaTB.ForeColor = Color.DarkSlateGray;
                ContraseñaTB.Text = "CONTRASEÑA";
            }
            //############## ABRIR ON SCREEN KEYBOARD  ###############
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
            WHPST_LOGIN.ActiveForm.Activate();
            UsuarioTB.Select();
        }
        private void ContraseñaTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (ContraseñaTB.Text == "CONTRASEÑA")
            {
                ContraseñaTB.Text = "";
                ContraseñaTB.ForeColor = Color.Gray;
            }
            if (UsuarioTB.Text == "")
            {
                UsuarioTB.ForeColor = Color.DarkSlateGray;
                UsuarioTB.Text = "USUARIO";
            }
            ContraseñaTB.UseSystemPasswordChar = true;
            //############## ABRIR ON SCREEN KEYBOARD  ###############
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
            WHPST_LOGIN.ActiveForm.Activate();
            ContraseñaTB.Select();
        }
        private void ContraseñaTB_TextChanged(object sender, EventArgs e)
        {


            if (ContraseñaTB.Text == "CONTRASEÑA")
            {
                ContraseñaTB.UseSystemPasswordChar = false;
            }
            else
            {
                ContraseñaTB.UseSystemPasswordChar = true;
                ContraseñaTB.ForeColor = Color.DarkSlateGray;
            }


        }

        /// <summary>
        /// Boton que determina si el usuario introducido es correcto.
        /// </summary>
        private void AccederB_Click(object sender, EventArgs e)
        {
            ComprobarUsuario();
        }
        private void ContraseñaTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ComprobarUsuario();
        }
        private void UsuarioTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ComprobarUsuario();
        }



        /// <summary>
        /// Función que comprueba si el usuario intriducido es correcto.
        /// </summary>
        public void ComprobarUsuario()
        {
            if (UsuarioTB.Text == MaquinaLinea.UsuarioAdministracion && ContraseñaTB.Text == MaquinaLinea.ContraseñaAdministracion)
            {
                usuariocorrecto = true;
                Properties.Settings.Default.Usuario = "Administracion";
                Properties.Settings.Default.Save();
                MaquinaLinea.usuario = Properties.Settings.Default.Usuario;
                MaquinaLinea.VolverInicioA = RetornoInicio.Menu;
                Utilidades.AbrirForm(parent, this, typeof(WHPST_INICIO));
            }
            if (UsuarioTB.Text == MaquinaLinea.UsuarioCalidad && ContraseñaTB.Text == MaquinaLinea.ContraseñaCalidad)
            {
                usuariocorrecto = true;
                Properties.Settings.Default.Usuario = "Calidad";
                Properties.Settings.Default.Save();
                MaquinaLinea.usuario = Properties.Settings.Default.Usuario;
                MaquinaLinea.VolverInicioA = RetornoInicio.Menu;
                Utilidades.AbrirForm(parent, this, typeof(WHPST_INICIO));
            }
            if (UsuarioTB.Text == MaquinaLinea.UsuarioEncargado && ContraseñaTB.Text == MaquinaLinea.ContraseñaEncargado)
            {
                usuariocorrecto = true;
                Properties.Settings.Default.Usuario = "Encargado";
                Properties.Settings.Default.Save();
                MaquinaLinea.usuario = Properties.Settings.Default.Usuario;
                MaquinaLinea.VolverInicioA = RetornoInicio.Menu;
                Utilidades.AbrirForm(parent, this, typeof(WHPST_INICIO));
            }
            if (UsuarioTB.Text == MaquinaLinea.UsuarioOficina && ContraseñaTB.Text == MaquinaLinea.ContraseñaOficina)
            {
                usuariocorrecto = true;
                Properties.Settings.Default.Usuario = "Oficina";
                Properties.Settings.Default.Save();
                MaquinaLinea.usuario = Properties.Settings.Default.Usuario;
                MaquinaLinea.VolverInicioA = RetornoInicio.Menu;
                Utilidades.AbrirForm(parent, this, typeof(WHPST_INICIO));
            }
            if (usuariocorrecto == false)
            {
                ContraseñaTB.UseSystemPasswordChar = false;
                ContraseñaTB.ForeColor = Color.Red;
                UsuarioTB.ForeColor = Color.Red;
                UsuarioTB.Text = "USUARIO";
                ContraseñaTB.PasswordChar = '\0';
                ContraseñaTB.Text = "CONTRASEÑA";
            }
        }
    } 
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WHPS.Administracion;
using WHPS.Model;
using WHPS.Utiles;
using System.Drawing;
using WHPS.Produccion;
using WHPS.Parte;
using System.IO;
using System.Diagnostics;

namespace WHPS.ProgramMenus
{
    public partial class WHPST_INICIO : Form
    {

        public WHPST_INICIO()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Boton con el que cierras la aplicación
        /// </summary>
        private void ExitB_Click(object sender, EventArgs e)
        {
            DialogResult opcion;
            opcion = MessageBox.Show("¿Quieres cerrar el programa?", "", MessageBoxButtons.OKCancel);
            if (opcion == DialogResult.OK)
            {
                Environment.Exit(0);
            }
        }
        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

        //#################       BOTONES BARRA MENU       #################
        /// <summary>
        /// Botones con los que vas abriendo los diferentes forms hijo del programa
        /// </summary>
        private void Menu_Click(object sender, EventArgs e)
        {
            ColorBoton("Menu", MaquinaLinea.usuario);
            //Abrimos el form segundario del menu
            AbrirFormHijo(new WHPST_MENU());
        }
        private void Linea2B_Click(object sender, EventArgs e)
        {
            ColorBoton("L2", MaquinaLinea.usuario);
            MaquinaLinea.numlin = 2;
            if (MaquinaLinea.checkL2 == true)
            {
                AbrirFormHijo(new WHPST_SELECTMAQ());
            }
            else
            {
                AbrirFormHijo(new WHPST_Cambio_Turno());
            }
        }
        private void Linea3B_Click(object sender, EventArgs e)
        {
            ColorBoton("L3", MaquinaLinea.usuario);
            MaquinaLinea.numlin = 3;
            if (MaquinaLinea.checkL3 == true)
            {
                AbrirFormHijo(new WHPST_SELECTMAQ());
            }
            else
            {
                AbrirFormHijo(new WHPST_Cambio_Turno());
            }
        }
        private void Linea5B_Click(object sender, EventArgs e)
        {
            ColorBoton("L5", MaquinaLinea.usuario);
            MaquinaLinea.numlin = 5;
            if (MaquinaLinea.checkL5 == true)
            {
                AbrirFormHijo(new WHPST_SELECTMAQ());
            }
            else
            {
                AbrirFormHijo(new WHPST_Cambio_Turno());
            }
        }
        public void LanzamientoB_Click(object sender, EventArgs e)
        {
            ColorBoton("Lanzamiento", MaquinaLinea.usuario);
            //Abrimos el form segundario del lanzamiento

            AbrirFormHijo(new WHPST_LANZ());
            
        }
        private void ProduccionB_Click(object sender, EventArgs e)
        {
            MainProduccion Form = new MainProduccion();
            Hide();
            Form.Show();
        }
        private void CalidadB_Click(object sender, EventArgs e)
        {
            MainAdministracion_Calidad Form = new MainAdministracion_Calidad();
            Hide();
            Form.Show();
        }
        private void BOMB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.RetornoInicio = "BOM";

            WHPST_BOM Form = new WHPST_BOM();
            Hide();
            Form.Show();
        }
        private void ParteB_Click(object sender, EventArgs e)
        {
            MainParte Form = new MainParte();
            Hide();
            Form.Show();
        }
        private void AjustesB_Click(object sender, EventArgs e)
        {
            ColorBoton("Ajustes", MaquinaLinea.usuario);
            AbrirFormHijo(new WHPST_AJUSTES());
        }
        private void SesionB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.usuario == "")
            {
                WHPST_LOGIN Form = new WHPST_LOGIN();
                Hide();
                Form.Show();
            }

            if (MaquinaLinea.usuario == "Administracion" || MaquinaLinea.usuario == "Oficina" || MaquinaLinea.usuario == "Calidad" || MaquinaLinea.usuario == "Encargado")
            {
                DialogResult opcion;
                opcion = MessageBox.Show("¿Quieres cerrar la sesión de " + MaquinaLinea.usuario + "?", "", MessageBoxButtons.OKCancel);
                if (opcion == DialogResult.OK)
                {
                    Properties.Settings.Default.Usuario = "";
                    Properties.Settings.Default.Save();
                    MaquinaLinea.usuario = Properties.Settings.Default.Usuario;
                    SesionPanel.BackColor = MaquinaLinea.COLOR1;
                    SesionB.Image = Properties.Resources.MenuIniciarSesion50x50;
                    SesionB.Text = "    Iniciar Sesión";
                    Linea2B.Visible = true;
                    Linea2Panel.Visible = true;
                    Linea3B.Visible = true;
                    Linea3Panel.Visible = true;
                    Linea5B.Visible = true;
                    Linea5Panel.Visible = true;
                    CalidadB.Visible = false;
                    ProduccionPanel.Visible = false;
                    CalidadPanel.Visible = false;
                    ProduccionB.Visible = false;
                    LanzamientoB.Visible = false;
                    LanzaminetoPanel.Visible = false;
                    ProduccionB.Visible = false;
                    CalidadPanel.Visible = false;
                    BOMB.Visible = false;
                    BOMPanel.Visible = false;
                    ParteB.Visible = false;
                    Partepanel.Visible = false;
                    AjustesPanel.Visible = false;
                    AjustesB.Visible = false;
                    MinimizarB.Visible = false;

                }
            }
        }
        //##################################################################

        //#################       FUNCIONES       #################
        /// <summary>
        /// Función que abre un form hijo en un panel determinado.
        /// </summary>
        /// <param name="WHPST_FORM">Parámetro que indica que form que desea abrir.</param>
        public void AbrirFormHijo(object WHPST_FORM)
        {
            if (this.PanelInicio.Controls.Count > 0)
            {
                this.PanelInicio.Controls.RemoveAt(0);
            }
            Form SM = WHPST_FORM as Form;
            SM.TopLevel = false;
            SM.Dock = DockStyle.Fill;
            this.PanelInicio.Controls.Add(SM);
            this.PanelInicio.Tag = SM;
            SM.Show();
        }

        /// <summary>
        /// Funcion embellezedora que cambia el color de un boton, si este abre un form hijo.
        /// </summary>
        /// <param name="Boton">Variable que indica que boton tiene que cambiar su color.</param>
        private void ColorBoton(string Boton, string Usuario)
        {
            switch (Boton)
            {
                case "Menu":
                    Menu.BackColor = MaquinaLinea.COLOR1;
                    MenuPanel.BackColor = Color.Gainsboro;
                    Linea2B.BackColor = Color.Transparent;
                    Linea2Panel.BackColor = MaquinaLinea.COLOR1;
                    Linea3B.BackColor = Color.Transparent;
                    Linea3Panel.BackColor = MaquinaLinea.COLOR1;
                    Linea5B.BackColor = Color.Transparent;
                    Linea5Panel.BackColor = MaquinaLinea.COLOR1;
                    LanzamientoB.BackColor = Color.Transparent;
                    LanzaminetoPanel.BackColor = MaquinaLinea.COLOR1;
                    ProduccionB.BackColor = Color.Transparent;
                    CalidadPanel.BackColor = MaquinaLinea.COLOR1;
                    CalidadB.BackColor = Color.Transparent;
                    ProduccionPanel.BackColor = MaquinaLinea.COLOR1;
                    BOMB.BackColor = Color.Transparent;
                    BOMPanel.BackColor = MaquinaLinea.COLOR1;
                    ParteB.BackColor = Color.Transparent;
                    Partepanel.BackColor = MaquinaLinea.COLOR1;
                    AjustesB.BackColor = Color.Transparent;
                    AjustesPanel.BackColor = MaquinaLinea.COLOR1;
                    PanelMinimizar.BackColor = Color.FromArgb(240,240,240);
                    switch (Usuario)
                    {
                        case "":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = MaquinaLinea.COLOR1;
                            break;
                        case "Administracion":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkTurquoise;
                            break;
                        case "Oficina":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkViolet;
                            break;
                        case "Encargado":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.OrangeRed;
                            break;
                        case "Calidad":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.Lime;
                            break;
                    }
                    break;
                case "L2":
                    Menu.BackColor = Color.Transparent;
                    MenuPanel.BackColor = MaquinaLinea.COLOR1;
                    Linea2B.BackColor = MaquinaLinea.COLOR1;
                    Linea2Panel.BackColor = Color.Gainsboro;
                    Linea3B.BackColor = Color.Transparent;
                    Linea3Panel.BackColor = MaquinaLinea.COLOR1;
                    Linea5B.BackColor = Color.Transparent;
                    Linea5Panel.BackColor = MaquinaLinea.COLOR1;
                    LanzamientoB.BackColor = Color.Transparent;
                    LanzaminetoPanel.BackColor = MaquinaLinea.COLOR1;
                    ProduccionB.BackColor = Color.Transparent;
                    CalidadPanel.BackColor = MaquinaLinea.COLOR1;
                    CalidadB.BackColor = Color.Transparent;
                    ProduccionPanel.BackColor = MaquinaLinea.COLOR1;
                    BOMB.BackColor = Color.Transparent;
                    BOMPanel.BackColor = MaquinaLinea.COLOR1;
                    ParteB.BackColor = Color.Transparent;
                    Partepanel.BackColor = MaquinaLinea.COLOR1;
                    AjustesB.BackColor = Color.Transparent;
                    AjustesPanel.BackColor = MaquinaLinea.COLOR1;
                    PanelMinimizar.BackColor = Color.White;

                    switch (Usuario)
                    {
                        case "":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = MaquinaLinea.COLOR1;
                            break;
                        case "Administracion":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkTurquoise;
                            break;
                        case "Oficina":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkViolet;
                            break;
                        case "Encargado":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.OrangeRed;
                            break;
                        case "Calidad":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.Lime;
                            break;
                    }
                    break;
                case "L3":
                    Menu.BackColor = Color.Transparent;
                    MenuPanel.BackColor = MaquinaLinea.COLOR1;
                    Linea2B.BackColor = Color.Transparent;
                    Linea2Panel.BackColor = MaquinaLinea.COLOR1;
                    Linea3B.BackColor = MaquinaLinea.COLOR1;
                    Linea3Panel.BackColor = Color.Gainsboro;
                    Linea5B.BackColor = Color.Transparent;
                    Linea5Panel.BackColor = MaquinaLinea.COLOR1;
                    LanzamientoB.BackColor = Color.Transparent;
                    LanzaminetoPanel.BackColor = MaquinaLinea.COLOR1;
                    ProduccionB.BackColor = Color.Transparent;
                    CalidadPanel.BackColor = MaquinaLinea.COLOR1;
                    CalidadB.BackColor = Color.Transparent;
                    ProduccionPanel.BackColor = MaquinaLinea.COLOR1;
                    BOMB.BackColor = Color.Transparent;
                    BOMPanel.BackColor = MaquinaLinea.COLOR1;
                    ParteB.BackColor = Color.Transparent;
                    Partepanel.BackColor = MaquinaLinea.COLOR1;
                    AjustesB.BackColor = Color.Transparent;
                    AjustesPanel.BackColor = MaquinaLinea.COLOR1;
                    PanelMinimizar.BackColor = Color.White;

                    switch (Usuario)
                    {
                        case "":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = MaquinaLinea.COLOR1;
                            break;
                        case "Administracion":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkTurquoise;
                            break;
                        case "Oficina":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkViolet;
                            break;
                        case "Encargado":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.OrangeRed;
                            break;
                        case "Calidad":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.Lime;
                            break;
                    }
                    break;
                case "L5":
                    Menu.BackColor = Color.Transparent;
                    MenuPanel.BackColor = MaquinaLinea.COLOR1;
                    Linea2B.BackColor = Color.Transparent;
                    Linea2Panel.BackColor = MaquinaLinea.COLOR1;
                    Linea3B.BackColor = Color.Transparent;
                    Linea3Panel.BackColor = MaquinaLinea.COLOR1;
                    Linea5B.BackColor = MaquinaLinea.COLOR1;
                    Linea5Panel.BackColor = Color.Gainsboro;
                    LanzamientoB.BackColor = Color.Transparent;
                    LanzaminetoPanel.BackColor = MaquinaLinea.COLOR1;
                    ProduccionB.BackColor = Color.Transparent;
                    CalidadPanel.BackColor = MaquinaLinea.COLOR1;
                    CalidadB.BackColor = Color.Transparent;
                    ProduccionPanel.BackColor = MaquinaLinea.COLOR1;
                    BOMB.BackColor = Color.Transparent;
                    BOMPanel.BackColor = MaquinaLinea.COLOR1;
                    ParteB.BackColor = Color.Transparent;
                    Partepanel.BackColor = MaquinaLinea.COLOR1;
                    AjustesB.BackColor = Color.Transparent;
                    AjustesPanel.BackColor = MaquinaLinea.COLOR1;
                    PanelMinimizar.BackColor = Color.White;

                    switch (Usuario)
                    {
                        case "":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = MaquinaLinea.COLOR1;
                            break;
                        case "Administracion":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkTurquoise;
                            break;
                        case "Oficina":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkViolet;
                            break;
                        case "Encargado":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.OrangeRed;
                            break;
                        case "Calidad":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.Lime;
                            break;
                    }
                    break;
                case "Lanzamiento":
                    Menu.BackColor = Color.Transparent;
                    MenuPanel.BackColor = MaquinaLinea.COLOR1;
                    Linea2B.BackColor = Color.Transparent;
                    Linea2Panel.BackColor = MaquinaLinea.COLOR1;
                    Linea3B.BackColor = Color.Transparent;
                    Linea3Panel.BackColor = MaquinaLinea.COLOR1;
                    Linea5B.BackColor = Color.Transparent;
                    Linea5Panel.BackColor = MaquinaLinea.COLOR1;
                    LanzamientoB.BackColor = MaquinaLinea.COLOR1;
                    LanzaminetoPanel.BackColor = Color.Gainsboro;
                    ProduccionB.BackColor = Color.Transparent;
                    CalidadPanel.BackColor = MaquinaLinea.COLOR1;
                    CalidadB.BackColor = Color.Transparent;
                    ProduccionPanel.BackColor = MaquinaLinea.COLOR1;
                    BOMB.BackColor = Color.Transparent;
                    BOMPanel.BackColor = MaquinaLinea.COLOR1;
                    ParteB.BackColor = Color.Transparent;
                    Partepanel.BackColor = MaquinaLinea.COLOR1;
                    AjustesB.BackColor = Color.Transparent;
                    AjustesPanel.BackColor = MaquinaLinea.COLOR1;
                    PanelMinimizar.BackColor = Color.White;

                    switch (Usuario)
                    {
                        case "":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = MaquinaLinea.COLOR1;
                            break;
                        case "Administracion":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkTurquoise;
                            break;
                        case "Oficina":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkViolet;
                            break;
                        case "Encargado":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.OrangeRed;
                            break;
                        case "Calidad":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.Lime;
                            break;
                    }
                    break;
                case "Ajustes":
                    Menu.BackColor = Color.Transparent;
                    MenuPanel.BackColor = MaquinaLinea.COLOR1;
                    Linea2B.BackColor = Color.Transparent;
                    Linea2Panel.BackColor = MaquinaLinea.COLOR1;
                    Linea3B.BackColor = Color.Transparent;
                    Linea3Panel.BackColor = MaquinaLinea.COLOR1;
                    Linea5B.BackColor = Color.Transparent;
                    Linea5Panel.BackColor = MaquinaLinea.COLOR1;
                    LanzamientoB.BackColor = Color.Transparent;
                    LanzaminetoPanel.BackColor = MaquinaLinea.COLOR1;
                    ProduccionB.BackColor = Color.Transparent;
                    CalidadPanel.BackColor = MaquinaLinea.COLOR1;
                    CalidadB.BackColor = Color.Transparent;
                    ProduccionPanel.BackColor = MaquinaLinea.COLOR1;
                    BOMB.BackColor = Color.Transparent;
                    BOMPanel.BackColor = MaquinaLinea.COLOR1;
                    ParteB.BackColor = Color.Transparent;
                    Partepanel.BackColor = MaquinaLinea.COLOR1;
                    AjustesB.BackColor = MaquinaLinea.COLOR1;
                    AjustesPanel.BackColor = Color.Gainsboro;
                    PanelMinimizar.BackColor = Color.White;

                    switch (Usuario)
                    {
                        case "":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = MaquinaLinea.COLOR1;
                            break;
                        case "Administracion":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkTurquoise;
                            break;
                        case "Oficina":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkViolet;
                            break;
                        case "Encargado":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.OrangeRed;
                            break;
                        case "Calidad":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.Lime;
                            break;
                    }
                    break;
                case "Produccion":
                    Menu.BackColor = Color.Transparent;
                    MenuPanel.BackColor = MaquinaLinea.COLOR1;
                    Linea2B.BackColor = Color.Transparent;
                    Linea2Panel.BackColor = MaquinaLinea.COLOR1;
                    Linea3B.BackColor = Color.Transparent;
                    Linea3Panel.BackColor = MaquinaLinea.COLOR1;
                    Linea5B.BackColor = Color.Transparent;
                    Linea5Panel.BackColor = MaquinaLinea.COLOR1;
                    LanzamientoB.BackColor = Color.Transparent;
                    LanzaminetoPanel.BackColor = MaquinaLinea.COLOR1;
                    ParteB.BackColor = Color.Transparent;
                    Partepanel.BackColor = MaquinaLinea.COLOR1;
                    AjustesB.BackColor = Color.Transparent;
                    AjustesPanel.BackColor = MaquinaLinea.COLOR1;
                    PanelMinimizar.BackColor = Color.White;

                    switch (Usuario)
                    {
                        case "":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = MaquinaLinea.COLOR1;
                            break;
                        case "Administracion":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkTurquoise;
                            break;
                        case "Oficina":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkViolet;
                            break;
                        case "Encargado":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.OrangeRed;
                            break;
                        case "Calidad":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.Lime;
                            break;
                    }
                    break;
                case "Calidad":
                    Menu.BackColor = Color.Transparent;
                    MenuPanel.BackColor = MaquinaLinea.COLOR1;
                    Linea2B.BackColor = Color.Transparent;
                    Linea2Panel.BackColor = MaquinaLinea.COLOR1;
                    Linea3B.BackColor = Color.Transparent;
                    Linea3Panel.BackColor = MaquinaLinea.COLOR1;
                    Linea5B.BackColor = Color.Transparent;
                    Linea5Panel.BackColor = MaquinaLinea.COLOR1;
                    LanzamientoB.BackColor = Color.Transparent;
                    LanzaminetoPanel.BackColor = MaquinaLinea.COLOR1;
                    ParteB.BackColor = Color.Transparent;
                    Partepanel.BackColor = MaquinaLinea.COLOR1;
                    AjustesB.BackColor = Color.Transparent;
                    AjustesPanel.BackColor = MaquinaLinea.COLOR1;
                    PanelMinimizar.BackColor = Color.White;

                    switch (Usuario)
                    {
                        case "":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = MaquinaLinea.COLOR1;
                            break;
                        case "Administracion":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkTurquoise;
                            break;
                        case "Oficina":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.DarkViolet;
                            break;
                        case "Encargado":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.OrangeRed;
                            break;
                        case "Calidad":
                            SesionB.BackColor = Color.Transparent;
                            SesionPanel.BackColor = Color.Lime;
                            break;
                    }
                    break;
            }
        }

        internal void Abrir_SelectMaquina()
        {
            AbrirFormHijo(new WHPST_SELECTMAQ());
        }

        /// <summary>
        /// Función que detecta si cambia el turno o el día y si esto ocurre pone a FALSE el CHECK de las 3 lineas.
        /// </summary>
        private void Carga_Personal()
        {
            Utilidades.ShiftCheck();
            //Generamos la lista de valores a filtrar y traer desde la lista de materiales AND, 'Columna', LIKE, 'Codigo Material'
            List<string[]> listavalores = new List<string[]>();

            //Llamamos la busqueda del fichero excel
            //Rellenamos el turno - Identificando el turno
            string Turno = "";
            int diaC = Convert.ToInt16(DateTime.Now.ToString("dd"));
            int hora = Convert.ToInt16(DateTime.Now.ToString("HH"));

            if (hora >= 7 && hora < 15)
            {
                Turno = "Mañana";
            }
            else
            {
                if (hora >= 15 && hora < 23)
                {
                    Turno = "Tarde";
                }
                else { Turno = "Noche"; }
            }
            if ((Turno != Properties.Settings.Default.turno) || (diaC != Properties.Settings.Default.diaT))
            {
                Properties.Settings.Default.checkL2 = false;
                Properties.Settings.Default.checkL3 = false;
                Properties.Settings.Default.checkL5 = false;
                Properties.Settings.Default.Save();
            }
        }
        //##########################################################

        //###################       OTROS       ###################
        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        private void WHPST_INICIO_Load(object sender, EventArgs e)
        {
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //Comenzamos mostrando inicialmente el MENU
            ColorBoton("Menu", MaquinaLinea.usuario);
            AbrirFormHijo(new WHPST_MENU());

            //Declaramos inicialmente variables que utilizaremos en todo el programa
            //Properties.Settings.Default.Reset();
            MaquinaLinea.usuario = Properties.Settings.Default.Usuario;
            MaquinaLinea.diaT = Properties.Settings.Default.diaT;
            MaquinaLinea.turno = "";
            MaquinaLinea.switchT = Properties.Settings.Default.switchT;
            MaquinaLinea.checkL2 = Properties.Settings.Default.checkL2;
            MaquinaLinea.checkL3 = Properties.Settings.Default.checkL3;
            MaquinaLinea.checkL5 = Properties.Settings.Default.checkL5;

            MaquinaLinea.chDesL2 = Properties.Settings.Default.chDesL2;
            MaquinaLinea.chLlenL2 = Properties.Settings.Default.chLlenL2;
            MaquinaLinea.chEtiqL2 = Properties.Settings.Default.chEtiqL2;
            MaquinaLinea.chEncL2 = Properties.Settings.Default.chEncL2;
            MaquinaLinea.chConL2 = Properties.Settings.Default.chConL2;

            MaquinaLinea.chDesL3 = Properties.Settings.Default.chDesL3;
            MaquinaLinea.chLlenL3 = Properties.Settings.Default.chLlenL3;
            MaquinaLinea.chEtiqL3 = Properties.Settings.Default.chEtiqL3;
            MaquinaLinea.chEncL3 = Properties.Settings.Default.chEncL3;
            MaquinaLinea.chConL3 = Properties.Settings.Default.chConL3;

            MaquinaLinea.chDesL5 = Properties.Settings.Default.chDesL5;
            MaquinaLinea.chLlenL5 = Properties.Settings.Default.chLlenL5;
            MaquinaLinea.chEtiqL5 = Properties.Settings.Default.chEtiqL5;
            MaquinaLinea.chEncL5 = Properties.Settings.Default.chEncL5;
            MaquinaLinea.chConL5 = Properties.Settings.Default.chConL5;

            MaquinaLinea.chalarma = Properties.Settings.Default.chalarma;
            MaquinaLinea.alarmah1 = Properties.Settings.Default.alarmah1;
            MaquinaLinea.alarmam1 = Properties.Settings.Default.alarmam1;
            MaquinaLinea.alarmah2 = Properties.Settings.Default.alarmah2;
            MaquinaLinea.alarmam2 = Properties.Settings.Default.alarmam2;
            MaquinaLinea.alarmah3 = Properties.Settings.Default.alarmah3;
            MaquinaLinea.alarmam3 = Properties.Settings.Default.alarmam3;

            //Si volvemos de un form padre, tenemos que mostrar el form hijo por el que nos hemos ido, por ello queda registrado en una variable
            if (MaquinaLinea.BackL2 == true || MaquinaLinea.RetornoInicio == "SelecMaquinaL2")
            {
                ColorBoton("L2", MaquinaLinea.usuario);
                AbrirFormHijo(new WHPST_SELECTMAQ());
                MaquinaLinea.BackL2 = false;
            }
            if (MaquinaLinea.BackL3 == true || MaquinaLinea.RetornoInicio == "SelecMaquinaL3")
            {
                ColorBoton("L3", MaquinaLinea.usuario);
                AbrirFormHijo(new WHPST_SELECTMAQ());
                MaquinaLinea.BackL3 = false;
            }
            if (MaquinaLinea.BackL5 == true || MaquinaLinea.RetornoInicio == "SelecMaquinaL5")
            {
                ColorBoton("L5", MaquinaLinea.usuario);
                AbrirFormHijo(new WHPST_SELECTMAQ());
                MaquinaLinea.BackL5 = false;
            }
            if (MaquinaLinea.RetornoInicio == "Menu")
            {
                ColorBoton("Menu", MaquinaLinea.usuario);
                AbrirFormHijo(new WHPST_MENU());
                MaquinaLinea.RetornoInicio = "";
            }
            if (MaquinaLinea.RetornoInicio == "CambioTurno")
            {
                if (MaquinaLinea.numlin == 2) ColorBoton("L2", MaquinaLinea.usuario);
                if (MaquinaLinea.numlin == 3) ColorBoton("L3", MaquinaLinea.usuario);
                if (MaquinaLinea.numlin == 5) ColorBoton("L5", MaquinaLinea.usuario);
                AbrirFormHijo(new WHPST_Cambio_Turno());
                MaquinaLinea.RetornoInicio = "";
            }

            //Se modifica el menu en función de que usuario se registre
            if (MaquinaLinea.usuario == "Administracion")
            {
                SesionPanel.BackColor = Color.DarkTurquoise;
                SesionB.Image = Properties.Resources.MenuCerrarSesion50x50;
                SesionB.Text = "    Cerrar Sesión";
                ProduccionB.Visible = true;
                ProduccionPanel.Visible = true;
                CalidadB.Visible = true;
                CalidadPanel.Visible = true;
                LanzamientoB.Visible=true;
                LanzaminetoPanel.Visible=true;
                BOMB.Visible=true;
                BOMPanel.Visible=true;
                ParteB.Visible = true;
                Partepanel.Visible = true;
                AjustesB.Visible = true;
                AjustesPanel.Visible = true;
            }
            if (MaquinaLinea.usuario == "Calidad")
            {
                SesionPanel.BackColor = Color.Lime;
                SesionB.Image = Properties.Resources.MenuCerrarSesion50x50;
                SesionB.Text = "    Cerrar Sesión";
                Linea2B.Visible = false;
                Linea2Panel.Visible = false;
                Linea3B.Visible = false;
                Linea3Panel.Visible = false;
                Linea5B.Visible = false;
                Linea5Panel.Visible = false;
                ProduccionB.Visible = false;
                ProduccionPanel.Visible = false;
                CalidadB.Visible = true;
                CalidadPanel.Visible = true;
                LanzamientoB.Visible=true;
                LanzaminetoPanel.Visible=true;
                ParteB.Visible = false;
                Partepanel.Visible = false;
                BOMB.Visible=true;
                BOMPanel.Visible=true;
            }
            if (MaquinaLinea.usuario == "Encargado")
            {
                SesionPanel.BackColor = Color.OrangeRed;
                SesionB.Image = Properties.Resources.MenuCerrarSesion50x50;
                SesionB.Text = "    Cerrar Sesión";
                Linea2B.Visible = false;
                Linea2Panel.Visible = false;
                Linea3B.Visible = false;
                Linea3Panel.Visible = false;
                Linea5B.Visible = false;
                Linea5Panel.Visible = false;
                ProduccionB.Visible = true;
                ProduccionPanel.Visible = true;
                CalidadB.Visible = false;
                CalidadPanel.Visible = false;
                LanzamientoB.Visible = true;
                LanzaminetoPanel.Visible = true;
                ParteB.Visible = true;
                Partepanel.Visible = true;
                BOMB.Visible = true;
                BOMPanel.Visible=true;
            }
            if (MaquinaLinea.usuario == "Oficina")
            {
                SesionPanel.BackColor = Color.DarkViolet;
                SesionB.Image = Properties.Resources.MenuCerrarSesion50x50;
                SesionB.Text = "    Cerrar Sesión";
                Linea2B.Visible = false;
                Linea2Panel.Visible = false;
                Linea3B.Visible = false;
                Linea3Panel.Visible = false;
                Linea5B.Visible = false;
                Linea5Panel.Visible = false;
                ProduccionB.Visible =   false;
                ProduccionPanel.Visible = false;
                CalidadB.Visible = true;
                CalidadPanel.Visible = true;
                LanzamientoB.Visible = true;
                LanzaminetoPanel.Visible = true;
                ParteB.Visible = false;
                Partepanel.Visible = false;
                BOMB.Visible = true;
                BOMPanel.Visible = true;
            }
            if (MaquinaLinea.usuario == "")
            {
                SesionB.Image = Properties.Resources.MenuIniciarSesion50x50;
                SesionB.Text = "    Iniciar Sesión";
                CalidadB.Visible = false;
                CalidadPanel.Visible = false;
                ProduccionB.Visible = false;
                ProduccionPanel.Visible = false;
                LanzamientoB.Visible = false;
                LanzaminetoPanel.Visible = false;
                ParteB.Visible = false;
                Partepanel.Visible = false;
                AjustesB.Visible = false;
                AjustesPanel.Visible = false;
                BOMB.Visible = false;
                BOMPanel.Visible = false;
            }

            //EN ESTE PUNTO SABEMOS SI HAY QUE ACTUALIZAR, REALIZAMOS LA CONSULTA POR SI QUIERE O NO ACTUALIZAR LA APLICACIÓN
            if (MaquinaLinea.ACTUALIZARPC == "SI")
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Hay una nueva actualización, se reiniciará la aplicación. ¿Quieres actualizar la aplicación?", "", MessageBoxButtons.YesNo);
                if (opcion == DialogResult.Yes)
                {
                    //ANTES DE ACTUALIZAR, CAMBIAMOS EL EXCEL ACTUALIZACION Y MODIFICAMOS LA FECHA.
                    //Filtro Encargado
                    List<string[]> valoresAFiltrar = new List<string[]>();
                    string[] filterval = new string[4];
                    filterval[0] = "AND";
                    filterval[1] = "IDPC";
                    filterval[2] = "LIKE";
                    filterval[3] = "\""+Properties.Settings.Default.IDPC+"\"";
                    valoresAFiltrar.Add(filterval);

                    List<string[]> valoresAActualizar = new List<string[]>();
                    string[] updateval = new string[2];
                    updateval[0] = "ACTUALIZACION";
                    updateval[1] = "NO";
                    valoresAActualizar.Add(updateval);
                    string salida;
                    salida = ExcelUtiles.ActualizarFicheroExcel("ComprobarActualizacion", "ACTUALIZACION", valoresAActualizar, valoresAFiltrar);
                    //MessageBox.Show(salida);

                    //### DESPALETIZADOR #####
                    updateval[0] = "ULTIMAFECHA";
                    updateval[1] = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
                    salida = ExcelUtiles.ActualizarFicheroExcel("ComprobarActualizacion", "ACTUALIZACION", valoresAActualizar, valoresAFiltrar);
                    //MessageBox.Show(salida);

                    //ACTUALIZAMOS LOS SETTINGS
                    MaquinaLinea.ACTUALIZARPC = "NO";
                    File.Delete(Properties.Settings.Default.RutaFichero + Properties.Settings.Default.NombreFichero);
                    File.Copy(Properties.Settings.Default.RutaFichero_Red + Properties.Settings.Default.NombreFichero, Properties.Settings.Default.RutaFichero + Properties.Settings.Default.NombreFichero);
                    Process.Start(MaquinaLinea.FileActualizacion);
                    Environment.Exit(0);
                }

            }

        }

        /// <summary>
        /// Funciones y acciones con las que realizamos el efecto que ocultar y mostrar la barra del menu
        /// </summary>
        private void timerocultarmenu_Tick(object sender, EventArgs e)
        {
            if (PanelMenu.Width <= 70)
            {
                timerocultarmenu.Enabled = false;
            }
            else
            {
                PanelMenu.Width = PanelMenu.Width - 10;
                PanelMenu2.Width = PanelMenu2.Width - 10;
                //PanelInicio.Width = PanelInicio.Width - 10;
            }
        }
        private void timermostrarmenu_Tick(object sender, EventArgs e)
        {
            if (PanelMenu.Width >= 225)
            {
                timermostrarmenu.Enabled = false;
            }
            else
            {
                PanelMenu.Width = PanelMenu.Width + 10;
                PanelMenu2.Width = PanelMenu2.Width + 10;
                //PanelInicio.Width = PanelInicio.Width + 10;

            }
        }
        private void BarraMenuB_Click(object sender, EventArgs e)
        {
            if (PanelMenu.Width <= 70)
            {
                timermostrarmenu.Enabled = true;
            }
            else
            {
                timerocultarmenu.Enabled = true;
            }
        }
        public void Abrir_Lanzamiento()
        {
            AbrirFormHijo(new WHPST_LANZ());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!MaquinaLinea.mostrar_whpst_inicio)
            {
                MaquinaLinea.mostrar_whpst_inicio = true;
                this.Dispose();
                this.Hide();
            }
            //Para que el form selecmaq no se quede abierto, lo cerramos si venimos de el, es decir, si la variable es true.
            if (MaquinaLinea.AbrirCambioTurno == true)
            {

                MaquinaLinea.AbrirCambioTurno = false;
                //if (MaquinaLinea.numlin == 2) Linea2B.BackColor = Color.LightGray;
                //if (MaquinaLinea.numlin == 3) Linea3B.BackColor = Color.LightGray;
                //if (MaquinaLinea.numlin == 5) Linea5B.BackColor = Color.LightGray;
                AbrirFormHijo(new WHPST_Cambio_Turno());
                this.Update();
            }
            if (MaquinaLinea.RetornoInicio == "SelecMaquina")
            {

                MaquinaLinea.RetornoInicio = "";
                //if (MaquinaLinea.numlin == 2) Linea2B.BackColor = Color.LightGray;
                //if (MaquinaLinea.numlin == 3) Linea3B.BackColor = Color.LightGray;
                //if (MaquinaLinea.numlin == 5) Linea5B.BackColor = Color.LightGray;
                AbrirFormHijo(new WHPST_SELECTMAQ());
                this.Update();

            }

        }
        //#########################################################
    }


      
}

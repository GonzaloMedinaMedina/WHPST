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
        //Array que indica los botones que se deben mostrar.
        bool[] ArrayBotones = new bool[18];

        public static WHPST_AJUSTES FormAjustes;
        public static WHPST_BOM FormBOM;
        public static WHPST_Cambio_Turno FormCambioTurno;
        public static WHPST_LANZ FormLanz;
        public static WHPST_LOGIN FormLogin;
        public static WHPST_MENU FormMenu;
        public static MainAdministracion_Calidad FormAdministracionCalidad;
        public static MainParte FormParte;
        public static MainProduccion FormProduccion;
        public static WHPST_SELECTMAQ FormSelecMaq;


        public WHPST_INICIO()
        {
            InitializeComponent();

            //FormAjustes = new WHPST_AJUSTES(this);
            //FormBOM = new WHPST_BOM(this);
            //FormCambioTurno = new WHPST_Cambio_Turno(this);
            //FormLanz = new WHPST_LANZ(this);
            //FormLogin = new WHPST_LOGIN(this);
            //FormAdministracionCalidad = new MainAdministracion_Calidad(this);
            //FormParte = new MainParte(this);
            //FormProduccion = new MainProduccion(this);
            //FormSelecMaq = new WHPST_SELECTMAQ(this);
        }

        public WHPST_INICIO(Form F)
        {
            InitializeComponent();

            /* FormAjustes = new WHPST_AJUSTES(this);
             FormBOM = new WHPST_BOM(this);
             FormCambioTurno = new WHPST_Cambio_Turno(this);
             FormLanz = new WHPST_LANZ(this);
             FormLogin = new WHPST_LOGIN(this);
             FormAdministracionCalidad = new MainAdministracion_Calidad(this);
             FormParte = new MainParte(this);
             FormProduccion = new MainProduccion(this);
             FormSelecMaq = new WHPST_SELECTMAQ(this);*/

            /*   switch (op)
               {
                   case 0:
                       break;
                   case 1:
                       break;

                   case 2:
                       break;

                   case 3:
                       break;

                   case 4:
                       break;

                   case 5:
                       break;

                   case 6:
                       break;

                   case 7:

                       break;

               }*/
        }

        /// <summary>
        /// Boton con el que cierras la aplicación
        /// </summary>
        private void ExitB_Click(object sender, EventArgs e)
        {
            DialogResult opcion = MessageBox.Show("¿Quieres cerrar el programa?", "", MessageBoxButtons.OKCancel);
            if (opcion == DialogResult.OK)Environment.Exit(0);
        }
        
        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        public void WHPST_INICIO_Load(object sender, EventArgs e)
        {
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //Ocultamos los botnes a los que no se tienen aceso.
            PermisosUsuario(MaquinaLinea.usuario);

            //Mostramos el form hijo correspondiente
            MostrarForm();

            //Si se requiere actualizacion ejecutamos la función.
            if (MaquinaLinea.ACTUALIZARPC == "SI") ActualizarAPP();


            ////Declaramos inicialmente variables que utilizaremos en todo el programa
            ////Properties.Settings.Default.Reset();
            //MaquinaLinea.usuario = Properties.Settings.Default.Usuario;
            //MaquinaLinea.diaT = Properties.Settings.Default.diaT;
            //MaquinaLinea.turno = "";
            //MaquinaLinea.switchT = Properties.Settings.Default.switchT;
            //MaquinaLinea.checkL2 = Properties.Settings.Default.checkL2;
            //MaquinaLinea.checkL3 = Properties.Settings.Default.checkL3;
            //MaquinaLinea.checkL5 = Properties.Settings.Default.checkL5;

            //MaquinaLinea.chDesL2 = Properties.Settings.Default.chDesL2;
            //MaquinaLinea.chLlenL2 = Properties.Settings.Default.chLlenL2;
            //MaquinaLinea.chEtiqL2 = Properties.Settings.Default.chEtiqL2;
            //MaquinaLinea.chEncL2 = Properties.Settings.Default.chEncL2;
            //MaquinaLinea.chConL2 = Properties.Settings.Default.chConL2;

            //MaquinaLinea.chDesL3 = Properties.Settings.Default.chDesL3;
            //MaquinaLinea.chLlenL3 = Properties.Settings.Default.chLlenL3;
            //MaquinaLinea.chEtiqL3 = Properties.Settings.Default.chEtiqL3;
            //MaquinaLinea.chEncL3 = Properties.Settings.Default.chEncL3;
            //MaquinaLinea.chConL3 = Properties.Settings.Default.chConL3;

            //MaquinaLinea.chDesL5 = Properties.Settings.Default.chDesL5;
            //MaquinaLinea.chLlenL5 = Properties.Settings.Default.chLlenL5;
            //MaquinaLinea.chEtiqL5 = Properties.Settings.Default.chEtiqL5;
            //MaquinaLinea.chEncL5 = Properties.Settings.Default.chEncL5;
            //MaquinaLinea.chConL5 = Properties.Settings.Default.chConL5;

            //MaquinaLinea.chalarma = Properties.Settings.Default.chalarma;
            //MaquinaLinea.alarmah1 = Properties.Settings.Default.alarmah1;
            //MaquinaLinea.alarmam1 = Properties.Settings.Default.alarmam1;
            //MaquinaLinea.alarmah2 = Properties.Settings.Default.alarmah2;
            //MaquinaLinea.alarmam2 = Properties.Settings.Default.alarmam2;
            //MaquinaLinea.alarmah3 = Properties.Settings.Default.alarmah3;
            //MaquinaLinea.alarmam3 = Properties.Settings.Default.alarmam3;
        }


        //#################       BOTONES BARRA MENU       #################
        /// <summary>
        /// Botones con los que vas abriendo los diferentes forms hijo del programa
        /// </summary>
        private void Menu_Click(object sender, EventArgs e)
        {
            //Abrimos el form segundario del menu
            AbrirFormHijo(new WHPST_MENU(this), "Menu");
        }
        private void Linea2B_Click(object sender, EventArgs e)
        {
            MaquinaLinea.numlin = 2;




            if (MaquinaLinea.checkL2 == true)
            {
                FormSelecMaq = new WHPST_SELECTMAQ(this);
                AbrirFormHijo(FormSelecMaq, "L" + MaquinaLinea.numlin);

            }
            else
            {
                FormCambioTurno = new WHPST_Cambio_Turno(this);
                AbrirFormHijo(FormCambioTurno, "L" + MaquinaLinea.numlin);
            }
        }
        private void Linea3B_Click(object sender, EventArgs e)
        {
            MaquinaLinea.numlin = 3;
            if (MaquinaLinea.checkL3 == true)
            {
                FormSelecMaq = new WHPST_SELECTMAQ(this);

                AbrirFormHijo(FormSelecMaq, "L" + MaquinaLinea.numlin);
            }
            else
            {
                FormCambioTurno = new WHPST_Cambio_Turno(this);
                AbrirFormHijo(FormCambioTurno, "L" + MaquinaLinea.numlin);
            }
        }
        private void Linea5B_Click(object sender, EventArgs e)
        {
            MaquinaLinea.numlin = 5;
            if (MaquinaLinea.checkL5 == true)
            {
                FormSelecMaq = new WHPST_SELECTMAQ(this);

                AbrirFormHijo(FormSelecMaq, "L" + MaquinaLinea.numlin);
            }
            else
            {
                FormCambioTurno = new WHPST_Cambio_Turno(this);
                AbrirFormHijo(FormCambioTurno, "L" + MaquinaLinea.numlin);
            }
        }

        public void LanzamientoB_Click(object sender, EventArgs e)
        {
            //if (FormLanz == null) FormLanz = new WHPST_LANZ(this);

            FormLanz = new WHPST_LANZ(this);
            AbrirFormHijo(FormLanz, "Lanzamiento");

        }
        private void ProduccionB_Click(object sender, EventArgs e)
        {
            if (FormProduccion == null) FormProduccion = new MainProduccion(this);
            Utilidades.AbrirForm(FormProduccion, this, typeof(MainProduccion));
        }
        private void CalidadB_Click(object sender, EventArgs e)
        {
            if (FormAdministracionCalidad == null) FormAdministracionCalidad = new MainAdministracion_Calidad(this);
            Utilidades.AbrirForm(FormAdministracionCalidad, this, typeof(MainAdministracion_Calidad));
        }
        private void BOMB_Click(object sender, EventArgs e)
        {
            if (FormBOM == null) FormBOM = new WHPST_BOM(this);

            MaquinaLinea.VolverA = RetornoBOM.Inicio;
            Utilidades.AbrirForm(FormBOM, this, typeof(WHPST_BOM));
        }
        private void ParteB_Click(object sender, EventArgs e)
        {
            if (FormParte == null) FormParte = new MainParte(this);
            Utilidades.AbrirForm(FormParte, this, typeof(MainParte));
        }
        private void AjustesB_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(FormAjustes, "Ajustes");
        }
        private void SesionB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.usuario == "")
            {
                Utilidades.AbrirForm(FormLogin, this, typeof(WHPST_LOGIN));
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
                    //                            |   L2    |     L3    |     L5    |  PRODUCCIÓN |   CALIDAD   |     LANZ    |      BOM     |    PARTE    |   AJUSTES   |
                    ArrayBotones = new bool[18] { true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false };
                    OcultarMostarBotones(ArrayBotones);
                    MinimizarB.Visible = false;

                }
            }
        }

  

        //#####################       FUNCIONES       #####################
        /// <summary>
        /// Función que abre un form hijo en un panel determinado.
        /// </summary>
        /// <param name="WHPST_FORM">Parámetro que indica que form que desea abrir.</param>
        public void AbrirFormHijo(Form WHPST_FORM, string Boton)
        {
            //Marca el boton seleccionado.
            ColorBoton(Boton, MaquinaLinea.usuario);

            //Abre el form hijo en el panel indicado.
            if (this.PanelInicio.Controls.Count > 0) this.PanelInicio.Controls.RemoveAt(0);
    
            //Form SM = WHPST_FORM as Form;

            WHPST_FORM.TopLevel = false;
            WHPST_FORM.Dock = DockStyle.Fill;
            this.PanelInicio.Controls.Add(WHPST_FORM);
            this.PanelInicio.Tag = WHPST_FORM;
            WHPST_FORM.Show();
        }

        /// <summary>
        /// Función que abre un form hijo dependiendo de la variabe MaquinaLinea.RetornoInicio
        /// </summary>
        private void MostrarForm()
        {
            //Si volvemos de un form padre, tenemos que mostrar el form hijo por el que nos hemos ido, por ello queda registrado en una variable
            switch (MaquinaLinea.RetornoInicio)
            {
                case "SelecMaquinaL2":
                    FormSelecMaq = new WHPST_SELECTMAQ(this);
                    AbrirFormHijo(FormSelecMaq, "L2");
                    break;
                case "SelecMaquinaL3":
                    FormSelecMaq = new WHPST_SELECTMAQ(this);
                    AbrirFormHijo(FormSelecMaq, "L3");
                    break;
                case "SelecMaquinaL5":
                    FormSelecMaq = new WHPST_SELECTMAQ(this);
                    AbrirFormHijo(FormSelecMaq, "L5");
                    break;
                case "CambioTurno":
                     FormCambioTurno = new WHPST_Cambio_Turno(this);
                    AbrirFormHijo(FormCambioTurno, "L" + MaquinaLinea.numlin);
                    break;
                case "Lanzamiento":
                    FormLanz = new WHPST_LANZ(this);
                    AbrirFormHijo(FormLanz, "Lanzamiento");
                    break;
                default:
                    FormMenu = new WHPST_MENU(this);
                    AbrirFormHijo(FormMenu, "Menu");
                    break;
            }
            MaquinaLinea.RetornoInicio = "";
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

        /// <summary>
        ///  Función que indica que botones y paneles deben ser visibles segun el usuario que ha accedido.
        /// </summary>
        /// <param name="Usuario"> Variable que indica que ususario esta registrado.</param>
        private void PermisosUsuario(string Usuario)
        {
            SesionB.Image = Properties.Resources.MenuCerrarSesion50x50;
            SesionB.Text = "    Cerrar Sesión";
            switch (Usuario)
            {
                case "Administracion":
                    SesionPanel.BackColor = Color.DarkTurquoise;
                    //                           |   L2    |     L3    |     L5    |PRODUCCIÓN |  CALIDAD  |    LANZ   |    BOM    |   PARTE   |  AJUSTES  |
                    ArrayBotones = new bool[18] {true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true};
                    break;
                case "Calidad":
                    SesionPanel.BackColor = Color.Lime;
                    //                            |    L2     |      L3     |      L5     | PRODUCCIÓN  |  CALIDAD  |    LANZ   |    BOM    |    PARTE    |   AJUSTES   |
                    ArrayBotones = new bool[18] { false, false, false, false, false, false, false, false, true, true, true, true, true, true, false, false, false, false};
                    break;
                case "Encargado":
                    SesionPanel.BackColor = Color.OrangeRed;
                    //                            |    L2     |      L3     |      L5     |PRODUCCIÓN |   CALIDAD   |    LANZ   |    BOM    |   PARTE   |   AJUSTES   |
                    ArrayBotones = new bool[18] { false, false, false, false, false, false, true, true, false, false, true, true, true, true, true, true, false, false};
                    break;
                case "Oficina":
                    SesionPanel.BackColor = Color.DarkViolet;
                    //                            |    L2     |      L3     |      L5     |  PRODUCCIÓN |  CALIDAD  |    LANZ   |    BOM    |    PARTE    |   AJUSTES   |
                    ArrayBotones = new bool[18] { false, false, false, false, false, false, false, false, true, true, true, true, true, true, false, false, false, false};
                    break;
                default:
                    SesionPanel.BackColor = Properties.Settings.Default.COLOR1;
                    //                            |   L2    |     L3    |     L5    |  PRODUCCIÓN |   CALIDAD   |     LANZ    |      BOM     |    PARTE    |   AJUSTES   |
                    ArrayBotones = new bool[18] { true, true, true, true, true, true, false, false, false, false, false, false, false,  false, false, false, false, false};
                    break;
            }

            OcultarMostarBotones(ArrayBotones);
        }
        /// <summary>
        /// Funcion que oculta o pone visible los botones y panele que se indica el array.
        /// </summary>
        /// <param name="ArrayBotones"> Array booleano de 18 valores.</param>
        private void OcultarMostarBotones(bool [] ArrayBotones)
        {
            Linea2B.Visible = ArrayBotones[0];
            Linea2Panel.Visible = ArrayBotones[1];
            Linea3B.Visible = ArrayBotones[2];
            Linea3Panel.Visible = ArrayBotones[3];
            Linea5B.Visible = ArrayBotones[4];
            Linea5Panel.Visible = ArrayBotones[5];
            BOMB.Visible = ArrayBotones[6];
            BOMPanel.Visible = ArrayBotones[7];
            LanzamientoB.Visible = ArrayBotones[8];
            LanzaminetoPanel.Visible = ArrayBotones[9];
            CalidadB.Visible = ArrayBotones[10];
            CalidadPanel.Visible = ArrayBotones[11];
            ProduccionB.Visible = ArrayBotones[12];
            ProduccionPanel.Visible = ArrayBotones[13];
            ParteB.Visible = ArrayBotones[14];
            Partepanel.Visible = ArrayBotones[15];
            AjustesB.Visible = ArrayBotones[16];
            AjustesPanel.Visible = ArrayBotones[17];
        }

        /// <summary>
        /// Función que actualiza la aplicación y así se ha indicado en la extraccion de datos de la pantalla de carga.
        /// </summary>
        private void ActualizarAPP()
        {
            //EN ESTE PUNTO SABEMOS SI HAY QUE ACTUALIZAR, REALIZAMOS LA CONSULTA POR SI QUIERE O NO ACTUALIZAR LA APLICACIÓN

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
                filterval[3] = "\"" + Properties.Settings.Default.IDPC + "\"";
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

        //#####################       ANIMACIÓN BARRA       #####################

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


        public Panel getpanelinicio()
        {
            return this.PanelInicio;
        }

        public void SetSelecMaq(WHPST_SELECTMAQ s)
        {
            FormSelecMaq = s;
        }

        public WHPST_SELECTMAQ GetSelecMaq()
        {
            return FormSelecMaq;
        }

        public void SetMenu(WHPST_MENU s)
        {
            FormMenu = s;
        }

        public WHPST_MENU GetMenu()
        {
            return FormMenu;
        }

        public void SetCambioTurno(WHPST_Cambio_Turno s)
        {
            FormCambioTurno = s;
        }

        public WHPST_Cambio_Turno GetCambioTurno()
        {
            return FormCambioTurno;
        }

        public void SetBOM(WHPST_BOM s)
        {
            FormBOM = s;
        }

        public WHPST_BOM GetBOM()
        {
            return FormBOM;
        }

        public void SetLanz(WHPST_LANZ l)
        {
            FormLanz = l;
        }

        public WHPST_LANZ GetLanz()
        {
            return FormLanz;
        }

        //#########################################################
    } 
}

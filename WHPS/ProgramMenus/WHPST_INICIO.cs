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

        Color ColorA = MaquinaLinea.COLOR1;
        Color ColorB = Color.Transparent;
        Color ColorC = Color.Gainsboro;

        bool[] ArrayColorBotones = new bool[20];

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
        }

        public WHPST_INICIO(Form F)
        {
            InitializeComponent();
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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CambioTurno.ResetCabioFueraHora();
            if (CambioTurno.ComprobarCambioTurno() && !CambioTurno.CambioFueraHora)
            {
                if (MaquinaLinea.numlin == 2 && MaquinaLinea.checkL2 == true)
                {
                    Properties.Settings.Default.checkL2 = false;
                    MaquinaLinea.checkL2 = Properties.Settings.Default.checkL2;
                }
                if (MaquinaLinea.numlin == 3 && MaquinaLinea.checkL3 == true)
                {
                    Properties.Settings.Default.checkL3 = false;
                    MaquinaLinea.checkL3 = Properties.Settings.Default.checkL3;
                }
                if (MaquinaLinea.numlin == 5 && MaquinaLinea.checkL5 == true)
                {
                    Properties.Settings.Default.checkL5 = false;
                    MaquinaLinea.checkL5 = Properties.Settings.Default.checkL5;
                }
                Properties.Settings.Default.Save();
                FormCambioTurno = new WHPST_Cambio_Turno(this);
                AbrirFormHijo(FormCambioTurno, "L" + MaquinaLinea.numlin);
            }
        }

        //#################       BOTONES BARRA MENU       #################
        /// <summary>
        /// Botones con los que vas abriendo los diferentes forms hijo del programa
        /// </summary>
        private void Menu_Click(object sender, EventArgs e)
        {
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

        private void LanzamientoB_Click(object sender, EventArgs e)
        {
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
            FormAjustes = new WHPST_AJUSTES(this);
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
            switch (MaquinaLinea.VolverInicioA)
            {
                case Model.RetornoInicio.L2:
                    Linea2B_Click(this, new EventArgs());
                    break;
                case Model.RetornoInicio.L3:
                    Linea3B_Click(this, new EventArgs());
                    break;
                case Model.RetornoInicio.L5:
                    Linea5B_Click(this, new EventArgs());
                    break;
                case Model.RetornoInicio.CambioTurno:
                    FormCambioTurno = new WHPST_Cambio_Turno(this);
                    AbrirFormHijo(FormCambioTurno, "L" + MaquinaLinea.numlin);
                    break;
                case Model.RetornoInicio.Lanzamiento:
                    LanzamientoB_Click(this, new EventArgs());
                    break;
                default:
                    Menu_Click(this, new EventArgs());
                    break;
            }
            MaquinaLinea.VolverInicioA = Model.RetornoInicio.Menu;
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
                    //                               |    MENU   |     L2      |      L3     |      L5     |     BOM     |     LANZ    |   CALIDAD   |    PARTE    | PRODUCCCION |   AJUSTES    |
                    ArrayColorBotones = new bool[20] { true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };break;
                case "L2":
                    //                               |     MENU    |    L2     |      L3     |      L5     |     BOM     |     LANZ    |   CALIDAD   |    PARTE    | PRODUCCCION |   AJUSTES    |
                    ArrayColorBotones = new bool[20] { false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };break;
                case "L3":
                    //                               |     MENU    |     L2      |     L3    |      L5     |     BOM     |     LANZ    |   CALIDAD   |    PARTE    | PRODUCCCION |   AJUSTES    |
                    ArrayColorBotones = new bool[20] { false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false };break;
                case "L5":
                    //                               |     MENU    |     L2      |      L3     |     L5    |     BOM     |     LANZ    |   CALIDAD   |    PARTE    | PRODUCCCION |   AJUSTES    |
                    ArrayColorBotones = new bool[20] { false, false, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false };break;
                case "BOM  ":
                    //                               |     MENU    |     L2      |      L3     |      L5     |    BOM    |     LANZ    |   CALIDAD   |    PARTE    | PRODUCCCION |   AJUSTES    |
                    ArrayColorBotones = new bool[20] { false, false, false, false, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false };break;
                case "Lanzamiento":
                    //                               |     MENU    |     L2      |      L3     |      L5     |     BOM     |    LANZ   |   CALIDAD   |    PARTE    | PRODUCCCION |   AJUSTES    |
                    ArrayColorBotones = new bool[20] { false, false, false, false, false, false, false, false, false, false, true, true, false, false, false, false, false, false, false, false };break;
                case "Calidad":
                    //                               |     MENU    |     L2      |      L3     |      L5     |     BOM     |     LANZ    |  CALIDAD  |    PARTE    | PRODUCCCION |   AJUSTES    |
                    ArrayColorBotones = new bool[20] { false, false, false, false, false, false, false, false, false, false, false, false, true, true, false, false, false, false, false, false }; break;
                case "Partes":
                    //                               |     MENU    |     L2      |      L3     |      L5     |     BOM     |     LANZ    |   CALIDAD   |   PARTE   | PRODUCCCION |   AJUSTES    |
                    ArrayColorBotones = new bool[20] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, true, false, false, false, false }; break;
                case "Produccion":
                    //                               |     MENU    |     L2      |      L3     |      L5     |     BOM     |     LANZ    |   CALIDAD   |    PARTE    |PRODUCCCION|   AJUSTES    |
                    ArrayColorBotones = new bool[20] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, true, false, false }; break;
                case "Ajustes":
                    //                               |     MENU    |     L2      |      L3     |      L5     |     BOM     |     LANZ    |   CALIDAD   |    PARTE    | PRODUCCCION |  AJUSTES   |
                    ArrayColorBotones = new bool[20] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, true }; break;
            }
            ColoresB(Usuario);
        }

        /// <summary>
        /// Función que modifica el color de los botones principales en  función del LA VARIABLE ArrayColorBoton[20].
        /// </summary>
        /// <param name="Usuario"></param>
        private void ColoresB(string Usuario)
        {
            Menu.BackColor = (ArrayColorBotones[0]) ? ColorA : ColorB;
            MenuPanel.BackColor = (ArrayColorBotones[1]) ? ColorC : ColorA;
            Linea2B.BackColor = (ArrayColorBotones[2]) ? ColorA : ColorB;
            Linea2Panel.BackColor = (ArrayColorBotones[3]) ?ColorC : ColorA;
            Linea3B.BackColor = (ArrayColorBotones[4]) ? ColorA : ColorB;
            Linea3Panel.BackColor = (ArrayColorBotones[5]) ?ColorC : ColorA;
            Linea5B.BackColor = (ArrayColorBotones[6]) ? ColorA : ColorB;
            Linea5Panel.BackColor = (ArrayColorBotones[7]) ?ColorC : ColorA;
            //BOMB.BackColor = (ArrayColorBotones[8]) ? ColorA : ColorB;
            //BOMPanel.BackColor = (ArrayColorBotones[9]) ?ColorC : ColorA;
            LanzamientoB.BackColor = (ArrayColorBotones[10]) ? ColorA : ColorB;
            LanzaminetoPanel.BackColor = (ArrayColorBotones[11]) ?ColorC : ColorA;
            //CalidadB.BackColor = (ArrayColorBotones[12]) ? ColorA : ColorB;
            //CalidadPanel.BackColor = (ArrayColorBotones[13]) ?ColorC : ColorA;
            //ParteB.BackColor = (ArrayColorBotones[14]) ? ColorA : ColorB;
            //Partepanel.BackColor = (ArrayColorBotones[15]) ?ColorC : ColorA;
            //ProduccionB.BackColor = (ArrayColorBotones[16]) ? ColorA : ColorB;
            //ProduccionPanel.BackColor = (ArrayColorBotones[17]) ?ColorC : ColorA;
            AjustesB.BackColor = (ArrayColorBotones[18]) ? ColorA : ColorB;
            AjustesPanel.BackColor = (ArrayColorBotones[19]) ?ColorC : ColorA;
            PanelMinimizar.BackColor = Color.FromArgb(240,240,240);
            SesionB.BackColor = ColorB;
            switch (Usuario)
            {
                case "":
                    SesionPanel.BackColor = ColorA; break;
                case "Administracion":
                    SesionPanel.BackColor = Color.DarkTurquoise; break;
                case "Oficina":
                    SesionPanel.BackColor = Color.DarkViolet; break;
                case "Encargado":
                    SesionPanel.BackColor = Color.OrangeRed; break;
                case "Calidad":
                    SesionPanel.BackColor = Color.Lime; break;
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

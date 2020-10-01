using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.ProgramMenus;
using WHPS.Utiles;
using WHPS;

namespace WHPS.Administracion
{

    public partial class MainAdministracion_Calidad : Form
    {
        //La variable puntero, selecciona que textbox se ha de rellenar
        public string puntero;
        //Con la variable busqueda determinamos si los parámetros introducidos son correctos
        public bool busqueda=false;
        //Con esta variable se establece que se ha finalizado la busqueda
        public static bool estadobusqueda = false;

        //public static bool MaquinaLinea.CARGANDO = false;
        public MainAdministracion_Calidad()
        {
            InitializeComponent();
        }

        //Para que al volver este seleccionado el menú por el que hemos entrado usamos la variable backcalidad
        private void BackB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.RetornoInicio = "Menu";
            WHPST_INICIO Form = new WHPST_INICIO();
            Hide();
            Form.Show();
        }
        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }


        private void MainAdministracion_Calidad_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;
            //Mostramos el Menú de inicio
            ColorBoton("Menu");
            AbrirForm(new Administracion_Menu());
            
            busqueda = false;
            //Ocultamos paneles y textbox inecesarios
            PanelRespuesta.Hide();
            AvisoLB.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Una vez finalizada la busqueda cargar directamente el menú del despaletizador
            if (estadobusqueda)
            {
                AbrirForm(new Administracion_Desp());
                estadobusqueda = false;
            }
        }

        //Rellenamos los parametros de busqueda
        private void BusDiaTB_MouseClick(object sender, MouseEventArgs e)
        {
            puntero = "DiaInicio";
            BusDiaTB.ForeColor = Color.Black;
            BusDiaTB.ReadOnly = false;
            PanelRespuesta.Hide();
            PanelCalendario.Show();

            BusDiaTB.Text="";

            
        }
        private void BusDiaFinTB_MouseClick(object sender, MouseEventArgs e)
        {
            puntero = "DiaFin";
            BusDiaFinTB.ForeColor = Color.Black;
            BusDiaFinTB.ReadOnly = false;
            PanelRespuesta.Hide();
            PanelCalendario.Show();
            BusDiaFinTB.Text = "";

        }
        private void BusTurnoTB_MouseClick(object sender, MouseEventArgs e)
        {
            BusTurnoTB.ReadOnly = false;
            puntero = "Turno";
            PanelRespuesta.Show();
            PanelCalendario.Hide();
            BusTurnoTB.Text = "";
            Opcion1B.Text = "Mañana";
            Opcion2B.Text = "Tarde";
            Opcion3B.Text = "Noche";
        }
        private void BusLineaTB_MouseClick(object sender, MouseEventArgs e)
        {
            BusLineaTB.ForeColor = Color.Black;
            BusLineaTB.ReadOnly = false;
            puntero = "Linea";
            PanelRespuesta.Show();
            PanelCalendario.Hide();
            BusLineaTB.Text = "";
            Opcion1B.Text = "L2";
            Opcion2B.Text = "L3";
            Opcion3B.Text = "L5";
        }
        private void BusLoteTB_MouseClick(object sender, MouseEventArgs e)
        {
            BusLoteTB.ForeColor = Color.Black;
            BusLoteTB.ReadOnly = false;
            PanelRespuesta.Hide();
            PanelCalendario.Hide();
            BusLoteTB.Text = "";
        }
        
        //Mostramos las diferentes opciones segun que marque la variable puntero
        private void Opcion1B_Click(object sender, EventArgs e)
        {
            if (puntero == "Turno")
            {
                BusTurnoTB.Text = "Mañana";
            }
            if (puntero == "Linea")
            {
                BusLineaTB.Text = "L2";
            }
        }
        private void Opcion2B_Click(object sender, EventArgs e)
        {
            if (puntero == "Turno")
            {
                BusTurnoTB.Text = "Tarde";
            }
            if (puntero == "Linea")
            {
                BusLineaTB.Text = "L3";
            }
        }
        private void Opcion3B_Click(object sender, EventArgs e)
        {
            if (puntero == "Turno")
            {
                BusTurnoTB.Text = "Noche";
            }
            if (puntero == "Linea")
            {
                BusLineaTB.Text = "L5";
            }
        }
        private void Busqueda(bool tipobusqueda)
        {
            MaquinaLinea.CARGANDO = true;
            ColorBoton("Desp");
            PanelCalendario.Hide();
            PanelRespuesta.Hide();
            busqueda = false;
            if ((BusDiaTB.Text == "" || BusDiaTB.ReadOnly == true) && (BusLoteTB.Text == "" || BusLoteTB.ReadOnly == true))
            {
                AvisoLB.Show();
                BusDiaTB.ReadOnly = false;
                BusLoteTB.ReadOnly = false;
                BusDiaTB.ForeColor = Color.IndianRed;
                BusLoteTB.ForeColor = Color.IndianRed;
                BusDiaTB.Text = "dd/MM/yyyy";
                BusDiaFinTB.Text = "dd/MM/yyyy";
                BusLoteTB.Text = "Lyyddd";
            }

            if (BusLineaTB.Text == "" || BusLineaTB.ReadOnly == true)
            {
                AvisoLB.Show();
                BusLineaTB.ReadOnly = false;
                BusLineaTB.ForeColor = Color.IndianRed;
                BusLineaTB.Text = "L2,L3,L5;";
            }
            //En el caso en el que se haya rellenado la linea y el lote o el dia se dará por valida la busqueda
            if ((BusLineaTB.Text != "L2,L3,L5;" && BusDiaTB.Text != "dd/MM/yyyy") || (BusLineaTB.Text != "L2,L3,L5;" && BusLoteTB.Text != "Lyyddd"))
            {
                Properties.Settings.Default.BusDia = BusDiaTB.Text;
                Properties.Settings.Default.BusDiaFin = BusDiaFinTB.Text;
                Properties.Settings.Default.BusLinea = BusLineaTB.Text;
                MaquinaLinea.numlin = Convert.ToInt16(Properties.Settings.Default.BusLinea.Substring(1, 1));
                Properties.Settings.Default.BusLote = BusLoteTB.Text;
                Properties.Settings.Default.BusTurno = BusTurnoTB.Text;
                Properties.Settings.Default.Save();
                busqueda = true;
            }

            if (busqueda)
            {

                //Visualización línea de carga

                AbrirForm(new Administracion_Carga());

                Apps_Administracion.AplicarFiltros(BusDiaTB.Text, BusDiaFinTB.Text, BusTurnoTB.Text, BusLoteTB.Text);

                /////Variable de ficheros /////
                MaquinaLinea.FileDespaletizador = "Desp_L" + MaquinaLinea.numlin.ToString();
                MaquinaLinea.FileLlenadora = "Llen_L" + MaquinaLinea.numlin.ToString();
                MaquinaLinea.FileEtiquetadora = "Etiq_L" + MaquinaLinea.numlin.ToString();
                MaquinaLinea.FileEncajonadora = "Enc_L" + MaquinaLinea.numlin.ToString();

                //Asignación de los filtros en modo básico
                Apps_Administracion.Filtros_Des(Datos_BusAdmin.FDesp, tipobusqueda);
                Apps_Administracion.Filtros_Llen(Datos_BusAdmin.FLlen, tipobusqueda);
                Apps_Administracion.Filtros_Etiq(Datos_BusAdmin.FEtiq, tipobusqueda);
                Apps_Administracion.Filtros_Enc(Datos_BusAdmin.FEnc, tipobusqueda);

                //Llamada a las funciones de busqueda
                Thread T1 = new Thread(() =>
                {
                    Apps_Administracion.CargaDespaletizador(Datos_BusAdmin.Despaletizador, Datos_BusAdmin.Despaletizador_est, Datos_BusAdmin.FDesp);
                });
                T1.Start();
                Thread T2 = new Thread(() =>
                {
                    Apps_Administracion.CargaLlenadora(Datos_BusAdmin.Llenadora, Datos_BusAdmin.Llenadora_est, Datos_BusAdmin.FLlen);
                });
                T2.Start();
                Thread T3 = new Thread(() =>
                {
                    Apps_Administracion.CargaEtiquetadora(Datos_BusAdmin.Etiquetadora, Datos_BusAdmin.Etiquetadora_est, Datos_BusAdmin.FEtiq);

                });
                T3.Start();
                Thread T4 = new Thread(() =>
                {
                    Apps_Administracion.CargaEncajonadora(Datos_BusAdmin.Encajonadora, Datos_BusAdmin.Encajonadora_est, Datos_BusAdmin.FEnc);
                });
                T4.Start();

            }


        }
        //Almacenamos las variables que usaremos para la busqueda y si se rellenan los parámetros correspondiente aceptará la busqueda
        private void BuscarB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                MaquinaLinea.CARGANDO = true;
                if (BuscarB.Text == "Buscar")
                {
                    MaquinaLinea.TipoBus = false;
                    Busqueda(false);
                }
                if (BuscarB.Text == "Buscar Avd.")
                {
                    MaquinaLinea.TipoBus = true;
                    Busqueda(true);
                }
            }
            
        }
        private void CambioBusquedaB_Click(object sender, EventArgs e)
        {
            switch (BuscarB.Text)
            {
                case "Buscar":
                    BuscarB.Text = "Buscar Avd.";
                    break;
                case "Buscar Avd.":
                    BuscarB.Text = "Buscar";
                    break;
            }
        }

        //Programación de los botones del panel vertical
        private void MenuAdminB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                ColorBoton("Menu");
                AbrirForm(new Administracion_Menu());
            }
        }
        private void DespB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                if (busqueda == true)
                {

                    //###### Precargamos la variable del fichero a grabar ##############
                    MaquinaLinea.FileDespaletizador = "Desp_L" + MaquinaLinea.numlin.ToString();
                    ColorBoton("Desp");
                    AbrirForm(new Administracion_Desp());
                }
            }
        }
        private void LlenB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                if (busqueda == true)
                {
                    //###### Precargamos la variable del fichero a grabar ##############
                    MaquinaLinea.FileLlenadora = "Llen_L" + MaquinaLinea.numlin.ToString();
                    ColorBoton("Llen");
                    AbrirForm(new Administracion_Llen());
                }
            }
        }
        private void EtiqB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                if (busqueda == true)
                {
                    //###### Precargamos la variable del fichero a grabar ##############
                    MaquinaLinea.FileEtiquetadora = "Etiq_L" + MaquinaLinea.numlin.ToString();
                    ColorBoton("Etiq");
                    AbrirForm(new Administracion_Etiq());
                }
            }
        }
        private void EncB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                if (busqueda == true)
                {
                    //###### Precargamos la variable del fichero a grabar ##############
                    MaquinaLinea.FileEncajonadora = "Enc_L" + MaquinaLinea.numlin.ToString();
                    ColorBoton("Enc");
                    AbrirForm(new Administracion_Enc());
                }
            }
        }

        
        //--------------FUNCIONES--------------
        //Funciones de form "hijo" con el que podemos mantener abierto un form segundario
        private void AbrirForm(object Form)
        {
            if (this.PanelBusqueda.Controls.Count > 0)
            {
                this.PanelBusqueda.Controls.RemoveAt(0);
            }
            Form SM = Form as Form;
            SM.TopLevel = false;
            SM.Dock = DockStyle.Fill;
            this.PanelBusqueda.Controls.Add(SM);
            this.PanelBusqueda.Tag = SM;
            SM.Show();
        }
 
        //Función que aplicará el cambio de color
        private void ColorBoton(string Boton)
        {
            switch (Boton)
            {
                case "Menu":
                    DespB.BackColor = Color.Transparent;
                    PanelDesp.BackColor = MaquinaLinea.COLOR1;
                    LlenB.BackColor = Color.Transparent;
                    PanelLlen.BackColor = MaquinaLinea.COLOR1;
                    EtiqB.BackColor = Color.Transparent;
                    PanelEtiq.BackColor = MaquinaLinea.COLOR1;
                    EncB.BackColor = Color.Transparent;
                    PanelEnc.BackColor = MaquinaLinea.COLOR1;
                    break;
                case "Desp":
                    DespB.BackColor = MaquinaLinea.COLOR1;
                    PanelDesp.BackColor = Color.Gainsboro;
                    LlenB.BackColor = Color.Transparent;
                    PanelLlen.BackColor = MaquinaLinea.COLOR1;
                    EtiqB.BackColor = Color.Transparent;
                    PanelEtiq.BackColor = MaquinaLinea.COLOR1;
                    EncB.BackColor = Color.Transparent;
                    PanelEnc.BackColor = MaquinaLinea.COLOR1;
                    break;
                case "Llen":
                    DespB.BackColor = Color.Transparent;
                    PanelDesp.BackColor = MaquinaLinea.COLOR1;
                    LlenB.BackColor = MaquinaLinea.COLOR1;
                    PanelLlen.BackColor = Color.Gainsboro;
                    EtiqB.BackColor = Color.Transparent;
                    PanelEtiq.BackColor = MaquinaLinea.COLOR1;
                    EncB.BackColor = Color.Transparent;
                    PanelEnc.BackColor = MaquinaLinea.COLOR1;
                    break;
                case "Etiq":
                    DespB.BackColor = Color.Transparent;
                    PanelDesp.BackColor  = MaquinaLinea.COLOR1;
                    LlenB.BackColor = Color.Transparent;
                    PanelLlen.BackColor  = MaquinaLinea.COLOR1;
                    EtiqB.BackColor = MaquinaLinea.COLOR1;
                    PanelEtiq.BackColor  = Color.Gainsboro;
                    EncB.BackColor = Color.Transparent;
                    PanelEnc.BackColor = MaquinaLinea.COLOR1;

                    break;
                case "Enc":
                    DespB.BackColor = Color.Transparent;
                    PanelDesp.BackColor  = MaquinaLinea.COLOR1;
                    LlenB.BackColor = Color.Transparent;
                    PanelLlen.BackColor  = MaquinaLinea.COLOR1;
                    EtiqB.BackColor = Color.Transparent;
                    PanelEtiq.BackColor  = MaquinaLinea.COLOR1;
                    EncB.BackColor = MaquinaLinea.COLOR1;
                    PanelEnc.BackColor = Color.Gainsboro;

                    break;
            }
        }



        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (puntero == "DiaInicio")
            {
                BusDiaTB.Text = monthCalendar1.SelectionStart.Date.ToString("dd/MM/yyyy");

            }
            if (puntero == "DiaFin")
            {
                BusDiaFinTB.Text = monthCalendar1.SelectionEnd.Date.ToString("dd/MM/yyyy");
            }
        }
    }
}

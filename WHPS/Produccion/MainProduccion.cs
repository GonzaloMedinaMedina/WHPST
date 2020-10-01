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

namespace WHPS.Produccion
{

    public partial class MainProduccion : Form
    {
        //La variable puntero, selecciona que textbox se ha de rellenar
        public string puntero;
        //Con la variable busqueda determinamos si los parámetros introducidos son correctos
        public bool busqueda=false;
        //Con esta variable se establece que se ha finalizado la busqueda
        public static bool estadobusqueda = false;
        public DateTime DIA;

        public MainProduccion()
        {
            InitializeComponent();
        }

        //Para que al volver este seleccionado el menú por el que hemos entrado usamos la variable backcalidad
        private void BackB_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BackCalidad = true;
            Properties.Settings.Default.Save();
            WHPST_INICIO Form = new WHPST_INICIO();
            Hide();
            Form.Show();
        }
        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }


        private void MainProduccion_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            if (BusDiaTB.Text == "") BusDiaTB.ReadOnly = false; BusDiaTB.Text =Convert.ToString(DateTime.Now.AddDays(-5)).Substring(0,10);
            if (BusDiaFinTB.Text == "") BusDiaFinTB.ReadOnly = false; BusDiaFinTB.Text = DateTime.Now.Date.ToString().Substring(0, 10);

            MaquinaLinea.TipoBus = false;
            Busqueda(false);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Una vez finalizada la busqueda cargar directamente el menú del despaletizador
            if (estadobusqueda)
            {
                AbrirForm(new Produccion_L2());
                estadobusqueda = false;
            }
        }

        //Rellenamos los parametros de busqueda
        private void BusDiaTB_MouseClick(object sender, MouseEventArgs e)
        {
            puntero = "DiaInicio";
            BusDiaTB.ForeColor = Color.Black;
            BusDiaTB.ReadOnly = false;
            PanelCalendario.Visible = true;

            BusDiaTB.Text="";

            
        }
        private void BusDiaFinTB_MouseClick(object sender, MouseEventArgs e)
        {
            puntero = "DiaFin";
            BusDiaFinTB.ForeColor = Color.Black;
            BusDiaFinTB.ReadOnly = false;
            PanelCalendario.Visible = true;
            BusDiaFinTB.Text = "";

        }
    
        private void Busqueda(bool tipobusqueda)
        {
            MaquinaLinea.CARGANDO = true;
            ColorBoton("L2");
            PanelCalendario.Visible = false;
            busqueda = false;
            if (BusDiaTB.Text == "" || BusDiaTB.ReadOnly == true)
            {
                AvisoLB.Show();
                BusDiaTB.ReadOnly = false;
                BusDiaTB.ForeColor = Color.IndianRed;
                BusDiaTB.Text = "dd/MM/yyyy";
                BusDiaFinTB.Text = "dd/MM/yyyy";
            }


            //En el caso en el que se haya rellenado la linea y el lote o el dia se dará por valida la busqueda
            if ((BusDiaTB.Text != "dd/MM/yyyy") )
            {
                Properties.Settings.Default.BusDia = BusDiaTB.Text;
                Properties.Settings.Default.BusDiaFin = BusDiaFinTB.Text;
                Properties.Settings.Default.Save();
                busqueda = true;
            }

            if (busqueda)
            {

                //Visualización línea de carga

                AbrirForm(new Produccion_Carga());

                Apps_Produccion.AplicarFiltros(BusDiaTB.Text, BusDiaFinTB.Text);

                /////Variable de ficheros /////
                MaquinaLinea.FileDespaletizador = "Enc_L" + "5".ToString();
                MaquinaLinea.FileLlenadora = "Llen_L" + MaquinaLinea.numlin.ToString();
                MaquinaLinea.FileEtiquetadora = "Enc_L" + "3".ToString();
                MaquinaLinea.FileEncajonadora = "Enc_L" +"2".ToString();

                //Asignación de los filtros en modo básico
                Apps_Produccion.Filtros_EncL5(Datos_Produccion.FEncL5);
                Apps_Produccion.Filtros_Llen(Datos_Produccion.FLlen);
                Apps_Produccion.Filtros_EncL3(Datos_Produccion.FEncL3);
                Apps_Produccion.Filtros_EncL2(Datos_Produccion.FEncL2);

                //Llamada a las funciones de busqueda
                Thread T1 = new Thread(() =>
                {
                    Apps_Produccion.CargaDespaletizador(Datos_Produccion.EncajonadoraL5, Datos_Produccion.EncajonadoraL5_est, Datos_Produccion.FEncL5);
                });
                T1.Start();
                Thread T2 = new Thread(() =>
                {
                    Apps_Produccion.CargaLlenadora(Datos_Produccion.Llenadora, Datos_Produccion.Llenadora_est, Datos_Produccion.FLlen);
                });
                T2.Start();
                Thread T3 = new Thread(() =>
                {
                    Apps_Produccion.CargaEtiquetadora(Datos_Produccion.EncajonadoraL3, Datos_Produccion.EncajonadoraL3_est, Datos_Produccion.FEncL3);

                });
                T3.Start();
                Thread T4 = new Thread(() =>
                {
                    Apps_Produccion.CargaEncajonadora(Datos_Produccion.EncajonadoraL2, Datos_Produccion.EncajonadoraL2_est, Datos_Produccion.FEncL2);
                });
                T4.Start();
            }


        }
        //Almacenamos las variables que usaremos para la busqueda y si se rellenan los parámetros correspondiente aceptará la busqueda
        private void BuscarB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                MaquinaLinea.TipoBus = false;
                Busqueda(false);
            }
        }

        //Programación de los botones del panel vertical
        private void MenuAdminB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                ColorBoton("Menu");
                AbrirForm(new Produccion_Menu());
            }
        }
        private void L2B_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                if (busqueda == true)
                {
                    ColorBoton("L2");
                    AbrirForm(new Produccion_L2());
                }
            }
        }
        private void L3B_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                if (busqueda == true)
                {
                    ColorBoton("L3");
                    AbrirForm(new Produccion_L3());
                }
            }
        }
        private void L5B_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                if (busqueda == true)
                {
                    ColorBoton("L5");
                    AbrirForm(new Produccion_L5());
                }
            }
        }
        private void TotalB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                if (busqueda == true)
                {
                    ColorBoton("Tot");
                    AbrirForm(new Produccion_T());
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
                    L2B.BackColor = Color.Transparent;
                    PanelL2.BackColor = MaquinaLinea.COLOR1;
                    L3B.BackColor = Color.Transparent;
                    PanelL3.BackColor = MaquinaLinea.COLOR1;
                    L5B.BackColor = Color.Transparent;
                    PanelL5.BackColor = MaquinaLinea.COLOR1;
                    TotalB.BackColor = Color.Transparent;
                    PanelTot.BackColor = MaquinaLinea.COLOR1;
                    break;
                case "L2":
                    L2B.BackColor = MaquinaLinea.COLOR1;
                    PanelL2.BackColor = Color.Gainsboro;
                    L3B.BackColor = Color.Transparent;
                    PanelL3.BackColor = MaquinaLinea.COLOR1;
                    L5B.BackColor = Color.Transparent;
                    PanelL5.BackColor = MaquinaLinea.COLOR1;
                    TotalB.BackColor = Color.Transparent;
                    PanelTot.BackColor = MaquinaLinea.COLOR1;
                    break;
                case "L3":
                    L2B.BackColor = Color.Transparent;
                    PanelL2.BackColor = MaquinaLinea.COLOR1;
                    L3B.BackColor = MaquinaLinea.COLOR1;
                    PanelL3.BackColor = Color.Gainsboro;
                    L5B.BackColor = Color.Transparent;
                    PanelL5.BackColor = MaquinaLinea.COLOR1;
                    TotalB.BackColor = Color.Transparent;
                    PanelTot.BackColor = MaquinaLinea.COLOR1;
                    break;
                case "L5":
                    L2B.BackColor = Color.Transparent;
                    PanelL2.BackColor = MaquinaLinea.COLOR1;
                    L3B.BackColor = Color.Transparent;
                    PanelL3.BackColor = MaquinaLinea.COLOR1;
                    L5B.BackColor = MaquinaLinea.COLOR1;
                    PanelL5.BackColor = Color.Gainsboro;
                    TotalB.BackColor = Color.Transparent;
                    PanelTot.BackColor = MaquinaLinea.COLOR1;
                    break;
                case "Tot":
                    L2B.BackColor = Color.Transparent;
                    PanelL2.BackColor = MaquinaLinea.COLOR1;
                    L3B.BackColor = Color.Transparent;
                    PanelL3.BackColor = MaquinaLinea.COLOR1;
                    L5B.BackColor = Color.Transparent;
                    PanelL5.BackColor = MaquinaLinea.COLOR1;
                    TotalB.BackColor = MaquinaLinea.COLOR1;
                    PanelTot.BackColor = Color.Gainsboro;
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

        private void BusDiaTB_TextChanged(object sender, EventArgs e)
        {
            if (BusDiaTB.Text != "")
            DIA = DateTime.Parse(BusDiaTB.Text).AddDays(+4);
            BusDiaFinTB.Text = Convert.ToString(DIA).Substring(0, 10);
        }
        private void BusDiaFinTB_TextChanged(object sender, EventArgs e)
        {
            if (BusDiaFinTB.Text != "")
            DIA = DateTime.Parse(BusDiaFinTB.Text).AddDays(-4);
            BusDiaTB.Text = Convert.ToString(DIA).Substring(0, 10);
        }


    }
}

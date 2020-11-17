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
using WHPS.Administracion;
using System.IO;
using System.Diagnostics;
using WHPS.Produccion;

namespace WHPS.Rotura
{

    public partial class MainRotura : Form
    {
        //La variable puntero, selecciona que textbox se ha de rellenar
        public string puntero;
        //Con esta variable se establece que se ha finalizado la busqueda
        public static bool estadobusqueda = false;

        //Con la variable busqueda determinamos si los parámetros introducidos son correctos
        public bool busqueda = false;

        public DateTime DIA;
        public MainRotura()
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


        private void MainRotura_Load(object sender, EventArgs e)
        {
            Owner.Hide();
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            if (BusDiaTB.Text == "") BusDiaTB.ReadOnly = false; BusDiaTB.Text = "01/01/"+ DateTime.Now.Year.ToString();
            if (BusDiaFinTB.Text == "") BusDiaFinTB.ReadOnly = false; BusDiaFinTB.Text = DateTime.Now.Date.ToString().Substring(0, 10);
            MaquinaLinea.TipoBus = false;
            Busqueda();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Una vez finalizada la busqueda cargar directamente el menú del despaletizador
            if (estadobusqueda)
            {
                AbrirForm(new Rotura_Desp());
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

            BusDiaTB.Text = "";
        }
        private void BusDiaFinTB_MouseClick(object sender, MouseEventArgs e)
        {
            puntero = "DiaFin";
            BusDiaFinTB.ForeColor = Color.Black;
            BusDiaFinTB.ReadOnly = false;
            PanelCalendario.Visible = true;
            BusDiaFinTB.Text = "";
        }
        private void Busqueda()
        {
            MaquinaLinea.CARGANDO = true;
            ColorBoton("Desp");
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
            if ((BusDiaTB.Text != "dd/MM/yyyy"))
            {
                Properties.Settings.Default.BusDia = DateTime.Now.ToString("dd/MM/yyyy");
                Properties.Settings.Default.BusTurno = MaquinaLinea.turno;
                Properties.Settings.Default.Save();
            }

                //Visualización línea de carga
                AbrirForm(new Rotura_Carga());

            Apps_Rotura.AplicarFiltros(BusDiaTB.Text, BusDiaFinTB.Text);

            /////Variable de ficheros /////
            MaquinaLinea.FileDespaletizador = "Desp_L" + MaquinaLinea.numlin.ToString();
                MaquinaLinea.FileLlenadora = "Llen_L" + MaquinaLinea.numlin.ToString();
                MaquinaLinea.FileEtiquetadora = "Etiq_L" + MaquinaLinea.numlin.ToString();
                MaquinaLinea.FileEncajonadora = "Enc_L" + MaquinaLinea.numlin.ToString();

                //Asignación de los filtros en modo básico
                Apps_Rotura.Filtros_Des(Datos_BusAdmin.FDesp);
                Apps_Rotura.Filtros_Llen(Datos_BusAdmin.FLlen);
                Apps_Rotura.Filtros_Etiq(Datos_BusAdmin.FEtiq);
                Apps_Rotura.Filtros_Enc(Datos_BusAdmin.FEnc);

                //Llamada a las funciones de busqueda
                Thread T1 = new Thread(() =>
                { Apps_Rotura.CargaDespaletizador(Datos_BusAdmin.Despaletizador, Datos_BusAdmin.Despaletizador_est, Datos_BusAdmin.FDesp); });
                T1.Start();
                Thread T2 = new Thread(() =>
                { Apps_Rotura.CargaLlenadora(Datos_BusAdmin.Llenadora, Datos_BusAdmin.Llenadora_est, Datos_BusAdmin.FLlen); });
                T2.Start();
                Thread T3 = new Thread(() =>
                { Apps_Rotura.CargaEtiquetadora(Datos_BusAdmin.Etiquetadora, Datos_BusAdmin.Etiquetadora_est, Datos_BusAdmin.FEtiq); });
                T3.Start();
                Thread T4 = new Thread(() =>
                { Apps_Rotura.CargaEncajonadora(Datos_BusAdmin.Encajonadora, Datos_BusAdmin.Encajonadora_est, Datos_BusAdmin.FEnc); });
                T4.Start();
            }
        

        //Almacenamos las variables que usaremos para la busqueda y si se rellenan los parámetros correspondiente aceptará la busqueda
        private void BuscarB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                MaquinaLinea.TipoBus = false;
                Busqueda();
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

        private void DespB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                ColorBoton("Desp");
                AbrirForm(new Rotura_Desp());
            }
        }
        private void LlenB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                ColorBoton("Llen");
                AbrirForm(new Rotura_Llen());
            }
        }
        private void EtiqB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                ColorBoton("Etiq");
                AbrirForm(new Rotura_Etiq());
            }
        }
        private void EncB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                ColorBoton("Enc");
                AbrirForm(new Rotura_Enc());
            }
        }

        private void GuardarB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                //HACEMOS UNA COPIA DEL FICHERO
                try
                {
                    File.Delete(ConexionDatos.ObtenerFicheroClave(MaquinaLinea.CarpetaRotura) + "PlantillaRotura" + ".xlsx");

                    File.Delete(ConexionDatos.ObtenerFicheroClave(MaquinaLinea.CarpetaRotura) + MaquinaLinea.NombreRotura + Convert.ToString(MaquinaLinea.numlin) + ".xlsx");


                    File.Copy(ConexionDatos.ObtenerFicheroClave(MaquinaLinea.PlantillaRotura), ConexionDatos.ObtenerFicheroClave(MaquinaLinea.CarpetaRotura) + "PlantillaRotura" + ".xlsx");


                    MaquinaLinea.Rotura_Guardar = true;
                    if (MaquinaLinea.Rotura_Desp == false)
                    {
                        AbrirForm(new Rotura_Desp());
                    }
                    while (MaquinaLinea.Rotura_Desp == false) { }
                    DespB.BackColor = Color.DarkSeaGreen;
                    if (MaquinaLinea.Rotura_Desp == true && MaquinaLinea.Rotura_Llen == false)
                    {
                        AbrirForm(new Rotura_Llen());
                    }
                    while (MaquinaLinea.Rotura_Llen == false) { }
                    LlenB.BackColor = Color.DarkSeaGreen;
                    if (MaquinaLinea.Rotura_Llen == true && MaquinaLinea.Rotura_Etiq == false)
                    {
                        AbrirForm(new Rotura_Etiq());
                    }
                    while (MaquinaLinea.Rotura_Etiq == false) { }
                    EtiqB.BackColor = Color.DarkSeaGreen;
                    if (MaquinaLinea.Rotura_Etiq == true && MaquinaLinea.Rotura_Enc == false)
                    {
                        AbrirForm(new Rotura_Enc());
                    }
                    EncB.BackColor = Color.DarkSeaGreen;
                    MaquinaLinea.Rotura_Guardar = false;
                    MaquinaLinea.Rotura_Desp = false;
                    MaquinaLinea.Rotura_Llen = false;
                    MaquinaLinea.Rotura_Etiq = false;
                    MaquinaLinea.Rotura_Enc = false;

                    File.Copy(ConexionDatos.ObtenerFicheroClave(MaquinaLinea.FileRotura), ConexionDatos.ObtenerFicheroClave(MaquinaLinea.CarpetaRotura) + MaquinaLinea.NombreRotura + Convert.ToString(MaquinaLinea.numlin) + ".xlsx");
                    File.Delete(ConexionDatos.ObtenerFicheroClave(MaquinaLinea.CarpetaRotura) + "PlantillaRotura" + ".xlsx");
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    MessageBox.Show("Esta en uso o eliminado el parte que se quiere sobreescribir o el archivo PlantillaRotura.xlsx");
                }
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

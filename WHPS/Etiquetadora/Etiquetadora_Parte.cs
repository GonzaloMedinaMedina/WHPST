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
using WHPS.Etiquetadora;
using WHPS.Model;
using WHPS.Parte;
using WHPS.ProgramMenus;
using WHPS.Utiles;

namespace WHPS.Etiquetadora
{
    public partial class Etiquetadora_Parte : Form
    {
        MainEtiquetadora parent;
        public Etiquetadora_Parte(MainEtiquetadora p)
        {
            InitializeComponent();
            parent = p;
        }
        //Con esta variable se establece que se ha finalizado la busqueda
        public static bool estadobusqueda = false;

        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>
        private void SiguienteB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainEtiquetadora));
                this.Hide();
                this.Dispose();
            }
        }
        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

        private void Etiquetadora_Parte_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;
            Busqueda();

            //Rellenadmos los DatosProduccion = { Orden, CodProducto, Referencia,  Capacidad, Producto , Cliente, Graduacion, NBot}; 
            OrdenTB.Text = MainEtiquetadora.DatosProduccion[0];
            CodProductoTB.Text = MainEtiquetadora.DatosProduccion[1];
            ProductoTB.Text = MainEtiquetadora.DatosProduccion[4];
            ClienteTB.Text = MainEtiquetadora.DatosProduccion[5];
            GraduacionTB.Text = MainEtiquetadora.DatosProduccion[6];
            NBotTB.Text = MainEtiquetadora.DatosProduccion[7];
            FormatoTB.Text = MainEtiquetadora.DatosProduccion[8];


            //Estado del boton de paro y de producción
            if (MaquinaLinea.numlin == 2)
            {
                //Producción
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEtiqL2;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEtiqL2;
                LoteTB.Text = Properties.Settings.Default.DPLoteEtiqL2;
            }
            if (MaquinaLinea.numlin == 3)
            {
                //Producción
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEtiqL3;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEtiqL3;
          LoteTB.Text = Properties.Settings.Default.DPLoteEtiqL3;
               
            }
            if (MaquinaLinea.numlin == 5)
            {
                //Producción
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEtiqL5;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEtiqL5;
             LoteTB.Text = Properties.Settings.Default.DPLoteEtiqL5;
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Una vez finalizada la busqueda cargar directamente el menú del despaletizador
            if (estadobusqueda)
            {
                AbrirForm(new Etiquetadora_RegistrosTurno());
                estadobusqueda = false;
            }
            string Hora = lbReloj.Text;
            if (Hora.Substring(3, 2) != "00" && Hora.Substring(3, 2) != "30") { MaquinaLinea.AnuladorAlarma = true; }
            if (Hora.Substring(3, 2) == "00" || Hora.Substring(3, 2) == "30") { if (MaquinaLinea.AnuladorAlarma == true) { /*Apps_Etiquetadora.AlarmaControl30min(); */} }
        }


        private void Busqueda()
        {
            //En el caso en el que se haya reEtiqado la linea y el lote o el dia se dará por valida la busqueda
            Properties.Settings.Default.BusDia = DateTime.Now.ToString("dd/MM/yyyy");
            Properties.Settings.Default.BusTurno = MaquinaLinea.turno;
            Properties.Settings.Default.Save();


            //Visualización línea de carga
            AbrirForm(new Etiquetadora_Carga());

            Apps_Etiquetadora.AplicarFiltros(Properties.Settings.Default.BusDia, Properties.Settings.Default.BusTurno);

            /////Variable de ficheros /////
            MaquinaLinea.FileDespaletizador = "Desp_L" + MaquinaLinea.numlin.ToString();
            MaquinaLinea.FileEtiquetadora = "Etiq_L" + MaquinaLinea.numlin.ToString();
            MaquinaLinea.FileLlenadora = "Llen_L" + MaquinaLinea.numlin.ToString();
            MaquinaLinea.FileEncajonadora = "Enc_L" + MaquinaLinea.numlin.ToString();

            //Asignación de los filtros en modo básico
            Apps_Etiquetadora.Filtros_Des(Datos_BusAdmin.FDesp);
            Apps_Etiquetadora.Filtros_Etiq(Datos_BusAdmin.FEtiq);
            Apps_Etiquetadora.Filtros_Llen(Datos_BusAdmin.FLlen);
            Apps_Etiquetadora.Filtros_Enc(Datos_BusAdmin.FEnc);

            //Llamada a las funciones de busqueda
            Thread T1 = new Thread(() =>
            { Apps_Etiquetadora.CargaDespaletizador(Datos_BusAdmin.Despaletizador, Datos_BusAdmin.Despaletizador_est, Datos_BusAdmin.FDesp); });
            T1.Start();
            Thread T2 = new Thread(() =>
            { Apps_Etiquetadora.CargaEtiquetadora(Datos_BusAdmin.Etiquetadora, Datos_BusAdmin.Etiquetadora_est, Datos_BusAdmin.FEtiq); });
            T2.Start();
            Thread T3 = new Thread(() =>
            { Apps_Etiquetadora.CargaEtiquetadora(Datos_BusAdmin.Etiquetadora, Datos_BusAdmin.Etiquetadora_est, Datos_BusAdmin.FEtiq); });
            T3.Start();
            Thread T4 = new Thread(() =>
            { Apps_Etiquetadora.CargaEtiquetadora(Datos_BusAdmin.Etiquetadora, Datos_BusAdmin.Etiquetadora_est, Datos_BusAdmin.FEtiq); });
            T4.Start();
        }


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
        private void PanelBusqueda_MouseClick(object sender, MouseEventArgs e)
        {
            AbrirForm(new Etiquetadora_RegistrosTurno());
        }

        //Incrementa o decrementan el contador
        private void SumaBotellasB_Click(object sender, EventArgs e)
        {
            ConteoBotellasTB.Text = Convert.ToString(Convert.ToDouble(ConteoBotellasTB.Text) + 1);
            OK_ConteoB.BackColor = Color.Maroon;
        }
        private void RestaBotellasB_Click(object sender, EventArgs e)
        {
            ConteoBotellasTB.Text = Convert.ToString(Convert.ToDouble(ConteoBotellasTB.Text) - 1);
            OK_ConteoB.BackColor = Color.Maroon;
        }
        private void OK_ConteoB_Click(object sender, EventArgs e)
        {
            if (NBotTB.Text != "")
            {
                if (MaquinaLinea.numlin == 2)
                {

                    Properties.Settings.Default.BotellasAProducirEtiqL2 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.BotellasAProducirEtiqL2) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NBotTB.Text = Properties.Settings.Default.BotellasAProducirEtiqL2;
                }
                if (MaquinaLinea.numlin == 3)
                {

                    Properties.Settings.Default.BotellasAProducirEtiqL3 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.BotellasAProducirEtiqL3) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NBotTB.Text = Properties.Settings.Default.BotellasAProducirEtiqL3;
                }
                if (MaquinaLinea.numlin == 5)
                {

                    Properties.Settings.Default.BotellasAProducirEtiqL5 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.BotellasAProducirEtiqL5) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NBotTB.Text = Properties.Settings.Default.BotellasAProducirEtiqL5;
                }
                Properties.Settings.Default.Save();
                ConteoBotellasTB.Text = "0";
                OK_ConteoB.BackColor = Color.FromArgb(27, 33, 41);
            }
        }

    }
}

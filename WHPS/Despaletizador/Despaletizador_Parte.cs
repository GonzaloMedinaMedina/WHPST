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
using WHPS.Despaletizador;
using WHPS.Encajonadora;
using WHPS.Model;
using WHPS.Parte;
using WHPS.ProgramMenus;
using WHPS.Utiles;

namespace WHPS.Despaletizador
{
    public partial class Despaletizador_Parte : Form
    {
        MainDespaletizador parent;
        public Despaletizador_Parte(MainDespaletizador p)
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
                Hide();
                parent.Show();
            }
        }
        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

        private void Despaletizador_Parte_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;
            Busqueda();

            

            //Estado del boton de paro y de producción
            if (MaquinaLinea.numlin == 2)
            {
                //Producción
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL2;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEncL2;
                //Producto
                NCajasTB.Text = Properties.Settings.Default.CajasAProducirEncL2;
                CodProductoTB.Text = Properties.Settings.Default.DPCodigoProdEncL2;
                OrdenTB.Text = Properties.Settings.Default.DPOrdenEncL2;
                ProductoTB.Text = Properties.Settings.Default.DPProductoEncL2;
                ClienteTB.Text = Properties.Settings.Default.DPClienteEncL2 ;
                LoteTB.Text = Properties.Settings.Default.DPLoteEncL2;
                FormatoTB.Text = Properties.Settings.Default.DPFormatoEncL2;
                
            }
            if (MaquinaLinea.numlin == 3)
            {
                //Producción
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL3;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEncL3;
                
                //Producto
                NCajasTB.Text = Properties.Settings.Default.CajasAProducirEncL3;
                CodProductoTB.Text = Properties.Settings.Default.DPCodigoProdEncL3;
                OrdenTB.Text = Properties.Settings.Default.DPOrdenEncL3;
                ProductoTB.Text = Properties.Settings.Default.DPProductoEncL3;
                ClienteTB.Text = Properties.Settings.Default.DPClienteEncL3;
                LoteTB.Text = Properties.Settings.Default.DPLoteEncL3;
                FormatoTB.Text = Properties.Settings.Default.DPFormatoEncL3;
            }
            if (MaquinaLinea.numlin == 5)
            {
                //Producción
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL5;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEncL5;
                
                //Producto
                NCajasTB.Text = Properties.Settings.Default.CajasAProducirEncL5;
                CodProductoTB.Text = Properties.Settings.Default.DPCodigoProdEncL5;
                OrdenTB.Text = Properties.Settings.Default.DPOrdenEncL5;
                ProductoTB.Text = Properties.Settings.Default.DPProductoEncL5;
                ClienteTB.Text = Properties.Settings.Default.DPClienteEncL5;
                LoteTB.Text = Properties.Settings.Default.DPLoteEncL5;
                FormatoTB.Text = Properties.Settings.Default.DPFormatoEncL5;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Una vez finalizada la busqueda cargar directamente el menú del despaletizador
            if (estadobusqueda)
            {
                AbrirForm(new Despaletizador_RegistrosTurno());
                estadobusqueda = false;
            }
            string Hora = lbReloj.Text;
        }


        private void Busqueda()
        {
            //En el caso en el que se haya reEncado la linea y el lote o el dia se dará por valida la busqueda
            Properties.Settings.Default.BusDia = DateTime.Now.ToString("dd/MM/yyyy");
            Properties.Settings.Default.BusTurno = MaquinaLinea.turno;
            Properties.Settings.Default.Save();


            //Visualización línea de carga
            AbrirForm(new Despaletizador_Carga());

            Apps_Despaletizador.AplicarFiltros(Properties.Settings.Default.BusDia, Properties.Settings.Default.BusTurno);

            /////Variable de ficheros /////
            MaquinaLinea.FileDespaletizador = "Desp_L" + MaquinaLinea.numlin.ToString();
            MaquinaLinea.FileLlenadora = "Llen_L" + MaquinaLinea.numlin.ToString();
            MaquinaLinea.FileEtiquetadora = "Etiq_L" + MaquinaLinea.numlin.ToString();
            MaquinaLinea.FileEncajonadora = "Enc_L" + MaquinaLinea.numlin.ToString();

            //Asignación de los filtros en modo básico
            Apps_Despaletizador.Filtros_Des(Datos_BusAdmin.FDesp);
            Apps_Despaletizador.Filtros_Llen(Datos_BusAdmin.FLlen);
            Apps_Despaletizador.Filtros_Etiq(Datos_BusAdmin.FEtiq);
            Apps_Despaletizador.Filtros_Enc(Datos_BusAdmin.FEnc);

            //Llamada a las funciones de busqueda
            Thread T1 = new Thread(() =>
            { Apps_Despaletizador.CargaDespaletizador(Datos_BusAdmin.Despaletizador, Datos_BusAdmin.Despaletizador_est, Datos_BusAdmin.FDesp); });
            T1.Start();
            Thread T2 = new Thread(() =>
            { /*Apps_Despaletizador.CargaLlenadora(Datos_BusAdmin.Llenadora, Datos_BusAdmin.Llenadora_est, Datos_BusAdmin.FLlen);*/ });
            T2.Start();
            Thread T3 = new Thread(() =>
            {/* Apps_Despaletizador.CargaEtiquetadora(Datos_BusAdmin.Etiquetadora, Datos_BusAdmin.Etiquetadora_est, Datos_BusAdmin.FEtiq); */});
            T3.Start();
            Thread T4 = new Thread(() =>
            {/* Apps_Despaletizador.CargaEncajonadora(Datos_BusAdmin.Encajonadora, Datos_BusAdmin.Encajonadora_est, Datos_BusAdmin.FEnc);*/ });
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
            AbrirForm(new Despaletizador_RegistrosTurno());
        }

      

        private void SiguienteB_Click_1(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainDespaletizador));
                this.Hide();
                this.Dispose();
            }
        }

        private void SumaBotellasB_Click_1(object sender, EventArgs e)
        {
            ConteoBotellasTB.Text = Convert.ToString(Convert.ToDouble(ConteoBotellasTB.Text) + 1);
            OK_ConteoB.BackColor = Color.Maroon;
        }
        private void RestaBotellasB_Click_1(object sender, EventArgs e)
        {
            ConteoBotellasTB.Text = Convert.ToString(Convert.ToDouble(ConteoBotellasTB.Text) - 1);
            OK_ConteoB.BackColor = Color.Maroon;
        }
        private void OK_ConteoB_Click_1(object sender, EventArgs e)
        {
            if (NCajasTB.Text != "")
            {
                if (MaquinaLinea.numlin == 2)
                {

                    Properties.Settings.Default.CajasAProducirEncL2 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.CajasAProducirEncL2) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NCajasTB.Text = Properties.Settings.Default.CajasAProducirEncL2;
                }
                if (MaquinaLinea.numlin == 3)
                {

                    Properties.Settings.Default.CajasAProducirEncL3 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.CajasAProducirEncL3) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NCajasTB.Text = Properties.Settings.Default.CajasAProducirEncL3;
                }
                if (MaquinaLinea.numlin == 5)
                {

                    Properties.Settings.Default.CajasAProducirEncL5 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.CajasAProducirEncL5) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NCajasTB.Text = Properties.Settings.Default.CajasAProducirEncL5;
                }
                Properties.Settings.Default.Save();
                ConteoBotellasTB.Text = "0";
                OK_ConteoB.BackColor = Color.FromArgb(27, 33, 41);
            }
        }
    }
}

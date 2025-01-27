﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Encajonadora;
using WHPS.Model;
using WHPS.Parte;
using WHPS.ProgramMenus;
using WHPS.Utiles;

namespace WHPS.Encajonadora
{
    public partial class Encajonadora_Parte : Form
    {
        MainEncajonadora parent;
        public Encajonadora_Parte(MainEncajonadora p)
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

        private void Encajonadora_Parte_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;
            Busqueda();


            //Rellenadmos los DatosProduccion = { Orden, CodProducto, Referencia,  Capacidad, Producto , Cliente, Graduacion, NBot}; 
            OrdenTB.Text = MainEncajonadora.DatosProduccion[0];
            CodProductoTB.Text = MainEncajonadora.DatosProduccion[1];
            ProductoTB.Text = MainEncajonadora.DatosProduccion[4];
            ClienteTB.Text = MainEncajonadora.DatosProduccion[5];
            FormatoTB.Text = MainEncajonadora.DatosProduccion[8];
            NCajasTB.Text = MainEncajonadora.DatosProduccion[9];

            //Estado del boton de paro y de producción
            if (MaquinaLinea.numlin == 2)
            {
                //Producción
                LoteTB.Text = Properties.Settings.Default.DPLoteEncL2;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL2;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEncL2;
            }
            if (MaquinaLinea.numlin == 3)
            {
                //Producción
                LoteTB.Text = Properties.Settings.Default.DPLoteEncL3;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL3;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEncL3;
            }
            if (MaquinaLinea.numlin == 5)
            {
                //Producción
                LoteTB.Text = Properties.Settings.Default.DPLoteEncL5;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL5;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioEncL5;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Una vez finalizada la busqueda cargar directamente el menú del despaletizador
            if (estadobusqueda)
            {
                AbrirForm(new Encajonadora_RegistrosTurno());
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
            AbrirForm(new Encajonadora_Carga());

            Apps_Encajonadora.AplicarFiltros(Properties.Settings.Default.BusDia, Properties.Settings.Default.BusTurno);

            /////Variable de ficheros /////
            MaquinaLinea.FileDespaletizador = "Desp_L" + MaquinaLinea.numlin.ToString();
            MaquinaLinea.FileLlenadora = "Llen_L" + MaquinaLinea.numlin.ToString();
            MaquinaLinea.FileEtiquetadora = "Etiq_L" + MaquinaLinea.numlin.ToString();
            MaquinaLinea.FileEncajonadora = "Enc_L" + MaquinaLinea.numlin.ToString();

            //Asignación de los filtros en modo básico
            Apps_Encajonadora.Filtros_Des(Datos_BusAdmin.FDesp);
            Apps_Encajonadora.Filtros_Llen(Datos_BusAdmin.FLlen);
            Apps_Encajonadora.Filtros_Etiq(Datos_BusAdmin.FEtiq);
            Apps_Encajonadora.Filtros_Enc(Datos_BusAdmin.FEnc);

            //Llamada a las funciones de busqueda
            Thread T1 = new Thread(() =>
            { Apps_Encajonadora.CargaDespaletizador(Datos_BusAdmin.Despaletizador, Datos_BusAdmin.Despaletizador_est, Datos_BusAdmin.FDesp); });
            T1.Start();
            Thread T2 = new Thread(() =>
            { Apps_Encajonadora.CargaLlenadora(Datos_BusAdmin.Llenadora, Datos_BusAdmin.Llenadora_est, Datos_BusAdmin.FLlen); });
            T2.Start();
            Thread T3 = new Thread(() =>
            { Apps_Encajonadora.CargaEtiquetadora(Datos_BusAdmin.Etiquetadora, Datos_BusAdmin.Etiquetadora_est, Datos_BusAdmin.FEtiq); });
            T3.Start();
            Thread T4 = new Thread(() =>
            { Apps_Encajonadora.CargaEncajonadora(Datos_BusAdmin.Encajonadora, Datos_BusAdmin.Encajonadora_est, Datos_BusAdmin.FEnc); });
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
            AbrirForm(new Encajonadora_RegistrosTurno());
        }

      

        private void SiguienteB_Click_1(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainEncajonadora));
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

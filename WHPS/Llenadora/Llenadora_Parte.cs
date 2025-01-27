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
using WHPS.Model;
using WHPS.Parte;
using WHPS.ProgramMenus;
using WHPS.Utiles;

namespace WHPS.Llenadora
{
    public partial class Llenadora_Parte : Form
    {
        static MainLlenadora parent;
        public Llenadora_Parte(MainLlenadora p)
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
                Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainLlenadora));
                this.Hide();
                this.Dispose();
            }
        }
        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

        private void Llenadora_Parte_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;
            Busqueda();


            //Rellenadmos los DatosProduccion = { Orden, CodProducto, Referencia,  Capacidad, Producto , Cliente, Graduacion, NBot}; 
            OrdenTB.Text = MainLlenadora.DatosProduccion[0];
            CodProductoTB.Text = MainLlenadora.DatosProduccion[1];
            CapacidadTB.Text = MainLlenadora.DatosProduccion[3];
            ProductoTB.Text = MainLlenadora.DatosProduccion[4];
            ClienteTB.Text = MainLlenadora.DatosProduccion[5];
            GraduacionTB.Text = MainLlenadora.DatosProduccion[6];
            NBotTB.Text = MainLlenadora.DatosProduccion[7];


            //Estado del boton de paro y de producción
            if (MaquinaLinea.numlin == 2)
            {
                //Producción
                HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL2;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioLlenL2;
                if (Properties.Settings.Default.DPDeposito2LlenL2 == "") DepositoTB.Text = Properties.Settings.Default.DPDepositoLlenL2;
                else DepositoTB.Text = Properties.Settings.Default.DPDeposito2LlenL2;
            }
            if (MaquinaLinea.numlin == 3)
            {
                //Producción
                HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL3;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioLlenL3;
                if (Properties.Settings.Default.DPDeposito2LlenL3 == "") DepositoTB.Text = Properties.Settings.Default.DPDepositoLlenL3;
                else DepositoTB.Text = Properties.Settings.Default.DPDeposito2LlenL3;
               
            }
            if (MaquinaLinea.numlin == 5)
            {
                //Producción
                HInicioTB.Text = Properties.Settings.Default.DPHInicioLlenL5;
                HInicioCambioTB.Text = Properties.Settings.Default.DPHInicioCambioLlenL5;
                if (Properties.Settings.Default.DPDeposito2LlenL5 == "") DepositoTB.Text = Properties.Settings.Default.DPDepositoLlenL5;
                else DepositoTB.Text = Properties.Settings.Default.DPDeposito2LlenL5;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Una vez finalizada la busqueda cargar directamente el menú del despaletizador
            if (estadobusqueda)
            {
                AbrirForm(new Llenadora_RegistrosTurno());
                estadobusqueda = false;
            }
            string Hora = lbReloj.Text;
            if (Hora.Substring(3, 2) != "00" && Hora.Substring(3, 2) != "30") { MaquinaLinea.AnuladorAlarma = true; }
            if (Hora.Substring(3, 2) == "00" || Hora.Substring(3, 2) == "30") { if (MaquinaLinea.AnuladorAlarma == true) { Apps_Llenadora.AlarmaControl30min(); } }
        }


        private void Busqueda()
        {
            //En el caso en el que se haya rellenado la linea y el lote o el dia se dará por valida la busqueda
            Properties.Settings.Default.BusDia = DateTime.Now.ToString("dd/MM/yyyy");
            Properties.Settings.Default.BusTurno = MaquinaLinea.turno;
            Properties.Settings.Default.Save();


            //Visualización línea de carga
            AbrirForm(new Llenadora_Carga());

            Apps_Llenadora.AplicarFiltros(Properties.Settings.Default.BusDia, Properties.Settings.Default.BusTurno);

            /////Variable de ficheros /////
            MaquinaLinea.FileDespaletizador = "Desp_L" + MaquinaLinea.numlin.ToString();
            MaquinaLinea.FileLlenadora = "Llen_L" + MaquinaLinea.numlin.ToString();
            MaquinaLinea.FileEtiquetadora = "Etiq_L" + MaquinaLinea.numlin.ToString();
            MaquinaLinea.FileEncajonadora = "Enc_L" + MaquinaLinea.numlin.ToString();

            //Asignación de los filtros en modo básico
            Apps_Llenadora.Filtros_Des(Datos_BusAdmin.FDesp);
            Apps_Llenadora.Filtros_Llen(Datos_BusAdmin.FLlen);
            Apps_Llenadora.Filtros_Etiq(Datos_BusAdmin.FEtiq);
            Apps_Llenadora.Filtros_Enc(Datos_BusAdmin.FEnc);

            //Llamada a las funciones de busqueda
            Thread T1 = new Thread(() =>
            { Apps_Llenadora.CargaDespaletizador(Datos_BusAdmin.Despaletizador, Datos_BusAdmin.Despaletizador_est, Datos_BusAdmin.FDesp); });
            T1.Start();
            Thread T2 = new Thread(() =>
            { Apps_Llenadora.CargaLlenadora(Datos_BusAdmin.Llenadora, Datos_BusAdmin.Llenadora_est, Datos_BusAdmin.FLlen); });
            T2.Start();
            Thread T3 = new Thread(() =>
            { Apps_Llenadora.CargaEtiquetadora(Datos_BusAdmin.Etiquetadora, Datos_BusAdmin.Etiquetadora_est, Datos_BusAdmin.FEtiq); });
            T3.Start();
            Thread T4 = new Thread(() =>
            { Apps_Llenadora.CargaEncajonadora(Datos_BusAdmin.Encajonadora, Datos_BusAdmin.Encajonadora_est, Datos_BusAdmin.FEnc); });
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
            AbrirForm(new Llenadora_RegistrosTurno());
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

                    Properties.Settings.Default.BotellasAProducirLlenL2 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.BotellasAProducirLlenL2) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NBotTB.Text = Properties.Settings.Default.BotellasAProducirLlenL2;
                }
                if (MaquinaLinea.numlin == 3)
                {

                    Properties.Settings.Default.BotellasAProducirLlenL3 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.BotellasAProducirLlenL3) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NBotTB.Text = Properties.Settings.Default.BotellasAProducirLlenL3;
                }
                if (MaquinaLinea.numlin == 5)
                {

                    Properties.Settings.Default.BotellasAProducirLlenL5 = Convert.ToString(Convert.ToDouble(Properties.Settings.Default.BotellasAProducirLlenL5) + Convert.ToDouble(ConteoBotellasTB.Text));
                    NBotTB.Text = Properties.Settings.Default.BotellasAProducirLlenL5;
                }
                Properties.Settings.Default.Save();
                ConteoBotellasTB.Text = "0";
                OK_ConteoB.BackColor = Color.FromArgb(27, 33, 41);
            }
        }

    }
}

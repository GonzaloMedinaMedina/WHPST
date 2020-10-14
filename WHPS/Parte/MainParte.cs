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

namespace WHPS.Parte
{

    public partial class MainParte : Form
    {
        
        //La variable TRUNO, selecciona que TURNO se va a mostrar
        public string turno;
        public string LineaMarcada = "0";


        public bool BusqLinea = false;
        public bool BusqTurno = false;
        //Con esta variable se establece que se ha finalizado la busqueda
        public static bool estadobusqueda = false;

        public MainParte()
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


        private void MainParte_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            if (BusqLinea == true && BusqTurno == true)
            {

                MaquinaLinea.CARGANDO = true;
                Busqueda();
                
                BusqTurno = false;
                turno = "";

                BusqLinea = false;
                MaquinaLinea.numlin = 0;
                PanelBuscador.Visible = true;

            }

            //Una vez finalizada la busqueda cargar directamente el menú del despaletizador
            if (estadobusqueda)
            {
                AbrirForm(new Parte_Desp());
                estadobusqueda = false;
            }

        }

        private void Busqueda()
        {
            ColorBoton("Desp");
            //En el caso en el que se haya rellenado la linea y el lote o el dia se dará por valida la busqueda
            
            Properties.Settings.Default.BusTurno = turno;
            Properties.Settings.Default.Save();


            //Visualización línea de carga
            AbrirForm(new Parte_Carga());
            if (BusDiaTB.Text == "")
            {
                Properties.Settings.Default.BusDia = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                Properties.Settings.Default.BusDia = BusDiaTB.Text;
            }
            Apps_Parte.AplicarFiltros(Properties.Settings.Default.BusDia, Properties.Settings.Default.BusTurno);

            /////Variable de ficheros /////
            MaquinaLinea.FileDespaletizador = "Desp_L" + MaquinaLinea.numlin.ToString();
            MaquinaLinea.FileLlenadora = "Llen_L" + MaquinaLinea.numlin.ToString();
            MaquinaLinea.FileEtiquetadora = "Etiq_L" + MaquinaLinea.numlin.ToString();
            MaquinaLinea.FileEncajonadora = "Enc_L" + MaquinaLinea.numlin.ToString();

            //Asignación de los filtros en modo básico
            Apps_Parte.Filtros_Des(Datos_BusAdmin.FDesp);
            Apps_Parte.Filtros_Llen(Datos_BusAdmin.FLlen);
            Apps_Parte.Filtros_Etiq(Datos_BusAdmin.FEtiq);
            Apps_Parte.Filtros_Enc(Datos_BusAdmin.FEnc);

            //Llamada a las funciones de busqueda
            Thread T1 = new Thread(() =>
            {Apps_Parte.CargaDespaletizador(Datos_BusAdmin.Despaletizador, Datos_BusAdmin.Despaletizador_est, Datos_BusAdmin.FDesp); });
            T1.Start();
            Thread T2 = new Thread(() =>
            {Apps_Parte.CargaLlenadora(Datos_BusAdmin.Llenadora, Datos_BusAdmin.Llenadora_est, Datos_BusAdmin.FLlen); });
            T2.Start();
            Thread T3 = new Thread(() =>
            {Apps_Parte.CargaEtiquetadora(Datos_BusAdmin.Etiquetadora, Datos_BusAdmin.Etiquetadora_est, Datos_BusAdmin.FEtiq); });
            T3.Start();
            Thread T4 = new Thread(() =>
            {Apps_Parte.CargaEncajonadora(Datos_BusAdmin.Encajonadora, Datos_BusAdmin.Encajonadora_est, Datos_BusAdmin.FEnc); });
            T4.Start();
        }
        
        //--------------FUNCIONES--------------
        //Funciones de form "hijo" con el que podemos mantener abierto un form segundario
        private void AbrirForm(object Form)
        {
            if (this.PanelBusqueda.Controls.Count > 0)
            {
                //this.PanelBusqueda.Controls.RemoveAt(0);
                this.PanelBusqueda.Controls.Clear();
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
                AbrirForm(new Parte_Desp());
            }
        }
        private void LlenB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                ColorBoton("Llen");
                AbrirForm(new Parte_Llen());
            }
        }
        private void EtiqB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                ColorBoton("Etiq");
                AbrirForm(new Parte_Etiq());
            }
        }
        private void EncB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false)
            {
                ColorBoton("Enc");
                AbrirForm(new Parte_Enc());
            }
        }
        private void GuardarB_Click(object sender, EventArgs e)
        {
            AbrirForm(new Parte_Carga());
            MaquinaLinea.CARGANDO = true;
            //HACEMOS UNA COPIA DEL FICHERO
            try
            {
                File.Delete(ConexionDatos.ObtenerFicheroClave(MaquinaLinea.CarpetaParte) + "PlantillaParte" + ".xlsx");
                if (BusDiaTB.Text == "")
                {
                    MaquinaLinea.NombreParte= "Parte_" + DateTime.Now.ToString("yyyy.MM.dd") + "_L" + LineaMarcada + "_" + Properties.Settings.Default.BusTurno;
                }
                else
                {
                    MaquinaLinea.NombreParte = "Parte_" + BusDiaTB.Text[6]+ BusDiaTB.Text[7]+BusDiaTB.Text[8]+ BusDiaTB.Text[9]
                        +"."+ BusDiaTB.Text[3]+ BusDiaTB.Text[4]+"." +BusDiaTB.Text[0]+ BusDiaTB.Text[1]+ "_L" + LineaMarcada + "_" + Properties.Settings.Default.BusTurno;

                }
                File.Delete(ConexionDatos.ObtenerFicheroClave(MaquinaLinea.CarpetaParte) + MaquinaLinea.NombreParte + ".xlsx");


                File.Copy(ConexionDatos.ObtenerFicheroClave(MaquinaLinea.PlantillaParte), ConexionDatos.ObtenerFicheroClave(MaquinaLinea.CarpetaParte) + "PlantillaParte" + ".xlsx");
              
                

                MaquinaLinea.Parte_Guardar = true;
                if (MaquinaLinea.Parte_Desp == false)
                {Thread T1 = new Thread(() =>{ 
                    new Parte_Desp(true, this.LineaMarcada); }); T1.Start();}
                while (MaquinaLinea.Parte_Desp == false) { }
                DespB.BackColor = Color.DarkSeaGreen;
                if (MaquinaLinea.Parte_Desp == true && MaquinaLinea.Parte_Llen == false)
                {Thread T2 = new Thread(() => { 
                    new Parte_Llen(true, this.LineaMarcada); }); T2.Start();}
                while (MaquinaLinea.Parte_Llen == false) { }
                LlenB.BackColor = Color.DarkSeaGreen;
                if (MaquinaLinea.Parte_Llen == true && MaquinaLinea.Parte_Etiq == false)
                {Thread T3 = new Thread(() => { 
                   new Parte_Etiq(true, this.LineaMarcada); }); T3.Start();}
                while (MaquinaLinea.Parte_Etiq == false) { }
                EtiqB.BackColor = Color.DarkSeaGreen;
                if (MaquinaLinea.Parte_Etiq == true && MaquinaLinea.Parte_Enc == false)
                {Thread T4 = new Thread(() => { 
                   new Parte_Enc(true, this.LineaMarcada); 
                }); T4.Start();}
                while (MaquinaLinea.Parte_Enc == false) { }

                EncB.BackColor = Color.DarkSeaGreen;
                
                MaquinaLinea.Parte_Desp = false;
                MaquinaLinea.Parte_Llen = false;
                MaquinaLinea.Parte_Etiq = false;
                MaquinaLinea.Parte_Enc = false;
                

                File.Copy(ConexionDatos.ObtenerFicheroClave(MaquinaLinea.FileParte), ConexionDatos.ObtenerFicheroClave(MaquinaLinea.CarpetaParte) + MaquinaLinea.NombreParte + ".xlsx");
                File.Delete(ConexionDatos.ObtenerFicheroClave(MaquinaLinea.CarpetaParte) + "PlantillaParte" + ".xlsx");

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                MessageBox.Show(ex.Message+" Esta en uso o eliminado el parte que se quiere sobreescribir o el archivo PlantillaParte.xlsx");
            }
            MaquinaLinea.Parte_Guardar = false;
            MaquinaLinea.CARGANDO = false;
        }

        //Botones de busqueda
        private void L2B_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false) ColorBotonLinea("L2");
        }
        private void L3B_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false) ColorBotonLinea("L3");
        }
        private void L5B_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false) ColorBotonLinea("L5");
        }
        private void NocheB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false) ColorBotonTurno("Noche");
        }
        private void MañanaB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false) ColorBotonTurno("Mañana");
        }
        private void TardeB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.CARGANDO == false) ColorBotonTurno("Tarde");
        }
        //Funciones de busqueda
        private void ColorBotonTurno(string Boton)
        {
            BusqTurno = true;
            switch (Boton)
            {
                case "Noche":
                    turno = "Noche";
                    NocheB.BackColor = Color.DarkSeaGreen;
                    MañanaB.BackColor = Color.FromArgb(27, 33, 41);
                    TardeB.BackColor = Color.FromArgb(27, 33, 41);
                    break;
                case "Mañana":
                    turno = "Mañana";
                    NocheB.BackColor = Color.FromArgb(27, 33, 41);
                    MañanaB.BackColor = Color.DarkSeaGreen;
                    TardeB.BackColor = Color.FromArgb(27, 33, 41);
                    break;
                case "Tarde":
                    turno = "Tarde";
                    NocheB.BackColor = Color.FromArgb(27, 33, 41);
                    MañanaB.BackColor = Color.FromArgb(27, 33, 41);
                    TardeB.BackColor = Color.DarkSeaGreen;
                    break;
            }
        }
        private void ColorBotonLinea(string Boton)
        {
            BusqLinea = true;
            switch (Boton)
            {
                case "L2":
                    MaquinaLinea.numlin = 2;
                    LineaMarcada = "2";
                    L2B.BackColor = Color.DarkSeaGreen;
                    L3B.BackColor = Color.FromArgb(27, 33, 41);
                    L5B.BackColor = Color.FromArgb(27, 33, 41);
                    break;
                case "L3":
                    MaquinaLinea.numlin = 3;
                    LineaMarcada = "3";
                    L2B.BackColor = Color.FromArgb(27, 33, 41);
                    L3B.BackColor = Color.DarkSeaGreen;
                    L5B.BackColor = Color.FromArgb(27, 33, 41);
                    break;
                case "L5":
                    MaquinaLinea.numlin = 5;
                    LineaMarcada = "5";
                    L2B.BackColor = Color.FromArgb(27, 33, 41);
                    L3B.BackColor = Color.FromArgb(27, 33, 41);
                    L5B.BackColor = Color.DarkSeaGreen;
                    break;
            }
        }

        

        private void BusDiaTB_MouseClick(object sender, MouseEventArgs e)
        {
            BusDiaTB.ForeColor = Color.Black;
            BusDiaTB.ReadOnly = false;
            PanelCalendario.Visible = true;
            BusDiaTB.Text = "";

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            BusDiaTB.Text = monthCalendar1.SelectionStart.Date.ToString("dd/MM/yyyy");

        }

        private void BuscarB_Click(object sender, EventArgs e)
        {
            PanelCalendario.Visible = false;
            timer1.Stop();
            if(L2B.BackColor == Color.DarkSeaGreen){ColorBotonLinea("L2");}
            else if(L3B.BackColor == Color.DarkSeaGreen){ColorBotonLinea("L3");}
            else if (L5B.BackColor == Color.DarkSeaGreen){ColorBotonLinea("L5");}
            if (NocheB.BackColor == Color.DarkSeaGreen){ColorBotonTurno("Noche");}
            else if(TardeB.BackColor == Color.DarkSeaGreen){ColorBotonTurno("Tarde");}
            else if(MañanaB.BackColor == Color.DarkSeaGreen){ColorBotonTurno("Mañana");}
            timer1.Start();




        }
    }
}

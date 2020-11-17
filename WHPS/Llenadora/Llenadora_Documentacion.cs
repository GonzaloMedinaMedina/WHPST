using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.ProgramMenus;

namespace WHPS.Llenadora
{
    public partial class Llenadora_Documentacion : Form
    {
        public static MainLlenadora parent;
        public static WHPST_FORMATOS FormFormato;
        public Llenadora_Documentacion(MainLlenadora p)
        {
            InitializeComponent();
            parent = p;
        }

        private void ExitB_Click(object sender, EventArgs e)
        {
            Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainLlenadora));
            this.Hide();
            this.Dispose();
        }
        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

        private void Llenadora_Documentacion_Load(object sender, EventArgs e)
        {
            //Puesto que el timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            if (MaquinaLinea.numlin == 5 || MaquinaLinea.numlin == 2)
            {
                BordelesaSeduccionB.Visible = false;
                BordolesaV075JerezanaN075B.Visible = false;
                Jerezana070JerezanaNegra070B.Visible = false;
                Canasta0375B.Visible = false;
                CanastaN070TNico070B.Visible = false;
                Canasta100B.Visible = false;
                AcaciaAvant100B.Visible = false;
                Acacia070B.Visible = false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Cada segundo carga la hora en pantalla
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Para activar la alarma debe, estar desactivada, haberse chequeado el inicio de turno y que la hora cuadre con la alarma 
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }
            string Hora = lbReloj.Text;
            if (Hora.Substring(3, 2) != "00" && Hora.Substring(3, 2) != "30") { MaquinaLinea.AnuladorAlarma = true; }
            if (Hora.Substring(3, 2) == "00" || Hora.Substring(3, 2) == "30") { if (MaquinaLinea.AnuladorAlarma == true) { Apps_Llenadora.AlarmaControl30min(); } }
        }

        //DOCUMENTOS DEL CAMBIO DE FORMATO
        private void GeneralB_Click(object sender, EventArgs e)
        {
            Utilidades.AbrirForm(FormFormato, this, typeof(WHPST_FORMATOS));

        }
        private void BordelesaSeduccionB_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "GA37604840" + ".PDF");
        }
        private void BordolesaV075JerezanaN075B_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "GA37604846" + ".PDF");
        }
        private void Jerezana070JerezanaNegra070B_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "GA37604847" + ".PDF");
        }
        private void Canasta0375B_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "GA37604848" + ".PDF");
        }
        private void CanastaN070TNico070B_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "GA37604850" + ".PDF");
        }
        private void Canasta100B_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "GA37604851" + ".PDF");
        }
        private void AcaciaAvant100B_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "GA37604854" + ".PDF");
        }
        private void Acacia070_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "GA37604983" + ".PDF");
        }

        //DOCUMENTOS DE LOS CICLOS DE LAVADO
        private void CPSinLavadoB_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "CP SIN LAVADO" + ".PDF");
        }
        private void CPAguaFriaB_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "CP ENJ AGUA FRIA" + ".PDF");
        }
        private void CPFCAManualB_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "CP ENJ AGUA (FCA) - MANUAL" + ".PDF");
        }
        private void CPFCAAutomaticoB_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "CP ENJ AGUA (FCA) - AUTOMATICO" + ".PDF");
        }
        private void CDManualB_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "CD - DESINFECCION AGUA - MANUAL" + ".PDF");
        }
        private void CDAutomaticoB_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "CD - DESINFECCION AGUA - AUTOMATICO" + ".PDF");
        }
        private void CicloSosaB_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "CICLO CON SOSA" + ".PDF");
        }
        private void CicloPeraceticoB_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "CICLO CON PERACETICO" + ".PDF");
        }
        private void CicloFinSemanaB_Click(object sender, EventArgs e)
        {
            Process.Start(MaquinaLinea.RutaFolderDocLlenadora + "CICLO FIN DE SEMANA" + ".PDF");
        }
        public MainLlenadora GetParentInicio()
        {
            return parent;
        }
    }
}

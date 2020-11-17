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
using System.Diagnostics;

namespace WHPS.Produccion
{

    public partial class WHPST_AJUSTES : Form
    {
        WHPST_INICIO parent;

        public WHPST_AJUSTES(WHPST_INICIO p)
        {

            InitializeComponent();
            parent = p;
        }

        private void WHPST_AJUSTES_Load(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //CONTROL GENERAL
            NombreFicheroTB.Text = Properties.Settings.Default.NombreFichero;
            RutaFicheroTB.Text = Properties.Settings.Default.RutaFichero;
            IDPCTB.Text = Properties.Settings.Default.IDPC;
            TurnoPrevioTB.Text = Properties.Settings.Default.MovimientosTurnoAnterior.ToString();
            LanzamientoTB.Text = Properties.Settings.Default.MovimientosLanzamientoAnterior.ToString();

            PrincipalTB.BackColor = MaquinaLinea.COLOR1;
            //Cargamos las alarmas que ya habiamos definido
            MañanaHTB.Text = Properties.Settings.Default.alarmah1;
            MañanaMTB.Text = Properties.Settings.Default.alarmam1;
            TardeHTB.Text = Properties.Settings.Default.alarmah2;
            TardeMTB.Text = Properties.Settings.Default.alarmam2;
            NocheHTB.Text = Properties.Settings.Default.alarmah3;
            NocheMTB.Text = Properties.Settings.Default.alarmam3;

            PresionLlenTB.Text = Properties.Settings.Default.PresionAjustesLlen;
            NCañosL2TB.Text = Properties.Settings.Default.NCañosL2;
            NCañosL3TB.Text = Properties.Settings.Default.NCañosL3;
            NCañosL5TB.Text = Properties.Settings.Default.NCañosL5;

            CV_Tol1_Mayor1LTB.Text = Convert.ToString(Properties.Settings.Default.CV_Tol1_Mayor1LTB);
            CV_Tol2_Mayor1LTB.Text = Convert.ToString(Properties.Settings.Default.CV_Tol2_Mayor1LTB);
            CV_Tol1_Menor1LTB.Text = Convert.ToString(Properties.Settings.Default.CV_Tol1_Menor1LTB);
            CV_Tol2_Menor1LTB.Text = Convert.ToString(Properties.Settings.Default.CV_Tol2_Menor1LTB);



            //CONTROL DE AVISOS
            MantenimientoTB.Text = Properties.Settings.Default.AvisoMantenimiento;
            CamposTB.Text = Properties.Settings.Default.AvisoCampos;
            ProblemaFicheroTB.Text = Properties.Settings.Default.AvisoProblemaFichero;
            TempLlenTB.Text = Properties.Settings.Default.AvisoVolumenLlen;
            RegistroAlarmaTB.Text = Properties.Settings.Default.AvisoAlarma;
        }

        private void SelecRutaB_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                RutaFicheroTB.Text = folderDlg.SelectedPath;
                //Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }










































        private void AbrirTestExcelB_Click(object sender, EventArgs e)
        {
            TestExcel tExcel = new TestExcel();
            tExcel.ShowDialog();
        }

        private void CambioTurnoB_Click(object sender, EventArgs e)
        {
            //WHPST_Cambio_Turno form = new WHPST_Cambio_Turno();
            //form.ShowDialog();
        }

        private void ConsultarMovimientosB_Click(object sender, EventArgs e)
        {
            string result = "";
            ExcelUtiles.ObtenerUltimosMovimientosTurnos("Desp_L3", out result);
        }
        private void TabletFTeclRB_CheckedChanged(object sender, EventArgs e) {MaquinaLinea.TecladoWindows = 0; }
        private void SiFTeclRB_CheckedChanged(object sender, EventArgs e)     {MaquinaLinea.TecladoWindows = 1;}
        private void NoFTeclRB_CheckedChanged(object sender, EventArgs e)     {MaquinaLinea.TecladoWindows = 2;}

        private void ResetB_Click(object sender, EventArgs e)
        {
            DialogResult opcion;
            opcion = MessageBox.Show("Al resetar las variables se cerrará el programa. ¿Desea continuar?", "", MessageBoxButtons.OKCancel);
            if (opcion == DialogResult.OK)
            {
                Properties.Settings.Default.Reset();
                Environment.Exit(0);
            }
        }

        private void GuardarB_Click(object sender, EventArgs e)
        {
            string rutaFormat = "";
            Properties.Settings.Default.NombreFichero = NombreFicheroTB.Text;
            Properties.Settings.Default.IDPC = IDPCTB.Text;
            rutaFormat = RutaFicheroTB.Text + @"\";
            rutaFormat = rutaFormat.Replace(@"\\", @"\");
            Properties.Settings.Default.RutaFichero = rutaFormat;

            Properties.Settings.Default.MovimientosTurnoAnterior = Convert.ToInt32(TurnoPrevioTB.Text);
            Properties.Settings.Default.MovimientosLanzamientoAnterior = Convert.ToInt32(LanzamientoTB.Text);

            //ALARMAS
            Properties.Settings.Default.alarmah1 = MañanaHTB.Text;
            Properties.Settings.Default.alarmam1 = MañanaMTB.Text;

            Properties.Settings.Default.alarmah2 = TardeHTB.Text;
            Properties.Settings.Default.alarmam2 = TardeMTB.Text;

            Properties.Settings.Default.alarmah3 = NocheHTB.Text;
            Properties.Settings.Default.alarmam3 = NocheMTB.Text;


            Debug.Print("Ruta:" + rutaFormat);

            Properties.Settings.Default.AvisoCampos = CamposTB.Text;
            Properties.Settings.Default.AvisoProblemaFichero = ProblemaFicheroTB.Text;
            Properties.Settings.Default.AvisoMantenimiento = MantenimientoTB.Text;
            Properties.Settings.Default.AvisoVolumenLlen = TempLlenTB.Text;
            Properties.Settings.Default.AvisoAlarma = RegistroAlarmaTB.Text;
            Properties.Settings.Default.Save();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        //ALARMAS
        private void MañanaAcpB_Click(object sender, EventArgs e)
        {
            if ((MañanaHTB.Text != "" && MañanaMTB.Text != ""))
            {
                //Convertimos las variables string en variables enteras para asgurarnos de que la alarma no permite registrar horarios erroneos
                int alarmah1TBint = Convert.ToInt16(MañanaHTB.Text);
                int alarmam1TBint = Convert.ToInt16(MañanaMTB.Text);

                //Comprobamos que la alarma fijada tiene una hora lógica
                if (((alarmah1TBint > 23 || alarmah1TBint < 0) || (alarmam1TBint > 59 || alarmam1TBint < 0)) || (MañanaHTB.Text.Length != 2 || MañanaMTB.Text.Length != 2))
                {
                    MañanaHTB.Text = "";
                    MañanaMTB.Text = "";


                    MañanaAcpB.BackColor = Color.White;
                    MessageBox.Show(Properties.Settings.Default.AvisoAlarma);
                }
                else
                {
                    MañanaAcpB.BackColor = Color.DarkSeaGreen;
                }
            }
        }
        private void TardeAcpB_Click(object sender, EventArgs e)
        {
            if ((TardeHTB.Text != "" && TardeMTB.Text != ""))
            {
                //Convertimos las variables string en variables enteras para asgurarnos de que la alarma no permite registrar horarios erroneos
                int alarmah2TBint = Convert.ToInt16(TardeHTB.Text);
                int alarmam2TBint = Convert.ToInt16(TardeMTB.Text);

                //Comprobamos que la alarma fijada tiene una hora lógica
                if (((alarmah2TBint > 23 || alarmah2TBint < 0) || (alarmam2TBint > 59 || alarmam2TBint < 0)) || (TardeHTB.Text.Length != 2 || TardeMTB.Text.Length != 2))
                {
                    TardeHTB.Text = "";
                    TardeMTB.Text = "";


                    TardeAcpB.BackColor = Color.White;
                    MessageBox.Show(Properties.Settings.Default.AvisoAlarma);
                }
                else
                {
                    TardeAcpB.BackColor = Color.DarkSeaGreen;
                }
            }
        }
        private void NocheAcpB_Click(object sender, EventArgs e)
        {
            if ((NocheHTB.Text != "" && NocheMTB.Text != ""))
            {
                //Convertimos las variables string en variables enteras para asgurarnos de que la alarma no permite registrar horarios erroneos
                int alarmah3TBint = Convert.ToInt16(NocheHTB.Text);
                int alarmam3TBint = Convert.ToInt16(NocheMTB.Text);

                //Comprobamos que la alarma fijada tiene una hora lógica
                if (((alarmah3TBint > 23 || alarmah3TBint < 0) || (alarmam3TBint > 59 || alarmam3TBint < 0)) || (NocheHTB.Text.Length != 2 || NocheMTB.Text.Length != 2))
                {
                    NocheHTB.Text = "";
                    NocheMTB.Text = "";

                    NocheAcpB.BackColor = Color.White;
                    MessageBox.Show(Properties.Settings.Default.AvisoAlarma);
                }
                else
                {
                    NocheAcpB.BackColor = Color.DarkSeaGreen;
                }
            }
        }

        private void SelecColorB_Click(object sender, EventArgs e)
        {
            ColorDialog Color = new ColorDialog();
            if (Color.ShowDialog() == DialogResult.OK)
            {
                PrincipalTB.BackColor = Color.Color;
                Properties.Settings.Default.COLOR1 = Color.Color;
                Properties.Settings.Default.Save();
                MaquinaLinea.COLOR1 = Properties.Settings.Default.COLOR1;
            }
        }


    }
}

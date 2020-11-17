using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;

namespace WHPS.ProgramMenus
{
    public partial class WHPST_MENU : Form
    {
        WHPST_INICIO parent; 
        public WHPST_MENU(WHPST_INICIO p)
        {
            InitializeComponent();
            parent = p;
        }
        //Mostramso en pantalla la fecha y el día de hoy
        public void WHPST_MENU_Load(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            lbFecha.Text = DateTime.Now.ToLongDateString();
        }

        //Timer que actualizará la hora
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void ActualizacionLB_DoubleClick(object sender, EventArgs e)
        {

            File.Delete(Properties.Settings.Default.RutaFichero + Properties.Settings.Default.NombreFichero);
            File.Copy(Properties.Settings.Default.RutaFichero_Red + Properties.Settings.Default.NombreFichero, Properties.Settings.Default.RutaFichero + Properties.Settings.Default.NombreFichero);
            Process.Start(MaquinaLinea.FileActualizacion);
            Environment.Exit(0);
        }
    }
}

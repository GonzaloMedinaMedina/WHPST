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
        public WHPST_MENU()
        {
            InitializeComponent();
        }
        //Mostramso en pantalla la fecha y el día de hoy
        private void WHPST_MENU_Load(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            lbFecha.Text = DateTime.Now.ToLongDateString();
            timer1.Enabled = true;
        }

        //Timer que actualizará la hora
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void ActualizacionLB_DoubleClick(object sender, EventArgs e)
        {
            //ID_ACTUALIZADA(Properties.Settings.Default.IDPC);

            File.Delete(Properties.Settings.Default.RutaFichero + Properties.Settings.Default.NombreFichero);
            File.Copy(Properties.Settings.Default.RutaFichero_Red + Properties.Settings.Default.NombreFichero, Properties.Settings.Default.RutaFichero + Properties.Settings.Default.NombreFichero);


            Process.Start(MaquinaLinea.FileActualizacion);

            Environment.Exit(0);

        }
        private void ID_ACTUALIZADA(string ID)
        {
            //switch (ID)
            //{
            //    case "IDPC_NULL":
            //        Properties.Settings.Default.Actualizar = false;
            //        break;
            //    case "ID_DespL5":
            //        if ()
            //        Properties.Settings.Default.Actualizar = true;
            //        break;

            //    case "ID_LlenL5":
            //        Properties.Settings.Default.Actualizar = true;
            //        break;
            //    case "ID_EtiqL5":
            //        Properties.Settings.Default.Actualizar = true;
            //        break;
            //    case "ID_EncL5":
            //        Properties.Settings.Default.Actualizar = true;
            //        break;
            //    case "PC_ADMINISTRADOR":
            //        Properties.Settings.Default.Actualizar = true;
            //        break;
            //    case "PC":
            //        Properties.Settings.Default.Actualizar = true;
            //        break;
            //}
            //Properties.Settings.Default.Save();
        }
    }
}

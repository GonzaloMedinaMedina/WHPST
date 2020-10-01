using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MENU : Form
    {
        public MENU()
        {
            InitializeComponent();
        }

        //Cerramos la aplicación
        private void MENU_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        //Cerramos la aplicación pulsando el boton salir
        private void BOTONSALIR_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        //Abrimos la ventana de ajustes
        private void BOTONAJUSTES_Click(object sender, EventArgs e)
        {
            PRGRAMA_MENU.AJUSTES FromAjustes = new PRGRAMA_MENU.AJUSTES();
            MENU.ActiveForm.Hide();
            FromAjustes.ShowDialog();
        }
        
        /*Abrimos la linea correspondiente y declaramos una variable,
        aunque se abra el mismo modelo de form se distinguirá por esa variable*/
        private void BOTONL2_Click(object sender, EventArgs e)
        {
            PRGRAMA_MENU.SELECCIONMAQUINA FromSeleccionmaquina = new PRGRAMA_MENU.SELECCIONMAQUINA();
            Globals.numero_linea = 2;
            MENU.ActiveForm.Hide();
            FromSeleccionmaquina.Show();
        }

        private void BOTONL3_Click(object sender, EventArgs e)
        {
            PRGRAMA_MENU.SELECCIONMAQUINA FromSeleccionmaquina = new PRGRAMA_MENU.SELECCIONMAQUINA();
            Globals.numero_linea = 3;
            MENU.ActiveForm.Hide();
            FromSeleccionmaquina.Show();
        }

        private void BOTONL5_Click(object sender, EventArgs e)
        {
            PRGRAMA_MENU.SELECCIONMAQUINA FromSeleccionmaquina = new PRGRAMA_MENU.SELECCIONMAQUINA();
            Globals.numero_linea = 5;
            MENU.ActiveForm.Hide();
            FromSeleccionmaquina.Show();
        }


        /*Desde el incicio declaramos las variables estaticas globales
        para que se puedan utilizar en los siguientes form*/

        static class Globals
        {
            //Variable de número de línea
            public static int numero_linea;
            public static string selectmaq;
            //Variables de personal
            public static string Responsable;
            public static string MDespaletizador;
            public static string MLlenadora;
            public static string MEtiquetadora;
            public static string MEncajonadora;
            public static string ControlCal;

            //Variables de cambio de turno
            public static int diaT = Properties.Settings.Default.diaT;
            public static string turno = Properties.Settings.Default.turno;
            public static bool switchT = Properties.Settings.Default.switchT;
            public static bool checkL2 = Properties.Settings.Default.checkL2;
            public static bool checkL3 = Properties.Settings.Default.checkL3;
            public static bool checkL5 = Properties.Settings.Default.checkL5;

            public static bool chDesL2 = Properties.Settings.Default.chDesL2;
            public static bool chLlenL2 = Properties.Settings.Default.chLlenL2;
            public static bool chEtiqL2 = Properties.Settings.Default.chEtiqL2;
            public static bool chEncL2 = Properties.Settings.Default.chEncL2;
            public static bool chConL2 = Properties.Settings.Default.chConL2;

            public static bool chDesL3 = Properties.Settings.Default.chDesL3;
            public static bool chLlenL3 = Properties.Settings.Default.chLlenL3;
            public static bool chEtiqL3 = Properties.Settings.Default.chEtiqL3;
            public static bool chEncL3 = Properties.Settings.Default.chEncL3;
            public static bool chConL3 = Properties.Settings.Default.chConL3;

            public static bool chDesL5 = Properties.Settings.Default.chDesL5;
            public static bool chLlenL5 = Properties.Settings.Default.chLlenL5;
            public static bool chEtiqL5 = Properties.Settings.Default.chEtiqL5;
            public static bool chEncL5 = Properties.Settings.Default.chEncL5;
            public static bool chConL5 = Properties.Settings.Default.chConL5;
            

            //Variables de fichero
            public static string FileMateriales = "Materiales";
            public static string FileDespaletizador;

            //Variables globales despaletizador
            public static string UltBotella_Ref = "";
            public static string UltBotella_Descp = "";
      }
   }
}
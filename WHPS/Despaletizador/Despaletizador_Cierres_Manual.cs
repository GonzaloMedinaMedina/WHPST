using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WHPS.Utiles;
using System.Diagnostics;
using System.IO;
using WHPS.ProgramMenus;
using WHPS.Model;


namespace WHPS.Despaletizador
{
    public partial class Despaletizador_Cierres_Manual : Form
    {

        public static bool modo_manual = false;
        public Process osk;
        Datos_Cierres datos_cierres = new Datos_Cierres();

        public Despaletizador_Cierres_Manual()
        {
            InitializeComponent();

            InputTB.Select();
            grupboxteclado.Hide();

            //Rellenamos la información conocida
            //Rellenamos responsable y maquinista
            respTB.Text = MaquinaLinea.Responsable;
            maqTB.Text = MaquinaLinea.MDespaletizador;
            //Rellenamos fecha
            dateTB.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

            //Rellenamos el turno - Identificando el turno
            turnoTB.Text = Utilidades.ShiftCheck();

            //Nos aseguramos que el modo manual esta desactivado al inicio
            ModoManualBot.BackColor = Color.LightGray;

        }

        //Cerramos la aplicación
        private void Despaletizador_Cierres_Manual_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //Volvemos a la pantalla anterior pulsando el boton back
        private void BackB_Click(object sender, EventArgs e)
        {
            MainDespaletizador Form = new MainDespaletizador();
            Hide();
            Form.Show();
        }

        //Cargamos directamente los datos que ya tenemos registrados
        private void Despaletizador_Cierres_Manual_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));

            Utilidades.ShiftCheck();
        }

        //Un temporizador, nos sincroniza con la pantalla del main para que si al volver ha se ha activado la alarma nos avise
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            if (Properties.Settings.Default.chalarma == false && ((MaquinaLinea.numlin == 2 && MaquinaLinea.chDesL2 == true) || (MaquinaLinea.numlin == 3 && MaquinaLinea.chDesL3 == true) || (MaquinaLinea.numlin == 5 && MaquinaLinea.chDesL5 == true)) && ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00"))))
            {
                Properties.Settings.Default.chalarma = true;
                Properties.Settings.Default.Save();
            }
        }

        //Borramos los campos ya marcados
        private void Borrar_Click(object sender, EventArgs e)
        {
            DescTB.Text = "";
            eanTB.Text = "";
            refwhTB.Text = "";
            provTB.Text = "";
            fabdateTB.Text = "";
            ssccTB.Text = "";
            loteTB.Text = "";
            InputTB.Text = "";
            InputTB.Select();
        }

        //Rellena automaticamente los datos del cierre leyendo y descodificando el código
        private void InputTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Datos_Cierres prasing = new Datos_Cierres();
                prasing = Apps_Despaletizador.ParsingCod_Cierres(InputTB.Text);

                //Rellenamos todos los datos que han sido identificados
                if (prasing.whDescrip != "")
                {
                    DescTB.Text = prasing.whDescrip;
                }
                if (prasing.ean != "")
                {
                    eanTB.Text = prasing.ean;
                }
                if (prasing.refInt != "")
                {
                    refwhTB.Text = prasing.refInt;
                }
                if (prasing.Proveedor != "")
                {
                    provTB.Text = prasing.Proveedor;
                }
                if (prasing.FechaFab != "")
                {
                    fabdateTB.Text = prasing.FechaFab;
                }
                if (prasing.SSCC != "")
                {
                    ssccTB.Text = prasing.SSCC;
                }
                if (prasing.LoteFab != "")
                {
                    loteTB.Text = prasing.LoteFab;
                }
                InputTB.Text = "";
            }
        }

        //Entramos en modo manual
        private void ModoManualBot_Click(object sender, EventArgs e)
        {
            if (modo_manual == true)
            {
                ModoManualBot.BackColor = Color.LightGray;
                modo_manual = false;
            }
            else
            {
                ModoManualBot.BackColor = Color.Green;
                modo_manual = true;
                grupboxteclado.Hide();
            }
        }
        private void InputTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (modo_manual == true)
            {
                grupboxteclado.Show();
            }
        }



        //
        //
        //CONSULTAR QUE ES LA BUSQUEDAD DE LA DESCRIPCION
        //
        //
        //############################  BUSQUEDA DE LA DESCRIPCIÓN ########################
        private void Search_Descp()
        {
            if (refwhTB.Text != "")
            {
                //Generamos la lista de valores a filtrar y traer desde la lista de materiales AND, 'Columna', LIKE, 'Codigo Material'
                List<string[]> listavalores = new List<string[]>();
                string[] valores = new string[4];
                valores[0] = "AND";
                valores[1] = "Codigo";
                valores[2] = "LIKE";
                valores[3] = " \"" + refwhTB.Text + "\"";
                listavalores.Add(valores);

                //Llamamos la busqueda del fichero excel
                string result;
                DataSet excelDataSet = new DataSet();
                excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileMateriales, "Cierres", "Codigo;Descripción".Split(';'), listavalores, out result);
                //MessageBox.Show(result);

                if (excelDataSet.Tables[0].Rows.Count > 0)
                {
                    DescTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Descripción"]);
                }
                else
                {
                    MessageBox.Show("CODIGO NO ENCONTRADO");

                }
            }
        }





        //##############################  ON SCREEN KEYBOARD  ####################################################
        private void button1_Click(object sender, EventArgs e)
        {
            InputTB.Text = InputTB.Text + "1";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            InputTB.Text = InputTB.Text + "2";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            InputTB.Text = InputTB.Text + "3";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            InputTB.Text = InputTB.Text + "4";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            InputTB.Text = InputTB.Text + "5";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            InputTB.Text = InputTB.Text + "6";
        }
        private void button7_Click(object sender, EventArgs e)
        {
            InputTB.Text = InputTB.Text + "7";
        }
        private void button8_Click(object sender, EventArgs e)
        {
            InputTB.Text = InputTB.Text + "8";
        }
        private void button9_Click(object sender, EventArgs e)
        {
            InputTB.Text = InputTB.Text + "9";
        }
        private void button10_Click(object sender, EventArgs e)
        {
            InputTB.Text = InputTB.Text + "0";
        }
        private void button11_Click(object sender, EventArgs e)
        {
            InputTB.Text = "";
        }
        private void button12_Click(object sender, EventArgs e)
        {
            InputTB.Select();
            SendKeys.Send("{ENTER}");
            grupboxteclado.Hide();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            if (Environment.Is64BitOperatingSystem)
            {
                try
                {
                    //Process.Start(Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "Sysnative", "cmd.exe"), "/c osk.exe" + " & exit");
                    Process p1 = new Process();
                    p1.StartInfo.FileName = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "Sysnative", "cmd.exe");
                    p1.StartInfo.Arguments = "/c osk.exe";
                    p1.StartInfo.UseShellExecute = false;
                    p1.StartInfo.CreateNoWindow = true;

                    p1.Start();

                    p1.WaitForExit(100);
                    p1.Close();
                }
                catch { }
            }
            else
            {
                try
                {
                    Process.Start(@"osk.exe");

                }
                catch { }
            }
            grupboxteclado.Hide();
            Activate();
            InputTB.Select();
        }
        
        private void exittecladobot_Click(object sender, EventArgs e)
        {
            grupboxteclado.Hide();
        }
        //##############################  END ON SCREEN KEYBOARD  ####################################################


        //##############################  ON SCREEN PROVEEDORES  ####################################################
        private void Prov1B_Click(object sender, EventArgs e)
        {
            provTB.Text = "CAPSULAS TORRENT, S.A.";
            Cod2TB.Text = "CAPSULAS TORRENT, S.A.";

        }
        private void Prov2B_Click(object sender, EventArgs e)
        {
            provTB.Text = "TAPON JEREZ, S.C.A.";
            Cod2TB.Text = "TAPON JEREZ, S.C.A.";

        }
        private void Prov3B_Click(object sender, EventArgs e)
        {
            provTB.Text = "HDROS.DE TORRENT MIRANDA, S.L.";
            Cod2TB.Text = "HDROS.DE TORRENT MIRANDA, S.L.";

        }
        private void Prov4B_Click(object sender, EventArgs e)
        {
            provTB.Text = "DIAM CORCHOS, S.A.";
            Cod2TB.Text = "DIAM CORCHOS, S.A.";

        }
        private void Prov5B_Click(object sender, EventArgs e)
        {
            provTB.Text = "MANUF.Y DIST. GADICORK, S.L.";
            Cod2TB.Text = "MANUF.Y DIST. GADICORK, S.L.";
        }
        private void Prov6B_Click(object sender, EventArgs e)
        {
            provTB.Text = "GUALA CLOSURES IBERICA S.A.";
            Cod2TB.Text = "GUALA CLOSURES IBERICA S.A.";

        }
        //###########################  END ON SCREEN PROVEEDORES  ####################################################

        //#### GUARDADO DEL FICHERO ####
        private void saveBot_Click(object sender, EventArgs e)
        {
            if ((eanTB.Text != "") && (refwhTB.Text != "") && (provTB.Text != "") && (fabdateTB.Text != "") && (loteTB.Text != ""))
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "Fecha", DateTime.Now.ToString("dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Hora", DateTime.Now.ToString("HH:mm:ss") });
                listavalores.Add(new string[2] { "FechaDB", DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Responsable", MaquinaLinea.Responsable });
                listavalores.Add(new string[2] { "Maquinista", MaquinaLinea.MDespaletizador });
                listavalores.Add(new string[2] { "Turno", turnoTB.Text });
                listavalores.Add(new string[2] { "LoteFab", datos_cierres.LoteFab });
                listavalores.Add(new string[2] { "RefInterna", refwhTB.Text });
                listavalores.Add(new string[2] { "Proveedor", provTB.Text });
                listavalores.Add(new string[2] { "EAN", datos_cierres.ean });
                listavalores.Add(new string[2] { "Cantidad", datos_cierres.Cantidad });
                listavalores.Add(new string[2] { "SSCC", datos_cierres.SSCC });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileDespaletizador, "Cierres", listavalores, "Id");
                if (salida.Contains("ERROR"))
                {
                    //MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                    MessageBox.Show(salida.ToString());
                }
                MainDespaletizador Form = new MainDespaletizador();
                Hide();
                Form.Show();
            }
            else
            {
                MessageBox.Show("FALTAN CAMPOS POR RELLENAR! -> NO SE HA GUARDADO LA INFORMACIÓN");
            }
        }
        
        //private void loteInTB_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (tecla == "Lote")
        //        {
        //            loteTB.Text = loteInTB.Text;
        //            groupBox3.Hide();
        //            foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("osk"))
        //            {
        //                process.Kill();
        //            }


        //        }
        //        if (tecla == "")
        //        {
        //            Datos_Cierres parsing = new Datos_Cierres();
        //            parsing = Apps_Despaletizador.ParsingCod_Cierres(loteInTB.Text);

        //            if (parsing.whDescrip != "")
        //            {
        //                DescTB.Text = parsing.whDescrip;
        //                datos_cierres.whDescrip = parsing.whDescrip;
        //            }
        //            if (parsing.ean != "")
        //            {
        //                //No esta incluido en la pantalla
        //                datos_cierres.whDescrip = parsing.whDescrip;
        //            }
        //            if (parsing.refInt != "")
        //            {
        //                refwhTB.Text = parsing.refInt;
        //                datos_cierres.refInt = parsing.refInt;
        //            }
        //            if (parsing.Proveedor != "")
        //            {
        //                provTB.Text = parsing.Proveedor;
        //                datos_cierres.Proveedor = parsing.Proveedor;
        //            }
        //            if (parsing.SSCC != "")
        //            {
        //                //No esta incluido en pantalla
        //                datos_cierres.SSCC = parsing.SSCC;
        //            }
        //            if (parsing.LoteFab != "")
        //            {
        //                loteTB.Text = parsing.LoteFab;
        //                datos_cierres.LoteFab = parsing.LoteFab;
        //            }
        //            loteInTB.Text = "";
        //        }

        //    }
        //}

        //private void RefInTB_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (tecla == "ReferenciaWH")
        //        {
        //            refwhTB.Text = RefInTB.Text;
        //            groupBox3.Hide();
        //            Search_Descp();
        //            foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("osk"))
        //            {
        //                process.Kill();
        //            }
        //        }
        //    }

        //}
        
    }//## END CLASS
} //## END NAMESPACE

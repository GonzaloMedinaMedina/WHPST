using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WHPS.Utiles;
using System.Diagnostics;
using System.IO;
using WHPS.Model;

namespace WHPS.ProgramMenus
{
    public partial class WHPST_Cambio_Turno : Form
    {
        public string Turno = "";
        public string Puntero = "";
        public bool cambio = false;
        public WHPST_Cambio_Turno()
        {
            InitializeComponent();
        }

        //Cagarmos los datos conocidos
        private void WHPST_Cambio_Turno_Load(object sender, EventArgs e)
        {
            //Rellenanmos los parametro ya conocidos
            numlinTB.Text = MaquinaLinea.numlin.ToString();
            turnoTB.Text = Utilidades.ObtenerTurnoActual();
            cambio = false;
            //################  LOAD FILES #################3
            //Generamos la lista de valores a filtrar y traer desde la lista de materiales AND, 'Columna', LIKE, 'Codigo Material'
            List<string[]> listavalores = new List<string[]>();
            Turno = turnoTB.Text;

            string result;
            DataSet excelDataSet = new DataSet();
            excelDataSet = ExcelUtiles.LeerFicheroExcel("Datos_Lineas", "L" + MaquinaLinea.numlin.ToString(), (Turno).Split(';'), listavalores, out result);
            if (excelDataSet.Tables.Count > 0 && excelDataSet.Tables[0].Rows.Count > 0)
            {
                try
                {
                    //PrintTableOrView(dataTable, "TABLA:");
                    respTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0][Turno]);
                    DespTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[1][Turno]);
                    LlenTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[2][Turno]);
                    EtiqTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[3][Turno]);
                    EncTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[4][Turno]);
                    ContTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[5][Turno]);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    //throw;
                }
            }
            else
            {
                MessageBox.Show("Error en la carga del fichero");
            }
        }
        //Función que muestra un teclado en pantalla
        private void OpenOSK()
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
            Activate();
        }
        //Funcion que muetra el siguiente form al guardar
        private void AbrirForm(object Form)
        {
            if (this.PanelCambioturno.Controls.Count > 0)
            {
                this.PanelCambioturno.Controls.RemoveAt(0);
            }
            Form SM = Form as Form;
            SM.TopLevel = false;
            SM.Dock = DockStyle.Fill;
            this.PanelCambioturno.Controls.Add(SM);
            this.PanelCambioturno.Tag = SM;
            SM.Show();
        }

        //Si el teclado no esta activo, se activará pulsando algunos de los textbox
        private void respTB_MouseDown(object sender, MouseEventArgs e)
        {
            //OpenOSK();
            Puntero = "Responsable";
            //Hacemos activo el campo de texto y marcamos el cursor para la selección
            WHPST_Cambio_Turno.ActiveForm.Activate();
            respTB.Select();
            cambio = true;
        }
        private void DespTB_MouseDown(object sender, MouseEventArgs e)
        {
            //OpenOSK();
            Puntero = "Despaletizador";
            //Hacemos activo el campo de texto y marcamos el cursor para la selección
            WHPST_Cambio_Turno.ActiveForm.Activate();
            DespTB.Select();
            cambio = true;
    }
        private void LlenTB_MouseDown(object sender, MouseEventArgs e)
        {
            //OpenOSK();
            Puntero = "Llenadora";
            //Hacemos activo el campo de texto y marcamos el cursor para la selección
            ActiveForm.Activate();
            LlenTB.Select();
            cambio = true;
        }
        private void EtiqTB_MouseDown(object sender, MouseEventArgs e)
        {
            //OpenOSK();
            Puntero = "Etiquetadora";
            //Hacemos activo el campo de texto y marcamos el cursor para la selección
            ActiveForm.Activate();
            EtiqTB.Select();
            cambio = true;
        }
        private void EncTB_MouseDown(object sender, MouseEventArgs e)
        {
            //OpenOSK();
            Puntero = "Encajadora";
            //Hacemos activo el campo de texto y marcamos el cursor para la selección
            ActiveForm.Activate();
            EncTB.Select();
            cambio = true;
        }
        private void ContTB_MouseDown(object sender, MouseEventArgs e)
        {
            //OpenOSK();
            Puntero = "Control";
            //Hacemos activo el campo de texto y marcamos el cursor para la selección
            ActiveForm.Activate();
            ContTB.Select();
            cambio = true;
        }

        //Guardamos la información
        private void saveBot_Click(object sender, EventArgs e)
        {
            if (cambio == true)
            {
                //Filtro Encargado
                List<string[]> valoresAFiltrar = new List<string[]>();
                string[] filterval = new string[4];
                filterval[0] = "AND";
                filterval[1] = "Puesto";
                filterval[2] = "LIKE";
                filterval[3] = "\"Encargado\"";
                valoresAFiltrar.Add(filterval);

                List<string[]> valoresAActualizar = new List<string[]>();
                string[] updateval = new string[2];
                updateval[0] = Turno;
                updateval[1] = respTB.Text;
                valoresAActualizar.Add(updateval);
                string salida;
                salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + MaquinaLinea.numlin.ToString(), valoresAActualizar, valoresAFiltrar);
                //MessageBox.Show(salida);

                //### DESPALETIZADOR #####
                filterval[3] = "\"Despaletizador\"";
                updateval[1] = DespTB.Text;
                salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + MaquinaLinea.numlin.ToString(), valoresAActualizar, valoresAFiltrar);
                //MessageBox.Show(salida);

                //### Llenadora #####
                filterval[3] = "\"Llenadora\"";
                updateval[1] = LlenTB.Text;
                salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + MaquinaLinea.numlin.ToString(), valoresAActualizar, valoresAFiltrar);
                //MessageBox.Show(salida);

                //### Etiquetadora #####
                filterval[3] = "\"Etiquetadora\"";
                updateval[1] = EtiqTB.Text;
                salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + MaquinaLinea.numlin.ToString(), valoresAActualizar, valoresAFiltrar);
                //MessageBox.Show(salida);

                //### Encajadora #####
                filterval[3] = "\"Encajadora\"";
                updateval[1] = EncTB.Text;
                salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + MaquinaLinea.numlin.ToString(), valoresAActualizar, valoresAFiltrar);
                //MessageBox.Show(salida);

                //### Control Calidad #####
                filterval[3] = "\"Control\"";
                updateval[1] = ContTB.Text;
                salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + MaquinaLinea.numlin.ToString(), valoresAActualizar, valoresAFiltrar);
                //MessageBox.Show(salida);

            }

            //#######  ACTUALIZAMOS SETTINGS  #############
            MaquinaLinea.turno = turnoTB.Text;
            MaquinaLinea.diaT = Convert.ToInt16(DateTime.Now.ToString("dd"));
            MaquinaLinea.switchT = true;

            Properties.Settings.Default.turno = turnoTB.Text;
            Properties.Settings.Default.diaT = MaquinaLinea.diaT;
            Properties.Settings.Default.switchT = true;


            //Marcamos que linea se ha comprobado el personal
            if (numlinTB.Text == "2" && MaquinaLinea.checkL2 == false)
            {
                Properties.Settings.Default.checkL2 = true;
                Properties.Settings.Default.chDesL2 = false;
                Properties.Settings.Default.chLlenL2 = false;
                Properties.Settings.Default.chEtiqL3 = false;
                Properties.Settings.Default.chEncL2 = false;
                Properties.Settings.Default.chalarmaDesL2 = false;
                Properties.Settings.Default.chalarmaLlenL2 = false;
                Properties.Settings.Default.chalarmaEtiqL2 = false;
                Properties.Settings.Default.chalarmaEncL2 = false;

                //Ponemos a 0 la alarma de la llenadora
                Properties.Settings.Default.ContadorC30LlenL2 = 16;
                Properties.Settings.Default.AlarmaC30LlenL2 = false;
            }
            if (numlinTB.Text == "3" && MaquinaLinea.checkL3 == false)
            {
                Properties.Settings.Default.checkL3 = true;
                Properties.Settings.Default.chDesL3 = false;
                Properties.Settings.Default.chLlenL3 = false;
                Properties.Settings.Default.chEtiqL3 = false;
                Properties.Settings.Default.chEncL3 = false;
                Properties.Settings.Default.chalarmaDesL3 = false;
                Properties.Settings.Default.chalarmaLlenL3 = false;
                Properties.Settings.Default.chalarmaEtiqL3 = false;
                Properties.Settings.Default.chalarmaEncL3 = false;

                //Ponemos a 0 la alarma de la llenadora
                Properties.Settings.Default.ContadorC30LlenL3 = 16;
                Properties.Settings.Default.AlarmaC30LlenL3 = false;
            }
            if (numlinTB.Text == "5" && MaquinaLinea.checkL5 == false)
            {
                Properties.Settings.Default.checkL5 = true;
                Properties.Settings.Default.chDesL5 = false;
                Properties.Settings.Default.chLlenL5 = false;
                Properties.Settings.Default.chEtiqL5 = false;
                Properties.Settings.Default.chEncL5 = false;
                Properties.Settings.Default.chalarmaDesL5 = false;
                Properties.Settings.Default.chalarmaLlenL5 = false;
                Properties.Settings.Default.chalarmaEtiqL5 = false;
                Properties.Settings.Default.chalarmaEncL5 = false;

                //Ponemos a 0 la alarma de la llenadora
                Properties.Settings.Default.ContadorC30LlenL5 = 16;
                Properties.Settings.Default.AlarmaC30LlenL5 = false;
            }
            Properties.Settings.Default.Save();
            MaquinaLinea.ActualizarYGuardarValores(respTB.Text, DespTB.Text, LlenTB.Text, EtiqTB.Text, EncTB.Text, ContTB.Text, turnoTB.Text, numlinTB.Text);
            //Cerramos y vamos al menú principal
            MaquinaLinea.RetornoInicio = "SelecMaquina";
            saveBot.BackColor = System.Drawing.Color.DarkSeaGreen;
            //WHPST_INICIO Form = new WHPST_INICIO();
            //Hide();
            //Form.Show();
        }

        private void PersonalListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            respTB.Text = ResponsableListBox.SelectedItem.ToString();
        }

        private void MaquinistaListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Puntero)
            {
                case "Despaletizador":
                    DespTB.Text = MaquinistaListBox.SelectedItem.ToString();
                    break;
                case "Llenadora":
                    LlenTB.Text = MaquinistaListBox.SelectedItem.ToString();

                    break;
                case "Etiquetadora":
                    EtiqTB.Text = MaquinistaListBox.SelectedItem.ToString();
                    break;
                case "Encajadora":
                    EncTB.Text = MaquinistaListBox.SelectedItem.ToString();
                    break;
                case "Control":
                    ContTB.Text = MaquinistaListBox.SelectedItem.ToString();
                    break;
            }
        }

        private void EventualesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Puntero)
            {
                case "Despaletizador":
                    DespTB.Text = EventualesListBox.SelectedItem.ToString();
                    break;
                case "Llenadora":
                    LlenTB.Text = EventualesListBox.SelectedItem.ToString();
                    break;
                case "Etiquetadora":
                    EtiqTB.Text = EventualesListBox.SelectedItem.ToString();
                    break;
                case "Encajadora":
                    EncTB.Text = EventualesListBox.SelectedItem.ToString();
                    break;
                case "Control":
                    ContTB.Text = EventualesListBox.SelectedItem.ToString();
                    break;
            }
        }
    }
}

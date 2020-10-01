using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.ProgramMenus;
using WHPS.Utiles;
using System.Diagnostics;
using System.IO;


namespace WHPS.Llenadora
{
    public partial class Llenadora_Verificacion_Cierre_Volumen : Form
    {
        //Declaramos las variable que iran registrando los valores en los diferentes textbox
        public string SensorTapon = "";
        public string NivelVolumen = "";
        public string CodigoMaterial = "";


        public Llenadora_Verificacion_Cierre_Volumen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>
        private void ExitB_Click(object sender, EventArgs e)
        {
            MainLlenadora Form = new MainLlenadora();
            Hide();
            Form.Show();
            GC.Collect();
        }
        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

        private void Llenadora_Verificacion_Cierre_Volumen_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //Rellenamos fecha
            dateTB.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //Rellenamos responsable y maquinista
            respTB.Text = MaquinaLinea.Responsable;
            maqTB.Text = MaquinaLinea.MLlenadora;

            //Rellenamos el turno - Identificando el turno
            turnoTB.Text = Utilidades.ObtenerTurnoActual();
            if (MaquinaLinea.CodigoProd != "")
            {
                try
                {
                    ExtraerDatosBOM(MaquinaLinea.CodigoProd);
                    ExtraerDatosMateriales(CodigoMaterial);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    MessageBox.Show("No se ha encontrado la referencia");
                }
            }

            //Cargamos las verificaciones registradas.
            if (MaquinaLinea.numlin == 2)
            {
                CompletarRegistros(1, Properties.Settings.Default.TipoCierre1LlenL2, Properties.Settings.Default.Proveedores1LlenL2, Properties.Settings.Default.HoraVerificacion1LlenL2, Properties.Settings.Default.SensorTapon1LlenL2, Properties.Settings.Default.NivelVolumen1LlenL2);
                CompletarRegistros(2, Properties.Settings.Default.TipoCierre2LlenL2, Properties.Settings.Default.Proveedores2LlenL2, Properties.Settings.Default.HoraVerificacion2LlenL2, Properties.Settings.Default.SensorTapon2LlenL2, Properties.Settings.Default.NivelVolumen2LlenL2);
                CompletarRegistros(3, Properties.Settings.Default.TipoCierre3LlenL2, Properties.Settings.Default.Proveedores3LlenL2, Properties.Settings.Default.HoraVerificacion3LlenL2, Properties.Settings.Default.SensorTapon3LlenL2, Properties.Settings.Default.NivelVolumen3LlenL2);
            }
            if (MaquinaLinea.numlin == 3)
            {
                CompletarRegistros(1, Properties.Settings.Default.TipoCierre1LlenL3, Properties.Settings.Default.Proveedores1LlenL3, Properties.Settings.Default.HoraVerificacion1LlenL3, Properties.Settings.Default.SensorTapon1LlenL3, Properties.Settings.Default.NivelVolumen1LlenL3);
                CompletarRegistros(2, Properties.Settings.Default.TipoCierre2LlenL3, Properties.Settings.Default.Proveedores2LlenL3, Properties.Settings.Default.HoraVerificacion2LlenL3, Properties.Settings.Default.SensorTapon2LlenL3, Properties.Settings.Default.NivelVolumen2LlenL3);
                CompletarRegistros(3, Properties.Settings.Default.TipoCierre3LlenL3, Properties.Settings.Default.Proveedores3LlenL3, Properties.Settings.Default.HoraVerificacion3LlenL3, Properties.Settings.Default.SensorTapon3LlenL3, Properties.Settings.Default.NivelVolumen3LlenL3);
            }
            if (MaquinaLinea.numlin == 5)
            {
                CompletarRegistros(1, Properties.Settings.Default.TipoCierre1LlenL5, Properties.Settings.Default.Proveedores1LlenL5, Properties.Settings.Default.HoraVerificacion1LlenL5, Properties.Settings.Default.SensorTapon1LlenL5, Properties.Settings.Default.NivelVolumen1LlenL5);
                CompletarRegistros(2, Properties.Settings.Default.TipoCierre2LlenL5, Properties.Settings.Default.Proveedores2LlenL5, Properties.Settings.Default.HoraVerificacion2LlenL5, Properties.Settings.Default.SensorTapon2LlenL5, Properties.Settings.Default.NivelVolumen2LlenL5);
                CompletarRegistros(3, Properties.Settings.Default.TipoCierre3LlenL5, Properties.Settings.Default.Proveedores3LlenL5, Properties.Settings.Default.HoraVerificacion3LlenL5, Properties.Settings.Default.SensorTapon3LlenL5, Properties.Settings.Default.NivelVolumen3LlenL5);
            }
            HoraVerificacionTB.Select();
        }

        //Un temporizador, nos sincroniza con la pantalla del main para que si al volver ha se ha activado la alarma nos avise
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Para activar la alarma debe, estar desactivada, haberse chequeado el inicio de turno y que la hora cuadre con la alarma 
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }
            string Hora = lbReloj.Text;
            if (Hora.Substring(3, 2) != "00" && Hora.Substring(3, 2) != "30")
            {
                MaquinaLinea.AnuladorAlarma = true;
            }
            if (Hora.Substring(3, 2) == "00" || Hora.Substring(3, 2) == "30")
            {
                if (MaquinaLinea.AnuladorAlarma == true)
                {
                    Apps_Llenadora.AlarmaControl30min();
                }
            }
        }

        //Para registrar la hora bastará con hacer click en el textbox, en caso de que ya este registrada se preguntará si desea sobreescribir
        private void HoraVerificacionTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (HoraVerificacionTB.Text != "")
            {
                DialogResult opcion;
                opcion = MessageBox.Show("¿Estas seguro de sobrescribir los datos? Perderá la informacion", "", MessageBoxButtons.OKCancel);
                if (opcion == DialogResult.OK)
                {
                    if (MaquinaLinea.numlin == 2)
                    {
                        HoraVerificacionTB.Text = DateTime.Now.ToString("HH:mm:ss");
                    }
                    if (MaquinaLinea.numlin == 3)
                    {
                        HoraVerificacionTB.Text = DateTime.Now.ToString("HH:mm:ss");
                    }
                    if (MaquinaLinea.numlin == 5)
                    {
                        HoraVerificacionTB.Text = DateTime.Now.ToString("HH:mm:ss");
                    }
                }
            }
            if (HoraVerificacionTB.Text == "")
            {
                HoraVerificacionTB.Text = DateTime.Now.ToString("HH:mm:ss");
            }
        }

        //Pulsando los botones, se guarará en las variables corresponientes
        private void SensorTapon_OK_B_Click(object sender, EventArgs e)
        {
            SensorTapon = "OK";
            SensorTapon_OK_B.BackColor = Color.DarkSeaGreen;
            SensorTapon_NOOK_B.BackColor = Color.LightGray;
        }
        private void SensorTapon_NOOK_B_Click(object sender, EventArgs e)
        {
            SensorTapon = "NO OK";
            SensorTapon_NOOK_B.BackColor = Color.IndianRed;
            SensorTapon_OK_B.BackColor = Color.LightGray;
        }
        private void NivelVolumen_OK_B_Click(object sender, EventArgs e)
        {
            NivelVolumen = "OK";
            NivelVolumen_OK_B.BackColor = Color.DarkSeaGreen;
            NivelVolumen_NOOK_B.BackColor = Color.LightGray;
        }
        private void NivelVolumen_NOOK_B_Click(object sender, EventArgs e)
        {
            NivelVolumen = "NO OK";
            NivelVolumen_NOOK_B.BackColor = Color.IndianRed;
            NivelVolumen_OK_B.BackColor = Color.LightGray;
        }

        //Un apartado de compentarios con hacer click aparece un teclado donde indicar cualquier observación
        private void ComentariosTB_MouseClick(object sender, MouseEventArgs e)
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
            //Hacemos activo el campo de texto y marcamos el cursor para la selección
            Activate();
            ComentariosTB.Select();
        }

        //Se resetarán las variables y los registros al pulsar el boton de la papelera
        private void BorrarB_Click(object sender, EventArgs e)
        {
            //Reestablecemos las variebles
            HoraVerificacionTB.Text = "";
            SensorTapon = "";
            NivelVolumen = "";
            ComentariosTB.Text = "";
            SensorTapon_OK_B.BackColor = Color.FromArgb(27, 33, 41);
            SensorTapon_NOOK_B.BackColor = Color.FromArgb(27, 33, 41);
            NivelVolumen_OK_B.BackColor = Color.FromArgb(27, 33, 41);
            NivelVolumen_NOOK_B.BackColor = Color.FromArgb(27, 33, 41);
            CompletarRegistros(1, "", "", "", "", "");
            CompletarRegistros(2, "", "", "", "", "");
            CompletarRegistros(3, "", "", "", "", "");
            EstablecerVariables(MaquinaLinea.numlin, 1, "", "", "", "", "");
            EstablecerVariables(MaquinaLinea.numlin, 2, "", "", "", "", "");
            EstablecerVariables(MaquinaLinea.numlin, 3, "", "", "", "", "");
        }
        //Función que establece la variable
        public void EstablecerVariables(int numlin, int Registro, string TipoCierre, string Proveedor, string HoraVerificacion, string SensorTapon, string NivelVolumen)
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (Registro == 1)
                {
                    Properties.Settings.Default.TipoCierre1LlenL2 = TipoCierre;
                    Properties.Settings.Default.Proveedores1LlenL2 = Proveedor;
                    Properties.Settings.Default.HoraVerificacion1LlenL2 = HoraVerificacion;
                    Properties.Settings.Default.SensorTapon1LlenL2 = SensorTapon;
                    Properties.Settings.Default.NivelVolumen1LlenL2 = NivelVolumen;
                }
                if (Registro == 2)
                {
                    Properties.Settings.Default.TipoCierre2LlenL2 = TipoCierre;
                    Properties.Settings.Default.Proveedores2LlenL2 = Proveedor;
                    Properties.Settings.Default.HoraVerificacion2LlenL2 = HoraVerificacion;
                    Properties.Settings.Default.SensorTapon2LlenL2 = SensorTapon;
                    Properties.Settings.Default.NivelVolumen2LlenL2 = NivelVolumen;
                }
                if (Registro == 3)
                {
                    Properties.Settings.Default.TipoCierre3LlenL2 = TipoCierre;
                    Properties.Settings.Default.Proveedores3LlenL2 = Proveedor;
                    Properties.Settings.Default.HoraVerificacion3LlenL2 = HoraVerificacion;
                    Properties.Settings.Default.SensorTapon3LlenL2 = SensorTapon;
                    Properties.Settings.Default.NivelVolumen3LlenL2 = NivelVolumen;
                }
                Properties.Settings.Default.Save();
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (Registro == 1)
                {
                    Properties.Settings.Default.TipoCierre1LlenL3 = TipoCierre;
                    Properties.Settings.Default.Proveedores1LlenL3 = Proveedor;
                    Properties.Settings.Default.HoraVerificacion1LlenL3 = HoraVerificacion;
                    Properties.Settings.Default.SensorTapon1LlenL3 = SensorTapon;
                    Properties.Settings.Default.NivelVolumen1LlenL3 = NivelVolumen;
                }
                if (Registro == 2)
                {
                    Properties.Settings.Default.TipoCierre2LlenL3 = TipoCierre;
                    Properties.Settings.Default.Proveedores2LlenL3 = Proveedor;
                    Properties.Settings.Default.HoraVerificacion2LlenL3 = HoraVerificacion;
                    Properties.Settings.Default.SensorTapon2LlenL3 = SensorTapon;
                    Properties.Settings.Default.NivelVolumen2LlenL3 = NivelVolumen;
                }
                if (Registro == 3)
                {
                    Properties.Settings.Default.TipoCierre3LlenL3 = TipoCierre;
                    Properties.Settings.Default.Proveedores3LlenL3 = Proveedor;
                    Properties.Settings.Default.HoraVerificacion3LlenL3 = HoraVerificacion;
                    Properties.Settings.Default.SensorTapon3LlenL3 = SensorTapon;
                    Properties.Settings.Default.NivelVolumen3LlenL3 = NivelVolumen;
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (Registro == 1)
                {
                    Properties.Settings.Default.TipoCierre1LlenL5 = TipoCierre;
                    Properties.Settings.Default.Proveedores1LlenL5 = Proveedor;
                    Properties.Settings.Default.HoraVerificacion1LlenL5 = HoraVerificacion;
                    Properties.Settings.Default.SensorTapon1LlenL5 = SensorTapon;
                    Properties.Settings.Default.NivelVolumen1LlenL5 = NivelVolumen;
                }
                if (Registro == 2)
                {
                    Properties.Settings.Default.TipoCierre2LlenL5 = TipoCierre;
                    Properties.Settings.Default.Proveedores2LlenL5 = Proveedor;
                    Properties.Settings.Default.HoraVerificacion2LlenL5 = HoraVerificacion;
                    Properties.Settings.Default.SensorTapon2LlenL5 = SensorTapon;
                    Properties.Settings.Default.NivelVolumen2LlenL5 = NivelVolumen;
                }
                if (Registro == 3)
                {
                    Properties.Settings.Default.TipoCierre3LlenL5 = TipoCierre;
                    Properties.Settings.Default.Proveedores3LlenL5 = Proveedor;
                    Properties.Settings.Default.HoraVerificacion3LlenL5 = HoraVerificacion;
                    Properties.Settings.Default.SensorTapon3LlenL5 = SensorTapon;
                    Properties.Settings.Default.NivelVolumen3LlenL5 = NivelVolumen;
                }
            }
            Properties.Settings.Default.Save();
        }
        //Función que completa los registros
        public void CompletarRegistros(int Registro, string TipoCierre, string Proveedor, string HoraVerificacion, string SensorTapon, string NivelVolumen)
        {
            if (Registro == 1)
            {
                TipoCierre1TB.Text = TipoCierre;
                Proveedor1TB.Text = Proveedor;
                HoraVerificacion1TB.Text = HoraVerificacion;
                SensorTapon1TB.Text = SensorTapon;
                NivelVolumen1TB.Text = NivelVolumen;
            }
            if (Registro == 2)
            {
                TipoCierre2TB.Text = TipoCierre;
                Proveedor2TB.Text = Proveedor;
                HoraVerificacion2TB.Text = HoraVerificacion;
                SensorTapon2TB.Text = SensorTapon;
                NivelVolumen2TB.Text = NivelVolumen;
            }
            if (Registro == 3)
            {
                TipoCierre3TB.Text = TipoCierre;
                Proveedor3TB.Text = Proveedor;
                HoraVerificacion3TB.Text = HoraVerificacion;
                SensorTapon3TB.Text = SensorTapon;
                NivelVolumen3TB.Text = NivelVolumen;
            }
        }

        public void ExtraerDatosBOM(string Referencia)
        {

            int i = 0;
            while (i < 3)
            {
                List<string[]> valoresAFiltrar = new List<string[]>();
                string[] filterval = new string[8];
                filterval[0] = "AND";
                filterval[1] = "CodProd";
                filterval[2] = "LIKE";
                filterval[3] = Referencia;
                valoresAFiltrar.Add(filterval);

                if (i == 0)
                {
                    string[] filterval1 = new string[4];
                    filterval1[0] = "AND";
                    filterval1[1] = "DescMaterial";
                    filterval1[2] = "LIKE";
                    filterval1[3] = "'%" + "TAP." + "%'";
                    valoresAFiltrar.Add(filterval1);
                }
                if (i == 1)
                {
                    string[] filterval2 = new string[4];
                    filterval2[0] = "AND";
                    filterval2[1] = "DescMaterial";
                    filterval2[2] = "LIKE";
                    filterval2[3] = "'%" + "CAPS." + "%'";
                    valoresAFiltrar.Add(filterval2);
                }
                if (i == 2)
                {
                    string[] filterval3 = new string[4];
                    filterval3[0] = "AND";
                    filterval3[1] = "DescMaterial";
                    filterval3[2] = "LIKE";
                    filterval3[3] = "'%" + "C.MET" + "%'";
                    valoresAFiltrar.Add(filterval3);
                }
                DataSet excelDataSet = new DataSet();
                string result;
                //List<string[]> valoresAFiltrar = dgvSelectFiltro.DataSource;
                excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileBOM, "FICHA", "CodMaterial ".Split(';'), valoresAFiltrar, out result);
                //tbSelectSalidaError.Text = result;
               //MessageBox.Show(result);
                    if (excelDataSet.Tables[0].Rows.Count > 0)
                    {
                        CodigoMaterial = Convert.ToString(excelDataSet.Tables[0].Rows[0]["CodMaterial"]);
                        i = 3;
                    }
                valoresAFiltrar.Clear();
                i++;
            }
        }
         public void ExtraerDatosMateriales(string CodigoMaterial)
        {
            List<string[]> valoresAFiltrar = new List<string[]>();
            string[] filterval = new string[8];
            filterval[0] = "AND";
            filterval[1] = "Codigo";
            filterval[2] = "LIKE";
            filterval[3] = CodigoMaterial;
            valoresAFiltrar.Add(filterval);


            DataSet excelDataSet = new DataSet();
            string result;
            //List<string[]> valoresAFiltrar = dgvSelectFiltro.DataSource;
            excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileMateriales, "Cierres", "Descripcion; Proveedor".Split(';'), valoresAFiltrar, out result);
            //tbSelectSalidaError.Text = result;
            //MessageBox.Show(result);
            if (excelDataSet.Tables[0].Rows.Count > 0)
            {
                TipoCierreTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Descripcion"]);
                ProveedorTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Proveedor"]);
            }
        }



        //Para guardar se ha de completar todos los datos
        private void saveBot_Click(object sender, EventArgs e)
        {
            if (HoraVerificacionTB.Text != "" && SensorTapon != "" && NivelVolumen != "")
            {
                List<string[]> listavalores = new List<string[]>();
                string[] valores0 = new string[2];
                string[] valores1 = new string[2];
                string[] valores01 = new string[2];
                string[] valores2 = new string[2];
                string[] valores3 = new string[2];
                string[] valores4 = new string[2];
                string[] valores5 = new string[2];
                string[] valores6 = new string[2];
                string[] valores7 = new string[2];
                string[] valores8 = new string[2];
                string[] valores9 = new string[2];
                string[] valores10 = new string[2];

                valores0[0] = "Fecha";
                valores0[1] = DateTime.Now.ToString("dd/MM/yyyy");
                listavalores.Add(valores0);
                valores1[0] = "Hora";
                valores1[1] = DateTime.Now.ToString("HH:mm:ss");
                listavalores.Add(valores1);
                valores01[0] = "FechaDB";
                valores01[1] = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
                listavalores.Add(valores01);
                valores2[0] = "Responsable";
                valores2[1] = MaquinaLinea.Responsable;
                listavalores.Add(valores2);
                valores3[0] = "Maquinista";
                valores3[1] = MaquinaLinea.MLlenadora;
                listavalores.Add(valores3);
                valores4[0] = "Turno";
                valores4[1] = turnoTB.Text;
                listavalores.Add(valores4);
                valores5[0] = "HoraVerificacion";
                valores5[1] = HoraVerificacionTB.Text;
                listavalores.Add(valores5);
                valores6[0] = "Cierre";
                valores6[1] = TipoCierreTB.Text;
                listavalores.Add(valores6);
                valores7[0] = "Proveedor";
                valores7[1] = ProveedorTB.Text;
                listavalores.Add(valores7);
                valores8[0] = "SensorSuperior";
                valores8[1] = SensorTapon;
                listavalores.Add(valores8);
                valores9[0] = "NivelVolumen";
                valores9[1] = NivelVolumen;
                listavalores.Add(valores9);
                valores10[0] = "Comentarios";
                valores10[1] = ComentariosTB.Text;
                listavalores.Add(valores10);

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileLlenadora, "VerificacionCierreVolumen", listavalores, "Id");
                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                }
                else
                {
                    if (HoraVerificacion3TB.Text == "" && HoraVerificacion2TB.Text != "")
                    {
                        EstablecerVariables(MaquinaLinea.numlin, 3, TipoCierreTB.Text, ProveedorTB.Text, HoraVerificacionTB.Text, SensorTapon, NivelVolumen);
                        if (MaquinaLinea.numlin == 2) CompletarRegistros(3, Properties.Settings.Default.TipoCierre3LlenL2, Properties.Settings.Default.Proveedores3LlenL2, Properties.Settings.Default.HoraVerificacion3LlenL2, Properties.Settings.Default.SensorTapon3LlenL2, Properties.Settings.Default.NivelVolumen3LlenL2);
                        if (MaquinaLinea.numlin == 3) CompletarRegistros(3, Properties.Settings.Default.TipoCierre3LlenL3, Properties.Settings.Default.Proveedores3LlenL3, Properties.Settings.Default.HoraVerificacion3LlenL3, Properties.Settings.Default.SensorTapon3LlenL3, Properties.Settings.Default.NivelVolumen3LlenL3);
                        if (MaquinaLinea.numlin == 5) CompletarRegistros(3, Properties.Settings.Default.TipoCierre3LlenL5, Properties.Settings.Default.Proveedores3LlenL5, Properties.Settings.Default.HoraVerificacion3LlenL5, Properties.Settings.Default.SensorTapon3LlenL5, Properties.Settings.Default.NivelVolumen3LlenL5);
                    }
                    if (HoraVerificacion2TB.Text == "" && HoraVerificacion1TB.Text != "")
                    {
                        EstablecerVariables(MaquinaLinea.numlin, 2, TipoCierreTB.Text, ProveedorTB.Text, HoraVerificacionTB.Text, SensorTapon, NivelVolumen);
                        if (MaquinaLinea.numlin == 2) CompletarRegistros(2, Properties.Settings.Default.TipoCierre2LlenL2, Properties.Settings.Default.Proveedores2LlenL2, Properties.Settings.Default.HoraVerificacion2LlenL2, Properties.Settings.Default.SensorTapon2LlenL2, Properties.Settings.Default.NivelVolumen2LlenL2);
                        if (MaquinaLinea.numlin == 3) CompletarRegistros(2, Properties.Settings.Default.TipoCierre2LlenL3, Properties.Settings.Default.Proveedores2LlenL3, Properties.Settings.Default.HoraVerificacion2LlenL3, Properties.Settings.Default.SensorTapon2LlenL3, Properties.Settings.Default.NivelVolumen2LlenL3);
                        if (MaquinaLinea.numlin == 5) CompletarRegistros(2, Properties.Settings.Default.TipoCierre2LlenL5, Properties.Settings.Default.Proveedores2LlenL5, Properties.Settings.Default.HoraVerificacion2LlenL5, Properties.Settings.Default.SensorTapon2LlenL5, Properties.Settings.Default.NivelVolumen2LlenL5);
                    }
                    if (HoraVerificacion1TB.Text == "")
                    {
                        EstablecerVariables(MaquinaLinea.numlin, 1, TipoCierreTB.Text, ProveedorTB.Text, HoraVerificacionTB.Text, SensorTapon, NivelVolumen);
                        if (MaquinaLinea.numlin == 2) CompletarRegistros(1, Properties.Settings.Default.TipoCierre1LlenL2, Properties.Settings.Default.Proveedores1LlenL2, Properties.Settings.Default.HoraVerificacion1LlenL2, Properties.Settings.Default.SensorTapon1LlenL2, Properties.Settings.Default.NivelVolumen1LlenL2);
                        if (MaquinaLinea.numlin == 3) CompletarRegistros(1, Properties.Settings.Default.TipoCierre1LlenL3, Properties.Settings.Default.Proveedores1LlenL3, Properties.Settings.Default.HoraVerificacion1LlenL3, Properties.Settings.Default.SensorTapon1LlenL3, Properties.Settings.Default.NivelVolumen1LlenL3);
                        if (MaquinaLinea.numlin == 5) CompletarRegistros(1, Properties.Settings.Default.TipoCierre1LlenL5, Properties.Settings.Default.Proveedores1LlenL5, Properties.Settings.Default.HoraVerificacion1LlenL5, Properties.Settings.Default.SensorTapon1LlenL5, Properties.Settings.Default.NivelVolumen1LlenL5);
                    }
                    //Reestablecemos las variables
                    HoraVerificacionTB.Text = "";
                    SensorTapon = "";
                    NivelVolumen = "";
                    ComentariosTB.Text = "";
                    SensorTapon_OK_B.BackColor = Color.FromArgb(27, 33, 41);
                    SensorTapon_NOOK_B.BackColor = Color.FromArgb(27, 33, 41);
                    NivelVolumen_OK_B.BackColor = Color.FromArgb(27, 33, 41);
                    NivelVolumen_NOOK_B.BackColor = Color.FromArgb(27, 33, 41);

                }
            }
            else
            {
                MessageBox.Show("FALTAN CAMPOS POR RELLENAR! -> NO SE HA GUARDADO LA INFORMACIÓN");
            }
        }
    }
}

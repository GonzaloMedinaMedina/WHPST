using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using WHPS.Despaletizador;
using WHPS.Encajonadora;
using WHPS.Etiquetadora;
using WHPS.Llenadora;
using WHPS.Model;
using WHPS.ProgramMenus;
using WHPS.Utiles;
using WHPS.Produccion;

namespace WHPS
{
    public partial class WHPST_SELECTMAQ : Form
    {
        string ID_Lanz = "";
        MainDespaletizador Desp;
        MainLlenadora Llen;
        MainEtiquetadora Etiq;
        MainEncajonadora Enc;
        public WHPST_SELECTMAQ()
        {
            InitializeComponent();
        }



        //Cargamos la información del personal de línea
        private void Carga_Personal()
        {
            Utilidades.ShiftCheck();
            //Generamos la lista de valores a filtrar y traer desde la lista de materiales AND, 'Columna', LIKE, 'Codigo Material'
            List<string[]> listavalores = new List<string[]>();

            //Rellenamos el turno - Identificando el turno
            string Turno = "";
            int diaC = Convert.ToInt16(DateTime.Now.ToString("dd"));
            int hora = Convert.ToInt16(DateTime.Now.ToString("HH"));

            if (hora >= 7 && hora < 15)
            {
                Turno = "Mañana";
            }
            else
            {
                if (hora >= 15 && hora < 23)
                {
                    Turno = "Tarde";
                }
                else { Turno = "Noche"; }
            }
            //###### CHEQUEAMOS SI ES NECESARIO ACTUALIZAR EL TURNO ###################
            if ((Turno != Properties.Settings.Default.turno) || (diaC != Properties.Settings.Default.diaT))
            {
                if ((numlinTB.Text == "2") && (MaquinaLinea.checkL2 == true))
                {
                    Properties.Settings.Default.checkL2 = false;
                    Properties.Settings.Default.checkL3 = false;
                    Properties.Settings.Default.checkL5 = false;
                    Properties.Settings.Default.Save();
                    MaquinaLinea.RetornoInicio = "CambioTurno";
                }
                if ((numlinTB.Text == "3") && (MaquinaLinea.checkL3 == true))
                {
                    Properties.Settings.Default.checkL2 = false;
                    Properties.Settings.Default.checkL3 = false;
                    Properties.Settings.Default.checkL5 = false;
                    Properties.Settings.Default.Save();
                    MaquinaLinea.RetornoInicio = "CambioTurno";
                }
                if ((numlinTB.Text == "5") && (MaquinaLinea.checkL5 == true))
                {
                    Properties.Settings.Default.checkL2 = false;
                    Properties.Settings.Default.checkL3 = false;
                    Properties.Settings.Default.checkL5 = false;
                    MaquinaLinea.RetornoInicio = "CambioTurno";
                }
            }



            string result;
                DataSet excelDataSet = new DataSet();
                excelDataSet = ExcelUtiles.LeerFicheroExcel("Datos_Lineas", "L" + numlinTB.Text, (MaquinaLinea.turno).Split(';'), listavalores, out result);
                //MessageBox.Show(result);
                string Texto = "";
                    if (excelDataSet.Tables[0].Rows.Count > 0)
                {                    
                    Texto = Convert.ToString(excelDataSet.Tables[0].Rows[0][MaquinaLinea.turno]);
                    respTB.Text = Texto;
                    MaquinaLinea.Responsable = Texto;
                    Texto = Convert.ToString(excelDataSet.Tables[0].Rows[1][MaquinaLinea.turno]);
                    DespTB.Text = Texto;
                    MaquinaLinea.MDespaletizador = Texto;
                    Texto = Convert.ToString(excelDataSet.Tables[0].Rows[2][MaquinaLinea.turno]);
                    LlenTB.Text = Texto;
                    MaquinaLinea.MLlenadora = Texto;
                    Texto = Convert.ToString(excelDataSet.Tables[0].Rows[3][MaquinaLinea.turno]);
                    EtiqTB.Text = Texto;
                    MaquinaLinea.MEtiquetadora = Texto;
                    Texto = Convert.ToString(excelDataSet.Tables[0].Rows[4][MaquinaLinea.turno]);
                    EncTB.Text = Texto;
                    MaquinaLinea.MEncajonadora = Texto;
                    Texto = Convert.ToString(excelDataSet.Tables[0].Rows[5][MaquinaLinea.turno]);
                    ContTB.Text = Texto;
                    MaquinaLinea.ControlCal = Texto;
                }
                else
                {
                    MessageBox.Show("Error en la carga del fichero");
                }
        }

        //Cargamos la información en pantalla al abrir esta
        private void WHPST_SELECTMAQ_Load(object sender, EventArgs e)
        {
            //Puesto que el timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));

            numlinTB.Text = MaquinaLinea.numlin.ToString();
            if (Utilidades.ObtenerTurnoActual() != MaquinaLinea.turno)
            {
                MaquinaLinea.turno = Utilidades.ObtenerTurnoActual();
                Carga_Personal();
            }
            else Carga_Personal();
            turnoTB.Text = MaquinaLinea.turno;
        }

        //Timer que muestra un reloj en pantalla y cada segundo comprueba si el turno ha cambiado
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (Utilidades.ObtenerTurnoActual()!= MaquinaLinea.turno)
            {

                MaquinaLinea.turno = Utilidades.ObtenerTurnoActual();
                Carga_Personal();
            }
            
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
        }
        private void EditarB_Click(object sender, EventArgs e)
        {
           // MaquinaLinea.AbrirCambioTurno = true;
            //Utilidades.AbrirFormHijo(WHPST_Cambio_Turno(), PanelInicio);
            if (MaquinaLinea.numlin == 2) MaquinaLinea.checkL2 = false;
            if (MaquinaLinea.numlin == 3) MaquinaLinea.checkL3 = false;
            if (MaquinaLinea.numlin == 5) MaquinaLinea.checkL5 = false;
            EditarB.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Update();
            this.ParentForm.Update();
            WHPST_INICIO f = this.ParentForm as WHPST_INICIO;
            f.AbrirFormHijo(new WHPST_Cambio_Turno());
        }
        //Funcion que muetra el siguiente form al guardar
        private void AbrirWHPST_CambioTurno(object WHPST_Cambio_Turno)
        {
            if (this.PanelSelectMaquina.Controls.Count > 0)
            {
                this.PanelSelectMaquina.Controls.RemoveAt(0);
            }
            Form SM = WHPST_Cambio_Turno as Form;
            SM.TopLevel = false;
            SM.Dock = DockStyle.Fill;
            this.PanelSelectMaquina.Controls.Add(SM);
            this.PanelSelectMaquina.Tag = SM;
            SM.Show();
        }
        //Abrimos el form del menu de las máquinas
        private void BDesp_Click(object sender, EventArgs e)
        {
            MaquinaLinea.SELECTMAQ = true;
            //###### Precargamos la variable del fichero a grabar ##############
            MaquinaLinea.FileDespaletizador = "Desp_L" + MaquinaLinea.numlin.ToString();

            //MainDespaletizador Form = new MainDespaletizador();
            //Hide();
            //Form.Show();
            if (Desp == null)
            {
                Desp = new MainDespaletizador();   //Create form if not created
                Desp.FormClosed += Desp_FormClosed;  //Add eventhandler to cleanup after form closes
            }

            Desp.Show(this);  //Show Form assigning this form as the forms owner
        }

        void Desp_FormClosed(object sender, FormClosedEventArgs e)
        {
            Desp = null;  //If form is closed make sure reference is set to null
            Show();
        }
        private void BLlen_Click(object sender, EventArgs e)
        {
            MaquinaLinea.SELECTMAQ = true;
            //###### Precargamos la variable del fichero a grabar ##############
            MaquinaLinea.FileLlenadora = "Llen_L" + MaquinaLinea.numlin.ToString();
            //MainLlenadora Form = new MainLlenadora();
            //Hide();
            //Form.Show();
            if (Llen == null)
            {
                Llen = new MainLlenadora();   //Create form if not created
                Llen.FormClosed += Llen_FormClosed;  //Add eventhandler to cleanup after form closes
            }

            Llen.Show(this);  //Show Form assigning this form as the forms owner

        }
        void Llen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Llen = null;  //If form is closed make sure reference is set to null
            Show();
        }
        private void BEtiq_Click(object sender, EventArgs e)
        {
            MaquinaLinea.SELECTMAQ = true;
            //###### Precargamos la variable del fichero a grabar ##############
            MaquinaLinea.FileEtiquetadora = "Etiq_L" + MaquinaLinea.numlin.ToString();
            //MainEtiquetadora Form = new MainEtiquetadora();
            //Hide();
            //Form.Show();
            if (Etiq == null)
            {
                Etiq = new MainEtiquetadora();   //Create form if not created
                Etiq.FormClosed += Etiq_FormClosed;  //Add eventhandler to cleanup after form closes
            }

            Etiq.Show(this);  //Show Form assigning this form as the forms owner

        }
        void Etiq_FormClosed(object sender, FormClosedEventArgs e)
        {
            Etiq = null;  //If form is closed make sure reference is set to null
            Show();
        }

        private void BEnc_Click(object sender, EventArgs e)
        {
            MaquinaLinea.SELECTMAQ = true;
            //###### Precargamos la variable del fichero a grabar ##############
            MaquinaLinea.FileEncajonadora = "Enc_L" + MaquinaLinea.numlin.ToString();
            //MainEncajonadora Form = new MainEncajonadora();
            //Hide();
            //Form.Show();
            if (Enc == null)
            {
                Enc = new MainEncajonadora();   //Create form if not created
                Enc.FormClosed += Enc_FormClosed;  //Add eventhandler to cleanup after form closes
            }

            Enc.Show(this);  //Show Form assigning this form as the forms owner

        }
        void Enc_FormClosed(object sender, FormClosedEventArgs e)
        {
            Enc = null;  //If form is closed make sure reference is set to null
            Show();
        }

        private void CargarDespB_Click(object sender, EventArgs e)
        {
            MostrarIndicadores("Desp");
        }
        private void CargarLlenB_Click(object sender, EventArgs e)
        {
            MostrarIndicadores("Llen");
        }
        private void CargarEtiqB_Click(object sender, EventArgs e)
        {
            MostrarIndicadores("Etiq");
        }
        private void CargarEncB_Click(object sender, EventArgs e)
        {
            MostrarIndicadores("Enc");
        }
        public void MostrarIndicadores(string Maquina)
        {
            groupBox1.Show();
            switch (Maquina)
            {
                case "Desp":
                    DatosProduccion.Text = "DATOS DE PRODUCCIÓN (DESPALETIZADOR)";
                    PanelDesp.Visible = true;
                    if (MaquinaLinea.numlin == 2)
                    {
                        ReferenciaTB.Text = Properties.Settings.Default.DPCodigoProdDespL2;
                        OrdenTB.Text = Properties.Settings.Default.DPOrdenDespL2;
                        ClienteTB.Text = Properties.Settings.Default.DPClienteDespL2;
                        ProductoTB.Text = Properties.Settings.Default.DPProductoDespL2;
                        NumBotTB.Text = Properties.Settings.Default.DPNumBotDespL2.ToString();
                        NumBotTotalTB.Text = Properties.Settings.Default.DPNumBotTotalDespL2;
                    }
                    if (MaquinaLinea.numlin == 3)
                    {
                        ReferenciaTB.Text = Properties.Settings.Default.DPCodigoProdDespL3;
                        OrdenTB.Text = Properties.Settings.Default.DPOrdenDespL3;
                        ClienteTB.Text = Properties.Settings.Default.DPClienteDespL3;
                        ProductoTB.Text = Properties.Settings.Default.DPProductoDespL3;
                        NumBotTB.Text = Properties.Settings.Default.DPNumBotDespL3.ToString();
                        NumBotTotalTB.Text = Properties.Settings.Default.DPNumBotTotalDespL3;
                    }
                    if (MaquinaLinea.numlin == 5)
                    {
                        ReferenciaTB.Text = Properties.Settings.Default.DPCodigoProdDespL5;
                        OrdenTB.Text = Properties.Settings.Default.DPOrdenDespL5;
                        ClienteTB.Text = Properties.Settings.Default.DPClienteDespL5;
                        ProductoTB.Text = Properties.Settings.Default.DPProductoDespL5;
                        NumBotTB.Text = Properties.Settings.Default.DPNumBotDespL5.ToString();
                        NumBotTotalTB.Text = Properties.Settings.Default.DPNumBotTotalDespL5;
                    }
                    break;
                case "Llen":
                    DatosProduccion.Text = "DATOS DE PRODUCCIÓN (LLENADORA)";
                    PanelDesp.Visible = false;
                    if (MaquinaLinea.numlin == 2)
                    {
                        ReferenciaTB.Text = Properties.Settings.Default.DPCodigoProdLlenL2;
                        OrdenTB.Text = Properties.Settings.Default.DPOrdenLlenL2;
                        ClienteTB.Text = Properties.Settings.Default.DPClienteLlenL2;
                        ProductoTB.Text = Properties.Settings.Default.DPProductoLlenL2;
                        InicioProduccionTB.Text = Properties.Settings.Default.DPHInicioLlenL2;
                    }
                    if (MaquinaLinea.numlin == 3)
                    {
                        ReferenciaTB.Text = Properties.Settings.Default.DPCodigoProdLlenL3;
                        OrdenTB.Text = Properties.Settings.Default.DPOrdenLlenL3;
                        ClienteTB.Text = Properties.Settings.Default.DPClienteLlenL3;
                        ProductoTB.Text = Properties.Settings.Default.DPProductoLlenL3;
                        InicioProduccionTB.Text = Properties.Settings.Default.DPHInicioLlenL3;
                    }
                    if (MaquinaLinea.numlin == 5)
                    {
                        ReferenciaTB.Text = Properties.Settings.Default.DPCodigoProdLlenL5;
                        OrdenTB.Text = Properties.Settings.Default.DPOrdenLlenL5;
                        ClienteTB.Text = Properties.Settings.Default.DPClienteLlenL5;
                        ProductoTB.Text = Properties.Settings.Default.DPProductoLlenL5;
                        InicioProduccionTB.Text = Properties.Settings.Default.DPHInicioLlenL5;
                    }
                    break;

                case "Etiq":
                    DatosProduccion.Text = "DATOS DE PRODUCCIÓN (ETIQUETADORA)";
                    PanelDesp.Visible = false;
                    if (MaquinaLinea.numlin == 2)
                    {
                        ReferenciaTB.Text = Properties.Settings.Default.DPCodigoProdEtiqL2;
                        OrdenTB.Text = Properties.Settings.Default.DPOrdenEtiqL2;
                        ClienteTB.Text = Properties.Settings.Default.DPClienteEtiqL2;
                        ProductoTB.Text = Properties.Settings.Default.DPProductoEtiqL2;
                        InicioProduccionTB.Text = Properties.Settings.Default.DPHInicioEtiqL2;
                    }
                    if (MaquinaLinea.numlin == 3)
                    {
                        ReferenciaTB.Text = Properties.Settings.Default.DPCodigoProdEtiqL3;
                        OrdenTB.Text = Properties.Settings.Default.DPOrdenEtiqL3;
                        ClienteTB.Text = Properties.Settings.Default.DPClienteEtiqL3;
                        ProductoTB.Text = Properties.Settings.Default.DPProductoEtiqL3;
                        InicioProduccionTB.Text = Properties.Settings.Default.DPHInicioEtiqL3;
                    }
                    if (MaquinaLinea.numlin == 5)
                    {
                        ReferenciaTB.Text = Properties.Settings.Default.DPCodigoProdEtiqL5;
                        OrdenTB.Text = Properties.Settings.Default.DPOrdenEtiqL5;
                        ClienteTB.Text = Properties.Settings.Default.DPClienteEtiqL5;
                        ProductoTB.Text = Properties.Settings.Default.DPProductoEtiqL5;
                        InicioProduccionTB.Text = Properties.Settings.Default.DPHInicioEtiqL5;
                    }
                    break;
                case "Enc":
                    DatosProduccion.Text = "DATOS DE PRODUCCIÓN (ENCAJONADORA)";
                    PanelDesp.Visible = false;
                    if (MaquinaLinea.numlin == 2)
                    {
                        ReferenciaTB.Text = Properties.Settings.Default.DPCodigoProdEncL2;
                        OrdenTB.Text = Properties.Settings.Default.DPOrdenEncL2;
                        ClienteTB.Text = Properties.Settings.Default.DPClienteEncL2;
                        ProductoTB.Text = Properties.Settings.Default.DPProductoEncL2;
                        InicioProduccionTB.Text = Properties.Settings.Default.DPHInicioEncL2;
                    }
                    if (MaquinaLinea.numlin == 3)
                    {
                        ReferenciaTB.Text = Properties.Settings.Default.DPCodigoProdEncL3;
                        OrdenTB.Text = Properties.Settings.Default.DPOrdenEncL3;
                        ClienteTB.Text = Properties.Settings.Default.DPClienteEncL3;
                        ProductoTB.Text = Properties.Settings.Default.DPProductoEncL3;
                        InicioProduccionTB.Text = Properties.Settings.Default.DPHInicioEncL3;
                    }
                    if (MaquinaLinea.numlin == 5)
                    {
                        ReferenciaTB.Text = Properties.Settings.Default.DPCodigoProdEncL5;
                        OrdenTB.Text = Properties.Settings.Default.DPOrdenEncL5;
                        ClienteTB.Text = Properties.Settings.Default.DPClienteEncL5;
                        ProductoTB.Text = Properties.Settings.Default.DPProductoEncL5;
                        InicioProduccionTB.Text = Properties.Settings.Default.DPHInicioEncL5;
                    }
                    break;
            }
        }
        private void RefrescarB_Click(object sender, EventArgs e)
        {
            //Realiza la busqueda para detectar si hay algun producto iniciado
            List<string[]> valoresAFiltrar = new List<string[]>();
            string[] filterval = new string[4];
            filterval[0] = "AND";
            filterval[1] = "ESTADO";
            filterval[2] = "LIKE";
            filterval[3] = " \"" + "Iniciado" + "\"";
            valoresAFiltrar.Add(filterval);

            DataSet excelDataSet = new DataSet();
            string result;
            //List<string[]> valoresAFiltrar = dgvSelectFiltro.DataSource;
            excelDataSet = ExcelUtiles.LeerFicheroExcel("DB_L"+MaquinaLinea.numlin, "Linea " + MaquinaLinea.numlin, "ID_Lanz;ORDEN".Split(';'), valoresAFiltrar, out result);

            //MessageBox.Show(result);

            //Una vez realizada la busqueda si esta es correcta se modifican los parámetros de la tabla para se adecuen a las necesidades del usuario
                if (excelDataSet.Tables[0].Rows.Count > 0)
                {
                    ID_Lanz = Convert.ToString(excelDataSet.Tables[0].Rows[0]["ID_Lanz"]);
                    OrdenPedTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["ORDEN"]);
                    EstadoTB.Text = "Iniciado";
                    EstadoTB.BackColor = System.Drawing.Color.Orange;
                    AvisoLB.Text = "";
                    //DescripcionTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["PRODUCTO"]);
                    ObtenerNumeroBotellas(ID_Lanz, "Llen_L", "NBotellasTotal");
                    ObtenerNumeroBotellas(ID_Lanz, "Etiq_L", "NBotellas");
                    ObtenerNumeroBotellas(ID_Lanz, "Enc_L", "NCajas;Producto");
                }
            
            else
            {
                //No hay ningun iniciado
                AvisoLB.Text = "No hay ningun pedido iniciado.";
            }
        }
        private void ObtenerNumeroBotellas(string Lanzamiento, string Maquina, string Columna)
        {
            string FileMaquina = Maquina + MaquinaLinea.numlin;

            //MessageBox.Show(Maquina);
            //Realiza la busqueda para detectar si hay algun producto iniciado
            List<string[]> valoresAFiltrar = new List<string[]>();
            string[] filterval = new string[4];
            filterval[0] = "AND";
            filterval[1] = "ID_Lanz";
            filterval[2] = "LIKE";
            filterval[3] = " \"" + ID_Lanz + "\"";
            valoresAFiltrar.Add(filterval);

            DataSet excelDataSet = new DataSet();
            string result;
            //List<string[]> valoresAFiltrar = dgvSelectFiltro.DataSource;
            excelDataSet = ExcelUtiles.LeerFicheroExcel(FileMaquina, "Registro", Columna.Split(';'), valoresAFiltrar, out result);

            //MessageBox.Show(result);

            //Una vez realizada la busqueda si esta es correcta se modifican los parámetros de la tabla para se adecuen a las necesidades del usuario
            if (excelDataSet.Tables[0].Rows.Count > 0)
            {
                string Nbotellas = "";
                switch (Maquina)
                {
                    case "Llen_L":
                        Nbotellas = Convert.ToString(excelDataSet.Tables[0].Rows[0][Columna]);
                        NBotLlenTB.Text = Nbotellas;

                        break;
                    case "Etiq_L":
                        Nbotellas = Convert.ToString(excelDataSet.Tables[0].Rows[0][Columna]);
                        NBotEtiqTB.Text = Nbotellas;
                        break;
                    case "Enc_L":
                        Nbotellas = Convert.ToString(excelDataSet.Tables[0].Rows[0]["NCajas"]);
                        NBotEncTB.Text = Nbotellas;
                        DescripcionTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Producto"]);
                        break;
                }
            }
            else
            {
                //No hay ningun iniciado
                switch (Maquina)
                {
                    case "Llen_L":
                        NBotLlenTB.Text = "No registrado";
                        break;
                    case "Etiq_L":
                        NBotEtiqTB.Text = "No registrado";
                        break;
                    case "Enc_L":
                        NBotEncTB.Text = "No registrado";
                        break;
                }
                DescripcionTB.Text = "No registrado";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MaquinaLinea.SELECTMAQ = true;
            MessageBox.Show(Properties.Settings.Default.AvisoMantenimiento);
        }

        private void AvisoLB_Click(object sender, EventArgs e)
        {

        }
    }
}

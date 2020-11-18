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
using System.Diagnostics;

namespace WHPS
{
    public partial class WHPST_SELECTMAQ : Form
    {
        //Variable para obtener el numero de botellas y cajas
        string ID_Lanz = "";
        //Variables que abren los diferentes forms.


        public static MainDespaletizador Desp;
        public static MainLlenadora Llen;
        public static MainEtiquetadora Etiq;
        public static MainEncajonadora Enc;
        public static WHPST_INICIO parentinicio;


        public WHPST_SELECTMAQ(WHPST_INICIO p)
        {
            InitializeComponent();
            parentinicio = p;
           
        }

        //Cargamos la información en pantalla al abrir esta
        public void WHPST_SELECTMAQ_Load(object sender, EventArgs e)
        {
            //Puesto que el timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Indicamos estamos en el formm SELECTMAQ, para despues anular la ventana.
            MaquinaLinea.SELECTMAQ = true;

            CompletarInformacionLinea();

        }

        //Timer que muestra un reloj en pantalla y cada segundo comprueba si el turno ha cambiado
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
        }


        //#################       BOTONES FORM       #################
        private void BDesp_Click(object sender, EventArgs e)
        {
            //Indicamos que vamos al main desde un form hijo.
            MaquinaLinea.SELECTMAQ = true;
            //Precargamos la variable del fichero a grabar.
            MaquinaLinea.FileDespaletizador = "Desp_L" + MaquinaLinea.numlin.ToString();

           Desp = Utilidades.AbrirForm(Desp, parentinicio, typeof(MainDespaletizador)) as MainDespaletizador;
        }

        private void BLlen_Click(object sender, EventArgs e)
        {
            //Indicamos que vamos al main desde un form hijo.
            MaquinaLinea.SELECTMAQ = true;

            //Precargamos la variable del fichero a grabar.
            MaquinaLinea.FileLlenadora = "Llen_L" + MaquinaLinea.numlin.ToString();

            Llen = Utilidades.AbrirForm(Llen, parentinicio, typeof(MainLlenadora)) as MainLlenadora;
        /*    Llen = new MainLlenadora(this);
            Llen.Show();
            parent.Hide();*/
        }
        private void BEtiq_Click(object sender, EventArgs e)
        {

            MaquinaLinea.SELECTMAQ = true;
            //###### Precargamos la variable del fichero a grabar ##############
            MaquinaLinea.FileEtiquetadora = "Etiq_L" + MaquinaLinea.numlin.ToString();
            Etiq = Utilidades.AbrirForm(Etiq, parentinicio,typeof(MainEtiquetadora)) as MainEtiquetadora;

        }

        internal void AvisaCambioTurno()
        {
            if (MaquinaLinea.numlin == 2)
            {
                if (!Properties.Settings.Default.chEtiqL2 && Etiq!=null) Etiq.ActivarTimer();
                if (!Properties.Settings.Default.chEncL2 && Enc!=null) Enc.ActivarTimer();
                if (!Properties.Settings.Default.chLlenL2 && Llen!=null) Llen.ActivarTimer();
                if (!Properties.Settings.Default.chDesL2 && Desp != null) Desp.ActivarTimer();
            }
            if (MaquinaLinea.numlin == 3)
            {
                if (!Properties.Settings.Default.chEtiqL3 && Etiq != null) Etiq.ActivarTimer();
                if (!Properties.Settings.Default.chEncL3 && Enc != null) Enc.ActivarTimer();
                if (!Properties.Settings.Default.chLlenL3 && Llen != null) Llen.ActivarTimer();
                if (!Properties.Settings.Default.chDesL3 && Desp!=null) Desp.ActivarTimer();
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (!Properties.Settings.Default.chEncL5 && Enc != null) Enc.ActivarTimer();
                if (!Properties.Settings.Default.chEtiqL5 && Etiq != null) Etiq.ActivarTimer();
                if (!Properties.Settings.Default.chLlenL5 && Llen != null) Llen.ActivarTimer();
                if (!Properties.Settings.Default.chDesL5 && Desp != null) Desp.ActivarTimer();
            }
        }

        private void BEnc_Click(object sender, EventArgs e)
        {
            MaquinaLinea.SELECTMAQ = true;
            //###### Precargamos la variable del fichero a grabar ##############
            MaquinaLinea.FileEncajonadora = "Enc_L" + MaquinaLinea.numlin.ToString();
            Enc = Utilidades.AbrirForm(Enc, parentinicio, typeof(MainEncajonadora)) as MainEncajonadora;

            /*Enc = new MainEncajonadora(parentinicio);
            Enc.Show();
            parentinicio.Hide();*/


        }
        private void GeneralesLineaB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.SELECTMAQ = true;
            MessageBox.Show(Properties.Settings.Default.AvisoMantenimiento);
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

        private void EditarB_Click(object sender, EventArgs e)
        {
            // MaquinaLinea.AbrirCambioTurno = true;
            //Utilidades.AbrirFormHijo(WHPST_Cambio_Turno(), PanelInicio);
            //if (MaquinaLinea.numlin == 2) MaquinaLinea.checkL2 = false;
            //if (MaquinaLinea.numlin == 3) MaquinaLinea.checkL3 = false;
            //if (MaquinaLinea.numlin == 5) MaquinaLinea.checkL5 = false;

            Hide();
            Dispose();

            MaquinaLinea.VolverInicioA = RetornoInicio.CambioTurno;
            Utilidades.AbrirForm(parentinicio,parentinicio, typeof(WHPST_INICIO));
        }


       
        //#####################       FUNCIONES       #####################
        /// <summary>
        /// Función que completa la información de línea.
        /// </summary>
        private void CompletarInformacionLinea()
        {
            //Numero de línea
            numlinTB.Text = MaquinaLinea.numlin.ToString();

            //Truno, se obtiene el turno con la hora actual.
            turnoTB.Text = MaquinaLinea.turno;

            //Obtenemos el personal que esta registrado en la correspondiente línea.
            CambioTurno.Obtener_Personal_Datos_Lineas();

            //Completamos el personal
            respTB.Text = MaquinaLinea.Responsable;
            DespTB.Text = MaquinaLinea.MDespaletizador;
            LlenTB.Text = MaquinaLinea.MLlenadora;
            EtiqTB.Text = MaquinaLinea.MEtiquetadora;
            EncTB.Text = MaquinaLinea.MEncajonadora;
            ContTB.Text = MaquinaLinea.ControlCal;
        }
        /// <summary>
        /// Función que completa los indicadores de línea.
        /// </summary>
        private void CompletarIndicadoresLinea()
        {
            try
            {
               DataSet excelDataSet = FuncionesExcel.LeerExcelDB("DB_L" + MaquinaLinea.numlin, "Linea " + MaquinaLinea.numlin, "ID_Lanz;ORDEN","ESTADO","Iniciado");
                if (excelDataSet.Tables[0].Rows.Count > 0)
                {
                    //Obtengo el ID_Lanzamineto del pedido Iniciado
                    ID_Lanz = Convert.ToString(excelDataSet.Tables[0].Rows[0]["ID_Lanz"]);

                    //Completamos los campos.
                    OrdenPedTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["ORDEN"]);
                    EstadoTB.Text = "Iniciado";
                    EstadoTB.BackColor = System.Drawing.Color.Orange;

                    AvisoLB.Text = "";

                    //Obtenemos el número de botellas y cajas de cada máquina.
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
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
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

        private void RecargarDatosB_Click(object sender, EventArgs e)
        {
            CompletarIndicadoresLinea();
        }

        public void SetDesp(MainDespaletizador d)
        {
            Desp = d;
        }
        public MainDespaletizador GetDesp()
        {
            return Desp;
        }
        public void SetDesp(MainLlenadora d)
        {
            Llen = d;
        }
        public MainLlenadora GetLlen()
        {
            return Llen;
        }

        public void SetEnc(MainEncajonadora d)
        {
            Enc = d;
        }
        public MainEncajonadora GetEnc()
        {
            return Enc;
        }

        public void SetEtiq(MainEtiquetadora d)
        {
            Etiq = d;
        }
        public MainEtiquetadora GetEtiq()
        {
            return Etiq;
        }



    }
}

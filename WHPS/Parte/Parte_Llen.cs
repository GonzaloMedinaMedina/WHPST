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
using WHPS.Utiles;

namespace WHPS.Parte
{
    public partial class Parte_Llen : Form
    {
        public string lineamarcada = "";
        public string[] Array1 = new string[17];
        public string[] Array2 = new string[17];
        public string[] DatosLlen = new string[14];
        public string[] Comentarios = new string[2];
        public string[] DatosLlenParo = new string[4];
        public string[] Rotura = new string[6];
        public string[] ControlPresion = new string[3];
        public string[] Control30MIN = new string[5];
        public string[] ControlVerificacion = new string[6];
        public string[] Torquimetro = new string[16];
        public string[] ControlVolumen = new string[7];
        public string[] ControlTemperatura = new string[6];
        public int j=0;
        public Parte_Llen()
        {
            InitializeComponent();
        }

        public Parte_Llen(bool guardar, string ln)
        {
            InitializeComponent();
            this.lineamarcada = ln;
            Parte_Llen_Load(this, new EventArgs());
            this.Close();
        }

        private void Parte_Llen_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewInicio.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[0, 0]];
                dataGridViewPresion.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[1, 0]];
                dataGridViewTempLLenadora.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[2, 0]];
                dataGridViewTempCaldera.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[3, 0]];
                dataGridViewRegistro.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[4, 0]];
                dataGridViewParo.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[5, 0]];
                dataGridViewControl30min.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[6, 0]];
                dataGridViewVerificacion.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[7, 0]];
                dataGridViewVolumen.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[8, 0]];
                dataGridViewTorquimetro.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[9, 0]];
                dataGridViewRoturas.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[10, 0]];
                dataGridViewComentarios.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[11, 0]];
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }
           /* //dataGridViewParo.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridViewComentarios.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTempLLenadora.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTempCaldera.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewVerificacion.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;*/
            if (MaquinaLinea.Parte_Guardar == true)
            {
                CompletarParteLlenadora();
                MaquinaLinea.Parte_Llen = true;
            }
        }


        public void CompletarParteLlenadora()
        {
            //########### DATOS INICIALES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "DATOS PRINCIPALES" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### OBTENER DATOS INICIALES ##############
            if (j < (dataGridViewInicio.RowCount - 1)) {

                //string prueba = dataGridViewInicio.Rows[0].Cells[i].Value.ToString();
                //MessageBox.Show(prueba);
                //if (dataGridViewInicio.Rows[0].Cells[i].Value.ToString() != null) Array1[i] = dataGridViewInicio.Rows[0].Cells[i].Value.ToString();
                //if (dataGridViewInicio.Rows[1].Cells[i].Value.ToString() != null) Array2[i] = dataGridViewInicio.Rows[1].Cells[i].Value.ToString();

                bool encontrado_ini = false, encontrado_fin = false;
                for (int f = 0; f < dataGridViewInicio.Rows.Count - 1 && (!encontrado_ini || !encontrado_fin); f++)
                {
                    //string prueba = dataGridViewInicio.Rows[0].Cells[i].Value.ToString();
                    //MessageBox.Show(prueba);
                    if (dataGridViewInicio.Rows[f].Cells[17].Value.ToString() == "Inicio" && !encontrado_ini)
                    {
                        encontrado_ini = true;
                        for (int i = 0; i < 17; i++)
                        {
                            if (dataGridViewInicio.Rows[f].Cells[i].Value.ToString() == null) encontrado_ini = false;
                            Array1[i] = dataGridViewInicio.Rows[f].Cells[i].Value.ToString();
                        }

                    }
                    if (dataGridViewInicio.Rows[dataGridViewInicio.Rows.Count - 2 - f].Cells[17].Value.ToString() == "Fin" && !encontrado_fin)
                    {
                        encontrado_fin = true;
                        for (int i = 0; i < 17; i++)
                        {
                            if (dataGridViewInicio.Rows[dataGridViewInicio.Rows.Count - 2 - f].Cells[i].Value.ToString() == null) encontrado_fin = false;
                            Array2[i] = dataGridViewInicio.Rows[dataGridViewInicio.Rows.Count - 2 - f].Cells[i].Value.ToString();
                        }

                    }
                }
            }
        
            //########### DATOS INICIALES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Línea" });
                listavalores.Add(new string[2] { "B", "L" + this.lineamarcada });
                listavalores.Add(new string[2] { "C", "Turno" });
                listavalores.Add(new string[2] { "D", Array1[4] });
                listavalores.Add(new string[2] { "E", "Fecha" });
                listavalores.Add(new string[2] { "F", Array1[0] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### TRABAJADORES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Resp. Línea" });
                listavalores.Add(new string[2] { "B", Array1[2] });
                listavalores.Add(new string[2] { "E", "Resp. Máquina" });
                listavalores.Add(new string[2] { "F", Array1[3] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### ESTADO DEL EQUIPO INICIAL ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Rev.Hora Inicio" });
                listavalores.Add(new string[2] { "B", Array1[1] });
                listavalores.Add(new string[2] { "C", "Limpieza" });
                listavalores.Add(new string[2] { "D", Array1[5] });
                listavalores.Add(new string[2] { "E", "Protecciones" });
                listavalores.Add(new string[2] { "F", Array1[6] });
                listavalores.Add(new string[2] { "G", "Cuter" });
                listavalores.Add(new string[2] { "H", Array1[7] });
                listavalores.Add(new string[2] { "I", "Herramientas" });
                listavalores.Add(new string[2] { "J", Array1[8] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### ESTADO DEL EQUIPO INICIAL ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Caños" });
                listavalores.Add(new string[2] { "B", Array1[9] });
                listavalores.Add(new string[2] { "C", "N. Caños" });
                listavalores.Add(new string[2] { "D", Array1[10] });
                listavalores.Add(new string[2] { "E", "Sensor" });
                listavalores.Add(new string[2] { "F", Array1[11] });
                listavalores.Add(new string[2] { "G", "Presion" });
                listavalores.Add(new string[2] { "H", Array1[12] });
                listavalores.Add(new string[2] { "I", "Enj/Llen" });
                listavalores.Add(new string[2] { "J", Array1[13] });
                listavalores.Add(new string[2] { "K", "Taponadora" });
                listavalores.Add(new string[2] { "L", Array1[14] });
                listavalores.Add(new string[2] { "M", "Capsuladora" });
                listavalores.Add(new string[2] { "N", Array1[15] });
                listavalores.Add(new string[2] { "O", "Transportadora" });
                listavalores.Add(new string[2] { "P", Array1[16] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### ESTADO DEL EQUIPO FINAL ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Rev.Hora Fin" });
                listavalores.Add(new string[2] { "B", Array2[1] });
                listavalores.Add(new string[2] { "C", "Limpieza" });
                listavalores.Add(new string[2] { "D", Array2[5] });
                listavalores.Add(new string[2] { "E", "Protecciones" });
                listavalores.Add(new string[2] { "F", Array2[6] });
                listavalores.Add(new string[2] { "G", "Cuter" });
                listavalores.Add(new string[2] { "H", Array2[7] });
                listavalores.Add(new string[2] { "I", "Herramientas" });
                listavalores.Add(new string[2] { "J", Array2[8] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### ESTADO DEL EQUIPO FINAL ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Caños" });
                listavalores.Add(new string[2] { "B", Array2[9] });
                listavalores.Add(new string[2] { "C", "N. Caños" });
                listavalores.Add(new string[2] { "D", Array2[10] });
                listavalores.Add(new string[2] { "E", "Sensor" });
                listavalores.Add(new string[2] { "F", Array2[11] });
                listavalores.Add(new string[2] { "G", "Presion" });
                listavalores.Add(new string[2] { "H", Array2[12] });
                listavalores.Add(new string[2] { "I", "Enj/Llen" });
                listavalores.Add(new string[2] { "J", Array2[13] });
                listavalores.Add(new string[2] { "K", "Taponadora" });
                listavalores.Add(new string[2] { "L", Array2[14] });
                listavalores.Add(new string[2] { "M", "Capsuladora" });
                listavalores.Add(new string[2] { "N", Array2[15] });
                listavalores.Add(new string[2] { "O", "Transportadora" });
                listavalores.Add(new string[2] { "P", Array2[16] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### DATOS INICIALES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "REGISTRO DE PRODUCCIÓN" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### CABECERA DATOS LLENADORA ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Hora" });
                listavalores.Add(new string[2] { "B", "Orden" });
                listavalores.Add(new string[2] { "C", "Barra1" });
                listavalores.Add(new string[2] { "D", "Deposito1" });
                listavalores.Add(new string[2] { "E", "Frio1" });
                listavalores.Add(new string[2] { "F", "N. Botellas1" });
                listavalores.Add(new string[2] { "G", "Barra2" });
                listavalores.Add(new string[2] { "H", "Deposito2" });
                listavalores.Add(new string[2] { "I", "Frio2" });
                listavalores.Add(new string[2] { "J", "N. Botellas2" });
                listavalores.Add(new string[2] { "K", "N. BotellasTotal" });
                listavalores.Add(new string[2] { "L", "Referencia" });
                listavalores.Add(new string[2] { "M", "Código" });
                listavalores.Add(new string[2] { "N", "Capacidad" });
                listavalores.Add(new string[2] { "O", "Producto" });
                listavalores.Add(new string[2] { "P", "Graduacion" });
                listavalores.Add(new string[2] { "Q", "Inicio" });
                listavalores.Add(new string[2] { "R", "Fin" });
                listavalores.Add(new string[2] { "S", "Inicio Preparacion" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### OBTENER DATOS llENADORA ##############
            for (int j = 0; j < (dataGridViewRegistro.RowCount - 1); j++)
            {
                DatosLlen = new string[20];
                for (int i = 0; i < 19; i++)
                {
                    if (dataGridViewRegistro.Rows[j].Cells[i].Value.ToString() != null) DatosLlen[i] = dataGridViewRegistro.Rows[j].Cells[i].Value.ToString();
                }
                DatosLlenadora();
            }

            //########### INFORMACIÓN DE PARADA ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "INFORMACIÓN DE PARADA" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### CABECERA INFORMACIÓN DE PARADA ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Inicio Parada" });
                listavalores.Add(new string[2] { "B", "Fin Parada" });
                listavalores.Add(new string[2] { "C", "Motivo" });
                listavalores.Add(new string[2] { "D", "Tiempo Parada" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### OBTENER DATOS LLENADORA PARADA ##############
            for (int j = 0; j < (dataGridViewParo.RowCount - 1); j++)
            {
                DatosLlenParo = new string[4];

                for (int i = 0; i < 4; i++)
                {

                    //string prueba = dataGridViewParo.Rows[0].Cells[i].Value.ToString();
                    //MessageBox.Show(prueba);
                    DatosLlenParo[i] = dataGridViewParo.Rows[j].Cells[i].Value.ToString();
                }
                DatosLlenadoraParo();
            }
            //########### DATOS COMENTARIOS ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "COMENTARIOS" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### OBTENER DATOS COMENTARIOS ##############
            for (int j = 0; j < (dataGridViewComentarios.RowCount - 1); j++)
            {
                for (int i = 0; i < 2; i++)
                {
                    Comentarios[i] = dataGridViewComentarios.Rows[j].Cells[i].Value.ToString();
                }
                DatosComentarios(Convert.ToString(j));
            }
            //########### ROTURAS ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "ROTURA DE BOTELLAS" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### CABECERA DATOS ROTURAS ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Hora" });
                listavalores.Add(new string[2] { "B", "Rotura de Bot." });
                listavalores.Add(new string[2] { "C", "Número de Roturas" });
                listavalores.Add(new string[2] { "D", "Área" });
                listavalores.Add(new string[2] { "E", "Maquinista" });
                listavalores.Add(new string[2] { "F", "Confirmación" });
                listavalores.Add(new string[2] { "G", "Máquina" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            //########### OBTENER DATOS ROTURAS ##############
            for (int j = 0; j < (dataGridViewRoturas.RowCount - 1); j++)
            {
                Rotura = new string[7];
                for (int i = 0; i < 7; i++)
                {
                    Rotura[i] = dataGridViewRoturas.Rows[j].Cells[i].Value.ToString();
                }
                DatosRotura();
            }
            //########### COMIENZO REGISTRO DE LA HOJA 2 ##############

            //########### CONTROL DE PRESION ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "CONTROL DE PRESIÓN" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora2", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Hora" });
                listavalores.Add(new string[2] { "B", "Medida (Bar)" });
                listavalores.Add(new string[2] { "C", "Estado" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora2", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            for (int j = 0; j < (dataGridViewPresion.RowCount - 1); j++)
            {
                ControlPresion = new string[3];
                for (int i = 0; i < 3; i++)
                {
                    ControlPresion[i] = dataGridViewPresion.Rows[j].Cells[i].Value.ToString();
                }
                DatosControlPresion();
            }
            //########### CONTROL CADA 30 MIN ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "CONTROL CADA 30MIN" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora2", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Hora" });
                listavalores.Add(new string[2] { "B", "Control Cierre" });
                listavalores.Add(new string[2] { "C", "Volumen" });
                listavalores.Add(new string[2] { "D", "Cuello/Boca" });
                listavalores.Add(new string[2] { "E", "Comentarios" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora2", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            for (int j = 0; j < (dataGridViewControl30min.RowCount - 1); j++)
            {
                Control30MIN = new string[5];
                for (int i = 0; i < 5; i++)
                {
                    Control30MIN[i] = dataGridViewControl30min.Rows[j].Cells[i].Value.ToString();
                }
                DatosControl30min();
            }
            //########### CONTROL DE VERIFICACION ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "VERIFICACIÓN EQUIPO CONTROL DE CIERRE Y NIVEL VOLUMEN" });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora2", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Hora" });
                listavalores.Add(new string[2] { "B", "Tipo Cierre" });
                listavalores.Add(new string[2] { "C", "Proveedor" });
                listavalores.Add(new string[2] { "D", "Sensor Superior" });
                listavalores.Add(new string[2] { "E", "Nivel Volumen" });
                listavalores.Add(new string[2] { "F", "Comentarios" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora2", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            for (int j = 0; j < (dataGridViewVerificacion.RowCount - 1); j++)
            {
                ControlVerificacion = new string[6];
                for (int i = 0; i < 6; i++)
                {
                    ControlVerificacion[i] = dataGridViewVerificacion.Rows[j].Cells[i].Value.ToString();
                }
                DatosControlVerificacion();
            }
            //########### TORQUIMETRO ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "MEDICIÓN PAR DE APERTURA - TORQUIMETRO" });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora2", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Hora" });
                listavalores.Add(new string[2] { "B", "Tipo Cierre" });
                listavalores.Add(new string[2] { "C", "Proveedor" });
                listavalores.Add(new string[2] { "D", "C1" });
                listavalores.Add(new string[2] { "E", "C2" });
                listavalores.Add(new string[2] { "F", "C3" });
                listavalores.Add(new string[2] { "G", "C4" });
                listavalores.Add(new string[2] { "H", "C5" });
                listavalores.Add(new string[2] { "I", "C6" });
                listavalores.Add(new string[2] { "J", "C7" });
                listavalores.Add(new string[2] { "K", "C8" });
                listavalores.Add(new string[2] { "L", "C9" });
                listavalores.Add(new string[2] { "M", "C10" });
                listavalores.Add(new string[2] { "N", "C11" });
                listavalores.Add(new string[2] { "O", "C12" });
                listavalores.Add(new string[2] { "P", "Comentarios" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora2", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            for (int j = 0; j < (dataGridViewTorquimetro.RowCount - 1); j++)
            {
                Torquimetro = new string[16];
                for (int i = 0; i < 16; i++)
                {
                    Torquimetro[i] = dataGridViewTorquimetro.Rows[j].Cells[i].Value.ToString();
                }
                DatosTorquimetro();
            }
            //########### CONTROL DE VOLUMEN ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "CONTROL DE VOLUMEN" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora2", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Hora" });
                listavalores.Add(new string[2] { "B", "Producto" });
                listavalores.Add(new string[2] { "C", "Media" });
                listavalores.Add(new string[2] { "D", "Desviación Tipica" });
                listavalores.Add(new string[2] { "E", "Varianza" });
                listavalores.Add(new string[2] { "F", "Rea Decreto" });
                listavalores.Add(new string[2] { "G", "BOE" });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora2", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            for (int j = 0; j < (dataGridViewVolumen.RowCount - 1); j++)
            {
                ControlVolumen = new string[7];
                for (int i = 0; i < 7; i++)
                {
                    ControlVolumen[i] = dataGridViewVolumen.Rows[j].Cells[i].Value.ToString();
                }
                DatosControlVolumen();
            }
            //########### CONTROL DE TEMPERATURA ##############
            //########### TEMPERATURA DE CALDERA ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "CONTROL DE LA TEMPERATURA DE CALDERA" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora3", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Responsable" });
                listavalores.Add(new string[2] { "B", "Hora de registro" });
                listavalores.Add(new string[2] { "C", "Temperatura" });
                listavalores.Add(new string[2] { "D", "Comentarios" });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora3", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            for (int j = 0; j < (dataGridViewTempCaldera.RowCount - 1); j++)
            {
                ControlTemperatura = new string[4];
                for (int i = 0; i < 4; i++)
                {
                    ControlTemperatura[i] = dataGridViewTempCaldera.Rows[j].Cells[i].Value.ToString();
                }
                DatosControlTemperaturaCaldera();
            }
            //########### TEMPERATURA DE LLENADORA ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "CONTROL DE LA TEMPERATURA DE LLENADORA" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora3", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Responsable" });
                listavalores.Add(new string[2] { "B", "Hora Inicio" });
                listavalores.Add(new string[2] { "C", "Temperatura Inicio" });
                listavalores.Add(new string[2] { "D", "Hora Fin" });
                listavalores.Add(new string[2] { "E", "Temperatura Fin" });
                listavalores.Add(new string[2] { "F", "Comentarios" });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora3", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            for (int j = 0; j < (dataGridViewTempLLenadora.RowCount - 1); j++)
            {
                ControlTemperatura = new string[6];
                for (int i = 0; i < 6; i++)
                {
                    ControlTemperatura[i] = dataGridViewTempLLenadora.Rows[j].Cells[i].Value.ToString();
                }
                DatosControlTemperaturaLlenadora();
            }
        }
        public void DatosLlenadora()
        {
            //########### DATOS LLENADORA ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", DatosLlen[0] });
                listavalores.Add(new string[2] { "B", DatosLlen[1] });
                listavalores.Add(new string[2] { "C", DatosLlen[2] });
                listavalores.Add(new string[2] { "D", DatosLlen[3] });
                listavalores.Add(new string[2] { "E", DatosLlen[4] });
                listavalores.Add(new string[2] { "F", DatosLlen[5] });
                listavalores.Add(new string[2] { "G", DatosLlen[6] });
                listavalores.Add(new string[2] { "H", DatosLlen[7] });
                listavalores.Add(new string[2] { "I", DatosLlen[8] });
                listavalores.Add(new string[2] { "J", DatosLlen[9] });
                listavalores.Add(new string[2] { "K", DatosLlen[10] });
                listavalores.Add(new string[2] { "L", DatosLlen[11] });
                listavalores.Add(new string[2] { "M", DatosLlen[12] });
                listavalores.Add(new string[2] { "N", DatosLlen[13] });
                listavalores.Add(new string[2] { "O", DatosLlen[14] });
                listavalores.Add(new string[2] { "P", DatosLlen[15] });
                listavalores.Add(new string[2] { "Q", DatosLlen[16] });
                listavalores.Add(new string[2] { "R", DatosLlen[17] });
                listavalores.Add(new string[2] { "S", DatosLlen[18] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
                catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosLlenadoraParo()
        {
            //########### DATOS DESPALETIZADOR CIERRES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", DatosLlenParo[0] });
                listavalores.Add(new string[2] { "B", DatosLlenParo[1] });
                listavalores.Add(new string[2] { "C", DatosLlenParo[2] });
                listavalores.Add(new string[2] { "D", DatosLlenParo[3] });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosComentarios(string j)
        {
            //########### DATOS COMENTARIOS ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Hora" });
                listavalores.Add(new string[2] { "B", Comentarios[0] });
                listavalores.Add(new string[2] { "C", "Comentario " + j });
                listavalores.Add(new string[2] { "D", Comentarios[1] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosRotura()
        {

            //########### DATOS ROTURAS ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", Rotura[0] });
                listavalores.Add(new string[2] { "B", Rotura[1] });
                listavalores.Add(new string[2] { "C", Rotura[2] });
                listavalores.Add(new string[2] { "D", Rotura[3] });
                listavalores.Add(new string[2] { "E", Rotura[4] });
                listavalores.Add(new string[2] { "F", Rotura[5] });
                listavalores.Add(new string[2] { "G", Rotura[6] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosControlPresion()
        {
            //########### DATOS CONTROL PRESION ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", ControlPresion[0] });
                listavalores.Add(new string[2] { "B", ControlPresion[1] });
                listavalores.Add(new string[2] { "C", ControlPresion[2] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora2", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosControl30min()
        {
            //########### DATOS CONTROL CADA 30 MIN ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", Control30MIN[0] });
                listavalores.Add(new string[2] { "B", Control30MIN[1] });
                listavalores.Add(new string[2] { "C", Control30MIN[2] });
                listavalores.Add(new string[2] { "D", Control30MIN[3] });
                listavalores.Add(new string[2] { "E", Control30MIN[4] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora2", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosControlVerificacion()
        {
            //########### DATOS CONTROL VERIFICACION ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", ControlVerificacion[0] });
                listavalores.Add(new string[2] { "B", ControlVerificacion[1] });
                listavalores.Add(new string[2] { "C", ControlVerificacion[2] });
                listavalores.Add(new string[2] { "D", ControlVerificacion[3] });
                listavalores.Add(new string[2] { "E", ControlVerificacion[4] });
                listavalores.Add(new string[2] { "F", ControlVerificacion[5] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora2", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosTorquimetro()
        {
            //########### TORQUIMETRO ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", Torquimetro[0] });
                listavalores.Add(new string[2] { "B", Torquimetro[1] });
                listavalores.Add(new string[2] { "C", Torquimetro[2] });
                listavalores.Add(new string[2] { "D", Torquimetro[3] });
                listavalores.Add(new string[2] { "E", Torquimetro[4] });
                listavalores.Add(new string[2] { "F", Torquimetro[5] });
                listavalores.Add(new string[2] { "G", Torquimetro[6] });
                listavalores.Add(new string[2] { "H", Torquimetro[7] });
                listavalores.Add(new string[2] { "I", Torquimetro[8] });
                listavalores.Add(new string[2] { "J", Torquimetro[9] });
                listavalores.Add(new string[2] { "K", Torquimetro[10] });
                listavalores.Add(new string[2] { "L", Torquimetro[11] });
                listavalores.Add(new string[2] { "M", Torquimetro[12] });
                listavalores.Add(new string[2] { "N", Torquimetro[13] });
                listavalores.Add(new string[2] { "O", Torquimetro[14] });
                listavalores.Add(new string[2] { "P", Torquimetro[15] });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora2", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosControlVolumen()
        {
            //########### Control Volumen ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", ControlVolumen[0] });
                listavalores.Add(new string[2] { "B", ControlVolumen[1] });
                listavalores.Add(new string[2] { "C", ControlVolumen[2] });
                listavalores.Add(new string[2] { "D", ControlVolumen[3] });
                listavalores.Add(new string[2] { "E", ControlVolumen[4] });
                listavalores.Add(new string[2] { "F", ControlVolumen[5] });
                listavalores.Add(new string[2] { "G", ControlVolumen[6] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora2", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosControlTemperaturaCaldera()
        {
            //########### Control Temperatura ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", ControlTemperatura[0] });
                listavalores.Add(new string[2] { "B", ControlTemperatura[1] });
                listavalores.Add(new string[2] { "C", ControlTemperatura[2] });
                listavalores.Add(new string[2] { "D", ControlTemperatura[3] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora3", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosControlTemperaturaLlenadora()
        {
            //########### Control Temperatura ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", ControlTemperatura[0] });
                listavalores.Add(new string[2] { "B", ControlTemperatura[1] });
                listavalores.Add(new string[2] { "C", ControlTemperatura[2] });
                listavalores.Add(new string[2] { "D", ControlTemperatura[3] });
                listavalores.Add(new string[2] { "E", ControlTemperatura[4] });
                listavalores.Add(new string[2] { "F", ControlTemperatura[5] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Llenadora3", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}

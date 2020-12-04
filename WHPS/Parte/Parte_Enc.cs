using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.Utiles;

namespace WHPS.Parte
{
    public partial class Parte_Enc : Form
    {
        public string lineamarcada="";
        public string[] Array1 = new string[9];
        public string[] Array2 = new string[9];
        public string[] DatosEnc = new string[13];
        public string[] DatosEncParo = new string[4];
        public string[] Comentarios = new string[2];
        public string[] Rotura = new string[6];
        public string[] Control30MIN = new string[3];
        public string[] VisionArtificial = new string[10];
        public int j=0;
        public string[] Resumen = new string[12];
        public bool Guardar = false;
        public Parte_Enc()
        {
            InitializeComponent();
        }

        public Parte_Enc(bool guardar, string lm)
        {
            InitializeComponent();
            this.lineamarcada = lm;
            Guardar = guardar;
            this.Parte_Enc_Load(this, new EventArgs());
            this.Close();
        }

        private void Parte_Enc_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewInicio.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[0, 0]];
                dataGridViewRegistro.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[1, 0]];
                dataGridViewParo.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[2, 0]];
                dataGridViewRoturas.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[3, 0]];
                dataGridViewComentarios.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[4, 0]];
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }

            /* //dataGridViewInicio.Columns["Fecha"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
             dataGridViewInicio.Columns["Maquinista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
             dataGridViewRegistro.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
             dataGridViewParo.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
             dataGridViewComentarios.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;*/
            if (Guardar == true)
            {
                Resumen = new string[12] { "ENCAJADORA", "X", "X", "X", "--", "X", "00", "X", "X", Properties.Settings.Default.BusDia, Properties.Settings.Default.BusTurno, lineamarcada };
                Resumen[7] = "";
                CompletarParteEncajadora(Datos_Parte.Encajonadora_est);
                MaquinaLinea.Parte_Enc = true;

            }
        }
        //public void CompletarParteEncajadora(bool[] estado)
        //{
        //    //Se divide la carga en 4 partes --> 
        //    try
        //    {
        //        Array.Clear(estado, 0, 4);
        //        //ITERACIÓN 1
        //        //Inicio
        //        Thread ThreadUno = new Thread(() =>
        //        {
        //            Parte1();
        //            estado[0] = true;
        //        });
        //        ThreadUno.Start();
        //        //FIN ITERACIÓN 1

        //        //ITERACIÓN 2
        //        //Botellas
        //        while (!estado[0]) { };
        //        Thread ThreadDos = new Thread(() =>
        //        {
        //            Parte2();
        //            estado[1] = true;
        //        });
        //        ThreadDos.Start();
        //        //FIN ITERACIÓN 2

        //        //ITERACIÓN 3
        //        //Cierres
        //        while (!estado[1]) { };
        //        Thread ThreadTres = new Thread(() =>
        //        {
        //            Parte3();
        //            estado[2] = true;
        //        });
        //        ThreadTres.Start();
        //        //FIN ITERACIÓN 3

        //        //ITERACIÓN 4
        //        //Roturas ; Comentarios
        //        while (!estado[2]) { };
        //        Thread ThreadCuatro = new Thread(() =>
        //        {
        //            Parte4();
        //            estado[3] = true;
        //        });
        //        ThreadCuatro.Start();
        //        //FIN ITERACIÓN 4
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.Print(ex.Message);
        //    }
        //}
        public void CompletarParteEncajadora(bool[] estado)
        {
            //Se divide la carga en 4 partes --> 
            try
            {
                Array.Clear(estado, 0, 4);
                Parte1();
                estado[0] = true;
                Parte2();
                estado[1] = true;
                Parte3();
                estado[2] = true;
                Parte4();
                estado[3] = true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        public void Parte1()
        {
            //########### DATOS PRINCIPALES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "DATOS PRINCIPALES" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### OBTENER DATOS INICIALES ##############
            if (j < (dataGridViewInicio.RowCount - 1))
            {
                bool encontrado_ini = false, encontrado_fin = false;
                for (int f = 0; f < dataGridViewInicio.Rows.Count - 1 && (!encontrado_ini || !encontrado_fin); f++)
                {
                    //string prueba = dataGridViewInicio.Rows[0].Cells[i].Value.ToString();
                    //MessageBox.Show(prueba);
                    if (dataGridViewInicio.Rows[f].Cells[9].Value.ToString() == "Inicio" && !encontrado_ini)
                    {
                        encontrado_ini = true;
                        Resumen[1] = "OK";
                        for (int i = 0; i < 9; i++)
                        {
                            if (dataGridViewInicio.Rows[f].Cells[i].Value.ToString() == null) encontrado_ini = false;
                            Array1[i] = dataGridViewInicio.Rows[f].Cells[i].Value.ToString();
                            if (i == 3)
                            {
                                Resumen[8] = Array1[3];
                            }
                        }

                    }
                    if (dataGridViewInicio.Rows[dataGridViewInicio.Rows.Count - 2 - f].Cells[9].Value.ToString() == "Fin" && !encontrado_fin)
                    {
                        encontrado_fin = true;
                        Resumen[2] = "OK";
                        for (int i = 0; i < 9; i++)
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

        }
        public void Parte2()
        {

            //########### REGISTRO DE PRODUCCION ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "REGISTRO DE PRODUCCIÓN" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### CABECERA DATOS ETIQUETADORA ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Hora" });
                listavalores.Add(new string[2] { "B", "Lote" });
                listavalores.Add(new string[2] { "C", "Orden" });
                listavalores.Add(new string[2] { "D", "Formato" });
                listavalores.Add(new string[2] { "E", "Cliente" });
                listavalores.Add(new string[2] { "F", "Producto" });
                listavalores.Add(new string[2] { "G", "N. Cajas" });
                listavalores.Add(new string[2] { "H", "Inicio" });
                listavalores.Add(new string[2] { "I", "Fin" });
                listavalores.Add(new string[2] { "J", "Inicio Preparacion" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            int Bot_totales = 0, Caj_totales = 0;
            string formato = "";
            //########### OBTENER DATOS ENCAJADORA ##############
            for (int j = 0; j < (dataGridViewRegistro.RowCount - 1); j++)
            {
                DatosEnc = new string[13];
                for (int i = 0; i < 10; i++)
                {
                    if (dataGridViewRegistro.Rows[j].Cells[i].Value.ToString() != null) DatosEnc[i] = dataGridViewRegistro.Rows[j].Cells[i].Value.ToString();
                    if (i == 3) formato = DatosEnc[3];
                    if (i == 6)
                    {
                        if (formato.Substring(2, 1) == "X")
                        {
                            formato = formato.Substring(0, 2);
                        }


                        else formato = formato.Substring(0, 1);


                        Bot_totales += Convert.ToInt32(DatosEnc[6]) * Convert.ToInt32(formato);
                    }
                }
                Resumen[5] = Convert.ToString(Bot_totales);
                DatosEncajadora();
            }
            //########### INFORMACIÓN DE PARADA ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "INFORMACIÓN DE PARADA" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void Parte3()
        {
            //########### CABECERA INFORMACIÓN DE PARADA ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Inicio Parada" });
                listavalores.Add(new string[2] { "B", "Fin Parada" });
                listavalores.Add(new string[2] { "C", "Motivo" });
                listavalores.Add(new string[2] { "D", "Tiempo Parada" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            List<int[]> list_tiempoparada = new List<int[]>();
            int min_totales = 0;
            //########### OBTENER DATOS LLENADORA PARADA ##############
            for (int j = 0; j < (dataGridViewParo.RowCount - 1); j++)
            {
                DatosEncParo = new string[4];

                for (int i = 0; i < 4; i++)
                {

                    //string prueba = dataGridViewParo.Rows[0].Cells[i].Value.ToString();
                    //MessageBox.Show(prueba);
                    DatosEncParo[i] = dataGridViewParo.Rows[j].Cells[i].Value.ToString();
                    if (i == 3)
                    {
                        int[] tiempoparada = new int[6];
                        tiempoparada[0] = Convert.ToInt32(DatosEncParo[3].Substring(1, 1));
                        tiempoparada[1] = Convert.ToInt32(DatosEncParo[3].Substring(1, 1));
                        tiempoparada[2] = Convert.ToInt32(DatosEncParo[3].Substring(3, 1));
                        tiempoparada[3] = Convert.ToInt32(DatosEncParo[3].Substring(4, 1));
                        tiempoparada[4] = Convert.ToInt32(DatosEncParo[3].Substring(6, 1));
                        tiempoparada[5] = Convert.ToInt32(DatosEncParo[3].Substring(7, 1));

                        list_tiempoparada.Add(tiempoparada);


                    }
                }
                foreach (int[] array in list_tiempoparada)
                {
                    int h_tom = (array[0] * 10 + array[1]) * 60;
                    int m = (array[2] * 10 + array[3]);
                    int s_tom = (array[4] * 10 + array[5]) / 60;
                    min_totales += h_tom + m + s_tom;

                }
                Resumen[6] = Convert.ToString(min_totales);
                DatosEncajadoraParo();
            }
            //########### DATOS COMENTARIOS ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "COMENTARIOS" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

        
    }
        public void Parte4()
        {
            //########### OBTENER DATOS COMENTARIOS ##############
            for (int j = 0; j < (dataGridViewComentarios.RowCount - 1); j++)
            {
                for (int i = 0; i < 2; i++)
                {
                    Comentarios[i] = dataGridViewComentarios.Rows[j].Cells[i].Value.ToString();
                    Resumen[4] = "!";
                }
                DatosComentarios(Convert.ToString(j));
            }
            //########### GENERO UN ESPACIO ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "ROTURA DE BOTELLAS" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
                //MessageBox.Show(salida);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### OBTENER DATOS ROTURAS ##############
            for (int j = 0; j < (dataGridViewRoturas.RowCount - 1); j++)
            {
                Rotura = new string[6];
                for (int i = 0; i < 6; i++)
                {
                    Rotura[i] = dataGridViewRoturas.Rows[j].Cells[i].Value.ToString();
                    Resumen[3] = "OK";
                }
                DatosRotura();
            }
            DatosResumen();
        }





        public void DatosEncajadora()
        {
            //########### DATOS ETIQUETADORA ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", DatosEnc[0] });
                listavalores.Add(new string[2] { "B", DatosEnc[1] });
                listavalores.Add(new string[2] { "C", DatosEnc[2] });
                listavalores.Add(new string[2] { "D", DatosEnc[3] });
                listavalores.Add(new string[2] { "E", DatosEnc[4] });
                listavalores.Add(new string[2] { "F", DatosEnc[5] });
                listavalores.Add(new string[2] { "G", DatosEnc[6] });
                listavalores.Add(new string[2] { "H", DatosEnc[7] });
                listavalores.Add(new string[2] { "I", DatosEnc[8] });
                listavalores.Add(new string[2] { "J", DatosEnc[9] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosEncajadoraParo()
        {
            //########### DATOS Encajadora CIERRES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", DatosEncParo[0] });
                listavalores.Add(new string[2] { "B", DatosEncParo[1] });
                listavalores.Add(new string[2] { "C", DatosEncParo[2] });
                listavalores.Add(new string[2] { "D", DatosEncParo[3] });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Encajadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosResumen()
        {
            //########### DATOS RESUMEN ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "" });
                listavalores.Add(new string[2] { "B", Resumen[0] });
                listavalores.Add(new string[2] { "C", Resumen[1] });
                listavalores.Add(new string[2] { "D", Resumen[2] });
                listavalores.Add(new string[2] { "E", Resumen[3] });
                listavalores.Add(new string[2] { "F", Resumen[4] });
                listavalores.Add(new string[2] { "G", Resumen[5] });
                listavalores.Add(new string[2] { "H", Resumen[6] });
                listavalores.Add(new string[2] { "I", Resumen[7] });
                listavalores.Add(new string[2] { "J", Resumen[8] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Resumen", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}

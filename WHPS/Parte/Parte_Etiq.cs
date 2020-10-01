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
    public partial class Parte_Etiq : Form
    {
        public string[] Array1 = new string[9];
        public string[] Array2 = new string[9];
        public string[] DatosEtiq = new string[14];
        public string[] DatosEtiqParo = new string[4];
        public string[] Comentarios = new string[2];
        public string[] Rotura = new string[6];
        public string[] Control30MIN = new string[3];
        public string[] VisionArtificial = new string[10];
        public int j=0;
        public Parte_Etiq()
        {
            InitializeComponent();
        }

        public Parte_Etiq(bool guardar)
        {
            InitializeComponent();
            Parte_Etiq_Load(this, new EventArgs());
            this.Close();
        }
        private void Parte_Etiq_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewInicio.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[0, 0]];
                dataGridViewRegistro.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[1, 0]];
                dataGridViewParo.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[2, 0]];
                dataGridViewControl30min.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[3, 0]];
                dataGridViewVisionArtificial.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[4, 0]];
                dataGridViewRoturas.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[5, 0]];
                dataGridViewComentarios.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[6, 0]];
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }

           /* dataGridViewInicio.Columns["Maquinista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewRegistro.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewParo.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewComentarios.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;*/

            if (MaquinaLinea.Parte_Guardar == true)
            {
                CompletarParteEtiquetadora();
                MaquinaLinea.Parte_Etiq = true;
            }
        }

        public void CompletarParteEtiquetadora()
        {
            //########### DATOS PRINCIPALES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "DATOS PRINCIPALES" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### OBTENER DATOS INICIALES ##############
            if (j < (dataGridViewInicio.RowCount - 1))
            {
                bool encontrado_ini=false, encontrado_fin=false;
                for (int f = 0; f < dataGridViewInicio.Rows.Count - 1 && (!encontrado_ini || !encontrado_fin); f++)
                {                    
                        //string prueba = dataGridViewInicio.Rows[0].Cells[i].Value.ToString();
                        //MessageBox.Show(prueba);
                        if (dataGridViewInicio.Rows[f].Cells[9].Value.ToString() == "Inicio" && !encontrado_ini)
                            {
                            encontrado_ini = true;
                            for (int i = 0; i < 9; i++)
                            {
                                if (dataGridViewInicio.Rows[f].Cells[i].Value.ToString() == null) encontrado_ini = false;
                                Array1[i] = dataGridViewInicio.Rows[f].Cells[i].Value.ToString();                            
                            }
                                
                            }
                        if (dataGridViewInicio.Rows[dataGridViewInicio.Rows.Count -2 - f].Cells[9].Value.ToString() == "Fin" && !encontrado_fin)
                        {
                            encontrado_fin = true;
                            for (int i = 0; i < 9; i++)
                            {
                                if (dataGridViewInicio.Rows[dataGridViewInicio.Rows.Count-2-f].Cells[i].Value.ToString() == null) encontrado_fin = false;
                                Array2[i] = dataGridViewInicio.Rows[dataGridViewInicio.Rows.Count-2-f].Cells[i].Value.ToString();
                            }
                           
                        }

                        

                    
                }

            }
            Console.WriteLine("ARRAYS A ESCRIBIR: " + Array1.ToString() + " y " + Array2.ToString());
            //########### DATOS INICIALES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Línea" });
                listavalores.Add(new string[2] { "B", "L" + Convert.ToString(MaquinaLinea.numlin) });
                listavalores.Add(new string[2] { "C", "Turno" });
                listavalores.Add(new string[2] { "D", Array1[4] });
                listavalores.Add(new string[2] { "E", "Fecha" });
                listavalores.Add(new string[2] { "F", Array1[0] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            //########### DATOS PRODUCCIÓN ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "REGISTRO DE PRODUCCIÓN" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
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
                    listavalores.Add(new string[2] { "G", "N. Botellas" });
                    listavalores.Add(new string[2] { "H", "Graduacion" });
                    listavalores.Add(new string[2] { "I", "Inicio" });
                    listavalores.Add(new string[2] { "J", "Fin" });
                    listavalores.Add(new string[2] { "K", "Inicio Preparacion" });
                    string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
                //########### OBTENER DATOS ETIQUETADORA ##############
                for (int j = 0; j < (dataGridViewRegistro.RowCount - 1) ; j++)
                {
                    DatosEtiq = new string[12];
                    for (int i = 0; i < 11; i++)
                    {
                        if (dataGridViewRegistro.Rows[j].Cells[i].Value.ToString() != null) DatosEtiq[i] = dataGridViewRegistro.Rows[j].Cells[i].Value.ToString();
                    }
                    DatosEtiquetadora();
                }
            //########### INFORMACIÓN DE PARADA ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "INFORMACIÓN DE PARADA" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### OBTENER DATOS LLENADORA PARADA ##############
            for (int j = 0; j < (dataGridViewParo.RowCount - 1); j++)
            {
                DatosEtiqParo = new string[4];

                for (int i = 0; i < 4; i++)
                {

                    //string prueba = dataGridViewParo.Rows[0].Cells[i].Value.ToString();
                    //MessageBox.Show(prueba);
                    DatosEtiqParo[i] = dataGridViewParo.Rows[j].Cells[i].Value.ToString();
                }
                DatosEtiquetadoraParo();
            }
            //########### DATOS COMENTARIOS ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "COMENTARIOS" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
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
                //########### DATOS ROTURAS ##############
                try
                {
                    List<string[]> listavalores = new List<string[]>();
                    listavalores.Add(new string[2] { "A", "-" });
                    listavalores.Add(new string[2] { "B", "ROTURA DE BOTELLAS" });
                    string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
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
                    string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
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
                    }
                    DatosRotura();
                }

                //########### CONTROL CADA 30 MIN ##############
                try
                {
                    List<string[]> listavalores = new List<string[]>();
                    listavalores.Add(new string[2] { "A", "-" });
                    listavalores.Add(new string[2] { "B", "CONTROL CADA 30MIN" });
                    string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
                try
                {
                    List<string[]> listavalores = new List<string[]>();
                    listavalores.Add(new string[2] { "A", "Hora" });
                    listavalores.Add(new string[2] { "B", "Estado Etiqueta" });

                    string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
                for (int j = 0; j < (dataGridViewControl30min.RowCount - 1); j++)
                {
                    Control30MIN = new string[2];
                    for (int i = 0; i < 2; i++)
                    {
                        Control30MIN[i] = dataGridViewControl30min.Rows[j].Cells[i].Value.ToString();
                    }
                    DatosControl30min();
                }
                //########### VISION ARTIFICIAL ##############
                try
                {
                    List<string[]> listavalores = new List<string[]>();
                    listavalores.Add(new string[2] { "A", "-" });
                    listavalores.Add(new string[2] { "B", "VISION ARTIFICIAL" });

                    string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
                try
                {
                    List<string[]> listavalores = new List<string[]>();
                    listavalores.Add(new string[2] { "A", "Hora" });
                    listavalores.Add(new string[2] { "B", "Revision 1" });
                    listavalores.Add(new string[2] { "C", "Estado 1" });
                    listavalores.Add(new string[2] { "D", "Revision 2" });
                    listavalores.Add(new string[2] { "E", "Estado 2" });
                    listavalores.Add(new string[2] { "F", "Revision 3" });
                    listavalores.Add(new string[2] { "G", "Estado 3" });
                    listavalores.Add(new string[2] { "H", "Revision 4" });
                    listavalores.Add(new string[2] { "I", "Estado 4" });
                    listavalores.Add(new string[2] { "J", "Comentarios" });
                    string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
                for (int j = 0; j < (dataGridViewVisionArtificial.RowCount - 1); j++)
                {
                    VisionArtificial = new string[10];
                    for (int i = 0; i < 10; i++)
                    {
                        VisionArtificial[i] = dataGridViewVisionArtificial.Rows[j].Cells[i].Value.ToString();
                    }
                    DatosVisionArtificial();
                }
            }

        public void DatosEtiquetadora()
        {
            //########### DATOS ETIQUETADORA ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", DatosEtiq[0] });
                listavalores.Add(new string[2] { "B", DatosEtiq[1] });
                listavalores.Add(new string[2] { "C", DatosEtiq[2] });
                listavalores.Add(new string[2] { "D", DatosEtiq[3] });
                listavalores.Add(new string[2] { "E", DatosEtiq[4] });
                listavalores.Add(new string[2] { "F", DatosEtiq[5] });
                listavalores.Add(new string[2] { "G", DatosEtiq[6] });
                listavalores.Add(new string[2] { "H", DatosEtiq[7] });
                listavalores.Add(new string[2] { "I", DatosEtiq[8] });
                listavalores.Add(new string[2] { "J", DatosEtiq[9] });
                listavalores.Add(new string[2] { "K", DatosEtiq[10] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosEtiquetadoraParo()
        {
            //########### DATOS Etiqueradora CIERRES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", DatosEtiqParo[0] });
                listavalores.Add(new string[2] { "B", DatosEtiqParo[1] });
                listavalores.Add(new string[2] { "C", DatosEtiqParo[2] });
                listavalores.Add(new string[2] { "D", DatosEtiqParo[3] });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiqueradora", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosVisionArtificial()
        {
            //########### DATOS VISION ARTIFICIAL ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", VisionArtificial[0] });
                listavalores.Add(new string[2] { "B", VisionArtificial[1] });
                listavalores.Add(new string[2] { "C", VisionArtificial[2] });
                listavalores.Add(new string[2] { "D", VisionArtificial[3] });
                listavalores.Add(new string[2] { "E", VisionArtificial[4] });
                listavalores.Add(new string[2] { "F", VisionArtificial[5] });
                listavalores.Add(new string[2] { "G", VisionArtificial[6] });
                listavalores.Add(new string[2] { "H", VisionArtificial[7] });
                listavalores.Add(new string[2] { "I", VisionArtificial[8] });
                listavalores.Add(new string[2] { "J", VisionArtificial[9] });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Etiquetadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

    }
}

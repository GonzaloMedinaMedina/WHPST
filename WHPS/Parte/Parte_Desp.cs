using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using WHPS.Utiles;
using WHPS.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using WHPS.ProgramMenus;

namespace WHPS.Parte
{
    public partial class Parte_Desp : Form
    {
        public string lineamarcada = "";
        public string[] Array1 = new string[9];
        public string[] Array2 = new string[9];
        public string[] DatosDespBot = new string[5];
        public string[] DatosDespCierre= new string[12];
        public string[] DatosDespParo = new string[12];
        public string[] Comentarios = new string[2];
        public string[] Rotura = new string[6];
        public int j=0;
        public Parte_Desp()
        {
            InitializeComponent();
        }

        public Parte_Desp(bool guardar, string ln)
        {
            InitializeComponent();
            this.lineamarcada = ln;
            Parte_Desp_Load(this, new EventArgs());
            this.Close();
        }
           private void Parte_Desp_Load(object sender, EventArgs e)
            {
            try
            {
                dataGridViewInicio.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[0, 0]];
                dataGridViewBotellas.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[1, 0]];
                dataGridViewCierres.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[2, 0]];
                dataGridViewParo.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[3, 0]];
                dataGridViewRoturas.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[4, 0]];
                dataGridViewComentarios.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[5, 0]];
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }

            if (MaquinaLinea.Parte_Guardar == true)
            {
                CompletarParteDespaletizador();
                MaquinaLinea.Parte_Desp = true;
            }
        }


        public void CompletarParteDespaletizador()
        {
            
            //########### DATOS INICIALES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "DATOS PRINCIPALES" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
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
                    if (dataGridViewInicio.Rows[f].Cells[9].Value.ToString() == "Inicio" && !encontrado_ini)
                    {
                        encontrado_ini = true;
                        for (int i = 0; i < 9; i++)
                        {
                            if (dataGridViewInicio.Rows[f].Cells[i].Value.ToString() == null) encontrado_ini = false;
                            Array1[i] = dataGridViewInicio.Rows[f].Cells[i].Value.ToString();
                        }

                    }
                    if (dataGridViewInicio.Rows[dataGridViewInicio.Rows.Count - 2 - f].Cells[9].Value.ToString() == "Fin" && !encontrado_fin)
                    {
                        encontrado_fin = true;
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### DATOS MATERIALES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "INFORMACIÓN DE BOTELLAS" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### CABECERA DATOS DESPALETIZADO BOTELLAS ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Hora" });
                listavalores.Add(new string[2] { "B", "Descripción Bot." });
                listavalores.Add(new string[2] { "C", "Proveedor Bot." });
                listavalores.Add(new string[2] { "D", "Lote Bot." });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### OBTENER DATOS DESPALETIZADOR ##############
            for (int j = 0; j < (dataGridViewBotellas.RowCount - 1); j++)
            {
                DatosDespBot = new string[4];

                for (int i = 0; i < 4; i++)
                {
                    DatosDespBot[i] = dataGridViewBotellas.Rows[j].Cells[i].Value.ToString();
                }

                DatosDespaletizadorBot();
            }
            //########### DATOS MATERIALES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "INFORMACIÓN DE CIERRES" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### CABECERA DATOS DESPALETIZADO BOTELLAS ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Hora" });
                listavalores.Add(new string[2] { "B", "Descripción Cier." });
                listavalores.Add(new string[2] { "C", "Proveedor Cier." });
                listavalores.Add(new string[2] { "D", "Lote Cier." });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### OBTENER DATOS DESPALETIZADOR ##############
            for (int j = 0; j < (dataGridViewCierres.RowCount - 1); j++)
            {
                DatosDespCierre = new string[4];

                for (int i = 0; i < 4; i++)
                {

                    //string prueba = dataGridViewCierres.Rows[0].Cells[i].Value.ToString();
                    //MessageBox.Show(prueba);
                    DatosDespCierre[i] = dataGridViewCierres.Rows[j].Cells[i].Value.ToString();

                }
                DatosDespaletizadorCierres();
            }
            //########### DATOS PARADA ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "INFORMACIÓN DE PARADA" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### CABECERA DATOS DESPALETIZADO BOTELLAS ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Inicio Parada" });
                listavalores.Add(new string[2] { "B", "Fin Parada" });
                listavalores.Add(new string[2] { "C", "Motivo" });
                listavalores.Add(new string[2] { "D", "Tiempo Parada" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### OBTENER DATOS DESPALETIZADOR ##############
            for (int j = 0; j < (dataGridViewParo.RowCount - 1); j++)
            {
                DatosDespParo = new string[4];

                for (int i = 0; i < 4; i++)
                {

                    //string prueba = dataGridViewInicio.Rows[0].Cells[i].Value.ToString();
                    //MessageBox.Show(prueba);
                    DatosDespParo[i] = dataGridViewParo.Rows[j].Cells[i].Value.ToString();

                }
                DatosDespaletizadorParo();
            }
            //########### DATOS MATERIALES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "COMENTARIOS" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
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
            //########### DATOS INICIALES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "ROTURA DE BOTELLAS" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
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
        }
        public void DatosDespaletizadorBot()
        {
            //########### DATOS DESPALETIZADOR BOTELLAS ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", DatosDespBot[0] });
                listavalores.Add(new string[2] { "B", DatosDespBot[1] });
                listavalores.Add(new string[2] { "C", DatosDespBot[2] });
                listavalores.Add(new string[2] { "D", DatosDespBot[3] });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosDespaletizadorCierres()
        {
            //########### DATOS DESPALETIZADOR CIERRES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", DatosDespCierre[0] });
                listavalores.Add(new string[2] { "B", DatosDespCierre[1] });
                listavalores.Add(new string[2] { "C", DatosDespCierre[2] });
                listavalores.Add(new string[2] { "D", DatosDespCierre[3] });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void DatosDespaletizadorParo()
        {
            //########### DATOS DESPALETIZADOR CIERRES ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", DatosDespParo[0] });
                listavalores.Add(new string[2] { "B", DatosDespParo[1] });
                listavalores.Add(new string[2] { "C", DatosDespParo[2] });
                listavalores.Add(new string[2] { "D", DatosDespParo[3] });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
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
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileParte, "Despaletizador", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}

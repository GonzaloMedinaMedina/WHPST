using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Windows.Forms;
using WHPS.Utiles;
using WHPS.Model;
using WHPS.ProgramMenus;
using System.Threading;
using System.Diagnostics;

namespace WHPS.Despaletizador
{
    class Apps_Despaletizador
    {

        //############################  PARSING CODIGO DE BOTELLAS  #############################
        public static Datos_Botellas ParsingCod_Botellas(string codigoToBC)
        {
            //Variables de trabajo
            Datos_Botellas datos_botellas = new Datos_Botellas();

            string codigo = codigoToBC;
            int numcaract;

            //Identificamos la longitud de la cádena de texto a realizar el parsing
            numcaract = codigo.Length;

            //################# VERALLIA ###########################################            
            //VERALLIA 1: (01)EAN (11) FECHA
            if (numcaract == 24 && codigo.Substring(0, 2) == "01" && codigo.Substring(16, 2) == "11")
            {
                //Extraemos el EAN tras el código (01)
                datos_botellas.ean = codigo.Substring(2, 14);
                //Extraemos la Fecha de Fabricación tras el (11)
                datos_botellas.FechaFab = codigo.Substring(18, 6);
            }
            //VERALLIA 2: (00) SSCC (10) LOTE Fab
            if (numcaract == 30 && codigo.Substring(0, 2) == "00" && codigo.Substring(20, 2) == "10")
            {
                //Extraemos el SSCC tras el (00)
                datos_botellas.SSCC = codigo.Substring(2, 18);
                //Extraemos el LoteFab tras el (10)
                datos_botellas.LoteFab = codigo.Substring(22,8);
            }

            //################# OI 1 ###########################################
            //ETIQUETA DE LA PLANTA DE FRANCIA
            //OI 1: (01)EAN (10) LOTE (11) FECHA
            if (numcaract == 35 && codigo.Substring(0, 2) == "01" && codigo.Substring(16, 2) == "10" && codigo.Substring(27, 2) == "11")
            {
                //Extraemos el EAN tras el código (01)
                datos_botellas.ean = codigo.Substring(2, 14);
                //Extraemos el Lote de Fabricación tras el (10)
                datos_botellas.LoteFab = codigo.Substring(18, 9);
                //Extraemos la Fecha de Fabricación tras el (11)
                datos_botellas.FechaFab = codigo.Substring(29, 6);
            }
            //OI 2: (00) SSCC
            if (numcaract == 20 && codigo.Substring(0, 2) == "00")
            {
                //Extraemos el SSCC tras el (00)
                datos_botellas.SSCC = codigo.Substring(2, 18);
            }
            //################# OI 2 ###########################################
            //ETIQUETA DE LA PLANTA DE BARCELONA
            //OI 1: (01)EAN (11) FECHA
            if (numcaract == 24 && codigo.Substring(0, 2) == "01" && codigo.Substring(16, 2) == "11")
            {
                //Extraemos el EAN tras el código (01)
                datos_botellas.ean = codigo.Substring(2, 14);
                //Extraemos la Fecha de Fabricación tras el (11)
                datos_botellas.FechaFab = codigo.Substring(18, 6);
            }
            //OI 2: (00) SSCC (10) LOTE Fab
            if (numcaract == 31 && codigo.Substring(0, 2) == "00" && codigo.Substring(20, 2) == "10")
            {
                //Extraemos el SSCC tras el (00)
                datos_botellas.SSCC = codigo.Substring(2, 18);
                //Extraemos el LoteFab tras el (10)
                datos_botellas.LoteFab = codigo.Substring(22, 9);
            }
            //################# SAVER_GLASS ###########################################
            //SAVERGLASS 1: (02)EAN (37) CANTIDAD
            if (numcaract == 24 && codigo.Substring(0, 2) == "02" && codigo.Substring(16, 2) == "37")
            {
                //Extraemos el EAN tras el código (01)
                datos_botellas.ean = codigo.Substring(2, 14);
                //Extraemos la Cantidad tras el código (37)
                datos_botellas.Cantidad = codigo.Substring(18, 6);
            }
            //SAVERGLASS 2: (10) Lote (21) NumPallet (11) Fecha
            if (numcaract == 31 && codigo.Substring(0, 2) == "10" && codigo.Substring(12, 2) == "21" && codigo.Substring(23, 2) == "11")
            {
                //Extraemos el Lote tras el (10)
                datos_botellas.LoteFab = codigo.Substring(2, 10);
                //Extraemos la Fecha de Fabricación tras el (11)
                datos_botellas.FechaFab = codigo.Substring(25, 6);
            }
            //SAVERGLASS 3: (00) SSCC 
            if (numcaract == 20 && codigo.Substring(0, 2) == "00")
            {
                //Extraemos el SSCC tras el (00)
                datos_botellas.SSCC = codigo.Substring(2, 18);
            }
            //################# TRABAJO MANUAL ###########################################
            //TRABAJO MANUAL 1: (02)EAN (37)CANTIDAD 
            if (numcaract == 22 && codigo.Substring(0, 2) == "02" && codigo.Substring(16, 2) == "37")
            {
                //Extraemos el EAN tras el código (01)
                datos_botellas.eanTM = codigo.Substring(2, 14);
                //Extraemos la Cantidad tras el (37)
                //datos_botellas.Cantidad = codigo.Substring(18, 4);
            }
            //TRABAJO MANUAL 2: (10) LOTE (11) FECHA 
            if (numcaract == 17 && codigo.Substring(0, 2) == "10" && codigo.Substring(9, 2) == "11")
            {
                //Extraemos el Lote de Fabricación tras el (10)
                datos_botellas.LoteFabTM = codigo.Substring(2, 7);
                //Extraemos la Fecha de fabricación tras el (11)
                datos_botellas.FechaFabTM = codigo.Substring(11, 6);
            }

            //Buscamos el proveedor en la tabla interna por el EAN
            if (datos_botellas.ean != "" || datos_botellas.eanTM != "")
            {
                if  (datos_botellas.eanTM != "") datos_botellas.ean = datos_botellas.eanTM;
                //Generamos la lista de valores a filtrar y traer desde la lista de materiales AND, 'Columna', LIKE, 'Codigo Material'
                List<string[]> listavalores = new List<string[]>();
                string[] valores = new string[4];
                valores[0] = "AND";
                valores[1] = "EAN";
                valores[2] = "LIKE";
                valores[3] = " \"" + datos_botellas.ean + "\"";
                listavalores.Add(valores);

                //Llamamos la busqueda del fichero excel
                string result;
                DataSet excelDataSet = new DataSet();
                excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileMateriales, "Botellas", "Codigo;Proveedor;Descripcion;Cantidad".Split(';'), listavalores, out result);
                //MessageBox.Show(result);

                if (excelDataSet.Tables[0].Rows.Count > 0)
                {
                    datos_botellas.refInt = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Codigo"]);
                    datos_botellas.whDescrip = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Descripcion"]);
                    datos_botellas.Proveedor = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Proveedor"]);
                    datos_botellas.Cantidad = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Cantidad"]);
                }
                else
                {
                    MessageBox.Show("CODIGO NO ENCONTRADO: Informar al responsable");
                }

            }
            if (datos_botellas.eanTM != "") datos_botellas.ean = "";
            return datos_botellas;

        }
        //################## END PARSING    ####################################################################


        //############################  PARSING CODIGO DE CIERRES  #############################
        public static Datos_Cierres ParsingCod_Cierres(string codigoToBC)
        {
            //Variables de trabajo
            Datos_Cierres datos_cierres = new Datos_Cierres();

            string codigo = codigoToBC;
            int numcaract;

            //Identificamos la longitud de la cádena de texto a realizar el parsing
            numcaract = codigo.Length;

            //################# TORRENT MIRANDA ###########################################
            //TAPONES DE CORCHO           
            //Torrent Miranda 1: (02)EAN (11) FECHA (37) CANTIDAD (10) LOTE
            if (numcaract == 50 && codigo.Substring(0, 2) == "02" && codigo.Substring(16, 2) == "11" && codigo.Substring(24, 2) == "37" && codigo.Substring(32, 2) == "10")
            {
                //Extraemos el EAN tras el código (02)
                datos_cierres.ean = codigo.Substring(2, 14);
                //Extraemos la Fecha de Fabricación tras el (11)
                datos_cierres.FechaFab = codigo.Substring(18, 6);
                //Extraemos la Cantidad tras el (37)
                datos_cierres.Cantidad = codigo.Substring(26, 6);
                //Extraemos el Lote tras el (10)
                datos_cierres.LoteFab = codigo.Substring(34, 16);
            }
            //Torrent Miranda 2: (00) SSCC (11) Nuestro Codigo
            if (numcaract == 32 && codigo.Substring(0, 2) == "00" && codigo.Substring(20, 2) == "11")
            {
                //Extraemos el SSCC tras el (00)
                datos_cierres.SSCC = codigo.Substring(2, 18);
            }


            //################# CAPSULAS TORRENT 1 ###########################################
            //CÓDIGO QUE TIENE ENCUENTA QUE LA CANTIDAD DE BOTELLA ES DE 4 CIFRAS 
            //CAPSULAS TORRENT 1: (02)EAN (11) FECHA (10) LOTE (37) CANTIDAD (94) NUMERO DE CAJA
            if (numcaract == 46 && codigo.Substring(0, 2) == "02" && codigo.Substring(16, 2) == "11" && codigo.Substring(24, 2) == "10" && codigo.Substring(37, 2) == "37" && codigo.Substring(43, 2) == "94")
            {
                //Extraemos el EAN tras el código (01)
                datos_cierres.ean = codigo.Substring(2, 14);
                //Extraemos la Fecha de fabricación tras el (11)
                datos_cierres.FechaFab = codigo.Substring(18, 6);
                //Extraemos el Lote de Fabricación tras el (10)
                datos_cierres.LoteFab = codigo.Substring(26, 11);
                //Extraemos la Cantidad tras el (37)
                datos_cierres.Cantidad = codigo.Substring(31, 4);
                //Extraemos el Numero de caja tras el (94)
            }

            //################# CAPSULAS TORRENT 2 ###########################################
            //CÓDIGO QUE TIENE ENCUENTA QUE LA CANTIDAD DE BOTELLA ES DE 3 CIFRAS 
            //CAPSULAS TORRENT 1: (02)EAN (11) FECHA (10) LOTE (37) CANTIDAD (94) NUMERO DE CAJA
            if (numcaract == 45 && codigo.Substring(0, 2) == "02" && codigo.Substring(16, 2) == "11" && codigo.Substring(24, 2) == "10" && codigo.Substring(37, 2) == "37" && codigo.Substring(43, 2) == "94")
            {
                //Extraemos el EAN tras el código (01)
                datos_cierres.ean = codigo.Substring(2, 14);
                //Extraemos la Fecha de fabricación tras el (11)
                datos_cierres.FechaFab = codigo.Substring(18, 6);
                //Extraemos el Lote de Fabricación tras el (10)
                datos_cierres.LoteFab = codigo.Substring(26, 11);
                //Extraemos la Cantidad tras el (37)
                datos_cierres.Cantidad = codigo.Substring(31, 3);
                //Extraemos el Numero de caja tras el (94)
            }
            //################# CAPSULAS TORRENT 3 ###########################################
            //CÓDIGO QUE TIENE ENCUENTA QUE LA CANTIDAD DE BOTELLA ES DE 4 CIFRAS DONDE NO SE TIENE EN CUENTA EL NUMERO DE LA CAJA PARA LOTES ANTIGUOS
            //CAPSULAS TORRENT 1: (02)EAN (11) FECHA (10) LOTE (37) CANTIDAD (94) NUMERO DE CAJA
            if (numcaract == 43 && codigo.Substring(0, 2) == "02" && codigo.Substring(16, 2) == "11" && codigo.Substring(24, 2) == "10" && codigo.Substring(37, 2) == "37")
            {
                //Extraemos el EAN tras el código (01)
                datos_cierres.ean = codigo.Substring(2, 14);
                //Extraemos la Fecha de fabricación tras el (11)
                datos_cierres.FechaFab = codigo.Substring(18, 6);
                //Extraemos el Lote de Fabricación tras el (10)
                datos_cierres.LoteFab = codigo.Substring(26, 11);
                //Extraemos la Cantidad tras el (37)
                datos_cierres.Cantidad = codigo.Substring(31, 4);
                //Extraemos el Numero de caja tras el (94)
            }

            //################# CAPSULAS TORRENT 4 ###########################################
            //CÓDIGO QUE TIENE ENCUENTA QUE LA CANTIDAD DE BOTELLA ES DE 3 CIFRAS DONDE NO SE TIENE EN CUENTA EL NUMERO DE LA CAJA PARA LOTES ANTIGUOS
            //CAPSULAS TORRENT 1: (02)EAN (11) FECHA (10) LOTE (37) CANTIDAD (94) NUMERO DE CAJA
            if (numcaract == 42 && codigo.Substring(0, 2) == "02" && codigo.Substring(16, 2) == "11" && codigo.Substring(24, 2) == "10" && codigo.Substring(37, 2) == "37")
            {
                //Extraemos el EAN tras el código (01)
                datos_cierres.ean = codigo.Substring(2, 14);
                //Extraemos la Fecha de fabricación tras el (11)
                datos_cierres.FechaFab = codigo.Substring(18, 6);
                //Extraemos el Lote de Fabricación tras el (10)
                datos_cierres.LoteFab = codigo.Substring(26, 11);
                //Extraemos la Cantidad tras el (37)
                datos_cierres.Cantidad = codigo.Substring(31, 3);
                //Extraemos el Numero de caja tras el (94)
            }

            //################# CAPSULAS TORRENT 5 ###########################################
            //CÓDIGO QUE TIENE ENCUENTA QUE LA CANTIDAD DE BOTELLA ES DE 4 CIFRAS DONDE NO SE TIENE EN CUENTA EL NUMERO DE LA CAJA PARA LOTES ANTIGUOS
            //CAPSULAS TORRENT 1: (02)EAN (11) FECHA (10) LOTE (37) CANTIDAD (94) NUMERO DE CAJA
            if (numcaract == 48 && codigo.Substring(0, 2) == "02" && codigo.Substring(16, 2) == "11" && codigo.Substring(24, 2) == "10" && codigo.Substring(37, 2) == "37")
            {
                //Extraemos el EAN tras el código (01)
                datos_cierres.ean = codigo.Substring(2, 14);
                //Extraemos la Fecha de fabricación tras el (11)
                datos_cierres.FechaFab = codigo.Substring(18, 6);
                //Extraemos el Lote de Fabricación tras el (10)
                datos_cierres.LoteFab = codigo.Substring(26, 11);
                //Extraemos la Cantidad tras el (37)
                datos_cierres.Cantidad = codigo.Substring(39, 4);
                //Extraemos el Numero de caja tras el (94)
            }
            //################# TAPON JEREZ 1 ###########################################
            //TAPON JEREZ 1: (02)EAN (10) LOTE (37) CANTIDAD
            //02 03163015500631 10 172 37 3500
            if (numcaract == 28 && codigo.Substring(0, 2) == "02" && codigo.Substring(16, 2) == "10" && codigo.Substring(21, 2) == "37")
            {
                //Extraemos el EAN tras el código (01)
                datos_cierres.ean = codigo.Substring(2, 14);
                //Extraemos el Lote de Fabricación tras el (10)
                datos_cierres.LoteFab = codigo.Substring(18, 3);
                //Extraemos la Cantidad tras el (37)
                datos_cierres.Cantidad = codigo.Substring(23, 5);
            }
            //TAPON JEREZ 2: (00)SSCC 
            if (numcaract == 32 && codigo.Substring(0, 2) == "00")
            {
                //Extraemos el SSCC tras el (00)
                datos_cierres.SSCC = codigo.Substring(2, 18);
            }
            //################# TAPON JEREZ 2 ###########################################
            //TAPON JEREZ 1: (02)EAN (10) LOTE (37) CANTIDAD
            //02 03163015500631 10 172 37 3500
            if (numcaract == 27 && codigo.Substring(0, 2) == "02" && codigo.Substring(16, 2) == "10" && codigo.Substring(21, 2) == "37")
            {
                //Extraemos el EAN tras el código (01)
                datos_cierres.ean = codigo.Substring(2, 14);
                //Extraemos el Lote de Fabricación tras el (10)
                datos_cierres.LoteFab = codigo.Substring(18, 3);
                //Extraemos la Cantidad tras el (37)
                datos_cierres.Cantidad = codigo.Substring(23, 4);
                //LA FECHA DE FABRICACIÓN NO ESTA CONTENIDA EN EL CÓDIGO
                datos_cierres.FechaFab = "-";
            }
            //TAPON JEREZ 2: (00)SSCC 
            if (numcaract == 32 && codigo.Substring(0, 2) == "00")
            {
                //Extraemos el SSCC tras el (00)
                datos_cierres.SSCC = codigo.Substring(2, 18);
            }
            //################# TAPON JEREZ 3 ###########################################
            //TAPON JEREZ 1: (02)EAN (10) LOTE (37) CANTIDAD
            if (numcaract == 26 && codigo.Substring(0, 2) == "02" && codigo.Substring(16, 2) == "10" && codigo.Substring(21, 2) == "37")
            {
                //Extraemos el EAN tras el código (01)
                datos_cierres.ean = codigo.Substring(2, 14);
                //Extraemos el Lote de Fabricación tras el (10)
                datos_cierres.LoteFab = codigo.Substring(18, 3);
                //Extraemos la Cantidad tras el (37)
                datos_cierres.Cantidad = codigo.Substring(23, 3);
                //LA FECHA DE FABRICACIÓN NO ESTA CONTENIDA EN EL CÓDIGO
                datos_cierres.FechaFab = "-";
            }
            //TAPON JEREZ 2: (00)SSCC 
            if (numcaract == 32 && codigo.Substring(0, 2) == "00")
            {
                //Extraemos el SSCC tras el (00)
                datos_cierres.SSCC = codigo.Substring(2, 18);
            }
            //################# TAPON JEREZ 4 ###########################################
            //TAPON JEREZ 1: (02)EAN (10) LOTE (37) CANTIDAD
            //02 03163015500631 10 172 37 3500
            //02 08435080309279 10 2158270 37 1200
            if (numcaract == 31 && codigo.Substring(0, 2) == "02" && codigo.Substring(16, 2) == "10" && codigo.Substring(25, 2) == "37")
            {
                //Extraemos el EAN tras el código (01)
                datos_cierres.ean = codigo.Substring(2, 14);
                //Extraemos el Lote de Fabricación tras el (10)
                datos_cierres.LoteFab = codigo.Substring(18, 7);
                //Extraemos la Cantidad tras el (37)
                datos_cierres.Cantidad = codigo.Substring(27, 4);
                //LA FECHA DE FABRICACIÓN NO ESTA CONTENIDA EN EL CÓDIGO
                datos_cierres.FechaFab = "-";
            }
            //TAPON JEREZ 2: (00)SSCC 
            if (numcaract == 20 && codigo.Substring(0, 2) == "00")
            {
                //Extraemos el SSCC tras el (00)
                datos_cierres.SSCC = codigo.Substring(2, 18);
            }


            //################# GUALA 1 ###########################################
            //GUALA 1: (02)EAN (37) CANTIDAD (10) LOTE 
            if (numcaract == 31 && codigo.Substring(0, 2) == "02" && codigo.Substring(16, 2) == "37" && codigo.Substring(22, 2) == "10")
            {
                //Extraemos el EAN tras el código (01)
                datos_cierres.ean = codigo.Substring(2, 14);
                //Extraemos la Cantidad tras el (37)
                datos_cierres.Cantidad = codigo.Substring(18, 4);
                //Extraemos el Lote de Fabricación tras el (10)
                datos_cierres.LoteFab = codigo.Substring(24, 7);
                //LA FECHA DE FABRICACIÓN NO ESTA CONTENIDA EN EL CÓDIGO
                datos_cierres.FechaFab = "-";
            }
            //GUALA 2: (00)SSCC 
            if (numcaract == 20 && codigo.Substring(0, 2) == "00")
            {
                //Extraemos el SSCC tras el (00)
                datos_cierres.SSCC = codigo.Substring(2, 18);
            }
            //################# GUALA 2 ###########################################
            //GUALA 1: (02)EAN (11) FECHA (10) LOTE (37) CANTIDAD (94) NUMERO CAJA 
            if (numcaract == 47 && codigo.Substring(0, 2) == "02" && codigo.Substring(16, 2) == "11" && codigo.Substring(24, 2) == "10" && codigo.Substring(37, 2) == "37" && codigo.Substring(43, 2) == "94")
            {
                //Extraemos el EAN tras el código (01)
                datos_cierres.ean = codigo.Substring(2, 14);
                //Extraemos la Fecha de fabricación tras el (11)
                datos_cierres.FechaFab = codigo.Substring(18, 6);
                //Extraemos el Lote de Fabricación tras el (10)
                datos_cierres.LoteFab = codigo.Substring(26, 11);
                //Extraemos la Cantidad tras el (37)
                datos_cierres.Cantidad = codigo.Substring(39, 4);
            }
            //GUALA 2: (00)SSCC 
            if (numcaract == 20 && codigo.Substring(0, 2) == "00")
            {
                //Extraemos el SSCC tras el (00)
                datos_cierres.SSCC = codigo.Substring(2, 18);
            }
            //################# GADICORK ###########################################
            //GUALA 1: (02)EAN (11) FECHA (10) LOTE (37) CANTIDAD 
            if (numcaract == 36 && codigo.Substring(0, 2) == "02" && codigo.Substring(16, 2) == "11" && codigo.Substring(24, 2) == "10" && codigo.Substring(30, 2) == "37")
            {
                //Extraemos el EAN tras el código (01)
                datos_cierres.ean = codigo.Substring(2, 14);
                //Extraemos la Fecha de fabricación tras el (11)
                datos_cierres.FechaFab = codigo.Substring(18, 6);
                //Extraemos el Lote de Fabricación tras el (10)
                datos_cierres.LoteFab = codigo.Substring(26, 4);
                //Extraemos la Cantidad tras el (37)
                datos_cierres.Cantidad = codigo.Substring(32, 4);
            }
            //GUALA 2: (00)SSCC 
            if (numcaract == 20 && codigo.Substring(0, 2) == "00")
            {
                //Extraemos el SSCC tras el (00)
                datos_cierres.SSCC = codigo.Substring(2, 18);
            }

            //Buscamos el proveedor en la tabla interna por el EAN
            if (datos_cierres.ean != "")
            {
                //Generamos la lista de valores a filtrar y traer desde la lista de materiales AND, 'Columna', LIKE, 'Codigo Material'
                List<string[]> listavalores = new List<string[]>();
                string[] valores = new string[4];
                valores[0] = "AND";
                valores[1] = "EAN";
                valores[2] = "LIKE";
                valores[3] = " \"" + datos_cierres.ean + "\"";
                listavalores.Add(valores);

                //Llamamos la busqueda del fichero excel
                string result;
                DataSet excelDataSet = new DataSet();
                excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileMateriales, "Cierres", "Codigo;Proveedor;Descripcion;Cantidad".Split(';'), listavalores, out result);
                //MessageBox.Show(result);

                if (excelDataSet.Tables[0].Rows.Count > 0)
                {
                    datos_cierres.refInt = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Codigo"]);
                    datos_cierres.whDescrip = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Descripcion"]);
                    datos_cierres.Proveedor = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Proveedor"]);
                    if (datos_cierres.Cantidad=="") datos_cierres.Cantidad = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Cantidad"]);
                }
                else
                {
                    MessageBox.Show("CODIGO NO ENCONTRADO: Informar al responsable");

                }
            }
            return datos_cierres;
        }

        /// <summary>
        /// Función que filtra por filas en función de cuanto campos de filtrado se han reEncado.
        /// </summary>
        /// <param name="DiaIni"></param>
        /// <param name="DiaFin"></param>
        /// <param name="Turno"></param>
        /// <param name="Lote"></param>
        public static void AplicarFiltros(string DiaIni, string Turno)
        {
            Datos_BusAdmin.valoresAFiltrar.Clear();

            //Filtramos si se ha establecido un turno para filtrado
            if (Turno != "")
            {
                string[] filterturno = new string[4];
                filterturno[0] = "AND";
                filterturno[1] = "Turno";
                filterturno[2] = "=";
                filterturno[3] = "\"" + Turno + "\"";
                Datos_BusAdmin.valoresAFiltrar.Add(filterturno);
            }
            //Filtro SOLO FECHA INICIAL --> Busqueda de un día en concreto
            if (DiaIni != "dd/MM/yyyy")
            {
                string[] filterdate = new string[4];
                filterdate[0] = "AND";
                filterdate[1] = "Fecha";
                filterdate[2] = "=";
                filterdate[3] = "Format('" + DiaIni + "','dd/MM/yyyy')";
                Datos_BusAdmin.valoresAFiltrar.Add(filterdate);
            }

        }


        //######################### CARGA EN PARALELO #########################
        /// <summary>
        /// Carga de datos Despaletizador.
        /// </summary>
        /// <param name="datos">DataSet del despaletizador</param>
        /// <param name="estado">Array de booleanos de la carga del fichero</param>
        /// <param name="filtros">Array de string que indica el filtro que se ha de aplicar</param>
        public static void CargaDespaletizador(DataSet datos, bool[] estado, string[,] filtros)
        {
            //Se divide la carga en 4 partes --> 
            try
            {
                Array.Clear(estado, 0, 4);
                datos.Reset();

                //ITERACIÓN 1
                //Inicio
                Thread ThreadUno = new Thread(() =>
                {
                    //filtros[0] --> Debe de ser el nombre de la pestaña
                    //filtros[X] --> Filtros de la pestaña a emplear
                    //RecargarDataSet(MaquinaLinea.FileDespaletizador, filtros[0, 0], filtros[0, 1], datos, filtros[0, 0]);
                    estado[0] = true;
                });
                ThreadUno.Start();
                //FIN ITERACIÓN 1

                //ITERACIÓN 2
                //Botellas
                while (!estado[0]) { };
                Thread ThreadDos = new Thread(() =>
                {
                    RecargarDataSet(MaquinaLinea.FileDespaletizador, filtros[1, 0], filtros[1, 1], datos, filtros[1, 0]);
                    estado[1] = true;
                });
                ThreadDos.Start();
                //FIN ITERACIÓN 2

                //ITERACIÓN 3
                //Cierres
                while (!estado[1]) { };
                Thread ThreadTres = new Thread(() =>
                {
                    RecargarDataSet(MaquinaLinea.FileDespaletizador, filtros[2, 0], filtros[2, 1], datos, filtros[2, 0]);
                    RecargarDataSet(MaquinaLinea.FileDespaletizador, filtros[3, 0], filtros[3, 1], datos, filtros[3, 0]);
                    estado[2] = true;
                });
                ThreadTres.Start();
                //FIN ITERACIÓN 3

                //ITERACIÓN 4
                //Roturas ; Comentarios
                while (!estado[2]) { };
                Thread ThreadCuatro = new Thread(() =>
                {
                    RecargarDataSet(MaquinaLinea.FileDespaletizador, filtros[4, 0], filtros[4, 1], datos, filtros[4, 0]);
                    RecargarDataSet(MaquinaLinea.FileDespaletizador, filtros[5, 0], filtros[5, 1], datos, filtros[5, 0]);
                    estado[3] = true;
                });
                ThreadCuatro.Start();
                //FIN ITERACIÓN 4
                datos.AcceptChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        } //// CARGA DESPALETIZADOR - FIN ////
        /// <summary>
        /// Carga de datos Encajonadora.
        /// </summary>
        /// <param name="datos">DataSet de la Encajonadora</param>
        /// <param name="estado">Array de booleanos de la carga del fichero</param>
        /// <param name="filtros">Array de string que indica el filtro que se ha de aplicar</param>
        public static void CargaLlenadora(DataSet datos, bool[] estado, string[,] filtros)
        {
            //Se divide la carga en 4 partes --> 
            try
            {
                Array.Clear(estado, 0, 4);
                datos.Reset();

                //ITERACIÓN 1
                //Inicio, Presion
                Thread ThreadUno = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[0, 0], filtros[0, 1], datos, filtros[0, 0]);
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[1, 0], filtros[1, 1], datos, filtros[1, 0]);
                    estado[0] = true;
                });
                ThreadUno.Start();
                //FIN ITERACIÓN 1

                //ITERACIÓN 2
                //TempEncajonadora, TempCaldera, Registro
                while (!estado[0]) { };
                Thread ThreadDos = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[2, 0], filtros[2, 1], datos, filtros[2, 0]);
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[3, 0], filtros[3, 1], datos, filtros[3, 0]);
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[4, 0], filtros[4, 1], datos, filtros[4, 0]);
                    estado[1] = true;
                });
                ThreadDos.Start();
                //FIN ITERACIÓN 2

                //ITERACIÓN 3
                //RegistroParada, Control30min, VerificacionCierreVolumen, ControlVolumen
                while (!estado[1]) { };
                Thread ThreadTres = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[5, 0], filtros[5, 1], datos, filtros[5, 0]);
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[6, 0], filtros[6, 1], datos, filtros[6, 0]);
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[7, 0], filtros[7, 1], datos, filtros[7, 0]);
                    estado[2] = true;
                });
                ThreadTres.Start();
                //FIN ITERACIÓN 3

                //ITERACIÓN 4
                //Torquimetro, Roturas, Comentarios
                while (!estado[2]) { };
                Thread ThreadCuatro = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[8, 0], filtros[8, 1], datos, filtros[8, 0]);
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[9, 0], filtros[9, 1], datos, filtros[9, 0]);
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[10, 0], filtros[10, 1], datos, filtros[10, 0]);
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[11, 0], filtros[11, 1], datos, filtros[11, 0]);
                    estado[3] = true;
                });
                ThreadCuatro.Start();
                //FIN ITERACIÓN 4
                datos.AcceptChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        } //// CARGA Encajonadora - FIN ////
        /// <summary>
        /// Carga de datos Etiquetadora.
        /// </summary>
        /// <param name="datos">DataSet de la etiquetadora</param>
        /// <param name="estado">Array de booleanos de la carga del fichero</param>
        /// <param name="filtros">Array de string que indica el filtro que se ha de aplicar</param>
        public static void CargaEtiquetadora(DataSet datos, bool[] estado, string[,] filtros)
        {
            //Se divide la carga en 4 partes --> 
            try
            {
                Array.Clear(estado, 0, 4);
                datos.Reset();

                //ITERACIÓN 1
                //Inicio, Registro
                Thread ThreadUno = new Thread(() =>
                {
                    //    RecargarDataSet(MaquinaLinea.FileEtiquetadora, filtros[0, 0], filtros[0, 1], datos, filtros[0, 0]);
                    //    RecargarDataSet(MaquinaLinea.FileEtiquetadora, filtros[1, 0], filtros[1, 1], datos, filtros[1, 0]);
                    estado[0] = true;
                });
                ThreadUno.Start();
                while (!estado[0]) { };
                //FIN ITERACIÓN 1

                //ITERACIÓN 2
                //RegistroParada
                Thread ThreadDos = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileEtiquetadora, filtros[2, 0], filtros[2, 1], datos, filtros[2, 0]);
                    estado[1] = true;
                });
                ThreadDos.Start();
                while (!estado[1]) { };
                //FIN ITERACIÓN 2

                //ITERACIÓN 3
                //Control30min; VisionArtificial
                Thread ThreadTres = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileEtiquetadora, filtros[3, 0], filtros[3, 1], datos, filtros[3, 0]);
                    //RecargarDataSet(MaquinaLinea.FileEtiquetadora, filtros[4, 0], filtros[4, 1], datos, filtros[4, 0]);
                    estado[2] = true;
                });
                ThreadTres.Start();
                while (!estado[2]) { };
                //FIN ITERACIÓN 3

                //ITERACIÓN 4
                //Roturas ; Comentarios
                Thread ThreadCuatro = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileEtiquetadora, filtros[5, 0], filtros[5, 1], datos, filtros[5, 0]);
                    //RecargarDataSet(MaquinaLinea.FileEtiquetadora, filtros[6, 0], filtros[6, 1], datos, filtros[6, 0]);
                    estado[3] = true;
                });
                ThreadCuatro.Start();
                //FIN ITERACIÓN 4
                datos.AcceptChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        } //// CARGA ETIQUETADORA - FIN ////
          /// <summary>
          /// Carga de datos Encajonadora.
          /// </summary>
          /// <param name="datos">DataSet de la encajonadora</param>
          /// <param name="estado">Array de booleanos de la carga del fichero</param>
          /// <param name="filtros">Array de string que indica el filtro que se ha de aplicar</param>
        public static void CargaEncajonadora(DataSet datos, bool[] estado, string[,] filtros)
        {
            //Se divide la carga en 4 partes --> 
            try
            {
                Array.Clear(estado, 0, 4);
                datos.Reset();

                //ITERACIÓN 1
                //Inicio, Registro
                Thread ThreadUno = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[0, 0], filtros[0, 1], datos, filtros[0, 0]);
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[1, 0], filtros[1, 1], datos, filtros[1, 0]);
                    estado[0] = true;
                });
                ThreadUno.Start();
                while (!estado[0]) { };
                //FIN ITERACIÓN 1

                //ITERACIÓN 2
                //RegistroParada
                Thread ThreadDos = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[2, 0], filtros[2, 1], datos, filtros[2, 0]);
                    estado[1] = true;
                });
                ThreadDos.Start();
                while (!estado[1]) { };
                //FIN ITERACIÓN 2

                //ITERACIÓN 3
                //Roturas
                Thread ThreadTres = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[3, 0], filtros[3, 1], datos, filtros[3, 0]);
                    estado[2] = true;
                });
                ThreadTres.Start();
                while (!estado[2]) { };
                //FIN ITERACIÓN 3

                //ITERACIÓN 4
                //Comentarios
                Thread ThreadCuatro = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileEncajonadora, filtros[4, 0], filtros[4, 1], datos, filtros[4, 0]);
                    estado[3] = true;
                });
                ThreadCuatro.Start();
                //FIN ITERACIÓN 4
                datos.AcceptChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        } //// CARGA Encajonadora - FIN ////


        /// <summary>
        /// Función que carga los valores en un DataSet asignando un nombre a la tabla.
        /// </summary>
        /// <param name="ruta">Ruta del fichero que se va a buscar</param>
        /// <param name="Hoja">Hoja del excel que donde se extraen los datos</param>
        /// <param name="Columnas">Columnas que se van a mortar en pantalla segun el tipo de filto</param>
        /// <param name="datos"></param>
        /// <param name="nombre"></param>
        public static void RecargarDataSet(string ruta, string Hoja, string Columnas, DataSet datos, string nombre)
        {
            DataSet excelDataSet = new DataSet();
            excelDataSet.Reset();
            //DataTable Operación de copia
            DataTable copy = new DataTable();
            copy.Reset();
            string result;
            //List<string[]> valoresAFiltrar = dgvSelectFiltro.DataSource;
            excelDataSet = ExcelUtiles.LeerFicheroExcel(ruta, Hoja, Columnas.Split(';'), Datos_BusAdmin.valoresAFiltrar, out result);
            //MessageBox.Show(result.ToString());
            try
            {
                //Asociamos el nombre de la tabla para despues copiarla con nombre al 
                excelDataSet.Tables[0].TableName = nombre;
                copy = excelDataSet.Tables[0].Copy();
                datos.Tables.Add(copy);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }
        }

        public static void Filtros_Des(string[,] filtros)
        {
            Array.Clear(filtros, 0, filtros.GetLength(0) * filtros.GetLength(1));

            //Declaración de todos los filtros
            filtros[0, 0] = "Inicio";
            filtros[0, 1] = "FORMAT(Fecha, 'dd/MM/yyyy') as Fecha;Hora;Responsable;Maquinista;Turno;Limpio;Protecciones;Cuter;Herramientas;CambioTurno";
            filtros[1, 0] = "Botellas";
            filtros[1, 1] = "Descripcion;Proveedor;LoteFab";
            filtros[2, 0] = "Cierres";
            filtros[2, 1] = "Descripcion;Proveedor;LoteFab";
            filtros[3, 0] = "RegistroParada";
            filtros[3, 1] = "ParoDesde;ParoHasta;Motivo";
            filtros[4, 0] = "Roturas";
            filtros[4, 1] = "BotRotas;NumAprox;Area;Trabajador;Confirmacion;Maquina";
            filtros[5, 0] = "Comentarios";
            filtros[5, 1] = "Comentarios";
        }//FIN FILTROS
        public static void Filtros_Llen(string[,] filtros)
        {
            Array.Clear(filtros, 0, filtros.GetLength(0) * filtros.GetLength(1));

            //Declaración de todos los filtros
            filtros[0, 0] = "Inicio";
            filtros[0, 1] = "FORMAT(Fecha, 'dd/MM/yyyy') as Fecha;Hora;Responsable;Maquinista;Turno;Limpio;Protecciones;Cuter;Herramientas;Caños;NumeroCaños;Sensor;PresionAlarma;EnjuagadoraEncajonadora;Taponadora;Capsuladora;Transportadora;CambioTurno";
            filtros[1, 0] = "Presion";
            filtros[1, 1] = "HoraMedida;MedidaPresion;Estado";
            filtros[2, 0] = "TemperaturaEncajonadora";
            filtros[2, 1] = "HoraInicial;TemperaturaInicial;HoraFinal;TemperaturaFinal;Comentarios";
            filtros[3, 0] = "TemperaturaCaldera";
            filtros[3, 1] = "HoraInicial;TemperaturaInicial;Comentarios";
            filtros[4, 0] = "Registro";
            filtros[4, 1] = "Barra1;Deposito1;Frio1;NBotellas1;Barra2;Deposito2;Frio2;NBotellas2;NBotellasTotal;Orden;Producto";
            filtros[5, 0] = "RegistroParada";
            filtros[5, 1] = "ParoDesde;ParoHasta;Motivo";
            filtros[6, 0] = "Control30min";
            filtros[6, 1] = "HoraControl;ControlCierre;Volumen;CuelloBoca;Comentarios";
            filtros[7, 0] = "VerificacionCierreVolumen";
            filtros[7, 1] = "HoraVerificacion;Cierre;Proveedor;SensorSuperior;NivelVolumen;Comentarios";
            filtros[8, 0] = "ControlVolumen";
            filtros[8, 1] = "Hora;Producto;Media;DesviacionTipica;Varianza;RealDecreto;BOE";
            filtros[9, 0] = "Torquimetro";
            filtros[9, 1] = "Hora;TipoCierre;Proveedores;C1;C2;C3;C4;C5;C6;C7;C8;C9;C10;C11;C12;Comentarios";
            filtros[10, 0] = "Roturas";
            filtros[10, 1] = "BotRotas;NumAprox;Area;Trabajador;Confirmacion;Maquina";
            filtros[11, 0] = "Comentarios";
            filtros[11, 1] = "Comentarios";

        }//FIN FILTROS
        public static void Filtros_Etiq(string[,] filtros)
        {
            Array.Clear(filtros, 0, filtros.GetLength(0) * filtros.GetLength(1));

            //Declaración de todos los filtros
            filtros[0, 0] = "Inicio";
            filtros[0, 1] = "FORMAT(Fecha, 'dd/MM/yyyy') as Fecha;Hora;Responsable;Maquinista;Turno;Limpio;Protecciones;Cuter;Herramientas;CambioTurno";
            filtros[1, 0] = "Registro";
            filtros[1, 1] = "Hora;Lote;Orden;Formato;Cliente;Producto;NBotellas;Graduacion;Inicio;Fin;InicioCambio";
            filtros[2, 0] = "RegistroParada";
            filtros[2, 1] = "ParoDesde;ParoHasta;Motivo";
            filtros[3, 0] = "Control30min";
            filtros[3, 1] = "HoraControl;ControlEtiqueta;Comentarios";
            filtros[4, 0] = "VisionArtificial";
            filtros[4, 1] = "Hora1;Revision1;Estado1;Revision2;Estado2;Revision3;Estado3;Revision4;Estado4;Comentarios";
            filtros[5, 0] = "Roturas";
            filtros[5, 1] = "Hora;BotRotas;NumAprox;Area;Trabajador;Confirmacion;Maquina";
            filtros[6, 0] = "Comentarios";
            filtros[6, 1] = "Hora;Comentarios";

        }//FIN FILTROS
        public static void Filtros_Enc(string[,] filtros)
        {
            Array.Clear(filtros, 0, filtros.GetLength(0) * filtros.GetLength(1));

            //Declaración de todos los filtros
            filtros[0, 0] = "Inicio";
            filtros[0, 1] = "FORMAT(Fecha, 'dd/MM/yyyy') as Fecha;Hora;Responsable;Maquinista;Turno;Limpio;Protecciones;Cuter;Herramientas;CambioTurno";
            filtros[1, 0] = "Registro";
            filtros[1, 1] = "Lote;NCajas;Orden;Cliente;Producto";
            filtros[2, 0] = "RegistroParada";
            filtros[2, 1] = "ParoDesde;ParoHasta;Motivo";
            filtros[3, 0] = "Roturas";
            filtros[3, 1] = "BotRotas;NumAprox;Area;Trabajador;Confirmacion;Maquina";
            filtros[4, 0] = "Comentarios";
            filtros[4, 1] = "Comentarios";
        }//FIN FILTROS
         //################## END PARSING    ####################################################################
    }
}


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
        //################## END PARSING    ####################################################################
    }
}


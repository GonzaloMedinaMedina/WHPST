﻿using System;
using System.Collections.Generic;
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

namespace WHPS.Etiquetadora
{
    class Apps_Etiquetadora
    {

        public static Datos_Volumen ParsingTablasVolumen(string capacidad, string graduacion, string temperatura)
        {
            //Variables de trabajo
            Datos_Volumen datos_volumen = new Datos_Volumen();
            datos_volumen.capacidad = capacidad;
            datos_volumen.graduacion = graduacion;
            datos_volumen.temperatura = "T" + temperatura;

            //Buscamos el proveedor en la tabla interna por el EAN
            if (datos_volumen.capacidad != "")
            {
                //Generamos la lista de valores a filtrar y traer desde la lista de materiales AND, 'Columna', LIKE, 'Codigo Material'
                List<string[]> listavalores = new List<string[]>();
                string[] valores = new string[4];
                valores[0] = "AND";
                valores[1] = "Capacidad";
                valores[2] = "LIKE";
                valores[3] = " \"" + datos_volumen.capacidad + "\"";
                listavalores.Add(valores);

                //Llamamos la busqueda del fichero excel
                string result;
                DataSet excelDataSet = new DataSet();
                excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileTablas, datos_volumen.graduacion, "T10;T11;T12;T13;T14;T15;T16;T17;T18;T19;T20;T21;T22;T23;T24;T25;T26;T27;T28;T29;T30".Split(';'), listavalores, out result);
                //MessageBox.Show(result);

                if (excelDataSet.Tables[0].Rows.Count > 0)
                {
                    datos_volumen.Volumen = Convert.ToString(excelDataSet.Tables[0].Rows[0][datos_volumen.temperatura]);
                }
                else
                {
                    MessageBox.Show("CODIGO NO ENCONTRADO: Informar al responsable");

                }

            }
            MaquinaLinea.VolumenTabla = datos_volumen.Volumen;
            return datos_volumen;
        }
        public static Datos_Volumen ParsingEstadoVolumen(decimal error, string capacidad)
        {
            //Variables de trabajo
            Datos_Volumen datos_volumen = new Datos_Volumen();
            datos_volumen.capacidad = capacidad;
            datos_volumen.error = error;

            //DETECTAMOS SI EL VOLUMEN CUMPLE CON EL ERROR PERMITIDO
            //Si la botella es de MENOS de 1L 
            // -1.2% < ESTADO VERDE < 1.2%
            // -3% < ESTADO NARANJA < 3% Y -1.2% > ESTADO NARANJA > 1.2%
            // -3% > ESTADO ROJO > 3%

            if (Convert.ToInt32(capacidad) < 1000)
            {

                //ESTADO VERDE
                if (((error * 100) < (Convert.ToDecimal(capacidad) * Convert.ToDecimal(Properties.Settings.Default.CV_Tol1_Menor1LTB))) || ((error * 100) > (Convert.ToDecimal(capacidad) * -Convert.ToDecimal(Properties.Settings.Default.CV_Tol1_Menor1LTB))))
                {
                    datos_volumen.estado = "Verde";
                }
                //ESTADO NARANJA
                if (((error * 100 > (Convert.ToDecimal(capacidad) * Convert.ToDecimal(Properties.Settings.Default.CV_Tol1_Menor1LTB))) && (error * 100 < (Convert.ToDecimal(capacidad) * Convert.ToDecimal(Properties.Settings.Default.CV_Tol2_Menor1LTB)))) || ((error * 100 < (Convert.ToDecimal(capacidad) * -Convert.ToDecimal(Properties.Settings.Default.CV_Tol1_Menor1LTB))) && (error * 100 > (Convert.ToDecimal(capacidad) * -Convert.ToDecimal(Properties.Settings.Default.CV_Tol2_Menor1LTB)))))
                {
                    datos_volumen.estado = "Naranja";
                }
                //ESTADO ROJO
                if (((error * 100) > (Convert.ToDecimal(capacidad) * Convert.ToDecimal(Properties.Settings.Default.CV_Tol2_Menor1LTB))) || ((error * 100) < (Convert.ToDecimal(capacidad) * -Convert.ToDecimal(Properties.Settings.Default.CV_Tol2_Menor1LTB))))
                {
                    datos_volumen.estado = "Rojo";
                }
            }

            //Si la botella es de 1L o MAS
            // -1.2% < ESTADO VERDE < 1.2%
            // -1.5% < ESTADO NARANJA < 1.5% Y -1.2% > ESTADO NARANJA > 1.2%
            // -1.5% > ESTADO ROJO > 1.5%
            if (Convert.ToInt32(capacidad) < 1000)
            {

                //ESTADO VERDE
                if (((error * 100) < (Convert.ToDecimal(capacidad) * Convert.ToDecimal(Properties.Settings.Default.CV_Tol1_Mayor1LTB))) || ((error * 100) > (Convert.ToDecimal(capacidad) * -Convert.ToDecimal(Properties.Settings.Default.CV_Tol1_Mayor1LTB))))
                {
                    datos_volumen.estado = "Verde";
                }
                //ESTADO NARANJA
                if (((error * 100 > (Convert.ToDecimal(capacidad) * Convert.ToDecimal(Properties.Settings.Default.CV_Tol1_Mayor1LTB))) && (error * 100 < (Convert.ToDecimal(capacidad) * Convert.ToDecimal(Properties.Settings.Default.CV_Tol2_Mayor1LTB)))) || ((error * 100 < (Convert.ToDecimal(capacidad) * -Convert.ToDecimal(Properties.Settings.Default.CV_Tol1_Mayor1LTB))) && (error * 100 > (Convert.ToDecimal(capacidad) * -Convert.ToDecimal(Properties.Settings.Default.CV_Tol2_Mayor1LTB)))))
                {
                    datos_volumen.estado = "Naranja";
                }
                //ESTADO ROJO
                if (((error * 100) > (Convert.ToDecimal(capacidad) * Convert.ToDecimal(Properties.Settings.Default.CV_Tol2_Mayor1LTB))) || ((error * 100) < (Convert.ToDecimal(capacidad) * -Convert.ToDecimal(Properties.Settings.Default.CV_Tol2_Mayor1LTB))))
                {
                    datos_volumen.estado = "Rojo";
                }
            }
            MaquinaLinea.estado = datos_volumen.estado;
            return datos_volumen;
        }


        //######################   APARTADO DE ALARMAS   #####################
        /*public static void AlarmaControl30min()
        {
            MaquinaLinea.AnuladorAlarma = false;
            if (MaquinaLinea.numlin == 2)
            {
                if (Properties.Settings.Default.AlarmaC30LlenL2 == false) Properties.Settings.Default.AlarmaC30LlenL2 = true;
                //else MessageBox.Show("Recuerda realizar el registro haciendo click en el boton de control cada 30min que se encuentra en el form de trabajo correspondiente a su maquina.");
            }

            if (MaquinaLinea.numlin == 3)
            {
                if (Properties.Settings.Default.AlarmaC30LlenL3 == false) Properties.Settings.Default.AlarmaC30LlenL3 = true;
                //else MessageBox.Show("Recuerda realizar el registro haciendo click en el boton de control cada 30min que se encuentra en el form de trabajo correspondiente a su maquina.");
            }
            if (MaquinaLinea.numlin == 5)
            {
                if (Properties.Settings.Default.AlarmaC30LlenL3 == false) Properties.Settings.Default.AlarmaC30LlenL3 = true;
                //else MessageBox.Show("Recuerda realizar el registro haciendo click en el boton de control cada 30min que se encuentra en el form de trabajo correspondiente a su maquina.");
            }
            Properties.Settings.Default.Save();
        }*/
        //########################################################################


        /// <summary>
        /// Función que filtra por filas en función de cuanto campos de filtrado se han rellenado.
        /// </summary>
        /// <param name="DiaIni"></param>
        /// <param name="DiaFin"></param>
        /// <param name="Turno"></param>
        /// <param name="Lote"></param>
        public static void AplicarFiltros(string DiaIni, string Turno)
        {
            //Limpiamos valores a filtrar
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
                    //RecargarDataSet(MaquinaLinea.FileDespaletizador, filtros[1, 0], filtros[1, 1], datos, filtros[1, 0]);
                    estado[1] = true;
                });
                ThreadDos.Start();
                //FIN ITERACIÓN 2

                //ITERACIÓN 3
                //Cierres
                while (!estado[1]) { };
                Thread ThreadTres = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileDespaletizador, filtros[2, 0], filtros[2, 1], datos, filtros[2, 0]);
                    //RecargarDataSet(MaquinaLinea.FileDespaletizador, filtros[3, 0], filtros[3, 1], datos, filtros[3, 0]);
                    estado[2] = true;
                });
                ThreadTres.Start();
                //FIN ITERACIÓN 3

                //ITERACIÓN 4
                //Roturas ; Comentarios
                while (!estado[2]) { };
                Thread ThreadCuatro = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileDespaletizador, filtros[4, 0], filtros[4, 1], datos, filtros[4, 0]);
                    //RecargarDataSet(MaquinaLinea.FileDespaletizador, filtros[5, 0], filtros[5, 1], datos, filtros[5, 0]);
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
        /// Carga de datos Llenadora.
        /// </summary>
        /// <param name="datos">DataSet de la llenadora</param>
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
                    //RecargarDataSet(MaquinaLinea.FileLlenadora, filtros[0, 0], filtros[0, 1], datos, filtros[0, 0]);
                    //RecargarDataSet(MaquinaLinea.FileLlenadora, filtros[1, 0], filtros[1, 1], datos, filtros[1, 0]);
                    estado[0] = true;
                });
                ThreadUno.Start();
                //FIN ITERACIÓN 1

                //ITERACIÓN 2
                //TempLlenadora, TempCaldera, Registro
                while (!estado[0]) { };
                Thread ThreadDos = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileLlenadora, filtros[2, 0], filtros[2, 1], datos, filtros[2, 0]);
                    //RecargarDataSet(MaquinaLinea.FileLlenadora, filtros[3, 0], filtros[3, 1], datos, filtros[3, 0]);
                    //RecargarDataSet(MaquinaLinea.FileLlenadora, filtros[4, 0], filtros[4, 1], datos, filtros[4, 0]);
                    estado[1] = true;
                });
                ThreadDos.Start();
                //FIN ITERACIÓN 2

                //ITERACIÓN 3
                //RegistroParada, Control30min, VerificacionCierreVolumen, ControlVolumen
                while (!estado[1]) { };
                Thread ThreadTres = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileLlenadora, filtros[5, 0], filtros[5, 1], datos, filtros[5, 0]);
                    //RecargarDataSet(MaquinaLinea.FileLlenadora, filtros[6, 0], filtros[6, 1], datos, filtros[6, 0]);
                    //RecargarDataSet(MaquinaLinea.FileLlenadora, filtros[7, 0], filtros[7, 1], datos, filtros[7, 0]);
                    estado[2] = true;
                });
                ThreadTres.Start();
                //FIN ITERACIÓN 3

                //ITERACIÓN 4
                //Torquimetro, Roturas, Comentarios
                while (!estado[2]) { };
                Thread ThreadCuatro = new Thread(() =>
                {
                    //RecargarDataSet(MaquinaLinea.FileLlenadora, filtros[8, 0], filtros[8, 1], datos, filtros[8, 0]);
                    //RecargarDataSet(MaquinaLinea.FileLlenadora, filtros[9, 0], filtros[9, 1], datos, filtros[9, 0]);
                    //RecargarDataSet(MaquinaLinea.FileLlenadora, filtros[10, 0], filtros[10, 1], datos, filtros[10, 0]);
                    //RecargarDataSet(MaquinaLinea.FileLlenadora, filtros[11, 0], filtros[11, 1], datos, filtros[11, 0]);
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
        } //// CARGA LLENADORA - FIN ////
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
                    RecargarDataSet(MaquinaLinea.FileEtiquetadora, filtros[0, 0], filtros[0, 1], datos, filtros[0, 0]);
                    RecargarDataSet(MaquinaLinea.FileEtiquetadora, filtros[1, 0], filtros[1, 1], datos, filtros[1, 0]);
                    estado[0] = true;
                });
                ThreadUno.Start();
                while (!estado[0]) { };
                //FIN ITERACIÓN 1

                //ITERACIÓN 2
                //RegistroParada
                Thread ThreadDos = new Thread(() =>
                {
                    RecargarDataSet(MaquinaLinea.FileEtiquetadora, filtros[2, 0], filtros[2, 1], datos, filtros[2, 0]);
                    estado[1] = true;
                });
                ThreadDos.Start();
                while (!estado[1]) { };
                //FIN ITERACIÓN 2

                //ITERACIÓN 3
                //Control30min; VisionArtificial
                Thread ThreadTres = new Thread(() =>
                {
                    RecargarDataSet(MaquinaLinea.FileEtiquetadora, filtros[3, 0], filtros[3, 1], datos, filtros[3, 0]);
                    RecargarDataSet(MaquinaLinea.FileEtiquetadora, filtros[4, 0], filtros[4, 1], datos, filtros[4, 0]);
                    estado[2] = true;
                });
                ThreadTres.Start();
                while (!estado[2]) { };
                //FIN ITERACIÓN 3

                //ITERACIÓN 4
                //Roturas ; Comentarios
                Thread ThreadCuatro = new Thread(() =>
                {
                    RecargarDataSet(MaquinaLinea.FileEtiquetadora, filtros[5, 0], filtros[5, 1], datos, filtros[5, 0]);
                    RecargarDataSet(MaquinaLinea.FileEtiquetadora, filtros[6, 0], filtros[6, 1], datos, filtros[6, 0]);
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


        /// <summary>
        /// Funciones aplican el filtro avanzado o el basico modificando las columnas que se muestran.
        /// </summary>
        /// <param name="filtros"></param>
        /// <param name="tipo"></param>
        public static void Filtros_Des(string[,] filtros)
        {
            Array.Clear(filtros, 0, filtros.GetLength(0) * filtros.GetLength(1));

            //Declaración de todos los filtros
            filtros[0, 0] = "Inicio";
            filtros[0, 1] = "FORMAT(Fecha, 'dd/MM/yyyy') as Fecha;Hora;Responsable;Maquinista;Turno;Limpio;Protecciones;Cuter;Herramientas;CambioTurno";
            filtros[1, 0] = "Botellas";
            filtros[1, 1] = "Hora;Descripcion;Proveedor;LoteFab";
            filtros[2, 0] = "Cierres";
            filtros[2, 1] = "Hora;Descripcion;Proveedor;LoteFab";
            filtros[3, 0] = "RegistroParada";
            filtros[3, 1] = "ParoDesde;ParoHasta;Motivo;TiempoParada";
            filtros[4, 0] = "Roturas";
            filtros[4, 1] = "Hora;BotRotas;NumAprox;Area;Trabajador;Confirmacion;Maquina";
            filtros[5, 0] = "Comentarios";
            filtros[5, 1] = "Hora;Comentarios";
        }//FIN FILTROS
        public static void Filtros_Llen(string[,] filtros)
        {
            Array.Clear(filtros, 0, filtros.GetLength(0) * filtros.GetLength(1));

            //Declaración de todos los filtros
            filtros[0, 0] = "Inicio";
            filtros[0, 1] = "FORMAT(Fecha, 'dd/MM/yyyy') as Fecha;Hora;Responsable;Maquinista;Turno;Limpio;Protecciones;Cuter;Herramientas;Caños;NumeroCaños;Sensor;PresionAlarma;EnjuagadoraLlenadora;Taponadora;Capsuladora;Transportadora;CambioTurno";
            filtros[1, 0] = "Presion";
            filtros[1, 1] = "HoraMedida;MedidaPresion;Estado";
            filtros[2, 0] = "TemperaturaLlenadora";
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
            filtros[1, 1] = "Lote;NBotellas;Orden;Cliente;Producto;RetiradaFrontal;RetiradaContra";
            filtros[2, 0] = "RegistroParada";
            filtros[2, 1] = "ParoDesde;ParoHasta;Motivo";
            filtros[3, 0] = "Control30min";
            filtros[3, 1] = "HoraControl;ControlEtiqueta;Comentarios";
            filtros[4, 0] = "VisionArtificial";
            filtros[4, 1] = "Hora1;Revision1;Estado1;Revision2;Estado2;Revision3;Estado3;Revision4;Estado4;Comentarios";
            filtros[5, 0] = "Roturas";
            filtros[5, 1] = "BotRotas;NumAprox;Area;Trabajador;Confirmacion;Maquina";
            filtros[6, 0] = "Comentarios";
            filtros[6, 1] = "Comentarios";

        }//FIN FILTROS
        public static void Filtros_Enc(string[,] filtros)
        {
            Array.Clear(filtros, 0, filtros.GetLength(0) * filtros.GetLength(1));

            //Declaración de todos los filtros
            filtros[0, 0] = "Inicio";
            filtros[0, 1] = "FORMAT(Fecha, 'dd/MM/yyyy') as Fecha;Hora;Responsable;Maquinista;Turno;Limpio;Protecciones;Cuter;Herramientas;CambioTurno";
            filtros[1, 0] = "Registro";
            filtros[1, 1] = "Hora;Lote;Orden;Formato;Cliente;Producto;NCajas;Inicio;Fin;InicioCambio";
            filtros[2, 0] = "RegistroParada";
            filtros[2, 1] = "ParoDesde;ParoHasta;Motivo";
            filtros[3, 0] = "Roturas";
            filtros[3, 1] = "Hora;BotRotas;NumAprox;Area;Trabajador;Confirmacion;Maquina";
            filtros[4, 0] = "Comentarios";
            filtros[4, 1] = "Hora;Comentarios";
        }//FIN FILTROS
         //#####################################################################
    }
}


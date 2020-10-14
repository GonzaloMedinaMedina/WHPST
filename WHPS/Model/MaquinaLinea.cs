using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WHPS.Utiles;
using System.Windows.Forms;

namespace WHPS.Model
{
    public static class MaquinaLinea
    {
        #region Atributos
        //########### VARIABLES GLOBALES ###########
        public static Color COLOR1 = Properties.Settings.Default.COLOR1;
        public static bool CARGANDO = false;
        public static bool AbrirCambioTurno = false;
        public static string ACTUALIZARPC = "NO";
        //Variable de número de línea
        public static int numlin = 2;

        //Variables de usuarios
        public static string usuario = Properties.Settings.Default.Usuario;
        public static string UsuarioOficina = Properties.Settings.Default.UsuarioOfiina;
        public static string ContraseñaOficina = Properties.Settings.Default.ContraseñaOfiina;
        public static string UsuarioAdministracion = Properties.Settings.Default.UsuarioAdministracion;
        public static string ContraseñaAdministracion = Properties.Settings.Default.ContraseñaAdministracion;
        public static string UsuarioEncargado = Properties.Settings.Default.UsuarioEncargado;
        public static string ContraseñaEncargado = Properties.Settings.Default.ContraseñaEncargado;
        public static string UsuarioCalidad = Properties.Settings.Default.UsuarioCalidad;
        public static string ContraseñaCalidad = Properties.Settings.Default.ContraseñaCalidad;



        //Variables de personal
        public static string Responsable = "-";
        public static string MDespaletizador = "-";
        public static string MLlenadora = "-";
        public static string MEtiquetadora = "-";
        public static string MEncajonadora = "-";
        public static string ControlCal = "-";

        //Variables del teclado
        public static string Teclado ="";
        public static string Form ="";
        public static string TextBox="";
        public static int TipoTeclado = 0;
        public static bool TecladoAbierto = false;
        public static bool StatusTeclado = false;
        public static VentanaTeclados numberpad2;
        public static bool Password = false;
        public static bool ModoTeclado = false;
        public static int TecladoWindows = 1;
        //Variables BOM
        public static string ReferenciaBOM = "";
        public static string RetornoBOM = "";
        //Variables Parte
        public static string PlantillaParte = "PlantillaParte";
        public static string CarpetaParte = "CarpetaParte";
        public static string FileParte = "FileParte";
        public static string NombreParte ="Parte_" + DateTime.Now.ToString("yyyy.MM.dd") + "_L";
        public static bool Parte_Guardar = false;
        public static bool Parte_Desp = false;
        public static bool Parte_Llen = false;
        public static bool Parte_Etiq = false;
        public static bool Parte_Enc = false;
        //Variables Rotura
        public static string PlantillaRotura = "PlantillaRotura";
        public static string CarpetaRotura = "CarpetaRotura";
        public static string FileRotura = "FileRotura";
        public static string NombreRotura = "Rotura_" + DateTime.Now.ToString("yyyy.MM.dd") + "_L";
        public static bool Rotura_Guardar = false;
        public static bool Rotura_Desp = false;
        public static bool Rotura_Llen = false;
        public static bool Rotura_Etiq = false;
        public static bool Rotura_Enc = false;

        //Valiables de la alarma
        public static string alarmah1 = Properties.Settings.Default.alarmah1;
        public static string alarmam1 = Properties.Settings.Default.alarmam1;
        public static string alarmah2 = Properties.Settings.Default.alarmah2;
        public static string alarmam2 = Properties.Settings.Default.alarmam2;
        public static string alarmah3 = Properties.Settings.Default.alarmah3;
        public static string alarmam3 = Properties.Settings.Default.alarmam3;
        public static bool chalarma = Properties.Settings.Default.chalarma;

        public static bool chalarmaDesL2 = Properties.Settings.Default.chalarmaDesL2;
        public static bool chalarmaDesL3 = Properties.Settings.Default.chalarmaDesL3;
        public static bool chalarmaDesL5 = Properties.Settings.Default.chalarmaDesL5;

        public static bool chalarmaLlenL2 = Properties.Settings.Default.chalarmaLlenL2;
        public static bool chalarmaLlenL3 = Properties.Settings.Default.chalarmaLlenL3;
        public static bool chalarmaLlenL5 = Properties.Settings.Default.chalarmaLlenL5;

        public static bool chalarmaEtiqL2 = Properties.Settings.Default.chalarmaEtiqL2;
        public static bool chalarmaEtiqL3 = Properties.Settings.Default.chalarmaEtiqL3;
        public static bool chalarmaEtiqL5 = Properties.Settings.Default.chalarmaEtiqL5;

        public static bool chalarmaEncL2 = Properties.Settings.Default.chalarmaEncL2;
        public static bool chalarmaEncL3 = Properties.Settings.Default.chalarmaEncL3;
        public static bool chalarmaEncL5 = Properties.Settings.Default.chalarmaEncL5;

        //Variables de fichero
        public static string FileComprobarActualizacion = "ComprobarActualizacion";
        public static string FileMateriales = "Materiales";
        public static string FileBOM = "BOM_Modificado";
        public static string FileBOFL2 = "BOFL2";
        public static string FileBOFL3 = "BOFL3";
        public static string FileBOFL5 = "BOFL5";
        public static string RutaImagen_BOFL2 = ConexionDatos.ObtenerFicheroClave("RutaImagen_BOFL2");
        public static string RutaImagen_BOFL3 = ConexionDatos.ObtenerFicheroClave("RutaImagen_BOFL3");
        public static string RutaImagen_BOFL5 = ConexionDatos.ObtenerFicheroClave("RutaImagen_BOFL5");
        public static string FileTablas = "tablas_ampl";
        public static string FileDespaletizador = "";
        public static string FileLanzador = "";
        public static string FileOperarios = "";
        public static string FileLlenadora = "";
        public static string FileEtiquetadora = "";
        public static string FileEncajonadora = "";
        public static string FileGeneralesLinea = "";
        public static string FileFiltracion = "Filtracion";
        public static string FileActualizacion = ConexionDatos.ObtenerFicheroClave("Actualizacion");
        public static string RutaFolderBOM = ConexionDatos.ObtenerFicheroClave("RutaDocumento_BOM");
        public static string RutaPicutreBOM = ConexionDatos.ObtenerFicheroClave("RutaImagen_BOM");
        public static string RutaPDFLOTE = ConexionDatos.ObtenerFicheroClave("RutaPDF_Lote");
        public static string RutaFolderOrden = ConexionDatos.ObtenerFicheroClave("RutaDocumento_Orden");
        public static string RutaFolderDocLlenadora = ConexionDatos.ObtenerFicheroClave("RutaDocumento_DocuentosLlenadora");
        public static string RutaRED = "Ruta_RED";
        //public static string RutaPicutreBOM = ConexionDatos.ObtenerFicheroClave("RutaImagen_BOM");
        //public static string RutaPDFLOTE = ConexionDatos.ObtenerFicheroClave("RutaPDF_Lote");
        //public static string RutaFolderOrden = ConexionDatos.ObtenerFicheroClave("RutaDocumento_Orden");
        //ROTURA DE BOTELLAS
        public static string RotCodProd = "-";
        public static string RotCodMat = "-";
        public static string RotDescMaterial = "-";
        public static string RotProveedorMaterial = "-";

        //########### VARIABLES MENU DE INICIO ###########
        public static bool SELECTMAQ = false;
        public static string RetornoInicio = "";
        //########### VARIABLES CALIDAD ###########
        public static bool TipoBus;
        public static string BusDia = Properties.Settings.Default.BusDia;
        public static string BusDiaFin = Properties.Settings.Default.BusDiaFin;
        public static string BusLinea = Properties.Settings.Default.BusLinea;
        public static string BusLote = Properties.Settings.Default.BusLote;
        public static string BusTurno = Properties.Settings.Default.BusTurno;

        //########### VARIABLES PRODUCCION ###########
        public static int OBJETIVOL2 = 12600;
        public static int OBJETIVOL3 = 78000;
        public static int OBJETIVOL5 = 78000;
        public static int[] DatosTotalesL2 = new int[5];
        public static int[] DatosTotalesL3 = new int[5];
        public static int[] DatosTotalesL5 = new int[5];
        //########### VARIABLES CAMBIO DE TRUNO ###########
        public static int diaT = Properties.Settings.Default.diaT;
        public static string turno = Properties.Settings.Default.turno;
        public static string Lote = "";
        public static bool switchT = Properties.Settings.Default.switchT;
        public static bool checkL2 = Properties.Settings.Default.checkL2;
        public static bool checkL3 = Properties.Settings.Default.checkL3;
        public static bool checkL5 = Properties.Settings.Default.checkL5;
        public static bool BackL2 = false;
        public static bool BackL3 = false;
        public static bool BackL5 = false;


        public static bool chDesL2 = Properties.Settings.Default.chDesL2;
        public static bool chLlenL2 = Properties.Settings.Default.chLlenL2;
        public static bool chEtiqL2 = Properties.Settings.Default.chEtiqL2;
        public static bool chEncL2 = Properties.Settings.Default.chEncL2;
        public static bool chConL2 = Properties.Settings.Default.chConL2;

        public static bool chDesL3 = Properties.Settings.Default.chDesL3;
        public static bool chLlenL3 = Properties.Settings.Default.chLlenL3;
        public static bool chEtiqL3 = Properties.Settings.Default.chEtiqL3;
        public static bool chEncL3 = Properties.Settings.Default.chEncL3;
        public static bool chConL3 = Properties.Settings.Default.chConL3;
        
        public static bool chDesL5 = Properties.Settings.Default.chDesL5;
        public static bool chLlenL5 = Properties.Settings.Default.chLlenL5;
        public static bool chEtiqL5 = Properties.Settings.Default.chEtiqL5;
        public static bool chEncL5 = Properties.Settings.Default.chEncL5;
        public static bool chConL5 = Properties.Settings.Default.chConL5;

        //########### VARIABLES DESPALETIADOR ###########
        public static string NumBot = "0";

        //Lanzamiento
        public static int MovimientosTurnoAnterior = Properties.Settings.Default.MovimientosTurnoAnterior;
        public static int MovimientosLanzamientoAnterior = Properties.Settings.Default.MovimientosLanzamientoAnterior;
        public static string MensajeAvisoTurno = Properties.Settings.Default.MensajeAvisoTurno;
        public static string AvisoTurnoMañana = Properties.Settings.Default.AvisoTurnoMañana;
        public static string AvisoTurnoTarde = Properties.Settings.Default.AvisoTurnoTarde;
        public static string AvisoTurnoNoche = Properties.Settings.Default.AvisoTurnoNoche;
        public static int TiempoComprobacionAviso = Properties.Settings.Default.TiempoComprobacionAviso;
        public static string CodigoProducto = "";
        public static string ProductoSeleccionadoDespL2 = "";
        public static string ProductoSeleccionadoDespL3 = "";
        public static string ProductoSeleccionadoDespL5 = "";

        //########### VARIABLES LLENADORA ###########
        public static string VolumenTabla = "";
        public static string estado = "";
        public static bool AnuladorAlarma = false;
        public static string CapacidadLlen = "";
        public static string GraduacionLLen = "";


        //Menú de control de temperatura
        public static string TemperaturaInicio = "";
        public static string HoraInicial = "";
        public static string TemperaturaFin = "";
        public static string HoraFin = "";


        //Menú de control de registro
        public static string ReferenciaLlen = "";
        public static string ReferenciaLlenL2 = "";
        public static string ReferenciaLlenL3 = "";
        public static string ReferenciaLlenL5 = "";
        public static string NBotellasLlen = "";
        public static string NBotellasLlenL2 = "";
        public static string NBotellasLlenL3 = "";
        public static string NBotellasLlenL5 = "";
        public static string HInicioLlen = "";
        public static string HInicioLlenL2 = "";
        public static string HInicioLlenL3 = "";
        public static string HInicioLlenL5 = "";
        public static string HFinLlen = "";
        public static string HFinLlenL2 = "";
        public static string HFinLlenL3 = "";
        public static string HFinLlenL5 = "";
        public static string HInicioCambioLlen = "";
        public static string HInicioCambioLlenL2 = "";
        public static string HInicioCambioLlenL3 = "";
        public static string HInicioCambioLlenL5 = "";
        public static string ParoDesdeLlenL2 = "";
        public static string ParoDesdeLlenL3 = "";
        public static string ParoDesdeLlenL5 = "";
        public static string MotivoParoLlenL2 = "";
        public static string MotivoParoLlenL3 = "";
        public static string MotivoParoLlenL5 = "";
        public static string ProductoSeleccionadoLlenL2 = "";
        public static string ProductoSeleccionadoLlenL3 = "";
        public static string ProductoSeleccionadoLlenL5 = "";
        public static string BarraCopiadoLlenL2 = "";
        public static string BarraCopiadoLlenL3 = "";
        public static string BarraCopiadoLlenL5 = "";
        public static string DepCopiadoLlenL2 = "";
        public static string DepCopiadoLlenL3 = "";
        public static string DepCopiadoLlenL5 = "";
        public static string FrioCopiadoLlenL2 = "";
        public static string FrioCopiadoLlenL3 = "";
        public static string FrioCopiadoLlenL5 = "";
        //Control de verificacion de cierre
        public static string CodigoProd = "";

        //########### VARIABLES ETIQUETADORA ###########
        //Menú de control de registro
        public static string HInicioEtiq = "";
        public static string HFinEtiq = "";
        public static string HInicioCambioEtiq = "";
        public static string NBotellasEtiq = "";
        public static string FormatoEtiq = "";
        public static string ProductoSeleccionadoEtiqL2 = "";
        public static string ProductoSeleccionadoEtiqL3 = "";
        public static string ProductoSeleccionadoEtiqL5 = "";
        public static string LoteCopiadoEtiqL2 = "";
        public static string LoteCopiadoEtiqL3 = "";
        public static string LoteCopiadoEtiqL5 = "";

        //########### VARIABLES ENCAJONADORA ###########
        //Menú de control de registro
        public static string HInicioEnc = "";
        public static string HFinEnc = "";
        public static string HInicioCambioEnc = "";
        public static string NCajasEnc = "";
        public static string FormatoEnc = "";
        public static string ProductoSeleccionadoEncL2 = "";
        public static string ProductoSeleccionadoEncL3 = "";
        public static string ProductoSeleccionadoEncL5 = "";
        public static string LoteCopiadoEncL2 = "";
        public static string LoteCopiadoEncL3 = "";
        public static string LoteCopiadoEncL5 = "";
        #endregion Atributos 

        //############################# FUNCIONES QUE GUARDAN FORM COMUNES ############################# 
        /// <summary>
        /// Función que guarda los datos del cambio de turno (Vale para Desp, Etiq, Enc).
        /// </summary>
        /// <param name="maquina">Indica en que ruta se van a guardar los datos</param>
        /// <param name="maquinista">Indica la persona que se encuentra en el puesto</param>
        /// <param name="limpio">Indica si está limpia la maquinaria</param>
        /// <param name="cuter">Indica si se encuentra el cuter en el puesto</param>
        /// <param name="herramientas">Indica si las herramientas necesarias estan en el puesto</param>
        /// <param name="protecciones">Indica si las protecciones estan en un buen estado</param>
        /// <returns></returns>
        public static string GuardarFormCambioTurno(string maquina,string maquinista ,string limpio, string cuter, string herramientas, string protecciones)
        {
            string salida = "";
            try
            {                
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "Fecha", DateTime.Now.ToString("dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Hora", DateTime.Now.ToString("HH:mm:ss") });
                listavalores.Add(new string[2] { "FechaDB", DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Responsable", Responsable });
                listavalores.Add(new string[2] { "Maquinista", maquinista });
                listavalores.Add(new string[2] { "Turno", turno });
                listavalores.Add(new string[2] { "Limpio", limpio });
                listavalores.Add(new string[2] { "Cuter", cuter });
                listavalores.Add(new string[2] { "Herramientas", herramientas });
                listavalores.Add(new string[2] { "Protecciones", protecciones });
                listavalores.Add(new string[2] { "CambioTurno", ChequearCambioTurno(maquina) });

                switch (maquina)
                {
                    case "Despaletizador": maquina = FileDespaletizador; break;
                    case "Etiquetadora":   maquina = FileEtiquetadora;   break;
                    case "Encajonadora":   maquina = FileEncajonadora;   break;
                }

                salida = ExcelUtiles.EscribirFicheroExcel(maquina, "Inicio", listavalores, "Id");
                return salida;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return salida;
            }
        }

        /// <summary>
        /// Función que guarda los datos del cambio de turno (Vale para Desp, Etiq, Enc).
        /// </summary>
        /// <param name="maquina">Indica en que ruta se van a guardar los datos</param>
        /// <param name="maquinista">Indica la persona que se encuentra en el puesto</param>
        /// <param name="limpio">Indica si está limpia la maquinaria</param>
        /// <param name="cuter">Indica si se encuentra el cuter en el puesto</param>
        /// <param name="herramientas">Indica si las herramientas necesarias estan en el puesto</param>
        /// <param name="protecciones">Indica si las protecciones estan en un buen estado</param>
        /// <returns></returns>
        public static string GuardarFormRoturaBotellas(string maquina, string maquinista, string BotRotas, string NumBotRotas, string LimpiezaArea, string LimpiezaTrabajador, string maquina2)
        {
            string salida = "";
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "Fecha", DateTime.Now.ToString("dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Hora", DateTime.Now.ToString("HH:mm:ss") });
                listavalores.Add(new string[2] { "FechaDB", DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Responsable", Responsable });
                listavalores.Add(new string[2] { "Maquinista", maquinista });
                listavalores.Add(new string[2] { "Turno", turno });
                listavalores.Add(new string[2] { "BotRotas", BotRotas });
                listavalores.Add(new string[2] { "NumAprox", NumBotRotas });
                listavalores.Add(new string[2] { "Area", LimpiezaArea });
                listavalores.Add(new string[2] { "Trabajador", LimpiezaTrabajador });
                listavalores.Add(new string[2] { "Confirmacion", Password == true ? "SI" : "NO" });
                listavalores.Add(new string[2] { "Maquina", maquina2 });
                listavalores.Add(new string[2] { "Proveedor", RotProveedorMaterial });
                listavalores.Add(new string[2] { "Descripcion", RotDescMaterial });

                switch (maquina)
                {
                    case "Despaletizador": maquina = FileDespaletizador; break;
                    case "Llenadora": maquina = FileLlenadora; break;
                    case "Etiquetadora": maquina = FileEtiquetadora; break;
                    case "Encajonadora": maquina = FileEncajonadora; break;
                }
                salida = ExcelUtiles.EscribirFicheroExcel(maquina, "Roturas", listavalores, "Id");
                return salida;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return salida;
            }
        }

        public static void ActualizarPuestoTrabajo()
        {
            //Volvemos a True la verificación del Puesto
            switch (numlin)
            {
                case 2:
                    chDesL2 = true;
                    Properties.Settings.Default.chDesL2 = true;
                    break;
                case 3:
                    chDesL3 = true;
                    Properties.Settings.Default.chDesL3 = true;
                    break;
                case 5:
                    chDesL5 = true;
                    Properties.Settings.Default.chDesL5 = true;
                    break;
                default:
                    chDesL2 = false;
                    chDesL3 = false;
                    chDesL5 = false;
                    Properties.Settings.Default.chDesL2 = false;
                    Properties.Settings.Default.chDesL3 = false;
                    Properties.Settings.Default.chDesL5 = false;
                    break;
            }
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
        public static void ActualizarYGuardarValores(string responsableP, string despaletizadorP, string llenadoraP, string etiquetadoraP, string encajadoraP, string controlP, string turnoP, string numLineaP)
        {
            //Filtro Encargado
            List<string[]> valoresAFiltrar = new List<string[]>();
            string[] filterval = new string[4];
            filterval[0] = "AND";
            filterval[1] = "Puesto";
            filterval[2] = "LIKE";
            filterval[3] = "\"Encargado\"";
            valoresAFiltrar.Add(filterval);

            List<string[]> valoresAActualizar = new List<string[]>();
            string[] updateval = new string[2];
            updateval[0] = turnoP;
            updateval[1] = responsableP;
            valoresAActualizar.Add(updateval);
            string salida;
            salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + numlin.ToString(), valoresAActualizar, valoresAFiltrar);
            //MessageBox.Show(salida);

            //### DESPALETIZADOR #####
            filterval[3] = "\"Despaletizador\"";
            updateval[1] = despaletizadorP;
            salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + numlin.ToString(), valoresAActualizar, valoresAFiltrar);
            //MessageBox.Show(salida);

            //### Llenadora #####
            filterval[3] = "\"Llenadora\"";
            updateval[1] = llenadoraP;
            salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + numlin.ToString(), valoresAActualizar, valoresAFiltrar);
            //MessageBox.Show(salida);

            //### Etiquetadora #####
            filterval[3] = "\"Etiquetadora\"";
            updateval[1] = etiquetadoraP;
            salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + numlin.ToString(), valoresAActualizar, valoresAFiltrar);
            //MessageBox.Show(salida);

            //### Encajadora #####
            filterval[3] = "\"Encajadora\"";
            updateval[1] = encajadoraP;
            salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + numlin.ToString(), valoresAActualizar, valoresAFiltrar);
            //MessageBox.Show(salida);

            //### Control Calidad #####
            filterval[3] = "\"Control\"";
            updateval[1] = controlP;
            salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + numlin.ToString(), valoresAActualizar, valoresAFiltrar);
            //MessageBox.Show(salida);


            //#######  ACTUALIZAMOS SETTINGS  #############
            //Actualizamos variables Globales
            turno = turnoP;
            Responsable = responsableP;
            MDespaletizador = despaletizadorP;
            MLlenadora = llenadoraP;
            MEtiquetadora = etiquetadoraP;
            MEncajonadora = encajadoraP;
            ControlCal = controlP;

            //Actualizamos fichero de settings
            Properties.Settings.Default.Responsable = responsableP;
            Properties.Settings.Default.MDespaletizador = despaletizadorP;
            Properties.Settings.Default.MLlenadora = llenadoraP;
            Properties.Settings.Default.MEtiquetadora = etiquetadoraP;
            Properties.Settings.Default.MEncajonadora = encajadoraP;
            Properties.Settings.Default.MControl = controlP;

            Properties.Settings.Default.turno = turnoP;

            diaT = Convert.ToInt16(DateTime.Now.ToString("dd"));
            Properties.Settings.Default.diaT = diaT;

            switchT = true;
            Properties.Settings.Default.switchT = true;
            Properties.Settings.Default.Save();
            //Marcamos que linea se ha comprobado el personal
            if (numLineaP == "2")
            {
                checkL2 = true;
                Properties.Settings.Default.checkL2 = true;
                chDesL2 = false;
                Properties.Settings.Default.chDesL2 = false;
                chLlenL2 = false;
                Properties.Settings.Default.chLlenL2 = false;
                chEtiqL2 = false;
                Properties.Settings.Default.chEtiqL3 = false;
                chEncL2 = false;
                Properties.Settings.Default.chEncL2 = false;
            }
            if (numLineaP == "3")
            {
                checkL3 = true;
                Properties.Settings.Default.checkL3 = true;
                chDesL3 = false;
                Properties.Settings.Default.chDesL3 = false;
                chLlenL3 = false;
                Properties.Settings.Default.chLlenL3 = false;
                chEtiqL3 = false;
                Properties.Settings.Default.chEtiqL3 = false;
                chEncL3 = false;
                Properties.Settings.Default.chEncL3 = false;
            }
            if (numLineaP == "5")
            {
                checkL5 = true;
                Properties.Settings.Default.checkL5 = true;
                chDesL5 = false;
                Properties.Settings.Default.chDesL5 = false;
                chLlenL5 = false;
                Properties.Settings.Default.chLlenL5 = false;
                chEtiqL5 = false;
                Properties.Settings.Default.chEtiqL5 = false;
                chEncL5 = false;
                Properties.Settings.Default.chEncL5 = false;
            }
            
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        public static string ChequearCambioTurno(string maquina) {
            string CambioTurno = "";
            switch (maquina)
            {
                case "Despaletizador":
                    if (numlin == 2)
                    {
                        if (chDesL2 == false) CambioTurno = "Inicio";
                        else CambioTurno = "Fin";
                    }
                    if (numlin == 3)
                    {
                        if (chDesL3 == false) CambioTurno = "Inicio";
                        else CambioTurno = "Fin";
                    }
                    if (numlin == 5)
                    {
                        if (chDesL5 == false) CambioTurno = "Inicio";
                        else CambioTurno = "Fin";
                    }
                    break;
                case "Llenadora":
                    if (numlin == 2)
                    {
                        if (chLlenL2 == false) CambioTurno = "Inicio";
                        else CambioTurno = "Fin";
                    }
                    if (numlin == 3)
                    {
                        if (chLlenL3 == false) CambioTurno = "Inicio";
                        else CambioTurno = "Fin";
                    }
                    if (numlin == 5)
                    {
                        if (chLlenL5 == false) CambioTurno = "Inicio";
                        else CambioTurno = "Fin";
                    }
                    break;
                case "Etiquetadora":
                    if (numlin == 2)
                    {
                        if (chEtiqL2 == false) CambioTurno = "Inicio";
                        else CambioTurno = "Fin";
                    }
                    if (numlin == 3)
                    {
                        if (chEtiqL3 == false) CambioTurno = "Inicio";
                        else CambioTurno = "Fin";
                    }
                    if (numlin == 5)
                    {
                        if (chEtiqL5 == false) CambioTurno = "Inicio";
                        else CambioTurno = "Fin";
                    }
                    break;
                case "Encajonadora":
                    if (numlin == 2)
                    {
                        if (chEncL2 == false) CambioTurno = "Inicio";
                        else CambioTurno = "Fin";
                    }
                    if (numlin == 3)
                    {
                        if (chEncL3 == false) CambioTurno = "Inicio";
                        else CambioTurno = "Fin";
                    }
                    if (numlin == 5)
                    {
                        if (chEncL5 == false) CambioTurno = "Inicio";
                        else CambioTurno = "Fin";
                    }
                    break;
            }

            return CambioTurno;
        }

        public static void ActivarAlarma()
        {
           //ACTIVA LA ALARMA PARA EL DESPALETIZADOR 
            if (MaquinaLinea.chDesL2 == true && MaquinaLinea.chalarmaDesL2 == false)
            {
                Properties.Settings.Default.chalarmaDesL2 = true;
                Properties.Settings.Default.Save();
                MaquinaLinea.chalarmaDesL2 = Properties.Settings.Default.chalarmaDesL2;
            }
            if (MaquinaLinea.chDesL3 == true && MaquinaLinea.chalarmaDesL3 == false)
            {
                Properties.Settings.Default.chalarmaDesL3 = true;
                Properties.Settings.Default.Save();
                MaquinaLinea.chalarmaDesL3 = Properties.Settings.Default.chalarmaDesL3;
            }
            if (MaquinaLinea.chDesL5 == true && MaquinaLinea.chalarmaDesL5 == false)
            {
                Properties.Settings.Default.chalarmaDesL5 = true;
                Properties.Settings.Default.Save();
                MaquinaLinea.chalarmaDesL5 = Properties.Settings.Default.chalarmaDesL5;
            }
            //ACTIVA LA ALARMA PARA LA LLENADORA
            if (MaquinaLinea.chLlenL2 == true && MaquinaLinea.chalarmaLlenL2 == false)
            {
                Properties.Settings.Default.chalarmaLlenL2 = true;
                Properties.Settings.Default.Save();
                MaquinaLinea.chalarmaLlenL2 = Properties.Settings.Default.chalarmaLlenL2;
            }
            if (MaquinaLinea.chLlenL3 == true && MaquinaLinea.chalarmaLlenL3 == false)
            {
                Properties.Settings.Default.chalarmaLlenL3 = true;
                Properties.Settings.Default.Save();
                MaquinaLinea.chalarmaLlenL3 = Properties.Settings.Default.chalarmaLlenL3;
            }
            if (MaquinaLinea.chLlenL5 == true && MaquinaLinea.chalarmaLlenL5 == false)
            {
                Properties.Settings.Default.chalarmaLlenL5 = true;
                Properties.Settings.Default.Save();
                MaquinaLinea.chalarmaLlenL5 = Properties.Settings.Default.chalarmaLlenL5;
            }
            //ACTIVA LA ALARMA PARA LA ETIQUETADORA
            if (MaquinaLinea.chEtiqL2 == true && MaquinaLinea.chalarmaEtiqL2 == false)
            {
                Properties.Settings.Default.chalarmaEtiqL2 = true;
                Properties.Settings.Default.Save();
                MaquinaLinea.chalarmaEtiqL2 = Properties.Settings.Default.chalarmaEtiqL2;
            }
            if (MaquinaLinea.chEtiqL3 == true && MaquinaLinea.chalarmaEtiqL3 == false)
            {
                Properties.Settings.Default.chalarmaEtiqL3 = true;
                Properties.Settings.Default.Save();
                MaquinaLinea.chalarmaEtiqL3 = Properties.Settings.Default.chalarmaEtiqL3;
            }
            if (MaquinaLinea.chEtiqL5 == true && MaquinaLinea.chalarmaEtiqL5 == false)
            {
                Properties.Settings.Default.chalarmaEtiqL5 = true;
                Properties.Settings.Default.Save();
                MaquinaLinea.chalarmaEtiqL5 = Properties.Settings.Default.chalarmaEtiqL5;
            }
            //ACTIVA LA ALARMA PARA LA ENCAJONADORA
            if (MaquinaLinea.chEncL2 == true && MaquinaLinea.chalarmaEncL2 == false)
            {
                Properties.Settings.Default.chalarmaEncL2 = true;
                Properties.Settings.Default.Save();
                MaquinaLinea.chalarmaEncL2 = Properties.Settings.Default.chalarmaEncL2;
            }
            if (MaquinaLinea.chEncL3 == true && MaquinaLinea.chalarmaEncL3 == false)
            {
                Properties.Settings.Default.chalarmaEncL3 = true;
                Properties.Settings.Default.Save();
                MaquinaLinea.chalarmaEncL3 = Properties.Settings.Default.chalarmaEncL3;
            }
            if (MaquinaLinea.chEncL5 == true && MaquinaLinea.chalarmaEncL5 == false)
            {
                Properties.Settings.Default.chalarmaEncL5 = true;
                Properties.Settings.Default.Save();
                MaquinaLinea.chalarmaEncL5 = Properties.Settings.Default.chalarmaEncL5;
            }
        }

        //Para indicar el lote
        public static string FuncionLote(string Cliente)
        {
            if (Cliente == "LIDL")
            {
                MaquinaLinea.Lote = "L" + Convert.ToString(Convert.ToInt32(DateTime.Now.DayOfYear.ToString())/7) + DateTime.Now.ToString("yy").Substring(1, 1) + "T" + numlin + "D";
            }
            else
            {
                if (Convert.ToInt16(DateTime.Now.DayOfYear.ToString()) >= 100)
                {
                    MaquinaLinea.Lote = "L" + DateTime.Now.ToString("yy") + DateTime.Now.DayOfYear.ToString();
                }
                if (Convert.ToInt16(DateTime.Now.DayOfYear.ToString()) < 100)
                {
                    MaquinaLinea.Lote = "L" + DateTime.Now.ToString("yy") + "0" + DateTime.Now.DayOfYear.ToString();
                }
                if (Convert.ToInt16(DateTime.Now.DayOfYear.ToString()) < 10)
                {
                    MaquinaLinea.Lote = "L" + DateTime.Now.ToString("yy") + "00" + DateTime.Now.DayOfYear.ToString();
                }
            }
            return MaquinaLinea.Lote;
        }


        public static string ExtraerCodigoBotellaRota(string Referencia)
        {
            int i = 0;
            while (i < 2)
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
                    filterval1[3] = "'%" + "BOT.0" + "%'";
                    valoresAFiltrar.Add(filterval1);
                }
                if (i == 1)
                {
                    string[] filterval2 = new string[4];
                    filterval2[0] = "AND";
                    filterval2[1] = "DescMaterial";
                    filterval2[2] = "LIKE";
                    filterval2[3] = "'%" + "BOT.1" + "%'";
                    valoresAFiltrar.Add(filterval2);
                }

                DataSet excelDataSet = new DataSet();
                string result;
                //List<string[]> valoresAFiltrar = dgvSelectFiltro.DataSource;
                excelDataSet = ExcelUtiles.LeerFicheroExcel(FileBOM, "FICHA", "CodMaterial ".Split(';'), valoresAFiltrar, out result);
                //tbSelectSalidaError.Text = result;
                //MessageBox.Show(result);
                if (excelDataSet.Tables[0].Rows.Count > 0)
                {
                    RotCodMat = Convert.ToString(excelDataSet.Tables[0].Rows[0]["CodMaterial"]);
                    i = 2;
                }
                valoresAFiltrar.Clear();
                i++;
            }
                return RotCodMat;
        }
        /// <summary>
        /// Función que completa los textbox de la descripcion del cierre y el proveedor del mismo.
        /// </summary>
        /// <param name="CodigoMaterial">Código extraido de la funcion EstraerDatosBOM.</param>
        public static void ExtraerDatosMateriales(string CodigoMaterial)
        {
            if (CodigoMaterial != "")
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
                excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileMateriales, "Botellas", "Descripcion; Proveedor".Split(';'), valoresAFiltrar, out result);
                //tbSelectSalidaError.Text = result;
                //MessageBox.Show(result);
                if (excelDataSet.Tables[0].Rows.Count > 0)
                {
                    RotDescMaterial= Convert.ToString(excelDataSet.Tables[0].Rows[0]["Descripcion"]);
                    RotProveedorMaterial = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Proveedor"]);
                }
                else MessageBox.Show("No se ha entontrado la descripción del cierre, comunicalo al responsable");
            }
        }
    }
}

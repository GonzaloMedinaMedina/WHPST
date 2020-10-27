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
using WHPS.ProgramMenus;
using WHPS.Utiles;

namespace WHPS.Encajonadora
{
    public partial class Encajonadora_Registro_Produccion : Form
    {
        public Encajonadora_Registro_Produccion()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void ExitB_Click(object sender, EventArgs e)
        {
            //Se realiza una "foto" de los datos que han sido introducidos
            if (MaquinaLinea.numlin == 2)
            {
                Properties.Settings.Default.DPLoteEncL2 = LoteTB.Text;
                Properties.Settings.Default.DPNCajasEncL2 = NCajasTB.Text;
                Properties.Settings.Default.DPHInicioEncL2 = HInicioTB.Text;
                Properties.Settings.Default.DPHFinEncL2 = HFinTB.Text;
                MaquinaLinea.LoteCopiadoEncL2 = LoteCopiadoTB.Text;
            }
            if (MaquinaLinea.numlin == 3)
            {
                Properties.Settings.Default.DPLoteEncL3 = LoteTB.Text;
                Properties.Settings.Default.DPNCajasEncL3 = NCajasTB.Text;
                Properties.Settings.Default.DPHInicioEncL3 = HInicioTB.Text;
                Properties.Settings.Default.DPHFinEncL3 = HFinTB.Text;
                MaquinaLinea.LoteCopiadoEncL3 = LoteCopiadoTB.Text;
            }
            if (MaquinaLinea.numlin == 5)
            {
                Properties.Settings.Default.DPLoteEncL5 = LoteTB.Text;
                Properties.Settings.Default.DPNCajasEncL5 = NCajasTB.Text;
                Properties.Settings.Default.DPHInicioEncL5 = HInicioTB.Text;
                Properties.Settings.Default.DPHFinEncL5 = HFinTB.Text;
                MaquinaLinea.LoteCopiadoEncL5 = LoteCopiadoTB.Text;
            }
            Properties.Settings.Default.Save();

            MainEncajonadora Form = new MainEncajonadora();
            Hide();
            Form.Show();
            GC.Collect();
        }

        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        private void Encajonadora_Registro_Produccion_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //Rellenamos los datos del equipo
            dateTB.Text = DateTime.Now.ToString("dd/MM/yyyy");
            respTB.Text = MaquinaLinea.Responsable;
            maqTB.Text = MaquinaLinea.MEncajonadora;
            turnoTB.Text = Utilidades.ObtenerTurnoActual();

            //Se muestra los datos del LOTE
            DiaJulianoLB.Text = "Día Juliano: " + DateTime.Now.DayOfYear.ToString();
            AñoLB.Text = "Año: " + DateTime.Now.Year.ToString();
            LineaLB.Text = "Línea: L" + MaquinaLinea.numlin;

            if (MaquinaLinea.numlin == 2)
            {
                //Rellenamos los datos obtenidos del lanzamiento
                OrdenTB.Text = Properties.Settings.Default.DPOrdenEncL2;
                ClienteTB.Text = Properties.Settings.Default.DPClienteEncL2;
                ProductoTB.Text = Properties.Settings.Default.DPProductoEncL2;
                CajasAProducirLB.Text = "(Cajas a producir: " + Properties.Settings.Default.CajasAProducirEncL2 + ")";

                //Rellenamos los registros que ya han sido guardados
                LoteTB.Text = Properties.Settings.Default.DPLoteEncL2;
                FormatoTB.Text = Properties.Settings.Default.DPFormatoEncL2;
                NCajasTB.Text = Properties.Settings.Default.DPNCajasEncL2;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL2;
                HFinTB.Text = Properties.Settings.Default.DPHFinEncL2;
                LoteCopiadoTB.Text = MaquinaLinea.LoteCopiadoEncL2;
                //Indicamos que muestre el boton verde cuando el tiempo de preparacion ha comenzado
                if (Properties.Settings.Default.DPHInicioCambioEncL2 != "" && HInicioTB.Text == "")LanzamientocargadoB.BackColor = Color.DarkSeaGreen;
            }
            if (MaquinaLinea.numlin == 3)
            {
                //Rellenamos los datos obtenidos del lanzamiento
                OrdenTB.Text = Properties.Settings.Default.DPOrdenEncL3;
                ClienteTB.Text = Properties.Settings.Default.DPClienteEncL3;
                ProductoTB.Text = Properties.Settings.Default.DPProductoEncL3;
                CajasAProducirLB.Text = "(Cajas a producir: " + Properties.Settings.Default.CajasAProducirEncL3 + ")";

                //Rellenamos los registros que ya han sido guardados
                LoteTB.Text = Properties.Settings.Default.DPLoteEncL3;
                FormatoTB.Text = Properties.Settings.Default.DPFormatoEncL3;
                NCajasTB.Text = Properties.Settings.Default.DPNCajasEncL3;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL3;
                HFinTB.Text = Properties.Settings.Default.DPHFinEncL3;
                LoteCopiadoTB.Text = MaquinaLinea.LoteCopiadoEncL3;
                //Indicamos que muestre el boton verde cuando el tiempo de preparacion ha comenzado
                if (Properties.Settings.Default.DPHInicioCambioEncL3 != "" && HInicioTB.Text == "") LanzamientocargadoB.BackColor = Color.DarkSeaGreen;
            }
            if (MaquinaLinea.numlin == 5)
            {
                //Rellenamos los datos obtenidos del lanzamiento
                OrdenTB.Text = Properties.Settings.Default.DPOrdenEncL5;
                ClienteTB.Text = Properties.Settings.Default.DPClienteEncL5;
                ProductoTB.Text = Properties.Settings.Default.DPProductoEncL5;
                CajasAProducirLB.Text = "(Cajas a producir: " + Properties.Settings.Default.CajasAProducirEncL5 + ")";

                //Rellenamos los registros que ya han sido guardados
                LoteTB.Text = Properties.Settings.Default.DPLoteEncL5;
                FormatoTB.Text = Properties.Settings.Default.DPFormatoEncL5;
                NCajasTB.Text = Properties.Settings.Default.DPNCajasEncL5;
                HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL5;
                HFinTB.Text = Properties.Settings.Default.DPHFinEncL5;
                LoteCopiadoTB.Text = MaquinaLinea.LoteCopiadoEncL5;
                //Indicamos que muestre el boton verde cuando el tiempo de preparacion ha comenzado
                if (Properties.Settings.Default.DPHInicioCambioEncL5 != "" && HInicioTB.Text == "")LanzamientocargadoB.BackColor = Color.DarkSeaGreen;
            }

            //Indicamos si estamos en tiempo de preparación o no cambiando el color del boton
            if (HInicioTB.Text != "") {LanzamientocargadoB.BackColor = Color.FromArgb(27, 33, 41);  ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;}
            else ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionInicio;
        }


        /// <summary>
        /// Función del temporizador que se ejecuta cada segundo, indíca la hora y si se activa la alarma. 
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            //Para activar la alarma debe, estar desactivada, haberse chequeado el inicio de turno y que la hora cuadre con la alarma 
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }
        }

        //Muestra el PDF donde viene como de ser el lote
        private void PDFLoteB_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MaquinaLinea.RutaPDFLOTE);
            Process.Start(MaquinaLinea.RutaPDFLOTE);
        }

        //Copia el lote que este en el portafolio
        private void InsertarLoteB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2) MaquinaLinea.LoteCopiadoEncL2 = LoteCopiadoTB.Text;
            if (MaquinaLinea.numlin == 3) MaquinaLinea.LoteCopiadoEncL3 = LoteCopiadoTB.Text;
            if (MaquinaLinea.numlin == 5) MaquinaLinea.LoteCopiadoEncL5 = LoteCopiadoTB.Text;
            LoteTB.Text = LoteCopiadoTB.Text;
            WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this, LoteTB);

        }

        //Al hacer click en los textbox se mostrará un teclado para completar el formulario
        private void LoteTB_MouseClick(object sender, MouseEventArgs e)
        {
            WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,LoteTB);
            
            
        }
        private void NCajasTB_MouseClick(object sender, MouseEventArgs e)
        {
            WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,NCajasTB);
            
        }

        private void LoteCopiadoTB_MouseClick(object sender, MouseEventArgs e)
        {
            WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this,LoteCopiadoTB);
        
        }

        //El teclado manual realiza la accion al ser ocultado
     

        //Registrará el tiempo en el TB de cuya variable sea true
        private void LanzamientocargadoB_Click(object sender, EventArgs e)
        {
            if (HInicioTB.Text == "")
            {
                if (MaquinaLinea.numlin == 2 && Properties.Settings.Default.DPHInicioCambioEncL2=="")
                {
                    Properties.Settings.Default.DPHInicioCambioEncL2 = DateTime.Now.ToString("HH:mm:ss");
                    LanzamientocargadoB.BackColor = Color.DarkSeaGreen;
                }
                if (MaquinaLinea.numlin == 3 && Properties.Settings.Default.DPHInicioCambioEncL3 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioEncL3 = DateTime.Now.ToString("HH:mm:ss");
                    LanzamientocargadoB.BackColor = Color.DarkSeaGreen;
                }
                if (MaquinaLinea.numlin == 5 && Properties.Settings.Default.DPHInicioCambioEncL5 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioEncL5 = DateTime.Now.ToString("HH:mm:ss");
                    LanzamientocargadoB.BackColor = Color.DarkSeaGreen;
                }
                Properties.Settings.Default.Save();
            }
        }
        private void ComienzoProdB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                //Puede ser que se haya iniciado la producción.
                if (Properties.Settings.Default.DPHInicioCambioEncL2 != "" && Properties.Settings.Default.DPHInicioEncL2 != "" && Properties.Settings.Default.DPHFinEncL2 == "")
                {
                    Properties.Settings.Default.DPHFinEncL2 = DateTime.Now.ToString("HH:mm:ss");
                    HFinTB.Text = Properties.Settings.Default.DPHFinEncL2;
                }
                //Puede ser que SI se haya iniciado la preparacion por que no sea necesario.
                //MessageBox.Show("prerp: " + Properties.Settings.Default.DPHInicioCambioEncL2 + "inicio: " + Properties.Settings.Default.DPHInicioEncL2 + "fin: " + Properties.Settings.Default.DPHFinEncL2);
                if (Properties.Settings.Default.DPHInicioCambioEncL2 != "" && Properties.Settings.Default.DPHInicioEncL2 == "" && Properties.Settings.Default.DPHFinEncL2 == "")
                {
                    Properties.Settings.Default.DPHInicioEncL2 = DateTime.Now.ToString("HH:mm:ss");
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL2;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinEncL2 = "";
                }
                //Puede ser que NO se haya iniciado la preparacion por que no sea necesario.
                if (Properties.Settings.Default.DPHInicioCambioEncL2 == "" && Properties.Settings.Default.DPHInicioEncL2 == "" && Properties.Settings.Default.DPHFinEncL2 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioEncL2 = DateTime.Now.ToString("HH:mm:ss");
                    Properties.Settings.Default.DPHInicioEncL2 = Properties.Settings.Default.DPHInicioCambioEncL2;
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL2;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinEncL2 = "";

                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                //Puede ser que se haya iniciado la producción.
                if (Properties.Settings.Default.DPHInicioCambioEncL3 != "" && Properties.Settings.Default.DPHInicioEncL3 != "" && Properties.Settings.Default.DPHFinEncL3 == "")
                {
                    Properties.Settings.Default.DPHFinEncL3 = DateTime.Now.ToString("HH:mm:ss");
                    HFinTB.Text = Properties.Settings.Default.DPHFinEncL3;
                }
                //Puede ser que SI se haya iniciado la preparacion por que no sea necesario.
                //MessageBox.Show("prerp: " + Properties.Settings.Default.DPHInicioCambioEncL3 + "inicio: " + Properties.Settings.Default.DPHInicioEncL3 + "fin: " + Properties.Settings.Default.DPHFinEncL3);
                if (Properties.Settings.Default.DPHInicioCambioEncL3 != "" && Properties.Settings.Default.DPHInicioEncL3 == "" && Properties.Settings.Default.DPHFinEncL3 == "")
                {
                    Properties.Settings.Default.DPHInicioEncL3 = DateTime.Now.ToString("HH:mm:ss");
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL3;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinEncL3 = "";
                }
                //Puede ser que NO se haya iniciado la preparacion por que no sea necesario.
                if (Properties.Settings.Default.DPHInicioCambioEncL3 == "" && Properties.Settings.Default.DPHInicioEncL3 == "" && Properties.Settings.Default.DPHFinEncL3 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioEncL3 = DateTime.Now.ToString("HH:mm:ss");
                    Properties.Settings.Default.DPHInicioEncL3 = Properties.Settings.Default.DPHInicioCambioEncL3;
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL3;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinEncL3 = "";

                   
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                //Puede ser que se haya iniciado la producción.
                if (Properties.Settings.Default.DPHInicioCambioEncL5 != "" && Properties.Settings.Default.DPHInicioEncL5 != "" && Properties.Settings.Default.DPHFinEncL5 == "")
                {
                    Properties.Settings.Default.DPHFinEncL5 = DateTime.Now.ToString("HH:mm:ss");
                    HFinTB.Text = Properties.Settings.Default.DPHFinEncL5;
                }
                //Puede ser que SI se haya iniciado la preparacion por que no sea necesario.
                //MessageBox.Show("prerp: " + Properties.Settings.Default.DPHInicioCambioEncL5 + "inicio: " + Properties.Settings.Default.DPHInicioEncL5 + "fin: " + Properties.Settings.Default.DPHFinEncL5);
                if (Properties.Settings.Default.DPHInicioCambioEncL5 != "" && Properties.Settings.Default.DPHInicioEncL5 == "" && Properties.Settings.Default.DPHFinEncL5 == "")
                {
                    Properties.Settings.Default.DPHInicioEncL5 = DateTime.Now.ToString("HH:mm:ss");
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL5;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinEncL5 = "";
                }
                //Puede ser que NO se haya iniciado la preparacion por que no sea necesario.
                if (Properties.Settings.Default.DPHInicioCambioEncL5 == "" && Properties.Settings.Default.DPHInicioEncL5 == "" && Properties.Settings.Default.DPHFinEncL5 == "")
                {
                    Properties.Settings.Default.DPHInicioCambioEncL5 = DateTime.Now.ToString("HH:mm:ss");
                    Properties.Settings.Default.DPHInicioEncL5 = Properties.Settings.Default.DPHInicioCambioEncL5;
                    HInicioTB.Text = Properties.Settings.Default.DPHInicioEncL5;
                    ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionFinalizar;
                    Properties.Settings.Default.DPHFinEncL5 = "";

                    
                }
            }
            Properties.Settings.Default.Save();
        }
        //Botón que elimina toda la información del registro
        private void BorrarB_Click(object sender, EventArgs e)
        {
            LoteTB.Text = "";
            NCajasTB.Text = "";
            HInicioTB.Text = "";
            HFinTB.Text = "";
            LanzamientocargadoB.BackColor = Color.FromArgb(27, 33, 41);
            ComienzoProdB.BackgroundImage = Properties.Resources.ProduccionInicio;
            if (MaquinaLinea.numlin == 2)Properties.Settings.Default.DPHInicioCambioEncL2 = "";
            if (MaquinaLinea.numlin == 3) Properties.Settings.Default.DPHInicioCambioEncL3 = "";
            if (MaquinaLinea.numlin == 5) Properties.Settings.Default.DPHInicioCambioEncL5 = "";
            Properties.Settings.Default.Save();
        }

        //Guardamos que la producción se ha terminado
        private void saveBot_Click(object sender, EventArgs e)
        {
            //Para poder guardar todos los campos deben estar cumplimentados
            if (OrdenTB.Text != "" && ClienteTB.Text != "" && ProductoTB.Text != "" && NCajasTB.Text != "" && HInicioTB.Text != "" && HFinTB.Text != "" && LoteTB.Text != "")
            {
                string ID_Lanz = "";
                //Cargamos las variables del cambio en variables globales
                if (MaquinaLinea.numlin == 2) MaquinaLinea.HInicioCambioEnc = Properties.Settings.Default.DPHInicioCambioEncL2; ID_Lanz = Properties.Settings.Default.DPiDLanzEncL2;
                if (MaquinaLinea.numlin == 3) MaquinaLinea.HInicioCambioEnc = Properties.Settings.Default.DPHInicioCambioEncL3; ID_Lanz = Properties.Settings.Default.DPiDLanzEncL3;
                if (MaquinaLinea.numlin == 5) MaquinaLinea.HInicioCambioEnc = Properties.Settings.Default.DPHInicioCambioEncL5; ID_Lanz = Properties.Settings.Default.DPiDLanzEncL5;

                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "Fecha", DateTime.Now.ToString("dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Hora", DateTime.Now.ToString("HH:mm:ss") });
                listavalores.Add(new string[2] { "FechaDB", DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") });
                listavalores.Add(new string[2] { "Responsable", MaquinaLinea.Responsable});
                listavalores.Add(new string[2] { "Maquinista", maqTB.Text});
                listavalores.Add(new string[2] { "Turno", turnoTB.Text });
                listavalores.Add(new string[2] { "ID_Lanz", ID_Lanz });
                listavalores.Add(new string[2] { "Lote", LoteTB.Text });
                listavalores.Add(new string[2] { "Orden", OrdenTB.Text });
                listavalores.Add(new string[2] { "Formato", FormatoTB.Text });
                listavalores.Add(new string[2] { "Cliente", ClienteTB.Text });
                listavalores.Add(new string[2] { "Producto", ProductoTB.Text });
                listavalores.Add(new string[2] { "NCajas", NCajasTB.Text });
                listavalores.Add(new string[2] { "Inicio", HInicioTB.Text });
                listavalores.Add(new string[2] { "Fin", HFinTB.Text });
                listavalores.Add(new string[2] { "InicioCambio", MaquinaLinea.HInicioCambioEnc});

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileEncajonadora, "Registro", listavalores, "Id");
                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show(salida);
                    MessageBox.Show(Properties.Settings.Default.AvisoProblemaFichero);
                }
                else
                {

                    //Restablecemos los valores correspondientes
                    LoteTB.Text = "";
                    OrdenTB.Text = "";
                    FormatoTB.Text = "";
                    ClienteTB.Text = "";
                    ProductoTB.Text = "";
                    HInicioTB.Text = "";
                    HFinTB.Text = "";

                    if (MaquinaLinea.numlin == 2)
                    {

                        if (Convert.ToInt32(NCajasTB.Text)<Convert.ToInt32(Properties.Settings.Default.CajasAProducirEncL2))
                        {

                            DialogResult opcion;
                            opcion = MessageBox.Show("El número de cajas indicado es menor que el número de cajas que debe tener la ORDEN, ¿Quieres modificar el estado de la ORDEN a COMPLETADO?", "", MessageBoxButtons.YesNo);
                            if (opcion == DialogResult.Yes)
                            {
                                //Filtro ID ORDEN
                                string[] valoresAFiltrar = new string[4];
                                valoresAFiltrar[0] = "AND";
                                valoresAFiltrar[1] = "ID_Lanz";
                                valoresAFiltrar[2] = "LIKE";
                                valoresAFiltrar[3] = "\"" + Properties.Settings.Default.DPiDLanzEncL2 + "\"";
                                string[] valoresAActualizar = new string[2];
                                valoresAActualizar[0] = "ESTADO";
                                valoresAActualizar[1] = "Completado";

                                bool output;
                                output = ExcelUtiles.ActualizarCeldaExcel("DB_L2", "Linea 2", valoresAActualizar, valoresAFiltrar);
                                //MessageBox.Show(output.ToString());
                            }
                        }
                        else
                        {
                            //Filtro ID ORDEN
                            string[] valoresAFiltrar = new string[4];
                            valoresAFiltrar[0] = "AND";
                            valoresAFiltrar[1] = "ID_Lanz";
                            valoresAFiltrar[2] = "LIKE";
                            valoresAFiltrar[3] = "\"" + Properties.Settings.Default.DPiDLanzEncL2 + "\"";
                            string[] valoresAActualizar = new string[2];
                            valoresAActualizar[0] = "ESTADO";
                            valoresAActualizar[1] = "Completado";

                            bool output;
                            output = ExcelUtiles.ActualizarCeldaExcel("DB_L2", "Linea 2", valoresAActualizar, valoresAFiltrar);
                        }



                        Properties.Settings.Default.FilaSeleccionadaEncL2 = "";
                        Properties.Settings.Default.DPiDLanzEncL2 = "";
                        Properties.Settings.Default.DPLoteEncL2 = "";
                        Properties.Settings.Default.DPHInicioEncL2 = "";
                        Properties.Settings.Default.DPHFinEncL2 = "";
                        Properties.Settings.Default.DPHInicioCambioEncL2 = "";
                        Properties.Settings.Default.DPNCajasEncL2 = "";



                    }
                    if (MaquinaLinea.numlin == 3)
                    {
                        if (Convert.ToInt32(NCajasTB.Text) < Convert.ToInt32(Properties.Settings.Default.CajasAProducirEncL3))
                        {

                            DialogResult opcion;
                            opcion = MessageBox.Show("El número de cajas indicado es menor que el número de cajas que debe tener la ORDEN, ¿Quieres modificar el estado de la ORDEN a COMPLETADO?", "", MessageBoxButtons.YesNo);
                            if (opcion == DialogResult.Yes)
                            {
                                //Filtro ID ORDEN
                                string[] valoresAFiltrar = new string[4];
                                valoresAFiltrar[0] = "AND";
                                valoresAFiltrar[1] = "ID_Lanz";
                                valoresAFiltrar[2] = "LIKE";
                                valoresAFiltrar[3] = "\"" + Properties.Settings.Default.DPiDLanzEncL3 + "\"";
                                string[] valoresAActualizar = new string[2];
                                valoresAActualizar[0] = "ESTADO";
                                valoresAActualizar[1] = "Completado";

                                bool output;
                                output = ExcelUtiles.ActualizarCeldaExcel("DB_L3", "Linea 3", valoresAActualizar, valoresAFiltrar);
                                //MessageBox.Show(output.ToString());
                            }
                            
                        }
                        else
                        {
                            //Filtro ID ORDEN
                            string[] valoresAFiltrar = new string[4];
                            valoresAFiltrar[0] = "AND";
                            valoresAFiltrar[1] = "ID_Lanz";
                            valoresAFiltrar[2] = "LIKE";
                            valoresAFiltrar[3] = "\"" + Properties.Settings.Default.DPiDLanzEncL3 + "\"";
                            string[] valoresAActualizar = new string[2];
                            valoresAActualizar[0] = "ESTADO";
                            valoresAActualizar[1] = "Completado";

                            bool output;
                            output = ExcelUtiles.ActualizarCeldaExcel("DB_L3", "Linea 3", valoresAActualizar, valoresAFiltrar);
                            //MessageBox.Show(output.ToString());
                       }

                        Properties.Settings.Default.FilaSeleccionadaEncL3 = "";
                        Properties.Settings.Default.DPiDLanzEncL3 = "";
                        Properties.Settings.Default.DPLoteEncL3 = "";
                        Properties.Settings.Default.DPHInicioEncL3 = "";
                        Properties.Settings.Default.DPHFinEncL3 = "";
                        Properties.Settings.Default.DPHInicioCambioEncL3 = "";
                        Properties.Settings.Default.DPNCajasEncL3 = "";
                    }
                    if (MaquinaLinea.numlin == 5)
                    {
                        //MessageBox.Show(NCajasTB.Text + ",,," + Properties.Settings.Default.CajasAProducirEncL5);

                        if (Convert.ToInt32(NCajasTB.Text) < Convert.ToInt32(Properties.Settings.Default.CajasAProducirEncL5))
                        {

                            DialogResult opcion;
                            opcion = MessageBox.Show("El número de cajas indicado es menor que el número de cajas que debe tener la ORDEN, ¿Quieres modificar el estado de la ORDEN a COMPLETADO?", "", MessageBoxButtons.YesNo);
                            if (opcion == DialogResult.Yes)
                            {
                                //Filtro ID ORDEN
                                string[] valoresAFiltrar = new string[4];
                                valoresAFiltrar[0] = "AND";
                                valoresAFiltrar[1] = "ID_Lanz";
                                valoresAFiltrar[2] = "LIKE";
                                valoresAFiltrar[3] = "\"" + Properties.Settings.Default.DPiDLanzEncL5 + "\"";
                                string[] valoresAActualizar = new string[2];
                                valoresAActualizar[0] = "ESTADO";
                                valoresAActualizar[1] = "Completado";

                                bool output;
                                output = ExcelUtiles.ActualizarCeldaExcel("DB_L5", "Linea 5", valoresAActualizar, valoresAFiltrar);
                                //MessageBox.Show(output.ToString());
                            }
                        }
                        else
                        {
                            //Filtro ID ORDEN
                            string[] valoresAFiltrar = new string[4];
                            valoresAFiltrar[0] = "AND";
                            valoresAFiltrar[1] = "ID_Lanz";
                            valoresAFiltrar[2] = "LIKE";
                            valoresAFiltrar[3] = "\"" + Properties.Settings.Default.DPiDLanzEncL5 + "\"";
                            string[] valoresAActualizar = new string[2];
                            valoresAActualizar[0] = "ESTADO";
                            valoresAActualizar[1] = "Completado";
                            bool salidaL5 = ExcelUtiles.ActualizarCeldaExcel("DB_L5", "Linea 5", valoresAActualizar, valoresAFiltrar);

                            //MessageBox.Show(output.ToString());
                        }

                        Properties.Settings.Default.FilaSeleccionadaEncL5 = "";
                        Properties.Settings.Default.DPiDLanzEncL5 = "";
                        Properties.Settings.Default.DPLoteEncL5 = "";
                        Properties.Settings.Default.DPHInicioEncL5 = "";
                        Properties.Settings.Default.DPHFinEncL5 = "";
                        Properties.Settings.Default.DPHInicioCambioEncL5 = "";
                        Properties.Settings.Default.DPNCajasEncL5 = "";
                    }
                    NCajasTB.Text = "";
                    LanzamientocargadoB.BackColor = Color.FromArgb(27, 33, 41);
                    Properties.Settings.Default.Save();

                    //MessageBox.Show(salida);
                    MainEncajonadora Form = new MainEncajonadora();
                    Hide();
                    Form.Show();
                    GC.Collect();
                }
            }
            else
            {
                MessageBox.Show(Properties.Settings.Default.AvisoCampos);
            }
        }

        private void CopiaCajasB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2) { NCajasTB.Text = Properties.Settings.Default.CajasAProducirEncL2; }
            if (MaquinaLinea.numlin == 3) { NCajasTB.Text = Properties.Settings.Default.CajasAProducirEncL3; }
            if (MaquinaLinea.numlin == 5) { NCajasTB.Text = Properties.Settings.Default.CajasAProducirEncL5; }
            WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this, NCajasTB);
        }

       
    }
}

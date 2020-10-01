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

namespace WHPS.Llenadora
{
    public partial class Llenadora_Torquimetro : Form
    {
        TextBox TextBox;
        public string CodigoMaterial;
        public Llenadora_Torquimetro()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>
        private void ExitB_Click(object sender, EventArgs e)
        {
            //Antes de salir registramos los valores rellenados pero no guardados para que no se pierdan
            if (MaquinaLinea.numlin == 2)
            {
                EstablecerVariablesL2(0, null, null, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text,
                                         C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
            }
            if (MaquinaLinea.numlin == 3)
            {
                EstablecerVariablesL3(0, null, null, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text,
                                         C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
            }

            if (MaquinaLinea.numlin == 5)
            {
                EstablecerVariablesL5(0, null, null, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text,
                                         C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
            }



            MainLlenadora Form = new MainLlenadora();
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
        private void Llenadora_Torquimetro_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //Se oculta el teclado
            numberpad1.Visible = false;

            //Rellenamos fecha
            dateTB.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //Rellenamos responsable y maquinista
            respTB.Text = MaquinaLinea.Responsable;
            maqTB.Text = MaquinaLinea.MLlenadora;


            //Rellenamos el turno - Identificando el turno
            turnoTB.Text = Utilidades.ObtenerTurnoActual();

            //cargamos los valores que han sido rellenados
            if (MaquinaLinea.numlin == 2)
            {
                CompletarRegistros(0, Properties.Settings.Default.TORQ_TipoCierreLlenL2, Properties.Settings.Default.TORQ_ProvLlenL2,
                      Properties.Settings.Default.TORQ_Cab1LlenL2, Properties.Settings.Default.TORQ_Cab2LlenL2, Properties.Settings.Default.TORQ_Cab3LlenL2, Properties.Settings.Default.TORQ_Cab4LlenL2,
                      Properties.Settings.Default.TORQ_Cab5LlenL2, Properties.Settings.Default.TORQ_Cab6LlenL2, Properties.Settings.Default.TORQ_Cab7LlenL2, Properties.Settings.Default.TORQ_Cab8LlenL2,
                      Properties.Settings.Default.TORQ_Cab9LlenL2, Properties.Settings.Default.TORQ_Cab10LlenL2, Properties.Settings.Default.TORQ_Cab11LlenL2, Properties.Settings.Default.TORQ_Cab12LlenL2);

                CompletarRegistros(1, Properties.Settings.Default.TORQ_TipoCierre1LlenL2, Properties.Settings.Default.TORQ_Prov1LlenL2,
                                      Properties.Settings.Default.TORQ_C11LlenL2, Properties.Settings.Default.TORQ_C21LlenL2, Properties.Settings.Default.TORQ_C31LlenL2, Properties.Settings.Default.TORQ_C41LlenL2,
                                      Properties.Settings.Default.TORQ_C51LlenL2, Properties.Settings.Default.TORQ_C61LlenL2, Properties.Settings.Default.TORQ_C71LlenL2, Properties.Settings.Default.TORQ_C81LlenL2,
                                      Properties.Settings.Default.TORQ_C91LlenL2, Properties.Settings.Default.TORQ_C101LlenL2, Properties.Settings.Default.TORQ_C111LlenL2, Properties.Settings.Default.TORQ_C121LlenL2);

                CompletarRegistros(2, Properties.Settings.Default.TORQ_TipoCierre2LlenL2, Properties.Settings.Default.TORQ_Prov2LlenL2,
                                      Properties.Settings.Default.TORQ_C12LlenL2, Properties.Settings.Default.TORQ_C22LlenL2, Properties.Settings.Default.TORQ_C32LlenL2, Properties.Settings.Default.TORQ_C42LlenL2,
                                      Properties.Settings.Default.TORQ_C52LlenL2, Properties.Settings.Default.TORQ_C62LlenL2, Properties.Settings.Default.TORQ_C72LlenL2, Properties.Settings.Default.TORQ_C82LlenL2,
                                      Properties.Settings.Default.TORQ_C92LlenL2, Properties.Settings.Default.TORQ_C102LlenL2, Properties.Settings.Default.TORQ_C112LlenL2, Properties.Settings.Default.TORQ_C122LlenL2);

                CompletarRegistros(3, Properties.Settings.Default.TORQ_TipoCierre3LlenL2, Properties.Settings.Default.TORQ_Prov3LlenL2,
                                      Properties.Settings.Default.TORQ_C13LlenL2, Properties.Settings.Default.TORQ_C23LlenL2, Properties.Settings.Default.TORQ_C33LlenL2, Properties.Settings.Default.TORQ_C43LlenL2,
                                      Properties.Settings.Default.TORQ_C53LlenL2, Properties.Settings.Default.TORQ_C63LlenL2, Properties.Settings.Default.TORQ_C73LlenL2, Properties.Settings.Default.TORQ_C83LlenL2,
                                      Properties.Settings.Default.TORQ_C93LlenL2, Properties.Settings.Default.TORQ_C103LlenL2, Properties.Settings.Default.TORQ_C113LlenL2, Properties.Settings.Default.TORQ_C123LlenL2);

                CompletarRegistros(4, Properties.Settings.Default.TORQ_TipoCierre4LlenL2, Properties.Settings.Default.TORQ_Prov4LlenL2,
                                      Properties.Settings.Default.TORQ_C14LlenL2, Properties.Settings.Default.TORQ_C24LlenL2, Properties.Settings.Default.TORQ_C34LlenL2, Properties.Settings.Default.TORQ_C44LlenL2,
                                      Properties.Settings.Default.TORQ_C54LlenL2, Properties.Settings.Default.TORQ_C64LlenL2, Properties.Settings.Default.TORQ_C74LlenL2, Properties.Settings.Default.TORQ_C84LlenL2,
                                      Properties.Settings.Default.TORQ_C94LlenL2, Properties.Settings.Default.TORQ_C104LlenL2, Properties.Settings.Default.TORQ_C114LlenL2, Properties.Settings.Default.TORQ_C124LlenL2);

                CompletarRegistros(5, Properties.Settings.Default.TORQ_TipoCierre5LlenL2, Properties.Settings.Default.TORQ_Prov5LlenL2,
                                      Properties.Settings.Default.TORQ_C15LlenL2, Properties.Settings.Default.TORQ_C25LlenL2, Properties.Settings.Default.TORQ_C35LlenL2, Properties.Settings.Default.TORQ_C45LlenL2,
                                      Properties.Settings.Default.TORQ_C55LlenL2, Properties.Settings.Default.TORQ_C65LlenL2, Properties.Settings.Default.TORQ_C75LlenL2, Properties.Settings.Default.TORQ_C85LlenL2,
                                      Properties.Settings.Default.TORQ_C95LlenL2, Properties.Settings.Default.TORQ_C105LlenL2, Properties.Settings.Default.TORQ_C115LlenL2, Properties.Settings.Default.TORQ_C125LlenL2);

                CompletarRegistros(6, Properties.Settings.Default.TORQ_TipoCierre6LlenL2, Properties.Settings.Default.TORQ_Prov6LlenL2,
                                      Properties.Settings.Default.TORQ_C16LlenL2, Properties.Settings.Default.TORQ_C26LlenL2, Properties.Settings.Default.TORQ_C36LlenL2, Properties.Settings.Default.TORQ_C46LlenL2,
                                      Properties.Settings.Default.TORQ_C56LlenL2, Properties.Settings.Default.TORQ_C66LlenL2, Properties.Settings.Default.TORQ_C76LlenL2, Properties.Settings.Default.TORQ_C86LlenL2,
                                      Properties.Settings.Default.TORQ_C96LlenL2, Properties.Settings.Default.TORQ_C106LlenL2, Properties.Settings.Default.TORQ_C116LlenL2, Properties.Settings.Default.TORQ_C126LlenL2);

                CompletarRegistros(7, Properties.Settings.Default.TORQ_TipoCierre7LlenL2, Properties.Settings.Default.TORQ_Prov7LlenL2,
                                      Properties.Settings.Default.TORQ_C17LlenL2, Properties.Settings.Default.TORQ_C27LlenL2, Properties.Settings.Default.TORQ_C37LlenL2, Properties.Settings.Default.TORQ_C47LlenL2,
                                      Properties.Settings.Default.TORQ_C57LlenL2, Properties.Settings.Default.TORQ_C67LlenL2, Properties.Settings.Default.TORQ_C77LlenL2, Properties.Settings.Default.TORQ_C87LlenL2,
                                      Properties.Settings.Default.TORQ_C97LlenL2, Properties.Settings.Default.TORQ_C107LlenL2, Properties.Settings.Default.TORQ_C117LlenL2, Properties.Settings.Default.TORQ_C127LlenL2);

            }
            if (MaquinaLinea.numlin == 3)
            {
                CompletarRegistros(0, Properties.Settings.Default.TORQ_TipoCierreLlenL3, Properties.Settings.Default.TORQ_ProvLlenL3,
                                      Properties.Settings.Default.TORQ_Cab1LlenL3, Properties.Settings.Default.TORQ_Cab2LlenL3, Properties.Settings.Default.TORQ_Cab3LlenL3, Properties.Settings.Default.TORQ_Cab4LlenL3,
                                      Properties.Settings.Default.TORQ_Cab5LlenL3, Properties.Settings.Default.TORQ_Cab6LlenL3, Properties.Settings.Default.TORQ_Cab7LlenL3, Properties.Settings.Default.TORQ_Cab8LlenL3,
                                      Properties.Settings.Default.TORQ_Cab9LlenL3, Properties.Settings.Default.TORQ_Cab10LlenL3, Properties.Settings.Default.TORQ_Cab11LlenL3, Properties.Settings.Default.TORQ_Cab12LlenL3);

                CompletarRegistros(1, Properties.Settings.Default.TORQ_TipoCierre1LlenL3, Properties.Settings.Default.TORQ_Prov1LlenL3,
                                      Properties.Settings.Default.TORQ_C11LlenL3, Properties.Settings.Default.TORQ_C21LlenL3, Properties.Settings.Default.TORQ_C31LlenL3, Properties.Settings.Default.TORQ_C41LlenL3,
                                      Properties.Settings.Default.TORQ_C51LlenL3, Properties.Settings.Default.TORQ_C61LlenL3, Properties.Settings.Default.TORQ_C71LlenL3, Properties.Settings.Default.TORQ_C81LlenL3,
                                      Properties.Settings.Default.TORQ_C91LlenL3, Properties.Settings.Default.TORQ_C101LlenL3, Properties.Settings.Default.TORQ_C111LlenL3, Properties.Settings.Default.TORQ_C121LlenL3);

                CompletarRegistros(2, Properties.Settings.Default.TORQ_TipoCierre2LlenL3, Properties.Settings.Default.TORQ_Prov2LlenL3,
                                      Properties.Settings.Default.TORQ_C12LlenL3, Properties.Settings.Default.TORQ_C22LlenL3, Properties.Settings.Default.TORQ_C32LlenL3, Properties.Settings.Default.TORQ_C42LlenL3,
                                      Properties.Settings.Default.TORQ_C52LlenL3, Properties.Settings.Default.TORQ_C62LlenL3, Properties.Settings.Default.TORQ_C72LlenL3, Properties.Settings.Default.TORQ_C82LlenL3,
                                      Properties.Settings.Default.TORQ_C92LlenL3, Properties.Settings.Default.TORQ_C102LlenL3, Properties.Settings.Default.TORQ_C112LlenL3, Properties.Settings.Default.TORQ_C122LlenL3);

                CompletarRegistros(3, Properties.Settings.Default.TORQ_TipoCierre3LlenL3, Properties.Settings.Default.TORQ_Prov3LlenL3,
                                      Properties.Settings.Default.TORQ_C13LlenL3, Properties.Settings.Default.TORQ_C23LlenL3, Properties.Settings.Default.TORQ_C33LlenL3, Properties.Settings.Default.TORQ_C43LlenL3,
                                      Properties.Settings.Default.TORQ_C53LlenL3, Properties.Settings.Default.TORQ_C63LlenL3, Properties.Settings.Default.TORQ_C73LlenL3, Properties.Settings.Default.TORQ_C83LlenL3,
                                      Properties.Settings.Default.TORQ_C93LlenL3, Properties.Settings.Default.TORQ_C103LlenL3, Properties.Settings.Default.TORQ_C113LlenL3, Properties.Settings.Default.TORQ_C123LlenL3);

                CompletarRegistros(4, Properties.Settings.Default.TORQ_TipoCierre4LlenL3, Properties.Settings.Default.TORQ_Prov4LlenL3,
                                      Properties.Settings.Default.TORQ_C14LlenL3, Properties.Settings.Default.TORQ_C24LlenL3, Properties.Settings.Default.TORQ_C34LlenL3, Properties.Settings.Default.TORQ_C44LlenL3,
                                      Properties.Settings.Default.TORQ_C54LlenL3, Properties.Settings.Default.TORQ_C64LlenL3, Properties.Settings.Default.TORQ_C74LlenL3, Properties.Settings.Default.TORQ_C84LlenL3,
                                      Properties.Settings.Default.TORQ_C94LlenL3, Properties.Settings.Default.TORQ_C104LlenL3, Properties.Settings.Default.TORQ_C114LlenL3, Properties.Settings.Default.TORQ_C124LlenL3);

                CompletarRegistros(5, Properties.Settings.Default.TORQ_TipoCierre5LlenL3, Properties.Settings.Default.TORQ_Prov5LlenL3,
                                      Properties.Settings.Default.TORQ_C15LlenL3, Properties.Settings.Default.TORQ_C25LlenL3, Properties.Settings.Default.TORQ_C35LlenL3, Properties.Settings.Default.TORQ_C45LlenL3,
                                      Properties.Settings.Default.TORQ_C55LlenL3, Properties.Settings.Default.TORQ_C65LlenL3, Properties.Settings.Default.TORQ_C75LlenL3, Properties.Settings.Default.TORQ_C85LlenL3,
                                      Properties.Settings.Default.TORQ_C95LlenL3, Properties.Settings.Default.TORQ_C105LlenL3, Properties.Settings.Default.TORQ_C115LlenL3, Properties.Settings.Default.TORQ_C125LlenL3);

                CompletarRegistros(6, Properties.Settings.Default.TORQ_TipoCierre6LlenL3, Properties.Settings.Default.TORQ_Prov6LlenL3,
                                      Properties.Settings.Default.TORQ_C16LlenL3, Properties.Settings.Default.TORQ_C26LlenL3, Properties.Settings.Default.TORQ_C36LlenL3, Properties.Settings.Default.TORQ_C46LlenL3,
                                      Properties.Settings.Default.TORQ_C56LlenL3, Properties.Settings.Default.TORQ_C66LlenL3, Properties.Settings.Default.TORQ_C76LlenL3, Properties.Settings.Default.TORQ_C86LlenL3,
                                      Properties.Settings.Default.TORQ_C96LlenL3, Properties.Settings.Default.TORQ_C106LlenL3, Properties.Settings.Default.TORQ_C116LlenL3, Properties.Settings.Default.TORQ_C126LlenL3);

                CompletarRegistros(7, Properties.Settings.Default.TORQ_TipoCierre7LlenL3, Properties.Settings.Default.TORQ_Prov7LlenL3,
                                      Properties.Settings.Default.TORQ_C17LlenL3, Properties.Settings.Default.TORQ_C27LlenL3, Properties.Settings.Default.TORQ_C37LlenL3, Properties.Settings.Default.TORQ_C47LlenL3,
                                      Properties.Settings.Default.TORQ_C57LlenL3, Properties.Settings.Default.TORQ_C67LlenL3, Properties.Settings.Default.TORQ_C77LlenL3, Properties.Settings.Default.TORQ_C87LlenL3,
                                      Properties.Settings.Default.TORQ_C97LlenL3, Properties.Settings.Default.TORQ_C107LlenL3, Properties.Settings.Default.TORQ_C117LlenL3, Properties.Settings.Default.TORQ_C127LlenL3);

            }
            if (MaquinaLinea.numlin == 5)
            {
                CompletarRegistros(0, Properties.Settings.Default.TORQ_TipoCierreLlenL5, Properties.Settings.Default.TORQ_ProvLlenL5,
                                      Properties.Settings.Default.TORQ_Cab1LlenL5, Properties.Settings.Default.TORQ_Cab2LlenL5, Properties.Settings.Default.TORQ_Cab3LlenL5, Properties.Settings.Default.TORQ_Cab4LlenL5,
                                      Properties.Settings.Default.TORQ_Cab5LlenL5, Properties.Settings.Default.TORQ_Cab6LlenL5, Properties.Settings.Default.TORQ_Cab7LlenL5, Properties.Settings.Default.TORQ_Cab8LlenL5,
                                      Properties.Settings.Default.TORQ_Cab9LlenL5, Properties.Settings.Default.TORQ_Cab10LlenL5, Properties.Settings.Default.TORQ_Cab11LlenL5, Properties.Settings.Default.TORQ_Cab12LlenL5);

                CompletarRegistros(1, Properties.Settings.Default.TORQ_TipoCierre1LlenL5, Properties.Settings.Default.TORQ_Prov1LlenL5,
                                      Properties.Settings.Default.TORQ_C11LlenL5, Properties.Settings.Default.TORQ_C21LlenL5, Properties.Settings.Default.TORQ_C31LlenL5, Properties.Settings.Default.TORQ_C41LlenL5,
                                      Properties.Settings.Default.TORQ_C51LlenL5, Properties.Settings.Default.TORQ_C61LlenL5, Properties.Settings.Default.TORQ_C71LlenL5, Properties.Settings.Default.TORQ_C81LlenL5,
                                      Properties.Settings.Default.TORQ_C91LlenL5, Properties.Settings.Default.TORQ_C101LlenL5, Properties.Settings.Default.TORQ_C111LlenL5, Properties.Settings.Default.TORQ_C121LlenL5);

                CompletarRegistros(2, Properties.Settings.Default.TORQ_TipoCierre2LlenL5, Properties.Settings.Default.TORQ_Prov2LlenL5,
                                      Properties.Settings.Default.TORQ_C12LlenL5, Properties.Settings.Default.TORQ_C22LlenL5, Properties.Settings.Default.TORQ_C32LlenL5, Properties.Settings.Default.TORQ_C42LlenL5,
                                      Properties.Settings.Default.TORQ_C52LlenL5, Properties.Settings.Default.TORQ_C62LlenL5, Properties.Settings.Default.TORQ_C72LlenL5, Properties.Settings.Default.TORQ_C82LlenL5,
                                      Properties.Settings.Default.TORQ_C92LlenL5, Properties.Settings.Default.TORQ_C102LlenL5, Properties.Settings.Default.TORQ_C112LlenL5, Properties.Settings.Default.TORQ_C122LlenL5);

                CompletarRegistros(3, Properties.Settings.Default.TORQ_TipoCierre3LlenL5, Properties.Settings.Default.TORQ_Prov3LlenL5,
                                      Properties.Settings.Default.TORQ_C13LlenL5, Properties.Settings.Default.TORQ_C23LlenL5, Properties.Settings.Default.TORQ_C33LlenL5, Properties.Settings.Default.TORQ_C43LlenL5,
                                      Properties.Settings.Default.TORQ_C53LlenL5, Properties.Settings.Default.TORQ_C63LlenL5, Properties.Settings.Default.TORQ_C73LlenL5, Properties.Settings.Default.TORQ_C83LlenL5,
                                      Properties.Settings.Default.TORQ_C93LlenL5, Properties.Settings.Default.TORQ_C103LlenL5, Properties.Settings.Default.TORQ_C113LlenL5, Properties.Settings.Default.TORQ_C123LlenL5);

                CompletarRegistros(4, Properties.Settings.Default.TORQ_TipoCierre4LlenL5, Properties.Settings.Default.TORQ_Prov4LlenL5,
                                      Properties.Settings.Default.TORQ_C14LlenL5, Properties.Settings.Default.TORQ_C24LlenL5, Properties.Settings.Default.TORQ_C34LlenL5, Properties.Settings.Default.TORQ_C44LlenL5,
                                      Properties.Settings.Default.TORQ_C54LlenL5, Properties.Settings.Default.TORQ_C64LlenL5, Properties.Settings.Default.TORQ_C74LlenL5, Properties.Settings.Default.TORQ_C84LlenL5,
                                      Properties.Settings.Default.TORQ_C94LlenL5, Properties.Settings.Default.TORQ_C104LlenL5, Properties.Settings.Default.TORQ_C114LlenL5, Properties.Settings.Default.TORQ_C124LlenL5);

                CompletarRegistros(5, Properties.Settings.Default.TORQ_TipoCierre5LlenL5, Properties.Settings.Default.TORQ_Prov5LlenL5,
                                      Properties.Settings.Default.TORQ_C15LlenL5, Properties.Settings.Default.TORQ_C25LlenL5, Properties.Settings.Default.TORQ_C35LlenL5, Properties.Settings.Default.TORQ_C45LlenL5,
                                      Properties.Settings.Default.TORQ_C55LlenL5, Properties.Settings.Default.TORQ_C65LlenL5, Properties.Settings.Default.TORQ_C75LlenL5, Properties.Settings.Default.TORQ_C85LlenL5,
                                      Properties.Settings.Default.TORQ_C95LlenL5, Properties.Settings.Default.TORQ_C105LlenL5, Properties.Settings.Default.TORQ_C115LlenL5, Properties.Settings.Default.TORQ_C125LlenL5);

                CompletarRegistros(6, Properties.Settings.Default.TORQ_TipoCierre6LlenL5, Properties.Settings.Default.TORQ_Prov6LlenL5,
                                      Properties.Settings.Default.TORQ_C16LlenL5, Properties.Settings.Default.TORQ_C26LlenL5, Properties.Settings.Default.TORQ_C36LlenL5, Properties.Settings.Default.TORQ_C46LlenL5,
                                      Properties.Settings.Default.TORQ_C56LlenL5, Properties.Settings.Default.TORQ_C66LlenL5, Properties.Settings.Default.TORQ_C76LlenL5, Properties.Settings.Default.TORQ_C86LlenL5,
                                      Properties.Settings.Default.TORQ_C96LlenL5, Properties.Settings.Default.TORQ_C106LlenL5, Properties.Settings.Default.TORQ_C116LlenL5, Properties.Settings.Default.TORQ_C126LlenL5);

                CompletarRegistros(7, Properties.Settings.Default.TORQ_TipoCierre7LlenL5, Properties.Settings.Default.TORQ_Prov7LlenL5,
                                      Properties.Settings.Default.TORQ_C17LlenL5, Properties.Settings.Default.TORQ_C27LlenL5, Properties.Settings.Default.TORQ_C37LlenL5, Properties.Settings.Default.TORQ_C47LlenL5,
                                      Properties.Settings.Default.TORQ_C57LlenL5, Properties.Settings.Default.TORQ_C67LlenL5, Properties.Settings.Default.TORQ_C77LlenL5, Properties.Settings.Default.TORQ_C87LlenL5,
                                      Properties.Settings.Default.TORQ_C97LlenL5, Properties.Settings.Default.TORQ_C107LlenL5, Properties.Settings.Default.TORQ_C117LlenL5, Properties.Settings.Default.TORQ_C127LlenL5);

            }
            if (MaquinaLinea.CodigoProd != "")
            {
                try
                {
                    ExtraerDatosBOM(MaquinaLinea.CodigoProd);
                    ExtraerDatosMateriales(CodigoMaterial);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    MessageBox.Show("No se ha encontrado la referencia");
                }
            }

            PanelRegistro.Visible = true;
            C1TB.Select();
        }

        /// <summary>
        /// Función del temporizador que se ejecuta cada segundo, indíca la hora y si se activa la alarma. 
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Para activar la alarma debe, estar desactivada, haberse chequeado el inicio de turno y que la hora cuadre con la alarma 
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }
            string Hora = lbReloj.Text;
            if (Hora.Substring(3, 2) != "00" && Hora.Substring(3, 2) != "30")
            {
                MaquinaLinea.AnuladorAlarma = true;
            }
            if (Hora.Substring(3, 2) == "00" || Hora.Substring(3, 2) == "30")
            {
                if (MaquinaLinea.AnuladorAlarma == true)
                {
                    Apps_Llenadora.AlarmaControl30min();
                }
            }
            //if (MaquinaLinea.StatusTeclado == true) Utilidades.EscribirTeclado(numberpad1, TextBox, null);
        }

        /// <summary>
        /// Aciones que muestran el teclado al hacer click en el textbox. 
        /// </summary>
        private void C1TB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(C1TB);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = C1TB;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }
        private void C2TB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(C1TB);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = C2TB;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }
        private void C3TB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(C1TB);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = C3TB;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }
        private void C4TB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(C1TB);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = C4TB;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }
        private void C5TB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(C1TB);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = C5TB;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }
        private void C6TB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(C1TB);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = C6TB;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }
        private void C7TB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(C1TB);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = C7TB;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }
        private void C8TB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(C1TB);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = C8TB;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }
        private void C9TB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(C1TB);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = C9TB;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }
        private void C10TB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(C1TB);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = C10TB;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }
        private void C11TB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(C1TB);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = C11TB;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }
        private void C12TB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(C1TB);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = C12TB;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }

        /// <summary>
        /// Botón que elimina todo los registros. 
        /// </summary>
        private void BorrarB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                EstablecerVariablesL2(0, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL2(1, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL2(2, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL2(3, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL2(4, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL2(5, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL2(6, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL2(7, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            if (MaquinaLinea.numlin == 3)
            {
                EstablecerVariablesL3(0, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL3(1, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL3(2, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL3(3, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL3(4, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL3(5, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL3(6, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL3(7, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            if (MaquinaLinea.numlin == 5)
            {
                EstablecerVariablesL5(0, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL5(1, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL5(2, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL5(3, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL5(4, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL5(5, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL5(6, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                EstablecerVariablesL5(7, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            CompletarRegistros(0, null, null, "", "", "", "", "", "", "", "", "", "", "", "");
            CompletarRegistros(1, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            CompletarRegistros(2, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            CompletarRegistros(3, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            CompletarRegistros(4, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            CompletarRegistros(5, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            CompletarRegistros(6, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            CompletarRegistros(7, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
        }

        /// <summary>
        /// Función que guarda las variables de los registros por filas.
        /// </summary>
        /// <param name="Registro">Fila que se desea completar 0-7.</param>
        /// <param name="TipoCierre">Parámetro que indica el tipo de cierre.</param>
        /// <param name="Proveedor">Parámetro que indica el proveedor del cierre.</param>
        /// <param name="CX">Parámetro que indica el valor del PAR resultante.</param>
        public void EstablecerVariablesL2(int Registro, string TipoCierre, string Proveedor, string C1, string C2, string C3, string C4, string C5, string C6, string C7, string C8, string C9, string C10, string C11, string C12)
        {
            switch (Registro)
            {
                case 0:
                    Properties.Settings.Default.TORQ_Cab1LlenL2 = C1;
                    Properties.Settings.Default.TORQ_Cab2LlenL2 = C2;
                    Properties.Settings.Default.TORQ_Cab3LlenL2 = C3;
                    Properties.Settings.Default.TORQ_Cab4LlenL2 = C4;
                    Properties.Settings.Default.TORQ_Cab5LlenL2 = C5;
                    Properties.Settings.Default.TORQ_Cab6LlenL2 = C6;
                    Properties.Settings.Default.TORQ_Cab7LlenL2 = C7;
                    Properties.Settings.Default.TORQ_Cab8LlenL2 = C8;
                    Properties.Settings.Default.TORQ_Cab9LlenL2 = C9;
                    Properties.Settings.Default.TORQ_Cab10LlenL2 = C10;
                    Properties.Settings.Default.TORQ_Cab11LlenL2 = C11;
                    Properties.Settings.Default.TORQ_Cab12LlenL2 = C12;
                    break;
                case 1:
                    //Fila 1
                    Properties.Settings.Default.TORQ_TipoCierre1LlenL2 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov1LlenL2 = Proveedor;
                    Properties.Settings.Default.TORQ_C11LlenL2 = C1;
                    Properties.Settings.Default.TORQ_C21LlenL2 = C2;
                    Properties.Settings.Default.TORQ_C31LlenL2 = C3;
                    Properties.Settings.Default.TORQ_C41LlenL2 = C4;
                    Properties.Settings.Default.TORQ_C51LlenL2 = C5;
                    Properties.Settings.Default.TORQ_C61LlenL2 = C6;
                    Properties.Settings.Default.TORQ_C71LlenL2 = C7;
                    Properties.Settings.Default.TORQ_C81LlenL2 = C8;
                    Properties.Settings.Default.TORQ_C91LlenL2 = C9;
                    Properties.Settings.Default.TORQ_C101LlenL2 = C10;
                    Properties.Settings.Default.TORQ_C111LlenL2 = C11;
                    Properties.Settings.Default.TORQ_C121LlenL2 = C12;
                    break;
                case 2:
                    //Fila 2
                    Properties.Settings.Default.TORQ_TipoCierre2LlenL2 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov2LlenL2 = Proveedor;
                    Properties.Settings.Default.TORQ_C12LlenL2 = C1;
                    Properties.Settings.Default.TORQ_C22LlenL2 = C2;
                    Properties.Settings.Default.TORQ_C32LlenL2 = C3;
                    Properties.Settings.Default.TORQ_C42LlenL2 = C4;
                    Properties.Settings.Default.TORQ_C52LlenL2 = C5;
                    Properties.Settings.Default.TORQ_C62LlenL2 = C6;
                    Properties.Settings.Default.TORQ_C72LlenL2 = C7;
                    Properties.Settings.Default.TORQ_C82LlenL2 = C8;
                    Properties.Settings.Default.TORQ_C92LlenL2 = C9;
                    Properties.Settings.Default.TORQ_C102LlenL2 = C10;
                    Properties.Settings.Default.TORQ_C112LlenL2 = C11;
                    Properties.Settings.Default.TORQ_C122LlenL2 = C12;
                    break;
                case 3:
                    //Fila 3
                    Properties.Settings.Default.TORQ_TipoCierre3LlenL2 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov3LlenL2 = Proveedor;
                    Properties.Settings.Default.TORQ_C13LlenL2 = C1;
                    Properties.Settings.Default.TORQ_C23LlenL2 = C2;
                    Properties.Settings.Default.TORQ_C33LlenL2 = C3;
                    Properties.Settings.Default.TORQ_C43LlenL2 = C4;
                    Properties.Settings.Default.TORQ_C53LlenL2 = C5;
                    Properties.Settings.Default.TORQ_C63LlenL2 = C6;
                    Properties.Settings.Default.TORQ_C73LlenL2 = C7;
                    Properties.Settings.Default.TORQ_C83LlenL2 = C8;
                    Properties.Settings.Default.TORQ_C93LlenL2 = C9;
                    Properties.Settings.Default.TORQ_C103LlenL2 = C10;
                    Properties.Settings.Default.TORQ_C113LlenL2 = C11;
                    Properties.Settings.Default.TORQ_C123LlenL2 = C12;
                    break;
                case 4:
                    //Fila 4
                    Properties.Settings.Default.TORQ_TipoCierre4LlenL2 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov4LlenL2 = Proveedor;
                    Properties.Settings.Default.TORQ_C14LlenL2 = C1;
                    Properties.Settings.Default.TORQ_C24LlenL2 = C2;
                    Properties.Settings.Default.TORQ_C34LlenL2 = C3;
                    Properties.Settings.Default.TORQ_C44LlenL2 = C4;
                    Properties.Settings.Default.TORQ_C54LlenL2 = C5;
                    Properties.Settings.Default.TORQ_C64LlenL2 = C6;
                    Properties.Settings.Default.TORQ_C74LlenL2 = C7;
                    Properties.Settings.Default.TORQ_C84LlenL2 = C8;
                    Properties.Settings.Default.TORQ_C94LlenL2 = C9;
                    Properties.Settings.Default.TORQ_C104LlenL2 = C10;
                    Properties.Settings.Default.TORQ_C114LlenL2 = C11;
                    Properties.Settings.Default.TORQ_C124LlenL2 = C12;
                    break;
                case 5:
                    //Fila 5
                    Properties.Settings.Default.TORQ_TipoCierre5LlenL2 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov5LlenL2 = Proveedor;
                    Properties.Settings.Default.TORQ_C15LlenL2 = C1;
                    Properties.Settings.Default.TORQ_C25LlenL2 = C2;
                    Properties.Settings.Default.TORQ_C35LlenL2 = C3;
                    Properties.Settings.Default.TORQ_C45LlenL2 = C4;
                    Properties.Settings.Default.TORQ_C55LlenL2 = C5;
                    Properties.Settings.Default.TORQ_C65LlenL2 = C6;
                    Properties.Settings.Default.TORQ_C75LlenL2 = C7;
                    Properties.Settings.Default.TORQ_C85LlenL2 = C8;
                    Properties.Settings.Default.TORQ_C95LlenL2 = C9;
                    Properties.Settings.Default.TORQ_C105LlenL2 = C10;
                    Properties.Settings.Default.TORQ_C115LlenL2 = C11;
                    Properties.Settings.Default.TORQ_C125LlenL2 = C12;
                    break;
                case 6:
                    //Fila 6
                    Properties.Settings.Default.TORQ_TipoCierre6LlenL2 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov6LlenL2 = Proveedor;
                    Properties.Settings.Default.TORQ_C16LlenL2 = C1;
                    Properties.Settings.Default.TORQ_C26LlenL2 = C2;
                    Properties.Settings.Default.TORQ_C36LlenL2 = C3;
                    Properties.Settings.Default.TORQ_C46LlenL2 = C4;
                    Properties.Settings.Default.TORQ_C56LlenL2 = C5;
                    Properties.Settings.Default.TORQ_C66LlenL2 = C6;
                    Properties.Settings.Default.TORQ_C76LlenL2 = C7;
                    Properties.Settings.Default.TORQ_C86LlenL2 = C8;
                    Properties.Settings.Default.TORQ_C96LlenL2 = C9;
                    Properties.Settings.Default.TORQ_C106LlenL2 = C10;
                    Properties.Settings.Default.TORQ_C116LlenL2 = C11;
                    Properties.Settings.Default.TORQ_C126LlenL2 = C12;
                    break;
                case 7:
                    //Fila 7
                    Properties.Settings.Default.TORQ_TipoCierre7LlenL2 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov7LlenL2 = Proveedor;
                    Properties.Settings.Default.TORQ_C17LlenL2 = C1;
                    Properties.Settings.Default.TORQ_C27LlenL2 = C2;
                    Properties.Settings.Default.TORQ_C37LlenL2 = C3;
                    Properties.Settings.Default.TORQ_C47LlenL2 = C4;
                    Properties.Settings.Default.TORQ_C57LlenL2 = C5;
                    Properties.Settings.Default.TORQ_C67LlenL2 = C6;
                    Properties.Settings.Default.TORQ_C77LlenL2 = C7;
                    Properties.Settings.Default.TORQ_C87LlenL2 = C8;
                    Properties.Settings.Default.TORQ_C97LlenL2 = C9;
                    Properties.Settings.Default.TORQ_C107LlenL2 = C10;
                    Properties.Settings.Default.TORQ_C117LlenL2 = C11;
                    Properties.Settings.Default.TORQ_C127LlenL2 = C12;
                    break;
            }
            Properties.Settings.Default.Save();
        }
        public void EstablecerVariablesL3(int Registro, string TipoCierre, string Proveedor, string C1, string C2, string C3, string C4, string C5, string C6, string C7, string C8, string C9, string C10, string C11, string C12)
        {
            switch (Registro)
            {
                case 0:
                    Properties.Settings.Default.TORQ_Cab1LlenL3 = C1;
                    Properties.Settings.Default.TORQ_Cab2LlenL3 = C2;
                    Properties.Settings.Default.TORQ_Cab3LlenL3 = C3;
                    Properties.Settings.Default.TORQ_Cab4LlenL3 = C4;
                    Properties.Settings.Default.TORQ_Cab5LlenL3 = C5;
                    Properties.Settings.Default.TORQ_Cab6LlenL3 = C6;
                    Properties.Settings.Default.TORQ_Cab7LlenL3 = C7;
                    Properties.Settings.Default.TORQ_Cab8LlenL3 = C8;
                    Properties.Settings.Default.TORQ_Cab9LlenL3 = C9;
                    Properties.Settings.Default.TORQ_Cab10LlenL3 = C10;
                    Properties.Settings.Default.TORQ_Cab11LlenL3 = C11;
                    Properties.Settings.Default.TORQ_Cab12LlenL3 = C12;
                    break;
                case 1:
                    //Fila 1
                    Properties.Settings.Default.TORQ_TipoCierre1LlenL3 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov1LlenL3 = Proveedor;
                    Properties.Settings.Default.TORQ_C11LlenL3 = C1;
                    Properties.Settings.Default.TORQ_C21LlenL3 = C2;
                    Properties.Settings.Default.TORQ_C31LlenL3 = C3;
                    Properties.Settings.Default.TORQ_C41LlenL3 = C4;
                    Properties.Settings.Default.TORQ_C51LlenL3 = C5;
                    Properties.Settings.Default.TORQ_C61LlenL3 = C6;
                    Properties.Settings.Default.TORQ_C71LlenL3 = C7;
                    Properties.Settings.Default.TORQ_C81LlenL3 = C8;
                    Properties.Settings.Default.TORQ_C91LlenL3 = C9;
                    Properties.Settings.Default.TORQ_C101LlenL3 = C10;
                    Properties.Settings.Default.TORQ_C111LlenL3 = C11;
                    Properties.Settings.Default.TORQ_C121LlenL3 = C12;
                    break;
                case 2:
                    //Fila 2
                    Properties.Settings.Default.TORQ_TipoCierre2LlenL3 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov2LlenL3 = Proveedor;
                    Properties.Settings.Default.TORQ_C12LlenL3 = C1;
                    Properties.Settings.Default.TORQ_C22LlenL3 = C2;
                    Properties.Settings.Default.TORQ_C32LlenL3 = C3;
                    Properties.Settings.Default.TORQ_C42LlenL3 = C4;
                    Properties.Settings.Default.TORQ_C52LlenL3 = C5;
                    Properties.Settings.Default.TORQ_C62LlenL3 = C6;
                    Properties.Settings.Default.TORQ_C72LlenL3 = C7;
                    Properties.Settings.Default.TORQ_C82LlenL3 = C8;
                    Properties.Settings.Default.TORQ_C92LlenL3 = C9;
                    Properties.Settings.Default.TORQ_C102LlenL3 = C10;
                    Properties.Settings.Default.TORQ_C112LlenL3 = C11;
                    Properties.Settings.Default.TORQ_C122LlenL3 = C12;
                    break;
                case 3:
                    //Fila 3
                    Properties.Settings.Default.TORQ_TipoCierre3LlenL3 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov3LlenL3 = Proveedor;
                    Properties.Settings.Default.TORQ_C13LlenL3 = C1;
                    Properties.Settings.Default.TORQ_C23LlenL3 = C2;
                    Properties.Settings.Default.TORQ_C33LlenL3 = C3;
                    Properties.Settings.Default.TORQ_C43LlenL3 = C4;
                    Properties.Settings.Default.TORQ_C53LlenL3 = C5;
                    Properties.Settings.Default.TORQ_C63LlenL3 = C6;
                    Properties.Settings.Default.TORQ_C73LlenL3 = C7;
                    Properties.Settings.Default.TORQ_C83LlenL3 = C8;
                    Properties.Settings.Default.TORQ_C93LlenL3 = C9;
                    Properties.Settings.Default.TORQ_C103LlenL3 = C10;
                    Properties.Settings.Default.TORQ_C113LlenL3 = C11;
                    Properties.Settings.Default.TORQ_C123LlenL3 = C12;
                    break;
                case 4:
                    //Fila 4
                    Properties.Settings.Default.TORQ_TipoCierre4LlenL3 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov4LlenL3 = Proveedor;
                    Properties.Settings.Default.TORQ_C14LlenL3 = C1;
                    Properties.Settings.Default.TORQ_C24LlenL3 = C2;
                    Properties.Settings.Default.TORQ_C34LlenL3 = C3;
                    Properties.Settings.Default.TORQ_C44LlenL3 = C4;
                    Properties.Settings.Default.TORQ_C54LlenL3 = C5;
                    Properties.Settings.Default.TORQ_C64LlenL3 = C6;
                    Properties.Settings.Default.TORQ_C74LlenL3 = C7;
                    Properties.Settings.Default.TORQ_C84LlenL3 = C8;
                    Properties.Settings.Default.TORQ_C94LlenL3 = C9;
                    Properties.Settings.Default.TORQ_C104LlenL3 = C10;
                    Properties.Settings.Default.TORQ_C114LlenL3 = C11;
                    Properties.Settings.Default.TORQ_C124LlenL3 = C12;
                    break;
                case 5:
                    //Fila 5
                    Properties.Settings.Default.TORQ_TipoCierre5LlenL3 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov5LlenL3 = Proveedor;
                    Properties.Settings.Default.TORQ_C15LlenL3 = C1;
                    Properties.Settings.Default.TORQ_C25LlenL3 = C2;
                    Properties.Settings.Default.TORQ_C35LlenL3 = C3;
                    Properties.Settings.Default.TORQ_C45LlenL3 = C4;
                    Properties.Settings.Default.TORQ_C55LlenL3 = C5;
                    Properties.Settings.Default.TORQ_C65LlenL3 = C6;
                    Properties.Settings.Default.TORQ_C75LlenL3 = C7;
                    Properties.Settings.Default.TORQ_C85LlenL3 = C8;
                    Properties.Settings.Default.TORQ_C95LlenL3 = C9;
                    Properties.Settings.Default.TORQ_C105LlenL3 = C10;
                    Properties.Settings.Default.TORQ_C115LlenL3 = C11;
                    Properties.Settings.Default.TORQ_C125LlenL3 = C12;
                    break;
                case 6:
                    //Fila 6
                    Properties.Settings.Default.TORQ_TipoCierre6LlenL3 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov6LlenL3 = Proveedor;
                    Properties.Settings.Default.TORQ_C16LlenL3 = C1;
                    Properties.Settings.Default.TORQ_C26LlenL3 = C2;
                    Properties.Settings.Default.TORQ_C36LlenL3 = C3;
                    Properties.Settings.Default.TORQ_C46LlenL3 = C4;
                    Properties.Settings.Default.TORQ_C56LlenL3 = C5;
                    Properties.Settings.Default.TORQ_C66LlenL3 = C6;
                    Properties.Settings.Default.TORQ_C76LlenL3 = C7;
                    Properties.Settings.Default.TORQ_C86LlenL3 = C8;
                    Properties.Settings.Default.TORQ_C96LlenL3 = C9;
                    Properties.Settings.Default.TORQ_C106LlenL3 = C10;
                    Properties.Settings.Default.TORQ_C116LlenL3 = C11;
                    Properties.Settings.Default.TORQ_C126LlenL3 = C12;
                    break;
                case 7:
                    //Fila 7
                    Properties.Settings.Default.TORQ_TipoCierre7LlenL3 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov7LlenL3 = Proveedor;
                    Properties.Settings.Default.TORQ_C17LlenL3 = C1;
                    Properties.Settings.Default.TORQ_C27LlenL3 = C2;
                    Properties.Settings.Default.TORQ_C37LlenL3 = C3;
                    Properties.Settings.Default.TORQ_C47LlenL3 = C4;
                    Properties.Settings.Default.TORQ_C57LlenL3 = C5;
                    Properties.Settings.Default.TORQ_C67LlenL3 = C6;
                    Properties.Settings.Default.TORQ_C77LlenL3 = C7;
                    Properties.Settings.Default.TORQ_C87LlenL3 = C8;
                    Properties.Settings.Default.TORQ_C97LlenL3 = C9;
                    Properties.Settings.Default.TORQ_C107LlenL3 = C10;
                    Properties.Settings.Default.TORQ_C117LlenL3 = C11;
                    Properties.Settings.Default.TORQ_C127LlenL3 = C12;
                    break;
            }
            Properties.Settings.Default.Save();
        }
        public void EstablecerVariablesL5(int Registro, string TipoCierre, string Proveedor, string C1, string C2, string C3, string C4, string C5, string C6, string C7, string C8, string C9, string C10, string C11, string C12)
        {
            switch (Registro)
            {
                case 0:
                    Properties.Settings.Default.TORQ_Cab1LlenL5 = C1;
                    Properties.Settings.Default.TORQ_Cab2LlenL5 = C2;
                    Properties.Settings.Default.TORQ_Cab3LlenL5 = C3;
                    Properties.Settings.Default.TORQ_Cab4LlenL5 = C4;
                    Properties.Settings.Default.TORQ_Cab5LlenL5 = C5;
                    Properties.Settings.Default.TORQ_Cab6LlenL5 = C6;
                    Properties.Settings.Default.TORQ_Cab7LlenL5 = C7;
                    Properties.Settings.Default.TORQ_Cab8LlenL5 = C8;
                    Properties.Settings.Default.TORQ_Cab9LlenL5 = C9;
                    Properties.Settings.Default.TORQ_Cab10LlenL5 = C10;
                    Properties.Settings.Default.TORQ_Cab11LlenL5 = C11;
                    Properties.Settings.Default.TORQ_Cab12LlenL5 = C12;
                    break;
                case 1:
                    //Fila 1
                    Properties.Settings.Default.TORQ_TipoCierre1LlenL5 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov1LlenL5 = Proveedor;
                    Properties.Settings.Default.TORQ_C11LlenL5 = C1;
                    Properties.Settings.Default.TORQ_C21LlenL5 = C2;
                    Properties.Settings.Default.TORQ_C31LlenL5 = C3;
                    Properties.Settings.Default.TORQ_C41LlenL5 = C4;
                    Properties.Settings.Default.TORQ_C51LlenL5 = C5;
                    Properties.Settings.Default.TORQ_C61LlenL5 = C6;
                    Properties.Settings.Default.TORQ_C71LlenL5 = C7;
                    Properties.Settings.Default.TORQ_C81LlenL5 = C8;
                    Properties.Settings.Default.TORQ_C91LlenL5 = C9;
                    Properties.Settings.Default.TORQ_C101LlenL5 = C10;
                    Properties.Settings.Default.TORQ_C111LlenL5 = C11;
                    Properties.Settings.Default.TORQ_C121LlenL5 = C12;
                    break;
                case 2:
                    //Fila 2
                    Properties.Settings.Default.TORQ_TipoCierre2LlenL5 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov2LlenL5 = Proveedor;
                    Properties.Settings.Default.TORQ_C12LlenL5 = C1;
                    Properties.Settings.Default.TORQ_C22LlenL5 = C2;
                    Properties.Settings.Default.TORQ_C32LlenL5 = C3;
                    Properties.Settings.Default.TORQ_C42LlenL5 = C4;
                    Properties.Settings.Default.TORQ_C52LlenL5 = C5;
                    Properties.Settings.Default.TORQ_C62LlenL5 = C6;
                    Properties.Settings.Default.TORQ_C72LlenL5 = C7;
                    Properties.Settings.Default.TORQ_C82LlenL5 = C8;
                    Properties.Settings.Default.TORQ_C92LlenL5 = C9;
                    Properties.Settings.Default.TORQ_C102LlenL5 = C10;
                    Properties.Settings.Default.TORQ_C112LlenL5 = C11;
                    Properties.Settings.Default.TORQ_C122LlenL5 = C12;
                    break;
                case 3:
                    //Fila 3
                    Properties.Settings.Default.TORQ_TipoCierre3LlenL5 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov3LlenL5 = Proveedor;
                    Properties.Settings.Default.TORQ_C13LlenL5 = C1;
                    Properties.Settings.Default.TORQ_C23LlenL5 = C2;
                    Properties.Settings.Default.TORQ_C33LlenL5 = C3;
                    Properties.Settings.Default.TORQ_C43LlenL5 = C4;
                    Properties.Settings.Default.TORQ_C53LlenL5 = C5;
                    Properties.Settings.Default.TORQ_C63LlenL5 = C6;
                    Properties.Settings.Default.TORQ_C73LlenL5 = C7;
                    Properties.Settings.Default.TORQ_C83LlenL5 = C8;
                    Properties.Settings.Default.TORQ_C93LlenL5 = C9;
                    Properties.Settings.Default.TORQ_C103LlenL5 = C10;
                    Properties.Settings.Default.TORQ_C113LlenL5 = C11;
                    Properties.Settings.Default.TORQ_C123LlenL5 = C12;
                    break;
                case 4:
                    //Fila 4
                    Properties.Settings.Default.TORQ_TipoCierre4LlenL5 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov4LlenL5 = Proveedor;
                    Properties.Settings.Default.TORQ_C14LlenL5 = C1;
                    Properties.Settings.Default.TORQ_C24LlenL5 = C2;
                    Properties.Settings.Default.TORQ_C34LlenL5 = C3;
                    Properties.Settings.Default.TORQ_C44LlenL5 = C4;
                    Properties.Settings.Default.TORQ_C54LlenL5 = C5;
                    Properties.Settings.Default.TORQ_C64LlenL5 = C6;
                    Properties.Settings.Default.TORQ_C74LlenL5 = C7;
                    Properties.Settings.Default.TORQ_C84LlenL5 = C8;
                    Properties.Settings.Default.TORQ_C94LlenL5 = C9;
                    Properties.Settings.Default.TORQ_C104LlenL5 = C10;
                    Properties.Settings.Default.TORQ_C114LlenL5 = C11;
                    Properties.Settings.Default.TORQ_C124LlenL5 = C12;
                    break;
                case 5:
                    //Fila 5
                    Properties.Settings.Default.TORQ_TipoCierre5LlenL5 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov5LlenL5 = Proveedor;
                    Properties.Settings.Default.TORQ_C15LlenL5 = C1;
                    Properties.Settings.Default.TORQ_C25LlenL5 = C2;
                    Properties.Settings.Default.TORQ_C35LlenL5 = C3;
                    Properties.Settings.Default.TORQ_C45LlenL5 = C4;
                    Properties.Settings.Default.TORQ_C55LlenL5 = C5;
                    Properties.Settings.Default.TORQ_C65LlenL5 = C6;
                    Properties.Settings.Default.TORQ_C75LlenL5 = C7;
                    Properties.Settings.Default.TORQ_C85LlenL5 = C8;
                    Properties.Settings.Default.TORQ_C95LlenL5 = C9;
                    Properties.Settings.Default.TORQ_C105LlenL5 = C10;
                    Properties.Settings.Default.TORQ_C115LlenL5 = C11;
                    Properties.Settings.Default.TORQ_C125LlenL5 = C12;
                    break;
                case 6:
                    //Fila 6
                    Properties.Settings.Default.TORQ_TipoCierre6LlenL5 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov6LlenL5 = Proveedor;
                    Properties.Settings.Default.TORQ_C16LlenL5 = C1;
                    Properties.Settings.Default.TORQ_C26LlenL5 = C2;
                    Properties.Settings.Default.TORQ_C36LlenL5 = C3;
                    Properties.Settings.Default.TORQ_C46LlenL5 = C4;
                    Properties.Settings.Default.TORQ_C56LlenL5 = C5;
                    Properties.Settings.Default.TORQ_C66LlenL5 = C6;
                    Properties.Settings.Default.TORQ_C76LlenL5 = C7;
                    Properties.Settings.Default.TORQ_C86LlenL5 = C8;
                    Properties.Settings.Default.TORQ_C96LlenL5 = C9;
                    Properties.Settings.Default.TORQ_C106LlenL5 = C10;
                    Properties.Settings.Default.TORQ_C116LlenL5 = C11;
                    Properties.Settings.Default.TORQ_C126LlenL5 = C12;
                    break;
                case 7:
                    //Fila 7
                    Properties.Settings.Default.TORQ_TipoCierre7LlenL5 = TipoCierre;
                    Properties.Settings.Default.TORQ_Prov7LlenL5 = Proveedor;
                    Properties.Settings.Default.TORQ_C17LlenL5 = C1;
                    Properties.Settings.Default.TORQ_C27LlenL5 = C2;
                    Properties.Settings.Default.TORQ_C37LlenL5 = C3;
                    Properties.Settings.Default.TORQ_C47LlenL5 = C4;
                    Properties.Settings.Default.TORQ_C57LlenL5 = C5;
                    Properties.Settings.Default.TORQ_C67LlenL5 = C6;
                    Properties.Settings.Default.TORQ_C77LlenL5 = C7;
                    Properties.Settings.Default.TORQ_C87LlenL5 = C8;
                    Properties.Settings.Default.TORQ_C97LlenL5 = C9;
                    Properties.Settings.Default.TORQ_C107LlenL5 = C10;
                    Properties.Settings.Default.TORQ_C117LlenL5 = C11;
                    Properties.Settings.Default.TORQ_C127LlenL5 = C12;
                    break;
            }
            Properties.Settings.Default.Save();
        }
        /// <summary>
        /// Función que muestra en pantalla los registros por filas.
        /// </summary>
        /// <param name="Registro">Fila que se desea completar 0-7.</param>
        /// <param name="TipoCierre">Parámetro que indica el tipo de cierre.</param>
        /// <param name="Proveedor">Parámetro que indica el proveedor del cierre.</param>
        /// <param name="CX">Parámetro que indica el valor del PAR resultante.</param>
        public void CompletarRegistros(int Registro, string TipoCierre, string Proveedor, string C1, string C2, string C3, string C4, string C5, string C6, string C7, string C8, string C9, string C10, string C11, string C12)
        {
            switch (Registro)
            {
                case 0:
                    if (TipoCierre != null && Proveedor != null)
                    {
                        TipoCierreMetalicoTB.Text = TipoCierre;
                        ProveedorTB.Text = Proveedor;
                    }
                    C1TB.Text = C1;
                    C2TB.Text = C2;
                    C3TB.Text = C3;
                    C4TB.Text = C4;
                    C5TB.Text = C5;
                    C6TB.Text = C6;
                    C7TB.Text = C7;
                    C8TB.Text = C8;
                    C9TB.Text = C9;
                    C10TB.Text = C10;
                    C11TB.Text = C11;
                    C12TB.Text = C12;
                    break;
                case 1:
                    TipoCierreMetalico1TB.Text = TipoCierre;
                    Proveedor1TB.Text = Proveedor;
                    Cab11TB.Text = C1;
                    Cab21TB.Text = C2;
                    Cab31TB.Text = C3;
                    Cab41TB.Text = C4;
                    Cab51TB.Text = C5;
                    Cab61TB.Text = C6;
                    Cab71TB.Text = C7;
                    Cab81TB.Text = C8;
                    Cab91TB.Text = C9;
                    Cab101TB.Text = C10;
                    Cab111TB.Text = C11;
                    Cab121TB.Text = C12;
                    break;
                case 2:
                    TipoCierreMetalico2TB.Text = TipoCierre;
                    Proveedor2TB.Text = Proveedor;
                    Cab12TB.Text = C1;
                    Cab22TB.Text = C2;
                    Cab32TB.Text = C3;
                    Cab42TB.Text = C4;
                    Cab52TB.Text = C5;
                    Cab62TB.Text = C6;
                    Cab72TB.Text = C7;
                    Cab82TB.Text = C8;
                    Cab92TB.Text = C9;
                    Cab102TB.Text = C10;
                    Cab112TB.Text = C11;
                    Cab122TB.Text = C12;
                    break;
                case 3:
                    TipoCierreMetalico3TB.Text = TipoCierre;
                    Proveedor3TB.Text = Proveedor;
                    Cab13TB.Text = C1;
                    Cab23TB.Text = C2;
                    Cab33TB.Text = C3;
                    Cab43TB.Text = C4;
                    Cab53TB.Text = C5;
                    Cab63TB.Text = C6;
                    Cab73TB.Text = C7;
                    Cab83TB.Text = C8;
                    Cab93TB.Text = C9;
                    Cab103TB.Text = C10;
                    Cab113TB.Text = C11;
                    Cab123TB.Text = C12;
                    break;
                case 4:
                    TipoCierreMetalico4TB.Text = TipoCierre;
                    Proveedor4TB.Text = Proveedor;
                    Cab14TB.Text = C1;
                    Cab24TB.Text = C2;
                    Cab34TB.Text = C3;
                    Cab44TB.Text = C4;
                    Cab54TB.Text = C5;
                    Cab64TB.Text = C6;
                    Cab74TB.Text = C7;
                    Cab84TB.Text = C8;
                    Cab94TB.Text = C9;
                    Cab104TB.Text = C10;
                    Cab114TB.Text = C11;
                    Cab124TB.Text = C12;
                    break;
                case 5:
                    TipoCierreMetalico5TB.Text = TipoCierre;
                    Proveedor5TB.Text = Proveedor;
                    Cab15TB.Text = C1;
                    Cab25TB.Text = C2;
                    Cab35TB.Text = C3;
                    Cab45TB.Text = C4;
                    Cab55TB.Text = C5;
                    Cab65TB.Text = C6;
                    Cab75TB.Text = C7;
                    Cab85TB.Text = C8;
                    Cab95TB.Text = C9;
                    Cab105TB.Text = C10;
                    Cab115TB.Text = C11;
                    Cab125TB.Text = C12;
                    break;
                case 6:
                    TipoCierreMetalico6TB.Text = TipoCierre;
                    Proveedor6TB.Text = Proveedor;
                    Cab16TB.Text = C1;
                    Cab26TB.Text = C2;
                    Cab36TB.Text = C3;
                    Cab46TB.Text = C4;
                    Cab56TB.Text = C5;
                    Cab66TB.Text = C6;
                    Cab76TB.Text = C7;
                    Cab86TB.Text = C8;
                    Cab96TB.Text = C9;
                    Cab106TB.Text = C10;
                    Cab116TB.Text = C11;
                    Cab126TB.Text = C12;
                    break;
                case 7:
                    TipoCierreMetalico7TB.Text = TipoCierre;
                    Proveedor7TB.Text = Proveedor;
                    Cab17TB.Text = C1;
                    Cab27TB.Text = C2;
                    Cab37TB.Text = C3;
                    Cab47TB.Text = C4;
                    Cab57TB.Text = C5;
                    Cab67TB.Text = C6;
                    Cab77TB.Text = C7;
                    Cab87TB.Text = C8;
                    Cab97TB.Text = C9;
                    Cab107TB.Text = C10;
                    Cab117TB.Text = C11;
                    Cab127TB.Text = C12;
                    break;
            }
        }

        /// <summary>
        /// Función que busca el código del cierre.
        /// </summary>
        /// <param name="Referencia">Parámetro que inidica el código del producto que se esta procesadon y se extrae del main.</param>
        /// <returns>Devuelve un código del cierre</returns>
        public void ExtraerDatosBOM(string Referencia)
        {
            int i = 0;
            while (i < 3)
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
                    filterval1[3] = "'%" + "TAP." + "%'";
                    valoresAFiltrar.Add(filterval1);
                }
                if (i == 1)
                {
                    string[] filterval2 = new string[4];
                    filterval2[0] = "AND";
                    filterval2[1] = "DescMaterial";
                    filterval2[2] = "LIKE";
                    filterval2[3] = "'%" + "CAPS." + "%'";
                    valoresAFiltrar.Add(filterval2);
                }
                if (i == 2)
                {
                    string[] filterval3 = new string[4];
                    filterval3[0] = "AND";
                    filterval3[1] = "DescMaterial";
                    filterval3[2] = "LIKE";
                    filterval3[3] = "'%" + "C.MET" + "%'";
                    valoresAFiltrar.Add(filterval3);
                }
                DataSet excelDataSet = new DataSet();
                string result;
                //List<string[]> valoresAFiltrar = dgvSelectFiltro.DataSource;
                excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileBOM, "FICHA", "CodMaterial ".Split(';'), valoresAFiltrar, out result);
                //tbSelectSalidaError.Text = result;
                //MessageBox.Show(result);
                if (excelDataSet.Tables[0].Rows.Count > 0)
                {
                    CodigoMaterial = Convert.ToString(excelDataSet.Tables[0].Rows[0]["CodMaterial"]);
                    i = 3;
                }
                valoresAFiltrar.Clear();
                i++;
            }
        }
        /// <summary>
        /// Función que completa los textbox de la descripcion del cierre y el proveedor del mismo.
        /// </summary>
        /// <param name="CodigoMaterial">Código extraido de la funcion EstraerDatosBOM.</param>
        public void ExtraerDatosMateriales(string CodigoMaterial)
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
                excelDataSet = ExcelUtiles.LeerFicheroExcel(MaquinaLinea.FileMateriales, "Cierres", "Descripcion; Proveedor; Pmax; Pmin".Split(';'), valoresAFiltrar, out result);
                //tbSelectSalidaError.Text = result;
                //MessageBox.Show(result);
                if (excelDataSet.Tables[0].Rows.Count > 0)
                {
                    TipoCierreMetalicoTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Descripcion"]);
                    ProveedorTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Proveedor"]);
                    PmaxTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Pmax"]);
                    PminTB.Text = Convert.ToString(excelDataSet.Tables[0].Rows[0]["Pmin"]);
                }
                else MessageBox.Show("No se ha entontrado la descripción del cierre, comunicalo al responsable");
            }
        }

        private void saveBot_Click(object sender, EventArgs e)
        {
            if (C1TB.Text != "" && C2TB.Text != "" && C3TB.Text != "" && C4TB.Text != "" && C5TB.Text != "" && C6TB.Text != "" && C7TB.Text != "" && C8TB.Text != "" && C9TB.Text != "" && C10TB.Text != "" && C11TB.Text != "" && C12TB.Text != "")
            {

                List<string[]> listavalores = new List<string[]>();
                string[] valores0 = new string[2];
                string[] valores1 = new string[2];
                string[] valores01 = new string[2];
                string[] valores2 = new string[2];
                string[] valores3 = new string[2];
                string[] valores4 = new string[2];
                string[] valores5 = new string[2];
                string[] valores6 = new string[2];
                string[] valores7 = new string[2];
                string[] valores8 = new string[2];
                string[] valores9 = new string[2];
                string[] valores10 = new string[2];
                string[] valores11 = new string[2];
                string[] valores12 = new string[2];
                string[] valores13 = new string[2];
                string[] valores14 = new string[2];
                string[] valores15 = new string[2];
                string[] valores16 = new string[2];
                string[] valores17 = new string[2];
                string[] valores18 = new string[2];
                string[] valores19 = new string[2];

                valores0[0] = "Fecha";
                valores0[1] = DateTime.Now.ToString("dd/MM/yyyy");
                listavalores.Add(valores0);
                valores1[0] = "Hora";
                valores1[1] = DateTime.Now.ToString("HH:mm:ss");
                listavalores.Add(valores1);
                valores01[0] = "FechaDB";
                valores01[1] = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
                listavalores.Add(valores01);
                valores2[0] = "Responsable";
                valores2[1] = MaquinaLinea.Responsable;
                listavalores.Add(valores2);
                valores3[0] = "Maquinista";
                valores3[1] = MaquinaLinea.MLlenadora;
                listavalores.Add(valores3);
                valores4[0] = "Turno";
                valores4[1] = turnoTB.Text;
                listavalores.Add(valores4);
                valores5[0] = "TipoCierre";
                valores5[1] = TipoCierreMetalicoTB.Text;
                listavalores.Add(valores5);
                valores6[0] = "Proveedores";
                valores6[1] = ProveedorTB.Text;
                listavalores.Add(valores6);
                valores7[0] = "C1";
                valores7[1] = C1TB.Text;
                listavalores.Add(valores7);
                valores8[0] = "C2";
                valores8[1] = C2TB.Text;
                listavalores.Add(valores8);
                valores9[0] = "C3";
                valores9[1] = C3TB.Text;
                listavalores.Add(valores9);
                valores10[0] = "C4";
                valores10[1] = C4TB.Text;
                listavalores.Add(valores10);
                valores11[0] = "C5";
                valores11[1] = C5TB.Text;
                listavalores.Add(valores11);
                valores12[0] = "C6";
                valores12[1] = C6TB.Text;
                listavalores.Add(valores12);
                valores13[0] = "C7";
                valores13[1] = C7TB.Text;
                listavalores.Add(valores13);
                valores14[0] = "C8";
                valores14[1] = C8TB.Text;
                listavalores.Add(valores14);
                valores15[0] = "C9";
                valores15[1] = C9TB.Text;
                listavalores.Add(valores15);
                valores16[0] = "C10";
                valores16[1] = C10TB.Text;
                listavalores.Add(valores16);
                valores17[0] = "C11";
                valores17[1] = C11TB.Text;
                listavalores.Add(valores17);
                valores18[0] = "C12";
                valores18[1] = C12TB.Text;
                listavalores.Add(valores18);
                valores19[0] = "Comentarios";
                valores19[1] = ComentariosTB.Text;
                listavalores.Add(valores19);
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileLlenadora, "Torquimetro", listavalores, "Id");
                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                }
                else
                {
                    if (MaquinaLinea.numlin == 2)
                    {

                        if (Cab17TB.Text == "" && Cab16TB.Text != "")
                        {
                            EstablecerVariablesL2(7, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(7, Properties.Settings.Default.TORQ_TipoCierre7LlenL2, Properties.Settings.Default.TORQ_Prov7LlenL2,
                                                  Properties.Settings.Default.TORQ_C17LlenL2, Properties.Settings.Default.TORQ_C27LlenL2, Properties.Settings.Default.TORQ_C37LlenL2, Properties.Settings.Default.TORQ_C47LlenL2,
                                                  Properties.Settings.Default.TORQ_C57LlenL2, Properties.Settings.Default.TORQ_C67LlenL2, Properties.Settings.Default.TORQ_C77LlenL2, Properties.Settings.Default.TORQ_C87LlenL2,
                                                  Properties.Settings.Default.TORQ_C97LlenL2, Properties.Settings.Default.TORQ_C107LlenL2, Properties.Settings.Default.TORQ_C117LlenL2, Properties.Settings.Default.TORQ_C127LlenL2);
                        }
                        if (Cab16TB.Text == "" && Cab15TB.Text != "")
                        {
                            EstablecerVariablesL2(6, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(6, Properties.Settings.Default.TORQ_TipoCierre6LlenL2, Properties.Settings.Default.TORQ_Prov6LlenL2,
                                                  Properties.Settings.Default.TORQ_C16LlenL2, Properties.Settings.Default.TORQ_C26LlenL2, Properties.Settings.Default.TORQ_C36LlenL2, Properties.Settings.Default.TORQ_C46LlenL2,
                                                  Properties.Settings.Default.TORQ_C56LlenL2, Properties.Settings.Default.TORQ_C66LlenL2, Properties.Settings.Default.TORQ_C76LlenL2, Properties.Settings.Default.TORQ_C86LlenL2,
                                                  Properties.Settings.Default.TORQ_C96LlenL2, Properties.Settings.Default.TORQ_C106LlenL2, Properties.Settings.Default.TORQ_C116LlenL2, Properties.Settings.Default.TORQ_C126LlenL2);
                        }
                        if (Cab15TB.Text == "" && Cab14TB.Text != "")
                        {
                            EstablecerVariablesL2(5, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(5, Properties.Settings.Default.TORQ_TipoCierre5LlenL2, Properties.Settings.Default.TORQ_Prov5LlenL2,
                                            Properties.Settings.Default.TORQ_C15LlenL2, Properties.Settings.Default.TORQ_C25LlenL2, Properties.Settings.Default.TORQ_C35LlenL2, Properties.Settings.Default.TORQ_C45LlenL2,
                                            Properties.Settings.Default.TORQ_C55LlenL2, Properties.Settings.Default.TORQ_C65LlenL2, Properties.Settings.Default.TORQ_C75LlenL2, Properties.Settings.Default.TORQ_C85LlenL2,
                                            Properties.Settings.Default.TORQ_C95LlenL2, Properties.Settings.Default.TORQ_C105LlenL2, Properties.Settings.Default.TORQ_C115LlenL2, Properties.Settings.Default.TORQ_C125LlenL2);
                        }
                        if (Cab14TB.Text == "" && Cab13TB.Text != "")
                        {
                            EstablecerVariablesL2(4, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(4, Properties.Settings.Default.TORQ_TipoCierre4LlenL2, Properties.Settings.Default.TORQ_Prov4LlenL2,
                              Properties.Settings.Default.TORQ_C14LlenL2, Properties.Settings.Default.TORQ_C24LlenL2, Properties.Settings.Default.TORQ_C34LlenL2, Properties.Settings.Default.TORQ_C44LlenL2,
                              Properties.Settings.Default.TORQ_C54LlenL2, Properties.Settings.Default.TORQ_C64LlenL2, Properties.Settings.Default.TORQ_C74LlenL2, Properties.Settings.Default.TORQ_C84LlenL2,
                              Properties.Settings.Default.TORQ_C94LlenL2, Properties.Settings.Default.TORQ_C104LlenL2, Properties.Settings.Default.TORQ_C114LlenL2, Properties.Settings.Default.TORQ_C124LlenL2);
                        }
                        if (Cab13TB.Text == "" && Cab12TB.Text != "")
                        {
                            EstablecerVariablesL2(3, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(3, Properties.Settings.Default.TORQ_TipoCierre3LlenL2, Properties.Settings.Default.TORQ_Prov3LlenL2,
                                                  Properties.Settings.Default.TORQ_C13LlenL2, Properties.Settings.Default.TORQ_C23LlenL2, Properties.Settings.Default.TORQ_C33LlenL2, Properties.Settings.Default.TORQ_C43LlenL2,
                                                  Properties.Settings.Default.TORQ_C53LlenL2, Properties.Settings.Default.TORQ_C63LlenL2, Properties.Settings.Default.TORQ_C73LlenL2, Properties.Settings.Default.TORQ_C83LlenL2,
                                                  Properties.Settings.Default.TORQ_C93LlenL2, Properties.Settings.Default.TORQ_C103LlenL2, Properties.Settings.Default.TORQ_C113LlenL2, Properties.Settings.Default.TORQ_C123LlenL2);
                        }
                        if (Cab12TB.Text == "" && Cab11TB.Text != "")
                        {
                            EstablecerVariablesL2(2, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);

                            CompletarRegistros(2, Properties.Settings.Default.TORQ_TipoCierre2LlenL2, Properties.Settings.Default.TORQ_Prov2LlenL2,
                                                  Properties.Settings.Default.TORQ_C12LlenL2, Properties.Settings.Default.TORQ_C22LlenL2, Properties.Settings.Default.TORQ_C32LlenL2, Properties.Settings.Default.TORQ_C42LlenL2,
                                                  Properties.Settings.Default.TORQ_C52LlenL2, Properties.Settings.Default.TORQ_C62LlenL2, Properties.Settings.Default.TORQ_C72LlenL2, Properties.Settings.Default.TORQ_C82LlenL2,
                                                  Properties.Settings.Default.TORQ_C92LlenL2, Properties.Settings.Default.TORQ_C102LlenL2, Properties.Settings.Default.TORQ_C112LlenL2, Properties.Settings.Default.TORQ_C122LlenL2);
                        }
                        if (Cab11TB.Text == "")
                        {
                            EstablecerVariablesL2(1, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(1, Properties.Settings.Default.TORQ_TipoCierre1LlenL2, Properties.Settings.Default.TORQ_Prov1LlenL2,
                                                  Properties.Settings.Default.TORQ_C11LlenL2, Properties.Settings.Default.TORQ_C21LlenL2, Properties.Settings.Default.TORQ_C31LlenL2, Properties.Settings.Default.TORQ_C41LlenL2,
                                                  Properties.Settings.Default.TORQ_C51LlenL2, Properties.Settings.Default.TORQ_C61LlenL2, Properties.Settings.Default.TORQ_C71LlenL2, Properties.Settings.Default.TORQ_C81LlenL2,
                                                  Properties.Settings.Default.TORQ_C91LlenL2, Properties.Settings.Default.TORQ_C101LlenL2, Properties.Settings.Default.TORQ_C111LlenL2, Properties.Settings.Default.TORQ_C121LlenL2);
                        }
                    }
                    if (MaquinaLinea.numlin == 3)
                    {

                        if (Cab17TB.Text == "" && Cab16TB.Text != "")
                        {
                            EstablecerVariablesL3(7, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(7, Properties.Settings.Default.TORQ_TipoCierre7LlenL3, Properties.Settings.Default.TORQ_Prov7LlenL3,
                                                  Properties.Settings.Default.TORQ_C17LlenL3, Properties.Settings.Default.TORQ_C27LlenL3, Properties.Settings.Default.TORQ_C37LlenL3, Properties.Settings.Default.TORQ_C47LlenL3,
                                                  Properties.Settings.Default.TORQ_C57LlenL3, Properties.Settings.Default.TORQ_C67LlenL3, Properties.Settings.Default.TORQ_C77LlenL3, Properties.Settings.Default.TORQ_C87LlenL3,
                                                  Properties.Settings.Default.TORQ_C97LlenL3, Properties.Settings.Default.TORQ_C107LlenL3, Properties.Settings.Default.TORQ_C117LlenL3, Properties.Settings.Default.TORQ_C127LlenL3);
                        }
                        if (Cab16TB.Text == "" && Cab15TB.Text != "")
                        {
                            EstablecerVariablesL3(6, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(6, Properties.Settings.Default.TORQ_TipoCierre6LlenL3, Properties.Settings.Default.TORQ_Prov6LlenL3,
                                                  Properties.Settings.Default.TORQ_C16LlenL3, Properties.Settings.Default.TORQ_C26LlenL3, Properties.Settings.Default.TORQ_C36LlenL3, Properties.Settings.Default.TORQ_C46LlenL3,
                                                  Properties.Settings.Default.TORQ_C56LlenL3, Properties.Settings.Default.TORQ_C66LlenL3, Properties.Settings.Default.TORQ_C76LlenL3, Properties.Settings.Default.TORQ_C86LlenL3,
                                                  Properties.Settings.Default.TORQ_C96LlenL3, Properties.Settings.Default.TORQ_C106LlenL3, Properties.Settings.Default.TORQ_C116LlenL3, Properties.Settings.Default.TORQ_C126LlenL3);
                        }
                        if (Cab15TB.Text == "" && Cab14TB.Text != "")
                        {
                            EstablecerVariablesL3(5, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(5, Properties.Settings.Default.TORQ_TipoCierre5LlenL3, Properties.Settings.Default.TORQ_Prov5LlenL3,
                                            Properties.Settings.Default.TORQ_C15LlenL3, Properties.Settings.Default.TORQ_C25LlenL3, Properties.Settings.Default.TORQ_C35LlenL3, Properties.Settings.Default.TORQ_C45LlenL3,
                                            Properties.Settings.Default.TORQ_C55LlenL3, Properties.Settings.Default.TORQ_C65LlenL3, Properties.Settings.Default.TORQ_C75LlenL3, Properties.Settings.Default.TORQ_C85LlenL3,
                                            Properties.Settings.Default.TORQ_C95LlenL3, Properties.Settings.Default.TORQ_C105LlenL3, Properties.Settings.Default.TORQ_C115LlenL3, Properties.Settings.Default.TORQ_C125LlenL3);
                        }
                        if (Cab14TB.Text == "" && Cab13TB.Text != "")
                        {
                            EstablecerVariablesL3(4, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(4, Properties.Settings.Default.TORQ_TipoCierre4LlenL3, Properties.Settings.Default.TORQ_Prov4LlenL3,
                              Properties.Settings.Default.TORQ_C14LlenL3, Properties.Settings.Default.TORQ_C24LlenL3, Properties.Settings.Default.TORQ_C34LlenL3, Properties.Settings.Default.TORQ_C44LlenL3,
                              Properties.Settings.Default.TORQ_C54LlenL3, Properties.Settings.Default.TORQ_C64LlenL3, Properties.Settings.Default.TORQ_C74LlenL3, Properties.Settings.Default.TORQ_C84LlenL3,
                              Properties.Settings.Default.TORQ_C94LlenL3, Properties.Settings.Default.TORQ_C104LlenL3, Properties.Settings.Default.TORQ_C114LlenL3, Properties.Settings.Default.TORQ_C124LlenL3);
                        }
                        if (Cab13TB.Text == "" && Cab12TB.Text != "")
                        {
                            EstablecerVariablesL3(3, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(3, Properties.Settings.Default.TORQ_TipoCierre3LlenL3, Properties.Settings.Default.TORQ_Prov3LlenL3,
                                                  Properties.Settings.Default.TORQ_C13LlenL3, Properties.Settings.Default.TORQ_C23LlenL3, Properties.Settings.Default.TORQ_C33LlenL3, Properties.Settings.Default.TORQ_C43LlenL3,
                                                  Properties.Settings.Default.TORQ_C53LlenL3, Properties.Settings.Default.TORQ_C63LlenL3, Properties.Settings.Default.TORQ_C73LlenL3, Properties.Settings.Default.TORQ_C83LlenL3,
                                                  Properties.Settings.Default.TORQ_C93LlenL3, Properties.Settings.Default.TORQ_C103LlenL3, Properties.Settings.Default.TORQ_C113LlenL3, Properties.Settings.Default.TORQ_C123LlenL3);
                        }
                        if (Cab12TB.Text == "" && Cab11TB.Text != "")
                        {
                            EstablecerVariablesL3(2, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);

                            CompletarRegistros(2, Properties.Settings.Default.TORQ_TipoCierre2LlenL3, Properties.Settings.Default.TORQ_Prov2LlenL3,
                                                  Properties.Settings.Default.TORQ_C12LlenL3, Properties.Settings.Default.TORQ_C22LlenL3, Properties.Settings.Default.TORQ_C32LlenL3, Properties.Settings.Default.TORQ_C42LlenL3,
                                                  Properties.Settings.Default.TORQ_C52LlenL3, Properties.Settings.Default.TORQ_C62LlenL3, Properties.Settings.Default.TORQ_C72LlenL3, Properties.Settings.Default.TORQ_C82LlenL3,
                                                  Properties.Settings.Default.TORQ_C92LlenL3, Properties.Settings.Default.TORQ_C102LlenL3, Properties.Settings.Default.TORQ_C112LlenL3, Properties.Settings.Default.TORQ_C122LlenL3);
                        }
                        if (Cab11TB.Text == "")
                        {
                            EstablecerVariablesL3(1, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(1, Properties.Settings.Default.TORQ_TipoCierre1LlenL3, Properties.Settings.Default.TORQ_Prov1LlenL3,
                                                  Properties.Settings.Default.TORQ_C11LlenL3, Properties.Settings.Default.TORQ_C21LlenL3, Properties.Settings.Default.TORQ_C31LlenL3, Properties.Settings.Default.TORQ_C41LlenL3,
                                                  Properties.Settings.Default.TORQ_C51LlenL3, Properties.Settings.Default.TORQ_C61LlenL3, Properties.Settings.Default.TORQ_C71LlenL3, Properties.Settings.Default.TORQ_C81LlenL3,
                                                  Properties.Settings.Default.TORQ_C91LlenL3, Properties.Settings.Default.TORQ_C101LlenL3, Properties.Settings.Default.TORQ_C111LlenL3, Properties.Settings.Default.TORQ_C121LlenL3);
                        }
                    }
                    if (MaquinaLinea.numlin == 5)
                    {

                        if (Cab17TB.Text == "" && Cab16TB.Text != "")
                        {
                            EstablecerVariablesL5(7, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(7, Properties.Settings.Default.TORQ_TipoCierre7LlenL5, Properties.Settings.Default.TORQ_Prov7LlenL5,
                                                  Properties.Settings.Default.TORQ_C17LlenL5, Properties.Settings.Default.TORQ_C27LlenL5, Properties.Settings.Default.TORQ_C37LlenL5, Properties.Settings.Default.TORQ_C47LlenL5,
                                                  Properties.Settings.Default.TORQ_C57LlenL5, Properties.Settings.Default.TORQ_C67LlenL5, Properties.Settings.Default.TORQ_C77LlenL5, Properties.Settings.Default.TORQ_C87LlenL5,
                                                  Properties.Settings.Default.TORQ_C97LlenL5, Properties.Settings.Default.TORQ_C107LlenL5, Properties.Settings.Default.TORQ_C117LlenL5, Properties.Settings.Default.TORQ_C127LlenL5);
                        }
                        if (Cab16TB.Text == "" && Cab15TB.Text != "")
                        {
                            EstablecerVariablesL5(6, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(6, Properties.Settings.Default.TORQ_TipoCierre6LlenL5, Properties.Settings.Default.TORQ_Prov6LlenL5,
                                                  Properties.Settings.Default.TORQ_C16LlenL5, Properties.Settings.Default.TORQ_C26LlenL5, Properties.Settings.Default.TORQ_C36LlenL5, Properties.Settings.Default.TORQ_C46LlenL5,
                                                  Properties.Settings.Default.TORQ_C56LlenL5, Properties.Settings.Default.TORQ_C66LlenL5, Properties.Settings.Default.TORQ_C76LlenL5, Properties.Settings.Default.TORQ_C86LlenL5,
                                                  Properties.Settings.Default.TORQ_C96LlenL5, Properties.Settings.Default.TORQ_C106LlenL5, Properties.Settings.Default.TORQ_C116LlenL5, Properties.Settings.Default.TORQ_C126LlenL5);
                        }
                        if (Cab15TB.Text == "" && Cab14TB.Text != "")
                        {
                            EstablecerVariablesL5(5, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(5, Properties.Settings.Default.TORQ_TipoCierre5LlenL5, Properties.Settings.Default.TORQ_Prov5LlenL5,
                                            Properties.Settings.Default.TORQ_C15LlenL5, Properties.Settings.Default.TORQ_C25LlenL5, Properties.Settings.Default.TORQ_C35LlenL5, Properties.Settings.Default.TORQ_C45LlenL5,
                                            Properties.Settings.Default.TORQ_C55LlenL5, Properties.Settings.Default.TORQ_C65LlenL5, Properties.Settings.Default.TORQ_C75LlenL5, Properties.Settings.Default.TORQ_C85LlenL5,
                                            Properties.Settings.Default.TORQ_C95LlenL5, Properties.Settings.Default.TORQ_C105LlenL5, Properties.Settings.Default.TORQ_C115LlenL5, Properties.Settings.Default.TORQ_C125LlenL5);
                        }
                        if (Cab14TB.Text == "" && Cab13TB.Text != "")
                        {
                            EstablecerVariablesL5(4, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(4, Properties.Settings.Default.TORQ_TipoCierre4LlenL5, Properties.Settings.Default.TORQ_Prov4LlenL5,
                              Properties.Settings.Default.TORQ_C14LlenL5, Properties.Settings.Default.TORQ_C24LlenL5, Properties.Settings.Default.TORQ_C34LlenL5, Properties.Settings.Default.TORQ_C44LlenL5,
                              Properties.Settings.Default.TORQ_C54LlenL5, Properties.Settings.Default.TORQ_C64LlenL5, Properties.Settings.Default.TORQ_C74LlenL5, Properties.Settings.Default.TORQ_C84LlenL5,
                              Properties.Settings.Default.TORQ_C94LlenL5, Properties.Settings.Default.TORQ_C104LlenL5, Properties.Settings.Default.TORQ_C114LlenL5, Properties.Settings.Default.TORQ_C124LlenL5);
                        }
                        if (Cab13TB.Text == "" && Cab12TB.Text != "")
                        {
                            EstablecerVariablesL5(3, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(3, Properties.Settings.Default.TORQ_TipoCierre3LlenL5, Properties.Settings.Default.TORQ_Prov3LlenL5,
                                                  Properties.Settings.Default.TORQ_C13LlenL5, Properties.Settings.Default.TORQ_C23LlenL5, Properties.Settings.Default.TORQ_C33LlenL5, Properties.Settings.Default.TORQ_C43LlenL5,
                                                  Properties.Settings.Default.TORQ_C53LlenL5, Properties.Settings.Default.TORQ_C63LlenL5, Properties.Settings.Default.TORQ_C73LlenL5, Properties.Settings.Default.TORQ_C83LlenL5,
                                                  Properties.Settings.Default.TORQ_C93LlenL5, Properties.Settings.Default.TORQ_C103LlenL5, Properties.Settings.Default.TORQ_C113LlenL5, Properties.Settings.Default.TORQ_C123LlenL5);
                        }
                        if (Cab12TB.Text == "" && Cab11TB.Text != "")
                        {
                            EstablecerVariablesL5(2, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);

                            CompletarRegistros(2, Properties.Settings.Default.TORQ_TipoCierre2LlenL5, Properties.Settings.Default.TORQ_Prov2LlenL5,
                                                  Properties.Settings.Default.TORQ_C12LlenL5, Properties.Settings.Default.TORQ_C22LlenL5, Properties.Settings.Default.TORQ_C32LlenL5, Properties.Settings.Default.TORQ_C42LlenL5,
                                                  Properties.Settings.Default.TORQ_C52LlenL5, Properties.Settings.Default.TORQ_C62LlenL5, Properties.Settings.Default.TORQ_C72LlenL5, Properties.Settings.Default.TORQ_C82LlenL5,
                                                  Properties.Settings.Default.TORQ_C92LlenL5, Properties.Settings.Default.TORQ_C102LlenL5, Properties.Settings.Default.TORQ_C112LlenL5, Properties.Settings.Default.TORQ_C122LlenL5);
                        }
                        if (Cab11TB.Text == "")
                        {
                            EstablecerVariablesL5(1, TipoCierreMetalicoTB.Text, ProveedorTB.Text, C1TB.Text, C2TB.Text, C3TB.Text, C4TB.Text, C5TB.Text, C6TB.Text, C7TB.Text, C8TB.Text, C9TB.Text, C10TB.Text, C11TB.Text, C12TB.Text);
                            CompletarRegistros(1, Properties.Settings.Default.TORQ_TipoCierre1LlenL5, Properties.Settings.Default.TORQ_Prov1LlenL5,
                                                  Properties.Settings.Default.TORQ_C11LlenL5, Properties.Settings.Default.TORQ_C21LlenL5, Properties.Settings.Default.TORQ_C31LlenL5, Properties.Settings.Default.TORQ_C41LlenL5,
                                                  Properties.Settings.Default.TORQ_C51LlenL5, Properties.Settings.Default.TORQ_C61LlenL5, Properties.Settings.Default.TORQ_C71LlenL5, Properties.Settings.Default.TORQ_C81LlenL5,
                                                  Properties.Settings.Default.TORQ_C91LlenL5, Properties.Settings.Default.TORQ_C101LlenL5, Properties.Settings.Default.TORQ_C111LlenL5, Properties.Settings.Default.TORQ_C121LlenL5);
                        }

                    }

                    CompletarRegistros(0, null, null, "", "", "", "", "", "", "", "", "", "", "", "");
                    ComentariosTB.Text = "";
                }
            }
            else
            {
                MessageBox.Show("FALTAN CAMPOS POR RELLENAR! -> NO SE HA GUARDADO LA INFORMACIÓN");

            }
        }

        private void numberpad1_VisibleChanged(object sender, EventArgs e)
        {
            if (MaquinaLinea.StatusTeclado == true) Utilidades.EscribirTeclado(numberpad1, TextBox, null);
        }



        private void ComentariosTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (MaquinaLinea.TecladoWindows == 1) Utilidades.MostrarTecladoPredeterminado(null);
            if (MaquinaLinea.TecladoWindows == 2)
            {
                Utilidades.ParametrosTeclado(false, 0);
                TextBox = null;
                numberpad1.Location = new Point(450, 250);
                numberpad1.Visible = true;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.ProgramMenus;

namespace WHPS.Llenadora
{
    public partial class Llenadora_Control_Presion : Form
    {
        //Declaramos las variable que iran registrando los valores en los diferentes textbox
        public string horapresion = "";
        public string medidapresion = "";
        public string estadopresion = "";

        public Llenadora_Control_Presion()
        {
            InitializeComponent();
        }


        //Volvemos a la página anterior pulsando el boton.

        private void ExitB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2) Properties.Settings.Default.MedidaPresionL2 = PresionTB.Text;
            if (MaquinaLinea.numlin == 3) Properties.Settings.Default.MedidaPresionL3 = PresionTB.Text;
            if (MaquinaLinea.numlin == 5) Properties.Settings.Default.MedidaPresionL5 = PresionTB.Text;
            Properties.Settings.Default.Save();
            MainLlenadora Form = new MainLlenadora();
            Hide();
            Form.Show();
            GC.Collect();

        }

        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }



        //Un temporizador, nos sincroniza con la pantalla del main para que si al volver ha se ha activado la alarma nos avise
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
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
            //if (MaquinaLinea.StatusTeclado == true) Utilidades.EscribirTeclado(numberpad1, PresionTB, null);
        }

  
        private void Llenadora_Control_Presión_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));


            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //Se rellenan los datos del equipo
            dateTB.Text = DateTime.Now.ToString("dd/MM/yyyy");
            respTB.Text = MaquinaLinea.Responsable;
            maqTB.Text = MaquinaLinea.MLlenadora;
            turnoTB.Text = Utilidades.ObtenerTurnoActual();


            //Cargamos el registro del turno
            if (MaquinaLinea.numlin == 2)
            {
                HoraPresion1TB.Text = Properties.Settings.Default.HoraPresion1L2;
                HoraPresion2TB.Text = Properties.Settings.Default.HoraPresion2L2;
                HoraPresion3TB.Text = Properties.Settings.Default.HoraPresion3L2;
                HoraPresion4TB.Text = Properties.Settings.Default.HoraPresion4L2;
                HoraPresion5TB.Text = Properties.Settings.Default.HoraPresion5L2;
                MedidaPresion1TB.Text = Properties.Settings.Default.MedidaPresion1L2;
                MedidaPresion2TB.Text = Properties.Settings.Default.MedidaPresion2L2;
                MedidaPresion3TB.Text = Properties.Settings.Default.MedidaPresion3L2;
                MedidaPresion4TB.Text = Properties.Settings.Default.MedidaPresion4L2;
                MedidaPresion5TB.Text = Properties.Settings.Default.MedidaPresion5L2;
                Estado1TB.Text = Properties.Settings.Default.EstadoPresion1L2;
                Estado2TB.Text = Properties.Settings.Default.EstadoPresion2L2;
                Estado3TB.Text = Properties.Settings.Default.EstadoPresion3L2;
                Estado4TB.Text = Properties.Settings.Default.EstadoPresion4L2;
                Estado5TB.Text = Properties.Settings.Default.EstadoPresion5L2;
            }
            if (MaquinaLinea.numlin == 3)
            {
                HoraPresion1TB.Text = Properties.Settings.Default.HoraPresion1L3;
                HoraPresion2TB.Text = Properties.Settings.Default.HoraPresion2L3;
                HoraPresion3TB.Text = Properties.Settings.Default.HoraPresion3L3;
                HoraPresion4TB.Text = Properties.Settings.Default.HoraPresion4L3;
                HoraPresion5TB.Text = Properties.Settings.Default.HoraPresion5L3;
                MedidaPresion1TB.Text = Properties.Settings.Default.MedidaPresion1L3;
                MedidaPresion2TB.Text = Properties.Settings.Default.MedidaPresion2L3;
                MedidaPresion3TB.Text = Properties.Settings.Default.MedidaPresion3L3;
                MedidaPresion4TB.Text = Properties.Settings.Default.MedidaPresion4L3;
                MedidaPresion5TB.Text = Properties.Settings.Default.MedidaPresion5L3;
                Estado1TB.Text = Properties.Settings.Default.EstadoPresion1L3;
                Estado2TB.Text = Properties.Settings.Default.EstadoPresion2L3;
                Estado3TB.Text = Properties.Settings.Default.EstadoPresion3L3;
                Estado4TB.Text = Properties.Settings.Default.EstadoPresion4L3;
                Estado5TB.Text = Properties.Settings.Default.EstadoPresion5L3;
            }
            if (MaquinaLinea.numlin == 5)
            {
                HoraPresion1TB.Text = Properties.Settings.Default.HoraPresion1L5;
                HoraPresion2TB.Text = Properties.Settings.Default.HoraPresion2L5;
                HoraPresion3TB.Text = Properties.Settings.Default.HoraPresion3L5;
                HoraPresion4TB.Text = Properties.Settings.Default.HoraPresion4L5;
                HoraPresion5TB.Text = Properties.Settings.Default.HoraPresion5L5;
                MedidaPresion1TB.Text = Properties.Settings.Default.MedidaPresion1L5;
                MedidaPresion2TB.Text = Properties.Settings.Default.MedidaPresion2L5;
                MedidaPresion3TB.Text = Properties.Settings.Default.MedidaPresion3L5;
                MedidaPresion4TB.Text = Properties.Settings.Default.MedidaPresion4L5;
                MedidaPresion5TB.Text = Properties.Settings.Default.MedidaPresion5L5;
                Estado1TB.Text = Properties.Settings.Default.EstadoPresion1L5;
                Estado2TB.Text = Properties.Settings.Default.EstadoPresion2L5;
                Estado3TB.Text = Properties.Settings.Default.EstadoPresion3L5;
                Estado4TB.Text = Properties.Settings.Default.EstadoPresion4L5;
                Estado5TB.Text = Properties.Settings.Default.EstadoPresion5L5;
            }
        }

        private void HoraPresionTB_MouseClick(object sender, MouseEventArgs e)
        {
            HoraPresionTB.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            horapresion = HoraPresionTB.Text;
            if (MaquinaLinea.numlin == 2) Properties.Settings.Default.HoraPresionL2 = horapresion;
            if (MaquinaLinea.numlin == 3) Properties.Settings.Default.HoraPresionL3 = horapresion;
            if (MaquinaLinea.numlin == 5) Properties.Settings.Default.HoraPresionL5 = horapresion;
            Properties.Settings.Default.Save();
        }

        private void PresionTB_MouseClick(object sender, MouseEventArgs e)
        {
            WHPS.Utiles.VentanaTeclados.AbrirCalculadora(PresionTB);      
        }




        //Marcamos el estado de la maquina con los botones
        private void Estado_OK_B_Click(object sender, EventArgs e)
        {
            estadopresion = "OK";
            Estado_OK_B.BackColor = Color.DarkSeaGreen;
            Estado_NOOK_B.BackColor = Color.LightGray;
            if (MaquinaLinea.numlin == 2) Properties.Settings.Default.EstadoPresionL2 = estadopresion;
            if (MaquinaLinea.numlin == 3) Properties.Settings.Default.EstadoPresionL3 = estadopresion;
            if (MaquinaLinea.numlin == 5) Properties.Settings.Default.EstadoPresionL5 = estadopresion;
            Properties.Settings.Default.Save();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            estadopresion = "NO OK";
            Estado_OK_B.BackColor = Color.LightGray;
            Estado_NOOK_B.BackColor = Color.IndianRed;
            if (MaquinaLinea.numlin == 2) Properties.Settings.Default.EstadoPresionL2 = estadopresion;
            if (MaquinaLinea.numlin == 3) Properties.Settings.Default.EstadoPresionL3 = estadopresion;
            if (MaquinaLinea.numlin == 5) Properties.Settings.Default.EstadoPresionL5 = estadopresion;
            Properties.Settings.Default.Save();
        }

        //Borramos todos los datos aplicados
        private void BorrarB_Click(object sender, EventArgs e)
        {
            HoraPresionTB.Text = "";
            PresionTB.Text = "";
            estadopresion = "";
            
            Estado_OK_B.BackColor = Color.FromArgb(27, 33, 41);
            Estado_NOOK_B.BackColor = Color.FromArgb(27, 33, 41);
            if(MaquinaLinea.numlin == 2)
            {
                Properties.Settings.Default.HoraPresionL2 = HoraPresionTB.Text;
                Properties.Settings.Default.MedidaPresionL2 = PresionTB.Text;
                Properties.Settings.Default.EstadoPresionL2 = estadopresion;
            }
            if (MaquinaLinea.numlin == 3)
            {
                Properties.Settings.Default.HoraPresionL3 = HoraPresionTB.Text;
                Properties.Settings.Default.MedidaPresionL3 = PresionTB.Text;
                Properties.Settings.Default.EstadoPresionL3 = estadopresion;
            }
            if (MaquinaLinea.numlin == 5)
            {
                Properties.Settings.Default.HoraPresionL5 = HoraPresionTB.Text;
                Properties.Settings.Default.MedidaPresionL5 = PresionTB.Text;
                Properties.Settings.Default.EstadoPresionL5 = estadopresion;
            }
            Properties.Settings.Default.Save();
        }
        //Borramos el registro
        private void Borrar2B_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                Properties.Settings.Default.HoraPresion1L2 = "";
                Properties.Settings.Default.HoraPresion2L2 = "";
                Properties.Settings.Default.HoraPresion3L2 = "";
                Properties.Settings.Default.HoraPresion4L2 = "";
                Properties.Settings.Default.HoraPresion5L2 = "";
                Properties.Settings.Default.MedidaPresion1L2 = "";
                Properties.Settings.Default.MedidaPresion2L2 = "";
                Properties.Settings.Default.MedidaPresion3L2 = "";
                Properties.Settings.Default.MedidaPresion4L2 = "";
                Properties.Settings.Default.MedidaPresion5L2 = "";
                Properties.Settings.Default.EstadoPresion1L2 = "";
                Properties.Settings.Default.EstadoPresion2L2 = "";
                Properties.Settings.Default.EstadoPresion3L2 = "";
                Properties.Settings.Default.EstadoPresion4L2 = "";
                Properties.Settings.Default.EstadoPresion5L2 = "";
                Properties.Settings.Default.Save();

                //Cargamos el registro del turno y si se ha dejado alguna 
                HoraPresion1TB.Text = Properties.Settings.Default.HoraPresion1L2;
                HoraPresion2TB.Text = Properties.Settings.Default.HoraPresion2L2;
                HoraPresion3TB.Text = Properties.Settings.Default.HoraPresion3L2;
                HoraPresion4TB.Text = Properties.Settings.Default.HoraPresion4L2;
                HoraPresion5TB.Text = Properties.Settings.Default.HoraPresion5L2;
                MedidaPresion1TB.Text = Properties.Settings.Default.MedidaPresion1L2;
                MedidaPresion2TB.Text = Properties.Settings.Default.MedidaPresion2L2;
                MedidaPresion3TB.Text = Properties.Settings.Default.MedidaPresion3L2;
                MedidaPresion4TB.Text = Properties.Settings.Default.MedidaPresion4L2;
                MedidaPresion5TB.Text = Properties.Settings.Default.MedidaPresion5L2;
                Estado1TB.Text = Properties.Settings.Default.EstadoPresion1L2;
                Estado2TB.Text = Properties.Settings.Default.EstadoPresion2L2;
                Estado3TB.Text = Properties.Settings.Default.EstadoPresion3L2;
                Estado4TB.Text = Properties.Settings.Default.EstadoPresion4L2;
                Estado5TB.Text = Properties.Settings.Default.EstadoPresion5L2;
            }
            if (MaquinaLinea.numlin == 3)
            {
                Properties.Settings.Default.HoraPresion1L3 = "";
                Properties.Settings.Default.HoraPresion2L3 = "";
                Properties.Settings.Default.HoraPresion3L3 = "";
                Properties.Settings.Default.HoraPresion4L3 = "";
                Properties.Settings.Default.HoraPresion5L3 = "";
                Properties.Settings.Default.MedidaPresion1L3 = "";
                Properties.Settings.Default.MedidaPresion2L3 = "";
                Properties.Settings.Default.MedidaPresion3L3 = "";
                Properties.Settings.Default.MedidaPresion4L3 = "";
                Properties.Settings.Default.MedidaPresion5L3 = "";
                Properties.Settings.Default.EstadoPresion1L3 = "";
                Properties.Settings.Default.EstadoPresion2L3 = "";
                Properties.Settings.Default.EstadoPresion3L3 = "";
                Properties.Settings.Default.EstadoPresion4L3 = "";
                Properties.Settings.Default.EstadoPresion5L3 = "";
                Properties.Settings.Default.Save();

                //Cargamos el registro del turno y si se ha dejado alguna 
                HoraPresion1TB.Text = Properties.Settings.Default.HoraPresion1L3;
                HoraPresion2TB.Text = Properties.Settings.Default.HoraPresion2L3;
                HoraPresion3TB.Text = Properties.Settings.Default.HoraPresion3L3;
                HoraPresion4TB.Text = Properties.Settings.Default.HoraPresion4L3;
                HoraPresion5TB.Text = Properties.Settings.Default.HoraPresion5L3;
                MedidaPresion1TB.Text = Properties.Settings.Default.MedidaPresion1L3;
                MedidaPresion2TB.Text = Properties.Settings.Default.MedidaPresion2L3;
                MedidaPresion3TB.Text = Properties.Settings.Default.MedidaPresion3L3;
                MedidaPresion4TB.Text = Properties.Settings.Default.MedidaPresion4L3;
                MedidaPresion5TB.Text = Properties.Settings.Default.MedidaPresion5L3;
                Estado1TB.Text = Properties.Settings.Default.EstadoPresion1L3;
                Estado2TB.Text = Properties.Settings.Default.EstadoPresion2L3;
                Estado3TB.Text = Properties.Settings.Default.EstadoPresion3L3;
                Estado4TB.Text = Properties.Settings.Default.EstadoPresion4L3;
                Estado5TB.Text = Properties.Settings.Default.EstadoPresion5L3;
            }
            if (MaquinaLinea.numlin == 5)
            {
                Properties.Settings.Default.HoraPresion1L5 = "";
                Properties.Settings.Default.HoraPresion2L5 = "";
                Properties.Settings.Default.HoraPresion3L5 = "";
                Properties.Settings.Default.HoraPresion4L5 = "";
                Properties.Settings.Default.HoraPresion5L5 = "";
                Properties.Settings.Default.MedidaPresion1L5 = "";
                Properties.Settings.Default.MedidaPresion2L5 = "";
                Properties.Settings.Default.MedidaPresion3L5 = "";
                Properties.Settings.Default.MedidaPresion4L5 = "";
                Properties.Settings.Default.MedidaPresion5L5 = "";
                Properties.Settings.Default.EstadoPresion1L5 = "";
                Properties.Settings.Default.EstadoPresion2L5 = "";
                Properties.Settings.Default.EstadoPresion3L5 = "";
                Properties.Settings.Default.EstadoPresion4L5 = "";
                Properties.Settings.Default.EstadoPresion5L5 = "";
                Properties.Settings.Default.Save();

                //Cargamos el registro del turno y si se ha dejado alguna 
                HoraPresion1TB.Text = Properties.Settings.Default.HoraPresion1L5;
                HoraPresion2TB.Text = Properties.Settings.Default.HoraPresion2L5;
                HoraPresion3TB.Text = Properties.Settings.Default.HoraPresion3L5;
                HoraPresion4TB.Text = Properties.Settings.Default.HoraPresion4L5;
                HoraPresion5TB.Text = Properties.Settings.Default.HoraPresion5L5;
                MedidaPresion1TB.Text = Properties.Settings.Default.MedidaPresion1L5;
                MedidaPresion2TB.Text = Properties.Settings.Default.MedidaPresion2L5;
                MedidaPresion3TB.Text = Properties.Settings.Default.MedidaPresion3L5;
                MedidaPresion4TB.Text = Properties.Settings.Default.MedidaPresion4L5;
                MedidaPresion5TB.Text = Properties.Settings.Default.MedidaPresion5L5;
                Estado1TB.Text = Properties.Settings.Default.EstadoPresion1L5;
                Estado2TB.Text = Properties.Settings.Default.EstadoPresion2L5;
                Estado3TB.Text = Properties.Settings.Default.EstadoPresion3L5;
                Estado4TB.Text = Properties.Settings.Default.EstadoPresion4L5;
                Estado5TB.Text = Properties.Settings.Default.EstadoPresion5L5;
            }
        }

        //Guaramos los datos y los registamos en pantalla
        private void saveBot_Click(object sender, EventArgs e)
        {
            //Para guardar, todos los campos deben estar rellenos
            if (HoraPresionTB.Text != "" && PresionTB.Text != "" && estadopresion != "")
            {
                //Si los campos estan rellenados cargamos por ultimo la medida de la presion que es introducida a mano
                medidapresion = PresionTB.Text;

                //Greneramos una lista de valores donde se regitraran los datos
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
                valores5[0] = "HoraMedida";
                valores5[1] = horapresion;
                listavalores.Add(valores5);
                valores6[0] = "MedidaPresion";
                valores6[1] = medidapresion;
                listavalores.Add(valores6);
                valores7[0] = "Estado";
                valores7[1] = estadopresion;
                listavalores.Add(valores7);


                string salida = Utiles.ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileLlenadora, "Presion", listavalores, "Id");

                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                }
                else
                {
                    if (MaquinaLinea.numlin == 2)
                    {
                        if (HoraPresion4TB.Text != "" && HoraPresion5TB.Text == "")
                        {
                            //Cargamos las variables para que no sean eliminadas al cerrar la pantalla
                            Properties.Settings.Default.HoraPresion5L2 = horapresion;
                            Properties.Settings.Default.MedidaPresion5L2 = medidapresion;
                            Properties.Settings.Default.EstadoPresion5L2 = estadopresion;
                            Properties.Settings.Default.Save();

                            //Mostramos el registro en pantalla
                            HoraPresion5TB.Text = Properties.Settings.Default.HoraPresion5L2;
                            MedidaPresion5TB.Text = Properties.Settings.Default.MedidaPresion5L2;
                            Estado5TB.Text = Properties.Settings.Default.EstadoPresion5L2;
                        }

                        if (HoraPresion3TB.Text != "" && HoraPresion4TB.Text == "")
                        {
                            //Cargamos las variables para que no sean eliminadas al cerrar la pantalla
                            Properties.Settings.Default.HoraPresion4L2 = horapresion;
                            Properties.Settings.Default.MedidaPresion4L2 = medidapresion;
                            Properties.Settings.Default.EstadoPresion4L2 = estadopresion;
                            Properties.Settings.Default.Save();

                            //Mostramos el registro en pantalla
                            HoraPresion4TB.Text = Properties.Settings.Default.HoraPresion4L2;
                            MedidaPresion4TB.Text = Properties.Settings.Default.MedidaPresion4L2;
                            Estado4TB.Text = Properties.Settings.Default.EstadoPresion4L2;
                        }

                        if (HoraPresion2TB.Text != "" && HoraPresion3TB.Text == "")
                        {
                            //Cargamos las variables para que no sean eliminadas al cerrar la pantalla
                            Properties.Settings.Default.HoraPresion3L2 = horapresion;
                            Properties.Settings.Default.MedidaPresion3L2 = medidapresion;
                            Properties.Settings.Default.EstadoPresion3L2 = estadopresion;
                            Properties.Settings.Default.Save();

                            //Mostramos el registro en pantalla
                            HoraPresion3TB.Text = Properties.Settings.Default.HoraPresion3L2;
                            MedidaPresion3TB.Text = Properties.Settings.Default.MedidaPresion3L2;
                            Estado3TB.Text = Properties.Settings.Default.EstadoPresion3L2;
                        }
                        if (HoraPresion1TB.Text != "" && HoraPresion2TB.Text == "")
                        {
                            //Cargamos las variables para que no sean eliminadas al cerrar la pantalla
                            Properties.Settings.Default.HoraPresion2L2 = horapresion;
                            Properties.Settings.Default.MedidaPresion2L2 = medidapresion;
                            Properties.Settings.Default.EstadoPresion2L2 = estadopresion;
                            Properties.Settings.Default.Save();

                            //Mostramos el registro en pantalla
                            HoraPresion2TB.Text = Properties.Settings.Default.HoraPresion2L2;
                            MedidaPresion2TB.Text = Properties.Settings.Default.MedidaPresion2L2;
                            Estado2TB.Text = Properties.Settings.Default.EstadoPresion2L2;
                        }



                        if (HoraPresion1TB.Text == "")
                        {
                            //Cargamos las variables para que no sean eliminadas al cerrar la pantalla
                            Properties.Settings.Default.HoraPresion1L2 = horapresion;
                            Properties.Settings.Default.MedidaPresion1L2 = medidapresion;
                            Properties.Settings.Default.EstadoPresion1L2 = estadopresion;
                            Properties.Settings.Default.Save();

                            //Mostramos el registro en pantalla
                            HoraPresion1TB.Text = Properties.Settings.Default.HoraPresion1L2;
                            MedidaPresion1TB.Text = Properties.Settings.Default.MedidaPresion1L2;
                            Estado1TB.Text = Properties.Settings.Default.EstadoPresion1L2;
                        }
                    }
                    if (MaquinaLinea.numlin == 3)
                    {
                        if (HoraPresion4TB.Text != "" && HoraPresion5TB.Text == "")
                        {

                            //Cargamos las variables para que no sean eliminadas al cerrar la pantalla
                            Properties.Settings.Default.HoraPresion5L3 = horapresion;
                            Properties.Settings.Default.MedidaPresion5L3 = medidapresion;
                            Properties.Settings.Default.EstadoPresion5L3 = estadopresion;
                            Properties.Settings.Default.Save();

                            //Mostramos el registro en pantalla
                            HoraPresion5TB.Text = Properties.Settings.Default.HoraPresion5L3;
                            MedidaPresion5TB.Text = Properties.Settings.Default.MedidaPresion5L3;
                            Estado5TB.Text = Properties.Settings.Default.EstadoPresion5L3;
                        }
                        if (HoraPresion3TB.Text != "" && HoraPresion4TB.Text == "")
                        {

                            //Cargamos las variables para que no sean eliminadas al cerrar la pantalla
                            Properties.Settings.Default.HoraPresion4L3 = horapresion;
                            Properties.Settings.Default.MedidaPresion4L3 = medidapresion;
                            Properties.Settings.Default.EstadoPresion4L3 = estadopresion;
                            Properties.Settings.Default.Save();

                            //Mostramos el registro en pantalla
                            HoraPresion4TB.Text = Properties.Settings.Default.HoraPresion4L3;
                            MedidaPresion4TB.Text = Properties.Settings.Default.MedidaPresion4L3;
                            Estado4TB.Text = Properties.Settings.Default.EstadoPresion4L3;
                        }
                        if (HoraPresion2TB.Text != "" && HoraPresion3TB.Text == "")
                        {

                            //Cargamos las variables para que no sean eliminadas al cerrar la pantalla
                            Properties.Settings.Default.HoraPresion3L3 = horapresion;
                            Properties.Settings.Default.MedidaPresion3L3 = medidapresion;
                            Properties.Settings.Default.EstadoPresion3L3 = estadopresion;
                            Properties.Settings.Default.Save();

                            //Mostramos el registro en pantalla
                            HoraPresion3TB.Text = Properties.Settings.Default.HoraPresion3L3;
                            MedidaPresion3TB.Text = Properties.Settings.Default.MedidaPresion3L3;
                            Estado3TB.Text = Properties.Settings.Default.EstadoPresion3L3;
                        }
                        if (HoraPresion1TB.Text != "" && HoraPresion2TB.Text == "")
                        {
                            //Cargamos las variables para que no sean eliminadas al cerrar la pantalla
                            Properties.Settings.Default.HoraPresion2L3 = horapresion;
                            Properties.Settings.Default.MedidaPresion2L3 = medidapresion;
                            Properties.Settings.Default.EstadoPresion2L3 = estadopresion;
                            Properties.Settings.Default.Save();

                            //Mostramos el registro en pantalla
                            HoraPresion2TB.Text = Properties.Settings.Default.HoraPresion2L3;
                            MedidaPresion2TB.Text = Properties.Settings.Default.MedidaPresion2L3;
                            Estado2TB.Text = Properties.Settings.Default.EstadoPresion2L3;
                        }
                        //Como los datos han sido guardados correctamente, registramos las variables en pantalla para que puean ser visualizadas
                        if (HoraPresion1TB.Text == "")
                        {
                            //Cargamos las variables para que no sean eliminadas al cerrar la pantalla
                            Properties.Settings.Default.HoraPresion1L3 = horapresion;
                            Properties.Settings.Default.MedidaPresion1L3 = medidapresion;
                            Properties.Settings.Default.EstadoPresion1L3 = estadopresion;
                            Properties.Settings.Default.Save();

                            //Mostramos el registro en pantalla
                            HoraPresion1TB.Text = Properties.Settings.Default.HoraPresion1L3;
                            MedidaPresion1TB.Text = Properties.Settings.Default.MedidaPresion1L3;
                            Estado1TB.Text = Properties.Settings.Default.EstadoPresion1L3;
                        }


                    }
                    if (MaquinaLinea.numlin == 5)
                    {

                        if (HoraPresion4TB.Text != "" && HoraPresion5TB.Text == "")
                        {

                            //Cargamos las variables para que no sean eliminadas al cerrar la pantalla
                            Properties.Settings.Default.HoraPresion5L5 = horapresion;
                            Properties.Settings.Default.MedidaPresion5L5 = medidapresion;
                            Properties.Settings.Default.EstadoPresion5L5 = estadopresion;
                            Properties.Settings.Default.Save();

                            //Mostramos el registro en pantalla
                            HoraPresion5TB.Text = Properties.Settings.Default.HoraPresion5L5;
                            MedidaPresion5TB.Text = Properties.Settings.Default.MedidaPresion5L5;
                            Estado5TB.Text = Properties.Settings.Default.EstadoPresion5L5;
                        }


                        if (HoraPresion3TB.Text != "" && HoraPresion4TB.Text == "")
                        {
                            //Cargamos las variables para que no sean eliminadas al cerrar la pantalla
                            Properties.Settings.Default.HoraPresion4L5 = horapresion;
                            Properties.Settings.Default.MedidaPresion4L5 = medidapresion;
                            Properties.Settings.Default.EstadoPresion4L5 = estadopresion;
                            Properties.Settings.Default.Save();

                            //Mostramos el registro en pantalla
                            HoraPresion4TB.Text = Properties.Settings.Default.HoraPresion4L5;
                            MedidaPresion4TB.Text = Properties.Settings.Default.MedidaPresion4L5;
                            Estado4TB.Text = Properties.Settings.Default.EstadoPresion4L5;
                        }


                        if (HoraPresion2TB.Text != "" && HoraPresion3TB.Text == "")
                        {

                            //Cargamos las variables para que no sean eliminadas al cerrar la pantalla
                            Properties.Settings.Default.HoraPresion3L5 = horapresion;
                            Properties.Settings.Default.MedidaPresion3L5 = medidapresion;
                            Properties.Settings.Default.EstadoPresion3L5 = estadopresion;
                            Properties.Settings.Default.Save();

                            //Mostramos el registro en pantalla
                            HoraPresion3TB.Text = Properties.Settings.Default.HoraPresion3L5;
                            MedidaPresion3TB.Text = Properties.Settings.Default.MedidaPresion3L5;
                            Estado3TB.Text = Properties.Settings.Default.EstadoPresion3L5;
                        }


                        if (HoraPresion1TB.Text != "" && HoraPresion2TB.Text == "")
                        {

                            //Cargamos las variables para que no sean eliminadas al cerrar la pantalla
                            Properties.Settings.Default.HoraPresion2L5 = horapresion;
                            Properties.Settings.Default.MedidaPresion2L5 = medidapresion;
                            Properties.Settings.Default.EstadoPresion2L5 = estadopresion;
                            Properties.Settings.Default.Save();

                            //Mostramos el registro en pantalla
                            HoraPresion2TB.Text = Properties.Settings.Default.HoraPresion2L5;
                            MedidaPresion2TB.Text = Properties.Settings.Default.MedidaPresion2L5;
                            Estado2TB.Text = Properties.Settings.Default.EstadoPresion2L5;
                        }


                        //Como los datos han sido guardados correctamente, registramos las variables en pantalla para que puean ser visualizadas
                        if (HoraPresion1TB.Text == "" && MedidaPresion1TB.Text == "" && Estado1TB.Text == "")
                        {
                            //Cargamos las variables para que no sean eliminadas al cerrar la pantalla
                            Properties.Settings.Default.HoraPresion1L5 = horapresion;
                            Properties.Settings.Default.MedidaPresion1L5 = medidapresion;
                            Properties.Settings.Default.EstadoPresion1L5 = estadopresion;
                            Properties.Settings.Default.Save();

                            //Mostramos el registro en pantalla
                            HoraPresion1TB.Text = Properties.Settings.Default.HoraPresion1L5;
                            MedidaPresion1TB.Text = Properties.Settings.Default.MedidaPresion1L5;
                            Estado1TB.Text = Properties.Settings.Default.EstadoPresion1L5;
                        }
                    }

                    PresionTB.Text = "";
                    HoraPresionTB.Text = "";
                    Estado_OK_B.BackColor = Color.FromArgb(27, 33, 41);
                    Estado_NOOK_B.BackColor = Color.FromArgb(27, 33, 41);
                    estadopresion = "";
                }
            }
            else
            {
                MessageBox.Show("FALTAN CAMPOS POR RELLENAR! -> NO SE HA GUARDADO LA INFORMACIÓN");
            }
        }

       

        private void PresionTB_TextChanged(object sender, EventArgs e)
        {
            if (PresionTB.Text != "")
            {
                if (Convert.ToDouble(PresionTB.Text) >= Convert.ToDouble(Properties.Settings.Default.PresionAjustesLlen))
                {
                    estadopresion = "OK";
                    Estado_OK_B.BackColor = Color.DarkSeaGreen;
                    Estado_NOOK_B.BackColor = Color.LightGray;
                    if (MaquinaLinea.numlin == 2) Properties.Settings.Default.EstadoPresionL2 = estadopresion;
                    if (MaquinaLinea.numlin == 3) Properties.Settings.Default.EstadoPresionL3 = estadopresion;
                    if (MaquinaLinea.numlin == 5) Properties.Settings.Default.EstadoPresionL5 = estadopresion;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    estadopresion = "NO OK";
                    Estado_OK_B.BackColor = Color.LightGray;
                    Estado_NOOK_B.BackColor = Color.IndianRed;
                    if (MaquinaLinea.numlin == 2) Properties.Settings.Default.EstadoPresionL2 = estadopresion;
                    if (MaquinaLinea.numlin == 3) Properties.Settings.Default.EstadoPresionL3 = estadopresion;
                    if (MaquinaLinea.numlin == 5) Properties.Settings.Default.EstadoPresionL5 = estadopresion;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void PresionTB_KeyDown(object sender, KeyEventArgs e)
        {

            //if (Convert.to(PresionTB.Text) <= 1.8)
            //{
            //    estadopresion = "OK";
            //    Estado_OK_B.BackColor = Color.DarkSeaGreen;
            //    Estado_NOOK_B.BackColor = Color.LightGray;
            //    if (MaquinaLinea.numlin == 2) Properties.Settings.Default.EstadoPresionL2 = estadopresion;
            //    if (MaquinaLinea.numlin == 3) Properties.Settings.Default.EstadoPresionL3 = estadopresion;
            //    if (MaquinaLinea.numlin == 5) Properties.Settings.Default.EstadoPresionL5 = estadopresion;
            //    Properties.Settings.Default.Save();
            //}
            //else
            //{
            //    estadopresion = "NO OK";
            //    Estado_OK_B.BackColor = Color.LightGray;
            //    Estado_NOOK_B.BackColor = Color.IndianRed;
            //    if (MaquinaLinea.numlin == 2) Properties.Settings.Default.EstadoPresionL2 = estadopresion;
            //    if (MaquinaLinea.numlin == 3) Properties.Settings.Default.EstadoPresionL3 = estadopresion;
            //    if (MaquinaLinea.numlin == 5) Properties.Settings.Default.EstadoPresionL5 = estadopresion;
            //    Properties.Settings.Default.Save();
            //}
        //}
    }
    }
}

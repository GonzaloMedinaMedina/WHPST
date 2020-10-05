using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.ProgramMenus;

namespace WHPS.Llenadora
{
    public partial class Llenadora_CambioTurno : Form
    {
        public string limpio = "";
        public string protecciones = "";
        public string cuter = "";
        public string herramientas = "";
        public string caños = "";
        public string Numcaños = "";
        public string sensor = "";
        public string presionalarma = "";
        public string revEnjLlen = "";
        public string revTap = "";
        public string revCap = "";
        public string revTrans = "";
        public string CambioTurno = "";

        public Llenadora_CambioTurno()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>         
        private void ExitB_Click(object sender, EventArgs e)
        {
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
        private void Llenadora_CambioTurno_Load(object sender, EventArgs e)
        {
            //Muestra la hora ya que el timer tarda 1s en tomar el control del Label lbReloj.
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;
            PresionalarmaTB.Text = Properties.Settings.Default.PresionAjustesLlen; 

            if (MaquinaLinea.numlin == 2) NumcañosTB.Text = Properties.Settings.Default.NCañosL2;
            if (MaquinaLinea.numlin == 3) NumcañosTB.Text = Properties.Settings.Default.NCañosL3;
            if (MaquinaLinea.numlin == 5) NumcañosTB.Text = Properties.Settings.Default.NCañosL5;
        }

        /// <summary>
        /// Función del temporizador que se ejecuta cada segundo, indíca la hora y activa la alarma. 
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");

            //Activamos la alarma cuando el reloj cuando coincida con la hora de la alarma
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
        }

        //#######################  BOTONES DECORACIÓN  ##############################
        private void SiguienteB_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }
        private void VolverB_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }
        private void btnSiPuestoLimpio_Click(object sender, EventArgs e)
        {
            limpio = "SI";
            btnSiPuestoLimpio.BackColor = Color.DarkSeaGreen;
            btnNoPuestoLimpio.BackColor = Color.LightGray;
        }
        private void btnNoPuestoLimpio_Click(object sender, EventArgs e)
        {
            limpio = "NO";
            btnSiPuestoLimpio.BackColor = Color.LightGray;
            btnNoPuestoLimpio.BackColor = Color.IndianRed;
        }
        private void btnSiProtecciones_Click(object sender, EventArgs e)
        {
            protecciones = "SI";
            btnSiProtecciones.BackColor = Color.DarkSeaGreen;
            btnNoProtecciones.BackColor = Color.LightGray;
        }
        private void btnNoProtecciones_Click(object sender, EventArgs e)
        {
            protecciones = "NO";
            btnSiProtecciones.BackColor = Color.LightGray;
            btnNoProtecciones.BackColor = Color.IndianRed;
        }
        private void btnSiCuter_Click(object sender, EventArgs e)
        {
            cuter = "SI";
            btnSiCuter.BackColor = Color.DarkSeaGreen;
            btnNoCuter.BackColor = Color.LightGray;
        }
        private void btnNoCuter_Click(object sender, EventArgs e)
        {
            cuter = "NO";
            btnSiCuter.BackColor = Color.LightGray;
            btnNoCuter.BackColor = Color.IndianRed;
        }
        private void btnSiHerramientas_Click(object sender, EventArgs e)
        {
            herramientas = "SI";
            btnSiHerramientas.BackColor = Color.DarkSeaGreen;
            btnNoHerramientas.BackColor = Color.LightGray;
        }
        private void btnNoHerramientas_Click(object sender, EventArgs e)
        {
            herramientas = "NO";
            btnSiHerramientas.BackColor = Color.LightGray;
            btnNoHerramientas.BackColor = Color.IndianRed;
        }
        private void btnOKCanos_Click(object sender, EventArgs e)
        {
            caños = "OK";
            btnOKCanos.BackColor = Color.DarkSeaGreen;
            btnNoOKCanos.BackColor = Color.LightGray;
        }
        private void btnNoOKCanos_Click(object sender, EventArgs e)
        {
            caños = "NO OK";
            btnOKCanos.BackColor = Color.LightGray;
            btnNoOKCanos.BackColor = Color.IndianRed;
        }
        private void btnOKSensor_Click(object sender, EventArgs e)
        {
            sensor = "OK";
            btnOKSensor.BackColor = Color.DarkSeaGreen;
            btnNoOKSensor.BackColor = Color.LightGray;
        }
        private void btnNoOKSensor_Click(object sender, EventArgs e)
        {
            sensor = "NO OK";
            btnOKSensor.BackColor = Color.LightGray;
            btnNoOKSensor.BackColor = Color.IndianRed;
        }

        private void btnOKEnjuagadoraLlenadora_Click(object sender, EventArgs e)
        {
            revEnjLlen = "OK";
            btnOKEnjuagadoraLlenadora.BackColor = Color.DarkSeaGreen;
            btnNOOKEnjuagadoraLlenadora.BackColor = Color.LightGray;
        }
        private void btnNOOKEnjuagadoraLlenadora_Click(object sender, EventArgs e)
        {
            revEnjLlen = "NO OK";
            btnOKEnjuagadoraLlenadora.BackColor = Color.LightGray;
            btnNOOKEnjuagadoraLlenadora.BackColor = Color.IndianRed;
        }
        private void btnOKTaponadora_Click(object sender, EventArgs e)
        {
            revTap = "OK";
            btnOKTaponadora.BackColor = Color.DarkSeaGreen;
            btnNOOKTaponadora.BackColor = Color.LightGray;
        }
        private void btnNOOKTaponadora_Click(object sender, EventArgs e)
        {
            revTap = "NO OK";
            btnOKTaponadora.BackColor = Color.LightGray;
            btnNOOKTaponadora.BackColor = Color.IndianRed;
        }
        private void btnOKCapsuladora_Click(object sender, EventArgs e)
        {
            revCap = "OK";
            btnOKCapsuladora.BackColor = Color.DarkSeaGreen;
            btnNOOKCapsuladora.BackColor = Color.LightGray;
        }
        private void btnNOOKCapsuladora_Click(object sender, EventArgs e)
        {
            revCap = "NO OK";
            btnOKCapsuladora.BackColor = Color.LightGray;
            btnNOOKCapsuladora.BackColor = Color.IndianRed;
        }
        private void btnOKTransportadora_Click(object sender, EventArgs e)
        {
            revTrans = "OK";
            btnOKTransportadora.BackColor = Color.DarkSeaGreen;
            btnNOOKTransportadora.BackColor = Color.LightGray;
        }
        private void btnNOOKTransportadora_Click(object sender, EventArgs e)
        {
            revTrans = "NO OK";
            btnOKTransportadora.BackColor = Color.LightGray;
            btnNOOKTransportadora.BackColor = Color.IndianRed;
        }
        


        //###############  GUARDADO DE LOS DATOS DE INICIO DEL TURNO  ###############
        private void saveBot_Click(object sender, EventArgs e)
        {
            //Guardamos si todos los campos han sido rellenados
            if (limpio != "" && protecciones != "" && cuter != "" && herramientas != "" && caños != "" && NumcañosTB.Text != "" && sensor != "" && PresionalarmaTB.Text != "" && revEnjLlen != "" && revTap != "" && revCap != "" && revTrans != "")
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
                valores4[1] = MaquinaLinea.turno;
                listavalores.Add(valores4);
                valores5[0] = "Limpio";
                valores5[1] = limpio;
                listavalores.Add(valores5);
                valores6[0] = "Protecciones";
                valores6[1] = protecciones;
                listavalores.Add(valores6);
                valores7[0] = "Cuter";
                valores7[1] = cuter;
                listavalores.Add(valores7);
                valores8[0] = "Herramientas";
                valores8[1] = herramientas;
                listavalores.Add(valores8);
                valores9[0] = "Caños";
                valores9[1] = caños;
                listavalores.Add(valores9);
                valores10[0] = "NumeroCaños";
                valores10[1] = NumcañosTB.Text;
                listavalores.Add(valores10);
                valores11[0] = "Sensor";
                valores11[1] = sensor;
                listavalores.Add(valores11);
                valores12[0] = "PresionAlarma";
                valores12[1] = PresionalarmaTB.Text;
                listavalores.Add(valores12);
                valores13[0] = "EnjuagadoraLlenadora";
                valores13[1] = revEnjLlen;
                listavalores.Add(valores13);
                valores14[0] = "Taponadora";
                valores14[1] = revTap;
                listavalores.Add(valores14);
                valores15[0] = "Capsuladora";
                valores15[1] = revCap;
                listavalores.Add(valores15);
                valores16[0] = "Transportadora";
                valores16[1] = revTrans;
                listavalores.Add(valores16);
                valores17[0] = "CambioTurno";
                valores17[1] = MaquinaLinea.ChequearCambioTurno("Llenadora");
                listavalores.Add(valores17);

                string salida = Utiles.ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileLlenadora, "Inicio", listavalores, "Id");
                //Si existe algun error de salvado de datos se expondrá en un MESSAGE 
                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show(Properties.Settings.Default.AvisoProblemaFichero);
                    //MessageBox.Show(salida);
                }
                //Cuando los datos han sido salvados correctamente reestablecemos los parámetros
                else
                {
                    //En función que que estado estemos, cambiaremos a otro.
                    if (MaquinaLinea.numlin == 2)
                    {
                        switch (MaquinaLinea.chLlenL2)
                        {
                            case false:
                                Properties.Settings.Default.chLlenL2 = true;
                                break;

                            case true:
                                Properties.Settings.Default.chLlenL2 = false;
                                if (MaquinaLinea.chalarmaLlenL2) Properties.Settings.Default.chalarmaLlenL2 = false;
                                break;
                        }
                    }
                    if (MaquinaLinea.numlin == 3)
                    {
                        switch (MaquinaLinea.chLlenL3)
                        {
                            case false:
                                Properties.Settings.Default.chLlenL3 = true;
                                break;

                            case true:
                                Properties.Settings.Default.chLlenL3 = false;
                                if (MaquinaLinea.chalarmaLlenL3) Properties.Settings.Default.chalarmaLlenL3 = false;
                                break;
                        }
                    }
                    if (MaquinaLinea.numlin == 5)
                    {
                        switch (MaquinaLinea.chLlenL5)
                        {
                            case false:
                                Properties.Settings.Default.chLlenL5 = true;
                                break;

                            case true:
                                Properties.Settings.Default.chLlenL5 = false;
                                if (MaquinaLinea.chalarmaLlenL5) Properties.Settings.Default.chalarmaLlenL5 = false;
                                break;
                        }
                    }

                    //Cargamos las variables ya que han sido modificadas
                    MaquinaLinea.chLlenL2 = Properties.Settings.Default.chLlenL2;
                    MaquinaLinea.chLlenL3 = Properties.Settings.Default.chLlenL3;
                    MaquinaLinea.chLlenL5 = Properties.Settings.Default.chLlenL5;
                    Properties.Settings.Default.Save();

                    //Si algún elemento no está en el estado que debe, se mostrará el form de comentarios si el operio lo puede justificar
                    if (limpio == "NO" || protecciones == "NO" || cuter == "NO" || herramientas == "NO")
                    {
                        DialogResult opcion;
                        opcion = MessageBox.Show("Algunos de los campos selecciondos no se encuentra en el estado que debería. ¿Puede indicar a que se debe?", "", MessageBoxButtons.YesNo);
                        if (opcion == DialogResult.Yes)
                        {
                            Llenadora_Comentarios Form = new Llenadora_Comentarios();
                            Hide();
                            Form.Show();
                        }
                        else
                        {
                            MainLlenadora Form = new MainLlenadora();
                            Hide();
                            Form.Show();
                        }
                    }
                    else
                    {
                        MainLlenadora Form = new MainLlenadora();
                        Hide();
                        Form.Show();
                        GC.Collect();
                    }

                }
            }

            //Si los campos no han sido rellenados un MESSAGE avisará al operario
            else
            {
                MessageBox.Show(Properties.Settings.Default.AvisoCampos);
            }
        }
        //###########################################################################
    }
}

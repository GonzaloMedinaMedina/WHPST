using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.Utiles;

namespace WHPS.Llenadora
{
    public partial class Llenadora_Registro : Form
    {
        public bool HInicio = false;
        public bool HFin = false;

        public Llenadora_Registro()
        {
            InitializeComponent();
        }

        //Volvemos al menu de la llenadora cerrando la ventana
        private void Llenadora_Registro_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                //Realizamos una "foto" de los datos que han sido introducidos
                Properties.Settings.Default.ReferenciaLlenL2 = ReferenciaTB.Text;
                Properties.Settings.Default.NBotellasLlenL2 = NBotTB.Text;
                Properties.Settings.Default.HInicioLlenL2 = HInicioTB.Text;
                Properties.Settings.Default.HFinLlenL2 = HFinTB.Text;
                Properties.Settings.Default.Save();
            }
            if (MaquinaLinea.numlin == 3)
            {
                //Realizamos una "foto" de los datos que han sido introducidos
                Properties.Settings.Default.ReferenciaLlenL3 = ReferenciaTB.Text;
                Properties.Settings.Default.NBotellasLlenL3 = NBotTB.Text;
                Properties.Settings.Default.HInicioLlenL3 = HInicioTB.Text;
                Properties.Settings.Default.HFinLlenL3 = HFinTB.Text;
                Properties.Settings.Default.Save();
            }
            if (MaquinaLinea.numlin == 5)
            {
                //Realizamos una "foto" de los datos que han sido introducidos
                Properties.Settings.Default.ReferenciaLlenL5 = ReferenciaTB.Text;
                Properties.Settings.Default.NBotellasLlenL5 = NBotTB.Text;
                Properties.Settings.Default.HInicioLlenL5 = HInicioTB.Text;
                Properties.Settings.Default.HFinLlenL5 = HFinTB.Text;
                Properties.Settings.Default.Save();
            }

            MainLlenadora Form = new MainLlenadora();
            Hide();
            Form.Show();
        }

        //Volvemos al menu de la llenadora pulsando el boton back
        private void BackB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin == 2)
            {
                //Realizamos una "foto" de los datos que han sido introducidos
                Properties.Settings.Default.ReferenciaLlenL2 = ReferenciaTB.Text;
                Properties.Settings.Default.NBotellasLlenL2 = NBotTB.Text;
                Properties.Settings.Default.HInicioLlenL2 = HInicioTB.Text;
                Properties.Settings.Default.HFinLlenL2 = HFinTB.Text;
                Properties.Settings.Default.Save();
            }
            if (MaquinaLinea.numlin == 3)
            {
                //Realizamos una "foto" de los datos que han sido introducidos
                Properties.Settings.Default.ReferenciaLlenL3 = ReferenciaTB.Text;
                Properties.Settings.Default.NBotellasLlenL3 = NBotTB.Text;
                Properties.Settings.Default.HInicioLlenL3 = HInicioTB.Text;
                Properties.Settings.Default.HFinLlenL3 = HFinTB.Text;
                Properties.Settings.Default.Save();
            }
            if (MaquinaLinea.numlin == 5)
            {
                //Realizamos una "foto" de los datos que han sido introducidos
                Properties.Settings.Default.ReferenciaLlenL5 = ReferenciaTB.Text;
                Properties.Settings.Default.NBotellasLlenL5 = NBotTB.Text;
                Properties.Settings.Default.HInicioLlenL5 = HInicioTB.Text;
                Properties.Settings.Default.HFinLlenL5 = HFinTB.Text;
                Properties.Settings.Default.Save();
            }

            MainLlenadora Form = new MainLlenadora();
            Hide();
            Form.Show();
        }

        //Cargamos la información que ya tenemos en pantalla
        private void Llenadora_Registro_Load(object sender, EventArgs e)
        {
            //Guardamos el teclado
            groupBoxTECLADO.Hide();

            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));

            //Rellenamos fecha
            dateTB.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //Rellenamos responsable y maquinista
            respTB.Text = MaquinaLinea.Responsable;
            maqTB.Text = MaquinaLinea.MLlenadora;

            //Rellenamos el turno - Identificando el turno
            turnoTB.Text = WHPS.ProgramMenus.Utilidades.ShiftCheck();


            if (MaquinaLinea.numlin == 2)
            {
                //Rellenamos los registros que ya han sido guardados
                ReferenciaTB.Text = Properties.Settings.Default.ReferenciaLlenL2;
                NBotTB.Text = Properties.Settings.Default.NBotellasLlenL2;
                HInicioTB.Text = Properties.Settings.Default.HInicioLlenL2;
                HFinTB.Text = Properties.Settings.Default.HFinLlenL2;

                //Indicamos si estamos en tiempo de preparación
                if (Properties.Settings.Default.HInicioCambioLlenL2 != "")
                {
                    LanzamientocargadoB.BackColor = Color.LightBlue;
                }
            }
            if (MaquinaLinea.numlin == 3)
            {
                //Rellenamos los registros que ya han sido guardados
                ReferenciaTB.Text = Properties.Settings.Default.ReferenciaLlenL3;
                NBotTB.Text = Properties.Settings.Default.NBotellasLlenL3;
                HInicioTB.Text = Properties.Settings.Default.HInicioLlenL3;
                HFinTB.Text = Properties.Settings.Default.HFinLlenL3;

                //Indicamos si estamos en tiempo de preparación
                if (Properties.Settings.Default.HInicioCambioLlenL3 != "")
                {
                    LanzamientocargadoB.BackColor = Color.LightBlue;
                }
            }
            if (MaquinaLinea.numlin == 5)
            {
                //Rellenamos los registros que ya han sido guardados
                ReferenciaTB.Text = Properties.Settings.Default.ReferenciaLlenL5;
                NBotTB.Text = Properties.Settings.Default.NBotellasLlenL5;
                HInicioTB.Text = Properties.Settings.Default.HInicioLlenL5;
                HFinTB.Text = Properties.Settings.Default.HFinLlenL5;

                //Indicamos si estamos en tiempo de preparación
                if (Properties.Settings.Default.HInicioCambioLlenL5 != "")
                {
                    LanzamientocargadoB.BackColor = Color.LightBlue;
                }
            }
        }

        //Temporizador, nos sincroniza con la pantalla del main para que si al volver se ha activado la alarma nos avise
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            if (Properties.Settings.Default.chalarma == false && ((MaquinaLinea.numlin == 2 && MaquinaLinea.chLlenL2 == true) || (MaquinaLinea.numlin == 3 && MaquinaLinea.chLlenL3 == true) || (MaquinaLinea.numlin == 5 && MaquinaLinea.chLlenL5 == true)) && ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00"))))
            {
                Properties.Settings.Default.chalarma = true;
                Properties.Settings.Default.Save();
            }
        }

        //Al clickear sobre TB, cargará la hora
        //Si la hora ya esta escrita, aparecera un mensaje de seguridad para sobreescribir
        private void HinicioTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (HInicioTB.Text != "")
            {
                DialogResult opcion;
                opcion = MessageBox.Show("¿Estas seguro de sobrescribir los datos? Perderá la informacion.", "", MessageBoxButtons.OKCancel);
                if (opcion == DialogResult.OK)
                {
                    HInicioTB.Select();
                    HInicio = true;
                    groupBoxTECLADO.Show();
                }
            }
                if (HInicioTB.Text == "")
            {
            HInicioTB.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            }
        }
        private void HFinTB_MouseClick(object sender, MouseEventArgs e)
        {
            if (HFinTB.Text != "")
            {
                DialogResult opcion;
                opcion = MessageBox.Show("¿Estas seguro de sobrescribir los datos? Perderá la informacion.", "", MessageBoxButtons.OKCancel);
                if (opcion == DialogResult.OK)
                {
                    HFinTB.Select();
                    HFin = true;
                    groupBoxTECLADO.Show();
                }
            }
            if (HFinTB.Text == "")
            {
                HFinTB.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            }
        }

        //Registrará el tiempo en el TB de cuya variable sea true
        private void LanzamientoCargadoB_Click(object sender, EventArgs e)
        {
            if (MaquinaLinea.numlin ==2)
            {
            Properties.Settings.Default.HInicioCambioLlenL2 = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
            LanzamientocargadoB.BackColor = Color.LightBlue;
            }
            if (MaquinaLinea.numlin == 3)
            {
                Properties.Settings.Default.HInicioCambioLlenL3 = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
                LanzamientocargadoB.BackColor = Color.LightBlue;
            }
            if (MaquinaLinea.numlin == 5)
            {
                Properties.Settings.Default.HInicioCambioLlenL5 = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));
                LanzamientocargadoB.BackColor = Color.LightBlue;
            }

        }

        //Botón que elimina toda la información del registro
        private void BorrarB_Click(object sender, EventArgs e)
        {
            DepositoTB.Text = "";
            ReferenciaTB.Text = "";
            CapacidadTB.Text = "";
            MarcaTB.Text = "";
            NBotTB.Text = "";
            AlcoholTB.Text = "";
            HInicioTB.Text = "";
            HFinTB.Text = "";
            if (MaquinaLinea.numlin == 2)
            {
                Properties.Settings.Default.HInicioLlenL2 = "";
                Properties.Settings.Default.HFinLlenL2 = "";
                Properties.Settings.Default.HInicioCambioLlenL2 = "";
                Properties.Settings.Default.Save();
            }
            if (MaquinaLinea.numlin == 3)
            {
                Properties.Settings.Default.HInicioLlenL3 = "";
                Properties.Settings.Default.HFinLlenL3 = "";
                Properties.Settings.Default.HInicioCambioLlenL3 = "";
                Properties.Settings.Default.Save();
            }
            if (MaquinaLinea.numlin == 5)
            {
                Properties.Settings.Default.HInicioLlenL5 = "";
                Properties.Settings.Default.HFinLlenL5 = "";
                Properties.Settings.Default.HInicioCambioLlenL5 = "";
                Properties.Settings.Default.Save();
            }
        }

        //Box teclado
        private void button1_Click(object sender, EventArgs e)
        {
            if (HInicio == true)
            {
                HInicioTB.Text = HInicioTB.Text + "1";
            }
            if (HFin == true)
            {
                HFinTB.Text = HFinTB.Text + "1";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (HInicio == true)
            {
                HInicioTB.Text = HInicioTB.Text + "2";
            }
            if (HFin == true)
            {
                HFinTB.Text = HFinTB.Text + "2";
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (HInicio == true)
            {
                HInicioTB.Text = HInicioTB.Text + "3";
            }
            if (HFin == true)
            {
                HFinTB.Text = HFinTB.Text + "3";
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (HInicio == true)
            {
                HInicioTB.Text = HInicioTB.Text + "4";
            }
            if (HFin == true)
            {
                HFinTB.Text = HFinTB.Text + "4";
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (HInicio == true)
            {
                HInicioTB.Text = HInicioTB.Text + "5";
            }
            if (HFin == true)
            {
                HFinTB.Text = HFinTB.Text + "5";
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (HInicio == true)
            {
                HInicioTB.Text = HInicioTB.Text + "6";
            }
            if (HFin == true)
            {
                HFinTB.Text = HFinTB.Text + "6";
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (HInicio == true)
            {
                HInicioTB.Text = HInicioTB.Text + "7";
            }
            if (HFin == true)
            {
                HFinTB.Text = HFinTB.Text + "7";
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (HInicio == true)
            {
                HInicioTB.Text = HInicioTB.Text + "8";
            }
            if (HFin == true)
            {
                HFinTB.Text = HFinTB.Text + "8";
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (HInicio == true)
            {
                HInicioTB.Text = HInicioTB.Text + "9";
            }
            if (HFin == true)
            {
                HFinTB.Text = HFinTB.Text + "9";
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (HInicio == true)
            {
                HInicioTB.Text = HInicioTB.Text + "0";
            }
            if (HFin == true)
            {
                HFinTB.Text = HFinTB.Text + "0";
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            if (HInicio == true)
            {
                HInicioTB.Text = HInicioTB.Text + ":";
            }
            if (HFin == true)
            {
                HFinTB.Text = HFinTB.Text + ":";
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            if (HInicio == true)
            {
                HInicioTB.Text = "";
            }
            if (HFin == true)
            {
                HFinTB.Text = "";
            }

        }
        private void button13_Click(object sender, EventArgs e)
        {
            HInicio = false;
            HFin = false;
            groupBoxTECLADO.Hide();
        }

        //Guardamos que la producción se ha terminado
        private void saveBot_Click(object sender, EventArgs e)
        {
            //Cargamos las variables del cambio en variables globales
            if (MaquinaLinea.numlin == 2)
            {
                MaquinaLinea.HInicioLlen = Properties.Settings.Default.HInicioLlenL2;
                MaquinaLinea.HFinLlen = Properties.Settings.Default.HFinLlenL2;
                MaquinaLinea.HInicioCambioLlen = Properties.Settings.Default.HInicioCambioLlenL2;
                MaquinaLinea.NBotellasLlen = Properties.Settings.Default.NBotellasLlenL2;
                MaquinaLinea.ReferenciaLlen = Properties.Settings.Default.ReferenciaLlenL2;
            }
            if (MaquinaLinea.numlin == 3)
            {
                MaquinaLinea.HInicioLlen = Properties.Settings.Default.HInicioLlenL3;
                MaquinaLinea.HFinLlen = Properties.Settings.Default.HFinLlenL3;
                MaquinaLinea.HInicioCambioLlen = Properties.Settings.Default.HInicioCambioLlenL3;
                MaquinaLinea.NBotellasLlen = Properties.Settings.Default.NBotellasLlenL3;
                MaquinaLinea.ReferenciaLlen = Properties.Settings.Default.ReferenciaLlenL3;
            }
            if (MaquinaLinea.numlin == 5)
            {
                MaquinaLinea.HInicioLlen = Properties.Settings.Default.HInicioLlenL5;
                MaquinaLinea.HFinLlen = Properties.Settings.Default.HFinLlenL5;
                MaquinaLinea.HInicioCambioLlen = Properties.Settings.Default.HInicioCambioLlenL5;
                MaquinaLinea.NBotellasLlen = Properties.Settings.Default.NBotellasLlenL5;
                MaquinaLinea.ReferenciaLlen = Properties.Settings.Default.ReferenciaLlenL5;
            }

            //Para poder guardar todos los campos deben estar cumplimentados
            if (DepositoTB.Text != "" && ReferenciaTB.Text != "" && CapacidadTB.Text != "" && MarcaTB.Text != "" && NBotTB.Text != "" && AlcoholTB.Text != "" && HInicioTB.Text != "" && HFinTB.Text != "")
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
                valores5[0] = "Deposito";
                valores5[1] = DepositoTB.Text;
                listavalores.Add(valores5);
                valores6[0] = "Referencia";
                valores6[1] = MaquinaLinea.ReferenciaLlen;
                listavalores.Add(valores6);
                valores7[0] = "Capacidad";
                valores7[1] = CapacidadTB.Text;
                listavalores.Add(valores7);
                valores8[0] = "Marcas";
                valores8[1] = MarcaTB.Text;
                listavalores.Add(valores8);
                valores9[0] = "N Botellas";
                valores9[1] = MaquinaLinea.NBotellasLlen;
                listavalores.Add(valores9);
                valores10[0] = "Alcohol";
                valores10[1] = AlcoholTB.Text;
                listavalores.Add(valores10);
                valores11[0] = "Inicio";
                valores11[1] = MaquinaLinea.HInicioLlen;
                listavalores.Add(valores11);
                valores12[0] = "Fin";
                valores12[1] = MaquinaLinea.HFinLlen;
                listavalores.Add(valores12);
                valores13[0] = "Inicio cambio";
                valores13[1] = MaquinaLinea.HInicioCambioLlen;
                listavalores.Add(valores13);
 


                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileLlenadora, "Registro", listavalores, "Id");
                    if (salida.Contains("ERROR"))
                    {
                        MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                    }
                //Restablecemos los valores correspondientes
                DepositoTB.Text = "";
                ReferenciaTB.Text = "";
                CapacidadTB.Text = "";
                MarcaTB.Text = "";
                NBotTB.Text = "";
                AlcoholTB.Text = "";
                HInicioTB.Text = "";
                HFinTB.Text = "";

                if (MaquinaLinea.numlin == 2)
                {
                    Properties.Settings.Default.HInicioLlenL2 = "";
                    Properties.Settings.Default.HFinLlenL2 = "";
                    Properties.Settings.Default.HInicioCambioLlenL2 = "";
                    Properties.Settings.Default.NBotellasLlenL2 = "";
                    Properties.Settings.Default.ReferenciaLlenL2 = "";
                    Properties.Settings.Default.Save();
                }
                if (MaquinaLinea.numlin == 3)
                {
                    Properties.Settings.Default.HInicioLlenL3 = "";
                    Properties.Settings.Default.HFinLlenL3 = "";
                    Properties.Settings.Default.HInicioCambioLlenL3 = "";
                    Properties.Settings.Default.NBotellasLlenL3 = "";
                    Properties.Settings.Default.ReferenciaLlenL3 = "";
                    Properties.Settings.Default.Save();
                }
                if (MaquinaLinea.numlin == 5)
                {
                    Properties.Settings.Default.HInicioLlenL5 = "";
                    Properties.Settings.Default.HFinLlenL5 = "";
                    Properties.Settings.Default.HInicioCambioLlenL5 = "";
                    Properties.Settings.Default.NBotellasLlenL5 = "";
                    Properties.Settings.Default.ReferenciaLlenL5 = "";
                    Properties.Settings.Default.Save();
                }
                //MessageBox.Show(salida);
                MainLlenadora Form = new MainLlenadora();
                Hide();
                Form.Show();
            }
            else
            {
                MessageBox.Show("FALTAN CAMPOS POR RELLENAR! -> NO SE HA GUARDADO LA INFORMACIÓN");
            }
        }
        
        private void button14_Click(object sender, EventArgs e)
        {
            DepositoTB.Text = "Prueba";
            ReferenciaTB.Text = "Prueba";
            CapacidadTB.Text = "Prueba";
            MarcaTB.Text = "Prueba";
            MarcaTB.Text = "Prueba";
            NBotTB.Text = "Prueba";
            AlcoholTB.Text = "Prueba";
            MessageBox.Show(Properties.Settings.Default.HInicioCambioLlenL2.ToString());
        }
    }
}

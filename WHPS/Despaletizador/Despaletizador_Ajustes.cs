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

namespace WHPS.Despaletizador
{
    public partial class Despaletizador_Ajustes : Form
    {
        string password = "";
        string campokeyboard = "";

        public Despaletizador_Ajustes()
        {
            InitializeComponent();
        }

        //Cerramos la aplicación
        private void Despaletizador_Ajustes_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //Volvemos a la pantalla anterior pulsando el boton back
        private void BackB_Click(object sender, EventArgs e)
        {
            MainDespaletizador Form = new MainDespaletizador();
            Despaletizador_Ajustes.ActiveForm.Hide();
            Form.Show();
        }

        //Cargamos directamente las alarmas que ya estaban activadas
        private void Despaletizador_Ajustes_Load(object sender, EventArgs e)
        {
            //Obligamos que el BOX que pide la contraseña salga al principio
            AjustesBOX.Hide();
            GrupoTecladoContraseña.Show();
            
            //Cargamos las alarmas que ya habiamos definido
            alarmah1TB.Text = Properties.Settings.Default.alarmah1;
            alarmam1TB.Text = Properties.Settings.Default.alarmam1;
            alarmah2TB.Text = Properties.Settings.Default.alarmah2;
            alarmam2TB.Text = Properties.Settings.Default.alarmam2;
            alarmah3TB.Text = Properties.Settings.Default.alarmah3;
            alarmam3TB.Text = Properties.Settings.Default.alarmam3;

            Carga_Alarma();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));

            //Para activar la alarma debe, estar desactivada, haberse chequeado el inicio de turno y que la hora cuadre con la alarma 
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }
        }

        //Para indicar que la alarma esta activada de manera válida indicaremos en al boton aceptar cambiando su color a verde
        private void Carga_Alarma()
        {
            if (alarmah1TB.Text == Properties.Settings.Default.alarmah1 && alarmam1TB.Text == Properties.Settings.Default.alarmam1)
            {
                Alarma1AcpBt.BackColor = Color.Green;
            }
            if ((alarmah1TB.Text != Properties.Settings.Default.alarmah1 && alarmam1TB.Text != Properties.Settings.Default.alarmam1) || alarmah1TB.Text == "")
            {
                Alarma1AcpBt.BackColor = Color.LightGray;
            }

            if (alarmah2TB.Text == Properties.Settings.Default.alarmah2 && alarmam2TB.Text == Properties.Settings.Default.alarmam2)
            {
                Alarma2AcpBt.BackColor = Color.Green;
            }
            if ((alarmah2TB.Text != Properties.Settings.Default.alarmah2 && alarmam2TB.Text != Properties.Settings.Default.alarmam2) || alarmah2TB.Text == "")
            {
                Alarma2AcpBt.BackColor = Color.LightGray;
            }

            if (alarmah3TB.Text == Properties.Settings.Default.alarmah3 && alarmam3TB.Text == Properties.Settings.Default.alarmam3)
            {
                Alarma3AcpBt.BackColor = Color.Green;
            }
            if ((alarmah3TB.Text != Properties.Settings.Default.alarmah3 && alarmam3TB.Text != Properties.Settings.Default.alarmam3) || alarmah3TB.Text == "")
            {
                Alarma3AcpBt.BackColor = Color.LightGray;
            }
        }

        /*Registramos las alarmas al pulsar el boton de aceptar, teniendo se que cumplir que 
          los campos estan rellenos correctamente y en un horario lógico.*/
        private void Alarma1AcpBt_Click(object sender, EventArgs e)
        {
            //Inicialmente se comprueba que tanto la hora como los minutos tienen 2 caracteres
            if (alarmah1TB.Text.Length == 2 && alarmam1TB.Text.Length == 2)
            {
                //Convertimos las variables string en variables enteras para asgurarnos de que la alarma no permite registrar horarios erroneos
                int alarmah1TBint = Convert.ToInt16(alarmah1TB.Text);
                int alarmam1TBint = Convert.ToInt16(alarmam1TB.Text);

                //Comprobamos que la alarma fijada tiene una hora lógica
                if ((alarmah1TBint > 23 || alarmah1TBint < 0) || (alarmam1TBint > 59 || alarmam1TBint < 0))
                {
                    alarmah1TB.Text = "";
                    alarmam1TB.Text = "";

                    Properties.Settings.Default.alarmah1 = "";
                    Properties.Settings.Default.alarmam1 = "";
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.alarmah1 = alarmah1TB.Text;
                    Properties.Settings.Default.alarmam1 = alarmam1TB.Text;
                    Properties.Settings.Default.Save();
                }
            }
            if (alarmah1TB.Text.Length != 2 || alarmam1TB.Text.Length != 2)
            {
                alarmah1TB.Text = "";
                alarmam1TB.Text = "";

                Properties.Settings.Default.alarmah1 = "";
                Properties.Settings.Default.alarmam1 = "";
                Properties.Settings.Default.Save();
                MessageBox.Show("La alarma no ha sido gurdada correctamente, recuerde introducir los datos en el siguiente formato hh:mm");
            }
            Carga_Alarma();
        }
        private void Alarma2AcpBt_Click(object sender, EventArgs e)
        {
            //Inicialmente comprobamos que tanto la hora como los minutos tienen 2 caracteres
            if (alarmah2TB.Text.Length == 2 && alarmam2TB.Text.Length == 2)
            {
                //Convertimos las variables string en variables enteras para asgurarnos de que la alarma no permite registrar horarios erroneos
                int alarmah2TBint = Convert.ToInt16(alarmah2TB.Text);
                int alarmam2TBint = Convert.ToInt16(alarmam2TB.Text);

                //Comprobamos que la alarma fijada tiene una hora lógica
                if ((alarmah2TBint > 23 || alarmah2TBint < 0) || (alarmam2TBint > 59 || alarmam2TBint < 0))
                {
                    alarmah2TB.Text = "";
                    alarmam2TB.Text = "";

                    Properties.Settings.Default.alarmah2 = "";
                    Properties.Settings.Default.alarmam2 = "";
                    Properties.Settings.Default.Save();
                    //MessageBox.Show("La alarma no ha sido gurdada correctamente, recuerde introducir los datos en el siguiente formato hh:mm");
                }
                else
                {
                    Properties.Settings.Default.alarmah2 = alarmah2TB.Text;
                    Properties.Settings.Default.alarmam2 = alarmam2TB.Text;
                    Properties.Settings.Default.Save();
                }
            }
            if (alarmah2TB.Text.Length != 2 || alarmam2TB.Text.Length != 2)
            {
                alarmah2TB.Text = "";
                alarmam2TB.Text = "";

                Properties.Settings.Default.alarmah2 = "";
                Properties.Settings.Default.alarmam2 = "";
                Properties.Settings.Default.Save();
                MessageBox.Show("La alarma no ha sido gurdada correctamente, recuerde introducir los datos en el siguiente formato hh:mm");
            }
            Carga_Alarma();
        }
        private void Alarma3AcpBt_Click(object sender, EventArgs e)
        {
            //Inicialmente comprobamos que tanto la hora como los minutos tienen 2 caracteres
            if (alarmah3TB.Text.Length == 2 && alarmam3TB.Text.Length == 2)
            {
                //Convertimos las variables string en variables enteras para asgurarnos de que la alarma no permite registrar horarios erroneos
                int alarmah3TBint = Convert.ToInt16(alarmah3TB.Text);
                int alarmam3TBint = Convert.ToInt16(alarmam3TB.Text);

                //Comprobamos que la alarma fijada tiene una hora lógica
                if ((alarmah3TBint > 23 || alarmah3TBint < 0) || (alarmam3TBint > 59 || alarmam3TBint < 0))
                {
                    alarmah3TB.Text = "";
                    alarmam3TB.Text = "";

                    Properties.Settings.Default.alarmah3 = "";
                    Properties.Settings.Default.alarmam3 = "";
                    Properties.Settings.Default.Save();
                    //MessageBox.Show("La alarma no ha sido gurdada correctamente, recuerde introducir los datos en el siguiente formato hh:mm");
                }
                else
                {
                    Properties.Settings.Default.alarmah3 = alarmah3TB.Text;
                    Properties.Settings.Default.alarmam3 = alarmam3TB.Text;
                    Properties.Settings.Default.Save();
                }
            }
            if (alarmah3TB.Text.Length != 2 || alarmam3TB.Text.Length != 2)
            {
                alarmah3TB.Text = "";
                alarmam3TB.Text = "";

                Properties.Settings.Default.alarmah3 = "";
                Properties.Settings.Default.alarmam3 = "";
                Properties.Settings.Default.Save();
                MessageBox.Show("La alarma no ha sido gurdada correctamente, recuerde introducir los datos en el siguiente formato hh:mm");
            }
            Carga_Alarma();
        }

        private void Alarma1BorBt_Click(object sender, EventArgs e)
        {
            alarmah1TB.Text = "";
            alarmam1TB.Text = "";

            Properties.Settings.Default.alarmah1 = "";
            Properties.Settings.Default.alarmam1 = "";
            Properties.Settings.Default.Save();
            Carga_Alarma();
        }
        private void Alarma2BorBt_Click(object sender, EventArgs e)
        {
            alarmah2TB.Text = "";
            alarmam2TB.Text = "";

            Properties.Settings.Default.alarmah2 = "";
            Properties.Settings.Default.alarmam2 = "";
            Properties.Settings.Default.Save();
            Carga_Alarma();
        }
        private void Alarma3BorBt_Click(object sender, EventArgs e)
        {
            alarmah3TB.Text = "";
            alarmam3TB.Text = "";

            Properties.Settings.Default.alarmah3 = "";
            Properties.Settings.Default.alarmam3 = "";
            Properties.Settings.Default.Save();
            Carga_Alarma();
        }

        //Grupo de teclado con el que rellener la hora de las distintas alarmas
        private void button11_Click(object sender, EventArgs e)
        {
            if (campokeyboard == "alarmah1")
            {
                alarmah1TB.Text = alarmah1TB.Text + "1";
            }
            if (campokeyboard == "alarmam1")
            {
                alarmam1TB.Text = alarmam1TB.Text + "1";
            }
            if (campokeyboard == "alarmah2")
            {
                alarmah2TB.Text = alarmah2TB.Text + "1";
            }
            if (campokeyboard == "alarmam2")
            {
                alarmam2TB.Text = alarmam2TB.Text + "1";
            }
            if (campokeyboard == "alarmah3")
            {
                alarmah3TB.Text = alarmah3TB.Text + "1";
            }
            if (campokeyboard == "alarmam3")
            {
                alarmam3TB.Text = alarmam3TB.Text + "1";
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            if (campokeyboard == "alarmah1")
            {
                alarmah1TB.Text = alarmah1TB.Text + "2";
            }
            if (campokeyboard == "alarmam1")
            {
                alarmam1TB.Text = alarmam1TB.Text + "2";
            }
            if (campokeyboard == "alarmah2")
            {
                alarmah2TB.Text = alarmah2TB.Text + "2";
            }
            if (campokeyboard == "alarmam2")
            {
                alarmam2TB.Text = alarmam2TB.Text + "2";
            }
            if (campokeyboard == "alarmah3")
            {
                alarmah3TB.Text = alarmah3TB.Text + "2";
            }
            if (campokeyboard == "alarmam3")
            {
                alarmam3TB.Text = alarmam3TB.Text + "2";
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            if (campokeyboard == "alarmah1")
            {
                alarmah1TB.Text = alarmah1TB.Text + "3";
            }
            if (campokeyboard == "alarmam1")
            {
                alarmam1TB.Text = alarmam1TB.Text + "3";
            }
            if (campokeyboard == "alarmah2")
            {
                alarmah2TB.Text = alarmah2TB.Text + "3";
            }
            if (campokeyboard == "alarmam2")
            {
                alarmam2TB.Text = alarmam2TB.Text + "3";
            }
            if (campokeyboard == "alarmah3")
            {
                alarmah3TB.Text = alarmah3TB.Text + "3";
            }
            if (campokeyboard == "alarmam3")
            {
                alarmam3TB.Text = alarmam3TB.Text + "3";
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            if (campokeyboard == "alarmah1")
            {
                alarmah1TB.Text = alarmah1TB.Text + "4";
            }
            if (campokeyboard == "alarmam1")
            {
                alarmam1TB.Text = alarmam1TB.Text + "4";
            }
            if (campokeyboard == "alarmah2")
            {
                alarmah2TB.Text = alarmah2TB.Text + "4";
            }
            if (campokeyboard == "alarmam2")
            {
                alarmam2TB.Text = alarmam2TB.Text + "4";
            }
            if (campokeyboard == "alarmah3")
            {
                alarmah3TB.Text = alarmah3TB.Text + "4";
            }
            if (campokeyboard == "alarmam3")
            {
                alarmam3TB.Text = alarmam3TB.Text + "4";
            }
        }
        private void button15_Click(object sender, EventArgs e)
        {
            if (campokeyboard == "alarmah1")
            {
                alarmah1TB.Text = alarmah1TB.Text + "5";
            }
            if (campokeyboard == "alarmam1")
            {
                alarmam1TB.Text = alarmam1TB.Text + "5";
            }
            if (campokeyboard == "alarmah2")
            {
                alarmah2TB.Text = alarmah2TB.Text + "5";
            }
            if (campokeyboard == "alarmam2")
            {
                alarmam2TB.Text = alarmam2TB.Text + "5";
            }
            if (campokeyboard == "alarmah3")
            {
                alarmah3TB.Text = alarmah3TB.Text + "5";
            }
            if (campokeyboard == "alarmam3")
            {
                alarmam3TB.Text = alarmam3TB.Text + "5";
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            if (campokeyboard == "alarmah1")
            {
                alarmah1TB.Text = alarmah1TB.Text + "6";
            }
            if (campokeyboard == "alarmam1")
            {
                alarmam1TB.Text = alarmam1TB.Text + "6";
            }
            if (campokeyboard == "alarmah2")
            {
                alarmah2TB.Text = alarmah2TB.Text + "6";
            }
            if (campokeyboard == "alarmam2")
            {
                alarmam2TB.Text = alarmam2TB.Text + "6";
            }
            if (campokeyboard == "alarmah3")
            {
                alarmah3TB.Text = alarmah3TB.Text + "6";
            }
            if (campokeyboard == "alarmam3")
            {
                alarmam3TB.Text = alarmam3TB.Text + "6";
            }
        }
        private void button17_Click(object sender, EventArgs e)
        {
            if (campokeyboard == "alarmah1")
            {
                alarmah1TB.Text = alarmah1TB.Text + "7";
            }
            if (campokeyboard == "alarmam1")
            {
                alarmam1TB.Text = alarmam1TB.Text + "7";
            }
            if (campokeyboard == "alarmah2")
            {
                alarmah2TB.Text = alarmah2TB.Text + "7";
            }
            if (campokeyboard == "alarmam2")
            {
                alarmam2TB.Text = alarmam2TB.Text + "7";
            }
            if (campokeyboard == "alarmah3")
            {
                alarmah3TB.Text = alarmah3TB.Text + "7";
            }
            if (campokeyboard == "alarmam3")
            {
                alarmam3TB.Text = alarmam3TB.Text + "7";
            }
        }
        private void button18_Click(object sender, EventArgs e)
        {
            if (campokeyboard == "alarmah1")
            {
                alarmah1TB.Text = alarmah1TB.Text + "8";
            }
            if (campokeyboard == "alarmam1")
            {
                alarmam1TB.Text = alarmam1TB.Text + "8";
            }
            if (campokeyboard == "alarmah2")
            {
                alarmah2TB.Text = alarmah2TB.Text + "8";
            }
            if (campokeyboard == "alarmam2")
            {
                alarmam2TB.Text = alarmam2TB.Text + "8";
            }
            if (campokeyboard == "alarmah3")
            {
                alarmah3TB.Text = alarmah3TB.Text + "8";
            }
            if (campokeyboard == "alarmam3")
            {
                alarmam3TB.Text = alarmam3TB.Text + "8";
            }
        }
        private void button19_Click(object sender, EventArgs e)
        {
            if (campokeyboard == "alarmah1")
            {
                alarmah1TB.Text = alarmah1TB.Text + "9";
            }
            if (campokeyboard == "alarmam1")
            {
                alarmam1TB.Text = alarmam1TB.Text + "9";
            }
            if (campokeyboard == "alarmah2")
            {
                alarmah2TB.Text = alarmah2TB.Text + "9";
            }
            if (campokeyboard == "alarmam2")
            {
                alarmam2TB.Text = alarmam2TB.Text + "9";
            }
            if (campokeyboard == "alarmah3")
            {
                alarmah3TB.Text = alarmah3TB.Text + "9";
            }
            if (campokeyboard == "alarmam3")
            {
                alarmam3TB.Text = alarmam3TB.Text + "9";
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (campokeyboard == "alarmah1")
            {
                alarmah1TB.Text = alarmah1TB.Text + "0";
            }
            if (campokeyboard == "alarmam1")
            {
                alarmam1TB.Text = alarmam1TB.Text + "0";
            }
            if (campokeyboard == "alarmah2")
            {
                alarmah2TB.Text = alarmah2TB.Text + "0";
            }
            if (campokeyboard == "alarmam2")
            {
                alarmam2TB.Text = alarmam2TB.Text + "0";
            }
            if (campokeyboard == "alarmah3")
            {
                alarmah3TB.Text = alarmah3TB.Text + "0";
            }
            if (campokeyboard == "alarmam3")
            {
                alarmam3TB.Text = alarmam3TB.Text + "0";
            }
        }
        private void buttonBorrar2_Click(object sender, EventArgs e)
        {
            if (campokeyboard == "alarmah1")
            {
                alarmah1TB.Text = "";
            }
            if (campokeyboard == "alarmam1")
            {
                alarmam1TB.Text = "";
            }
            if (campokeyboard == "alarmah2")
            {
                alarmah2TB.Text = "";
            }
            if (campokeyboard == "alarmam2")
            {
                alarmam2TB.Text = "";
            }
            if (campokeyboard == "alarmah3")
            {
                alarmah3TB.Text = "";
            }
            if (campokeyboard == "alarmam3")
            {
                alarmam3TB.Text = "";
            }
        }
        private void buttonEnter2_Click(object sender, EventArgs e)
        {
            if (campokeyboard == "alarmah1")
            {
                alarmam1TB.Select();
                campokeyboard = "alarmam1";
            }
            if (campokeyboard == "alarmam1" && alarmam1TB.Text != "")
            {
                alarmah2TB.Select();
                campokeyboard = "alarmah2";
            }
            if (campokeyboard == "alarmah2" && alarmah2TB.Text != "")
            {
                alarmam2TB.Select();
                campokeyboard = "alarmam2";
            }
            if (campokeyboard == "alarmam2" && alarmam2TB.Text != "")
            {
                alarmah3TB.Select();
                campokeyboard = "alarmah3";
            }
            if (campokeyboard == "alarmah3" && alarmah3TB.Text != "")
            {
                alarmam3TB.Select();
                campokeyboard = "alarmam3";
            }
            if (campokeyboard == "alarmam3" && alarmam3TB.Text != "")
            {
                alarmah1TB.Select();
                campokeyboard = "alarmah1";
            }
        }

        //El teclado reconoce donde debe escribir en función de donde se haya hecho click
        private void alarmah1TB_MouseClick(object sender, MouseEventArgs e)
        {
            campokeyboard = "alarmah1";
        }
        private void alarmam1TB_MouseClick(object sender, MouseEventArgs e)
        {
            campokeyboard = "alarmam1";
        }
        private void alarmah2TB_MouseClick(object sender, MouseEventArgs e)
        {
            campokeyboard = "alarmah2";
        }
        private void alarmam2TB_MouseClick(object sender, MouseEventArgs e)
        {
            campokeyboard = "alarmam2";
        }
        private void alarmah3TB_MouseClick(object sender, MouseEventArgs e)
        {
            campokeyboard = "alarmah3";
        }
        private void alarmam3TB_MouseClick(object sender, MouseEventArgs e)
        {
            campokeyboard = "alarmam3";
        }

        //Gurpo inicial donde es necesario introducir la contraseña para acceder a al ajuste posterior
        private void button1_Click(object sender, EventArgs e)
        {
            passwordTB.Text = passwordTB.Text + "*";
            password = password + "1";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            passwordTB.Text = passwordTB.Text + "*";
            password = password + "2";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            passwordTB.Text = passwordTB.Text + "*";
            password = password + "3";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            passwordTB.Text = passwordTB.Text + "*";
            password = password + "4";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            passwordTB.Text = passwordTB.Text + "*";
            password = password + "5";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            passwordTB.Text = passwordTB.Text + "*";
            password = password + "6";
        }
        private void button7_Click(object sender, EventArgs e)
        {
            passwordTB.Text = passwordTB.Text + "*";
            password = password + "7";
        }
        private void button8_Click(object sender, EventArgs e)
        {
            passwordTB.Text = passwordTB.Text + "*";
            password = password + "8";
        }
        private void button9_Click(object sender, EventArgs e)
        {
            passwordTB.Text = passwordTB.Text + "*";
            password = password + "9";
        }
        private void button0_Click(object sender, EventArgs e)
        {
            passwordTB.Text = passwordTB.Text + "*";
            password = password + "0";
        }
        private void buttonBorrar_Click(object sender, EventArgs e)
        {
            passwordTB.Text = "";
            password = "";
        }
        private void buttoEnter_Click(object sender, EventArgs e)
        {
            if (password == "1877")
            {
                GrupoTecladoContraseña.Hide();
                AjustesBOX.Show();
                alarmah1TB.Select();
                campokeyboard = "alarmah1";
            }
            else
            {
                passwordTB.Text = "";
                password = "";
            }
        }
        private void Back2B_Click(object sender, EventArgs e)
        {
            MainDespaletizador Form = new MainDespaletizador();
            Despaletizador_Ajustes.ActiveForm.Hide();
            Form.Show();
        }
    }
}

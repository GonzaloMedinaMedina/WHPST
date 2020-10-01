﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;

namespace WHPS.Etiquetadora
{
    public partial class Etiquetadora_CambioTurno : Form
    {
        public string limpio = "";
        public string protecciones = "";
        public string cuter = "";
        public string herramientas = "";

        public Etiquetadora_CambioTurno()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Boton que te redirige al form anterior.
        /// </summary>       
        private void ExitB_Click(object sender, EventArgs e)
        {
            MainEtiquetadora Form = new MainEtiquetadora();
            Hide();
            Form.Show();
        }

        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }


        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        private void Etiquetadora_CambioTurno_Load(object sender, EventArgs e)
        {
            //Muestra la hora ya que el timer tarda 1s en tomar el control del Label lbReloj.
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;
        }

        /// <summary>
        /// Función del temporizador que se ejecuta cada segundo, indíca la hora y activa la alarma. 
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            //Activamos la alarma cuando el reloj coincida con la hora de la alarma
            if ((lbReloj.Text == (Properties.Settings.Default.alarmah1 + ":" + Properties.Settings.Default.alarmam1 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah2 + ":" + Properties.Settings.Default.alarmam2 + ":" + "00") || lbReloj.Text == (Properties.Settings.Default.alarmah3 + ":" + Properties.Settings.Default.alarmam3 + ":" + "00")))
            {
                MaquinaLinea.ActivarAlarma();
            }
        }

        //#######################  BOTONES DECORACIÓN  ##############################
        private void btnSiPuestoLimpio_Click(object sender, EventArgs e)
        {
            limpio = "SI";
            btnSiPuestoLimpio.BackColor = Color.DarkSeaGreen;
            btnNoPuestoLimpio.BackColor = Color.Gray;
        }
        private void btnNoPuestoLimpio_Click(object sender, EventArgs e)
        {
            limpio = "NO";

            btnSiPuestoLimpio.BackColor = Color.Gray;
            btnNoPuestoLimpio.BackColor = Color.IndianRed;
        }
        private void btnSiProtecciones_Click(object sender, EventArgs e)
        {
            protecciones = "SI";
            btnSiProtecciones.BackColor = Color.DarkSeaGreen;
            btnNoProtecciones.BackColor = Color.Gray;
        }
        private void btnNoProtecciones_Click(object sender, EventArgs e)
        {
            protecciones = "NO";
            btnSiProtecciones.BackColor = Color.Gray;
            btnNoProtecciones.BackColor = Color.IndianRed;
        }
        private void btnSiCuterUbicado_Click(object sender, EventArgs e)
        {
            cuter = "SI";
            btnSiCuterUbicado.BackColor = Color.DarkSeaGreen;
            btnNoCuterUbicado.BackColor = Color.Gray;
        }
        private void btnNoCuterUbicado_Click(object sender, EventArgs e)
        {
            cuter = "NO";
            btnSiCuterUbicado.BackColor = Color.Gray;
            btnNoCuterUbicado.BackColor = Color.IndianRed;
        }
        private void btnSiHerramientas_Click(object sender, EventArgs e)
        {
            herramientas = "SI";
            btnSiHerramientas.BackColor = Color.DarkSeaGreen;
            btnNoHerramientas.BackColor = Color.Gray;
        }
        private void btnNoHerramientas_Click(object sender, EventArgs e)
        {
            herramientas = "NO";
            btnSiHerramientas.BackColor = Color.Gray;
            btnNoHerramientas.BackColor = Color.IndianRed;
        }
        //###########################################################################

        //###############  GUARDADO DE LOS DATOS DE INICIO DEL TURNO  ###############
        private void saveBot_Click(object sender, EventArgs e)
        {
            //Guardamos si todos los campos han sido rellenados
            if (limpio != "" && protecciones != "" && cuter != "" && herramientas != "")
            {
                string salida = MaquinaLinea.GuardarFormCambioTurno("Etiquetadora", MaquinaLinea.MEtiquetadora, limpio, cuter, herramientas, protecciones);
                //Si existe algun error de salvado de datos se expondrá en un MESSAGE 
                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                    //MessageBox.Show(salida);
                }
                //Cuando los datos han sido salvados correctamente reestablecemos los parámetros
                else
                {
                    //En función que que estado estemos, cambiaremos a otro.
                    if (MaquinaLinea.numlin == 2)
                    {
                        switch (MaquinaLinea.chEtiqL2)
                        {
                            case false:
                                Properties.Settings.Default.chEtiqL2 = true;
                                break;

                            case true:
                                Properties.Settings.Default.chEtiqL2 = false;
                                if (MaquinaLinea.chalarmaEtiqL2) Properties.Settings.Default.chalarmaEtiqL2 = false;
                                break;
                        }
                    }
                    if (MaquinaLinea.numlin == 3)
                    {
                        switch (MaquinaLinea.chEtiqL3)
                        {
                            case false:
                                Properties.Settings.Default.chEtiqL3 = true;
                                break;

                            case true:
                                Properties.Settings.Default.chEtiqL3 = false;
                                if (MaquinaLinea.chalarmaEtiqL3) Properties.Settings.Default.chalarmaEtiqL3 = false;
                                break;
                        }
                    }
                    if (MaquinaLinea.numlin == 5)
                    {
                        switch (MaquinaLinea.chEtiqL5)
                        {
                            case false:
                                Properties.Settings.Default.chEtiqL5 = true;
                                break;

                            case true:
                                Properties.Settings.Default.chEtiqL5 = false;
                                if (MaquinaLinea.chalarmaEtiqL5) Properties.Settings.Default.chalarmaEtiqL5 = false;
                                break;
                        }
                    }

                    //Cargamos las variables ya que han sido modificadas
                    MaquinaLinea.chEtiqL2 = Properties.Settings.Default.chEtiqL2;
                    MaquinaLinea.chEtiqL3 = Properties.Settings.Default.chEtiqL3;
                    MaquinaLinea.chEtiqL5 = Properties.Settings.Default.chEtiqL5;
                    Properties.Settings.Default.Save();

                    //Si algún elemento no está en el estado que debe, se mostrará el form de comentarios si el operio lo puede justificar
                    if (limpio == "NO" || protecciones == "NO" || cuter == "NO" || herramientas == "NO")
                    {
                        DialogResult opcion;
                        opcion = MessageBox.Show("Algunos de los campos selecciondos no se encuentra en el estado que debería. ¿Puede indicar a que se debe?", "", MessageBoxButtons.YesNo);
                        if (opcion == DialogResult.Yes)
                        {
                            Etiquetadora_Comentarios Form = new Etiquetadora_Comentarios();
                            Hide();
                            Form.Show();
                        }
                        else
                        {
                            MainEtiquetadora Form = new MainEtiquetadora();
                            Hide();
                            Form.Show();
                        }
                    }
                    else
                    {
                        MainEtiquetadora Form = new MainEtiquetadora();
                        Hide();
                        Form.Show();
                    }

                }
            }

            //Si los campos no han sido rellenados un MESSAGE avisará al operario
            else
            {
                MessageBox.Show("FALTAN CAMPOS POR RELLENAR! -> NO SE HA GUARDADO LA INFORMACIÓN");
            }
        }
        //###########################################################################
    }
}

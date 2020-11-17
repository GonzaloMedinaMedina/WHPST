﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WHPS.Utiles;
using System.Diagnostics;
using System.IO;
using WHPS.Model;
using WHPS.ProgramMenus;

namespace WHPS.Llenadora
{
    public partial class Llenadora_Comentarios : Form
    {
        MainLlenadora parent;
        public Llenadora_Comentarios(MainLlenadora p)
        {
            InitializeComponent();
            parent = p;
        }


        private void ExitB_Click(object sender, EventArgs e)
        {
            Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainLlenadora));
            this.Hide();
            this.Dispose();
        }

        /// <summary>
        /// Boton que minimiza la ventana.
        /// </summary>
        private void MinimizarB_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

        private void Llenadora_Comentarios_Load(object sender, EventArgs e)
        {
            //El timer tiene un pequeño retraso cargamos desde el load el primer tiempo que debe marcar el reloj al cargar
            lbReloj.Text = (DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss"));

            //Si se está registrado con un usuario mostraremos un boton que permite minimizar el programa.
            if (MaquinaLinea.usuario != "") MinimizarB.Visible = true;

            //############## ABRIR ON SCREEN KEYBOARD  ###############
            /*try
            {
                //Process.Start(Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "Sysnative", "cmd.exe"), "/c osk.exe" + " & exit");
                Process p1 = new Process();
                p1.StartInfo.FileName = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "Sysnative", "cmd.exe");
                p1.StartInfo.Arguments = "/c osk.exe";
                p1.StartInfo.UseShellExecute = false;
                p1.StartInfo.CreateNoWindow = true;

                p1.Start();

                p1.WaitForExit(100);
                p1.Close();
            }
            catch { }*/
            //Hacemos activo el campo de texto y marcamos el cursor para la selección
            Activate();
            ComentariosTB.Select();
        }

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
        }

        private void saveBot_Click(object sender, EventArgs e)
        {
            List<string[]> listavalores = new List<string[]>();
            string[] valores0 = new string[2];
            string[] valores1 = new string[2];
            string[] valores01 = new string[2];
            string[] valores2 = new string[2];
            string[] valores3 = new string[2];
            string[] valores4 = new string[2];
            string[] valores5 = new string[2];


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
            valores5[0] = "Comentarios";
            valores5[1] = ComentariosTB.Text;
            listavalores.Add(valores5);

            if (ComentariosTB.Text != "")
            {
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileLlenadora, "Comentarios", listavalores, "Id");

                if (salida.Contains("ERROR"))
                {
                    MessageBox.Show("Error en el grabado de datos, tomar nota y guardar documentación");
                }
                //MessageBox.Show(salida);
                else
                {
                    Utilidades.AbrirForm(parent, parent.GetParentInicio(), typeof(MainLlenadora));
                    this.Hide();
                    this.Dispose();
                }
            }


        }

        private void ComentariosTB_Click(object sender, EventArgs e)
        {
            WHPS.Utiles.VentanaTeclados.AbrirCalculadora(this, this.ComentariosTB as TextBoxBase);
        }
    }
}

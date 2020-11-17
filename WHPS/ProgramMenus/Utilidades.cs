using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.Utiles;

namespace WHPS.ProgramMenus
{
    public class Utilidades
    {
        //Variables empleadas en las distintas funciones para controlar el cambio de turno.
        private static string Turno = "";
        private static int diaC = DateTime.Now.Day;
        private static int Hora = DateTime.Now.Hour;



        public static void AbrirForm(Form siguiente, Form actual, Type t)
        {
            //Obtenemos el tipo de objeto de siguiente


            Type[] types = new Type[1];
            //Obtenemos los constructores para el tipo de objeto siguiente, el nuevo form a abrir
            ConstructorInfo[] ci = t.GetConstructors();
            //Declaramos los parámetros, que normalmente será el form actual (padre)
            Object[] o = new Object[1];
            o[0] = actual;

            //Si el primer constructor está vacío, es que nos encontramos con una clase como WHPST_INICIO, y elegimos el segundo constructor
            if (ci[0].ToString() == "Void .ctor()")
            {
                //Invocamos al constructor y le asignamos a siguiente el nuevo objeto
                siguiente = ci[1].Invoke(o) as Form;
            }
            else
            {
                siguiente = ci[0].Invoke(o) as Form;
            }

            siguiente.Show();
            actual.Hide();
            if (typeof(WHPST_INICIO) != actual.GetType())
            {

                actual.Dispose();
                actual = null;
            }
        }
        public static void AbrirFormHijo(Panel p, Form siguiente, Form siguientehijo, Form actual)
        {
            //Abrimos form padre
        //    AbrirForm(siguiente, actual, typeof(siguiente));

            //Abre el form hijo en el panel indicado.
            if (p.Controls.Count > 0) p.Controls.RemoveAt(0);

            //Form SM = WHPST_FORM as Form;
            siguientehijo.TopLevel = false;
            siguientehijo.Dock = DockStyle.Fill;
            p.Controls.Add(siguientehijo);
            p.Tag = siguientehijo;
            siguientehijo.Show();
        }

        public static void FuncionLoad(TextBox MaquinistaTB, string Maquinista, bool CheckL2, bool CheckL3, bool CheckL5, Button CambioTurnoB)
        {
            MaquinistaTB.Text = Maquinista;
            if (MaquinaLinea.numlin == 2)
            {
                //Definimos el color del maquinista segun la Linea.
                MaquinistaTB.BackColor = Color.IndianRed;

                //Marcamos la entrada o salida del turno.
                if (!CheckL2) CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoEntrar;
                else CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoSalir;

            }
            if (MaquinaLinea.numlin == 3)
            {
                //Definimos el color del maquinista segun la Linea.
                MaquinistaTB.BackColor = Color.Green;

                //Marcamos la entrada o salida del turno.
                if (!CheckL3) CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoEntrar;
                else CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoSalir;
            }
            if (MaquinaLinea.numlin == 5)
            {
                //Definimos el color del maquinista segun la Linea.
                MaquinistaTB.BackColor = Color.LightSkyBlue;

                //Marcamos la entrada o salida del turno.
                if (!CheckL5) CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoEntrar;
                else CambioTurnoB.BackgroundImage = Properties.Resources.CambioTurnoSalir;
            }
        }
        /// <summary>
        /// Función abre el form del cambio de turno si este se efectua o cambia el día
        /// </summary>
        public static bool ShiftCheck()
        {
            bool Cambio = false;
            //Obtenermos el turno y el día actual
            string Turno = MaquinaLinea.turno;

            //Si han cambiado rediccionaremos a Cambio de Turno. 
            if ((Turno != MaquinaLinea.turno))
            {
                if (((MaquinaLinea.numlin == 2) && (MaquinaLinea.checkL2 == true)) || ((MaquinaLinea.numlin == 3) && (MaquinaLinea.checkL3 == true)) || ((MaquinaLinea.numlin == 5) && (MaquinaLinea.checkL5 == true)))
                {
                    MaquinaLinea.RetornoInicio = "CambioTurno";
                    Cambio = true;
                }
                else MaquinaLinea.RetornoInicio = "";
            }
            return Cambio;
        }






        /// <summary>
        /// Función que obtiene el personal correspondiente a una línea de prodducción y un turno de trabajo.
        /// </summary>
        public static void Carga_Personal()
        {
            //###### CHEQUEAMOS SI ES NECESARIO ACTUALIZAR EL TURNO ###################
            if (ShiftCheck())
            {
                if ((MaquinaLinea.numlin == 2) && (MaquinaLinea.checkL2 == true))
                {
                    Properties.Settings.Default.checkL2 = false;
                    Properties.Settings.Default.checkL3 = false;
                    Properties.Settings.Default.checkL5 = false;
                    Properties.Settings.Default.Save();
                    MaquinaLinea.RetornoInicio = "CambioTurno";
                }
                if ((MaquinaLinea.numlin ==3) && (MaquinaLinea.checkL3 == true))
                {
                    Properties.Settings.Default.checkL2 = false;
                    Properties.Settings.Default.checkL3 = false;
                    Properties.Settings.Default.checkL5 = false;
                    Properties.Settings.Default.Save();
                    MaquinaLinea.RetornoInicio = "CambioTurno";
                }
                if ((MaquinaLinea.numlin == 5) && (MaquinaLinea.checkL5 == true))
                {
                    Properties.Settings.Default.checkL2 = false;
                    Properties.Settings.Default.checkL3 = false;
                    Properties.Settings.Default.checkL5 = false;
                    MaquinaLinea.RetornoInicio = "CambioTurno";
                }
            }
            CambioTurno.Obtener_Personal_Datos_Lineas();
        }

        /// <summary>
        /// Función que muesta la imagen de la busqueda o de la fila selecionada en pantalla.
        /// </summary>
        /// <param name="NombreImagen">El nombre de la imagen se guarda con el códgo de referencia de aquel producto que se este exhibiendo.</param>
        /// <param name="Imagen">Se indica en nombre del PictureBox donde se va a mostrar la imagen.</param>
        public static void MostrarImagen(string NombreImagen, PictureBox Imagen)
        {
            //Las imagenes se guardan en formato .jpg por ello se le añade la terminación
            NombreImagen = NombreImagen + ".jpg";
            if (NombreImagen != "")
            {
                try
                {
                    Imagen.Image = Image.FromFile(MaquinaLinea.RutaPicutreBOM + NombreImagen);
                }
                //Si la referencia no tiene imagen se muestra una imagen  establezida que indicará que no tiene foto el producto
                catch (System.IO.FileNotFoundException)
                {
                    Imagen.Image = Properties.Resources.GenSinImagen;
                }
            }
        }
        /// <summary>
        /// Función que registra los parámetros necesarios para utilizar correctamente el teclado.
        /// </summary>
        /// <param name="Form">Determina el form donde aparece el teclado.</param>
        /// <param name="TextBox">Determina el TextBox donde va a escribir el teclado.</param>
        /// <param name="ModoTeclado">Parametro que indica si el teclado es para una contraseña o no.</param>
        /// <param name="TipoTeclado">Parametro que modifica el boton adicional u lo oculta.</param>
        public static void ParametrosTeclado(bool ModoTeclado, int TipoTeclado)
        {
            MaquinaLinea.ModoTeclado = ModoTeclado;
            MaquinaLinea.TipoTeclado = TipoTeclado;
        }
        public static void MostrarTecladoPredeterminado(TextBox TextBox)
        {
            //############## ABRIR ON SCREEN KEYBOARD  ###############
            try
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
            catch { }
            //Hacemos activo el campo de texto y marcamos el cursor para la selección
            WHPST_LOGIN.ActiveForm.Activate();
        }

        /// <summary>
        /// Función que abre un form hijo en un panel determinado.
        /// </summary>
        /// <param name="WHPST_FORM">Parámetro que indica que form que desea abrir.</param>
        private static void AbrirFormHijo(object WHPST_FORM , Panel Panel)
        {
            if (Panel.Controls.Count > 0)
            {
                Panel.Controls.RemoveAt(0);
            }
            Form SM = WHPST_FORM as Form;
            SM.TopLevel = false;
            SM.Dock = DockStyle.Fill;
            Panel.Controls.Add(SM);
            Panel.Tag = SM;
            SM.Show();
        }
    }
}
  
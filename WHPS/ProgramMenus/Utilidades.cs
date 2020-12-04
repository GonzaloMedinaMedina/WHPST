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



        public static Form AbrirForm(Form siguiente, Form actual, Type t)
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
            return siguiente;
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
        /// Función que devuelve el número de botellas dado el formato y el número de cajas.
        /// </summary>
        public static string ObtenerBotellas(string formato ,string cajas)
        {
            string Botellas = "ERROR";

            if (formato.Contains("X") || formato.Contains("x"))
            {
                if (formato.Substring(2, 1) == "X" || formato.Substring(2, 1) == "X") formato = formato.Substring(0, 2);
                else formato = formato.Substring(0, 1);

                return Botellas = Convert.ToString(Convert.ToInt32(formato) * Convert.ToInt32(cajas));
            }
            else return Botellas;
        }
        /// <summary>
        /// Función que devuelve el número de botellas dado el formato y el número de cajas.
        /// </summary>
        public static string ObtenerCapacidad(string formato)
        {
            string Capacidad = "ERROR";

            if (formato.Contains("X"))
            {
                int n = formato.Length - formato.IndexOf('X') - 1;
                formato = formato.Substring(formato.IndexOf('X') + 1, n);
                return Capacidad = Convert.ToString(Convert.ToDouble(formato) * 1000);
            }
            else return Capacidad;
        }



        /// <summary>
        /// Función busca que fila estamos segun el idorden se haya indicado.
        /// </summary>
        public static int BuscarFila(string idlanz, DataGridView dgv)
        {
            int fila = 0;
            for (int i = 0; (i < (dgv.RowCount - 1)); i++)
            {
                if (MaquinaLinea.numlin == 2) { if (dgv.Rows[i].Cells[1].Value.ToString() == idlanz) { fila = i;  } }
                if (MaquinaLinea.numlin == 3) { if (dgv.Rows[i].Cells[1].Value.ToString() == idlanz) { fila = i;  } }
                if (MaquinaLinea.numlin == 5) { if (dgv.Rows[i].Cells[1].Value.ToString() == idlanz) { fila = i;  } }
            }
            return fila;
        }

        /// <summary>
        /// Función que marca una serie de columnas de un determinado color
        /// </summary>
        public static void SeleccionFila(DataGridView dgv, Color Color, int fila)
        {
            dgv.Rows[fila].Cells["ORDEN"].Style.BackColor = Color;
            dgv.Rows[fila].Cells["FORM."].Style.BackColor = Color;
            dgv.Rows[fila].Cells["CAJAS"].Style.BackColor = Color;
            dgv.Rows[fila].Cells["PRODUCTO"].Style.BackColor = Color;
            dgv.Rows[fila].Cells["CLIENTE"].Style.BackColor = Color;
            dgv.Rows[fila].Cells["CÓDIGO"].Style.BackColor = Color;
        }
        /// <summary>
        /// Función que da color a unos text box determinados para indicar el estado del producto
        /// </summary>
        public static void ColorTextBox(TextBox Estado, TextBox Liquido, TextBox Materiales)
        {
            switch (Estado.Text)
            {
                case "Completado":
                    Estado.BackColor = System.Drawing.Color.Green;
                    Estado.ForeColor = System.Drawing.Color.White;
                    break;
                case "Saltado":
                    Estado.BackColor = System.Drawing.Color.Yellow;
                    break;
                case "Iniciado":
                    Estado.BackColor = System.Drawing.Color.Orange;
                    break;
                case "Sin Terminar":
                    Estado.BackColor = System.Drawing.Color.Red;
                    break;
                case "":
                    Estado.BackColor = System.Drawing.Color.Gainsboro;
                    break;
            }
            switch (Liquido.Text)
            {
                case "OK":
                    Liquido.BackColor = System.Drawing.Color.Green;
                    Liquido.ForeColor = System.Drawing.Color.White;
                    break;
                case "ELABORACIÓN":
                    Liquido.BackColor = System.Drawing.Color.Yellow;
                    break;
                case "NOK":
                    Liquido.BackColor = System.Drawing.Color.Red;
                    break;
                case "":
                    Liquido.BackColor = System.Drawing.Color.Gainsboro;
                    break;
            }
            switch (Materiales.Text)
            {
                case "OK":
                    Materiales.BackColor = System.Drawing.Color.Green;
                    Materiales.ForeColor = System.Drawing.Color.White;
                    break;
                case "PENDIENTE":
                    Materiales.BackColor = System.Drawing.Color.Orange;
                    break;
                case "NOK":
                    Materiales.BackColor = System.Drawing.Color.Red;
                    break;
                case "":
                    Materiales.BackColor = System.Drawing.Color.Gainsboro;
                    break;
            }
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

        internal static Color AvisoBoton(Color c)
        {
            Color r;
            if (c == Color.White)
            {
                r = Color.Yellow;
            }
            else r = (c == Color.Red) ? Color.Yellow : Color.Red;

            return r;
        }
    }
}
  
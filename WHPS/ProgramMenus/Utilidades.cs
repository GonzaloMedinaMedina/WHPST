using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.Utiles;

namespace WHPS.ProgramMenus
{
    public class Utilidades
    {

        public static float f_HeightRatio = new float();
        public static float f_WidthRatio = new float();
        /// <summary>
        /// Función abre el form del cambio de turno si este se efectua o cambia el día
        /// </summary>
        public static void ShiftCheck()
        {
            string Turno = ObtenerTurnoActual();
            int diaC = DateTime.Now.Day;

            //###### CHEQUEAMOS SI ES NECESARIO ACTUALIZAR EL TURNO ###################
            if ((Turno != MaquinaLinea.turno) || (diaC != MaquinaLinea.diaT))
            {
                if ((MaquinaLinea.numlin == 2) && (MaquinaLinea.checkL2 == true))
                {
                    MaquinaLinea.RetornoInicio = "CambioTurno";
                }
                if ((MaquinaLinea.numlin == 3) && (MaquinaLinea.checkL3 == true))
                {
                    MaquinaLinea.RetornoInicio = "CambioTurno";
                }
                if ((MaquinaLinea.numlin == 5) && (MaquinaLinea.checkL5 == true))
                {
                    MaquinaLinea.RetornoInicio = "CambioTurno";
                }
            }
        }

        /// <summary>
        /// Función que indica el turno actual en función de la hora.
        /// </summary>
        /// <returns>Devuelve el valor del turno</returns>
        public static string ObtenerTurnoActual()
        {
            string Turno = "";

            int hora = DateTime.Now.Hour;
            if (hora >= 7 && hora < 15) Turno = "Mañana";
            if (hora >= 15 && hora < 23) Turno = "Tarde";
            if (hora >= 23 || hora < 7) Turno = "Noche";

            return Turno;
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
        public static string EscribirTeclado(numberpad numberpad1, TextBox TextBox, Button Boton)
        {

            if (MaquinaLinea.ModoTeclado == false) return MaquinaLinea.Teclado;

            if (MaquinaLinea.ModoTeclado == true)
            {
                if (MaquinaLinea.Password == true) Boton.BackColor = Color.DarkSeaGreen;
                if (MaquinaLinea.Password == false) Boton.BackColor = Color.IndianRed;
                return "";
            }
            MaquinaLinea.StatusTeclado = false;

            MaquinaLinea.Teclado = "";
            numberpad1.Visible = false;
            return "";
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
        public static void AjustarResolucion(System.Windows.Forms.Form Form)
        {
            String ancho = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width.ToString();
            String alto = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height.ToString();
            String tamaño = ancho + "x" + alto;
            switch (tamaño)
            {
                case "800x600":
                    CambiarResolucion(Form, 110F, 110F);
                    break;
                case "1024x600":
                    CambiarResolucion(Form, 110F, 110F);
                    break;
                default:
                    CambiarResolucion(Form, 96F, 96F);
                    break;
            }
        }
        public static void CambiarResolucion(System.Windows.Forms.Form Form, float ancho, float alto)
        {
            Form.AutoScaleDimensions = new System.Drawing.SizeF(ancho, alto);
            Form.PerformAutoScale();
        }


        public static void AjustarResolucion1(Form ObjForm, int DesignerHeight, int DesignerWidth)
        {

            #region Code for Resizing and Font Change According to Resolution
            //Specify Here the Resolution Y component in which this form is designed
            //For Example if the Form is Designed at 800 * 600 Resolution then DesignerHeight=600
            int i_StandardHeight = DesignerHeight;
            //Specify Here the Resolution X component in which this form is designed
            //For Example if the Form is Designed at 800 * 600 Resolution then DesignerWidth=800
            int i_StandardWidth = DesignerWidth;
            int i_PresentHeight = Screen.PrimaryScreen.Bounds.Height;//Present Resolution Height
            int i_PresentWidth = Screen.PrimaryScreen.Bounds.Width;//Presnet Resolution Width
            f_HeightRatio = (float)((float)i_PresentHeight / (float)i_StandardHeight);
            f_WidthRatio = (float)((float)i_PresentWidth / (float)i_StandardWidth);
            ObjForm.AutoScaleMode = AutoScaleMode.None;//Make the Autoscale Mode=None
            ObjForm.Scale(new SizeF(f_WidthRatio, f_HeightRatio));
            foreach (Control c in ObjForm.Controls)
            {
                if (c.HasChildren)
                {
                    ResizeControlStore(c);
                }
                else
                {
                    c.Font = new Font(c.Font.FontFamily, c.Font.Size * f_HeightRatio, c.Font.Style, c.Font.Unit, ((byte)(0)));
                }
            }
            ObjForm.Font = new Font(ObjForm.Font.FontFamily, ObjForm.Font.Size * f_HeightRatio, ObjForm.Font.Style, ObjForm.Font.Unit, ((byte)(0)));
            #endregion
        }

        private static void ResizeControlStore(Control objCtl)
        {
            if (objCtl.HasChildren)
            {
                foreach (Control cChildren in objCtl.Controls)
                {
                    if (cChildren.HasChildren)
                    {
                        ResizeControlStore(cChildren);
                    }
                    else
                    {
                        cChildren.Font = new Font(cChildren.Font.FontFamily, cChildren.Font.Size * f_HeightRatio, cChildren.Font.Style, cChildren.Font.Unit, ((byte)(0)));
                    }
                }
                objCtl.Font = new Font(objCtl.Font.FontFamily, objCtl.Font.Size * f_HeightRatio, objCtl.Font.Style, objCtl.Font.Unit, ((byte)(0)));
            }
            else
            {
                objCtl.Font = new Font(objCtl.Font.FontFamily, objCtl.Font.Size * f_HeightRatio, objCtl.Font.Style, objCtl.Font.Unit, ((byte)(0)));
            }

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
  
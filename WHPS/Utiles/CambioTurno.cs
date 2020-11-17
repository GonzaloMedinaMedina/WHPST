using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHPS.Model;

namespace WHPS.Utiles
{
    class CambioTurno
    {

        private static string Turno, TurnoGuardado;
        private static int Hora = DateTime.Now.Hour;
        public static bool CambioFueraHora = false;
        public static int HoraGuardada;


        /// <summary>
        /// Función que indica el turno actual en función de la hora actual.
        /// </summary>
        /// <returns>Devuelve el valor del turno</returns>
        public static string ObtenerTurnoActual()
        {
            if (Hora >= 7 && Hora < 15) Turno = "Mañana";
            if (Hora >= 15 && Hora < 23) Turno = "Tarde";
            if (Hora >= 23 || Hora < 7) Turno = "Noche";
            return Turno;
        }
        /// <summary>
        /// Función que obtiene el personal correspondiente a una línea de prodducción y un turno de trabajo.
        /// </summary>
        public static void Obtener_Personal_Datos_Lineas()
        {
            FuncionesExcel.LeerExcelDatos_Lineas("Datos_Lineas", "L" + MaquinaLinea.numlin, MaquinaLinea.turno);
        }
        /// <summary>
        /// Marca como check la linea y masrca como no leido el check de la máquina.
        /// </summary>
        public static void ActualizarYGuardarValores()
        {

            Properties.Settings.Default.turno = MaquinaLinea.turno;
            CambioFueraHora = false;
            if (MaquinaLinea.turno != ObtenerTurnoActual()) CambioFueraHora = true;
            Properties.Settings.Default.diaT = DateTime.Now.ToString("dd/MM/yyyy");
            HoraGuardada = DateTime.Now.Hour;
            MaquinaLinea.diaT = Properties.Settings.Default.diaT;
            if (MaquinaLinea.numlin == 2)
            {
                Properties.Settings.Default.checkL2 = true;
                Properties.Settings.Default.chDesL2 = false;
                Properties.Settings.Default.chLlenL2 = false;
                Properties.Settings.Default.chEtiqL2 = false;
                Properties.Settings.Default.chEncL2 = false;
                Properties.Settings.Default.chConL2 = false;
                MaquinaLinea.checkL2 = Properties.Settings.Default.checkL2;
                MaquinaLinea.chDesL2 = Properties.Settings.Default.chDesL2;
                MaquinaLinea.chLlenL2 = Properties.Settings.Default.chLlenL2;
                MaquinaLinea.chEtiqL2 = Properties.Settings.Default.chEtiqL2;
                MaquinaLinea.chEncL2 = Properties.Settings.Default.chEncL2;
                MaquinaLinea.chConL2 = Properties.Settings.Default.chConL2;


            }
            if (MaquinaLinea.numlin == 3)
            {
                Properties.Settings.Default.checkL3 = true;
                Properties.Settings.Default.chDesL3 = false;
                Properties.Settings.Default.chLlenL3 = false;
                Properties.Settings.Default.chEtiqL3 = false;
                Properties.Settings.Default.chEncL3 = false;
                Properties.Settings.Default.chConL3 = false;
                MaquinaLinea.checkL3 = Properties.Settings.Default.checkL3;
                MaquinaLinea.chDesL3 = Properties.Settings.Default.chDesL3;
                MaquinaLinea.chLlenL3 = Properties.Settings.Default.chLlenL3;
                MaquinaLinea.chEtiqL3 = Properties.Settings.Default.chEtiqL3;
                MaquinaLinea.chEncL3 = Properties.Settings.Default.chEncL3;
                MaquinaLinea.chConL3 = Properties.Settings.Default.chConL3;

            }
            if (MaquinaLinea.numlin == 5)
            {
                Properties.Settings.Default.checkL5 = true;
                Properties.Settings.Default.chDesL5 = false;
                Properties.Settings.Default.chLlenL5 = false;
                Properties.Settings.Default.chEtiqL5 = false;
                Properties.Settings.Default.chEncL5 = false;
                Properties.Settings.Default.chConL5 = false;
                MaquinaLinea.checkL5 = Properties.Settings.Default.checkL5;
                MaquinaLinea.chDesL5 = Properties.Settings.Default.chDesL5;
                MaquinaLinea.chLlenL5 = Properties.Settings.Default.chLlenL5;
                MaquinaLinea.chEtiqL5 = Properties.Settings.Default.chEtiqL5;
                MaquinaLinea.chEncL5 = Properties.Settings.Default.chEncL5;
                MaquinaLinea.chConL5 = Properties.Settings.Default.chConL5;

            }
            Properties.Settings.Default.Save();
        }

        public static bool ComprobarCambioTurno()
        {
            //Obtengo el turno actual y el día actual para comparar
            string TurnoActual = ObtenerTurnoActual();
            string DiaActual = DateTime.Now.ToString("dd/MM/yyyy");

            //Si el turno o el día no es el mismo que el ultimo guardado, se activará. A demas CambioFueraHora debe ser false.
            if (((MaquinaLinea.turno != TurnoActual) || (MaquinaLinea.diaT != DiaActual)) && !CambioFueraHora)
            {
                if (MaquinaLinea.numlin == 2 && MaquinaLinea.checkL2 == true)
                {
                    return true;
                }
                if (MaquinaLinea.numlin == 3 && MaquinaLinea.checkL3 == true)
                {
                    return true;
                }
                if (MaquinaLinea.numlin == 5 && MaquinaLinea.checkL5 == true)
                {
                    return true;
                }
                return false;
            }
            else return false;
        }
        public static bool ComprobarCambioTurno2()
        {
            //Obtengo el turno actual y el día actual para comparar
            string TurnoActual = ObtenerTurnoActual();
            string DiaActual = DateTime.Now.ToString("dd/MM/yyyy");

            //Si el turno o el día no es el mismo que el ultimo guardado, se activará. A demas CambioFueraHora debe ser false.
            if (((MaquinaLinea.turno != TurnoActual) || (MaquinaLinea.diaT != DiaActual) ) && !CambioFueraHora)
            {
                if (MaquinaLinea.numlin == 2 && MaquinaLinea.checkL2==true)
                {
                    Properties.Settings.Default.checkL2 = false;
                    MaquinaLinea.checkL2 = Properties.Settings.Default.checkL2;
                }
                if (MaquinaLinea.numlin == 3 && MaquinaLinea.checkL3 == true)
                {
                    Properties.Settings.Default.checkL3 = false;
                    MaquinaLinea.checkL3 = Properties.Settings.Default.checkL3;
                }
                if (MaquinaLinea.numlin == 5 && MaquinaLinea.checkL5 == true)
                {
                    Properties.Settings.Default.checkL5 = false;
                    MaquinaLinea.checkL5 = Properties.Settings.Default.checkL5;
                }
                Properties.Settings.Default.Save();
                return true;
            }
            return false;
        }
        public static void ResetCabioFueraHora()
        {
            if (HoraGuardada >= 7 && HoraGuardada < 15) TurnoGuardado = "Mañana";
            if (HoraGuardada >= 15 && HoraGuardada < 23) TurnoGuardado = "Tarde";
            if (HoraGuardada >= 23 || HoraGuardada < 7) TurnoGuardado = "Noche";
            if (TurnoGuardado != ObtenerTurnoActual()) CambioFueraHora = false;
        }
    }
}

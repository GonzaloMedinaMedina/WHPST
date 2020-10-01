using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.FUNCIONES
{
    public class ACTUALIZAR_TURNO
    {
        public static string ACTUALIZARTURNO()
        {
            string Turno = "";
            int diaC = Convert.ToInt16(DateTime.Now.ToString("dd"));

            int hora = Convert.ToInt16(DateTime.Now.ToString("HH"));
            if (hora >= 7 && hora < 15)
            {
                Turno = "Mañana";
            }
            else
            {
                if (hora >= 15 && hora < 23)
                {
                    Turno = "Tarde";
                }
                else { Turno = "Noche"; }
            }
            //###### CHEQUEAMOS SI ES NECESARIO ACTUALIZAR EL TURNO ###################
            if ((Turno != Globals.turno) || (diaC != Globals.diaT) || ((Globals.numlin == 2) && (!Globals.checkL2)) || ((Globals.numlin == 3) && (!Globals.checkL3)) || ((Globals.numlin == 5) && (!Globals.checkL5)))
            {
                FUNCIONES.ACTUALIZAR_TURNO Form2 = new FUNCIONES.ACTUALIZAR_TURNO();
                Form ActualF = Form.ActiveForm;
                ActualF.Hide();
                Form2.Show();
            }
            return Turno;
        }
    }
}

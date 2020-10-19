using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;

namespace WHPS.Utiles
{
    class AlertaTurno
    {
        private TimeSpan _TurnoMañanaIni = new TimeSpan(Convert.ToInt32(MaquinaLinea.AvisoTurnoMañana.Split('.')[0]), Convert.ToInt32(MaquinaLinea.AvisoTurnoMañana.Split('.')[1]), 0);
        private TimeSpan _TurnoMañanaFin = new TimeSpan(Convert.ToInt32(MaquinaLinea.AvisoTurnoMañana.Split('.')[0]), Convert.ToInt32(MaquinaLinea.AvisoTurnoMañana.Split('.')[1])+15, 0);
        private TimeSpan _TurnoTardeIni = new TimeSpan(Convert.ToInt32(MaquinaLinea.AvisoTurnoTarde.Split('.')[0]), Convert.ToInt32(MaquinaLinea.AvisoTurnoTarde.Split('.')[1]), 0);
        private TimeSpan _TurnoTardeFin = new TimeSpan(Convert.ToInt32(MaquinaLinea.AvisoTurnoTarde.Split('.')[0]), Convert.ToInt32(MaquinaLinea.AvisoTurnoTarde.Split('.')[1])+15, 0);
        private TimeSpan _TurnoNocheIni = new TimeSpan(Convert.ToInt32(MaquinaLinea.AvisoTurnoNoche.Split('.')[0]), Convert.ToInt32(MaquinaLinea.AvisoTurnoNoche.Split('.')[1]), 0);
        private TimeSpan _TurnoNocheFin = new TimeSpan(Convert.ToInt32(MaquinaLinea.AvisoTurnoNoche.Split('.')[0]), Convert.ToInt32(MaquinaLinea.AvisoTurnoNoche.Split('.')[1])+15, 0);
                
        private int _AvisoNumero = 0;
        private bool _MostrarAviso = false;

        public AlertaTurno()
        {
            BackgroundWorker alertaTurnoBW = new BackgroundWorker();
            alertaTurnoBW.WorkerReportsProgress = true;
            alertaTurnoBW.DoWork += ComprobarHoraCierreTurno;
            alertaTurnoBW.RunWorkerAsync();
        }

        private void MostrarMensajeAviso()
        {
            if (_MostrarAviso && _AvisoNumero == 0)
            {
                MessageBox.Show(MaquinaLinea.MensajeAvisoTurno);
                _MostrarAviso = false;
                _AvisoNumero++;
            }
            else
            {
                if (!_MostrarAviso)
                {
                    _AvisoNumero = 0;
                }
            }
        }

        private void ComprobarHoraCierreTurno(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (DateTime.Now.TimeOfDay > _TurnoMañanaIni && DateTime.Now.TimeOfDay < _TurnoMañanaFin)
                {
                    MostrarMensajeAviso();
                    _MostrarAviso = true;
                }
                else if (DateTime.Now.TimeOfDay > _TurnoTardeIni && DateTime.Now.TimeOfDay < _TurnoTardeFin)
                {
                    MostrarMensajeAviso();
                    _MostrarAviso = true;
                }
                else if (DateTime.Now.TimeOfDay > _TurnoNocheIni && DateTime.Now.TimeOfDay < _TurnoNocheFin)
                {
                    MostrarMensajeAviso();
                    _MostrarAviso = true;
                }
                else
                {
                    _MostrarAviso = false;
                }
                Thread.Sleep(MaquinaLinea.TiempoComprobacionAviso*1000);
            }
        }
        
        public void ActualizarTurnos()
        {
            _TurnoMañanaIni = new TimeSpan(Convert.ToInt32(MaquinaLinea.AvisoTurnoMañana.Split('.')[0]), Convert.ToInt32(MaquinaLinea.AvisoTurnoMañana.Split('.')[1]), 0);
            _TurnoMañanaFin = new TimeSpan(Convert.ToInt32(MaquinaLinea.AvisoTurnoMañana.Split('.')[0]), Convert.ToInt32(MaquinaLinea.AvisoTurnoMañana.Split('.')[1])+15, 0);
            _TurnoTardeIni = new TimeSpan(Convert.ToInt32(MaquinaLinea.AvisoTurnoTarde.Split('.')[0]), Convert.ToInt32(MaquinaLinea.AvisoTurnoTarde.Split('.')[1]), 0);
            _TurnoTardeFin = new TimeSpan(Convert.ToInt32(MaquinaLinea.AvisoTurnoTarde.Split('.')[0]), Convert.ToInt32(MaquinaLinea.AvisoTurnoTarde.Split('.')[1])+15, 0);
            _TurnoNocheIni = new TimeSpan(Convert.ToInt32(MaquinaLinea.AvisoTurnoNoche.Split('.')[0]), Convert.ToInt32(MaquinaLinea.AvisoTurnoNoche.Split('.')[1]), 0);
            _TurnoNocheFin = new TimeSpan(Convert.ToInt32(MaquinaLinea.AvisoTurnoNoche.Split('.')[0]), Convert.ToInt32(MaquinaLinea.AvisoTurnoNoche.Split('.')[1])+15, 0);
        }
        //Pasamos siempre t1 como la fecha más antigua que resta a t2
        public static int[] DiferenciaEntreHoras(int[] t1, int[] t2) {
            
            int h=0, m=0, s=0;

            if(t2[2] - t1[2] < 0)
            {
                s = (60 - t1[2]) + t2[2];
                m++;
            }
            else {
                s = t2[2] - t1[2];
            }
            if (t2[1] - t1[1] < 0)
            {
                m = (60 - t1[1]) + t2[1] + m;
                h++;
            }
            else
            {
                m = t2[1] - (t1[1] + m);
            }
            if (t2[0] - t1[0] < 0){
                h = (24 - t1[0]) + t2[0] +h;
            }
            else
            {
                h = t2[0] - (t1[0] + h);
            }
            
            int[] result ={ h, m, s};
            return result;
        }
    }
}

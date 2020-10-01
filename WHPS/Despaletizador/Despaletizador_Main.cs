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
using WHPS.ProgramMenus;
using WHPS.Utiles;

namespace WHPS.Despaletizador
{
    public partial class Despaletizador_Main : Form
    {
        public bool statusboton = false;
        public MainDespaletizador()
        {
            InitializeComponent();
        }

        //Cerramos la aplicación
        private void MainDespaletizador_FormClosing(object sender, FormClosingEventArgs e)
        {
            WHPST_SELECTMAQ Form = new WHPST_SELECTMAQ();
            Hide();
            Form.Show();
        }

        //Volvemos a la pantalla anterior pulsando el boton back
        private void BackB_Click(object sender, EventArgs e)
        {
            Despaletizador_Bot_Auto Form = new Despaletizador_Bot_Auto();
            MainDespaletizador.ActiveForm.Hide();
            Form.Show();
        }

        //Ajuste para cambiar la información del personal
        private void AjustesB_Click(object sender, EventArgs e)
        {
            WHPST_SELECTMAQ Form = new WHPST_SELECTMAQ();
            MainDespaletizador.ActiveForm.Hide();
            Form.Show();
        }

        /*Carga la directamente el responsable de la instalación y reconoce 
          si se ha comprobado el estado la instalación en el cambio de turno.*/
        private void MainDespaletizador_Load(object sender, EventArgs e)
        {
            respTB.Text = MaquinaLinea.MDespaletizador;
            ProgramMenus.Utilidades.ShiftCheck();
        }

        //Abrimos el form del cierre de las botellas        
        private void CierreB_Click(object sender, EventArgs e)
        {
            Despaletizador_Cierres_Manual Form = new Despaletizador_Cierres_Manual();
            MainDespaletizador.ActiveForm.Hide();
            Form.Show();
        }

        //Abrimos el form de las botellas rotas
        private void RotasB_Click(object sender, EventArgs e)
        {
            Despaletizador_RotBotellas Form = new Despaletizador_RotBotellas();
            MainDespaletizador.ActiveForm.Hide();
            Form.Show();
        }

        private void Coment_Click(object sender, EventArgs e)
        {
            Despaletizador_Comentarios Form = new Despaletizador_Comentarios();
            MainDespaletizador.ActiveForm.Hide();
            Form.Show();
        }
    }
}

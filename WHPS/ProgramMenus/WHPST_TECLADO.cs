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

namespace WHPS.ProgramMenus
{
    public partial class WHPST_TECLADO : Form
    {
        //Tenemos 2 modos en los que tenemos que escribir CONTRASEÑA (TRUE) NORMAL (TRUE)
        public bool ModoTeclado = false;
        //Existen varios tipos de taclado en función de las necesidades que tenga el usuario
        //  Tipo 0 ---> Sin botón adicional
        //  Tipo 1 ---> Con botón de "."



        public string TextoTeclado;
        public WHPST_TECLADO()
        {
            InitializeComponent();
        }

        //private void buttonENTER_Click(object sender, EventArgs e)
        //{
        //    if (MaquinaLinea.ModoTeclado == false)
        //    {
        //        MaquinaLinea.Teclado = TecladoTB.Text;
        //    }
        //    if (MaquinaLinea.ModoTeclado == true)
        //    {
        //        MaquinaLinea.Teclado = TextoTeclado;
        //       if  (MaquinaLinea.Teclado == "1877")
        //        {
        //            MaquinaLinea.Password = true;
        //        }
        //        if (MaquinaLinea.Teclado != "1877")
        //        {
        //            MaquinaLinea.Password = false;
        //        }
        //    }
        //    EscribirTeclado();
        //}

        //private void buttonBORRAR_Click(object sender, EventArgs e)
        //{
        //    if(TecladoTB.Text!="") TecladoTB.Text = TecladoTB.Text.Remove(TecladoTB.Text.Length - 1, 1);
        //}

        //private void buttonpoint_Click(object sender, EventArgs e)
        //{
        //    if (MaquinaLinea.ModoTeclado == false)
        //    {
        //        TecladoTB.Text += ".";
        //    }
        //    if (MaquinaLinea.ModoTeclado == true)
        //    {
        //        TecladoTB.Text += "*";
        //        TextoTeclado += ".";
        //    }
        //}

        //private void button0_Click(object sender, EventArgs e)
        //{
        //    if (MaquinaLinea.ModoTeclado == false)
        //    {
        //        TecladoTB.Text += "0";
        //    }
        //    if (MaquinaLinea.ModoTeclado == true)
        //    {
        //        TecladoTB.Text += "*";
        //        TextoTeclado += "0";
        //    }
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    if (MaquinaLinea.ModoTeclado == false)
        //    {
        //        TecladoTB.Text += "1";
        //    }
        //    if (MaquinaLinea.ModoTeclado == true)
        //    {
        //        TecladoTB.Text += "*";
        //        TextoTeclado += "1";
        //    }
        //}
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    if (MaquinaLinea.ModoTeclado == false)
        //    {
        //        TecladoTB.Text += "2";
        //    }
        //    if (MaquinaLinea.ModoTeclado == true)
        //    {
        //        TecladoTB.Text += "*";
        //        TextoTeclado += "2";
        //    }
        //}
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    if (MaquinaLinea.ModoTeclado == false)
        //    {
        //        TecladoTB.Text += "3";
        //    }
        //    if (MaquinaLinea.ModoTeclado == true)
        //    {
        //        TecladoTB.Text += "*";
        //        TextoTeclado += "3";
        //    }
        //}
        //private void button4_Click(object sender, EventArgs e)
        //{
        //    if (MaquinaLinea.ModoTeclado == false)
        //    {
        //        TecladoTB.Text += "4";
        //    }
        //    if (MaquinaLinea.ModoTeclado == true)
        //    {
        //        TecladoTB.Text += "*";
        //        TextoTeclado += "4";
        //    }
        //}
        //private void button5_Click(object sender, EventArgs e)
        //{
        //    if (MaquinaLinea.ModoTeclado == false)
        //    {
        //        TecladoTB.Text += "5";
        //    }
        //    if (MaquinaLinea.ModoTeclado == true)
        //    {
        //        TecladoTB.Text += "*";
        //        TextoTeclado += "5";
        //    }
        //}
        //private void button6_Click(object sender, EventArgs e)
        //{
        //    if (MaquinaLinea.ModoTeclado == false)
        //    {
        //        TecladoTB.Text += "6";
        //    }
        //    if (MaquinaLinea.ModoTeclado == true)
        //    {
        //        TecladoTB.Text += "*";
        //        TextoTeclado += "6";
        //    }
        //}
        //private void button7_Click(object sender, EventArgs e)
        //{
        //    if (MaquinaLinea.ModoTeclado == false)
        //    {
        //        TecladoTB.Text += "7";
        //    }
        //    if (MaquinaLinea.ModoTeclado == true)
        //    {
        //        TecladoTB.Text += "*";
        //        TextoTeclado += "7";
        //    }
        //}
        //private void button8_Click(object sender, EventArgs e)
        //{
        //    if (MaquinaLinea.ModoTeclado == false)
        //    {
        //        TecladoTB.Text += "8";
        //    }
        //    if (MaquinaLinea.ModoTeclado == true)
        //    {
        //        TecladoTB.Text += "*";
        //        TextoTeclado += "8";
        //    }
        //}
        //private void button9_Click(object sender, EventArgs e)
        //{
        //    if (MaquinaLinea.ModoTeclado == false)
        //    {
        //        TecladoTB.Text += "9";
        //    }
        //    if (MaquinaLinea.ModoTeclado == true)
        //    {
        //        TecladoTB.Text += "*";
        //        TextoTeclado += "9";
        //    }
        //}



        ///// <summary>
        ///// Función que escribirá en el TB seleccionado
        ///// </summary>
        ///// <param name="Form">Form indica que en que pantalla tiene que escribir la función</param>
        ///// <returns>Devuelve TRUE si la contraseña es correcta y fale si no lo es</returns>
        //public void EscribirTeclado()
        //{
        //    //Form del Despaletizador
        //    if (MaquinaLinea.Form == "Despaletizador_RotBotellas")
        //    {
        //        switch (MaquinaLinea.TextBox)
        //        {
        //            case "numrotasTB":

        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.NumRotasDespL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.NumRotasDespL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5)Properties.Settings.Default.NumRotasDespL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Despaletizador.Despaletizador_RotBotellas Form = new Despaletizador.Despaletizador_RotBotellas();
        //                Form.numrotasTB.Text = MaquinaLinea.Teclado;
        //                Form.ShowDialog();
        //                break;
        //            case "ConfrRespB":
        //                Hide();
        //                Owner.Hide();
        //                Despaletizador.Despaletizador_RotBotellas Form1 = new Despaletizador.Despaletizador_RotBotellas();
        //                if (MaquinaLinea.Password == true) Form1.ConfrRespB.BackColor = Color.DarkSeaGreen;
        //                if (MaquinaLinea.Password == false)Form1.ConfrRespB.BackColor = Color.IndianRed;
        //                Form1.ShowDialog();
        //                break;
        //        }
        //    }
        //    if (MaquinaLinea.Form == "Despaletizador_Botellas")
        //    {
        //        Hide();
        //        Owner.Hide();
        //        Despaletizador.Despaletizador_Botellas Form = new Despaletizador.Despaletizador_Botellas();
        //        Form.InputTB.Text = MaquinaLinea.Teclado;
        //        Form.ShowDialog();
        //    }
        //    if (MaquinaLinea.Form == "Despaletizador_Cierres")
        //    {
        //        Hide();
        //        Owner.Hide();
        //        Despaletizador.Despaletizador_Cierres Form = new Despaletizador.Despaletizador_Cierres();
        //        Form.InputTB.Text = MaquinaLinea.Teclado;
        //        Form.ShowDialog();
        //    }
        //    //Form de la Llenadora
        //    if (MaquinaLinea.Form == "Llenadora_RotBotellas")
        //    {
        //        switch (MaquinaLinea.TextBox)
        //        {
        //            case "numrotasTB":

        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.NumRotasLlenL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.NumRotasLlenL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5)Properties.Settings.Default.NumRotasLlenL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_RotBotellas Form = new Llenadora.Llenadora_RotBotellas();
        //                Form.numrotasTB.Text = MaquinaLinea.Teclado;
        //                Form.ShowDialog();
        //                break;
        //            case "ConfrRespB":
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_RotBotellas Form1 = new Llenadora.Llenadora_RotBotellas();
        //                if (MaquinaLinea.Password == true) Form1.ConfrRespB.BackColor = Color.DarkSeaGreen;
        //                if (MaquinaLinea.Password == false) Form1.ConfrRespB.BackColor = Color.IndianRed;
        //                Form1.ShowDialog();
        //                break;
        //        }
        //    }
        //    if (MaquinaLinea.Form == "Llenadora_Control_Presion")
        //    {
        //        switch (MaquinaLinea.TextBox)
        //        {
        //            case "PresionTB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.MedidaPresionL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.MedidaPresionL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.MedidaPresionL3 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Control_Presion Form = new Llenadora.Llenadora_Control_Presion();
        //                Form.PresionTB.Text = MaquinaLinea.Teclado;
        //                Form.ShowDialog();
        //                break;
        //        }
        //    }
        //    if (MaquinaLinea.Form == "Llenadora_Control_Temperatura")
        //    {
        //        switch (MaquinaLinea.TextBox)
        //        {
        //            case "TemperaturaInicioTB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.TemperaturaInicioL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.TemperaturaInicioL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.TemperaturaInicioL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Control_Temperatura Form = new Llenadora.Llenadora_Control_Temperatura();
        //                Form.TemperaturaInicioTB.Text = MaquinaLinea.Teclado;
        //                Form.ShowDialog();
        //                break;
        //            case "TemperaturaFinTB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.TemperaturaFinL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.TemperaturaFinL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.TemperaturaFinL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Control_Temperatura Form1 = new Llenadora.Llenadora_Control_Temperatura();
        //                Form1.TemperaturaFinTB.Text = MaquinaLinea.Teclado;
        //                Form1.ShowDialog();
        //                break;
        //        }

        //    }
        //    if (MaquinaLinea.Form == "Llenadora_Control_Volumen")
        //    {
        //        switch (MaquinaLinea.TextBox)
        //        {
        //            case "TemperaturaTB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.TemperaturaContVolLlenL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.TemperaturaContVolLlenL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.TemperaturaContVolLlenL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Control_Volumen Form = new Llenadora.Llenadora_Control_Volumen();
        //                Form.TemperaturaTB.Text = MaquinaLinea.Teclado;
        //                Form.ShowDialog();
        //                break;
        //            case "VolumenTB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.VolumenContVolLlenL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.VolumenContVolLlenL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.VolumenContVolLlenL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Control_Volumen Form1 = new Llenadora.Llenadora_Control_Volumen();
        //                Form1.VolumenTB.Text = MaquinaLinea.Teclado;
        //                Form1.ShowDialog();
        //                break;
        //        }
        //    }
        //    if (MaquinaLinea.Form == "Llenadora_Torquimetro")
        //    {
        //        switch (MaquinaLinea.TextBox)
        //        {
        //            case "C1TB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.TORQ_Cab1LlenL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.TORQ_Cab1LlenL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.TORQ_Cab1LlenL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Torquimetro Form1 = new Llenadora.Llenadora_Torquimetro();
        //                Form1.C1TB.Text = MaquinaLinea.Teclado;
        //                Form1.ShowDialog();
        //                break;
        //            case "C2TB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.TORQ_Cab2LlenL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.TORQ_Cab2LlenL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.TORQ_Cab2LlenL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Torquimetro Form2 = new Llenadora.Llenadora_Torquimetro();
        //                Form2.C2TB.Text = MaquinaLinea.Teclado;
        //                Form2.ShowDialog();
        //                break;
        //            case "C3TB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.TORQ_Cab3LlenL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.TORQ_Cab3LlenL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.TORQ_Cab3LlenL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Torquimetro Form3 = new Llenadora.Llenadora_Torquimetro();
        //                Form3.C3TB.Text = MaquinaLinea.Teclado;
        //                Form3.ShowDialog();
        //                break;
        //            case "C4TB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.TORQ_Cab4LlenL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.TORQ_Cab4LlenL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.TORQ_Cab4LlenL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Torquimetro Form4 = new Llenadora.Llenadora_Torquimetro();
        //                Form4.C4TB.Text = MaquinaLinea.Teclado;
        //                Form4.ShowDialog();
        //                break;
        //            case "C5TB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.TORQ_Cab5LlenL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.TORQ_Cab5LlenL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.TORQ_Cab5LlenL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Torquimetro Form5 = new Llenadora.Llenadora_Torquimetro();
        //                Form5.C5TB.Text = MaquinaLinea.Teclado;
        //                Form5.ShowDialog();
        //                break;
        //            case "C6TB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.TORQ_Cab6LlenL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.TORQ_Cab6LlenL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.TORQ_Cab6LlenL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Torquimetro Form6 = new Llenadora.Llenadora_Torquimetro();
        //                Form6.C6TB.Text = MaquinaLinea.Teclado;
        //                Form6.ShowDialog();
        //                break;
        //            case "C7TB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.TORQ_Cab7LlenL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.TORQ_Cab7LlenL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.TORQ_Cab7LlenL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Torquimetro Form7 = new Llenadora.Llenadora_Torquimetro();
        //                Form7.C7TB.Text = MaquinaLinea.Teclado;
        //                Form7.ShowDialog();
        //                break;
        //            case "C8TB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.TORQ_Cab8LlenL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.TORQ_Cab8LlenL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.TORQ_Cab8LlenL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Torquimetro Form8 = new Llenadora.Llenadora_Torquimetro();
        //                Form8.C8TB.Text = MaquinaLinea.Teclado;
        //                Form8.ShowDialog();
        //                break;
        //            case "C9TB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.TORQ_Cab9LlenL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.TORQ_Cab9LlenL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.TORQ_Cab9LlenL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Torquimetro Form9 = new Llenadora.Llenadora_Torquimetro();
        //                Form9.C9TB.Text = MaquinaLinea.Teclado;
        //                Form9.ShowDialog();
        //                break;
        //            case "C10TB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.TORQ_Cab10LlenL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.TORQ_Cab10LlenL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.TORQ_Cab10LlenL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Torquimetro Form10 = new Llenadora.Llenadora_Torquimetro();
        //                Form10.C10TB.Text = MaquinaLinea.Teclado;
        //                Form10.ShowDialog();
        //                break;
        //            case "C11TB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.TORQ_Cab11LlenL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.TORQ_Cab11LlenL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.TORQ_Cab11LlenL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Torquimetro Form11 = new Llenadora.Llenadora_Torquimetro();
        //                Form11.C11TB.Text = MaquinaLinea.Teclado;
        //                Form11.ShowDialog();
        //                break;
        //            case "C12TB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.TORQ_Cab12LlenL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.TORQ_Cab12LlenL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.TORQ_Cab12LlenL5 = MaquinaLinea.Teclado;
        //                Hide();
        //                Owner.Hide();
        //                Llenadora.Llenadora_Torquimetro Form12 = new Llenadora.Llenadora_Torquimetro();
        //                Form12.C12TB.Text = MaquinaLinea.Teclado;
        //                Form12.ShowDialog();
        //                break;
        //        }
        //    }
        //    //Form de la Etiquetadora
        //    if (MaquinaLinea.Form == "Etiquetadora_RotBotellas")
        //    {
        //        switch (MaquinaLinea.TextBox)
        //        {
        //            case "numrotasTB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.NumRotasEtiqL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.NumRotasEtiqL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.NumRotasEtiqL5 = MaquinaLinea.Teclado;

        //                Hide();
        //                Owner.Hide();
        //                Etiquetadora.Etiquetadora_RotBotellas Form = new Etiquetadora.Etiquetadora_RotBotellas();
        //                Form.numrotasTB.Text = MaquinaLinea.Teclado;
        //                Form.ShowDialog();
        //                break;
        //            case "ConfrRespB":
        //                Hide();
        //                Owner.Hide();
        //                Etiquetadora.Etiquetadora_RotBotellas Form1 = new Etiquetadora.Etiquetadora_RotBotellas();
        //                if (MaquinaLinea.Password == true) Form1.ConfrRespB.BackColor = Color.DarkSeaGreen;
        //                if (MaquinaLinea.Password == false) Form1.ConfrRespB.BackColor = Color.IndianRed;
        //                Form1.ShowDialog();
        //                break;
        //        }
        //    }
        //    //Form de la Encajonadora
        //    if (MaquinaLinea.Form == "Encajonadora_RotBotellas")
        //    {
        //        switch (MaquinaLinea.TextBox)
        //        {
        //            case "numrotasTB":
        //                if (MaquinaLinea.numlin == 2) Properties.Settings.Default.NumRotasEncL2 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 3) Properties.Settings.Default.NumRotasEncL3 = MaquinaLinea.Teclado;
        //                if (MaquinaLinea.numlin == 5) Properties.Settings.Default.NumRotasEncL5 = MaquinaLinea.Teclado;

        //                Hide();
        //                Owner.Hide();
        //                Encajonadora.Encajonadora_RotBotellas Form = new Encajonadora.Encajonadora_RotBotellas();
        //                Form.numrotasTB.Text = MaquinaLinea.Teclado;
        //                Form.ShowDialog();
        //                break;
        //            case "ConfrRespB":
        //                Hide();
        //                Owner.Hide();
        //                Encajonadora.Encajonadora_RotBotellas Form1 = new Encajonadora.Encajonadora_RotBotellas();
        //                if (MaquinaLinea.Password == true) Form1.ConfrRespB.BackColor = Color.DarkSeaGreen;
        //                if (MaquinaLinea.Password == false) Form1.ConfrRespB.BackColor = Color.IndianRed;
        //                Form1.ShowDialog();
        //                break;
        //        }
        //    }


        //    Properties.Settings.Default.Save();
        //}
        //private void WHPST_TECLADO_Load(object sender, EventArgs e)
        //{
        //    if (MaquinaLinea.TipoTeclado == 0) buttonpoint.Visible = false;
        //    if (MaquinaLinea.TipoTeclado == 1) buttonpoint.Visible = true;
        //}
    }
}

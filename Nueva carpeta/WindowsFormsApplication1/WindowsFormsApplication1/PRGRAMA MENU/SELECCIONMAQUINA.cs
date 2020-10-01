using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.PRGRAMA_MENU
{
    public partial class SELECCIONMAQUINA : Form
    {
        public SELECCIONMAQUINA()
        {
            InitializeComponent();
        }

        //Cerramos la aplicacion
        private void SELECCIONMAQUINA_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        //Volvemos a la pantalla anterior
        private void BATRAS_Click(object sender, EventArgs e)
        {
            MENU FormAtras = new MENU();
            SELECCIONMAQUINA.ActiveForm.Hide();
            FormAtras.Show();
        }


        private void SELECCIONMAQUINA_Load(object sender, EventArgs e)
        {
            numero_linea.Text = PRGRAMA_MENU.m.numero_linea.ToString();
            turnoTB.Text = Globals.turno;
            FUNCIONES.ACTUALIZAR_TURNO.ACTUALIZARTURNO();
            //###### Precargamos la variable del fichero a grabar ##############
            Globals.FileDespaletizador = "Desp_L" + Globals.numero_linea.ToString();
        }

        //Abrimos el despaletizador
        private void BDESPELETIZADOR_Click(object sender, EventArgs e)
        {
            SELECCIONMAQUINA FormMenu_Despaletizador = new SELECCIONMAQUINA();
            DESPALETIZADOR.MENU_DESPALETIZADOR.ActiveForm.Hide();
            FormMenu_Despaletizador.Show();
        }



        //Falta por introducir los botones de las distitas maquinas  
        //Falta por introducir el boton de ajuste

        //Cargamos en la caja agrupada la informacion de linea
        private void Carga_Personal()
        {
            //Generamos la lista de valores a filtrar y traer desde la lista de materiales AND, 'Columna', LIKE, 'Codigo Material'
            List<string[]> listavalores = new List<string[]>();
            //string[] valores = new string[4];
            //valores[0] = "AND";
            //valores[1] = "Codigo";
            //valores[2] = "LIKE";
            //valores[3] = #######VALOR A FILTRAR#####;
            //listavalores.Add(valores);

            //Llamamos la busqueda del fichero excel
            //Rellenamos el turno - Identificando el turno
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
            if ((Turno != Globals.turno) || (diaC != Globals.diaT) || ((numlinTB.Text == "2") && (!Globals.checkL2)) || ((numlinTB.Text == "3") && (!Globals.checkL3)) || ((numlinTB.Text == "5") && (!Globals.checkL5)))
            {

                ProgramMenus.WHPST_Cambio_Turno Form2 = new ProgramMenus.WHPST_Cambio_Turno();
                Form ActualF = Form.ActiveForm;
                ActualF.Hide();
                Form2.Show();



                FUNCIONES.ACTUALIZAR_TURNO.ACTUALIZARTURNO();

                string result;
                DataSet excelDataSet = new DataSet();
                excelDataSet = ExcelUtiles.LeerFicheroExcel("Datos_Lineas", "L" + numlinTB.Text, (Globals.turno).Split(';'), listavalores, out result);
                //MessageBox.Show(result);
                string Texto = "";
                if (excelDataSet.Tables[0].Rows.Count > 0)
                {
                    Texto = Convert.ToString(excelDataSet.Tables[0].Rows[0][Globals.turno]);
                    respTB.Text = Texto;
                    Globals.Responsable = Texto;
                    Texto = Convert.ToString(excelDataSet.Tables[0].Rows[1][Globals.turno]);
                    DespTB.Text = Texto;
                    Globals.MDespaletizador = Texto;
                    Texto = Convert.ToString(excelDataSet.Tables[0].Rows[2][Globals.turno]);
                    LlenTB.Text = Texto;
                    Globals.MLlenadora = Texto;
                    Texto = Convert.ToString(excelDataSet.Tables[0].Rows[3][Globals.turno]);
                    EtiqTB.Text = Texto;
                    Globals.MEtiquetadora = Texto;
                    Texto = Convert.ToString(excelDataSet.Tables[0].Rows[4][Globals.turno]);
                    EncTB.Text = Texto;
                    Globals.MEncajonadora = Texto;
                    Texto = Convert.ToString(excelDataSet.Tables[0].Rows[5][Globals.turno]);
                    ContTB.Text = Texto;
                    Globals.ControlCal = Texto;
                }
                else
                {
                    MessageBox.Show("Error en la carga del fichero");

                }
            }





        }
        }
    
}

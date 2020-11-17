using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WHPS.Utiles;
using System.Diagnostics;
using System.IO;
using WHPS.Model;
using System.Threading;

namespace WHPS.ProgramMenus
{
    public partial class WHPST_Cambio_Turno : Form
    {
        public string Puntero = "";

        private bool cambio_responsable = false;
        private bool cambio_despaletizador = false;
        private bool cambio_etiquetadora = false;
        private bool cambio_encajonadora = false;
        private bool cambio_llenadora = false;
        private bool cambio_control_calidad = false;
        private bool cambio_turno = false;

        WHPST_INICIO parent;
        public WHPST_Cambio_Turno(WHPST_INICIO p)
        {
            InitializeComponent();
            parent = p;
        }

        //Cagarmos los datos conocidos
        public void WHPST_Cambio_Turno_Load(object sender, EventArgs e)
        {
            //Rellenanmos los parametro ya conocidos
            numlinTB.Text = MaquinaLinea.numlin.ToString();
            TurnoCB.Text = CambioTurno.ObtenerTurnoActual();

            //Obtenemos los valores de datos líneas
            CambioTurno.Obtener_Personal_Datos_Lineas();

            //Rellenamos los valores obtenidos
            respTB.Text = MaquinaLinea.Responsable;
            DespTB.Text = MaquinaLinea.MDespaletizador;
            LlenTB.Text = MaquinaLinea.MLlenadora;
            EtiqTB.Text = MaquinaLinea.MEtiquetadora;
            EncTB.Text = MaquinaLinea.MEncajonadora;
            ContTB.Text = MaquinaLinea.ControlCal;
        }

        //Activamos un TEXTBOX determinado para modificar su nombre
        private void respTB_MouseDown(object sender, MouseEventArgs e)
        {
            Puntero = "Responsable";
        }
        private void DespTB_MouseDown(object sender, MouseEventArgs e)
        {
            Puntero = "Despaletizador";
        }
        private void LlenTB_MouseDown(object sender, MouseEventArgs e)
        {
            Puntero = "Llenadora";
        }
        private void EtiqTB_MouseDown(object sender, MouseEventArgs e)
        {
            Puntero = "Etiquetadora";
        }
        private void EncTB_MouseDown(object sender, MouseEventArgs e)
        {
            Puntero = "Encajadora";
        }
        private void ContTB_MouseDown(object sender, MouseEventArgs e)
        {
            Puntero = "Control";
        }

        //Cambiamos el nombre del TEXTBOX activo e indiacamos que asi ha sido
        private void PersonalListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            cambio_responsable = (ResponsableListBox.SelectedItem.ToString() != respTB.Text) ? true : false;
            respTB.Text = ResponsableListBox.SelectedItem.ToString();
        }
        private void MaquinistaListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Puntero)
            {
                case "Despaletizador":
                    cambio_despaletizador = (MaquinistaListBox.SelectedItem.ToString() != DespTB.Text) ? true : false;
                    DespTB.Text = MaquinistaListBox.SelectedItem.ToString();

                    break;
                case "Llenadora":
                    cambio_llenadora = (MaquinistaListBox.SelectedItem.ToString() != LlenTB.Text) ? true : false;
                    LlenTB.Text = MaquinistaListBox.SelectedItem.ToString();

                    break;
                case "Etiquetadora":
                    cambio_etiquetadora = (MaquinistaListBox.SelectedItem.ToString() != EtiqTB.Text) ? true : false;
                    EtiqTB.Text = MaquinistaListBox.SelectedItem.ToString();
                    break;
                case "Encajadora":
                    cambio_encajonadora = (MaquinistaListBox.SelectedItem.ToString() != EncTB.Text) ? true : false;
                    EncTB.Text = MaquinistaListBox.SelectedItem.ToString();
                    break;
                case "Control":
                    cambio_control_calidad = (MaquinistaListBox.SelectedItem.ToString() != ContTB.Text) ? true : false;
                    ContTB.Text = MaquinistaListBox.SelectedItem.ToString();
                    break;
            }
        }
        private void EventualesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Puntero)
            {
                case "Despaletizador":
                    cambio_despaletizador = (EventualesListBox.SelectedItem.ToString() != DespTB.Text) ? true : false;
                    DespTB.Text = EventualesListBox.SelectedItem.ToString();
                    break;
                case "Llenadora":
                    cambio_llenadora = (EventualesListBox.SelectedItem.ToString() != LlenTB.Text) ? true : false;
                    LlenTB.Text = EventualesListBox.SelectedItem.ToString();
                    break;
                case "Etiquetadora":
                    cambio_etiquetadora = (EventualesListBox.SelectedItem.ToString() != EtiqTB.Text) ? true : false;
                    EtiqTB.Text = EventualesListBox.SelectedItem.ToString();
                    break;
                case "Encajadora":
                    cambio_encajonadora = (EventualesListBox.SelectedItem.ToString() != EncTB.Text) ? true : false;
                    EncTB.Text = EventualesListBox.SelectedItem.ToString();
                    break;
                case "Control":
                    cambio_control_calidad = (EventualesListBox.SelectedItem.ToString() != ContTB.Text) ? true : false;
                    ContTB.Text = EventualesListBox.SelectedItem.ToString();
                    break;
            }
        }
        private void TurnoCB_TextChanged(object sender, EventArgs e)
        {
            MaquinaLinea.turno = TurnoCB.Text;
            //Obtenemos el personal que esta registrado en la correspondiente línea.
            CambioTurno.Obtener_Personal_Datos_Lineas();

            //Completamos el personal
            respTB.Text = MaquinaLinea.Responsable;
            DespTB.Text = MaquinaLinea.MDespaletizador;
            LlenTB.Text = MaquinaLinea.MLlenadora;
            EtiqTB.Text = MaquinaLinea.MEtiquetadora;
            EncTB.Text = MaquinaLinea.MEncajonadora;
            ContTB.Text = MaquinaLinea.ControlCal;

            cambio_turno = true;
        }

        //Guardamos la información
        private void saveBot_Click(object sender, EventArgs e)
        {
            //Si en algun momento se ha realizado algun cambio este se accutalizará en el excel.
            if (cambio_responsable || cambio_llenadora || cambio_control_calidad || cambio_despaletizador || cambio_encajonadora || cambio_etiquetadora || cambio_turno)
            {
                //######################################################################################################################################################### UTILIZAR UNA FUNCION QUE ACTUALIZE EL EXCEL
                //Actualiamos el excel de Datos Linea.
                ActualizarDatos_Lienas();

                //Actualizamos el cambio de turno con el seleccionado.
                MaquinaLinea.turno = TurnoCB.Text;

                //Actualizamos las variables de personal.
                CambioTurno.Obtener_Personal_Datos_Lineas();

                //Actualiamos las variables de checklinea y checkMaquina.
                CambioTurno.ActualizarYGuardarValores();
            }
            

            //Cerramos y vamos selección de máquina
            MaquinaLinea.VolverInicioA = (MaquinaLinea.numlin == 2) ? RetornoInicio.L2 : RetornoInicio.L5;
            MaquinaLinea.VolverInicioA = (MaquinaLinea.numlin == 3) ? RetornoInicio.L3 : MaquinaLinea.VolverInicioA;
            Utilidades.AbrirForm(parent, parent, typeof(WHPST_INICIO));
        }




        private void ActualizarDatos_Lienas()
        {
            List<string[]> valoresAFiltrar = new List<string[]>();
            string[] filterval = new string[4];
            filterval[0] = "AND";
            filterval[1] = "Puesto";
            filterval[2] = "LIKE";
            filterval[3] = "";
            List<string[]> valoresAActualizar = new List<string[]>();
            string[] updateval = new string[2];
            updateval[0] = TurnoCB.Text;
            updateval[1] = "";
            valoresAFiltrar.Add(filterval);
            valoresAActualizar.Add(updateval);

            string salida;

            if (cambio_responsable)
            {
                //Filtro Encargado
                filterval[3] = "\"Encargado\"";
                updateval[1] = respTB.Text;

                salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + MaquinaLinea.numlin.ToString(), valoresAActualizar, valoresAFiltrar);
                //MessageBox.Show(salida);
                cambio_responsable = false;
            }

            //### DESPALETIZADOR #####
            if (cambio_despaletizador)
            {
                filterval[3] = "\"Despaletizador\"";
                updateval[1] = DespTB.Text;
                salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + MaquinaLinea.numlin.ToString(), valoresAActualizar, valoresAFiltrar);
                //MessageBox.Show(salida);
                cambio_despaletizador = false;
            }
            //### Llenadora #####
            if (cambio_llenadora)
            {
                filterval[3] = "\"Llenadora\"";
                updateval[1] = LlenTB.Text;
                salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + MaquinaLinea.numlin.ToString(), valoresAActualizar, valoresAFiltrar);
                //MessageBox.Show(salida);
                cambio_llenadora = false;
            }
            //### Etiquetadora #####
            if (cambio_etiquetadora)
            {
                filterval[3] = "\"Etiquetadora\"";
                updateval[1] = EtiqTB.Text;
                salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + MaquinaLinea.numlin.ToString(), valoresAActualizar, valoresAFiltrar);
                //MessageBox.Show(salida);
                cambio_etiquetadora = false;
            }
            //### Encajadora #####
            if (cambio_encajonadora)
            {
                filterval[3] = "\"Encajadora\"";
                updateval[1] = EncTB.Text;
                salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + MaquinaLinea.numlin.ToString(), valoresAActualizar, valoresAFiltrar);
                // MessageBox.Show(salida);
                cambio_encajonadora = false;
            }
            //### Control Calidad #####
            if (cambio_control_calidad)
            {
                filterval[3] = "\"Control\"";
                updateval[1] = ContTB.Text;
                salida = ExcelUtiles.ActualizarFicheroExcel("Datos_Lineas", "L" + MaquinaLinea.numlin.ToString(), valoresAActualizar, valoresAFiltrar);
                //MessageBox.Show(salida);
                cambio_control_calidad = false;
            }
        }
    }
}

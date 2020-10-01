using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHPS.Model;
using WHPS.Utiles;

namespace WHPS.Rotura
{
    public partial class Rotura_Enc : Form
    {
        public string[] Rotura = new string[7];
        public string[] ArraySintetizada = new string[7];
        public string[] Descripcion = new string[50];
        public string[] Proveedor = new string[50];
        public string[] NRotas = new string[50];
        public int[] Turno = new int[3];
        public bool registro = false;
        public int contadorDescripiones = 0;
        public string BotellaMax = "";
        public string Comparador = "";
        public string NumBotellaMax = "";
        public string ProveedorMax = "";
        public string DiaAnterior = "";
        public string DiaActual = "";
        public bool Dato1 = false;
        public int j = 0;
        public Rotura_Enc()
        {
            InitializeComponent();
        }

        private void Rotura_Enc_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                Turno[i] = 0;
            }
            Dato1 = false;
            try
            {
                //dataGridViewInicio.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[0, 0]];
                //dataGridViewRegistro.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[1, 0]];
                //dataGridViewParo.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[2, 0]];
                dataGridViewRoturas.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[3, 0]];
                //dataGridViewComentarios.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[4, 0]];
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }

                //dataGridViewInicio.Columns["Fecha"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                //dataGridViewInicio.Columns["Maquinista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //dataGridViewRegistro.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //dataGridViewParo.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //dataGridViewComentarios.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (MaquinaLinea.Rotura_Guardar == true)
                {
                    AvisoCargaLB.Visible = true;
                    CompletarRoturaEncajadora();
                    AvisoCargaLB.Visible = false;
                    MaquinaLinea.Rotura_Enc = true;
                }
            }
        public void CompletarRoturaEncajadora()
        {
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "-" });
                listavalores.Add(new string[2] { "B", "ROTURA DE BOTELLAS" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileRotura, "Encajadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### CABECERA DATOS ROTURAS ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", "Fecha" });
                listavalores.Add(new string[2] { "B", "Turno" });
                listavalores.Add(new string[2] { "C", "Rotura de Bot." });
                listavalores.Add(new string[2] { "D", "Número de Roturas" });
                listavalores.Add(new string[2] { "E", "Proveedor" });
                listavalores.Add(new string[2] { "F", "Descripcion" });
                listavalores.Add(new string[2] { "G", "Maquina" });
                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileRotura, "Encajadora", listavalores, "Id");

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            //########### OBTENER DATOS ROTURAS ##############
            for (int j = 0; j < (dataGridViewRoturas.RowCount - 1); j++)
            {
                Rotura = new string[7];
                for (int i = 0; i < 7; i++)
                {
                    Rotura[i] = dataGridViewRoturas.Rows[j].Cells[i].Value.ToString();
                }
                if (Rotura[2] == "SI") ProcesamientoDatos(Rotura);
            }
            for (int i = 0; i < 7; i++)
            {
                Rotura[i] = dataGridViewRoturas.Rows[j].Cells[i].Value.ToString();
            }
            if (Rotura[2] == "SI") ProcesamientoDatos(Rotura);
        }

        public void DatosRotura()
        {
            //########### DATOS ROTURAS ##############
            try
            {
                List<string[]> listavalores = new List<string[]>();
                listavalores.Add(new string[2] { "A", ArraySintetizada[0] });
                listavalores.Add(new string[2] { "B", ArraySintetizada[1] });
                listavalores.Add(new string[2] { "C", ArraySintetizada[2] });
                listavalores.Add(new string[2] { "D", ArraySintetizada[3] });
                listavalores.Add(new string[2] { "E", ArraySintetizada[4] });
                listavalores.Add(new string[2] { "F", ArraySintetizada[5] });
                listavalores.Add(new string[2] { "G", ArraySintetizada[6] });

                string salida = ExcelUtiles.EscribirFicheroExcel(MaquinaLinea.FileRotura, "Encajadora", listavalores, "Id");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        public void ProcesamientoDatos(string[] Rotura)
        {
            string DiaActual = Rotura[0];
            if (DiaActual == DiaAnterior)
            {
                SinstsisDatos(Rotura);
            }
            else
            {
                if (Dato1 == false)
                {
                    SinstsisDatos(Rotura);
                }
                else
                {
                    Comparador = ArraySintetizada[0];
                    if (Comparador != DiaActual)
                    {
                        EnviaDatos();
                        for (int i = 0; i < 3; i++)
                        {
                            Turno[i] = 0;
                        }

                        SinstsisDatos(Rotura);
                    }
                    else SinstsisDatos(Rotura);
                }
            }
            DiaAnterior = DiaActual;
        }





        public void EnviaDatos()
        {
            DatosRotura();
        }
        public void SinstsisDatos(string[] Rotura)
        {
            //Es el primer dato 
            if (Dato1 == false)
            {
                //SINTETIZO LA ARRAY ROTURA
                for (int i = 0; i < 7; i++)
                {
                    //OBTENER MAYOR PROVEEDOR
                    if (i == 5)
                    {
                        Descripcion[0] = Rotura[i];
                        BotellaMax = Rotura[i];
                        Proveedor[0] = Rotura[i - 1];
                        ProveedorMax = Rotura[i - 1];
                        NRotas[0] = Rotura[i - 2];
                        NumBotellaMax = Rotura[i - 2];
                        contadorDescripiones = 1;
                    }

                    //OBTENER NUMERO DE TURNOS
                    if (i == 1)
                    {
                        switch (Rotura[i])
                        {
                            case "Mañana":
                                Turno[0] = 1;
                                break;
                            case "Tarde":
                                Turno[1] = 1;
                                break;
                            case "Noche":
                                Turno[2] = 1;
                                break;
                        }
                        ArraySintetizada[i] = "1";
                    }
                    if (i != 1) ArraySintetizada[i] = Rotura[i];

                }
                Dato1 = true;
            }
            //Si no es el primer dato es que es el mismo día y ya tenemos una array sintetizada
            else
            {
                DiaActual = Rotura[0];
                if (DiaActual == DiaAnterior)
                {
                    //SINTETIZO INCREMENTO LA ARRAY SINTETIZADA CON EL VECTOR ROTURA
                    for (int i = 0; i < 8; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                //El dia es el mismo 
                                ArraySintetizada[i] = Rotura[i];

                                break;
                            case 1:
                                //El turno puede incrementar 
                                switch (Rotura[i])
                                {

                                    case "Mañana":
                                        Turno[0] = 1;
                                        break;
                                    case "Tarde":
                                        Turno[1] = 1;
                                        break;
                                    case "Noche":
                                        Turno[2] = 1;
                                        break;
                                }
                                ArraySintetizada[i] = Convert.ToString(Turno[0] + Turno[1] + Turno[2]);
                                break;
                            case 2:
                                //SOLO SE PROCESAN LOS DATOS SI SE ROMPEN BOTELLAS
                                ArraySintetizada[i] = Rotura[i];


                                break;
                            case 3:
                                //INCREMENTAMOS EL NÚMERO DE ROTURAS
                                long ACUMULADO = Convert.ToInt64(ArraySintetizada[i]);
                                ACUMULADO += Convert.ToInt64(Rotura[i]);
                                ArraySintetizada[i] = Convert.ToString(ACUMULADO);

                                break;
                            case 4:
                                //EL PROVEEDOR SE COMPLETARÁ CUANDO SE DETECTE CUAL HA SIDO LA BOTELLA QUE MAS SE HA ROTO

                                break;
                            case 5:
                                int k = 0;
                                int p = 0;
                                for (k = 0; k < contadorDescripiones; k++)
                                {
                                    if (Descripcion[k] == Rotura[i]) registro = false;
                                    else registro = true; p = k;
                                }

                                if (registro)
                                {
                                    //SI NO ESTA EN EL REGISTRO, TENEMOS  QUE GUARDARLO EN EL ULTIMO LUGAR
                                    contadorDescripiones += 1;
                                    Descripcion[contadorDescripiones] = Rotura[i];
                                    Proveedor[contadorDescripiones] = Rotura[i - 1];
                                    NRotas[contadorDescripiones] = Rotura[i - 2];
                                    if (Convert.ToInt64(NRotas[contadorDescripiones]) >= Convert.ToInt64(NumBotellaMax))
                                    {
                                        //CAMBIAMOS LA DESCRIPCIÓN
                                        NumBotellaMax = Rotura[i - 2];
                                        ProveedorMax = Rotura[i - 1];
                                        BotellaMax = Rotura[i];
                                    }
                                }
                                else
                                {

                                    //SI YA TENEMOS EL REGISTRO TENEMOS QUE INCREMENTAR EL NUMERO DE BOTELLAS DE ESTE
                                    NRotas[p] = Convert.ToString(Convert.ToInt64(NRotas[p]) + Convert.ToInt64(Rotura[i - 2]));

                                    if (Convert.ToInt64(NRotas[p]) >= Convert.ToInt64(NumBotellaMax))
                                    {
                                        //CAMBIAMOS LA DESCRIPCIÓN
                                        NumBotellaMax = NRotas[p];
                                        ProveedorMax = Proveedor[p];
                                        BotellaMax = Descripcion[p];
                                    }
                                }

                                ArraySintetizada[i] = BotellaMax;
                                ArraySintetizada[i - 1] = ProveedorMax;
                                break;
                            case 6:
                                //LA MAQUINA NO CAMBIA
                                ArraySintetizada[i] = Rotura[i];
                                break;
                        }
                    }
                }
                else
                {
                    //SINTETIZO LA ARRAY NUEVA
                    for (int i = 0; i < 7; i++)
                    {
                        //OBTENER MAYOR PROVEEDOR
                        if (i == 5)
                        {
                            Descripcion[0] = Rotura[i];
                            BotellaMax = Rotura[i];
                            Proveedor[0] = Rotura[i - 1];
                            ProveedorMax = Rotura[i - 1];
                            NRotas[0] = Rotura[i - 2];
                            NumBotellaMax = Rotura[i - 2];
                            contadorDescripiones = 1;
                        }

                        //OBTENER NUMERO DE TURNOS
                        if (i == 1)
                        {
                            switch (Rotura[i])
                            {
                                case "Mañana":
                                    Turno[0] = 1;
                                    break;
                                case "Tarde":
                                    Turno[1] = 1;
                                    break;
                                case "Noche":
                                    Turno[2] = 1;
                                    break;
                            }
                            ArraySintetizada[i] = "1";
                        }
                        if (i != 1) ArraySintetizada[i] = Rotura[i];

                    }
                }
            }
        }
    }
}

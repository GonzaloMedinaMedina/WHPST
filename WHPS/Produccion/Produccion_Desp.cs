using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using WHPS.Utiles;
using WHPS.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace WHPS.Produccion
{

    public partial class Produccion_Desp : Form
    {
        public string Linea;
        public string Dia;
        public string Lote;
        public string Turno;
        public string ColumnasFiltro;
        public int Puntero;
        public int Filtro;
        public int suma = 0;
        public string formato;
        public int cajas = 0;
        public DateTime DiaInicio;
        public Produccion_Desp()
        {
            InitializeComponent();
        }


        private void Produccion_Desp_Load(object sender, EventArgs e)
        {
            DiaInicio = DateTime.Parse(Properties.Settings.Default.BusDia);

            Dia1LB.Text = DiaInicio.ToString().Substring(0, 10);
            Dia2LB.Text = Convert.ToString(DiaInicio.AddDays(+1)).Substring(0, 10);
            Dia3LB.Text = Convert.ToString(DiaInicio.AddDays(+2)).Substring(0, 10);
            Dia4LB.Text = Convert.ToString(DiaInicio.AddDays(+3)).Substring(0, 10);
            Dia5LB.Text = Convert.ToString(DiaInicio.AddDays(+4)).Substring(0, 10);

            try
            {
                dataGridViewBotellas.DataSource = Datos_BusAdmin.Despaletizador.Tables[Datos_BusAdmin.FDesp[1, 0]];
                dataGridViewRegistroLlen.DataSource = Datos_BusAdmin.Llenadora.Tables[Datos_BusAdmin.FLlen[4, 0]];
                dataGridViewRegistroEtiq.DataSource = Datos_BusAdmin.Etiquetadora.Tables[Datos_BusAdmin.FEtiq[1, 0]];
                dataGridViewRegistroEnc.DataSource = Datos_BusAdmin.Encajonadora.Tables[Datos_BusAdmin.FEnc[1, 0]];

            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
            }

            for (int i = 0; i < dataGridViewBotellas.RowCount - 1; i++)
            {
                //MessageBox.Show(dataGridViewBotellas.Rows[i].Cells[5].Value.ToString());
                suma += int.Parse(dataGridViewBotellas.Rows[i].Cells[5].Value.ToString());
            }


            DespTB.Text = Convert.ToString(suma);
            suma = 0;

            for (int i = 0; i < dataGridViewRegistroLlen.RowCount - 1; i++)
            {
                //MessageBox.Show(dataGridViewRegistroLlen.Rows[i].Cells[4].Value.ToString());
                suma += int.Parse(dataGridViewRegistroLlen.Rows[i].Cells[4].Value.ToString());
            }

            LlenTB.Text = Convert.ToString(suma);
            suma = 0;
            for (int i = 0; i < dataGridViewRegistroEtiq.RowCount - 1; i++)
            {
                //MessageBox.Show(dataGridViewRegistroEtiq.Rows[i].Cells[5].Value.ToString());
                suma += int.Parse(dataGridViewRegistroEtiq.Rows[i].Cells[5].Value.ToString());
            }
            EtiqTB.Text = Convert.ToString(suma);
            suma = 0;
            for (int i = 0; i < dataGridViewRegistroEnc.RowCount-1; i++)
            {

                //MessageBox.Show(dataGridViewRegistroEnc.Rows[i].Cells[4].Value.ToString());
                formato = dataGridViewRegistroEnc.Rows[i].Cells[5].Value.ToString();
                cajas = int.Parse(dataGridViewRegistroEnc.Rows[i].Cells[7].Value.ToString());
                if (formato.Substring(1, 1) == "X")
                {
                    suma += Convert.ToInt16(formato.Substring(0, 1)) * cajas;
                    CompletarRegistros(dataGridViewRegistroEnc.Rows[i].Cells[3].Value.ToString(), dataGridViewRegistroEnc.Rows[i].Cells[0].Value.ToString(), Convert.ToInt16(formato.Substring(0, 1)) * cajas);
                }
                if (formato.Substring(2, 1) == "X")
                {
                    suma += Convert.ToInt16(formato.Substring(0, 2)) * cajas;
                    CompletarRegistros(dataGridViewRegistroEnc.Rows[i].Cells[3].Value.ToString(), dataGridViewRegistroEnc.Rows[i].Cells[0].Value.ToString(), Convert.ToInt16(formato.Substring(0, 2)) * cajas);
                }
            }
            EncTB.Text = Convert.ToString(suma);
            suma = 0;
        }
        private void CompletarRegistros(string truno, string dia, int valor)
        {
            switch (truno)
            {
                case "Mañana":
                    if (dia == Dia1LB.Text) MD1TB.Text = Convert.ToString(Convert.ToInt64(MD1TB.Text) + valor);
                    if (dia == Dia2LB.Text) MD2TB.Text = Convert.ToString(Convert.ToInt64(MD2TB.Text) + valor);
                    if (dia == Dia3LB.Text) MD3TB.Text = Convert.ToString(Convert.ToInt64(MD3TB.Text) + valor);
                    if (dia == Dia4LB.Text) MD4TB.Text = Convert.ToString(Convert.ToInt64(MD4TB.Text) + valor);
                    if (dia == Dia5LB.Text) MD5TB.Text = Convert.ToString(Convert.ToInt64(MD5TB.Text) + valor);
                    break;
                case "Tarde":
                    if (dia == Dia1LB.Text) TD1TB.Text = Convert.ToString(Convert.ToInt64(TD1TB.Text) + valor);
                    if (dia == Dia2LB.Text) TD2TB.Text = Convert.ToString(Convert.ToInt64(TD2TB.Text) + valor);
                    if (dia == Dia3LB.Text) TD3TB.Text = Convert.ToString(Convert.ToInt64(TD3TB.Text) + valor);
                    if (dia == Dia4LB.Text) TD4TB.Text = Convert.ToString(Convert.ToInt64(TD4TB.Text) + valor);
                    if (dia == Dia5LB.Text) TD5TB.Text = Convert.ToString(Convert.ToInt64(TD5TB.Text) + valor);
                    break;
                case "Noche":
                    if (dia == Dia1LB.Text) ND1TB.Text = Convert.ToString(Convert.ToInt64(ND1TB.Text) + valor);
                    if (dia == Dia2LB.Text) ND2TB.Text = Convert.ToString(Convert.ToInt64(ND2TB.Text) + valor);
                    if (dia == Dia3LB.Text) ND3TB.Text = Convert.ToString(Convert.ToInt64(ND3TB.Text) + valor);
                    if (dia == Dia4LB.Text) ND4TB.Text = Convert.ToString(Convert.ToInt64(ND4TB.Text) + valor);
                    if (dia == Dia5LB.Text) ND5TB.Text = Convert.ToString(Convert.ToInt64(ND5TB.Text) + valor);
                    break;
            }
        }
    }
}

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

namespace WHPS.Precinta
{
    public partial class MainPrecinta : Form
    {
        public MainPrecinta()
        {
            MaquinaLinea.mostrar_whpst_inicio = false;

            InitializeComponent();
        }

        

        private void MainPrecinta_Load(object sender, EventArgs e)
        {
            if (TabControl.SelectedIndex == 0)
            {
                //Inicialmente precargamos el lanzamiento de la línea 2
                MaquinaLinea.numlin = 2;
                //dgv = dataGridViewL2;
                ExcelUtiles.CrearTablaLanzamientosPrecinta(dataGridL2);
                dataGridL2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridL2.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridL2.Columns["Cliente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridL2.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            }

        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedIndex == 0)
            {
                //Inicialmente precargamos el lanzamiento de la línea 2
                MaquinaLinea.numlin = 2;

                ExcelUtiles.CrearTablaLanzamientosPrecinta(dataGridL2);
                dataGridL2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridL2.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridL2.Columns["Cliente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridL2.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            }
            if (TabControl.SelectedIndex == 1)
            {
                //Inicialmente precargamos el lanzamiento de la línea 3
                MaquinaLinea.numlin = 3;
                ExcelUtiles.CrearTablaLanzamientosPrecinta(dataGridL3);
                dataGridL3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridL3.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridL3.Columns["Cliente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridL3.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;


            }
            if (TabControl.SelectedIndex == 2)
            {
                //Inicialmente precargamos el lanzamiento de la línea 5
                MaquinaLinea.numlin = 5;

                ExcelUtiles.CrearTablaLanzamientosPrecinta(dataGridL5);
                dataGridL5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridL5.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridL5.Columns["Cliente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridL5.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;


            }


            /* ExcelUtiles.CrearTablaLanzamientos(dgv);
             dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
             dgv.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
             dgv.Columns["Cliente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
             dgv.Columns["Comentarios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;*/
        }

        private void ExitB_Click(object sender, EventArgs e)
        {
            MaquinaLinea.mostrar_whpst_inicio = true;
            WHPST_INICIO Form = new WHPST_INICIO();
            Form.Show();
            Form.Abrir_Lanzamiento();
            Hide();
        }


    }
}

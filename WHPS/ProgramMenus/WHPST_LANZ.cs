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
using WHPS.Parte;
using WHPS.Precinta;
using WHPS.Rotura;
using WHPS.Utiles;

namespace WHPS.ProgramMenus
{
    public partial class WHPST_LANZ : Form
    {
        WHPST_INICIO Padre;
        MainParte Parte;
        MainRotura Rotura;
        DataGridView dgv;
        public bool ModoAñadirProducto = false;

        public WHPST_LANZ()
        {
            InitializeComponent();
        }

        public WHPST_LANZ(WHPST_INICIO wHPST_INICIO)
        {
            this.Padre = wHPST_INICIO;
        }

        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        private void WHPST_LANZ_Load(object sender, EventArgs e)
        {

            if (TabControlLanzamiento.SelectedIndex == 0)
            {
                //Inicialmente precargamos el lanzamiento de la línea 2
                MaquinaLinea.numlin = 2;
                dgv = dataGridViewL2;
                ExcelUtiles.CrearTablaLanzamientosAmd(dataGridViewL2);
                //   DataGridViewColumn column = new DataGridViewColumn();
                DataTable dt = dataGridViewL2.DataSource as DataTable;
                //    column.ValueType = typeof(int);
                //column.DefaultHeaderCellType = typeof(int);
                //      dataGridViewL2.Columns.Insert(0, new DataGridViewColumn(typeof(int)));
            }
        }

        private void TabControlLanzamiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControlLanzamiento.SelectedIndex == 0)
            {
                //Inicialmente precargamos el lanzamiento de la línea 2
                MaquinaLinea.numlin = 2;
                dgv = dataGridViewL2;
            }
            if (TabControlLanzamiento.SelectedIndex == 1)
            {
                //Inicialmente precargamos el lanzamiento de la línea 3
                MaquinaLinea.numlin = 3;
                dgv = dataGridViewL3;
            }
            if (TabControlLanzamiento.SelectedIndex == 2)
            {
                //Inicialmente precargamos el lanzamiento de la línea 5
                MaquinaLinea.numlin = 5;
                dgv = dataGridViewL5;
            }
            if (TabControlLanzamiento.SelectedIndex == 3)
            {
                MessageBox.Show(Properties.Settings.Default.AvisoMantenimiento);
            }
            if (TabControlLanzamiento.SelectedIndex == 4)
            {
                MessageBox.Show(Properties.Settings.Default.AvisoMantenimiento);
            }

            ExcelUtiles.CrearTablaLanzamientosAmd(dgv);
        }

        /// <summary>
        /// Función que detecta las celdas, marca las celdas que tienen que sobresalir y detecta que producto esta iniciado para rellenar los datos de producción.
        /// </summary>
        private void dataGridViewL2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DGV_CellFormatting(e);
        }
        private void dataGridViewL3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DGV_CellFormatting(e);
        }
        private void dataGridViewL5_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DGV_CellFormatting(e);
        }
        public void DGV_CellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            if (dgv.Columns[e.ColumnIndex].Name == "LÍQUIDOS")
            {
                switch (Convert.ToString(e.Value))
                {
                    case "OK":
                        e.CellStyle.BackColor = Color.Green;
                        e.CellStyle.ForeColor = Color.White;
                        break;
                    case "ELABORACIÓN":
                        e.CellStyle.BackColor = Color.Yellow;
                        break;
                    case "NOK":
                        e.CellStyle.BackColor = Color.Red;
                        break;
                }
            }
            if (dgv.Columns[e.ColumnIndex].Name == "MATERIALES")
            {
                switch (Convert.ToString(e.Value))
                {
                    case "OK":
                        e.CellStyle.BackColor = Color.Green;
                        e.CellStyle.ForeColor = Color.White;
                        break;
                    case "PENDIENTE":
                        e.CellStyle.BackColor = Color.Orange;
                        break;
                    case "NOK":
                        e.CellStyle.BackColor = Color.Red;
                        break;
                }
            }
            if (dgv.Columns[e.ColumnIndex].Name == "ESTADO")
            {
                {
                    switch (Convert.ToString(e.Value))
                    {
                        case "Completado":
                            e.CellStyle.BackColor = Color.Green;
                            e.CellStyle.ForeColor = Color.White;
                            break;
                        case "Saltado":
                            e.CellStyle.BackColor = Color.Yellow;
                            break;
                        case "Iniciado":
                            e.CellStyle.BackColor = Color.Orange;
                            break;
                        case "Sin Terminar":
                            e.CellStyle.BackColor = Color.Red;
                            break;
                    }
                }
                if (dgv.Columns[e.ColumnIndex].Name == "ESTADO")
                {
                    if (Convert.ToString(e.Value) == "Iniciado")
                    {
                        //Se muestra los datos del productos que se está tratando
                        //ExtraerDatosProduccion(e.RowIndex, dgv);
                    }
                }
            }
        }

        /// <summary>
        /// Botones que mueven de fila los productos.
        /// </summary>
        private void MoverB_Click(object sender, EventArgs e)
        {
            UpB.Visible = true;
            DownB.Visible = true;
        }
        private void UpB_Click(object sender, EventArgs e)
        {
            var row = dgv.SelectedRows[0];

            if (row != null && row.Index > 0)
            {
                var swapRow = dgv.Rows[row.Index - 1];
                object[] values = new object[swapRow.Cells.Count];

                foreach (DataGridViewCell cell in swapRow.Cells)
                {
                    values[cell.ColumnIndex] = cell.Value;
                    cell.Value = row.Cells[cell.ColumnIndex].Value;
                }

                foreach (DataGridViewCell cell in row.Cells)
                    cell.Value = values[cell.ColumnIndex];

                dgv.Rows[row.Index - 1].Selected = true;//have the selection follow the moving cellç
                SaveB.BackColor = Color.IndianRed;
            }
        }
        private void DownB_Click(object sender, EventArgs e)
        {
            var row = dgv.SelectedRows[0];
            int ultimafila = dgv.RowCount - 2;

            if (row != null && row.Index > 0 && row.Index < ultimafila)
            {
                var swapRow = dgv.Rows[row.Index + 1];
                object[] values = new object[swapRow.Cells.Count];

                foreach (DataGridViewCell cell in swapRow.Cells)
                {
                    values[cell.ColumnIndex] = cell.Value;
                    cell.Value = row.Cells[cell.ColumnIndex].Value;
                }

                foreach (DataGridViewCell cell in row.Cells)
                    cell.Value = values[cell.ColumnIndex];

                dgv.Rows[row.Index + 1].Selected = true;//have the selection follow the moving cell
                SaveB.BackColor = Color.IndianRed;
            }

        }

        private void SaveB_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Properties.Settings.Default.AvisoMantenimiento);
            //SaveB.BackColor = Color.FromArgb(27, 33, 41);
            //UpB.Visible = false;
            //DownB.Visible = false;
        }


        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = ((DataTable)dgv.DataSource).Copy();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (!column.Visible)
                {
                    dt.Columns.Remove(column.Name);
                }
            }
            return dt;
        }



        private void EliminarB_Click(object sender, EventArgs e)
        {
            dgv.Rows.Remove(dgv.CurrentRow);
            SaveB.BackColor = Color.IndianRed;
        }

        private void AñadirProductoB_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Properties.Settings.Default.AvisoMantenimiento);
            switch (ModoAñadirProducto)
            {
                case false:
                    AñadirElementoBOX.Visible = true;
                    AñadirProductoB.BackColor = Color.DarkSeaGreen;
                    ModoAñadirProducto = true;
                    break;
                case true:
                    AñadirElementoBOX.Visible = false;
                    AñadirProductoB.BackColor = Color.FromArgb(27, 33, 41);
                    ModoAñadirProducto = false;
                    break;
            }
        }

        private void AñadirB_Click(object sender, EventArgs e)
        {
            DataTable dt = LanzamientoLinea.DBL2.Tables[0];
            DataRow fila = dt.NewRow();
            fila[1] = LoteTB.Text;
            fila[2] = ReferenciaTB.Text;
            fila[3] = OrdenTB.Text;
            fila[4] = ClienteTB.Text;
            fila[5] = ProductoTB.Text;
            fila[6] = CajasTB.Text;
            fila[7] = FormatoTB.Text;
            fila[8] = PATB.Text;
            fila[9] = RefLiqTB.Text;
            fila[10] = GradosTB.Text;
            fila[11] = TipoTB.Text;
            fila[12] = ComentariosTB.Text;
            dt.Rows.Add(fila);
            SaveB.BackColor = Color.IndianRed;
        }


        private void ExcelB_Click(object sender, EventArgs e)
        {

            Form Form = new MainPrecinta();
            this.Hide();

            Form.Show();

        }

        private void BotRotaB_Click(object sender, EventArgs e)
        {
            //if (Rotura == null)
            //{
            //    Rotura = new MainRotura();   //Create form if not created
            //    Rotura.FormClosed += Rotura_FormClosed;  //Add eventhandler to cleanup after form closes
            //}

            //Rotura.Show(this);  //Show Form assigning this form as the forms owner
            MessageBox.Show(Properties.Settings.Default.AvisoMantenimiento);
        }
        void Rotura_FormClosed(object sender, FormClosedEventArgs e)
        {
            Rotura = null;  //If form is closed make sure reference is set to null
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Properties.Settings.Default.AvisoMantenimiento);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Properties.Settings.Default.AvisoMantenimiento);
        }

        private void AñadirB_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridViewL2_DoubleClick(object sender, EventArgs e)
        {
            /*for (int i = 0; i < dataGridViewL2.SelectedCells.Count; i++) {
                Console.WriteLine("Celda: " + dataGridViewL2.SelectedCells[i].RowIndex + " " + dataGridViewL2.SelectedCells[i].RowIndex + " " + dataGridViewL2.SelectedCells[i].Value.ToString());

            }*/
            RellenarDatosSeleccionados(dataGridViewL2);
        }

        private void RellenarDatosSeleccionados(DataGridView dgv)
        {
            DatosSeleccionadoBOX.Visible = true;
            ComentariosSelecTB.Text = dgv.SelectedCells[12].Value.ToString();
            TipoSelecTB.Text = dgv.SelectedCells[11].Value.ToString(); ;
            FormatoSelecTB.Text = dgv.SelectedCells[7].Value.ToString();
            GradSelecTB.Text = dgv.SelectedCells[10].Value.ToString();
            MaterialesSelecTB.Text = dgv.SelectedCells[15].Value.ToString();
            LiquidoSelecTB.Text = dgv.SelectedCells[13].Value.ToString();
            ObservLabSelecTB.Text = dgv.SelectedCells[14].Value.ToString();
            EstadoSelecTB.Text = dgv.SelectedCells[16].Value.ToString();
            CajasSelecTB.Text = dgv.SelectedCells[6].Value.ToString();
            CodProductoSelecTB.Text = dgv.SelectedCells[2].Value.ToString();
            ClienteSelecTB.Text = dgv.SelectedCells[4].Value.ToString();
            OrdenSelecTB.Text = dgv.SelectedCells[3].Value.ToString();
            ReferenciaSelecTB.Text = dgv.SelectedCells[9].Value.ToString();
            ObservLabSelecTB.Text = dgv.SelectedCells[18].Value.ToString();
            PASelecTB.Text = dgv.SelectedCells[8].Value.ToString();
            ProductoSelecTB.Text = dgv.SelectedCells[5].Value.ToString();
            Utilidades.MostrarImagen(CodProductoSelecTB.Text, Imagen);
        }
        private void SeleccionarProductoB_Click(object sender, EventArgs e)
        {

        }

        private void VolverB_Click(object sender, EventArgs e)
        {

        }

        private void BOMB_Click(object sender, EventArgs e)
        {

        }

        private void VolverB_Click_1(object sender, EventArgs e)
        {
            DatosSeleccionadoBOX.Visible = false;
        }

        private void dataGridViewL3_DoubleClick(object sender, EventArgs e)
        {
            RellenarDatosSeleccionados(dataGridViewL3);

        }

        private void dataGridViewL5_DoubleClick(object sender, EventArgs e)
        {
            RellenarDatosSeleccionados(dataGridViewL5);

        }

        private void BOMB_Click_1(object sender, EventArgs e)
        {
            MaquinaLinea.RetornoBOM = "Lanzamiento";
            MaquinaLinea.ReferenciaBOM = CodProductoSelecTB.Text;
            WHPST_BOM Form = new WHPST_BOM();
            Hide();
            ParentForm.Hide();
            Form.Show();
        }
    }
}

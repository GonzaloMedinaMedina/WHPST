using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        public static WHPST_INICIO parent;
        public MainParte parte;
        public MainRotura rotura;

        DataGridView dgv;
        string result = "";

        public bool ModoAñadirProducto = false;

        public WHPST_LANZ(WHPST_INICIO p)
        {

            InitializeComponent();

            parent = p;
        }

        /// <summary>
        /// Función que se ejecuta al mostrar el form.
        /// </summary>
        private void WHPST_LANZ_Load(object sender, EventArgs e)
        {
            if (TabControlLanzamiento != null)
            if (TabControlLanzamiento.SelectedIndex == 0)
            {
                LanzamientoLinea.DBL2 = ExcelUtiles.ObtenerUltimosMovimientosLanzadorAmd("DB_L2", "Linea 2", out result);
                //Inicialmente precargamos el lanzamiento de la línea 2
                MaquinaLinea.numlin = 2;

                    dgv = dataGridViewL2;
               
                    ExcelUtiles.CrearTablaLanzamientosAmd(dataGridViewL2);
                    if (MaquinaLinea.usuario == "Encargado")
                    {
                        foreach (DataGridViewColumn c in dataGridViewL2.Columns)
                        {
                            if (c.Name != "ESTADO" || c.Name!="FECHA INICIO"  || c.Name!= "OBSERVACIONES PROD.") c.ReadOnly = true;
                        }
                    }
                    if (MaquinaLinea.usuario == "Laboratorio")
                    {
                        foreach (DataGridViewColumn c in dataGridViewL2.Columns)
                        {
                            if (c.Name != "LÍQUIDOS" || c.Name != "OBSERVACIONES LAB") c.ReadOnly = true;
                        }

                    }
                    if (MaquinaLinea.usuario == "Calidad")
                    {
                        foreach (DataGridViewColumn c in dataGridViewL2.Columns)
                        {
                            if (c.Name != "MATERIALES") c.ReadOnly = true;
                        }

                    }
                    if(MaquinaLinea.usuario == "Administracion")
                    {

                    }
                    else
                    {
                        foreach (DataGridViewColumn c in dataGridViewL2.Columns)
                        {
                           c.ReadOnly = true;
                        }
                    }
                    //    dataGridViewL2.Columns["ESTADO"].ReadOnly = false; }

                    //   DataGridViewColumn column = new DataGridViewColumn();
                    DataTable dt = dataGridViewL2.DataSource as DataTable;
                    //    column.ValueType = typeof(int);
                    //column.DefaultHeaderCellType = typeof(int);
                    //      dataGridViewL2.Columns.Insert(0, new DataGridViewColumn(typeof(int)));
                }
        }

        private void TabControlLanzamiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel2.Visible = false;
            DatosSeleccionadoBOX.Visible = false;
            if (TabControlLanzamiento.SelectedIndex == 0)
            {
                //Inicialmente precargamos el lanzamiento de la línea 2
                MaquinaLinea.numlin = 2;
                dgv = dataGridViewL2;
            }
            if (TabControlLanzamiento.SelectedIndex == 1)
            {
                LanzamientoLinea.DBL3 = ExcelUtiles.ObtenerUltimosMovimientosLanzadorAmd("DB_L3", "Linea 3", out result);

                //Inicialmente precargamos el lanzamiento de la línea 3
                MaquinaLinea.numlin = 3;
                dgv = dataGridViewL3;
            }
            if (TabControlLanzamiento.SelectedIndex == 2)
            {
                LanzamientoLinea.DBL5 = ExcelUtiles.ObtenerUltimosMovimientosLanzadorAmd("DB_L5", "Linea 5", out result);
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

        /// <summary>
        /// Función que da color a unos text box determinados para indicar el estado del producto
        /// </summary>
        public void ColorTextBox()
        {
            switch (EstadoSelecTB.Text)
            {
                case "Completado":
                    EstadoSelecTB.BackColor = System.Drawing.Color.Green;
                    EstadoSelecTB.ForeColor = System.Drawing.Color.White;
                    break;
                case "Saltado":
                    EstadoSelecTB.BackColor = System.Drawing.Color.Yellow;
                    break;
                case "Iniciado":
                    EstadoSelecTB.BackColor = System.Drawing.Color.Orange;
                    break;
                case "Sin Terminar":
                    EstadoSelecTB.BackColor = System.Drawing.Color.Red;
                    break;
                case "":
                    EstadoSelecTB.BackColor = System.Drawing.Color.Gainsboro;
                    break;
            }
            switch (LiquidoSelecTB.Text)
            {
                case "OK":
                    LiquidoSelecTB.BackColor = System.Drawing.Color.Green;
                    LiquidoSelecTB.ForeColor = System.Drawing.Color.White;
                    break;
                case "ELABORACIÓN":
                    LiquidoSelecTB.BackColor = System.Drawing.Color.Yellow;
                    break;
                case "NOK":
                    LiquidoSelecTB.BackColor = System.Drawing.Color.Red;
                    break;
                case "":
                    LiquidoSelecTB.BackColor = System.Drawing.Color.Gainsboro;
                    break;
            }
            switch (MaterialesSelecTB.Text)
            {
                case "OK":
                    MaterialesSelecTB.BackColor = System.Drawing.Color.Green;
                    MaterialesSelecTB.ForeColor = System.Drawing.Color.White;
                    break;
                case "PENDIENTE":
                    MaterialesSelecTB.BackColor = System.Drawing.Color.Orange;
                    break;
                case "NOK":
                    MaterialesSelecTB.BackColor = System.Drawing.Color.Red;
                    break;
                case "":
                    MaterialesSelecTB.BackColor = System.Drawing.Color.Gainsboro;
                    break;
            }
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
                    panel3.BringToFront();
                    AñadirElementoBOX.BringToFront();
                    panel3.Visible = true;
                    AñadirProductoB.BackColor = Color.DarkSeaGreen;
                    ModoAñadirProducto = true;
                    break;
                case true:
                    panel3.SendToBack();
                    AñadirElementoBOX.SendToBack();
                    panel3.Visible = false;
                    AñadirProductoB.BackColor = Color.FromArgb(27, 33, 41);
                    ModoAñadirProducto = false;
                    break;
            }
        }

        private void AñadirB_Click(object sender, EventArgs e)
        {
//------------------------ARREGLAR
            //OrdenTB ClienteTB ProductoTB CajasTB FormatoTB PATB RefLiqTB GradosTB TiposTB ComentariosTB
            //Ahora referencia es código
            //Ref. es referencia liquido
             DataTable dt = LanzamientoLinea.DBL2.Tables[0];
             DataRow fila = dt.NewRow();
             fila[1] = ID_Orden.Text;
             fila[2] = CodigoTB.Text;
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


            //Creamos un dataTable a partir del datagridview de lanzamiento
            DataTable dt = dgv.DataSource as DataTable;

            int id=0;

            //Recorremos las filas del dt para obtener la fila dónde vamos a insertar el nuevo producto
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                //Si el ID de la fila es igual al ID_Orden introducido, será la fila i a insertar
                if (ID_Orden.Text == Convert.ToString(dt.Rows[i]["ID"])) id = i;
            }
            int aux;

            //Empezando por la última fila del dt, desplazamos una posición abajo cada fila hasta llegar a id
            for(int i=dt.Rows.Count-1; i>=id; i--)
            {
                aux= Convert.ToInt32(dt.Rows[i]["ID"])+1;
                dt.Rows[i]["ID"] = aux;

            }
            //Creamos la fila con los datos introducidos
            DataRow newdr = dt.NewRow();

            newdr["ID"] = ID_Orden.Text;
            newdr["ID Lanz"] = ID_Lanzamiento.Text;
            newdr["CÓDIGO"] = CodigoTB.Text;
            newdr["CLIENTE"] = ClienteTB.Text;
            newdr["ORDEN"] = OrdenTB.Text;
            newdr["CAJAS"] = CajasTB.Text;
            newdr["FORM."] = FormatoTB.Text;
            newdr["PRODUCTO"] = ProductoTB.Text;
            newdr["PA"] = PATB.Text;
            newdr["REF."] = RefLiqTB.Text;
            newdr["GDO."] = GradosTB.Text;
            newdr["TIPO"] = TipoTB.Text;
            newdr["Comentarios"] = ComentariosTB.Text;


            //insertamos la nueva fila en id, dónde ya tendremos un hueco
            dt.Rows.InsertAt(newdr, id);

            dgv.DataSource = null;
            dgv.DataSource = dt;
            dgv.Update();
            
            int r = ExcelUtiles.ExportDtToExcel(dt, MaquinaLinea.FileLanzador,"Linea "+MaquinaLinea.numlin);

        }

        private void dataGridViewL2_DoubleClick(object sender, EventArgs e)
        {
            /*for (int i = 0; i < dataGridViewL2.SelectedCells.Count; i++) {
                Console.WriteLine("Celda: " + dataGridViewL2.SelectedCells[i].RowIndex + " " + dataGridViewL2.SelectedCells[i].RowIndex + " " + dataGridViewL2.SelectedCells[i].Value.ToString());

            }*/

            RellenarDatosSeleccionados(dataGridViewL2);
            ColorTextBox();
        }

        private void RellenarDatosSeleccionados(DataGridView dgv)
        {
            panel2.Visible = true;
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
            DatosSeleccionadoBOX.Visible = true;
        }
        private void SeleccionarProductoB_Click(object sender, EventArgs e)
        {

        }

        private void VolverB_Click(object sender, EventArgs e)
        {

        }

        private void VolverB_Click_1(object sender, EventArgs e)
        {
            DatosSeleccionadoBOX.Visible = false;
            panel2.Visible = false;
        }

        private void dataGridViewL3_DoubleClick(object sender, EventArgs e)
        {
            RellenarDatosSeleccionados(dataGridViewL3);
        }

        private void dataGridViewL5_DoubleClick(object sender, EventArgs e)
        {
            RellenarDatosSeleccionados(dataGridViewL5);
        }

        private void BOMB_Click(object sender, EventArgs e)
        {

            MaquinaLinea.ReferenciaBOM = CodProductoSelecTB.Text;
            MaquinaLinea.VolverA = RetornoBOM.Lanz;
            Hide();
            this.Dispose();

            Utilidades.AbrirForm(parent.GetBOM(), parent, typeof(WHPST_BOM));
        }



        private void dataGridViewL2_MouseLeave(object sender, EventArgs e)
        {
            DoDragDrop(sender, DragDropEffects.All);

        }

       
        private void dataGridViewL2_MouseDown(object sender, MouseEventArgs e)
        {
          
        }

        private void dataGridViewL2_MouseUp_1(object sender, MouseEventArgs e)
        {
            DoDragDrop(sender, DragDropEffects.Move);
            DataGridViewSelectedRowCollection rc = dataGridViewL2.SelectedRows;
        }

        private void dataGridViewL2_DragDrop(object sender, DragEventArgs e)
        {
            string salida = "";
            foreach (DataGridViewRow r in dataGridViewL2.SelectedRows)
            {
                salida += r.ToString() + Environment.NewLine;
            }
            LABELPRUEBA.Text = salida;
            MessageBox.Show(salida);
        }
    }
}

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
    public enum operaciones { ANIADIR,BORRAR}

    //tipo de dato que define la operacion realizada y las filas afectadas del dgv
    public struct cambios
    {
        public operaciones op;
       // public DataGridViewSelectedRowCollection filas;
        public List<DataGridViewRow> filas;

        public cambios(operaciones o, List<DataGridViewRow> f)
        {
            op = o;
            filas = f;
        }
    }
    //tipo de dato que guarda para un dgv todas las operaciones que se han realizado sobre el
    public struct pair
    {   
        //variabla para saber el dvg seleccionado 0 es para L2, ..., 2 para L5 y 3 para datagridview1
        public int dvg;
        public List<cambios> cambios_realizados;

        public pair(int d, List<cambios> cr)
        {
            dvg = d;
            cambios_realizados = cr;
        }
    }
    

    public partial class WHPST_LANZ : Form
    {
       
        public static WHPST_INICIO parent;
        public MainParte parte;
        public MainRotura rotura;

        DataGridView dgv;
        string result = "";
        DataGridViewSelectedRowCollection filas_nuevas;
        //lista de coleccion de filas que contendrá las filas a borrar para cada dgv
        DataGridViewSelectedRowCollection filas_borrarl2;
        DataGridViewSelectedRowCollection filas_borrarl3;
        DataGridViewSelectedRowCollection filas_borrarl5;

        //Lista de id_ords añadidos
        List<string> Nuevos_ID_Lanz = new List<string>();

        //Lista de cambios para cada dvg, cada posición en la lista contiene un struct pair, donde dvg es el datagridview donde se realizaron los cambios
        //y una lista del struct cambios que contiene la operacion realizada y las filas tratadas
        List<pair> cambios = new List<pair>();
        public bool ModoAñadirProducto = false;

        public WHPST_LANZ(WHPST_INICIO p)
        {

            InitializeComponent();
            

            cambios.Add(new pair(0,new List<cambios>()));
            cambios.Add(new pair(1, new List<cambios>()));
            cambios.Add(new pair(2, new List<cambios>()));
            cambios.Add(new pair(3, new List<cambios>()));
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

                //    Object o = dataGridViewL2.DataSource;
                //    dataGridView1.DataSource = o;
                    foreach (DataGridViewColumn c in dataGridViewL2.Columns)
                    {
                        DataGridViewColumn col = new DataGridViewColumn();
                        col.Name = c.Name;
                        col.CellTemplate = c.CellTemplate;
                        dataGridView1.Columns.Add(col);

                    }
                    dataGridView1.Columns["Estado EXP"].Visible = false;
                    dataGridView1.Columns["FECHA EXP"].Visible = false;
               /*     for (int i = 1; i < 10; i++)
                    {
                       
                        dataGridView1.Rows.Add(i, "prueba" + i, "prueba" + i, "prueba" + i, "prueba" + i, "prueba" + i, "prueba" + i, "prueba" + i, "prueba" + i,
                            "prueba" + i, "prueba" + i, "prueba" + i, "prueba" + i, "prueba" + i, "prueba" + i, "prueba" + i, "prueba" + i, "prueba" + i,
                            "prueba" + i, "prueba" + i, "prueba" + i);


                    }*/
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

        //Devuelve el dgv seleccionado con su indice correspondiente en la lista de cambios
        private Tuple <int, DataGridView> dgvSelected()
        {
            if (TabControlLanzamiento.SelectedIndex == 0)
            {
                return new Tuple<int, DataGridView>( 0 , dataGridViewL2);

            }
            if (TabControlLanzamiento.SelectedIndex == 1)
            {
                return new Tuple<int, DataGridView>(1, dataGridViewL3);

            }
            if (TabControlLanzamiento.SelectedIndex == 2)
            {
                return new Tuple<int, DataGridView>(2, dataGridViewL5);
            }
            return null;
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
            if (dgv.Columns[e.ColumnIndex].Name == "ID LANZ")
            {
                if (Nuevos_ID_Lanz.Contains(Convert.ToString(dgv.Rows[Convert.ToInt32(e.RowIndex)].Cells[1].Value)))
                {
                    for (int j = 0; j < 13; ++j)
                    {
                        dgv.Rows[Convert.ToInt32(e.RowIndex)].Cells[j].Style.BackColor = Color.Yellow;
                        dgv.Rows[Convert.ToInt32(e.RowIndex)].Cells[j].Style.ForeColor = Color.Red;
                    }
                   
                }
            }
            
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


        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Properties.Settings.Default.AvisoMantenimiento);
        }

        private void aniadir_fila_dgv(DataGridView dgv, DataGridViewRow r, bool ordenar)
        {
            //Creamos un dataTable a partir del datagridview de lanzamiento
            DataTable dt = dgv.DataSource as DataTable;
            int id = 0;

            if (ordenar)
            {

                if (Convert.ToInt32(r.Cells[0].Value) > Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["ID"]))
                {
                    id = dt.Rows.Count - 1;
                }
                else
                {
                    //Recorremos las filas del dt para obtener la fila dónde vamos a insertar el nuevo producto
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Si el ID de la fila es igual al ID_Orden introducido, será la fila i a insertar
//                        Console.WriteLine(Convert.ToString(r.Cells[0].Value) + " " + Convert.ToString(dt.Rows[i]["ID"]));
                        if (Convert.ToInt32(r.Cells[0].Value)+1 == Convert.ToInt32(dt.Rows[i]["ID"])) id = i;
                    }
                    int aux;

                    //Empezando por la última fila del dt, desplazamos una posición abajo cada fila hasta llegar a id
                    for (int i = dt.Rows.Count - 1; i >= id; i--)
                    {
                        aux = Convert.ToInt32(dt.Rows[i]["ID"]) + 1;
                        dt.Rows[i]["ID"] = aux;

                    }
                }
            }
            //Creamos la fila con los datos introducidos
            DataRow newdr = dt.NewRow();

            newdr["ID"] = r.Cells[0].Value;
            newdr["ID Lanz"] = r.Cells[1].Value;
            newdr["CÓDIGO"] = r.Cells[2].Value;
            newdr["CLIENTE"] = r.Cells[3].Value;
            newdr["ORDEN"] = r.Cells[4].Value;
            newdr["CAJAS"] = r.Cells[5].Value;
            newdr["FORM."] = r.Cells[6].Value;
            newdr["PRODUCTO"] = r.Cells[7].Value;
            newdr["PA"] = r.Cells[8].Value;
            newdr["REF."] = r.Cells[9].Value;
            newdr["GDO."] = r.Cells[10].Value;
            newdr["TIPO"] = r.Cells[11].Value;
            newdr["Comentarios"] = r.Cells[12].Value;


            //insertamos la nueva fila en id, dónde ya tendremos un hueco
            dt.Rows.InsertAt(newdr, id);

            dgv.DataSource = null;
            dgv.DataSource = dt;
            dgv.Update();
        }
        private DataTable aniadir_fila_dt(DataTable dt, DataGridViewRow r)
        {
            

        
            //Creamos la fila con los datos introducidos
            DataRow newdr = dt.NewRow();

            newdr["ID"] = r.Cells[0].Value;
            newdr["ID Lanz"] = r.Cells[1].Value;
            newdr["CÓDIGO"] = r.Cells[2].Value;
            newdr["CLIENTE"] = r.Cells[3].Value;
            newdr["ORDEN"] = r.Cells[4].Value;
            newdr["CAJAS"] = r.Cells[5].Value;
            newdr["FORM."] = r.Cells[6].Value;
            newdr["PRODUCTO"] = r.Cells[7].Value;
            newdr["PA"] = r.Cells[8].Value;
            newdr["REF."] = r.Cells[9].Value;
            newdr["GDO."] = r.Cells[10].Value;
            newdr["TIPO"] = r.Cells[11].Value;
            newdr["Comentarios"] = r.Cells[12].Value;


            //insertamos la nueva fila en id, dónde ya tendremos un hueco
            dt.Rows.InsertAt(newdr, dt.Rows.Count);
            return dt;
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
//          SystemInformation.PrimaryMonitorSize.Height;
//          SystemInformation.PrimaryMonitorSize.Width;
            TabControlLanzamiento.Height =  475;
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
            TabControlLanzamiento.Height = 938;
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

        private void RestarID(int id, int n_borrados, DataGridView item2) {

            DataTable dt = item2.DataSource as DataTable;
            //Creamos un dataTable a partir del datagridview de lanzamiento


            //Empezando por la última fila del dt, desplazamos una posición abajo cada fila hasta llegar a id
            for (int i = id ; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["ID"] = Convert.ToInt32(dt.Rows[i]["ID"])-n_borrados;

            }

            dgv.DataSource = null;
            dgv.DataSource = dt;
            dgv.Update();



        }
        /*private void Ordenar_dgv(DataGridView item2)
        {
            DataTable dt = item2.DataSource as DataTable;
            //Creamos un dataTable a partir del datagridview de lanzamiento

            int id = 0;

            //Empezando por la última fila del dt, desplazamos una posición abajo cada fila hasta llegar a id
            for (int i = dt.Rows.Count-1; i >= id; i--)
            {     
                dt.Rows[i]["ID"] = i+1;

            }
   
            dgv.DataSource = null;
            dgv.DataSource = dt;
            dgv.Update();
        }*/

     

        private void aniadir_filas_Click(object sender, EventArgs e)
        {
            
            if (filas_nuevas != null)
            {
                //Recorremos el array de filas que hay que añadir
                if (filas_nuevas.Count > 0)
                {
                    //Creamos una lista de listas de filas
                    //Esto se hace por si se hubieran añadido filas con los siguientes ids 2,3,5,10,11,14,17
                    //Entonces debemos añadirlas en grupos ordenados y empezando por el id más grande
                    //Sería: {17}, {14},  
                    List<List<DataGridViewRow>> list_r = new List<List<DataGridViewRow>>();
                    list_r.Add(new List < DataGridViewRow > ());

                    //número de listas que contendrá list_r
                    int n_list = 0;


                    //recorremos las filas seleccionadas para añadir
                    for (int i= filas_nuevas.Count - 1; i>=0; i--)
                    {
                        if (i > 0)
                        {
                            //mientras que los valores sean consecutivos, añadimos todo en la misma posición
                            if ((Convert.ToInt32(filas_nuevas[i].Cells["ID"].Value) + 1) == Convert.ToInt32(filas_nuevas[i - 1].Cells["ID"].Value))
                            {

                                list_r[n_list].Add(filas_nuevas[i]);
                            }
                            //sino, creamos una lista nueva y avanzamos n_list
                            else
                            {                            
                                list_r[n_list].Add(filas_nuevas[i]);
                                list_r.Add(new List<DataGridViewRow>());
                                n_list++;
                            }
                        }
                        else
                        {                          
                           list_r[n_list].Add(filas_nuevas[i]);
                        }

                    }
                    //posición actual en la lista
                    int pos=0;
                    //posición con el id más bajo
                    int min=0;

                    //O(n^2)
                    //Para cada posición en la lista la comparamos con todas las demás
                    //En cada recorrido obtenemos el grupo con el id más bajo y lo añadimos al dgv hasta que list_r quede vacía
                    while (list_r.Count > 0)
                    {
                        //Si no hemos llegado al final, comparamos el id de la primar fila de pos con min, si pos<min, min=pos
                        if (pos != list_r.Count - 1)
                        {
                            if (Convert.ToInt32(list_r[min].First().Cells["ID"].Value) >Convert.ToInt32(list_r[pos].First().Cells["ID"].Value))
                            {
                                min = pos;
                            }
                        }
                        //Si hemos terminado de recorrer la lista, añadimos el minimo obtenido list_r[min]
                        else
                        {
                            Aniadir_List_Filas_dgv(dgvSelected().Item2, list_r[min],true);
                            list_r.RemoveAt(min);
                            min = 0;

                        }
                        //mientras queden elementos, poss++
                        if (list_r.Count>0) {
                            pos++;
                            //aseguramos no acceder a una posición fuera de list_r
                            pos = pos % list_r.Count;
                        }

                    }
                    List<DataGridViewRow> filas = new List<DataGridViewRow>();
                    foreach (DataGridViewRow r in filas_nuevas) filas.Add(r);

                    //Console.WriteLine("FILAS ANIADIDAS");
                    //foreach(DataGridViewRow f in filas_nuevas) { Console.Write(f.Cells[0].Value+" "); }
                    filas_nuevas = null;
                    
                }
            }
        }
        private void aniadir_filas_Click(List<DataGridViewRow> fn)
        {

            if (fn != null)
            {
                //Recorremos el array de filas que hay que añadir
                if (fn.Count > 0)
                {
                    //Creamos una lista de listas de filas
                    //Esto se hace por si se hubieran añadido filas con los siguientes ids 2,3,5,10,11,14,17
                    //Entonces debemos añadirlas en grupos ordenados y empezando por el id más grande
                    //Sería: {17}, {14},  
                    List<List<DataGridViewRow>> list_r = new List<List<DataGridViewRow>>();
                    list_r.Add(new List<DataGridViewRow>());

                    //número de listas que contendrá list_r
                    int n_list = 0;


                    //recorremos las filas seleccionadas para añadir
                    for (int i = fn.Count - 1; i >= 0; i--)
                    {
                        if (i > 0)
                        {
                            //mientras que los valores sean consecutivos, añadimos todo en la misma posición
                            if ((Convert.ToInt32(fn[i].Cells[0].Value) + 1) == Convert.ToInt32(fn[i - 1].Cells[0].Value))
                            {

                                list_r[n_list].Add(fn[i]);
                            }
                            //sino, creamos una lista nueva y avanzamos n_list
                            else
                            {
                                list_r[n_list].Add(fn[i]);
                                list_r.Add(new List<DataGridViewRow>());
                                n_list++;
                            }
                        }
                        else
                        {
                            list_r[n_list].Add(fn[i]);
                        }

                    }
                    //posición actual en la lista
                    int pos = 0;
                    //posición con el id más bajo
                    int min = 0;

                    //O(n^2)
                    //Para cada posición en la lista la comparamos con todas las demás
                    //En cada recorrido obtenemos el grupo con el id más bajo y lo añadimos al dgv hasta que list_r quede vacía
                    while (list_r.Count > 0)
                    {
                        //Si no hemos llegado al final, comparamos el id de la primar fila de pos con min, si pos<min, min=pos
                        if (pos != list_r.Count - 1)
                        {
                            if (Convert.ToInt32(list_r[min].First().Cells[0].Value) > Convert.ToInt32(list_r[pos].First().Cells[0].Value))
                            {
                                min = pos;
                            }
                        }
                        //Si hemos terminado de recorrer la lista, añadimos el minimo obtenido list_r[min]
                        else
                        {
                            Aniadir_List_Filas_dgv(dgvSelected().Item2, list_r[min], false);
                            list_r.RemoveAt(min);
                            min = 0;

                        }
                        //mientras queden elementos, poss++
                        if (list_r.Count > 0)
                        {
                            pos++;
                            //aseguramos no acceder a una posición fuera de list_r
                            pos = pos % list_r.Count;
                        }

                    }
                }
            }
        }
        private void Aniadir_List_Filas_dgv(DataGridView item2, List<DataGridViewRow> list_r, bool pertenece)
        {
            int n_añadidos = list_r.Count;
            //Creamos un dataTable a partir del datagridview de lanzamiento
            DataTable dt = item2.DataSource as DataTable;
           
            int id = 0;
            

            for(int jlist=0; list_r.Count>0 && jlist < list_r.Count; jlist++)
            {
                for (int ilist = 0; list_r.Count > 0 && ilist < list_r.Count; ilist++)
                {
                    if(ilist!= jlist && 
                       Convert.ToString(list_r[ilist].Cells[1].Value) == Convert.ToString(list_r[jlist].Cells[1].Value))
                    {
                        list_r.Remove(list_r[jlist]);
                    }
                }
            }


            for(int ilist=0; list_r.Count>0 && ilist<list_r.Count; ilist++) { 

                for (int rows = 0; list_r.Count > 0 && rows < item2.Rows.Count; rows++)
                {
                    if ( item2.Rows[rows].Cells[1].Value == list_r[ilist].Cells[1].Value)
                    {
                        list_r.Remove(list_r[ilist]);
                        MessageBox.Show("Ya existe un pedido con ese ID Lanz.");

                    }
                }
            }
            if (list_r.Count > 0)
            {

                //si el primer ID de la lista ordenada a añadir es más grande que el último del dgv, insertamos al final
                if (Convert.ToInt32(list_r.First().Cells[0].Value) > Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["ID"]))
                {
                    id = dt.Rows.Count - 1;
                }
                else
                {
                    //Recorremos las filas del dt para obtener la fila dónde vamos a insertar el nuevo producto
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Si el ID de la fila es igual al ID_Orden introducido, será la fila i a insertar
                        if (Convert.ToString(list_r.First().Cells[0].Value) == Convert.ToString(dt.Rows[i]["ID"])) id = i;
                    }
                    int aux;

                    //Empezando por la última fila del dt, desplazamos una posición abajo cada fila hasta llegar a id
                    for (int i = dt.Rows.Count - 1; i >= id; i--)
                    {
                        aux = Convert.ToInt32(dt.Rows[i]["ID"]) + list_r.Count;
                        dt.Rows[i]["ID"] = aux;

                    }
                }

                //Creamos la fila con los datos introducidos
                for (int i = list_r.Count - 1; i >= 0; i--)
                {

                    DataGridViewRow r = list_r[i];
                    DataRow newdr = dt.NewRow();

                    if (pertenece)
                    {
                        newdr["ID"] = r.Cells["ID"].Value;
                        newdr["ID Lanz"] = r.Cells["ID Lanz"].Value;
                        newdr["CÓDIGO"] = r.Cells["CÓDIGO"].Value;
                        newdr["CLIENTE"] = r.Cells["CLIENTE"].Value;
                        newdr["ORDEN"] = r.Cells["ORDEN"].Value;
                        newdr["CAJAS"] = r.Cells["CAJAS"].Value;
                        newdr["FORM."] = r.Cells["FORM."].Value;
                        newdr["PRODUCTO"] = r.Cells["PRODUCTO"].Value;
                        newdr["PA"] = r.Cells["PA"].Value;
                        newdr["REF."] = r.Cells["REF."].Value;
                        newdr["GDO."] = r.Cells["GDO."].Value;
                        newdr["TIPO"] = r.Cells["TIPO"].Value;
                        newdr["Comentarios"] = r.Cells["Comentarios"].Value;
                        newdr["LÍQUIDOS"] = r.Cells["LÍQUIDOS"].Value;
                        newdr["OBSERVACIONES LAB"] = r.Cells["OBSERVACIONES LAB"].Value;
                        newdr["MATERIALES"] = r.Cells["MATERIALES"].Value;
                        newdr["ESTADO"] = r.Cells["ESTADO"].Value;
                        newdr["FECHA INICIO"] = r.Cells["FECHA INICIO"].Value;
                        newdr["OBSERVACIONES PROD."] = r.Cells["OBSERVACIONES PROD."].Value;

                    }
                    else
                    {
                        newdr["ID"] = r.Cells[0].Value;
                        newdr["ID Lanz"] = r.Cells[1].Value;
                        newdr["CÓDIGO"] = r.Cells[2].Value;
                        newdr["CLIENTE"] = r.Cells[4].Value;
                        newdr["ORDEN"] = r.Cells[3].Value;
                        newdr["CAJAS"] = r.Cells[6].Value;
                        newdr["FORM."] = r.Cells[7].Value;
                        newdr["PRODUCTO"] = r.Cells[5].Value;
                        newdr["PA"] = r.Cells[8].Value;
                        newdr["REF."] = r.Cells[9].Value;
                        newdr["GDO."] = r.Cells[10].Value;
                        newdr["TIPO"] = r.Cells[11].Value;
                        newdr["Comentarios"] = r.Cells[12].Value;
                        newdr["LÍQUIDOS"] = r.Cells[13].Value;
                        newdr["OBSERVACIONES LAB"] = r.Cells[14].Value;
                        newdr["MATERIALES"] = r.Cells[15].Value;
                        newdr["ESTADO"] = r.Cells[16].Value;
                        newdr["FECHA INICIO"] = r.Cells[17].Value;
                        newdr["OBSERVACIONES PROD."] = r.Cells[18].Value;

                    }

                    Nuevos_ID_Lanz.Add(Convert.ToString(newdr["ID Lanz"]));
                    dt.Rows.InsertAt(newdr, id);

                }
                item2.DataSource = null;
                item2.DataSource = dt;
                cambios[dgvSelected().Item1].cambios_realizados.Add(new cambios(operaciones.ANIADIR, list_r));

                item2.Update();
            }
        }

        private void deshacer_camios_Click(object sender, EventArgs e)
        {

            if (cambios[dgvSelected().Item1].cambios_realizados.Count > 0)
            {
                //PROBANDO CON LIST
                List<DataGridViewRow> filas_borradas_añadidas;

                operaciones op = cambios[dgvSelected().Item1].cambios_realizados.Last().op;

                if (cambios[dgvSelected().Item1].cambios_realizados.Count > 0)
                {
                    filas_borradas_añadidas = cambios[dgvSelected().Item1].cambios_realizados.Last().filas;

                    if (op == operaciones.BORRAR)
                    {
                        aniadir_filas_Click(filas_borradas_añadidas);
                    }
                    else if(op == operaciones.ANIADIR)
                    {
                     //   borrar_filas_lanz_Click(filas_borradas_añadidas);

                        foreach (DataGridViewRow row in filas_borradas_añadidas)
                        {                        
                            foreach (DataGridViewRow lookfr in dgvSelected().Item2.Rows)
                            {
                                if (Convert.ToString(lookfr.Cells["ID"].Value) == Convert.ToString(row.Cells[0].Value))
                                {
                                    if (op == operaciones.ANIADIR)
                                    {
                                        //borrar_filas_lanz_Click();
                                        dgvSelected().Item2.Rows.Remove(lookfr);
                                    }

                                }
                            }
                        }
                    }
                }
               //if (cambios_deshechos) Ordenar_dgv(dgvSelected().Item2);
               //cambios[dgvSelected().Item1].cambios_realizados.Remove(filas_borradas_añadidas);
                this.cambios[dgvSelected().Item1].cambios_realizados.Remove(cambios[dgvSelected().Item1].cambios_realizados.Last());
            }
        }

    

        private void borrar_filas_lanz_Click(object sender, EventArgs e)
        {
            if (dgvSelected().Item2.Rows.Count>1)
            {
                int n_linea = dgvSelected().Item1;
                int n_borrados = 0;
                int ultimo_indice = 0;

                DataGridViewSelectedRowCollection f_a_borrar;
                List<DataGridViewRow> filas = new List<DataGridViewRow>();

                f_a_borrar = (n_linea == 0) ? filas_borrarl2 : filas_borrarl3;
                f_a_borrar = (n_linea == 1) ? filas_borrarl3 : f_a_borrar;
                f_a_borrar = (n_linea == 2) ? filas_borrarl5 : f_a_borrar;


                if (f_a_borrar != null)
                {

                    //recorremos las filas a borrar
                    foreach (DataGridViewRow r in f_a_borrar)
                    {
                        if (r == f_a_borrar[f_a_borrar.Count - 1]) ultimo_indice = Convert.ToInt32(r.Cells[0].Value);
                        //si se encuentran en el dgv seleccionado, borramos
                        if (dgvSelected().Item2.Rows.Contains(r))
                        {

                            DataGridViewRow newdr = new DataGridViewRow();
                            int i = 0;
                            foreach(DataGridViewCell c in r.Cells)
                            {
                                newdr.Cells.Add(new DataGridViewTextBoxCell { });
                                newdr.Cells[i].Value = r.Cells[i].Value;
                                i++;
                            }
                            filas.Add(newdr);

                            dgvSelected().Item2.Rows.Remove(r);
                            n_borrados++;



                        }

                    }
                    --ultimo_indice;
                    RestarID(ultimo_indice, n_borrados, dgvSelected().Item2);
                    if (n_linea == 0) filas_borrarl2 = dgvSelected().Item2.SelectedRows;
                    if (n_linea == 1) filas_borrarl3 = dgvSelected().Item2.SelectedRows;
                    if (n_linea == 2) filas_borrarl3 = dgvSelected().Item2.SelectedRows;
                    cambios[n_linea].cambios_realizados.Add(new cambios(operaciones.BORRAR, filas));
                }
            }
        }
        private void borrar_filas_lanz_Click(List<DataGridViewRow> fb)
        {
            if (dgvSelected().Item2.Rows.Count > 1)
            {
                int n_linea = dgvSelected().Item1;
                int n_borrados = 0;
                int ultimo_indice = 0;

                if (fb != null)
                {

                    //recorremos las filas a borrar
                    foreach (DataGridViewRow r in fb)
                    {
                        if (r == fb[fb.Count - 1]) ultimo_indice = Convert.ToInt32(r.Cells[0].Value);
                        //si se encuentran en el dgv seleccionado, borramos
                       // if (dgvSelected().Item2.Rows.Contains(r))
                      //  {

                            dgvSelected().Item2.Rows.Remove(r);
                            n_borrados++;
                      //  }

                    }
                    --ultimo_indice;
                    RestarID(ultimo_indice, n_borrados, dgvSelected().Item2);
                    if (n_linea == 0) filas_borrarl2 = dgvSelected().Item2.SelectedRows;
                    if (n_linea == 1) filas_borrarl3 = dgvSelected().Item2.SelectedRows;
                    if (n_linea == 2) filas_borrarl3 = dgvSelected().Item2.SelectedRows;
                }
            }
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {

            filas_nuevas = dataGridView1.SelectedRows;

        }


        private void dataGridViewL2_MouseUp_1(object sender, MouseEventArgs e)
        {
          
            filas_borrarl2 = dataGridViewL2.SelectedRows;

        }
       
        private void dataGridViewL3_MouseUp(object sender, MouseEventArgs e)
        {
            filas_borrarl3 = dataGridViewL3.SelectedRows;

        }
        private void dataGridViewL5_MouseUp(object sender, MouseEventArgs e)
        {
            filas_borrarl5 = dataGridViewL5.SelectedRows;
        }

        private void guardar_lanz_boton_Click(object sender, EventArgs e)
        {
            
            foreach (DataGridViewRow row in dgvSelected().Item2.Rows)
            {
                if(row.Cells[0].Style.BackColor==Color.Yellow || row.Cells[0].Style.BackColor == Color.Red)
                {
                    Console.WriteLine("linea con colores cambiadas");
                }
            }
            int r = ExcelUtiles.ExportDtToExcel(Nuevos_ID_Lanz, dgvSelected().Item2.DataSource as DataTable, MaquinaLinea.FileLanzador, "Linea " + MaquinaLinea.numlin);

            
        }
        //Evento que cambia los valores de las celdas ESTADO, LIQUIDOS y MATERIALES haciendo un doble click sobre ellas
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name=="ESTADO"){

                if (Convert.ToString(dataGridView1.CurrentCell.Value) == "Iniciado")
                {
                    dataGridView1.CurrentCell.Value = "Completado";
                }
                else if (Convert.ToString(dataGridView1.CurrentCell.Value) == "Completado")
                {
                    dataGridView1.CurrentCell.Value = "Pendiente";
                }
                else if (Convert.ToString(dataGridView1.CurrentCell.Value) == "Pendiente")
                {
                    dataGridView1.CurrentCell.Value = "Sin Terminar";
                }
                else if (Convert.ToString(dataGridView1.CurrentCell.Value) == "Sin Terminar")
                {
                    dataGridView1.CurrentCell.Value = "Saltado";
                }
                else if (Convert.ToString(dataGridView1.CurrentCell.Value) == "Saltado")
                {
                    dataGridView1.CurrentCell.Value = "Iniciado";
                }
                else
                {
                    dataGridView1.CurrentCell.Value = "Iniciado";

                }



            }
            else if(dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name == "MATERIALES"){
               
                if (Convert.ToString(dataGridView1.CurrentCell.Value) == "OK")
                {
                    dataGridView1.CurrentCell.Value = "NOK";
                }
                else if (Convert.ToString(dataGridView1.CurrentCell.Value) == "NOK")
                {
                    dataGridView1.CurrentCell.Value = "PENDIENTE";
                }
                else if (Convert.ToString(dataGridView1.CurrentCell.Value) == "PENDIENTE")
                {
                    dataGridView1.CurrentCell.Value = "OK";
                }
                else
                {
                    dataGridView1.CurrentCell.Value = "OK";

                }

            }
            else if(dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name == "LÍQUIDOS"){
               
                if (Convert.ToString(dataGridView1.CurrentCell.Value) == "OK")
                {
                    dataGridView1.CurrentCell.Value = "NOK";
                }
                else if (Convert.ToString(dataGridView1.CurrentCell.Value) == "NOK")
                {
                    dataGridView1.CurrentCell.Value = "ELABORACIÓN";
                }
                else if (Convert.ToString(dataGridView1.CurrentCell.Value) == "ELABORACIÓN")
                {
                    dataGridView1.CurrentCell.Value = "OK";
                }
                else
                {
                    dataGridView1.CurrentCell.Value = "OK";

                }
            }

        }

        //Función que mantiene los formatos para las celdas de cada fila cuándo añadimos nuevas filas al dgv
        private void DesplazarFormatos(DataGridView dgv, int nfilas, int id) {
            for(int i=id; i<id+nfilas; ++id) dgv.Rows[id].DefaultCellStyle.BackColor = Color.Yellow;
           
        }
    }
}

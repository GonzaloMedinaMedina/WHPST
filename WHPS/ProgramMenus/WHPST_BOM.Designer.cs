namespace WHPS.ProgramMenus
{
    partial class WHPST_BOM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WHPST_BOM));
            this.RefTB = new System.Windows.Forms.TextBox();
            this.BuscadorBOX = new System.Windows.Forms.GroupBox();
            this.lbReloj = new System.Windows.Forms.Label();
            this.DescripcionTB = new System.Windows.Forms.TextBox();
            this.DescripcionProductoLB = new System.Windows.Forms.Label();
            this.BuscarB = new System.Windows.Forms.Button();
            this.ReferenciaProductoLB = new System.Windows.Forms.Label();
            this.BuscadorLB = new System.Windows.Forms.Label();
            this.ListaComponentesBOX = new System.Windows.Forms.GroupBox();
            this.dataGridViewBOM = new System.Windows.Forms.DataGridView();
            this.ListaComponentesLB = new System.Windows.Forms.Label();
            this.DocumentosProductoBOX = new System.Windows.Forms.GroupBox();
            this.DocumentacionProductoListBox = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ImagenesBOX = new System.Windows.Forms.GroupBox();
            this.Imagen = new System.Windows.Forms.PictureBox();
            this.ImagenLB = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.DocumentosMaterialBOX = new System.Windows.Forms.GroupBox();
            this.DocumentacionMaterialListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ExitB = new System.Windows.Forms.Button();
            this.EdicionB = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.MinimizarB = new System.Windows.Forms.Button();
            this.BajarB = new System.Windows.Forms.Button();
            this.SubirB = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BusquedaMaterialB = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.BuscadorBOX.SuspendLayout();
            this.ListaComponentesBOX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBOM)).BeginInit();
            this.DocumentosProductoBOX.SuspendLayout();
            this.ImagenesBOX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Imagen)).BeginInit();
            this.DocumentosMaterialBOX.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // RefTB
            // 
            this.RefTB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.RefTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefTB.Location = new System.Drawing.Point(510, 46);
            this.RefTB.Name = "RefTB";
            this.RefTB.Size = new System.Drawing.Size(162, 26);
            this.RefTB.TabIndex = 0;
            this.RefTB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RefTB_MouseClick);
            this.RefTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RefTB_KeyDown);
            // 
            // BuscadorBOX
            // 
            this.BuscadorBOX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BuscadorBOX.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BuscadorBOX.Controls.Add(this.lbReloj);
            this.BuscadorBOX.Controls.Add(this.DescripcionTB);
            this.BuscadorBOX.Controls.Add(this.DescripcionProductoLB);
            this.BuscadorBOX.Controls.Add(this.BuscarB);
            this.BuscadorBOX.Controls.Add(this.RefTB);
            this.BuscadorBOX.Controls.Add(this.ReferenciaProductoLB);
            this.BuscadorBOX.Controls.Add(this.BuscadorLB);
            this.BuscadorBOX.Location = new System.Drawing.Point(0, 0);
            this.BuscadorBOX.Name = "BuscadorBOX";
            this.BuscadorBOX.Size = new System.Drawing.Size(1040, 122);
            this.BuscadorBOX.TabIndex = 27;
            this.BuscadorBOX.TabStop = false;
            // 
            // lbReloj
            // 
            this.lbReloj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbReloj.BackColor = System.Drawing.Color.White;
            this.lbReloj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReloj.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbReloj.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbReloj.Location = new System.Drawing.Point(970, 8);
            this.lbReloj.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbReloj.Name = "lbReloj";
            this.lbReloj.Size = new System.Drawing.Size(68, 18);
            this.lbReloj.TabIndex = 30;
            this.lbReloj.Text = "00:00:00";
            this.lbReloj.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DescripcionTB
            // 
            this.DescripcionTB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DescripcionTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescripcionTB.Location = new System.Drawing.Point(510, 76);
            this.DescripcionTB.Name = "DescripcionTB";
            this.DescripcionTB.Size = new System.Drawing.Size(162, 26);
            this.DescripcionTB.TabIndex = 28;
            this.DescripcionTB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DescripcionTB_MouseClick);
            this.DescripcionTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DescripcionTB_KeyDown);
            // 
            // DescripcionProductoLB
            // 
            this.DescripcionProductoLB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DescripcionProductoLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescripcionProductoLB.Location = new System.Drawing.Point(232, 73);
            this.DescripcionProductoLB.Name = "DescripcionProductoLB";
            this.DescripcionProductoLB.Size = new System.Drawing.Size(272, 25);
            this.DescripcionProductoLB.TabIndex = 29;
            this.DescripcionProductoLB.Text = "Descripción del producto:";
            this.DescripcionProductoLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BuscarB
            // 
            this.BuscarB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BuscarB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.BuscarB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BuscarB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BuscarB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuscarB.ForeColor = System.Drawing.Color.White;
            this.BuscarB.Image = global::WHPS.Properties.Resources.GenLupa50x50;
            this.BuscarB.Location = new System.Drawing.Point(723, 13);
            this.BuscarB.Margin = new System.Windows.Forms.Padding(2);
            this.BuscarB.Name = "BuscarB";
            this.BuscarB.Size = new System.Drawing.Size(100, 100);
            this.BuscarB.TabIndex = 27;
            this.BuscarB.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BuscarB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BuscarB.UseVisualStyleBackColor = false;
            this.BuscarB.Click += new System.EventHandler(this.BuscarB_Click);
            // 
            // ReferenciaProductoLB
            // 
            this.ReferenciaProductoLB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ReferenciaProductoLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReferenciaProductoLB.Location = new System.Drawing.Point(238, 43);
            this.ReferenciaProductoLB.Name = "ReferenciaProductoLB";
            this.ReferenciaProductoLB.Size = new System.Drawing.Size(266, 25);
            this.ReferenciaProductoLB.TabIndex = 26;
            this.ReferenciaProductoLB.Text = "Referencia del producto:";
            this.ReferenciaProductoLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BuscadorLB
            // 
            this.BuscadorLB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BuscadorLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.BuscadorLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuscadorLB.ForeColor = System.Drawing.Color.White;
            this.BuscadorLB.Location = new System.Drawing.Point(0, 6);
            this.BuscadorLB.Name = "BuscadorLB";
            this.BuscadorLB.Size = new System.Drawing.Size(320, 22);
            this.BuscadorLB.TabIndex = 25;
            this.BuscadorLB.Text = "BUSCADOR";
            this.BuscadorLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ListaComponentesBOX
            // 
            this.ListaComponentesBOX.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ListaComponentesBOX.Controls.Add(this.dataGridViewBOM);
            this.ListaComponentesBOX.Controls.Add(this.ListaComponentesLB);
            this.ListaComponentesBOX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListaComponentesBOX.Location = new System.Drawing.Point(0, 0);
            this.ListaComponentesBOX.Name = "ListaComponentesBOX";
            this.ListaComponentesBOX.Size = new System.Drawing.Size(866, 377);
            this.ListaComponentesBOX.TabIndex = 28;
            this.ListaComponentesBOX.TabStop = false;
            // 
            // dataGridViewBOM
            // 
            this.dataGridViewBOM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewBOM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewBOM.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewBOM.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewBOM.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewBOM.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewBOM.ColumnHeadersHeight = 25;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewBOM.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewBOM.EnableHeadersVisualStyles = false;
            this.dataGridViewBOM.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.dataGridViewBOM.Location = new System.Drawing.Point(3, 38);
            this.dataGridViewBOM.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewBOM.Name = "dataGridViewBOM";
            this.dataGridViewBOM.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Goldenrod;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewBOM.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewBOM.RowTemplate.Height = 24;
            this.dataGridViewBOM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBOM.Size = new System.Drawing.Size(860, 335);
            this.dataGridViewBOM.TabIndex = 73;
            this.dataGridViewBOM.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBOM_CellClick);
            this.dataGridViewBOM.SelectionChanged += new System.EventHandler(this.dataGridViewBOM_SelectionChanged);
            // 
            // ListaComponentesLB
            // 
            this.ListaComponentesLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.ListaComponentesLB.Dock = System.Windows.Forms.DockStyle.Top;
            this.ListaComponentesLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListaComponentesLB.ForeColor = System.Drawing.Color.White;
            this.ListaComponentesLB.Location = new System.Drawing.Point(3, 16);
            this.ListaComponentesLB.Name = "ListaComponentesLB";
            this.ListaComponentesLB.Size = new System.Drawing.Size(860, 23);
            this.ListaComponentesLB.TabIndex = 25;
            this.ListaComponentesLB.Text = "LISTA DE COMPONENTES";
            this.ListaComponentesLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DocumentosProductoBOX
            // 
            this.DocumentosProductoBOX.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DocumentosProductoBOX.Controls.Add(this.DocumentacionProductoListBox);
            this.DocumentosProductoBOX.Controls.Add(this.label6);
            this.DocumentosProductoBOX.Dock = System.Windows.Forms.DockStyle.Right;
            this.DocumentosProductoBOX.Location = new System.Drawing.Point(370, 0);
            this.DocumentosProductoBOX.Name = "DocumentosProductoBOX";
            this.DocumentosProductoBOX.Size = new System.Drawing.Size(670, 128);
            this.DocumentosProductoBOX.TabIndex = 29;
            this.DocumentosProductoBOX.TabStop = false;
            // 
            // DocumentacionProductoListBox
            // 
            this.DocumentacionProductoListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentacionProductoListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DocumentacionProductoListBox.FormattingEnabled = true;
            this.DocumentacionProductoListBox.ItemHeight = 20;
            this.DocumentacionProductoListBox.Location = new System.Drawing.Point(3, 38);
            this.DocumentacionProductoListBox.Margin = new System.Windows.Forms.Padding(2);
            this.DocumentacionProductoListBox.Name = "DocumentacionProductoListBox";
            this.DocumentacionProductoListBox.Size = new System.Drawing.Size(664, 87);
            this.DocumentacionProductoListBox.TabIndex = 26;
            this.DocumentacionProductoListBox.SelectedIndexChanged += new System.EventHandler(this.DocumentacionProductoListBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(664, 22);
            this.label6.TabIndex = 25;
            this.label6.Text = "DOCUMENTOS DEL PRODUCTO";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ImagenesBOX
            // 
            this.ImagenesBOX.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ImagenesBOX.Controls.Add(this.Imagen);
            this.ImagenesBOX.Controls.Add(this.ImagenLB);
            this.ImagenesBOX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImagenesBOX.Location = new System.Drawing.Point(0, 0);
            this.ImagenesBOX.Name = "ImagenesBOX";
            this.ImagenesBOX.Size = new System.Drawing.Size(174, 377);
            this.ImagenesBOX.TabIndex = 29;
            this.ImagenesBOX.TabStop = false;
            // 
            // Imagen
            // 
            this.Imagen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Imagen.Location = new System.Drawing.Point(3, 38);
            this.Imagen.Name = "Imagen";
            this.Imagen.Size = new System.Drawing.Size(168, 336);
            this.Imagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Imagen.TabIndex = 26;
            this.Imagen.TabStop = false;
            this.Imagen.DoubleClick += new System.EventHandler(this.Imagen_DoubleClick);
            // 
            // ImagenLB
            // 
            this.ImagenLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.ImagenLB.Dock = System.Windows.Forms.DockStyle.Top;
            this.ImagenLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImagenLB.ForeColor = System.Drawing.Color.White;
            this.ImagenLB.Location = new System.Drawing.Point(3, 16);
            this.ImagenLB.Name = "ImagenLB";
            this.ImagenLB.Size = new System.Drawing.Size(168, 22);
            this.ImagenLB.TabIndex = 25;
            this.ImagenLB.Text = "IMAGEN";
            this.ImagenLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "6301550004.jpg");
            this.imageList1.Images.SetKeyName(1, "6303020001.jpg");
            this.imageList1.Images.SetKeyName(2, "6801000111.jpg");
            this.imageList1.Images.SetKeyName(3, "SinImagen.jpg");
            // 
            // DocumentosMaterialBOX
            // 
            this.DocumentosMaterialBOX.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DocumentosMaterialBOX.Controls.Add(this.DocumentacionMaterialListBox);
            this.DocumentosMaterialBOX.Controls.Add(this.label1);
            this.DocumentosMaterialBOX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentosMaterialBOX.Location = new System.Drawing.Point(0, 0);
            this.DocumentosMaterialBOX.Name = "DocumentosMaterialBOX";
            this.DocumentosMaterialBOX.Size = new System.Drawing.Size(370, 128);
            this.DocumentosMaterialBOX.TabIndex = 30;
            this.DocumentosMaterialBOX.TabStop = false;
            // 
            // DocumentacionMaterialListBox
            // 
            this.DocumentacionMaterialListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentacionMaterialListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DocumentacionMaterialListBox.FormattingEnabled = true;
            this.DocumentacionMaterialListBox.ItemHeight = 20;
            this.DocumentacionMaterialListBox.Location = new System.Drawing.Point(3, 38);
            this.DocumentacionMaterialListBox.Margin = new System.Windows.Forms.Padding(2);
            this.DocumentacionMaterialListBox.Name = "DocumentacionMaterialListBox";
            this.DocumentacionMaterialListBox.Size = new System.Drawing.Size(364, 87);
            this.DocumentacionMaterialListBox.TabIndex = 26;
            this.DocumentacionMaterialListBox.SelectedIndexChanged += new System.EventHandler(this.DocumentacionMaterialListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(364, 22);
            this.label1.TabIndex = 25;
            this.label1.Text = "DOCUMENTOS DEL MATERIAL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ExitB
            // 
            this.ExitB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExitB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.ExitB.BackgroundImage = global::WHPS.Properties.Resources.GenCasa;
            this.ExitB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExitB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitB.ForeColor = System.Drawing.Color.White;
            this.ExitB.Location = new System.Drawing.Point(10, 15);
            this.ExitB.Margin = new System.Windows.Forms.Padding(2);
            this.ExitB.Name = "ExitB";
            this.ExitB.Size = new System.Drawing.Size(160, 160);
            this.ExitB.TabIndex = 75;
            this.ExitB.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ExitB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ExitB.UseVisualStyleBackColor = false;
            this.ExitB.Click += new System.EventHandler(this.ExitB_Click);
            // 
            // EdicionB
            // 
            this.EdicionB.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.EdicionB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.EdicionB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EdicionB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EdicionB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EdicionB.ForeColor = System.Drawing.Color.White;
            this.EdicionB.Location = new System.Drawing.Point(440, 15);
            this.EdicionB.Margin = new System.Windows.Forms.Padding(2);
            this.EdicionB.Name = "EdicionB";
            this.EdicionB.Size = new System.Drawing.Size(160, 160);
            this.EdicionB.TabIndex = 76;
            this.EdicionB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.EdicionB.UseVisualStyleBackColor = false;
            this.EdicionB.Click += new System.EventHandler(this.EdicionB_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MinimizarB
            // 
            this.MinimizarB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimizarB.BackColor = System.Drawing.Color.Gainsboro;
            this.MinimizarB.BackgroundImage = global::WHPS.Properties.Resources.GenMinimizar;
            this.MinimizarB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MinimizarB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizarB.Location = new System.Drawing.Point(1016, 0);
            this.MinimizarB.Margin = new System.Windows.Forms.Padding(2);
            this.MinimizarB.Name = "MinimizarB";
            this.MinimizarB.Size = new System.Drawing.Size(24, 24);
            this.MinimizarB.TabIndex = 101;
            this.MinimizarB.UseVisualStyleBackColor = false;
            this.MinimizarB.Visible = false;
            this.MinimizarB.Click += new System.EventHandler(this.MinimizarB_Click);
            // 
            // BajarB
            // 
            this.BajarB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BajarB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.BajarB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BajarB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BajarB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BajarB.ForeColor = System.Drawing.Color.White;
            this.BajarB.Location = new System.Drawing.Point(786, 15);
            this.BajarB.Margin = new System.Windows.Forms.Padding(2);
            this.BajarB.Name = "BajarB";
            this.BajarB.Size = new System.Drawing.Size(120, 160);
            this.BajarB.TabIndex = 102;
            this.BajarB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BajarB.UseVisualStyleBackColor = false;
            this.BajarB.Visible = false;
            this.BajarB.Click += new System.EventHandler(this.BajarB_Click);
            // 
            // SubirB
            // 
            this.SubirB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SubirB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.SubirB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SubirB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SubirB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubirB.ForeColor = System.Drawing.Color.White;
            this.SubirB.Location = new System.Drawing.Point(910, 15);
            this.SubirB.Margin = new System.Windows.Forms.Padding(2);
            this.SubirB.Name = "SubirB";
            this.SubirB.Size = new System.Drawing.Size(120, 160);
            this.SubirB.TabIndex = 103;
            this.SubirB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SubirB.UseVisualStyleBackColor = false;
            this.SubirB.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MinimizarB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1040, 24);
            this.panel1.TabIndex = 104;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BuscadorBOX);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1040, 122);
            this.panel2.TabIndex = 105;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BusquedaMaterialB);
            this.panel3.Controls.Add(this.SubirB);
            this.panel3.Controls.Add(this.ExitB);
            this.panel3.Controls.Add(this.EdicionB);
            this.panel3.Controls.Add(this.BajarB);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 651);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1040, 184);
            this.panel3.TabIndex = 106;
            // 
            // BusquedaMaterialB
            // 
            this.BusquedaMaterialB.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BusquedaMaterialB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.BusquedaMaterialB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BusquedaMaterialB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BusquedaMaterialB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BusquedaMaterialB.ForeColor = System.Drawing.Color.White;
            this.BusquedaMaterialB.Location = new System.Drawing.Point(622, 15);
            this.BusquedaMaterialB.Margin = new System.Windows.Forms.Padding(2);
            this.BusquedaMaterialB.Name = "BusquedaMaterialB";
            this.BusquedaMaterialB.Size = new System.Drawing.Size(160, 160);
            this.BusquedaMaterialB.TabIndex = 104;
            this.BusquedaMaterialB.Text = "BUSQUEDA DE PRODUCTOS POR MATERIALES";
            this.BusquedaMaterialB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BusquedaMaterialB.UseVisualStyleBackColor = false;
            this.BusquedaMaterialB.Visible = false;
            this.BusquedaMaterialB.Click += new System.EventHandler(this.BusquedaMaterialB_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.DocumentosMaterialBOX);
            this.panel4.Controls.Add(this.DocumentosProductoBOX);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 523);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1040, 128);
            this.panel4.TabIndex = 107;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 146);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1040, 377);
            this.panel5.TabIndex = 108;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.ImagenesBOX);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(866, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(174, 377);
            this.panel7.TabIndex = 31;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.ListaComponentesBOX);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(866, 377);
            this.panel6.TabIndex = 30;
            // 
            // WHPST_BOM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1040, 835);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WHPST_BOM";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.WHPST_BOM_Load);
            this.BuscadorBOX.ResumeLayout(false);
            this.BuscadorBOX.PerformLayout();
            this.ListaComponentesBOX.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBOM)).EndInit();
            this.DocumentosProductoBOX.ResumeLayout(false);
            this.ImagenesBOX.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Imagen)).EndInit();
            this.DocumentosMaterialBOX.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox RefTB;
        private System.Windows.Forms.GroupBox BuscadorBOX;
        private System.Windows.Forms.Label BuscadorLB;
        private System.Windows.Forms.Label ReferenciaProductoLB;
        private System.Windows.Forms.Button BuscarB;
        private System.Windows.Forms.GroupBox ListaComponentesBOX;
        private System.Windows.Forms.Label ListaComponentesLB;
        private System.Windows.Forms.GroupBox DocumentosProductoBOX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox ImagenesBOX;
        private System.Windows.Forms.Label ImagenLB;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DataGridView dataGridViewBOM;
        private System.Windows.Forms.ListBox DocumentacionProductoListBox;
        private System.Windows.Forms.GroupBox DocumentosMaterialBOX;
        private System.Windows.Forms.ListBox DocumentacionMaterialListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ExitB;
        private System.Windows.Forms.PictureBox Imagen;
        private System.Windows.Forms.TextBox DescripcionTB;
        private System.Windows.Forms.Label DescripcionProductoLB;
        private System.Windows.Forms.Button EdicionB;
        private System.Windows.Forms.Label lbReloj;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button MinimizarB;
        private System.Windows.Forms.Button BajarB;
        private System.Windows.Forms.Button SubirB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button BusquedaMaterialB;
    }
}
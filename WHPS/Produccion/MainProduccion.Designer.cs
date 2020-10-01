namespace WHPS.Produccion
{
    partial class MainProduccion
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
            this.lbReloj = new System.Windows.Forms.Label();
            this.BusDiaTB = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.PanelVertical = new System.Windows.Forms.Panel();
            this.BackB = new System.Windows.Forms.Button();
            this.MenuAdminB = new System.Windows.Forms.PictureBox();
            this.PanelL5 = new System.Windows.Forms.Panel();
            this.L5B = new System.Windows.Forms.Button();
            this.PanelL3 = new System.Windows.Forms.Panel();
            this.L3B = new System.Windows.Forms.Button();
            this.PanelL2 = new System.Windows.Forms.Panel();
            this.L2B = new System.Windows.Forms.Button();
            this.PanelBusqueda = new System.Windows.Forms.Panel();
            this.PanelForm = new System.Windows.Forms.Panel();
            this.MinimizarB = new System.Windows.Forms.Button();
            this.PanelSeparacion2 = new System.Windows.Forms.Panel();
            this.PanelSeparacion1 = new System.Windows.Forms.Panel();
            this.PanelResultados = new System.Windows.Forms.Panel();
            this.PanelBuscador = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.BusDiaFinTB = new System.Windows.Forms.TextBox();
            this.AvisoLB = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BuscarB = new System.Windows.Forms.Button();
            this.PanelCalendario = new System.Windows.Forms.Panel();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PanelTot = new System.Windows.Forms.Panel();
            this.TotalB = new System.Windows.Forms.Button();
            this.PanelVertical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MenuAdminB)).BeginInit();
            this.PanelForm.SuspendLayout();
            this.PanelBuscador.SuspendLayout();
            this.PanelCalendario.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbReloj
            // 
            this.lbReloj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbReloj.BackColor = System.Drawing.Color.White;
            this.lbReloj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReloj.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbReloj.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbReloj.Location = new System.Drawing.Point(1798, 4);
            this.lbReloj.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbReloj.Name = "lbReloj";
            this.lbReloj.Size = new System.Drawing.Size(88, 26);
            this.lbReloj.TabIndex = 21;
            this.lbReloj.Text = "00:00:00";
            this.lbReloj.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BusDiaTB
            // 
            this.BusDiaTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BusDiaTB.Location = new System.Drawing.Point(233, 48);
            this.BusDiaTB.Margin = new System.Windows.Forms.Padding(2);
            this.BusDiaTB.Name = "BusDiaTB";
            this.BusDiaTB.ReadOnly = true;
            this.BusDiaTB.Size = new System.Drawing.Size(115, 30);
            this.BusDiaTB.TabIndex = 1;
            this.BusDiaTB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BusDiaTB_MouseClick);
            this.BusDiaTB.TextChanged += new System.EventHandler(this.BusDiaTB_TextChanged);
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(105, 45);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(124, 32);
            this.label20.TabIndex = 16;
            this.label20.Text = "Dia Inicio:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PanelVertical
            // 
            this.PanelVertical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.PanelVertical.Controls.Add(this.PanelTot);
            this.PanelVertical.Controls.Add(this.TotalB);
            this.PanelVertical.Controls.Add(this.BackB);
            this.PanelVertical.Controls.Add(this.MenuAdminB);
            this.PanelVertical.Controls.Add(this.PanelL5);
            this.PanelVertical.Controls.Add(this.L5B);
            this.PanelVertical.Controls.Add(this.PanelL3);
            this.PanelVertical.Controls.Add(this.L3B);
            this.PanelVertical.Controls.Add(this.PanelL2);
            this.PanelVertical.Controls.Add(this.L2B);
            this.PanelVertical.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelVertical.Location = new System.Drawing.Point(0, 0);
            this.PanelVertical.Margin = new System.Windows.Forms.Padding(2);
            this.PanelVertical.Name = "PanelVertical";
            this.PanelVertical.Size = new System.Drawing.Size(92, 1080);
            this.PanelVertical.TabIndex = 43;
            // 
            // BackB
            // 
            this.BackB.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BackB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.BackB.BackgroundImage = global::WHPS.Properties.Resources.MenuCasa50x50;
            this.BackB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackB.FlatAppearance.BorderSize = 0;
            this.BackB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackB.Location = new System.Drawing.Point(21, 1014);
            this.BackB.Name = "BackB";
            this.BackB.Size = new System.Drawing.Size(50, 50);
            this.BackB.TabIndex = 42;
            this.BackB.UseVisualStyleBackColor = false;
            this.BackB.Click += new System.EventHandler(this.BackB_Click);
            // 
            // MenuAdminB
            // 
            this.MenuAdminB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MenuAdminB.BackgroundImage = global::WHPS.Properties.Resources.GenLupa;
            this.MenuAdminB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MenuAdminB.Location = new System.Drawing.Point(16, 11);
            this.MenuAdminB.Margin = new System.Windows.Forms.Padding(2);
            this.MenuAdminB.Name = "MenuAdminB";
            this.MenuAdminB.Size = new System.Drawing.Size(55, 55);
            this.MenuAdminB.TabIndex = 40;
            this.MenuAdminB.TabStop = false;
            this.MenuAdminB.Click += new System.EventHandler(this.MenuAdminB_Click);
            // 
            // PanelL5
            // 
            this.PanelL5.BackColor = System.Drawing.Color.Maroon;
            this.PanelL5.Location = new System.Drawing.Point(1, 257);
            this.PanelL5.Margin = new System.Windows.Forms.Padding(2);
            this.PanelL5.Name = "PanelL5";
            this.PanelL5.Size = new System.Drawing.Size(5, 70);
            this.PanelL5.TabIndex = 24;
            // 
            // L5B
            // 
            this.L5B.FlatAppearance.BorderSize = 0;
            this.L5B.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SlateGray;
            this.L5B.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this.L5B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.L5B.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L5B.ForeColor = System.Drawing.Color.White;
            this.L5B.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.L5B.Location = new System.Drawing.Point(8, 259);
            this.L5B.Margin = new System.Windows.Forms.Padding(2);
            this.L5B.Name = "L5B";
            this.L5B.Size = new System.Drawing.Size(80, 70);
            this.L5B.TabIndex = 23;
            this.L5B.Text = "L5";
            this.L5B.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.L5B.UseVisualStyleBackColor = false;
            this.L5B.Click += new System.EventHandler(this.L5B_Click);
            // 
            // PanelL3
            // 
            this.PanelL3.BackColor = System.Drawing.Color.Maroon;
            this.PanelL3.Location = new System.Drawing.Point(1, 171);
            this.PanelL3.Margin = new System.Windows.Forms.Padding(2);
            this.PanelL3.Name = "PanelL3";
            this.PanelL3.Size = new System.Drawing.Size(5, 70);
            this.PanelL3.TabIndex = 22;
            // 
            // L3B
            // 
            this.L3B.FlatAppearance.BorderSize = 0;
            this.L3B.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SlateGray;
            this.L3B.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this.L3B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.L3B.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L3B.ForeColor = System.Drawing.Color.White;
            this.L3B.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.L3B.Location = new System.Drawing.Point(8, 171);
            this.L3B.Margin = new System.Windows.Forms.Padding(2);
            this.L3B.Name = "L3B";
            this.L3B.Size = new System.Drawing.Size(80, 70);
            this.L3B.TabIndex = 21;
            this.L3B.Text = "L3";
            this.L3B.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.L3B.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.L3B.UseVisualStyleBackColor = false;
            this.L3B.Click += new System.EventHandler(this.L3B_Click);
            // 
            // PanelL2
            // 
            this.PanelL2.BackColor = System.Drawing.Color.Maroon;
            this.PanelL2.Location = new System.Drawing.Point(1, 85);
            this.PanelL2.Margin = new System.Windows.Forms.Padding(2);
            this.PanelL2.Name = "PanelL2";
            this.PanelL2.Size = new System.Drawing.Size(5, 70);
            this.PanelL2.TabIndex = 20;
            // 
            // L2B
            // 
            this.L2B.FlatAppearance.BorderSize = 0;
            this.L2B.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SlateGray;
            this.L2B.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this.L2B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.L2B.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L2B.ForeColor = System.Drawing.Color.White;
            this.L2B.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.L2B.Location = new System.Drawing.Point(8, 85);
            this.L2B.Margin = new System.Windows.Forms.Padding(2);
            this.L2B.Name = "L2B";
            this.L2B.Size = new System.Drawing.Size(84, 70);
            this.L2B.TabIndex = 19;
            this.L2B.Text = "L2";
            this.L2B.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.L2B.UseVisualStyleBackColor = false;
            this.L2B.Click += new System.EventHandler(this.L2B_Click);
            // 
            // PanelBusqueda
            // 
            this.PanelBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelBusqueda.Location = new System.Drawing.Point(91, 131);
            this.PanelBusqueda.Name = "PanelBusqueda";
            this.PanelBusqueda.Size = new System.Drawing.Size(1827, 949);
            this.PanelBusqueda.TabIndex = 38;
            // 
            // PanelForm
            // 
            this.PanelForm.Controls.Add(this.lbReloj);
            this.PanelForm.Controls.Add(this.MinimizarB);
            this.PanelForm.Controls.Add(this.PanelSeparacion2);
            this.PanelForm.Controls.Add(this.PanelSeparacion1);
            this.PanelForm.Controls.Add(this.PanelResultados);
            this.PanelForm.Controls.Add(this.PanelBuscador);
            this.PanelForm.Controls.Add(this.PanelCalendario);
            this.PanelForm.Controls.Add(this.PanelBusqueda);
            this.PanelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelForm.Location = new System.Drawing.Point(0, 0);
            this.PanelForm.Margin = new System.Windows.Forms.Padding(2);
            this.PanelForm.Name = "PanelForm";
            this.PanelForm.Size = new System.Drawing.Size(1920, 1080);
            this.PanelForm.TabIndex = 37;
            // 
            // MinimizarB
            // 
            this.MinimizarB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimizarB.BackColor = System.Drawing.Color.Gainsboro;
            this.MinimizarB.BackgroundImage = global::WHPS.Properties.Resources.GenMinimizar;
            this.MinimizarB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MinimizarB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizarB.Location = new System.Drawing.Point(1890, 0);
            this.MinimizarB.Margin = new System.Windows.Forms.Padding(2);
            this.MinimizarB.Name = "MinimizarB";
            this.MinimizarB.Size = new System.Drawing.Size(30, 30);
            this.MinimizarB.TabIndex = 100;
            this.MinimizarB.UseVisualStyleBackColor = false;
            this.MinimizarB.Visible = false;
            this.MinimizarB.Click += new System.EventHandler(this.MinimizarB_Click);
            // 
            // PanelSeparacion2
            // 
            this.PanelSeparacion2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelSeparacion2.BackColor = System.Drawing.Color.Gainsboro;
            this.PanelSeparacion2.Location = new System.Drawing.Point(52, 104);
            this.PanelSeparacion2.Margin = new System.Windows.Forms.Padding(2);
            this.PanelSeparacion2.Name = "PanelSeparacion2";
            this.PanelSeparacion2.Size = new System.Drawing.Size(1870, 5);
            this.PanelSeparacion2.TabIndex = 22;
            // 
            // PanelSeparacion1
            // 
            this.PanelSeparacion1.BackColor = System.Drawing.Color.Gainsboro;
            this.PanelSeparacion1.Location = new System.Drawing.Point(884, 0);
            this.PanelSeparacion1.Margin = new System.Windows.Forms.Padding(2);
            this.PanelSeparacion1.Name = "PanelSeparacion1";
            this.PanelSeparacion1.Size = new System.Drawing.Size(5, 106);
            this.PanelSeparacion1.TabIndex = 21;
            // 
            // PanelResultados
            // 
            this.PanelResultados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelResultados.Location = new System.Drawing.Point(888, 32);
            this.PanelResultados.Margin = new System.Windows.Forms.Padding(2);
            this.PanelResultados.Name = "PanelResultados";
            this.PanelResultados.Size = new System.Drawing.Size(1030, 72);
            this.PanelResultados.TabIndex = 37;
            // 
            // PanelBuscador
            // 
            this.PanelBuscador.Controls.Add(this.label3);
            this.PanelBuscador.Controls.Add(this.BusDiaFinTB);
            this.PanelBuscador.Controls.Add(this.AvisoLB);
            this.PanelBuscador.Controls.Add(this.label2);
            this.PanelBuscador.Controls.Add(this.label20);
            this.PanelBuscador.Controls.Add(this.BusDiaTB);
            this.PanelBuscador.Controls.Add(this.BuscarB);
            this.PanelBuscador.Location = new System.Drawing.Point(92, 0);
            this.PanelBuscador.Margin = new System.Windows.Forms.Padding(2);
            this.PanelBuscador.Name = "PanelBuscador";
            this.PanelBuscador.Size = new System.Drawing.Size(792, 104);
            this.PanelBuscador.TabIndex = 36;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(352, 47);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 33);
            this.label3.TabIndex = 81;
            this.label3.Text = "Dia Fin:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BusDiaFinTB
            // 
            this.BusDiaFinTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BusDiaFinTB.Location = new System.Drawing.Point(461, 46);
            this.BusDiaFinTB.Margin = new System.Windows.Forms.Padding(2);
            this.BusDiaFinTB.Name = "BusDiaFinTB";
            this.BusDiaFinTB.ReadOnly = true;
            this.BusDiaFinTB.Size = new System.Drawing.Size(115, 30);
            this.BusDiaFinTB.TabIndex = 80;
            this.BusDiaFinTB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BusDiaFinTB_MouseClick);
            this.BusDiaFinTB.TextChanged += new System.EventHandler(this.BusDiaFinTB_TextChanged);
            // 
            // AvisoLB
            // 
            this.AvisoLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvisoLB.Location = new System.Drawing.Point(226, 1);
            this.AvisoLB.Name = "AvisoLB";
            this.AvisoLB.Size = new System.Drawing.Size(363, 29);
            this.AvisoLB.TabIndex = 79;
            this.AvisoLB.Text = " AVISO: Debe introducir la linea y al menos el lote o el día de inicio.";
            this.AvisoLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 28);
            this.label2.TabIndex = 38;
            this.label2.Text = "BUSCADOR";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BuscarB
            // 
            this.BuscarB.BackColor = System.Drawing.Color.Gray;
            this.BuscarB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BuscarB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BuscarB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuscarB.ForeColor = System.Drawing.Color.White;
            this.BuscarB.Image = global::WHPS.Properties.Resources.GenLupa50x50;
            this.BuscarB.Location = new System.Drawing.Point(693, 4);
            this.BuscarB.Margin = new System.Windows.Forms.Padding(2);
            this.BuscarB.Name = "BuscarB";
            this.BuscarB.Size = new System.Drawing.Size(95, 95);
            this.BuscarB.TabIndex = 17;
            this.BuscarB.Text = "Buscar";
            this.BuscarB.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BuscarB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BuscarB.UseVisualStyleBackColor = false;
            this.BuscarB.Click += new System.EventHandler(this.BuscarB_Click);
            // 
            // PanelCalendario
            // 
            this.PanelCalendario.Controls.Add(this.monthCalendar1);
            this.PanelCalendario.Controls.Add(this.panel2);
            this.PanelCalendario.Controls.Add(this.panel3);
            this.PanelCalendario.Controls.Add(this.panel4);
            this.PanelCalendario.Location = new System.Drawing.Point(92, 111);
            this.PanelCalendario.Margin = new System.Windows.Forms.Padding(2);
            this.PanelCalendario.Name = "PanelCalendario";
            this.PanelCalendario.Size = new System.Drawing.Size(242, 212);
            this.PanelCalendario.TabIndex = 85;
            this.PanelCalendario.Visible = false;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(1, 1);
            this.monthCalendar1.Margin = new System.Windows.Forms.Padding(7);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 85;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Location = new System.Drawing.Point(234, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(4, 210);
            this.panel2.TabIndex = 22;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gainsboro;
            this.panel3.Location = new System.Drawing.Point(1101, -2);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(5, 106);
            this.panel3.TabIndex = 22;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Gainsboro;
            this.panel4.Location = new System.Drawing.Point(-1, 206);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(230, 4);
            this.panel4.TabIndex = 23;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PanelTot
            // 
            this.PanelTot.BackColor = System.Drawing.Color.Maroon;
            this.PanelTot.Location = new System.Drawing.Point(1, 345);
            this.PanelTot.Margin = new System.Windows.Forms.Padding(2);
            this.PanelTot.Name = "PanelTot";
            this.PanelTot.Size = new System.Drawing.Size(5, 70);
            this.PanelTot.TabIndex = 44;
            // 
            // TotalB
            // 
            this.TotalB.FlatAppearance.BorderSize = 0;
            this.TotalB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SlateGray;
            this.TotalB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this.TotalB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TotalB.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalB.ForeColor = System.Drawing.Color.White;
            this.TotalB.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.TotalB.Location = new System.Drawing.Point(8, 347);
            this.TotalB.Margin = new System.Windows.Forms.Padding(2);
            this.TotalB.Name = "TotalB";
            this.TotalB.Size = new System.Drawing.Size(80, 70);
            this.TotalB.TabIndex = 43;
            this.TotalB.Text = " T";
            this.TotalB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TotalB.UseVisualStyleBackColor = false;
            this.TotalB.Click += new System.EventHandler(this.TotalB_Click);
            // 
            // MainProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.PanelVertical);
            this.Controls.Add(this.PanelForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainProduccion";
            this.Text = "MainProduccion";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainProduccion_Load);
            this.PanelVertical.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MenuAdminB)).EndInit();
            this.PanelForm.ResumeLayout(false);
            this.PanelBuscador.ResumeLayout(false);
            this.PanelBuscador.PerformLayout();
            this.PanelCalendario.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbReloj;
        private System.Windows.Forms.Button BuscarB;
        private System.Windows.Forms.TextBox BusDiaTB;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button BackB;
        private System.Windows.Forms.Panel PanelVertical;
        private System.Windows.Forms.PictureBox MenuAdminB;
        private System.Windows.Forms.Panel PanelL5;
        private System.Windows.Forms.Button L5B;
        private System.Windows.Forms.Panel PanelL3;
        private System.Windows.Forms.Button L3B;
        private System.Windows.Forms.Panel PanelL2;
        private System.Windows.Forms.Button L2B;
        private System.Windows.Forms.Panel PanelForm;
        private System.Windows.Forms.Panel PanelResultados;
        private System.Windows.Forms.Panel PanelBuscador;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel PanelBusqueda;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel PanelSeparacion2;
        private System.Windows.Forms.Panel PanelSeparacion1;
        private System.Windows.Forms.Label AvisoLB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox BusDiaFinTB;
        public System.Windows.Forms.Panel PanelCalendario;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button MinimizarB;
        private System.Windows.Forms.Panel PanelTot;
        private System.Windows.Forms.Button TotalB;
    }
}
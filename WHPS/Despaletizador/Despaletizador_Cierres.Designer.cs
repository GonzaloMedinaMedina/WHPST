namespace WHPS.Despaletizador
{
    partial class Despaletizador_Cierres
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
            this.RefWB = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.numberpad1 = new WHPS.Utiles.numberpad();
            this.MinimizarB = new System.Windows.Forms.Button();
            this.ExitB = new System.Windows.Forms.Button();
            this.CampoIntroduccionAutomaticaBOX = new System.Windows.Forms.GroupBox();
            this.EnviarB = new System.Windows.Forms.Button();
            this.CampoIntroduccionLB = new System.Windows.Forms.Label();
            this.lbReloj = new System.Windows.Forms.Label();
            this.InputLB = new System.Windows.Forms.Label();
            this.ModoManualBot = new System.Windows.Forms.Button();
            this.InputTB = new System.Windows.Forms.TextBox();
            this.IntroduccionManualLB = new System.Windows.Forms.Label();
            this.InformacionProductoBOX = new System.Windows.Forms.GroupBox();
            this.ssccTB = new System.Windows.Forms.TextBox();
            this.ssccLB = new System.Windows.Forms.Label();
            this.saveBot = new System.Windows.Forms.Button();
            this.InfProductoLB = new System.Windows.Forms.Label();
            this.cantidadTB = new System.Windows.Forms.TextBox();
            this.cantidadLB = new System.Windows.Forms.Label();
            this.loteTB = new System.Windows.Forms.TextBox();
            this.loteLB = new System.Windows.Forms.Label();
            this.DescTB = new System.Windows.Forms.TextBox();
            this.fabdateTB = new System.Windows.Forms.TextBox();
            this.fabdateLB = new System.Windows.Forms.Label();
            this.provTB = new System.Windows.Forms.TextBox();
            this.provLB = new System.Windows.Forms.Label();
            this.refwhTB = new System.Windows.Forms.TextBox();
            this.refwhLB = new System.Windows.Forms.Label();
            this.eanTB = new System.Windows.Forms.TextBox();
            this.eanLB = new System.Windows.Forms.Label();
            this.turnoTB = new System.Windows.Forms.TextBox();
            this.turnoLB = new System.Windows.Forms.Label();
            this.maqTB = new System.Windows.Forms.TextBox();
            this.maqLB = new System.Windows.Forms.Label();
            this.respTB = new System.Windows.Forms.TextBox();
            this.respLB = new System.Windows.Forms.Label();
            this.dateTB = new System.Windows.Forms.TextBox();
            this.dateLB = new System.Windows.Forms.Label();
            this.descripLB = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.CampoIntroduccionAutomaticaBOX.SuspendLayout();
            this.InformacionProductoBOX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // RefWB
            // 
            this.RefWB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.RefWB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RefWB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RefWB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefWB.ForeColor = System.Drawing.Color.White;
            this.RefWB.Location = new System.Drawing.Point(1736, 43);
            this.RefWB.Name = "RefWB";
            this.RefWB.Size = new System.Drawing.Size(109, 100);
            this.RefWB.TabIndex = 31;
            this.RefWB.Text = "R-WH";
            this.RefWB.UseVisualStyleBackColor = false;
            this.RefWB.Click += new System.EventHandler(this.RefWB_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // numberpad1
            // 
            this.numberpad1.BackColor = System.Drawing.Color.White;
            this.numberpad1.Location = new System.Drawing.Point(304, 822);
            this.numberpad1.Margin = new System.Windows.Forms.Padding(5);
            this.numberpad1.Name = "numberpad1";
            this.numberpad1.Size = new System.Drawing.Size(334, 420);
            this.numberpad1.TabIndex = 109;
            // 
            // MinimizarB
            // 
            this.MinimizarB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MinimizarB.BackColor = System.Drawing.Color.Gainsboro;
            this.MinimizarB.BackgroundImage = global::WHPS.Properties.Resources.GenMinimizar;
            this.MinimizarB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MinimizarB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizarB.Location = new System.Drawing.Point(1890, 0);
            this.MinimizarB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimizarB.Name = "MinimizarB";
            this.MinimizarB.Size = new System.Drawing.Size(30, 30);
            this.MinimizarB.TabIndex = 108;
            this.MinimizarB.UseVisualStyleBackColor = false;
            this.MinimizarB.Visible = false;
            this.MinimizarB.Click += new System.EventHandler(this.MinimizarB_Click);
            // 
            // ExitB
            // 
            this.ExitB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ExitB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.ExitB.BackgroundImage = global::WHPS.Properties.Resources.GenCasa;
            this.ExitB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExitB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitB.ForeColor = System.Drawing.Color.White;
            this.ExitB.Location = new System.Drawing.Point(12, 819);
            this.ExitB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ExitB.Name = "ExitB";
            this.ExitB.Size = new System.Drawing.Size(250, 250);
            this.ExitB.TabIndex = 107;
            this.ExitB.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ExitB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ExitB.UseVisualStyleBackColor = false;
            this.ExitB.Click += new System.EventHandler(this.ExitB_Click);
            // 
            // CampoIntroduccionAutomaticaBOX
            // 
            this.CampoIntroduccionAutomaticaBOX.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CampoIntroduccionAutomaticaBOX.Controls.Add(this.EnviarB);
            this.CampoIntroduccionAutomaticaBOX.Controls.Add(this.CampoIntroduccionLB);
            this.CampoIntroduccionAutomaticaBOX.Controls.Add(this.lbReloj);
            this.CampoIntroduccionAutomaticaBOX.Controls.Add(this.InputLB);
            this.CampoIntroduccionAutomaticaBOX.Controls.Add(this.RefWB);
            this.CampoIntroduccionAutomaticaBOX.Controls.Add(this.ModoManualBot);
            this.CampoIntroduccionAutomaticaBOX.Controls.Add(this.InputTB);
            this.CampoIntroduccionAutomaticaBOX.Controls.Add(this.IntroduccionManualLB);
            this.CampoIntroduccionAutomaticaBOX.Location = new System.Drawing.Point(13, 32);
            this.CampoIntroduccionAutomaticaBOX.Margin = new System.Windows.Forms.Padding(4);
            this.CampoIntroduccionAutomaticaBOX.Name = "CampoIntroduccionAutomaticaBOX";
            this.CampoIntroduccionAutomaticaBOX.Padding = new System.Windows.Forms.Padding(4);
            this.CampoIntroduccionAutomaticaBOX.Size = new System.Drawing.Size(1894, 159);
            this.CampoIntroduccionAutomaticaBOX.TabIndex = 105;
            this.CampoIntroduccionAutomaticaBOX.TabStop = false;
            // 
            // EnviarB
            // 
            this.EnviarB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.EnviarB.BackgroundImage = global::WHPS.Properties.Resources.GenFlechaBlanca;
            this.EnviarB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.EnviarB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EnviarB.Location = new System.Drawing.Point(1169, 80);
            this.EnviarB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EnviarB.Name = "EnviarB";
            this.EnviarB.Size = new System.Drawing.Size(69, 26);
            this.EnviarB.TabIndex = 28;
            this.EnviarB.UseVisualStyleBackColor = false;
            this.EnviarB.Click += new System.EventHandler(this.EnviarB_Click);
            // 
            // CampoIntroduccionLB
            // 
            this.CampoIntroduccionLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.CampoIntroduccionLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CampoIntroduccionLB.ForeColor = System.Drawing.Color.White;
            this.CampoIntroduccionLB.Location = new System.Drawing.Point(-3, 7);
            this.CampoIntroduccionLB.Name = "CampoIntroduccionLB";
            this.CampoIntroduccionLB.Size = new System.Drawing.Size(500, 25);
            this.CampoIntroduccionLB.TabIndex = 27;
            this.CampoIntroduccionLB.Text = "CAMPO DE INTRODUCCIÓN AUTOMÁTICA";
            this.CampoIntroduccionLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbReloj
            // 
            this.lbReloj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbReloj.BackColor = System.Drawing.Color.Transparent;
            this.lbReloj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReloj.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbReloj.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbReloj.Location = new System.Drawing.Point(1805, 7);
            this.lbReloj.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbReloj.Name = "lbReloj";
            this.lbReloj.Size = new System.Drawing.Size(89, 27);
            this.lbReloj.TabIndex = 25;
            this.lbReloj.Text = "00:00:00";
            this.lbReloj.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InputLB
            // 
            this.InputLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputLB.Location = new System.Drawing.Point(441, 79);
            this.InputLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.InputLB.Name = "InputLB";
            this.InputLB.Size = new System.Drawing.Size(248, 31);
            this.InputLB.TabIndex = 11;
            this.InputLB.Text = "Código del producto:";
            this.InputLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ModoManualBot
            // 
            this.ModoManualBot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ModoManualBot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.ModoManualBot.BackgroundImage = global::WHPS.Properties.Resources.DespModoManual;
            this.ModoManualBot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ModoManualBot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ModoManualBot.Location = new System.Drawing.Point(1582, 43);
            this.ModoManualBot.Margin = new System.Windows.Forms.Padding(4);
            this.ModoManualBot.Name = "ModoManualBot";
            this.ModoManualBot.Size = new System.Drawing.Size(100, 100);
            this.ModoManualBot.TabIndex = 4;
            this.ModoManualBot.UseVisualStyleBackColor = false;
            this.ModoManualBot.Click += new System.EventHandler(this.ModoManualBot_Click);
            // 
            // InputTB
            // 
            this.InputTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputTB.Location = new System.Drawing.Point(697, 78);
            this.InputTB.Margin = new System.Windows.Forms.Padding(4);
            this.InputTB.Name = "InputTB";
            this.InputTB.Size = new System.Drawing.Size(465, 30);
            this.InputTB.TabIndex = 7;
            this.InputTB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.InputTB_MouseClick);
            this.InputTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputTB_KeyDown);
            // 
            // IntroduccionManualLB
            // 
            this.IntroduccionManualLB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IntroduccionManualLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IntroduccionManualLB.Location = new System.Drawing.Point(1350, 78);
            this.IntroduccionManualLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.IntroduccionManualLB.Name = "IntroduccionManualLB";
            this.IntroduccionManualLB.Size = new System.Drawing.Size(224, 30);
            this.IntroduccionManualLB.TabIndex = 6;
            this.IntroduccionManualLB.Text = "INTRODUCCIÓN MANUAL";
            this.IntroduccionManualLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InformacionProductoBOX
            // 
            this.InformacionProductoBOX.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.InformacionProductoBOX.Controls.Add(this.ssccTB);
            this.InformacionProductoBOX.Controls.Add(this.ssccLB);
            this.InformacionProductoBOX.Controls.Add(this.saveBot);
            this.InformacionProductoBOX.Controls.Add(this.InfProductoLB);
            this.InformacionProductoBOX.Controls.Add(this.cantidadTB);
            this.InformacionProductoBOX.Controls.Add(this.cantidadLB);
            this.InformacionProductoBOX.Controls.Add(this.loteTB);
            this.InformacionProductoBOX.Controls.Add(this.loteLB);
            this.InformacionProductoBOX.Controls.Add(this.DescTB);
            this.InformacionProductoBOX.Controls.Add(this.fabdateTB);
            this.InformacionProductoBOX.Controls.Add(this.fabdateLB);
            this.InformacionProductoBOX.Controls.Add(this.provTB);
            this.InformacionProductoBOX.Controls.Add(this.provLB);
            this.InformacionProductoBOX.Controls.Add(this.refwhTB);
            this.InformacionProductoBOX.Controls.Add(this.refwhLB);
            this.InformacionProductoBOX.Controls.Add(this.eanTB);
            this.InformacionProductoBOX.Controls.Add(this.eanLB);
            this.InformacionProductoBOX.Controls.Add(this.turnoTB);
            this.InformacionProductoBOX.Controls.Add(this.turnoLB);
            this.InformacionProductoBOX.Controls.Add(this.maqTB);
            this.InformacionProductoBOX.Controls.Add(this.maqLB);
            this.InformacionProductoBOX.Controls.Add(this.respTB);
            this.InformacionProductoBOX.Controls.Add(this.respLB);
            this.InformacionProductoBOX.Controls.Add(this.dateTB);
            this.InformacionProductoBOX.Controls.Add(this.dateLB);
            this.InformacionProductoBOX.Controls.Add(this.descripLB);
            this.InformacionProductoBOX.Location = new System.Drawing.Point(13, 199);
            this.InformacionProductoBOX.Margin = new System.Windows.Forms.Padding(4);
            this.InformacionProductoBOX.Name = "InformacionProductoBOX";
            this.InformacionProductoBOX.Padding = new System.Windows.Forms.Padding(4);
            this.InformacionProductoBOX.Size = new System.Drawing.Size(1894, 614);
            this.InformacionProductoBOX.TabIndex = 106;
            this.InformacionProductoBOX.TabStop = false;
            // 
            // ssccTB
            // 
            this.ssccTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssccTB.Location = new System.Drawing.Point(347, 546);
            this.ssccTB.Margin = new System.Windows.Forms.Padding(4);
            this.ssccTB.Name = "ssccTB";
            this.ssccTB.ReadOnly = true;
            this.ssccTB.Size = new System.Drawing.Size(587, 30);
            this.ssccTB.TabIndex = 43;
            // 
            // ssccLB
            // 
            this.ssccLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssccLB.Location = new System.Drawing.Point(152, 546);
            this.ssccLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ssccLB.Name = "ssccLB";
            this.ssccLB.Size = new System.Drawing.Size(187, 30);
            this.ssccLB.TabIndex = 42;
            this.ssccLB.Text = "SSCC:";
            this.ssccLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // saveBot
            // 
            this.saveBot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBot.BackgroundImage = global::WHPS.Properties.Resources.GenGuardar;
            this.saveBot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveBot.FlatAppearance.BorderSize = 0;
            this.saveBot.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.saveBot.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.saveBot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveBot.Location = new System.Drawing.Point(1353, 152);
            this.saveBot.Margin = new System.Windows.Forms.Padding(4);
            this.saveBot.Name = "saveBot";
            this.saveBot.Size = new System.Drawing.Size(333, 308);
            this.saveBot.TabIndex = 29;
            this.saveBot.UseVisualStyleBackColor = true;
            this.saveBot.Click += new System.EventHandler(this.saveBot_Click);
            // 
            // InfProductoLB
            // 
            this.InfProductoLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.InfProductoLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfProductoLB.ForeColor = System.Drawing.Color.White;
            this.InfProductoLB.Location = new System.Drawing.Point(-3, 7);
            this.InfProductoLB.Name = "InfProductoLB";
            this.InfProductoLB.Size = new System.Drawing.Size(500, 25);
            this.InfProductoLB.TabIndex = 28;
            this.InfProductoLB.Text = "INFORMACION DEL PRODUCTO";
            this.InfProductoLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cantidadTB
            // 
            this.cantidadTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidadTB.Location = new System.Drawing.Point(347, 466);
            this.cantidadTB.Margin = new System.Windows.Forms.Padding(4);
            this.cantidadTB.Name = "cantidadTB";
            this.cantidadTB.ReadOnly = true;
            this.cantidadTB.Size = new System.Drawing.Size(587, 30);
            this.cantidadTB.TabIndex = 27;
            // 
            // cantidadLB
            // 
            this.cantidadLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidadLB.Location = new System.Drawing.Point(152, 466);
            this.cantidadLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cantidadLB.Name = "cantidadLB";
            this.cantidadLB.Size = new System.Drawing.Size(187, 30);
            this.cantidadLB.TabIndex = 26;
            this.cantidadLB.Text = "Cantidad:";
            this.cantidadLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // loteTB
            // 
            this.loteTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loteTB.Location = new System.Drawing.Point(347, 506);
            this.loteTB.Margin = new System.Windows.Forms.Padding(4);
            this.loteTB.Name = "loteTB";
            this.loteTB.ReadOnly = true;
            this.loteTB.Size = new System.Drawing.Size(587, 30);
            this.loteTB.TabIndex = 25;
            // 
            // loteLB
            // 
            this.loteLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loteLB.Location = new System.Drawing.Point(152, 506);
            this.loteLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.loteLB.Name = "loteLB";
            this.loteLB.Size = new System.Drawing.Size(187, 30);
            this.loteLB.TabIndex = 24;
            this.loteLB.Text = "Lote fabricación:";
            this.loteLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DescTB
            // 
            this.DescTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescTB.Location = new System.Drawing.Point(347, 266);
            this.DescTB.Margin = new System.Windows.Forms.Padding(4);
            this.DescTB.Name = "DescTB";
            this.DescTB.ReadOnly = true;
            this.DescTB.Size = new System.Drawing.Size(587, 30);
            this.DescTB.TabIndex = 7;
            // 
            // fabdateTB
            // 
            this.fabdateTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fabdateTB.Location = new System.Drawing.Point(347, 426);
            this.fabdateTB.Margin = new System.Windows.Forms.Padding(4);
            this.fabdateTB.Name = "fabdateTB";
            this.fabdateTB.ReadOnly = true;
            this.fabdateTB.Size = new System.Drawing.Size(587, 30);
            this.fabdateTB.TabIndex = 21;
            // 
            // fabdateLB
            // 
            this.fabdateLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fabdateLB.Location = new System.Drawing.Point(89, 426);
            this.fabdateLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fabdateLB.Name = "fabdateLB";
            this.fabdateLB.Size = new System.Drawing.Size(250, 30);
            this.fabdateLB.TabIndex = 20;
            this.fabdateLB.Text = "Fecha fabricación:";
            this.fabdateLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // provTB
            // 
            this.provTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.provTB.Location = new System.Drawing.Point(347, 386);
            this.provTB.Margin = new System.Windows.Forms.Padding(4);
            this.provTB.Name = "provTB";
            this.provTB.ReadOnly = true;
            this.provTB.Size = new System.Drawing.Size(587, 30);
            this.provTB.TabIndex = 19;
            // 
            // provLB
            // 
            this.provLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.provLB.Location = new System.Drawing.Point(152, 386);
            this.provLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.provLB.Name = "provLB";
            this.provLB.Size = new System.Drawing.Size(187, 30);
            this.provLB.TabIndex = 18;
            this.provLB.Text = "Proveedor:";
            this.provLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // refwhTB
            // 
            this.refwhTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refwhTB.Location = new System.Drawing.Point(347, 346);
            this.refwhTB.Margin = new System.Windows.Forms.Padding(4);
            this.refwhTB.Name = "refwhTB";
            this.refwhTB.ReadOnly = true;
            this.refwhTB.Size = new System.Drawing.Size(587, 30);
            this.refwhTB.TabIndex = 17;
            // 
            // refwhLB
            // 
            this.refwhLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refwhLB.Location = new System.Drawing.Point(84, 346);
            this.refwhLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.refwhLB.Name = "refwhLB";
            this.refwhLB.Size = new System.Drawing.Size(255, 30);
            this.refwhLB.TabIndex = 16;
            this.refwhLB.Text = "Referencia interna WH:";
            this.refwhLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // eanTB
            // 
            this.eanTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eanTB.Location = new System.Drawing.Point(347, 306);
            this.eanTB.Margin = new System.Windows.Forms.Padding(4);
            this.eanTB.Name = "eanTB";
            this.eanTB.ReadOnly = true;
            this.eanTB.Size = new System.Drawing.Size(587, 30);
            this.eanTB.TabIndex = 15;
            // 
            // eanLB
            // 
            this.eanLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eanLB.Location = new System.Drawing.Point(152, 306);
            this.eanLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.eanLB.Name = "eanLB";
            this.eanLB.Size = new System.Drawing.Size(187, 30);
            this.eanLB.TabIndex = 14;
            this.eanLB.Text = "EAN:";
            this.eanLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // turnoTB
            // 
            this.turnoTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.turnoTB.Location = new System.Drawing.Point(347, 226);
            this.turnoTB.Margin = new System.Windows.Forms.Padding(4);
            this.turnoTB.Name = "turnoTB";
            this.turnoTB.ReadOnly = true;
            this.turnoTB.Size = new System.Drawing.Size(587, 30);
            this.turnoTB.TabIndex = 13;
            // 
            // turnoLB
            // 
            this.turnoLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.turnoLB.Location = new System.Drawing.Point(152, 226);
            this.turnoLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.turnoLB.Name = "turnoLB";
            this.turnoLB.Size = new System.Drawing.Size(187, 30);
            this.turnoLB.TabIndex = 12;
            this.turnoLB.Text = "Turno:";
            this.turnoLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // maqTB
            // 
            this.maqTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maqTB.Location = new System.Drawing.Point(347, 186);
            this.maqTB.Margin = new System.Windows.Forms.Padding(4);
            this.maqTB.Name = "maqTB";
            this.maqTB.ReadOnly = true;
            this.maqTB.Size = new System.Drawing.Size(587, 30);
            this.maqTB.TabIndex = 11;
            // 
            // maqLB
            // 
            this.maqLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maqLB.Location = new System.Drawing.Point(152, 186);
            this.maqLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.maqLB.Name = "maqLB";
            this.maqLB.Size = new System.Drawing.Size(187, 30);
            this.maqLB.TabIndex = 10;
            this.maqLB.Text = "Maquinista:";
            this.maqLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // respTB
            // 
            this.respTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.respTB.Location = new System.Drawing.Point(347, 146);
            this.respTB.Margin = new System.Windows.Forms.Padding(4);
            this.respTB.Name = "respTB";
            this.respTB.ReadOnly = true;
            this.respTB.Size = new System.Drawing.Size(587, 30);
            this.respTB.TabIndex = 9;
            // 
            // respLB
            // 
            this.respLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.respLB.Location = new System.Drawing.Point(152, 146);
            this.respLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.respLB.Name = "respLB";
            this.respLB.Size = new System.Drawing.Size(187, 30);
            this.respLB.TabIndex = 8;
            this.respLB.Text = "Responsable:";
            this.respLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTB
            // 
            this.dateTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTB.Location = new System.Drawing.Point(347, 106);
            this.dateTB.Margin = new System.Windows.Forms.Padding(4);
            this.dateTB.Name = "dateTB";
            this.dateTB.ReadOnly = true;
            this.dateTB.Size = new System.Drawing.Size(587, 30);
            this.dateTB.TabIndex = 7;
            // 
            // dateLB
            // 
            this.dateLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLB.Location = new System.Drawing.Point(152, 106);
            this.dateLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dateLB.Name = "dateLB";
            this.dateLB.Size = new System.Drawing.Size(187, 30);
            this.dateLB.TabIndex = 6;
            this.dateLB.Text = "Fecha:";
            this.dateLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // descripLB
            // 
            this.descripLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descripLB.Location = new System.Drawing.Point(152, 266);
            this.descripLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.descripLB.Name = "descripLB";
            this.descripLB.Size = new System.Drawing.Size(187, 30);
            this.descripLB.TabIndex = 6;
            this.descripLB.Text = "Descripción:";
            this.descripLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = global::WHPS.Properties.Resources.DespCierre;
            this.pictureBox1.Location = new System.Drawing.Point(1658, 819);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 250);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 110;
            this.pictureBox1.TabStop = false;
            // 
            // Despaletizador_Cierres
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.numberpad1);
            this.Controls.Add(this.MinimizarB);
            this.Controls.Add(this.ExitB);
            this.Controls.Add(this.InformacionProductoBOX);
            this.Controls.Add(this.CampoIntroduccionAutomaticaBOX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Despaletizador_Cierres";
            this.Text = "Insertar datos del Cierre - MANUAL";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Despaletizador_Cierres_Load);
            this.CampoIntroduccionAutomaticaBOX.ResumeLayout(false);
            this.CampoIntroduccionAutomaticaBOX.PerformLayout();
            this.InformacionProductoBOX.ResumeLayout(false);
            this.InformacionProductoBOX.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button RefWB;
        private Utiles.numberpad numberpad1;
        private System.Windows.Forms.Button MinimizarB;
        private System.Windows.Forms.Button ExitB;
        private System.Windows.Forms.GroupBox CampoIntroduccionAutomaticaBOX;
        private System.Windows.Forms.Button EnviarB;
        private System.Windows.Forms.Label CampoIntroduccionLB;
        private System.Windows.Forms.Label lbReloj;
        private System.Windows.Forms.Label InputLB;
        private System.Windows.Forms.Button ModoManualBot;
        public System.Windows.Forms.TextBox InputTB;
        private System.Windows.Forms.Label IntroduccionManualLB;
        private System.Windows.Forms.GroupBox InformacionProductoBOX;
        private System.Windows.Forms.TextBox ssccTB;
        private System.Windows.Forms.Label ssccLB;
        private System.Windows.Forms.Button saveBot;
        private System.Windows.Forms.Label InfProductoLB;
        private System.Windows.Forms.TextBox cantidadTB;
        private System.Windows.Forms.Label cantidadLB;
        private System.Windows.Forms.TextBox loteTB;
        private System.Windows.Forms.Label loteLB;
        private System.Windows.Forms.TextBox DescTB;
        private System.Windows.Forms.TextBox fabdateTB;
        private System.Windows.Forms.Label fabdateLB;
        private System.Windows.Forms.TextBox provTB;
        private System.Windows.Forms.Label provLB;
        private System.Windows.Forms.TextBox refwhTB;
        private System.Windows.Forms.Label refwhLB;
        private System.Windows.Forms.TextBox eanTB;
        private System.Windows.Forms.Label eanLB;
        private System.Windows.Forms.TextBox turnoTB;
        private System.Windows.Forms.Label turnoLB;
        private System.Windows.Forms.TextBox maqTB;
        private System.Windows.Forms.Label maqLB;
        private System.Windows.Forms.TextBox respTB;
        private System.Windows.Forms.Label respLB;
        private System.Windows.Forms.TextBox dateTB;
        private System.Windows.Forms.Label dateLB;
        private System.Windows.Forms.Label descripLB;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
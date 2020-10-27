namespace WHPS.Etiquetadora
{
    partial class Etiquetadora_RotBotellas
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.MinimizarB = new System.Windows.Forms.Button();
            this.RoturaBotellaBOX = new System.Windows.Forms.GroupBox();
            this.BotRotas_SI_B = new System.Windows.Forms.Button();
            this.RoturaBotellasTB = new System.Windows.Forms.Label();
            this.lbReloj = new System.Windows.Forms.Label();
            this.BotRotas_NO_B = new System.Windows.Forms.Button();
            this.RoturasLB = new System.Windows.Forms.Label();
            this.ExitB = new System.Windows.Forms.Button();
            this.saveBot = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.numrotasTB = new System.Windows.Forms.TextBox();
            this.NumAproxLB = new System.Windows.Forms.Label();
            this.InspArea_SI_B = new System.Windows.Forms.Button();
            this.InspArea_NO_B = new System.Windows.Forms.Button();
            this.AreaLB = new System.Windows.Forms.Label();
            this.TrabajadorLB = new System.Windows.Forms.Label();
            this.InspTrab_SI_B = new System.Windows.Forms.Button();
            this.InspTrab_NO_B = new System.Windows.Forms.Button();
            this.ConformacionLB = new System.Windows.Forms.Label();
            this.ConfrRespB = new System.Windows.Forms.Button();
            this.ContraseñaTB = new System.Windows.Forms.TextBox();
            this.DatosRoturaBOX = new System.Windows.Forms.GroupBox();
            this.RoturaBotellaBOX.SuspendLayout();
            this.DatosRoturaBOX.SuspendLayout();
            this.SuspendLayout();
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
            this.MinimizarB.Location = new System.Drawing.Point(1890, 0);
            this.MinimizarB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimizarB.Name = "MinimizarB";
            this.MinimizarB.Size = new System.Drawing.Size(30, 30);
            this.MinimizarB.TabIndex = 106;
            this.MinimizarB.UseVisualStyleBackColor = false;
            this.MinimizarB.Visible = false;
            this.MinimizarB.Click += new System.EventHandler(this.MinimizarB_Click);
            // 
            // RoturaBotellaBOX
            // 
            this.RoturaBotellaBOX.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.RoturaBotellaBOX.Controls.Add(this.BotRotas_SI_B);
            this.RoturaBotellaBOX.Controls.Add(this.RoturaBotellasTB);
            this.RoturaBotellaBOX.Controls.Add(this.lbReloj);
            this.RoturaBotellaBOX.Controls.Add(this.BotRotas_NO_B);
            this.RoturaBotellaBOX.Controls.Add(this.RoturasLB);
            this.RoturaBotellaBOX.Location = new System.Drawing.Point(12, 32);
            this.RoturaBotellaBOX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RoturaBotellaBOX.Name = "RoturaBotellaBOX";
            this.RoturaBotellaBOX.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RoturaBotellaBOX.Size = new System.Drawing.Size(1896, 204);
            this.RoturaBotellaBOX.TabIndex = 101;
            this.RoturaBotellaBOX.TabStop = false;
            // 
            // BotRotas_SI_B
            // 
            this.BotRotas_SI_B.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.BotRotas_SI_B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BotRotas_SI_B.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotRotas_SI_B.ForeColor = System.Drawing.Color.White;
            this.BotRotas_SI_B.Location = new System.Drawing.Point(828, 64);
            this.BotRotas_SI_B.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BotRotas_SI_B.Name = "BotRotas_SI_B";
            this.BotRotas_SI_B.Size = new System.Drawing.Size(250, 100);
            this.BotRotas_SI_B.TabIndex = 12;
            this.BotRotas_SI_B.Text = "SI";
            this.BotRotas_SI_B.UseVisualStyleBackColor = false;
            this.BotRotas_SI_B.Click += new System.EventHandler(this.BotRotas_SI_B_Click);
            // 
            // RoturaBotellasTB
            // 
            this.RoturaBotellasTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.RoturaBotellasTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoturaBotellasTB.ForeColor = System.Drawing.Color.White;
            this.RoturaBotellasTB.Location = new System.Drawing.Point(1, 7);
            this.RoturaBotellasTB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.RoturaBotellasTB.Name = "RoturaBotellasTB";
            this.RoturaBotellasTB.Size = new System.Drawing.Size(305, 30);
            this.RoturaBotellasTB.TabIndex = 31;
            this.RoturaBotellasTB.Text = "ROTURA DE BOTELLAS";
            this.RoturaBotellasTB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbReloj
            // 
            this.lbReloj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbReloj.BackColor = System.Drawing.Color.Transparent;
            this.lbReloj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReloj.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbReloj.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbReloj.Location = new System.Drawing.Point(1797, 10);
            this.lbReloj.Name = "lbReloj";
            this.lbReloj.Size = new System.Drawing.Size(96, 27);
            this.lbReloj.TabIndex = 28;
            this.lbReloj.Text = "00:00:00";
            this.lbReloj.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BotRotas_NO_B
            // 
            this.BotRotas_NO_B.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.BotRotas_NO_B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BotRotas_NO_B.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotRotas_NO_B.ForeColor = System.Drawing.Color.White;
            this.BotRotas_NO_B.Location = new System.Drawing.Point(1084, 64);
            this.BotRotas_NO_B.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BotRotas_NO_B.Name = "BotRotas_NO_B";
            this.BotRotas_NO_B.Size = new System.Drawing.Size(250, 100);
            this.BotRotas_NO_B.TabIndex = 13;
            this.BotRotas_NO_B.Text = "NO";
            this.BotRotas_NO_B.UseVisualStyleBackColor = false;
            this.BotRotas_NO_B.Click += new System.EventHandler(this.BotRotas_NO_B_Click);
            // 
            // RoturasLB
            // 
            this.RoturasLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoturasLB.Location = new System.Drawing.Point(364, 95);
            this.RoturasLB.Name = "RoturasLB";
            this.RoturasLB.Size = new System.Drawing.Size(423, 45);
            this.RoturasLB.TabIndex = 11;
            this.RoturasLB.Text = "¿Se han roto botellas?";
            this.RoturasLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.ExitB.TabIndex = 104;
            this.ExitB.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ExitB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ExitB.UseVisualStyleBackColor = false;
            this.ExitB.Click += new System.EventHandler(this.ExitB_Click);
            // 
            // saveBot
            // 
            this.saveBot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.saveBot.BackgroundImage = global::WHPS.Properties.Resources.GenGuardar;
            this.saveBot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveBot.FlatAppearance.BorderSize = 0;
            this.saveBot.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.saveBot.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.saveBot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveBot.Location = new System.Drawing.Point(1658, 817);
            this.saveBot.Margin = new System.Windows.Forms.Padding(4);
            this.saveBot.Name = "saveBot";
            this.saveBot.Size = new System.Drawing.Size(250, 250);
            this.saveBot.TabIndex = 103;
            this.saveBot.UseVisualStyleBackColor = true;
            this.saveBot.Click += new System.EventHandler(this.saveBot_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(1, 9);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(305, 30);
            this.label7.TabIndex = 29;
            this.label7.Text = "DATOS DE ROTURA";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numrotasTB
            // 
            this.numrotasTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numrotasTB.Location = new System.Drawing.Point(918, 96);
            this.numrotasTB.Margin = new System.Windows.Forms.Padding(2);
            this.numrotasTB.Name = "numrotasTB";
            this.numrotasTB.Size = new System.Drawing.Size(504, 38);
            this.numrotasTB.TabIndex = 30;
            this.numrotasTB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.numrotasTB_MouseClick);
            // 
            // NumAproxLB
            // 
            this.NumAproxLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumAproxLB.Location = new System.Drawing.Point(173, 89);
            this.NumAproxLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NumAproxLB.Name = "NumAproxLB";
            this.NumAproxLB.Size = new System.Drawing.Size(730, 50);
            this.NumAproxLB.TabIndex = 31;
            this.NumAproxLB.Text = "Número aprox.roturas:";
            this.NumAproxLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // InspArea_SI_B
            // 
            this.InspArea_SI_B.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.InspArea_SI_B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InspArea_SI_B.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InspArea_SI_B.ForeColor = System.Drawing.Color.White;
            this.InspArea_SI_B.Location = new System.Drawing.Point(918, 198);
            this.InspArea_SI_B.Margin = new System.Windows.Forms.Padding(2);
            this.InspArea_SI_B.Name = "InspArea_SI_B";
            this.InspArea_SI_B.Size = new System.Drawing.Size(250, 100);
            this.InspArea_SI_B.TabIndex = 33;
            this.InspArea_SI_B.Text = "SI";
            this.InspArea_SI_B.UseVisualStyleBackColor = false;
            this.InspArea_SI_B.Click += new System.EventHandler(this.InspArea_SI_B_Click);
            // 
            // InspArea_NO_B
            // 
            this.InspArea_NO_B.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.InspArea_NO_B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InspArea_NO_B.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InspArea_NO_B.ForeColor = System.Drawing.Color.White;
            this.InspArea_NO_B.Location = new System.Drawing.Point(1172, 198);
            this.InspArea_NO_B.Margin = new System.Windows.Forms.Padding(2);
            this.InspArea_NO_B.Name = "InspArea_NO_B";
            this.InspArea_NO_B.Size = new System.Drawing.Size(250, 100);
            this.InspArea_NO_B.TabIndex = 34;
            this.InspArea_NO_B.Text = "NO";
            this.InspArea_NO_B.UseVisualStyleBackColor = false;
            this.InspArea_NO_B.Click += new System.EventHandler(this.InspArea_NO_B_Click);
            // 
            // AreaLB
            // 
            this.AreaLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AreaLB.Location = new System.Drawing.Point(184, 223);
            this.AreaLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.AreaLB.Name = "AreaLB";
            this.AreaLB.Size = new System.Drawing.Size(730, 50);
            this.AreaLB.TabIndex = 32;
            this.AreaLB.Text = "¿Se ha limpiado e inspeccionado el área?";
            this.AreaLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TrabajadorLB
            // 
            this.TrabajadorLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrabajadorLB.Location = new System.Drawing.Point(184, 327);
            this.TrabajadorLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TrabajadorLB.Name = "TrabajadorLB";
            this.TrabajadorLB.Size = new System.Drawing.Size(730, 50);
            this.TrabajadorLB.TabIndex = 35;
            this.TrabajadorLB.Text = "¿Se ha limpiado e inspeccionado al trabajador?";
            this.TrabajadorLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InspTrab_SI_B
            // 
            this.InspTrab_SI_B.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.InspTrab_SI_B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InspTrab_SI_B.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InspTrab_SI_B.ForeColor = System.Drawing.Color.White;
            this.InspTrab_SI_B.Location = new System.Drawing.Point(918, 302);
            this.InspTrab_SI_B.Margin = new System.Windows.Forms.Padding(2);
            this.InspTrab_SI_B.Name = "InspTrab_SI_B";
            this.InspTrab_SI_B.Size = new System.Drawing.Size(250, 100);
            this.InspTrab_SI_B.TabIndex = 36;
            this.InspTrab_SI_B.Text = "SI";
            this.InspTrab_SI_B.UseVisualStyleBackColor = false;
            this.InspTrab_SI_B.Click += new System.EventHandler(this.InspTrab_SI_B_Click);
            // 
            // InspTrab_NO_B
            // 
            this.InspTrab_NO_B.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.InspTrab_NO_B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InspTrab_NO_B.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InspTrab_NO_B.ForeColor = System.Drawing.Color.White;
            this.InspTrab_NO_B.Location = new System.Drawing.Point(1172, 302);
            this.InspTrab_NO_B.Margin = new System.Windows.Forms.Padding(2);
            this.InspTrab_NO_B.Name = "InspTrab_NO_B";
            this.InspTrab_NO_B.Size = new System.Drawing.Size(250, 100);
            this.InspTrab_NO_B.TabIndex = 37;
            this.InspTrab_NO_B.Text = "NO";
            this.InspTrab_NO_B.UseVisualStyleBackColor = false;
            this.InspTrab_NO_B.Click += new System.EventHandler(this.InspTrab_NO_B_Click);
            // 
            // ConformacionLB
            // 
            this.ConformacionLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConformacionLB.Location = new System.Drawing.Point(430, 431);
            this.ConformacionLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ConformacionLB.Name = "ConformacionLB";
            this.ConformacionLB.Size = new System.Drawing.Size(469, 50);
            this.ConformacionLB.TabIndex = 38;
            this.ConformacionLB.Text = "Confirmación del Responsable:";
            this.ConformacionLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ConfrRespB
            // 
            this.ConfrRespB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.ConfrRespB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConfrRespB.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfrRespB.ForeColor = System.Drawing.Color.White;
            this.ConfrRespB.Location = new System.Drawing.Point(918, 406);
            this.ConfrRespB.Margin = new System.Windows.Forms.Padding(2);
            this.ConfrRespB.Name = "ConfrRespB";
            this.ConfrRespB.Size = new System.Drawing.Size(504, 100);
            this.ConfrRespB.TabIndex = 39;
            this.ConfrRespB.Text = "CONFIRMACIÓN";
            this.ConfrRespB.UseVisualStyleBackColor = false;
            this.ConfrRespB.Click += new System.EventHandler(this.ConfrRespB_Click);
            // 
            // ContraseñaTB
            // 
            this.ContraseñaTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContraseñaTB.Location = new System.Drawing.Point(1441, 437);
            this.ContraseñaTB.Margin = new System.Windows.Forms.Padding(2);
            this.ContraseñaTB.Name = "ContraseñaTB";
            this.ContraseñaTB.Size = new System.Drawing.Size(125, 38);
            this.ContraseñaTB.TabIndex = 40;
            this.ContraseñaTB.Visible = false;
            this.ContraseñaTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContraseñaTB_KeyDown);
            // 
            // DatosRoturaBOX
            // 
            this.DatosRoturaBOX.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DatosRoturaBOX.Controls.Add(this.ContraseñaTB);
            this.DatosRoturaBOX.Controls.Add(this.ConfrRespB);
            this.DatosRoturaBOX.Controls.Add(this.ConformacionLB);
            this.DatosRoturaBOX.Controls.Add(this.InspTrab_NO_B);
            this.DatosRoturaBOX.Controls.Add(this.InspTrab_SI_B);
            this.DatosRoturaBOX.Controls.Add(this.TrabajadorLB);
            this.DatosRoturaBOX.Controls.Add(this.AreaLB);
            this.DatosRoturaBOX.Controls.Add(this.InspArea_NO_B);
            this.DatosRoturaBOX.Controls.Add(this.InspArea_SI_B);
            this.DatosRoturaBOX.Controls.Add(this.NumAproxLB);
            this.DatosRoturaBOX.Controls.Add(this.numrotasTB);
            this.DatosRoturaBOX.Controls.Add(this.label7);
            this.DatosRoturaBOX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DatosRoturaBOX.Location = new System.Drawing.Point(12, 241);
            this.DatosRoturaBOX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DatosRoturaBOX.Name = "DatosRoturaBOX";
            this.DatosRoturaBOX.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DatosRoturaBOX.Size = new System.Drawing.Size(1896, 570);
            this.DatosRoturaBOX.TabIndex = 100;
            this.DatosRoturaBOX.TabStop = false;
            // 
            // Etiquetadora_RotBotellas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.MinimizarB);
            this.Controls.Add(this.RoturaBotellaBOX);
            this.Controls.Add(this.ExitB);
            this.Controls.Add(this.saveBot);
            this.Controls.Add(this.DatosRoturaBOX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Etiquetadora_RotBotellas";
            this.Text = "Etiquetadora_RotBotellas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Etiquetadora_RotBotellas_Load);
            this.RoturaBotellaBOX.ResumeLayout(false);
            this.DatosRoturaBOX.ResumeLayout(false);
            this.DatosRoturaBOX.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button MinimizarB;
        private System.Windows.Forms.GroupBox RoturaBotellaBOX;
        private System.Windows.Forms.Button BotRotas_SI_B;
        private System.Windows.Forms.Label RoturaBotellasTB;
        private System.Windows.Forms.Label lbReloj;
        private System.Windows.Forms.Button BotRotas_NO_B;
        private System.Windows.Forms.Label RoturasLB;
        private System.Windows.Forms.Button ExitB;
        private System.Windows.Forms.Button saveBot;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox numrotasTB;
        private System.Windows.Forms.Label NumAproxLB;
        private System.Windows.Forms.Button InspArea_SI_B;
        private System.Windows.Forms.Button InspArea_NO_B;
        private System.Windows.Forms.Label AreaLB;
        private System.Windows.Forms.Label TrabajadorLB;
        private System.Windows.Forms.Button InspTrab_SI_B;
        private System.Windows.Forms.Button InspTrab_NO_B;
        private System.Windows.Forms.Label ConformacionLB;
        public System.Windows.Forms.Button ConfrRespB;
        public System.Windows.Forms.TextBox ContraseñaTB;
        private System.Windows.Forms.GroupBox DatosRoturaBOX;
    }
}
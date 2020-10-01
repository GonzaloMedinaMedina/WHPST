namespace WHPS.Etiquetadora
{
    partial class Etiquetadora_Registro_Paro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Etiquetadora_Registro_Paro));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DatosEquiposLB = new System.Windows.Forms.Label();
            this.lbReloj = new System.Windows.Forms.Label();
            this.turnoTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.maqTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.respTB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTB = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.saveBot = new System.Windows.Forms.Button();
            this.CancelarB = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TemporizadorTB = new System.Windows.Forms.TextBox();
            this.MinimizarB = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Motivo2CB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PDesdeTB = new System.Windows.Forms.TextBox();
            this.MotivoCB = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.ComentariosTB = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.BackColor = System.Drawing.Color.Yellow;
            this.groupBox1.Controls.Add(this.DatosEquiposLB);
            this.groupBox1.Controls.Add(this.lbReloj);
            this.groupBox1.Controls.Add(this.turnoTB);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.maqTB);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.respTB);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dateTB);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Location = new System.Drawing.Point(10, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1515, 178);
            this.groupBox1.TabIndex = 228;
            this.groupBox1.TabStop = false;
            // 
            // DatosEquiposLB
            // 
            this.DatosEquiposLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(18)))), ((int)(((byte)(7)))));
            this.DatosEquiposLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatosEquiposLB.ForeColor = System.Drawing.Color.White;
            this.DatosEquiposLB.Location = new System.Drawing.Point(-2, 6);
            this.DatosEquiposLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DatosEquiposLB.Name = "DatosEquiposLB";
            this.DatosEquiposLB.Size = new System.Drawing.Size(244, 24);
            this.DatosEquiposLB.TabIndex = 73;
            this.DatosEquiposLB.Text = "DATOS DEL EQUIPO";
            this.DatosEquiposLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbReloj
            // 
            this.lbReloj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbReloj.BackColor = System.Drawing.Color.Transparent;
            this.lbReloj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReloj.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbReloj.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbReloj.Location = new System.Drawing.Point(1442, 8);
            this.lbReloj.Name = "lbReloj";
            this.lbReloj.Size = new System.Drawing.Size(71, 22);
            this.lbReloj.TabIndex = 27;
            this.lbReloj.Text = "00:00:00";
            this.lbReloj.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // turnoTB
            // 
            this.turnoTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.turnoTB.Location = new System.Drawing.Point(542, 129);
            this.turnoTB.Name = "turnoTB";
            this.turnoTB.ReadOnly = true;
            this.turnoTB.Size = new System.Drawing.Size(512, 26);
            this.turnoTB.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(400, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "Turno:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // maqTB
            // 
            this.maqTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maqTB.Location = new System.Drawing.Point(542, 98);
            this.maqTB.Name = "maqTB";
            this.maqTB.ReadOnly = true;
            this.maqTB.Size = new System.Drawing.Size(512, 26);
            this.maqTB.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(400, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Maquinista:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // respTB
            // 
            this.respTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.respTB.Location = new System.Drawing.Point(542, 69);
            this.respTB.Name = "respTB";
            this.respTB.ReadOnly = true;
            this.respTB.Size = new System.Drawing.Size(512, 26);
            this.respTB.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(400, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Responsable:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTB
            // 
            this.dateTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTB.Location = new System.Drawing.Point(542, 40);
            this.dateTB.Name = "dateTB";
            this.dateTB.ReadOnly = true;
            this.dateTB.Size = new System.Drawing.Size(512, 26);
            this.dateTB.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(400, 40);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(138, 20);
            this.label15.TabIndex = 14;
            this.label15.Text = "Fecha Actual:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // saveBot
            // 
            this.saveBot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.saveBot.BackColor = System.Drawing.Color.White;
            this.saveBot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("saveBot.BackgroundImage")));
            this.saveBot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveBot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBot.Location = new System.Drawing.Point(1326, 654);
            this.saveBot.Name = "saveBot";
            this.saveBot.Size = new System.Drawing.Size(200, 200);
            this.saveBot.TabIndex = 227;
            this.saveBot.UseVisualStyleBackColor = false;
            this.saveBot.Click += new System.EventHandler(this.saveBot_Click);
            // 
            // CancelarB
            // 
            this.CancelarB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CancelarB.BackColor = System.Drawing.Color.White;
            this.CancelarB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CancelarB.BackgroundImage")));
            this.CancelarB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelarB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelarB.Location = new System.Drawing.Point(10, 654);
            this.CancelarB.Name = "CancelarB";
            this.CancelarB.Size = new System.Drawing.Size(200, 200);
            this.CancelarB.TabIndex = 226;
            this.CancelarB.UseVisualStyleBackColor = false;
            this.CancelarB.Click += new System.EventHandler(this.CancelarB_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox2.BackgroundImage = global::WHPS.Properties.Resources.LlenParoRayas;
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.TemporizadorTB);
            this.groupBox2.Location = new System.Drawing.Point(10, 331);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1513, 299);
            this.groupBox2.TabIndex = 225;
            this.groupBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(18)))), ((int)(((byte)(7)))));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(244, 24);
            this.label5.TabIndex = 75;
            this.label5.Text = "TIEMPO DE PARADA";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TemporizadorTB
            // 
            this.TemporizadorTB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TemporizadorTB.BackColor = System.Drawing.Color.Black;
            this.TemporizadorTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TemporizadorTB.ForeColor = System.Drawing.Color.Yellow;
            this.TemporizadorTB.Location = new System.Drawing.Point(618, 113);
            this.TemporizadorTB.Name = "TemporizadorTB";
            this.TemporizadorTB.Size = new System.Drawing.Size(279, 80);
            this.TemporizadorTB.TabIndex = 23;
            // 
            // MinimizarB
            // 
            this.MinimizarB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MinimizarB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(18)))), ((int)(((byte)(7)))));
            this.MinimizarB.BackgroundImage = global::WHPS.Properties.Resources.GenMinimizar;
            this.MinimizarB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MinimizarB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizarB.Location = new System.Drawing.Point(1512, 0);
            this.MinimizarB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimizarB.Name = "MinimizarB";
            this.MinimizarB.Size = new System.Drawing.Size(24, 24);
            this.MinimizarB.TabIndex = 224;
            this.MinimizarB.UseVisualStyleBackColor = false;
            this.MinimizarB.Visible = false;
            this.MinimizarB.Click += new System.EventHandler(this.MinimizarB_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox3.BackColor = System.Drawing.Color.Yellow;
            this.groupBox3.Controls.Add(this.Motivo2CB);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.PDesdeTB);
            this.groupBox3.Controls.Add(this.MotivoCB);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Location = new System.Drawing.Point(10, 210);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1515, 115);
            this.groupBox3.TabIndex = 223;
            this.groupBox3.TabStop = false;
            // 
            // Motivo2CB
            // 
            this.Motivo2CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Motivo2CB.FormattingEnabled = true;
            this.Motivo2CB.Items.AddRange(new object[] {
            "",
            "EXCESO DE COLA EN LAS PALAS",
            "FALLO CAMARA",
            "FALLO PRECINTA",
            "ROTURA PAPEL SOPORTE"});
            this.Motivo2CB.Location = new System.Drawing.Point(1059, 66);
            this.Motivo2CB.Name = "Motivo2CB";
            this.Motivo2CB.Size = new System.Drawing.Size(330, 28);
            this.Motivo2CB.TabIndex = 76;
            this.Motivo2CB.Visible = false;
            this.Motivo2CB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Motivo2CB_MouseClick);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(18)))), ((int)(((byte)(7)))));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(-2, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 24);
            this.label3.TabIndex = 74;
            this.label3.Text = "REGISTRO DE PARADA";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PDesdeTB
            // 
            this.PDesdeTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PDesdeTB.Location = new System.Drawing.Point(542, 37);
            this.PDesdeTB.Name = "PDesdeTB";
            this.PDesdeTB.Size = new System.Drawing.Size(512, 26);
            this.PDesdeTB.TabIndex = 0;
            // 
            // MotivoCB
            // 
            this.MotivoCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MotivoCB.FormattingEnabled = true;
            this.MotivoCB.Items.AddRange(new object[] {
            "",
            "ACUMULACION DE BOTELLAS",
            "FALTA DE BOTELLAS",
            "LIMPIEZA O LAVADO",
            "PARADA ETIQUETADORA",
            "RECUENTO DE BOTELLAS",
            "ROTURA DE BOTELLAS"});
            this.MotivoCB.Location = new System.Drawing.Point(542, 66);
            this.MotivoCB.Name = "MotivoCB";
            this.MotivoCB.Size = new System.Drawing.Size(512, 28);
            this.MotivoCB.TabIndex = 3;
            this.MotivoCB.TextChanged += new System.EventHandler(this.MotivoCB_TextChanged);
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(386, 66);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(151, 20);
            this.label18.TabIndex = 22;
            this.label18.Text = "Motivo:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(322, 36);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(214, 20);
            this.label19.TabIndex = 20;
            this.label19.Text = "Comienzo de la parada:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ComentariosTB
            // 
            this.ComentariosTB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComentariosTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComentariosTB.Location = new System.Drawing.Point(225, 654);
            this.ComentariosTB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ComentariosTB.Name = "ComentariosTB";
            this.ComentariosTB.Size = new System.Drawing.Size(1086, 202);
            this.ComentariosTB.TabIndex = 229;
            this.ComentariosTB.Text = "";
            this.ComentariosTB.Visible = false;
            this.ComentariosTB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ComentariosTB_MouseClick);
            // 
            // Etiquetadora_Registro_Paro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Yellow;
            this.ClientSize = new System.Drawing.Size(1536, 864);
            this.Controls.Add(this.ComentariosTB);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.saveBot);
            this.Controls.Add(this.CancelarB);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.MinimizarB);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Etiquetadora_Registro_Paro";
            this.Text = "Etiquetadora_Registro_Paro";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Etiquetadora_Registro_Paro_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label DatosEquiposLB;
        private System.Windows.Forms.Label lbReloj;
        private System.Windows.Forms.TextBox turnoTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox maqTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox respTB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox dateTB;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button saveBot;
        private System.Windows.Forms.Button CancelarB;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TemporizadorTB;
        private System.Windows.Forms.Button MinimizarB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox PDesdeTB;
        private System.Windows.Forms.ComboBox MotivoCB;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.RichTextBox ComentariosTB;
        private System.Windows.Forms.ComboBox Motivo2CB;
    }
}
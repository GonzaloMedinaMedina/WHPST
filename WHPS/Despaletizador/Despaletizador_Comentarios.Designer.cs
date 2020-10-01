namespace WHPS.Despaletizador
{
    partial class Despaletizador_Comentarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Despaletizador_Comentarios));
            this.ComentariosTB = new System.Windows.Forms.RichTextBox();
            this.lbReloj = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ExitB = new System.Windows.Forms.Button();
            this.saveBot = new System.Windows.Forms.Button();
            this.CampoIntroduccionLB = new System.Windows.Forms.Label();
            this.MinimizarB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ComentariosTB
            // 
            this.ComentariosTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComentariosTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComentariosTB.Location = new System.Drawing.Point(11, 61);
            this.ComentariosTB.Name = "ComentariosTB";
            this.ComentariosTB.Size = new System.Drawing.Size(1897, 735);
            this.ComentariosTB.TabIndex = 14;
            this.ComentariosTB.Text = "";
            // 
            // lbReloj
            // 
            this.lbReloj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbReloj.BackColor = System.Drawing.Color.Transparent;
            this.lbReloj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReloj.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbReloj.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbReloj.Location = new System.Drawing.Point(1821, 38);
            this.lbReloj.Name = "lbReloj";
            this.lbReloj.Size = new System.Drawing.Size(82, 22);
            this.lbReloj.TabIndex = 23;
            this.lbReloj.Text = "00:00:00";
            this.lbReloj.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
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
            this.ExitB.Location = new System.Drawing.Point(11, 819);
            this.ExitB.Margin = new System.Windows.Forms.Padding(2);
            this.ExitB.Name = "ExitB";
            this.ExitB.Size = new System.Drawing.Size(250, 250);
            this.ExitB.TabIndex = 81;
            this.ExitB.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ExitB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ExitB.UseVisualStyleBackColor = false;
            this.ExitB.Click += new System.EventHandler(this.ExitB_Click);
            // 
            // saveBot
            // 
            this.saveBot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBot.BackgroundImage = global::WHPS.Properties.Resources.GenGuardar;
            this.saveBot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saveBot.FlatAppearance.BorderSize = 0;
            this.saveBot.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.saveBot.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.saveBot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveBot.Location = new System.Drawing.Point(1658, 818);
            this.saveBot.Name = "saveBot";
            this.saveBot.Size = new System.Drawing.Size(250, 250);
            this.saveBot.TabIndex = 80;
            this.saveBot.UseVisualStyleBackColor = true;
            this.saveBot.Click += new System.EventHandler(this.saveBot_Click);
            // 
            // CampoIntroduccionLB
            // 
            this.CampoIntroduccionLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.CampoIntroduccionLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CampoIntroduccionLB.ForeColor = System.Drawing.Color.White;
            this.CampoIntroduccionLB.Location = new System.Drawing.Point(11, 37);
            this.CampoIntroduccionLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CampoIntroduccionLB.Name = "CampoIntroduccionLB";
            this.CampoIntroduccionLB.Size = new System.Drawing.Size(189, 25);
            this.CampoIntroduccionLB.TabIndex = 82;
            this.CampoIntroduccionLB.Text = "COMENTARIOS";
            this.CampoIntroduccionLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MinimizarB
            // 
            this.MinimizarB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimizarB.BackColor = System.Drawing.Color.Gainsboro;
            this.MinimizarB.BackgroundImage = global::WHPS.Properties.Resources.GenMinimizar;
            this.MinimizarB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MinimizarB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizarB.Location = new System.Drawing.Point(1890, 0);
            this.MinimizarB.Name = "MinimizarB";
            this.MinimizarB.Size = new System.Drawing.Size(30, 30);
            this.MinimizarB.TabIndex = 100;
            this.MinimizarB.UseVisualStyleBackColor = false;
            this.MinimizarB.Visible = false;
            this.MinimizarB.Click += new System.EventHandler(this.MinimizarB_Click);
            // 
            // Despaletizador_Comentarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.MinimizarB);
            this.Controls.Add(this.CampoIntroduccionLB);
            this.Controls.Add(this.ExitB);
            this.Controls.Add(this.saveBot);
            this.Controls.Add(this.lbReloj);
            this.Controls.Add(this.ComentariosTB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Despaletizador_Comentarios";
            this.Text = "Despaletizador: Insertar Comentario";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Despaletizador_Comentarios_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox ComentariosTB;
        private System.Windows.Forms.Label lbReloj;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button ExitB;
        private System.Windows.Forms.Button saveBot;
        private System.Windows.Forms.Label CampoIntroduccionLB;
        private System.Windows.Forms.Button MinimizarB;
    }
}
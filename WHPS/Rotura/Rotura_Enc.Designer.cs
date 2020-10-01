namespace WHPS.Rotura
{
    partial class Rotura_Enc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.AvisoCargaLB = new System.Windows.Forms.Label();
            this.dataGridViewRoturas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRoturas)).BeginInit();
            this.SuspendLayout();
            // 
            // AvisoCargaLB
            // 
            this.AvisoCargaLB.AutoSize = true;
            this.AvisoCargaLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvisoCargaLB.Location = new System.Drawing.Point(288, 375);
            this.AvisoCargaLB.Name = "AvisoCargaLB";
            this.AvisoCargaLB.Size = new System.Drawing.Size(344, 29);
            this.AvisoCargaLB.TabIndex = 79;
            this.AvisoCargaLB.Text = "PROCESANDO EL PARTE...";
            this.AvisoCargaLB.Visible = false;
            // 
            // dataGridViewRoturas
            // 
            this.dataGridViewRoturas.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewRoturas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewRoturas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewRoturas.ColumnHeadersHeight = 25;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewRoturas.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewRoturas.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewRoturas.EnableHeadersVisualStyles = false;
            this.dataGridViewRoturas.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.dataGridViewRoturas.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewRoturas.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewRoturas.Name = "dataGridViewRoturas";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Goldenrod;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewRoturas.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewRoturas.RowHeadersVisible = false;
            this.dataGridViewRoturas.RowTemplate.Height = 24;
            this.dataGridViewRoturas.Size = new System.Drawing.Size(966, 265);
            this.dataGridViewRoturas.TabIndex = 80;
            // 
            // Rotura_Enc
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(966, 641);
            this.Controls.Add(this.dataGridViewRoturas);
            this.Controls.Add(this.AvisoCargaLB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Rotura_Enc";
            this.Text = "Rotura_Enc";
            this.Load += new System.EventHandler(this.Rotura_Enc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRoturas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label AvisoCargaLB;
        private System.Windows.Forms.DataGridView dataGridViewRoturas;
    }
}
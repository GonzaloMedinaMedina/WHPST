namespace WHPS.Precinta
{
    partial class MainPrecinta
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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabL2 = new System.Windows.Forms.TabPage();
            this.dataGridL2 = new System.Windows.Forms.DataGridView();
            this.TabL3 = new System.Windows.Forms.TabPage();
            this.dataGridL3 = new System.Windows.Forms.DataGridView();
            this.TabL5 = new System.Windows.Forms.TabPage();
            this.dataGridL5 = new System.Windows.Forms.DataGridView();
            this.TabControl.SuspendLayout();
            this.TabL2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridL2)).BeginInit();
            this.TabL3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridL3)).BeginInit();
            this.TabL5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridL5)).BeginInit();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TabL2);
            this.TabControl.Controls.Add(this.TabL3);
            this.TabControl.Controls.Add(this.TabL5);
            this.TabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl.Location = new System.Drawing.Point(12, 31);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(1276, 521);
            this.TabControl.TabIndex = 0;
            this.TabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // TabL2
            // 
            this.TabL2.Controls.Add(this.dataGridL2);
            this.TabL2.Location = new System.Drawing.Point(4, 27);
            this.TabL2.Name = "TabL2";
            this.TabL2.Padding = new System.Windows.Forms.Padding(3);
            this.TabL2.Size = new System.Drawing.Size(1292, 1013);
            this.TabL2.TabIndex = 0;
            this.TabL2.Text = "L2";
            this.TabL2.UseVisualStyleBackColor = true;
            // 
            // dataGridL2
            // 
            this.dataGridL2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridL2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridL2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridL2.Location = new System.Drawing.Point(3, 3);
            this.dataGridL2.Name = "dataGridL2";
            this.dataGridL2.RowTemplate.Height = 24;
            this.dataGridL2.Size = new System.Drawing.Size(1286, 1007);
            this.dataGridL2.TabIndex = 0;
            // 
            // TabL3
            // 
            this.TabL3.Controls.Add(this.dataGridL3);
            this.TabL3.Location = new System.Drawing.Point(4, 27);
            this.TabL3.Name = "TabL3";
            this.TabL3.Padding = new System.Windows.Forms.Padding(3);
            this.TabL3.Size = new System.Drawing.Size(1292, 1013);
            this.TabL3.TabIndex = 2;
            this.TabL3.Text = "L3";
            this.TabL3.UseVisualStyleBackColor = true;
            // 
            // dataGridL3
            // 
            this.dataGridL3.BackgroundColor = System.Drawing.Color.White;
            this.dataGridL3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridL3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridL3.Location = new System.Drawing.Point(3, 3);
            this.dataGridL3.Name = "dataGridL3";
            this.dataGridL3.RowTemplate.Height = 24;
            this.dataGridL3.Size = new System.Drawing.Size(1286, 1007);
            this.dataGridL3.TabIndex = 1;
            // 
            // TabL5
            // 
            this.TabL5.Controls.Add(this.dataGridL5);
            this.TabL5.Location = new System.Drawing.Point(4, 27);
            this.TabL5.Name = "TabL5";
            this.TabL5.Padding = new System.Windows.Forms.Padding(3);
            this.TabL5.Size = new System.Drawing.Size(1268, 490);
            this.TabL5.TabIndex = 1;
            this.TabL5.Text = "L5";
            this.TabL5.UseVisualStyleBackColor = true;
            // 
            // dataGridL5
            // 
            this.dataGridL5.BackgroundColor = System.Drawing.Color.White;
            this.dataGridL5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridL5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridL5.Location = new System.Drawing.Point(3, 3);
            this.dataGridL5.Name = "dataGridL5";
            this.dataGridL5.RowTemplate.Height = 24;
            this.dataGridL5.Size = new System.Drawing.Size(1262, 484);
            this.dataGridL5.TabIndex = 1;
            // 
            // MainPrecinta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 1044);
            this.Controls.Add(this.TabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainPrecinta";
            this.Text = "MainPrecinta";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainPrecinta_Load);
            this.TabControl.ResumeLayout(false);
            this.TabL2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridL2)).EndInit();
            this.TabL3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridL3)).EndInit();
            this.TabL5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridL5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabL2;
        private System.Windows.Forms.TabPage TabL5;
        private System.Windows.Forms.TabPage TabL3;
        private System.Windows.Forms.DataGridView dataGridL2;
        private System.Windows.Forms.DataGridView dataGridL3;
        private System.Windows.Forms.DataGridView dataGridL5;
    }
}
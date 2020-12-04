namespace WHPS.ProgramMenus
{
    partial class WHPST_MENU
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WHPST_MENU));
            this.PanelMenu = new System.Windows.Forms.Panel();
            this.MenuBOX = new System.Windows.Forms.GroupBox();
            this.ActualizacionLB = new System.Windows.Forms.Label();
            this.NombreLB = new System.Windows.Forms.Label();
            this.PlantaEmbotelladoLB = new System.Windows.Forms.Label();
            this.IconoPT = new System.Windows.Forms.PictureBox();
            this.lbFecha = new System.Windows.Forms.Label();
            this.lbReloj = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PanelMenu.SuspendLayout();
            this.MenuBOX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconoPT)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelMenu
            // 
            this.PanelMenu.BackColor = System.Drawing.SystemColors.Control;
            this.PanelMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PanelMenu.Controls.Add(this.MenuBOX);
            this.PanelMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMenu.Location = new System.Drawing.Point(0, 0);
            this.PanelMenu.Margin = new System.Windows.Forms.Padding(2);
            this.PanelMenu.Name = "PanelMenu";
            this.PanelMenu.Size = new System.Drawing.Size(1376, 857);
            this.PanelMenu.TabIndex = 38;
            // 
            // MenuBOX
            // 
            this.MenuBOX.BackColor = System.Drawing.SystemColors.Control;
            this.MenuBOX.Controls.Add(this.ActualizacionLB);
            this.MenuBOX.Controls.Add(this.NombreLB);
            this.MenuBOX.Controls.Add(this.PlantaEmbotelladoLB);
            this.MenuBOX.Controls.Add(this.IconoPT);
            this.MenuBOX.Controls.Add(this.lbFecha);
            this.MenuBOX.Controls.Add(this.lbReloj);
            this.MenuBOX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MenuBOX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MenuBOX.Location = new System.Drawing.Point(0, 0);
            this.MenuBOX.Name = "MenuBOX";
            this.MenuBOX.Size = new System.Drawing.Size(1376, 857);
            this.MenuBOX.TabIndex = 48;
            this.MenuBOX.TabStop = false;
            // 
            // ActualizacionLB
            // 
            this.ActualizacionLB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ActualizacionLB.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActualizacionLB.Location = new System.Drawing.Point(1232, 8);
            this.ActualizacionLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ActualizacionLB.Name = "ActualizacionLB";
            this.ActualizacionLB.Size = new System.Drawing.Size(142, 23);
            this.ActualizacionLB.TabIndex = 50;
            this.ActualizacionLB.Text = "Actualización: 04/12/2020";
            this.ActualizacionLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ActualizacionLB.DoubleClick += new System.EventHandler(this.ActualizacionLB_DoubleClick);
            // 
            // NombreLB
            // 
            this.NombreLB.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.NombreLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NombreLB.Location = new System.Drawing.Point(362, 658);
            this.NombreLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NombreLB.Name = "NombreLB";
            this.NombreLB.Size = new System.Drawing.Size(652, 50);
            this.NombreLB.TabIndex = 49;
            this.NombreLB.Text = "Williams && Humbert Production System Tool";
            this.NombreLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PlantaEmbotelladoLB
            // 
            this.PlantaEmbotelladoLB.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.PlantaEmbotelladoLB.BackColor = System.Drawing.Color.Maroon;
            this.PlantaEmbotelladoLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlantaEmbotelladoLB.ForeColor = System.Drawing.Color.White;
            this.PlantaEmbotelladoLB.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.PlantaEmbotelladoLB.Location = new System.Drawing.Point(346, 582);
            this.PlantaEmbotelladoLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PlantaEmbotelladoLB.Name = "PlantaEmbotelladoLB";
            this.PlantaEmbotelladoLB.Size = new System.Drawing.Size(693, 59);
            this.PlantaEmbotelladoLB.TabIndex = 48;
            this.PlantaEmbotelladoLB.Text = "PLANTA DE EMBOTELLADO";
            this.PlantaEmbotelladoLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IconoPT
            // 
            this.IconoPT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconoPT.BackColor = System.Drawing.Color.Transparent;
            this.IconoPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.IconoPT.Image = global::WHPS.Properties.Resources.GenICONO_WH;
            this.IconoPT.InitialImage = null;
            this.IconoPT.Location = new System.Drawing.Point(394, 171);
            this.IconoPT.Margin = new System.Windows.Forms.Padding(2);
            this.IconoPT.Name = "IconoPT";
            this.IconoPT.Size = new System.Drawing.Size(603, 342);
            this.IconoPT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.IconoPT.TabIndex = 47;
            this.IconoPT.TabStop = false;
            // 
            // lbFecha
            // 
            this.lbFecha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFecha.BackColor = System.Drawing.Color.Transparent;
            this.lbFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.lbFecha.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbFecha.Location = new System.Drawing.Point(58, 800);
            this.lbFecha.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFecha.Name = "lbFecha";
            this.lbFecha.Size = new System.Drawing.Size(1275, 39);
            this.lbFecha.TabIndex = 43;
            this.lbFecha.Text = "Fecha";
            this.lbFecha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbReloj
            // 
            this.lbReloj.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbReloj.BackColor = System.Drawing.Color.Transparent;
            this.lbReloj.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReloj.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(159)))), ((int)(((byte)(55)))));
            this.lbReloj.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbReloj.Location = new System.Drawing.Point(180, 732);
            this.lbReloj.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbReloj.Name = "lbReloj";
            this.lbReloj.Size = new System.Drawing.Size(1036, 84);
            this.lbReloj.TabIndex = 42;
            this.lbReloj.Text = "Hora";
            this.lbReloj.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // WHPST_MENU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1376, 857);
            this.Controls.Add(this.PanelMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "WHPST_MENU";
            this.Text = "WHPST_MENU";
            this.Load += new System.EventHandler(this.WHPST_MENU_Load);
            this.PanelMenu.ResumeLayout(false);
            this.MenuBOX.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IconoPT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelMenu;
        private System.Windows.Forms.GroupBox MenuBOX;
        private System.Windows.Forms.Label lbFecha;
        private System.Windows.Forms.Label lbReloj;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label NombreLB;
        private System.Windows.Forms.Label PlantaEmbotelladoLB;
        private System.Windows.Forms.PictureBox IconoPT;
        private System.Windows.Forms.Label ActualizacionLB;
    }
}
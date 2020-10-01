namespace WHPS.Rotura
{
    partial class Rotura_Menu
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
            this.IconoPT = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.IconoPT)).BeginInit();
            this.SuspendLayout();
            // 
            // IconoPT
            // 
            this.IconoPT.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IconoPT.BackColor = System.Drawing.Color.Transparent;
            this.IconoPT.BackgroundImage = global::WHPS.Properties.Resources.GenICONO_WH;
            this.IconoPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.IconoPT.InitialImage = null;
            this.IconoPT.Location = new System.Drawing.Point(325, 187);
            this.IconoPT.Margin = new System.Windows.Forms.Padding(2);
            this.IconoPT.Name = "IconoPT";
            this.IconoPT.Size = new System.Drawing.Size(274, 248);
            this.IconoPT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.IconoPT.TabIndex = 36;
            this.IconoPT.TabStop = false;
            // 
            // Rotura_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(966, 641);
            this.Controls.Add(this.IconoPT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Rotura_Menu";
            this.Text = "Rotura_Menu";
            this.Load += new System.EventHandler(this.Rotura_Menu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IconoPT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox IconoPT;
    }
}
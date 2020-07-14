namespace MineFishV2_Setting.Ctl {
    partial class Link {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.iconBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).BeginInit();
            this.SuspendLayout();
            // 
            // iconBox
            // 
            this.iconBox.BackColor = System.Drawing.Color.Transparent;
            this.iconBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconBox.Location = new System.Drawing.Point(0, 0);
            this.iconBox.Name = "iconBox";
            this.iconBox.Size = new System.Drawing.Size(50, 50);
            this.iconBox.TabIndex = 0;
            this.iconBox.TabStop = false;
            this.iconBox.Click += new System.EventHandler(this.iconBox_Click);
            this.iconBox.DoubleClick += new System.EventHandler(this.iconBox_Click);
            this.iconBox.Resize += new System.EventHandler(this.iconBox_Resize);
            // 
            // Link
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.iconBox);
            this.Name = "Link";
            this.Size = new System.Drawing.Size(50, 50);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Link_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox iconBox;
    }
}

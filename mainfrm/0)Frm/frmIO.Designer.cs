namespace mainfrm
{
    partial class frmIO
    {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.gbX = new System.Windows.Forms.GroupBox();
            this.gbY = new System.Windows.Forms.GroupBox();
            this.tmUIUpdate = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // gbX
            // 
            this.gbX.Location = new System.Drawing.Point(48, 30);
            this.gbX.Name = "gbX";
            this.gbX.Size = new System.Drawing.Size(687, 176);
            this.gbX.TabIndex = 0;
            this.gbX.TabStop = false;
            this.gbX.Text = "X축";
            // 
            // gbY
            // 
            this.gbY.Location = new System.Drawing.Point(48, 241);
            this.gbY.Name = "gbY";
            this.gbY.Size = new System.Drawing.Size(687, 176);
            this.gbY.TabIndex = 1;
            this.gbY.TabStop = false;
            this.gbY.Text = "Y축";
            // 
            // tmUIUpdate
            // 
            this.tmUIUpdate.Tick += new System.EventHandler(this.tmUIUpdate_Tick);
            // 
            // frmIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbY);
            this.Controls.Add(this.gbX);
            this.Name = "frmIO";
            this.Text = "frmIO";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbX;
        private System.Windows.Forms.GroupBox gbY;
        private System.Windows.Forms.Timer tmUIUpdate;
    }
}
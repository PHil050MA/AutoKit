namespace mainfrm
{
    partial class frmFDC
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
            this.gridSVID = new System.Windows.Forms.DataGridView();
            this.gridConfig = new System.Windows.Forms.DataGridView();
            this.tmFDC = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridSVID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSVID
            // 
            this.gridSVID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSVID.Location = new System.Drawing.Point(38, 267);
            this.gridSVID.Name = "gridSVID";
            this.gridSVID.RowTemplate.Height = 23;
            this.gridSVID.Size = new System.Drawing.Size(580, 132);
            this.gridSVID.TabIndex = 3;
            // 
            // gridConfig
            // 
            this.gridConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridConfig.Location = new System.Drawing.Point(38, 91);
            this.gridConfig.Name = "gridConfig";
            this.gridConfig.RowTemplate.Height = 23;
            this.gridConfig.Size = new System.Drawing.Size(580, 152);
            this.gridConfig.TabIndex = 2;
            // 
            // tmFDC
            // 
            this.tmFDC.Tick += new System.EventHandler(this.tmFDC_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(38, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 37);
            this.button1.TabIndex = 4;
            this.button1.Text = "FDC 저장";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(160, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 37);
            this.button2.TabIndex = 5;
            this.button2.Text = "FDC 데이터 로드";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmFDC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 461);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gridSVID);
            this.Controls.Add(this.gridConfig);
            this.Name = "frmFDC";
            this.Text = "FDCAgent";
            ((System.ComponentModel.ISupportInitialize)(this.gridSVID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridSVID;
        private System.Windows.Forms.DataGridView gridConfig;
        private System.Windows.Forms.Timer tmFDC;
        private System.Windows.Forms.Button button1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button button2;
    }
}
namespace mainfrm
{
    partial class frmImage
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
            this.picture = new System.Windows.Forms.PictureBox();
            this.btnGetValue = new System.Windows.Forms.Button();
            this.btnROISave = new System.Windows.Forms.Button();
            this.btnLive = new System.Windows.Forms.Button();
            this.cbProduct = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.gridColorParam = new System.Windows.Forms.DataGridView();
            this.gridROI = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridColorParam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridROI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // picture
            // 
            this.picture.Location = new System.Drawing.Point(83, 219);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(644, 362);
            this.picture.TabIndex = 7;
            this.picture.TabStop = false;
            // 
            // btnGetValue
            // 
            this.btnGetValue.Location = new System.Drawing.Point(591, 76);
            this.btnGetValue.Name = "btnGetValue";
            this.btnGetValue.Size = new System.Drawing.Size(137, 29);
            this.btnGetValue.TabIndex = 6;
            this.btnGetValue.Text = "Get Value";
            this.btnGetValue.UseVisualStyleBackColor = true;
            this.btnGetValue.Click += new System.EventHandler(this.btnGetValue_Click);
            // 
            // btnROISave
            // 
            this.btnROISave.Location = new System.Drawing.Point(226, 39);
            this.btnROISave.Name = "btnROISave";
            this.btnROISave.Size = new System.Drawing.Size(118, 27);
            this.btnROISave.TabIndex = 5;
            this.btnROISave.Text = "ROI Save";
            this.btnROISave.UseVisualStyleBackColor = true;
            this.btnROISave.Click += new System.EventHandler(this.btnROISave_Click);
            // 
            // btnLive
            // 
            this.btnLive.Location = new System.Drawing.Point(83, 39);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(115, 27);
            this.btnLive.TabIndex = 4;
            this.btnLive.Text = "Live";
            this.btnLive.UseVisualStyleBackColor = true;
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // cbProduct
            // 
            this.cbProduct.FormattingEnabled = true;
            this.cbProduct.Location = new System.Drawing.Point(590, 122);
            this.cbProduct.Name = "cbProduct";
            this.cbProduct.Size = new System.Drawing.Size(137, 20);
            this.cbProduct.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(733, 219);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 27);
            this.button1.TabIndex = 9;
            this.button1.Text = "find";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gridColorParam
            // 
            this.gridColorParam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridColorParam.Location = new System.Drawing.Point(350, 72);
            this.gridColorParam.Name = "gridColorParam";
            this.gridColorParam.RowTemplate.Height = 23;
            this.gridColorParam.Size = new System.Drawing.Size(235, 141);
            this.gridColorParam.TabIndex = 11;
            // 
            // gridROI
            // 
            this.gridROI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridROI.Location = new System.Drawing.Point(82, 72);
            this.gridROI.Name = "gridROI";
            this.gridROI.RowTemplate.Height = 23;
            this.gridROI.Size = new System.Drawing.Size(262, 141);
            this.gridROI.TabIndex = 10;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(590, 148);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 29);
            this.button2.TabIndex = 12;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(733, 252);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(155, 142);
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // frmImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 593);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.gridColorParam);
            this.Controls.Add(this.gridROI);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbProduct);
            this.Controls.Add(this.picture);
            this.Controls.Add(this.btnGetValue);
            this.Controls.Add(this.btnROISave);
            this.Controls.Add(this.btnLive);
            this.Name = "frmImage";
            this.Text = "frmImage";
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridColorParam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridROI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.Button btnGetValue;
        private System.Windows.Forms.Button btnROISave;
        private System.Windows.Forms.Button btnLive;
        private System.Windows.Forms.ComboBox cbProduct;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView gridColorParam;
        private System.Windows.Forms.DataGridView gridROI;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
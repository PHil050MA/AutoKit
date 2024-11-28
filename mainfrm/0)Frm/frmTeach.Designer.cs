namespace mainfrm
{
    partial class frmTeach
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnMove = new System.Windows.Forms.Button();
            this.btnTeach = new System.Windows.Forms.Button();
            this.lvTeachTable = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button29 = new System.Windows.Forms.Button();
            this.button28 = new System.Windows.Forms.Button();
            this.button27 = new System.Windows.Forms.Button();
            this.button26 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.gbX = new System.Windows.Forms.GroupBox();
            this.tbXMove = new System.Windows.Forms.TextBox();
            this.btnInchMove = new System.Windows.Forms.Button();
            this.gbY = new System.Windows.Forms.GroupBox();
            this.tbYMove = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.btnLoadTeach = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.gbX.SuspendLayout();
            this.gbY.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoadTeach);
            this.groupBox1.Controls.Add(this.btnMove);
            this.groupBox1.Controls.Add(this.btnTeach);
            this.groupBox1.Controls.Add(this.lvTeachTable);
            this.groupBox1.Location = new System.Drawing.Point(30, 256);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 247);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "티칭 위치";
            // 
            // btnMove
            // 
            this.btnMove.Location = new System.Drawing.Point(229, 213);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(97, 23);
            this.btnMove.TabIndex = 2;
            this.btnMove.Text = "Move";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnTeach
            // 
            this.btnTeach.Location = new System.Drawing.Point(112, 212);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(111, 23);
            this.btnTeach.TabIndex = 1;
            this.btnTeach.Text = "보내기위치 저장";
            this.btnTeach.UseVisualStyleBackColor = true;
            this.btnTeach.Click += new System.EventHandler(this.btnTeach_Click);
            // 
            // lvTeachTable
            // 
            this.lvTeachTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvTeachTable.FullRowSelect = true;
            this.lvTeachTable.GridLines = true;
            this.lvTeachTable.HideSelection = false;
            this.lvTeachTable.Location = new System.Drawing.Point(7, 21);
            this.lvTeachTable.Name = "lvTeachTable";
            this.lvTeachTable.Size = new System.Drawing.Size(319, 185);
            this.lvTeachTable.TabIndex = 0;
            this.lvTeachTable.UseCompatibleStateImageBehavior = false;
            this.lvTeachTable.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Position Num";
            this.columnHeader1.Width = 111;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "X Axis Value";
            this.columnHeader2.Width = 97;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Y Axis Value";
            this.columnHeader3.Width = 102;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button29);
            this.groupBox2.Controls.Add(this.button28);
            this.groupBox2.Controls.Add(this.button27);
            this.groupBox2.Controls.Add(this.button26);
            this.groupBox2.Location = new System.Drawing.Point(384, 327);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(266, 165);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cylinder";
            // 
            // button29
            // 
            this.button29.Location = new System.Drawing.Point(147, 125);
            this.button29.Name = "button29";
            this.button29.Size = new System.Drawing.Size(75, 23);
            this.button29.TabIndex = 3;
            this.button29.Text = "Vac Off";
            this.button29.UseVisualStyleBackColor = true;
            this.button29.Click += new System.EventHandler(this.button29_Click);
            // 
            // button28
            // 
            this.button28.BackColor = System.Drawing.SystemColors.Control;
            this.button28.Location = new System.Drawing.Point(147, 53);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(75, 23);
            this.button28.TabIndex = 2;
            this.button28.Text = "Vac On";
            this.button28.UseVisualStyleBackColor = false;
            this.button28.Click += new System.EventHandler(this.button28_Click);
            // 
            // button27
            // 
            this.button27.Location = new System.Drawing.Point(31, 125);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(75, 23);
            this.button27.TabIndex = 1;
            this.button27.Text = "Down";
            this.button27.UseVisualStyleBackColor = true;
            this.button27.Click += new System.EventHandler(this.button27_Click);
            // 
            // button26
            // 
            this.button26.Location = new System.Drawing.Point(31, 53);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(75, 23);
            this.button26.TabIndex = 0;
            this.button26.Text = "Up";
            this.button26.UseVisualStyleBackColor = true;
            this.button26.Click += new System.EventHandler(this.button26_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button4);
            this.groupBox5.Controls.Add(this.button3);
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Location = new System.Drawing.Point(390, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(165, 177);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Jog Motion";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(108, 71);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(50, 42);
            this.button4.TabIndex = 3;
            this.button4.Text = "▶";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.button4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button4_MouseUp);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(8, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 42);
            this.button3.TabIndex = 2;
            this.button3.Text = "◀";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.button3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button4_MouseUp);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(58, 127);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 42);
            this.button2.TabIndex = 1;
            this.button2.Text = "▼";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.button2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button4_MouseUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(58, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "▲";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.button1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button4_MouseUp);
            // 
            // gbX
            // 
            this.gbX.Controls.Add(this.tbXMove);
            this.gbX.Controls.Add(this.btnInchMove);
            this.gbX.Location = new System.Drawing.Point(423, 215);
            this.gbX.Name = "gbX";
            this.gbX.Size = new System.Drawing.Size(222, 47);
            this.gbX.TabIndex = 20;
            this.gbX.TabStop = false;
            this.gbX.Text = "x축 이동";
            // 
            // tbXMove
            // 
            this.tbXMove.Location = new System.Drawing.Point(11, 20);
            this.tbXMove.Name = "tbXMove";
            this.tbXMove.Size = new System.Drawing.Size(75, 21);
            this.tbXMove.TabIndex = 11;
            // 
            // btnInchMove
            // 
            this.btnInchMove.Location = new System.Drawing.Point(128, 20);
            this.btnInchMove.Name = "btnInchMove";
            this.btnInchMove.Size = new System.Drawing.Size(75, 23);
            this.btnInchMove.TabIndex = 10;
            this.btnInchMove.Text = "Move";
            this.btnInchMove.UseVisualStyleBackColor = true;
            this.btnInchMove.Click += new System.EventHandler(this.btnInchMove_Click);
            // 
            // gbY
            // 
            this.gbY.Controls.Add(this.tbYMove);
            this.gbY.Controls.Add(this.button5);
            this.gbY.Location = new System.Drawing.Point(423, 267);
            this.gbY.Name = "gbY";
            this.gbY.Size = new System.Drawing.Size(222, 47);
            this.gbY.TabIndex = 21;
            this.gbY.TabStop = false;
            this.gbY.Text = "Y축 이동";
            // 
            // tbYMove
            // 
            this.tbYMove.Location = new System.Drawing.Point(11, 20);
            this.tbYMove.Name = "tbYMove";
            this.tbYMove.Size = new System.Drawing.Size(75, 21);
            this.tbYMove.TabIndex = 11;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(128, 20);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 10;
            this.button5.Text = "Move";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnInchMove_Click);
            // 
            // btnLoadTeach
            // 
            this.btnLoadTeach.Location = new System.Drawing.Point(7, 212);
            this.btnLoadTeach.Name = "btnLoadTeach";
            this.btnLoadTeach.Size = new System.Drawing.Size(99, 23);
            this.btnLoadTeach.TabIndex = 3;
            this.btnLoadTeach.Text = "받기위치 저장";
            this.btnLoadTeach.UseVisualStyleBackColor = true;
            this.btnLoadTeach.Click += new System.EventHandler(this.btnLoadTeach_Click);
            // 
            // frmTeach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 511);
            this.Controls.Add(this.gbY);
            this.Controls.Add(this.gbX);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmTeach";
            this.Text = "frmTeach";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.gbX.ResumeLayout(false);
            this.gbX.PerformLayout();
            this.gbY.ResumeLayout(false);
            this.gbY.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Button btnTeach;
        private System.Windows.Forms.ListView lvTeachTable;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button29;
        private System.Windows.Forms.Button button28;
        private System.Windows.Forms.Button button27;
        private System.Windows.Forms.Button button26;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gbX;
        private System.Windows.Forms.TextBox tbXMove;
        private System.Windows.Forms.Button btnInchMove;
        private System.Windows.Forms.GroupBox gbY;
        private System.Windows.Forms.TextBox tbYMove;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnLoadTeach;
    }
}
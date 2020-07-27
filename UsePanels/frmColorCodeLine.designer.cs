namespace MapWinGIS_AE
{
    partial class frmColorCodeLine
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
            this.lstIRIValue = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.txtFair = new System.Windows.Forms.TextBox();
            this.txtGood = new System.Windows.Forms.TextBox();
            this.btnColorCode = new System.Windows.Forms.Button();
            this.lblCopyFair = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstIRIValue
            // 
            this.lstIRIValue.FormattingEnabled = true;
            this.lstIRIValue.Location = new System.Drawing.Point(27, 39);
            this.lstIRIValue.Name = "lstIRIValue";
            this.lstIRIValue.Size = new System.Drawing.Size(120, 277);
            this.lstIRIValue.TabIndex = 0;
            this.lstIRIValue.SelectedIndexChanged += new System.EventHandler(this.lstIRIValue_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 164);
            this.label1.MaximumSize = new System.Drawing.Size(195, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Poor / Fair Boundary";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 39);
            this.label2.MaximumSize = new System.Drawing.Size(195, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fair / Good Boundary";
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(24, 9);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(27, 13);
            this.lblFieldName.TabIndex = 3;
            this.lblFieldName.Text = "Title";
            // 
            // txtFair
            // 
            this.txtFair.Location = new System.Drawing.Point(217, 225);
            this.txtFair.Name = "txtFair";
            this.txtFair.Size = new System.Drawing.Size(100, 20);
            this.txtFair.TabIndex = 4;
            // 
            // txtGood
            // 
            this.txtGood.Location = new System.Drawing.Point(213, 104);
            this.txtGood.Name = "txtGood";
            this.txtGood.Size = new System.Drawing.Size(100, 20);
            this.txtGood.TabIndex = 5;
            // 
            // btnColorCode
            // 
            this.btnColorCode.Location = new System.Drawing.Point(217, 282);
            this.btnColorCode.Name = "btnColorCode";
            this.btnColorCode.Size = new System.Drawing.Size(100, 23);
            this.btnColorCode.TabIndex = 6;
            this.btnColorCode.Text = "Apply";
            this.btnColorCode.UseVisualStyleBackColor = true;
            this.btnColorCode.Click += new System.EventHandler(this.btnColorCode_Click);
            // 
            // lblCopyFair
            // 
            this.lblCopyFair.AutoSize = true;
            this.lblCopyFair.Location = new System.Drawing.Point(175, 215);
            this.lblCopyFair.Name = "lblCopyFair";
            this.lblCopyFair.Size = new System.Drawing.Size(19, 13);
            this.lblCopyFair.TabIndex = 7;
            this.lblCopyFair.Text = ">>";
            this.lblCopyFair.Click += new System.EventHandler(this.lblCopyFair_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = ">>";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(175, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "<<";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(171, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "<<";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(214, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Suggest (Avg - Std)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(220, 209);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Suggest(Avg+Std)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(214, 61);
            this.label8.MaximumSize = new System.Drawing.Size(195, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "(currently using: 275)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(212, 186);
            this.label9.MaximumSize = new System.Drawing.Size(195, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "(currently using: 501";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 22);
            this.label10.MaximumSize = new System.Drawing.Size(195, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "(Average)";
            // 
            // frmColorCodeLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 327);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCopyFair);
            this.Controls.Add(this.btnColorCode);
            this.Controls.Add(this.txtGood);
            this.Controls.Add(this.txtFair);
            this.Controls.Add(this.lblFieldName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstIRIValue);
            this.Name = "frmColorCodeLine";
            this.Text = "Choose ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ListBox lstIRIValue;
        public System.Windows.Forms.Label lblFieldName;
        public System.Windows.Forms.TextBox txtFair;
        public System.Windows.Forms.TextBox txtGood;
        public System.Windows.Forms.Button btnColorCode;
        private System.Windows.Forms.Label lblCopyFair;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label10;
    }
}
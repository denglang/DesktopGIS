namespace MapWinGIS_AE
{
    partial class frmSelectByAttributes
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbLayer = new System.Windows.Forms.ComboBox();
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.lstFieldName = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.btnOr = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.btnNot = new System.Windows.Forms.Button();
            this.lstFieldValue = new System.Windows.Forms.ListBox();
            this.rTxtQueryBuilder = new System.Windows.Forms.RichTextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.btnGetFieldValue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Layer: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Method: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 311);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "SELECT * FROM layer WHERE:";
            // 
            // cmbLayer
            // 
            this.cmbLayer.FormattingEnabled = true;
            this.cmbLayer.Items.AddRange(new object[] {
            "Select a layer below: "});
            this.cmbLayer.Location = new System.Drawing.Point(82, 6);
            this.cmbLayer.Name = "cmbLayer";
            this.cmbLayer.Size = new System.Drawing.Size(223, 21);
            this.cmbLayer.TabIndex = 3;
            this.cmbLayer.SelectedIndexChanged += new System.EventHandler(this.cmbLayer_SelectedIndexChanged);
            // 
            // cmbMethod
            // 
            this.cmbMethod.DisplayMember = "Creae a new selection";
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.Items.AddRange(new object[] {
            "Creae a new selection",
            "Remove from current selection",
            "Add to current selection",
            "Select from current selection"});
            this.cmbMethod.Location = new System.Drawing.Point(82, 35);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(223, 21);
            this.cmbMethod.TabIndex = 4;
            this.cmbMethod.Text = "Creae a new selection";
            // 
            // lstFieldName
            // 
            this.lstFieldName.FormattingEnabled = true;
            this.lstFieldName.Location = new System.Drawing.Point(15, 62);
            this.lstFieldName.Name = "lstFieldName";
            this.lstFieldName.Size = new System.Drawing.Size(290, 95);
            this.lstFieldName.TabIndex = 5;
            this.lstFieldName.SelectedIndexChanged += new System.EventHandler(this.lstFieldName_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "=";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(57, 167);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(31, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "<>";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(15, 196);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(31, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = ">";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(15, 225);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(31, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "<";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(94, 167);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(41, 23);
            this.button5.TabIndex = 10;
            this.button5.Text = "Like";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(57, 196);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(31, 23);
            this.button6.TabIndex = 11;
            this.button6.Text = ">=";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(94, 196);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(41, 23);
            this.button7.TabIndex = 12;
            this.button7.Text = "And";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // btnOr
            // 
            this.btnOr.Location = new System.Drawing.Point(94, 225);
            this.btnOr.Name = "btnOr";
            this.btnOr.Size = new System.Drawing.Size(41, 23);
            this.btnOr.TabIndex = 13;
            this.btnOr.Text = "Or";
            this.btnOr.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(15, 254);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(31, 23);
            this.button9.TabIndex = 14;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(57, 225);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(31, 23);
            this.button10.TabIndex = 15;
            this.button10.Text = "<=";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(57, 254);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(31, 23);
            this.button11.TabIndex = 16;
            this.button11.Text = "( )";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // btnNot
            // 
            this.btnNot.Location = new System.Drawing.Point(94, 254);
            this.btnNot.Name = "btnNot";
            this.btnNot.Size = new System.Drawing.Size(41, 23);
            this.btnNot.TabIndex = 17;
            this.btnNot.Text = "Not";
            this.btnNot.UseVisualStyleBackColor = true;
            // 
            // lstFieldValue
            // 
            this.lstFieldValue.FormattingEnabled = true;
            this.lstFieldValue.Location = new System.Drawing.Point(141, 167);
            this.lstFieldValue.Name = "lstFieldValue";
            this.lstFieldValue.Size = new System.Drawing.Size(174, 108);
            this.lstFieldValue.TabIndex = 18;
            this.lstFieldValue.SelectedIndexChanged += new System.EventHandler(this.lstFieldValue_SelectedIndexChanged);
            // 
            // rTxtQueryBuilder
            // 
            this.rTxtQueryBuilder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rTxtQueryBuilder.Location = new System.Drawing.Point(12, 327);
            this.rTxtQueryBuilder.Name = "rTxtQueryBuilder";
            this.rTxtQueryBuilder.Size = new System.Drawing.Size(293, 106);
            this.rTxtQueryBuilder.TabIndex = 20;
            this.rTxtQueryBuilder.Text = "";
            this.rTxtQueryBuilder.TextChanged += new System.EventHandler(this.rTxtQueryBuilder_TextChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(7, 440);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(54, 23);
            this.btnClear.TabIndex = 21;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(67, 440);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(54, 23);
            this.button14.TabIndex = 22;
            this.button14.Text = "Help";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(131, 440);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(54, 23);
            this.btnOK.TabIndex = 23;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(251, 440);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(54, 23);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(191, 439);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(54, 23);
            this.btnApply.TabIndex = 26;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(94, 285);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(41, 23);
            this.button8.TabIndex = 29;
            this.button8.Text = "Null";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(57, 285);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(31, 23);
            this.button12.TabIndex = 28;
            this.button12.Text = "In";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(15, 285);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(31, 23);
            this.button13.TabIndex = 27;
            this.button13.Text = "Is";
            this.button13.UseVisualStyleBackColor = true;
            // 
            // btnGetFieldValue
            // 
            this.btnGetFieldValue.Location = new System.Drawing.Point(141, 285);
            this.btnGetFieldValue.Name = "btnGetFieldValue";
            this.btnGetFieldValue.Size = new System.Drawing.Size(118, 23);
            this.btnGetFieldValue.TabIndex = 30;
            this.btnGetFieldValue.Text = "get Unique Values";
            this.btnGetFieldValue.UseVisualStyleBackColor = true;
            this.btnGetFieldValue.Click += new System.EventHandler(this.btnGetFieldValue_Click);
            // 
            // frmSelectByAttributes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 467);
            this.Controls.Add(this.btnGetFieldValue);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.rTxtQueryBuilder);
            this.Controls.Add(this.lstFieldValue);
            this.Controls.Add(this.btnNot);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.btnOr);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lstFieldName);
            this.Controls.Add(this.cmbMethod);
            this.Controls.Add(this.cmbLayer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmSelectByAttributes";
            this.Text = "frmSelectByAttributes";
            this.Load += new System.EventHandler(this.frmSelectByAttributes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btnOr;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button btnNot;
        private System.Windows.Forms.ListBox lstFieldValue;
        private System.Windows.Forms.RichTextBox rTxtQueryBuilder;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnApply;
        public System.Windows.Forms.ComboBox cmbLayer;
        public System.Windows.Forms.ComboBox cmbMethod;
        public System.Windows.Forms.ListBox lstFieldName;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button btnGetFieldValue;
    }
}
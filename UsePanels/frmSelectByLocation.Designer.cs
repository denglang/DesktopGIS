namespace UsePanels
{
    partial class frmSelectByLocation
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
            this.lblSelectByLocationTitle = new System.Windows.Forms.Label();
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSourceLayer = new System.Windows.Forms.ComboBox();
            this.chkLstBox = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSpatialMethod = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSelectByLocationTitle
            // 
            this.lblSelectByLocationTitle.AutoSize = true;
            this.lblSelectByLocationTitle.Location = new System.Drawing.Point(13, 9);
            this.lblSelectByLocationTitle.MaximumSize = new System.Drawing.Size(300, 0);
            this.lblSelectByLocationTitle.Name = "lblSelectByLocationTitle";
            this.lblSelectByLocationTitle.Size = new System.Drawing.Size(288, 26);
            this.lblSelectByLocationTitle.TabIndex = 0;
            this.lblSelectByLocationTitle.Text = "Select feature from one or more target layers based on their location in relation" +
    " to the features in the source layer";
            this.lblSelectByLocationTitle.Click += new System.EventHandler(this.lblSelectByLocationTitle_Click);
            // 
            // cmbMethod
            // 
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.Items.AddRange(new object[] {
            "Select features from"});
            this.cmbMethod.Location = new System.Drawing.Point(12, 62);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(337, 21);
            this.cmbMethod.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Target layer(s):";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Selection methods";
            // 
            // cmbSourceLayer
            // 
            this.cmbSourceLayer.FormattingEnabled = true;
            this.cmbSourceLayer.Items.AddRange(new object[] {
            "--Select  a layer below--"});
            this.cmbSourceLayer.Location = new System.Drawing.Point(12, 358);
            this.cmbSourceLayer.Name = "cmbSourceLayer";
            this.cmbSourceLayer.Size = new System.Drawing.Size(337, 21);
            this.cmbSourceLayer.Sorted = true;
            this.cmbSourceLayer.TabIndex = 5;
            // 
            // chkLstBox
            // 
            this.chkLstBox.FormattingEnabled = true;
            this.chkLstBox.Location = new System.Drawing.Point(12, 118);
            this.chkLstBox.Name = "chkLstBox";
            this.chkLstBox.Size = new System.Drawing.Size(337, 199);
            this.chkLstBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 339);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Source layer:";
            // 
            // cmbSpatialMethod
            // 
            this.cmbSpatialMethod.FormattingEnabled = true;
            this.cmbSpatialMethod.Items.AddRange(new object[] {
            "intersect the source layer feature",
            "completely contain the source layer feature",
            "are within a distance of the source layer feature"});
            this.cmbSpatialMethod.Location = new System.Drawing.Point(12, 421);
            this.cmbSpatialMethod.Name = "cmbSpatialMethod";
            this.cmbSpatialMethod.Size = new System.Drawing.Size(337, 21);
            this.cmbSpatialMethod.TabIndex = 8;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 386);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(93, 478);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(236, 478);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 406);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(238, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Spatial Selection method  for target layer features";
            // 
            // frmSelectByLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 513);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cmbSpatialMethod);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkLstBox);
            this.Controls.Add(this.cmbSourceLayer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbMethod);
            this.Controls.Add(this.lblSelectByLocationTitle);
            this.Name = "frmSelectByLocation";
            this.Text = "frmSelectByLocation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSelectByLocationTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.ComboBox cmbMethod;
        public System.Windows.Forms.ComboBox cmbSourceLayer;
        public System.Windows.Forms.CheckedListBox chkLstBox;
        public System.Windows.Forms.ComboBox cmbSpatialMethod;
        private System.Windows.Forms.Label label4;
    }
}
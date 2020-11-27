namespace UsePanels
{
    partial class frmLayerProperties
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
            this.lblLayerDescription = new System.Windows.Forms.Label();
            this.lblLayerSource = new System.Windows.Forms.Label();
            this.lblMaxScale = new System.Windows.Forms.Label();
            this.lblMinScale = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLayerDescription
            // 
            this.lblLayerDescription.AutoSize = true;
            this.lblLayerDescription.Location = new System.Drawing.Point(35, 42);
            this.lblLayerDescription.Name = "lblLayerDescription";
            this.lblLayerDescription.Size = new System.Drawing.Size(58, 13);
            this.lblLayerDescription.TabIndex = 0;
            this.lblLayerDescription.Text = "description";
            // 
            // lblLayerSource
            // 
            this.lblLayerSource.AutoSize = true;
            this.lblLayerSource.Location = new System.Drawing.Point(35, 97);
            this.lblLayerSource.Name = "lblLayerSource";
            this.lblLayerSource.Size = new System.Drawing.Size(75, 13);
            this.lblLayerSource.TabIndex = 1;
            this.lblLayerSource.Text = "Name & Source";
            // 
            // lblMaxScale
            // 
            this.lblMaxScale.AutoSize = true;
            this.lblMaxScale.Location = new System.Drawing.Point(35, 150);
            this.lblMaxScale.Name = "lblMaxScale";
            this.lblMaxScale.Size = new System.Drawing.Size(84, 13);
            this.lblMaxScale.TabIndex = 2;
            this.lblMaxScale.Text = "MaxVisibleScale";
            // 
            // lblMinScale
            // 
            this.lblMinScale.AutoSize = true;
            this.lblMinScale.Location = new System.Drawing.Point(35, 216);
            this.lblMinScale.Name = "lblMinScale";
            this.lblMinScale.Size = new System.Drawing.Size(80, 13);
            this.lblMinScale.TabIndex = 3;
            this.lblMinScale.Text = "minVisibleScale";
            // 
            // frmLayerProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 450);
            this.Controls.Add(this.lblMinScale);
            this.Controls.Add(this.lblMaxScale);
            this.Controls.Add(this.lblLayerSource);
            this.Controls.Add(this.lblLayerDescription);
            this.Name = "frmLayerProperties";
            this.Text = "frmLayerProperties";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblLayerDescription;
        public System.Windows.Forms.Label lblLayerSource;
        public System.Windows.Forms.Label lblMaxScale;
        public System.Windows.Forms.Label lblMinScale;
    }
}
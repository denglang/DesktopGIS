namespace UsePanels
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axMap1 = new AxMapWinGIS.AxMap();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAttributeTitle = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnAddFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.btnIdentify = new System.Windows.Forms.ToolStripButton();
            this.toolClearSelection = new System.Windows.Forms.ToolStripButton();
            this.toolSelectShape = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conversionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aRDToShapefileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVToShapefileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shapefileToKMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectByLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intersectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bufferToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.zoomToLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAttributeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllLayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toShapefileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shapefileToKMLToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bufferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeLineSymbolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkBox_addBaseMap = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.chkToggleLabelWin = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.moveLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.symbologyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axMap1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // axMap1
            // 
            this.axMap1.Enabled = true;
            this.axMap1.Location = new System.Drawing.Point(183, 51);
            this.axMap1.Name = "axMap1";
            this.axMap1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMap1.OcxState")));
            this.axMap1.Size = new System.Drawing.Size(619, 665);
            this.axMap1.TabIndex = 0;
            this.axMap1.ShapeIdentified += new AxMapWinGIS._DMapEvents_ShapeIdentifiedEventHandler(this.axMap1_ShapeIdentified);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(10, 6);
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblAttributeTitle);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(808, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 692);
            this.panel1.TabIndex = 1;
            this.panel1.Visible = false;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(267, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "X";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblAttributeTitle
            // 
            this.lblAttributeTitle.AutoSize = true;
            this.lblAttributeTitle.Location = new System.Drawing.Point(9, 12);
            this.lblAttributeTitle.Name = "lblAttributeTitle";
            this.lblAttributeTitle.Size = new System.Drawing.Size(76, 13);
            this.lblAttributeTitle.TabIndex = 1;
            this.lblAttributeTitle.Text = "Attribute Table";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip2;
            this.dataGridView1.Location = new System.Drawing.Point(2, 37);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(279, 629);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToCSVToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(147, 26);
            // 
            // exportToCSVToolStripMenuItem
            // 
            this.exportToCSVToolStripMenuItem.Name = "exportToCSVToolStripMenuItem";
            this.exportToCSVToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exportToCSVToolStripMenuItem.Text = "Export to CSV";
            this.exportToCSVToolStripMenuItem.Click += new System.EventHandler(this.exportToCSVToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnAddFile,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.btnIdentify,
            this.toolClearSelection,
            this.toolSelectShape});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(808, 27);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            this.toolTip1.SetToolTip(this.toolStrip1, "Select shape");
            // 
            // tsBtnAddFile
            // 
            this.tsBtnAddFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnAddFile.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnAddFile.Image")));
            this.tsBtnAddFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAddFile.Name = "tsBtnAddFile";
            this.tsBtnAddFile.Size = new System.Drawing.Size(23, 24);
            this.tsBtnAddFile.Text = "Add File";
            this.tsBtnAddFile.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 24);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "Zoom In";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 24);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "Zoom Out";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 24);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.ToolTipText = "Pan";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 24);
            this.toolStripButton4.Text = "toolStripButton4";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 24);
            this.toolStripButton5.Text = "toolStripButton5";
            this.toolStripButton5.ToolTipText = "Zoom to all layers";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // btnIdentify
            // 
            this.btnIdentify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIdentify.Image = ((System.Drawing.Image)(resources.GetObject("btnIdentify.Image")));
            this.btnIdentify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIdentify.Name = "btnIdentify";
            this.btnIdentify.Size = new System.Drawing.Size(23, 24);
            this.btnIdentify.Text = "toolStripButton6";
            this.btnIdentify.ToolTipText = "Identify";
            this.btnIdentify.Click += new System.EventHandler(this.btnIdentify_Click);
            // 
            // toolClearSelection
            // 
            this.toolClearSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolClearSelection.Image = ((System.Drawing.Image)(resources.GetObject("toolClearSelection.Image")));
            this.toolClearSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolClearSelection.Name = "toolClearSelection";
            this.toolClearSelection.Size = new System.Drawing.Size(110, 24);
            this.toolClearSelection.Text = "Clear selection";
            this.toolClearSelection.ToolTipText = "Clear All Selections";
            this.toolClearSelection.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolSelectShape
            // 
            this.toolSelectShape.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolSelectShape.Image = ((System.Drawing.Image)(resources.GetObject("toolSelectShape.Image")));
            this.toolSelectShape.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSelectShape.Name = "toolSelectShape";
            this.toolSelectShape.Size = new System.Drawing.Size(96, 24);
            this.toolSelectShape.Text = "Select shape";
            this.toolSelectShape.Click += new System.EventHandler(this.toolSelectShape_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.conversionToolStripMenuItem,
            this.selectionToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1092, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.UseWaitCursor = true;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTableToolStripMenuItem,
            this.openImageToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openTableToolStripMenuItem
            // 
            this.openTableToolStripMenuItem.Name = "openTableToolStripMenuItem";
            this.openTableToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.openTableToolStripMenuItem.Text = "Attribute Table";
            this.openTableToolStripMenuItem.Click += new System.EventHandler(this.openTableToolStripMenuItem_Click);
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.openImageToolStripMenuItem.Text = "Open Image";
            this.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            // 
            // conversionToolStripMenuItem
            // 
            this.conversionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aRDToShapefileToolStripMenuItem,
            this.cSVToShapefileToolStripMenuItem,
            this.shapefileToKMLToolStripMenuItem});
            this.conversionToolStripMenuItem.Name = "conversionToolStripMenuItem";
            this.conversionToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.conversionToolStripMenuItem.Text = "Conversion";
            // 
            // aRDToShapefileToolStripMenuItem
            // 
            this.aRDToShapefileToolStripMenuItem.Name = "aRDToShapefileToolStripMenuItem";
            this.aRDToShapefileToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.aRDToShapefileToolStripMenuItem.Text = "ARD to Shapefile";
            this.aRDToShapefileToolStripMenuItem.Click += new System.EventHandler(this.aRDToShapefileToolStripMenuItem_Click);
            // 
            // cSVToShapefileToolStripMenuItem
            // 
            this.cSVToShapefileToolStripMenuItem.Name = "cSVToShapefileToolStripMenuItem";
            this.cSVToShapefileToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.cSVToShapefileToolStripMenuItem.Text = "CSV to Shapefile";
            // 
            // shapefileToKMLToolStripMenuItem
            // 
            this.shapefileToKMLToolStripMenuItem.Name = "shapefileToKMLToolStripMenuItem";
            this.shapefileToKMLToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.shapefileToKMLToolStripMenuItem.Text = "Batch Shapefile to KML";
            this.shapefileToKMLToolStripMenuItem.Click += new System.EventHandler(this.shapefileToKMLToolStripMenuItem_Click);
            // 
            // selectionToolStripMenuItem
            // 
            this.selectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectByToolStripMenuItem,
            this.selectByLocationToolStripMenuItem});
            this.selectionToolStripMenuItem.Name = "selectionToolStripMenuItem";
            this.selectionToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.selectionToolStripMenuItem.Text = "Selection";
            // 
            // selectByToolStripMenuItem
            // 
            this.selectByToolStripMenuItem.Name = "selectByToolStripMenuItem";
            this.selectByToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.selectByToolStripMenuItem.Text = "Select By Attributes";
            this.selectByToolStripMenuItem.Click += new System.EventHandler(this.selectByToolStripMenuItem_Click);
            // 
            // selectByLocationToolStripMenuItem
            // 
            this.selectByLocationToolStripMenuItem.Name = "selectByLocationToolStripMenuItem";
            this.selectByLocationToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.selectByLocationToolStripMenuItem.Text = "Select By Location";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.intersectToolStripMenuItem,
            this.labelToolStripMenuItem1,
            this.bufferToolStripMenuItem1});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // intersectToolStripMenuItem
            // 
            this.intersectToolStripMenuItem.Name = "intersectToolStripMenuItem";
            this.intersectToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.intersectToolStripMenuItem.Text = "Intersect";
            this.intersectToolStripMenuItem.Click += new System.EventHandler(this.intersectToolStripMenuItem_Click);
            // 
            // labelToolStripMenuItem1
            // 
            this.labelToolStripMenuItem1.Name = "labelToolStripMenuItem1";
            this.labelToolStripMenuItem1.Size = new System.Drawing.Size(119, 22);
            this.labelToolStripMenuItem1.Text = "Label";
            this.labelToolStripMenuItem1.Click += new System.EventHandler(this.labelToolStripMenuItem1_Click);
            // 
            // bufferToolStripMenuItem1
            // 
            this.bufferToolStripMenuItem1.Name = "bufferToolStripMenuItem1";
            this.bufferToolStripMenuItem1.Size = new System.Drawing.Size(119, 22);
            this.bufferToolStripMenuItem1.Text = "Buffer";
            this.bufferToolStripMenuItem1.Click += new System.EventHandler(this.bufferToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolHelpToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolHelpToolStripMenuItem
            // 
            this.toolHelpToolStripMenuItem.Name = "toolHelpToolStripMenuItem";
            this.toolHelpToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.toolHelpToolStripMenuItem.Text = "Tool Help";
            this.toolHelpToolStripMenuItem.Click += new System.EventHandler(this.toolHelpToolStripMenuItem_Click);
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(0, 51);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowLines = false;
            this.treeView1.ShowPlusMinus = false;
            this.treeView1.Size = new System.Drawing.Size(177, 614);
            this.treeView1.TabIndex = 3;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            this.treeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToLayerToolStripMenuItem,
            this.openAttributeToolStripMenuItem,
            this.removeLayerToolStripMenuItem,
            this.removeAllLayersToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.bufferToolStripMenuItem,
            this.labelToolStripMenuItem,
            this.changeLineSymbolToolStripMenuItem,
            this.moveLayerToolStripMenuItem,
            this.symbologyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 224);
            // 
            // zoomToLayerToolStripMenuItem
            // 
            this.zoomToLayerToolStripMenuItem.Name = "zoomToLayerToolStripMenuItem";
            this.zoomToLayerToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.zoomToLayerToolStripMenuItem.Text = "Zoom To Layer";
            this.zoomToLayerToolStripMenuItem.Click += new System.EventHandler(this.zoomToLayerToolStripMenuItem_Click);
            // 
            // openAttributeToolStripMenuItem
            // 
            this.openAttributeToolStripMenuItem.Name = "openAttributeToolStripMenuItem";
            this.openAttributeToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.openAttributeToolStripMenuItem.Text = "Open Attribute";
            this.openAttributeToolStripMenuItem.Click += new System.EventHandler(this.openAttributeToolStripMenuItem_Click);
            // 
            // removeLayerToolStripMenuItem
            // 
            this.removeLayerToolStripMenuItem.Name = "removeLayerToolStripMenuItem";
            this.removeLayerToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.removeLayerToolStripMenuItem.Text = "Remove Layer";
            this.removeLayerToolStripMenuItem.Click += new System.EventHandler(this.removeLayerToolStripMenuItem_Click);
            // 
            // removeAllLayersToolStripMenuItem
            // 
            this.removeAllLayersToolStripMenuItem.Name = "removeAllLayersToolStripMenuItem";
            this.removeAllLayersToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.removeAllLayersToolStripMenuItem.Text = "Remove All Layers";
            this.removeAllLayersToolStripMenuItem.Click += new System.EventHandler(this.removeAllLayersToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toShapefileToolStripMenuItem,
            this.shapefileToKMLToolStripMenuItem1});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // toShapefileToolStripMenuItem
            // 
            this.toShapefileToolStripMenuItem.Name = "toShapefileToolStripMenuItem";
            this.toShapefileToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.toShapefileToolStripMenuItem.Text = "To Shapefile";
            this.toShapefileToolStripMenuItem.Click += new System.EventHandler(this.toShapefileToolStripMenuItem_Click);
            // 
            // shapefileToKMLToolStripMenuItem1
            // 
            this.shapefileToKMLToolStripMenuItem1.Name = "shapefileToKMLToolStripMenuItem1";
            this.shapefileToKMLToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.shapefileToKMLToolStripMenuItem1.Text = "Shapefile to KML";
            this.shapefileToKMLToolStripMenuItem1.Click += new System.EventHandler(this.shapefileToKMLToolStripMenuItem1_Click);
            // 
            // bufferToolStripMenuItem
            // 
            this.bufferToolStripMenuItem.Name = "bufferToolStripMenuItem";
            this.bufferToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.bufferToolStripMenuItem.Text = "Buffer";
            this.bufferToolStripMenuItem.Click += new System.EventHandler(this.bufferToolStripMenuItem_Click);
            // 
            // labelToolStripMenuItem
            // 
            this.labelToolStripMenuItem.Name = "labelToolStripMenuItem";
            this.labelToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.labelToolStripMenuItem.Text = "Label";
            this.labelToolStripMenuItem.Click += new System.EventHandler(this.labelToolStripMenuItem_Click);
            // 
            // changeLineSymbolToolStripMenuItem
            // 
            this.changeLineSymbolToolStripMenuItem.Name = "changeLineSymbolToolStripMenuItem";
            this.changeLineSymbolToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.changeLineSymbolToolStripMenuItem.Text = "Change Line Symbol";
            this.changeLineSymbolToolStripMenuItem.Click += new System.EventHandler(this.changeLineSymbolToolStripMenuItem_Click);
            // 
            // chkBox_addBaseMap
            // 
            this.chkBox_addBaseMap.AutoSize = true;
            this.chkBox_addBaseMap.Location = new System.Drawing.Point(404, 28);
            this.chkBox_addBaseMap.Name = "chkBox_addBaseMap";
            this.chkBox_addBaseMap.Size = new System.Drawing.Size(107, 17);
            this.chkBox_addBaseMap.TabIndex = 6;
            this.chkBox_addBaseMap.Text = "Toggle BaseMap";
            this.chkBox_addBaseMap.UseVisualStyleBackColor = true;
            this.chkBox_addBaseMap.CheckedChanged += new System.EventHandler(this.chkBox_addBaseMap_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 694);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(808, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // chkToggleLabelWin
            // 
            this.chkToggleLabelWin.AutoSize = true;
            this.chkToggleLabelWin.Location = new System.Drawing.Point(517, 27);
            this.chkToggleLabelWin.Name = "chkToggleLabelWin";
            this.chkToggleLabelWin.Size = new System.Drawing.Size(103, 17);
            this.chkToggleLabelWin.TabIndex = 8;
            this.chkToggleLabelWin.Text = "ToggleLabelBox";
            this.chkToggleLabelWin.UseVisualStyleBackColor = true;
            this.chkToggleLabelWin.CheckedChanged += new System.EventHandler(this.chkToggleLabelWin_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(67, 439);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // moveLayerToolStripMenuItem
            // 
            this.moveLayerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.upToolStripMenuItem,
            this.downToolStripMenuItem,
            this.toTopToolStripMenuItem,
            this.toBottomToolStripMenuItem});
            this.moveLayerToolStripMenuItem.Name = "moveLayerToolStripMenuItem";
            this.moveLayerToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.moveLayerToolStripMenuItem.Text = "Move Layer";
            // 
            // upToolStripMenuItem
            // 
            this.upToolStripMenuItem.Name = "upToolStripMenuItem";
            this.upToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.upToolStripMenuItem.Text = "Up";
            this.upToolStripMenuItem.Click += new System.EventHandler(this.upToolStripMenuItem_Click);
            // 
            // downToolStripMenuItem
            // 
            this.downToolStripMenuItem.Name = "downToolStripMenuItem";
            this.downToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.downToolStripMenuItem.Text = "Down";
            this.downToolStripMenuItem.Click += new System.EventHandler(this.downToolStripMenuItem_Click);
            // 
            // toTopToolStripMenuItem
            // 
            this.toTopToolStripMenuItem.Name = "toTopToolStripMenuItem";
            this.toTopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.toTopToolStripMenuItem.Text = "To Top";
            this.toTopToolStripMenuItem.Click += new System.EventHandler(this.toTopToolStripMenuItem_Click);
            // 
            // toBottomToolStripMenuItem
            // 
            this.toBottomToolStripMenuItem.Name = "toBottomToolStripMenuItem";
            this.toBottomToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.toBottomToolStripMenuItem.Text = "To Bottom";
            this.toBottomToolStripMenuItem.Click += new System.EventHandler(this.toBottomToolStripMenuItem_Click);
            // 
            // symbologyToolStripMenuItem
            // 
            this.symbologyToolStripMenuItem.Name = "symbologyToolStripMenuItem";
            this.symbologyToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.symbologyToolStripMenuItem.Text = "Symbology";
            this.symbologyToolStripMenuItem.Click += new System.EventHandler(this.symbologyToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 716);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.chkToggleLabelWin);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkBox_addBaseMap);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.axMap1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "AE GIS";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axMap1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTableToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAttributeTitle;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem zoomToLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAttributeToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnAddFile;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripMenuItem removeLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllLayersToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripMenuItem conversionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aRDToShapefileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cSVToShapefileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shapefileToKMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectByToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectByLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnIdentify;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkBox_addBaseMap;
        public AxMapWinGIS.AxMap axMap1;
        private System.Windows.Forms.ToolStripButton toolClearSelection;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toShapefileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToCSVToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem shapefileToKMLToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bufferToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkToggleLabelWin;
        private System.Windows.Forms.ToolStripMenuItem changeLineSymbolToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intersectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bufferToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton toolSelectShape;
        private System.Windows.Forms.ToolStripMenuItem moveLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem upToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toBottomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem symbologyToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}


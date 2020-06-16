using AxMapWinGIS;
using MapWinGIS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AmesDataFormat;
using MapWinGIS_AE;
using System.Text;

namespace UsePanels
{
    public partial class Form1 : Form
    {
        public Dictionary<string, int> layerControl = new Dictionary<string, int>();
        public bool drag;
        public int mouseX;
        public int mouseY;
        int n = 0;
        //int m = 0;
        
        public Form1()
         {
            InitializeComponent();
            
        new GlobalSettings() { AllowProjectionMismatch = true, ReprojectLayersOnAdding = true };
            axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
            axMap1.KnownExtents = tkKnownExtents.keUSA;
            panel1.Hide();
            //axMap1.MouseDownEvent -= AxMap1MouseDownEvent2;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent2;
            //axMap1.SendMouseMove = true;
            axMap1.SendMouseDown = true; 
            axMap1.ShapeIdentified += axMap1_ShapeIdentified;
            axMap1.ShapeHighlighted += AxMap1ShapeHighlighted;
        }
        private ToolStripStatusLabel m_label = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToLongDateString();
        }
        public void ShowAttributes()
        {
            axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;           
            Shapefile sf = new Shapefile();
            int handle = getLayerHandle();
            sf = axMap1.get_Shapefile(handle);
            
                //m_layerHandle = axMap1.AddLayer(sf,true);
                //sf = axMap1.get_Shapefile(m_layerHandle);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding

                axMap1.SendMouseMove = true;
                axMap1.CursorMode = tkCursorMode.cmIdentify;
                axMap1.ShapeHighlighted += AxMap1ShapeHighlighted;
                m_label = toolStripStatusLabel1;
                       
        }
        private void AxMap1ShapeHighlighted(object sender, _DMapEvents_ShapeHighlightedEvent e)
        {
            toolStripStatusLabel1.Text = "";
            Shapefile sf = axMap1.get_Shapefile(e.layerHandle);
            if (sf != null)
            {
                string s = "";
                for (int i = 0; i < sf.NumFields; i++)
                {
                    string val = sf.get_CellValue(i, e.shapeIndex).ToString();
                    if (val == "") val = "null";
                    s += sf.Table.Field[i].Name + ":" + val + "; ";
                }
                toolStripStatusLabel1.Text = s;
            }
        }
        private void openTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        public void createAttributeTable(int[] lstSelected = null, string query="")
        {
            Shapefile sf = new Shapefile();
            //shp.Open(@"C:\Work\GIS\data\states.shp");
            //lstSelected = lstSelected ?? new List<int>();
            int layerHandle = -1; // getLayerHandle();
            string layerName = "";
            if (treeView1.SelectedNode != null)
            {
                layerName = treeView1.SelectedNode.Text;
                layerHandle = layerControl[layerName];
            }
            else
            {
                MessageBox.Show("Please select a layer.");
                return;
            }
            sf = axMap1.get_Shapefile(layerHandle);
            // GeoProjection proj = new GeoProjection();
            // proj.SetGoogleMercator();
            // shp.Reproject(proj, 1);
            //int handle = axMap1.AddLayer(shp, true);
            // shp.DefaultDrawingOptions.FillTransparency = 0;
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            //dataGridView1.Columns.Add("ID","ID");

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID"));
            for (int i = 0; i < sf.NumFields; i++)
            {
                var s = sf.get_Field(i).Name.ToString();

                dt.Columns.Add(new DataColumn(s));
                //MessageBox.Show(sf.get_Field(i).Name.ToString());
                DataRow dr = null;
                for (int j = 0; j < sf.NumShapes; j++)
                {                   
                    //if j in lstSelected 
                    //MessageBox.Show(sf.get_CellValue(i, j).ToString());
                    dr = dt.NewRow();
                    dr["ID"] = j;

                    if (i == 0)
                    {
                        dr[s] = sf.get_CellValue(i, j);
                       // if (lstSelected != null && lstSelected.Contains(j))
                       // {
                            dt.Rows.Add(dr);
                        //    dt.Select();                      
                                            
                    }
                    else
                    {
                        //DataRow newDr = dt.Rows[i];
                        dt.Rows[j][s] = sf.get_CellValue(i, j);
                    }
                }
            }
            //if (query != null)
            //    dt.Select(query);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;

                if (lstSelected != null)
                {
                    foreach (int i in lstSelected)
                    {
                        for (int  j= 0; j < dataGridView1.RowCount-1; j++)
                        {
                            if (j==i) dataGridView1.Rows[j].Selected = true;
                            //dataGridView1.FirstDisplayedScrollingRowIndex = j;
                            //dataGridView1.CurrentCell = dataGridView1.Rows[j].Cells[0];
                            //dataGridView1.Focus();
                        }
                    }
                }                             
            }
            /////////////////////////////////////////
            if (lstSelected != null && lstSelected.Length > 0)
            {
                lblAttributeTitle.Text = layerName + ", with " + sf.NumShapes.ToString() + " rows with "+ lstSelected.Length+ " selected";
            } else lblAttributeTitle.Text = layerName + ", with " + sf.NumShapes.ToString() + " rows";
            panel1.Show();
        }
        private void openAttributeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createAttributeTable();
        }

        private void AddLayerToMap(string filename)
        {
            if (filename != string.Empty)
            {
                //MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK)
                // axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
                //axMap1.Measuring.MeasuringType = tkMeasuringType.MeasureArea;
                //axMap1.CursorMode = tkCursor;
                //axMap1.RemoveAllLayers();
                //axMap1.LockWindow(tkLockMode.lmLock);

                axMap1.GrabProjectionFromData = true;

                //**************************************************
                // GeoProjection proj = new GeoProjection();
                // proj.ReadFromFile(filename);
                //lblMapProjection.Text = proj.Name;
                //lblMapProjection.Text = axMap1.Projection.ToString();
                //***********************************************
                //axMap1.Projection = tkMapProjection;
                //MessageBox.Show(axMap1.Projection.ToString());

                int layerHandle = -1;
               
                string fname = "";

                if (filename.Length > 0 && filename.Contains(".shp"))
                {
                    layerHandle = axMap1.AddLayerFromFilename(filename, tkFileOpenStrategy.fosAutoDetect, true);

                    fname = filename.Split('\\').Last();
                    fname = fname.Remove(fname.Length - 4);  //get rid of the .shp
                }
                else return;

                //add the layerHandle and shapefile name to layerControl dictionary
                try
                {
                    layerControl.Add(fname, layerHandle);
                }
                catch (ArgumentException)
                {
                    if (n == 0) n += 1;
                    else n += 2;
                    Console.WriteLine("An element with Key = " + fname + " already exists, adding a duplicate layer");
                    fname = fname + "("+n.ToString()+")";
                    //if (layerControl[fname]>-1) {
                        layerControl.Add(fname, layerHandle+1000+n);
                   // }
                }
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
                //MessageBox.Show(projectDirectory + "\\images\\Line.PNG");
                ImageList myImageList = new ImageList();
                System.Drawing.Image pointIcon = System.Drawing.Image.FromFile(projectDirectory + "\\images\\Point.PNG");
                System.Drawing.Image lineIcon = System.Drawing.Image.FromFile(projectDirectory + "\\images\\Line.PNG");
                System.Drawing.Image polygonIcon = System.Drawing.Image.FromFile(projectDirectory + "\\images\\polygon.PNG");
                System.Drawing.Image imageIcon = System.Drawing.Image.FromFile(projectDirectory + "\\images\\Image.PNG");
                myImageList.Images.Add(pointIcon);
                myImageList.Images.Add(lineIcon);
                myImageList.Images.Add(polygonIcon);
                myImageList.Images.Add(imageIcon);
                treeView1.ImageList = myImageList;
                treeView1.CheckBoxes = true; //show checkbox of each node

                // MessageBox.Show(axMap1.get_LayerFilename(layerHandle));
                //MessageBox.Show(axMap1.get_LayerDescription(layerHandle));
                // string layerName = Path.GetFileNameWithoutExtension(pa

                if (layerHandle == -1)
                {
                    System.Diagnostics.Debug.WriteLine("Failed to open datasource: " + axMap1.FileManager.get_ErrorMsg(axMap1.FileManager.LastErrorCode));
                }

                axMap1.ZoomToLayer(layerHandle);
                var sf = new Shapefile();
                sf = axMap1.get_Shapefile(layerHandle);
                //sf.GenerateLabels(9, tkLabelPositioning.lpCentroid);
                //if (sf.ShapefileType == ShpfileType.SHP_POLYGON)
                //{
                //    //AddCategoryRange(sf);
                //    PolygonNoFill(sf); //show polygon outline only
                //    treeView1.ImageList = imageList1;

                //}
                if (sf.ShapefileType == ShpfileType.SHP_POLYLINE)
                {
                    TreeNode N = new TreeNode(fname);
                    N.ImageIndex = 1;
                    //treeView1.Nodes
                    treeView1.Nodes.Add(N);
                    //N.Checked = true;
                    N.SelectedImageIndex = N.ImageIndex;
                    ColorCodeShape(sf);
                    //    //Utils utils = new Utils();                  
                    //    //sf.DefaultDrawingOptions.LineWidth = 2;
                    //    // sf.DefaultDrawingOptions.LineColor = utils.ColorByName(tkMapColor.Blue);
                }
                else if (sf.ShapefileType == ShpfileType.SHP_POINT)
                {
                    TreeNode N = new TreeNode(fname);
                    N.ImageIndex = 0;
                    treeView1.Nodes.Add(N);
                    ////N.Checked = true;
                    N.SelectedImageIndex = N.ImageIndex; //so when selected, the image won't change
                    //ColorCodeShape(sf);
                    Utils utils = new Utils();
                    ShapeDrawingOptions options = sf.DefaultDrawingOptions;
                    options.FillColor = utils.ColorByName(tkMapColor.Red);

                    // standard symbol
                    options.PointType = tkPointSymbolType.ptSymbolStandard;
                    options.PointShape = tkPointShapeType.ptShapeStar;
                    options.PointSidesCount = 8;

                }
                else if (sf.ShapefileType == ShpfileType.SHP_POLYGON)
                {
                    TreeNode N = new TreeNode(fname);
                    N.ImageIndex = 2;
                    treeView1.Nodes.Add(N);
                    //N.Checked = true;
                    N.SelectedImageIndex = N.ImageIndex;

                    ShapeDrawingOptions options = sf.DefaultDrawingOptions;
                    Utils utils = new Utils();

                    //standard fill
                    options.FillType = tkFillType.ftStandard;
                    options.FillColor = utils.ColorByName(tkMapColor.Red);
                    options.FillTransparency = 100;

                    //ColorCodeShape(sf);
                    //Utils utils = new Utils();
                    //ShapeDrawingOptions options = sf.DefaultDrawingOptions;
                    //options.FillColor = utils.ColorByName(tkMapColor.Red);

                    //// standard symbol
                    //options.PointType = tkPointSymbolType.ptSymbolStandard;
                    //options.PointShape = tkPointShapeType.ptShapeStar;
                    //options.PointSidesCount = 8;

                }
            }
            else
            {
                return;
            }
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            string layerName = lblAttributeTitle.Text.Split(',')[0];
            //MessageBox.Show(layerName);
            //int handle = getLayerHandle();
            int handle = layerControl[layerName];
            var sf = new Shapefile();
            sf = axMap1.get_Shapefile(handle);
            sf.SelectNone();
            //MessageBox.Show(Convert.ToUInt16(dataGridView1.SelectedRows[0].Cells[0].Value).ToString());
            // select a row, not a cell value in the attribute table, the above code is not good, since a cell 
            // calue can be anything that we cannot contorl. selectedrowindex is an int, no need to convert it.
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;           
            sf.set_ShapeSelected(selectedrowindex, true);
            //sf.set_ShapeSelected(Convert.ToUInt16(selectedrowindex.ToString()),true);
            //sf.set_ShapeSelected(Convert.ToUInt16(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()), true);
            axMap1.ZoomToShape(handle, selectedrowindex);
        }

        public int getLayerHandle()
        {

            if (this.treeView1.SelectedNode == null)
            {
                MessageBox.Show("Please select a map layer!");
                return -1;
            }

            string layerName = treeView1.SelectedNode.Text;
            //MessageBox.Show(layerName);

            int layerHandle = layerControl[layerName];

            return layerHandle;
        }

        private void zoomToLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int layerHandle = getLayerHandle();
            axMap1.ZoomToLayer(layerHandle);
        }

        public bool AddLayers(AxMap axMap1, string dataPath)
        {
            axMap1.RemoveAllLayers();
            axMap1.LockWindow(tkLockMode.lmLock);
            try
            {
                string[] files = System.IO.Directory.GetFiles(dataPath);
                foreach (string file in files)
                {
                    int layerHandle = -1;
                    if (file.ToLower().EndsWith(".shp"))
                    {
                        AddLayerToMap(file);
                        //Shapefile sf = new Shapefile();
                        //if (sf.Open(file, null))
                        //{
                        //    layerHandle = axMap1.AddLayer(sf, true);
                        //}
                        //else
                        //    MessageBox.Show(sf.ErrorMsg[sf.LastErrorCode]);
                    }
                    else if (file.ToLower().EndsWith(".tif") ||
                             file.ToLower().EndsWith(".png"))
                    {
                        MapWinGIS.Image img = new MapWinGIS.Image();
                        if (img.Open(file, ImageType.TIFF_FILE, false, null))
                        {
                            layerHandle = axMap1.AddLayer(img, true);
                        }
                        else
                            MessageBox.Show(img.ErrorMsg[img.LastErrorCode]);
                    }
                    if (layerHandle != -1)
                        axMap1.set_LayerName(layerHandle, Path.GetFileName(file));
                }
            }
            finally
            {
                axMap1.LockWindow(tkLockMode.lmUnlock);
                System.Diagnostics.Debug.Print("Layers added to the map: " + axMap1.NumLayers);
            }
            //SortLayersByType();
            return axMap1.NumLayers > 0;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            string[] filenames = openFile();
            if (filenames == null || filenames.Length == 0)
            {
                return;
            }

            if (filenames.Length == 1)
            {
                AddLayerToMap(filenames[0]);
            }
            else
            {
                foreach (string f in filenames)
                {
                    AddLayerToMap(f);
                    //if (!f.EndsWith(".shp"))
                    //{

                    //    contextMenuStrip1.Items[2].Visible = false;
                    //    contextMenuStrip1.Items[3].Visible = false;
                    //    contextMenuStrip1.Items[4].Visible = false;
                    //    contextMenuStrip1.Items[8].Visible = false;

                    //}
                }
            }
        }

        private string[] openFile()
        {
            string filename = string.Empty;
            string[] fileNames = null;
            var fileContent = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "shape files (*.shp)|*.shp|ARD Files (*.ard)|*.ard|JPEG Files: (*.JPG;*.JPEG;*.JPE;*.JFIF)|*.JPG;*.JPEG;*.JPE;*.JFIF|PNG Files: (*.PNG)|*.PNG|Tiff file (*.tif)|*.tif|BMP Files: (*.BMP;*.DIB;*.RLE)|*.BMP;*.DIB;*.RLE|GIF Files: (*.GIF)|*.GIF|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                //openFileDialog.InitialDirectory = @"D:\shp";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filename = openFileDialog.FileName;
                    fileNames = openFileDialog.FileNames;

                    //Read the contents of the file into a stream
                    //var fileStream = openFileDialog.OpenFile();
                    //using (StreamReader reader = new StreamReader(fileStream))
                    //{
                    //    fileContent = reader.ReadToEnd();
                    //}
                }
                else
                {
                    MessageBox.Show("No valid file selected.");
                    return null;
                }
            }
            return fileNames;
        }

        private string[] openFile(string fileType)
        {
            string filename = string.Empty;
            string[] fileNames = null;
            var fileContent = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = fileType + " Files (*." + fileType + ")|*." + fileType;
                //MessageBox.Show(openFileDialog.Filter);
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                //openFileDialog.InitialDirectory = @"D:\shp";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filename = openFileDialog.FileName;
                    fileNames = openFileDialog.FileNames;
                }
                else
                {
                    return null;
                }
            }
            return fileNames;
        }

        public void ColorCodeShape(Shapefile sf, double min = 275.0, double max = 501.0)
        {
            int fieldIndex = -1;
            string[] iriNamelist = { "IRI Averag", "IRI", "IRI_Left", "IRI_Right", "IRI_Averag" };

            for (int i = 0; i < iriNamelist.Length; i++)
            {
                //MessageBox.Show(iriNamelist[i]);
                fieldIndex = sf.Table.FieldIndexByName[iriNamelist[i]];

                if (fieldIndex > -1)
                {
                    break;
                }
            }
            //fieldIndex = sf.Table.FieldIndexByName["IRI Averag"];
            if (fieldIndex == -1)
            {
                MessageBox.Show("No IRI field found in shapefile " + sf.Filename);
                return;
            }

            var scheme = new ColorScheme();

            Utils utils = new Utils();

            for (int i = 0; i < sf.NumShapes; i++)
            {
                //if (sf.get_CellValue(fieldIndex, i) == null)
                //{
                //    MessageBox.Show("No IRI value found in shapefile!");
                //    return;
                //}
                double value = (double)sf.get_CellValue(fieldIndex, i);

                if (value > max)
                {
                    ShapefileCategory ct = sf.Categories.Add("Poor");
                    LinePattern pattern = new LinePattern();
                    //Color c = new Color();
                    //if (colorDialog1.ShowDialog() == DialogResult.OK)
                    //{
                    //    c = colorDialog1.Color;
                    //}
                    pattern.AddLine(utils.ColorByName(tkMapColor.Red), 6.0f, tkDashStyle.dsSolid);
                    // pattern.AddLine(ColorToUint(c), 6.0f, tkDashStyle.dsSolid);
                    ct.DrawingOptions.LinePattern = pattern;
                    ct.DrawingOptions.UseLinePattern = true;
                    sf.set_ShapeCategory(i, i);

                }
                else if (value <= min)
                {
                    ShapefileCategory ct = sf.Categories.Add("Good");
                    LinePattern pattern = new LinePattern();
                    pattern.AddLine(utils.ColorByName(tkMapColor.Green), 6.0f, tkDashStyle.dsSolid);
                    ct.DrawingOptions.LinePattern = pattern;
                    ct.DrawingOptions.UseLinePattern = true;
                    sf.set_ShapeCategory(i, i);
                }
                else
                {

                    ShapefileCategory ct = sf.Categories.Add("Fair");
                    LinePattern pattern = new LinePattern();
                    //Color c = new Color();
                    //if (colorDialog1.ShowDialog() == DialogResult.OK)
                    //{
                    //    c = colorDialog1.Color;
                    //}

                    pattern.AddLine(utils.ColorByName(tkMapColor.Yellow), 6.0f, tkDashStyle.dsSolid);
                    //pattern.AddLine(ColorToUint(c), 6.0f, tkDashStyle.dsSolid);
                    ct.DrawingOptions.LinePattern = pattern;
                    ct.DrawingOptions.UseLinePattern = true;
                    sf.set_ShapeCategory(i, i);
                }

            }
            axMap1.Refresh();
            axMap1.Redraw();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            mouseX = Cursor.Position.X - this.Left;
            mouseY = Cursor.Position.Y - this.Top; 
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                this.Left = Cursor.Position.X - mouseX;
                this.Top = Cursor.Position.Y - mouseY;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
            //panel1.MinimumSize=10;
        }

        

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            axMap1.ZoomIn(20.00);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            axMap1.ZoomOut(20.00);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            axMap1.CursorMode = tkCursorMode.cmPan;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            axMap1.CursorMode = tkCursorMode.cmMeasure;
            axMap1.Measuring.MeasuringType = tkMeasuringType.MeasureArea;
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            int layerHandle = -1;
            //here we cannot call getlayerHandle, it doesn't get populated yet,
            //we use e.Index to get the layerHandle
            //if (layerControl.Count == 1)
            //{
                //    //layerHandle = e.Node.Index;
                layerHandle = layerControl[e.Node.Text];
                // axMap1.set_LayerDynamicVisibility(layerHandle, false);
                if (e.Node.Checked) {
                    if (axMap1.get_LayerVisible(layerHandle)) { 
                        axMap1.set_LayerVisible(layerHandle, true);
                } else axMap1.set_LayerVisible(layerHandle, true);
            }
            else axMap1.set_LayerVisible(layerHandle, false);
            // } else {

            // List<TreeNode> checked_nodes = new List<TreeNode>();
            //foreach (TreeNode node in treeView1.Nodes)
            // {
            //if (node.Checked) checked_nodes.Add(node);
            //layerHandle = layerControl[node.Text];
            ////MessageBox.Show(node.Text);
            //if (node.Checked)
            //{
            //    axMap1.set_LayerVisible(layerHandle, true);
            //}
            //else
            //{
            //    axMap1.set_LayerVisible(layerHandle, false);
            //}
            // }
            //}
            //axMap1.Redraw();
            //foreach (TreeNode N in checked_nodes)
            //{
            //    layerHandle = layerControl[N.Text];
            //    axMap1.set_LayerDynamicVisibility(layerHandle, true);
            //}
        }

        private void removeAllLayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMap1.RemoveAllLayers();
            treeView1.Nodes.Clear();
            layerControl.Clear();
        }

        private void removeLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, int> kvp in layerControl)
            {
                //MessageBox.Show(kvp.Key +","+ kvp.Value.ToString());
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
            string layerName = treeView1.SelectedNode.Text;
            if(layerControl[layerName]!=-1) {
                axMap1.RemoveLayer(layerControl[layerName]);
                layerControl.Remove(layerName);
                treeView1.SelectedNode.Remove();
            } else
            {
                MessageBox.Show(layerName + " is not found in map layer control; No layer removed.");
                return; 
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            axMap1.ZoomToMaxExtents();
        }

        private void btnIdentify_Click(object sender, EventArgs e)
        {
            ShowAttributes();
            //Cursor.Current = Cursor
            //axMap1.CursorMode = tkCursorMode.cmIdentify;
            //if (treeView1.SelectedNode != null)
            //{
            //    Shapefile sf = new Shapefile();
            //    int layerHandle = getLayerHandle();
            //    sf = axMap1.get_Shapefile(layerHandle);

            //    string expression = "";
            //    for (int i = 1; i < sf.NumFields; i++)      // all the fields will be displayed apart the first one
            //    {
            //        expression += "[" + sf.Field[i].Name + "]";
            //        if (i != sf.NumFields - 1)
            //        {
            //            const string endLine = "\"\n\"";
            //            expression += string.Format("+ {0} +", endLine);
            //        }
            //    }
            //     sf.Labels.Generate(expression, tkLabelPositioning.lpCentroid, false);
            //     sf.Labels.TextRenderingHint = tkTextRenderingHint.SystemDefault;

            //    //axMap1.SendMouseDown = true;
            //    axMap1.CursorMode = tkCursorMode.cmIdentify;
            //    //axMap1.ShapeIdentified += axMap1_ShapeIdentified;
            //    // change MapEvents to axMap1
            //    // axMap1.MouseDownEvent -= AxMap1MouseDownEvent2;
            //    // axMap1.MouseDownEvent += AxMap1MouseDownEvent2;
            //    // this.ZoomToValue(sf, "Name", "Iowa");
            //}
            //else MessageBox.Show("Please select a layer");
        }

        private void AxMap1MouseDownEvent2(object sender, _DMapEvents_MouseDownEvent e)
        {
        }
       
        void FormFormClosed(object sender, FormClosedEventArgs e)
        {
            int layerHandle = getLayerHandle();
            Shapefile sf = axMap1.get_Shapefile(layerHandle);
            if (sf != null)
            {
                sf.SelectNone();
                axMap1.Redraw();
            }
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            int layerHandle = -1;
            string[] filenames = openFile("jpg");
            if (filenames == null || filenames.Length == 0)
            {
                //MessageBox.Show("Nothing selected");
                return;
            }

            foreach (string f in filenames)
            {
                //MessageBox.Show(f);
                var img = new MapWinGIS.Image();

                if (img.Open(f))
                {
                    layerHandle = axMap1.AddLayer(img, true);
                    string path = axMap1.get_LayerFilename(layerHandle);

                    string layerName = Path.GetFileNameWithoutExtension(path);

                    layerControl.Add(layerName, layerHandle);

                    //hide all menustrips that are only applied to shapefiles 
                    TreeNode N = new TreeNode(layerName);
                    N.ImageIndex = 3;
                    treeView1.Nodes.Add(N);
                    //treeView1.Nodes.Add(layerName);
                    //contextMenuStrip1.Items[2].Visible = false;
                    //contextMenuStrip1.Items[3].Visible = false;
                    //contextMenuStrip1.Items[4].Visible = false;
                    //contextMenuStrip1.Items[8].Visible = false;
                }
                else
                {
                    Debug.WriteLine("Failed to open image: " + img.get_ErrorMsg(img.LastErrorCode));
                }
            }
        }
        
        private void deleteFiles(string fullFileName)
        {
            string path = Path.GetDirectoryName(fullFileName);
            string fName = Path.GetFileNameWithoutExtension(fullFileName);

            string[] files = Directory.GetFiles(path, fName + ".*");
            foreach (string f in files)
            {
                //MessageBox.Show(f);
                File.Delete(f);
            }
        }

        private void saveFile(Shapefile sf)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            //saveFileDialog1.Title = "Save an Image File";
            sfd.Filter = "Shape File|*.shp";
            sfd.Title = "Save a Shapefile";
            sfd.InitialDirectory = @"C:\work\gis\data";
            sfd.ShowDialog();
            //sfd.CheckFileExists = true;
            //sfd.CheckPathExists = true;
            
            string f = "";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                f = sfd.FileName;
            }

            //File.Delete(f);
            //deleteFiles(f);
            // MessageBox.Show(f);
            if (sf != null)
            {
                sf.SaveAs(f, null);
                //frm.Hide();
                // MessageBox.Show("Shapefile creatd and saved to " + f);
                AddLayerToMap(f);
            }
            else
            {
                MessageBox.Show("No shapefile created!");
            }
        }
        private void aRDToShapefileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArdToShapefile();
        }
        public void ArdToShapefile()
        {
            //string path = "";
            //Cursor.Current = Cursors.WaitCursor;
            string path = openFile("ard")[0];

            readARD ra = new readARD(path);

            Shapefile sf = new Shapefile();
            //sf.CreateNew("", ShpfileType.SHP_POLYLINE);
            bool result = sf.CreateNewWithShapeID("", ShpfileType.SHP_POLYLINE);

            //bool result = sf.CreateNewWithShapeID("", ShpfileType.SHP_POLYLINE);
            //sf.Projection = tkMapProjection.PROJECTION_WGS84.ToString(); //this cannot set the map projection
            sf.GeoProjection.ImportFromEPSG(4326); //this set the right projection

            if (!sf.Table.StartEditingTable(null))
            {
                MessageBox.Show(@"Failed to open editing mode.");
                return;
            }

            //double iri = ra.IRI;
            AmesData data = ra.data;

            //GPSPoint[] points = ra.points; //doesn't work
            //GPSPoint[] points = ra.getPoints(path);
            //if (points != null) {
            //    MessageBox.Show(points.Length.ToString());
            //}
            double totalLength;
            //const double dd = 100.00;

            if (data != null)
            {
                totalLength = data.DistanceFeet;
                //MessageBox.Show("Total length "+totalLength.ToString()+"Points: "+(totalLength/528).ToString());
            }
            else
            {
                MessageBox.Show("Data is null");
                return;
            }
            double d = 528;

            //byte n = 0; 
            //do {
            //    n++;
            for (int n = 0; n < totalLength / d; n++)
            {
                GPSPoint g0 = data.GPS.DistanceToGpsPoint(n * d);
                double x = g0.LongitudeDecimalDegrees;
                double y = g0.LatiudeDecimalDegrees;
                Console.WriteLine(x.ToString() + "," + y.ToString());
                double IRI_left = data.ProfileLeft.IntervalIRI(n * d, (n + 1) * d);
                double IRI_right = data.ProfileRight.IntervalIRI(n * d, (n + 1) * d);
                double z = g0.Elevation;

                GPSPoint g1 = data.GPS.DistanceToGpsPoint((n + 1) * d);
                double x1 = g1.LongitudeDecimalDegrees;
                double y1 = g1.LatiudeDecimalDegrees;
                //double IRI_left2 = data.ProfileLeft.IntervalIRI(n*d,(n+1)*d);
                //double IRI_right2 = data.ProfileRight.IntervalIRI(n*d,(n+1)*d);
                double z1 = g1.Elevation;
                /////////////////////////////////////////////
                int index;
                Shape shp = new Shape(); //shp need to be created in the loop
                shp.Create(ShpfileType.SHP_POLYLINE);
                if (x < 0.00 && y > 0.00)
                {
                    MapWinGIS.Point pnt = new MapWinGIS.Point();
                    pnt.x = x;
                    pnt.y = y;
                    pnt.Z = z;
                    index = shp.numPoints;
                    shp.InsertPoint(pnt, ref index);
                }
                if (x1 < 0.00 && y1 > 0.00)
                {
                    MapWinGIS.Point pnt = new MapWinGIS.Point(); //the second point
                    pnt.x = x1;
                    pnt.y = y1;
                    pnt.Z = z1;
                    index = shp.numPoints;
                    shp.InsertPoint(pnt, ref index);
                }
                index = sf.NumShapes;
                sf.EditInsertShape(shp, ref index);

                string[] fList = { "Latitude", "Longitude", "Elevation", "IRI_Left", "IRI_Right" };
                foreach (string fd in fList)
                {
                    int fieldIndex = sf.Table.get_FieldIndexByName(fd);
                    if (fieldIndex == -1)
                    {
                        //make IRI field a double to get ready for color-coding
                        if (fd.Contains("IRI"))
                        {
                            fieldIndex = sf.EditAddField(fd, FieldType.DOUBLE_FIELD, 6, 9);
                        }
                        else
                        {
                            fieldIndex = sf.EditAddField(fd, FieldType.STRING_FIELD, 0, 30);
                        }
                    }
                    if (fd == "Latitude")
                    {
                        sf.Table.EditCellValue(fieldIndex, n, y.ToString());
                    }
                    if (fd == "Longitude")
                    {
                        sf.Table.EditCellValue(fieldIndex, n, x.ToString());
                    }

                    if (fd == "Elevation")
                    {
                        sf.Table.EditCellValue(fieldIndex, n, z1.ToString());
                    }
                    if (fd == "IRI_Left")
                    {
                        sf.Table.EditCellValue(fieldIndex, n, IRI_left);
                    }
                    if (fd == "IRI_Right")
                    {
                        sf.Table.EditCellValue(fieldIndex, n, IRI_right);
                    }
                }

                //////////////////////////////////////////

                // d *= d;
                // totalLength -= d;
            } //while (totalLength-d >0);
            Cursor.Current = Cursors.Default;
            string fname = Path.GetFileName(path).Replace(".ard", ".shp");
            //string f = @"C:\work\GIS\data" + fname;
            string f = "";
            SaveFileDialog sfdialog = new SaveFileDialog();
            sfdialog.Filter = "Shape File|*.shp";
            sfdialog.FileName = fname;
            sfdialog.Title = "Save Shapefile";

            if (sfdialog.ShowDialog() == DialogResult.OK)
            {             
                f = sfdialog.FileName;
            }
           
            //File.Delete(f);
            //deleteFiles(f);
            // MessageBox.Show(f);
            if (sf != null)
            {
                sf.SaveAs(f, null);
                //frm.Hide();
                // MessageBox.Show("Shapefile creatd and saved to " + f);
                AddLayerToMap(f);
            }
            else
            {
                MessageBox.Show("No shapefile created!");
            }
        }

        public void addArcGISService()
        {
            int ID = (int)tkTileProvider.ProviderCustom;
            //add custom provider definition

            axMap1.Tiles.Providers.Add(ID, "ESRI",
                "https://services.arcgisonline.com/arcgis/rest/services/World_Imagery/MapServer/tile/{zoom}/{y}/{x}",
                //"https://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{zoom}/{y}/{x}",
                tkTileProjection.SphericalMercator, 0, 19,
                "Esri, DigitalGlobe, GeoEye, i-cubed, USDA FSA, USGS, AEX, Getmapping, Aerogrid, IGN, IGP, swisstopo, and the GIS User Community");

            //set custom provider
            axMap1.Tiles.ProviderId = ID;

            //string fname = "World_Imagery";
            axMap1.Redraw3(tkRedrawType.RedrawSkipAllLayers, true);
            
        }

        public void removeArcGISService()
        {
            int ID = (int)tkTileProvider.ProviderCustom;
            //add custom provider definition

            axMap1.Tiles.Providers.Clear(true);
            axMap1.Tiles.Providers.Add(ID, "ESRI", "", tkTileProjection.SphericalMercator, 0, 19);
            //axMap1.Tiles.Providers.Add(ID, "ESRI",
            //    "https://services.arcgisonline.com/arcgis/rest/services/World_Imagery/MapServer/tile/{zoom}/{y}/{x}",
            //    tkTileProjection.SphericalMercator, 0, 19,
            //    "Esri, DigitalGlobe, GeoEye, i-cubed, USDA FSA, USGS, AEX, Getmapping, Aerogrid, IGN, IGP, swisstopo, and the GIS User Community");

            //set custom provider
            axMap1.Tiles.ProviderId = ID;

            //string fname = "World_Imagery";
            axMap1.Redraw3(tkRedrawType.RedrawSkipAllLayers, true);           
        }

        private void chkBox_addBaseMap_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBox_addBaseMap.Checked == true)
            {
                addArcGISService();
            }
            else
            {
                removeArcGISService();
            }
        }

        private void selectByToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSelectByAttributes fm = Application
              .OpenForms
              .OfType<frmSelectByAttributes>()
              .LastOrDefault();

            if (null == fm)
            {
               // fm = new frmSelectByAttributes(sf);
                fm = new frmSelectByAttributes();
                fm.cmbLayer.Items.Clear();
                foreach (TreeNode N in treeView1.Nodes)
                {
                    fm.cmbLayer.Items.Add(N.Text);
                }  
                fm.Show();
                fm.BringToFront();
            }
            else
            { // ...Yes. We have to activate it (i.e. bring to front, restore if minimized, focus)
                fm.Activate();
            }
        }

        private void axMap1_ShapeIdentified(object sender, _DMapEvents_ShapeIdentifiedEvent e)
        {
            int layerHandle = getLayerHandle();
            
            Shapefile sf = axMap1.get_Shapefile(layerHandle);
            sf.Identifiable = true;
            //MessageBox.Show(layerControl[layerHandle])
            if (sf != null)
            {
                double projX = 0.0;
                double projY = 0.0;
                // axMap1.PixelToProj(e.pointX, e.pointY, ref projX, ref projY);
                projX = e.pointX;
                projY = e.pointY;
                object result = null;
                Extents ext = new Extents(); 
                ext.SetBounds(projX, projY, 0.0, projX, projY, 0.0);
                if (sf.SelectShapes(ext, 0.0, SelectMode.INTERSECTION, ref result))
                {
                    int[] shapes = result as int[];
                    if (shapes == null) return;

                    if (shapes.Length > 1)
                    {
                        string s = "More than one shapes were selected. Shape indices:";
                        for (int i = 0; i < shapes.Length; i++)
                        {
                            s += shapes[i] + Environment.NewLine;
                            MessageBox.Show(s);
                        }
                    }
                    else
                    {
                        sf.set_ShapeSelected(shapes[0], true);  // selecting the shape we are about to edit
                        axMap1.Redraw(); Application.DoEvents();

                        Form form = new Form();
                        for (int i = 0; i < sf.NumFields; i++)
                        {
                            System.Windows.Forms.Label label = new System.Windows.Forms.Label();
                            label.Left = 15;
                            label.Top = i * 30 + 5;
                            label.Text = sf.Field[i].Name;
                            label.Width = 60;
                            form.Controls.Add(label);
                            //TextBox box = new TextBox();
                            System.Windows.Forms.Label box = new System.Windows.Forms.Label();
                            box.Left = 80;
                            box.Top = label.Top;
                            box.Width = 80;
                            box.Text = sf.CellValue[i, shapes[0]].ToString();
                            box.Name = sf.Field[i].Name;
                            form.Controls.Add(box);
                        }
                        form.Width = 180;
                        form.Height = sf.NumFields * 30 + 70;

                        form.FormClosed += FormFormClosed;
                        form.Text = "Shape: " + shapes[0];
                        form.ShowInTaskbar = false;
                        form.StartPosition = FormStartPosition.CenterParent;
                        form.FormBorderStyle = FormBorderStyle.FixedDialog;
                        form.MaximizeBox = false;
                        form.MinimizeBox = false;
                        form.Show();
                        //form.ShowDialog(axMap1.Parent);
                        // if (CheckOpened("form"))
                        // {
                        //     form.Activate();
                        // }
                        // else form.Show();
                    }
                }
                else MessageBox.Show("Nothing Selected");               
            }
        }

        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < axMap1.NumLayers; i++)
            {
                int layerHandle = axMap1.get_LayerHandle(i);
                Shapefile sf = axMap1.get_Shapefile(layerHandle);
                sf.SelectNone();
                axMap1.Redraw();
            }
        }

        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void toShapefileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int layerHandle = getLayerHandle();
            Shapefile sf = new Shapefile();
            Shapefile sf_export = new Shapefile();

            sf = axMap1.get_Shapefile(layerHandle);
            sf_export = sf.ExportSelection();
            saveFile(sf_export);
        }

        private void exportToCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "Output.csv";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Cannot write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dataGridView1.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[dataGridView1.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames += dataGridView1.Columns[i].HeaderText.ToString() + ",";
                            }
                            outputCsv[0] += columnNames;

                            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    outputCsv[i] += dataGridView1.Rows[i].Cells[j].Value.ToString() + ",";
                                }
                            }

                            File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }
        }
    }

    public class readARD
    {
        public AmesData data;
        public GPSPoint[] points;
        // public double IRI;
        public readARD(string path)
        {
            AmesData data = FileFormats.AmesRunData.File.RunFile.Read(path);
            //this.points = data.GPS.AllValues();
            //this.IRI = data.ProfileLeft.IntervalIRI(0, 258);

            //GPSPoint g0 = data.GPS.DistanceToGpsPoint(258);
            //double x = g0.LatiudeDecimalDegrees;
            this.data = data;

        }

        //public AmesData getData()
        //{
        //    if (data == null) {
        //        MessageBox.Show("data is empty");
        //    }

        //    return data; 
        //}


        public GPSPoint[] getPoints(string path)
        {

            //string path = @"\\Samba2\Shared-Big-SSD\AE Profiler Data Files\Napier Rd\Aetest1.ard";
            //path = @"\\Samba2\Shared-Big-SSD\AE Profiler Data Files\March 2015\Testing 03132015\GPS_DMI_Napier_WithFix4.ard";
            AmesData data = FileFormats.AmesRunData.File.RunFile.Read(path); //, !Active.Settings.DoNotApplyReverseFilter, _viewerConfig.ViewerPrePostSettings, Active.Settings.Analysis_LowSpeedCutoffMph, Active.Settings.AddGpsDropoutEvents);
            GPSPoint[] points = data.GPS.AllValues();

            //Profile[] Profiles = data.ProfileCenter;
            //double IRI = data.ProfileRight.IRI;

            this.points = points;

            //List<GPSPoint> points = data.GPS.AllValues();
            if (points.Length <= 0)
            {
                MessageBox.Show("File is empty");
            }
            return points;
        }
    }


}

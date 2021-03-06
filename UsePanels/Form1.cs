﻿ using AxMapWinGIS;
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
using System.Drawing;
using Microsoft.VisualBasic;


namespace UsePanels
{
    public partial class Form1 : Form
    {
        public Dictionary<string, int> layerControl = new Dictionary<string, int>();
        ListBox lstBShapeFields = new ListBox();

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
            //axMap1.Projection = tkMapProjection.PROJECTION_WGS84;
            axMap1.KnownExtents = tkKnownExtents.keUSA;
            panel1.Hide();
            //axMap1.MouseDownEvent -= AxMap1MouseDownEvent2;
            //axMap1.MouseDownEvent += AxMap1MouseDownEvent2;
            //axMap1.SendMouseMove = true;
            axMap1.SendMouseDown = true;
            axMap1.ShapeIdentified += axMap1_ShapeIdentified;
            axMap1.ShapeHighlighted += AxMap1ShapeHighlighted;

            treeView1.HideSelection = false;
        }
        private ToolStripStatusLabel m_label = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToLongDateString();
        }
        public void ShowAttributes()
        {
            axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
           // Shapefile sf = new Shapefile();
            //int handle = getLayerHandle();
            //sf = axMap1.get_Shapefile(handle);

            //m_layerHandle = axMap1.AddLayer(sf,true);
            //sf = axMap1.get_Shapefile(m_layerHandle);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding

            axMap1.SendMouseMove = true;
            axMap1.CursorMode = tkCursorMode.cmIdentify;
            axMap1.ShapeHighlighted += AxMap1ShapeHighlighted;
            //m_label = toolStripStatusLabel1;

        }
        private void AxMap1ShapeHighlighted(object sender, _DMapEvents_ShapeHighlightedEvent e)
        {
            toolStripStatusLabel1.Text = ""; 
            //lstAttributes.Items.Clear();
            Shapefile sf = axMap1.get_Shapefile(e.layerHandle); //very important way to get layerHandle
            if (sf != null)
            {
                string s = "";
                for (int i = 0; i < sf.NumFields; i++)
                {
                    if ( i <= 10) { 
                        string val = sf.get_CellValue(i, e.shapeIndex).ToString();
                        if (val == "") val = "null";
                        s += sf.Table.Field[i].Name + ": " + val + "; ";
                            //lstAttributes.Items.Add(sf.Table.Field[i].Name + ":" + val + "; ");
                    }
                }
                toolStripStatusLabel1.Text = s;
                //MessageBox.Show(s);
                //lstAttributes.Text = s; 
                lstAttributes.Hide();
                //lstAttributes.BringToFront();
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

        public void createAttributeTable(int[] lstSelected = null, string query = "")
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
                        for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                        {
                            if (j == i) dataGridView1.Rows[j].Selected = true;
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
                lblAttributeTitle.Text = layerName + ", with " + sf.NumShapes.ToString() + " rows with " + lstSelected.Length + " selected";
            }
            else lblAttributeTitle.Text = layerName + ", with " + sf.NumShapes.ToString() + " rows";
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
                //axMap1.DrawLineEx()
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
                    fname = fname + "(" + n.ToString() + ")";
                    //if (layerControl[fname]>-1) {
                    layerControl.Add(fname, layerHandle + 1000 + n);
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

                foreach (TreeNode node in treeView1.Nodes)
                {
                    node.Checked = true;
                }

                //System.Drawing.Image.FromHbitmap()
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
                    //ColorCodeShape(sf);
                    Utils utils = new Utils();
                    sf.DefaultDrawingOptions.LineWidth = 2;
                    sf.DefaultDrawingOptions.LineColor = utils.ColorByName(tkMapColor.Blue);
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
                    //ShowLegend(sf);
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
                //ShowLegend();
            }
            else
            {
                return;
            }
        }

        public void CheckAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = true;
                CheckChildren(node, true);
            }
        }

        public void UncheckAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = false;
                CheckChildren(node, false);
            }
        }

        private void CheckChildren(TreeNode rootNode, bool isChecked)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                CheckChildren(node, isChecked);
                node.Checked = isChecked;
            }
        }
        private void ShowLegend(Shapefile sf)
        {
            int width = 40;
            int height = 20;
            int padding = 5;
            int drawHandle = axMap1.NewDrawing(tkDrawReferenceList.dlScreenReferencedList);
            Labels labels = axMap1.get_DrawingLabels(drawHandle);
            if (labels != null)
                labels.Alignment = tkLabelAlignment.laBottomRight;
            //Shapefile sf = new Shapefile();
            //string message = "";
            for (int i = 0; i < axMap1.NumLayers; i++)
            {
                int layerHandle = axMap1.get_LayerHandle(i);
                sf = axMap1.get_Shapefile(layerHandle);
                // adds rectangle
                object x, y;
                int top = padding + i * (height + padding);
                if (sf.ShapefileType == ShpfileType.SHP_POLYGON)
                {
                    this.getRectange(padding, top, width, height, out x, out y);
                    axMap1.DrawPolygonEx(drawHandle, ref x, ref y, 4, sf.DefaultDrawingOptions.LineColor, true);
                }
                else if (sf.ShapefileType == ShpfileType.SHP_POINT)
                {
                    //double x = 0.0;
                    //double y = 0.0;
                    //shp.get_XY(0, ref x, ref y);
                    axMap1.DrawPointEx(drawHandle, 0.0, 0.0, 5, 0);
                }
                else if (sf.ShapefileType == ShpfileType.SHP_POLYLINE)
                {
                    axMap1.DrawLineEx(layerHandle, 0, 0, 0, 9, 3, sf.DefaultDrawingOptions.LineColor);
                }
                // adds text
                string text = axMap1.get_LayerName(layerHandle) + ".shp";
                var dlbls = axMap1.get_DrawingLabels(drawHandle);
                if (dlbls != null)
                    dlbls.AddLabel(text, padding * 2 + width, top + padding);
                // the position of text (for debugging)
                axMap1.DrawPointEx(drawHandle, padding * 2 + width, top, 2, 255);
            }
        }
        // <summary>
        // Returns coordinates of the rectangle with specified size and position
        // </summary>
        private void getRectange(int left, int top, int width, int height, out object xArr, out object yArr)
        {
            double[] x = new double[4];
            double[] y = new double[4];
            x[0] = left;
            x[1] = left;
            x[2] = left + width;
            x[3] = left + width;
            y[0] = top;
            y[1] = top + height;
            y[2] = top + height;
            y[3] = top;
            xArr = x; yArr = y;
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
            //axMap1.Projection = tkMapProjection.PROJECTION_WGS84;
            axMap1.ZoomToLayer(layerHandle);
            createAttributeTable();

            //Shapefile sf1 =axMap1.get_Shapefile(layerHandle);
            Shapefile sf1 = new Shapefile();
            Shapefile sf2 = new Shapefile();
           // string f1 = @"D:\shp\EB Cumberland + Roslyn Ave_profile_shapefile_csv.shp";
          //  string f2 = @"D:\shp\EB Greenwood_profile_shapefile_csv.shp";
          //  sf1.Open(f1, null);
          //  sf2.Open(f2, null);

           // mergeShapefile(sf1, sf2);

        }

        private void shpQueue()
        {
            System.Collections.Queue pointQueue = new System.Collections.Queue();
            System.Collections.Queue lineQueue = new System.Collections.Queue();
            System.Collections.Queue polygonQueue = new System.Collections.Queue();

            string[] filenames = openFile();

            if (filenames == null || filenames.Length == 0)
            {
                return;
            }

            if (filenames.Length == 1)
            {
                //AddLayerToMap(filenames[0]);
                MessageBox.Show("Need at least two shapefile to merge.");
                return;
            }

            foreach (var f in filenames)
            {
                //create shapefile here, get shapeType and add to different stack by type(point, polyline or polygon)
                Shapefile sp = new Shapefile();
                sp.Open(f, null);
                if (sp.ShapefileType == ShpfileType.SHP_POLYLINE)
                {
                    lineQueue.Enqueue(sp);
                }
                else if (sp.ShapefileType == ShpfileType.SHP_POINT)
                {
                    pointQueue.Enqueue(sp);
                }
                else if (sp.ShapefileType == ShpfileType.SHP_POLYGON)
                {
                    polygonQueue.Enqueue(sp);
                }
                else
                {
                    MessageBox.Show("Invalid Shapetype.");
                    return;
                }
              
            }
            if (lineQueue.Count > 0) mergeQueueFile(lineQueue);
            if (pointQueue.Count > 0) mergeQueueFile(pointQueue);
            if (polygonQueue.Count > 0) mergeQueueFile(polygonQueue);


            //Shapefile sf = new Shapefile();
            //sf.Open(fileQueue.Dequeue().ToString(),null);
            //Shapefile sf2 = new Shapefile();

            //while (fileQueue.Count > 0)
            //{
            //    sf2.Open(fileQueue.Dequeue().ToString(), null);
            //    sf = mergeShapefile(sf,sf2); 
            //}

           
        }
        private void btnMergeShapefiles_Click(object sender, EventArgs e)
        {
             //shpQueue();
            shpStack();
        }  
        
        private void shpStack()
        {
            System.Collections.Stack fileStack = new System.Collections.Stack();
            System.Collections.Stack pointStack = new System.Collections.Stack();
            System.Collections.Stack polylineStack = new System.Collections.Stack();
            System.Collections.Stack polygonStack = new System.Collections.Stack();

            string[] filenames = openFile();
            if (filenames == null || filenames.Length == 0)
            {
                return;
            }

            if (filenames.Length == 1)
            {
                //AddLayerToMap(filenames[0]);
                MessageBox.Show("Need at least two shapefile to merge.");
                return;
            }

            foreach (var f in filenames)
            {
                //create shapefile here, get shapeType and add to different stack by type(point, polyline or polygon)
                Shapefile sp = new Shapefile();
                sp.Open(f, null);
                if (sp.ShapefileType == ShpfileType.SHP_POLYLINE)
                {
                    polylineStack.Push(sp);
                }
                else if (sp.ShapefileType == ShpfileType.SHP_POINT)
                {
                    pointStack.Push(sp);
                }
                else if (sp.ShapefileType == ShpfileType.SHP_POLYGON)
                {
                    polygonStack.Push(sp);
                }
                else
                {
                    MessageBox.Show("Invalid Shapetype.");
                    return;
                }
                //fileStack.Push(f);
                //MessageBox.Show(f)
            }

            if (polylineStack.Count > 0) mergeStackFile(polylineStack);
            if (pointStack.Count > 0) mergeStackFile(pointStack);
            if (polygonStack.Count > 0) mergeStackFile(polygonStack);

        }
            

        public void mergeStackFile(System.Collections.Stack fileStack)
        {
            Shapefile sf = (Shapefile)fileStack.Pop();
            Shapefile sf2 = new Shapefile();
            while (fileStack.Count > 0)
            {
                sf2 = (Shapefile)fileStack.Pop();
                if (sf != null)
                {
                    sf = mergeShapefile(sf, sf2);
                }

            }
            DateTime now = DateTime.Now;

            string tString = now.ToString("yyyy'_'MM'_'dd'_'HH':'mm':'ss").Replace(":", "_").Replace(" ", "_");

           // MessageBox.Show(tString);
            
            // save if needed
            string filename = @"D:\shp\NorthDakota\merged_"+sf.ShapefileType+"_"+tString + ".shp";
            sf.SaveAs(filename, null);
            AddLayerToMap(filename);

        }

        public void mergeQueueFile(System.Collections.Queue fileQueue)
        {
            Shapefile sf = (Shapefile)fileQueue.Dequeue();
            Shapefile sf2 = new Shapefile();
            while (fileQueue.Count > 0)
            {
                sf2 = (Shapefile)fileQueue.Dequeue();
                if (sf != null)
                {
                    sf = mergeShapefile(sf, sf2);
                }

            }
            DateTime now = DateTime.Now;

            string tString = now.ToString("yyyy'_'MM'_'dd'_'HH':'mm':'ss").Replace(":", "_").Replace(" ", "_");

            // MessageBox.Show(tString);

            // save if needed
            string filename = @"D:\shp\NorthDakota\merged_" + sf.ShapefileType + "_" + tString + ".shp";
            sf.SaveAs(filename, null);
            AddLayerToMap(filename);

        }

        public Shapefile mergeShapefile(Shapefile sp1, Shapefile sp2)
        {
            if (sp1.ShapefileType != sp2.ShapefileType)
            {
                MessageBox.Show(sp1.Filename + " is " + sp1.ShapefileType + ", different from " + sp2.Filename + ", which is " + sp2.ShapefileType);
                return  null;
            }

            //if (sp1.NumFields != sp2.NumFields)
            //{

            //}

            Shapefile sf = sp1.Merge(false, sp2, false);
            return sf;
            //axMap1.AddLayer(sf, true);
            //

           // string filename = @"D:\shp\merged.shp";
          //  sf.SaveAs(filename, null);
           // AddLayerToMap(filename);
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
            //var fileContent = string.Empty;
            //open multiple shapefiles from on folder 
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
            //string filename = string.Empty;
            string[] fileNames = null;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (fileType.Contains("image"))
                {
                    openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.bmp, *.gif, *.png,*.tif) | *.jpg; *.jpeg; *.bmp; *.gif; *.png; *.tif";
                }
                else openFileDialog.Filter = fileType + " Files (*." + fileType + ")|*." + fileType;

                //MessageBox.Show(openFileDialog.Filter);
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                //openFileDialog.InitialDirectory = @"D:\shp";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    //filename = openFileDialog.FileName;
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

            //sf.DefaultDrawingOptions.UseLinePattern = false;
            //sf.DefaultDrawingOptions.LineColor = utils.ColorByName(tkMapColor.DarkRed);
            //sf.DefaultDrawingOptions.LineWidth = 2;
            //axMap1.Redraw();
            //ShapefileCategory ct = sf.Categories.Add("Poor");
            //ct = sf.Categories.Add("Good");
            //ct = sf.Categories.Add("Fair");
            //ct.DrawingOptions.UseLinePattern = false;

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
            //axMap1.Refresh();
            axMap1.Redraw();
            //axMap1.ZoomToWorld();
            // string nam = Path.GetFileNameWithoutExtension(sf.Filename);
            // MessageBox.Show(nam);

            //axMap1.ZoomToLayer(layerControl[nam]);
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
            if (e.Node.Checked)
            {
                if (axMap1.get_LayerVisible(layerHandle))
                {
                    axMap1.set_LayerVisible(layerHandle, true);
                }
                else axMap1.set_LayerVisible(layerHandle, true);
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
            if (layerControl[layerName] != -1)
            {
                axMap1.RemoveLayer(layerControl[layerName]);
                layerControl.Remove(layerName);
                treeView1.SelectedNode.Remove();
                panel1.Hide(); //hide the attribute table
            }
            else
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

           // ShowAttributes();
            //Cursor.Current = Cursor
            //axMap1.CursorMode = tkCursorMode.cmIdentify;
            axMap1.SendMouseDown = true;
            axMap1.CursorMode = tkCursorMode.cmIdentify;
            axMap1.ShapeIdentified += axMap1_ShapeIdentified;
            //if (treeView1.SelectedNode != null)
            //{
            //Shapefile sf = new Shapefile();
            ////int layerHandle = getLayerHandle();

            //axMap1.Identifier.IdentifierMode = tkIdentifierMode.imAllLayersStopOnFirst;    //imAllLayers;
            //Color theColor = Color.Brown;
            //axMap1.Identifier.OutlineColor = (uint)theColor.ToArgb();
            //axMap1.Identifier.HotTracking = false;

            //var shapes = axMap1.IdentifiedShapes;
            //for (int j = 0; j < shapes.Count; j++)
            //{
            //    int hnd = shapes.LayerHandle[j];

            //    sf = axMap1.get_Shapefile(hnd);
            //    sf.Identifiable = true;

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
            //    sf.Labels.Generate(expression, tkLabelPositioning.lpCentroid, false);
            //    sf.Labels.TextRenderingHint = tkTextRenderingHint.SystemDefault;

            //    MessageBox.Show(expression);
                
            //}


            // change MapEvents to axMap1
            // axMap1.MouseDownEvent -= AxMap1MouseDownEvent2;
            // axMap1.MouseDownEvent += AxMap1MouseDownEvent2;
            // this.ZoomToValue(sf, "Name", "Iowa");
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

            var img = new MapWinGIS.Image();

            string[] filenames = openFile("image");
            // OpenFileDialog dlg = new OpenFileDialog { Filter = img.CdlgFilter };
            if (filenames == null || filenames.Length == 0)
            {
                //MessageBox.Show("Nothing selected");
                return;
            }

            foreach (string f in filenames)
            {
                MessageBox.Show(f);
                if (f.ToLower().EndsWith(".tif") || f.ToLower().EndsWith(".jpg") || f.ToLower().EndsWith(".png"))
                {
                    if (img.Open(f))//,ImageType.JPEG_FILE,false,null))
                    {
                        layerHandle = axMap1.AddLayer(img, true);
                        if (layerHandle == -1)
                        {
                            Debug.Print(f + " cannot be added to map");
                            return;
                        }
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

        private string saveFile(string fileType)
        {
            string filename = string.Empty;
            //string[] fileNames = null;
            var fileContent = string.Empty;
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = fileType + " Files (*." + fileType + ")|*." + fileType;
                //MessageBox.Show(openFileDialog.Filter);
                sfd.FilterIndex = 1;
                sfd.RestoreDirectory = true;
                //sfd.InitialDirectory = @"D:\shp";
                sfd.FileName = treeView1.SelectedNode.Text;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filename = sfd.FileName;
                    // fileNames = sfd.FileNames;
                }
                else
                {
                    return null;
                }
            }
            return filename;
        }
        private void aRDToShapefileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] filenames = openFile("ard");
            // OpenFileDialog dlg = new OpenFileDialog { Filter = img.CdlgFilter };
            if (filenames == null || filenames.Length == 0)
            {
                //MessageBox.Show("Nothing selected");
                return;
            }

            foreach (string file in filenames)
            {
                readARD ra = new readARD(file);

                ArdToShapefile(file, ra, filenames.Length == 1);
            }
        }
        public void ArdToShapefile(string file, readARD ra, bool oneFile)
        {

            int m = 0;
            //Cursor.Current = Cursors.WaitCursor;
            //MessageBox.Show(openFile("ard"));

            //string[] st = openFile("ard");
            //string path = st[0];
            // string path = openFile("ard")[0];

            //readARD ra = new readARD(path); original code for single ard


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

                double IRI_left = 0;
                if (data.ProfileLeft != null)
                {
                    IRI_left = data.ProfileLeft.IntervalIRI(n * d, (n + 1) * d);
                }

                double IRI_right = 0;// 
                if (data.ProfileRight != null)
                {
                    IRI_right = data.ProfileRight.IntervalIRI(n * d, (n + 1) * d);
                }
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
            string shpFolder = "";

            if (sf != null)
            {
                m += 1;
                string fname = Path.GetFileName(file).Replace(".ard", ".shp");
                //string f = @"C:\work\GIS\data" + fname;
                string f = "";


                if (oneFile)
                {
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

                    sf.SaveAs(f, null);
                    //frm.Hide();
                    // MessageBox.Show("Shapefile creatd and saved to " + f);
                    AddLayerToMap(f);
                    return;
                }
                else //batch 
                {
                    string DirName = Path.GetDirectoryName(file);
                    shpFolder = DirName + "\\ConvertedShape";
                    System.IO.Directory.CreateDirectory(shpFolder);
                    f = shpFolder + "\\" + fname;
                    //File.Delete(fname+".*");
                    //if (File.Exists(f))
                    //{
                    //    string[] fs = Directory.GetFiles(shpFolder, fname.Replace(".shp", "") + ".*");
                    //    foreach (string af in fs)
                    //    {
                    //        MessageBox.Show(af);
                    //    }
                    //    return; 
                    //}

                    //MessageBox.Show(f);
                    sf.SaveAs(f, null);
                    //frm.Hide();
                    // MessageBox.Show("Shapefile creatd and saved to " + f);
                    AddLayerToMap(f);
                }
                // MessageBox.Show(string.Format("{0}.ard converted and saved to {1}", fname, shpFolder));
            }
            else
            {
                MessageBox.Show("No shapefile created!");
                return;
            }

        }

        private void ConvertShapeFileToKML(string KMLFileName, Shapefile sf)
        {
            //open the kml output file 
            var KMLFile = OpenKMLFile(KMLFileName);

            //Loop through the shape file, shape by shape
            int ConvertedShapeCount = 0;

            //the following code doesn't change the sf projection
            //sf.Projection = tkMapProjection.PROJECTION_WGS84.ToString();
            //axMap1.GrabProjectionFromData = true;

            //if (sf.ShapefileType == ShpfileType.SHP_POLYGON)
            if (sf.ShapefileType == ShpfileType.SHP_POLYLINE)
            {
                for (int ShapeIndex = 0; ShapeIndex <= sf.NumShapes - 1; ShapeIndex++)
                {
                    Shape shp = new Shape();
                    shp.Create(ShpfileType.SHP_POLYLINE);
                    shp = sf.Shape[ShapeIndex];  //Indexer cannot pass ref, so have to create local shp 
                    AddPolygonKML(ref shp, ref KMLFile, (ShapeIndex).ToString(), sf);
                    ConvertedShapeCount += 1;
                }

                CloseKMLFile(ref KMLFile);
                //Debug.Print("Processed " + ShapeIndex + " Shapes and converted " + ConvertedShapeCount + " to KML.");
                Debug.Print("Converted " + ConvertedShapeCount + " to KML.");
            }
            else
            {
                Debug.Print("Input shapefile is not a polygon file,conversion not supported yet");
            }

            //MessageBox.Show("KML " + KMLFileName + " created!");

            //open the kml in google earth
            if (File.Exists(@"C:\Program Files\Google\Google Earth Pro\client\googleearth.exe"))
            {
                // System.Diagnostics.Process.Start(,KMLFileName);
                System.Diagnostics.Process.Start(@KMLFileName);
            }
            else
            {
                MessageBox.Show("Google Earth is not found on this machine");
                return;
            }
        }
        //////////////////////////////////////////////////////////
        private void AddPolygonKML(ref MapWinGIS.Shape Shape, ref StreamWriter oWrite, string ShapeID, Shapefile sf)
        {
            Double LatDeg;
            Double LonDeg;
            Double Height;

            GeoProjection proj = new GeoProjection();
            //proj.ImportFromESRI()
            //Note: depending on whether this is AGL or ASL, change the KML setting below accordingly

            //Create a Placemark with no label to wrap the Polygon in
            oWrite.WriteLine("   <Placemark>");

            //Use the Shapes ID as a Name (optional)
            oWrite.WriteLine("      <name>Shape " + ShapeID + "</name>");

            //Pick up the Style we wish to use (otional, but allows for nice colours etc)
            oWrite.WriteLine("      <styleUrl>Shape Style</styleUrl>");

            //Start the Polygon KML object
            // oWrite.WriteLine("      <Polygon>");

            //Note there are two common options for Altitude:
            // "relativeToGround"  for Above Ground Level
            // "absolute" for Above Sea Level
            // See https://developers.google.com/kml/documentation/altitudemode

            //A Polygon is defined by a ring of coordinates
            //oWrite.WriteLine("         <outerBoundaryIs>");
            oWrite.WriteLine("           <styleUrl>#orange-5px</styleUrl>");
            oWrite.WriteLine("            <LinearRing>");
            //The <extrude> tag extends the line down to the ground
            oWrite.WriteLine("         <extrude>1</extrude>");
            oWrite.WriteLine("            <tessellate>1</tessellate>");
            oWrite.WriteLine("         <altitudeMode>relativeToGround</altitudeMode>");
            oWrite.WriteLine("            <coordinates>");

            //Loop through the points one by one
            //int fieldIndex = sf.Table.FieldIndexByName["IRI Averag"];
            int fieldIndex1 = sf.Table.FieldIndexByName["Elevation"]; ;
            //if (sf.Table.FieldIndexByName["Elevation"]>=0)
            //{
            //    fieldIndex1 = sf.Table.FieldIndexByName["Elevation"];
            //}

            for (int PointIndex = 0; PointIndex < Shape.numPoints; PointIndex++)
            {
                //Extract the 3D coordinates for the Point
                LonDeg = Shape.Point[PointIndex].x;
                //Console.WriteLine(LonDeg);
                LatDeg = Shape.Point[PointIndex].y;

                if (fieldIndex1 >= 0)
                {
                    // Height = Shape.Point[PointIndex].Z;

                    string h = (string)sf.get_CellValue(fieldIndex1, PointIndex);
                    // MessageBox.Show("h: "+ sf.get_CellValue(fieldIndex1, PointIndex).ToString());
                    //Height = Double.Parse(h != null ? (string)h : "0");
                    // MessageBox.Show(Height.ToString());
                    Height = Double.Parse(h);
                }
                else { Height = 500.000; }


                //MessageBox.Show(Height.ToString());
                // Debug.Print(Height.ToString());
                //create KML coordinate string is <lon,lat,height> {space} <lon,lat,height>
                //Note: Any space next to a ',' will create a new coordinate!
                oWrite.Write("                  ");
                oWrite.Write(string.Format("{0:0.000000}", LonDeg) + ",");
                oWrite.Write(string.Format("{0:0.000000}", LatDeg) + ",");
                oWrite.Write(string.Format("{0:000}", Height) + " ");
                oWrite.WriteLine();

            }

            //Note to save space we limit the Latitude to 6 decimal places

            //Close the relevant folders in reverse order
            oWrite.WriteLine("               </coordinates>");
            oWrite.WriteLine("            </LinearRing>");
            // oWrite.WriteLine("         </outerBoundaryIs>");
            // oWrite.WriteLine("      </Polygon>");
            oWrite.WriteLine("   </Placemark>");

        }

        private StreamWriter OpenKMLFile(string KMLFileName)
        {
            //To prevent appending from multiple runs, we delete the existing file, if any.
            if (File.Exists(KMLFileName))
            {
                File.Delete(KMLFileName);
            }

            //Create a new text file, to write the KML to
            StreamWriter oWrite = File.CreateText(KMLFileName);

            //Write the basic KML Header required so Google Earth can recognise the file as being KML
            oWrite.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            oWrite.WriteLine("<kml xmlns=\"http://earth.google.com/kml/2.2\">");

            //Start the Document
            oWrite.WriteLine("<Document>");

            //Choose a name, in this case we just use the filename.
            //Note this will cause errors in GE, if the Filename contains punctuation characters which aren//t allowed in GE comments
            oWrite.WriteLine("   <name>" + KMLFileName + "</name>");

            //Add an optional description
            oWrite.WriteLine("   <description> Google Earth KML file generated from an ERSI ShapeFile using MapWinGIS");
            oWrite.WriteLine("   Shape File to KML converter written by Ben Freeman,modified by Lang at Ames Engineering. </description>");

            //Define a style for the shapes 
            oWrite.WriteLine("   <Style id=" + (char)34 + "Shape Style" + (char)34 + ">");
            oWrite.WriteLine("      <LineStyle>");
            oWrite.WriteLine("         <color>ffff66c6</color>");
            oWrite.WriteLine("         <width>2</width>");
            oWrite.WriteLine("      </LineStyle>");
            oWrite.WriteLine("      <PolyStyle>");
            oWrite.WriteLine("         <color>5fff66c6</color>");
            oWrite.WriteLine("      </PolyStyle>");
            oWrite.WriteLine("   </Style>");

            //Open a folder for all the Shapes
            oWrite.WriteLine("   <Folder>");
            oWrite.WriteLine("      <name>Shapes</name>");

            //Return the file handle
            return oWrite;
        }

        private void CloseKMLFile(ref StreamWriter oWrite)
        {

            //Close kml document contents
            oWrite.WriteLine("   </Folder>");
            oWrite.WriteLine("</Document>");
            oWrite.WriteLine("</kml>");

            //Close the file
            oWrite.Close();
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

            int layerHandle = e.layerHandle; //getLayerHandle();
            if (layerHandle < 0) return; 
            Shapefile sf = axMap1.get_Shapefile(layerHandle);
            sf.Identifiable = true;
            //MessageBox.Show(layerControl[layerHandle])
            if (sf != null)
            {
                double projX = 0.0;
                double projY = 0.0;
                axMap1.PixelToProj(e.pointX, e.pointY, ref projX, ref projY);
                //projX = e.pointX;
                //projY = e.pointY;
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
            //int[] handles = new int[axMap1.NumLayers];
            //string[] names = new string[axMap1.NumLayers];

            //MessageBox.Show(handles.Length.ToString());
            //for (int i = 0; i < axMap1.NumLayers; i++)
            //{
            //    int layerHandle = axMap1.get_LayerHandle(i);
            //    handles[i] = layerHandle;
            //    names[i] = axMap1.get_LayerName(layerHandle);
            //}
            //foreach (var item in names)
            //{
            //    MessageBox.Show(item);
            //}

            clearSelections();
        }
        private void clearSelections()
        {
            //clear all selections in all layers 
            for (int i = 0; i < axMap1.NumLayers; i++)
            {
                int layerHandle = axMap1.get_LayerHandle(i);
                Shapefile sf = axMap1.get_Shapefile(layerHandle);
                sf.SelectNone();
                axMap1.Redraw();
            }
            dataGridView1.ClearSelection();
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

                            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
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

        private void shapefileToKMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //make sure mapproject is geographic (ie: WGS84)
            axMap1.Projection = tkMapProjection.PROJECTION_WGS84;
            //var c = axMap1.NumLayers;
            //Console.WriteLine("Layers in map: " + c.ToString());
            //if (c == 0)
            //{
            //    MessageBox.Show("No layers in map, need to add one");
            //    return;
            //}
            string[] filenames = openFile();
            if (filenames == null || filenames.Length == 0)
            {
                return;
            }

            int counter = 0;
            string filePath = Path.GetDirectoryName(filenames[0]);
            string logFile = Path.Combine(filePath, "log.txt");
            using (StreamWriter w = File.AppendText(logFile)) //create one if not exist, else append to the existing one
            {
                try
                {
                    w.WriteLine(filenames.Length + " shape files to be converted:");

                    foreach (string f in filenames)
                    {
                        Shapefile sf = new Shapefile();
                        if (f.EndsWith(".shp"))
                        {
                            sf.Open(f);
                            ConvertShapeFileToKML(f.Replace(".shp", ".kml"), sf);
                            w.WriteLine(f + " converted to kml.");
                            counter += 1;
                        }
                        else
                        {
                            MessageBox.Show(f + " is not a shapefile, will not convert.");
                            w.WriteLine(f + " cannot be converted to kml.");
                        }
                    }
                    w.WriteLine(counter + " shapefiles converted to kml.");
                }
                catch (Exception EX)
                {
                    w.WriteLine(EX.ToString());
                }
            }
        }

        private void shapefileToKMLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //make sure mapproject is geographic (ie: WGS84)
            //axMap1.Projection = tkMapProjection.PROJECTION_WGS84;
            //var c = axMap1.NumLayers;
            // int layerHandle = getLayerHandle();  
            //var sf = new Shapefile();
            int layerHandle = getLayerHandle();
            Shapefile sf = axMap1.get_Shapefile(layerHandle);
            string kmlfileName = saveFile("kml");
            ConvertShapeFileToKML(kmlfileName, sf);
        }

        public void CreateBuffer(ToolStripStatusLabel label)
        {
            axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
            //string filename = dataPath + "waterways.shp";
            //if (!File.Exists(filename))
            //{
            //    MessageBox.Show("The shapefile with rivers wasn't found: " + filename);
            //}
            //else
            int handle = getLayerHandle();
            var callback = new Callback(label);

            var sf = new Shapefile();
            sf = axMap1.get_Shapefile(handle);
            //if (!sf.Open(filename, callback))
            //{
            //    MessageBox.Show(sf.ErrorMsg[sf.LastErrorCode]);
            //}
            //else
            //{
            int layerHandle = axMap1.AddLayer(sf, true);
            sf = axMap1.get_Shapefile(layerHandle);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding

            var utils = new Utils();
            sf.DefaultDrawingOptions.LineWidth = 3.0f;
            sf.DefaultDrawingOptions.LineColor = utils.ColorByName(tkMapColor.Blue);

            const double distance = 150; // meters
            var buffers = new List<Shapefile>();
            for (int i = 1; i < 5; i++)
            {
                Shapefile sfBuffer = sf.BufferByDistance(distance * i, 30, false, true);
                if (sfBuffer == null)
                {
                    MessageBox.Show("Failed to calculate the buffer: " + sf.ErrorMsg[sf.LastErrorCode]);
                    return;
                }
                else
                {
                    sfBuffer.GlobalCallback = callback;
                    buffers.Add(sfBuffer);
                }
            }
            // now subtract smaller buffers from larger ones
            for (int i = buffers.Count - 1; i > 0; i--)
            {
                Shapefile sfDiff = buffers[i].Difference(false, buffers[i - 1], false);
                if (sfDiff == null)
                {
                    MessageBox.Show("Failed to calculate the difference: " + sf.ErrorMsg[sf.LastErrorCode]);
                    return;
                }
                else
                {
                    buffers[i] = sfDiff;
                }
            }
            // pass all the resulting shapes to a single shapefile and mark their distance
            Shapefile sfResult = buffers[0].Clone();
            sfResult.GlobalCallback = callback;
            int fieldIndex = sfResult.EditAddField("Buffer_D", FieldType.DOUBLE_FIELD, 10, 12);

            for (int i = 0; i < buffers.Count; i++)
            {
                Shapefile sfBuffer = buffers[i];
                for (int j = 0; j < sfBuffer.NumShapes; j++)
                {
                    int index = sfResult.NumShapes;
                    sfResult.EditInsertShape(sfBuffer.Shape[j].Clone(), ref index);
                    sfResult.EditCellValue(fieldIndex, index, distance * (i + 1));
                }
            }

            // create visualization categories
            sfResult.DefaultDrawingOptions.FillType = tkFillType.ftStandard;
            sfResult.Categories.Generate(fieldIndex, tkClassificationType.ctUniqueValues, 0);
            sfResult.Categories.ApplyExpressions();
            // apply color scheme
            var scheme = new ColorScheme();
            scheme.SetColors2(tkMapColor.LightBlue, tkMapColor.LightYellow);
            sfResult.Categories.ApplyColorScheme(tkColorSchemeType.ctSchemeGraduated, scheme);
            layerHandle = axMap1.AddLayer(sfResult, true);
            axMap1.ZoomToLayer(layerHandle);
            axMap1.Redraw();
            //sfResult.SaveAs(@"c:\buffers.shp", null);
            //}

        }

        private void bufferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createBuffer();
        }

        private void labelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstBShapeFields.Location = new System.Drawing.Point(700, 60);
            //lstBShapeFields.Name = "lstBShapeFields";
            lstBShapeFields.Text = "Choose Field to Label:";
            lstBShapeFields.Size = new System.Drawing.Size(100, 200);
            lstBShapeFields.BackColor = Color.Green;
            lstBShapeFields.ForeColor = Color.White;
            //subscribe the event
            lstBShapeFields.SelectedIndexChanged += lstBShapeFields_SelectedIndexChanged;
            this.Controls.Add(lstBShapeFields);

            lstBShapeFields.BringToFront();
            if (labelToolStripMenuItem.Checked == true)
            {
                labelToolStripMenuItem.Checked = false;
                lstBShapeFields.Hide();
                //chkToggleLabelWin.Checked = false;
            }
            else
            {
                labelToolStripMenuItem.Checked = true;
                lstBShapeFields.Show();
                //chkToggleLabelWin.Checked = true; 
            }

            UpdateFieldsList();

        }

        private void UpdateFieldsList()
        {
            int layerHandle = getLayerHandle();
            //lableling doesn't work on line feature if not in google_mercator
            // axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
            // axMap1.ZoomToLayer(layerHandle);
            var sf = new Shapefile();

            sf = axMap1.get_Shapefile(layerHandle);
            lstBShapeFields.Items.Clear();
            for (int i = 0; i < sf.NumFields; i++)
            {
                // listBox1.Items.Add(sf.Field[i].Name);
                lstBShapeFields.Items.Add(sf.Field[i].Name);

            }
        }
        private void lstBShapeFields_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            //MessageBox.Show("Index Change Fired");
            int layerHandle = getLayerHandle();

            var sf = new Shapefile();

            sf = axMap1.get_Shapefile(layerHandle);


            // Get the currently selected item in the ListBox.

            string labelFiledName = lstBShapeFields.SelectedItem.ToString();
            ///////////////////////////////
            int fieldIndex = sf.Table.get_FieldIndexByName(labelFiledName);

            //sf.Field.GetType()

            //double valField = 0.00;
            // get average of a numberic field, has some issue.
            //for (int i = 0; i < sf.NumShapes; i++)
            //{
            //    valField += Convert.ToDouble(sf.CellValue[fieldIndex, i]);
            //}
            // string averageVal = (valField / sf.NumShapes).ToString();
            // MessageBox.Show("Average: "+averageVal);
            //labelFiledName = curItem;

            if (labelToolStripMenuItem.Checked == false)
            {
                if (sf.ShapefileType == ShpfileType.SHP_POLYLINE)
                {
                    sf.Labels.Generate("[" + labelFiledName + "]", tkLabelPositioning.lpMiddleSegment, false);
                }
                else
                {
                    sf.Labels.Generate("[" + labelFiledName + "]", tkLabelPositioning.lpCentroid, false);
                    
                }
                //sf.Labels.Generate("[COUNTY]", tkLabelPositioning.lpCentroid, false);
                sf.Labels.Synchronized = true;
                sf.Labels.TextRenderingHint = tkTextRenderingHint.SystemDefault;
                axMap1.Redraw();
                labelToolStripMenuItem.Checked = true; //add check mark to the menu item
            }
            else
            {
                sf.Labels.Clear(); //remove all labels
                axMap1.Redraw();
                labelToolStripMenuItem.Checked = false;
            }
        }

        private void chkToggleLabelWin_CheckedChanged(object sender, EventArgs e)
        {
            int handle = getLayerHandle();

            if (lstBShapeFields.Visible)
            {
                lstBShapeFields.Hide();
                this.Text = "Show Label Box";

            }
            else
            {
                lstBShapeFields.Show();
                this.Text = "Hide Label Box";
                UpdateFieldsList();
            }
        }

        private void changeLineSymbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int layerHandle = getLayerHandle();
            Shapefile sf = axMap1.get_Shapefile(layerHandle);

            TreeNode nd = treeView1.SelectedNode;
            int id = nd.SelectedImageIndex;

            Bitmap image1 = (Bitmap)treeView1.ImageList.Images[id];
            int x, y;
            for (x = 0; x < image1.Width; x++)
            {
                for (y = 0; y < image1.Height; y++)
                {
                    Color pixelColor = image1.GetPixel(x, y);
                    Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
                    image1.SetPixel(x, y, newColor);
                }
            }
            pictureBox1.Image = image1;
            //clear all selections in map
            clearSelections();
            //axMap1.Refresh();
            frmColorCodeLine fm = Application
              .OpenForms
              .OfType<frmColorCodeLine>()
              .LastOrDefault();
            if (fm == null)
            {
                fm = new frmColorCodeLine();//(ref sf);
                fm.Show();
            }
            else fm.Activate();
            //frmColorCodeLine fm = new frmColorCodeLine(ref sf);
            fm.lstIRIValue.Items.Clear();
            fm.Text = sf.Filename;// + "Color Code Break";

            int fieldIndex = -1;
            string[] iriNamelist = { "IRI Averag", "IRI", "IRI_Left", "IRI_Right" };

            for (int i = 0; i < iriNamelist.Length; i++)
            {
                //MessageBox.Show(iriNamelist[i]);
                fieldIndex = sf.Table.FieldIndexByName[iriNamelist[i]];

                if (fieldIndex > -1)
                {
                    fm.lblFieldName.Text = iriNamelist[i] + " Field Values:";
                    break;
                }
            }
            //fieldIndex = sf.Table.FieldIndexByName["IRI Averag"];
            if (fieldIndex == -1)
            {
                MessageBox.Show("No IRI field found in shapefile " + sf.Filename);
                return;
            }
            List<double> IriList = new List<double>();
            for (int i = 0; i < sf.NumShapes; i++)
            {
                double value = (double)sf.get_CellValue(fieldIndex, i);
                fm.lstIRIValue.Items.Add(value);
                fm.lstIRIValue.Sorted = true;
                IriList.Add(value);
            }

            double average = IriList.Average();
            double sumOfSquaresOfDifferences = IriList.Select(val => (val - average) * (val - average)).Sum();
            double sd = Math.Sqrt(sumOfSquaresOfDifferences / IriList.Count);
            string poorValue = Math.Round(average + sd).ToString();
            string goodValue = Math.Round(average - sd).ToString();

            fm.label10.Text = "Total line section: " + IriList.Count + " /Average: " + Math.Round(average).ToString();
            fm.txtFair.Text = poorValue; //.PadLeft(3,'0');          
            fm.txtGood.Text = goodValue;//.PadLeft(3,'0');
                                        //fm.BringToFront();
                                        //axMap1.SendToBack();
                                        // managerForm.CreateShape += ManagerForm_CreateShape;
                                        //fm.ColorCodeShape += ColorCodeShape;  //add to delegate 
            fm.Show();
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //Unhighlight old node
            if (treeView1.SelectedNode != null)
            {
                treeView1.SelectedNode.BackColor = SystemColors.Window;
                treeView1.SelectedNode.ForeColor = SystemColors.WindowText;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Highlight new node
            treeView1.SelectedNode.BackColor = SystemColors.Highlight;
            treeView1.SelectedNode.ForeColor = SystemColors.HighlightText;
        }
        // the node selection occurs on MouseUp instead of MouseDown,
        //and that nothing is highlighted while the mouse is down on subsequent selections.
        //To remedy this, simply select the node in the MouseDown event:
        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode node = treeView1.GetNodeAt(e.X, e.Y);
            if (node != null) treeView1.SelectedNode = node;

        }
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Rectangle ee = new Rectangle(10, 10, 30, 30);
            using (Pen pen = new Pen(Color.Red, 2))
            {
                e.Graphics.DrawRectangle(pen, ee);
            }
        }
        private void intersectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripStatusLabel label = new ToolStripStatusLabel();
            string dataPath = @"C:\Work\GIS\data\USA\";
            string filename1 = dataPath + "states.shp";
            string filename2 = dataPath + "rivers.shp";

            if (!File.Exists(filename1) || !File.Exists(filename2))
            {
                MessageBox.Show("The necessary files (waterways.shp, building.shp) are missing: " + dataPath);
            }
            else
            {
                Shapefile sfStates = new Shapefile();
                sfStates.Open(filename1, null);
                sfStates.StartEditingShapes(true, null);

                Field field = new Field();
                field.Name = "Length";
                field.Type = FieldType.DOUBLE_FIELD;
                field.Precision = 10;
                int fieldIndex = sfStates.NumShapes;
                sfStates.EditInsertField(field, ref fieldIndex, null);

                Shapefile sfRivers = new Shapefile();
                sfRivers.Open(filename2, null);
                sfRivers.StartEditingShapes(true, null);
                Utils utils = new Utils();
                sfRivers.DefaultDrawingOptions.LineWidth = 2;
                sfRivers.DefaultDrawingOptions.LineColor = utils.ColorByName(tkMapColor.Blue);

                Shapefile sfNew = sfRivers.Clone();
                ShapeDrawingOptions options = sfNew.DefaultDrawingOptions;

                LinePattern pattern = new LinePattern();
                pattern.AddLine(utils.ColorByName(tkMapColor.Blue), 8, tkDashStyle.dsSolid);
                pattern.AddLine(utils.ColorByName(tkMapColor.LightBlue), 4, tkDashStyle.dsSolid);
                options.LinePattern = pattern;
                options.UseLinePattern = true;

                for (int i = 0; i < sfStates.NumShapes; i++)
                {
                    Shape shp1 = sfStates.get_Shape(i);
                    double length = 0.0;    // the length of intersection

                    for (int j = 0; j < sfRivers.NumShapes; j++)
                    {
                        Shape shp2 = sfRivers.get_Shape(j);
                        if (shp1.Intersects(shp2))
                        {
                            Shape result = shp1.Clip(shp2, tkClipOperation.clIntersection);
                            if (result != null)
                            {
                                int index = sfNew.EditAddShape(result);
                                length += result.Length;
                            }
                        }
                    }
                    sfStates.EditCellValue(fieldIndex, i, length);
                    label.Text = string.Format("Length: {0}/{1}", i + 1, sfStates.NumShapes);
                    Application.DoEvents();
                }


                // generating charts
                ChartField chartField = new ChartField();
                chartField.Name = "Length";
                chartField.Color = utils.ColorByName(tkMapColor.LightBlue);
                chartField.Index = fieldIndex;
                sfStates.Charts.AddField(chartField);
                sfStates.Charts.Generate(tkLabelPositioning.lpInteriorPoint);
                sfStates.Charts.ChartType = tkChartType.chtBarChart;
                sfStates.Charts.BarHeight = 100;
                sfStates.Charts.ValuesVisible = true;
                sfStates.Charts.Visible = true;

                AddLayerToMap(filename1);
                AddLayerToMap(filename2);
                string newShape = dataPath + "interSect.shp";
                if (sfNew.SaveAs(newShape, null))
                {
                    AddLayerToMap(newShape);
                }
                else MessageBox.Show(newShape + " is not saved.");

                //axMap1.AddLayer(sfStates, true);
                //axMap1.AddLayer(sfRivers, true);
                //axMap1.AddLayer(sfNew, true);
                axMap1.ZoomToMaxExtents();
            }
        }

        private void toolHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Map control uses the following built-in keyboard hotkeys:\n" +
               " \n" +
           "'+' - zoom in;\n" +
           " \n" +
           "'-' => zoom out;\n" +
           " \n" +
           "'*' => zoom to the closest tile level;\n" +
           " \n" +
           "'Home' => zoom to combined extents of all data layers(max extents);\n" +
           " \n" +
           "'Backspace' => zoom to previous extents;\n" +
           " \n" +
           "'Shift + Left, 'Shift + Right' => zoom to the prev / next layer;\n" +
           " \n" +
           "'Z' => ZoomIn tool;\n" +
           " \n" +
           "'M' => measuring tool: press first is for length, press again measues area.\n" +
           " \n" +
           "'Space' => switches to panning mode; after releasing shift the previous map cursor is restored;\n" +
           " \n" +
           "arrow keys => to move the map;\n" +
           " \n" +
           "mouse wheel => to zoom in/out regardless of the current tool.\n" +
           " \n" +
          "Hot keys will work if map is focused.It's enough to click the map with mouse to set input focus to it.");

        }

        private void labelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var sf = new Shapefile();
            sf = axMap1.get_Shapefile(0);     // in case a copy of shapefile was created by GlobalSettings.ReprojectLayersOnAdding
            int fieldIndex = 0;
            for (int i = 0; i < sf.NumShapes; i++)
            {
                Shape shp = sf.Shape[i];
                string text = sf.CellValue[fieldIndex, i].ToString();
                MapWinGIS.Point pnt = shp.Centroid;
                sf.Labels.AddLabel(text, pnt.x, pnt.y, 0.0, -1);
                // the old method should be used like this
                //axMap1.AddLabel(layerHandle, text, 0, pnt.x, pnt.y, tkHJustification.hjCenter);
            }
            sf.Labels.Synchronized = true;
            axMap1.Redraw();
        }

        private void bufferToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }
        private void createBuffer()
        {
            ToolStripStatusLabel label = new ToolStripStatusLabel();
            var callback = new Callback(label);
            double distance = 0.00;
            string bufferDistance = Interaction.InputBox("Enter a distance as ft (feet), mi (mile) or me (meter):", "Buffer Distance", "500 ft", 200, 300);

            axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
            if (bufferDistance != "")
            {
                MessageBox.Show(bufferDistance);
            }

            if (bufferDistance.Split(' ')[1] == "ft")
            {
                distance = Convert.ToDouble(bufferDistance.Split(' ')[0]) / 3.33;
            }
            else if (bufferDistance.Split(' ')[1] == "mi")
            {
                distance = Convert.ToDouble(bufferDistance.Split(' ')[0]) * 1609;
            }
            else distance = Convert.ToDouble(bufferDistance.Split(' ')[0]);

            var sf = new Shapefile();
            int layerHandle = getLayerHandle();
            sf = axMap1.get_Shapefile(layerHandle);

            //MessageBox.Show(DateAndTime.TimeString);

            var utils = new Utils();
            sf.DefaultDrawingOptions.LineWidth = 3.0f;
            sf.DefaultDrawingOptions.LineColor = utils.ColorByName(tkMapColor.Blue);
            var buffers = new List<Shapefile>();
            for (int i = 1; i < 5; i++)
            {
                Shapefile sfBuffer = sf.BufferByDistance(distance * i, 30, false, true);
                if (sfBuffer == null)
                {
                    MessageBox.Show("Failed to calculate the buffer: " + sf.ErrorMsg[sf.LastErrorCode]);
                    return;
                }
                else
                {
                    sfBuffer.GlobalCallback = callback;
                    buffers.Add(sfBuffer);
                }
            }
            // now subtract smaller buffers from larger ones
            for (int i = buffers.Count - 1; i > 0; i--)
            {
                Shapefile sfDiff = buffers[i].Difference(false, buffers[i - 1], false);
                if (sfDiff == null)
                {
                    MessageBox.Show("Failed to calculate the difference: " + sf.ErrorMsg[sf.LastErrorCode]);
                    return;
                }
                else
                {
                    buffers[i] = sfDiff;
                }
            }
            // pass all the resulting shapes to a single shapefile and mark their distance
            Shapefile sfResult = buffers[0].Clone();
            sfResult.GlobalCallback = callback;
            int fieldIndex = sfResult.EditAddField("Distance", FieldType.DOUBLE_FIELD, 10, 12);

            for (int i = 0; i < buffers.Count; i++)
            {
                Shapefile sfBuffer = buffers[i];
                for (int j = 0; j < sfBuffer.NumShapes; j++)
                {
                    int index = sfResult.NumShapes;
                    sfResult.EditInsertShape(sfBuffer.Shape[j].Clone(), ref index);
                    sfResult.EditCellValue(fieldIndex, index, distance * (i + 1));
                }
            }

            // create visualization categories
            sfResult.DefaultDrawingOptions.FillType = tkFillType.ftStandard;
            sfResult.Categories.Generate(fieldIndex, tkClassificationType.ctUniqueValues, 0);
            sfResult.Categories.ApplyExpressions();
            // apply color scheme
            var scheme = new ColorScheme();
            scheme.SetColors2(tkMapColor.LightBlue, tkMapColor.LightYellow);
            sfResult.Categories.ApplyColorScheme(tkColorSchemeType.ctSchemeGraduated, scheme);
            layerHandle = axMap1.AddLayer(sfResult, true);
            axMap1.set_ShapeLayerFillTransparency(layerHandle, 0.5f);
            axMap1.Redraw();
            //string f = @"c:\work\gis\data\buffer_" + bufferDistance + "_" + DateAndTime.TimeString.Replace(":", "_") + ".shp";
            //sfResult.SaveAs(f, null);
            //axMap1.AddLayerFromFilename(f, tkFileOpenStrategy.fosAutoDetect, true);

        }

        private void toolSelectShape_Click(object sender, EventArgs e)
        {
            axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
            //axMap1.SendMouseDown = true;
            //axMap1.CursorMode = tkCursorMode.cmNone;
            // axMap1.MouseDownEvent += AxMap1MouseDownEvent3;  // change MapEvents to axMap1
            Shapefile sf = new Shapefile();
            int layerHandle = getLayerHandle();//axMap1.get_LayerHandle(0);
            sf = axMap1.get_Shapefile(layerHandle);
            if (!sf.StartEditingShapes(true, null))
            {
                MessageBox.Show("Failed to start edit mode: " + sf.Table.ErrorMsg[sf.LastErrorCode]);
            }
            else
            {
                sf.UseQTree = true;
                //sf.Labels.Generate("[Name]", tkLabelPositioning.lpCentroid, false);
                axMap1.AddLayer(sf, true);
                axMap1.SendSelectBoxFinal = true;
                axMap1.SelectBoxFinal += AxMap1SelectBoxFinal; // change MapEvents to axMap1
                axMap1.MapUnits = tkUnitsOfMeasure.umMeters;
                //axMap1.CurrentScale = 50000;
                axMap1.CursorMode = tkCursorMode.cmSelection;
            }
        }

        void AxMap1SelectBoxFinal(object sender, _DMapEvents_SelectBoxFinalEvent e)
        {
            // it's assumed here that the layer we want to edit is the first 1 (with 0 index)
            //MessageBox.Show(e.ToString());
            //lblAttributeTitle.Hide();
            // var shapes = axMap1.IdentifiedShapes;

            //for (int j = 0; j < shapes.Count; j++)
            //{
            //    int layerHandle = shapes.LayerHandle[j];
            //    Shapefile sf = axMap1.get_Shapefile(layerHandle);
            //    sf = axMap1.get_Shapefile(layerHandle);
            //    sf.Identifiable = true;
            int layerHandle = getLayerHandle();//axMap1.get_LayerHandle(0);
            Shapefile sf = axMap1.get_Shapefile(layerHandle);

            if (sf != null)
                {
                    double left = 0.0;
                    double top = 0.0;
                    double bottom = 0.0;
                    double right = 0.0;
                    axMap1.PixelToProj(e.left, e.top, ref left, ref top);
                    axMap1.PixelToProj(e.right, e.bottom, ref right, ref bottom);
                    object result = null;
                    var ext = new Extents();
                    ext.SetBounds(left, bottom, 0.0, right, top, 0.0);
                   
                    sf.SelectNone();
                    if (sf.SelectShapes(ext, 0.0, SelectMode.INTERSECTION, ref result))
                    {
                        int[] selectedShapes = result as int[];
                        if (selectedShapes == null) return;

                        for (int i = 0; i < selectedShapes.Length; i++)
                        {
                            sf.set_ShapeSelected(selectedShapes[i], true);
                        //selectedList[i] = i;

                        }
                    //MessageBox.Show(selectedShapes.Length.ToString());
                    createAttributeTable(selectedShapes);
                }
                              
                moveSelectedRowstoTop_dataGridView(sf);
                axMap1.Redraw();
                           
            }
           
            //MessageBox.Show(sf.NumSelected.ToString());
            //axMap1.SelectBoxFinal -= AxMap1SelectBoxFinal;
            //axMap1.CursorMode = tkCursorMode.cmPan;
        }

        void moveSelectedRowstoTop_dataGridView(Shapefile sf)
        {
            for (int i = 0; i < sf.NumShapes; i++)
            {
                
                if (sf.ShapeSelected[i] == true)
                {
                    dataGridView1.Rows[i].Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
                }
            }
        }
        private void AxMap1MouseDownEvent3(object sender, _DMapEvents_MouseDownEvent e)
        {
            //int layerHandle = axMap1.get_LayerHandle(0);  // it's assumed here that the layer we want to edit is the first 1 (with 0 index)
            //string layerName = treeView1.SelectedNode.Text;
            var shapesFound = axMap1.IdentifiedShapes;
            for (int j = 0; j < shapesFound.Count; j++)
            {
                int layerHandle = shapesFound.LayerHandle[j];
                Shapefile sf = axMap1.get_Shapefile(layerHandle);
                sf = axMap1.get_Shapefile(layerHandle);
                //int layerHandle = layerControl[layerName];
                //Shapefile sf = axMap1.get_Shapefile(layerHandle);
            
            //tkWgs84Projection.Wgs84_UTM_zone_15N = 32615;
            //GeoProjection gp = new GeoProjection();
            //gp.ImportFromEPSG(32615);
            //axMap1.GeoProjection = gp;

                if (sf != null)
                {
                    double projX = 0.0;
                    double projY = 0.0;
                    axMap1.PixelToProj(e.x, e.y, ref projX, ref projY);

                    object result = null;
                    Extents ext = new Extents();
                    ext.SetBounds(projX, projY, 0.1, projX, projY, 0.1);
                    if (sf.SelectShapes(ext, 0.0, SelectMode.INCLUSION, ref result))
                    {
                        int[] shapes = result as int[];
                        if (shapes == null) return;
                        if (shapes.Length > 1)
                        {
                            string s = "More than one shapes were selected. Shape indices:";
                            for (int i = 0; i < shapes.Length; i++)
                                s += shapes[i] + Environment.NewLine;
                            MessageBox.Show(s);
                        }
                        else
                        {
                            sf.set_ShapeSelected(shapes[0], true);  // selecting the shape we are about to edit
                            axMap1.Redraw();
                            Application.DoEvents();
                        }
                    }
                }
                MessageBox.Show(sf.NumSelected.ToString());
            }           
        }


        class Callback : ICallback
        {
            private ToolStripStatusLabel m_label = null;
            public Callback(ToolStripStatusLabel label)
            {
                m_label = label;
                if (label == null)
                    throw new NullReferenceException("No reference to the label was provided");
            }

            public void Error(string KeyOfSender, string ErrorMsg)
            {
                Debug.Print("Error reported: " + ErrorMsg);
            }
            public void Progress(string KeyOfSender, int Percent, string Message)
            {
                m_label.Text = Message + ": " + Percent + "%";
                Application.DoEvents();
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


        private void upToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int handle = getLayerHandle();
            //int position = axMap1.get_LayerPosition(handle);
            //axMap1.MoveLayerUp(position - 1);
            MoveItem(-1);
            //treeView1.SelectedNode = null;
            //treeView1.SelectedNode = treeView1.Nodes[0];
        }

        private void downToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int handle = getLayerHandle();
            //int position = axMap1.get_LayerPosition(handle);
            //axMap1.MoveLayerDown(position);
            MoveItem(1);
        }

        private void toTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int handle = getLayerHandle();
            //int position = axMap1.get_LayerPosition(handle);
            //axMap1.MoveLayerTop(position);
            int currentPosition = treeView1.SelectedNode.Index;
            MoveItem(-currentPosition);
            treeView1.SelectedNode = null;
            //treeView1.SelectedNode = treeView1.Nodes[0];
        }

        private void toBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            //axMap1.MoveLayerBottom(position);
            int currentPosition = treeView1.SelectedNode.Index;
            MoveItem(axMap1.NumLayers-currentPosition-1);
            treeView1.SelectedNode = null; //unselect all nodes, 
            //treeView1.SelectedNode = treeView1.Nodes[axMap1.NumLayers-1]; //select the one at the bottom (just moved to)
        }

        public void MoveItem(int direction)
        {
            if (treeView1.SelectedNode == null || axMap1.NumLayers == 0) return;

            int newPosition = treeView1.SelectedNode.Index + direction;

            if (newPosition < 0 || newPosition >= axMap1.NumLayers) return;


            TreeNode N = treeView1.SelectedNode;
            treeView1.SelectedNode.Remove();
           treeView1.Nodes.Insert(newPosition,N);
            //treeView1.SelectedNode = null;
            treeView1.HideSelection = true;

        }

        private void symbologyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int handle = getLayerHandle();
            SetBasicSymbology(handle);
            Shapefile sf = new Shapefile();
            sf = axMap1.get_Shapefile(handle);

            Color c = new Color(); 
            if (colorDialog1.ShowDialog() != DialogResult.Cancel)
            {
                c = colorDialog1.Color;
                //UInt32 color = (uint)(((c.A << 24) | (c.R << 16) | (c.G << 8) | c.B) & 0xffffffffL);            
            }

            if (sf.ShapefileType == ShpfileType.SHP_POLYLINE)
            {
                axMap1.set_ShapeLayerLineColor(handle, (UInt32)System.Drawing.ColorTranslator.ToOle(c));
                float lineWidth = axMap1.get_ShapeLayerLineWidth(handle);
                lineWidth = Int32.Parse(Interaction.InputBox("Enter a line size between 1 and 10:", "Give a line size", lineWidth.ToString(), 200, 300));
                axMap1.set_ShapeLayerLineWidth(handle, lineWidth);
            }
            else if (sf.ShapefileType == ShpfileType.SHP_POLYGON)
            {
                //SetBasicSymbology(handle);
                axMap1.set_ShapeLayerFillColor(handle, (UInt32)System.Drawing.ColorTranslator.ToOle(c));
                float lineWidth = Int32.Parse(Interaction.InputBox("Enter a line size between 5 and 40:", "Give a line size", "3", 200, 300));
                float transparencyRate = float.Parse(Interaction.InputBox("Enter a value between 0 and 1:", "Set transparency percentage", "0.5", 100, 150));
                axMap1.set_ShapeLayerFillTransparency(handle, transparencyRate);
                axMap1.set_ShapeLayerLineWidth(handle, lineWidth);
            }

            else if (sf.ShapefileType == ShpfileType.SHP_POINT)
            {
                axMap1.set_ShapeLayerPointColor(handle, (UInt32)System.Drawing.ColorTranslator.ToOle(c));
                float pointSize = axMap1.get_ShapeLayerPointSize(handle);
                pointSize = Int32.Parse(Interaction.InputBox("Enter a line size between 5 and 40:", "Give a line size", pointSize.ToString(), 200, 300));
                axMap1.set_ShapeLayerPointSize(handle, pointSize);
            }
        }
        public void SetBasicSymbology(int intHandler)
        {
          
            //Set Filling color of the polygon shapefile
            axMap1.set_ShapeLayerFillColor(intHandler,
            (UInt32)(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Honeydew)));
            //Set the outline color of the polygon features
            axMap1.set_ShapeLayerLineColor(intHandler,
            (UInt32)(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Goldenrod)));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //foreach (DataGridViewColumn column in dataGridView1.Columns)
            //{

            //    MessageBox.Show(column.HeaderText);

            //    //String.Concat("Column ",column.Index.ToString());
            //}

        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int intHandler = getLayerHandle(); 
            Shapefile sf = new Shapefile();
            sf = axMap1.get_Shapefile(intHandler);
            //DataGridViewColumn c = dataGridView1.Columns(e.ColumnIndex);
            string columnName = dataGridView1.Columns[e.ColumnIndex].HeaderText;
            string columnType = dataGridView1.Columns[e.ColumnIndex].ValueType.ToString();
            int myFieldIndex = e.ColumnIndex;
            //MessageBox.Show(columnName + "," + columnType);
            //MapWinGIS.Table myTable = new MapWinGIS.Table();
            //myTable.Open(@"D:\GISSampleData2\arabcntry.dbf", null);
            //Table myTable = (Table)dataGridView1;
            //int numberOfRows = myTable.NumRows; //dataGridView1.RowCount;

            ////Create an array to store the cell values of the field
            //double[] myCellsValues = new double[numberOfRows];
            ////Populate the array with cell values restored from the Table instance
            //for (int i = 0; i < numberOfRows - 1; i++)
            //{
            //    myCellsValues[i] = Convert.ToDouble(myTable.get_CellValue(1, i));
            //}
            ////Get the minimum and maximum values
            //double minValue = myCellsValues.Min();
            //double maxValue = myCellsValues.Max();

            sf.DefaultDrawingOptions.FillType = tkFillType.ftStandard;
            sf.Categories.Generate(myFieldIndex, tkClassificationType.ctUniqueValues, 0);
            sf.Categories.ApplyExpressions();

            ColorScheme scheme = new ColorScheme();
            scheme.SetColors2(tkMapColor.Blue, tkMapColor.Red);

            sf.Categories.ApplyColorScheme(tkColorSchemeType.ctSchemeGraduated, scheme);
            axMap1.Redraw();
            //ShapefileCategories myScheme = new ShapefileCategories();
            //myScheme.Shapefile = axMap1.get_Shapefile(intHandler);
            //myScheme.AddRange(myFieldIndex, tkClassificationType.ctNaturalBreaks, 5, minValue, maxValue);
            //Set the field index to use in symbology

            ////Create a new instance for MapWinGIS.ShapefileColorBreak 
            //MapWinGIS.ShapefileColorBreak myBreak = new MapWinGIS.ShapefileColorBreak();
            ////Set the minimum value in the field as a start value
            //myBreak.StartValue = minValue;
            ////Set the start color of the scheme
            //myBreak.StartColor =
            //    (UInt32)(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Aqua));
            ////Set the maximum value in the field as an end value
            //myBreak.EndValue = maxValue;
            ////Set the end color of the scheme
            //myBreak.EndColor =
            //    (UInt32)(System.Drawing.ColorTranslator.ToOle
            //    (System.Drawing.Color.DarkBlue));
            ////Add the break to the color scheme
            //myScheme.Add(myBreak);
            ////Upgrade display using the scheme
            //axMap1.ApplyLegendColors(myScheme);
        }

        private void selectByDistanceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selectByLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSelectByLocation fm = Application
              .OpenForms
              .OfType<frmSelectByLocation>()
              .LastOrDefault();

            if (null == fm)
            {
                // fm = new frmSelectByAttributes(sf);
                fm = new frmSelectByLocation();
                fm.chkLstBox.Items.Clear();
                fm.cmbSourceLayer.Items.Clear();
                //fm.cmbSourceLayer.Items.Insert(0, "Please select a layer below");
                foreach (TreeNode N in treeView1.Nodes)
                {                   
                    fm.chkLstBox.Items.Add(N.Text);
                    fm.cmbSourceLayer.Items.Add(N.Text);
                }
                fm.cmbSourceLayer.SelectedIndex = 0;
                fm.cmbSourceLayer.SelectedIndex = 0;
                fm.cmbSpatialMethod.SelectedIndex = 0;
                fm.cmbMethod.SelectedIndex = 0;
                fm.Show();
                fm.BringToFront();
            }
            else
            { // ...Yes. We have to activate it (i.e. bring to front, restore if minimized, focus)
                fm.Activate();
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int layerHandle = getLayerHandle();
            frmLayerProperties fm = Application
              .OpenForms
              .OfType<frmLayerProperties>()
              .LastOrDefault();

            if (null == fm)
            {
                // fm = new frmSelectByAttributes(sf);
                fm = new frmLayerProperties();
                fm.lblLayerDescription.Text = axMap1.get_LayerDescription(layerHandle);
                fm.lblLayerSource.Text = axMap1.get_LayerFilename(layerHandle);
                fm.lblMaxScale.Text = axMap1.get_LayerMaxVisibleScale(layerHandle).ToString();
                fm.lblMinScale.Text = axMap1.get_LayerMinVisibleScale(layerHandle).ToString();
                //fm.cmbSourceLayer.Items.Clear();
                //fm.cmbSourceLayer.Items.Insert(0, "Please select a layer below");
                //foreach (TreeNode N in treeView1.Nodes)
                //{
                //    fm.chkLstBox.Items.Add(N.Text);
                //    fm.cmbSourceLayer.Items.Add(N.Text);
                //}
                //fm.cmbSourceLayer.SelectedIndex = 0;
                //fm.cmbSourceLayer.SelectedIndex = 0;
                //fm.cmbSpatialMethod.SelectedIndex = 0;
                //fm.cmbMethod.SelectedIndex = 0;
                fm.Show();
                fm.BringToFront();
            }
            else
            { // ...Yes. We have to activate it (i.e. bring to front, restore if minimized, focus)
                fm.Activate();
            }
        }

        private void addCircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void btnShowAttributes_Click(object sender, EventArgs e)
        {
            ShowAttributes();
        }

        private void toolStripButtonGoTo_Click(object sender, EventArgs e)
        {

        }

        private void exportAllShapefilesInTOCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count > 0)
            {
                var folderBrowserDialog1 = new FolderBrowserDialog();
                folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
                folderBrowserDialog1.SelectedPath = @"C:\";
                //folderBrowserDialog1.SelectedPath = @"C:Work\data\test";
                // Show the FolderBrowserDialog.
                DialogResult result = folderBrowserDialog1.ShowDialog();
                string folderName = "";
                if (result == DialogResult.OK)
                {
                    folderName = folderBrowserDialog1.SelectedPath;          
                }

                for (int i = 0; i < treeView1.Nodes.Count; i++)
                {
                    string path = Path.GetFullPath(treeView1.Nodes[i].Text);
                   // var fileInfo = new FileInfo(treeView1.Nodes[i].Text);
                    //string nn = fileInfo.DirectoryName;
                    //if (path.EndsWith(".shp")) {                   
                    
                        string lyName = treeView1.Nodes[i].Text;
                        int lyHandle = layerControl[lyName];
                        Shapefile sf = axMap1.get_Shapefile(lyHandle);
                        //sf.Shape()
                        string f = folderName + "\\" + lyName + "_exported.shp";
                        sf.SaveAs(f, null);
                    //}
                    AddLayerToMap(f);                    
                }
            }
            else MessageBox.Show("No layers in map"); return;            
        }

        private void proToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void proToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GeoProjection proj = new GeoProjection();

            // EPSG code
            //proj.ImportFromEPSG(4326);  // WGS84
            proj.ImportFromEPSG(26915); //utm15N

           
            Shapefile sp = new Shapefile();
            Shapefile spProjected = new Shapefile();

            int handle = getLayerHandle();

            sp = axMap1.get_Shapefile(handle);

            int countProj = 0;
            spProjected = sp.Reproject(proj, ref countProj);

            DateTime now = DateTime.Now;

            string tString = now.ToString("yyyy'_'MM'_'dd'_'HH':'mm':'ss").Replace(":", "_").Replace(" ", "_");

            string fileName = @"D:\shp\reprojected"+tString+".shp";

            sp.SaveAs(fileName);
            AddLayerToMap(fileName);

        }
    }
}

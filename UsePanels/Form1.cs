using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapWinGIS;
using AxMapWinGIS;
using System.IO;

namespace UsePanels
{
    public partial class Form1 : Form
    {
        public static Dictionary<string, int> layerControl = new Dictionary<string, int>();
        public Form1()
        {
            InitializeComponent();
            axMap1.Projection = tkMapProjection.PROJECTION_WGS84;
            axMap1.KnownExtents = tkKnownExtents.keUSA;
            panel1.Hide();
        }

        private void openTableToolStripMenuItem_Click(object sender, EventArgs e)
        {

            panel1.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        private void openAttributeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shapefile shp = new Shapefile();
            //shp.Open(@"C:\Work\GIS\data\states.shp");
            int layerHandle = getLayerHandle();

            shp = axMap1.get_Shapefile(layerHandle);
            // GeoProjection proj = new GeoProjection();
            // proj.SetGoogleMercator();
            // shp.Reproject(proj, 1);
            //int handle = axMap1.AddLayer(shp, true);
            // shp.DefaultDrawingOptions.FillTransparency = 0;
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < shp.NumFields; i++)
            {
                dataGridView1.Columns.Add("ID", "ID");
                dataGridView1.Columns.Add(shp.Field[i].Name, shp.Field[i].Name);
            }

            for (int i = 0; i < shp.NumFields; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = i;

                for (int j = 1; j < shp.NumFields; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = shp.CellValue[j, i];
                }
            }
            panel1.Show();
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
                //GeoProjection proj = new GeoProjection();
                //proj.ReadFromFile(filename);
                //lblMapProjection.Text = proj.Name;
                //lblMapProjection.Text = axMap1.Projection.ToString();
                //***********************************************
                //axMap1.Projection = tkMapProjection
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
                    Console.WriteLine("An element with Key = " + fname + " already exists, adding a duplicate layer");
                    //layerControl.Add(fname + "_2", layerHandle);
                }

                ImageList myImageList = new ImageList();
                System.Drawing.Image myImage1 = System.Drawing.Image.FromFile(@"c:\work\gis\symbol\point.png");
                System.Drawing.Image myImage2 = System.Drawing.Image.FromFile(@"c:\work\gis\symbol\line.png");
                System.Drawing.Image myImage3 = System.Drawing.Image.FromFile(@"c:\work\gis\symbol\polygon.png");
                myImageList.Images.Add(myImage1);
                myImageList.Images.Add(myImage2);
                myImageList.Images.Add(myImage3);
                treeView1.ImageList = myImageList;

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
                    N.Checked = true;
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
                    N.Checked = true;
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
                    N.Checked = true;
                    N.SelectedImageIndex = N.ImageIndex;

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
            int handle = getLayerHandle();
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
    }
}

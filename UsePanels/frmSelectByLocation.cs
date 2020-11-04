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
using UsePanels;

namespace UsePanels
{
    public partial class frmSelectByLocation : Form
    {
        public frmSelectByLocation()
        {
            InitializeComponent();
        }

        private void lblSelectByLocationTitle_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public object getForm()
        {
            Form1 fm = Application
                 .OpenForms
                 .OfType<Form1>()
                 .LastOrDefault();
            if (fm == null)
            {
                fm = new Form1();
                fm.Show();
            }
            else fm.Activate();

            return fm;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Form1 fm = (Form1)getForm();
            //fm.axMap1.Projection = tkMapProjection.PROJECTION_WGS84;
            //if (this.cmbMethod.SelectedText== "select features from")
            //{
            if (chkLstBox.CheckedItems.Count==1)
                {
                    int targetLayerHandle = fm.layerControl[chkLstBox.CheckedItems[0].ToString()];
                    int sourceLayerHandle = fm.layerControl[cmbSourceLayer.SelectedItem.ToString()];
                    Shapefile sfTarget = new Shapefile();
                    Shapefile sfSource = new Shapefile();
                    sfTarget = fm.axMap1.get_Shapefile(targetLayerHandle);
                    sfSource = fm.axMap1.get_Shapefile(sourceLayerHandle);

                    ShapefileCategory ct = sfSource.Categories.Add("Parks");

                    // choose parks and make them green
                    //ct.Expression = "[Type] = \"Park\"";
                    var utils = new Utils();
                    ct.DrawingOptions.FillColor = utils.ColorByName(tkMapColor.Green);
                    sfSource.Categories.ApplyExpression(0);

                // hide the rest types of objects on the layer
                    sfSource.DefaultDrawingOptions.Visible = false;
                    double maxDistance = 0.1;

                bool editing = sfTarget.StartEditingShapes(true, null);
                    sfTarget.UseQTree = true;   // this will build a spatial index to speed up selection
                    for (int i = 0; i < sfSource.NumShapes; i++)
                    {
                        int index = sfSource.ShapeCategory[i];
                        if (index == 0)
                        {
                            object result = null;
                            Shape shp = sfSource.Shape[i];
                            if (sfTarget.SelectShapes(shp.Extents, maxDistance, SelectMode.INTERSECTION, ref result))
                            {
                                int[] shapes = result as int[];
                                if (shapes == null) return;
                                for (int j = 0; j < shapes.Length; j++)
                                {
                                    if (!sfSource.ShapeSelected[shapes[j]])
                                    {
                                        Shape shp2 = sfTarget.Shape[shapes[j]];
                                        double dist = shp.Distance(shp2);
                                        if (dist <= maxDistance)
                                            sfTarget.set_ShapeSelected(shapes[j], true);
                                    }
                                }
                            }
                        }
                    }

                } else MessageBox.Show("No Target Layer Checked or more than one checked");

           // }

        }
    }
}

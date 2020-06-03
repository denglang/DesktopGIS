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

namespace MapWinGIS_AE
{
    public partial class frmSelectByAttributes : Form
    {
        public delegate void SelectByAttributeHandler(Shapefile sf);
        public event SelectByAttributeHandler SelectByAttribute;

        Shapefile sf = new Shapefile();



        //loop through all buttons in the form and add the text to attribute query builder when clicked
        private void buttonSelectChangedHandler(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    c.Click += new EventHandler(buttonClicked);
                }
                else
                {
                    buttonSelectChangedHandler(c);
                }
            }
        }
      
        private void buttonClicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            List<string> btnNames = new List<string>() {"get Unique Values","Clear","Help","OK",
                "Apply", "Close","Verify"};
           
                if (!btnNames.Contains(btn.Text))
                {
                    rTxtQueryBuilder.Text += " "+btn.Text;
                }                      
        }
        //public frmSelectByAttributes(Shapefile sf1)
        public frmSelectByAttributes()
        {
            //sf = sf1;
            InitializeComponent();          
        }

        private void frmSelectByAttributes_Load(object sender, EventArgs e)
        {
            buttonSelectChangedHandler(this);
        }

        private void btnGetFieldValue_Click(object sender, EventArgs e)
        {          
            lstFieldValue.Items.Clear();      
            int fieldIndex = -1;
            if (lstFieldName.SelectedIndex >= 0)
            {
               // rTxtQueryBuilder.Text += "["+lstFieldName.SelectedItem.ToString()+"]";
                fieldIndex = sf.Table.FieldIndexByName[lstFieldName.SelectedItem.ToString()];
            }
            else
            {
                MessageBox.Show("Please select a field.");
                return;
            }

            for (int i = 0; i < sf.NumShapes; i++)
            {
                lstFieldValue.Items.Add(sf.get_CellValue(fieldIndex, i));
            }
        }

        private void rTxtQueryBuilder_TextChanged(object sender, EventArgs e)
        {
            //buttonSelectChangedHandler(this);
        }

        private void lstFieldValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFieldValue.SelectedItem is string)
            {
                string vl = "'" + lstFieldValue.SelectedItem + "'";
                vl = vl.Replace("'", "\"");
                rTxtQueryBuilder.Text += vl;
            }
            else rTxtQueryBuilder.Text += lstFieldValue.SelectedItem;
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rTxtQueryBuilder.Text = "";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Help File");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
            
            //sf.Labels.Generate("[IRI Averag]", tkLabelPositioning.lpCentroid, true);
            string error = "";
            object result = null;

            string query = rTxtQueryBuilder.Text;
            //string query = "[IRI Averag] < 343.0";
            //query = "[ACRES]< 400000 AND [ACRES] > 300000";
            MessageBox.Show(query);
            sf.SelectNone();
            if (sf.Table.Query(query, ref result, ref error))
            {
                int[] shapes = result as int[];
                if (shapes != null)
                {
                    for (int i = 0; i < shapes.Length; i++)
                    {
                        sf.set_ShapeSelected(shapes[i], true);
                    }
                }
                string layerName = cmbLayer.Text;               
                
              //  MessageBox.Show(Form1.layerControl.ToString());
                int layerHandle = -1;
                if (layerName != null) {
                    layerHandle = fm.layerControl[layerName];
                } else {
                    MessageBox.Show("Map Layer not found!");
                    layerHandle = 0; 
                   // return; 
                }
                //fm.Focus();                 
                //fm.axMap1.ClearDrawingLabels(;
                fm.axMap1.ZoomToSelected(layerHandle);
               
                fm.axMap1.Refresh();
                fm.axMap1.Redraw();
                MessageBox.Show("Objects selected: " + sf.NumSelected);
            }
            else
            {
                MessageBox.Show("No shapes agree with the condition.");
            }
        }

        private void cmbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string layerName = cmbLayer.SelectedItem.ToString();
            Form1 fm = (Form1)getForm();
            int layerHandle = fm.layerControl[layerName];
            sf = fm.axMap1.get_Shapefile(layerHandle);

            lstFieldName.Items.Clear();
            for (int i = 0; i < sf.NumFields; i++)
            {
                string it = sf.Table.Field[i].Name;
                lstFieldName.Items.Add(it);
            }          
            this.Show();
        }

        private void lstFieldName_SelectedIndexChanged(object sender, EventArgs e)
        {
            rTxtQueryBuilder.Text += "[" + lstFieldName.SelectedItem.ToString() + "]";
        }
    }
}

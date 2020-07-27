using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxMapWinGIS;
using MapWinGIS;
using System.Diagnostics;
using UsePanels;
//using static MapWinGIS_AE;

namespace MapWinGIS_AE
{
    public partial class frmColorCodeLine : Form
    {
        //public delegate void ColorCodeShapeHandler(Shapefile sf, double poor, double fair);
        //public event ColorCodeShapeHandler ColorCodeShape;

        //Shapefile sf = new Shapefile();


       public frmColorCodeLine()//ref Shapefile sf1)        
        {
            //sf = sf1; 
            InitializeComponent();
        }

        private void lstIRIValue_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void btnColorCode_Click(object sender, EventArgs e)
        {
            
            // MessageBox.Show("Hello");
            //Control ctl = btnColorCode.Parent;

            // Form fm1= Application.OpenForms["Form1"];
            // fm1.ShowDialog(this);

            //if (Int32.TryParse(txtFair.Text,out m))
            // {
            //     Program.intPoor = m;
            // } else Console.WriteLine("String could not be parsed.");
            // Program.intPoor = Int16.Parse(txtFair.Text);
            //  Program.intFair = Int16.Parse(txtGood.Text);
            double intPoor = Convert.ToDouble(txtFair.Text);
            double intFair = Convert.ToDouble(txtGood.Text);

            // Shapefile sf = new Shapefile();
            //need to pass shapefile from Form1 to here. 

            //sf.Open(this.Text);
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

            //ColorCodeShape?.Invoke(sf, intFair, intPoor);
            int handle = fm.getLayerHandle();
            Shapefile sf = fm.axMap1.get_Shapefile(handle);
            fm.ColorCodeShape(sf, intFair, intPoor);

            // this.Owner.Name;

            // Debug.Print(ctl.ToString());
            //  Debug.Print(this.ToString());
            //this.Close(); 
           // using (Form1 fm = new Form1()) {
           //     fm.axMap1.get_Shapefile()

            //    fm.ShowDialog();
            //    fm.ApplyButton_fromColorCodeLine();
            // fm.ColorCodeShape()
            //  MessageBox.Show(fm.checkedListBox1.SelectedIndex.ToString());
            // fm.ColorCodeShape();
           //   }
        }

        private void lblCopyFair_Click(object sender, EventArgs e)
        {
            txtFair.Text = lstIRIValue.SelectedItem.ToString();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            txtFair.Text = "";
        }

        private void label3_Click(object sender, EventArgs e)
        {
            txtGood.Text = lstIRIValue.SelectedItem.ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            txtGood.Text = "";
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}

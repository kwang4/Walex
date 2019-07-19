using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Walex
{
    
    public partial class Form1 : Form
    {
        List<int> ColorCount;
        int[,] bigData;
        int[,] Alloc;
        public Form1()
        {
            InitializeComponent();
            
            
        }

        private void HighColors()
        {
            int[,] compare = new int[2, 2] { { 1, 2 }, { -3, 4 } };//first array is biggest value and pos, second is 2nd biggest and pos
            for (int i =0;i<ColorCount.Count();i++)
            {
                if(ColorCount[i]>=compare[0,0])
                {
                    compare[1, 0] = compare[0, 0];compare[1, 1] = compare[0, 1];//medium = big
                    compare[0, 0] = ColorCount[i];compare[0, 1] = i;
                }
                else if(ColorCount[i]<compare[0,0] && ColorCount[i]>=compare[1,0])
                {
                    compare[1, 0] = ColorCount[i];compare[1, 1] = i;
                }
            }
            bigData = compare;
            panel2.Enabled = true;
            panel1.Enabled = false;
            switch (compare[0,1])
            {
                case 0: lblBig.Text = "Red";break;
                case 1: lblBig.Text = "Orange"; break;
                case 2: lblBig.Text = "Yellow"; break;
                case 3: lblBig.Text = "Blue"; break;
                case 4: lblBig.Text = "Green"; break;
            }
            switch (compare[1, 1])
            {
                case 0: lblMed.Text = "Red"; break;
                case 1: lblMed.Text = "Orange"; break;
                case 2: lblMed.Text = "Yellow"; break;
                case 3: lblMed.Text = "Blue"; break;
                case 4: lblMed.Text = "Green"; break;
            }

        }
        
        private void CalcFinal()
        {
            var dicList1 = new Dictionary<string, int>() { { "S", Alloc[0, 0] }, { "B", Alloc[0, 1] }, { "X", Alloc[0, 2] }, { "C", Alloc[0, 3] }, { "R", Alloc[0, 4] } }.ToList();
            var dicList2 = new Dictionary<string, int>() { { "S", Alloc[1, 0] }, { "B", Alloc[1, 1] }, { "X", Alloc[1, 2] }, { "C", Alloc[1, 3] }, { "R", Alloc[1, 4] } }.ToList();
            var addedList = dicList1.Concat(dicList2).GroupBy(pair => pair.Key, pair => pair.Value).ToDictionary(item => item.Key, item => item.Sum()).OrderByDescending(pair => pair.Value).ToList();
            //Above line combines the 2 lists, and adds the the ints with same key value
            switch (bigData[0, 0])
            {
                case 1:
                    txtFinal.Text = "Split chips evenly among all categories";//Due to rng this has a high probability of working
                    break;                                                    //Without so much code
                case 2:
                        if (addedList[1].Value >= 2)
                        {
                            txtFinal.Text = "Put 75%  of chips in \"" + addedList[0].Key + "\" and the rest in \"" + addedList[1].Key + "\"";
                        }
                        else
                        {
                            txtFinal.Text = "Put all the chips in \"" + addedList[0].Key + "\"";
                        }
                    break;
                default:
                    txtFinal.Text = "Put all chips in \"" + dicList1.OrderByDescending(pair => pair.Value).ToList()[0].Key + "\"";
                    break;
            }
        }
        

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
               ColorCount = new List<int>() { Int32.Parse(txtR.Text), Int32.Parse(txtO.Text), Int32.Parse(txtY.Text), Int32.Parse(txtB.Text), Int32.Parse(txtG.Text) };
                HighColors();


            } catch { MessageBox.Show("Data Error"); }

        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblTitle_MouseHover(object sender, EventArgs e)
        {
            lblTitle.Text = "Walex Idiot's Master Program";
        }

        private void lblTitle_MouseLeave(object sender, EventArgs e)
        {
            lblTitle.Text = "W.I.M.P";
        }

        private void btnSet2_Click(object sender, EventArgs e)
        {
            try
            {
                Alloc = new int[2, 5] { {Int32.Parse(txtS1.Text), Int32.Parse(txtB1.Text), Int32.Parse(txtX1.Text), Int32.Parse(txtC1.Text), Int32.Parse(txtR1.Text) }, 
                                               {Int32.Parse(txtS2.Text), Int32.Parse(txtB2.Text), Int32.Parse(txtX2.Text), Int32.Parse(txtC2.Text), Int32.Parse(txtR2.Text)}};
                //Top is biggest, Bottom is 2nd biggest
                btnCalc.Enabled = true;
            }
            catch { MessageBox.Show("Error", "Data Type Error"); }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            if(btnCalc.Text == "Calculate")
            {
                btnCalc.Text = "Reset";
                CalcFinal();
            }
            else
            {
                panel2.Enabled = false; panel1.Enabled = true;
                btnCalc.Enabled = false;
                btnCalc.Text = "Calculate";
                txtFinal.Text = "";
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}

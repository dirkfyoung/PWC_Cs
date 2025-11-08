using System.Windows.Forms;

namespace PWC_Cs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AppTableDisplay.ColumnCount=2;
            AppTableDisplay.Columns[0].Name = "Days";
            AppTableDisplay.Columns[0].Width = 70;
            AppTableDisplay.Columns[1].Name = "Amount (kg/ha)";
            AppTableDisplay.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            AppTableDisplay.Columns[1].Width = 60;

            var combo = new DataGridViewComboBoxColumn{ HeaderText = "Application Method",Width = 135};
            combo.Items.Add(Standard.Method1);
            combo.Items.Add(Standard.Method2);
            combo.Items.Add(Standard.Method3);
            combo.Items.Add(Standard.Method4);
            combo.Items.Add(Standard.Method5);
            combo.Items.Add(Standard.Method6);
            combo.Items.Add(Standard.Method7);

            AppTableDisplay.Columns.Add(combo);
            AppTableDisplay.Columns.Add("Depth", "Depth (cm)");
            AppTableDisplay.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            AppTableDisplay.Columns[3].Width = 42; ;
            AppTableDisplay.Columns.Add("Split", "Split");
            AppTableDisplay.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            AppTableDisplay.Columns[4].Width = 44;

            DataGridViewComboBoxColumn driftcombo = new DataGridViewComboBoxColumn
            {
                HeaderText = "Drift Type",
                DropDownWidth = 220,
                Width = 160,
                FlatStyle = FlatStyle.Flat,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
            };

            // Add items from Standard.sprayterm1 to sprayterm15
            driftcombo.Items.Add(Standard.SprayTerms[1]);
            driftcombo.Items.Add(Standard.SprayTerms[2]);
            driftcombo.Items.Add(Standard.SprayTerms[3]);
            driftcombo.Items.Add(Standard.SprayTerms[4]);
            driftcombo.Items.Add(Standard.SprayTerms[5]);
            driftcombo.Items.Add(Standard.SprayTerms[6]);
            driftcombo.Items.Add(Standard.SprayTerms[7]);
            driftcombo.Items.Add(Standard.SprayTerms[8]);
            driftcombo.Items.Add(Standard.SprayTerms[9]);
            driftcombo.Items.Add(Standard.SprayTerms[10]);
            driftcombo.Items.Add(Standard.SprayTerms[11]);
            driftcombo.Items.Add(Standard.SprayTerms[12]);
            driftcombo.Items.Add(Standard.SprayTerms[13]);
            driftcombo.Items.Add(Standard.SprayTerms[14]);
            driftcombo.Items.Add(Standard.SprayTerms[15]);
            // driftcombo.Items.Add(Standard.sprayterm16); // Optional

            AppTableDisplay.Columns.Add(driftcombo);



        }





        private void SaveInputFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SaveMainInputToTextFile(saveFileDialog1.FileName);
            }
        }

        private void RetrieveInputFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                RetrieveMainInputFromTextFile(openFileDialog1.FileName);
            }
        }


        public void RetrieveMainInputFromTextFile(string readfilename)
        {
            string[] lines = File.ReadAllLines(readfilename);
            string[] col;

            col = lines[0].Split(',');//Version
            col = lines[1].Split(',');
            col = lines[2].Split(',');
            col = lines[3].Split(',');
            col = lines[4].Split(',');
            col = lines[5].Split(',');
            col = lines[6].Split(',');

            col = lines[7].Split(','); //sorption
            sorption1.Text = col[0];
            sorption2.Text = col[1];
            sorption3.Text = col[2];

            col = lines[8].Split(",");
            Nexp1Reg1.Text = col[0];
            Nexp2Reg1.Text = col[1];
            Nexp3Reg1.Text = col[2];

            col = lines[9].Split(",");
            Kf1Reg2.Text = col[0];
            Kf2Reg2.Text = col[1];
            Kf3Reg2.Text = col[2];

            col = lines[10].Split(",");
            Nexp1Reg2.Text = col[0];
            Nexp2Reg2.Text = col[1];
            Nexp3Reg2.Text = col[2];

            col = lines[11].Split(",");
            MassTransferRegion2.Text = col[0];
            MassTransferRegion2Daughter.Text = col[1];
            MassTransferRegion2GrandDaughter.Text = col[2];

            col = lines[12].Split(",");
            FreundlichMinimumConc.Text = col[0];
            SubTimeSteps.Text = col[1];

            col = lines[13].Split(",");
            WaterColMetab1.Text = col[0];
            WaterColMetab2.Text = col[1];
            WaterColMetab3.Text = col[2];
            WaterMolarRatio1.Text = col[3];
            WaterMolarRatio2.Text = col[4];

            col = lines[14].Split(",");
            WaterColRef1.Text = col[0];
            WaterColRef2.Text = col[1];
            WaterColRef3.Text = col[2];

            col = lines[15].Split(",");
            BenthicMetab1.Text = col[0];
            BenthicMetab2.Text = col[1];
            BenthicMetab3.Text = col[2];
            BenthicMolarRatio1.Text = col[3];
            BenthicMolarRatio2.Text = col[4];

            col = lines[16].Split(",");
            BenthicRef1.Text = col[0];
            BenthicRef2.Text = col[1];
            BenthicRef3.Text = col[2];

            col = lines[17].Split(",");
            Photo1.Text = col[0];
            Photo2.Text = col[1];
            Photo3.Text = col[2];
            PhotoMolarRatio1.Text = col[3];
            PhotoMolarRatio2.Text = col[4];

            col = lines[18].Split(",");
            PhotoLat1.Text = col[0];
            PhotoLat2.Text = col[1];
            PhotoLat3.Text = col[2];

            col = lines[19].Split(",");
            Hydrolysis1.Text = col[0];
            Hydrolysis2.Text = col[1];
            Hydrolysis3.Text = col[2];
            HydroMolarRatio1.Text = col[3];
            HydroMolarRatio2.Text = col[4];

            col = lines[20].Split(",");
            SoilDegradation1.Text = col[0];
            SoilDegradation2.Text = col[1];
            SoilDegradation3.Text = col[2];
            SoilMolarRatio1.Text = col[3];
            SoilMolarRatio2.Text = col[4];

            col = lines[21].Split(",");
            SoilRef1.Text = col[0];
            SoilRef2.Text = col[1];
            SoilRef3.Text = col[2];

            col = lines[22].Split(",");
            FoliarDeg1.Text = col[0];
            FoliarDeg2.Text = col[1];
            FoliarDeg3.Text = col[2];
            FoliarMolarRatio1.Text = col[3];
            FoliarMolarRatio2.Text = col[4];

            col = lines[23].Split(",");
            FoliarWashoff1.Text = col[0];
            FoliarWashoff2.Text = col[1];
            FoliarWashoff3.Text = col[2];

            col = lines[24].Split(",");
            MWT1.Text = col[0];
            MWT2.Text = col[1];
            MWT3.Text = col[2];

            col = lines[25].Split(",");
            VaporPress1.Text = col[0];
            VaporPress2.Text = col[1];
            VaporPress3.Text = col[2];

            col = lines[26].Split(",");
            Sol1.Text = col[0];
            Sol2.Text = col[1];
            Sol3.Text = col[2];

            col = lines[27].Split(",");
            Henry1.Text = col[0];
            Henry2.Text = col[1];
            Henry3.Text = col[2];

            col = lines[28].Split(",");
            AirDiff1.Text = col[0];
            AirDiff2.Text = col[1];
            AirDiff3.Text = col[2];

            col = lines[29].Split(",");
            HeatHenry1.Text = col[0];
            HeatHenry2.Text = col[1];
            HeatHenry3.Text = col[2];

            col = lines[30].Split(",");
            Q10.Text = col[0];

            col = lines[31].Split(",");
            ConstantProfile.Checked = Convert.ToBoolean(col[0]);

            col = lines[32].Split(",");
            RampProfile.Checked = Convert.ToBoolean(col[0]);
            profileDepth1.Text = col[1];
            ProfileDepth2.Text = col[2];
            RampEndValue.Text = col[3];


            // ExponentialProfile.Checked, ExpParameter1.Text, ExpParameter2.Text




        }


        public void SaveMainInputToTextFile(string savefilename)
        {

            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter(savefilename);
            //Write a line of text
            sw.WriteLine("version info");
            sw.WriteLine("working directory");
            sw.WriteLine("family name");
            sw.WriteLine("scenario directory");
            sw.WriteLine("pfac");
            sw.WriteLine("options");
            sw.WriteLine("nchem");

            sw.WriteLine(sorption1.Text + "," + sorption2.Text + "," + sorption3.Text);
            sw.WriteLine(Nexp1Reg1.Text + "," + Nexp2Reg1.Text + "," + Nexp3Reg1.Text);
            sw.WriteLine(Kf1Reg2.Text + "," + Kf2Reg2.Text + "," + Kf3Reg2.Text);
            sw.WriteLine(Nexp1Reg2.Text + "," + Nexp2Reg2.Text + "," + Nexp3Reg2.Text);
            sw.WriteLine(MassTransferRegion2.Text + "," + MassTransferRegion2Daughter.Text + "," + MassTransferRegion2GrandDaughter.Text);
            sw.WriteLine(FreundlichMinimumConc.Text + "," + SubTimeSteps.Text);

            sw.WriteLine(WaterColMetab1.Text + "," + WaterColMetab2.Text + "," + WaterColMetab3.Text + "," + WaterMolarRatio1.Text + "," + WaterMolarRatio2.Text);
            sw.WriteLine(WaterColRef1.Text + "," + WaterColRef2.Text + "," + WaterColRef3.Text);

            sw.WriteLine(BenthicMetab1.Text + "," + BenthicMetab2.Text + "," + BenthicMetab3.Text + "," + BenthicMolarRatio1.Text + "," + BenthicMolarRatio2.Text);
            sw.WriteLine(BenthicRef1.Text + "," + BenthicRef2.Text + "," + BenthicRef3.Text);
            sw.WriteLine(Photo1.Text + "," + Photo2.Text + "," + Photo3.Text + "," + PhotoMolarRatio1.Text + "," + PhotoMolarRatio2.Text);
            sw.WriteLine(PhotoLat1.Text + "," + PhotoLat2.Text + "," + PhotoLat3.Text);
            sw.WriteLine(Hydrolysis1.Text + "," + Hydrolysis2.Text + "," + Hydrolysis3.Text + "," + HydroMolarRatio1.Text + "," + HydroMolarRatio2.Text);
            sw.WriteLine(SoilDegradation1.Text + "," + SoilDegradation2.Text + "," + SoilDegradation3.Text + "," + SoilMolarRatio1.Text + "," + SoilMolarRatio2.Text + "," + IsAllMedia.Checked);
            sw.WriteLine(SoilRef1.Text + "," + SoilRef2.Text + "," + SoilRef3.Text);
            sw.WriteLine(FoliarDeg1.Text + "," + FoliarDeg2.Text + "," + FoliarDeg3.Text + "," + FoliarMolarRatio1.Text + "," + FoliarMolarRatio2.Text);
            sw.WriteLine(FoliarWashoff1.Text + "," + FoliarWashoff2.Text + "," + FoliarWashoff3.Text);

            sw.WriteLine(MWT1.Text + "," + MWT2.Text + "," + MWT3.Text);
            sw.WriteLine(VaporPress1.Text + "," + VaporPress2.Text + "," + VaporPress3.Text);
            sw.WriteLine(Sol1.Text + "," + Sol2.Text + "," + Sol3.Text);
            sw.WriteLine(Henry1.Text + "," + Henry2.Text + "," + Henry3.Text);
            sw.WriteLine(AirDiff1.Text + "," + AirDiff2.Text + "," + AirDiff3.Text);
            sw.WriteLine(HeatHenry1.Text + "," + HeatHenry2.Text + "," + HeatHenry3.Text);
            sw.WriteLine(Q10.Text);

            sw.WriteLine(ConstantProfile.Checked);

            sw.WriteLine(RampProfile.Checked + "," + profileDepth1.Text + "," + ProfileDepth2.Text + "," + RampEndValue.Text);
            sw.WriteLine(ExponentialProfile.Checked + "," + ExpParameter1.Text + "," + ExpParameter2.Text);


            int NumberOfSchemes = 0;
            int actualRowsInAppTable = 0;
            SchemeDetails ApplicationTable = new SchemeDetails();






            //Write a second line of text
            sw.WriteLine("From the StreamWriter class");
            //Close the file
            sw.Close();


        }

    }
}

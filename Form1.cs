using System.Windows.Forms;
using System.Linq;


namespace PWC_Cs
{
    public partial class Form1 : Form
    {

        private readonly List<SchemeDetails> SchemeInfoList = new List<SchemeDetails>();

        public Form1()
        {
            InitializeComponent();
        }

        //**********************************************************************
        private void Form1_Load(object sender, EventArgs e)
        {
            AppTableDisplay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            AppTableDisplay.ColumnHeadersHeight = AppTableDisplay.ColumnHeadersHeight * 2;
            AppTableDisplay.ColumnCount = 2;
            AppTableDisplay.Columns[0].Name = "Days";
            AppTableDisplay.Columns[0].FillWeight = 10;

            AppTableDisplay.Columns[1].Name = "Amount (kg/ha)";
            AppTableDisplay.Columns[1].FillWeight = 15;

            var combo = new DataGridViewComboBoxColumn { HeaderText = "Application Method", FillWeight = 30 };
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
            AppTableDisplay.Columns[3].FillWeight = 10;

            AppTableDisplay.Columns.Add("Split", "Split");
            AppTableDisplay.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            AppTableDisplay.Columns[4].FillWeight = 10;

            DataGridViewComboBoxColumn driftcombo = new()
            {
                HeaderText = "Drift Type",
                DropDownWidth = 270,
                FillWeight = 50,
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
            AppTableDisplay.Columns.Add(driftcombo);

            AppTableDisplay.Columns.Add("Buffer", "Drift Buffer (ft)");
            AppTableDisplay.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            AppTableDisplay.Columns[6].FillWeight = 15;

            AppTableDisplay.Columns.Add("Periodicity", "Period (days)");
            AppTableDisplay.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            AppTableDisplay.Columns[7].FillWeight = 11;

            AppTableDisplay.Columns.Add("Lag", "Lag (days)");
            AppTableDisplay.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
            AppTableDisplay.Columns[8].FillWeight = 11;

            DataGridViewButtonColumn btnApp = new()
            {
                Text = "delete",
                HeaderText = "Delete",
                Name = "Delete",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Popup,
                DefaultCellStyle = { BackColor = Color.Orange }
            };

            AppTableDisplay.Columns.Add(btnApp);
            AppTableDisplay.Columns["Delete"].FillWeight = 12;













            //DataGridViewButtonColumn btnScheme = new()
            //{
            //    Text = "delete",
            //    HeaderText = "Delete",
            //    Name = "Delete",
            //    UseColumnTextForButtonValue = true,
            //    FlatStyle = FlatStyle.Popup,
            //    DefaultCellStyle = { BackColor = Color.Orange }
            //};


            //SchemeTableDisplay.Columns.Add(btnScheme);
            //SchemeTableDisplay.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //SchemeTableDisplay.Columns["Delete"].FillWeight = 20;

            //SchemeTableDisplay.CellValueChanged += SchemeTableDisplay_CellValueChanged;
            //SchemeTableDisplay.CurrentCellDirtyStateChanged += SchemeTableDisplay_CurrentCellDirtyStateChanged;

        }
        //**********************************************************************
        private void SaveInputFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SaveMainInputToTextFile(saveFileDialog1.FileName);
            }
        }
        //**********************************************************************
        private void RetrieveInputFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                RetrieveMainInputFromTextFile(openFileDialog1.FileName);
            }
        }
        //**********************************************************************
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
            IsAllMedia.Checked = Convert.ToBoolean(col[5]);

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

            col = lines[33].Split(",");
            ExponentialProfile.Checked = Convert.ToBoolean(col[0]);
            ExpParameter1.Text = col[1];
            ExpParameter2.Text = col[2];

            col = lines[34].Split(",");
            int NumberOfSchemes = Convert.ToInt16(col[0]);

            SchemeTableDisplay.Rows.Clear();


            //for (int i = 0; i < NumberOfSchemes; i++)
            //{
            //    var ApplicationTable = new SchemeDetails
            //    {
            //        Days = { },
            //        Amount = { },
            //        Method = { },
            //        Depth = { },
            //        Split = { },
            //        Drift = { },
            //        DriftBuffer = { },
            //        Periodicity = { },
            //        Lag = { },
            //        Scenarios = { }
            //    };

            //    col = lines[34].Split(",");







        }
        //**********************************************************************
        public void SaveMainInputToTextFile(string savefilename)
        {
            //Pass the filepath and filename to the StreamWriter Constructor
            using StreamWriter sw = new StreamWriter(savefilename);
            {

                //the following will be populated later with varibles. leave asis for now
                sw.WriteLine("PWC Version 4.0 C#");
                sw.WriteLine("working directory");
                sw.WriteLine("family name");
                sw.WriteLine("scenario directory");
                sw.WriteLine("pfac");
                sw.WriteLine("options");
                sw.WriteLine("nchem");

                //Chemical properties 
                sw.WriteLine(string.Join(",", sorption1.Text, sorption2.Text, sorption3.Text));
                sw.WriteLine(string.Join(",", Nexp1Reg1.Text, Nexp2Reg1.Text, Nexp3Reg1.Text));
                sw.WriteLine(string.Join(",", Kf1Reg2.Text, Kf2Reg2.Text, Kf3Reg2.Text));
                sw.WriteLine(string.Join(",", Nexp1Reg2.Text, Nexp2Reg2.Text, Nexp3Reg2.Text));
                sw.WriteLine(string.Join(",", MassTransferRegion2.Text, MassTransferRegion2Daughter.Text, MassTransferRegion2GrandDaughter.Text));
                sw.WriteLine(string.Join(",", FreundlichMinimumConc.Text, SubTimeSteps.Text));
                sw.WriteLine(string.Join(",", WaterColMetab1.Text, WaterColMetab2.Text, WaterColMetab3.Text, WaterMolarRatio1.Text, WaterMolarRatio2.Text));
                sw.WriteLine(string.Join(",", WaterColRef1.Text, WaterColRef2.Text, WaterColRef3.Text));
                sw.WriteLine(string.Join(",", BenthicMetab1.Text, BenthicMetab2.Text, BenthicMetab3.Text, BenthicMolarRatio1.Text, BenthicMolarRatio2.Text));
                sw.WriteLine(string.Join(",", BenthicRef1.Text, BenthicRef2.Text, BenthicRef3.Text));
                sw.WriteLine(string.Join(",", Photo1.Text, Photo2.Text, Photo3.Text, PhotoMolarRatio1.Text, PhotoMolarRatio2.Text));
                sw.WriteLine(string.Join(",", PhotoLat1.Text, PhotoLat2.Text, PhotoLat3.Text));
                sw.WriteLine(string.Join(",", Hydrolysis1.Text, Hydrolysis2.Text, Hydrolysis3.Text, HydroMolarRatio1.Text, HydroMolarRatio2.Text));
                sw.WriteLine(string.Join(",", SoilDegradation1.Text, SoilDegradation2.Text, SoilDegradation3.Text, SoilMolarRatio1.Text, SoilMolarRatio2.Text, IsAllMedia.Checked.ToString()));
                sw.WriteLine(string.Join(",", SoilRef1.Text, SoilRef2.Text, SoilRef3.Text));
                sw.WriteLine(string.Join(",", FoliarDeg1.Text, FoliarDeg2.Text, FoliarDeg3.Text, FoliarMolarRatio1.Text, FoliarMolarRatio2.Text));
                sw.WriteLine(string.Join(",", FoliarWashoff1.Text, FoliarWashoff2.Text, FoliarWashoff3.Text));
                sw.WriteLine(string.Join(",", MWT1.Text, MWT2.Text, MWT3.Text));
                sw.WriteLine(string.Join(",", VaporPress1.Text, VaporPress2.Text, VaporPress3.Text));
                sw.WriteLine(string.Join(",", Sol1.Text, Sol2.Text, Sol3.Text));
                sw.WriteLine(string.Join(",", Henry1.Text, Henry2.Text, Henry3.Text));
                sw.WriteLine(string.Join(",", AirDiff1.Text, AirDiff2.Text, AirDiff3.Text));
                sw.WriteLine(string.Join(",", HeatHenry1.Text, HeatHenry2.Text, HeatHenry3.Text));

                sw.WriteLine(Q10.Text);
                sw.WriteLine(ConstantProfile.Checked.ToString());
                sw.WriteLine(string.Join(",", RampProfile.Checked.ToString(), profileDepth1.Text, ProfileDepth2.Text, RampEndValue.Text));
                sw.WriteLine(string.Join(",", ExponentialProfile.Checked.ToString(), ExpParameter1.Text, ExpParameter2.Text));

                // *********************Process the schemes: Extract info from SchemeInfo **************************

                AppTableDisplay.CommitEdit(DataGridViewDataErrorContexts.Commit);  //commit the cell if cursor still on box
                SchemeTableDisplay.CommitEdit(DataGridViewDataErrorContexts.Commit); //commit the cell if cursor still on box
                RecordCheckedScheme(); //save the current scheme to SchemeInfoList in case it has been edited. 

                //This avoids relying on (rowcout-1), which can be error-prone if the grid is empty or the new row is disabled.
                int NumberOfSchemes = SchemeTableDisplay.Rows.Cast<DataGridViewRow>().Count(row => !row.IsNewRow);

                sw.WriteLine(NumberOfSchemes.ToString());  //Line 35


                for (int i = 0; i < NumberOfSchemes; i++)
                {
                    var cellValue = SchemeTableDisplay.Rows[i].Cells[2].Value?.ToString() ?? "";
                    sw.WriteLine($"{i + 1},{cellValue}");                 //scheme number and description  Line 36

                    int referencedate;
                    if (SchemeInfoList[i].AbsoluteRelative) referencedate = 0;
                    else if (SchemeInfoList[i].Emerge) referencedate = 1;
                    else if (SchemeInfoList[i].Maturity) referencedate = 2;
                    else if (SchemeInfoList[i].Removal) referencedate = 3;
                    else referencedate = 99;
                    sw.WriteLine(referencedate);

                    //Go through the apps
                    for (int j = 0; j < SchemeInfoList[i].Days.Count; j++)
                    {
                        sw.WriteLine(string.Join(",",
                            SchemeInfoList[i].Days[j],
                            SchemeInfoList[i].Amount[j],
                            SchemeInfoList[i].Method[j],
                            SchemeInfoList[i].Depth[j],
                            SchemeInfoList[i].Split[j],
                            SchemeInfoList[i].Drift[j],
                            SchemeInfoList[i].DriftBuffer[j],
                            SchemeInfoList[i].Periodicity[j],
                            SchemeInfoList[i].Lag[j]
                            ));
                    }

                    sw.WriteLine(string.Join(",", SchemeInfoList[i].UseApplicationWindow, SchemeInfoList[i].ApplicationWindowSpan, SchemeInfoList[i].ApplicationWindowStep   ));
                    sw.WriteLine(string.Join(",", SchemeInfoList[i].UseRainFast, SchemeInfoList[i].RainLimit, SchemeInfoList[i].IntolerableRainWindow, SchemeInfoList[i].OptimumApplicationWindow, SchemeInfoList[i].MinDaysBetweenApps));

                    sw.WriteLine(SchemeInfoList[i].Scenarios.Count);  //number of scenarios



                }

                //Write a second line of text
                sw.WriteLine("From the StreamWriter class");

            } // The using statement automatically closes the StreamWriter
        }

        private void SchemeTableDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;



namespace PWC_Cs
{
    public partial class Form1 : Form
    {
        public void RetrieveMainInputFromTextFile(string readfilename)
        {
            using StreamReader reader = new StreamReader(readfilename);
            string[] col;
            string line;

            line = reader.ReadLine()!;//Version
            line = reader.ReadLine()!;
            line = reader.ReadLine()!;
            line = reader.ReadLine()!;
            line = reader.ReadLine()!;
            line = reader.ReadLine()!;
            line = reader.ReadLine()!;

            col = reader.ReadLine()!.Split(',');
            sorption1.Text = col[0];
            sorption2.Text = col[1];
            sorption3.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            Nexp1Reg1.Text = col[0];
            Nexp2Reg1.Text = col[1];
            Nexp3Reg1.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            Kf1Reg2.Text = col[0];
            Kf2Reg2.Text = col[1];
            Kf3Reg2.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            Nexp1Reg2.Text = col[0];
            Nexp2Reg2.Text = col[1];
            Nexp3Reg2.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            MassTransferRegion2.Text = col[0];
            MassTransferRegion2Daughter.Text = col[1];
            MassTransferRegion2GrandDaughter.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            FreundlichMinimumConc.Text = col[0];
            SubTimeSteps.Text = col[1];

            col = reader.ReadLine()!.Split(',');
            WaterColMetab1.Text = col[0];
            WaterColMetab2.Text = col[1];
            WaterColMetab3.Text = col[2];
            WaterMolarRatio1.Text = col[3];
            WaterMolarRatio2.Text = col[4];

            col = reader.ReadLine()!.Split(',');
            WaterColRef1.Text = col[0];
            WaterColRef2.Text = col[1];
            WaterColRef3.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            BenthicMetab1.Text = col[0];
            BenthicMetab2.Text = col[1];
            BenthicMetab3.Text = col[2];
            BenthicMolarRatio1.Text = col[3];
            BenthicMolarRatio2.Text = col[4];

            col = reader.ReadLine()!.Split(',');
            BenthicRef1.Text = col[0];
            BenthicRef2.Text = col[1];
            BenthicRef3.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            Photo1.Text = col[0];
            Photo2.Text = col[1];
            Photo3.Text = col[2];
            PhotoMolarRatio1.Text = col[3];
            PhotoMolarRatio2.Text = col[4];

            col = reader.ReadLine()!.Split(',');
            PhotoLat1.Text = col[0];
            PhotoLat2.Text = col[1];
            PhotoLat3.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            Hydrolysis1.Text = col[0];
            Hydrolysis2.Text = col[1];
            Hydrolysis3.Text = col[2];
            HydroMolarRatio1.Text = col[3];
            HydroMolarRatio2.Text = col[4];

            col = reader.ReadLine()!.Split(',');
            SoilDegradation1.Text = col[0];
            SoilDegradation2.Text = col[1];
            SoilDegradation3.Text = col[2];
            SoilMolarRatio1.Text = col[3];
            SoilMolarRatio2.Text = col[4];
            IsAllMedia.Checked = Convert.ToBoolean(col[5]);

            col = reader.ReadLine()!.Split(',');
            SoilRef1.Text = col[0];
            SoilRef2.Text = col[1];
            SoilRef3.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            FoliarDeg1.Text = col[0];
            FoliarDeg2.Text = col[1];
            FoliarDeg3.Text = col[2];
            FoliarMolarRatio1.Text = col[3];
            FoliarMolarRatio2.Text = col[4];

            col = reader.ReadLine()!.Split(',');
            FoliarWashoff1.Text = col[0];
            FoliarWashoff2.Text = col[1];
            FoliarWashoff3.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            MWT1.Text = col[0];
            MWT2.Text = col[1];
            MWT3.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            VaporPress1.Text = col[0];
            VaporPress2.Text = col[1];
            VaporPress3.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            Sol1.Text = col[0];
            Sol2.Text = col[1];
            Sol3.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            Henry1.Text = col[0];
            Henry2.Text = col[1];
            Henry3.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            AirDiff1.Text = col[0];
            AirDiff2.Text = col[1];
            AirDiff3.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            HeatHenry1.Text = col[0];
            HeatHenry2.Text = col[1];
            HeatHenry3.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            Q10.Text = col[0];

            col = reader.ReadLine()!.Split(',');
            ConstantProfile.Checked = Convert.ToBoolean(col[0]);

            col = reader.ReadLine()!.Split(',');
            RampProfile.Checked = Convert.ToBoolean(col[0]);
            profileDepth1.Text = col[1];
            ProfileDepth2.Text = col[2];
            RampEndValue.Text = col[3];

            col = reader.ReadLine()!.Split(',');
            ExponentialProfile.Checked = Convert.ToBoolean(col[0]);
            ExpParameter1.Text = col[1];
            ExpParameter2.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            int NumberOfSchemes = Convert.ToInt16(col[0]);

            // Clear old SchemeInfoList and Load it with the schemes from input file
            SchemeInfoList.Clear();
            for (int i = 0; i < NumberOfSchemes; i++)
            {
                SchemeDetails SingleScheme = new SchemeDetails();

                line = reader.ReadLine()!;
                int firstQuote = line.IndexOf('"');
                int lastQuote = line.LastIndexOf('"');

                if (firstQuote >= 0 && lastQuote > firstQuote)
                {
                    SingleScheme.SchemeDescription = line[(firstQuote + 1)..lastQuote];
                }
                else
                {
                    SingleScheme.SchemeDescription = "";
                }

                // read the relative app reference 
                SingleScheme.AbsoluteDays = false;
                SingleScheme.Emerge = false;
                SingleScheme.Maturity = false;
                SingleScheme.Removal = false;

                col = reader.ReadLine()!.Split(',');
                int ff = Convert.ToInt32(col[0]);

                SingleScheme.AbsoluteDays = ff == 0;
                SingleScheme.Emerge = ff == 1;
                SingleScheme.Maturity = ff == 2;
                SingleScheme.Removal = ff == 3;

                col = reader.ReadLine()!.Split(',');
                int NumberOfApps = Convert.ToInt32(col[0]);

                for (int j = 0; j < NumberOfApps; j++)
                {
                    line = reader.ReadLine()!;
                    if (line == null) break; // stop if file ended early

                    col = line.Split(',');

                    SingleScheme.Days.Add(col.Length > 0 ? col[0].Trim() : "");
                    SingleScheme.Amount.Add(col.Length > 1 ? col[1].Trim() : "");
                    SingleScheme.Method.Add(col.Length > 2 ? col[2].Trim() : "");
                    SingleScheme.Depth.Add(col.Length > 3 ? col[3].Trim() : "");
                    SingleScheme.Split.Add(col.Length > 4 ? col[4].Trim() : "");
                    SingleScheme.Drift.Add(col.Length > 5 ? col[5].Trim() : "");
                    SingleScheme.DriftBuffer.Add(col.Length > 6 ? col[6].Trim() : "");
                    SingleScheme.Periodicity.Add(col.Length > 7 ? col[7].Trim() : "");
                    SingleScheme.Lag.Add(col.Length > 8 ? col[8].Trim() : "");
                }

                col = reader.ReadLine()!.Split(',');                 //Application Window
                SingleScheme.UseApplicationWindow = Convert.ToBoolean(col[0]);
                SingleScheme.ApplicationWindowSpan = col[1];
                SingleScheme.ApplicationWindowStep = col[2];

                col = reader.ReadLine()!.Split(',');                  //Rain Restrictions
                SingleScheme.UseRainFast = Convert.ToBoolean(col[0]);
                SingleScheme.RainLimit = col[1];
                SingleScheme.IntolerableRainWindow = col[2];
                SingleScheme.OptimumApplicationWindow = col[3];
                SingleScheme.MinDaysBetweenApps = col[4];

                col = reader.ReadLine()!.Split(',');                  //Number of scenarios
                int numScenarios = Convert.ToInt32(col[0]);

                for (int s = 0; s < numScenarios; s++)
                {
                    line = reader.ReadLine()!;
                    if (line == null) break; // stop if file ended early
                    SingleScheme.Scenarios.Add(line);
                }
                col = reader.ReadLine()!.Split(',');
                SingleScheme.UseBatchScenarioFile = Convert.ToBoolean(col[0]);
                col = reader.ReadLine()!.Split(',');
                SingleScheme.ScenarioBatchFileName = col[0];


                line = reader.ReadLine()!;  // Mitigations
                if (line != null && !line.StartsWith("Mitigations"))
                {
                    // No mitigations provided, set defaults
                    SingleScheme.RunoffMitigation = "1.0";
                    SingleScheme.ErosionMitigation = "1.0";
                    SingleScheme.DriftMitigation = "1.0";
                }
                else
                {
                    line = reader.ReadLine()!;
                    col = line!.Split(',');
                    SingleScheme.RunoffMitigation = col[0];
                    SingleScheme.ErosionMitigation = col[1];
                    SingleScheme.DriftMitigation = col[2];

                  
                }
                SchemeInfoList.Add(SingleScheme);
            }  //end of schemes read

            col = reader.ReadLine()!.Split(',');  //erosion model
            ErosionFlag.Text = col[0];

            reader.ReadLine();  //blank lines for future
            reader.ReadLine();
            reader.ReadLine();
            reader.ReadLine();
            reader.ReadLine();

            col = reader.ReadLine()!.Split(',');  //CN moisture adjustment
            AdjustCN.Checked = Convert.ToBoolean(col[0]);

            //Waterbody Inputs
            col = reader.ReadLine()!.Split(',');  //Waterbody logicals
            ItsaPond.Checked = Convert.ToBoolean(col[0]);
            ItsaReservoir.Checked = Convert.ToBoolean(col[1]);
            ItsOther.Checked = Convert.ToBoolean(col[2]);
            ItsTPEZWPEZ.Checked = Convert.ToBoolean(col[3]);
            UseTPEZbuffers.Checked = Convert.ToBoolean(col[4]);

            col = reader.ReadLine()!.Split(',');  //Waterbody others count
            int countOfSpecialWaterbodies = Convert.ToInt32(col[0]);

            WaterbodyList.Items.Clear();
            for (int i = 0; i < countOfSpecialWaterbodies; i++)
            {
                string? raw = reader.ReadLine();
                if (string.IsNullOrEmpty(raw)) continue;

                var fields = raw.Split(',');
                string name = fields.Length > 0 ? fields[0].Trim().Trim('"') : string.Empty;
                if (!string.IsNullOrEmpty(name))
                    WaterbodyList.Items.Add(name);
            }


            //Read output options

            col = reader.ReadLine()!.Split(',');
            outputRunoff.Checked = Convert.ToBoolean(col[0]);

            col = reader.ReadLine()!.Split(',');
            outputErosion.Checked = Convert.ToBoolean(col[0]);

            col = reader.ReadLine()!.Split(',');
            outputPestRunoff.Checked = Convert.ToBoolean(col[0]);

            col = reader.ReadLine()!.Split(',');
            outputPestErosion.Checked = Convert.ToBoolean(col[0]);  

            col = reader.ReadLine()!.Split(',');
            outputConcLastLayer.Checked = Convert.ToBoolean(col[0]);

            col = reader.ReadLine()!.Split(',');
            outputDailyFieldVolatilization.Checked = Convert.ToBoolean(col[0]);

            col = reader.ReadLine()!.Split(',');
            outputDailyPestLeached.Checked = Convert.ToBoolean(col[0]);
            chemInfiltrationDepth.Text = col[1];

            col = reader.ReadLine()!.Split(',');
            outputDecayedPest.Checked = Convert.ToBoolean(col[0]);
            outputDecayDepth1.Text = col[1];
            outputDecayDepth2.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            outputMassInSoilProfile.Checked = Convert.ToBoolean(col[0]);

            col = reader.ReadLine()!.Split(',');
            outputMassSoilSpecific.Checked = Convert.ToBoolean(col[0]);
            outputMassDepth1.Text = col[1];
            outputMassDepth2.Text = col[2];

            col = reader.ReadLine()!.Split(',');
            outputMassOnFoliage.Checked = Convert.ToBoolean(col[0]);

            col = reader.ReadLine()!.Split(',');
            outputPrecipitation.Checked = Convert.ToBoolean(col[0]);

            col = reader.ReadLine()!.Split(',');
            outputActualEvap.Checked = Convert.ToBoolean(col[0]); 
            
            col = reader.ReadLine()!.Split(',');
            outputTotalSoilWater.Checked = Convert.ToBoolean(col[0]);

            col = reader.ReadLine()!.Split(',');
            outputIrrigation.Checked = Convert.ToBoolean(col[0]);

            col = reader.ReadLine()!.Split(',');
            outputInfiltrationAtDepth.Checked = Convert.ToBoolean(col[0]);
            outputInfiltrationDepth.Text = col[1];

            col = reader.ReadLine()!.Split(',');
            outputInfiltratedWaterLastLayer.Checked = Convert.ToBoolean(col[0]);

            col = reader.ReadLine()!.Split(',');
            outputWaterConc.Checked = Convert.ToBoolean(col[0]);

            bool isChecked;
            col = reader.ReadLine()!.Split(',');
            bool.TryParse(col[0].ToString(), out isChecked);
            outputSpraydrift.Checked = isChecked;

            col = reader.ReadLine()!.Split(',');
            bool.TryParse(col[0].ToString(), out isChecked);
            output_GW_BTC.Checked = isChecked;

            reader.ReadLine();  //blank lines for future
            reader.ReadLine();
            reader.ReadLine();
            reader.ReadLine();

            col = reader.ReadLine()!.Split(',');
            bool.TryParse(col[0].ToString(), out isChecked);
            CalculateEoF.Checked = isChecked;


            int NumberOfVintagePRZMrows;
            col = reader.ReadLine()!.Split(',');
            NumberOfVintagePRZMrows = Convert.ToInt32(col[0]);

            for (int i = 0; i < NumberOfVintagePRZMrows; i++)
            {
                col = reader.ReadLine()!.Split(',');
                AdditionalOutputGridView.Rows.Add(col[0], col[1], col[2], col[3], col[4], col[5]);
            }





            //Load Scheme Descriptions to Scheme Table Display
            SchemeTableDisplay.Rows.Clear();
            for (int i = 0; i < NumberOfSchemes; i++) 
            { 
                SchemeTableDisplay.Rows.Add();
                SchemeTableDisplay.Rows[i].Cells[2].Value = SchemeInfoList[i].SchemeDescription;
            }

        }


        //**********************************************************************
        public void SaveMainInputToTextFile(string savefilename)
        {
            // place near top of method

            static string Safe(object? o) => o?.ToString()?.Trim() ?? string.Empty;


            void WriteCsvLine(StreamWriter writer, params object?[] parts)
            {
                //Trailing comma to indicate end of line, especially for Fortran compatibility
                writer.WriteLine(string.Join(",", parts.Select(Safe)) + ",");
            }


            //Pass the filepath and filename to the StreamWriter Constructor
            using StreamWriter sw = new StreamWriter(savefilename);
            {
                //the following will be populated later with varibles. leave asis for now
                WriteCsvLine(sw, "PWC Version 4.0 C#");
                WriteCsvLine(sw, "WorkingDirectoryLabel.Text");
                WriteCsvLine(sw, "IOFamilyName.Text");
                WriteCsvLine(sw, WeatherFileDirectory);
                WriteCsvLine(sw, WaterbodyEvapAdjustment.Text);
                WriteCsvLine(sw, isKoc.Checked, UseFreundlich.Checked, UseNonequilibrium.Checked, poundToKiloConversion.Checked, IsHydrolysisOverride.Checked);

                string nchem = DoDegradate1.Checked? (DoDegradate2.Checked ? "3" : "2"): "1";
                WriteCsvLine(sw, nchem);

                //Chemical properties 
                WriteCsvLine(sw, sorption1.Text, sorption2.Text, sorption3.Text);
                WriteCsvLine(sw, Nexp1Reg1.Text, Nexp2Reg1.Text, Nexp3Reg1.Text);
                WriteCsvLine(sw, Kf1Reg2.Text, Kf2Reg2.Text, Kf3Reg2.Text);
                WriteCsvLine(sw, Nexp1Reg2.Text, Nexp2Reg2.Text, Nexp3Reg2.Text);
                WriteCsvLine(sw, MassTransferRegion2.Text, MassTransferRegion2Daughter.Text, MassTransferRegion2GrandDaughter.Text);
                WriteCsvLine(sw, FreundlichMinimumConc.Text, SubTimeSteps.Text);
                WriteCsvLine(sw, WaterColMetab1.Text, WaterColMetab2.Text, WaterColMetab3.Text, WaterMolarRatio1.Text, WaterMolarRatio2.Text);
                WriteCsvLine(sw, WaterColRef1.Text, WaterColRef2.Text, WaterColRef3.Text);
                WriteCsvLine(sw, BenthicMetab1.Text, BenthicMetab2.Text, BenthicMetab3.Text, BenthicMolarRatio1.Text, BenthicMolarRatio2.Text);
                WriteCsvLine(sw, BenthicRef1.Text, BenthicRef2.Text, BenthicRef3.Text);
                WriteCsvLine(sw, Photo1.Text, Photo2.Text, Photo3.Text, PhotoMolarRatio1.Text, PhotoMolarRatio2.Text);
                WriteCsvLine(sw, PhotoLat1.Text, PhotoLat2.Text, PhotoLat3.Text);
                WriteCsvLine(sw, Hydrolysis1.Text, Hydrolysis2.Text, Hydrolysis3.Text, HydroMolarRatio1.Text, HydroMolarRatio2.Text);
                WriteCsvLine(sw, SoilDegradation1.Text, SoilDegradation2.Text, SoilDegradation3.Text, SoilMolarRatio1.Text, SoilMolarRatio2.Text, IsAllMedia.Checked);
                WriteCsvLine(sw, SoilRef1.Text, SoilRef2.Text, SoilRef3.Text);
                WriteCsvLine(sw, FoliarDeg1.Text, FoliarDeg2.Text, FoliarDeg3.Text, FoliarMolarRatio1.Text, FoliarMolarRatio2.Text);
                WriteCsvLine(sw, FoliarWashoff1.Text, FoliarWashoff2.Text, FoliarWashoff3.Text);
                WriteCsvLine(sw, MWT1.Text, MWT2.Text, MWT3.Text);
                WriteCsvLine(sw, VaporPress1.Text, VaporPress2.Text, VaporPress3.Text);
                WriteCsvLine(sw, Sol1.Text, Sol2.Text, Sol3.Text);
                WriteCsvLine(sw, Henry1.Text, Henry2.Text, Henry3.Text);
                WriteCsvLine(sw, AirDiff1.Text, AirDiff2.Text, AirDiff3.Text);
                WriteCsvLine(sw, HeatHenry1.Text, HeatHenry2.Text, HeatHenry3.Text);

                WriteCsvLine(sw, Q10.Text);
                WriteCsvLine(sw, ConstantProfile.Checked);
                WriteCsvLine(sw, RampProfile.Checked.ToString(), profileDepth1.Text, ProfileDepth2.Text, RampEndValue.Text);
                WriteCsvLine(sw, ExponentialProfile.Checked.ToString(), ExpParameter1.Text, ExpParameter2.Text);

                // *********************Process the schemes: Extract info from SchemeInfo **************************

                AppTableDisplay.CommitEdit(DataGridViewDataErrorContexts.Commit);  //commit the cell if cursor still on box
                SchemeTableDisplay.CommitEdit(DataGridViewDataErrorContexts.Commit); //commit the cell if cursor still on box
                RecordCheckedScheme(); //save the current scheme to SchemeInfoList in case it has been edited. 

                //This avoids relying on (rowcout-1), which can be error-prone if the grid is empty or the new row is disabled.
                int NumberOfSchemes = SchemeTableDisplay.Rows.Cast<DataGridViewRow>().Count(row => !row.IsNewRow);

                WriteCsvLine(sw, NumberOfSchemes, " ***** Schemes Start Here ******");  //Line 35


                for (int i = 0; i < NumberOfSchemes; i++)
                {
                    //var cellValue = SchemeTableDisplay.Rows[i].Cells[2].Value?.ToString() ?? "";
                    //sw.WriteLine($"{i + 1},{cellValue}");                 //scheme number and description  Line 36
                    string des = (string)SchemeTableDisplay.Rows[i].Cells[2].Value;

                    WriteCsvLine(sw, i + 1, '"' + des + '"');  //put quotes around description

                    int referencedate;
                    if (SchemeInfoList[i].AbsoluteDays) referencedate = 0;
                    else if (SchemeInfoList[i].Emerge) referencedate = 1;
                    else if (SchemeInfoList[i].Maturity) referencedate = 2;
                    else if (SchemeInfoList[i].Removal) referencedate = 3;
                    else referencedate = 99;
                    WriteCsvLine(sw, referencedate);


                    WriteCsvLine(sw, SchemeInfoList[i].Days.Count);

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

                    WriteCsvLine(sw, SchemeInfoList[i].UseApplicationWindow, SchemeInfoList[i].ApplicationWindowSpan, SchemeInfoList[i].ApplicationWindowStep);
                    WriteCsvLine(sw, SchemeInfoList[i].UseRainFast, SchemeInfoList[i].RainLimit, SchemeInfoList[i].IntolerableRainWindow, SchemeInfoList[i].OptimumApplicationWindow, SchemeInfoList[i].MinDaysBetweenApps);
                    WriteCsvLine(sw, SchemeInfoList[i].Scenarios.Count);  //number of scenarios

                    for (int j = 0; j < SchemeInfoList[i].Scenarios.Count; j++)
                    {
                        WriteCsvLine(sw, SchemeInfoList[i].Scenarios[j]);
                    }

                    WriteCsvLine(sw, SchemeInfoList[i].UseBatchScenarioFile);
                    WriteCsvLine(sw, SchemeInfoList[i].ScenarioBatchFileName);
                    WriteCsvLine(sw, "Mitigations (flag to make older versions still readable)");
                    WriteCsvLine(sw, SchemeInfoList[i].RunoffMitigation, SchemeInfoList[i].ErosionMitigation, SchemeInfoList[i].DriftMitigation);
                }

                WriteCsvLine(sw, ErosionFlag.Text);
                sw.WriteLine(',');
                sw.WriteLine(',');
                sw.WriteLine(',');
                sw.WriteLine(',');
                sw.WriteLine(',');
                WriteCsvLine(sw, AdjustCN.Checked);
                WriteCsvLine(sw, ItsaPond.Checked, ItsaReservoir.Checked, ItsOther.Checked, ItsTPEZWPEZ.Checked, UseTPEZbuffers.Checked);
                WriteCsvLine(sw, WaterbodyList.Items.Count);

                foreach (var item in WaterbodyList.Items)
                {
                    WriteCsvLine(sw, item);
                }


                //*********** OUTPUT *****************************

                string SafeText(string? s) => (s ?? string.Empty).Trim();

                string CellValue(int row, int col) =>
                    AdditionalOutputGridView.Rows[row].Cells[col].Value?.ToString() is string v ? SafeText(v) : string.Empty;

                int numberAdditionalOutputs = Math.Max(0, AdditionalOutputGridView.Rows.Count - 1);


                // Simple flags (one-per-line)
                WriteCsvLine(sw, outputRunoff.Checked, "runoff output");
                WriteCsvLine(sw, outputErosion.Checked, "erosion output");
                WriteCsvLine(sw, outputPestRunoff.Checked, "chem runoff");
                WriteCsvLine(sw, outputPestErosion.Checked, "chem erosion");

                WriteCsvLine(sw, outputConcLastLayer.Checked, "last conc");
                WriteCsvLine(sw, outputDailyFieldVolatilization.Checked);

                WriteCsvLine(sw, outputDailyPestLeached.Checked, chemInfiltrationDepth.Text);
                WriteCsvLine(sw, outputDecayedPest.Checked, outputDecayDepth1.Text, outputDecayDepth2.Text);

                WriteCsvLine(sw, outputMassInSoilProfile.Checked);
                WriteCsvLine(sw, outputMassSoilSpecific.Checked, outputMassDepth1.Text, outputMassDepth2.Text);
                WriteCsvLine(sw, outputMassOnFoliage.Checked);

                WriteCsvLine(sw, outputPrecipitation.Checked);
                WriteCsvLine(sw, outputActualEvap.Checked);
                WriteCsvLine(sw, outputTotalSoilWater.Checked);
                WriteCsvLine(sw, outputIrrigation.Checked);

                WriteCsvLine(sw, outputInfiltrationAtDepth.Checked, outputInfiltrationDepth.Text);
                WriteCsvLine(sw, outputInfiltratedWaterLastLayer.Checked);

                WriteCsvLine(sw, outputWaterConc.Checked);
                WriteCsvLine(sw, outputSpraydrift.Checked);
                WriteCsvLine(sw, output_GW_BTC.Checked);

                // placeholders
                sw.WriteLine("holder for future expansion,");
                sw.WriteLine("holder for future expansion,");
                sw.WriteLine("holder for future expansion,");
                sw.WriteLine("holder for future expansion,");
                WriteCsvLine(sw, CalculateEoF.Checked);

                // Additional outputs count
                WriteCsvLine(sw, numberAdditionalOutputs);


                // Additional outputs rows
                for (int i = 0; i < numberAdditionalOutputs; i++)
                {
                    var parts = new[]
                    {
                       CellValue(i, 0),
                       CellValue(i, 1),
                       CellValue(i, 2),
                       CellValue(i, 3),
                       CellValue(i, 4),
                       CellValue(i, 5)
                    };
                    sw.WriteLine();
                    WriteCsvLine(sw, parts);
                }

                sw.WriteLine("done...........");

            } // The using statement automatically closes the StreamWriter
        }


    }
}

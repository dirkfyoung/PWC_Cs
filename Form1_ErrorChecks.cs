using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PWC_Cs.Core;

namespace PWC_Cs
{
    public partial class Form1 : Form
    {
        public bool ValidateInputs()
        {
            if(!ValidateChemicalInputs()) return false;
            if(!ValidateSchemeInputs()) return false;
            if(!ValidateOptionalOutput()) return false;
            if(!ValidateAdvanceInputs()) return false;
            return true;
        }
        private bool ValidateChemicalInputs()
        {
            // Required numeric fields
            if (!ValidateGroup([Sorption1, FoliarWashoff1, MWT1, VaporPress1, Sol1, Henry1]))
                return false;

            // Optional numeric fields: blank is allowed, but if filled it must be numeric
            if (!ValidateOptionalNumeric(WaterColMetab1)) return false;
            if (!ValidateOptionalNumeric(BenthicMetab1)) return false;
            if (!ValidateOptionalNumeric(Photo1)) return false;
            if (!ValidateOptionalNumeric(Hydrolysis1)) return false;
            if (!ValidateOptionalNumeric(SoilDegradation1)) return false;
            if (!ValidateOptionalNumeric(FoliarDeg1)) return false;
            if (!ValidateOptionalNumeric(AirDiff1)) return false;
            if (!ValidateOptionalNumeric(HeatHenry1)) return false;

            // References only required when the primary field has a value
            if (!ValidateReferences(WaterColMetab1, WaterColRef1)) return false;
            if (!ValidateReferences(BenthicMetab1, BenthicRef1)) return false;
            if (!ValidateReferences(Photo1, PhotoLat1)) return false;
            if (!ValidateReferences(SoilDegradation1, SoilRef1)) return false;

            if (DoDegradate1.Checked)
            {
                if (!ValidateGroup([WaterMolarRatio1, BenthicMolarRatio1, PhotoMolarRatio1, HydroMolarRatio1, SoilMolarRatio1, FoliarMolarRatio1]))
                    return false;

                // For daughter values, make the same fields optional where needed
                if (!ValidateGroup([Sorption2, FoliarWashoff2, MWT2, VaporPress2, Sol2, Henry2, AirDiff2, HeatHenry2]))
                    return false;

                if (!ValidateOptionalNumeric(WaterColMetab2)) return false;
                if (!ValidateOptionalNumeric(BenthicMetab2)) return false;
                if (!ValidateOptionalNumeric(Photo2)) return false;
                if (!ValidateOptionalNumeric(Hydrolysis2)) return false;
                if (!ValidateOptionalNumeric(SoilDegradation2)) return false;
                if (!ValidateOptionalNumeric(FoliarDeg2)) return false;

                if (!ValidateReferences(WaterColMetab2, WaterColRef2)) return false;
                if (!ValidateReferences(BenthicMetab2, BenthicRef2)) return false;
                if (!ValidateReferences(Photo2, PhotoLat2)) return false;
                if (!ValidateReferences(SoilDegradation2, SoilRef2)) return false;
            }

            if (DoDegradate2.Checked)
            {
                if (!ValidateGroup([WaterMolarRatio2, BenthicMolarRatio2, PhotoMolarRatio2, HydroMolarRatio2, SoilMolarRatio2, FoliarMolarRatio2]))
                    return false;

                if (!ValidateGroup([Sorption3, FoliarWashoff3, MWT3, VaporPress3, Sol3, Henry3, AirDiff3, HeatHenry3]))
                    return false;

                if (!ValidateOptionalNumeric(WaterColMetab3)) return false;
                if (!ValidateOptionalNumeric(BenthicMetab3)) return false;
                if (!ValidateOptionalNumeric(Photo3)) return false;
                if (!ValidateOptionalNumeric(Hydrolysis3)) return false;
                if (!ValidateOptionalNumeric(SoilDegradation3)) return false;
                if (!ValidateOptionalNumeric(FoliarDeg3)) return false;

                if (!ValidateReferences(WaterColMetab3, WaterColRef3)) return false;
                if (!ValidateReferences(BenthicMetab3, BenthicRef3)) return false;
                if (!ValidateReferences(Photo3, PhotoLat3)) return false;
                if (!ValidateReferences(SoilDegradation3, SoilRef3)) return false;
            }

            return true;
        }

        private bool ValidateSchemeInputs()
        {
            // Commit any pending edits in the DataGridView
            AppTableDisplay.CommitEdit(DataGridViewDataErrorContexts.Commit);
            SchemeTableDisplay.CommitEdit(DataGridViewDataErrorContexts.Commit);

            int numberOfSchemes = SchemeTableDisplay.RowCount - 1;

            for (int i = 0; i < numberOfSchemes; i++)
            {
                var applicationTable = SchemeInfoList[i];

                if (applicationTable.UseApplicationWindow)
                {
                    if (!HandleValidationResult(NumberValidator.TestActualIntegers(applicationTable.ApplicationWindowStep), $"Window step in scheme {i + 1}")) return false;
                    if (!HandleValidationResult(NumberValidator.TestActualIntegers(applicationTable.ApplicationWindowSpan), $"Window span in scheme {i + 1}")) return false;

                    if (int.TryParse(applicationTable.ApplicationWindowSpan, out int windowSpan) && windowSpan > 365)
                    {
                        ShowErrorMessage($"Application window span cannot be greater than 365, scheme {i + 1}");
                        return false;
                    }
                }

                // Application Table Information
                int actualRowsInAppTable = applicationTable.Days.Count;
                if (actualRowsInAppTable < 1)
                {
                    ShowErrorMessage($"There are no pesticide applications for scheme number {i + 1}");
                    return false;
                }

                string[] formats = { "MM/d/yyyy", "MM/dd/yyyy", "M/dd/yyyy", "M/d/yyyy", "M/d", "MM/d", "M/d", "M/dd" };

                for (int j = 0; j < actualRowsInAppTable; j++)
                {
                    if (applicationTable.AbsoluteDays)
                    {
                        if (!DateTime.TryParseExact(applicationTable.Days[j], formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                        {
                            ShowErrorMessage($"Absolute Application date is not in the right format for Scheme {i + 1}, Row {j + 1}");
                            return false;
                        }
                    }
                    else
                    {
                        if (!HandleValidationResult(NumberValidator.TestActualIntegers(applicationTable.Days[j]), $"for Scheme {i + 1}, Row {j + 1}")) return false;
                    }

                    if (!HandleValidationResult(NumberValidator.TestRealNumbers(applicationTable.Amount[j]), $"Application Amount for Scheme {i + 1}, Row {j + 1}")) return false;
                    if (!HandleValidationResult(NumberValidator.TestRealNumbers(applicationTable.Depth[j]), $"Application Depth for Scheme {i + 1}, Row {j + 1}")) return false;
                    if (!HandleValidationResult(NumberValidator.TestRealNumbers(applicationTable.Split[j]), $"Split Value for Scheme {i + 1}, Row {j + 1}")) return false;
                    if (!HandleValidationResult(NumberValidator.TestRealNumbers(applicationTable.Drift[j]), "Drift Value")) return false;
                    if (!HandleValidationResult(NumberValidator.TestRealNumbers(applicationTable.DriftBuffer[j]), "Buffer Distance")) return false;
                    if (!HandleValidationResult(NumberValidator.TestRealNumbers(applicationTable.Periodicity[j]), "Application Period")) return false;

                    if (double.TryParse(applicationTable.Periodicity[j], out double periodicity) && periodicity < 1)
                    {
                        ShowErrorMessage("Periodicity in Application Table must be 1 or greater");
                        return false;
                    }

                    if (!HandleValidationResult(NumberValidator.TestRealNumbers(applicationTable.Lag[j]), "Application Lag")) return false;
                }
            }

            return true;
        }
        private bool ValidateOptionalOutput()
        {

            if (outputDailyPestLeached.Checked)
            {
                if (!ValidateGroup([chemInfiltrationDepth])) return false;
            }
            if (outputDecayedPest.Checked)
            {
                if (!ValidateGroup([outputDecayDepth1, outputDecayDepth2])) return false; 
            }
            if (outputMassSoilSpecific.Checked)
            {
                if (!ValidateGroup([outputMassDepth1, outputMassDepth2])) return false;
            }
            if (outputInfiltrationAtDepth.Checked)
            {
                if (!ValidateGroup([ outputInfiltrationDepth])) return false;
            }

            // Check Optional Output Table
            AdditionalOutputGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            var ztsModes = new List<string> { "TSER", "TCUM", "TAVE", "TSUM" };

            for (int i = 0; i < AdditionalOutputGridView.RowCount - 1; i++)
            {
                if (!HandleValidationResult(NumberValidator.TestActualIntegers(AdditionalOutputGridView[1, i].Value.ToString()), $"Row {i + 1} in Optional Outputs Table")) return false;

                if (!DoDegradate1.Checked && Convert.ToInt32(AdditionalOutputGridView[1, i].Value) > 1)
                {
                    ShowErrorMessage($"Chemical form must be less than 2. Row {i + 1} in Optional Outputs Table. Degradate calculations were not selected on chemical tab.");
                    return false;
                }

                if (!DoDegradate2.Checked && Convert.ToInt32(AdditionalOutputGridView[1, i].Value) > 2)
                {
                    ShowErrorMessage($"Chemical form must be less than 3. Row {i + 1} in Optional Outputs Table. Grandaughter calculations were not selected on chemical tab.");
                    return false;
                }

                if (!ztsModes.Contains(AdditionalOutputGridView[2, i].Value.ToString()))
                {
                    ShowErrorMessage($"Mode selection can only be TSER, TAVE, TSUM, or TCUM. Row {i + 1} in Optional Outputs Table.");
                    return false;
                }

                if (!HandleValidationResult(NumberValidator.TestActualIntegers(AdditionalOutputGridView[3, i].Value.ToString()), $"Arg1 in Row {i + 1} in Optional Outputs Table")) return false;
                if (!HandleValidationResult(NumberValidator.TestActualIntegers(AdditionalOutputGridView[4, i].Value.ToString()), $"Arg2 in Row {i + 1} in Optional Outputs Table")) return false;
                if (!HandleValidationResult(NumberValidator.TestRealNumbers(AdditionalOutputGridView[5, i].Value.ToString()), $"Multiplier in Row {i + 1} in Optional Outputs Table")) return false;
            }
            return true;
        }
        private bool ValidateAdvanceInputs()
        {
            // Validate default linear isotherm requirements (daughter and granddaughter optional really)
           if( !ValidateGroup([SubTimeSteps, Nexp1Reg1, Nexp2Reg1, Nexp3Reg1, Q10, WaterbodyEvapAdjustment])) return false;

            if (UseFreundlich.Checked)
            {
                if (!ValidateGroup(new[] { FreundlichMinimumConc })) return false;
            }

            if (UseNonequilibrium.Checked)
            {
                if(!ValidateGroup([Kf1Reg2, Kf2Reg2, Kf3Reg2, Nexp1Reg2, Nexp2Reg2, Nexp3Reg2, MassTransferRegion2, MassTransferRegion2Daughter, MassTransferRegion2GrandDaughter])) return false;
            }

            if (ErosionFlag.Text.Trim() == "1" || ErosionFlag.Text.Trim() == "2" || ErosionFlag.Text.Trim() == "3")
            {
                ErosionFlag.BackColor = Color.White;
            }
            else 
            {  
                ErosionFlag.BackColor = Color.Orange;
                MessageBox.Show("Erosion flag must be 1, 2, or 3", "Input Error");
                return false;
            }

            if (RampProfile.Checked)
            {
                if (!ValidateGroup([ProfileDepth1, ProfileDepth2, RampEndValue])) return false;
            }

            if (ExponentialProfile.Checked)
            {
                if (!ValidateGroup([ExpParameter1, ExpParameter2])) return false;
            }

            if (RampProfile.Checked)
            {
                if(!ValidateGroup([ProfileDepth1, ProfileDepth2, RampEndValue])) return false;
                if(!ValidateReferences(WaterColMetab2, WaterColRef2)) return false;
                if (!ValidateReferences(BenthicMetab2, BenthicRef2)) return false;
                if (!ValidateReferences(Photo2, PhotoLat2)) return false;
                if (!ValidateReferences(SoilDegradation2, SoilRef2)) return false;
            }

 
            return true;
        }


        private static bool ValidateGroup(IEnumerable<TextBox> inputs, string? exception = null)
        {
            foreach (var input in inputs)
            {
                if (!ValidateInput(input, exception)) return false;
            }
            return true;
        }
        private static bool ValidateReferences(TextBox primary, TextBox reference)
        {
            if (!string.IsNullOrEmpty(primary.Text) && !ValidateInput(reference))
            {
                return false;
            }
            return true;
        }
        private static bool ValidateInput(TextBox input, string? exception = null)
        {
            var result = NumberValidator.TestRealNumbers(input.Text, exception);

            if (!result.IsValid)
            {
                input.BackColor = Color.Orange;
                MessageBox.Show($"{result.Message} in {input.Name}", "Input Error");
                return false;
            }
            else
            {
                input.BackColor = Color.White;
            }

            return true;
        }
        private bool HandleValidationResult(ValidationResult result, string context)
        {
            if (!result.IsValid)
            {
                ShowErrorMessage($"{result.Message}: {context}");
                return false;
            }
            return true;
        }
        private static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private static bool ValidateOptionalNumeric(TextBox input)
        {
     
            if (string.IsNullOrWhiteSpace(input.Text))
            {
                input.BackColor = Color.White;
                return true;
            }

            var result = NumberValidator.TestRealNumbers(input.Text);

            if (!result.IsValid)
            {
                input.BackColor = Color.Orange;
                MessageBox.Show($"{result.Message} in {input.Name}", "Input Error");
                return false;
            }

            input.BackColor = Color.White;
            return true;
        }








    }
}

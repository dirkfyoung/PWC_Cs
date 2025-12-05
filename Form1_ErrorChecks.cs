using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PWC_Cs
{
    public partial class Form1 : Form
    {
        private bool isValid;
        private string message;

        private void ResetValidationState()
        {
            isValid = true;
            message = string.Empty;
        }

        public void ValidateInputs()
        {
            ResetValidationState();

            ValidateChemicalInputs();
            ValidateSchemeInputs();
            ValidateOptionalOutput();
            ValidateAdvanceInputs();
        }
        private void ValidateChemicalInputs()
        {
            // Validate primary inputs
            ValidateGroup([Sorption1, WaterColMetab1, BenthicMetab1, Photo1, Hydrolysis1, SoilDegradation1, FoliarDeg1, FoliarWashoff1, MWT1, VaporPress1, Sol1, Henry1, AirDiff1, HeatHenry1]);

            // Validate references if applicable
            ValidateReferences(WaterColMetab1, WaterColRef1);
            ValidateReferences(BenthicMetab1, BenthicRef1);
            ValidateReferences(Photo1, PhotoLat1);
            ValidateReferences(SoilDegradation1, SoilRef1);

            if (DoDegradate1.Checked)
            {
                ValidateGroup(new[] { WaterMolarRatio1, BenthicMolarRatio1, PhotoMolarRatio1, HydroMolarRatio1, SoilMolarRatio1, FoliarMolarRatio1 });
                ValidateGroup(new[] { Sorption2, WaterColMetab2, BenthicMetab2, Photo2, Hydrolysis2, SoilDegradation2, FoliarDeg2, FoliarWashoff2, MWT2, VaporPress2, Sol2, Henry2, AirDiff2, HeatHenry2 });
                ValidateReferences(WaterColMetab2, WaterColRef2);
                ValidateReferences(BenthicMetab2, BenthicRef2);
                ValidateReferences(Photo2, PhotoLat2);
                ValidateReferences(SoilDegradation2, SoilRef2);
            }

            if (DoDegradate2.Checked)
            {
                ValidateGroup(new[] { WaterMolarRatio2, BenthicMolarRatio2, PhotoMolarRatio2, HydroMolarRatio2, SoilMolarRatio2, FoliarMolarRatio2 });
                ValidateGroup(new[] { Sorption3, WaterColMetab3, BenthicMetab3, Photo3, Hydrolysis3, SoilDegradation3, FoliarDeg3, FoliarWashoff3, MWT3, VaporPress3, Sol3, Henry3, AirDiff3, HeatHenry3 });
                ValidateReferences(WaterColMetab3, WaterColRef3);
                ValidateReferences(BenthicMetab3, BenthicRef3);
                ValidateReferences(Photo3, PhotoLat3);
                ValidateReferences(SoilDegradation3, SoilRef3);
            }




        }

        private void ValidateSchemeInputs()
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
                    if (!HandleValidationResult(NumberValidator.TestActualIntegers(applicationTable.ApplicationWindowStep), $"Window step in scheme {i + 1}")) return;
                    if (!HandleValidationResult(NumberValidator.TestActualIntegers(applicationTable.ApplicationWindowSpan), $"Window span in scheme {i + 1}")) return;

                    if (int.TryParse(applicationTable.ApplicationWindowSpan, out int windowSpan) && windowSpan > 365)
                    {
                        ShowErrorMessage($"Application window span cannot be greater than 365, scheme {i + 1}");
                        return;
                    }
                }

                // Application Table Information
                int actualRowsInAppTable = applicationTable.Days.Count;
                if (actualRowsInAppTable < 1)
                {
                    ShowErrorMessage($"There are no pesticide applications for scheme number {i + 1}");
                    return;
                }

                string[] formats = { "MM/d/yyyy", "MM/dd/yyyy", "M/dd/yyyy", "M/d/yyyy", "M/d", "MM/d", "M/d", "M/dd" };

                for (int j = 0; j < actualRowsInAppTable; j++)
                {
                    if (applicationTable.AbsoluteDays)
                    {
                        if (!DateTime.TryParseExact(applicationTable.Days[j], formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                        {
                            ShowErrorMessage($"Absolute Application date is not in the right format for Scheme {i + 1}, Row {j + 1}");
                            return;
                        }
                    }
                    else
                    {
                        if (!HandleValidationResult(NumberValidator.TestActualIntegers(applicationTable.Days[j]), $"for Scheme {i + 1}, Row {j + 1}")) return;
                    }

                    if (!HandleValidationResult(NumberValidator.TestRealNumbers(applicationTable.Amount[j]), $"Application Amount for Scheme {i + 1}, Row {j + 1}")) return;
                    if (!HandleValidationResult(NumberValidator.TestRealNumbers(applicationTable.Depth[j]), $"Application Depth for Scheme {i + 1}, Row {j + 1}")) return;
                    if (!HandleValidationResult(NumberValidator.TestRealNumbers(applicationTable.Split[j]), $"Split Value for Scheme {i + 1}, Row {j + 1}")) return;
                    if (!HandleValidationResult(NumberValidator.TestRealNumbers(applicationTable.Drift[j]), "Drift Value")) return;
                    if (!HandleValidationResult(NumberValidator.TestRealNumbers(applicationTable.DriftBuffer[j]), "Buffer Distance")) return;
                    if (!HandleValidationResult(NumberValidator.TestRealNumbers(applicationTable.Periodicity[j]), "Application Period")) return;

                    if (double.TryParse(applicationTable.Periodicity[j], out double periodicity) && periodicity < 1)
                    {
                        ShowErrorMessage("Periodicity in Application Table must be 1 or greater");
                        return;
                    }

                    if (!HandleValidationResult(NumberValidator.TestRealNumbers(applicationTable.Lag[j]), "Application Lag")) return;
                }
            }


        }

        private void ValidateOptionalOutput()
        {
            // Check Optional Output Table
            AdditionalOutputGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            var ztsModes = new List<string> { "TSER", "TCUM", "TAVE", "TSUM" };

            for (int i = 0; i < AdditionalOutputGridView.RowCount - 1; i++)
            {
                if (!HandleValidationResult(NumberValidator.TestActualIntegers(AdditionalOutputGridView[1, i].Value.ToString()), $"Row {i + 1} in Optional Outputs Table")) return;

                if (!DoDegradate1.Checked && Convert.ToInt32(AdditionalOutputGridView[1, i].Value) > 1)
                {
                    ShowErrorMessage($"Chemical form must be less than 2. Row {i + 1} in Optional Outputs Table. Degradate calculations were not selected on chemical tab.");
                    return;
                }

                if (!DoDegradate2.Checked && Convert.ToInt32(AdditionalOutputGridView[1, i].Value) > 2)
                {
                    ShowErrorMessage($"Chemical form must be less than 3. Row {i + 1} in Optional Outputs Table. Grandaughter calculations were not selected on chemical tab.");
                    return;
                }

                if (!ztsModes.Contains(AdditionalOutputGridView[2, i].Value.ToString()))
                {
                    ShowErrorMessage($"Mode selection can only be TSER, TAVE, TSUM, or TCUM. Row {i + 1} in Optional Outputs Table.");
                    return;
                }

                if (!HandleValidationResult(NumberValidator.TestActualIntegers(AdditionalOutputGridView[3, i].Value.ToString()), $"Arg1 in Row {i + 1} in Optional Outputs Table")) return;
                if (!HandleValidationResult(NumberValidator.TestActualIntegers(AdditionalOutputGridView[4, i].Value.ToString()), $"Arg2 in Row {i + 1} in Optional Outputs Table")) return;
                if (!HandleValidationResult(NumberValidator.TestRealNumbers(AdditionalOutputGridView[5, i].Value.ToString()), $"Multiplier in Row {i + 1} in Optional Outputs Table")) return;
            }
        }

        private void ValidateAdvanceInputs()
        {
            // Validate default linear isotherm requirements (daughter and granddaughter optional really)
            ValidateGroup([SubTimeSteps, Nexp1Reg1, Nexp2Reg1, Nexp3Reg1]);

            if (UseFreundlich.Checked)
            {
                ValidateGroup(new[] { FreundlichMinimumConc });
            }


            if (UseNonequilibrium.Checked)
            {
                ValidateGroup([Kf1Reg2, Kf2Reg2, Kf3Reg2, Nexp1Reg2, Nexp2Reg2, Nexp3Reg2, MassTransferRegion2, MassTransferRegion2Daughter, MassTransferRegion2GrandDaughter]);
            }

            if (ErosionFlag.Text.Trim() == "1" || ErosionFlag.Text.Trim() == "2" || ErosionFlag.Text.Trim() == "3")
            {
                ErosionFlag.BackColor = Color.White;
            }
            else 
            {  
                ErosionFlag.BackColor = Color.Orange;
                MessageBox.Show("Erosion flag must be 1, 2, or 3", "Input Error");
            }

                if (UseNonequilibrium.Checked)
            {
                ValidateGroup([Kf1Reg2, Kf2Reg2, Kf3Reg2, Nexp1Reg2, Nexp2Reg2, Nexp3Reg2, MassTransferRegion2, MassTransferRegion2Daughter, MassTransferRegion2GrandDaughter]);
                ValidateReferences(WaterColMetab2, WaterColRef2);
                ValidateReferences(BenthicMetab2, BenthicRef2);
                ValidateReferences(Photo2, PhotoLat2);
                ValidateReferences(SoilDegradation2, SoilRef2);
            }


            // Validate primary inputs
            ValidateGroup([Sorption1, WaterColMetab1, BenthicMetab1, Photo1, Hydrolysis1, SoilDegradation1, FoliarDeg1, FoliarWashoff1, MWT1, VaporPress1, Sol1, Henry1, AirDiff1, HeatHenry1]);

            // Validate references if applicable
            ValidateReferences(WaterColMetab1, WaterColRef1);
            ValidateReferences(BenthicMetab1, BenthicRef1);
            ValidateReferences(Photo1, PhotoLat1);
            ValidateReferences(SoilDegradation1, SoilRef1);







        }






        private void ValidateGroup(IEnumerable<TextBox> inputs, string? exception = null)
        {
            foreach (var input in inputs)
            {
                if (!ValidateInput(input, exception)) return;
            }
        }

        private void ValidateReferences(TextBox primary, TextBox reference)
        {
            if (!string.IsNullOrEmpty(primary.Text) && !ValidateInput(reference)) return;
        }

        private bool ValidateInput(TextBox input, string? exception = null)
        {
            var result = NumberValidator.TestRealNumbers(input.Text, exception);

            if (!result.IsValid)
            {
                input.BackColor = Color.Orange;
                MessageBox.Show($"{result.Message} in {input.Name}", "Input Error");
            }
            else
            {
                input.BackColor = Color.White;
            }

            return result.IsValid;
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

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


}

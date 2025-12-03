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

            if (!ValidateInput(Sorption1)) return;
            if (!ValidateInput(WaterColMetab1, string.Empty)) return;

            if (!string.IsNullOrEmpty(WaterColMetab1.Text))
            {
                ValidateInput(WaterColRef1);
            }

            if (!ValidateInput(BenthicMetab1, string.Empty)) return;
            if (!string.IsNullOrEmpty(BenthicMetab1.Text) && !ValidateInput(BenthicRef1)) return;

            if (!ValidateInput(Photo1, string.Empty)) return;
            if (!string.IsNullOrEmpty(Photo1.Text) && !ValidateInput(PhotoLat1)) return;

            if (!ValidateInput(Hydrolysis1, string.Empty)) return;

            if (!ValidateInput(SoilDegradation1, string.Empty)) return;
            if (!string.IsNullOrEmpty(SoilDegradation1.Text) && !ValidateInput(SoilRef1)) return;

            if (!ValidateInput(FoliarDeg1, string.Empty)) return;
            if (!ValidateInput(FoliarWashoff1, string.Empty)) return;
           
            if (!ValidateInput(MWT1, string.Empty)) return;
            if (!ValidateInput(VaporPress1, string.Empty)) return;
            if (!ValidateInput(Sol1, string.Empty)) return;
            if (!ValidateInput(Henry1, string.Empty)) return;
            if (!ValidateInput(AirDiff1, string.Empty)) return;
            if (!ValidateInput(HeatHenry1, string.Empty)) return;

            if (DoDegradate1.Checked)
            {
                if (!ValidateInput(WaterMolarRatio1, string.Empty)) return;
                if (!ValidateInput(BenthicMolarRatio1, string.Empty)) return;
                if (!ValidateInput(PhotoMolarRatio1, string.Empty)) return;
                if (!ValidateInput(HydroMolarRatio1, string.Empty)) return;
                if (!ValidateInput(SoilMolarRatio1, string.Empty)) return;
                if (!ValidateInput(FoliarMolarRatio1, string.Empty)) return;

                if (!ValidateInput(Sorption2)) return;
                if (!ValidateInput(WaterColMetab2, string.Empty)) return;
                if (!string.IsNullOrEmpty(WaterColMetab2.Text) && !ValidateInput(WaterColRef2)) return;

                if (!ValidateInput(BenthicMetab2, string.Empty)) return;
                if (!string.IsNullOrEmpty(BenthicMetab2.Text) && !ValidateInput(BenthicRef2)) return;

                if (!ValidateInput(Photo2, string.Empty)) return;
                if (!string.IsNullOrEmpty(Photo2.Text) && !ValidateInput(PhotoLat2)) return;

                if (!ValidateInput(Hydrolysis2, string.Empty)) return;
                if (!ValidateInput(SoilDegradation2, string.Empty)) return;
                if (!string.IsNullOrEmpty(SoilDegradation2.Text) && !ValidateInput(SoilRef2)) return;

                if (!ValidateInput(FoliarDeg2, string.Empty)) return;
                if (!ValidateInput(FoliarWashoff2, string.Empty)) return;

                if (!ValidateInput(MWT2, string.Empty)) return;
                if (!ValidateInput(VaporPress2, string.Empty)) return;
                if (!ValidateInput(Sol2, string.Empty)) return;
                if (!ValidateInput(Henry2, string.Empty)) return;
                if (!ValidateInput(AirDiff2, string.Empty)) return;
                if (!ValidateInput(HeatHenry2, string.Empty)) return;
            }

            // Validate additional inputs if DoDegradate2 is checked
            if (DoDegradate2.Checked)
            {
                if (!ValidateInput(WaterMolarRatio2, string.Empty)) return;
                if (!ValidateInput(BenthicMolarRatio2, string.Empty)) return;
                if (!ValidateInput(PhotoMolarRatio2, string.Empty)) return;
                if (!ValidateInput(HydroMolarRatio2, string.Empty)) return;
                if (!ValidateInput(SoilMolarRatio2, string.Empty)) return;
                if (!ValidateInput(FoliarMolarRatio2, string.Empty)) return;

                if (!ValidateInput(Sorption3)) return;
                if (!ValidateInput(WaterColMetab3, string.Empty)) return;
                if (!string.IsNullOrEmpty(WaterColMetab3.Text) && !ValidateInput(WaterColRef3)) return;

                if (!ValidateInput(BenthicMetab3, string.Empty)) return;
                if (!string.IsNullOrEmpty(BenthicMetab3.Text) && !ValidateInput(BenthicRef3)) return;

                if (!ValidateInput(Photo3, string.Empty)) return;
                if (!string.IsNullOrEmpty(Photo3.Text) && !ValidateInput(PhotoLat3)) return;

                if (!ValidateInput(Hydrolysis3, string.Empty)) return;
                if (!ValidateInput(SoilDegradation3, string.Empty)) return;
                if (!string.IsNullOrEmpty(SoilDegradation3.Text) && !ValidateInput(SoilRef3)) return;

                if (!ValidateInput(FoliarDeg3, string.Empty)) return;
                if (!ValidateInput(FoliarWashoff3, string.Empty)) return;

                if (!ValidateInput(MWT3, string.Empty)) return;
                if (!ValidateInput(VaporPress3, string.Empty)) return;
                if (!ValidateInput(Sol3, string.Empty)) return;
                if (!ValidateInput(Henry3, string.Empty)) return;
                if (!ValidateInput(AirDiff3, string.Empty)) return;
                if (!ValidateInput(HeatHenry3, string.Empty)) return;
            }

            AppTableDisplay.CommitEdit(DataGridViewDataErrorContexts.Commit);
            SchemeTableDisplay.CommitEdit(DataGridViewDataErrorContexts.Commit); //'commit the cell if cursor still on box

            int numberOfSchemes = SchemeTableDisplay.RowCount - 1;

            for (int i = 0; i < numberOfSchemes; i++)
            {
                SchemeDetails applicationTable = SchemeInfoList[i];

                if (applicationTable.UseApplicationWindow)
                {
                    var result = ValidateInteger(applicationTable.ApplicationWindowStep, $"Window step in scheme {i + 1}");
                    if (!result.IsValid) return;

                    result = ValidateInteger(applicationTable.ApplicationWindowSpan, $"Window span in scheme {i + 1}");
                    if (!result.IsValid) return;

                    if (Convert.ToInt32(applicationTable.ApplicationWindowSpan) > 365)
                    {
                        message = $"Application window span cannot be greater than 365, scheme {i + 1}";
                        MessageBox.Show(message, "Input Error");
                        isValid = false;
                        return;
                    }
                }

                // Application Table Information
                int actualRowsInAppTable = applicationTable.Days.Count;
                if (actualRowsInAppTable < 1)
                {
                    message = $"There are no pesticide applications for scheme number {i + 1}";
                    isValid = false;
                    return;
                }

                string[] formats = { "MM/d/yyyy", "MM/dd/yyyy", "M/dd/yyyy", "M/d/yyyy", "M/d", "MM/d", "M/d", "M/dd" };

                for (int j = 0; j < actualRowsInAppTable; j++)
                {
                    if (applicationTable.AbsoluteDays)
                    {
                        if (!DateTime.TryParseExact(applicationTable.Days[j], formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                        {
                            message = $"Absolute Application date is not in the right format for Scheme {i + 1}, Row {j + 1}";
                            isValid = false;
                            return;
                        }
                    }
                    else
                    {
                        ValidateInteger(applicationTable.Days[j], $"for Scheme {i + 1}, Row {j + 1}");
                        if (!isValid) return;
                    }
                }
            }






            //To be converted

        //    Dim formats() As String = { "MM/d/yyyy", "MM/dd/yyyy", "M/dd/yyyy", "M/d/yyyy", "M/d", "MM/d", "M/d", "M/dd" }
        //    Dim thisDt As DateTime

        //    For j As Integer = 0 To actualRowsInAppTable -1

        //        If ApplicationTable.AbsoluteRelative Then  'TRUE MEANS ABSOLUTE
        //            If Not DateTime.TryParseExact(ApplicationTable.Days(j), formats, Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, thisDt) Then
        //                msg = "Absolute Application date is not in the right format" & String.Format(" for Scheme {0}, Row {1}", i + 1, j + 1)
        //                TrueOrFalse = False
        //                Return
        //            End If

        //        Else
        //            TestActualIntegers(TrueOrFalse, msg, ApplicationTable.Days(j))
        //            msg = msg & String.Format(" for Scheme {0}, Row {1}", i + 1, j + 1)
        //            If TrueOrFalse = False Then Return
        //        End If

        //        TestActualRealNumbers(TrueOrFalse, msg, ApplicationTable.Amount(j))

        //        If TrueOrFalse = False Then
        //            msg = msg & " Application Amount" & String.Format(" for Scheme {0}, Row {1}", i + 1, j + 1)
        //            Return
        //        End If

        //        TestActualRealNumbers(TrueOrFalse, msg, ApplicationTable.Depth(j))


        //        If TrueOrFalse = False Then
        //            msg = msg & " Application Depth" & String.Format(" for Scheme {0}, Row {1}", i + 1, j + 1)
        //            Return
        //        End If

        //        TestActualRealNumbers(TrueOrFalse, msg, ApplicationTable.Split(j))
        //        If TrueOrFalse = False Then
        //            msg = msg & " Split Value" & String.Format(" for Scheme {0}, Row {1}", i + 1, j + 1)
        //            Return
        //        End If

        //        'TestActualRealNumbers(TrueOrFalse, msg, ApplicationTable.Efficiency(j))
        //        'If TrueOrFalse = False Then Return


        //        TestActualRealNumbers(TrueOrFalse, msg, ApplicationTable.Drift(j))
        //        If TrueOrFalse = False Then
        //            msg = msg & " Drift Value"
        //            Return
        //        End If

        //        TestActualRealNumbers(TrueOrFalse, msg, ApplicationTable.DriftBuffer(j))
        //        If TrueOrFalse = False Then
        //            msg = msg & " Buffer Distance"
        //            Return
        //        End If

        //        TestActualRealNumbers(TrueOrFalse, msg, ApplicationTable.Periodicity(j))
        //        If TrueOrFalse = False Then
        //            msg = msg & " Application Period"
        //            Return
        //        End If


        //        'Periodicity must be 1 or greater
        //        If ApplicationTable.Periodicity(j) < 1 Then
        //            msg = "Periodicity in Application Table must be 1 or greater"
        //            TrueOrFalse = False
        //            Return
        //        End If



        //        TestActualRealNumbers(TrueOrFalse, msg, ApplicationTable.Lag(j))
        //        If TrueOrFalse = False Then
        //            msg = msg & " Application Lag"
        //            Return
        //        End If



        //    Next
        //Next



        //'Check Optional Output Table
        //AdditionalOutputGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        //Dim zts_modes As New List(Of String) From { "TSER", "TCUM", "TAVE", "TSUM"}

        //    For i As Integer = 0 To AdditionalOutputGridView.RowCount - 2  'minus 2 because there is always a last empty row

        //    TestActualIntegers(TrueOrFalse, msg, AdditionalOutputGridView.Item(1, i).Value)
        //    If TrueOrFalse = False Then Return

        //    If DoDegradate1.Checked = False Then
        //        If AdditionalOutputGridView.Item(1, i).Value > 1 Then
        //            msg = String.Format("Chemical form must be less than 2.  Row {0} in Optional Outputs Table.  Degradate calculations were not selected on chemical tab.", i + 1)
        //            TrueOrFalse = False
        //            Return
        //        End If
        //    End If



        //    If DoDegradate2.Checked = False Then
        //        If AdditionalOutputGridView.Item(1, i).Value > 2 Then
        //            msg = String.Format("Chemical form must be less than 3. Row {0} in Optional Outputs Table. Grandaughter calculations were not selected on chemical tab.", i + 1)
        //            TrueOrFalse = False
        //            Return
        //        End If
        //    End If

        //    If Not zts_modes.Contains((AdditionalOutputGridView.Item(2, i).Value)) Then
        //        msg = String.Format("Mode selection can only be TSER, TAVE, TSUM, or TCUM.  Row {0} in Optional Outputs Table.", i + 1)
        //        TrueOrFalse = False
        //        Return
        //    End If


        //    TestActualIntegers(TrueOrFalse, msg, AdditionalOutputGridView.Item(3, i).Value)
        //    msg = msg & String.Format(" Arg1 in Row {0} in Optional Outputs Table.", i + 1)
        //    If TrueOrFalse = False Then Return


        //    TestActualIntegers(TrueOrFalse, msg, AdditionalOutputGridView.Item(4, i).Value)
        //    msg = msg & String.Format(" Arg2 in Row {0} in Optional Outputs Table.", i + 1)
        //    If TrueOrFalse = False Then Return


        //    TestActualRealNumbers(TrueOrFalse, msg, AdditionalOutputGridView.Item(5, i).Value)
        //    msg = msg & String.Format(" Multiplier in Row {0} in Optional Outputs Table.", i + 1)
        //    If TrueOrFalse = False Then Return

        //Next





















        }

        private bool ValidateInput(TextBox input, string exception = null)
        {
            var result = NumberValidator.TestRealNumbers(input.Text, exception);

            if (!result.IsValid)
            {
                input.BackColor = Color.Orange;
                MessageBox.Show(result.Message + " in " + input.Name, "Input Error");
            }
            else 
            {
                input.BackColor = Color.White;
            }

            return result.IsValid;
        }

        private ValidationResult ValidateInteger(string input, string context)
        {
            var result = NumberValidator.TestActualIntegers(input);

            if (!result.IsValid)
            {
                MessageBox.Show($"{result.Message}: {context}", "Input Error");
            }

            return result;
        }









    }





}

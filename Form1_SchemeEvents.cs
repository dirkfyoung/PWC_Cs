using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWC_Cs
{
    public partial class Form1 : Form
    {
        internal SchemeDetails GetSingleSchemeFromGUI(int schemenumber)
        {
            // Gets scheme details residing in App and scenario tabs
            // Need to send in scheme number to get description from scheme table

            SchemeDetails singleScheme = new SchemeDetails();

            AppTableDisplay.EndEdit();
            AppTableDisplay.CommitEdit(DataGridViewDataErrorContexts.Commit);

            singleScheme.SchemeDescription =
                Convert.ToString(SchemeTableDisplay.Rows[schemenumber].Cells[2].Value) ?? string.Empty;

            for (int i = 0; i < AppTableDisplay.Rows.Count; i++)
            {
                if (AppTableDisplay.Rows[i].IsNewRow) continue;

                singleScheme.Days.Add(AppTableDisplay[0, i].Value?.ToString() ?? "");
                singleScheme.Amount.Add(AppTableDisplay[1, i].Value?.ToString() ?? "");

                singleScheme.Method.Add(GetMethodCode(AppTableDisplay[2, i].FormattedValue?.ToString()));


                singleScheme.Depth.Add(AppTableDisplay[3, i].Value?.ToString() ?? "");
                singleScheme.Split.Add(AppTableDisplay[4, i].Value?.ToString() ?? "");

                singleScheme.Drift.Add(GetDriftCode(AppTableDisplay[5, i].FormattedValue?.ToString()));


                singleScheme.DriftBuffer.Add(AppTableDisplay[6, i].Value?.ToString() ?? "");
                singleScheme.Periodicity.Add(AppTableDisplay[7, i].Value?.ToString() ?? "");
                singleScheme.Lag.Add(AppTableDisplay[8, i].Value?.ToString() ?? "");
            }

            singleScheme.AbsoluteDays = AbsoluteDaysButton.Checked;
            singleScheme.Emerge = emerge.Checked;
            singleScheme.Maturity = maturity.Checked;
            singleScheme.Removal = removal.Checked;

            singleScheme.UseApplicationWindow = UseApplicationWindow.Checked;
            singleScheme.ApplicationWindowSpan = ApplicationWindowSpan.Text;
            singleScheme.ApplicationWindowStep = ApplicationWindowStep.Text;

            singleScheme.UseRainFast = UseRainFast.Checked;
            singleScheme.RainLimit = RainLimit.Text;
            singleScheme.IntolerableRainWindow = IntolerableRainWindow.Text;
            singleScheme.OptimumApplicationWindow = OptimumApplicationWindow.Text;
            singleScheme.MinDaysBetweenApps = MinDaysBetweenApps.Text;

            singleScheme.RunoffMitigation = RunoffMitigation.Text;
            singleScheme.ErosionMitigation = ErosionMitigation.Text;
            singleScheme.DriftMitigation = DriftMitigation.Text;

            singleScheme.Scenarios = ScenarioListBox.Items.Cast<string>().ToList();
            singleScheme.UseBatchScenarioFile = GetScenariosBatchCheckBox.Checked;
            singleScheme.ScenarioBatchFileName = ScenarioBatchFileName.Text;

            return singleScheme;
        }


        private void RecordScheme(int schemeNumber)
        {
            if (schemeNumber < 0)
            {
                return;
            }

            SchemeDetails appData = GetSingleSchemeFromGUI(schemeNumber); //= new SchemeDetails();

            if (schemeNumber >= 0 && schemeNumber < SchemeInfoList.Count)
            {
                // Index exists — overwrite
                SchemeInfoList[schemeNumber] = appData;
            }
            else
            {
                // Index doesn't exist — add to the end
                SchemeInfoList.Add(appData);
            }
        }


        private void SchemeTableDisplay_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {               //This just handles the edit box checks 
            int editColumnIndex = 1;
            if (e.ColumnIndex != editColumnIndex) return;

            //if (editColumnIndex == -1 || e.ColumnIndex != editColumnIndex) return;
            if (e.RowIndex < 0 || e.RowIndex >= SchemeTableDisplay.Rows.Count) return;

            var changedRow = SchemeTableDisplay.Rows[e.RowIndex];
            bool isChecked = Convert.ToBoolean(changedRow.Cells[editColumnIndex].Value);

            if (isChecked)  // find the row that just got unchecked and RecordScheme
            {
                foreach (DataGridViewRow row in SchemeTableDisplay.Rows) //Find just unchecked and record before loading the checked scheme
                {
                    if (row.Index != e.RowIndex && !row.IsNewRow)
                    {
                        if (Convert.ToBoolean(row.Cells[editColumnIndex].Value)) 
                        {
                            RecordScheme(row.Index);  //this is the previously checked scheme
                        }

                    }
                }

                //Now Load the checked scheme into the Display Table for apps and scenarios
           
                 LoadSchemeIntoDisplay(e.RowIndex);
               
            }

            if (!isChecked) // if one was just unchecked and nothing else checked then record the newly unchecked scheme
            {
                bool nothingChecked = true;
                foreach (DataGridViewRow row in SchemeTableDisplay.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (Convert.ToBoolean(row.Cells[editColumnIndex].Value)) nothingChecked = false;
                    }  
                }
                if (nothingChecked) RecordScheme(e.RowIndex);  //unchecked box and nothing else checked
            }


            //Now go through schemes and uncheck anything except the newly checked box
            if (isChecked)
            {
                foreach (DataGridViewRow row in SchemeTableDisplay.Rows)
                {
                    if (row.Index != e.RowIndex && !row.IsNewRow)
                    {
                        row.Cells[editColumnIndex].Value = false;
                    }
                }
            }
        }

        private void SchemeTableDisplay_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (SchemeTableDisplay.CurrentCell is DataGridViewCheckBoxCell && SchemeTableDisplay.IsCurrentCellDirty)
            {
                SchemeTableDisplay.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }


        private void SchemeTableDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { //Delete scheme
            if (e.ColumnIndex == 3 && e.RowIndex >= 0 && !SchemeTableDisplay.Rows[e.RowIndex].IsNewRow)
            {
                RecordScheme(e.RowIndex); //got to do this for the case where someone checks an unpopulated scheme and then deletes it,so not null
                SchemeTableDisplay.Rows.RemoveAt(e.RowIndex);
                SchemeInfoList.RemoveAt(e.RowIndex);
    
            }
        }

        private void RecordCheckedScheme()
        {
            //Record the possibly uncommitted scheme with the checked box
            SchemeTableDisplay.CommitEdit(DataGridViewDataErrorContexts.Commit);
            int checkboxColumnIndex = 1; // set to checkbox column index
            int checkedrow = -1;
            for (int r = 0; r < SchemeTableDisplay.Rows.Count; r++)
            {
                var cell = SchemeTableDisplay[checkboxColumnIndex, r];
                if (cell?.Value is bool b && b)
                {
                    checkedrow = r;
                    break;
                }
            }
            RecordScheme(checkedrow); //save current scheme before saving file
        }

        private void LoadSchemeIntoDisplay(int schemeNumber)
        {
            AppTableDisplay.Rows.Clear();
            ScenarioListBox.Items.Clear();

            if (schemeNumber < 0 || schemeNumber >= SchemeInfoList.Count)
            {
                return;  // Scheme doesn't exist yet, don't try to load to display
            }

            SchemeTableDisplay.Rows[schemeNumber].Cells[2].Value = SchemeInfoList[schemeNumber].SchemeDescription;

            int numberApps = SchemeInfoList[schemeNumber].Days.Count;
            for (int i = 0; i < numberApps; i++)
            {
                AppTableDisplay.Rows.Add();

                AppTableDisplay.Rows[i].Cells["Days"].Value = SchemeInfoList[schemeNumber].Days[i];
                AppTableDisplay.Rows[i].Cells[1].Value = SchemeInfoList[schemeNumber].Amount[i];

                // Application Method combo box
                AppTableDisplay.Rows[i].Cells[2].Value = GetMethodDisplay(SchemeInfoList[schemeNumber].Method[i]);

                AppTableDisplay.Rows[i].Cells[3].Value = SchemeInfoList[schemeNumber].Depth[i];
                AppTableDisplay.Rows[i].Cells[4].Value = SchemeInfoList[schemeNumber].Split[i];

                // Drift Type combo box
                AppTableDisplay.Rows[i].Cells[5].Value = GetDriftDisplay(SchemeInfoList[schemeNumber].Drift[i]);

                AppTableDisplay.Rows[i].Cells[6].Value = SchemeInfoList[schemeNumber].DriftBuffer[i];
                AppTableDisplay.Rows[i].Cells[7].Value = SchemeInfoList[schemeNumber].Periodicity[i];
                AppTableDisplay.Rows[i].Cells[8].Value = SchemeInfoList[schemeNumber].Lag[i];
            }

            AbsoluteDaysButton.Checked = SchemeInfoList[schemeNumber].AbsoluteDays;
            emerge.Checked = SchemeInfoList[schemeNumber].Emerge;
            maturity.Checked = SchemeInfoList[schemeNumber].Maturity;
            removal.Checked = SchemeInfoList[schemeNumber].Removal;

            UseApplicationWindow.Checked = SchemeInfoList[schemeNumber].UseApplicationWindow;
            ApplicationWindowStep.Text = SchemeInfoList[schemeNumber].ApplicationWindowStep;
            ApplicationWindowSpan.Text = SchemeInfoList[schemeNumber].ApplicationWindowSpan;

            UseRainFast.Checked = SchemeInfoList[schemeNumber].UseRainFast;
            RainLimit.Text = SchemeInfoList[schemeNumber].RainLimit;
            IntolerableRainWindow.Text = SchemeInfoList[schemeNumber].IntolerableRainWindow;
            OptimumApplicationWindow.Text = SchemeInfoList[schemeNumber].OptimumApplicationWindow;
            MinDaysBetweenApps.Text = SchemeInfoList[schemeNumber].MinDaysBetweenApps;

            ScenarioListBox.Items.AddRange(SchemeInfoList[schemeNumber].Scenarios.ToArray());

            GetScenariosBatchCheckBox.Checked = SchemeInfoList[schemeNumber].UseBatchScenarioFile;
            ScenarioBatchFileName.Text = SchemeInfoList[schemeNumber].ScenarioBatchFileName;

            RunoffMitigation.Text = SchemeInfoList[schemeNumber].RunoffMitigation;
            ErosionMitigation.Text = SchemeInfoList[schemeNumber].ErosionMitigation;
            DriftMitigation.Text = SchemeInfoList[schemeNumber].DriftMitigation;
        }



        private static string GetMethodCode(string? displayText)
        {
            return displayText switch
            {
                null => "1",
                var m when m == Standard.Method1 => "1",
                var m when m == Standard.Method2 => "2",
                var m when m == Standard.Method3 => "3",
                var m when m == Standard.Method4 => "4",
                var m when m == Standard.Method5 => "5",
                var m when m == Standard.Method6 => "6",
                var m when m == Standard.Method7 => "7",
                _ => "1"
            };
        }

        private static string GetMethodDisplay(string? code)
        {
            return code switch
            {
                "1" => Standard.Method1,
                "2" => Standard.Method2,
                "3" => Standard.Method3,
                "4" => Standard.Method4,
                "5" => Standard.Method5,
                "6" => Standard.Method6,
                "7" => Standard.Method7,
                _ => Standard.Method1
            };
        }

        private static string GetDriftCode(string? displayText)
        {
            return displayText switch
            {
                null => "15",
                var d when d == Standard.SprayTerms[1] => "1",
                var d when d == Standard.SprayTerms[2] => "2",
                var d when d == Standard.SprayTerms[3] => "3",
                var d when d == Standard.SprayTerms[4] => "4",
                var d when d == Standard.SprayTerms[5] => "5",
                var d when d == Standard.SprayTerms[6] => "6",
                var d when d == Standard.SprayTerms[7] => "7",
                var d when d == Standard.SprayTerms[8] => "8",
                var d when d == Standard.SprayTerms[9] => "9",
                var d when d == Standard.SprayTerms[10] => "10",
                var d when d == Standard.SprayTerms[11] => "11",
                var d when d == Standard.SprayTerms[12] => "12",
                var d when d == Standard.SprayTerms[13] => "13",
                var d when d == Standard.SprayTerms[14] => "14",
                var d when d == Standard.SprayTerms[15] => "15",
                _ => "15"
            };
        }

        private static string GetDriftDisplay(string? code)
        {
            return code switch
            {
                "1" => Standard.SprayTerms[1],
                "2" => Standard.SprayTerms[2],
                "3" => Standard.SprayTerms[3],
                "4" => Standard.SprayTerms[4],
                "5" => Standard.SprayTerms[5],
                "6" => Standard.SprayTerms[6],
                "7" => Standard.SprayTerms[7],
                "8" => Standard.SprayTerms[8],
                "9" => Standard.SprayTerms[9],
                "10" => Standard.SprayTerms[10],
                "11" => Standard.SprayTerms[11],
                "12" => Standard.SprayTerms[12],
                "13" => Standard.SprayTerms[13],
                "14" => Standard.SprayTerms[14],
                "15" => Standard.SprayTerms[15],
                _ => Standard.SprayTerms[15]
            };
        }




    }


}

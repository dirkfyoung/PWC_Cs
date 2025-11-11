using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWC_Cs
{
    public partial class Form1 : Form
    {


        private void RecordScheme(int schemeNumber)
        {

            if (schemeNumber < 0)
            {
                MessageBox.Show($"Row {schemeNumber} is checked.");
                return;
            }

            var appData = new SchemeDetails();

            AppTableDisplay.CommitEdit(DataGridViewDataErrorContexts.Commit);


            for (int i = 0; i < AppTableDisplay.Rows.Count; i++)
            {
                if (AppTableDisplay.Rows[i].IsNewRow) continue;
                // rest of logic

                appData.Days.Add(AppTableDisplay[0, i].Value?.ToString() ?? "");
                appData.Amount.Add(AppTableDisplay[1, i].Value?.ToString() ?? "");

                var method = AppTableDisplay[2, i].Value;
                appData.Method.Add(method switch
                {
                    var m when m == Standard.Method1 => "1",
                    var m when m == Standard.Method2 => "2",
                    var m when m == Standard.Method3 => "3",
                    var m when m == Standard.Method4 => "4",
                    var m when m == Standard.Method5 => "5",
                    var m when m == Standard.Method6 => "6",
                    var m when m == Standard.Method7 => "7",
                    _ => "1"
                });

                appData.Depth.Add(AppTableDisplay[3, i].Value?.ToString() ?? "");
                appData.Split.Add(AppTableDisplay[4, i].Value?.ToString() ?? "");

                var drift = AppTableDisplay[5, i].Value;
                appData.Drift.Add(drift switch
                {
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
                });

                appData.DriftBuffer.Add(AppTableDisplay[6, i].Value?.ToString() ?? "");
                appData.Periodicity.Add(AppTableDisplay[7, i].Value?.ToString() ?? "");
                appData.Lag.Add(AppTableDisplay[8, i].Value?.ToString() ?? "");
            }

            appData.AbsoluteRelative = AbsoluteDaysButton.Checked;
            appData.Emerge = emerge.Checked;
            appData.Maturity = maturity.Checked;
            appData.Removal = removal.Checked;

            appData.UseApplicationWindow = UseApplicationWindow.Checked;
            appData.ApplicationWindowSpan = ApplicationWindowDays.Text;
            appData.ApplicationWindowStep = ApplicationWindowStep.Text;

            appData.UseRainFast = UseRainFast.Checked;
            appData.RainLimit = RainLimit.Text;
            appData.IntolerableRainWindow = IntolerableRainWindow.Text;
            appData.OptimumApplicationWindow = OptimumApplicationWindow.Text;
            appData.MinDaysBetweenApps = MinDaysBetweenApps.Text;

            appData.RunoffMitigation = RunoffMitigation.Text;
            appData.ErosionMitigation = ErosionMitigation.Text;
            appData.DriftMitigation = DriftMitigation.Text;

            //appData.Scenarios = ScenarioListBox.Items.Cast<string>().ToList();
            //appData.UseBatchScenarioFile = GetScenariosBatchCheckBox.Checked;
            //appData.ScenarioBatchFileName = ScenarioBatchFileName.Text;

            //if (schemeInfoList.Count - 1 < schemeNumber)
            //    schemeInfoList.Add(appData);
            //else if (schemeNumber >= 0)
            //    schemeInfoList[schemeNumber] = appData;


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
        {
            // Find the column index with header "Edit"
            int editColumnIndex = -1;
            foreach (DataGridViewColumn col in SchemeTableDisplay.Columns)
            {
                if (col.HeaderText == "Edit")
                {
                    editColumnIndex = col.Index;
                    break;
                }
            }

            if (editColumnIndex == -1 || e.ColumnIndex != editColumnIndex) return;
            if (e.RowIndex < 0 || e.RowIndex >= SchemeTableDisplay.Rows.Count) return;

            // If the new value is true, uncheck all other rows
            var changedRow = SchemeTableDisplay.Rows[e.RowIndex];

            bool isChecked = Convert.ToBoolean(changedRow.Cells[editColumnIndex].Value);




            // i think this should be moved to after routine that follow currently commnted out

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



            //        private void SchemeTableDisplay_CellValueChanged(object sender, DataGridViewCellEventArgs e)
            //        {
            //            if (e.ColumnIndex == yourCheckBoxColumnIndex)
            //            {
            //                bool isChecked = Convert.ToBoolean(SchemeTableDisplay[e.ColumnIndex, e.RowIndex].Value);

            //                if (isChecked)
            //                {
            //                    // Uncheck the previously checked row
            //                    if (previouslyCheckedRow.HasValue && previouslyCheckedRow != e.RowIndex)
            //                    {
            //                        SchemeTableDisplay[e.ColumnIndex, previouslyCheckedRow.Value].Value = false;
            //                    }

            //                    previouslyCheckedRow = e.RowIndex;
            //                }
            //                else if (previouslyCheckedRow == e.RowIndex)
            //                {
            //                    previouslyCheckedRow = null;
            //                }
            //            }
            //        }












            //from ai
            //    private void SchemeTableDisplay_CellValueChanged(object sender, DataGridViewCellEventArgs e)
            //{
            //    if (e.ColumnIndex == yourCheckBoxColumnIndex)
            //    {
            //        bool isChecked = Convert.ToBoolean(SchemeTableDisplay[e.ColumnIndex, e.RowIndex].Value);

            //        if (isChecked)
            //        {
            //            // ✅ This is the new checked row
            //            int newCheckedRow = e.RowIndex;
            //            Console.WriteLine($"New checked row: {newCheckedRow}");

            //            // You can now uncheck the previous one if needed
            //        }
            //    }
            //}





        }


        private void SchemeTableDisplay_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (SchemeTableDisplay.CurrentCell is DataGridViewCheckBoxCell && SchemeTableDisplay.IsCurrentCellDirty)
            {
                SchemeTableDisplay.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }







            ////////////////////////////////////////////////////////////////

        //private void SchemeTableDisplay_CurrentCellDirtyStateChanged(object sender, EventArgs e)
//        {
//            if (SchemeTableDisplay.IsCurrentCellDirty)
//            {
//                // Commit the edit so CellValueChanged will fire
//                SchemeTableDisplay.CommitEdit(DataGridViewDataErrorContexts.Commit);
//            }
//        }







        ////////////////////////////////////////////////////////////////























    }
    // Other event handlers or logic






















}
}

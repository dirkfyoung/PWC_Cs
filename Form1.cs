using Microsoft.VisualBasic.Logging;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Net.Mime.MediaTypeNames;
//using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace PWC_Cs
{
    public partial class Form1 : Form
    {

        private readonly List<SchemeDetails> SchemeInfoList = new List<SchemeDetails>();
        private SchemeDetails? copiedScheme;  //null protection in place in paste routine
        private TabPage hiddenOptionalOutputTabPage = null;
        private TabPage hiddenAdvancedTabPage = null;



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


            //Hide the optional tab pages
            hiddenOptionalOutputTabPage = HideTab(tabControl1, OptionalOutputTab, hiddenOptionalOutputTabPage);
            hiddenAdvancedTabPage = HideTab(tabControl1, AdvancedTab, hiddenAdvancedTabPage);

        }
        //**********************************************************************
        private void SaveInputFile_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PWC 3 INPUT Files (*.PW4)|*.PW4|PWC 3 INPUT Files (*.PW3)|*.PW3|ALL Files (*.*)|*.*";

            var candidate = FileNames.WorkingDirectory;
            if (Directory.Exists(candidate))
            {
                saveFileDialog1.InitialDirectory = candidate;
            }

            var result = saveFileDialog1.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var selectedFile = saveFileDialog1.FileName;
                var dir = string.IsNullOrEmpty(selectedFile) ? null : Path.GetDirectoryName(selectedFile);
                if (!string.IsNullOrEmpty(dir))
                {
                    FileNames.WorkingDirectory = dir + Path.DirectorySeparatorChar;
                    WorkingDirectoryTextBox.Text = FileNames.WorkingDirectory;
                    IOFamilyName.Text = Path.GetFileNameWithoutExtension(selectedFile);
                    SaveMainInputToTextFile(selectedFile);
                }
            }
        }
        //**********************************************************************
        private void RetrieveInputFile_Click(object sender, EventArgs e)
        {
            retrieveMainInputDialog.Filter =
                "PWC 3 INPUT Files (*.PW4)|*.PW4|PWC 3 INPUT Files (*.PW3)|*.PW3|ALL Files (*.*)|*.*";

            // use window default or previous working directory if it exists (even after previous shutdowns)
            var candidate = FileNames.WorkingDirectory;
            if (Directory.Exists(candidate))
            {
                retrieveMainInputDialog.InitialDirectory = candidate;
            }

            retrieveMainInputDialog.FileName = string.Empty;

            var result = retrieveMainInputDialog.ShowDialog(this);


            if (result == DialogResult.OK)
            {
                // FileNames.WorkingDirectory = Path.GetDirectoryName(retrieveMainInputDialog.FileName) + "\\";

                var selectedFile = retrieveMainInputDialog.FileName;
                var dir = string.IsNullOrEmpty(selectedFile) ? null : Path.GetDirectoryName(selectedFile);
                if (!string.IsNullOrEmpty(dir))
                {
                    FileNames.WorkingDirectory = dir + Path.DirectorySeparatorChar;
                    WorkingDirectoryTextBox.Text = FileNames.WorkingDirectory;
                    IOFamilyName.Text = Path.GetFileNameWithoutExtension(selectedFile);
                    RetrieveMainInputFromTextFile(selectedFile);
                }

            }
        }

        private void SelectOtherWaterbodies_Click(object sender, EventArgs e)
        {

            openOtherWaterbody.Filter = "Water Body Files (*.WAT)|*.WAT|All files (*.*)|*.*";
            if (Directory.Exists(FileNames.WaterBodyDirectory))
            {
                openOtherWaterbody.InitialDirectory = FileNames.WaterBodyDirectory;
            }


            var result = openOtherWaterbody.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                var selectedFile = openOtherWaterbody.FileName;
                var dir = string.IsNullOrEmpty(selectedFile) ? null : Path.GetDirectoryName(selectedFile);
                if (!string.IsNullOrEmpty(dir))
                {
                    FileNames.WaterBodyDirectory = dir + Path.DirectorySeparatorChar;

                    // Add each selected file (full path) to the list
                    foreach (var selectedScenario in openOtherWaterbody.FileNames)
                    {
                        WaterbodyList.Items.Add(selectedScenario);
                    }

                }

            }

        }

        private void ClearAllWaterBodies_Click(object sender, EventArgs e)
        {
            WaterbodyList.Items.Clear();
        }

        private void ClearSelectedWaterBody_Click(object sender, EventArgs e)
        {
            // Create a temporary list of items to remove to avoid issues while modifying the original collection
            var itemsToRemove = new System.Collections.Generic.List<string>();
            foreach (string selectedItem in WaterbodyList.SelectedItems)
            {
                itemsToRemove.Add(selectedItem);
            }

            // Remove items from the underlying collection (UI updates automatically)
            foreach (string item in itemsToRemove)
            {
                WaterbodyList.Items.Remove(item);
            }

        }

        private void GetWeatherFileDirectory_Click(object sender, EventArgs e)
        {
            weatherFileDialog.Filter = "Weather Files (*.wea)|*.WEA|ALL Files (*.*)|*.*";

            var candidate = FileNames.WeatherFileDirectory;
            if (Directory.Exists(candidate))
            {
                weatherFileDialog.InitialDirectory = candidate;
            }

            weatherFileDialog.FileName = string.Empty;

            var result = weatherFileDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {

                var selectedFile = weatherFileDialog.FileName;
                var dir = string.IsNullOrEmpty(selectedFile) ? null : Path.GetDirectoryName(selectedFile);
                if (!string.IsNullOrEmpty(dir))
                {
                    FileNames.WeatherFileDirectory = dir + Path.DirectorySeparatorChar;
                    WeatherFileDirectory.Text = FileNames.WeatherFileDirectory;
                }

            }
        }

        private void WriteSchemeTable_Click(object sender, EventArgs e)
        {
            saveSchemeFile.Filter = "CSV File (*.csv)|*.CSV|ALL Files (*.*)|*.*";
            saveSchemeFile.FileName = string.Empty;
            saveSchemeFile.InitialDirectory = FileNames.WorkingDirectory;

            var result = saveSchemeFile.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                var selectedFile = saveSchemeFile.FileName;
                var dir = string.IsNullOrEmpty(selectedFile) ? null : Path.GetDirectoryName(selectedFile);
                if (!string.IsNullOrEmpty(dir))
                {
                    SaveSchemeTableAsTextFile(saveSchemeFile.FileName);
                }
            }
        }

        private void SelectScenarios_Click(object sender, EventArgs e)
        {
            openScenarios.Filter = "Scenario Files (*.SCN2)|*.SCN2|All files (*.*)|*.*";
            if (Directory.Exists(FileNames.ScenarioDirectory))
            {
                openScenarios.InitialDirectory = FileNames.ScenarioDirectory;
            }

            var result = openScenarios.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var selectedFile = openScenarios.FileName;
                var dir = string.IsNullOrEmpty(selectedFile) ? null : Path.GetDirectoryName(selectedFile);
                if (!string.IsNullOrEmpty(dir))
                {
                    FileNames.ScenarioDirectory = dir + Path.DirectorySeparatorChar;

                    // Add each selected file (full path) to the list
                    foreach (var selectedScenario in openScenarios.FileNames)
                    {
                        ScenarioListBox.Items.Add(selectedScenario);
                    }
                }
            }


        }

        private void SchemeTableDisplay_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Skip numbering for the new row
            if (!SchemeTableDisplay.Rows[e.RowIndex].IsNewRow)
            {
                // Calculate the row number
                string rowNumber = (e.RowIndex + 1).ToString();

                // Determine the size and position of the row number
                SizeF size = e.Graphics.MeasureString(rowNumber, SchemeTableDisplay.Font);
                PointF location = new PointF(
                    e.RowBounds.Location.X + 15,
                    e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2)
                );

                // Draw the row number
                e.Graphics.DrawString(rowNumber, SchemeTableDisplay.Font, SystemBrushes.ControlText, location);
            }
        }

        private void copyScheme_Click(object sender, EventArgs e)
        {

            //find row with checked box
            foreach (DataGridViewRow row in SchemeTableDisplay.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (Convert.ToBoolean(row.Cells[1].Value))
                    {  // Get Scheme number that has check to be copied

                        copiedScheme = GetSingleSchemeFromGUI(row.Index);  //Copy a scheme into SchemeDetail variable 
                        break;
                    }
                }
            }




        }

        private void pasteScheme_Click(object sender, EventArgs e)
        {

            // Check if copiedScheme is null
            if (copiedScheme == null)
            {
                MessageBox.Show("No scheme has been copied. Please copy a scheme first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //find row with checked box
            foreach (DataGridViewRow row in SchemeTableDisplay.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (Convert.ToBoolean(row.Cells[1].Value))
                    {  // Put Scheme into SchemeInfoList and then load it into GUI

                        if (row.Index >= 0 && row.Index < SchemeInfoList.Count)
                        {
                            // Index exists — overwrite
                            SchemeInfoList[row.Index] = copiedScheme;
                        }
                        else
                        {
                            // Index doesn't exist — add to the end
                            SchemeInfoList.Add(copiedScheme);
                        }
                        LoadSchemeIntoDisplay(row.Index);
                    }
                }
            }
        }

        private void DoDegradate1_CheckedChanged(object sender, EventArgs e)
        {
            SetDaughterVisibleStatus(false);
            DoDegradate2.Visible = false;
            GranddaughterLabel.Visible = false;
            DoDegradate2.Checked = false;
            if (DoDegradate1.Checked == true)
            {
                SetDaughterVisibleStatus(true);
                DoDegradate2.Visible = true;
                GranddaughterLabel.Visible = true;
            }

        }


        private void DoDegradate2_CheckedChanged(object sender, EventArgs e)
        {
            SetGrandaughterVisibleStatus(false);
            if (DoDegradate2.Checked == true) SetGrandaughterVisibleStatus(true);
        }


        private void SetGrandaughterVisibleStatus(bool status)
        {
            Sorption3.Visible = status;
            WaterColMetab3.Visible = status;
            WaterColRef3.Visible = status;
            BenthicMetab3.Visible = status;
            BenthicRef3.Visible = status;
            Photo3.Visible = status;
            PhotoLat3.Visible = status;
            Hydrolysis3.Visible = status;
            SoilDegradation3.Visible = status;
            SoilRef3.Visible = status;
            FoliarDeg3.Visible = status;
            FoliarWashoff3.Visible = status;
            MWT3.Visible = status;
            VaporPress3.Visible = status;
            Sol3.Visible = status;
            Henry3.Visible = status;
            AirDiff3.Visible = status;
            HeatHenry3.Visible = status;

            WaterMolarRatio2.Visible = status;
            BenthicMolarRatio2.Visible = status;
            PhotoMolarRatio2.Visible = status;
            HydroMolarRatio2.Visible = status;
            SoilMolarRatio2.Visible = status;
            FoliarMolarRatio2.Visible = status;
        }

        private void SetDaughterVisibleStatus(bool status)
        {
            Sorption2.Visible = status;
            WaterColMetab2.Visible = status;
            WaterColRef2.Visible = status;
            BenthicMetab2.Visible = status;
            BenthicRef2.Visible = status;
            Photo2.Visible = status;
            PhotoLat2.Visible = status;
            Hydrolysis2.Visible = status;
            SoilDegradation2.Visible = status;
            SoilRef2.Visible = status;
            FoliarDeg2.Visible = status;
            FoliarWashoff2.Visible = status;
            MWT2.Visible = status;
            VaporPress2.Visible = status;
            Sol2.Visible = status;
            Henry2.Visible = status;
            AirDiff2.Visible = status;
            HeatHenry2.Visible = status;

            WaterMolarRatio1.Visible = status;
            BenthicMolarRatio1.Visible = status;
            PhotoMolarRatio1.Visible = status;
            HydroMolarRatio1.Visible = status;
            SoilMolarRatio1.Visible = status;
            FoliarMolarRatio1.Visible = status;
        }

        private void ItsaPond_CheckedChanged(object sender, EventArgs e)
        {
            ItsTPEZWPEZ.Enabled = false;
            UseTPEZbuffers.Enabled = false;
            ItsTPEZWPEZ.Checked = false;
            UseTPEZbuffers.Checked = false;


            if (ItsaPond.Checked)
            {
                ItsTPEZWPEZ.Enabled = true;
                UseTPEZbuffers.Enabled = true;
            }
        }


        private void WorkingDirectoryTextBox_MouseEnter(object sender, EventArgs e)
        {
            WorkingDirectoryTextBox.ForeColor = Color.Blue;
        }

        private void WorkingDirectoryTextBox_MouseLeave(object sender, EventArgs e)
        {
            WorkingDirectoryTextBox.ForeColor = Color.Black;
        }


        private void toggleOptionalOutput_Click(object sender, EventArgs e)
        {
            if (tabControl1.Contains(OptionalOutputTab))
            {
                hiddenOptionalOutputTabPage = HideTab(tabControl1, OptionalOutputTab, hiddenOptionalOutputTabPage);
            }
            else
            {
                ShowTab(tabControl1, hiddenOptionalOutputTabPage);
            }

        }
        private void toggleAdvancedSettings_Click(object sender, EventArgs e)
        {
            if (tabControl1.Contains(AdvancedTab))
            {
                hiddenAdvancedTabPage = HideTab(tabControl1, AdvancedTab, hiddenAdvancedTabPage);
            }
            else
            {
                ShowTab(tabControl1, hiddenAdvancedTabPage);
            }
        }



        private TabPage HideTab(TabControl tabControl, TabPage tabPage, TabPage hiddenTabPage)
        {
            hiddenTabPage = tabPage;
            tabControl.TabPages.Remove(tabPage);
            return hiddenTabPage;
        }

        private void ShowTab(TabControl tabControl, TabPage hiddenTabPage)
        {
            tabControl.TabPages.Add(hiddenTabPage);

        }

        private void WorkingDirectoryTextBox_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(WorkingDirectoryTextBox.Text))
            {
                Process.Start("explorer.exe", WorkingDirectoryTextBox.Text);
            }
        }

        private async void CalculateButton_Click(object sender, EventArgs e)
        {

            //First record the scheme if it was not auto committed by checking or unchecking rows
            foreach (DataGridViewRow row in SchemeTableDisplay.Rows)       //Find the checked row
            {
                if (Convert.ToBoolean(row.Cells[1].Value))
                {
                    RecordScheme(row.Index);  //this is the checked scheme if there is one
                }
            }


            //Check for a working directory
            if (!Directory.Exists(WorkingDirectoryTextBox.Text))
            {
                MessageBox.Show("No working directory. Save this work, and a working directory will be created automatically", "Error");
                return;
            }
            System.IO.Directory.SetCurrentDirectory(WorkingDirectoryTextBox.Text);

            //Check values for errors

            if (!ValidateInputs()) return;

            //Run PRZM-VWM.exe

            await RunExternalProcessAsync();

        }


        private async Task RunExternalProcessAsync()
        {
            // Get the directory path of the currently executing assembly
            string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

            // Combine the directory path with the executable name
            string exePath = Path.Combine(directoryPath, "PRZM-VVWM.exe");


            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = "przmvvwm.txt",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            try
            {

                using Process process = new()
                { StartInfo = startInfo };
                process.Start();

                // Read output and error streams asynchronously
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();

                await Task.Run(() => process.WaitForExit());

                // Combine output and error messages
                string combinedOutput = $"Output:\n{output}\nErrors:\n{error}";

                // Write combined output to a file
                await File.WriteAllTextAsync("run_status.txt", combinedOutput);

                MessageBox.Show("Process finished with exit code: " + process.ExitCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Start proceess error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }














     
    }









}

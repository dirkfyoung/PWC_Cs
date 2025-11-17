using Microsoft.VisualBasic.Logging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


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

            // configure filter
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

            // Cancel button will cause return without further execution
            if (result == DialogResult.Cancel)
            {
                return;
            }

            // store the working directory
            var selectedFile = retrieveMainInputDialog.FileName;
            var dir = string.IsNullOrEmpty(selectedFile) ? null : Path.GetDirectoryName(selectedFile);
            if (!string.IsNullOrEmpty(dir))
            {
                FileNames.WorkingDirectory = dir + Path.DirectorySeparatorChar;
            }

            // read inputs from the selected file
            RetrieveMainInputFromTextFile(selectedFile);


        }








            //// Opens the desktop if there is no previous open --commented out
            //if (Directory.Exists(FileNames.WorkingDirectory))
            //{
            //    retrieveMainInputDialog.InitialDirectory = FileNames.WorkingDirectory;
            //}
            ////else
            ////{
            ////   retrieveMainInputDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ////}


            //if (retrieveMainInputDialog.ShowDialog(this) == DialogResult.OK)
            //{
            //    RetrieveMainInputFromTextFile(retrieveMainInputDialog.FileName);
            //}







       

        private void SelectOtherWaterbodies_Click(object sender, EventArgs e)
        {

            DialogResult result;

            openOtherWaterbody.Filter = "Water Body Files (*.WAT)|*.WAT|All files (*.*)|*.*";

            openOtherWaterbody.InitialDirectory = FileNames.DefaultWaterBodyDirectory;

            if (Directory.Exists(FileNames.PreviousWaterBodyPath))
                openOtherWaterbody.InitialDirectory = FileNames.PreviousWaterBodyPath;

            result = openOtherWaterbody.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            // store the directory of the selected file (guard against null)
            var dir = Path.GetDirectoryName(openOtherWaterbody.FileName);
            if (!string.IsNullOrEmpty(dir))
                FileNames.PreviousWaterBodyPath = dir;


            // Add each selected file (full path) to the list
            foreach (var selectedScenario in openOtherWaterbody.FileNames)
            {
                WaterbodyList.Items.Add(selectedScenario);
            }

            // store previous scenario path as well (guard against null)
            var scenarioDir = Path.GetDirectoryName(openOtherWaterbody.FileName);
            if (!string.IsNullOrEmpty(scenarioDir))
                FileNames.PreviousScenarioPath = scenarioDir;





        }
        //**********************************************************************


    }
}

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
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                RetrieveMainInputFromTextFile(openFileDialog1.FileName);
            }
        }
        //**********************************************************************
 

    }
}

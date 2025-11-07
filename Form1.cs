using System.Windows.Forms;

namespace PWC_Cs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SaveInputFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SaveMainInputToTextFile(saveFileDialog1.FileName);
            }

        }

        private void RetrieveInputFile_Click(object sender, EventArgs e)
        {
            if  (openFileDialog1.ShowDialog(this) == DialogResult.OK) 
            {
                RetrieveMainInputFromTextFile(openFileDialog1.FileName);
            }
        }




        public void RetrieveMainInputFromTextFile(string readfilename) 
        {
            string[] lines = File.ReadAllLines(readfilename);
            string[] col;
            col = lines[0].Split(',');
            sorption1.Text = col[0];
            sorption2.Text = col[1];
            sorption3.Text = col[2];
           

        }






        public void SaveMainInputToTextFile(string savefilename)
        {

            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter(savefilename);
            //Write a line of text
            sw.WriteLine("version info");
            sw.WriteLine("working directory");
            sw.WriteLine("family name");
            sw.WriteLine("scenario directory");
            sw.WriteLine("pfac");
            sw.WriteLine("options");
            sw.WriteLine("nchem");



            sw.WriteLine(sorption1.Text + ", " + sorption2.Text + ", " + sorption3.Text);
            sw.WriteLine(Nexp1Reg1.Text + ", " + Nexp2Reg1.Text + ", " + Nexp3Reg1.Text);
            sw.WriteLine(Kf1Reg2.Text + ", " + Kf2Reg2.Text + ", " + Kf3Reg2.Text);
            sw.WriteLine(Nexp1Reg2.Text + ", " + Nexp2Reg2.Text + ", " + Nexp3Reg2.Text);
            sw.WriteLine(MassTransferRegion2.Text + ", " + MassTransferRegion2Daughter.Text + ", " + MassTransferRegion2GrandDaughter.Text);
            sw.WriteLine(FreundlichMinimumConc.Text + ", " + SubTimeSteps.Text);

            sw.WriteLine(WaterColMetab1.Text + ", " + WaterColMetab2.Text + ", " + WaterColMetab3.Text);


            //Write a second line of text
            sw.WriteLine("From the StreamWriter class");
            //Close the file
            sw.Close();


        }

    }
}

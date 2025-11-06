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


            //SaveFileDialog saveFileDialog8 = new SaveFileDialog();
            //saveFileDialog8.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            //saveFileDialog8.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();
            
             

            SaveMainInputToTextFile(saveFileDialog1.FileName);




        }

        public void SaveMainInputToTextFile(string filename) 
        {

            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter(filename);
            //Write a line of text
            sw.WriteLine(sorption1.Text + ", " + sorption2.Text + ", " + sorption3.Text);
            sw.WriteLine(WaterColMetab1.Text + ", " + WaterColMetab2.Text + ", " + WaterColMetab3.Text);


            //Write a second line of text
            sw.WriteLine("From the StreamWriter class");
            //Close the file
            sw.Close();
        
        
        }





    }
}

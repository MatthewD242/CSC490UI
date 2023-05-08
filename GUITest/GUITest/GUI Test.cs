using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;

namespace GUITest
{
    public partial class Form1 : Form
    {
        //Initializes variables filePath and currentIndex for later use
        private string filePath = "";
        private int currentIndex = 0;

        //This part deserializes from XML to the UI to load into the input boxes
        private MyData DeserializeData(string path)
        {
            MyData data = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(MyData));
                using (StreamReader reader = new StreamReader(path))
                {
                    data = (MyData)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
            return data;
        }


        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Uses the desrialization class to put any values into the UI
            if (File.Exists(filePath))
            {
                MyData data = DeserializeData(filePath);
                if (data != null)
                {
                    numericUpDown1.Value = decimal.Parse(data.dryMass);
                    numericUpDown2.Value = decimal.Parse(data.pressure);
                    numericUpDown3.Value = decimal.Parse(data.mass);
                    numericUpDown4.Value = decimal.Parse(data.drag);
                    numericUpDown5.Value = decimal.Parse(data.time);
                    myTextBox1.Text = data.modelName;
                    textBox5.Text = data.massVTime;
                    textBox3.Text = data.thrustVTime;
                }
            }
        }

        private bool hasOpenFile = false;



        //Serializes data to an XML file for the Python to use
        private void SerializeData(string path)
        {
            if (path == null)
            {
                path = filePath;
            }
            // Create an instance of the object to be serialized
            MyData data = new MyData();
            data.dryMass = numericUpDown1.Value.ToString();
            data.pressure = numericUpDown2.Value.ToString();
            data.mass = numericUpDown3.Value.ToString();
            data.drag = numericUpDown4.Value.ToString();
            data.time = numericUpDown5.Value.ToString();
            data.modelName = myTextBox1.Text;
            data.massVTime = textBox5.Text;
            data.thrustVTime = textBox3.Text;

            try
            {
                //Serialize the object to an XML file
                XmlSerializer serializer = new XmlSerializer(typeof(MyData));
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
                {
                    serializer.Serialize(file, data);
                }

                Console.WriteLine("Serialization complete. Press any key to exit.");
                Console.Read();
                MessageBox.Show("Data saved successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message);
            }

            // Update the file path
            filePath = path;
        }

        //Declares more varaibles for various uses
        public class MyData
        {
            public string dryMass;
            public string pressure;
            public string mass;
            public string drag;
            public string modelName;
            public string massVTime;
            public string thrustVTime;
            public string time;
        }

        //This is so that a XMl file can be overwritten
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.xml";
            saveFileDialog.Title = "Save as XML File";
            saveFileDialog.FileName = "FlightModel-" + myTextBox1.Text; // Set the default file name
            SerializeData(filePath);
        }
        //To create a new XML file
        private void saveAsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text File|*.xml";
            saveFileDialog1.Title = "Save as XML File";
            saveFileDialog1.FileName = "FlightModel-" + myTextBox1.Text; // Set the default file name

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog1.FileName;  // Update the filePath field
                SerializeData(filePath); // Pass the filePath to SerializeData
            }
        }

        //Opens an XML file
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                MyData data = DeserializeData(openFileDialog.FileName);
                if (data != null)
                {
                    numericUpDown1.Value = decimal.Parse(data.dryMass);
                    numericUpDown2.Value = decimal.Parse(data.pressure);
                    numericUpDown3.Value = decimal.Parse(data.mass);
                    numericUpDown4.Value = decimal.Parse(data.drag);
                    numericUpDown5.Value = decimal.Parse(data.time);
                    myTextBox1.Text = data.modelName;
                    textBox5.Text = data.massVTime;
                    textBox3.Text = data.thrustVTime;

                    // Update the file path
                    filePath = openFileDialog.FileName;
                }
            }
        }

        //Sets myTextBox1 to only accept strings
        private void myTextBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(myTextBox1.Text) && !myTextBox1.Text.All(char.IsLetter))
            {
                MessageBox.Show("Please enter only letters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
        //Same as above, but for textBox5
        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox5.Text) && !textBox5.Text.All(char.IsLetter))
            {
                MessageBox.Show("Please enter only letters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
        //Same asa above for textBox3
        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text) && !textBox3.Text.All(char.IsLetter))
            {
                MessageBox.Show("Please enter only letters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
        
        //This loads the Python File, runs it, and displays the output
        //The console output is put into a textbox, while the images
        //Are loaded into a picture box, which can be cycled through
        //It also has a small amount of code so that the image that
        //Is being displayed is labeled
        private void pythonRunButton_Click(object sender, EventArgs e)

        {

            if (filePath != "")

            {

                string fileName = "FlightModel-" + myTextBox1.Text + ".xml";

                string location = @"C:\Users\Mebox\source\repos\GUITest\GUITest\rocketSimFinal\rocketSimFinal\rocketSimFinal.py";

                ProcessStartInfo start = new ProcessStartInfo();

                start.FileName = "python.exe"; // Path to Python

                start.Arguments = @"C:\Users\Mebox\source\repos\GUITest\GUITest\rocketSimFinal\rocketSimFinal\rocketSimFinal.py " + fileName;
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.WorkingDirectory = Path.GetDirectoryName(location);
                Process process = Process.Start(start);
                StreamReader reader = process.StandardOutput;
                process.WaitForExit();
                string result = reader.ReadToEnd();

                textBox1.Text = result;
                Image image2 = Image.FromFile(@"C:\Users\Mebox\source\repos\GUITest\GUITest\rocketSimFinal\rocketSimFinal\altitude.png");
                Image image3 = Image.FromFile(@"C:\Users\Mebox\source\repos\GUITest\GUITest\rocketSimFinal\rocketSimFinal\velocity.png");

                imageList1.Images.Add(image2);
                imageList1.Images.Add(image3);



                pictureBox1.Image = imageList1.Images[0];
                if (imageList1.Images.Count == 1)
                {
                    textBox2.Text = "Altitude:";
                }

                else if (imageList1.Images.Count == 2)
                {
                    textBox2.Text = "Velocity:";
                }
            }

        }
        //Cycles images, as mentioned above
        private void button1_Click(object sender, EventArgs e)
        {
            if (imageList1.Images.Count > 0)
            {
                currentIndex = (currentIndex + 1) % imageList1.Images.Count;
                pictureBox1.Image = imageList1.Images[currentIndex];
            }
            else
            {
                textBox1.Text = "NO IMAGES FOUND";
            }
        }
    }
}

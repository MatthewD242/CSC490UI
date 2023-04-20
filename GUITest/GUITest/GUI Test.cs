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
        private string filePath = "";

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
            if (File.Exists(filePath))
            {
                MyData data = DeserializeData(filePath);
                if (data != null)
                {
                    numericUpDown1.Value = decimal.Parse(data.dryMass);
                    numericUpDown2.Value = decimal.Parse(data.pressure);
                    numericUpDown3.Value = decimal.Parse(data.mass);
                    numericUpDown4.Value = decimal.Parse(data.drag);
                    myTextBox1.Text = data.modelName;
                    textBox5.Text = data.massVTime;
                    textBox3.Text = data.thrustVTime;
                }
            }
        }

        private bool hasOpenFile = false;

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
            data.modelName = myTextBox1.Text;
            data.massVTime = textBox5.Text;
            data.thrustVTime = textBox3.Text;

            try
            {
                // Serialize the object to an XML file
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


        private void label1_Click(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void myTextBox1_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void numericUpDown3_ValueChanged(object sender, EventArgs e) { }
        private void numericUpDown4_ValueChanged(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { }
        public class MyData
        {
            public string dryMass;
            public string pressure;
            public string mass;
            public string drag;
            public string modelName;
            public string massVTime;
            public string thrustVTime;
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.xml";
            saveFileDialog.Title = "Save as XML File";
            saveFileDialog.FileName = "FlightModel-" + myTextBox1.Text; // Set the default file name
            SerializeData(filePath);
        }

        private void saveAsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text File|*.xml";
            saveFileDialog1.Title = "Save as XML File";
            saveFileDialog1.FileName = "FlightModel-" + myTextBox1.Text; // Set the default file name

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog1.FileName;  // Update the filePath field
                SerializeData(filePath);
            }
        }


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
                    myTextBox1.Text = data.modelName;
                    textBox5.Text = data.massVTime;
                    textBox3.Text = data.thrustVTime;

                    // Update the file path
                    filePath = openFileDialog.FileName;
                }
            }
        }

        private void myTextBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(myTextBox1.Text) && !myTextBox1.Text.All(char.IsLetter))
            {
                MessageBox.Show("Please enter only letters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox5.Text) && !textBox5.Text.All(char.IsLetter))
            {
                MessageBox.Show("Please enter only letters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text) && !textBox3.Text.All(char.IsLetter))
            {
                MessageBox.Show("Please enter only letters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }


        // ...
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        

       

        private void pythonRunButton_Click(object sender, EventArgs e)
        {
            // code to execute when pythonRunButton is clicked
            // Set up the process info for running the Python script
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python.exe"; // The path to your Python interpreter
            start.Arguments = "C:\\Users\\Mebox\\source\\repos\\GUITest\\GUITest\\TEST.py"; // The name of the Python script to execute
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    textBox1.Text = result;
                    
                    
                }                
            }
            callPicture();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
        private void callPicture()
        {
            Image image = Image.FromFile("C:\\Users\\Mebox\\source\\repos\\GUITest\\GUITest\\foo.png");
            pictureBox1.Image = image;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}

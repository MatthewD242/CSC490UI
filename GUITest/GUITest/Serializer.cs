using GUITest;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using static GUITest.Form1;

public class serializerForInput
{
    public string inputToBeSer;
}

public class Program
{
    static void Main(string[] args)
    {
        // Create an instance of the form that contains the input boxes
        Form1 inputForm = new Form1();
        DialogResult result = inputForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            MyData data = new MyData();

            // Get input from input boxes and set fields in the MyData object
            data.dryMass = inputForm.numericUpDown1.Value.ToString();
            data.pressure = inputForm.numericUpDown2.Value.ToString();
            data.mass = inputForm.numericUpDown3.Value.ToString();
            data.drag = inputForm.numericUpDown4.Value.ToString();
            data.massVTime = inputForm.textBox5.Text;
            data.thrustVTime = inputForm.textBox3.Text;

            // Get file name from input box
            string fileName = inputForm.myTextBox1.Text;

            // Serialize the object
            XmlSerializer serializer = new XmlSerializer(typeof(MyData));
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Mebox\Desktop\C#\data.xml"))
            {
                serializer.Serialize(file, data);
            }

            Console.WriteLine("Serialization complete. Press any key to exit.");
            Console.ReadKey();
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace List
{
    public partial class Form1 : Form
    {
        string path = "NeedText.txt";
        StreamReader streamReader = null;
        string text = null;
        public Form1()
        {
            InitializeComponent();
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            streamReader = new StreamReader(path, Encoding.UTF8);
            text = streamReader.ReadToEnd();
            streamReader.Close();
            progressBar1.Value = 0;
            textBox1.Text = text;

            int sizeText = textBox1.Text.Length;
            int capasityText = 1000;
            progressBar1.Value = (100 * sizeText) / capasityText;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(textBox1.Text);
            }
        }
    }
}

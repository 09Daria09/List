using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Anketa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("txt");
            comboBox1.Items.Add("xml");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string surname = textBox2.Text;
            string email = maskedTextBox1.Text;
            string phone = maskedTextBox2.Text.Replace("+380", "").Replace(" ", "").Replace("-", "");

            listBox1.Items.Add(name + " " + surname + " " + email + " " + phone);
            textBox1.Clear();
            textBox2.Clear();
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string selectedValue = listBox1.SelectedItem.ToString();
            string[] parts = selectedValue.Split(' ');

            textBox1.Text = parts[0].Trim();
            textBox2.Text = parts[1].Trim();
            maskedTextBox1.Text = parts[2].Trim();
            maskedTextBox2.Text = "+380" + Regex.Replace(parts[3].Trim(), @"(\d{3})(\d{2})(\d{2})(\d{3})", "$1 $2 $3 $4");

            listBox1.Items.Remove(selectedValue);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedValue = listBox1.SelectedItem.ToString();
            listBox1.Items.Remove(selectedValue);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();

            if (selectedValue == "txt")
            {
                string fileName = "listBoxData.txt";
                string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (string item in listBox1.Items)
                    {
                        writer.WriteLine(item);
                    }
                }
                listBox1.Items.Add(fileName); 
                MessageBox.Show("Список сохранен в файл: " + filePath);
                
            }
            else if (selectedValue == "xml")
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<string>));

                using (StreamWriter writer = new StreamWriter("listBoxData.xml"))
                {
                    serializer.Serialize(writer, listBox1.Items.Cast<string>().ToList());
                }

                MessageBox.Show("Данные успешно сохранены в файл listBoxData.xml");
            }
        }

    }
}

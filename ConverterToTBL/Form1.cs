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

namespace ConverterToTBL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox2.Text != "")
            {
                string file = textBox1.Text;
                string newFile = textBox2.Text;
                if (file.Contains(".gb"))
                {
                    //Converter converter = new Converter();
                    //converter.readFile(file, newFile);
                    //Console.WriteLine(converter.getFile());
                    //ConvertGbToTBL convertGbToTBL = new ConvertGbToTBL();
                    //convertGbToTBL.readFile(file, newFile);
                    ConverterGbv2 converterGbv2 = new ConverterGbv2();
                    converterGbv2.readFile(file, newFile);
                }
                else if (file.Contains(".gff"))
                {
                    ConvertGffToTBL converter = new ConvertGffToTBL();
                    converter.readFile(file, newFile);
                    //Console.WriteLine(converter.getFile());
                }
                
                //FileStream fs = (FileStream) saveFileDialog1.OpenFile();
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Choose file to convert");
            }
            else if (textBox2.Text =="")
            {
                MessageBox.Show("Choose place to save a new tbl file");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            textBox1.Text = openFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            textBox2.Text = saveFileDialog1.FileName;
        }
    }
}

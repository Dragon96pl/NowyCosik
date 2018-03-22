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
        public List<CheckBox> checkBoxList;
        public Form1()
        {
            InitializeComponent();
            FileManagement.StartString = "FEATURES";
            FileManagement.EndString = "//";
            this.checkBoxList = new List<CheckBox>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                
                string file = textBox1.Text;
                string newFile = textBox2.Text;
                if (file.Contains(".gb"))
                {
                    HashSet<string> keys = new HashSet<string>();
                    foreach (var obj in this.checkBoxList)
                        if (obj.Checked)
                            keys.Add(obj.Text);
                    HashSet<string> names = new HashSet<string>();
                    foreach (var obj in this.checkBoxList)
                        if (obj.Checked && obj.Name.Contains("NAME_"))
                            names.Add(obj.Text);
                    //Converter converter = new Converter();
                    //converter.readFile(file, newFile);
                    //Console.WriteLine(converter.getFile());
                    //ConvertGbToTBL convertGbToTBL = new ConvertGbToTBL();
                    //convertGbToTBL.readFile(file, newFile);
                    ConverterGbv2 converterGbv2 = new ConverterGbv2();
                    converterGbv2.readFile(file, newFile,keys,names);
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

        private void analyzeBtn_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                string file = textBox1.Text;
                AnalyzeGb.getKeHashSet(file);
                if (this.checkBoxList.FindAll(x => x.Name.Contains("KEY_")).Count > 0){
                    foreach(var checkbox in this.checkBoxList.FindAll(x => x.Name.Contains("KEY_")))
                    {
                        this.Controls.Remove(checkbox);
                    }
                    this.checkBoxList.RemoveAll(x => x.Name.Contains("KEY_"));
                }
                if (this.checkBoxList.FindAll(x => x.Name.Contains("NAME_")).Count > 0)
                {
                    foreach (var checkbox in this.checkBoxList.FindAll(x => x.Name.Contains("NAME_")))
                    {
                        this.Controls.Remove(checkbox);
                    }
                    this.checkBoxList.RemoveAll(x => x.Name.Contains("NAME_"));
                }
                int top = 25;
                int id = 0;
                foreach(var key in AnalyzeGb.Keys){
                    CheckBox chkBox = new CheckBox();
                    this.Controls.Add(chkBox);
                    chkBox.Name = "KEY_"+key;
                    chkBox.Top = 10 + top * id++;
                    chkBox.Left = 280;
                    chkBox.Text = key;
                    this.checkBoxList.Add(chkBox);
                    
                }
                top = 25;
                id = 0;
                foreach (var name in AnalyzeGb.Names)
                {
                    CheckBox chkBox = new CheckBox();
                    this.Controls.Add(chkBox);
                    chkBox.Name = "NAME_" + name;
                    chkBox.Top = 10 + top * id++;
                    chkBox.Left = 420;
                    chkBox.Text = name;
                    this.checkBoxList.Add(chkBox);

                }
            }
            
        }
    }
}

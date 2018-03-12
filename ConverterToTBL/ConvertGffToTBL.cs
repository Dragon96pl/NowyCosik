using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterToTBL
{
    class ConvertGffToTBL
    {
        List<TblFromGff> sequences = new List<TblFromGff>();

        public string newFile { get; set; }
        public string readFile(string fileName, string newFileName)
        {
            string s = "";
            using (StreamReader sr = File.OpenText(fileName))
            {
                TblFromGff sequence = new TblFromGff();
                int i = 0;
                int j = 0;
                string id = "";
                string name = "";
                while ((s = sr.ReadLine()) != null)
                {       
                    if (s[0] != '#')
                    {
                        string[] row = s.Split('\t');
                        if(!id.Contains(row[0]))
                        {
                            if(i != 0)
                                this.sequences.Add(sequence);
                            sequence = new TblFromGff();
                            sequence.Sequence = ">Feature Seq" + i;
                            i++;
                            id = row[0];
                            name = "";
                        }
                       
                        TblRowGff tbl = new TblRowGff();
                        if (row[6].Contains("-"))
                        {
                            tbl.FirstColumn = row[4];
                            tbl.SecondColumn = row[3];
                        }
                        else
                        {
                            tbl.FirstColumn = row[3];
                            tbl.SecondColumn = row[4];
                        }
                        if (row[2].Equals("gene"))
                            tbl.FourthColumn = "gene";
                        else
                            tbl.FourthColumn = "product";
                        tbl.ThirdColumn = row[2];
                        if (row[8].Contains(';') && name == "")
                        {
                            name = row[8].Substring(row[8].IndexOf("=") + 1, row[8].IndexOf(";") - row[8].IndexOf("=") -1 );
                            Console.WriteLine("Asd " + row[8].IndexOf(";"));
                        }
                            
                       
                        else if(!row[8].Contains(';') && name == "" )
                            name = row[8].Substring(row[8].IndexOf("=") + 1);
                        tbl.FifthColumn = name;
                        sequence.rows.Add(tbl);
                        
                    }
                    
                    
                    
                }
                this.sequences.Add(sequence);
                Console.WriteLine(this.getFile());
                this.newFile = getFile();
                if (!newFileName.Contains(".tbl"))
                    newFileName += ".tbl";
                if (File.Exists(newFileName))
                    File.Delete(newFileName);
                using (FileStream fs = File.Create(newFileName))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(this.newFile);
                    fs.Write(info, 0, info.Length);
                    System.Windows.Forms.MessageBox.Show("Created");
                }

                return s;
            }
        }

        public String getFile()
        {
            String results = "";
            //for (int i = 0; i < this.tblList.Count; i++)
            //    results += tblList[i].toString();
            foreach (TblFromGff sequence in this.sequences)
            {
                results += sequence.ToString();
            }
            return results;
        }
        public ConvertGffToTBL()
        {
            this.sequences = new List<TblFromGff>();
        }
    }
}

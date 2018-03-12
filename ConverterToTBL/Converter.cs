using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterToTBL
{
    class Converter
    {
        List<TBL> tblList = new List<TBL>();
        public string newFile { get; set; }
        public string readFile(string fileName, string newFileName)
        {
            string s = "";
            using (StreamReader sr = File.OpenText(fileName))
            {
                TBL tbl = new TBL();
                int id = 0;
                int i = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    if (s.Contains("LOCUS"))
                    {
                        if (id != 0)
                            tblList.Add(tbl);
                        tbl = new TBL();
                        id++;
                        i = 0;
                        tbl.name = "Feature Seq " + id.ToString();
                    }

                    if (i == 9)
                    {
                        s = s.Trim();
                        tbl.column7 = s.Substring(0, s.IndexOf(" "));
                        s = s.Substring(s.IndexOf(" "));
                        s = s.Trim();
                        
                        String part1 = s.Substring((s.IndexOf("(") + 1) );
                        Console.WriteLine(part1.IndexOf(","));
                        part1 = part1.Substring(0, part1.IndexOf(","));
                        tbl.column5 = part1.Substring(0, part1.IndexOf(".") );
                        tbl.column6 = part1.Substring(part1.LastIndexOf(".") + 1);
                        part1 = s.Substring(s.IndexOf(",") + 1);
                        part1 = part1.Substring(0, part1.LastIndexOf(")"));
                        tbl.column8 = part1.Substring(0, part1.IndexOf("."));
                        tbl.column9 = part1.Substring(part1.LastIndexOf(".") + 1);
                        

                    }
                    if(i == 10)
                    {
                        s = s.Trim();
                        s = s.Substring(1);
                        tbl.column10 = s.Substring(0, s.IndexOf("="));
                        s = s.Substring(s.IndexOf("=")+1);
                        tbl.column11 = s.Substring(1, s.Length - 2);
                    }
                    if(i == 13)
                    {
                        s = s.Trim();
                        s = s.Substring(1);
                        tbl.column12 = s.Substring(0, s.IndexOf("="));
                        s = s.Substring(s.IndexOf("=")+1);
                        tbl.column13 = s.Substring(1, s.Length-2);
                    }
                    if (i == 17)
                    {
                        s = s.Trim();
                        tbl.column2 = s.Substring(0, s.IndexOf(" "));
                        s = s.Substring(s.IndexOf(" "));
                        s = s.Trim();
                        tbl.column0 = s.Substring(0, s.IndexOf("."));
                        tbl.column1 = s.Substring(s.LastIndexOf(".") + 1);
                        //tbl.column4 = s.
                    }
                    if(i == 18)
                    {
                        try
                        {
                            s = s.Trim();
                            s = s.Substring(1);
                            tbl.column3 = s.Substring(0, s.IndexOf("="));
                            s = s.Substring(s.IndexOf("=") + 1);
                            tbl.column4 = s.Substring(1, s.Length - 2);
                        }
                        catch(Exception e) { }
                    }
                    i++;

                }
                tblList.Add(tbl);
                //Console.WriteLine(tblList.ToString());
                this.newFile = getFile();
                if (!newFileName.Contains(".tbl"))
                    newFileName += ".tbl";
                if (File.Exists(newFileName))
                    File.Delete(newFileName);
                using(FileStream fs = File.Create(newFileName))
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
            for (int i = 0; i < this.tblList.Count; i++)
                results += tblList[i].toString();
            return results;
        }
    }
}

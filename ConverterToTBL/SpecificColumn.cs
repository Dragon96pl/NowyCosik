using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterToTBL
{
    class SpecificColumn
    {
        public List<string> Name { get; set; }
        public List<string> Value { get; set; }
        public string Key { get; set; }     
        public string From { get; set; }
        public string To { get; set; }
        public SpecificColumn(string key,string from, string to, List<string> name, List<string> value)
        {
            this.Key = key;
            this.Name = name;
            this.Value = value;
            this.From = from;
            this.To = to;
        }
        public SpecificColumn()
        {

        }

        public override string ToString()
        {
            //komentarz dla gita
            //string results = "Key = " + this.Key + "\nFrom = "+ this.From + 
            //    " ; To = " + this.To + "\n";
            string results = this.From + "\t" + this.To + "\t" + this.Key+"\n";
            for (int i = 0; i < this.Name.Count(); i++)
                 results += "\t\t\t\t" + this.Name[i] + "\t" + this.Value[i]+"\n";
            return results;
        }
    }
}

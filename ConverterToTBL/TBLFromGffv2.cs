using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterToTBL
{
    class TBLFromGffv2
    {
        public String name { get; set; }
        public String column0 { get; set; }
        public String column1 { get; set; }
        public String column2 { get; set; }
        public List<List<string>> columnNameList { get; set; }
        public List<List<string>> columnValueList { get; set; }

        public String toString()
        {
            String result = ">" + this.name + "\n" +
                column0 + "\t" + column1 + "\t" + column2 + "\n";
            for (int i = 0; i < this.columnNameList.Count; i++)
                for (int j = 0; j < this.columnNameList[i].Count; j++)
                    result += "\t\t\t" + columnNameList[i][j] + "\t" + columnValueList[i][j] + "\n";
            //result += "\n" + columnNameList.Count.ToString() + " ==> " + columnValueList.Count.ToString() + "\n";
            result +="\n";

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterToTBL
{
    class TBLForSpecificColumn
    {
        public List<SpecificColumn> columns = new List<SpecificColumn>();
        public string mainName { get; set; }

        public override string ToString()
        {
            string results = ">" + this.mainName+"\n";
            foreach (var obj in this.columns)
                results += obj.ToString();
            return results;
        }
    }
}

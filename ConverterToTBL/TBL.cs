using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterToTBL
{
    class TBL
    {
        public String name { get; set; }
        public String column0 { get; set; }
        public String column1 { get; set; }
        public String column2 { get; set; }
        public String column3 { get; set; }
        public String column4 { get; set; }
        public String column5 { get; set; }
        public String column6 { get; set; }
        public String column7 { get; set; }
        public String column8 { get; set; }
        public String column9 { get; set; }
        public String column10 { get; set; }
        public String column11 { get; set; }
        public String column12 { get; set; }
        public String column13 { get; set; }

        public String toString()
        {
            String result = ">" + this.name + "\n" +
                column0 + "\t" + column1 + "\t" + column2 + "\n" +
                "\t\t\t" + column3 + "\t" + column4 + "\n" +
                column5 + "\t" + column6 + "\t" + column7 + "\n" +
                column8 + "\t" + column9 + "\n" +
                "\t\t\t" + column10 + "\t" + column11 + "\n" +
                "\t\t\t" + column12 + "\t" + column13 +
                "\n";

            return result;
        }
    }
}

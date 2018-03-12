using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterToTBL
{
    class TblFromGff
    {
        public string Sequence { get; set; }
        public List<TblRowGff> rows { get; set; }
        public TblFromGff()
        {
            this.rows = new List<TblRowGff>();
        }
        public override string ToString()
        {
            string result = "";
            result += this.Sequence + " \n";
            foreach (TblRowGff row in this.rows) {
                result += row.FirstColumn + "\t" +
                    row.SecondColumn + " \t" +
                    row.ThirdColumn + "\n\t\t\t" +
                    row.FourthColumn + "\t" +
                    row.FifthColumn + "\n";
            }

            return result;
        }
    }
}

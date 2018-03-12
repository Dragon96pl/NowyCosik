using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterToTBL
{
    static class StringUtils
    {
        static public String getPieceOFString(String txt, String begin, String end)
        {
            return txt.Substring(0, 1);
        }
        static public String substringOfString (String text, String from, String to)
        {
            return text.Substring(text.IndexOf(from), text.IndexOf(to));
        }
    }
}

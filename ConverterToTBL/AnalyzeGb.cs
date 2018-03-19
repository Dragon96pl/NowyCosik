using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConverterToTBL
{
    class AnalyzeGb
    {
        static public HashSet<string> Keys { get; set; }
        static public void getKeHashSet(string fileName){
            AnalyzeGb.Keys = new HashSet<string>();
            var start = false;
            var lines = File.ReadLines(fileName);
            foreach (var line in lines){
                string singleLine = line.Trim(); // usuniecie bialych spacji z poczatku i konca lini
                singleLine = Regex.Replace(singleLine, " {2,}", "\t"); //zastapienie wiecej niz 2 spacji pod rzad znakiem tabulacji
                string[] splitLine = singleLine.Split('\t'); // rozdzielenie jako elementy tablicy lini na podstawie znaku tabulacji
                if (splitLine[0].Contains(FileManagement.EndString)) //jezeli linia zawiera ciag znakow konca algorytmu zmienna start = false
                    start = false;
                if (start)
                    if (splitLine.Length > 1)
                        AnalyzeGb.Keys.Add(splitLine[0]);
                if (splitLine[0].Contains(FileManagement.StartString))
                    start = true;
            };
            foreach (var e in AnalyzeGb.Keys)
                Console.WriteLine(e);
        }
    }
}

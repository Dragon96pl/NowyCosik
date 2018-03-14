using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConverterToTBL
{
    class ConverterGbv2
    {
        public List<TBLForSpecificColumn> tbl = new List<TBLForSpecificColumn>(); //zmienna wynikowa
        public string readFile(string fileName, string newFileName)
        {
            var lines = File.ReadLines(fileName); //zczytanie wszystkich lini pliku
            Boolean start = false; //zmienna start odpowiedzialna za uruchomienie alorytmu analizy pliku
            string startString = "FEATURES"; // zmienna zawierajaca ciag znakow odpowiedzialny za rozpoczecie analizy
            string endString = "//"; // zmienna zawierajaca ciag znakow odpowiedzialny za zatrzymanie analizy pliku
            string key = ""; 
            List<string> names = new List<string>();
            List<string> values = new List<string>();
            string value = "";
            string from = "";
            string to = "";
            int idOfFeatures = 1;
            List<SpecificColumn> listOfValues = new List<SpecificColumn>(); //Lista przechowujaca kolejne wiersze Features
            foreach (var line in lines)
            {
                string singleLine = line.Trim(); // usuniecie bialych spacji z poczatku i konca lini
                singleLine = Regex.Replace(singleLine, " {2,}", "\t"); //zastapienie wiecej niz 2 spacji pod rzad znakiem tabulacji
                string[] splitLine = singleLine.Split('\t'); // rozdzielenie jako elementy tablicy lini na podstawie znaku tabulacji
                if (splitLine[0].Contains(endString)) //jezeli linia zawiera ciag znakow konca algorytmu zmienna start = false
                {
                    start = false;
                    values.Add(value); 
                    SpecificColumn column = new SpecificColumn(); //stworzenie nowego obiektu SpecificColumn z aktualnymi wartosciami
                    column.From = from;
                    column.Key = key;
                    column.To = to;
                    column.Name = names;
                    column.Value = values;
                    listOfValues.Add(column);
                    names = new List<string>();
                    values = new List<string>();
                    value = "";
                    key = "";
                    TBLForSpecificColumn tblTemp = new TBLForSpecificColumn(); //stworzenie obiektu TBL
                    tblTemp.mainName = "Features" + idOfFeatures.ToString();
                    idOfFeatures++;
                    listOfValues.ForEach(element => tblTemp.columns.Add(element));
                    this.tbl.Add(tblTemp); // dodanie nowego TBL do wszystkich obiektow TBL
                    listOfValues = new List<SpecificColumn>();
                }

                if (start)
                {
                    if (splitLine.Length > 1) // jezeli linia zawiera slowo kluczowe plus wartosci to
                    {
                        if (key != "") //jezeli klucz nie jest pusty
                        {
                            values.Add(value); //dodaj do listy wartosci aktualna wartosc
                            SpecificColumn column = new SpecificColumn(); //stworzenie nowego obiektu specific column odpowiadajacego wierszowi np. CDS
                            column.From = from;
                            column.Key = key;
                            column.To = to;
                            column.Name = names;
                            column.Value = values;
                            listOfValues.Add(column);
                            names = new List<string>();
                            values = new List<string>();
                            value = "";

                        }
                        key = splitLine[0]; //zczytanie nowego klucz np. CDS
                        var res = this.getNumbers(splitLine[1]);
                        //from = splitLine[1].Substring(0, splitLine[1].IndexOf("."));
                        //to = splitLine[1].Substring(splitLine[1].LastIndexOf(".") + 1);
                        //from = from.Replace("\\D+", "");
                        //from = from.Replace("(", "");
                        //to = to.Replace("\\D+", "");
                        //from = this.replaceAll(from);
                        //to = this.replaceAll(to);
                        //Regex pattern = new Regex("[a-zA-Z]");
                        //to = pattern.Replace(to, "");
                        //from = pattern.Replace(from, "");
                        from = res[0];
                        to = res[1];
                    }
                    else
                    {
                        if (splitLine[0].First() == '/' && splitLine[0].Contains('='))
                        {
                            names.Add(splitLine[0].Substring(1, splitLine[0].IndexOf('=') - 1));
                            if (value != "")
                                values.Add(value);
                            value = this.replaceAll(splitLine[0].Substring(splitLine[0].IndexOf('=') + 1));
                        }
                        else
                        {
                            value += " " + this.replaceAll(splitLine[0]);
                        }

                    }
                }

                if (splitLine[0].Contains(startString)) // jesli linia zawiera ciag znakow rozpoczynajacy analize pliku
                    start = true; // zmienna start jest rowna true

            } //koniec petli foreach
            //for (int i = 0; i < values.Count; i++)
            //    Console.WriteLine(names[i] + " ==> " + values[i]);
            string results = "";
            foreach (var tbl in this.tbl) //zczytanie wszystkich features do jednej zmiennej w celu jej zapisania
                results += tbl.ToString() + "\n"; 
            if (!newFileName.Contains(".tbl")) //dodanie do nowego pliku rozszerzenia tbl jesli takowego nie posiada
                newFileName += ".tbl";
            if (File.Exists(newFileName)) //jezeli plik istnieje o podanej nazwie
                File.Delete(newFileName); //zostanie nadpisany
            using (FileStream fs = File.Create(newFileName))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(results); //zmiana zmiennej wynikowej na tablice bajtow
                fs.Write(info, 0, info.Length); //zapisanie wynikow do pliku
                System.Windows.Forms.MessageBox.Show("Created"); //wyswietlenie okna ze plik zostal utworzony
            }
            return "";
        }

        //metoda usuwajaca konkretne znaki ze stringa
        protected string replaceAll(string value)
        {
            value = value.Replace("\"", "");
            value = value.Replace("(", "");
            value = value.Replace(")", "");
            value = value.Replace("=", "");
            value = value.Replace("/", "");
            value = value.Replace("\\", "");
            value = value.Replace("\"", "");
            return value;
        }
        //Metoda wyciagajaca liczby from i to ze stringa
        protected string[] getNumbers(string text)
        {
            string[] results = new string[2];
            string temp = "";
            int id = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsDigit(text[i]))
                    temp += text[i];
                else if (id >= 2)
                    return results;
                else if (!Char.IsDigit(text[i]) && temp != "")
                {
                    results[id++] = temp;
                    
                    temp = "";
                }
            }
            if(id < 2)
            results[id] = temp;

            return results;
        }
    }

}

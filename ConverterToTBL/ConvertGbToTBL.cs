using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConverterToTBL
{
    class ConvertGbToTBL
    {
        List<TBLFromGffv2> tblList = new List<TBLFromGffv2>();
        public string newFile { get; set; }
        public Dictionary<Int32, Dictionary<string, List<string>>> resultsOfRead { get; set; }
        public string readFile(string fileName, string newFileName)
        {
            using(StreamReader streamReader = File.OpenText(fileName))
            {
                TBLFromGffv2 tblFromGff = new TBLFromGffv2();
                String temp = streamReader.ReadLine();
                temp = Regex.Replace(temp, " {2,}", "\t");
                String[] results = temp.Split('\t');
                var resultsMap = new Dictionary<Int32, Dictionary<string, List<string>>>();
                int id = 0;
                string key = "";
                string keyValues = "";
                List<string> value = new List<string>();
                while (results != null)
                {
                    if (results[0].Contains("FEATURES"))
                    {
                        int start = 1;
                        while(start == 1)
                        {
                            try
                            {
                                temp = streamReader.ReadLine();
                                try { temp = Regex.Replace(temp, " {2,}", "\t"); } catch { }
                                results = temp.Split('\t');
                               
                                if (!results[0].Contains(""))
                                {
                                    start = 0;
                                    break;
                                }
                                //if (results[0] == "")
                                //{
                                //    if (!results[1].Contains("/"))
                                //        Console.WriteLine("\n"+results[1]);
                                //    else
                                //        Console.Write(results[1]);
                                //}
                                if (results[0].Contains("//"))
                                {
                                    Console.WriteLine("New features \n\n");
                                    start = 0;
                                    break;
                                }
                                if (results.Length > 2)
                                {
                                    if (id > 0)
                                    {
                                        var tempDictionary = new Dictionary<string, List<string>>();
                                        tempDictionary.Add(key, value);
                                        resultsMap.Add(id, tempDictionary);
                                        value.Clear();
                                        keyValues = "";
                                    }
                                    id++;
                                    key = results[1];
                                    value.Add(results[2]);
                                }
                                else
                                {
                                    if (results[1].Contains("/"))
                                    {
                                        value.Add(keyValues.Substring(1,keyValues.Length));
                                        keyValues = "";
                                        keyValues += results[1];
                                    }
                                    else
                                    {
                                        keyValues += results[1];
                                       // Console.WriteLine("==>" + keyValues);
                                    }
                                        
                                }
                            }
                            catch(Exception e)
                            {
                                start = 0;
                                break;
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            temp = streamReader.ReadLine();
                            temp = Regex.Replace(temp, " {2,}", "\t");
                            results = temp.Split('\t');
                        }
                        catch (Exception e)
                        {
                            break;
                        }
                    }
                }
                this.resultsOfRead = resultsMap;
                string nameOfTBL = "Features";
                int idOfTBL = 1;
                foreach (KeyValuePair<Int32, Dictionary<string, List<string>>> element in resultsMap)
                {
                    TBLFromGffv2 tbl = new TBLFromGffv2();
                    tbl.columnNameList = new List<List<string>>();
                    tbl.columnValueList = new List<List<string>>();
                    tbl.name = nameOfTBL + idOfTBL.ToString();
                    idOfTBL++;
                    
                    
                    foreach(KeyValuePair<string, List<string>> obj in element.Value)
                    {
                        tbl.column2 = obj.Key;
                        id = 0;
                        List<string> nameList = new List<string>();
                        List<string> valueList = new List<string>();
                        foreach (string valueOfList in obj.Value)
                        {
                            
                            if(id == 0)
                            {
                                string a = valueOfList.Substring(0, valueOfList.IndexOf("."));
                                string b = valueOfList.Substring(valueOfList.LastIndexOf(".")+1);
                                a = a.Replace("\\D+", "");
                                b = b.Replace("\\D+", "");
                                //Console.WriteLine(a + " " + b);
                                tbl.column0 = a;
                                tbl.column1 = b;
                                //Console.WriteLine(valueOfList);
                            }
                            else
                            {
                                if (valueOfList.Contains("="))
                                {
                                    nameList.Add(valueOfList.Substring(0, valueOfList.IndexOf("=")));
                                valueList.Add(valueOfList.Substring(valueOfList.IndexOf("=") + 1));
                                }
                                
                            }
                            //Console.WriteLine(valueOfList);
                            id++;
                        }
                        tbl.columnNameList.Add(nameList);
                        tbl.columnValueList.Add(valueList);
                    }
                    //Console.WriteLine(tbl.toString());
                    this.tblList.Add(tbl);
                }
            }
            return null;
        }
    }


}


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode2020.Day4
{

    //Passport Processing
    public class Day4
    {
        const string filePath = @"InputData\passport_input.txt";
        
        //Valid Passport headers
        //All titles are essential apart from Country ID (cid).
        static Dictionary<string, bool> PassportTitles = new Dictionary<string, bool> 
        {
            {"byr", true }, {"iyr", true }, {"eyr", true },{"hgt", true },{"hcl", true },{"ecl", true }, {"pid", true },{"cid", false }
        };
        

        private static string ReadData(string filePath)
        {
            string lines = null;
            using (var sr = new StreamReader(filePath)) 
            {
               lines = sr.ReadToEnd();
            }
            return lines;    
        }

        /*var ParsedPassports = passportParts.Select(x => x.Split(':').Select(x => new { Key = x[0], Value = x[1] })).ToList();
        //loop over values and check if it parses.
        foreach (var field in ParsedPassports)
        {
            var myKey = field.Where(x => x.Key == x.Key);
            if (PassportTitles.TryGetValue(field.Where(x = > x.Key == x), out bool required)) 
            {

            }
        }*/

        static List<string> ParsePassportData() 
        {
            string testFile = @"InputData\passport_input.txt";
            List<string> parsedData = new List<string>();
            string UnsortedPassports = ReadData(filePath);
            //Move character to the beginning of the line.
            string[] tempArr = UnsortedPassports.Replace("\r","").Replace("\n\n", "@").Split(new char[] {'@'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in tempArr) 
            {
                string[] passportParts = s.Split(new char[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < passportParts.Length; i++) 
                {
                    parsedData.Add(passportParts[i]);
                }

            }
            return parsedData;
        }

        public static int SolutionToPart1() 
        {
            //check for the following fields
            //byr
            //hgt 
            int validPassports = 0;
            int optionalFieldCount = 0;
            int requiredPassportCount = 0; //Number of required fields found in the data. 
            int expectedPassportCount = PassportTitles.Select(x => x.Value).Count(); //Count the number of expected fields 
            var PassportToCheck = new List<Tuple<string, string>>();
            var PassportDataList = ParsePassportData();

            foreach (string pInfo in PassportDataList) 
            {
                string[] prepareData = pInfo.Split(':');
                PassportToCheck.Add(Tuple.Create(prepareData[0], prepareData[1])); 
            }

            foreach (Tuple<string, string> field in PassportToCheck) 
            {
                if (PassportTitles.TryGetValue(field.Item1, out bool required))
                {
                    if (required) requiredPassportCount++;
                    else optionalFieldCount++;
                }
                else 
                {
                    Console.WriteLine($"Unrecogrnised field {field.Item1}!");
                }

                if (requiredPassportCount == expectedPassportCount) validPassports++;
            }
            //Dictionary<string, string> PassportsToCheck = ParsePassportData();
          
            
            //List<string> PassportList
           /* foreach (KeyValuePair<string,string> dic in PassportsToCheck)
            {
                if (dic.Key == "byr" || dic.Key == "hgt") 
                {
                    validPassports++;
                }
            }*/


            Console.WriteLine("Number of Valid passports: {0}", validPassports);
            return validPassports;
        }

    }
}

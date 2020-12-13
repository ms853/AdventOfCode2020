using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode2020.Day4
{

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

        private static bool ValidatePassport(string passportUnsorted) 
        {
            var passport = passportUnsorted.Replace('\n', ' ');

            if (!passport.Contains("byr")) return false;
            if (!passport.Contains("hgt")) return false;

            if (!passport.Contains("cid") && passport.Length == 7) return true;
                
            return true;
        }

        public static void Part1() 
        {
            int validPassport = 0;

            string testFile = @"InputData\testFile4.txt";
            List<string> parsedData = new List<string>();
            string UnsortedPassports = ReadData(filePath);
            //Move character to the beginning of the line.
            string tempData = UnsortedPassports.Replace("\r", "");
            //.Replace("\n\n", "@").Split(new char[] {'@'}, StringSplitOptions.RemoveEmptyEntries);
            var noNewLinesInData = tempData.Replace("\n\n", "@");
            var PassportInfo = noNewLinesInData.Split(new char[] { '@'});

            foreach (string p in PassportInfo) 
            {
                if (ValidatePassport(p)) 
                {
                    validPassport++;
                }
            }
          
            Console.WriteLine($"The number of valid passports are: {validPassport}");
            
        }

       

    }
}

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
        const string filePath = @"InputData\passport.txt";
        
        //Valid Passport headers
        //All titles are essential apart from Country ID (cid).
       
        private static List<string> ReadData(string filePath)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            return lines;    
        }

        public static void Part1()
        {
            Dictionary<string, string> passportFields = new Dictionary<string, string>();

            int validPassportCount = 0;
            string[] RequiredFields = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            string testFile = @"InputData\testFile4.txt";

            List<string> rawPassportData = ReadData(filePath);

            foreach (string data in rawPassportData)
            {
                //This check ensures that each group is read and added to the data.
                if (data.Equals(""))
                {
                    bool valid = true; //initially true until passport field is not found. 
                    foreach (var key in RequiredFields)
                    {
                        if (!passportFields.ContainsKey(key))
                        {
                            valid = false;
                            break;
                        }
                    }

                    if (valid)
                    {
                        validPassportCount++;
                    }

                    passportFields.Clear(); //Clear data
                }
                else
                {
                    Console.WriteLine($"Data is: {data}");
                    var fieldList = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var s in fieldList) 
                    {
                        string[] fieldParts = s.Split(':');
                        passportFields.Add(fieldParts[0], fieldParts[1]);
                       
                    }
   
                }
            
            }
            Console.WriteLine($"Total valid passport data is: {validPassportCount}");
        }

        public static void Part2() 
        {
            
        }

       

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

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

        private static bool VerifyPassportGroup(Dictionary<string, string> passports, string key) 
        {

            if (!passports.ContainsKey(key))
            {
                return false;
            }

            if (passports.ContainsKey(key))
            {
                if (ValidatePassportData(key, passports[key]))
                   // validPassports++;
                    return true;
                else
                    return false;

            }

            return true;
        }

        public static void Day4Solution()
        {
            List<Dictionary<string, string>> ListofPassports = new List<Dictionary<string, string>>();
            Dictionary<string, string> passportFields = new Dictionary<string, string>();
            
            bool valid = false;
            int validPassportCount = 0;
            string[] RequiredFields = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            
            string testFile = @"InputData\testFile4.txt";

            List<string> rawPassportData = ReadData(filePath);

            foreach (string data in rawPassportData)
            {
                
                //This check ensures that each group is read and added to the data.
                if (data.Equals(""))
                {

                    foreach (string key in RequiredFields) 
                    {
                        if (!passportFields.ContainsKey(key)) 
                        {
                            valid = false;
                            //break;
                        }
                        else 
                        {
                            valid = true;
                        }
                           
                            
                    }

                    int count = 0;

                    if (valid)
                    {

                        if (passportFields.ContainsKey("byr"))
                        {
                            if (ValidatePassportData("byr", passportFields["byr"])) count++;
                            else
                                valid = false;
                        }
                        if (passportFields.ContainsKey("iyr"))
                        {
                            if (ValidatePassportData("iyr", passportFields["iyr"])) count++;
                            else
                                valid = false;
                        }
                        if (passportFields.ContainsKey("eyr"))
                        {
                            if (ValidatePassportData("eyr", passportFields["eyr"])) count++;
                            else
                                valid = false;
                        }
                        if (passportFields.ContainsKey("hgt"))
                        {
                            if (ValidatePassportData("hgt", passportFields["hgt"])) count++;
                            else
                                valid = false;
                        }
                        if (passportFields.ContainsKey("hcl"))
                        {
                            if (ValidatePassportData("hcl", passportFields["hcl"])) count++;
                            else
                                valid = false;
                        }
                        if (passportFields.ContainsKey("ecl"))
                        {
                            if (ValidatePassportData("ecl", passportFields["ecl"])) count++;
                            else
                                valid = false;
                        }
                        if (passportFields.ContainsKey("pid"))
                        {
                            if (ValidatePassportData("pid", passportFields["pid"])) count++;
                            else
                                valid = false;
                        }

                        validPassportCount += count;
                    }
                    else 
                    {
                        validPassportCount = 0;
                    }




                    //These are all checks for part1.


                    passportFields.Clear(); //Clear data
                }
                else
                {
                    //Console.WriteLine($"Data is: {data}");
                    var fieldList = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var s in fieldList) 
                    {
                        string[] fieldParts = s.Split(':');
                        passportFields.Add(fieldParts[0], fieldParts[1]);
                    }


                    ListofPassports.Add(passportFields);
   
                }
            
            }
            Console.WriteLine($"Total valid passport data is: {validPassportCount}");
        }

        /// <summary>
        /// Validation method for part2
        /// </summary>
        private static bool ValidatePassportData(string fieldID, string fieldValue) 
        {
            switch (fieldID) 
            {
                case "byr":
                    if (fieldValue.Length == 4) 
                    {
                        int birthYear = int.Parse(fieldValue);
                        if (birthYear >= 1920 && birthYear <= 2002)
                            return true;
                    }
                    break;

                case "iyr":
                    if (fieldValue.Length == 4)
                    {
                        int issueYear = int.Parse(fieldValue);
                        if (issueYear >= 2010 && issueYear <= 2020)
                            return true;
                    }
                    break;
                case "eyr":
                    if (fieldValue.Length == 4)
                    {
                        int expirationYear = int.Parse(fieldValue);
                        if (expirationYear >= 2020 && expirationYear <= 2030)
                            return true;
                    }
                    break;

                case "hgt":
                    if (!(fieldValue.Contains("cm") || fieldValue.Contains("in")))
                        return false;

                    if (fieldValue.Length == 5) 
                    {

                        string heightStr = fieldValue.Substring(0, fieldValue.Length - 2);
                        int heightInCm = int.Parse(heightStr);
                        if (heightInCm >= 150 && heightInCm <= 193) 
                        {
                            return true;
                        }

                    }
                    if (fieldValue.Length == 4) 
                    {
                        string value = fieldValue.Substring(0, fieldValue.Length - 2);
                        int heightInInches = int.Parse(value);

                        if (heightInInches >= 56 && heightInInches <= 76)
                        {
                            return true;
                        }
                    }
                    break;

                case "hcl":
                    if (fieldValue.Length != 7)
                        return false;

                    string regexCheck1 = @"^(?:[\#\d]*)?$";
                    string regexCheck2 = @"^(?:[\#a-f]*)$";
                    if (Regex.IsMatch(fieldValue, regexCheck1) || Regex.IsMatch(fieldValue, regexCheck2))
                        return true;
                    break;

                case "ecl":
                    string[] validEyeColours = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                    foreach (string col in validEyeColours) 
                    {
                        if (fieldValue.Equals(col))
                            return true;
                    }

                    break;
                case "pid":
                    if (fieldValue.Length != 9) return false;
                    
                    string regexCheck = @"^(?:[\0-1]*)$";
                    if (Regex.IsMatch(fieldValue, regexCheck))
                        return true;
                    
                    break;
                default: 
                    return false;
            }

            return false;
        }

       

    }
}

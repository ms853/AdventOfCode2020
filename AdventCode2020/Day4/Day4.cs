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

        static string[] invalidPassports = new string[]
        {
            "eyr:1972 cid:100",
            "hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926",
            "",
            "iyr:2019 hcl:#602927",
            "eyr:1967 hgt:170cm ecl:grn pid:012533040 byr:1946",
            "",
            "hcl:dab227 iyr:2012",
            "ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277",
            "",
            "hgt:59cm ecl:zzz",
            "eyr:2038 hcl:74454a iyr:2023",
            "pid:3556412378 byr:2007"
        };

       static string[] validPassportdataset = new string[]
        {
            "pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980",
            "hcl:#623a2f",
            "",
            "eyr:2029 ecl:blu cid:129 byr:1989",
            "iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm",
            "",
            "hcl:#888785",
            "hgt:164cm byr:2001 iyr:2015 cid:88",
            "pid:545766238 ecl:hzl",
            "eyr:2022",
            "",
            "iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719"
        };

        //Hold the list of valid passwords.
        static List<Dictionary<string, string>> ListofPassports = new List<Dictionary<string, string>>();

        /// <summary>
        /// Read data with a given file path, and returns an a long string consisting of all the contents of the file. 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static string ReadData(string filePath)
        {
            string lines = null;
            using (var sr = new StreamReader(filePath))
            {
                lines = sr.ReadToEnd();
            }
            return lines;
        }


        public static void SolutionPart1()
        {
            //This data 
            Dictionary<string, bool> passportFieldKeys = new Dictionary<string, bool>() 
            {
                {"byr", true},
                {"ecl", true},
                {"hgt", true},
                {"eyr", true},
                {"iyr", true},
                {"hcl", true},
                {"pid", true},
                {"cid", false}
            };

            int validPassportCount = 0, totalNoRequiredFields = 7;
 
            string testFile = @"InputData\testFile4.txt";
            string testFile1 = @"InputData\passportTest2.txt";

            string rawPassportData = ReadData(filePath); 
            //Using double line feeds to differentiate between passport groups.
            string[] passports = rawPassportData.Replace("\r", "").Split("\n\n"); 

            int counter = 0;
            foreach (string passport in passports) 
            {
                int requiredFields = 0, optionalFields = 0;
                var data = passport.Split(new char[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries); //Data will result in eyr:2013,...
                //Creating an anonymous type, which will consists of splitting up the passport of fields into key value pairs. 
                var fieldParts = data.Select(x => x.Split(":")).Select(x => new { Key = x[0], Value = x[1] }).ToList();

                foreach (var fieldKey in fieldParts)
                {
                    /*
                     * Do a lookup to check if the Field Id exists in our dictionary, and if it is a required field.
                     * If it is a required field then we want to increment the required field count.
                     * If not increment the optional field count.
                     * **/
                    if (passportFieldKeys.TryGetValue(fieldKey.Key, out bool required))
                    {
                        if (required) 
                            requiredFields++;
                        else 
                            optionalFields++;
                    }
                }
                //Valid passport count is incremented only when the total number of required field is equal to the expected number.
                if (requiredFields == totalNoRequiredFields) 
                {
                    validPassportCount++;
                    var FieldDict = fieldParts.ToDictionary(part => part.Key, part => part.Value); 
                    ListofPassports.Add(FieldDict);
                }
                
            }

            Console.WriteLine($"Total valid passport data is: {validPassportCount}");
        }

        public static void SolutionPart2()
        {
            int validCount = 0, invalidCount = 0, totalValidPassports = 0;
            bool isValid;

            foreach (Dictionary<string, string> passport in ListofPassports)
            {
                foreach (string key in passport.Keys)
                {
                    if (key.Equals("cid")) continue;
 
                    isValid = ValidatePassportData(key, passport[key]);
                   
                    if (isValid)
                    {
                        validCount++;
                    }
                    else
                    {
                        invalidCount++;
                    }

                }

                if (invalidCount == 0) totalValidPassports++;
            }
            Console.WriteLine($"Total number of valid passports that have been validated as valid: {totalValidPassports}");
        }

        private static bool CheckPassportValidity(Dictionary<string, string> passportFields)
        {

            if (!(passportFields.ContainsKey("byr") && passportFields.ContainsKey("iyr") && passportFields.ContainsKey("eyr") && passportFields.ContainsKey("hgt")
                && passportFields.ContainsKey("hcl") && passportFields.ContainsKey("ecl") && passportFields.ContainsKey("pid")))
            {
                return false;
            }
            else 
            {
               
            }

            
            if (passportFields.Keys.Count == 7 && !passportFields.ContainsKey("cid"))
            {
                //ListofPassports.Add(passportFields);
                return true;
            }

            //ListofPassports.Add(passportFields);
            return true;
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

                        if (heightInInches >= 59 && heightInInches <= 76)
                        {
                            return true;
                        }
                    }
                    break;

                case "hcl":
                    if (fieldValue.Length != 7)
                        return false;

                    string regexCheck1 = @"^(?:[\#\da-f]*)?$";
                    string regexCheck2 = @"^(?:[\#\d]*)$";
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
                    if (fieldValue.Length == 9) return true;
                    
                    //string regexCheck = @"^(?:[\0-1]*)$";
                    /*if (Regex.IsMatch(fieldValue, regexCheck))*/

                    break;
                default:
                    return false;
            }

            return false;
        }


        /*
         * 
            foreach (string data in rawPassportData)
            {
                //This check ensures that each group is read and added to the data.
                if (data.Equals(""))
                {
                    // Console.WriteLine($"Data is: {data}");
                    continue;
                }
                if (data.Equals(rawPassportData[rawPassportData.Count - 1])) { }
                var fieldList = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var s in fieldList)
                {
                    string[] fieldParts = s.Split(':');
                    if (passportFields.ContainsKey(fieldParts[0])) break;
                    passportFields.Add(fieldParts[0], fieldParts[1]);
                }
        

                if (passportFields.Keys.Count >= 7)
                {
                    valid = CheckPassportValidity(passportFields);
                    if (valid)
                    {
                        validPassportCount++;
                        ListofPassports.Add(passportFields);
                        passportFields.Clear();
                    }
                }
                //passportFields.Clear();

            }
         * 
         * 
         * 
         *  if (passportFields.ContainsKey("byr"))
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
         * **/
    }
}

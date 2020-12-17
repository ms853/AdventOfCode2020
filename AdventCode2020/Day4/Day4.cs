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

        string[] invalidPassports = new string[]
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

        string[] validPasswordsDataset = new string[]
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
            "eyr:2022l",
            "",
            "iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719"
        };

        //Hold the list of valid passwords.
        static List<Dictionary<string, string>> ListofPassports = new List<Dictionary<string, string>>();

        private static List<string> ReadData(string filePath)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            return lines;
        }


        public static void SolutionPart2()
        {

        }

        public static void SolutionPart1()
        {
            Dictionary<string, string> passportFields = new Dictionary<string, string>();

            bool valid = true;
            int validPassportCount = 0;
 
            string testFile = @"InputData\testFile4.txt";
            string testFile1 = @"InputData\passportTest2.txt";

            List<string> rawPassportData = ReadData(filePath);

            foreach (string data in rawPassportData)
            {
                //This check ensures that each group is read and added to the data.
                if (data.Equals(""))
                {
                    if (passportFields.Keys.Count < 7)
                    {
                        valid = false;
                    }
                  

                    if (!(passportFields.ContainsKey("byr") && passportFields.ContainsKey("iyr") && passportFields.ContainsKey("eyr") && passportFields.ContainsKey("hgt")
                        && passportFields.ContainsKey("hcl") && passportFields.ContainsKey("ecl") && passportFields.ContainsKey("pid")))
                    {
                        valid = false;
                    }
                    else 
                    {
                        valid = true;
                    }

                    if (passportFields.Keys.Count == 7 && !passportFields.ContainsKey("cid"))
                    {
                        valid = true;
                    }

                    if (valid)
                    {
                        validPassportCount++;
                        ListofPassports.Add(passportFields);
                    }

                    passportFields.Clear(); //Clear data
                }
                else
                {
                   // Console.WriteLine($"Data is: {data}");
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


        /*
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

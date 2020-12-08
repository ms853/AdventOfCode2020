using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day2
{
    /// <summary>
    /// Password Philosophy
    /// </summary>
    class DayTwo
    {

        //For solution
        //Read the data, and store it in a dictionary. 
        //Then come up with an algorithm to compare each one key and value simultaneously
        //Check the rule and the password that follows after the colon. 
        private static List<string> ReadData(string filePath) 
        {
            List<string> lines = File.ReadAllLines(filePath).ToList(); 
            return lines;
        }

        private static Dictionary<string, bool> GroupData() 
        {
            string filePath = @"InputData\password_policies.txt";
            List<string> DataList = ReadData(filePath);
            Dictionary<string, bool> PasswordRulePairs = new Dictionary<string, bool>();
            bool isPasswordValid;
            foreach (string line in DataList) 
            {
                string[] tempArray = line.Split(":");
                isPasswordValid = ValidatePasswords(tempArray[0],tempArray[1]);
                PasswordRulePairs.Add(line, isPasswordValid);
            }

            return PasswordRulePairs;
        }

        private static bool ValidatePasswords(string pwdPolicy, string password) 
        {
            int maxOccur, minimumOccur, occurs = 0;
            char[] policy = pwdPolicy.ToCharArray();
            char[] passwordArray = password.ToCharArray();
            char charToMatch = policy[policy.Length-1];
            string max = pwdPolicy.Substring(2).Trim(charToMatch);
            string min = pwdPolicy.Substring(0, 1);
            minimumOccur = int.Parse(min);
            maxOccur = int.Parse(max);

            for (int i = 0; i < passwordArray.Length; i++) 
            {
                if (passwordArray[i] == charToMatch)
                {
                    occurs++;
                }
                else if (!passwordArray.Contains(charToMatch)) 
                {
                    return false;
                }
            }

            if (occurs >= minimumOccur && occurs <= maxOccur)
            {
                return true;

            }
           
            else if (occurs < minimumOccur || occurs > maxOccur)
            {
                return false;
            }
          
            return false;
        }

        public static int Solution() 
        {
            int sumOfValidPasswords = 0;
            Dictionary<string, bool> GroupedData = GroupData();

            foreach(bool isValidFlag in GroupedData.Values) 
            {
                if (isValidFlag == true) 
                {
                    sumOfValidPasswords++;
                }
            }
           
            return sumOfValidPasswords;
        }

    }
}

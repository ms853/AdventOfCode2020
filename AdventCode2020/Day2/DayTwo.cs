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

        private static List<string> ReadData(string filePath) 
        {
            List<string> lines = File.ReadAllLines(filePath).ToList(); 
            return lines;
        }

       /* private static Dictionary<string, bool> ProcessData(List<string> linesRead)
        {
           
            List<string> DataList = ReadData(filePath);
            Dictionary<string, bool> PasswordRulePairs = new Dictionary<string, bool>();
            bool isPasswordValid;

            

            return PasswordRulePairs;
        }*/

        /*private static bool ValidatePasswords(string pwdPolicy, string password) 
        {
            int maxOccur, minimumOccur, occurs = 0;
            char[] policy = pwdPolicy.ToCharArray();
            char[] passwordArray = password.ToCharArray();
            char charToMatch = policy[policy.Length-1];
            string max = pwdPolicy.Substring(2).Trim(charToMatch);
            string min = pwdPolicy.Substring(0, 1);
            minimumOccur = int.Parse(min);
            maxOccur = int.Parse(max);

            if (!passwordArray.Contains(charToMatch))
                return false;

            for (int i = 0; i < passwordArray.Length; i++) 
            {
                if (passwordArray[i] == charToMatch)
                {
                    occurs++;
                }
            }

            if (occurs >= minimumOccur && occurs <= maxOccur)
            {
                return true;

            }
            else 
            {
                return false;
            }
           
        }*/

        private static bool CheckPasswords(string pwdPolicy, string charToMatch, string password)
        {
            int maxOccur, minimumOccur, occurs = 0;
          
            charToMatch = charToMatch.Substring(0,1);
            var targetChar = char.Parse(charToMatch);
            string max = pwdPolicy.Substring(2);
            string min = pwdPolicy.Substring(0, 1);
            minimumOccur = int.Parse(min);
            maxOccur = int.Parse(max);

            if (!password.Contains(targetChar))
                return false;

            var passwordArray = password.ToCharArray();

            for (int i = 0; i < passwordArray.Length; i++)
            {
                if (passwordArray[i] == targetChar)
                {
                    occurs++;
                }
            }

            if (occurs >= minimumOccur && occurs <= maxOccur)
            {
                return true;

            }
            else
            {
                return false;
            }

        }

        public static int Solution() 
        {
            int sumOfValidPasswords = 0;
            
            string filePath = @"InputData\password_policies.txt";

            List<string> DataList = ReadData(filePath);

            foreach (string line in DataList)
            {
                string[] tempArray = line.Split(' ');

                if (CheckPasswords(tempArray[0], tempArray[1], tempArray[2])) 
                {
                    sumOfValidPasswords++;
                }
               
            }

            return sumOfValidPasswords;
        }

    }
}

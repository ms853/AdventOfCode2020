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
        const string filePath = @"InputData\password_policies.txt";

        private static List<string> ReadData(string filePath) 
        {
            List<string> lines = File.ReadAllLines(filePath).ToList(); 
            return lines;
        }


        private static bool ValidatePasswordPolicy(string policy, string charToMatch, string password)
        {

            int position1, position2;
            string[] positions = policy.Split('-');
            position1 = int.Parse(positions[0]) -1; //Position is counted from 1 not zero.
            position2 = int.Parse(positions[1]) -1;
            charToMatch = charToMatch.Substring(0, 1);
            var targetChar = char.Parse(charToMatch);

            if (position1 == password.IndexOf(targetChar) || position2 == password.IndexOf(targetChar)) 
            {
                return true;
            }



            return false;
        }

        private static bool CheckPasswords(string pwdPolicy, string charToMatch, string password)
        {
            int maxOccur, minimumOccur, occurs = 0;
            string[] minimumMax = pwdPolicy.Split("-");
            string min = minimumMax[0]; 
            string max = minimumMax[1];
            charToMatch = charToMatch.Substring(0,1);
            var targetChar = char.Parse(charToMatch);
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



        public static int SolutionToPart1() 
        {
            int sumOfValidPasswords = 0;
            
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


        public static int SolutionToPart2()
        {
            int totalValidPwds = 0;
            List<string> DataList = ReadData(filePath);

            foreach (string line in DataList) 
            {
                string[] tempArray = line.Split(' ');
                
                if (ValidatePasswordPolicy(tempArray[0], tempArray[1], tempArray[2])) 
                {
                    totalValidPwds++;
                }
            }
            return totalValidPwds;
        }

    }
}

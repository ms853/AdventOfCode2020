using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Day1
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

        private Dictionary<string,string> GroupData() 
        {
            string filePath = @"InputData\password_policies.txt";
            List<string> DataList = ReadData(filePath);
            Dictionary<string, string> PasswordRulePairs = new Dictionary<string, string>(); 
            foreach (string line in DataList) 
            {
                string[] tempArray = line.Split(":");
                PasswordRulePairs.Add(tempArray[0],tempArray[1]);
            }
            return PasswordRulePairs;
        }

        public void Solution() 
        {
            Dictionary<string, string> PwdAndRules = GroupData();
            
        }

    }
}

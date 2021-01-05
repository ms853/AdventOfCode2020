using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day9
{
    /// <summary>
    /// --- Day 9: Encoding Error ---
    /// </summary>
    public class Day9
    {
        const string file = @"InputData\encoding_input.txt";
        

        private static List<string> ReadData(string fileName) 
        {
            List<string> lines = File.ReadAllLines(fileName).ToList();
            return lines;
        }

        //Valid Criteria
        //Numbers must be different to each other and the there must be one pair that is the sum of the number.

        public static void Part1()
        {

            bool numberFound;
            List<string> Data = ReadData(file);
            //Preambled numbers
            string[] testData = new string[]
            {
            "35","20","15","25","47","40","62","55","65","95","102","117","150","182","127","219","299","277","309","576"
            };

            List<int> prev25Numbers = new List<int>();
            
            for (int i = 0; i < 25; i++) 
            {
                prev25Numbers.Add(int.Parse(Data[i]));
               
            }

            for (int i = 0; i < Data.Count; i++) 
            {
                int number = int.Parse(Data[i]);
                numberFound = ProcessPreambledNumbers(number, prev25Numbers);

                if (numberFound)
                {
                    prev25Numbers.RemoveAt(0);
                    prev25Numbers.Add(number);
                }
                else 
                {
                    Console.WriteLine($"Part 1 Answer: {number}");
                    break;
                }
   
            }
            
            
              
            
        }

        private static bool ProcessPreambledNumbers(int number, List<int> prev25Numbers)
        {
           
            for (int i = 0; i < prev25Numbers.Count; i++) 
            {
                for (int j = i; j < prev25Numbers.Count; j++) 
                {
                    int previousNum1 = prev25Numbers[i];
                    int previousNum2 = prev25Numbers[j];

                    if (previousNum1 + previousNum2 == number)
                        return true;
                   
                }   
            }

            return false;
        }
    }
}

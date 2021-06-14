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

        private static HashSet<long> SetForNumbers = new HashSet<long>();  

        //Valid Criteria
        //Numbers must be different to each other and the there must be one pair that is the sum of the number.

        public static long Part1()
        {

            long numberToReturn = 0;
            List<string> Data = ReadData(file);
            //Preambled numbers
            string[] testData = new string[]
            {
            "35","20","15","25","47","40","62","55","65","95","102","117","150","182","127","219","299","277","309","576"
            };

            List<Int64> prev25Numbers = new List<Int64>();
            
            for (int i = 0; i < 25; i++) 
            {
                prev25Numbers.Add(int.Parse(Data[i]));
               
            }

            for (int i = 25; i < Data.Count; i++) 
            {
                Int64 number = Int64.Parse(Data[i]);

                bool numberFound;
                numberFound = ProcessPreambledNumbers(number, prev25Numbers);
                  
                if (numberFound)
                {
                    prev25Numbers.RemoveAt(0);
                    prev25Numbers.Add(number);
                }
                else 
                {
                    Console.WriteLine($"Part 1 Answer: {number}");
                    numberToReturn = number;
                    break;
                }

            }

            return numberToReturn;
        }

        private static bool ProcessPreambledNumbers(Int64 number, List<Int64> prev25Numbers)
        {
            bool found = false;
            
            for (int j = 0; j < prev25Numbers.Count; j++) 
            {
                if (found) break;
                
                for (int k = j + 1; k < prev25Numbers.Count; k++) 
                {
                    Int64 previousNum1 = prev25Numbers[j];
                    Int64 previousNum2 = prev25Numbers[k];

                    if (previousNum1 + previousNum2 == number)
                    {
                        found = true;
                    }
                    else 
                    {
                        SetForNumbers.Add(previousNum1);
                        SetForNumbers.Add(previousNum2);
                    }
                        


                }   
            }

            return found;
        }

        public static void Part2() 
        {
            long targetNumber = Part1();

            /*for (int i = 0; i < SetForNumbers.Count(); i++) 
            {
                for (int j = i + 1; j < SetForNumbers.Count; j++) 
                {
                    
                }
            }*/
            if (SetForNumbers.Sum() == targetNumber)
            {
                Console.WriteLine($"It equals: {targetNumber}"); 
            }
            else 
            {
                Console.WriteLine($"Set equals: {SetForNumbers.Sum()}");
            }
        }
    }
}

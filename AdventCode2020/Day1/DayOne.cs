using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020
{
    public class DayOne
    {
        //Part One.

        /// <summary>
        /// Method for reading the input file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static List<string> ReadInput(string fileName)
        {
            List<string> inputList = new List<string>();
            string[] dataArray = null;
            TextReader tr = new StreamReader(fileName);

            if (!File.Exists(fileName)) 
            {
                Console.WriteLine("Couldn't find file!!");
                return new List<string>();
            }

            try 
            {
               
                string data = tr.ReadToEnd();
                tr.Close();
                data = data[0..^1];
                dataArray = data.Split("\n");
            } 
            catch (IOException io) 
            {
                Console.WriteLine("Something went wrong: ", io.Message);
                tr.Close();
            }
           
            for (int i = 0; i < dataArray.Length; i++)
            {

                inputList.Add(dataArray[i]);
            }

            return inputList;
        }

        /// <summary>
        /// Algorithm which will checking every two numbers in the list and see if the sum is equal to 2020.
        /// If that is the case, the numbers will be multiplied and that's the answer.
        /// </summary>
        /// <returns></returns>
        public static int SolutionToPart1()
        {
            string pathToFile = @"D:\Private Study\AdventOfCode2020\AdventCode2020\InputData\input.txt";
            List<string> DataList = ReadInput(pathToFile);
            string[] inputArray = DataList.ToArray();
            int answer = 0;
            //Nested loop for iterating over the numbers in the array.
            //This ensures that the number is not read more than once.
            for (int i = 0; i < inputArray.Length; i++)
            {
                for (int j = i + 1; j < inputArray.Length; j++)
                {

                    int num1 = int.Parse(inputArray[i]);
                    int num2 = int.Parse(inputArray[j]);
                   
                    if (num1 + num2 == 2020)
                    {
                        answer = num1 * num2;
                        return answer;
                    }
                }
            }

            return answer;
        }

        /// <summary>
        /// Same principle just another iteration to get the third number.
        /// </summary>
        /// <returns></returns>
        public static int SolutionToPart2() 
        {
            string pathToFile = @"D:\Private Study\AdventOfCode2020\AdventCode2020\InputData\input.txt";

            List<string> DataList = ReadInput(pathToFile);
            string[] inputArray = DataList.ToArray();

            for (int i = 0; i < inputArray.Length; i++)
            {
                for (int j = i + 1; j < inputArray.Length; j++)
                {
                    for (int k = j + 1; k < inputArray.Length; k++) 
                    {
                        int num1 = int.Parse(inputArray[i]);
                        int num2 = int.Parse(inputArray[j]);
                        int num3 = int.Parse(inputArray[k]);

                        if (num1 + num2 + num3 == 2020)
                        {
                            int answer = num1 * num2 * num3;
                            return answer;
                        }
                    }
                   
                }
            }

            return 0;
        }

    }
}

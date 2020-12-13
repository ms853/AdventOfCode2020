using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day6
{
    /// <summary>
    /// Day 6 - Custom Customs
    /// The form asks a series of 26 yes-or-no questions marked a through z. 
    /// All you need to do is identify the questions for which anyone in your group answers "yes". 
    /// Since your group is just you, this doesn't take very long.
    /// </summary>
    class Day6
    {
        /**
         * However, the person sitting next to you seems to be experiencing a language barrier and asks if you can help. 
         * For each of the people in their group, you write down the questions for which they answer "yes", one per line.
         */
        const string fileName = @"InputData\question_input.txt";

        private static string ReadData(string filePath)
        {
            string lines = null;
            using (var sr = new StreamReader(filePath))
            {
                lines = sr.ReadToEnd();
            }
            return lines;
        }

        private static List<string> ParseData() 
        {
            string testFile = @"InputData\testFile6.txt";
            string rawData = ReadData(fileName);
            string noNewLines = rawData.Replace("\r","").Replace("\n\n", "@").Trim();
            var questionsList = noNewLines.Split('@', StringSplitOptions.RemoveEmptyEntries).ToList();

            return questionsList;
        }

        public static void Day6SolutionPart1() 
        {
            int answeredQuestionsCount = 0;
          
            List<string> questions = ParseData();
            //This datastructure will hold the the list of all the group answers and the number of occurances. 
            List<Dictionary<char, int>> groupAnswerWithTotal = new List<Dictionary<char, int>>();

            foreach (string q in questions) 
            {
                Dictionary<char, int> AnswerWithCount = new Dictionary<char, int>();
                string checkQuestion = q.Replace(" ", "").Replace("\n", "");
                foreach (char answerChar in checkQuestion) 
                {
                    if (AnswerWithCount.TryGetValue(answerChar, out int occuranceCount))
                        occuranceCount++;
                    else
                        AnswerWithCount[answerChar] = 1;
                   
                }
                groupAnswerWithTotal.Add(AnswerWithCount); //Once all the occurences of the letter have been added, then I add to the list of dictionaries. 
                answeredQuestionsCount += AnswerWithCount.Count();
                Console.WriteLine($"Number of people who answered questions in the Group: {groupAnswerWithTotal.Count}, {AnswerWithCount.Count} -- Total Questions Answered: {answeredQuestionsCount} ");

            }

        }
    }
}

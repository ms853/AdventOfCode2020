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
            string rawData = ReadData(testFile);
            string noNewLines = rawData.Replace("\r","").Replace("\n\n", "@").Trim();
            var questionsList = noNewLines.Split('@', StringSplitOptions.RemoveEmptyEntries).ToList();

            return questionsList;
        }

        public static void Day6SolutionPart1() 
        {
            int answerPerGroupCount = 0;
            int answeredQuestionsCount = 0, answerCount = 0;
            int totalNumberOfQuestions = 6; //Will be used to check against the length of the string of answers per group.
            List<string> questions = ParseData();
            Dictionary<string, int> groupAnswers = new Dictionary<string,int>();
            
            foreach (string q in questions) 
            {
                var checkQuestion = q.Split(new char[] { '\n', ','});
                for (int i = 0; i < checkQuestion.Length; i++) 
                {
                    if (checkQuestion[i].Length != totalNumberOfQuestions) 
                    {
                        answerCount = 0;
                        for (int j = 0; j < checkQuestion[i].Length; j++) 
                        {
                            answerCount++;
                            //groupAnswers.Add(checkQuestion[j],answerCount);
                        }
                        answerPerGroupCount += answerCount;
                        
                    }
                   
                }
                
            }
            answeredQuestionsCount = answerPerGroupCount;

            Console.WriteLine($"Total Number of questions answered yes to is: {answeredQuestionsCount}");
        }
    }
}

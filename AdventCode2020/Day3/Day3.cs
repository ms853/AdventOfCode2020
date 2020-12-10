using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2020.Day3
{
    ///--- Day 3: Toboggan Trajectory ---

    public class Day3
    {
        const string filePath = @"InputData\map_input";

        private static List<string> ReadData(string filePath)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            return lines;
        }

     

        public static int SolutionToPart1() 
        { 
            string testFile = @"InputData\test.txt";
            List<string> MapData = ReadData(testFile);
            int numberOfTrees = TraverseMap(MapData);

            return numberOfTrees;
        }

        private static int TraverseMap(List<string> mapData)
        {
            
            int treesTotal = 0;
            int rightCount = 3;

            foreach(string line in mapData) 
            {
                //split to a character array
                for (int i = 0; i < line.Length; i++)
                {
                    if (i == rightCount) break;

                    if (line[i] == '#')
                    {
                        treesTotal++;
                    }

                }
      
            }
       
            return treesTotal;
        }

        public static void SolutionToPart2() { }

        
    }
}

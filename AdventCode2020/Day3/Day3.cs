using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Xsl;

namespace AdventOfCode2020.Day3
{
    ///--- Day 3: Toboggan Trajectory ---

    public class Day3
    {
        const string filePath = @"InputData\map_input.txt";
   
        private static List<string> ReadData(string filePath)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            return lines;
        }

     

        public static int SolutionToPart1() 
        { 
            //string testFile = @"InputData\test.txt";
            List<string> MapData = ReadData(filePath);
            int numberOfTrees = TraverseMap(MapData);

            return numberOfTrees;
        }

        private static int TraverseMap(List<string> mapData)
        {
            
            int treesTotal = 0;
            int xSlope = 3;
            int x = 0;


            foreach(string line in mapData) 
            {
                if (x >= line.Length)
                {
                    x -= line.Length;
                }

                if (line[x] == '#') 
                {
                    treesTotal++;
                }

                x += xSlope;

            }

            return treesTotal;
        }

        public static void SolutionToPart2() { }

        
    }
}

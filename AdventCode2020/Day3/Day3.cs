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

        //Part 1
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

        public static double SolutionToPart2() 
        {
            string testFile = @"InputData\test.txt";
            List<string> Map = ReadData(filePath);
            Int64 totalTreesPerSlope = TotalNoOfTreesEncountered(Map);
            return totalTreesPerSlope;
        }

        //Part 2
        private static Int64 TotalNoOfTreesEncountered(List<string> mapData) 
        {
               
            Int64 slopeOne, slopeTwo, slopeThree, slopeFour, slopeFive;
           
            slopeOne = TraverseMapWithSlope(1, 1, mapData);
            slopeTwo = TraverseMapWithSlope(3, 1, mapData);
            slopeThree = TraverseMapWithSlope(5, 1, mapData);
            slopeFour = TraverseMapWithSlope(7, 1, mapData);
            slopeFive = TraverseMapWithSlope(1, 2, mapData);

         
            return slopeOne * slopeTwo * slopeThree * slopeFour * slopeFive;
            
        }

        private static Int64 TraverseMapWithSlope(int xSlope, int ySlope, List<string> mapData)
        {
            int y = 0;
            int x = 0;
            Int64 treesCounter = 0;
            //int totalTrees = 1; //Minimum encounter of a tree.
            //string[] mapArray = mapData.ToArray();

            while (y < mapData.Count) 
            {
                if (x >= mapData[y].Length) 
                {
                    x -= mapData[y].Length;
                }
                
                if (mapData[y][x] == '#') 
                {
                    treesCounter++;
                }

                x += xSlope;
                y += ySlope;
                
                
            }

            return treesCounter;
        }


    }
}

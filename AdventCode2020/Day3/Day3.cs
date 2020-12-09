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
    
    class Day3 
    {
        const string filePath = @"InputData\map_input";

        private static List<string> ReadData(string filePath)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            return lines;
        }

        public static void SolutionToPart1() 
        {
            List<string> MapData = ReadData(filePath);
        }

        public static void SolutionToPart2() { }

        
    }
}

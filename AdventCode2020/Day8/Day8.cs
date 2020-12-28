using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day8
{
    /// <summary>
    /// --- Day 8: Handheld Halting ---
    /// </summary>
    public class Day8
    {
        private static int accumulator = 0;
        private static int programCounter = 0;
        const string fileName = @"InputData\boot_input.txt";
        private static List<string> ReadData(string filePath)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            return lines;
        }

        public static void Part1() 
        {
            string testFile = @"InputData\test.txt";

            List<string> bootData = ReadData(fileName);
            bool run = true;

            List<int> ProgramCountList = new List<int>();
            while (run) 
            {
                ProgramCountList.Add(programCounter);
                string s = bootData[programCounter];
                
                string[] instructions = s.Split(" ");
                   
                ProcessInstruction(instructions);

                if (ProgramCountList.Contains(programCounter))
                    run = false;


            }
            Console.WriteLine($"Part1: Accumulator: {accumulator}, Program Counter: {programCounter}");
        }

        private static void ProcessInstruction(string[] instructOp)
        {
            string operation = instructOp[0];
            switch (operation) 
            {
                case "nop":
                    programCounter++;
                    break;

                case "acc":
                    accumulator += int.Parse(instructOp[1]);
                    programCounter++;
                    break;

                case "jmp":
                    programCounter += int.Parse(instructOp[1]);
                    break;
               
            }
        }

        public static void Part2() 
        {
        
        }
    }
}

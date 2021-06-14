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
        private static int accumulator;
        private static int programCounter; // To help determine which instruction to run.
        private static bool run = true, isSwapped = false;
        private static List<string[]> swappedInstruct = new List<string[]>(); //for storing swapped instructions
        const string fileName = @"InputData\boot_input.txt";
        string testFile = @"InputData\test.txt";

        private static List<string> ReadData(string filePath)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            return lines;
        }

        public static void Part1() 
        {

            List<string> bootData = ReadData(fileName);
            
            List<int> ProgramCountList = new List<int>();
            accumulator = 0;
            programCounter = 0;

            while (run) 
            {
                ProgramCountList.Add(programCounter);
                string s = bootData[programCounter];
                
                string[] instructions = s.Split(" ");
                   
                ProcessInstruction(instructions, "");

                if (ProgramCountList.Contains(programCounter))
                    run = false;
            }
            Console.WriteLine($"Part1: Accumulator: {accumulator}, Program Counter: {programCounter}");
        }

        private static void ProcessInstruction(string[] instructOp, string partOfSol)
        {
            string operation = instructOp[0];

            switch (operation) 
            {
                case "nop":
                    if (partOfSol == "part2")
                    {
                        isSwapped = false;
                        if (!isSwapped && !swappedInstruct.Contains(instructOp))
                        {
                            isSwapped = true;
                            swappedInstruct.Add(instructOp);
                            programCounter += int.Parse(instructOp[1]);
                            programCounter++;
                        }


                    }
                    else 
                    {
                        programCounter++;
                    }
                    
                    break;

                case "acc":
                    accumulator += int.Parse(instructOp[1]);
                    programCounter++;
                    break;

                case "jmp":
                    if (partOfSol == "part2")
                    {
                        if (!isSwapped && !swappedInstruct.Contains(instructOp))
                        {
                            isSwapped = true;
                            swappedInstruct.Add(instructOp);
                            programCounter += int.Parse(instructOp[1]);
                            programCounter++;
                        }
                    }
                    else 
                    {
                        programCounter += int.Parse(instructOp[1]);
                    }
                    
                    break;
            }
        }

        public static void Part2() 
        {
            List<string> bootData = ReadData(fileName);
            
            accumulator = 0;
            programCounter = 0;
            run = true;
            
            List<int> ProgramCountList = new List<int>();

            while (run) 
            {
                ProgramCountList.Add(programCounter);
                string s = bootData[programCounter];
                var instructOp = s.Split(" ");

                ProcessInstruction(instructOp, "part2");

                if (swappedInstruct.Contains(instructOp))
                    run = false;
            }
            Console.WriteLine($"Part2: Accumulator: {accumulator}, Program Counter: {programCounter}");
        }
    }
}

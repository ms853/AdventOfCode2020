using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day7
{
    
    /// <summary>
    /// --- Day 7: Handy Haversacks ---
    /// Luggage processing problem!
    /// Consider the bag types,
    /// I have a shiny gold bag, how many different bag colours can, eventually, contain at least one shiny gold bag?
    /// 
    /// </summary>
    public class Day7
    {
        const string fileName = @"InputData\rules_input.txt";

      

        private static List<string> ReadData(string filePath)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            return lines;
        }

        private static int GetBags() 
        {
            //Test Data - specify the required contents for 9 bag types.
            string[] BagRulesTest = new string[] {
                "light red bags contain 1 bright white bag, 2 muted yellow bags.",
                "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                "bright white bags contain 1 shiny gold bag.",
                "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                "faded blue bags contain no other bags.",
                "dotted black bags contain no other bags."
            };

            
            List<string> DataToParse = ReadData(fileName);
            //List<string> BagsHoldingShinyGold = new List<string>();
            int quantityToReturn = 0; //total number of shiny shoes that can be added by the outermoset bag.
            foreach (string rule in DataToParse) 
            {
                
                var firstBag = rule.Substring(0, rule.Length - 1).Split("bags contain"); //Split consist of the colour of the outer bag and the bags it can hold.
                var outermostBag = firstBag[0]; //Outermost bag
                var innerBags = firstBag[1].Split(",");
                //Go through the inner bags array, find the quantity and check if bags consist a of shiny gold bag.
                foreach (string bagInfo in innerBags) 
                {
                    
                    bagInfo.Trim();
                    if (bagInfo.Contains("no")) continue;

                   
                    if (bagInfo.Contains("shiny gold")) 
                    {
                        int quantity = int.Parse(bagInfo.Substring(0, 3));
                        quantityToReturn += quantity;
                    }
                    
                }

                if (outermostBag.StartsWith("shiny gold"))
                    quantityToReturn++;
            }

            return quantityToReturn;
        }

        //How many colours can eventually contain at least one shiny gold bag?
        public static void Day7SolutionPart1() 
        {
            int totalNoBags = GetBags();
            
            Console.WriteLine($"The total number of bag colours that can contain at least one shiny gold bag is: {totalNoBags}");
        }
        
    }
}

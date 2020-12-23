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
    /// </summary>
    public class Day7
    {
        /// <summary>
        /// Small class that will hold information about the name of the bag and quantity.
        /// </summary>
        class BagDetails 
        {
            public int Quantity;
            public string BagColour;
        }
        
        const string fileName = @"InputData\rules_input.txt";

        private static List<string> ReadData(string filePath)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            return lines;
        }

        //For holding all the colour-codeds with their parent bags.
        static Dictionary<string, List<BagDetails>> BagColoursInABag = new Dictionary<string, List<BagDetails>>();
        
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

            int shinyBagCount = 0; //total number of shiny shoes that can be added by the outermoset bag.
            List<string> DataToParse = ReadData(fileName);
            

            foreach (string rule in DataToParse) 
            {
                List<BagDetails> bagContained = new List<BagDetails>(); //List of Colour-coded bags.
                var firstBag = rule.Substring(0, rule.Length - 1).Split(" bags contain "); //Split consist of the colour of the outer bag and the bags it can hold.
                var outermostBag = firstBag[0];

                //int bagWord = firstBag[1].IndexOf("bag");
                string innerBags = firstBag[1].Trim();
                var childBags = innerBags.Split(", ", StringSplitOptions.RemoveEmptyEntries);

                foreach (string bagInfo in childBags) 
                {
                    if (bagInfo.Contains("no other bags")) continue;
                    var bagInfoList = bagInfo.Split(" ");
                    int quantity = 0; string bagColour = null;
                    quantity = int.Parse(bagInfoList[0]);
                    bagColour = String.Join(' ', bagInfoList[1], bagInfoList[2]);

                    var bagDetails = new BagDetails();
                    bagDetails.Quantity = quantity;
                    bagDetails.BagColour = bagColour;

                    bagContained.Add(bagDetails);
                }
                BagColoursInABag.Add(outermostBag, bagContained);
                
            }

            
            foreach (string parentBag in BagColoursInABag.Keys)
            {
                if (FindShinyBags(parentBag)) shinyBagCount++;
            }

            return shinyBagCount;
        }

        /// <summary>
        /// Recursive Function
        /// Checks the parent bag, for the shiny bag, if none is found, then it will search again.
        /// This time round, it will search the bag contained in the parent bag for the shiny gold bag.
        /// This mechanism is repeated recursively by calling this method again until a shiney gold bag is found.
        /// </summary>
        /// <param name="outerbag"></param>
        /// <returns></returns>
        private static bool FindShinyBags(string outerbag)
        {
            foreach (var subBags in BagColoursInABag[outerbag])
            {
                if (subBags.BagColour == "shiny gold")
                    return true;
                return FindShinyBags(subBags.BagColour);
            }
         
            return false;
        }

        //How many colours can eventually contain at least one shiny gold bag?
        public static void Day7SolutionPart1() 
        {
            int totalNoBags = GetBags();
            
            Console.WriteLine($"The total number of bag colours that can contain at least one shiny gold bag is: {totalNoBags}");
        }
        
    }
}

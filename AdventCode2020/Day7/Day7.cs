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
    /// </summary>
    public class Day7
    {
        const string fileName = @"InputData\rules_input.txt";

        private static string ReadData(string filePath)
        {
            string lines = null;
            using (var sr = new StreamReader(filePath))
            {
                lines = sr.ReadToEnd();
            }
            return lines;
        }
    }
}

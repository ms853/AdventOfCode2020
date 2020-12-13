using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day5
{
    /*Binary Boarding - Scanning all the nearby boarding passes to find your seat through process of elimination.
     * Example seat: FBFBBFFRLR = where F means "front", B means "back", L means "left", and R means "right"
     * First 7 characters will either be F or B; these specify one of the 128 rows on the plane. (numbered 0 through 127).
     * Each letter tells you which half of the region the seat is in. 
     * Start with whole list of rows (128). Front (F) - between 0 - 63, Back (64 through 127)
     * Next letter (L/R) indicate which half the seat is in. 
     * 
     * For example, consider just the last 3 characters of FBFBBFFRLR:
     * 8 columns of seats on the plane (numbered 0 through 7)
     * R means to take the upper half, keeping columns 4 through 7.
     * L means to take the lower half, keeping columns 4 through 5.
     * The final R keeps the upper of the two, column 5.
     * Answer to there the seat is in row 44, column 5
     * 
     * seat ID: multiply the row by 8 (total columns), then add the column. In this example, the seat has ID 44 * 8 + 5 = 357.
     * 
     * Essentially the task here is to decode bytes. 
     * **/
    
    public class Day5
    {
        const string fileName = @"InputData\boarding_passes.txt";
        const char front = 'F', back = 'B', left = 'L', right = 'R'; //Characters to represent what each letter represents
       

        private static string LoadData(string fileInput) 
        {
            string line = null;
            using (var sr = new StreamReader(fileInput)) 
            {
                line = sr.ReadToEnd();
            }
            return line;
        }

        public static void SolutionToDay5() 
        {
            //string[] testStrings = new string[] { "BFFFBBFRRR", "FFFBBBFRRR", "BBFFBBFRLL" };

            string unsortedData = LoadData(fileName);
  
            //Split them into separate characters separated by new line.
            string[] lines = unsortedData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            //Convert F - B chars to binary.
            //The power will be to 6
            double maxSeatId = 0;
            List<double> SeatIds = new List<double>();
            //Going through every list of lines and converting each character of the string from binary to decimal to get the desired rows and columns.
            foreach (string s in lines) 
            {
                double row = 0.0; //The row in decimal I want to end up with after conversion from binary to decimal.
                double column = 0.0; //column I want to end up with. 
                for (int i = 0; i < 7; i++) 
                {
                    if (s[i] == back) row += Math.Pow(2, 6 - i);
                }
                //Here I will deal with columns. 
                for (int i = 7; i < 10; i++) 
                {
                    if (s[i] == right) column += Math.Pow(2, 9 - i);
                }
                double rowColumn = (row * 8) + column;
                SeatIds.Add(rowColumn);
                if (rowColumn > maxSeatId)
                    maxSeatId = rowColumn;
            }

            Console.WriteLine($"Part 1 - Total Seat Id is: {maxSeatId}");
            Part2(SeatIds);
        }

        /// <summary>
        /// Goes through the list, sorts it in a sortedset collection and finds the missing seat in the list.
        /// </summary>
        /// <param name="seatIds"></param>
        public static void Part2(List<double> seatIds) 
        {
            int lastSeatId = int.MaxValue;

            var sortedList = new SortedSet<int>();
            for (int i = 0; i < seatIds.Count; i++) 
            {
                sortedList.Add((int)seatIds[i]);
                
            }
            foreach (int id in sortedList) 
            {
                Console.WriteLine($"Seat Ids {id}");
                if (id - lastSeatId > 1) 
                {
                    Console.WriteLine($"Missing Seat Id: {id - 1}");
                    break;
                }

                lastSeatId = id;
            }

        }

    }
}

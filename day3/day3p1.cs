using System.IO;
using System;
using System.Diagnostics;

namespace AdventOfCode2023
{
    class NumberSave
    {
        public string Number = "";
        public bool Save = false;
    }

    class Day3P1
    {
        static bool IsSymbol(char c)
        {
            return !char.IsDigit(c) && c != '.';
        }
        static void FindSymbol(int row, int col, string[] input, NumberSave numberSave)
        {
            int rowNum = input.Length-1;
            int colNum = input[0].Length-1;
            
            // if col > 0 = left
            if(col > 0)
            {
                if (IsSymbol(input[row][col-1]))
                {
                    numberSave.Save = true;
                    return;
                }

            }
            
            // if col < colNum = right
            if(col < colNum)
            {
                if(IsSymbol(input[row][col+1]))
                {
                    numberSave.Save = true;
                    return;
                }
            }

            // if row > 0 = up
            if(row > 0)
            {
                if(IsSymbol(input[row-1][col]))
                {
                    numberSave.Save = true;
                    return;
                }
            }

            // if row < rowNum = down
            if(row < rowNum)
            {
                if(IsSymbol(input[row+1][col]))
                {
                    numberSave.Save = true;
                    return;
                }
            }

            // if col > 0 and row > 0 = left up
            if(col > 0 && row > 0)
            {
                if(IsSymbol(input[row-1][col-1]))
                {
                    numberSave.Save = true;
                    return;
                }
            }

            // if col > 0 and row < rowNum = left down
            if(col > 0 && row < rowNum)
            {
                if(IsSymbol(input[row+1][col-1]))
                {
                    numberSave.Save = true;
                    return;
                }
            }

            // if col < colNum and row > 0 = right up
            if(col < colNum && row > 0)
            {
                if(IsSymbol(input[row-1][col+1]))
                {
                    numberSave.Save = true;
                    return;
                }
            }

            // if col < colNum and row < rowNum = right down
            if(col < colNum && row < rowNum)
            {
                if(IsSymbol(input[row+1][col+1]))
                {
                    numberSave.Save = true;
                    return;
                }
            }
        }
        static void Main()
        {
            string filePath = @"C:\Users\Michael Terekhov\advent_of_code2023\day3\inputs\2.txt";

            int sum = 0;
            string[] input = File.ReadAllLines(filePath);

            int rowNum = input.Length;
            int colNum = input[0].Length;

            NumberSave numberSave = new NumberSave();
            for(int row = 0; row < rowNum; row++)
            {
                for(int col = 0; col < colNum; col++)
                {
                    // if is digit
                    if(char.IsDigit(input[row][col]))
                    {
                        //  add to the number
                        numberSave.Number += input[row][col];

                        //  if Save is false
                        if(!numberSave.Save)
                        {
                            // check surrounding for symbols
                            FindSymbol(row, col, input, numberSave);
                        }
                    }
                    else
                    {
                        //  if number not empty and save is true
                        if(numberSave.Number != "" && numberSave.Save)
                        {
                            // parse into int and add to sum
                            sum += int.Parse(numberSave.Number);
                        }
                        numberSave = new NumberSave();
                        
                    }
                    
                }
            }
            Console.WriteLine("Sum: " + sum);                    
        }
    }
}


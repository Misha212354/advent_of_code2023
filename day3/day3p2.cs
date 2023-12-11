using System.IO;
using System;
using System.Diagnostics;

namespace AdventOfCode2023
{
   
    class Day3P2
    {
        class Coordinates
        {
            public int Row;
            public int Column;
            public Coordinates(int row, int column)
            {
                Row = row;
                Column = column; 
            }
        }

        class NumberStar
        {
            public string Number = "";
            public Coordinates StarRC = new Coordinates(-1, -1);
            public bool Adjacent = false;

        }


        static bool IsStar(char c)
        {
            return c == '*';
        }
        
        static void FindStar(int row, int col, string[] input, NumberStar numberStar)
        {
            int rowNum = input.Length-1;
            int colNum = input[0].Length-1;
            
            // if col > 0 = left
            if(col > 0)
            {
                if (IsStar(input[row][col-1]))
                {
                    numberStar.Adjacent = true;
                    numberStar.StarRC.Row = row;
                    numberStar.StarRC.Column = col-1;
                    return;
                }

            }
            
            // if col < colNum = right
            if(col < colNum)
            {
                if(IsStar(input[row][col+1]))
                {
                    numberStar.Adjacent = true;
                    numberStar.StarRC.Row = row;
                    numberStar.StarRC.Column = col+1;
                    return;
                }
            }

            // if row > 0 = up
            if(row > 0)
            {
                if(IsStar(input[row-1][col]))
                {
                    numberStar.Adjacent = true;
                    numberStar.StarRC.Row = row-1;
                    numberStar.StarRC.Column = col;
                    return;
                }
            }

            // if row < rowNum = down
            if(row < rowNum)
            {
                if(IsStar(input[row+1][col]))
                {
                    numberStar.Adjacent = true;
                    numberStar.StarRC.Row = row+1;
                    numberStar.StarRC.Column = col;
                    return;
                }
            }

            // if col > 0 and row > 0 = left up
            if(col > 0 && row > 0)
            {
                if(IsStar(input[row-1][col-1]))
                {
                    numberStar.Adjacent = true;
                    numberStar.StarRC.Row = row-1;
                    numberStar.StarRC.Column = col-1;
                    return;
                }
            }

            // if col > 0 and row < rowNum = left down
            if(col > 0 && row < rowNum)
            {
                if(IsStar(input[row+1][col-1]))
                {
                    numberStar.Adjacent = true;
                    numberStar.StarRC.Row = row+1;
                    numberStar.StarRC.Column = col-1;
                    return;
                }
            }

            // if col < colNum and row > 0 = right up
            if(col < colNum && row > 0)
            {
                if(IsStar(input[row-1][col+1]))
                {
                    numberStar.Adjacent = true;
                    numberStar.StarRC.Row = row-1;
                    numberStar.StarRC.Column = col+1;
                    return;
                }
            }

            // if col < colNum and row < rowNum = right down
            if(col < colNum && row < rowNum)
            {
                if(IsStar(input[row+1][col+1]))
                {
                    numberStar.Adjacent = true;
                    numberStar.StarRC.Row = row+1;
                    numberStar.StarRC.Column = col+1;
                    return;
                }
            }
        }

        static void Main()
        {
            string filePath = @"C:\Users\Michael Terekhov\advent_of_code2023\day3\inputs\2.txt";

            
            string[] input = File.ReadAllLines(filePath);

            int rowNum = input.Length;
            int colNum = input[0].Length;

            NumberStar numberStar = new NumberStar();

            Dictionary<string, int[]> numberStarDictionary = new Dictionary<string, int[]>();

            for(int row = 0; row < rowNum; row++)
            {
                for(int col = 0; col < colNum; col++)
                {
                    // if is digit
                    if(char.IsDigit(input[row][col]))
                    {
                        //  add to the number
                        numberStar.Number += input[row][col];
                        
                        // if star adjacent not found yet
                        if(!numberStar.Adjacent)
                        {
                            FindStar(row, col, input, numberStar); 
                        } 
                    }
                    else
                    {
                        if(numberStar.Number != "" && numberStar.Adjacent)
                        {
                            // take coordinates
                            // add to dict
                            string keyToCheck = (numberStar.StarRC.Row + "") + (numberStar.StarRC.Column + "");
                            int valueToAdd = int.Parse(numberStar.Number);

                            if (numberStarDictionary.ContainsKey(keyToCheck))
                            {
                                // Key exists, add the value to the existing int array
                                numberStarDictionary[keyToCheck] = numberStarDictionary[keyToCheck].Append(valueToAdd).ToArray();
                            }
                            else
                            {
                                // Key doesn't exist, create a new entry with a new int array
                                numberStarDictionary[keyToCheck] = new int[] { valueToAdd };
                            }
                        }
                        numberStar = new NumberStar();
                        
                    }
                    
                    
                }
            }

            int sum = 0;

            foreach (var item in numberStarDictionary)
            {
                if(item.Value.Length == 2){
                    sum += item.Value[0] * item.Value[1];
                }
            }

            Console.WriteLine("Sum: " + sum);                    
        }
    }
}


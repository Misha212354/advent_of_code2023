using System;
using System.Threading.Tasks.Dataflow;
using Microsoft.VisualBasic;

namespace AdventOfCode2023
{
    public class Day4P2
    {
        static int FindWinNums(string line)
        {
            string currNum = "";
            bool inWinNums = false;
            bool inMyNums = false;
            int winNums = 0;
            Dictionary<string, string> winValuesDict = new Dictionary<string, string>{};

            for(int i = 0; i < line.Length; i++)
            {
                char charRead = line[i];
                if(charRead == '\n')
                {
                    if(currNum == "")
                    {
                        return winNums;
                    }
                    else
                    {
                        i--;
                    }
                }

                if(charRead == ':')
                {
                    inWinNums = true;
                }
                else if(inWinNums)
                {
                    bool isDigit = char.IsDigit(charRead);

                    if(isDigit)
                    {
                        currNum += charRead;    
                    }
                    else 
                    {
                        if(currNum != "")
                        {
                            winValuesDict.Add(currNum, currNum);
                            currNum = "";
                        }

                        if(charRead == '|')
                        {
                            inWinNums = false;
                            inMyNums = true;
                        }   
                    }
                }
                else if(inMyNums)
                {
                    bool isDigit = char.IsDigit(charRead);

                    if(isDigit)
                    {
                        currNum += charRead;    
                    }
                    else
                    {
                        if(currNum != "")
                        {
                            if(winValuesDict.ContainsKey(currNum))
                            {
                                winNums++;
                            }

                            currNum = "";
                        }
                    }
                } 


            }
            return -1;
        }
        static void Main()
        {
            string filePath = @"C:\Users\Michael Terekhov\advent_of_code2023\day4\inputs\1.txt";
            
            int res = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();

                int currWinNum = 0;
                int currKey = 1;

                while (!reader.EndOfStream)
                {
                    // Read the current line
                    string? line = reader.ReadLine() + '\n';

                    if(line != null)
                    {
                        // covers the edge case when the value was never added
                        if(!keyValuePairs.ContainsKey(currKey))
                        {
                            keyValuePairs.Add(currKey, 1);
                        }

                        currWinNum = FindWinNums(line);

                        for(int j = currKey+1; j <= currKey+currWinNum; j++)
                        {
                            // add value to dict if never was add to track the number copies
                            if(!keyValuePairs.ContainsKey(j))
                            {
                                keyValuePairs.Add(j, 1);
                            }
                            keyValuePairs[j] = keyValuePairs[j] + keyValuePairs[currKey];
                        }
                    }
                    currKey++;
                }

                foreach (int value in keyValuePairs.Values)
                {
                    res+=value;
                }
                Console.WriteLine("Res: "+res);

            }
        }
    }
}
using System;
using Microsoft.VisualBasic;

namespace AdventOfCode2023
{
    public class Day4P1
    {
        static void Main()
        {
            string filePath = @"C:\Users\Michael Terekhov\advent_of_code2023\day4\inputs\2.txt";
            
            int res = 0;

            using(FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int byteRead;

                string currNum = "";
                bool inWinNums = false;
                bool inMyNums = false;
                int winNums = 0;
                int currWin = 0;

                Dictionary<string, string> winValuesDict = new Dictionary<string, string>{};              

                while((byteRead = fileStream.ReadByte()) != -1)
                {
                    if((char)byteRead == '\n')
                    {
                        if(currNum == "")
                        {
                            res += currWin;

                            currNum = "";
                            inWinNums = false;
                            inMyNums = false;
                            winNums = 0;
                            currWin = 0;
                            winValuesDict = new Dictionary<string, string>{};
                        }
                        else
                        {
                            fileStream.Seek(-1, SeekOrigin.Current);
                        }
                        
                    }
                    
                    if((char)byteRead == ':')
                    {
                        inWinNums = true;
                        //fileStream.Seek(1, SeekOrigin.Current);
                    } 
                    else if(inWinNums)
                    {
                        bool isDigit = char.IsDigit((char)byteRead);

                        if(isDigit)
                        {
                            currNum += (char)byteRead;    
                        }
                        else 
                        {
                            if(currNum != "")
                            {
                                winValuesDict.Add(currNum, currNum);
                                currNum = "";
                            }

                            if((char)byteRead == '|')
                            {
                                inWinNums = false;
                                inMyNums = true;
                            }   
                        }
                    }
                    else if(inMyNums)
                    {
                        bool isDigit = char.IsDigit((char)byteRead);

                        if(isDigit)
                        {
                            currNum += (char)byteRead;    
                        }
                        else
                        {
                            if(currNum != "")
                            {
                                if(winValuesDict.ContainsKey(currNum))
                                {
                                    currWin = (int)Math.Pow(2.00, winNums);
                                    winNums++;
                                }

                                currNum = "";
                            }
                        }
                    } 
                
                }

            }
            Console.WriteLine("Result: " + res);
        }
    }
}

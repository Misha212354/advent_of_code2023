using System.IO;
using System;
using System.Diagnostics;

namespace AdventOfCode2023
{
    class Day1P2
    {
        static long TicksToNanoseconds(long ticks)
        {
            // One tick represents 100 ns
            const long NanosecondsPerTick = 100;
            return ticks * NanosecondsPerTick;
        }
        static Dictionary<string, string> WordToDigitMapping = new Dictionary<string, string>
        {
            { "one", "1" },
            { "two", "2" },
            { "six", "6" },
            { "four", "4" },
            { "five", "5" },
            { "nine", "9" },
            { "three", "3" },
            { "seven", "7" },
            { "eight", "8" }
        };

        static string WordToDigit(string curr_word)
    {
        // Check if the word and length are present in the dictionary
        if (WordToDigitMapping.ContainsKey(curr_word))
        {
            return WordToDigitMapping[curr_word];
        }

        return "";
    }

        static void Main()
        {
            
            string filePath = @"C:\Users\Michael Terekhov\advent_of_code2023\day1\inputs\day1\p2\2.txt";
            
            int sum = 0;
            
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {

                int byteRead;
                
                string first = "";
                string last = "";
                
                string curr_word = "";
                int curr_len = 0;
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                while ((byteRead = fileStream.ReadByte()) != -1)
                {   
                    // if reached the end
                    if((char)byteRead == '\n')
                    {
                        
                        if(curr_word == "")
                        {
                            sum += int.Parse(first + last);
                            first = "";
                            last = "";
                            curr_word = "";
                            curr_len = 0;

                        }
                        else
                        {
                            fileStream.Seek(-curr_len, SeekOrigin.Current);

                            curr_word = "";
                            curr_len = 0;
                            
                        }

                    }
                    else
                    {
                        bool IsDigit = char.IsDigit((char)byteRead);
                        if(IsDigit)
                        {
                            // if digit first in the collection a word then just use it
                            if(curr_word == "")
                            {
                                if(first == "")
                                {   
                                    first = (char)byteRead + "";
                                    last = first;
                                }
                                else
                                {
                                    last = (char)byteRead + "";
                                    
                                }
                            }
                            // if part of the word sequance
                            else
                            {
                                
                                curr_word += (char)byteRead + "";
                                curr_len += 1;

                                if(curr_len > 5) 
                                {
                                    //stop checking and move back
                                    fileStream.Seek(-curr_len+1, SeekOrigin.Current);

                                    curr_word = "";
                                    curr_len = 0;
                                    
                                }
                            }
                        }
                        else if(!IsDigit)
                        {
                            curr_word += (char)byteRead + "";
                            curr_len += 1;   
                            
                            string curr_digit;

                            if(curr_len >= 3 && curr_len <= 5)
                            {
                                curr_digit = WordToDigit(curr_word);

                                if(curr_digit != "" && first == "")
                                {
                                    
                                    first = curr_digit;
                                    last = curr_digit;
                                    // we need to go back -1 because we can have matching e or n
                                    fileStream.Seek(-1, SeekOrigin.Current);
                                    
                                    curr_word = "";
                                    curr_len = 0;


                                }
                                else if(curr_digit != "" && first != "")
                                {
                                    last = curr_digit;
                                    // we need to go back -1 because we can have matching e or n
                                    fileStream.Seek(-1, SeekOrigin.Current);
                                    curr_word = "";
                                    curr_len = 0;

                                }
                            }
                            else if(curr_len > 5) 
                            {
                                //stop checking and move back
                                fileStream.Seek(-curr_len+1, SeekOrigin.Current);

                                curr_word = "";
                                curr_len = 0;
                            }

                        }
                    }               
                }
                sum += int.Parse(first + last);
                stopwatch.Stop();
                long elapsedTicks = stopwatch.ElapsedTicks;
                long elapsedNanoseconds = TicksToNanoseconds(elapsedTicks);

                Console.WriteLine($"Elapsed Time: {elapsedNanoseconds} nanoseconds");
            }
            
            Console.WriteLine("Sum: " + sum);            
        }
    }

}
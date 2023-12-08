using System.IO;

namespace AdventOfCode2023
{
    class Day2P2
    {
        static (int number, int length) IdentifyNumber(char d1, char d2, char d3)
        {
            bool isDigit1 = char.IsDigit(d1);
            bool isDigit2 = char.IsDigit(d2);
            bool isDigit3 = char.IsDigit(d3);

            if(isDigit1 && isDigit2 && isDigit3)
            {
                string res1 = d1 + "";
                string res2 = d2 + "";
                string res3 = d3 + "";

                return (int.Parse(res1 + res2 + res3), 3);
            }
            else if (isDigit1 && isDigit2)
            {
                string res1 = d1 + "";
                string res2 = d2 + "";

                return (int.Parse(res1 + res2), 2);
            }
            else if (isDigit1)
            {
                string res1 = d1 + "";

                return (int.Parse(res1), 1);
            }
            return (0, 0);
        }

        static void Main()
        {
            string filePath = @"C:\Users\Michael Terekhov\advent_of_code2023\day2\inputs\2.txt";

            int sum = 0;
            
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int byteRead;

                int game = 0;
                int currRed = 0;
                int currGreen = 0;
                int currBlue = 0;

                while((byteRead = fileStream.ReadByte()) != -1)
                {                   
                    if((char)byteRead == '\n')
                    {
                        sum += (currRed * currGreen * currBlue);

                        game = 0;
                        currRed = 0;
                        currGreen = 0;
                        currBlue = 0;

                    }
                    else
                    {
                        bool isDigit = char.IsDigit((char)byteRead);

                        if(isDigit)
                        {
                            
                            char d1 = (char)byteRead;
                            byteRead = fileStream.ReadByte();
                            char d2 = (char)byteRead;
                            byteRead = fileStream.ReadByte();
                            char d3 = (char)byteRead;

                            (int Number, int Length) comb = IdentifyNumber(d1, d2, d3);

                            fileStream.Seek(-2, SeekOrigin.Current);

                            if(game == 0)
                            {
                                game = comb.Number;
                            }
                            else
                            {
                                
                                fileStream.Seek(comb.Length, SeekOrigin.Current);
                                byteRead = fileStream.ReadByte();
                                char charByte = (char)byteRead;
                                if((char)byteRead == 'r')
                                {
                                    if(currRed < comb.Number)
                                    {
                                        currRed = comb.Number;
                                    }
                                }
                                else if((char)byteRead == 'g')
                                {
                                    if(currGreen < comb.Number)
                                    {
                                        currGreen = comb.Number;
                                    }
                                }
                                else if((char)byteRead == 'b')
                                {
                                    if(currBlue < comb.Number)
                                    {
                                        currBlue = comb.Number;
                                    }
                                }
                            }
                        } 
                    }

                }
                 Console.WriteLine("Sum: " + sum);
            }

        }
    }
}
using System.IO;
using System;
using System.Diagnostics;

class Programm
{
    static void Main()
    {
        

        string filePath = @"C:\Users\Michael Terekhov\advent_of_code2023\day1\inputs\day1\p1\2.txt";
        
        int sum = 0;
        
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int byteRead;
            char first = 'd';
            char last = 'd';
            
            while ((byteRead = fileStream.ReadByte()) != -1)
            {
                if((char)byteRead == '\n')
                {
                    sum += int.Parse(first.ToString() + last.ToString());
                    //Console.WriteLine(sum);
                    first = 'd';
                    last = 'd';
                    //Console.WriteLine("-------------------------------------------------------");
                    
                }else{
                    // if the first digit
                    bool IsDigit= char.IsDigit((char)byteRead);

                    if(IsDigit && first == 'd')
                    {
                        first = (char)byteRead;
                        last = first; 

                    }else if (IsDigit && first != 'd'){
                        last = (char)byteRead;
                    }
                    //Console.WriteLine("First: "+first);
                    //Console.WriteLine("Last: "+last);
                }
            }
            sum += int.Parse(first.ToString() + last.ToString());
            Console.WriteLine(sum);

        }

        
    }
}

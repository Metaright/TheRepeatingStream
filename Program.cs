using System;
using System.Threading;

namespace TheRepeatingStream
{
    class Program
    {
        static void Main(string[] args)
        {
            RecentNumbers num = new RecentNumbers { FirstRecent = -1, SecondRecent = -2 };
            Thread generateThread = new Thread(num.GenerateNumbers);
            Thread checkThread = new Thread(num.CheckForMatch);

            generateThread.Start();
            checkThread.Start();
        }
    }

    public class RecentNumbers
    {
        public int FirstRecent { get; set; }
        public int SecondRecent { get; set; }
        private readonly object RecentLock = new object();

        public void GenerateNumbers()
        {
            Random random = new Random();
            while (true)
            {
                int newNumber = random.Next(10);

                lock (RecentLock)
                {
                    SecondRecent = FirstRecent;
                    FirstRecent = newNumber;
                }

                Console.WriteLine(newNumber);
                Thread.Sleep(1000);
            }
        }

        public void CheckForMatch()
        {
            bool isMatch = false;

            while (true)
            {
                Console.ReadKey(true);

                lock (RecentLock)
                {
                    if (FirstRecent == SecondRecent) isMatch = true;
                    else isMatch = false;
                }

                if (isMatch)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You did it!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You suck!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

    }


}

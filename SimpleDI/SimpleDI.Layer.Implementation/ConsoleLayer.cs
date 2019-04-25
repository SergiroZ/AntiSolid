using System;

namespace SimpleDI
{
    public class ConsoleLayer : ILayer
    {
        public void Write(string text)
        {
            Console.WriteLine("Console: " + text);
        }
    }
}
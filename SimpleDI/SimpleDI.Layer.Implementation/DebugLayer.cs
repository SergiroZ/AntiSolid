using System;

namespace SimpleDI
{
    public class DebugLayer : ILayer
    {
        public void Write(string text)
        {
            Console.WriteLine("Debug: " + text);
        }
    }
}
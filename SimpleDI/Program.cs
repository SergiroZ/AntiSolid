using System;

namespace SimpleDI
{
    internal static class Program
    {
        private static void Main()
        {
            var logFirst = new LoggingInTo(true);
            logFirst.Write("Hello !!");

            var logSecond = new Logging(new DebugLayer()); //Ioc and DIP
            logSecond.Write("Hello DI !!!");

            //*************** IoC container **********
            DIContainer.Register<ILayer, ConsoleLayer>();

            ILayer layer = DIContainer.Resolve<ILayer>();
            layer.Write("Hello from IoC!!!");

            Console.WriteLine();
        }
    }
}
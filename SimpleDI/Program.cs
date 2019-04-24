using System;
using System.Collections.Generic;

namespace SimpleDI
{
    public interface ILayer
    {
        void Write(string text);
    }

    public class ConsoleLayer : ILayer
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }

    public class DebugLayer : ILayer
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }

    /// <summary>
    ///     Выбор места логирования (true - Console, false - Debug)
    /// </summary>
    public class LoggingInTo
    {
        private readonly ILayer _instance;

        public LoggingInTo(bool i) =>
            _instance = i ? (ILayer) new ConsoleLayer() : new DebugLayer();

        public void Write(string text)
        {
            _instance.Write(text);
        }
    }

    /// <summary>
    ///     Логирование
    /// </summary>
    public class Logging
    {
        private readonly ILayer _instance;

        /// <summary>
        ///     Абстракции не зависят от деталей. Детали зависят от абстракций.
        ///     Т.е. мы не знаем деталей того, что происходит в классе Logging,
        ///     мы просто передаем через конструктор класс, реализующий
        ///     необходимую абстракцию.
        /// </summary>
        /// <param name="instance">класс, реализующий необходимую абстракцию</param>
        public Logging(ILayer instance) => _instance = instance;

        public void Write(string text)
        {
            _instance.Write(text);
        }
    }

    /// <summary>
    ///     DI контейнер
    /// </summary>
    public static class DIContainer
    {
        private static readonly Dictionary<Type, Type> RegisteredObjects =
            new Dictionary<Type, Type>();

        public static dynamic Resolve<TKey>()
        {
            return Activator.CreateInstance(RegisteredObjects[typeof(TKey)]);
        }

        public static void Register<TKey, TConcrete>() where TConcrete : TKey
        {
            RegisteredObjects[typeof(TKey)] = typeof(TConcrete);
        }
    }


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
        }
    }
}
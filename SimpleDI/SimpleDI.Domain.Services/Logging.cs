namespace SimpleDI
{
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
}
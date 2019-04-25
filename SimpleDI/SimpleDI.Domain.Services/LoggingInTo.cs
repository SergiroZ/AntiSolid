namespace SimpleDI
{
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
}
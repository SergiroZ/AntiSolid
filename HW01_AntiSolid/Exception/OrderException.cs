namespace HW01_AntiSolid.Exception
{
    public class OrderException : System.Exception
    {
        public OrderException(string message, System.Exception innerException)
            : base(message, innerException) { }
    }
}
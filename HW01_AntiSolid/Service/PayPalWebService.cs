namespace HW01_AntiSolid.Service
{
    /// <summary>
    /// Нарушение принципа замещения Барбары Лисков
    /// Liskov substitution principle (LSP):
    /// метод MakeRefund согласно принципу LSP должен быть взаимозаменяем
    /// для классов PayPalWebService и WorldPayWebService
    /// </summary>
    public class PayPalWebService
    {
        public string GetTransactionToken(string username, string password)
        {
            return "Hello from PayPal";
        }

        public string MakeRefund(decimal amount, string transactionId, string token)
        {
            return "Auth";
        }
    }

    public class WorldPayWebService
    {
        public string MakeRefund(decimal amount, string transactionId, string username,
            string password, string productId)
        {
            return "Success";
        }
    }

}
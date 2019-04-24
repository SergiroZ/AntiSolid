using System;
using System.Globalization;
using HW01_AntiSolid.Entity;

namespace HW01_AntiSolid
{
    internal static class Program
    {
        private static void Main()
        {
            var shoppingCart = new ShoppingCart("test@customer.com");
            shoppingCart.Add(new Item {Description = "Weight fish", Quantity = 1});
            shoppingCart.Add(new Item {Description = "Each oil", Quantity = 5});

            Console.WriteLine(shoppingCart.TotalAmount());

            var order = new Order();
            order.Checkout(
                shoppingCart,
                paymentDetails: new PaymentDetails
                {
                    CardholderName = "Ivan",
                    CreditCardNumber = "1111 2222 3333 4444",
                    ExpiryDate = DateTime.ParseExact(
                        "12/11/17 2:52:35 PM", 
                        "yy/MM/dd h:mm:ss tt",
                        CultureInfo.InvariantCulture),
                    PaymentMethod = PaymentMethod.CreditCard
                },
                notifyCustomer: true);
        }
    }
}
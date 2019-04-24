using System;
using HW01_AntiSolid.Exception;
using HW01_AntiSolid.Service;

namespace HW01_AntiSolid.Entity
{
    /// <summary>
    /// Нарушение принципа единственной ответственности 
    /// Single responsibility principle (SRP):
    /// класс Order выполняет много разнородных вещей - проверка после размещения
    /// заказа клиентом, отправка электронной почты, регистрация исключений,
    /// использование кредитных карт
    /// 
    /// </summary>
    public class Order
    {
        public void Checkout(ShoppingCart shoppingCart, PaymentDetails paymentDetails,
            bool notifyCustomer)
        {
            if (paymentDetails.PaymentMethod == PaymentMethod.CreditCard)
                ChargeCard(paymentDetails, shoppingCart);

            ReserveInventory(shoppingCart);

            if (notifyCustomer) NotifyCustomer(shoppingCart);
        }

        public void NotifyCustomer(ShoppingCart cart)
        {
            var customerEmail = cart.CustomerEmail;
            if (string.IsNullOrEmpty(customerEmail)) return;
            try
            {
                Console.WriteLine($"Post to {customerEmail} good message.");
                //construct the email message and send it, implementation ignored
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                //log the emailing error, implementation ignored
            }
        }

        public void ReserveInventory(ShoppingCart cart)
        {
            foreach (var item in cart.OrderItems)
                try
                {
                    var inventoryService = new InventoryService();
                    inventoryService.Reserve(item.Description, item.Quantity);
                }
                catch (InsufficientInventoryException ex)
                {
                    throw new OrderException(
                        "Insufficient inventory for item " + item.Description, ex);
                }
                catch (System.Exception ex)
                {
                    throw new OrderException("Problem reserving inventory", ex);
                }
        }

        public void ChargeCard(PaymentDetails paymentDetails, ShoppingCart cart)
        {
            var paymentService = new PaymentService();

            try
            {
                paymentService.Credentials = "Credentials";
                paymentService.CardNumber = paymentDetails.CreditCardNumber;
                paymentService.ExpiryDate = paymentDetails.ExpiryDate;
                paymentService.NameOnCard = paymentDetails.CardholderName;
                paymentService.AmountToCharge = cart.TotalAmount();

                paymentService.Charge();
            }
            catch (AccountBalanceMismatchException ex)
            {
                throw new OrderException(
                    "The card gateway rejected the card based on the address provided.",
                    ex);
            }
            catch (System.Exception ex)
            {
                throw new OrderException("There was a problem with your card.", ex);
            }
        }
    }
}
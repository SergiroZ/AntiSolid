using System;
using System.Collections.Generic;

namespace HW01_AntiSolid
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public string Identifier { get; set; }
        public int Quantity { get; set; }
    }

    public enum PaymentMethod
    {
        CreditCard,
        Cheque
    }

    public class PaymentDetails
    {
        public PaymentMethod PaymentMethod { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CardholderName { get; set; }
    }

    public class Order
    {
        public void Checkout(ShoppingCart shoppingCart, PaymentDetails paymentDetails,
            bool notifyCustomer)
        {
            if (paymentDetails.PaymentMethod == PaymentMethod.CreditCard)
            {
                ChargeCard(paymentDetails, shoppingCart);
            }

            ReserveInventory(shoppingCart);

            if (notifyCustomer)
            {
                NotifyCustomer(shoppingCart);
            }
        }

        public void NotifyCustomer(ShoppingCart cart)
        {
            var customerEmail = cart.CustomerEmail;
            if (string.IsNullOrEmpty(customerEmail)) return;
            try
            {
                //construct the email message and send it, implementation ignored
            }
            catch (Exception)
            {
                //log the emailing error, implementation ignored
            }
        }

        public void ReserveInventory(ShoppingCart cart)
        {
            foreach (var item in cart.OrderItems)
            {
                try
                {
                    var inventoryService = new InventoryService();
                    inventoryService.Reserve(item.Identifier, item.Quantity);
                }
                catch (InsufficientInventoryException ex)
                {
                    throw new OrderException(
                        "Insufficient inventory for item " + item.Identifier, ex);
                }
                catch (Exception ex)
                {
                    throw new OrderException("Problem reserving inventory", ex);
                }
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
            catch (Exception ex)
            {
                throw new OrderException("There was a problem with your card.", ex);
            }
        }
    }

    public class OrderException : Exception
    {
        public OrderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class InventoryService
    {
        public void Reserve(string identifier, int quantity)
        {
            throw new InsufficientInventoryException();
        }
    }

    public class InsufficientInventoryException : Exception
    {
    }

    public class PaymentService
    {
        public string CardNumber { get; set; }
        public string Credentials { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string NameOnCard { get; set; }
        public decimal AmountToCharge { get; set; }

        public void Charge()
        {
            throw new AccountBalanceMismatchException();
        }
    }

    public class AccountBalanceMismatchException : Exception
    {
    }

    public class ShoppingCart
    {
        private readonly List<OrderItem> _orderItems;

        public ShoppingCart()
        {
            _orderItems = new List<OrderItem>();
        }

        public IEnumerable<OrderItem> OrderItems => _orderItems;

        public string CustomerEmail { get; set; }

        public void Add(OrderItem orderItem)
        {
            _orderItems.Add(orderItem);
        }

        public decimal TotalAmount()
        {
            var total = 0m;
            foreach (var orderItem in OrderItems)
            {
                if (orderItem.Identifier.StartsWith("Each"))
                {
                    total += orderItem.Quantity * 4m;
                }
                else if (orderItem.Identifier.StartsWith("Weight"))
                {
                    total += orderItem.Quantity * 3m / 1000; //1 kilogram
                }
                else if (orderItem.Identifier.StartsWith("Spec"))
                {
                    total += orderItem.Quantity * .3m;
                    var setsOfFour = orderItem.Quantity / 4;
                    total -= setsOfFour * .15m; //discount on groups of 4 items
                }
            }

            return total;
        }
    }

    internal class Program
    {
        private static void Main()
        {
        }
    }
}
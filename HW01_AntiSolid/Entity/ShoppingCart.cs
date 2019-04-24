using System.Collections.Generic;

namespace HW01_AntiSolid.Entity
{
    /// <summary>
    ///     Нарушение принципа Открытости/Закрытости
    ///     Open-Closed principle (OCP):
    ///     сложная логика TotalAmount при вероятном расширении условий
    ///     ценообразования повлечёт изменение этой функции, т.е.
    ///     возникнет необходимость множественного пересмотра оператора if-else,
    ///     для его применения с новыми правилами и модифицикацией существующих,
    ///     а это существенно усложнит отладку и поддержание кода.
    /// </summary>
    public class ShoppingCart
    {
        private readonly List<Item> _orderItems;

        public ShoppingCart(string customerEmail)
        {
            CustomerEmail = customerEmail;
            _orderItems = new List<Item>();
        }

        public IEnumerable<Item> OrderItems => _orderItems;

        public string CustomerEmail { get; }

        public void Add(Item item)
        {
            _orderItems.Add(item);
        }

        public decimal TotalAmount()
        {
            var total = 0m;
            foreach (var orderItem in OrderItems)
                if (orderItem.Description.StartsWith("Each"))
                {
                    total += orderItem.Quantity * 4m;
                }
                else if (orderItem.Description.StartsWith("Weight"))
                {
                    total += orderItem.Quantity * 3m / 1000; //1 kilogram
                }
                else if (orderItem.Description.StartsWith("Spec"))
                {
                    total += orderItem.Quantity * .3m;
                    var setsOfFour = orderItem.Quantity / 4;
                    total -= setsOfFour * .15m; //discount on groups of 4 items
                }

            return total;
        }
    }
}
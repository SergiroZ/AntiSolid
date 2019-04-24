using System;

namespace HW01_AntiSolid.Exception
{
    public class InsufficientInventoryException : System.Exception
    {
        public InsufficientInventoryException()
        {
            Console.WriteLine("Insufficient Inventory for an item..");
        }
    }
}
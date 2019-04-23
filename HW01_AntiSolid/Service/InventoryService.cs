using HW01_AntiSolid.Exception;

namespace HW01_AntiSolid.Service
{
    public class InventoryService
    {
        public void Reserve(string identifier, int quantity)
        {
            throw new InsufficientInventoryException();
        }
    }
}
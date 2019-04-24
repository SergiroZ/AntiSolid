using System;

namespace HW01_AntiSolid.Exception
{
    public class AccountBalanceMismatchException : System.Exception
    {
        public AccountBalanceMismatchException()
        {
            Console.WriteLine("Your Account Balance Mismatch.");
        }
    }
}
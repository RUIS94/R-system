using System.Security.Principal;
using Model.DomainModels;

namespace Business.Validation
{
    public static class AccountValidator
    {

        public static void ValidateAccount(Account account)
        {
            if (account == null)
                throw new ArgumentException("Account cannot be null");
        }
        public static void ValidateBalance(decimal balance)
        {
            if (balance < 0)
                throw new ArgumentException("Balance cannot be negative");
        }
        public static void ValidateCustomerID(int customerID)
        {
            if (customerID <= 0)
                throw new ArgumentException("Customer ID must be greater than zero");
        }
    }
}

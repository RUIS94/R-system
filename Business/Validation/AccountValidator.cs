using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Business.Validation
{
    public static class AccountValidator
    {
        public static void ValidateAccount(Account account)
        {
            if (account == null)
                throw new ArgumentException("Account cannot be null");

            if (account.Balance < 0)
                throw new ArgumentException("Balance cannot be negative");

            if (account.CustomerID <= 0)
                throw new ArgumentException("Customer ID must be greater than zero");
        }
    }
}

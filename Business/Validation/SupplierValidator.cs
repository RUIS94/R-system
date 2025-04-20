using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Business.Validation
{
    public static class SupplierValidator
    {
        public static void Validate(Supplier supplier)
        {
            if (supplier == null)
                throw new ArgumentNullException(nameof(supplier));

            CommonValidator.ValidateRequired(supplier.Name, "Supplier name");

            if (!string.IsNullOrWhiteSpace(supplier.Email))
                CommonValidator.ValidateEmail(supplier.Email);

            if (!string.IsNullOrWhiteSpace(supplier.Phone))
                CommonValidator.ValidatePhone(supplier.Phone);

            if (!string.IsNullOrWhiteSpace(supplier.StreetAddress))
                CommonValidator.ValidateAddress(supplier.StreetAddress);

            CommonValidator.ValidateRequired(supplier.City, "City");
            CommonValidator.ValidateRequired(supplier.State, "State");
            CommonValidator.ValidateRequired(supplier.Country, "Country");
            CommonValidator.ValidateRequired(supplier.ZipCode, "Zip code");
        }
    }
}

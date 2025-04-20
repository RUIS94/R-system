using Model.DomainModels;

namespace Business.Validation
{
    public static class ProductValidator
    {
        public static void ValidateName(string? name)
        {
            CommonValidator.ValidateRequired(name ?? "", "Product name");
        }

        public static void ValidateBarcode(string? barcode)
        {
            CommonValidator.ValidateRequired(barcode ?? "", "Barcode");
        }

        public static void ValidateCategory(string? category)
        {
            CommonValidator.ValidateRequired(category ?? "", "Category");
        }

        public static void ValidatePrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentException("Price cannot be negative");
        }

        public static void ValidateCost(decimal cost)
        {
            if (cost < 0)
                throw new ArgumentException("Cost cannot be negative");
        }

        public static void ValidateInventory(int inventory)
        {
            if (inventory < 0)
                throw new ArgumentException("Inventory cannot be negative");
        }

        //public static void ValidateGst(decimal gst)
        //{
        //    if (gst < 0 || gst > 1)
        //        throw new ArgumentException("GST must be between 0 and 1");
        //}

        public static void Validate(Product product)
        {
            CommonValidator.ValidateStringLength(product.Name!, "Product Name", 2, 100);
            CommonValidator.ValidateStringLength(product.Description!, "Product Description", 2, 500);
            CommonValidator.ValidateDecimalRange(product.Gst, "GST", 0, 1);
            ValidateName(product.Name);
            ValidateBarcode(product.Barcode);
            ValidateCategory(product.Category);
            ValidatePrice(product.Price);
            ValidateCost(product.Cost);
            ValidateInventory(product.Inventory);
            //ValidateGst(product.Gst);
        }
    }
}

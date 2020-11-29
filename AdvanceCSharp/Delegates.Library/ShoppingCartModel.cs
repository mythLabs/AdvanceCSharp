using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delegates.Library
{
    public class ShoppingCartModel
    {
        public delegate void MentionDiscount(decimal subtotal);

        public List<ProductModel> Items { get; set; } = new List<ProductModel>();

        public decimal GenerateTotal(MentionDiscount mentionDiscount,
            Func<List<ProductModel>, decimal, decimal> calculateDiscountTotal,
            Action<string> showDiscounting)
        {
            decimal subtotal = Items.Sum(x => x.Price);

            mentionDiscount(subtotal);

            showDiscounting("We are applying discounts");

            return calculateDiscountTotal(Items, subtotal);
        }
    }
}

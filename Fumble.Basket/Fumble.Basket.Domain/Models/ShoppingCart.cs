namespace Fumble.Basket.Domain.Models
{
    public class ShoppingCart
    {
        public ShoppingCart(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
        public IList<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }

                return totalPrice;
            }
        }
    }
}

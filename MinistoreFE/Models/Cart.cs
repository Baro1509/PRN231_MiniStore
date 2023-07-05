namespace MinistoreFE.Models {
    public class Cart {
        public string StaffId { get; set; }
        public List<CartItem> CartItems { get; set; }
    }

    public class CartItem {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
    }
}

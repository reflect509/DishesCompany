namespace DishesCompany
{
    public class Product_orders
    {
        public string Product_articul { get; set; }
        public int Order_id { get; set; }
        public int Amount { get; set; } = 1;

        public Products Product_entity { get; set; }
        public Orders Order_entity { get; set; }
    }
}

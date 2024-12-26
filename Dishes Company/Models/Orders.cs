using System.ComponentModel.DataAnnotations;

namespace DishesCompany
{
    public class Orders
    {
        [Key] public int Order_id { get; set; }
        public DateOnly Order_date { get; set; }
        public DateOnly? Delivery_date { get; set; }
        public int Delivery_place_id { get; set; }
        public int? User_id { get; set; }
        public string? Receiving_code { get; set; }
        public string Order_status { get; set; }

        public Delivery_places DeliveryPlace_entity { get; set; }
        public Users User_entity { get; set; }
        public List<Product_orders> Product_order_entities { get; set; }

        public Orders(DateOnly order_date, DateOnly? delivery_date, int delivery_place_id,
            int? user_id, string order_status)
        {
            Order_date = order_date;
            Delivery_date = delivery_date;
            Delivery_place_id = delivery_place_id;
            User_id = user_id;
            Order_status = order_status;
        }
    }
}

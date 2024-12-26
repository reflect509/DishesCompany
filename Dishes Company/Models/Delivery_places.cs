using System.ComponentModel.DataAnnotations;

namespace DishesCompany
{
    public class Delivery_places
    {
        [Key] public int Delivery_place_id { get; set; }
        public string Delivery_place_address { get; set; }

        public List<Orders> Order_entities { get; set; }
    }
}

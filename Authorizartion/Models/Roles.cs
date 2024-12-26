using System.ComponentModel.DataAnnotations;

namespace DishesCompany
{
    public class Roles
    {
        [Key] public int Role_id { get; set; }
        public string Role_name { get; set; }

        public List<Users> User_entities { get; set; }
    }
}

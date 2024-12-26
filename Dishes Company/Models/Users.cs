using System.ComponentModel.DataAnnotations;

namespace DishesCompany
{
    public class Users
    {
        [Key] public int User_id { get; set; }
        public string Full_name { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public int Role_id { get; set; }


        public Roles Role_entity { get; set; }
        public List<Orders> Order_entities { get; set; }

        public Users(string full_name, string login, string pass)
        {
            Full_name = full_name;
            Login = login;
            Pass = pass;
            Role_id = 1;
        }
        public Users()
        {
            Full_name = "Гость";
            Role_id = 0;
        }
    }
}

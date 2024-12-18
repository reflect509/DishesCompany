using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishesCompany
{
    public class Roles
    {
        [Key] public int Role_id { get; set; }
        public string Role_name { get; set; }
        
        public List<Users> User_entities { get; set; }
    }
}

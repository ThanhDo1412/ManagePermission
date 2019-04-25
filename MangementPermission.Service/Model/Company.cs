using System.Collections.Generic;

namespace MangementPermission.Service.Model
{
    public class Company
    {
        public Manager CEO { get; set; }
        public List<Manager> Managers { get; set; }
        public List<User> Users { get; set; }
    }
}

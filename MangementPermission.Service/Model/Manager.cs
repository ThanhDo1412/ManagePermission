using System.Collections.Generic;
using System.Linq;

namespace MangementPermission.Service.Model
{
    public class Manager : User
    {
        public List<User> Users { get; set; }
        public List<Manager> Managers { get; set; }

        //public override string GetPermission()
        //{
        //    var 
        //}

        private List<string> GetChildrentPermission()
        {
            var result = new List<string>();
            if (Users.Count == 0 && Managers.Count == 0)
            {
                return new List<string>();
            }
            else
            {
                
            }
        }
    }
}

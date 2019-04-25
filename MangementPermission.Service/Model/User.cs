using System.Collections.Generic;
using System.Linq;

namespace MangementPermission.Service.Model
{
    public class User
    {
        private List<string> permissions;

        public List<string> Permissions {
            get => permissions;
            set => permissions = value;
        }

        public virtual string GetPermission()
        {
            return string.Join(", ", this.permissions);
        }
    }
}

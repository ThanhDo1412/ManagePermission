using System.Collections.Generic;

namespace MangementPermission.Service.Model
{
    public class User
    {
        public List<string> Permissions { get; set; }
        public List<int> MemberIndex { get; set; }
        public List<string> FullPermissions { get; set; }
    }
}

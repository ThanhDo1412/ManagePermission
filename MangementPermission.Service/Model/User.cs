using System.Collections.Generic;

namespace MangementPermission.Service.Model
{
    public class User
    {
        public string[] Permissions { get; set; }
        public List<int> MemberIndex { get; set; }
        public string[] FullPermissions { get; set; }
    }
}

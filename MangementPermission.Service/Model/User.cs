using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MangementPermission.Service.Model
{
    public class User
    {
        private string[] permissions;
        private LinkedList<User> members;
        private int index;

        public User(string[] permissions, int index)
        {
            this.permissions = permissions;
            members = new LinkedList<User>();
            this.index = index;
        }

        public int Index
        {
            get => index;
            set => index = value;
        }

        public LinkedList<User> Members
        {
            get => members;
            set => members = value;
        }

        public void AddMember(string[] permissions, int index)
        {
            members.AddFirst(new User(permissions, index));
        }

        public User GetMember(int i)
        {
            foreach (var n in members)
                if (--i == 0)
                    return n;
            return null;
        }

        

        public void Traverse(User user, Action<string[]> visitor)
        {
            visitor(user.permissions);
            foreach (var member in user.members)
            {
                Traverse(member, visitor);
            }
        }
    }
}

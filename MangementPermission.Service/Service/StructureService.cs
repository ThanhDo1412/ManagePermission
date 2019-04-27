using System.Collections.Generic;
using System.Linq;
using MangementPermission.Service.Model;

namespace MangementPermission.Service.Service
{
    public class StructureService
    {
        /// <summary>
        /// Create structure of company in tree with ceo is root
        /// </summary>
        /// <param name="input"></param>
        /// <returns>
        /// null: create fail
        /// User: create success
        /// </returns>
        public User CreateCompanyStructure(List<string> input)
        {
            //Check quatity of company
            var total = ValidationQualityOfUsers(input[0]);
            if (total == 0)
            {
                return null;
            }

            //Set permission for CEO
            var ceoPermissions = ValidationPermission(input[1]);
            if (ceoPermissions == null)
            {
                return null;
            }
            var ceo = new User(ceoPermissions, 0);

            //Set permission and manager for all user
            for (var i = 2; i < total + 2; i++)
            {
                var permissions = ValidationPermission(input[i]);
                if (permissions == null)
                {
                    return null;
                }

                var position = ValidationManager(input[i + total]);
                if (position == -1)
                {
                    return null;
                }

                if (position == 0)
                {
                    ceo.AddMember(permissions, i - 1);
                }
                else
                {
                    var manager = GetUserByIndex(position, ceo);
                    manager.AddMember(permissions, i - 1);
                }
            }

            return ceo;
        }

        public User GetUserByIndex(int index, User ceo)
        {
            var queue = new Queue<User>();
            queue.Enqueue(ceo);

            while (queue.Count != 0)
            {
                var user = queue.Dequeue();

                foreach (var member in user.Members)
                {
                    if (member.Index == index)
                    {
                        return member;
                    }
                    queue.Enqueue(member);
                }
            }

            return null;
        }

        #region validation

        /// <summary>
        /// Validation quatity of user
        /// </summary>
        /// <param name="input">quatity of user</param>
        /// <returns>
        /// 0 if invalid
        /// int if valid
        /// </returns>
        private int ValidationQualityOfUsers(string input)
        {
            return int.TryParse(input, out var total) && total < 100000 && total > 0 ? total : 0;
        }

        /// <summary>
        /// Validation permission of user
        /// </summary>
        /// <param name="input">permission of user</param>
        /// <returns>
        /// null: invalid
        /// array of permission: valid
        /// </returns>
        private string[] ValidationPermission(string input)
        {
            var permissions = input.Split(' ');
            return permissions.Length > 0 && permissions.Length < 100 ? permissions : null;
        }

        /// <summary>
        /// Check manager of user
        /// </summary>
        /// <param name="input">CEO or position of user</param>
        /// <returns>
        /// 0 if CEO
        /// number if manager is user
        /// -1 if false
        /// </returns>
        private int ValidationManager(string input)
        {
            if (input.Equals("CEO"))
            {
                return 0;
            }

            if (int.TryParse(input, out var position) && position > 0 && position < 100000)
            {
                return position;
            }

            return -1;
        }

        #endregion

    }
}

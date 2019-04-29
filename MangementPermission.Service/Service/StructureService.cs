using System;
using MangementPermission.Service.Model;
using System.Collections.Generic;
using System.Linq;

namespace MangementPermission.Service.Service
{
    public class StructureService
    {
        /// <summary>
        /// Create company structure
        /// </summary>
        /// <param name="input"></param>
        /// <returns>
        /// null if create failed
        /// list of user if create succecced
        /// </returns>
        public User[] CreateCompanyStruture(List<string> input)
        {
            //Check quatity of company
            var total = ValidationQualityOfUsers(input[0], input.Count);

            var users = new User[total + 1];
            //Set permission for CEO
            var ceoPermissions = ValidationPermission(input[1]);
            users[0] = new User()
            {
                Permissions = ceoPermissions,
                MemberIndex = new List<int>()
            };

            //Set permission and manager for all user
            for (var i = 2; i < total + 2; i++)
            {
                var currentIndex = i - 1;
                var permissions = ValidationPermission(input[i]);
                var managerIndex = ValidationManager(input[i + total], currentIndex, total + 1);

                //Create new user and add to array
                var user = new User()
                {
                    Permissions = permissions,
                    MemberIndex = new List<int>()
                };
                users[currentIndex] = user;

                //Update member of Manager
                users[managerIndex].MemberIndex.Add(i - 1);
            }

            return users;
        }

        /// <summary>
        /// Get permission of all users in company
        /// </summary>
        /// <param name="company">Array of users in company</param>
        /// <returns>Array permission of all users</returns>
        public List<string> GetPermissionsOfCompany(User[] company)
        {
            for (var i = 0; i < company.Length; i++)
            {
                //Get lastest user
                GetFullPermission(company, company.Length - i - 1);
            }

            return company.Select(x => string.Join(", ", x.FullPermissions)).ToList();
        }

        /// <summary>
        /// Separate input to array user and array queries
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Tuple<List<string>, string[]> SeparateUsersAndQueries(List<string> input)
        {
            var users = new List<string>();
            var index = 0;
            
            foreach (var line in input)
            {
                if (Contanst.Command.Any(x => line.StartsWith(x)))
                {
                    break;
                }
                users.Add(line);
                index++;
            }

            var queries = new string[input.Count - index];
            Array.Copy(input.ToArray(), index, queries, 0, input.Count - index);
            return new Tuple<List<string>, string[]>(users, queries);
        }

        /// <summary>
        /// Execute all queries
        /// </summary>
        /// <param name="users"></param>
        /// <param name="queries"></param>
        /// <returns></returns>
        public List<string> ExecuteQueried(User[] users, string[] queries)
        {
            var output = new List<string>();
            foreach (var query in queries)
            {
                var detail = query.Split(' ');
                if (detail.Length < 2 || string.IsNullOrWhiteSpace(query))
                {
                    throw new Exception(ErrorMessage.QueryInvalid);
                }
                var userIndex = ValidationIndex(detail[1], users.Length);
                
                switch (detail[0])
                {
                    case "ADD":
                        if (detail.Length != 3)
                        {
                            throw new Exception(ErrorMessage.QueryInvalid);
                        }
                        AddPermission(users[userIndex], detail[2]);
                        break;
                    case "REMOVE":
                        if (detail.Length != 3)
                        {
                            throw new Exception(ErrorMessage.QueryInvalid);
                        }
                        RemovePermission(users[userIndex], detail[2]);
                        break;
                    case "QUERY":
                        output.Add(string.Join(", ", GetFullPermission(users, userIndex)));
                        break;
                    default:
                        throw new Exception(ErrorMessage.QueryInvalid);
                }
            }

            return output;
        }

        #region validation

        /// <summary>
        /// Validation quatity of users
        /// </summary>
        /// <param name="input">quatity of user</param>
        /// <param name="inputLenght">lenght of input</param>
        /// <returns> Quatity of users </returns>
        private int ValidationQualityOfUsers(string input, int inputLenght)
        {
            if (!int.TryParse(input, out var total))
            {
                throw new Exception(ErrorMessage.QuantityInvalid);
            }

            if (total >= 100000 || total <= 0)
            {
                throw new Exception(ErrorMessage.QuantityOutOfRange);
            }

            if (total != ((inputLenght - 2) / 2))
            {
                throw new Exception(ErrorMessage.QuantityNotFix);
            }

            return total;
        }

        /// <summary>
        /// Validation permission of user
        /// </summary>
        /// <param name="input">permission of user</param>
        /// <returns> array of permissions </returns>
        private List<string> ValidationPermission(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception(ErrorMessage.PermissionInvalid);
            }

            var permissions = input.Split(' ');
            if (permissions.Length <= 0 || permissions.Length >= 100)
            {
                throw new Exception(ErrorMessage.PermissionOutOfRange);
            }
            return permissions.ToList();
        }

        /// <summary>
        /// Check validation of manager index and return index
        /// </summary>
        /// <param name="input">CEO or position of user</param>
        /// <param name="currentIndex">quatity of user at current</param>
        /// <param name="totalUser">total of users in input</param>
        /// <returns>
        /// 0 if CEO
        /// number if manager is user
        /// </returns>
        private int ValidationManager(string input, int currentIndex, int totalUser)
        {
            if (input.Equals("CEO"))
            {
                return 0;
            }

            if (!int.TryParse(input, out var position)
                || position <= 0 || position >= 100000
                || position >= currentIndex
                || position > totalUser)
            {
                //the user after CEO should be member of CEO
                if (currentIndex == 1)
                {
                    throw new Exception(ErrorMessage.CEONoMember);
                }
                throw new Exception(ErrorMessage.UserInvalid);
            }

            return position;
        }

        /// <summary>
        /// Check validation of manager index and return index
        /// </summary>
        /// <param name="input">CEO or position of user</param>
        /// <param name="totalUser">total of users in input</param>
        /// <returns>
        /// 0 if CEO
        /// number if manager is user
        /// </returns>
        private int ValidationIndex(string input, int totalUser)
        {
            if (input.Equals("CEO"))
            {
                return 0;
            }

            if (!int.TryParse(input, out var position)
                || position > totalUser)
            {
                throw new Exception(ErrorMessage.UserInvalid);
            }

            return position;
        }

        #endregion

        #region Query
        /// <summary>
        /// Add permission for user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="permission"></param>
        private void AddPermission(User user, string permission)
        {
            user.Permissions.Add(permission);
        }

        /// <summary>
        /// Remove permission of user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="permission"></param>
        private void RemovePermission(User user, string permission)
        {
            if (!user.Permissions.Any(m => m.Equals(permission)))
            {
                throw new Exception(ErrorMessage.QueryInvalid);
            }
            user.Permissions.Remove(permission);
        }

        /// <summary>
        /// Update and get full permission
        /// </summary>
        /// <param name="company"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private List<string> GetFullPermission(User[] company, int index)
        {
            var user = company[index];
            user.FullPermissions = user.Permissions;

            if (user.MemberIndex.Any())
            {
                foreach (var memberIndex in user.MemberIndex)
                {
                    user.FullPermissions =
                        user.FullPermissions.Union(company[memberIndex].FullPermissions).OrderBy(x => x).ToList();
                }
            }

            return user.FullPermissions;
        }

        #endregion
    }
}

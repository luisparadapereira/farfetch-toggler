using System;
using Farfetch.Models;
using Farfetch.ServiceManager;
using Farfetch.ServiceManager.Interfaces;

namespace Farfetch.UserAccounts.Service
{
    public class UserAccountService : DbCrudService<User>, IService
    {
        /// <summary>
        /// Overrides the parent class so we can check when inserting
        /// if the item already exists. If it does then updates it.
        /// </summary>
        /// <param name="user">The user to insert</param>
        public new void Insert(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(user.Username)) throw new NullReferenceException("Username was not defined");
            User existingUser = GetByExpression(x => x.Username == user.Username);
            if (existingUser != null) return;
            base.Insert(user);
        }
    }
}
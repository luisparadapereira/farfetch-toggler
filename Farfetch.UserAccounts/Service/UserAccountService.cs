using System;
using System.Collections.Generic;
using Farfetch.CoreUnitOfWork;
using Farfetch.Models;
using Farfetch.ServiceManager;
using Farfetch.ServiceManager.Interfaces;

namespace Farfetch.UserAccounts.Service
{
    public class UserAccountService: DbCrudService<User>, IService
    {
       
    }
}
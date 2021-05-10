using ApiGoBarber.Entities;
using ApiGoBarber.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {

        Task<User> GetByEmail(string email);
    }
}

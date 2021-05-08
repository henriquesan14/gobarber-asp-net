using ApiGoBarber.Entities;
using ApiGoBarber.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

        Task<User> GetByEmail(string email);
    }
}

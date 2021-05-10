using ApiGoBarber.Context;
using ApiGoBarber.Entities;
using ApiGoBarber.Repositories.Base;
using ApiGoBarber.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(GoBarberContext dbContext) : base(dbContext)
        {

        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _dbContext.Users
                                .Where(u => u.Email == email).FirstOrDefaultAsync();
            return user;
        }
    }
}

using ApiGoBarber.Context;
using ApiGoBarber.Entities;
using ApiGoBarber.Repositories.Base;
using ApiGoBarber.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Repositories
{
    public class FileRepository : Repository<Avatar>, IFileRepository
    {
        public FileRepository(GoBarberContext dbContext) : base(dbContext)
        {

        }
    }
}

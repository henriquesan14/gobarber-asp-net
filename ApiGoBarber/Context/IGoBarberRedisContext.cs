using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Context
{
    public interface IGoBarberRedisContext
    {
        IDatabase Redis { get;  }
    }
}

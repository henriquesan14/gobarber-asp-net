using ApiGoBarber.Entities.SchemasMongoDb;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Context
{
    public interface IGoBarberMongoContext
    {
        IMongoCollection<Notification> Notifications { get; }
    }
}

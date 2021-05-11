using ApiGoBarber.Entities.SchemasMongoDb;
using ApiGoBarber.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Context
{
    public class GoBarberMongoContext : IGoBarberMongoContext
    {
        public GoBarberMongoContext(IGoBarberDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Notifications = database.GetCollection<Notification>(settings.CollectionName);
        }

        public IMongoCollection<Notification> Notifications { get; }
    }
}

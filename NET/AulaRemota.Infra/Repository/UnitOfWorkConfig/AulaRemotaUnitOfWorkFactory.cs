using AulaRemota.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace AulaRemota.Infra.Repository.UnitOfWorkConfig
{
    public class AulaRemotaUnitOfWorkFactory<TContext> : IUnitOfWorkFactory<MySqlContext>
        where TContext : DbContext, new()
    {
        private static Func<MySqlContext> _objectContextDelegate;
        private static readonly Object _lockObject = new object();

        //define qual dbContex sera usado por essa instancia do UOW
        public static void SetObjectContext(Func<MySqlContext> objectContextDelegate)
        {
            _objectContextDelegate = objectContextDelegate;
        }

        public IUnitOfWork<MySqlContext> Create()
        {
            MySqlContext context;

            lock (_lockObject)
            {
                context = _objectContextDelegate();
            }

           return new AulaRemotaUnitOfWork(context);
        }
    }
}

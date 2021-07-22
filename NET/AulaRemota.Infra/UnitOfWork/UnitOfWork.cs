using AulaRemota.Infra.Context;
using System;
using System.Collections;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AulaRemota.Infra.UnitOfWork
{
    public class UnitOfWork
    {
        /*
        private const string HTTPCONTEXTKEY = "PrimaryObjects.Repository.Base.HttpContext.Key";
        private static IServiceProvider _services;              
        private static IUnitOfWorkFactory<MySqlContext> _unitOfWorkFactory;
        public static IConfiguration Configuration { get; set; }

        public static HttpContext HttpContext
        {
            get
            {
                return GetHttpContext();
            }

        }

        private static readonly Hashtable _threads = new Hashtable();
        public static IServiceCollection ServiceCollection { get; set; }
        private static IServiceProvider Services
        {
            get
            {
                if (_services == null) BuildServiceProvider();
                return _services;
            }
        }

        public static IUnitOfWork<MySqlContext> Current
        {
            get
            {
                IUnitOfWork<MySqlContext> unitOfWork = GetUnitOfWork();

                if (unitOfWork == null)
                {
                    _unitOfWorkFactory = Services.GetRequiredService<IUnitOfWorkFactory<MySqlContext>>();
                    unitOfWork = _unitOfWorkFactory.Create();
                    SaveUnitOfWork(unitOfWork);
                }
                return unitOfWork;
            }
        }

        // se ignoreUnitOfWork for true será criado uma nova instancia do MySqlContext a cada solicitação
        // se ignoreUnitOfWork for false será retornado sempre a mesma instancia do MySqlContext a cada solicitação
        public static MySqlContext GetContext(bool ignoreUnitOfWork = false)
        {
            if (ignoreUnitOfWork)
            {
                return new MySqlContext(useProvider: true);
            }
            else
            {
                return Current.Context;
            }
        }

        private static HttpContext GetHttpContext()
        {
            return Services.GetRequiredService<IHttpContextAccessor>().HttpContext;
        }

        private static void BuildServiceProvider()
        {
            _services = ServiceCollection.BuildServiceProvider();
        }

        private static IUnitOfWork<MySqlContext> GetUnitOfWork()
        {
            if (HttpContext != null)
            {
                if (HttpContext.Items.ContainsKey(HTTPCONTEXTKEY))
                {
                    return (IUnitOfWork<MySqlContext>)HttpContext.Items[HTTPCONTEXTKEY];
                }
                return null;
            }
            else
            {
                Thread thread = Thread.CurrentThread;
                if (string.IsNullOrEmpty(thread.Name))
                {
                    thread.Name = Guid.NewGuid().ToString();
                    return null;
                }
                else
                {
                    lock (_threads.SyncRoot)
                    {
                        return (IUnitOfWork<MySqlContext>)_threads[Thread.CurrentThread.Name];
                    }
                }
            }
        }

        private static void SaveUnitOfWork(IUnitOfWork<MySqlContext> unitOfWork)
        {
            if (HttpContext != null)
            {
                HttpContext.Items[HTTPCONTEXTKEY] = unitOfWork;
            }
            else
            {
                lock (_threads.SyncRoot)
                {
                    _threads[Thread.CurrentThread.Name] = unitOfWork;
                }
            }
        }
        */
    }
}
using Ninject.Modules;
using TarasPerepichka.IDP.DataLayer.Interfaces;
using TarasPerepichka.IDP.DataLayer.Repositories;

namespace TarasPerepichka.IDP.BusinessLayer.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private readonly string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<DapperUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}

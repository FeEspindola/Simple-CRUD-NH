using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleCRUD.Site.Data;
using SimpleCRUD.Site.Data.Interfaces;
using SimpleCRUD.Site.Entities;

namespace SimpleCRUD.Site.Configuration
{
    public static class NhibernateConfig
    {

        public static void AddNhibernateConfigServices(this IServiceCollection services,IConfiguration configuration)
        {
          var configureSessionFactory =  Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(
                    configuration.GetConnectionString("NHSampleConnection")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Client>())
                .BuildSessionFactory();

          services.AddSingleton(configureSessionFactory);
          services.AddScoped(sessionFactory => configureSessionFactory.OpenSession());
          services.AddScoped<INHMapperSession, NHMapperSession>();

        }

      
    }
}

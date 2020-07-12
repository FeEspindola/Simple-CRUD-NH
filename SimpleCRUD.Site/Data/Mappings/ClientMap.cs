
using FluentNHibernate.Mapping;
using SimpleCRUD.Site.Entities;

namespace SimpleCRUD.Site.Mappings
{
    public class ClientMap : ClassMap<Client>
    {

        public ClientMap()
        {
            

            Id(client => client.Id)
                .Column("Id")
                .GeneratedBy.Guid();
        
            Map(client => client.Nome);
            
            Map(client => client.Email);
           

        }
    }
}

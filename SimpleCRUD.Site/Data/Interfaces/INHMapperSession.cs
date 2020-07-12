using SimpleCRUD.Site.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRUD.Site.Data.Interfaces
{
    public interface INHMapperSession
    {
        void StartTransaction();
        void EndTransaction();
        Task Commit();
        Task Rollback();
        Task Save(Client entity);
        Task Delete(Client entity);
        Task Update(Client entity);
        IQueryable<Client> GetAll();
        Task<Client> Get(Guid id);
    }
}
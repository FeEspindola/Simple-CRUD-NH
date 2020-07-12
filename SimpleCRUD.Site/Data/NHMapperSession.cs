using NHibernate;
using SimpleCRUD.Site.Data.Interfaces;
using SimpleCRUD.Site.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRUD.Site.Data
{
    public class NHMapperSession: INHMapperSession
    {
        private readonly ISession _session;
        private ITransaction transaction;

        public NHMapperSession(ISession session)
        => this._session = session;
        

        public void StartTransaction()
        => transaction = _session.BeginTransaction();
        

        public void EndTransaction()
        => transaction?.Dispose();


        public async Task Save(Client entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        
        }


        public async Task Update(Client entity)
        {
            await _session.UpdateAsync(entity);
            await _session.FlushAsync();
        }



        public async Task Delete(Client entity)
            => await _session.DeleteAsync(entity);

        public async Task Rollback()
            => await transaction.RollbackAsync();

        public async Task Commit()
            => await transaction.CommitAsync();

        public IQueryable<Client> GetAll()
            => _session.Query<Client>();

        public  async Task<Client> Get(Guid id)
            => await _session.GetAsync<Client>(id);
    }
}
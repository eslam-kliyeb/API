using E_Commerce.Core.Entities.Product;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly Hashtable _repositories;
        public UnitOfWork(DataContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }
        public async Task<int> CompletesAsync() => await _context.SaveChangesAsync();
        public async ValueTask DisposeAsync() => await _context.DisposeAsync();
        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(typeName))
            {
                var repo = new GenericRepository<TEntity, TKey>(_context);
                _repositories.Add(typeName, repo);
                return repo;
            }
            return (_repositories[typeName] as GenericRepository<TEntity, TKey>)!;
        }
    }
}

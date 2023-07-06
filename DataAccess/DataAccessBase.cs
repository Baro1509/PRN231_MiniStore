using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DataAccessBase<T> where T : class
    {
        private readonly MiniStoreContext _context;
        private readonly DbSet<T> _dbSet;
        public DataAccessBase()
        {
            _context = new MiniStoreContext();
            _dbSet = _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {

            return _dbSet;
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
        public void Update(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            //_dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}

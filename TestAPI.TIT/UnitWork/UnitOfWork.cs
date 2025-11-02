using TestAPI.TIT.Models;
using TestAPI.TIT.Repository;

namespace TestAPI.TIT.UnitWork
{
    public class UnitOfWork<TEntity> where TEntity : class
    {
        private readonly ITI_newContext _context;
        private GenaricRepository<TEntity>? _repo;

        public UnitOfWork(ITI_newContext context)
        {
            _context = context;
        }

        public GenaricRepository<TEntity> Repository
        {
            get
            {
                if (_repo == null)
                    _repo = new GenaricRepository<TEntity>(_context);

                return _repo;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }

}

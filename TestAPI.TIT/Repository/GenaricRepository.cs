using TestAPI.TIT.Models;
using TestAPI.TIT.UnitWork;

namespace TestAPI.TIT.Repository
{
    public class GenaricRepository<TEntity> : IGenaricRepository<TEntity> where TEntity : class
    {
        private readonly ITI_newContext _context;

        public GenaricRepository(ITI_newContext context)
        {
            _context= context;
        }

        public void Create(TEntity entity)
        {
           _context.Add(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            if (entity == null)
                throw new Exception($"Entity with id {id} not found");

            _context.Set<TEntity>().Remove(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }



        public List<TEntity> GetAll()
        {
           return _context.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
          return  _context.Set<TEntity>().Find(id);
        }

       

        public void Update(TEntity entity)
        {
           _context.Update(entity);
        }
    }
}

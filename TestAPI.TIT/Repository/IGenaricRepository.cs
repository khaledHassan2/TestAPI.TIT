namespace TestAPI.TIT.Repository
{
    public interface IGenaricRepository<TEntity> where TEntity : class
    {
       public List<TEntity> GetAll();
        public TEntity GetById(int id);
        //public TEntity GetByName(string name);
        public void Delete(int id);
        public void Update(TEntity entity);
        public void Create(TEntity entity);
    }
}

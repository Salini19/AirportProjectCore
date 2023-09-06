namespace AirportProjectCore.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _dbcontext;
        public Repository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void Add(T Entity)
        {
            _dbcontext.Set<T>().Add(Entity);
            _dbcontext.SaveChanges();

        }

        public List<T> Get()
        {
            return _dbcontext.Set<T>().ToList();
        }
    }
}

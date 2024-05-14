using NoticiasApi.Models.Entities;

namespace NoticiasApi.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public ItesrcneOctavoContext _context { get; set; }
        public Repository(ItesrcneOctavoContext context)
        {
            _context = context;
        }
        public virtual T? Get(object id)
        {
            return _context.Find<T>(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }
        public virtual void Insert(T item)
        {
            _context.Add(item);
            _context.SaveChanges();
        }
        public virtual void Update(T item)
        {
            _context.Update(item);
            _context.SaveChanges();
        }
        public virtual void Delete(T item)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }
    }
}

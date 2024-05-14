using NoticiasApi.Models.Entities;

namespace NoticiasApi.Repositories
{
    public interface IRepository<T> where T : class
    {
        ItesrcneOctavoContext _context { get; set; }

        void Delete(T item);
        T? Get(object id);
        IEnumerable<T> GetAll();
        void Insert(T item);
        void Update(T item);
    }
}
public interface ITrashRepository : IRepository<Trash>
{
    Task<IEnumerable<Trash>> GetByDateAsync(DateTime date);
    Task<IEnumerable<Trash>> GetAfterDateAsync(DateTime date);
    Task<IEnumerable<Trash>> GetBeforeDateAsync(DateTime date);
    Task<IEnumerable<Trash>> GetByTypeAsync(string type);
    Task<Trash> AddAsync(Trash trash);
}

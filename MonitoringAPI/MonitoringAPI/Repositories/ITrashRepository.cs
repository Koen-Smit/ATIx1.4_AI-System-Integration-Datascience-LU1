public interface ITrashRepository : IRepository<Trash>
{
    Task<IEnumerable<Trash>> GetFilteredAsync(
        DateTime? date, DateTime? after, DateTime? before, string? type);
    Task<Trash> AddAsync(Trash trash);
}

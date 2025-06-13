public interface ITrashRepository : IRepository<Trash>
{
    Task<IEnumerable<Trash>> GetFilteredAsync(
        DateTime? date, DateTime? after, DateTime? before, string? type, string? dagCategorie);
    new Task<Trash> AddAsync(Trash trash);
}

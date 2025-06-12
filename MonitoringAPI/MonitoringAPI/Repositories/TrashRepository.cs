using Microsoft.EntityFrameworkCore;

public class TrashRepository : Repository<Trash>, ITrashRepository
{
    public TrashRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Trash>> GetFilteredAsync(
        DateTime? date, DateTime? after, DateTime? before, string? type)
    {
        var query = _dbSet.AsQueryable();

        if (date.HasValue)
            query = query.Where(t => t.DateCollected.Date == date.Value.Date);

        if (after.HasValue)
            query = query.Where(t => t.DateCollected > after.Value);

        if (before.HasValue)
            query = query.Where(t => t.DateCollected < before.Value);

        if (!string.IsNullOrWhiteSpace(type))
            query = query.Where(t => t.TypeAfval.ToLower() == type.ToLower());

        return await query.ToListAsync();
    }

    public async Task<Trash> AddAsync(Trash trash)
    {
        await _dbSet.AddAsync(trash);
        await _context.SaveChangesAsync();
        return trash;
    }

}

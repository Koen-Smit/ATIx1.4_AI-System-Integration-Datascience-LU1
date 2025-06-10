using Microsoft.EntityFrameworkCore;

public class TrashRepository : Repository<Trash>, ITrashRepository
{
    public TrashRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Trash>> GetByDateAsync(DateTime date) =>
        await _dbSet.Where(t => t.DateCollected.Date == date.Date).ToListAsync();

    public async Task<IEnumerable<Trash>> GetAfterDateAsync(DateTime date) =>
        await _dbSet.Where(t => t.DateCollected > date).ToListAsync();

    public async Task<IEnumerable<Trash>> GetBeforeDateAsync(DateTime date) =>
        await _dbSet.Where(t => t.DateCollected < date).ToListAsync();

    public async Task<IEnumerable<Trash>> GetByTypeAsync(string type) =>
        await _dbSet.Where(t => t.TypeAfval.ToLower() == type.ToLower()).ToListAsync();
    public async Task<Trash> AddAsync(Trash trash)
    {
        await _dbSet.AddAsync(trash);
        await _context.SaveChangesAsync();
        return trash;
    }

}

using Microsoft.EntityFrameworkCore;

public class TrashRepository : Repository<Trash>, ITrashRepository
{
    private readonly IHolidayService _holidayService;

    public TrashRepository(AppDbContext context, IHolidayService holidayService) : base(context)
    {
        _holidayService = holidayService;
    }

    public async Task<IEnumerable<Trash>> GetFilteredAsync(
        DateTime? date, DateTime? after, DateTime? before, string? type, string? dagCategorie)
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

        if (!string.IsNullOrWhiteSpace(dagCategorie))
            query = query.Where(t => t.DagCategorie.ToLower() == dagCategorie.ToLower());

        return await query.ToListAsync();
    }

    public new async Task<Trash> AddAsync(Trash trash)
    {
        if (!string.IsNullOrWhiteSpace(trash.DagCategorie))
            throw new ArgumentException("DagCategorie is determined automatically.");

        trash.DagCategorie = await DetermineDagCategorieAsync(trash.DateCollected);

        await _dbSet.AddAsync(trash);
        await _context.SaveChangesAsync();
        return trash;
    }


    private async Task<string> DetermineDagCategorieAsync(DateTime date)
    {
        try
        {
            bool isHoliday = await _holidayService.IsHolidayAsync(date, "NL");
            if (isHoliday)
                return "Feestdag";
        }
        catch
        {
        }

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            return "Weekend";

        return "Werkdag";
    }



}

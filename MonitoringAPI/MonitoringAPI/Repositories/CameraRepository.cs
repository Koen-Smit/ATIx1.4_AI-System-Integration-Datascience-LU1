using Microsoft.EntityFrameworkCore;

public class CameraRepository : Repository<Camera>, ICameraRepository
{
    public CameraRepository(AppDbContext context) : base(context) { }

    public new async Task<Camera> AddAsync(Camera camera)
    {
        await _dbSet.AddAsync(camera);
        await _context.SaveChangesAsync();
        return camera;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var camera = await _dbSet.FindAsync(id);
        if (camera == null) return false;

        _dbSet.Remove(camera);
        await _context.SaveChangesAsync();
        return true;
    }
}

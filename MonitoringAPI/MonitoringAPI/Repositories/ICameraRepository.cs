public interface ICameraRepository : IRepository<Camera>
{
    Task<Camera> AddAsync(Camera camera);
    Task<bool> DeleteAsync(int id);
}

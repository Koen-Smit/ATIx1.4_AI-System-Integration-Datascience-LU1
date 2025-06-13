public interface ICameraRepository : IRepository<Camera>
{
    new Task<Camera> AddAsync(Camera camera);
    Task<bool> DeleteAsync(int id);
}


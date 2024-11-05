using DotnetApi.models;

namespace DotnetApi.Data
{
    public interface IUserRepository
    {
        public bool SaveChanges();
        public void AddEntity<T>(T entityToAdd);

        public void RemoveEntity<T>(T entityToRemove);

        public User GetSingleUser(int userId);
        public IEnumerable<User> GetUsers();
    }
}

using DotnetApi.models;

namespace DotnetApi.Data;

public class UserRepository(IConfiguration config) : IUserRepository
{
    readonly DataContextEF _entityFramework = new(config);

    public bool SaveChanges()
    {
        return _entityFramework.SaveChanges() > 0;
    }

    // public bool AddEntity<T>(T entityToAdd)
    // {
    //     if (entityToAdd != null)
    //     {
    //         _entityFramework.Add(entityToAdd);
    //         return true;
    //     }
    //     return false;
    // }
    public void AddEntity<T>(T entityToAdd)
    {
        if (entityToAdd != null)
        {
            _entityFramework.Add(entityToAdd);
        }
    }

    public void RemoveEntity<T>(T entityToRemove)
    {
        if (entityToRemove != null)
        {
            _entityFramework.Remove(entityToRemove);
        }
    }

    public User GetSingleUser(int id)
    {
        User? user = _entityFramework.Users.Where(u => u.UserId == id).FirstOrDefault<User>();
        if (user != null)
        {
            return user;
        }
        throw new Exception("Failed to get user");
    }

    public IEnumerable<User> GetUsers()
    {
        return [.. _entityFramework.Users];
    }
}

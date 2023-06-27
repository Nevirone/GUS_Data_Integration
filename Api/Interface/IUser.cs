using Api.Entities;

namespace Api.Interface
{
    public interface IUser
    {
        public Task<List<User>> GetUsers();
        public User GetUser(int id);
        public Task AddUser(User user);
        public void UpdateUser(User user);
        public User DeleteUser(int id);
        public bool CheckUser(int id);
    }
}

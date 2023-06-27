using Api.Entities;
using Api.Interface;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Api.Repository
{
    public class UserRepository : IUser
    {
        private readonly DatabaseContext _dbContext;

        public UserRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUser(User user)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _dbContext.User.Add(user);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public bool CheckUser(int id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var exists = _dbContext.User.Any(user => user.UserId == id);
                    transaction.Commit();
                    return exists;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public User DeleteUser(int id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    User user = _dbContext.User.Find(id);

                    if (user != null)
                    {
                        _dbContext.User.Remove(user);
                        _dbContext.SaveChanges();
                        transaction.Commit();
                        return user;
                    }
                    else
                    {
                        transaction.Rollback();
                        return null;
                    }
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public User GetUser(int id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    User user = _dbContext.User.Find(id);
                    transaction.Commit();
                    return user;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<List<User>> GetUsers()
        {
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    List<User> users = await _dbContext.User.ToListAsync();
                    await transaction.CommitAsync();
                    return users;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async void UpdateUser(User user)
        {
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _dbContext.Entry(user).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}

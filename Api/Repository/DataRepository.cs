using Api.Entities;
using Api.Interface;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Api.Repository
{
    public class DataRepository : IData
    {
        private readonly DatabaseContext _dbContext;

        public DataRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddData(Data data)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _dbContext.Data.Add(data);
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

        public bool CheckData(int id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var exists = _dbContext.Data.Any(data => data.Id == id);
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

        public Data DeleteData(int id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    Data data = _dbContext.Data.Find(id);

                    if (data != null)
                    {
                        _dbContext.Data.Remove(data);
                        _dbContext.SaveChanges();
                        transaction.Commit();
                        return data;
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

        public Data GetData(int id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    Data data = _dbContext.Data.Find(id);
                    transaction.Commit();
                    return data;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<List<Data>> GetDatas()
        {
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    List<Data> datas = await _dbContext.Data.ToListAsync();
                    await transaction.CommitAsync();
                    return datas;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public void UpdateData(Data data)
        {
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _dbContext.Entry(data).State = EntityState.Modified;
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

using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinApp.Models;

namespace XamarinApp.Services
{

    public class DataStoreTasks : IDataStore<TaskModel>
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<DataStoreTasks> Instance = new AsyncLazy<DataStoreTasks>(async () =>
        {
            var instance = new DataStoreTasks();
            CreateTableResult result = await Database.CreateTableAsync<TaskModel>();
            return instance;
        });

        public DataStoreTasks()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public async Task<bool> AddItemAsync(TaskModel item) //currently not used 
        {
            await Database.InsertAsync(item);
            return await Task.FromResult(true);
        }

        public Task<int> SaveItemAsync(TaskModel item)
        {
            return Database.InsertAsync(item);
        }

        public async Task<bool> UpdateItemAsync(TaskModel item)
        {
            Database.UpdateAsync(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = await Database.Table<TaskModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
            if (oldItem != null)
            {
                await Database.DeleteAsync(oldItem);
                return true;
            }
            return false;
        }

        public async Task<TaskModel> GetItemAsync(int id)
        {
            return await Database.Table<TaskModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TaskModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Database.Table<TaskModel>().ToListAsync();
        }
    }
}
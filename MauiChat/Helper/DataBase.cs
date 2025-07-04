using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MauiChat
{
    public class ImageDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public ImageDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
        }

        public Task<List<ImageModel>> GetImagesAsync()
        {
            return _database.Table<ImageModel>().ToListAsync();
        }

        public Task<ImageModel> GetImageByIdAsync(int id)
        {
            return _database.Table<ImageModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveImageAsync(ImageModel image)
        {
            return _database.InsertAsync(image);
        }

        public Task<int> DeleteImageAsync(ImageModel image)
        {
            return _database.DeleteAsync(image);
        }

        public Task<int> UpdateImageAsync(ImageModel image)
        {
            return _database.UpdateAsync(image);
        }

        public async void RefreshDataBaseTable()
        {
            await _database.DropTableAsync<ImageModel>();
            _database.CreateTableAsync<ImageModel>().Wait();
            return;
        }
    }
}
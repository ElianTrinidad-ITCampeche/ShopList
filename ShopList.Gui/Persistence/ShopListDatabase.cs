using ShopList.Gui.Models;
using ShopList.Gui.Persistence.Configuration;
using SQLite;

namespace ShopList.Gui.Persistence
{
    public class ShopListDatabase
    {
        private SQLiteAsyncConnection? _connection;
        private async Task InitAsync()
        {
            if (_connection != null)
            {
                return;
            }
            _connection = new SQLiteAsyncConnection(
                Constants.DatabasePath,
                Constants.FLags
                );
            await _connection.CreateTableAsync<Item>();
        }

        public async Task<int> SaveItemAsync(Item item)
        {
            await InitAsync();
            return await _connection!.InsertAsync(item);
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            await InitAsync();
            return await _connection!.Table<Item>()
                .ToListAsync();

        }

        public async Task<int> RemoveItemAsync(Item item)
        {
            await InitAsync();
            return await _connection!.DeleteAsync(item);
        }
    }
}


using Azure;
using Azure.Data.Tables;
using Azure.Identity;
using CLDV6212_ICE2_ST10449407.Models;

namespace CLDV6212_ICE2_ST10449407.Services
{
    public class TableStorageService
    {

        private readonly TableClient _tableClient;

        public TableStorageService(string connectionString, string tableName)
        {
            _tableClient = new TableClient(connectionString, tableName);
            _tableClient.CreateIfNotExists();
        }

        public async Task AddProductAsync(Product product)
        {
            await _tableClient.UpsertEntityAsync(product, TableUpdateMode.Replace);
        }


        public async Task<List<Product>> GetAllProductsAsync()
        {
            var results = _tableClient.QueryAsync<Product>();

            var entities = new List<Product>();
            await foreach (var item in results)
            {
                entities.Add(item);
            }
            return entities;
        }

    }
}

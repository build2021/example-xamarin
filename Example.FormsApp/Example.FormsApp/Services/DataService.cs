namespace Example.FormsApp.Services
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.Data.Sqlite;

    using Smart.Data;
    using Smart.Data.Mapper;

    public class DataServiceOptions
    {
        public string Path { get; set; }
    }

    public class DataService
    {
        private readonly DataServiceOptions options;

        private readonly DelegateDbProvider provider;

        public DataService(DataServiceOptions options)
        {
            this.options = options;

            var connectionString = $"Data Source={options.Path}";
            provider = new DelegateDbProvider(() => new SqliteConnection(connectionString));
        }

        public void DeleteAll()
        {
            if (File.Exists(options.Path))
            {
                File.Delete(options.Path);
            }
        }

        public async ValueTask RebuildAsync()
        {
            if (File.Exists(options.Path))
            {
                File.Delete(options.Path);
            }

            await provider.UsingAsync(async con =>
            {
                await con.ExecuteAsync("PRAGMA AUTO_VACUUM=1");
                // TODO
            });

            // TODO ?
        }
    }
}

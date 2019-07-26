using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace TestPackageApp.Database
{
    public class DatabaseService
    {
        private DatabaseService() { }
        private static DatabaseService instance;
        public static DatabaseService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseService();
                }
                return instance;
            }
        }
        SQLiteAsyncConnection sqliteAsyncConnection;
        SQLiteAsyncConnection DBConnection
        {
            get
            {
                if (sqliteAsyncConnection == null)
                {
                    sqliteAsyncConnection = new SQLiteAsyncConnection(DatabaseFilePath);
                    //TODO: Just a pseudo code for now, we need to implement proper way to get PRAGMA.
                    //DBConnection.QueryAsync<int>("PRAGMA key='tempkey'");
                }
                return sqliteAsyncConnection;
            }
        }
        string databaseFilePath = null;
        private string DatabaseFilePath
        {
            get
            {
                if (databaseFilePath == null)
                {
                    databaseFilePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), Constants.DatabasePath);
                }
                return databaseFilePath;
            }
        }
        public void CreateTables()
        {
            DBConnection.CreateTableAsync<Package>();
        }
        public async Task<int> InsertItem<T>(T item)
        {
            return await DBConnection.InsertAsync(item);
        }
        public async Task<List<T>> GetAllItems<T>() where T : new()
        {
            return await DBConnection.Table<T>().ToListAsync();
        }
    }
}

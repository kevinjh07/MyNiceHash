using System;
using SQLite;
using SQLitePCL;
using System.IO;
using Xamarin.Forms;
using MyNiceHash.Droid.Storage;
using MyNiceHash.Storage;

[assembly: Dependency(typeof(SQLiteDroid))]
namespace MyNiceHash.Droid.Storage  {
    public class SQLiteDroid : ISQLite {
        public SQLiteConnection GetConnection() {
            Batteries.Init();
            var fileName = "MyNiceHash.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);
            var connection = new SQLiteConnection(path);

            return connection;
        }
    }
}
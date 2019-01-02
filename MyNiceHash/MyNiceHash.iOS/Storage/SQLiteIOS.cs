using System;
using SQLite;
using System.IO;
using Xamarin.Forms;
using CryptoCurrency.iOS.Storage.Implementations;
using MyNiceHash.Storage.Interfaces;

[assembly: Dependency(typeof(SQLiteIOS))]
namespace CryptoCurrency.iOS.Storage.Implementations {
    public class SQLiteIOS : ISQLite {
        public SQLiteConnection GetConnection() {
            var fileName = "MyNiceHash.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);
            var connection = new SQLiteConnection(path);

            return connection;
        }
    }
}
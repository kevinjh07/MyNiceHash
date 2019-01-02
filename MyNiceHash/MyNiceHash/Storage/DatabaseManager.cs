using SQLite;
using Xamarin.Forms;
using System.Linq;
using System;
using MyNiceHash.Models;
using System.Collections.Generic;

namespace MyNiceHash.Storage {
    public interface IKeyObject {
        string Key { get; set; }
    }

    public class DatabaseManager {
        private SQLiteConnection database;

        public DatabaseManager() {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<BtcAddress>();
        }

        public void SaveValue<T>(T value) where T : IKeyObject, new() {
            var items = (from entry in database.Table<T>().AsEnumerable<T>()
                         where entry.Key == value.Key
                         select entry).ToList();
            if (items.Count == 0) {
                database.Insert(value);
            } else {
                database.Update(value);
            }
        }

        public void DeleteValue<T>(T value) where T : IKeyObject, new() {
            var items = (from entry in database.Table<T>().AsEnumerable<T>()
                         where entry.Key == value.Key
                         select entry).ToList();
            if (items.Count == 1) {
                database.Delete(value);
            } else {
                throw new Exception("Item with specified key not found");
            }
        }

        public List<TSource> GetAllItems<TSource>() where TSource : IKeyObject, new() {
            return database.Table<TSource>().AsEnumerable<TSource>().ToList();
        }

        public TSource GetItem<TSource>(string key) where TSource : IKeyObject, new() {
            var result = (from entry in database.Table<TSource>().AsEnumerable<TSource>()
                          where entry.Key == key
                          select entry).FirstOrDefault();
            return result;
        }
    }
}

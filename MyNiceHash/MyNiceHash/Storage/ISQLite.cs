using SQLite;

namespace MyNiceHash.Storage {
    public interface ISQLite {
        SQLiteConnection GetConnection();
    }
}

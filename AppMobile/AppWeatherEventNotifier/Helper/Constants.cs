
namespace AppWeatherEventNotifier.Helper
{
    public static class Constants
    {
        public const string PHOTO_DIR = "Pictures";
        public const string NO_PHOTO = "Non hai altre foto";
        public const string FILE_DIR = "Files";
        public const string SERVER = "http://192.168.1.9:8080";
        public const string SERVER_DEBUG = "http://192.168.1.9:8080"; //indirizzo publico del server se si fa port forwarding altrimenti collegare il telefono alla stessa rete del server e mettere indirizzo ip locale del server     

        public const bool debug= true;

        public const string DatabaseFilename = "TodoSQLite.db3";
        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;
        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
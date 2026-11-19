using BlazingPizzaApp.Model;
using SQLite;

namespace BlazingPizzaApp.Services
{
    public class NoteDatabase
    {
        private SQLiteAsyncConnection? _db;

        public static string DbPath => Path.Combine(FileSystem.AppDataDirectory, "notes.db3");

        private async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            if (_db != null)
            {
                return _db;
            }

            _db = new SQLiteAsyncConnection(DbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
            await _db.CreateTableAsync<Note>();
            return _db;
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            var db = await GetConnectionAsync();
            return await db.Table<Note>().OrderByDescending(n => n.CreatedAt).ToListAsync();
        }

        public async Task<int> SaveNoteAsync(Note note)
        {
            var db = await GetConnectionAsync();
            return note.Id != 0 ? await db.UpdateAsync(note) : await db.InsertAsync(note);
        }

        public async Task<int> DeleteNoteAsync(Note note)
        {
            var db = await GetConnectionAsync();
            return await db.DeleteAsync(note);
        }
    }
}

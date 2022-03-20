using MySnippet_Data;
using MySnippet_DataAccess.Repository.IRepository;
using MySnippet_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySnippet_DataAccess.Repository
{
    public class SnippetRepository : Repository<Snippet>, ISnippetRepository
    {
        private readonly MySnippetDbContext _db;

        public SnippetRepository(MySnippetDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Snippet> GetSnippets(string userId, string filter = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("Utente non specificato");

            return GetAll(
                x => (string.IsNullOrWhiteSpace(filter) || x.Title.ToLower().Contains(filter.ToLower()))
                    && x.UserCreateId == userId,
                x => x.OrderByDescending(x => x.Creation));
        }

        public Snippet GetSnippet(long snippetId, string userId)
        {
            if (snippetId <= 0)
                throw new ArgumentException("Id non valido");

            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("Utente non specificato");

            var result = FirstOrDefault(x => x.Id == snippetId && x.UserCreateId == userId);

            if(result == null)
                throw new KeyNotFoundException("Snippet non trovato");

            return result;
        }

        public void CreateSnippet(Snippet snippet, string userId) {
            if (snippet == null)
                throw new ArgumentNullException("Snippet non valorizzato");

            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("Utente non specificato");

            snippet.UserCreateId = userId;
            snippet.Creation = DateTime.Now;
            _db.Snippets.Add(snippet);
            Save();
        }

        public void RemoveSnippet(long snippetId, string userId)
        {
            if (snippetId <= 0)
                throw new ArgumentException("Id non valido");

            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("Utente non specificato");

            var itemToRemove = FirstOrDefault(x => x.Id == snippetId && x.UserCreateId == userId);

            if (itemToRemove == null)
                throw new KeyNotFoundException("Snippet non trovato");

            Remove(itemToRemove);
            Save();
        }

        public void UpdateSnippet(Snippet snippet, string userId)
        {
            if (snippet.Id <= 0)
                throw new ArgumentException("Id non valido");

            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("Utente non specificato");

            if(string.IsNullOrEmpty(snippet.Title))
                throw new ArgumentException("Inserire un titolo");


            var itemToUpdate = FirstOrDefault(x => x.Id == snippet.Id && x.UserCreateId == userId);

            if (itemToUpdate == null)
                throw new KeyNotFoundException("Snippet non trovato");

            itemToUpdate.Update = DateTime.Now;
            itemToUpdate.Title = snippet.Title;
            itemToUpdate.Description = snippet.Description;
            itemToUpdate.Content = snippet.Content;

            Update(itemToUpdate);
            Save();
        }
    }
}

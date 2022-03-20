using Microsoft.AspNetCore.Identity;
using MySnippet_Data;
using MySnippet_DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySnippet_DataAccess.Repository
{
    public class UserRepository : Repository<IdentityUser>, IUserRepository
    {
        private readonly MySnippetDbContext _db;

        public UserRepository(MySnippetDbContext db) : base(db)
        {
            _db = db;
        }

        public string GetUserEmail(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("Utente non specificato");

            return Find(userId).Email;
        }
    }
}

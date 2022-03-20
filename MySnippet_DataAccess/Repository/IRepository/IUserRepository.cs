using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySnippet_DataAccess.Repository.IRepository
{
    public interface IUserRepository
    {
        /// <summary>
        /// Restituisce la mail dell'utente.
        /// </summary>
        /// <param name="userId">Id utente</param>
        string GetUserEmail(string userId);
    }
}

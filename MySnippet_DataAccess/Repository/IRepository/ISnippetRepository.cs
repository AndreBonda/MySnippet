using MySnippet_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySnippet_DataAccess.Repository.IRepository
{
    public interface ISnippetRepository
    {
        /// <summary>
        /// Restituisce una collezione di snippets.
        /// </summary>
        /// <param name="user">Utente autenticato</param>
        /// <param name="filter">Filtro di ricerca per titolo</param>
        IEnumerable<Snippet> GetSnippets(string user, string filter = null);

        /// <summary>
        /// Restituisce il contenuto dello snippet.
        /// </summary>
        /// <param name="snippetId">Id snippet</param>
        /// <param name="userId">Id utente</param>
        /// <returns></returns>
        Snippet GetSnippet(long snippetId, string userId);

        /// <summary>
        /// Crea un nuovo snippet
        /// </summary>
        /// <param name="snippet">Snippet obj</param>
        /// <param name="userId">Id utente</param>
        /// <returns>Restituisce l'id dello snippet creato</returns>
        void CreateSnippet(Snippet snippet, string userId);

        /// <summary>
        /// Elimina lo snippet.
        /// </summary>
        /// <param name="snippetId">Id snippet</param>
        /// <param name="userId">Id utente</param>
        void RemoveSnippet(long snippetId, string userId);

        /// <summary>
        /// Esegue l'update dello snippet.
        /// </summary>
        /// <param name="snippet">Snippet obj</param>
        /// <param name="userId">Id utente</param>
        /// <returns></returns>
        void UpdateSnippet(Snippet snippet, string userId);

    }
}

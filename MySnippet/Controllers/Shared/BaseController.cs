using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MySnippet.Controllers.Shared
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Restituisce l'id dell'utente.
        /// </summary>
        public string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}

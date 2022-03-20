using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySnippet.Controllers.Shared;
using MySnippet_Data;
using MySnippet_DataAccess.Repository;
using MySnippet_DataAccess.Repository.IRepository;
using MySnippet_Models;
using MySnippet_Models.ViewModels;

namespace MySnippet.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISnippetRepository _snippetRepo;
        private readonly IUserRepository _userRepo;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ISnippetRepository snippetRepo, IUserRepository userRepo, SignInManager<IdentityUser> signInManager)
        {
            _snippetRepo = snippetRepo;
            _userRepo = userRepo;
            _signInManager = signInManager;
        }

        public IActionResult Index(HomeVM model)
        {
            return View(FillModel(model));
        }

        [Authorize]
        [HttpGet]
        public IActionResult NewSnippet() {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(NewSnippetVM model) 
        {
            try
            {
                var snippet = new Snippet()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content
                };

                _snippetRepo.CreateSnippet(snippet, GetUserId());

                return RedirectToAction("Index");
            }
            catch (Exception e) {
                string errMsg = string.Empty;

                if (e is ArgumentNullException || e is ArgumentException)
                    errMsg = e.Message;
                else
                    errMsg = "Errore imprevisto";

                // specificando all renderizza l'errore nel summary della form
                ModelState.AddModelError("All", errMsg);
                return View("NewSnippet");
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(long snippetId) {
            try
            {
                _snippetRepo.RemoveSnippet(snippetId, GetUserId());
            }
            catch (Exception e) {
                // TODO: gestire un alert
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> LogOut() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region API
        [Authorize]
        [HttpGet]
        public IActionResult GetSnippets(string filter)
        {
            try
            {
                if (filter == null)
                    filter = string.Empty;

                return Json(new
                {
                    status = (int)HttpStatusCode.OK,
                    snippets = _snippetRepo.GetSnippets(GetUserId(), filter)
                });
            }
            catch (Exception e) {
                if (e is ArgumentException || e is KeyNotFoundException)
                {
                    return Json(new
                    {
                        status = (int)HttpStatusCode.UnprocessableEntity,
                        msg = e.Message
                    });
                }

                return Json(new
                {
                    status = (int)HttpStatusCode.InternalServerError,
                    msg = "Error"
                });
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetSnippet(long snippetId)
        {
            try { 
                return Json(new {
                    status = (int)HttpStatusCode.OK,
                    snippet = _snippetRepo.GetSnippet(snippetId, GetUserId()) 
                });
            }
            catch (Exception e)
            {
                if (e is ArgumentException || e is KeyNotFoundException)
                {
                    return Json(new
                    {
                        status = (int)HttpStatusCode.UnprocessableEntity,
                        msg = e.Message
                    });
                }

                return Json(new
                {
                    status = (int)HttpStatusCode.InternalServerError,
                    msg = "Error"
                });
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteSnippet(long snippetId)
        {
            try
            {
                _snippetRepo.RemoveSnippet(snippetId, GetUserId());
                return Json(new
                {
                    status = (int)HttpStatusCode.OK
                });
            }
            catch (Exception e)
            {
                if (e is ArgumentException || e is KeyNotFoundException)
                {
                    return Json(new
                    {
                        status = (int)HttpStatusCode.UnprocessableEntity,
                        msg = e.Message
                    });
                }

                return Json(new
                {
                    status = (int)HttpStatusCode.InternalServerError,
                    msg = "Error"
                });
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult UpdateSnippet([FromBody] Snippet snippet)
        {
            try
            {
                _snippetRepo.UpdateSnippet(snippet, GetUserId());
                return Json(new
                {
                    status = (int)HttpStatusCode.OK
                });
            }
            catch (Exception e)
            {
                if (e is ArgumentException || e is KeyNotFoundException)
                {
                    return Json(new
                    {
                        status = (int)HttpStatusCode.UnprocessableEntity,
                        msg = e.Message
                    });
                }

                return Json(new
                {
                    status = (int)HttpStatusCode.InternalServerError,
                    msg = "Error"
                });
            }
        }

        #endregion

        #region Private
        private HomeVM FillModel(HomeVM model)
        {
            if (User.Identity.IsAuthenticated)
            {
                model.User = _userRepo.GetUserEmail(GetUserId());
                model.Snippets = _snippetRepo.GetSnippets(GetUserId());
            }
            return model;
        }
        #endregion
    }
}

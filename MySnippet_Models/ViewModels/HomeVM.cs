using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySnippet_Models.ViewModels
{
    public class HomeVM
    {
        public string GetSnipperURL { get; set; }
        public string GetSnippetContentURL { get; set; }
        public string User { get; set; }
        public IEnumerable<Snippet> Snippets { get; set; }

        public HomeVM()
        {
            Snippets = new List<Snippet>();
        }
    }
}

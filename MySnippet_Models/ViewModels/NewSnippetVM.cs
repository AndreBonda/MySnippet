using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySnippet_Models.ViewModels
{
    public class NewSnippetVM
    {
        [Required(ErrorMessage = "Inserire un titolo")]
        [Display(Name = "Titolo")]
        public string Title { get; set; }

        [Display(Name = "Descrizione")]
        public string Description { get; set; }

        [Display(Name = "Snippet")]
        public string Content { get; set; }
    }
}

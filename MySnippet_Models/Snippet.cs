using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySnippet_Models
{
    /// <summary>
    /// Classe che rappresenta gli snippet gestiti dall'utente.
    /// </summary>
    public class Snippet
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Codice sorgente
        /// </summary>
        public string Content { get; set; }
        [Required]
        public DateTime Creation {get;set;}
        public DateTime? Update { get; set; }
        [Required]
        public string UserCreateId { get; set; }
        [ForeignKey("UserCreateId")]
        public IdentityUser UserCreate { get; set; }
    }
}

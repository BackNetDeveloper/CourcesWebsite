using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnMoreApp.Entities
{
    public class Content
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " This Field Is Required ")]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }

        [Display(Name = " Html Content")]
        public string HtmlContent { get; set; }

        [Display(Name = "Video Link")]
        public string VideoLink { get; set; }
        public CategoryItem CategoryItem { get; set; }

        //-------------------------------------------------------------------------------------------------------
        [NotMapped] // Will Not Be A Column In Database
        public int CatItemId { get; set; } // Represent ForiegnKey from The Navigational Property [CategoryItem]

        // This Property Named Like This Because The Name CategoryItemId
        // will Cause Interfere With Future Migrations.

        // It Has Been Named Like This So As Not To Conflict With The EF Core Naming Confintion.
        //-------------------------------------------------------------------------------------------------------
        [NotMapped] // Will Not Be A Column In Database
        public int CategoryId { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnMoreApp.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = " This Field Is Required ")]
        [StringLength(200,MinimumLength =2)]
         public string Title { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = " This Field Is Required ")]

        [Display(Name = "Thumbnail Image Path")]
        public string ThumbnailImagePath { get; set; } 

        [ForeignKey("CategoryId")]
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ICollection<UserCategory> UserCategory { get; set; }

    }
}

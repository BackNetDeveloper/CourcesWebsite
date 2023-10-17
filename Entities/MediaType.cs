using LearnMoreApp.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnMoreApp.Entities
{
    public class MediaType : IPrimaryProperties
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " This Field Is Required ")]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
      
        [Required(ErrorMessage = " This Field Is Required ")]

        [Display(Name = "Thumbnail Image Path")]
        public string ThumbnailImagePath { get; set; }

        [ForeignKey("MediaTypeId")]
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }
    }
}

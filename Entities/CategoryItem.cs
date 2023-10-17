using LearnMoreApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnMoreApp.Entities
{
    public class CategoryItem
    {
        private DateTime _releasedDate = DateTime.MinValue;
        public int Id { get; set; }

        [Required(ErrorMessage = " This Field Is Required ")]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please Select A Valid Item From The '{0}' DropDown List")]
        [Display(Name = " Media Type ")]
        public int MediaTypeId { get; set; }

        [NotMapped]
        public ICollection<SelectListItem> MediaTypes { get; set; }

        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]

        [Display(Name = " Release Date ")]
        public DateTime DateTimeItemReleased 
        {
            get 
            {
                return (_releasedDate == DateTime.MinValue) ? DateTime.Now : _releasedDate;
            }
            set
            {
                _releasedDate = value;
            }
        }

        [NotMapped]  // We Use It For Check To Display An Add Or Update The Content ReLated To The Current Item
        public int ContentId { get; set; }
    }
}

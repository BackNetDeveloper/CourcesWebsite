using LearnMoreApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace LearnMoreApp.Extentions
{
    public static class ConvertExtentions
    {
        public static List<SelectListItem> ConvertToSelectList<T>(this IEnumerable<T> Collection ,int SelectedValue) where T:IPrimaryProperties
        {
            return (from Item in Collection
                    select new SelectListItem
                    {
                        Text = Item.Title,
                        Value = Item.Id.ToString(),
                        Selected = (Item.Id==SelectedValue)
                    }
                ).ToList();
        }
    }
}

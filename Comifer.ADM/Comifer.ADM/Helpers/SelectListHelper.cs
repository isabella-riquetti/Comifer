using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Comifer.ADM
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> source, Func<T, string> valueSelector, Func<T, string> textSelector)
        {
            List<SelectListItem> d = new List<SelectListItem>();
            foreach (T element in source)
                d.Add(new SelectListItem()
                {
                    Value = valueSelector(element),
                    Text = textSelector(element)
                });
            return d;
        }

        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> source, Func<T, string> valueSelector, Func<T, string> text1Selector, Func<T, string> text2Selector)
        {
            List<SelectListItem> d = new List<SelectListItem>();
            foreach (T element in source)
                d.Add(new SelectListItem()
                {
                    Value = valueSelector(element),
                    Text = text1Selector(element) + " | " + text2Selector(element)
                });
            return d;
        }
    }
}

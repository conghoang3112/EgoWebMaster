using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Reflection;
using TLS.Common.Enums.EnumAttribute;

namespace TLS.Common.Enums.Extension
{
    public static class EnumConverter
    {
        public static SelectList ToSelectList<TEnum>(this TEnum obj, object selectedValue)
        {
            var selectListItems = Enum.GetValues(typeof(TEnum)).OfType<Enum>().Select(x => new SelectListItem
            {
                Text = x.DisplayName(),
                Value = Convert.ToInt32(x).ToString(),
                Selected = selectedValue != null ? (int)selectedValue == Convert.ToInt32(x) : false
            });
            return new SelectList(selectListItems, "Value", "Text", selectedValue);
        }

        public static string DisplayName(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            EnumDisplayNameAttribute attribute = Attribute.GetCustomAttribute(field, typeof(EnumDisplayNameAttribute)) as EnumDisplayNameAttribute;
            return attribute == null ? value.ToString() : attribute.DisplayName;
        }
    }
}

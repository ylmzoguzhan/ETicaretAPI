using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Helpers
{
    public static class NameHelper
    {
        public static string CharacterRegulatory(string name)
        {
            name = name
                .Replace("ı", "i").Replace("İ", "i")
                .Replace("Ö", "o").Replace("ö", "o")
                .Replace("Ü", "u").Replace("ü", "u")
                .Replace("Ğ", "g").Replace("ğ", "g")
                .Replace("Ç", "c").Replace("ç", "c")
                .Replace("Ş", "s").Replace("ş", "s")
                .Replace("â", "a").Replace("î", "i")
                .Replace(" ", "-")
                .Replace("æ", "")
                .Replace("\"", "");
            string pattern = "[\"!'^+%&/()=?_@€¨~,;:.<>|]";
            string replacement = "";

            Regex regex = new Regex(pattern);
            string result = regex.Replace(name, replacement);

            return result;
        }
    }
}

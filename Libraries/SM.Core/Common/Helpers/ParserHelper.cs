using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SM.Core.Common.Helpers
{
	public static class ParserHelper
	{
        public static bool ParseStringIdValidation(string text)
        {
            foreach (string textStr in text.Split(','))
            {
                if (!int.TryParse(textStr, out int _))
                    return false;
            }
            return true;
        }

        public static List<int> ParseStringIdToIntArray(string text)
        {

            if (string.IsNullOrEmpty(text))
                return null;

            List<int> ids = new List<int>();

            foreach (string textStr in text.Split(','))
            {
                if (int.TryParse(textStr, out int parsedInt))
                    ids.Add(parsedInt);
                else
                    return null;
            }

            if (ids.Contains(0))
                ids.Remove(0);

            return ids;
        }
    }
}


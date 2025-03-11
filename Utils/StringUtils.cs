using System.Globalization;
using System.Text;

namespace ACRP_API.Utils
{
    public static class StringUtils
    {
        public static string NormalizeString(string input)
        {
            return new string([.. input.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)])
                .ToLowerInvariant()
                .Trim();
        }
    }
}
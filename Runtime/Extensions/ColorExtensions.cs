using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Converts a Unity Color to a hex string.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string GetHexCode(this Color color)
        {
            Color32 color32 = color;

            return $"#{color32.r:X2}{color32.g:X2}{color32.b:X2}{color32.a:X2}";
        }
    }
}
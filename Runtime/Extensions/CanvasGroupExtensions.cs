using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.Extensions
{
    public static class CanvasGroupExtensions
    {
        /// <summary>
        /// Sets the CanvasGroup component visibility.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="visible"></param>
        public static void SetVisibility(this CanvasGroup parent, bool visible)
        {
            parent.alpha = visible ? 1 : 0;
            parent.blocksRaycasts = visible;
            parent.interactable = visible;
        }
    }
}
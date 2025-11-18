using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Finds components in the children, excluding the parent.
        /// </summary>
        public static T[] GetComponentsInChildrenExclusive<T>(this GameObject parent, bool includeInactive = false) where T : Component
        {
            return parent.transform.GetComponentsInChildrenExclusive<T>(includeInactive);
        }

        /// <summary>
        /// TryGetComponent version of GetComponentInChildren.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="value"></param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public static bool TryGetComponentInChildren<T>(this GameObject parent, out T value, bool includeInactive = false) where T : Component
        {
            return parent.transform.TryGetComponentInChildren(out value, includeInactive);
        }

        /// <summary>
        /// TryGetComponent version of GetComponentInParent.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="value"></param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public static bool TryGetComponentInParent<T>(this GameObject parent, out T value, bool includeInactive = false) where T : Component
        {
            return parent.transform.TryGetComponentInParent(out value, includeInactive);
        }

        /// <summary>
        /// Get GameObject's scene path.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static string GetScenePath(this GameObject parent)
        {
            return parent.GetScenePath();
        }
    }
}
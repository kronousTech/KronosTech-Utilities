using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.Extensions
{
    public static class MonoBehaviourExtensions
    {
        /// <summary>
        /// Finds components in the children, excluding the parent.
        /// </summary>
        public static T[] GetComponentsInChildrenExclusive<T>(this MonoBehaviour parent, bool includeInactive = false) where T : Component
        {
            return parent.transform.GetComponentsInChildrenExclusive<T>(includeInactive);
        }

        /// <summary>
        /// TryGetComponent version of GetComponentInChildren
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="value"></param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public static bool TryGetComponentInChildren<T>(this MonoBehaviour parent, out T value, bool includeInactive = false) where T : Component
        {
            return parent.transform.TryGetComponentInChildren(out value, includeInactive);
        }

        /// <summary>
        /// TryGetComponent version of GetComponentInParent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="value"></param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public static bool TryGetComponentInParent<T>(this MonoBehaviour parent, out T value, bool includeInactive = false) where T : Component
        {
            return parent.transform.TryGetComponentInParent(out value, includeInactive);
        }
    }
}
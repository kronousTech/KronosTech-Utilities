using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Destroys all child objects of a Transform.
        /// </summary>
        /// <param name="parent">The Transform whose children should be destroyed.</param>
        public static void DestroyChildren(this Transform parent)
        {
            foreach (Transform child in parent)
            {
                Object.Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Shuffles hierarchy index for all child objects of a Transform.
        /// </summary>
        /// <param name="parent">The Transform whose children should be shuffled.</param>
        public static void ShuffleChildren(this Transform parent)
        {
            List<Transform> children = new();

            foreach (Transform child in parent)
            {
                children.Add(child);
            }

            children = children.OrderBy(item => new System.Random().Next()).ToList();

            foreach (Transform child in children)
            {
                child.SetAsLastSibling();
            }
        }

        /// <summary>
        /// Finds components in the children, excluding the parent.
        /// </summary>
        public static T[] GetComponentsInChildrenExclusive<T>(this Transform parent, bool includeInactive = false) where T : Component
        {
            var components = new List<T>();
            components.AddRange(parent.GetComponentsInChildren<T>(includeInactive));

            // If the parent contains the component,
            // remove it
            if (parent.TryGetComponent<T>(out var value))
            {
                components.Remove(value);
            }

            return components.ToArray();
        }

        /// <summary>
        /// Casts current Transform to a RectTransform.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static RectTransform GetRectTransform(this Transform parent)
        {
            return (RectTransform)parent;
        }

        /// <summary>
        /// TryGetComponent version of GetComponentInChildren
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="value"></param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public static bool TryGetComponentInChildren<T>(this Transform parent, out T value, bool includeInactive = false) where T : Component
        {
            value = parent.GetComponentInChildren<T>(includeInactive);

            return value != null;
        }

        /// <summary>
        /// TryGetComponent version of GetComponentInParent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="value"></param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public static bool TryGetComponentInParent<T>(this Transform parent, out T value, bool includeInactive = false) where T : Component
        {
            value = parent.GetComponentInParent<T>(includeInactive);

            return value != null;
        }

        /// <summary>
        /// Get Transforms's scene path.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static string GetScenePath(this Transform parent)
        {
            var path = parent.name;
            var current = parent.transform;

            while (current.parent != null)
            {
                current = current.parent;
                path = current.name + "/" + path;
            }

            return path;
        }
    }
}
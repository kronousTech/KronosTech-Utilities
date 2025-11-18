using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Swaps one element with another.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="indexA"></param>
        /// <param name="indexB"></param>
        public static void Swap<T>(this T[] parent, int indexA, int indexB)
        {
            if (!IsValidIndex(parent, indexA))
            {
                Debug.LogError("ArrayExtensions.cs: indexA is out of bounds");

                return;
            }
            if (!IsValidIndex(parent, indexB))
            {
                Debug.LogError("ArrayExtensions.cs: indexB is out of bounds");

                return;
            }

            (parent[indexB], parent[indexA]) = (parent[indexA], parent[indexB]);
        }

        /// <summary>
        /// Checks if index is valid for the array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool IsValidIndex<T>(this T[] parent, int index)
        {
            return index >= 0 && index < parent.Length;
        }

        /// <summary>
        /// Get a random element from the array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T GetRandomElement<T>(this T[] parent)
        {
            return parent[UnityEngine.Random.Range(0, parent.Length)];
        }
    }
}
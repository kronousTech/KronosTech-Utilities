using System.Collections.Generic;
using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.Extensions
{
    public static class ListExtensions
    {
        private static readonly System.Random s_Random = new();

        /// <summary>
        /// Swaps one element with another.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="indexA"></param>
        /// <param name="indexB"></param>
        public static void Swap<T>(this IList<T> parent, int indexA, int indexB)
        {
            if (!IsValidIndex(parent, indexA))
            {
                Debug.LogError("ListExtensions.cs: indexA is out of bounds");

                return;
            }
            if (!IsValidIndex(parent, indexB))
            {
                Debug.LogError("ListExtensions.cs: indexB is out of bounds");

                return;
            }

            (parent[indexB], parent[indexA]) = (parent[indexA], parent[indexB]);
        }

        /// <summary>
        /// Shuffles list elements using the Fisher–Yates shuffle algorithm.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        public static void Shuffle<T>(this IList<T> parent)
        {
            int n = parent.Count;
            int k;

            while (n > 1)
            {
                n--;
                k = s_Random.Next(n + 1);
                (parent[k], parent[n]) = (parent[n], parent[k]);
            }
        }

        /// <summary>
        /// Checks if the index is valid for the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool IsValidIndex<T>(this IList<T> parent, int index)
        {
            return index >= 0 && index < parent.Count;
        }

        /// <summary>
        /// Get a random element from the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T GetRandomElement<T>(this IList<T> parent)
        {
            return parent[UnityEngine.Random.Range(0, parent.Count)];
        }

        /// <summary>
        /// Adds element to list if not already added. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool TryAdd<T>(this IList<T> parent, T element)
        {
            if (parent.Contains(element))
            {
                return false;
            }
            else
            {
                parent.Add(element);

                return true;
            }
        }
    }
}
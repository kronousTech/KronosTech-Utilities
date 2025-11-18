using System.IO;
using UnityEditor;
using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.EditorTools
{
    public static class EditorHelper
    {
        public static void SelectAssetInProject(string assetPath)
        {
            Object assetFile = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            if (assetFile != null)
            {
                Selection.activeObject = assetFile; // Select the asset in the Project view
                EditorGUIUtility.PingObject(assetFile); // Optionally, focus and highlight the object in the Project window
            }
            else
            {
                Debug.LogError("EditorHelper.cs Failed to select script: Object not found at " + assetPath);
            }
        }

        /// <summary>
        /// Creates missing folders for path.
        /// </summary>
        /// <param name="basePath">Base where the path starts</param>
        /// <param name="path"></param>
        public static void CreatePathFolders(string basePath, string path)
        {
            path = path.Replace("\\", "/");
            var folders = path.Split(new char[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (string folder in folders)
            {
                basePath = Path.Combine(basePath, folder);

                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);

                    AssetDatabase.Refresh();

                    Debug.Log($"{nameof(EditorHelper)}.cs: Created folder: {basePath}");
                }
            }
        }

        /// <summary>
        /// Creates missing folders for path.
        /// </summary>
        /// <param name="path"></param>
        public static void CreatePathFolders(string path)
        {
            CreatePathFolders(Application.dataPath, path);
        }
    }
}

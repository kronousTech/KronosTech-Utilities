using System.IO;
using UnityEditor;
using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.EditorTools
{
    public class EditorToolCreateCustomPackagePrefabsVariant : MonoBehaviour
    {
        private const string k_menuItemName = "Assets/KendirStudios EditorTools/Create CustomPackage Prefab Variant";

        [MenuItem(k_menuItemName, true, 0)]
        private static bool ValidateCreateVariant()
        {
            // Only enable when selection is a prefab asset
            GameObject selected = Selection.activeObject as GameObject;
            if (selected == null)
            {
                return false;
            }

            string path = AssetDatabase.GetAssetPath(selected);
            return path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase);
        }
        [MenuItem(k_menuItemName, false, 0)]
        private static void CreateVariant()
        {
            GameObject basePrefab = Selection.activeObject as GameObject;
            if (basePrefab == null) return;

            string basePath = AssetDatabase.GetAssetPath(basePrefab);
            string baseName = Path.GetFileNameWithoutExtension(basePath);

            // Ask where to save the variant
            string destination = EditorUtility.SaveFilePanelInProject(
                "Save Prefab Variant",
                baseName + " Variant",
                "prefab",
                "Choose location to save the prefab variant"
            );

            CreateVariantByResources(basePrefab, destination);
        }

        public static void CreateVariant(GameObject basePrefab, string destination)
        {
            CreateVariantByResources(basePrefab, destination);
        }

        private static void CreateVariantByResources(GameObject basePrefab, string destination)
        {
            if (string.IsNullOrEmpty(destination))
            {
                Debug.LogError($"{nameof(EditorToolCreateCustomPackagePrefabsVariant)}.cs: " +
                    $"The destination is empty.");

                return;
            }

            // Instantiate the package prefab in the scene
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(basePrefab);

            try
            {
                // Save the instance as a variant of the base prefab
                PrefabUtility.SaveAsPrefabAssetAndConnect(instance, destination, InteractionMode.UserAction);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"{nameof(EditorToolCreateCustomPackagePrefabsVariant)}.cs: " +
                    $"Could not Make Prefab Variant! \n" + ex);
                Object.DestroyImmediate(instance);

                return;
            }

            // Destroy the scene instance after saving
            Object.DestroyImmediate(instance);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"{nameof(EditorToolCreateCustomPackagePrefabsVariant)}.cs:" +
                $"Prefab Variant created at: {destination}");
        }
    }
}
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KendirStudios.CustomPackages.Utilities.EditorTools
{
    public class EditorWindowFindMissingScripts : EditorWindow
    {
        private readonly List<ObjectMissingScriptsData> m_objects = new();
        private Vector2 m_scrollPosition;
        private string m_info = string.Empty;
        private const int k_buttonsHeight = 40;
        private const int k_listButtonSize = 66;
        private const string k_searchSceneIcon = "UnityLogo";
        private const string k_searchPrefabIcon = "Prefab Icon";
        private const string k_searchAllIcon = "Toolbar Plus More";
        private const string k_selectIcon = "GameObject Icon";
        private const string k_deleteIcon = "AS Badge Delete";

        private void OnGUI()
        {
            GUILayout.Space(10);
            // BUTTON -> FIND IN CURRENT SCENE
            if (GUILayout.Button(new GUIContent(" Find in Current Scene", EditorGUIUtility.IconContent(k_searchSceneIcon).image),
                GUILayout.Height(k_buttonsHeight)))
            {
                m_objects.Clear();
                m_objects.AddRange(FindMissingScriptsInScene(SceneManager.GetActiveScene().path));

                m_info = m_objects.Count == 0 ? "Didn't found any missing scripts on the current scene" : string.Empty;
            }

            // BUTTON -> FIND IN ALL SCENES
            if (GUILayout.Button(new GUIContent(" Find in ALL Scenes", EditorGUIUtility.IconContent(k_searchSceneIcon).image),
                GUILayout.Height(k_buttonsHeight)))
            {
                var startingScene = SceneManager.GetActiveScene().path;
                m_objects.Clear();
                m_objects.AddRange(FindMissingScriptsInAllScenes());
                EditorSceneManager.OpenScene(startingScene, OpenSceneMode.Single);

                m_info = m_objects.Count == 0 ? "Didn't found any missing scripts on the scenes" : string.Empty;
            }

            // BUTTON -> FIND IN PREFABS
            if (GUILayout.Button(new GUIContent(" Find in ALL Prefabs", EditorGUIUtility.IconContent(k_searchPrefabIcon).image),
                GUILayout.Height(k_buttonsHeight)))
            {
                m_objects.Clear();
                m_objects.AddRange(FindMissingScriptsInPrefabs());

                m_info = m_objects.Count == 0 ? "Didn't found any missing scripts on the prefabs" : string.Empty;
            }

            // BUTTON -> FIND ALL
            if (GUILayout.Button(new GUIContent(" Find ALL", EditorGUIUtility.IconContent(k_searchAllIcon).image),
                GUILayout.Height(k_buttonsHeight)))
            {
                m_objects.Clear();
                m_objects.AddRange(FindMissingScriptsInAllScenes());
                m_objects.AddRange(FindMissingScriptsInPrefabs());

                m_info = m_objects.Count == 0 ? "Didn't found any missing scripts on the prefabs" : string.Empty;
            }


            if (m_info != string.Empty)
            {
                EditorGUILayout.HelpBox(m_info, MessageType.Info);
            }

            GUILayout.Space(10);
            if (m_objects.Count > 0)
            {
                m_scrollPosition = EditorGUILayout.BeginScrollView(m_scrollPosition);
                for (int i = 0; i < m_objects.Count; i++)
                {
                    SetObjectWithMissingScriptInfo(i, m_objects[i]);
                }
                EditorGUILayout.EndScrollView();

                if (GUILayout.Button(new GUIContent(" DELETE ALL MISSING SCRIPTS FOUND", EditorGUIUtility.IconContent(k_deleteIcon).image),
                    GUILayout.Height(k_buttonsHeight)))
                {
                    var count = m_objects.Count;

                    foreach (var obj in m_objects)
                    {
                        obj.DeleteMissingScripts();
                    }

                    m_objects.Clear();

                    m_info = "Removed Missing scripts from " + count + " GameObjects";
                }
            }
        }

        [MenuItem("KendirStudios/Tools/Find Missing Scripts")]
        private static void OpenWindow()
        {
            GetWindow<EditorWindowFindMissingScripts>("Find Missing Scripts");
        }
        private List<ObjectMissingScriptsData> FindMissingScriptsInScene(string scenePath)
        {
            var objectsWithMissingScripts = new List<ObjectMissingScriptsData>();

            if (SceneManager.GetActiveScene().path != scenePath)
            {
                EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            }

            foreach (GameObject obj in FindObjectsOfType<GameObject>(true))
            {
                if (GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(obj) == 0)
                {
                    continue;
                }
                else
                {
                    objectsWithMissingScripts.Add(new ObjectMissingScriptsData(obj, scenePath, GetGameObjectPath(obj), ObjectType.SceneObject));
                }
            }

            return objectsWithMissingScripts;
        }
        private List<ObjectMissingScriptsData> FindMissingScriptsInAllScenes()
        {
            var objectsWithMissingScripts = new List<ObjectMissingScriptsData>();

            var allScenes = Directory.GetFiles("Assets", "*.unity", SearchOption.AllDirectories);

            foreach (string scenePath in allScenes)
            {
                objectsWithMissingScripts.AddRange(FindMissingScriptsInScene(scenePath));
            }

            return objectsWithMissingScripts;
        }
        private List<ObjectMissingScriptsData> FindMissingScriptsInPrefabs()
        {
            var objects = new List<ObjectMissingScriptsData>();
            var allPrefabs = Directory.GetFiles("Assets", "*.prefab", SearchOption.AllDirectories);

            foreach (string prefabPath in allPrefabs)
            {
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                var children = prefab.GetComponentsInChildren<Component>(true);

                if (prefab != null)
                {
                    foreach (Component comp in children)
                    {
                        if (comp == null)
                        {
                            objects.Add(new ObjectMissingScriptsData(prefab, string.Empty, prefabPath, ObjectType.Prefab));

                            break;
                        }
                    }
                }
            }

            return objects;
        }
        private string GetGameObjectPath(GameObject obj)
        {
            var path = new List<string>();
            while (obj != null)
            {
                path.Insert(0, obj.name);
                obj = obj.transform.parent ? obj.transform.parent.gameObject : null;
            }
            return string.Join("/", path);
        }
        private void SetObjectWithMissingScriptInfo(int index, ObjectMissingScriptsData obj)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox(obj.GetDisplayText(index), MessageType.None);

            if (GUILayout.Button(EditorGUIUtility.IconContent(k_deleteIcon),
                    GUILayout.Width(k_listButtonSize),
                    GUILayout.Height(k_listButtonSize)))
            {
                obj.DeleteMissingScripts();

                m_objects.Remove(obj);
            }

            if (GUILayout.Button(EditorGUIUtility.IconContent(k_selectIcon),
                    GUILayout.Width(k_listButtonSize),
                    GUILayout.Height(k_listButtonSize)))
            {
                obj.SelectGameObject();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
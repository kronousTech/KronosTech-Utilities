using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.EditorTools
{
    public class EditorWindowFindScriptGUID : EditorWindow
    {
        private string m_scriptName = "";
        private string m_info = string.Empty;
        private readonly List<string> m_guids = new();
        private Vector2 m_scrollPosition = Vector2.zero;
        private const int k_buttonSize = 30;
        private const int k_scrollviewSize = 600;
        private const string k_copyIcon = "Clipboard";
        private const string k_selectIcon = "cs Script Icon";
        private const string k_explorerIcon = "d_Folder Icon";
        private const string k_searchSceneIcon = "ViewToolZoom On";
        private const string k_deleteIcon = "Toolbar Minus";

        private void OnGUI()
        {
            GUILayout.Space(10);

            m_scriptName = EditorGUILayout.TextField("Script Name:", m_scriptName);

            GUILayout.Space(10);
            if (GUILayout.Button(new GUIContent(" Find GUIDs", EditorGUIUtility.IconContent(k_searchSceneIcon).image),
                GUILayout.Height(40)))
            {
                FindGUID(m_scriptName);
            }

            if (!string.IsNullOrEmpty(m_info))
            {
                EditorGUILayout.HelpBox(m_info, MessageType.Info);
            }

            GUILayout.Space(20);
            m_scrollPosition = EditorGUILayout.BeginScrollView(m_scrollPosition, GUILayout.Height(k_scrollviewSize));
            for (int i = 0; i < m_guids.Count; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(m_guids[i]);
                EditorGUILayout.LabelField("Script " + (i + 1) + ": " + Path.GetFileNameWithoutExtension(path), EditorStyles.boldLabel);

                // ----- GUID
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.HelpBox($"GUID: \n{m_guids[i]}", MessageType.None);
                if (GUILayout.Button(
                    EditorGUIUtility.IconContent(k_copyIcon),
                    GUILayout.Width(k_buttonSize),
                    GUILayout.Height(k_buttonSize)))
                {
                    EditorGUIUtility.systemCopyBuffer = m_guids[i];
                }
                if (GUILayout.Button(new GUIContent(string.Empty,
                    EditorGUIUtility.IconContent(k_deleteIcon).image, "Remove from List"),
                    GUILayout.Width(k_buttonSize),
                    GUILayout.Height(k_buttonSize)))
                {
                    m_guids.Remove(m_guids[i]);
                }
                EditorGUILayout.EndHorizontal();


                //----- PATH
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.HelpBox($"Path: \n{path}", MessageType.None);
                if (GUILayout.Button(
                    EditorGUIUtility.IconContent(k_selectIcon),
                    GUILayout.Width(k_buttonSize),
                    GUILayout.Height(k_buttonSize)))
                {
                    EditorHelper.SelectAssetInProject(path);
                }
                if (GUILayout.Button(
                    EditorGUIUtility.IconContent(k_explorerIcon),
                    GUILayout.Width(k_buttonSize),
                    GUILayout.Height(k_buttonSize)))
                {
                    EditorUtility.RevealInFinder(path);
                }
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(10);
            }
            EditorGUILayout.EndScrollView();
        }

        [MenuItem("KendirStudios/Tools/Find Script GUID")]
        private static void ShowWindow()
        {
            GetWindow<EditorWindowFindScriptGUID>("Find Script GUID");
        }
        private void FindGUID(string name)
        {
            m_guids.Clear();

            if (string.IsNullOrEmpty(name))
            {
                m_info = "EditorWindowFindScriptGUID.cs: Please enter a script name.";

                return;
            }

            m_guids.AddRange(AssetDatabase.FindAssets(name + " t:Script"));

            m_info = m_guids.Count == 0 ? $"EditorWindowFindScriptGUID.cs: Script '{name}' not found!" : string.Empty;
        }
    }
}
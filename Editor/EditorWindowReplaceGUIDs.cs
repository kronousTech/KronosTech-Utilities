using System.IO;
using UnityEditor;
using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.EditorTools
{
    public class EditorWindowReplaceGUIDs : EditorWindow
    {
        private string m_oldGUID = string.Empty;
        private string m_newGUID = string.Empty;
        private string m_info = string.Empty;
        private string m_warning = string.Empty;

        private void OnGUI()
        {
            GUILayout.Space(10);
            m_oldGUID = EditorGUILayout.TextField("Old GUID:", m_oldGUID);
            m_newGUID = EditorGUILayout.TextField("New GUID:", m_newGUID);

            GUILayout.Space(10);
            if (GUILayout.Button(new GUIContent(" Replace GUID in project", EditorGUIUtility.IconContent("_Popup").image),
                GUILayout.Height(40)))
            {
                m_info = string.Empty;
                m_warning = string.Empty;

                if (!string.IsNullOrEmpty(m_oldGUID) && !string.IsNullOrEmpty(m_newGUID))
                {
                    ReplaceGUIDInProject(m_oldGUID, m_newGUID);

                    m_info += "GUID Updated Successfully!";
                }
                else
                {
                    m_warning = "Please enter both Old and New GUIDs.";
                }
            }

            if (!string.IsNullOrEmpty(m_info))
            {
                EditorGUILayout.HelpBox(m_info, MessageType.Info);
            }
            if (!string.IsNullOrEmpty(m_warning))
            {
                EditorGUILayout.HelpBox(m_warning, MessageType.Warning);
            }
        }

        [MenuItem("KendirStudios/Tools/Replace GUID")]
        private static void ShowWindow()
        {
            GetWindow<EditorWindowReplaceGUIDs>("Replace GUID");
        }
        private void ReplaceGUIDInProject(string oldGuid, string newGuid)
        {
            string[] files = Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                if (file.EndsWith(".meta") || file.EndsWith(".cs")) continue; // Skip .meta and script files

                string text = File.ReadAllText(file);
                if (text.Contains(oldGuid))
                {
                    text = text.Replace(oldGuid, newGuid);
                    File.WriteAllText(file, text);

                    m_info += $"Updated GUID in: {file} \n";
                }
            }

            AssetDatabase.Refresh();
        }
    }
}
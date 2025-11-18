using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.EditorTools
{
    public class EditorWindowSceneGeometryStatistics : EditorWindow
    {
        private readonly List<ObjectGeometryData> m_individualData = new();
        private Vector2 m_meshCount = Vector2.zero;
        private Vector2 m_vertexCount = Vector2.zero;
        private Vector2 m_submeshCount = Vector2.zero;
        private Vector2 m_triangleCount = Vector2.zero;
        private Vector2 m_scrollPosition = Vector2.zero;
        private string m_errorText = string.Empty;
        private bool m_hasData = false;
        private const int k_buttonsHeight = 50;
        private const int k_sortButtonsHeight = 30;
        private const int k_columnWidth = 95;
        private const string k_searchSceneIcon = "UnityLogo";
        private const string k_searchSelectedIcon = "Prefab Icon";
        private const string k_selectIcon = "GameObject Icon";
        private const int k_individualButtonSize = 50;

        private void OnGUI()
        {
            var headersStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 18
            };

            GUILayout.Space(10);
            if (GUILayout.Button(
                new GUIContent(" Refresh Current Scene Geometry Data",
                EditorGUIUtility.IconContent(k_searchSceneIcon).image),
                GUILayout.Height(k_buttonsHeight)))
            {
                RefreshSceneGeometryData();
            }

            GUILayout.Space(10);
            if (GUILayout.Button(
                new GUIContent(" Refresh Selected Objects Geometry Data",
                EditorGUIUtility.IconContent(k_searchSelectedIcon).image),
                GUILayout.Height(k_buttonsHeight)))
            {
                RefreshSelectedObjectsData();
            }

            GUILayout.Space(15);
            if (m_hasData)
            {
                GUILayout.BeginVertical();
                GUILayout.Label("General Data", headersStyle);
                GUILayout.Space(5);
                CreateRow(string.Empty, "Total", "Active");
                CreateRow("Meshs:", m_meshCount.x.ToString(), m_meshCount.y.ToString());
                CreateRow("Vertexes:", m_vertexCount.x.ToString(), m_vertexCount.y.ToString());
                CreateRow("Submeshes:", m_submeshCount.x.ToString(), m_submeshCount.y.ToString());
                CreateRow("Triangles:", m_triangleCount.x.ToString(), m_triangleCount.y.ToString());
                GUILayout.EndVertical();

                GUILayout.Space(15);
                GUILayout.Label("Individual Data", headersStyle);
                GUILayout.Space(5);
                // ORDER BUTTONS
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Sort By Size UP", GUILayout.Height(k_sortButtonsHeight)))
                {
                    SortList(false);
                }
                if (GUILayout.Button("Sort By Size DOWN", GUILayout.Height(k_sortButtonsHeight)))
                {
                    SortList(true);
                }
                GUILayout.EndHorizontal();

                m_scrollPosition = EditorGUILayout.BeginScrollView(m_scrollPosition);
                for (int i = 0; i < m_individualData.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.HelpBox((i + 1) + ".\n" + m_individualData[i].GetDisplayText(), MessageType.None);
                    if (GUILayout.Button(
                        EditorGUIUtility.IconContent(k_selectIcon),
                        GUILayout.Width(k_individualButtonSize),
                        GUILayout.Height(k_individualButtonSize)))
                    {
                        Selection.activeGameObject = m_individualData[i].GameObject;
                    }
                    GUILayout.EndHorizontal();
                }
                EditorGUILayout.EndScrollView();
            }

            if (m_errorText != string.Empty)
            {
                EditorGUILayout.HelpBox(m_errorText, MessageType.Error);
            }
        }

        [MenuItem("KendirStudios/Tools/Scene Geometry Statistics")]
        private static void OpenWindow()
        {
            GetWindow<EditorWindowSceneGeometryStatistics>("Scene Geometry Statistics");
        }
        private void CreateRow(string info1, string info2, string info3)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(info1, GUILayout.Width(k_columnWidth));
            GUILayout.Label(info2, GUILayout.Width(k_columnWidth));
            GUILayout.Label(info3, GUILayout.Width(k_columnWidth));
            GUILayout.EndVertical();
        }
        private void RefreshSceneGeometryData()
        {
            var meshRenderers = FindObjectsOfType<MeshRenderer>(true);
            var skinnedMeshRenderers = FindObjectsOfType<SkinnedMeshRenderer>(true);

            GetDataFromRenderers(meshRenderers, skinnedMeshRenderers);
        }
        private void RefreshSelectedObjectsData()
        {
            var selection = Selection.gameObjects;
            if (selection.Length == 0)
            {
                ClearData();

                m_errorText = "Select a GameObject.";

                return;
            }

            var meshRenderers = new List<MeshRenderer>();
            var skinnedmeshRenderers = new List<SkinnedMeshRenderer>();

            foreach (var go in selection)
            {
                meshRenderers.AddRange(go.GetComponentsInChildren<MeshRenderer>(true));
                skinnedmeshRenderers.AddRange(go.GetComponentsInChildren<SkinnedMeshRenderer>(true));
            }

            GetDataFromRenderers(meshRenderers.ToArray(), skinnedmeshRenderers.ToArray());
        }
        private void GetDataFromRenderers(MeshRenderer[] meshRenderers, SkinnedMeshRenderer[] skinnedMeshRenderers)
        {
            ClearData();

            // Process static meshes (MeshRenderer)
            foreach (var renderer in meshRenderers)
            {
                if (renderer.TryGetComponent<MeshFilter>(out var filter) && filter.sharedMesh != null)
                {
                    AddMeshData(renderer.gameObject,
                        filter.sharedMesh,
                        renderer.enabled && renderer.gameObject.activeSelf);
                }
            }

            // Process skinned meshes (SkinnedMeshRenderer)
            foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
            {
                if (skinnedMeshRenderer.sharedMesh != null)
                {
                    AddMeshData(skinnedMeshRenderer.gameObject,
                        skinnedMeshRenderer.sharedMesh,
                        skinnedMeshRenderer.enabled && skinnedMeshRenderer.gameObject.activeSelf);
                }
            }

            m_hasData = true;
        }
        private void AddMeshData(GameObject go, Mesh mesh, bool isActive = true)
        {
            m_individualData.Add(new ObjectGeometryData(go, mesh, isActive));

            m_meshCount.x++;
            m_vertexCount.x += mesh.vertexCount;
            m_triangleCount.x += mesh.triangles.Length / 3;
            m_submeshCount.x += mesh.subMeshCount;

            if (isActive)
            {
                m_meshCount.y++;
                m_vertexCount.y += mesh.vertexCount;
                m_triangleCount.y += mesh.triangles.Length / 3;
                m_submeshCount.y += mesh.subMeshCount;
            }
        }
        private void ClearData()
        {
            m_individualData.Clear();

            m_meshCount = Vector2.zero;
            m_vertexCount = Vector2.zero;
            m_submeshCount = Vector2.zero;
            m_triangleCount = Vector2.zero;
            m_hasData = false;
            m_errorText = string.Empty;
        }
        private void SortList(bool side)
        {
            if (side)
            {
                m_individualData.Sort((a, b) => a.VertexCount.CompareTo(b.VertexCount));
            }
            else
            {
                m_individualData.Sort((a, b) => b.VertexCount.CompareTo(a.VertexCount));
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace KendirStudios.CustomPackages.Utilities.EditorTools
{
    public class EditorWindowUpdatePackages : EditorWindow
    {
        private readonly List<MessageTypeStringPair> m_logList = new();

        private static ListRequest s_listRequest;
        private static AddRequest s_addRequest;
        private static readonly Queue<PackageInfo> s_packagesToUpdate = new();
        private static int s_updatedCount;
        private Vector2 m_scrollPosition;
        private const int k_buttonsHeight = 40;
        private const string k_updateCustomButton = "Prefab Icon";
        private const string k_updateAllButton = "UnityLogo";

        private Predicate<PackageInfo> searchFilter; 

        private void OnGUI()
        {
            GUILayout.Space(10);
            if (GUILayout.Button(new GUIContent(" Update Custom Packages", EditorGUIUtility.IconContent(k_updateCustomButton).image),
                GUILayout.Height(k_buttonsHeight)))
            {
                searchFilter =
                    p => !p.name.StartsWith("com.unity") && !p.name.StartsWith("com.unity.modules");

                UpdatePackages();
            }
            GUILayout.Space(5);
            if (GUILayout.Button(new GUIContent(" Update All Packages", EditorGUIUtility.IconContent(k_updateAllButton).image),
                GUILayout.Height(k_buttonsHeight)))
            {
                searchFilter = 
                    p => !p.name.StartsWith("com.unity.modules");

                UpdatePackages();
            }
            GUILayout.Space(5);

            m_scrollPosition = EditorGUILayout.BeginScrollView(m_scrollPosition, GUILayout.ExpandHeight(true));
            foreach (var log in m_logList)
            {
                EditorGUILayout.HelpBox(log.Message, log.Type);
            }
            EditorGUILayout.EndScrollView();
            GUILayout.Space(5);
        }

        [MenuItem("KendirStudios/Tools/Update Packages")]
        private static void ShowWindow()
        {
            GetWindow<EditorWindowUpdatePackages>("Update Packages");
        }
        
        
        private void UpdatePackages()
        {
            m_logList.Clear();
            m_logList.Add(new MessageTypeStringPair(MessageType.Info, "Updating Packages..."));

            s_listRequest = Client.List(true);

            EditorApplication.update += ProcessPackageListCallback;
        }

        private void ProcessPackageListCallback()
        {
            if(!s_listRequest.IsCompleted)
            {
                return;
            }

            if (s_listRequest.Status == StatusCode.Success)
            {
                s_packagesToUpdate.Clear();

                foreach (var package in s_listRequest.Result)
                {
                    if(searchFilter(package))
                    {
                        s_packagesToUpdate.Enqueue(package);
                    }
                }

                s_updatedCount = 0;

                m_logList.Add(new MessageTypeStringPair(
                    type: MessageType.Info,
                    message: $"Found {s_packagesToUpdate.Count} packages to update."));

                ProcessNextPackage();
            }
            else
            {
                m_logList.Add(new MessageTypeStringPair(
                    type: MessageType.Error,
                    message: $"Failed to retrieve package list."));
            }

            EditorApplication.update -= ProcessPackageListCallback;
        }

        private void ProcessNextPackage()
        {
            if (s_packagesToUpdate.Count == 0)
            {
                m_logList.Add(new MessageTypeStringPair(
                    MessageType.Info,
                    $"All packages updated!"));

                EditorUtility.ClearProgressBar();

                return;
            }

            string packageName = s_packagesToUpdate.Dequeue().packageId;

            s_updatedCount++;

            EditorUtility.DisplayProgressBar(
                title: "Updating Packages", 
                info: $"Updating {packageName} ({s_updatedCount}/{s_packagesToUpdate.Count})", 
                progress: (float)s_updatedCount / s_packagesToUpdate.Count);

            m_logList.Add(new MessageTypeStringPair(
                    MessageType.Info,
                    $"Updating {packageName}...!"));

            s_addRequest = Client.Add(packageName);

            EditorApplication.update += MonitorPackageUpdate;
        }
        private void MonitorPackageUpdate()
        {
            if (!s_addRequest.IsCompleted)
            {
                return;
            }

            if (s_addRequest.Status == StatusCode.Success)
            {
                m_logList.Add(new MessageTypeStringPair(
                    MessageType.Info,
                    $"Updated: {s_addRequest.Result.name}"));

                s_updatedCount++;
            }
            else
            {
                m_logList.Add(new MessageTypeStringPair(
                    MessageType.Error,
                    $"Failed to update: {s_addRequest.Error.message}"));
            }

            EditorApplication.update -= MonitorPackageUpdate;

            ProcessNextPackage();
        }
    }
}
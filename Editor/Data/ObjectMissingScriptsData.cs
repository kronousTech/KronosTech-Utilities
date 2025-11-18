using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KendirStudios.CustomPackages.Utilities.EditorTools
{
    public class ObjectMissingScriptsData
    {
        public GameObject Source;
        public string ScenePath;
        public string ObjectPath;
        public string Name;
        public int MissingScriptsCount;
        public ObjectType Type;

        public ObjectMissingScriptsData(
            GameObject sourceGameObject,
            string scenePath,
            string objectPath,
            ObjectType type)
        {
            this.Source = sourceGameObject;
            this.ScenePath = scenePath;
            this.ObjectPath = objectPath;
            this.Name = sourceGameObject.name;
            this.MissingScriptsCount = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(sourceGameObject);
            this.Type = type;
        }

        public string GetDisplayText(int index)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(index + 1 + ".\n");
            stringBuilder.Append("Name: " + Name + ".\n");
            stringBuilder.Append("Type: " + Type.ToString() + ".\n");
            stringBuilder.Append("Missing Scripts Count: " + MissingScriptsCount + ".\n");
            stringBuilder.Append("Path: " + (Type == ObjectType.SceneObject ? ScenePath.ToString() : ObjectPath.ToString()));

            return stringBuilder.ToString();
        }
        public void SelectGameObject()
        {
            if (Type == ObjectType.SceneObject && Source == null)
            {
                GoToObjectsScene();

                Source = FindGameObjectByPath(ObjectPath);
            }

            Selection.activeGameObject = Source;
        }
        public void DeleteMissingScripts()
        {
            if (Type == ObjectType.SceneObject && Source == null)
            {
                GoToObjectsScene();

                Source = FindGameObjectByPath(ObjectPath);

                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(Source);

                EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
            }
            else
            {
                foreach (Transform tf in Source.GetComponentsInChildren<Transform>(true))
                {
                    if (GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(tf.gameObject) > 0)
                    {
                        GameObjectUtility.RemoveMonoBehavioursWithMissingScript(tf.gameObject);

                        PrefabUtility.SavePrefabAsset(Source);
                    }
                }
            }
        }

        private void GoToObjectsScene()
        {
            if (SceneManager.GetActiveScene().path != ScenePath)
            {
                EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
            }
        }
        private GameObject FindGameObjectByPath(string path)
        {
            var names = path.Split('/');
            GameObject currentObj = null;

            foreach (string name in names)
            {
                if (currentObj == null)
                {
                    currentObj = GameObject.Find(name);
                }
                else
                {
                    Transform child = currentObj.transform.Find(name);
                    if (child == null) return null;
                    currentObj = child.gameObject;
                }
            }
            return currentObj;
        }
    }
}
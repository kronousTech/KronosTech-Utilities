using UnityEngine;

namespace KendirStudios.CustomPackages.Utilities.EditorTools
{
    public class ObjectGeometryData
    {
        public GameObject GameObject;
        public string MeshName;
        public bool IsActive;
        public int VertexCount;
        public int TriangleCount;
        public int SubmeshCount;

        public ObjectGeometryData(GameObject go, Mesh mesh, bool isActive)
        {
            this.GameObject = go;
            this.MeshName = mesh.name;
            this.IsActive = isActive;
            this.VertexCount = mesh.vertexCount;
            this.TriangleCount = mesh.triangles.Length / 3;
            this.SubmeshCount = mesh.subMeshCount;
        }

        public string GetDisplayText()
        {
            return "GameObject: " + GameObject.name + "\n" +
                "Mesh: " + MeshName + "\n" +
                "Vertexes: " + VertexCount + "\n" +
                "Triangles: " + TriangleCount + "\n" +
                "Submeshes: " + SubmeshCount;
        }
    }
}
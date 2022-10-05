using CjLib;
using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public sealed class Line : Primitive
    {

        public float lineWidth;

        private Mesh lineMesh;
        private Vector3[] lineVerts = new Vector3[] { new Vector3(0,0,0), new Vector3(0,0,0) };
        private int[] lineIndices = new[] { 0, 1 };

        public Line()
        {
            lineMesh ??= new Mesh();
            lineMesh.SetVertices(lineVerts);
            lineMesh.SetIndices(lineIndices, MeshTopology.Lines, 0);
        }

        internal void Init(Vector3 p0, Vector3 p1, Color color, float duration, bool depthEnabled)
        {
            lineVerts[0] = p0;
            lineVerts[1] = p1;
            lineMesh.SetVertices(lineVerts);
            lineMesh.SetIndices(lineIndices, MeshTopology.Lines, 0);
            this.color = color;
            this.duration = duration;
            this.depthEnabled = depthEnabled;
        }

        internal override void Render (Material material, MaterialPropertyBlock materialProperties)
        {
            materialProperties.SetColor("_Color", color);
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            Graphics.DrawMesh(lineMesh, Matrix4x4.identity, material, 0, null, 0, materialProperties, false, false, false);
        }
    }
}

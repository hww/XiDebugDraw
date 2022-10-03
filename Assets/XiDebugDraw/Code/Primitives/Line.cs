using CjLib;
using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class Line : Primitive
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

        public void SetTransform(Vector3 p0, Vector3 p1)
        {
            lineVerts[0] = p0;
            lineVerts[1] = p1;
            lineMesh.SetVertices(lineVerts);
            lineMesh.SetIndices(lineIndices, MeshTopology.Lines, 0);
        }

        public override void Render ( )
        {
            MaterialPropertyBlock materialProperties = GetMaterialPropertyBlock();
            materialProperties.SetColor("_Color", color);
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            materialProperties.SetFloat("_ZBias", s_wireframeZBias);
            Graphics.DrawMesh(lineMesh, Matrix4x4.identity, s_PrimitiveMaterial, 0, null, 0, materialProperties, false, false, false);
        }
    }
}

using UnityEngine;

using CjLib;

namespace XiDebugDraw.Primitives
{
    public sealed class Triangle : Primitive
    {
        internal float lineWidth = 1f;

        private Mesh triMesh;

        Vector3[] triVerts = new Vector3[] 
        {
            new(0,0,0), new(1,0,0), new(1,0,0), new(0,0,1), new(0,0,1), new(0,0,0)
        };
        
        int[] triIndices = new int[] { 0,1,2,3,4,5 };

        public Triangle()
        {
            triMesh = PrimitiveMeshFactory.Lines(triVerts);
        }

        internal void SetTransform(Vector3 p0, Vector3 p1, Vector3 p2)
        {
                triVerts[0] = p0;
                triVerts[1] = p1;
                triVerts[2] = p1;
                triVerts[3] = p2;
                triVerts[4] = p2;
                triVerts[5] = p0;
                triMesh.SetVertices(triVerts);
                triMesh.SetIndices(triIndices, MeshTopology.Lines, 0);
        }

        internal override void Render(Material material, MaterialPropertyBlock materialProperties)
        {
            materialProperties.SetColor("_Color", color);
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            Graphics.DrawMesh(triMesh, Matrix4x4.identity, material, 0, null, 0, materialProperties, false, false, false);
        }
    }
}

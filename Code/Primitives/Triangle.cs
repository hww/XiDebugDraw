using UnityEngine;

using CjLib;

namespace XiDebugDraw.Primitives
{
    public class Triangle : Primitive
    {
        public float lineWidth = 1f;

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

        public void SetTransform(Vector3 p0, Vector3 p1, Vector3 p2)
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

        public override void Render ( )
        {

            MaterialPropertyBlock materialProperties = GetMaterialPropertyBlock();
            materialProperties.SetColor("_Color", color);
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            materialProperties.SetFloat("_ZBias", s_wireframeZBias);
            Graphics.DrawMesh(triMesh, Matrix4x4.identity, s_PrimitiveMaterial, 0, null, 0, materialProperties, false, false, false);
        }
    }
}

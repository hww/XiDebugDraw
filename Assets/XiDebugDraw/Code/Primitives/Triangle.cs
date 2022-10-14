using UnityEngine;

using CjLib;

namespace XiDebugDraw.Primitives
{
    /// <summary>A triangle. This class cannot be inherited.</summary>
    public sealed class Triangle : Primitive
    {
        /// <summary>Width of the line.</summary>
        internal float lineWidth = 1f;

        /// <summary>The triangle mesh.</summary>
        private Mesh triMesh;

        /// <summary>The triangle vertices.</summary>
        Vector3[] triVerts = new Vector3[] 
        {
            new(0,0,0), new(1,0,0), new(1,0,0), new(0,0,1), new(0,0,1), new(0,0,0)
        };
        
        /// <summary>The triangle indices.</summary>
        int[] triIndices = new int[] { 0,1,2,3,4,5 };

        /// <summary>Default constructor.</summary>
        public Triangle()
        {
            triMesh = PrimitiveMeshFactory.Lines(triVerts);
        }

        ///--------------------------------------------------------------------
        /// <summary>Sets a transform.</summary>
        ///
        /// <param name="p0">The p 0.</param>
        /// <param name="p1">The first Vector3.</param>
        /// <param name="p2">The second Vector3.</param>
        ///--------------------------------------------------------------------

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

        ///--------------------------------------------------------------------
        /// <summary>Renders this object.</summary>
        ///
        /// <param name="material">          The material.</param>
        /// <param name="materialProperties">The material properties.</param>
        ///--------------------------------------------------------------------

        internal override void Render(Material material, MaterialPropertyBlock materialProperties)
        {
            materialProperties.SetColor("_Color", color);
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            Graphics.DrawMesh(triMesh, Matrix4x4.identity, material, 0, null, 0, materialProperties, false, false, false);
        }
    }
}

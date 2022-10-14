using CjLib;
using UnityEngine;

namespace XiDebugDraw.Primitives
{
    /// <summary>A line. This class cannot be inherited.</summary>
    public sealed class Line : Primitive
    {
        /// <summary>Width of the line.</summary>
        public float lineWidth;
        /// <summary>The line mesh.</summary>
        private Mesh lineMesh;
        /// <summary>The line vertices.</summary>
        private Vector3[] lineVerts = new Vector3[] { new Vector3(0,0,0), new Vector3(0,0,0) };
        /// <summary>The line indices.</summary>
        private int[] lineIndices = new[] { 0, 1 };

        /// <summary>Default constructor.</summary>
        public Line()
        {
            lineMesh ??= new Mesh();
            lineMesh.SetVertices(lineVerts);
            lineMesh.SetIndices(lineIndices, MeshTopology.Lines, 0);
        }

        ///--------------------------------------------------------------------
        /// <summary>Initializes this object.</summary>
        ///
        /// <param name="p0">          The p 0.</param>
        /// <param name="p1">          The first Vector3.</param>
        /// <param name="color">       The color.</param>
        /// <param name="duration">    The duration.</param>
        /// <param name="depthEnabled"> True to enable, false to disable the
        ///                             depth.</param>
        ///--------------------------------------------------------------------

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

        ///--------------------------------------------------------------------
        /// <summary>Renders this object.</summary>
        ///
        /// <param name="material">          The material.</param>
        /// <param name="materialProperties">The material properties.</param>
        ///--------------------------------------------------------------------

        internal override void Render (Material material, MaterialPropertyBlock materialProperties)
        {
            materialProperties.SetColor("_Color", color);
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            Graphics.DrawMesh(lineMesh, Matrix4x4.identity, material, 0, null, 0, materialProperties, false, false, false);
        }
    }
}

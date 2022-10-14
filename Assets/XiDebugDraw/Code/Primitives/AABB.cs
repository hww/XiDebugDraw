using UnityEngine;


namespace XiDebugDraw.Primitives
{
    /// <summary>An AABB volume. This class cannot be inherited.</summary>
    public sealed class AABB : Primitive
    {
        /// <summary>Width of the line.</summary>
        internal float lineWidth = 1;
        /// <summary>The matrix.</summary>
        Matrix4x4 matrix;

        /// <summary>Default constructor.</summary>
        public AABB()
        {
        }

        ///--------------------------------------------------------------------
        /// <summary>Initializes this object.</summary>
        ///
        /// <param name="minCoords">   The minimum coordinates.</param>
        /// <param name="maxCoords">   The maximum coordinates.</param>
        /// <param name="color">       The color.</param>
        /// <param name="lineWidth">   Width of the line.</param>
        /// <param name="duration">    The duration.</param>
        /// <param name="depthEnabled"> True to enable, false to disable the
        ///                             depth.</param>
        ///--------------------------------------------------------------------

        internal void Init(Vector3 minCoords, Vector3 maxCoords, Color color, float lineWidth, float duration, bool depthEnabled)
        {
            var size = maxCoords - minCoords;
            var center = minCoords + (size * 0.5f);
            matrix = Matrix4x4.TRS(center, Quaternion.identity, size);
            this.color = color;
            this.lineWidth = lineWidth;
            this.duration = duration;
            this.depthEnabled = depthEnabled;

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
            Graphics.DrawMesh(s_BoxMesh, matrix, material, 0, null, 0, materialProperties, false, false, false);
        }

    }

}

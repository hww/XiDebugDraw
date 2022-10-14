using UnityEngine;

namespace XiDebugDraw.Primitives
{
    /// <summary>An AOBB volume. This class cannot be inherited.</summary>
    public sealed class AOBB : Primitive
    {
        /// <summary>Width of the line.</summary>
        internal float lineWidth = 1;
        /// <summary>The matrix.</summary>
        private Matrix4x4 matrix;

        /// <summary>Default constructor.</summary>
        public AOBB()
        {
        }

        ///--------------------------------------------------------------------
        /// <summary>Initializes this object.</summary>
        ///
        /// <param name="centerTransform">The center transform.</param>
        /// <param name="scaleXYZ">       The scale xyz.</param>
        /// <param name="color">          The color.</param>
        /// <param name="lineWidth">      Width of the line.</param>
        /// <param name="duration">       The duration.</param>
        /// <param name="depthEnabled">     True to enable, false to disable
        ///                                 the depth.</param>
        ///--------------------------------------------------------------------

        internal void Init(Transform centerTransform, Vector3 scaleXYZ, Color color, float lineWidth, float duration, bool depthEnabled)
        {
            matrix = Matrix4x4.TRS(centerTransform.position, centerTransform.rotation, scaleXYZ);
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

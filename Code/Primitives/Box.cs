using CjLib;
using UnityEngine;

namespace XiDebugDraw.Primitives
{
    /// <summary>A box. This class cannot be inherited.</summary>
    public sealed class Box : Primitive
    {
        /// <summary>The matrix.</summary>
        Matrix4x4 matrix;

        /// <summary>Default constructor.</summary>
        public Box()
        {
          
        }

        ///--------------------------------------------------------------------
        /// <summary>Initializes this object.</summary>
        ///
        /// <param name="position">    The position.</param>
        /// <param name="rotation">    The rotation.</param>
        /// <param name="size">        The size.</param>
        /// <param name="color">       The color.</param>
        /// <param name="duration">    The duration.</param>
        /// <param name="depthEnabled"> True to enable, false to disable the
        ///                             depth.</param>
        ///--------------------------------------------------------------------

        internal void Init(Vector3 position, Quaternion rotation, Vector3 size, Color color, float duration, bool depthEnabled)
        {
            matrix = Matrix4x4.TRS(position, rotation, size);
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

        internal override void Render(Material material, MaterialPropertyBlock materialProperties)
        {
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            materialProperties.SetColor("_Color", color);
            Graphics.DrawMesh(s_BoxMesh, matrix, material, 0, null, 0, materialProperties, false, false, false);
        }
    }
}


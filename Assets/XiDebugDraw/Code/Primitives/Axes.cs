using CjLib;
using UnityEngine;

namespace XiDebugDraw.Primitives
{
    /// <summary>An axes. This class cannot be inherited.</summary>
    public sealed class Axes : Primitive
    {
        /// <summary>The axis mesh.</summary>
        static Mesh s_AxisMesh;
        /// <summary>The matrix.</summary>
        private Matrix4x4 matrix;

        /// <summary>(Immutable) the lines.</summary>
        private static readonly Vector3[] lines =  {
            new(0,0,0), new(10,0,0),
            new(0,0,0), new(0,10,0),
            new(0,0,0), new(0,0,10)
            };
        /// <summary>(Immutable) the colors.</summary>
        private static readonly Color[] colors =  {
            new(1,0,0), new(1,0,0), new(0,1,0), new(0,1,0), new(0,0,1), new(0,0,1)
            };

        /// <summary>Default constructor.</summary>
        public Axes()
        {
            s_AxisMesh ??= MakeLines(lines, colors);
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

        internal void Init(Vector3 position, Quaternion rotation, float size, Color color, float duration, bool depthEnabled)
        {
            matrix = Matrix4x4.TRS(position, rotation, new(size*0.1f, size * 0.1f, size * 0.1f));
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
            materialProperties.SetColor("_Color", Color.white);
            Graphics.DrawMesh(s_AxisMesh, matrix, material, 0, null, 0, materialProperties, false, false, false);
            materialProperties.SetColor("_Color", color);
            Graphics.DrawMesh(s_BoxMesh, matrix, material, 0, null, 0, materialProperties, false, false, false);
        }
    }
}

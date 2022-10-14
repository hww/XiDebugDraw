using CjLib;
using UnityEngine;
using static CjLib.DebugUtil;

namespace XiDebugDraw.Primitives
{
    /// <summary>A capsule. This class cannot be inherited.</summary>
    public sealed class Capsule : Primitive
    {
        /// <summary>The position.</summary>
        public Vector3 position;
        /// <summary>The rotation.</summary>
        public Quaternion rotation;
        /// <summary>The radius.</summary>
        float radius; 
        /// <summary>The height.</summary>
        float height;

        /// <summary>The capsule mesh.</summary>
        static Mesh s_CapsuleMesh;

        /// <summary>Default constructor.</summary>
        public Capsule ()
        {
            s_CapsuleMesh = PrimitiveMeshFactory.CapsuleWireframe(4, 12, true, false, true);
        }

        ///--------------------------------------------------------------------
        /// <summary>Initializes this object.</summary>
        ///
        /// <param name="position">    The position.</param>
        /// <param name="rotation">    The rotation.</param>
        /// <param name="radius">      The radius.</param>
        /// <param name="height">      The height.</param>
        /// <param name="color">       The color.</param>
        /// <param name="depthEnabled"> True to enable, false to disable the
        ///                             depth.</param>
        ///--------------------------------------------------------------------

        internal void Init(Vector3 position, Quaternion rotation, float radius, float height, Color color, bool depthEnabled)
        {
            this.position = position;
            this.rotation = rotation;
            this.radius = radius;
            this.height = Mathf.Max(0, height - radius - radius);
            this.color = color;
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
            materialProperties.SetVector("_Dimensions", new Vector4(radius, radius, radius, height));
            materialProperties.SetColor("_Color", color);
            Graphics.DrawMesh(s_CapsuleMesh, position, rotation, material, 0, null, 0, materialProperties, false, false, false); ;
        }
    }
}


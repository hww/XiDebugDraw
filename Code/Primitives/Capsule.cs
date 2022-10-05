using CjLib;
using UnityEngine;
using static CjLib.DebugUtil;

namespace XiDebugDraw.Primitives
{
    public sealed class Capsule : Primitive
    {
        public Vector3 position;
        public Quaternion rotation;
        float radius; float height;

        static Mesh s_CapsuleMesh;

        public Capsule ()
        {
            s_CapsuleMesh = PrimitiveMeshFactory.CapsuleWireframe(4, 12, true, false, true);
        }

        internal void Init(Vector3 position, Quaternion rotation, float radius, float height, Color color, bool depthEnabled)
        {
            this.position = position;
            this.rotation = rotation;
            this.radius = radius;
            this.height = Mathf.Max(0, height - radius - radius);
            this.color = color;
            this.depthEnabled = depthEnabled;
        }

        internal override void Render(Material material, MaterialPropertyBlock materialProperties)
        {
            materialProperties.SetVector("_Dimensions", new Vector4(radius, radius, radius, height));
            materialProperties.SetColor("_Color", color);
            Graphics.DrawMesh(s_CapsuleMesh, position, rotation, material, 0, null, 0, materialProperties, false, false, false); ;
        }

    }
}


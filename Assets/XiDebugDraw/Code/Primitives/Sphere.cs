using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public sealed class Sphere : Primitive
    {
        Matrix4x4 matrix;
        internal float radius;

        public Sphere ( )
        {

        }

        internal void Init(Vector3 position, float size, Color color, float duration, bool depthEnabled)
        {
            matrix = Matrix4x4.TRS(position, Quaternion.identity, new Vector3(size, size, size));
            this.color = color;
            this.duration = duration;
            this.depthEnabled = depthEnabled;
        }

        internal override void Render(Material material, MaterialPropertyBlock materialProperties)
        {
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            materialProperties.SetColor("_Color", color);
            Graphics.DrawMesh(s_SphereMesh, matrix, material, 0, null, 0, materialProperties, false, false, false);
        }
    }
}


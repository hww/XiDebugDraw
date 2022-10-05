using CjLib;
using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public sealed class Circle : Primitive
    {
        private Matrix4x4 matrix;

        public Circle()
        {

        }

        internal void Init(Vector3 position, Vector3 normal, float size, Color color, float duration, bool depthEnabled)
        {
            Vector3 normalCrosser = Mathf.Abs(Vector3.Dot(normal, Vector3.up)) < 0.5f ? Vector3.up : Vector3.forward;
            Vector3 tangent = Vector3.Normalize(Vector3.Cross(normalCrosser, normal));
            Quaternion rotation = Quaternion.LookRotation(tangent, normal);
            matrix = Matrix4x4.TRS(position, rotation, new(size, size, size));
            this.color = color;
            this.duration = duration;
            this.depthEnabled = depthEnabled;
        }
        internal override void Render(Material material, MaterialPropertyBlock materialProperties)
        {
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            materialProperties.SetColor("_Color", color);
            Graphics.DrawMesh(s_Circle, matrix, material, 0, null, 0, materialProperties, false, false, false);
        }
    }
}

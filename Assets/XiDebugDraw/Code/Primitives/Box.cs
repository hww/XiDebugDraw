using CjLib;
using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public sealed class Box : Primitive
    {
        Matrix4x4 matrix;

        public Box()
        {
          
        }

        internal void Init(Vector3 position, Quaternion rotation, Vector3 size, Color color, float duration, bool depthEnabled)
        {
            matrix = Matrix4x4.TRS(position, rotation, size);
            this.color = color;
            this.duration = duration;
            this.depthEnabled = depthEnabled;
        }

        internal override void Render(Material material, MaterialPropertyBlock materialProperties)
        {
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            materialProperties.SetColor("_Color", color);
            Graphics.DrawMesh(s_BoxMesh, matrix, material, 0, null, 0, materialProperties, false, false, false);
        }
    }
}


using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public sealed class AOBB : Primitive
    {
        internal float lineWidth = 1;
        private Matrix4x4 matrix;

        public AOBB()
        {
        }
        internal void Init(Transform centerTransform, Vector3 scaleXYZ, Color color, float lineWidth, float duration, bool depthEnabled)
        {
            matrix = Matrix4x4.TRS(centerTransform.position, centerTransform.rotation, scaleXYZ);
            this.color = color;
            this.lineWidth = lineWidth;
            this.duration = duration;
            this.depthEnabled = depthEnabled;
        }

        internal override void Render(Material material, MaterialPropertyBlock materialProperties)
        {
            materialProperties.SetColor("_Color", color);
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            Graphics.DrawMesh(s_BoxMesh, matrix, material, 0, null, 0, materialProperties, false, false, false);
        }

    }

}

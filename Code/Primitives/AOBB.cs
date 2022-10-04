using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class AOBB : Primitive
    {
        public float lineWidth = 1;
        private Matrix4x4 matrix;

        public AOBB()
        {
        }
        public void Init(Matrix4x4 centerTransform, Vector3 scaleXYZ, Color color, float lineWidth, float duration, bool depthEnabled)
        {
            matrix = Matrix4x4.TRS(centerTransform.GetPosition(), centerTransform.rotation, scaleXYZ);
            this.color = color;
            this.lineWidth = lineWidth;
            this.duration = duration;
            this.depthEnabled = depthEnabled;
        }

        public override void Render()
        {
            MaterialPropertyBlock materialProperties = GetMaterialPropertyBlock();
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            materialProperties.SetFloat("_ZBias", s_wireframeZBias);
            materialProperties.SetColor("_Color", color);
            Graphics.DrawMesh(s_BoxMesh, matrix, s_PrimitiveMaterial, 0, null, 0, materialProperties, false, false, false);
        }

    }

}

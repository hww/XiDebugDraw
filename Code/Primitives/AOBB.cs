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
        public void SetTransform(Matrix4x4 centerTransform, Vector3 scaleXYZ)
        {
            matrix = Matrix4x4.TRS(centerTransform.GetPosition(), centerTransform.rotation, scaleXYZ);
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

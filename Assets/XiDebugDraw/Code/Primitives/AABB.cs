using UnityEngine;


namespace XiDebugDraw.Primitives
{
    public class AABB : Primitive
    {
        public float lineWidth = 1;
        Matrix4x4 matrix;

        public AABB()
        {
        }
        public void Init(Vector3 minCoords, Vector3 maxCoords, Color color, float lineWidth, float duration, bool depthEnabled)
        {
            var size = maxCoords - minCoords;
            var center = minCoords + (size * 0.5f);
            matrix = Matrix4x4.TRS(center, Quaternion.identity, size);
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

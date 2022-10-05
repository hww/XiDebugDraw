using UnityEngine;


namespace XiDebugDraw.Primitives
{
    public sealed class AABB : Primitive
    {
        internal float lineWidth = 1;
        Matrix4x4 matrix;

        public AABB()
        {
        }
        internal void Init(Vector3 minCoords, Vector3 maxCoords, Color color, float lineWidth, float duration, bool depthEnabled)
        {
            var size = maxCoords - minCoords;
            var center = minCoords + (size * 0.5f);
            matrix = Matrix4x4.TRS(center, Quaternion.identity, size);
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

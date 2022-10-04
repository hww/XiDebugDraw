using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class Sphere : Primitive
    {
        Matrix4x4 matrix;
        public float radius;

        public Sphere ( )
        {

        }

        public void Init(Vector3 position, float size, Color color, float duration, bool depthEnabled)
        {
            matrix = Matrix4x4.TRS(position, Quaternion.identity, new Vector3(size, size, size));
            this.color = color;
            this.duration = duration;
            this.depthEnabled = depthEnabled;
        }

        public override void Render()
        {
            MaterialPropertyBlock materialProperties = GetMaterialPropertyBlock();
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            materialProperties.SetFloat("_ZBias", s_wireframeZBias);
            materialProperties.SetColor("_Color", color);
            Graphics.DrawMesh(s_SphereMesh, matrix, s_PrimitiveMaterial, 0, null, 0, materialProperties, false, false, false);
        }
    }
}


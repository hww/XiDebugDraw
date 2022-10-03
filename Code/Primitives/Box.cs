using CjLib;
using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class Box : Primitive
    {
        Matrix4x4 matrix;

        public Box()
        {
          
        }

        public void SetTransform(Vector3 position, Quaternion rotation, Vector3 size)
        {
            matrix = Matrix4x4.TRS(position, rotation, size);
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


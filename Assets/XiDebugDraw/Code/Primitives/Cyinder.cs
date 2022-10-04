using CjLib;
using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class Cylinder : Primitive
    {
        Matrix4x4 matrix;

        public Cylinder()
        {
          
        }
        public void SetTransform(Vector3 position, Quaternion rotation, float radius, float height)
        {
            matrix = Matrix4x4.TRS(position, rotation, new Vector3(radius,height,radius));
        }


        public override void Render()
        {
            MaterialPropertyBlock materialProperties = GetMaterialPropertyBlock();
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            materialProperties.SetFloat("_ZBias", s_wireframeZBias);
            materialProperties.SetColor("_Color", color);
            Graphics.DrawMesh(s_CylinderMesh, matrix, s_PrimitiveMaterial, 0, null, 0, materialProperties, false, false, false);
        }
    }
}


using CjLib;
using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class Cross : Primitive
    {
     
        Matrix4x4 matrix;

        public Cross()
        {      
        }

        public void SetTransform(Vector3 position, float size)
        {
            matrix = Matrix4x4.TRS(position, Quaternion.identity, new(size, size, size));
        }

        public override void Render ( )
        {
            MaterialPropertyBlock materialProperties = GetMaterialPropertyBlock();
            materialProperties.SetColor("_Color", color);
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            materialProperties.SetFloat("_ZBias", s_wireframeZBias);

            Graphics.DrawMesh(s_CrossMesh, matrix, s_PrimitiveMaterial, 0, null, 0, materialProperties, false, false, false);
        }


    }
}

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

        public void Init(Vector3 position, float size, Color color, float duration, bool depthEnabled)
        {
            matrix = Matrix4x4.TRS(position, Quaternion.identity, new(size, size, size));
            this.color = color;
            this.duration = duration;
            this.depthEnabled = depthEnabled;
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

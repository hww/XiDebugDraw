using CjLib;
using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class Axes : Primitive
    {
        static Mesh s_AxisMesh;
        private Matrix4x4 matrix;

        private static readonly Vector3[] lines =  {
            new(0,0,0), new(10,0,0),
            new(0,0,0), new(0,10,0),
            new(0,0,0), new(0,0,10)
            };
        private static readonly Color[] colors =  {
            new(1,0,0), new(1,0,0), new(0,1,0), new(0,1,0), new(0,0,1), new(0,0,1)
            };

        public Axes()
        {
            s_AxisMesh ??= MakeLines(lines, colors);
        }
        public void SetTransform(Vector3 position, Quaternion rotation, float size)
        {
            matrix = Matrix4x4.TRS(position, rotation, new(size*0.1f, size * 0.1f, size * 0.1f));
        }

        public override void Render()
        {
            MaterialPropertyBlock materialProperties = GetMaterialPropertyBlock();
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            materialProperties.SetFloat("_ZBias", s_wireframeZBias);
            materialProperties.SetColor("_Color", Color.white);
            Graphics.DrawMesh(s_AxisMesh, matrix, s_PrimitiveMaterial, 0, null, 0, materialProperties, false, false, false);
            materialProperties.SetColor("_Color", color);
            Graphics.DrawMesh(s_BoxMesh, matrix, s_PrimitiveMaterial, 0, null, 0, materialProperties, false, false, false);
        }
    }
}

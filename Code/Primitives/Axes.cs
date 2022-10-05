using CjLib;
using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public sealed class Axes : Primitive
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
        internal void Init(Vector3 position, Quaternion rotation, float size, Color color, float duration, bool depthEnabled)
        {
            matrix = Matrix4x4.TRS(position, rotation, new(size*0.1f, size * 0.1f, size * 0.1f));
            this.color = color;
            this.duration = duration;
            this.depthEnabled = depthEnabled;
        }

        internal override void Render(Material material, MaterialPropertyBlock materialProperties)
        {
            materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
            materialProperties.SetColor("_Color", Color.white);
            Graphics.DrawMesh(s_AxisMesh, matrix, material, 0, null, 0, materialProperties, false, false, false);
            materialProperties.SetColor("_Color", color);
            Graphics.DrawMesh(s_BoxMesh, matrix, material, 0, null, 0, materialProperties, false, false, false);
        }
    }
}

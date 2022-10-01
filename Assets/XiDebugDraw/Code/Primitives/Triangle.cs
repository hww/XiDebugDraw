using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class Triangle : Primitive
    {
        public Vector3 vertex0;
        public Vector3 vertex1;
        public Vector3 vertex2;
        public Color color;
        public float lineWidth = 1f;
        public bool depthEnabled = true;

        public override void Render ( )
        {
            Draw.DrawLine ( vertex0, vertex1, color );
            Draw.DrawLine ( vertex1, vertex2, color );
            Draw.DrawLine ( vertex2, vertex0, color);
        }
    }
}

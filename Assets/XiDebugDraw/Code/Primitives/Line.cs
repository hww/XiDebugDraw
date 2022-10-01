using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class Line : Primitive
    {
        public Vector3 fromPosition;
        public Vector3 toPosition;
        public Color color;
        public float lineWidth;
        public bool depthEnabled;

        public override void Render ( )
        {
            //Draw.color = color;
            //Draw.Line3D ( fromPosition , toPosition );
        }
    }
}

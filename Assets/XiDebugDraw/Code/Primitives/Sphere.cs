using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class Sphere : Primitive
    {
        public Vector3 position;
        public float radius;
        public Color color;
        public bool depthEnabled = true;

        public Sphere ( )
        {

        }

        public override void Render ( ) {
            XiGraphics.DrawSphere(position, radius, color);
        }
    }
}


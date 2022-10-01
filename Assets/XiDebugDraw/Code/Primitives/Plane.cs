using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class Plane : Primitive
    {
        public Vector3 position;
        public Vector3 normal;
        public float size;
        public Color color;
        public bool depthEnabled;

        public override void Render ( )
        {
           // Draw.Rect ( position,normal );
        }
    }
}

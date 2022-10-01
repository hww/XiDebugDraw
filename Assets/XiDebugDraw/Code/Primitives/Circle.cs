using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class Circle : Primitive
    {
        public Vector3 position;
        public Vector3 normal;
        public float radius;
        public Color color;
        public bool depthEnabled;

        public override void Render ( )
        {
            //Draw.color = color;
            //Draw.Circle3D ( position, radius, normal );
        }
    }
}

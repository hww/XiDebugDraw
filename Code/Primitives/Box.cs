using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class Box : Primitive
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 size;
        public Color color;
        public bool depthEnabled;

        public override void Render ( ) {
        }
    }
}


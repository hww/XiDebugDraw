using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class AABB : Primitive
    {
        public Vector3 minCoords;
        public Vector3 maxCoords;
        public Color color;
        public float lineWidth;
        public bool depthEnabled;

        public override void Render ( )
        {
            var size = maxCoords - minCoords;
            var center = size * 0.5f;
            //Draw.Cube ( minCoords, size, Vector3.forward, Vector3.up );
        }
    }

}

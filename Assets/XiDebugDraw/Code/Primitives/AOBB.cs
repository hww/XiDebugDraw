using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class AOBB : Primitive
    {
        public Matrix4x4 centerTransform;
        public Vector3 scaleXYZ;

        public override void Render ( )
        {
            //var size = maxCoords - minCoords;
            //var center = size * 0.5f;
            //Draw.Cube ( minCoords, size, Vector3.forward, Vector3.up );
        }
    }

}

using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class OBB : Primitive
    {
        public Matrix4x4 centerTransform;
        public Vector3 scaleXYZ;
        public Vector3 maxCoords;
        public Color color;
        public float lineWidth;
        public bool depthEnabled;

        public override void Render ( )
        {
            throw new System.NotImplementedException ( );
        }
    }
}

using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class Cross : Primitive
    {
        public Vector3 position;
        public float size;

        public override void Render ( )
        {
            var x = new Vector3 ( size, 0, 0 );
            var y = new Vector3 ( 0, size, 0 );
            var z = new Vector3 ( 0, 0, size );
            //Draw.color = color;
            //Draw.Line3D ( position - x, position + x );
            //Draw.Line3D ( position - y, position + y );
            //Draw.Line3D ( position - z, position + z );
        }
    }
}

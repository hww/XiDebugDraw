using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class Axes : Primitive
    {
        public Transform transform;
        public float size;
        public Color color;
        public bool depthEnabled;

        public Axes ( )
        {

        }

        public override void Render ( )
        {
            //Draw.color = color;
           // Draw.Cube ( transform.position, Vector3.one * size * 0.1f );
            //Draw.color = Color.red;
            //Draw.Line2D ( transform.position, transform.position + transform.forward * size);
            //Draw.color = Color.green;
            //Draw.Line2D ( transform.position, transform.position + transform.up * size );
            //Draw.color = Color.blue;
            //Draw.Line2D ( transform.position, transform.position + transform.right * size );
        }
    }
}

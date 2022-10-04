using UnityEngine;
using XiDebugDraw;

public class DebugDrawDemoItem : MonoBehaviour
{
    public enum Mode
    {
        Sphere, Box, Cube, Capsule, String, AABB, AOBB, Axes, Cross, Line, Ray, Trinagle, Cone, Cylinder, Circle, Plane
    }

    public bool drawOnUpdate = true;
    public Mode mode = Mode.Sphere;
    public float radius = 1;
    public float size = 1;
    public Color lineColor = Color.white;
    public float lineWidth = 1;
    public float duration = 0;
    public bool depthTest;
    public Transform point0;
    public Transform point1;
    public Transform point2;
    public BoxCollider boxCollider;
    public string text = "Hello World";

    private void OnEnable()
    {
        DebugDrawManager.Initialize();
    }

    public void Update()
    {
        if (drawOnUpdate)
            Draw();
    }
    public void Draw()
    {
        switch (mode)
        {
            case Mode.Sphere:
                DebugDrawManager.AddSphere(transform.position, radius, lineColor, duration, depthTest);
                break;
            case Mode.Box:
                DebugDrawManager.AddBox(transform.position, transform.rotation, transform.localScale, lineColor, duration, depthTest);
                break;
            case Mode.Cube:
                DebugDrawManager.AddCube(transform.position, transform.rotation, size, lineColor, duration, depthTest);
                break;
            case Mode.Capsule:
                DebugDrawManager.AddCapsule(transform.position, transform.rotation, radius, size, lineColor, duration, depthTest);
                break;
            case Mode.String:
                DebugDrawManager.AddString(transform.position, text, lineColor, 0.1f, duration, depthTest);
                break;
            case Mode.AABB:
                DebugDrawManager.AddAABB(boxCollider.bounds.min, boxCollider.bounds.max, lineColor, lineWidth, duration, depthTest);
                break;
            case Mode.AOBB:
                DebugDrawManager.AddAOBB(transform, boxCollider.size, lineColor, lineWidth, duration, depthTest);
                break;
            case Mode.Axes:
                DebugDrawManager.AddAxes(transform, lineColor, size, duration, depthTest);
                break;
            case Mode.Cross:
                DebugDrawManager.AddCross(transform.position, lineColor, size, duration, depthTest);
                break;
            case Mode.Line:
                DebugDrawManager.AddLine(point0.position, point1.position, lineColor, lineWidth, duration, depthTest);
                break;
            case Mode.Ray:
                DebugDrawManager.AddRay(transform.position, transform.up, size, lineColor, duration, depthTest);
                break;
            case Mode.Trinagle:
                DebugDrawManager.AddTriangle(point0.position, point1.position, point2.position, lineColor, lineWidth, duration, depthTest);
                break;
            case Mode.Cone:
                DebugDrawManager.AddCone(transform.position, transform.rotation, radius, size, lineColor, duration, depthTest);
                break;
            case Mode.Cylinder:
                DebugDrawManager.AddCylinder(transform.position, transform.rotation, radius, size, lineColor, duration, depthTest);
                break;
            case Mode.Circle:
                DebugDrawManager.AddCircle(transform.position, transform.up, radius,lineColor, duration, depthTest);
                break;
            case Mode.Plane:
                DebugDrawManager.AddPlane(transform.position, transform.up, size, lineColor, duration, depthTest);
                break;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector3.one * 0.1f );
    }

}

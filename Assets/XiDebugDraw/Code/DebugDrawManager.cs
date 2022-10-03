#define _DEBUG

using Conditional = System.Diagnostics.ConditionalAttribute;

using XiDebugDraw.Primitives;
using Vector3 = UnityEngine.Vector3;
using Color = UnityEngine.Color;
using Transform = UnityEngine.Transform;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Quaternion = UnityEngine.Quaternion;

namespace XiDebugDraw
{
    public static class DebugDrawManager
    {
        static bool isInitialized = false;
        static PrimitivesPool<Line> _line;
        static PrimitivesPool<AABB> _aabb;
        static PrimitivesPool<AOBB> _aobb;
        static PrimitivesPool<Axes> _axes;
        static PrimitivesPool<Box> _boxes;
        static PrimitivesPool<Cone> _cones;
        static PrimitivesPool<Cylinder> _cylinders;
        static PrimitivesPool<Circle> _circle;
        static PrimitivesPool<Cross> _cross;
        static PrimitivesPool<Ray> _ray;
        static PrimitivesPool<Sphere> _spheres;
        static PrimitivesPool<Text> _text;
        static PrimitivesPool<Triangle> _triangle;
        static PrimitivesPool<Capsule> _capsule;

        static PrimitivesPool<Plane> _planes;

        [Conditional("_DEBUG")]
        public static void Initialize()
        {
            if (!isInitialized)
            {
                Primitive.Initialize();
                _line = new PrimitivesPool<Line>(8);
                _aabb = new PrimitivesPool<AABB>(8);
                _aobb = new PrimitivesPool<AOBB>(8);
                _axes = new PrimitivesPool<Axes>(8);
                _boxes = new PrimitivesPool<Box>(8);
                _cones = new PrimitivesPool<Cone>(8);
                _cylinders = new PrimitivesPool<Cylinder>(8);
                _circle = new PrimitivesPool<Circle>(8);
                _cross = new PrimitivesPool<Cross>(8);
                _ray = new PrimitivesPool<Ray>(8);
                _planes = new PrimitivesPool<Plane>(8);
                _spheres = new PrimitivesPool<Sphere>(8);
                _text = new PrimitivesPool<Text>(8);
                _triangle = new PrimitivesPool<Triangle>(8);
                _capsule = new PrimitivesPool<Capsule>(8);
                isInitialized = true;
            }
        }
        [Conditional("_DEBUG")]
        public static void Deinitialize()
        {
            _line.Clear();  
            _aabb.Clear();
            _aobb.Clear();
            _axes.Clear();
            _boxes.Clear();
            _cones.Clear();
            _cylinders.Clear();
            _circle.Clear();
            _cross.Clear();
            _ray.Clear();
            _planes.Clear();
            _spheres.Clear();
            _text.Clear();
            _triangle.Clear();
            _capsule.Clear();
        }
        [Conditional("_DEBUG")]
        public static void Render()
        {
            var dt = UnityEngine.Time.deltaTime;
            _line.Render(dt);
            _aabb.Render(dt);
            _aobb.Render(dt);
            _axes.Render(dt);
            _boxes.Render(dt);
            _circle.Render(dt);
            _cross.Render(dt);
            _ray.Render(dt);
            _spheres.Render(dt);
            _cones.Render(dt);
            _cylinders.Render(dt);
            _planes.Render(dt);
            _text.Render(dt);
            _triangle.Render(dt);
            _capsule.Render(dt);
        }

        public static string GetStatistics()
        {
            if (isInitialized)
            {
                var dt = UnityEngine.Time.deltaTime;
                var sb = new System.Text.StringBuilder();
                sb.AppendLine(_line.GetStatistics());
                sb.AppendLine(_aabb.GetStatistics());
                sb.AppendLine(_aobb.GetStatistics());
                sb.AppendLine(_axes.GetStatistics());
                sb.AppendLine(_boxes.GetStatistics());
                sb.AppendLine(_circle.GetStatistics());
                sb.AppendLine(_cross.GetStatistics());
                sb.AppendLine(_ray.GetStatistics());
                sb.AppendLine(_spheres.GetStatistics());
                sb.AppendLine(_text.GetStatistics());
                sb.AppendLine(_triangle.GetStatistics());
                sb.AppendLine(_capsule.GetStatistics());
                return sb.ToString();
            }
            else
                return string.Empty;
        }


        [Conditional("_DEBUG")]
        public static void AddLine(Vector3 fromPosition,
                                   Vector3 toPosition, 
                                   Color color, 
                                   float lineWidth = 1.0f, 
                                   float duration = 0,
                                   bool depthEnabled = true)
        {
            var item = _line.Get();
            item.SetTransform(fromPosition, toPosition);
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            item.SetVisible(true);
        }
        [Conditional("_DEBUG")]
        public static void AddCross(Vector3 position,
                                    Color color,
                                    float size,
                                    float duration = 0,
                                    bool depthEnabled = true)
        {
            var item = _cross.Get();
            item.SetTransform(position, size);
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            item.SetVisible(true);
        }

        [Conditional("_DEBUG")]
        public static void AddSphere(Vector3 position,
                                     float radius,
                                     Color color,
                                     float duration = 0,
                                     bool depthEnabled = true)
        {
            var item = _spheres.Get();
            item.SetTransform(position, radius);
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            item.SetVisible(true);
        }
        [Conditional("_DEBUG")]
        public static void AddCircle(Vector3 position,
                                     Vector3 normal,
                                     float radius,
                                     Color color,
                                     float duration = 0,
                                     bool depthEnabled = true)
        {
            var item = _circle.Get();
            item.SetTransform(position, normal, radius);
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            item.SetVisible(true);
        }
        [Conditional("_DEBUG")]
        public static void AddPlane(Vector3 position,
                                     Vector3 normal,
                                     float size,
                                     Color color,
                                     float duration = 0,
                                     bool depthEnabled = true)
        {
            var item = _planes.Get();
            item.SetTransform(position, normal, size);
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            item.SetVisible(true);
        }

        [Conditional("_DEBUG")]
        public static void AddAxes(Transform transform,
                                   Color color,
                                   float size,
                                   float duration = 0,
                                   bool depthEnabled = true)
        {
            var item = _axes.Get();
            item.SetTransform(transform.position, transform.rotation,size);    
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            item.SetVisible(true);
        }

        [Conditional("_DEBUG")]
        public static void AddTriangle(Vector3 vertex0,
                                       Vector3 vertex1,
                                       Vector3 vertex2,
                                       Color color,
                                       float lineWidth,
                                       float duration = 0,
                                       bool depthEnabled = true)
        {
            var item = _triangle.Get();
            item.SetTransform(vertex0, vertex1, vertex2);
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            item.SetVisible(true);
        }

        [Conditional("_DEBUG")]
        public static void AddBox(Vector3 position,
                                  Quaternion rotation,
                                  Vector3 size,
                                  Color color,
                                  float duration = 0,
                                  bool depthEnabled = true)
        {
            var item = _boxes.Get();
            item.SetTransform(position, rotation, size);
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            item.SetVisible(true);
        }
        [Conditional("_DEBUG")]
        public static void AddCone(Vector3 position,
                          Quaternion rotation,
                          float radius,
                          float heigth,
                          Color color,
                          float duration = 0,
                          bool depthEnabled = true)
        {
            var item = _cones.Get();
            item.SetTransform(position, rotation, radius, heigth);
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            item.SetVisible(true);
        }
        [Conditional("_DEBUG")]
        public static void AddCylinder(Vector3 position,
                                 Quaternion rotation,
                                 float radius,
                                 float heigth,
                                 Color color,
                                 float duration = 0,
                                 bool depthEnabled = true)
        {
            var item = _cylinders.Get();
            item.SetTransform(position, rotation, radius, heigth);
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            item.SetVisible(true);
        }

        [Conditional("_DEBUG")]
        public static void AddCube(Vector3 position,
                                   Quaternion rotation,
                                   float size,
                                   Color color,
                                   float duration = 0,
                                   bool depthEnabled = true)
        {
            var item = _boxes.Get();
            item.SetTransform(position, rotation, new Vector3(size, size, size));
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            item.SetVisible(true);
        }
        [Conditional("_DEBUG")]
        public static void AddRay(Vector3 position,
                           Vector3 direction,
                           float size,
                           Color color,
                           float duration = 0,
                           bool depthEnabled = true)
        {
            var item = _ray.Get();
            item.SetTransform(position, direction, size);
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            item.SetVisible(true);
        }


        [Conditional("_DEBUG")]
        public static void AddAABB(Vector3 minCoords,
                                   Vector3 maxCoord,
                                   Color color,
                                   float lineWidth,
                                   float duration = 0,
                                   bool depthEnabled = true)
        {
            var item = _aabb.Get();
            item.SetTransform(minCoords, maxCoord);
            item.color = color;
            item.lineWidth = lineWidth;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            item.SetVisible(true);
        }

        [Conditional("_DEBUG")]
        public static void AddAOBB(Matrix4x4 centerTransform,
                                  Vector3 scaleXYZ,
                                  Color color,
                                  float lineWidth,
                                  float duration = 0,
                                  bool depthEnabled = true)
        {
            var item = _aobb.Get();
            item.SetTransform(centerTransform, scaleXYZ);
            item.color = color;
            item.lineWidth = lineWidth;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            item.SetVisible(true);
        }

        [Conditional("_DEBUG")]
        public static void AddString(Vector3 position,
                                  string text,
                                  Color color,
                                  float size = 0.1f,
                                  float duration = 0,
                                  bool depthEnabled = true)
        {
            var item = _text.Get();
            item.position = position;
            item.text = text;
            item.size = size;
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            item.SetVisible(true);
        }
        [Conditional("_DEBUG")] 
        public static void AddCapsule(Vector3 position,
                          Quaternion roation,
                          float radius,

                          float heigth,
                          Color color,
                          float duration = 0,
                          bool depthEnabled = true)
        {
            var item = _capsule.Get();
            item.position = position;
            item.rotation = roation;
            item.radius = radius;
            item.heigth = heigth;
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
        }
    }
}

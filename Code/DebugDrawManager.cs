#define _DEBUG

using Conditional = System.Diagnostics.ConditionalAttribute;

using XiDebugDraw.Primitives;
using Vector3 = UnityEngine.Vector3;
using Color = UnityEngine.Color;
using Transform = UnityEngine.Transform;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Quaternion = UnityEngine.Quaternion;
using System.Collections.Generic;
using Material = UnityEngine.Material;
using MaterialPropertyBlock = UnityEngine.MaterialPropertyBlock;

using static CjLib.DebugUtil;

namespace XiDebugDraw
{
    public static class DebugDrawManager
    {
        static bool isInitialized = false;
        static PrimitivesPool<Line> s_Lines;
        static PrimitivesPool<AABB> s_AABBs;
        static PrimitivesPool<AOBB> s_AOBBs;
        static PrimitivesPool<Axes> s_Axes;
        static PrimitivesPool<Box> s_Boxes;
        static PrimitivesPool<Cone> s_Cones;
        static PrimitivesPool<Cylinder> s_Cylinders;
        static PrimitivesPool<Circle> s_Circles;
        static PrimitivesPool<Cross> s_Crosses;
        static PrimitivesPool<Ray> s_Rays;
        static PrimitivesPool<Plane> s_Planes;
        static PrimitivesPool<Sphere> s_Spheres;
        static PrimitivesPool<Text> s_Strings;
        static PrimitivesPool<Triangle> s_Triangles;
        static PrimitivesPool<Capsule> s_Capsules;

        static LinkedList<Primitive> s_PrimitivesDepthEnabled;
        static LinkedList<Primitive> s_PrimitivesDepthDisabled;
        
        [Conditional("_DEBUG")]
        public static void Initialize()
        {
            if (!isInitialized)
            {
                Primitive.Initialize();
                s_Lines = new PrimitivesPool<Line>(8);
                s_AABBs = new PrimitivesPool<AABB>(8);
                s_AOBBs = new PrimitivesPool<AOBB>(8);
                s_Axes = new PrimitivesPool<Axes>(8);
                s_Boxes = new PrimitivesPool<Box>(8);
                s_Cones = new PrimitivesPool<Cone>(8);
                s_Cylinders = new PrimitivesPool<Cylinder>(8);
                s_Circles = new PrimitivesPool<Circle>(8);
                s_Crosses = new PrimitivesPool<Cross>(8);
                s_Rays = new PrimitivesPool<Ray>(8);
                s_Planes = new PrimitivesPool<Plane>(8);
                s_Spheres = new PrimitivesPool<Sphere>(8);
                s_Strings = new PrimitivesPool<Text>(8);
                s_Triangles = new PrimitivesPool<Triangle>(8);
                s_Capsules = new PrimitivesPool<Capsule>(8);
                s_PrimitivesDepthEnabled = new ();
                s_PrimitivesDepthDisabled = new();
                isInitialized = true;
            }
        }
        [Conditional("_DEBUG")]
        public static void Deinitialize()
        {
            s_Lines.Clear();  
            s_AABBs.Clear();
            s_AOBBs.Clear();
            s_Axes.Clear();
            s_Boxes.Clear();
            s_Cones.Clear();
            s_Cylinders.Clear();
            s_Circles.Clear();
            s_Crosses.Clear();
            s_Rays.Clear();
            s_Planes.Clear();
            s_Spheres.Clear();
            s_Strings.Clear();
            s_Triangles.Clear();
            s_Capsules.Clear();
            s_PrimitivesDepthEnabled.Clear();
            s_PrimitivesDepthDisabled.Clear();
            isInitialized = false;
        }

        public static void GetStatistics(out int used, out int free)
        {
            if (isInitialized)
            {

                free = s_Lines.CountFree +
                s_AABBs.CountFree +
                s_AOBBs.CountFree +
                s_Axes.CountFree +
                s_Boxes.CountFree +
                s_Cones.CountFree +
                s_Cylinders.CountFree +
                s_Circles.CountFree +
                s_Crosses.CountFree +
                s_Rays.CountFree +
                s_Planes.CountFree +
                s_Spheres.CountFree +
                s_Strings.CountFree +
                s_Triangles.CountFree +
                s_Capsules.CountFree;

                used = s_PrimitivesDepthDisabled.Count + s_PrimitivesDepthEnabled.Count;
            }
            else
            {
                used = 0 ;
                free = 0 ;
            }
        }

        [Conditional("_DEBUG")]
        internal static void Render()
        {
            var dt = UnityEngine.Time.deltaTime;
            var materialProperties = Primitive.GetMaterialPropertyBlock();
            materialProperties.SetFloat("_ZBias", Primitive.s_wireframeZBias);

            var materialDepthEnabled = GetMaterial(Style.Wireframe, true, false);
            Render(dt, s_PrimitivesDepthEnabled, materialDepthEnabled, materialProperties);

            var materialDepthDisabled = GetMaterial(Style.Wireframe, false, false);
            Render(dt, s_PrimitivesDepthDisabled, materialDepthDisabled, materialProperties);
        }

        private static void Render(float dt, LinkedList<Primitive> list, Material material, MaterialPropertyBlock materialProperties)
        {
            var curent = list.First;
            while (curent != null)
            {
                var next = curent.Next;
                var prim = curent.Value;
                if (prim.duration >= 0)
                {
                    prim.Render(material, materialProperties);
                    prim.duration -= dt;
                }
                else
                {
                    prim.Deinit();
                    list.Remove(curent);
                    prim.pool.Release(prim);
                }
                curent = next;
            }
        }

        private static void AddPrimitive(Primitive primitive)
        {
            if (primitive.depthEnabled)
                s_PrimitivesDepthEnabled.AddFirst(primitive);
            else
                s_PrimitivesDepthDisabled.AddFirst(primitive);
        }

        [Conditional("_DEBUG")]
        public static void AddLine(Vector3 fromPosition,
                                   Vector3 toPosition, 
                                   Color color, 
                                   float lineWidth = 1.0f, 
                                   float duration = 0,
                                   bool depthEnabled = true)
        {
            var item = s_Lines.Get();
            item.Init(fromPosition, toPosition, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        [Conditional("_DEBUG")]
        public static void AddCross(Vector3 position,
                                    Color color,
                                    float size,
                                    float duration = 0,
                                    bool depthEnabled = true)
        {
            var item = s_Crosses.Get();
            item.Init(position, size, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        [Conditional("_DEBUG")]
        public static void AddSphere(Vector3 position,
                                     float radius,
                                     Color color,
                                     float duration = 0,
                                     bool depthEnabled = true)
        {
            var item = s_Spheres.Get();
            item.Init(position, radius, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        [Conditional("_DEBUG")]
        public static void AddCircle(Vector3 position,
                                     Vector3 normal,
                                     float radius,
                                     Color color,
                                     float duration = 0,
                                     bool depthEnabled = true)
        {
            var item = s_Circles.Get();
            item.Init(position, normal, radius, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        [Conditional("_DEBUG")]
        public static void AddPlane(Vector3 position,
                                     Vector3 normal,
                                     float size,
                                     Color color,
                                     float duration = 0,
                                     bool depthEnabled = true)
        {
            var item = s_Planes.Get();
            item.Init(position, normal, size, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        [Conditional("_DEBUG")]
        public static void AddAxes(Transform transform,
                                   Color color,
                                   float size,
                                   float duration = 0,
                                   bool depthEnabled = true)
        {
            var item = s_Axes.Get();
            item.Init(transform.position, transform.rotation, size,color, duration, depthEnabled);
            AddPrimitive(item);
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
            var item = s_Triangles.Get();
            item.SetTransform(vertex0, vertex1, vertex2);
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            AddPrimitive(item);
        }

        [Conditional("_DEBUG")]
        public static void AddBox(Vector3 position,
                                  Quaternion rotation,
                                  Vector3 size,
                                  Color color,
                                  float duration = 0,
                                  bool depthEnabled = true)
        {
            var item = s_Boxes.Get();
            item.Init(position, rotation, size, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        [Conditional("_DEBUG")]
        public static void AddCone(Vector3 position,
                          Quaternion rotation,
                          float radius,
                          float height,
                          Color color,
                          float duration = 0,
                          bool depthEnabled = true)
        {
            var item = s_Cones.Get();
            item.Init(position, rotation, radius, height, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        [Conditional("_DEBUG")]
        public static void AddCylinder(Vector3 position,
                                 Quaternion rotation,
                                 float radius,
                                 float height,
                                 Color color,
                                 float duration = 0,
                                 bool depthEnabled = true)
        {
            var item = s_Cylinders.Get();
            item.Init(position, rotation, radius, height, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        [Conditional("_DEBUG")]
        public static void AddCube(Vector3 position,
                                   Quaternion rotation,
                                   float size,
                                   Color color,
                                   float duration = 0,
                                   bool depthEnabled = true)
        {
            var item = s_Boxes.Get();
            item.Init(position, rotation, new Vector3(size, size, size), color, duration, depthEnabled);
            AddPrimitive(item);
        }

        [Conditional("_DEBUG")]
        public static void AddRay(Vector3 position,
                           Vector3 direction,
                           float size,
                           Color color,
                           float duration = 0,
                           bool depthEnabled = true)
        {
            var item = s_Rays.Get();
            item.Init(position, direction, size, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        [Conditional("_DEBUG")]
        public static void AddAABB(Vector3 minCoords,
                                   Vector3 maxCoord,
                                   Color color,
                                   float lineWidth,
                                   float duration = 0,
                                   bool depthEnabled = true)
        {
            var item = s_AABBs.Get();
            item.Init(minCoords, maxCoord, color, lineWidth, duration, depthEnabled);
            AddPrimitive(item);
        }

        [Conditional("_DEBUG")]
        public static void AddAOBB(Transform centerTransform,
                                  Vector3 scaleXYZ,
                                  Color color,
                                  float lineWidth,
                                  float duration = 0,
                                  bool depthEnabled = true)
        {
            var item = s_AOBBs.Get();
            item.Init(centerTransform, scaleXYZ, color, lineWidth, duration, depthEnabled);
            AddPrimitive(item);
        }

        [Conditional("_DEBUG")] 
        public static void AddCapsule(Vector3 position,
                          Quaternion roation,
                          float radius,

                          float height,
                          Color color,
                          float duration = 0,
                          bool depthEnabled = true)
        {
            var item = s_Capsules.Get();
            item.duration = duration;
            item.Init(position,roation,radius,height,color, depthEnabled);
            AddPrimitive(item);
        }

        [Conditional("_DEBUG")]
        public static void AddString(Vector3 position,
                          string text,
                          Color color,
                          float size = 0.1f,
                          float duration = 0,
                          bool depthEnabled = true)
        {
            var item = s_Strings.Get();
            item.Init(position, text, color, size, duration, depthEnabled);
            AddPrimitive(item);
        }
    }
}

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
    /// <summary>Manager for debug draws.</summary>
    public static class DebugDrawManager
    {
        /// <summary>True if is initialized, false if not.</summary>
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
        static LinkedList<Primitive> s_StringsDepthEnabled;

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) initializes this
        /// object.</summary>
        ///--------------------------------------------------------------------

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
                s_StringsDepthEnabled = new();
                s_PrimitivesDepthEnabled = new ();
                s_PrimitivesDepthDisabled = new();
                isInitialized = true;
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) de-initializes this
        /// object and frees any resources it is using.</summary>
        ///--------------------------------------------------------------------

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
            s_StringsDepthEnabled.Clear();
            s_PrimitivesDepthEnabled.Clear();
            s_PrimitivesDepthDisabled.Clear();
            isInitialized = false;
        }

        ///--------------------------------------------------------------------
        /// <summary>Gets the statistics.</summary>
        ///
        /// <param name="used">[out] The used.</param>
        /// <param name="free">[out] The free.</param>
        ///--------------------------------------------------------------------

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

                used = s_PrimitivesDepthDisabled.Count 
                     + s_PrimitivesDepthEnabled.Count 
                     + s_StringsDepthEnabled.Count;
            }
            else
            {
                used = 0 ;
                free = 0 ;
            }
        }

        /// <summary>(Only available in _DEBUG builds) renders this object.</summary>
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

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) renders this object.</summary>
        ///
        /// <param name="dt">                The delta time.</param>
        /// <param name="list">              The list to render.</param>
        /// <param name="material">          The material.</param>
        /// <param name="materialProperties">The material properties.</param>
        ///--------------------------------------------------------------------

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
                    prim.pool.Free(prim);
                }

                curent = next;
            }
        }

        /// <summary>Called for rendering and handling GUI events.</summary>
        internal static void OnGUI()
        {
            var dt = UnityEngine.Time.deltaTime;
            var materialProperties = Primitive.GetMaterialPropertyBlock();
            materialProperties.SetFloat("_ZBias", Primitive.s_wireframeZBias);

            var materialDepthEnabled = GetMaterial(Style.Wireframe, true, false);
            OnGUI(dt, s_StringsDepthEnabled, materialDepthEnabled, materialProperties);
        }

        ///--------------------------------------------------------------------
        /// <summary>Called for rendering and handling GUI events.</summary>
        ///
        /// <param name="dt">                The delta time.</param>
        /// <param name="list">              The list to render.</param>
        /// <param name="material">          The material.</param>
        /// <param name="materialProperties">The material properties.</param>
        ///--------------------------------------------------------------------

        private static void OnGUI(float dt, LinkedList<Primitive> list, Material material, MaterialPropertyBlock materialProperties)
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
                    prim.pool.Free(prim);
                }

                curent = next;
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>Adds a primitive.</summary>
        ///
        /// <param name="primitive">The primitive.</param>
        ///--------------------------------------------------------------------

        private static void AddPrimitive(Primitive primitive)
        {
            if (primitive.depthEnabled)
                s_PrimitivesDepthEnabled.AddFirst(primitive);
            else
                s_PrimitivesDepthDisabled.AddFirst(primitive);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds a line.</summary>
        ///
        /// <param name="fromPosition">from position.</param>
        /// <param name="toPosition">  to position.</param>
        /// <param name="color">       The color.</param>
        /// <param name="lineWidth">   (Optional) Width of the line.</param>
        /// <param name="duration">    (Optional) The duration.</param>
        /// <param name="depthEnabled"> (Optional) True to enable, false to
        ///                             disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")]
        public static void AddLine(Vector3 fromPosition,
                                   Vector3 toPosition, 
                                   Color color, 
                                   float lineWidth = 1.0f, 
                                   float duration = 0,
                                   bool depthEnabled = true)
        {
            var item = s_Lines.Allocate();
            item.Init(fromPosition, toPosition, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds the cross.</summary>
        ///
        /// <param name="position">    The position.</param>
        /// <param name="color">       The color.</param>
        /// <param name="size">        The size.</param>
        /// <param name="duration">    (Optional) The duration.</param>
        /// <param name="depthEnabled"> (Optional) True to enable, false to
        ///                             disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")]
        public static void AddCross(Vector3 position,
                                    Color color,
                                    float size,
                                    float duration = 0,
                                    bool depthEnabled = true)
        {
            var item = s_Crosses.Allocate();
            item.Init(position, size, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds a sphere.</summary>
        ///
        /// <param name="position">    The position.</param>
        /// <param name="radius">      The radius.</param>
        /// <param name="color">       The color.</param>
        /// <param name="duration">    (Optional) The duration.</param>
        /// <param name="depthEnabled"> (Optional) True to enable, false to
        ///                             disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")]
        public static void AddSphere(Vector3 position,
                                     float radius,
                                     Color color,
                                     float duration = 0,
                                     bool depthEnabled = true)
        {
            var item = s_Spheres.Allocate();
            item.Init(position, radius, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds a circle.</summary>
        ///
        /// <param name="position">    The position.</param>
        /// <param name="normal">      The normal.</param>
        /// <param name="radius">      The radius.</param>
        /// <param name="color">       The color.</param>
        /// <param name="duration">    (Optional) The duration.</param>
        /// <param name="depthEnabled"> (Optional) True to enable, false to
        ///                             disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")]
        public static void AddCircle(Vector3 position,
                                     Vector3 normal,
                                     float radius,
                                     Color color,
                                     float duration = 0,
                                     bool depthEnabled = true)
        {
            var item = s_Circles.Allocate();
            item.Init(position, normal, radius, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds a plane.</summary>
        ///
        /// <param name="position">    The position.</param>
        /// <param name="normal">      The normal.</param>
        /// <param name="size">        The size.</param>
        /// <param name="color">       The color.</param>
        /// <param name="duration">    (Optional) The duration.</param>
        /// <param name="depthEnabled"> (Optional) True to enable, false to
        ///                             disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")]
        public static void AddPlane(Vector3 position,
                                     Vector3 normal,
                                     float size,
                                     Color color,
                                     float duration = 0,
                                     bool depthEnabled = true)
        {
            var item = s_Planes.Allocate();
            item.Init(position, normal, size, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds the axes.</summary>
        ///
        /// <param name="transform">   The transform.</param>
        /// <param name="color">       The color.</param>
        /// <param name="size">        The size.</param>
        /// <param name="duration">    (Optional) The duration.</param>
        /// <param name="depthEnabled"> (Optional) True to enable, false to
        ///                             disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")]
        public static void AddAxes(Transform transform,
                                   Color color,
                                   float size,
                                   float duration = 0,
                                   bool depthEnabled = true)
        {
            var item = s_Axes.Allocate();
            item.Init(transform.position, transform.rotation, size,color, duration, depthEnabled);
            AddPrimitive(item);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds a triangle.</summary>
        ///
        /// <param name="vertex0">     The vertex 0.</param>
        /// <param name="vertex1">     The first vertex.</param>
        /// <param name="vertex2">     The second vertex.</param>
        /// <param name="color">       The color.</param>
        /// <param name="lineWidth">   Width of the line.</param>
        /// <param name="duration">    (Optional) The duration.</param>
        /// <param name="depthEnabled"> (Optional) True to enable, false to
        ///                             disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")]
        public static void AddTriangle(Vector3 vertex0,
                                       Vector3 vertex1,
                                       Vector3 vertex2,
                                       Color color,
                                       float lineWidth,
                                       float duration = 0,
                                       bool depthEnabled = true)
        {
            var item = s_Triangles.Allocate();
            item.SetTransform(vertex0, vertex1, vertex2);
            item.color = color;
            item.duration = duration;
            item.depthEnabled = depthEnabled;
            AddPrimitive(item);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds a box.</summary>
        ///
        /// <param name="position">    The position.</param>
        /// <param name="rotation">    The rotation.</param>
        /// <param name="size">        The size.</param>
        /// <param name="color">       The color.</param>
        /// <param name="duration">    (Optional) The duration.</param>
        /// <param name="depthEnabled"> (Optional) True to enable, false to
        ///                             disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")]
        public static void AddBox(Vector3 position,
                                  Quaternion rotation,
                                  Vector3 size,
                                  Color color,
                                  float duration = 0,
                                  bool depthEnabled = true)
        {
            var item = s_Boxes.Allocate();
            item.Init(position, rotation, size, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds a cone.</summary>
        ///
        /// <param name="position">    The position.</param>
        /// <param name="rotation">    The rotation.</param>
        /// <param name="radius">      The radius.</param>
        /// <param name="height">      The height.</param>
        /// <param name="color">       The color.</param>
        /// <param name="duration">    (Optional) The duration.</param>
        /// <param name="depthEnabled"> (Optional) True to enable, false to
        ///                             disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")]
        public static void AddCone(Vector3 position,
                          Quaternion rotation,
                          float radius,
                          float height,
                          Color color,
                          float duration = 0,
                          bool depthEnabled = true)
        {
            var item = s_Cones.Allocate();
            item.Init(position, rotation, radius, height, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds a cylinder.</summary>
        ///
        /// <param name="position">    The position.</param>
        /// <param name="rotation">    The rotation.</param>
        /// <param name="radius">      The radius.</param>
        /// <param name="height">      The height.</param>
        /// <param name="color">       The color.</param>
        /// <param name="duration">    (Optional) The duration.</param>
        /// <param name="depthEnabled"> (Optional) True to enable, false to
        ///                             disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")]
        public static void AddCylinder(Vector3 position,
                                 Quaternion rotation,
                                 float radius,
                                 float height,
                                 Color color,
                                 float duration = 0,
                                 bool depthEnabled = true)
        {
            var item = s_Cylinders.Allocate();
            item.Init(position, rotation, radius, height, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds a cube.</summary>
        ///
        /// <param name="position">    The position.</param>
        /// <param name="rotation">    The rotation.</param>
        /// <param name="size">        The size.</param>
        /// <param name="color">       The color.</param>
        /// <param name="duration">    (Optional) The duration.</param>
        /// <param name="depthEnabled"> (Optional) True to enable, false to
        ///                             disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")]
        public static void AddCube(Vector3 position,
                                   Quaternion rotation,
                                   float size,
                                   Color color,
                                   float duration = 0,
                                   bool depthEnabled = true)
        {
            var item = s_Boxes.Allocate();
            item.Init(position, rotation, new Vector3(size, size, size), color, duration, depthEnabled);
            AddPrimitive(item);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds a ray.</summary>
        ///
        /// <param name="position">    The position.</param>
        /// <param name="direction">   The direction.</param>
        /// <param name="size">        The size.</param>
        /// <param name="color">       The color.</param>
        /// <param name="duration">    (Optional) The duration.</param>
        /// <param name="depthEnabled"> (Optional) True to enable, false to
        ///                             disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")]
        public static void AddRay(Vector3 position,
                           Vector3 direction,
                           float size,
                           Color color,
                           float duration = 0,
                           bool depthEnabled = true)
        {
            var item = s_Rays.Allocate();
            item.Init(position, direction, size, color, duration, depthEnabled);
            AddPrimitive(item);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds a bb.</summary>
        ///
        /// <param name="minCoords">   The minimum coordinates.</param>
        /// <param name="maxCoord">    The maximum coordinate.</param>
        /// <param name="color">       The color.</param>
        /// <param name="lineWidth">   Width of the line.</param>
        /// <param name="duration">    (Optional) The duration.</param>
        /// <param name="depthEnabled"> (Optional) True to enable, false to
        ///                             disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")]
        public static void AddAABB(Vector3 minCoords,
                                   Vector3 maxCoord,
                                   Color color,
                                   float lineWidth,
                                   float duration = 0,
                                   bool depthEnabled = true)
        {
            var item = s_AABBs.Allocate();
            item.Init(minCoords, maxCoord, color, lineWidth, duration, depthEnabled);
            AddPrimitive(item);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds a obb.</summary>
        ///
        /// <param name="centerTransform">The center transform.</param>
        /// <param name="scaleXYZ">       The scale xyz.</param>
        /// <param name="color">          The color.</param>
        /// <param name="lineWidth">      Width of the line.</param>
        /// <param name="duration">       (Optional) The duration.</param>
        /// <param name="depthEnabled">     (Optional) True to enable, false
        ///                                 to disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")]
        public static void AddAOBB(Transform centerTransform,
                                  Vector3 scaleXYZ,
                                  Color color,
                                  float lineWidth,
                                  float duration = 0,
                                  bool depthEnabled = true)
        {
            var item = s_AOBBs.Allocate();
            item.Init(centerTransform, scaleXYZ, color, lineWidth, duration, depthEnabled);
            AddPrimitive(item);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds a capsule.</summary>
        ///
        /// <param name="position">    The position.</param>
        /// <param name="roation">     The roation.</param>
        /// <param name="radius">      The radius.</param>
        /// <param name="height">      The height.</param>
        /// <param name="color">       The color.</param>
        /// <param name="duration">    (Optional) The duration.</param>
        /// <param name="depthEnabled"> (Optional) True to enable, false to
        ///                             disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")] 
        public static void AddCapsule(Vector3 position,
                          Quaternion roation,
                          float radius,

                          float height,
                          Color color,
                          float duration = 0,
                          bool depthEnabled = true)
        {
            var item = s_Capsules.Allocate();
            item.duration = duration;
            item.Init(position,roation,radius,height,color, depthEnabled);
            AddPrimitive(item);
        }

        ///--------------------------------------------------------------------
        /// <summary>(Only available in _DEBUG builds) adds a string.</summary>
        ///
        /// <param name="position">    The position.</param>
        /// <param name="text">        The text.</param>
        /// <param name="color">       The color.</param>
        /// <param name="size">        (Optional) The size.</param>
        /// <param name="duration">    (Optional) The duration.</param>
        /// <param name="depthEnabled"> (Optional) True to enable, false to
        ///                             disable the depth.</param>
        ///--------------------------------------------------------------------

        [Conditional("_DEBUG")]
        public static void AddString(Vector3 position,
                          string text,
                          Color color,
                          float size = 0.1f,
                          float duration = 0,
                          bool depthEnabled = true)
        {
            var item = s_Strings.Allocate();
            item.Init(position, text, color, size, duration, depthEnabled);
            s_StringsDepthEnabled.AddFirst(item);
        }
    }
}

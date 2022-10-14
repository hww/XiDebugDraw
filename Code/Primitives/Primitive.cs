using CjLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace XiDebugDraw.Primitives
{
    ///------------------------------------------------------------------------
    /// <summary>A renderable primitive. All primitives will be inherinced
    /// from this class.</summary>
    ///------------------------------------------------------------------------

    public class Primitive
    {
        /// <summary>Fields.</summary>
        internal IBasePool pool;
        /// <summary>The link.</summary>
        internal LinkedListNode<Primitive> link;
        /// <summary>The duration.</summary>
        internal float duration = 0f;
        /// <summary>True to enable, false to disable the depth.</summary>
        internal bool depthEnabled;
        /// <summary>The color.</summary>
        internal Color color;

        /// <summary>constructors.</summary>
        public Primitive()
        {
            link = new LinkedListNode<Primitive>(this);
        }

        ///--------------------------------------------------------------------
        /// <summary>Renders this object.</summary>
        ///
        /// <exception cref="Exception">    Thrown when an exception error
        ///                                 condition occurs.</exception>
        ///
        /// <param name="material">          The material.</param>
        /// <param name="materialProperties">The material properties.</param>
        ///--------------------------------------------------------------------

        internal virtual void Render(Material material, MaterialPropertyBlock materialProperties)
        {
            throw new System.Exception();
        }

        /// <summary>Deinits this object.</summary>
        internal virtual void Deinit()
        {

        }

        /// <summary>The font.</summary>
        internal static Font s_Font;

        /// <summary>The text mesh material.</summary>
        internal static Material s_TextMeshMaterial;

        internal static Mesh s_BoxMesh;
        internal static Mesh s_ConeMesh;
        internal static Mesh s_CrossMesh;
        internal static Mesh s_PlaneMesh;
        internal static Mesh s_Circle;
        internal static Mesh s_CylinderMesh;
        internal static Mesh s_SphereMesh;

        /// <summary>(Immutable) the cross lines vertices.</summary>
        private static readonly Vector3[] s_CrossLinesVertices =  {
            new(-0.5f,0,0), new(0.5f,0,0),
            new(0,-0.5f,0), new(0,0.5f,0),
            new(0,0,-0.5f), new(0,0,0.5f)
            };

        /// <summary>The menu skin.</summary>
        public static GUISkin menuSkin;

        /// <summary>Initializes this object.</summary>
        internal static void Initialize()
        {
            s_Font = Resources.Load<Font>("XiDebugDraw/Fonts/LiberationMono");
            s_TextMeshMaterial = Resources.Load<Material>("XiDebugDraw/Materials/TextMeshMaterial");
            // Skin
            menuSkin = Resources.Load<GUISkin>("XiDebugDraw/Skins/Default Skin");
            menuSkin.box.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.7f));
            s_BoxMesh ??= PrimitiveMeshFactory.BoxWireframe();
            s_ConeMesh ??= PrimitiveMeshFactory.ConeWireframe(8);
            s_CrossMesh ??= MakeLines(s_CrossLinesVertices);
            s_PlaneMesh ??= PrimitiveMeshFactory.RectWireframe();
            s_Circle ??= PrimitiveMeshFactory.CircleWireframe(12);
            s_CylinderMesh ??= PrimitiveMeshFactory.CylinderWireframe(12);
            s_SphereMesh ??= PrimitiveMeshFactory.SphereWireframe(12, 12);
        }

        ///--------------------------------------------------------------------
        /// <summary>Makes a tex.</summary>
        ///
        /// <param name="width"> The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="col">   The col.</param>
        ///
        /// <returns>A Texture2D.</returns>
        ///--------------------------------------------------------------------

        internal static Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        ///--------------------------------------------------------------------
        /// <summary>Makes the lines.</summary>
        ///
        /// <param name="aVert">The vertical.</param>
        ///
        /// <returns>A Mesh.</returns>
        ///--------------------------------------------------------------------

        internal static Mesh MakeLines(Vector3[] aVert)
        {
            Mesh mesh = new Mesh();

            int[] aIndex = new int[aVert.Length];
            for (int i = 0; i < aVert.Length; ++i)
            {
                aIndex[i] = i;
            }

            mesh.vertices = aVert;
            mesh.SetIndices(aIndex, MeshTopology.Lines, 0);
            return mesh;
        }

        ///--------------------------------------------------------------------
        /// <summary>Makes the lines.</summary>
        ///
        /// <param name="aVert"> The vertical.</param>
        /// <param name="colors">The colors.</param>
        ///
        /// <returns>A Mesh.</returns>
        ///--------------------------------------------------------------------

        internal static Mesh MakeLines(Vector3[] aVert, Color[] colors)
        {
            Mesh mesh = new Mesh();

            int[] aIndex = new int[aVert.Length];
            for (int i = 0; i < aVert.Length; ++i)
            {
                aIndex[i] = i;
            }

            mesh.vertices = aVert;
            mesh.SetIndices(aIndex, MeshTopology.Lines, 0);
            mesh.SetColors(colors);
            return mesh;
        }


        /// <summary>The wireframe z coordinate bias.</summary>
        internal static float s_wireframeZBias = 1.0e-4f;


        /// <summary>The material properties.</summary>
        private static MaterialPropertyBlock s_materialProperties;

        ///--------------------------------------------------------------------
        /// <summary>Gets material property block.</summary>
        ///
        /// <returns>The material property block.</returns>
        ///--------------------------------------------------------------------

        internal static MaterialPropertyBlock GetMaterialPropertyBlock()
        {
            return (s_materialProperties != null) ? s_materialProperties : (s_materialProperties = new MaterialPropertyBlock());
        }
    }
    /// <summary>Interface for base pool.</summary>
    internal interface IBasePool
    {
        ///--------------------------------------------------------------------
        /// <summary>Releases the given object.</summary>
        ///
        /// <param name="o">A Primitive to process.</param>
        ///--------------------------------------------------------------------

        void Free(Primitive o);
    }

    ///------------------------------------------------------------------------
    /// <summary>The primitives pool.</summary>
    ///
    /// <typeparam name="T">Generic type parameter.</typeparam>
    ///------------------------------------------------------------------------

    internal class PrimitivesPool<T> : IBasePool where T : Primitive, new()
    {
        /// <summary>List of free primitives.</summary>
        LinkedList<Primitive> freeList = new LinkedList<Primitive>();

        ///--------------------------------------------------------------------
        /// <summary>Constructor.</summary>
        ///
        /// <param name="size">The size.</param>
        ///--------------------------------------------------------------------

        internal PrimitivesPool(int size)
        {
            for (var i = 0; i < size; i++)
            {
                var obj = new T();
                obj.pool = this;
                freeList.AddFirst(obj.link);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>Gets a list of frees.</summary>
        ///
        /// <value>A list of frees.</value>
        ///--------------------------------------------------------------------

        internal LinkedList<Primitive> FreeList => freeList;

        ///--------------------------------------------------------------------
        /// <summary>Allocate the primitive from this pool.</summary>
        ///
        /// <returns>A T.</returns>
        ///--------------------------------------------------------------------

        internal T Allocate()
        {
            if (freeList.Count == 0)
            {
                var o = new T();
                o.pool = this;
                return o;
            }
            else
            {
                var node = freeList.First;
                node.List.Remove(node);
                var o = (T)(object)node.Value;
                return o;
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>Releases the given object.</summary>
        ///
        /// <param name="o">A Primitive to process.</param>
        ///--------------------------------------------------------------------

        public void Free(Primitive o)
        {
            if (o.link.List != null)
                o.link.List.Remove(o.link);
            freeList.AddFirst(o.link);
        }

        ///--------------------------------------------------------------------
        /// <summary>Gets the total number of free.</summary>
        ///
        /// <value>The total number of free.</value>
        ///--------------------------------------------------------------------

        internal int CountFree
        {
            get { return freeList.Count; }
        }

        /// <summary>Clears this object to its blank/initial state.</summary>
        internal void Clear()
        {
            var curent = freeList.First;
            while (curent != null)
            {
                var next = curent.Next;
                freeList.Remove(curent);
                curent = next;
            }
        }
    }
}


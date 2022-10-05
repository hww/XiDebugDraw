using CjLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace XiDebugDraw.Primitives
{

    public class Primitive
    {
        // fields
        internal IBasePool pool;
        internal LinkedListNode<Primitive> link;
        internal float duration = 0f;
        internal bool depthEnabled;
        internal Color color;


        // constructors
        public Primitive()
        {
            link = new LinkedListNode<Primitive>(this);
        }

        internal virtual void Render(Material material, MaterialPropertyBlock materialProperties)
        {
            throw new System.Exception();
        }

        internal virtual void Deinit()
        {

        }

        internal static Font s_Font;

        internal static Material s_TextMeshMaterial;

        internal static Mesh s_BoxMesh;
        internal static Mesh s_ConeMesh;
        internal static Mesh s_CrossMesh;
        internal static Mesh s_PlaneMesh;
        internal static Mesh s_Circle;
        internal static Mesh s_CylinderMesh;
        internal static Mesh s_SphereMesh;



        private static readonly Vector3[] s_CrossLinesVertices =  {
            new(-0.5f,0,0), new(0.5f,0,0),
            new(0,-0.5f,0), new(0,0.5f,0),
            new(0,0,-0.5f), new(0,0,0.5f)
            };

        public static GUISkin menuSkin;

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


        internal static float s_wireframeZBias = 1.0e-4f;


        private static MaterialPropertyBlock s_materialProperties;
        internal static MaterialPropertyBlock GetMaterialPropertyBlock()
        {
            return (s_materialProperties != null) ? s_materialProperties : (s_materialProperties = new MaterialPropertyBlock());
        }
    }
    internal interface IBasePool
    {
        void Release(Primitive o);
    }

    internal class PrimitivesPool<T> : IBasePool where T : Primitive, new()
    {
        LinkedList<Primitive> freeList = new LinkedList<Primitive>();

        internal PrimitivesPool(int size)
        {
            for (var i = 0; i < size; i++)
            {
                var obj = new T();
                obj.pool = this;
                freeList.AddFirst(obj.link);
            }
        }

        internal LinkedList<Primitive> FreeList => freeList;

        internal T Get()
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
        public void Release(Primitive o)
        {
            if (o.link.List != null)
                o.link.List.Remove(o.link);
            freeList.AddFirst(o.link);
        }

        internal int CountFree
        {
            get { return freeList.Count; }
        }

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


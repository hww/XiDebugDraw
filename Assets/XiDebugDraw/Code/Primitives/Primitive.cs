using CjLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace XiDebugDraw.Primitives
{

    public partial class Primitive 
    {
        // fields
        public LinkedListNode<Primitive> link;
        public float duration = 0f;
        public bool depthEnabled;
        public Color color;


        // constructors
        public Primitive ()
        {
            link = new LinkedListNode<Primitive> ( this );
        }

        public virtual void Render ( )
        {
            throw new System.Exception();
        }

        public virtual void SetVisible(bool visible)
        {

        }

        public static Font s_Font;

        public static Material s_TextMeshMaterial;
        public static Material s_PrimitiveMaterial;
        public static Material s_PrimitiveMaterial_NoZTest;

        public static Mesh s_BoxMesh;
        public static Mesh s_ConeMesh;
        public static Mesh s_CrossMesh;
        public static Mesh s_PlaneMesh;
        public static Mesh s_Circle;
        public static Mesh s_CylinderMesh;
        public static Mesh s_SphereMesh;



        private static readonly Vector3[] s_CrossLinesVertices =  {
            new(-0.5f,0,0), new(0.5f,0,0),
            new(0,-0.5f,0), new(0,0.5f,0),
            new(0,0,-0.5f), new(0,0,0.5f)
            };

        public static GUISkin menuSkin;

        public static void Initialize()
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
            s_SphereMesh ??= PrimitiveMeshFactory.SphereWireframe(12,12);
            // Load materials
            s_PrimitiveMaterial = Resources.Load<Material>("XiDebugDraw/Materials/CjLib_Primitive");
            s_PrimitiveMaterial_NoZTest = Resources.Load<Material>("XiDebugDraw/Materials/CjLib_PrimitiveNoZTest");
        }

        private static Texture2D MakeTex(int width, int height, Color col)
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

        public static Mesh MakeLines(Vector3[] aVert)
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
        public static Mesh MakeLines(Vector3[] aVert, Color[] colors)
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

    
        protected static float s_wireframeZBias = 1.0e-4f;


        private static MaterialPropertyBlock s_materialProperties;
        protected static MaterialPropertyBlock GetMaterialPropertyBlock()
        {
            return (s_materialProperties != null) ? s_materialProperties : (s_materialProperties = new MaterialPropertyBlock());
        }
    }


    public class PrimitivesPool<T> where T : Primitive, new () 
    {
        LinkedList<Primitive> freeList = new LinkedList<Primitive> ( );
        LinkedList<Primitive> usedList = new LinkedList<Primitive>();

        public PrimitivesPool ( int size )
        {
            for ( var i = 0 ; i < size ; i++ )
            {
                object obj = new T();
                freeList.AddFirst ( ((Primitive)obj).link );
            }
        }

        public LinkedList<Primitive> UsedList => usedList;
        public LinkedList<Primitive> FreeList => freeList;
        public T Get ( )
        {
            if (freeList.Count == 0)
            {
                var o = new T();
                usedList.AddFirst(o.link);
                return o;
            }
            else
            {
                var node = freeList.First;
                node.List.Remove (node);
                var o = (T)(object)node.Value;
                usedList.AddFirst(node);
                return o;
            }
        }

        public void Release ( Primitive primitive )
        {
            if (primitive.link.List == usedList)
            {
                usedList.Remove(primitive.link);
                freeList.AddFirst(primitive.link);
            }
        }

        public int CountFree
        {
            get { return freeList.Count; }
        }

        public void Render(float dt)
        {
            var curent = usedList.First;
            while (curent != null)
            {
                var next = curent.Next;
                if (curent.Value.duration >= 0)
                {
                    curent.Value.Render();
                    curent.Value.duration -= dt;
                }
                else
                {
                    curent.Value.SetVisible(false);
                    usedList.Remove(curent);
                    freeList.AddFirst(curent);
                }
                curent = next;
            }
        }

        public void Clear()
        {
            var curent = usedList.First;
            while (curent != null)
            {
                var next = curent.Next;
                usedList.Remove(curent);
                curent = next;
            }
        }

        public string GetStatistics()
        {
            return string.Format("Used: {0} Free: {1}", UsedList.Count, FreeList.Count);
        }
    }
}


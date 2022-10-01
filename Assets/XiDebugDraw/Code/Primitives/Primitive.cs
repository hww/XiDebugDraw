using System.Collections.Generic;
using UnityEngine;

namespace XiDebugDraw.Primitives
{

    public class Primitive
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

        public string GetStatistics()
        {
            return string.Format("Used: {0} Free: {1}", UsedList.Count, FreeList.Count);
        }
    }
}


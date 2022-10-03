using UnityEngine;

namespace XiDebugDraw.Primitives
{
    public class Capsule : Primitive
    {
        public Vector3 position;
        public Quaternion rotation;
        public float radius;
        public float heigth;
        

        public void SetTransform(Vector3 position, Quaternion rotation, float radius)
        {
           // var size = maxCoords - minCoords;
           // var center = minCoords + (size * 0.5f);
           // matrix = Matrix4x4.TRS(center, Quaternion.identity, size);
        }

        public override void Render()
        {
      //     MaterialPropertyBlock materialProperties = GetMaterialPropertyBlock();
      //     materialProperties.SetVector("_Dimensions", new Vector4(1.0f, 1.0f, 1.0f, 0.0f));
      //     materialProperties.SetFloat("_ZBias", s_wireframeZBias);
      //     materialProperties.SetColor("_Color", color);
      //     Graphics.DrawMesh(s_BoxMesh, matrix, Primitive.s_PrimitiveMaterial, 0, null, 0, materialProperties, false, false, false);
        }


    //    public override void Render ( ) {
    //        Primitive.DrawCapsule(position, rotation, objectsPlacementRadius, heigth, color, depthEnabled);
    //    }
    }
}


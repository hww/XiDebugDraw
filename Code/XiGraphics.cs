using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace XiDebugDraw
{
	public class XiGraphics
	{
		public static Font m_Font;
		public static Mesh m_MeshBox;
		public static Mesh m_MeshCapsuleBody;
		public static Mesh m_MeshCapsuleCap;
		public static Mesh m_MeshCone;
		public static Mesh m_MeshCylinder;
		public static Mesh m_MeshDisc;
		public static Mesh m_MeshPyramid;
		public static Mesh m_MeshQuad;
		public static Mesh m_MeshRhombus;
		public static Mesh m_MeshSphere;

		public static Material m_XiDrawGLMaterial;
		public static Material m_XiDrawMeshMaterial;
		public static Material m_XiDrawMeshTransparendMaterial;
		public static Material m_XiDrawTextMeshMaterial;
		public static Material m_XiDrawGizmosMaterial;

		public static GUISkin menuSkin;

		public static void Initialize()
		{
			m_Font = Resources.Load<Font>("XiDebugDraw/Fonts/LiberationMono");
			m_XiDrawTextMeshMaterial = Resources.Load<Material>("XiDebugDraw/Materials/TextMeshMaterial");
			// Skin
			menuSkin = Resources.Load<GUISkin>("XiDebugDraw/Skins/Default Skin");
			menuSkin.box.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.7f));
			// Load materials
			m_XiDrawGLMaterial = Resources.Load<Material>("XiDebugDraw/Materials/XiDrawGLMaterial");
			m_XiDrawMeshMaterial = Resources.Load<Material>("XiDebugDraw/Materials/Wireframe-Transparent");
			m_XiDrawMeshTransparendMaterial = Resources.Load<Material>("XiDebugDraw/Materials/Wireframe-Transparent");
			m_XiDrawGizmosMaterial = Resources.Load<Material>("XiDebugDraw/Materials/XiGizmosMaterial");
			// Meshes
			m_MeshBox = Resources.Load<Mesh>("XiDebugDraw/Meshes/XiDrawBox");
			m_MeshCapsuleBody = Resources.Load<Mesh>("XiDebugDraw/Meshes/XiDrawCapsuleBody");
			m_MeshCapsuleCap = Resources.Load<Mesh>("XiDebugDraw/Meshes/XiDrawCapsuleCap");
			m_MeshCone = Resources.Load<Mesh>("XiDebugDraw/Meshes/XiDrawCone");
			m_MeshCylinder = Resources.Load<Mesh>("XiDebugDraw/Meshes/XiDrawCylinder");
			m_MeshDisc = Resources.Load<Mesh>("XiDebugDraw/Meshes/XiDrawDisc");
			m_MeshPyramid = Resources.Load<Mesh>("XiDebugDraw/Meshes/XiDrawPyramid");
			m_MeshQuad = Resources.Load<Mesh>("XiDebugDraw/Meshes/XiDrawQuad");
			m_MeshRhombus = Resources.Load<Mesh>("XiDebugDraw/Meshes/XiDrawRhombus");
			m_MeshSphere = Resources.Load<Mesh>("XiDebugDraw/Meshes/XiDrawSphere");
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
		const int Z_TEST_ENABLED = (int)CompareFunction.Less;
		const int Z_TEST_DISABLED = (int)CompareFunction.Disabled;
		static MaterialPropertyBlock block = new MaterialPropertyBlock();
		public static void DrawBox(Vector3 position, Quaternion rotation, float size, Color color, bool depthEnabled = true)
		{
			Matrix4x4 mat = Matrix4x4.TRS(position, rotation, size * Vector3.one);
			block.SetColor("_WireColor", color);
			block.SetInt("_ZTest", depthEnabled ? Z_TEST_ENABLED : Z_TEST_DISABLED);
			UnityEngine.Graphics.DrawMesh(m_MeshBox, mat, m_XiDrawMeshMaterial, 0, null, 0, block);
		}
		public static void DrawBox(Vector3 position, Quaternion rotation, Vector3 size, Color color, bool depthEnabled = true)
		{
			Matrix4x4 mat = Matrix4x4.TRS(position, rotation, size);
			block.SetColor("_WireColor", color);
			block.SetInt("_ZTest", depthEnabled ? Z_TEST_ENABLED : Z_TEST_DISABLED); 
			UnityEngine.Graphics.DrawMesh(m_MeshBox, mat, m_XiDrawMeshMaterial, 1, null, 0, block);
		}

		static readonly Quaternion Rotate180 = Quaternion.AngleAxis(180, Vector3.forward);

		public static void DrawCapsule(Vector3 position, Quaternion rotation, float radius, float heigth, Color color, bool depthEnabled = true)
		{
			Matrix4x4 mat = Matrix4x4.TRS(position, rotation, Vector3.one);
			block.SetColor("_WireColor", color);
			block.SetInt("_ZTest", depthEnabled ? Z_TEST_ENABLED : Z_TEST_DISABLED);

			
			var cylinderHeight = UnityEngine.Mathf.Max(0, heigth - radius - radius);
			if (cylinderHeight > 0)
			{
				Matrix4x4 mat1 = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(radius, cylinderHeight, radius));
				UnityEngine.Graphics.DrawMesh(m_MeshCapsuleBody, mat * mat1, m_XiDrawMeshMaterial, 0, null, 0, block);
			}
			Matrix4x4 mat2 = Matrix4x4.TRS(new Vector3(0, cylinderHeight * 0.5f, 0), Quaternion.identity, new Vector3(radius, radius, radius));
			UnityEngine.Graphics.DrawMesh(m_MeshCapsuleCap, mat * mat2, m_XiDrawMeshMaterial, 0, null, 0, block);
			Matrix4x4 mat3 = Matrix4x4.TRS(new Vector3(0, -cylinderHeight * 0.5f, 0), Rotate180, new Vector3(radius, radius, radius));
			UnityEngine.Graphics.DrawMesh(m_MeshCapsuleCap, mat * mat3, m_XiDrawMeshMaterial, 0, null, 0, block);
		}

		public static void DrawCone(Vector3 position, Quaternion rotation, Vector3 scale, Color color, bool depthEnabled = true)
		{
			Matrix4x4 mat = Matrix4x4.TRS(position, rotation, scale);
			block.SetColor("_WireColor", color);
			block.SetInt("_ZTest", depthEnabled ? Z_TEST_ENABLED : Z_TEST_DISABLED);
			UnityEngine.Graphics.DrawMesh(m_MeshCone, mat, m_XiDrawMeshMaterial, 0, null, 0, block);
		}
		public static void DrawCylinder(Vector3 position, Quaternion rotation, Vector3 scale, Color color, bool depthEnabled = true)
		{
			scale.y *= 2;
			Matrix4x4 mat = Matrix4x4.TRS(position, rotation, scale * 0.5f);
			block.SetColor("_WireColor", color);
			block.SetInt("_ZTest", depthEnabled ? Z_TEST_ENABLED : Z_TEST_DISABLED);
			UnityEngine.Graphics.DrawMesh(m_MeshCylinder, mat, m_XiDrawMeshMaterial, 0, null, 0, block);
		}
		public static void DrawDisc(Vector3 position, Quaternion rotation, Vector3 scale, Color color, bool depthEnabled = true)
		{
			Matrix4x4 mat = Matrix4x4.TRS(position, rotation, scale * 0.5f);
			block.SetColor("_WireColor", color);
			block.SetInt("_ZTest", depthEnabled ? Z_TEST_ENABLED : Z_TEST_DISABLED);
			UnityEngine.Graphics.DrawMesh(m_MeshDisc, mat, m_XiDrawMeshMaterial, 0, null, 0, block);
		}
		public static void DrawPyramid(Vector3 position, Quaternion rotation, Vector3 scale, Color color, bool depthEnabled = true)
		{
			Matrix4x4 mat = Matrix4x4.TRS(position, rotation, scale);
			block.SetColor("_WireColor", color);
			block.SetInt("_ZTest", depthEnabled ? Z_TEST_ENABLED : Z_TEST_DISABLED);
			UnityEngine.Graphics.DrawMesh(m_MeshPyramid, mat, m_XiDrawMeshMaterial, 0, null, 0, block);
		}
		public static void DrawQuad(Vector3 position, Quaternion rotation, Vector3 scale, Color color, bool depthEnabled = true)
		{
			Matrix4x4 mat = Matrix4x4.TRS(position, rotation, scale);
			block.SetColor("_WireColor", color);
			block.SetInt("_ZTest", depthEnabled ? Z_TEST_ENABLED : Z_TEST_DISABLED);
			UnityEngine.Graphics.DrawMesh(m_MeshQuad, mat, m_XiDrawMeshMaterial, 0, null, 0, block);
		}
		public static void DrawRhombus(Vector3 position, Quaternion rotation, Vector3 scale, Color color, bool depthEnabled = true)
		{
			Matrix4x4 mat = Matrix4x4.TRS(position, rotation, scale);
			block.SetColor("_WireColor", color);
			block.SetInt("_ZTest", depthEnabled ? Z_TEST_ENABLED : Z_TEST_DISABLED);
			UnityEngine.Graphics.DrawMesh(m_MeshRhombus, mat, m_XiDrawMeshMaterial, 0, null, 0, block);
		}
		public static void DrawSphere(Vector3 position, float radius, Color color, bool depthEnabled = true)
		{
			Matrix4x4 mat = Matrix4x4.TRS(position, Quaternion.identity, radius * Vector3.one);
			block.SetColor("_WireColor", color);
			block.SetInt("_ZTest", depthEnabled ? Z_TEST_ENABLED : Z_TEST_DISABLED);
			UnityEngine.Graphics.DrawMesh(m_MeshSphere, mat, m_XiDrawMeshMaterial, 0, null, 0, block);
		}


	}
}
using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace XiDebugDraw.Primitives
{
	public class Text : Primitive, IDisposable
	{
		public Vector3 position;
		public string text;
		public float size;

		private TextMesh textMesh;
		private Transform transform;

		public Text()
        {

		}

		~Text()
		{
			//Debug.LogWarning("~Text");

			GameObject.DestroyImmediate(transform.gameObject);
			textMesh = null;
			transform = null;
		}

		public override void Render ( )
        {
			transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
		}

		public override void SetVisible(bool visible)
		{
			if (visible)
            {
				if (transform==null) 
					Create();
				transform.position = position;
				textMesh.characterSize = size;
				textMesh.text = text;
				textMesh.color = color;
				transform.gameObject.SetActive(visible);
			}
			else
            {
				transform.gameObject.SetActive(visible);  
			}
		}

		public void Create()
		{
			UnityEngine.Debug.Assert(transform == null);
			//Debug.LogWarning("Create");
			var gameObject = new GameObject();
#if UNITY_EDITOR
			gameObject.name = "XiDebugDrawText";
			gameObject.hideFlags = HideFlags.HideInHierarchy;
#endif // UNITY_EDITOR
			transform = gameObject.transform;

			var meshRenderer = gameObject.AddComponent<MeshRenderer>();
			textMesh = gameObject.AddComponent<TextMesh>();

			meshRenderer.material = s_TextMeshMaterial;
			meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
			meshRenderer.receiveShadows = false;
			meshRenderer.sharedMaterial = s_TextMeshMaterial;
			meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
			meshRenderer.lightProbeUsage = LightProbeUsage.Off;
			meshRenderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;


            textMesh.font = s_Font;
			textMesh.richText = true;

			gameObject.SetActive(false);
		}

		public void Dispose()
		{
			//Debug.LogWarning("Dispose");
			if (transform != null)
				GameObject.DestroyImmediate(transform.gameObject);
			textMesh = null;
			transform = null;
		}

	}
}

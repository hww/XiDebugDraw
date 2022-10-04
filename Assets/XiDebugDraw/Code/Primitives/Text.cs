using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace XiDebugDraw.Primitives
{
	public class Text : Primitive, IDisposable
	{
		public Vector3 position;

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

		public void Init(Vector3 position, string text, Color color, float size, float duration, bool depthEnabled)
        {

			if (transform == null)
				Create();
			this.position = position;
			this.color = color;

			transform.position = position;
			textMesh.text = text;
			textMesh.characterSize = size;
			textMesh.color = color;
			this.duration = duration;
			this.depthEnabled = depthEnabled;
			transform.gameObject.SetActive(true);
		}
		public override void Deinit()
		{
			transform.gameObject.SetActive(false);  
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

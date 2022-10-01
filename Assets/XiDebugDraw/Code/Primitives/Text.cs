using UnityEngine;
using UnityEngine.Rendering;

namespace XiDebugDraw.Primitives
{
	public class Text : Primitive
	{
		public Vector3 position;
		public string text;
		public float size;

		GameObject gameObject;
		TextMesh textMesh;
		Transform transform;

		public Text()
        {
			Create();
		}

		~Text()
		{
			GameObject.Destroy(gameObject);
		}

		public override void Render ( )
        {
			transform.LookAt(Camera.main.transform);
		}

		public override void SetVisible(bool visible)
		{
			if (visible)
            {
				transform.position = position;
				textMesh.text = text;
				textMesh.characterSize = size;
				textMesh.color = color;
			}
			gameObject.SetActive(visible);
		}

		public void Create()
		{
			gameObject = new GameObject();
#if UNITY_EDITOR
			gameObject.name = "XiDebugDrawText";
			gameObject.hideFlags = HideFlags.HideInHierarchy;
#endif // UNITY_EDITOR
			transform = gameObject.transform;

			var meshRenderer = gameObject.AddComponent<MeshRenderer>();
			textMesh = gameObject.AddComponent<TextMesh>();

			meshRenderer.material = XiGraphics.m_XiDrawTextMeshMaterial;
			meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
			meshRenderer.receiveShadows = false;
			meshRenderer.sharedMaterial = XiGraphics.m_XiDrawTextMeshMaterial;
			meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
			meshRenderer.lightProbeUsage = LightProbeUsage.Off;
			meshRenderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;


			textMesh.font = XiGraphics.m_Font;
			textMesh.richText = true;

			gameObject.SetActive(false);
		}
	}
}

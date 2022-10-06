using System;
using UnityEngine;
using UnityEngine.Rendering;
using XiDebugDraw.Render;

namespace XiDebugDraw.Primitives
{
	public sealed class Text : Primitive
	{
		internal Vector3 position;
		internal string text;

		public Text()
        {

		}

		internal override void Render(Material material, MaterialPropertyBlock materialProperties)
		{
			GUIExtention.DrawString(text, position, color);
		}


		internal void Init(Vector3 position, string text, Color color, float size, float duration, bool depthEnabled)
        {

			this.position = position;
			this.text = text;
			this.color = color;
			this.duration = duration;
			this.depthEnabled = depthEnabled;
		}


	}
}

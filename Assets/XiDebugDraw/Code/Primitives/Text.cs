using System;
using UnityEngine;
using UnityEngine.Rendering;
using XiDebugDraw.Render;

namespace XiDebugDraw.Primitives
{
    /// <summary>A text. This class cannot be inherited.</summary>
	public sealed class Text : Primitive
	{
        /// <summary>The position.</summary>
		internal Vector3 position;
        /// <summary>The text.</summary>
		internal string text;

        /// <summary>Default constructor.</summary>
		public Text()
        {

		}

        ///--------------------------------------------------------------------
        /// <summary>Renders this object.</summary>
        ///
        /// <param name="material">          The material.</param>
        /// <param name="materialProperties">The material properties.</param>
        ///--------------------------------------------------------------------

		internal override void Render(Material material, MaterialPropertyBlock materialProperties)
		{
			GUIExtention.DrawString(text, position, color);
		}

        ///--------------------------------------------------------------------
        /// <summary>Initializes this object.</summary>
        ///
        /// <param name="position">    The position.</param>
        /// <param name="text">        The text.</param>
        /// <param name="color">       The color.</param>
        /// <param name="size">        The size.</param>
        /// <param name="duration">    The duration.</param>
        /// <param name="depthEnabled"> True to enable, false to disable the
        ///                             depth.</param>
        ///--------------------------------------------------------------------

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

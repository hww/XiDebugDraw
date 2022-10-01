Shader "hidden/XiDebugDraw/Wireframe-Global"
{	Properties
	{
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Int) = 4
	}
	SubShader
	{
		// Each color represents a meter.

		Tags { "RenderType"="Opaque" }

		Pass
		{
			// Wireframe shader based on the the following
			// http://developer.download.nvidia.com/SDK/10/direct3d/Source/SolidWireframe/Doc/SolidWireframe.pdf
                        ZTest [_ZTest]
			CGPROGRAM
			#pragma vertex vert
			#pragma geometry geom
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "../../Wireframe.cginc"

			ENDCG
		}
	}
}

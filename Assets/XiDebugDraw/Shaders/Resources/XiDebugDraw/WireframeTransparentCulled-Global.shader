Shader "hidden/XiDebugDraw/Wireframe-Transparent-Culled-Global"
{	Properties
	{
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Int) = 4
	}
	SubShader
	{
		// Each color represents a meter.

		Tags {
		"IgnoreProjector"="True"
		"Queue"="Transparent"
		"RenderType"="Opaque"
        }

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
 			Cull Back
            ZTest [_ZTest]

			// Wireframe shader based on the the following
			// http://developer.download.nvidia.com/SDK/10/direct3d/Source/SolidWireframe/Doc/SolidWireframe.pdf

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

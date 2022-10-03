using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Copyright (c) 2021 dr. ext (Vladimir Sigalkin) */

using UnityEngine;

namespace XiDebugDraw
{
    [ExecuteInEditMode]
	internal sealed class DebugHooksBehaviour : MonoBehaviour
	{
		#region Unity Methods

		private void Update() => DebugDrawManager.Render();

        void OnEnable() => DebugDrawManager.Initialize();

        void OnDisable() => DebugDrawManager.Deinitialize();

        #endregion
    }
}
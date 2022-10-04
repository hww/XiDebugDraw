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
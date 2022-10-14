using UnityEngine;

namespace XiDebugDraw
{
    /// <summary>A debug hooks behaviour. This class cannot be inherited.</summary>
    [ExecuteInEditMode]
	internal sealed class DebugHooksBehaviour : MonoBehaviour
	{
		#region Unity Methods

        /// <summary>Called every frame, if the MonoBehaviour is enabled.</summary>
		void Update() => DebugDrawManager.Render();

        /// <summary>Called when the object becomes enabled and active.</summary>
        void OnEnable() => DebugDrawManager.Initialize();

        ///--------------------------------------------------------------------
        /// <summary>Called when the behaviour becomes disabled or
        /// inactive.</summary>
        ///--------------------------------------------------------------------

        void OnDisable() => DebugDrawManager.Deinitialize();

        /// <summary>Called for rendering and handling GUI events.</summary>
        void OnGUI() => DebugDrawManager.OnGUI();


        #endregion
    }
}
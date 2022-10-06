using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace XiDebugDraw.Render
{
    public class GUIExtention
    {

        public static void DrawString(string text, Vector3 worldPos, Color? textColor = null, Color? backColor = null)
        {

            var restoreTextColor = GUI.color;
            var restoreBackColor = GUI.backgroundColor;

            GUI.color = textColor ?? Color.white;
            GUI.backgroundColor = backColor ?? new Color(0, 0, 0, 0.7f);

            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
            {
                GUI.color = restoreTextColor;
                UnityEditor.Handles.EndGUI();
                return;
            }
            var h = Camera.main.pixelHeight; //  view.position.height
            Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
            var r = new Rect(screenPos.x - (size.x / 2), -screenPos.y + h + 4, size.x, size.y);
            GUI.Box(r, "", EditorStyles.numberField);
            GUI.Label(r, text);
            GUI.color = restoreTextColor;
            GUI.backgroundColor = restoreBackColor;

        }
    }

    public class GizmosExtention
    {
        public static void DrawString(string text, Vector3 worldPos, Color? textColor = null, Color? backColor = null)
        {
            UnityEditor.Handles.BeginGUI();
            var restoreTextColor = GUI.color;
            var restoreBackColor = GUI.backgroundColor;

            GUI.color = textColor ?? Color.white;
            GUI.backgroundColor = backColor ?? Color.black;

            var view = UnityEditor.SceneView.currentDrawingSceneView;
            if (view != null && view.camera != null)
            {
                Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);
                if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
                {
                    GUI.color = restoreTextColor;
                    UnityEditor.Handles.EndGUI();
                    return;
                }
                Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
                var r = new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y);
                GUI.Box(r, text, EditorStyles.numberField);
                GUI.Label(r, text);
                GUI.color = restoreTextColor;
                GUI.backgroundColor = restoreBackColor;
            }
            UnityEditor.Handles.EndGUI();
        }
    }
}

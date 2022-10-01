using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XiDebugDraw;

public class DrawMeshesScene : MonoBehaviour
{
    public int quantity = 1;
    public float duration = 0;
    public bool zTest;
    public Color lineColor = Color.white;
    public Color fillColor = Color.white;

    // Start is called before the first frame update
    void OnEnable()
    {
        DebugDrawManager.Initialize();
        QualitySettings.vSyncCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        var position = transform.position;
        for (var i = 0; i < quantity; i++)
        {
            XiGraphics.DrawSphere(position, transform.localScale.x, lineColor, zTest);
            position.x += 2f;
            XiGraphics.DrawBox(position, transform.rotation, transform.localScale.x, lineColor, zTest);
            position.x += 2f;
            XiGraphics.DrawBox(position, transform.rotation, transform.localScale, lineColor, zTest);
            position.x += 2f;
            XiGraphics.DrawCapsule(position, transform.rotation, transform.localScale.x, transform.localScale.y, lineColor, zTest);
            position.x += 2f;
            XiGraphics.DrawCone(position, transform.rotation, transform.localScale, lineColor, zTest);
            position.x += 2f;
            XiGraphics.DrawCylinder(position, transform.rotation, transform.localScale, lineColor, zTest);
            position.x += 2f;
            XiGraphics.DrawDisc(position, transform.rotation, transform.localScale, lineColor, zTest);
            position.x += 2f;
            XiGraphics.DrawPyramid(position, transform.rotation, transform.localScale, lineColor, zTest);
            position.x += 2f;
            XiGraphics.DrawQuad(position, transform.rotation, transform.localScale, lineColor, zTest);
            position.x += 2f;
            XiGraphics.DrawRhombus(position, transform.rotation, transform.localScale, lineColor, zTest);
            position.x = transform.position.x;
            position.z += 2f;
        }
    }

}

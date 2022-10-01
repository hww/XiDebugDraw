using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XiDebugDraw;

public class DrawDebugItemsScene : MonoBehaviour
{
    public float duration = 0;
    public bool zTest;
    public Color lineColor = Color.white;

    // Start is called before the first frame update
    void OnEnable()
    {
        DebugDrawManager.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        DebugDrawManager.AddSphere(transform.position, transform.localScale.x, lineColor, duration, zTest);
        DebugDrawManager.AddBox(transform.position, transform.rotation, transform.localScale, lineColor, duration, zTest);
        DebugDrawManager.AddBox(transform.position, transform.rotation, transform.localScale, lineColor, duration, zTest);
        DebugDrawManager.AddCapsule(transform.position, transform.rotation, transform.localScale, lineColor, duration, zTest);
        DebugDrawManager.AddString(transform.position, "Test", lineColor, 0.1f, duration, zTest);

        DebugDrawManager.Render();
        //statistics = DebugDrawManager.GetStatistics(); 
        //DebugDrawManager.DrawCapsuleCap(transform.position, transform.rotation, transform.localScale, lineColor, zTest);
        //DebugDrawManager.AddCone(transform.position, transform.rotation, transform.localScale, lineColor, zTest);
        //DebugDrawManager.AddCylinder(transform.position, transform.rotation, transform.localScale, lineColor, zTest);
        //DebugDrawManager.AddDisc(transform.position, transform.rotation, transform.localScale, lineColor, zTest);
        //DebugDrawManager.AddPyramid(transform.position, transform.rotation, transform.localScale, lineColor, zTest);
        //DebugDrawManager.AddQuad(transform.position, transform.rotation, transform.localScale, lineColor, zTest);
        //DebugDrawManager.AddRhombus(transform.position, transform.rotation, transform.localScale, lineColor, zTest);
    }

    private void LateUpdate()
    {
 
    }
}

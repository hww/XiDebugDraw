using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XiDebugDraw;


public class DebugDrawDemoScene : MonoBehaviour
{
    public float objectsPlacementRadius = 1;
    public float rotationSpeed = 10f;
    public float movingUpSpeed = 1f;
    public float movingUpAmp = 10f;
    public float drawEach = 0.5f;
    public float drawDuration = 2f;

    private float drawAt;
    private float rotationTime;
    private float movingUpTime;


 
    private void Update()
    {
        var step = Mathf.Deg2Rad * 360.0f / transform.childCount;
        var angle = 0.0f;

        var draw = Time.time > drawAt;
        if (draw) 
            drawAt = Time.time + drawEach;

        for (var i = 0; i < transform.childCount; i++)
        {
            var item = transform.GetChild(i);
            item.transform.position = new Vector3(
                Mathf.Sin(angle + rotationTime) * objectsPlacementRadius,
                Mathf.Sin(angle + movingUpTime) * movingUpAmp,
                Mathf.Cos(angle + rotationTime) * objectsPlacementRadius);
            var compItem = item.GetComponent<DebugDrawDemoItem>();
            compItem.duration = drawDuration;
            if (draw)
                compItem?.Draw();
            angle += step;
        }
        rotationTime += Time.deltaTime * rotationSpeed;
        movingUpTime += Time.deltaTime * movingUpSpeed;
    }

    private void OnGUI()
    {
        int used, free;
        DebugDrawManager.GetStatistics(out used, out free);
        GUI.Label(new Rect(10,10,100,100), $"Used {used} of {free}");
    }
}

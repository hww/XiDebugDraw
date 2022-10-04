# XiDebugDraw _Debugging drawing tool for Unity 3D_

![](https://img.shields.io/badge/unity-2018.3%20or%20later-green.svg)
[![⚙ Build and Release](https://github.com/hww/XiDebugDraw/actions/workflows/ci.yml/badge.svg)](https://github.com/hww/XiDebugDraw/actions/workflows/ci.yml)
[![openupm](https://img.shields.io/npm/v/com.hww.xidebugdraw?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.hww.xidebugdraw/)
[![](https://img.shields.io/github/license/hww/XiDebugDraw.svg)](https://github.com/hww/XiDebugDraw/blob/master/LICENSE)
[![semantic-release: angular](https://img.shields.io/badge/semantic--release-angular-e10079?logo=semantic-release)](https://github.com/semantic-release/semantic-release)

![XiDebugDraw Title Image](/Documentation/title_image.png)
 
Collection of primitives for rendering debug info created by [hww](https://github.com/hww)


## Install

The package is available on the openupm registry. You can install it via openupm-cli.

```bash
openupm add com.hww.xidebugdraw
```
You can also install via git url by adding this entry in your manifest.json

```bash
"com.hww.xidebugdraw": "https://github.com/hww/XiDebugDraw.git#upm"
```

## Used Sources

- [Unity Wireframe Shaders](https://github.com/Chaser324/unity-wireframe) Great asset, but I decided do not use it while.
- [Unity3d Runtime Debug Draw](https://github.com/jagt/unity3d-runtime-debug-draw) Another great code, but I decided do not use it while.
- [CJ Lib - Utility Library for Unity](https://github.com/TheAllenChou/unity-cj-lib) Used by the XiDebutDraw 

## ToDo

- [ ] Add the gizmos API
- [ ] To eliminate dependency on Unity3d Runtime Debug Draw
- [ ] Add the line thicknes
- [ ] Add the spline renderer
- [ ] Performance optimization

## API


```C#
public static void AddLine(Vector3 fromPosition, Vector3 toPosition,  Color color,  float lineWidth = 1.0f, 
                           float duration = 0,bool depthEnabled = true);
public static void AddCross(Vector3 position, Color color, float size,
                            float duration = 0, bool depthEnabled = true);
public static void AddSphere(Vector3 position, float radius, Color color,
                             float duration = 0, bool depthEnabled = true);
public static void AddCircle(Vector3 position, Vector3 normal, float radius, Color color,
                             float duration = 0, bool depthEnabled = true);
public static void AddPlane(Vector3 position, Vector3 normal, float size, Color color,
                            float duration = 0, bool depthEnabled = true);
public static void AddAxes(Transform transform, Color color, float size,
                           float duration = 0, bool depthEnabled = true);
public static void AddTriangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2, Color color, float lineWidth,
                               float duration = 0, bool depthEnabled = true);
public static void AddBox(Vector3 position, Quaternion rotation, Vector3 size, Color color,
                          float duration = 0, bool depthEnabled = true);
public static void AddCone(Vector3 position, Quaternion rotation, float radius, float height, Color color,
                           float duration = 0, bool depthEnabled = true);
public static void AddCylinder(Vector3 position, Quaternion rotation, float radius, float height, Color color, 
                               float duration = 0, bool depthEnabled = true);

public static void AddCube(Vector3 position, Quaternion rotation, float size, Color color, 
                           float duration = 0, bool depthEnabled = true);

public static void AddRay(Vector3 position, Vector3 direction, float size, Color color, 
                          float duration = 0, bool depthEnabled = true);

public static void AddAABB(Vector3 minCoords, Vector3 maxCoord, Color color, float lineWidth, 
                           float duration = 0, bool depthEnabled = true);

public static void AddAOBB(Transform centerTransform, Vector3 scaleXYZ, Color color, float lineWidth, 
                           float duration = 0, bool depthEnabled = true);

public static void AddCapsule(Vector3 position, Quaternion roation, float radius, float height, Color color, 
                              float duration = 0, bool depthEnabled = true);

public static void AddString(Vector3 position, string text, Color color, float size = 0.1f, 
                             float duration = 0, bool depthEnabled = true);
```

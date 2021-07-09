using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class BuildTarget
{
    public static bool IsWebGLBuild()
    {
#if UNITY_WEBGL
        return true;
#endif
        return false;
    }
}
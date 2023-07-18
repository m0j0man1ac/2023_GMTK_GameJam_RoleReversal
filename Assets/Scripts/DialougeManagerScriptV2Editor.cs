using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(DialougeManagerScriptv2))]
public class DialougeManagerScriptV2Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var myscript = (DialougeManagerScriptv2)target;

        if (GUILayout.Button("Preview Dialouge (PlayModeOnly)"))
        {
            if (!Application.isPlaying) return;
            myscript.StartTextBubble("Preview dialouge.\nThis is gonna test what multiple lines can do to the height checking of my script");
        }
    }
}

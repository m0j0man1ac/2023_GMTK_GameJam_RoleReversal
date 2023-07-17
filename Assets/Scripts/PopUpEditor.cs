using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ScriptableObjects;
using UnityEngine.UIElements;

[CustomEditor(typeof(PopUpText))]
public class PopUpEditor : Editor
{
    string textToPreview;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PopUpText myScript = (PopUpText)target;

        GUILayout.BeginHorizontal();
        textToPreview = GUILayout.TextField(textToPreview);
        if (GUILayout.Button("Preview PopUp")) myScript.PreviewPopUp(textToPreview);
        GUILayout.EndHorizontal();
    }
}

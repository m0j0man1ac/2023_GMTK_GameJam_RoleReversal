using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ScriptableObjects;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(CardDisplay))]
public class CardDisplayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CardDisplay myScript = (CardDisplay)target;
        if (GUILayout.Button("Update Card")) myScript.CardUpdate(myScript.setCard);
    }
}

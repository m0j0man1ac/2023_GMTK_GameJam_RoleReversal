using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ScriptableObjects;
using UnityEngine.UIElements;

[CustomEditor(typeof(HeroAnimation))]
public class HeroAnimationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HeroAnimation myScript = (HeroAnimation)target;

        GUILayout.BeginHorizontal();
            if (GUILayout.Button("Idle")) ;
            if (GUILayout.Button("Attack Prep")) myScript.AttackPrep();
            if (GUILayout.Button("Hit")) myScript.Hit();
        GUILayout.EndHorizontal();
    }
}

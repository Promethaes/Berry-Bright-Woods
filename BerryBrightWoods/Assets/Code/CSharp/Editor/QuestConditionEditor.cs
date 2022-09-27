using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using System.Linq;
using UnityEditor.UIElements;

[CustomEditor(typeof(QuestCondition))]
public class QuestConditionEditor : Editor
{
   bool foldout = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
    }
}

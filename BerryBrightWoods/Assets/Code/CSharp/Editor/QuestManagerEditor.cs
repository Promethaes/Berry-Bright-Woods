using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(QuestManager))]
public class QuestManagerEditor : Editor
{
    List<Quest> allQuests = null;
    string[] questNames;
    int selection = -1;

    //kinda pointless
    public override void OnInspectorGUI()
    {
            base.OnInspectorGUI();
            return;
        //// GUILayout.Label("Select Quest");
        //// var tempSelection = GUILayout.SelectionGrid(selection == -1 ? 0 : selection, questNames, 4);
        //// if (tempSelection != selection)
        //// {
        ////     selection = tempSelection;
        ////     serializedObject.FindProperty("quest").objectReferenceValue = allQuests[selection];
        ////     serializedObject.ApplyModifiedProperties();
        ////     serializedObject.Update();
        //// }
        //// base.OnInspectorGUI();
    }

    public override VisualElement CreateInspectorGUI()
    {
        //// allQuests = Helper.FindScriptableObjectByType<Quest>();
        //// questNames = allQuests.Select(quest => quest.GetName()).ToArray();
        return base.CreateInspectorGUI();
    }
}
